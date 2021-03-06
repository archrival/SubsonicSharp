﻿using Subsonic.Client.Constants;
using Subsonic.Client.Exceptions;
using Subsonic.Client.Interfaces;
using Subsonic.Common.Classes;
using Subsonic.Common.Enums;
using Subsonic.Common.Interfaces;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Subsonic.Client.Windows
{
    public class SubsonicRequest<T> : Client.SubsonicRequest<T> where T : class, IDisposable
    {
        public SubsonicRequest(ISubsonicServer subsonicServer, IImageFormatFactory<T> imageFormatFactory) : base(subsonicServer, imageFormatFactory)
        {
        }

        public override async Task<long> RequestAsync(string path, bool pathOverride, Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            var requestUri = SubsonicServer.BuildRequestUri(method, methodApiVersion, parameters);
            var clientHandler = GetClientHandler();
            var client = GetClient(clientHandler);

            cancelToken?.ThrowIfCancellationRequested();

            long bytesTransferred = 0;
            var download = true;

            try
            {
                using (var response = cancelToken.HasValue ? await client.GetAsync(requestUri, cancelToken.Value) : await client.GetAsync(requestUri))
                {
                    if (response.Content.Headers.ContentType != null && response.Content.Headers.ContentType.MediaType.Contains(HttpContentTypes.TextXml))
                    {
                        var stringResponse = await response.Content.ReadAsStringAsync();

                        if (stringResponse == null)
                            throw new SubsonicErrorException("HTTP response contains no content");

                        var result = await DeserializeResponseAsync(stringResponse);

                        if (result.ItemElementName == ItemChoiceType.Error)
                            throw new SubsonicErrorException("Error occurred during request.", result.Item as Error);

                        throw new SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "Unexpected response type: {0}", Enum.GetName(typeof(ItemChoiceType), result.ItemElementName)));
                    }

                    // Read the file name from the Content-Disposition header if a path override value was not provided
                    if (!pathOverride)
                    {
                        var contentDisposition = response.Content.Headers.ContentDisposition;

                        if (contentDisposition != null)
                        {
                            if (!string.IsNullOrWhiteSpace(contentDisposition.FileName))
                                path = Path.Combine(path, contentDisposition.FileName);
                            else
                                throw new SubsonicApiException("FileName was not provided in the Content-Disposition header, you must use the path override flag.");
                        }
                        else
                        {
                            throw new SubsonicApiException("Content-Disposition header was not provided, you must use the path override flag.");
                        }
                    }

                    var lastModified = response.Content.Headers.LastModified.GetValueOrDefault();

                    if (File.Exists(path))
                    {
                        var fileInfo = new FileInfo(path);

                        // If the file on disk matches the file on the server, do not attempt a download
                        if (response.Content.Headers.ContentLength >= 0 && response.Content.Headers.ContentLength == fileInfo.Length && response.Content.Headers.LastModified != null)
                            download = lastModified.LocalDateTime != fileInfo.LastWriteTime;
                    }

                    var directoryName = Path.GetDirectoryName(path);

                    if (!string.IsNullOrWhiteSpace(directoryName) && !System.IO.Directory.Exists(directoryName))
                        System.IO.Directory.CreateDirectory(directoryName);

                    if (download)
                    {
                        cancelToken?.ThrowIfCancellationRequested();

                        using (var fileStream = File.Open(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                        {
                            cancelToken?.ThrowIfCancellationRequested();

                            await response.Content.CopyToAsync(fileStream);
                            bytesTransferred = fileStream.Length;
                        }

                        if (response.Content.Headers.LastModified != null)
                            File.SetLastWriteTime(path, lastModified.LocalDateTime);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SubsonicApiException(ex.Message, ex);
            }

            cancelToken?.ThrowIfCancellationRequested();

            return bytesTransferred;
        }
    }
}