using System.Security.AccessControl;
using Subsonic.Client.Common;
using Subsonic.Common;
using Subsonic.Common.Classes;
using Subsonic.Common.Enums;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Subsonic.Client.Windows
{
    public class HttpRequest : ISubsonicRequest
    {
        private SubsonicClient SubsonicClient { get; set; }
        private SubsonicRequest SubsonicRequest { get; set; }

        public HttpRequest(SubsonicClient client)
        {
            SubsonicClient = client;
            SubsonicRequest = new SubsonicRequest(client);
        }

        public async Task<Response> RequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            Response result;
            var requestUri = SubsonicRequest.BuildRequestUri(method, methodApiVersion, parameters);
            var request = BuildRequest(requestUri);

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            try
            {
                string restResponse = null;

                using (var response = await request.GetResponseAsync())
                {
                    if (response != null)
                    {
                        if (response.ContentType.StartsWith(HttpContentTypes.TextXml))
                        {
                            if (cancelToken.HasValue)
                                cancelToken.Value.ThrowIfCancellationRequested();

                            using (var stream = response.GetResponseStream())
                                if (stream != null)
                                    using (var streamReader = new StreamReader(stream))
                                        restResponse = await streamReader.ReadToEndAsync();
                        }
                        else
                        {
                            throw new Exceptions.SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "HTTP response does not contain XML, content type is: {0}", response.ContentType));
                        }
                    }
                    else
                    {
                        throw new Exceptions.SubsonicErrorException("HTTP response is null");
                    }
                }

                result = DeserializeResponse(restResponse);
            }
            catch (Exception ex)
            {
                throw new Exceptions.SubsonicApiException(ex.Message, ex);
            }

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            return result;
        }

        /// <summary>
        /// Save response to disk.
        /// </summary>
        /// <param name="path">Directory to save the response to, the filename is provided by the server using the Content-Disposition header.</param>
        /// <param name="pathOverride">If specified, the value of path becomes the complete path including filename.</param>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>long</returns>
        public async Task<long> RequestAsync(string path, bool pathOverride, Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            long bytesTransferred = 0;
            var requestUri = SubsonicRequest.BuildRequestUri(method, methodApiVersion, parameters);

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
                                        path = Path.Combine(path, contentDisposition.FileName);
                                    else
                                        throw new Exceptions.SubsonicApiException("FileName was not provided in the Content-Disposition header, you must use the path override flag.");
                                }
                                else
                                {
                                    throw new Exceptions.SubsonicApiException("Content-Disposition header was not provided, you must use the path override flag.");
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
                                result = restResponse.DeserializeFromXml<Response>();

                            if (result.ItemElementName == ItemChoiceType.Error)
                                throw new Exceptions.SubsonicErrorException("Error occurred during request.", result.Item as Error);

                            throw new Exceptions.SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "Unexpected response type: {0}", Enum.GetName(typeof(ItemChoiceType), result.ItemElementName)));
                        }

                        if (File.Exists(path))
                        {
                            var fileInfo = new FileInfo(path);

                            // If the file on disk matches the file on the server, do not attempt a download
                            if (response.ContentLength >= 0 && response.LastModified == fileInfo.LastWriteTime && response.ContentLength == fileInfo.Length)
                                download = false;
                        }

                        var directoryName = Path.GetDirectoryName(path);

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
                        throw new Exceptions.SubsonicErrorException("HTTP response is null");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exceptions.SubsonicApiException(ex.Message, ex);
            }

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            return bytesTransferred;
        }

        /// <summary>
        /// Make an async web request without waiting for a response
        /// </summary>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>long</returns>
        public async Task<long> RequestAsyncNoResponse(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            var requestUri = SubsonicRequest.BuildRequestUri(method, methodApiVersion, parameters);
            var request = BuildRequest(requestUri);

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            try
            {
                using (await request.GetResponseAsync()) { }
            }
            catch (Exception ex)
            {
                throw new Exceptions.SubsonicApiException(ex.Message, ex);
            }

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            return 0;
        }

        /// <summary>
        /// Return a Subsonic Response object for the given Subsonic method.
        /// </summary>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <returns>Response</returns>
        public Response Request(Methods method, Version methodApiVersion, SubsonicParameters parameters = null)
        {
            Response result;
            var requestUri = SubsonicRequest.BuildRequestUri(method, methodApiVersion, parameters);
            var request = BuildRequest(requestUri);

            try
            {
                string restResponse = null;

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (response != null)
                    {
                        if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Redirect)
                        {
                            if (response.ContentType.Contains(HttpContentTypes.TextXml))
                            {
                                using (var stream = response.GetResponseStream())
                                    if (stream != null)
                                        using (var streamReader = new StreamReader(stream))
                                            restResponse = streamReader.ReadToEnd();
                            }
                            else
                            {
                                throw new Exceptions.SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "HTTP response does not contain XML, content type is: {0}", response.ContentType));
                            }
                        }
                        else
                        {
                            throw new Exceptions.SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "Invalid HTTP response status code: {0}", response.StatusCode));
                        }
                    }
                    else
                    {
                        throw new Exceptions.SubsonicErrorException("HTTP response is null");
                    }
                }

                result = DeserializeResponse(restResponse);
            }
            catch (Exception ex)
            {
                throw new Exceptions.SubsonicApiException(ex.Message, ex);
            }

            return result;
        }

        /// <summary>
        /// Build an HTTP request using the values provided in the class.
        /// </summary>
        /// <param name="requestUri">URI for the request.</param>
        /// <param name="method"></param>
        /// <returns>HttpWebRequest</returns>
        private HttpWebRequest BuildRequest(Uri requestUri, string method = HttpMethod.POST)
        {
            var request = WebRequest.Create(requestUri) as HttpWebRequest;

            if (request != null)
            {
                request.UserAgent = SubsonicClient.Name;
                request.Method = method;

                // Add credentials
                request.Credentials = new NetworkCredential(SubsonicClient.UserName, SubsonicClient.Password);

                // Add Authorization header
                var authInfo = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", SubsonicClient.UserName, SubsonicClient.Password);
                authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
                request.Headers[HttpHeaderField.Authorization] = string.Format(CultureInfo.InvariantCulture, "Basic {0}", authInfo);

                // Add proxy information if specified, limit to valid ports
                if (!string.IsNullOrWhiteSpace(SubsonicClient.ProxyServerUrl) && (SubsonicClient.ProxyPort > 0 && SubsonicClient.ProxyPort < 65536))
                {
                    var proxy = new WebProxy(SubsonicClient.ProxyServerUrl, SubsonicClient.ProxyPort);

                    if (!string.IsNullOrWhiteSpace(SubsonicClient.ProxyUserName))
                    {
                        if (string.IsNullOrWhiteSpace(SubsonicClient.ProxyPassword))
                            throw new Exceptions.SubsonicApiException("When specifying a proxy username, you must also specify a password.");

                        proxy.Credentials = new NetworkCredential(SubsonicClient.ProxyUserName, SubsonicClient.ProxyPassword);
                    }

                    request.Proxy = proxy;
                }
            }

            return request;
        }

        /// <summary>
        /// Return an Image for the specified method.
        /// </summary>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>Image</returns>
        public async Task<long> ImageSizeRequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            var requestUri = SubsonicRequest.BuildRequestUri(method, methodApiVersion, parameters);
            var request = BuildRequest(requestUri, HttpMethod.GET);
            long length = -1;

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            try
            {
                using (var response = await request.GetResponseAsync())
                {
                    if (response != null)
                    {
                        if (!response.ContentType.Contains(HttpContentTypes.TextXml))
                        {
                            if (cancelToken.HasValue)
                                cancelToken.Value.ThrowIfCancellationRequested();

                            length = response.ContentLength;
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
                                        restResponse = streamReader.ReadToEnd();

                            if (!string.IsNullOrWhiteSpace(restResponse))
                                result = restResponse.DeserializeFromXml<Response>();

                            if (result.ItemElementName == ItemChoiceType.Error)
                                throw new Exceptions.SubsonicErrorException("Error occurred during request.", result.Item as Error);

                            throw new Exceptions.SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "Unexpected response type: {0}", Enum.GetName(typeof(ItemChoiceType), result.ItemElementName)));
                        }
                    }
                }
            }
            catch (WebException wex)
            {
                var response = wex.Response as HttpWebResponse;
                
                if (response != null && response.StatusCode == HttpStatusCode.NotFound)
                    return length;
            }
            catch (Exception ex)
            {
                throw new Exceptions.SubsonicApiException(ex.Message, ex);
            }

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            return length;
        }

        /// <summary>
        /// Return an Image for the specified method.
        /// </summary>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>Image</returns>
        public async Task<IImageFormat<Image>> ImageRequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            var requestUri = SubsonicRequest.BuildRequestUri(method, methodApiVersion, parameters);
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
                        if (!response.ContentType.Contains(HttpContentTypes.TextXml))
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
                                        restResponse = streamReader.ReadToEnd();

                            if (!string.IsNullOrWhiteSpace(restResponse))
                                result = restResponse.DeserializeFromXml<Response>();

                            if (result.ItemElementName == ItemChoiceType.Error)
                                throw new Exceptions.SubsonicErrorException("Error occurred during request.", result.Item as Error);

                            throw new Exceptions.SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "Unexpected response type: {0}", Enum.GetName(typeof(ItemChoiceType), result.ItemElementName)));
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
                throw new Exceptions.SubsonicApiException(ex.Message, ex);
            }

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            return new WindowsImageFormat(Image.FromStream(content));
        }

        /// <summary>
        /// Return an Image for the specified method.
        /// </summary>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <returns>Image</returns>
        public IImageFormat<Image> ImageRequest(Methods method, Version methodApiVersion, SubsonicParameters parameters = null)
        {
            var requestUri = SubsonicRequest.BuildRequestUri(method, methodApiVersion, parameters);
            var request = BuildRequest(requestUri);
            var image = default(Image);

            try
            {
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (response != null && (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Redirect))
                    {
                        if (!response.ContentType.Contains(HttpContentTypes.TextXml))
                        {
                            using (var stream = response.GetResponseStream())
                                if (stream != null) image = Image.FromStream(stream);
                        }
                        else
                        {
                            string restResponse = null;
                            var result = new Response();

                            using (var stream = response.GetResponseStream())
                                if (stream != null)
                                    using (var streamReader = new StreamReader(stream))
                                        restResponse = streamReader.ReadToEnd();

                            if (!string.IsNullOrWhiteSpace(restResponse))
                                result = restResponse.DeserializeFromXml<Response>();

                            if (result.ItemElementName == ItemChoiceType.Error)
                                throw new Exceptions.SubsonicErrorException("Error occurred during request.", result.Item as Error);

                            throw new Exceptions.SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "Unexpected response type: {0}", Enum.GetName(typeof(ItemChoiceType), result.ItemElementName)));
                        }
                    }
                    else
                    {
                        if (response != null)
                            throw new Exceptions.SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "Invalid HTTP response status code: {0}", response.StatusCode));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exceptions.SubsonicApiException(ex.Message, ex);
            }

            return new WindowsImageFormat(image);
        }

        /// <summary>
        /// Save response to disk.
        /// </summary>
        /// <param name="path">Directory to save the response to, the filename is provided by the server using the Content-Disposition header.</param>
        /// <param name="pathOverride">If specified, the value of path becomes the complete path including filename.</param>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <returns>long</returns>
        public long Request(string path, bool pathOverride, Methods method, Version methodApiVersion, SubsonicParameters parameters = null)
        {
            long bytesTransferred = 0;
            var requestUri = SubsonicRequest.BuildRequestUri(method, methodApiVersion, parameters);
            var request = BuildRequest(requestUri);
            var download = true;

            try
            {
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (response != null && (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Redirect))
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
                                        path = Path.Combine(path, contentDisposition.FileName);
                                    else
                                        throw new Exceptions.SubsonicApiException("FileName was not provided in the Content-Disposition header, you must use the path override flag.");
                                }
                                else
                                {
                                    throw new Exceptions.SubsonicApiException("Content-Disposition header was not provided, you must use the path override flag.");
                                }
                            }
                        }
                        else
                        {
                            string restResponse = null;
                            var result = new Response();

                            using (var stream = response.GetResponseStream())
                                if (stream != null)
                                    using (var streamReader = new StreamReader(stream))
                                        restResponse = streamReader.ReadToEnd();

                            if (!string.IsNullOrWhiteSpace(restResponse))
                                result = restResponse.DeserializeFromXml<Response>();

                            if (result.ItemElementName == ItemChoiceType.Error)
                                throw new Exceptions.SubsonicErrorException("Error occurred during request.", result.Item as Error);

                            throw new Exceptions.SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "Unexpected response type: {0}", Enum.GetName(typeof(ItemChoiceType), result.ItemElementName)));
                        }

                        if (File.Exists(path))
                        {
                            var fileInfo = new FileInfo(path);

                            // If the file on disk matches the file on the server, do not attempt a download
                            if (response.ContentLength >= 0 && response.LastModified == fileInfo.LastWriteTime && response.ContentLength == fileInfo.Length)
                                download = false;
                        }

                        var directoryName = Path.GetDirectoryName(path);

                        if (!string.IsNullOrWhiteSpace(directoryName) && !System.IO.Directory.Exists(directoryName))
                            System.IO.Directory.CreateDirectory(directoryName);

                        if (download)
                        {
                            using (var stream = response.GetResponseStream())
                            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
                            {
                                if (stream != null)
                                    stream.CopyTo(fileStream);

                                bytesTransferred = fileStream.Length;
                            }

                            File.SetLastWriteTime(path, response.LastModified);
                        }
                    }
                    else
                    {
                        if (response != null)
                            throw new Exceptions.SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "Invalid HTTP response status code: {0}", response.StatusCode));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exceptions.SubsonicApiException(ex.Message, ex);
            }

            return bytesTransferred;
        }

        private Response DeserializeResponse(string response)
        {
            Response result;

            if (!string.IsNullOrWhiteSpace(response))
            {
                result = response.DeserializeFromXml<Response>();

                if (SubsonicClient.ServerApiVersion == null)
                    SubsonicClient.ServerApiVersion = Version.Parse(result.Version);
            }
            else
            {
                throw new Exceptions.SubsonicApiException("Empty HTTP response returned.");
            }

            return result;
        }
    }
}
