using Subsonic.Client.Exceptions;
using Subsonic.Client.Interfaces;
using Subsonic.Common.Classes;
using Subsonic.Common.Enums;
using Subsonic.Common.Interfaces;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Subsonic.Client
{
    public class SubsonicResponse<T> : ISubsonicResponse<T>
    {
        private ISubsonicServer SubsonicServer { get; set; }
        public ISubsonicRequest<T> SubsonicRequest { get; private set; }

        protected SubsonicResponse(ISubsonicServer subsonicServer)
        {
            SubsonicServer = subsonicServer;
            SubsonicRequest = new SubsonicRequest<T>(subsonicServer);
        }

        protected SubsonicResponse(ISubsonicServer subsonicServer, ISubsonicRequest<T> subsonicRequest)
        {
            SubsonicServer = subsonicServer;
            SubsonicRequest = subsonicRequest;
        }

        /// <summary>
        /// Get a boolean response from the Subsonic server for the given method.
        /// </summary>
        /// <param name="method" cref="Methods">Subsonic API method to call.</param>
        /// <param name="methodApiVersion" cref="Version">Subsonic API version of the method.</param>
        /// <param name="parameters" cref="SubsonicParameters">Parameters used by the method.</param>
        /// <param name="cancelToken" cref="CancellationToken"></param>
        /// <returns cref="bool">Returns true on success</returns>
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
        /// <returns cref="long">Returns bytes transferred</returns>
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
        /// <returns cref="{TResponse}"></returns>
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

        /// <summary>
        /// Get a response from the Subsonic server for the given setting method.
        /// </summary>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>HttpWebResponse</returns>
        public virtual async Task<bool> GetSettingChangeResponseAsync(SettingMethods method, CancellationToken? cancelToken = null)
        {
            return await SubsonicRequest.SettingChangeRequestAsync(method, cancelToken);
        }

        protected void ValidateApiVersion(Methods method, Version methodApiVersion)
        {
            if (SubsonicServer.GetApiVersion() != null && methodApiVersion > SubsonicServer.GetApiVersion())
                throw new SubsonicInvalidApiException(string.Format(CultureInfo.CurrentCulture, "Method {0} requires Subsonic Server API version {1}, but the actual Subsonic Server API version is {2}.", Enum.GetName(typeof(Methods), method), methodApiVersion, SubsonicServer.GetApiVersion()));
        }
    }
}
