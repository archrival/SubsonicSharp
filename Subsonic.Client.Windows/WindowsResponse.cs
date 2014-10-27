using Subsonic.Client.Exceptions;
using Subsonic.Common.Enums;
using Subsonic.Common.Interfaces;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Subsonic.Client.Windows
{
    public class WindowsResponse<T> : SubsonicHttpResponse<T>
    {
        private SubsonicClient<T> SubsonicClient { get; set; }
        internal WindowsRequest<T> WindowsRequest { get; private set; }

        public WindowsResponse(SubsonicClient<T> client) : base(client)
        {
            SubsonicClient = client;
            WindowsRequest = new WindowsRequest<T>(client);
        }

        /// <summary>
        /// Get a response from the Subsonic server for the given method.
        /// </summary>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>T</returns>
        public override async Task<IImageFormat<T>> GetImageResponseAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            if (SubsonicClient.ServerApiVersion != null && methodApiVersion > SubsonicClient.ServerApiVersion)
                throw new SubsonicInvalidApiException(string.Format(CultureInfo.CurrentCulture, "Method {0} requires Subsonic Server API version {1}, but the actual Subsonic Server API version is {2}.", Enum.GetName(typeof(Methods), method), methodApiVersion, SubsonicClient.ServerApiVersion));

            return await WindowsRequest.ImageRequestAsync(method, methodApiVersion, parameters, cancelToken);
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
        public virtual async Task<long> GetResponseAsync(string path, bool pathOverride, Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            if (SubsonicClient.ServerApiVersion != null && methodApiVersion > SubsonicClient.ServerApiVersion)
                throw new SubsonicInvalidApiException(string.Format(CultureInfo.CurrentCulture, "Method {0} requires Subsonic Server API version {1}, but the actual Subsonic Server API version is {2}.", Enum.GetName(typeof(Methods), method), methodApiVersion, SubsonicClient.ServerApiVersion));

            return await WindowsRequest.RequestAsync(path, pathOverride, method, methodApiVersion, parameters, cancelToken);
        }

    }
}
