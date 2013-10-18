using Subsonic.Client.Common;
using Subsonic.Client.Common.Exceptions;
using Subsonic.Common;
using Subsonic.Common.Classes;
using Subsonic.Common.Enums;
using System;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Subsonic.Client.Windows
{
    public class HttpResponse : ISubsonicResponse
    {
        private SubsonicClient SubsonicClient { get; set; }
        private HttpRequest HttpRequest { get; set; }

        public HttpResponse(SubsonicClient client)
        {
            SubsonicClient = client;
            HttpRequest = new HttpRequest(client);
        }

        /// <summary>
        /// Get a boolean response from the Subsonic server for the given method.
        /// </summary>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>bool</returns>
        public async Task<bool> GetResponseAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            bool success = false;

            if (SubsonicClient.ServerApiVersion != null && methodApiVersion > SubsonicClient.ServerApiVersion)
                throw new SubsonicInvalidApiException(string.Format(CultureInfo.CurrentCulture, "Method {0} requires Subsonic Server API version {1}, but the actual Subsonic Server API version is {2}.", Enum.GetName(typeof (Methods), method), methodApiVersion, SubsonicClient.ServerApiVersion));

            Response response = await HttpRequest.RequestAsync(method, methodApiVersion, parameters, cancelToken);

            switch (response.Status)
            {
                case ResponseStatus.Ok:
                    success = true;
                    break;
                case ResponseStatus.Failed:
                    if (response.ItemElementName == ItemChoiceType.Error)
                        throw new SubsonicErrorException(string.Format(CultureInfo.CurrentCulture, "Error occurred in {0}", Enum.GetName(typeof (Methods), method)), response.Item as Error);

                    break;
            }

            return success;
        }

        /// <summary>
        /// Get a response and save it to a path from the Subsonic server for the given method.
        /// </summary>
        /// <param name="pathOverride"></param>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <param name="cancelToken"></param>
        /// <param name="path"></param>
        /// <returns>bool</returns>
        public async Task<long> GetResponseAsync(string path, bool pathOverride, Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            if (SubsonicClient.ServerApiVersion != null && methodApiVersion > SubsonicClient.ServerApiVersion)
                throw new SubsonicInvalidApiException(string.Format(CultureInfo.CurrentCulture, "Method {0} requires Subsonic Server API version {1}, but the actual Subsonic Server API version is {2}.", Enum.GetName(typeof(Methods), method), methodApiVersion, SubsonicClient.ServerApiVersion));

            return await HttpRequest.RequestAsync(path, pathOverride, method, methodApiVersion, parameters, cancelToken);
        }

        /// <summary>
        /// Get a boolean response from the Subsonic server for the given method.
        /// </summary>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>bool</returns>
        public async Task<long> GetResponseAsyncNoResponse(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            if (SubsonicClient.ServerApiVersion != null && methodApiVersion > SubsonicClient.ServerApiVersion)
                throw new SubsonicInvalidApiException(string.Format(CultureInfo.CurrentCulture, "Method {0} requires Subsonic Server API version {1}, but the actual Subsonic Server API version is {2}.", Enum.GetName(typeof(Methods), method), methodApiVersion, SubsonicClient.ServerApiVersion));

            return await HttpRequest.RequestAsyncNoResponse(method, methodApiVersion, parameters, cancelToken);
        }

        /// <summary>
        /// Get a boolean response from the Subsonic server for the given method.
        /// </summary>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <returns>bool</returns>
        public bool GetResponse(Methods method, Version methodApiVersion, SubsonicParameters parameters = null)
        {
            bool success = false;

            if (SubsonicClient.ServerApiVersion != null && methodApiVersion > SubsonicClient.ServerApiVersion)
                throw new SubsonicInvalidApiException(string.Format(CultureInfo.CurrentCulture, "Method {0} requires Subsonic Server API version {1}, but the actual Subsonic Server API version is {2}.", Enum.GetName(typeof(Methods), method), methodApiVersion, SubsonicClient.ServerApiVersion));

            Response response = HttpRequest.Request(method, methodApiVersion, parameters);

            switch (response.Status)
            {
                case ResponseStatus.Ok:
                    success = true;
                    break;
                case ResponseStatus.Failed:
                    if (response.ItemElementName == ItemChoiceType.Error)
                        throw new SubsonicErrorException(string.Format(CultureInfo.CurrentCulture, "Error occurred in {0}", Enum.GetName(typeof(Methods), method)), response.Item as Error);

                    break;
            }

            return success;
        }

        /// <summary>
        /// Get a boolean response from the Subsonic server for the given method.
        /// </summary>
        /// <param name="pathOverride"></param>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <param name="path"></param>
        /// <returns>bool</returns>
        public long GetResponse(string path, bool pathOverride, Methods method, Version methodApiVersion, SubsonicParameters parameters = null)
        {
            if (SubsonicClient.ServerApiVersion != null && methodApiVersion > SubsonicClient.ServerApiVersion)
                throw new SubsonicInvalidApiException(string.Format(CultureInfo.CurrentCulture, "Method {0} requires Subsonic Server API version {1}, but the actual Subsonic Server API version is {2}.", Enum.GetName(typeof(Methods), method), methodApiVersion, SubsonicClient.ServerApiVersion));

            return HttpRequest.Request(path, pathOverride, method, methodApiVersion, parameters);
        }

        /// <summary>
        /// Get a response from the Subsonic server for the given method.
        /// </summary>
        /// <typeparam name="T">Object type the method will return.</typeparam>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>T</returns>
        public async Task<T> GetResponseAsync<T>(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            T result = default(T);

            if (SubsonicClient.ServerApiVersion != null && methodApiVersion > SubsonicClient.ServerApiVersion)
                throw new SubsonicInvalidApiException(string.Format(CultureInfo.CurrentCulture, "Method {0} requires Subsonic Server API version {1}, but the actual Subsonic Server API version is {2}.", Enum.GetName(typeof(Methods), method), methodApiVersion, SubsonicClient.ServerApiVersion));

            Response response = await HttpRequest.RequestAsync(method, methodApiVersion, parameters, cancelToken);

            switch (response.Status)
            {
                case ResponseStatus.Ok:
                    result = (T)response.Item;
                    break;
                case ResponseStatus.Failed:
                    if (response.ItemElementName == ItemChoiceType.Error)
                        throw new SubsonicErrorException(string.Format(CultureInfo.CurrentCulture, "Error occurred in {0}", Enum.GetName(typeof(Methods), method)), response.Item as Error);

                    throw new SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "Unknown error occurred in {0}", Enum.GetName(typeof(Methods), method)));
            }

            return result;
        }

        /// <summary>
        /// Get a response from the Subsonic server for the given method.
        /// </summary>
        /// <typeparam name="T">Object type the method will return.</typeparam>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <returns>T</returns>
        public T GetResponse<T>(Methods method, Version methodApiVersion, SubsonicParameters parameters = null)
        {
            T result = default(T);

            if (SubsonicClient.ServerApiVersion != null && methodApiVersion > SubsonicClient.ServerApiVersion)
                throw new SubsonicInvalidApiException(string.Format(CultureInfo.CurrentCulture, "Method {0} requires Subsonic Server API version {1}, but the actual Subsonic Server API version is {2}.", Enum.GetName(typeof(Methods), method), methodApiVersion, SubsonicClient.ServerApiVersion));

            Response response = HttpRequest.Request(method, methodApiVersion, parameters);

            switch (response.Status)
            {
                case ResponseStatus.Ok:
                    result = (T)response.Item;
                    break;
                case ResponseStatus.Failed:
                    if (response.ItemElementName == ItemChoiceType.Error)
                        throw new SubsonicErrorException(string.Format(CultureInfo.CurrentCulture, "Error occurred in {0}", Enum.GetName(typeof(Methods), method)), response.Item as Error);

                    throw new SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "Unknown error occurred in {0}", Enum.GetName(typeof(Methods), method)));
            }

            return result;
        }

        /// <summary>
        /// Get a response from the Subsonic server for the given method.
        /// </summary>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>T</returns>
        public async Task<long> GetImageSizeAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            if (SubsonicClient.ServerApiVersion != null && methodApiVersion > SubsonicClient.ServerApiVersion)
                throw new SubsonicInvalidApiException(string.Format(CultureInfo.CurrentCulture, "Method {0} requires Subsonic Server API version {1}, but the actual Subsonic Server API version is {2}.", Enum.GetName(typeof(Methods), method), methodApiVersion, SubsonicClient.ServerApiVersion));

            return await HttpRequest.ImageSizeRequestAsync(method, methodApiVersion, parameters, cancelToken);
        }

        /// <summary>
        /// Get a response from the Subsonic server for the given method.
        /// </summary>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>T</returns>
        public async Task<IImageFormat<Image>> GetImageResponseAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            if (SubsonicClient.ServerApiVersion != null && methodApiVersion > SubsonicClient.ServerApiVersion)
                throw new SubsonicInvalidApiException(string.Format(CultureInfo.CurrentCulture, "Method {0} requires Subsonic Server API version {1}, but the actual Subsonic Server API version is {2}.", Enum.GetName(typeof(Methods), method), methodApiVersion, SubsonicClient.ServerApiVersion));

            return await HttpRequest.ImageRequestAsync(method, methodApiVersion, parameters, cancelToken);
        }

        /// <summary>
        /// Get a response from the Subsonic server for the given method.
        /// </summary>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <returns>T</returns>
        public IImageFormat<Image> GetImageResponse(Methods method, Version methodApiVersion, SubsonicParameters parameters = null)
        {
            if (SubsonicClient.ServerApiVersion != null && methodApiVersion > SubsonicClient.ServerApiVersion)
                throw new SubsonicInvalidApiException(string.Format(CultureInfo.CurrentCulture, "Method {0} requires Subsonic Server API version {1}, but the actual Subsonic Server API version is {2}.", Enum.GetName(typeof(Methods), method), methodApiVersion, SubsonicClient.ServerApiVersion));

            return HttpRequest.ImageRequest(method, methodApiVersion, parameters);
        }
    }
}
