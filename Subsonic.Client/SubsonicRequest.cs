using Subsonic.Client.Constants;
using Subsonic.Client.Exceptions;
using Subsonic.Client.Extensions;
using Subsonic.Client.Interfaces;
using Subsonic.Common.Classes;
using Subsonic.Common.Enums;
using Subsonic.Common.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Subsonic.Client
{
    public class SubsonicRequest<T> : ISubsonicRequest<T>
    {
        protected ISubsonicServer SubsonicServer { private get; set; }

        public SubsonicRequest(ISubsonicServer subsonicServer)
        {
            SubsonicServer = subsonicServer;
        }

        /// <summary>
        /// Return Response for the specified method.
        /// </summary>
        /// <param name="method" cref="Methods">Subsonic API method to call.</param>
        /// <param name="methodApiVersion" cref="Version">Subsonic API version of the method.</param>
        /// <param name="parameters" cref="SubsonicParameters">Parameters used by the method.</param>
        /// <param name="cancelToken" cref="CancellationToken"></param>
        /// <returns cref="Response"></returns>
        public virtual async Task<Response> RequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            Response result;
            var requestUri = BuildRequestUri(method, methodApiVersion, parameters);
            var clientHandler = GetClientHandler();
            var client = GetClient(clientHandler);

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            try
            {
                using (var response = cancelToken.HasValue ? await client.PostAsync(requestUri, null, cancelToken.Value) : await client.PostAsync(requestUri, null))
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();

                    if (stringResponse == null) throw new SubsonicErrorException("HTTP response contains no content");

                    if (response.Content.Headers.ContentType.MediaType.Contains(HttpContentTypes.TextXml))
                        result = await DeserializeResponseAsync(stringResponse);
                    else
                        throw new SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "HTTP response does not contain XML, content type is: {0}", response.Content.Headers.ContentType.MediaType));
                }
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
        /// <param name="path" cref="string">Directory to save the response to, the filename is provided by the server using the Content-Disposition header.</param>
        /// <param name="pathOverride" cref="bool">If specified, the value of path becomes the complete path including filename.</param>
        /// <param name="method" cref="Methods">Subsonic API method to call.</param>
        /// <param name="methodApiVersion" cref="Version">Subsonic API version of the method.</param>
        /// <param name="parameters" cerf="SubsonicParameters">Parameters used by the method.</param>
        /// <param name="cancelToken" cref="CancellationToken"></param>
        /// <returns>long</returns>
        public virtual Task<long> RequestAsync(string path, bool pathOverride, Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Make an async web request without waiting for a response
        /// </summary>
        /// <param name="method" cref="Methods">Subsonic API method to call.</param>
        /// <param name="methodApiVersion" cref="Version">Subsonic API version of the method.</param>
        /// <param name="parameters" cref="SubsonicParameters">Parameters used by the method.</param>
        /// <param name="cancelToken" cref="CancellationToken"></param>
        /// <returns>long</returns>
        public virtual async Task<long> RequestAsyncNoResponse(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            var requestUri = BuildRequestUri(method, methodApiVersion, parameters);
            var clientHandler = GetClientHandler();
            var client = GetClient(clientHandler);

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            try
            {
                using (HttpRequestMessage message = new HttpRequestMessage(System.Net.Http.HttpMethod.Head, requestUri))
                using (var ignored = cancelToken.HasValue ? await client.SendAsync(message, cancelToken.Value) : await client.SendAsync(message)) { }
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
        /// Return a string for the specified method.
        /// </summary>
        /// <param name="method" cref="Methods">Subsonic API method to call.</param>
        /// <param name="methodApiVersion" cref="Version">Subsonic API version of the method.</param>
        /// <param name="parameters" cref="SubsonicParameters">Parameters used by the method.</param>
        /// <param name="cancelToken" cref="CancellationToken"></param>
        /// <returns cref="IImageFormat{T}"></returns>
        public virtual async Task<string> StringRequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            var requestUri = BuildRequestUri(method, methodApiVersion, parameters);
            var clientHandler = GetClientHandler();
            var client = GetClient(clientHandler);

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            try
            {
                using (var response = cancelToken.HasValue ? await client.PostAsync(requestUri, null, cancelToken.Value) : await client.PostAsync(requestUri, null))
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();

                    if (stringResponse == null) throw new SubsonicErrorException("HTTP response contains no content");

                    if (!response.Content.Headers.ContentType.MediaType.Contains(HttpContentTypes.TextXml))
                        return stringResponse;

                    Response result = await DeserializeResponseAsync(stringResponse);

                    if (result.ItemElementName == ItemChoiceType.Error)
                        throw new SubsonicErrorException("Error occurred during request.", result.Item as Error);

                    throw new SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "Unexpected response type: {0}", Enum.GetName(typeof(ItemChoiceType), result.ItemElementName)));
                }
            }
            catch (Exception ex)
            {
                throw new SubsonicApiException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Return an Image for the specified method.
        /// </summary>
        /// <param name="method" cref="Methods">Subsonic API method to call.</param>
        /// <param name="methodApiVersion" cref="Version">Subsonic API version of the method.</param>
        /// <param name="parameters" cref="SubsonicParameters">Parameters used by the method.</param>
        /// <param name="cancelToken" cref="CancellationToken"></param>
        /// <returns cref="IImageFormat{T}"></returns>
        public virtual Task<IImageFormat<T>> ImageRequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return an Image for the specified method.
        /// </summary>
        /// <param name="method" cref="Methods">Subsonic API method to call.</param>
        /// <param name="methodApiVersion" cref="Version">Subsonic API version of the method.</param>
        /// <param name="parameters" cref="SubsonicParameters">Parameters used by the method.</param>
        /// <param name="cancelToken" cref="CancellationToken"></param>
        /// <returns cref="long"></returns>
        public virtual async Task<long> ImageSizeRequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            var requestUri = BuildRequestUri(method, methodApiVersion, parameters);
            var clientHandler = GetClientHandler();
            var client = GetClient(clientHandler);

            long length = -1;

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            try
            {
                using (HttpRequestMessage message = new HttpRequestMessage(System.Net.Http.HttpMethod.Head, requestUri))
                using (HttpResponseMessage response = cancelToken.HasValue ? await client.SendAsync(message, cancelToken.Value) : await client.SendAsync(message))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "Unexpected response status code: {0}", Enum.GetName(typeof(ItemChoiceType), response.StatusCode)));

                    if (response.Content.Headers.ContentLength != null)
                        length = response.Content.Headers.ContentLength.GetValueOrDefault();
                }
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
        /// Execute a setting change request
        /// </summary>
        /// <param name="method" cref="SettingMethods">Subsonic settings method to call.</param>
        /// <param name="cancelToken" cref="CancellationToken"></param>
        /// <returns cref="bool"></returns>
        public virtual async Task<bool> SettingChangeRequestAsync(SettingMethods method, CancellationToken? cancelToken = null)
        {
            var requestUri = BuildSettingsRequestUri(method);
            var clientHandler = GetClientHandler();
            var client = GetClient(clientHandler);

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            bool success;

            try
            {
                using (HttpResponseMessage response = cancelToken.HasValue ? await client.GetAsync(requestUri, cancelToken.Value) : await client.GetAsync(requestUri))
                    success = response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new SubsonicApiException(ex.Message, ex);
            }

            return success;
        }

        /// <summary>
        /// Builds a URI to be used for the request.
        /// </summary>
        /// <param name="method" cref="Methods">Subsonic API method to call.</param>
        /// <param name="methodApiVersion" cref="Version">Subsonic API version of the method.</param>
        /// <param name="parameters" cref="SubsonicParameters">Parameters used by the method.</param>
        /// <returns cref="Uri"></returns>
        public virtual Uri BuildRequestUri(Methods method, Version methodApiVersion, SubsonicParameters parameters = null)
        {
            UriBuilder uriBuilder = new UriBuilder(SubsonicServer.GetUrl());

            StringBuilder pathBuilder = new StringBuilder(uriBuilder.Path);
            pathBuilder.AppendFormat("/rest/{0}.view", method.GetXmlEnumAttribute());
            uriBuilder.Path = Regex.Replace(pathBuilder.ToString(), "/+", "/");

            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.AppendFormat("v={0}&c={1}", methodApiVersion, SubsonicServer.GetClientName());

            if (parameters != null && parameters.Parameters.Count > 0)
            {
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

                    queryBuilder.AppendFormat("&{0}={1}", Uri.EscapeDataString(key), Uri.EscapeDataString(value));
                }
            }

            uriBuilder.Query = queryBuilder.ToString();
            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds a URI to be used for the request.
        /// </summary>
        /// <param name="method" cref="Methods">Subsonic API method to call.</param>
        /// <param name="methodApiVersion" cref="Version">Subsonic API version of the method.</param>
        /// <param name="parameters" cref="SubsonicParameters">Parameters used by the method.</param>
        /// <returns cref="Uri"></returns>
        public virtual Uri BuildRequestUriUser(Methods method, Version methodApiVersion, SubsonicParameters parameters = null)
        {
            UriBuilder uriBuilder = new UriBuilder(BuildRequestUri(method, methodApiVersion, parameters));

            StringBuilder queryBuilder = new StringBuilder(uriBuilder.Query);
            string encodedPassword = string.Format("enc:{0}", SubsonicServer.GetPassword().ToHex());
            queryBuilder.AppendFormat("&u={0}&p={1}", SubsonicServer.GetUserName(), encodedPassword);

            uriBuilder.Query = queryBuilder.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Builds a URI to be used for the request.
        /// </summary>
        /// <param name="method" cref="SettingMethods">Subsonic API method to call.</param>
        /// <returns cref="Uri"></returns>
        public virtual Uri BuildSettingsRequestUri(SettingMethods method)
        {
            UriBuilder uriBuilder = new UriBuilder(SubsonicServer.GetUrl())
            {
                UserName = SubsonicServer.GetUserName(),
                Password = SubsonicServer.GetPassword()
            };

            StringBuilder pathBuilder = new StringBuilder(uriBuilder.Path);
            pathBuilder.Append("/musicFolderSettings.view");
            uriBuilder.Path = Regex.Replace(pathBuilder.ToString(), "/+", "/");

            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.AppendFormat("{0}", method.GetXmlEnumAttribute());

            uriBuilder.Query = queryBuilder.ToString();

            return uriBuilder.Uri;
        }

        /// <summary>
        /// Get the default HttpClientHandler
        /// </summary>
        /// <returns cref="HttpClientHandler"></returns>
        public virtual HttpClientHandler GetClientHandler()
        {
            return new HttpClientHandler
            {
                Credentials = new NetworkCredential(SubsonicServer.GetUserName(), SubsonicServer.GetPassword()),
                PreAuthenticate = true,
                UseCookies = false,
                AllowAutoRedirect = true,
                Proxy = SubsonicServer.GetProxy(),
                UseProxy = SubsonicServer.GetProxy() != null
            };
        }

        /// <summary>
        /// Get the default HttpClient
        /// </summary>
        /// <param name="handler" cref="HttpMessageHandler">Handler containing common HttpClient settings (Proxy, Credentials, etc.).</param>
        /// <param name="addAuthentication"></param>/// 
        /// <returns cref="HttpClient">Add basic authentication header to every request.</returns>
        public virtual HttpClient GetClient(HttpMessageHandler handler, bool addAuthentication = true)
        {
            var httpClient = new HttpClient(handler);

            if (addAuthentication)
                httpClient.DefaultRequestHeaders.Add(HttpHeaderField.Authorization, GetAuthorizationHeader());

            httpClient.DefaultRequestHeaders.Add(HttpHeaderField.UserAgent, SubsonicServer.GetClientName());

            return httpClient;
        }

        private string GetAuthorizationHeader()
        {
            var authInfo = string.Format(CultureInfo.InvariantCulture, "{0}:{1}", SubsonicServer.GetUserName(), SubsonicServer.GetPassword());
            authInfo = Convert.ToBase64String(Encoding.UTF8.GetBytes(authInfo));
            return string.Format(CultureInfo.InvariantCulture, "Basic {0}", authInfo);
        }

        protected async Task<Response> DeserializeResponseAsync(string response)
        {
            Response result;

            if (!string.IsNullOrWhiteSpace(response))
            {
                result = await response.DeserializeFromXmlAsync<Response>();

                if (SubsonicServer.GetApiVersion() == null)
                    SubsonicServer.SetApiVersion(Version.Parse(result.Version));
            }
            else
            {
                throw new SubsonicApiException("Empty HTTP response returned.");
            }

            return result;
        }
    }
}
