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

        public override async Task<IImageFormat<T>> GetImageResponseAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            ValidateApiVersion(method, methodApiVersion);

            return await SubsonicRequest.ImageRequestAsync(method, methodApiVersion, parameters, cancelToken);
        }

        public override async Task<long> GetResponseAsync(string path, bool pathOverride, Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            ValidateApiVersion(method, methodApiVersion);

            return await SubsonicRequest.RequestAsync(path, pathOverride, method, methodApiVersion, parameters, cancelToken);
        }
    }
}
