using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Subsonic.Client.Exceptions;
using Subsonic.Client.Interfaces;
using Subsonic.Common.Classes;
using Subsonic.Common.Enums;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client
{
    public class SubsonicHttpResponse<T> : ISubsonicResponse<T>
    {
        private ISubsonicServer SubsonicServer { get; set; }
        private ISubsonicRequest<T> SubsonicRequest { get; set; }

        protected SubsonicHttpResponse(ISubsonicServer subsonicServer)
        {
            SubsonicServer = subsonicServer;
            SubsonicRequest = new SubsonicHttpRequest<T>(subsonicServer);
        }

        /// <summary>
        /// Get a boolean response from the Subsonic server for the given method.
        /// </summary>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>bool</returns>
        public virtual async Task<bool> GetResponseAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            ValidateApiVersion(method, methodApiVersion);

            bool success = false;

            Response response = await SubsonicRequest.RequestAsync(method, methodApiVersion, parameters, cancelToken);

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
        public virtual Task<long> GetResponseAsync(string path, bool pathOverride, Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a boolean response from the Subsonic server for the given method.
        /// </summary>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>bool</returns>
        public virtual async Task<long> GetResponseAsyncNoResponse(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            ValidateApiVersion(method, methodApiVersion);

            return await SubsonicRequest.RequestAsyncNoResponse(method, methodApiVersion, parameters, cancelToken);
        }

        /// <summary>
        /// Get a response from the Subsonic server for the given method.
        /// </summary>
        /// <typeparam name="TResponse">Object type the method will return.</typeparam>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>T</returns>
        public virtual async Task<TResponse> GetResponseAsync<TResponse>(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            ValidateApiVersion(method, methodApiVersion);

            TResponse result = default(TResponse);

            Response response = await SubsonicRequest.RequestAsync(method, methodApiVersion, parameters, cancelToken);

            switch (response.Status)
            {
                case ResponseStatus.Ok:
                    result = (TResponse)response.Item;
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
        public virtual async Task<long> GetImageSizeAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            ValidateApiVersion(method, methodApiVersion);
            return await SubsonicRequest.ImageSizeRequestAsync(method, methodApiVersion, parameters, cancelToken);
        }

        /// <summary>
        /// Get a response from the Subsonic server for the given method.
        /// </summary>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>T</returns>
        public virtual async Task<IImageFormat<T>> GetImageResponseAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            ValidateApiVersion(method, methodApiVersion);
            return await SubsonicRequest.ImageRequestAsync(method, methodApiVersion, parameters, cancelToken);
        }

        /// <summary>
        /// Get a response from the Subsonic server for the given method.
        /// </summary>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <param name="cancelToken"></param>
        /// <returns>T</returns>
        public virtual async Task<string> GetStringResponseAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            ValidateApiVersion(method, methodApiVersion);
            return await SubsonicRequest.StringRequestAsync(method, methodApiVersion, parameters, cancelToken);
        }

        protected void ValidateApiVersion(Methods method, Version methodApiVersion)
        {
            if (SubsonicServer.GetApiVersion() != null && methodApiVersion > SubsonicServer.GetApiVersion())
                throw new SubsonicInvalidApiException(string.Format(CultureInfo.CurrentCulture, "Method {0} requires Subsonic Server API version {1}, but the actual Subsonic Server API version is {2}.", Enum.GetName(typeof(Methods), method), methodApiVersion, SubsonicServer.GetApiVersion()));
        }
    }
}
