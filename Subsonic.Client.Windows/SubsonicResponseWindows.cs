using System;
using System.Threading;
using System.Threading.Tasks;
using Subsonic.Client.Interfaces;
using Subsonic.Common.Enums;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Windows
{
    public class SubsonicResponseWindows<T> : SubsonicResponse<T>
    {
        public SubsonicResponseWindows(ISubsonicServer subsonicServer) : base(subsonicServer, new SubsonicRequestWindows<T>(subsonicServer)) { }

        /// <summary>
        /// Get a response from the Subsonic server for the given method.
        /// </summary>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <param name="cancelToken"> </param>
        /// <returns cref="IImageFormat{T}"></returns>
        public override async Task<IImageFormat<T>> GetImageResponseAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            ValidateApiVersion(method, methodApiVersion);

            return await SubsonicRequest.ImageRequestAsync(method, methodApiVersion, parameters, cancelToken);
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
        public override async Task<long> GetResponseAsync(string path, bool pathOverride, Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            ValidateApiVersion(method, methodApiVersion);

            return await SubsonicRequest.RequestAsync(path, pathOverride, method, methodApiVersion, parameters, cancelToken);
        }
    }
}
