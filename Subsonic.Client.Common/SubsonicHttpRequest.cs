using Subsonic.Client.Exceptions;
using Subsonic.Client.Interfaces;
using Subsonic.Common.Classes;
using Subsonic.Common.Enums;
using Subsonic.Common.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Subsonic.Client
{
    public class SubsonicHttpRequest<T> : ISubsonicRequest<T>
    {
        private SubsonicClient<T> SubsonicClient { get; set; }

        public SubsonicHttpRequest(SubsonicClient<T> client)
        {
            SubsonicClient = client;
        }

        public virtual async Task<Response> RequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            Response result;
            var requestUri = BuildRequestUri(method, methodApiVersion, parameters);
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
                            throw new SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "HTTP response does not contain XML, content type is: {0}", response.ContentType));
                        }
                    }
                    else
                    {
                        throw new SubsonicErrorException("HTTP response is null");
                    }
                }

                result = DeserializeResponse(restResponse);
            }
            catch (Exception ex)
            {
                throw new SubsonicApiException(ex.Message, ex);
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
        public virtual Task<long> RequestAsync(string path, bool pathOverride, Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Make an async web request without waiting for a response
        /// </summary>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>long</returns>
        public virtual async Task<long> RequestAsyncNoResponse(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            var requestUri = BuildRequestUri(method, methodApiVersion, parameters);
            var request = BuildRequest(requestUri);

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            try
            {
                using (await request.GetResponseAsync()) { }
            }
            catch (Exception ex)
            {
                throw new SubsonicApiException(ex.Message, ex);
            }

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            return 0;
        }

        /// <summary>
        /// Build an HTTP request using the values provided in the class.
        /// </summary>
        /// <param name="requestUri">URI for the request.</param>
        /// <param name="method"></param>
        /// <returns>HttpWebRequest</returns>
        protected HttpWebRequest BuildRequest(Uri requestUri, string method = HttpMethod.Post)
        {
            var request = WebRequest.Create(requestUri) as HttpWebRequest;

            if (request != null)
            {
                request.Method = method;

                // Add credentials
                request.Credentials = new NetworkCredential(SubsonicClient.UserName, SubsonicClient.Password);

                // Add Authorization header
                var authInfo = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", SubsonicClient.UserName, SubsonicClient.Password);
                authInfo = Convert.ToBase64String(Encoding.UTF8.GetBytes(authInfo));
                request.Headers[HttpHeaderField.Authorization] = string.Format(CultureInfo.InvariantCulture, "Basic {0}", authInfo);
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
        public virtual async Task<long> ImageSizeRequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            var requestUri = BuildRequestUri(method, methodApiVersion, parameters);
            var request = BuildRequest(requestUri, HttpMethod.Get);
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
                    return length;
            }
            catch (Exception ex)
            {
                throw new SubsonicApiException(ex.Message, ex);
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
        public virtual Task<IImageFormat<T>> ImageRequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            throw new NotImplementedException();
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
                throw new SubsonicApiException("Empty HTTP response returned.");
            }

            return result;
        }

        /// <summary>
        /// Builds a URI to be used for the request.
        /// </summary>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <returns>string</returns>
        public virtual Uri BuildRequestUri(Methods method, Version methodApiVersion, SubsonicParameters parameters = null)
        {
            string request = string.Format(CultureInfo.InvariantCulture, "{0}/rest/{1}.view?v={2}&c={3}", SubsonicClient.ServerUrl, method.GetXmlEnumAttribute(), methodApiVersion, SubsonicClient.Name);

            if (parameters == null || parameters.Parameters.Count <= 0)
                return new Uri(request);

            foreach (var parameter in parameters.Parameters)
            {
                string key = string.Empty;
                string value = string.Empty;

                if (parameter is DictionaryEntry)
                {
                    var entry = (DictionaryEntry)parameter;

                    key = entry.Key.ToString();
                    value = entry.Value.ToString();
                }

                if (parameter is KeyValuePair<string, string>)
                {
                    var entry = (KeyValuePair<string, string>)parameter;

                    key = entry.Key;
                    value = entry.Value;
                }

                request += string.Format("&{0}={1}", Uri.EscapeDataString(key), Uri.EscapeDataString(value));
            }

            return new Uri(request);
        }

        /// <summary>
        /// Builds a URI to be used for the request.
        /// </summary>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <returns>string</returns>
        public virtual Uri BuildRequestUriUser(Methods method, Version methodApiVersion, SubsonicParameters parameters = null)
        {
            string encodedPassword = string.Format("enc:{0}", SubsonicClient.Password.ToHex());
            string request = string.Format("{0}/rest/{1}.view?v={2}&c={3}&u={4}&p={5}", SubsonicClient.ServerUrl, method.GetXmlEnumAttribute(), methodApiVersion, Uri.EscapeDataString(SubsonicClient.Name), SubsonicClient.UserName, encodedPassword);

            if (parameters != null && parameters.Parameters.Count > 0)
                request = parameters.Parameters.Cast<DictionaryEntry>().Aggregate(request, (current, parameter) => current + string.Format("&{0}={1}", Uri.EscapeDataString(parameter.Key.ToString()), Uri.EscapeDataString(parameter.Value.ToString())));

            return new Uri(request);
        }
    }
}
