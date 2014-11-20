using Android.Graphics;
using Subsonic.Client.Exceptions;
using Subsonic.Common.Classes;
using Subsonic.Common.Enums;
using Subsonic.Common.Interfaces;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace Subsonic.Client.Android
{
    public class AndroidRequest<T> : SubsonicHttpRequest<T>
    {
        public AndroidRequest(SubsonicClient<T> client) : base(client) { }

        public override async Task<IImageFormat<T>> ImageRequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            var requestUri = BuildRequestUri(method, methodApiVersion, parameters);
            var request = BuildRequest(requestUri);
            var content = new MemoryStream();

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            try
            {
                using (var response = await request.GetResponseAsync())
                {
                    if (response != null)
                    {
                        if (response.ContentType == null || !response.ContentType.Contains(HttpContentTypes.TextXml))
                        {
                            if (cancelToken.HasValue)
                                cancelToken.Value.ThrowIfCancellationRequested();

                            using (var stream = response.GetResponseStream())
                                if (stream != null)
                                    await stream.CopyToAsync(content);
                        }
                        else
                        {
                            string restResponse = null;
                            var result = new Response();

                            if (cancelToken.HasValue)
                                cancelToken.Value.ThrowIfCancellationRequested();

                            using (var stream = response.GetResponseStream())
                                if (stream != null)
                                    using (var streamReader = new StreamReader(stream))
                                        restResponse = await streamReader.ReadToEndAsync();

                            if (!string.IsNullOrWhiteSpace(restResponse))
                                result = await restResponse.DeserializeFromXmlAsync<Response>();

                            if (result.ItemElementName == ItemChoiceType.Error)
                                throw new SubsonicErrorException("Error occurred during request.", result.Item as Error);

                            throw new SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "Unexpected response type: {0}", Enum.GetName(typeof(ItemChoiceType), result.ItemElementName)));
                        }
                    }
                }
            }
            catch (WebException wex)
            {
                var response = wex.Response as HttpWebResponse;

                if (response != null && response.StatusCode == HttpStatusCode.NotFound)
                    return null;
            }
            catch (Exception ex)
            {
                throw new SubsonicApiException(ex.Message, ex);
            }

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            content.Position = 0;
            var image = await BitmapFactory.DecodeStreamAsync(content);

            return new AndroidImageFormat(image) as IImageFormat<T>;
        }

        public override async Task<long> RequestAsync(string path, bool pathOverride, Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            long bytesTransferred = 0;
            var requestUri = BuildRequestUri(method, methodApiVersion, parameters);

            var request = BuildRequest(requestUri);
            var download = true;

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            try
            {
                using (var response = (HttpWebResponse)(await request.GetResponseAsync()))
                {
                    if (response != null)
                    {
                        if (!response.ContentType.Contains(HttpContentTypes.TextXml))
                        {
                            // Read the file name from the Content-Disposition header if a path override value was not provided
                            if (!pathOverride)
                            {
                                if (response.Headers.AllKeys.Contains(HttpHeaderField.ContentDisposition))
                                {
                                    var contentDisposition = new ContentDisposition(response.Headers[HttpHeaderField.ContentDisposition]);

                                    if (!string.IsNullOrWhiteSpace(contentDisposition.FileName))
                                        path = System.IO.Path.Combine(path, contentDisposition.FileName);
                                    else
                                        throw new SubsonicApiException("FileName was not provided in the Content-Disposition header, you must use the path override flag.");
                                }
                                else
                                {
                                    throw new SubsonicApiException("Content-Disposition header was not provided, you must use the path override flag.");
                                }
                            }
                        }
                        else
                        {
                            string restResponse = null;
                            var result = new Response();

                            if (cancelToken.HasValue)
                                cancelToken.Value.ThrowIfCancellationRequested();

                            using (var stream = response.GetResponseStream())
                                if (stream != null)
                                    using (var streamReader = new StreamReader(stream))
                                        restResponse = await streamReader.ReadToEndAsync();

                            if (!string.IsNullOrWhiteSpace(restResponse))
                                result = await restResponse.DeserializeFromXmlAsync<Response>();

                            if (result.ItemElementName == ItemChoiceType.Error)
                                throw new SubsonicErrorException("Error occurred during request.", result.Item as Error);

                            throw new SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "Unexpected response type: {0}", Enum.GetName(typeof(ItemChoiceType), result.ItemElementName)));
                        }

                        if (File.Exists(path))
                        {
                            var fileInfo = new FileInfo(path);

                            // If the file on disk matches the file on the server, do not attempt a download
                            if (response.ContentLength >= 0 && response.LastModified == fileInfo.LastWriteTime && response.ContentLength == fileInfo.Length)
                                download = false;
                        }

                        var directoryName = System.IO.Path.GetDirectoryName(path);

                        if (!string.IsNullOrWhiteSpace(directoryName) && !System.IO.Directory.Exists(directoryName))
                            System.IO.Directory.CreateDirectory(directoryName);

                        if (download)
                        {
                            if (cancelToken.HasValue)
                                cancelToken.Value.ThrowIfCancellationRequested();

                            using (var stream = response.GetResponseStream())
                            using (var fileStream = File.Open(path, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                            {
                                if (stream != null)
                                {
                                    if (cancelToken.HasValue)
                                        cancelToken.Value.ThrowIfCancellationRequested();

                                    await stream.CopyToAsync(fileStream);
                                    bytesTransferred = fileStream.Length;
                                }
                            }

                            File.SetLastWriteTime(path, response.LastModified);
                        }
                    }
                    else
                    {
                        throw new SubsonicErrorException("HTTP response is null");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SubsonicApiException(ex.Message, ex);
            }

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            return bytesTransferred;
        }

        /// <summary>
        /// Build an HTTP request using the values provided in the class.
        /// </summary>
        /// <param name="requestUri">URI for the request.</param>
        /// <param name="method"></param>
        /// <returns>HttpWebRequest</returns>
        protected override HttpWebRequest BuildRequest(Uri requestUri, string method = HttpMethod.Post)
        {
            HttpWebRequest request = base.BuildRequest(requestUri, method);

            // Add proxy information if specified, limit to valid ports
            if (!string.IsNullOrWhiteSpace(SubsonicClient.ProxyServerUrl) && (SubsonicClient.ProxyPort > 0 && SubsonicClient.ProxyPort < 65536))
            {
                var proxy = new WebProxy(SubsonicClient.ProxyServerUrl, SubsonicClient.ProxyPort);

                if (!string.IsNullOrWhiteSpace(SubsonicClient.ProxyUserName))
                {
                    if (string.IsNullOrWhiteSpace(SubsonicClient.ProxyPassword))
                        throw new SubsonicApiException("When specifying a proxy username, you must also specify a password.");

                    proxy.Credentials = new NetworkCredential(SubsonicClient.ProxyUserName, SubsonicClient.ProxyPassword);
                }

                request.Proxy = proxy;
            }

            return request;
        }
    }
}
