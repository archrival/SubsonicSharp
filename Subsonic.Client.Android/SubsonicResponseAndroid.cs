using System;
using System.Threading;
using System.Threading.Tasks;
using Subsonic.Common.Enums;
using Subsonic.Common.Interfaces;
using Subsonic.Client.Interfaces;

namespace Subsonic.Client.Android
{
    public class SubsonicResponseAndroid<T> : SubsonicResponse<T>
    {
        public SubsonicResponseAndroid(ISubsonicServer subsonicServer, IImageFormatFactory<T> imageFormatFactory) : base(subsonicServer, new SubsonicRequestAndroid<T>(subsonicServer, imageFormatFactory)) { }

        public override async Task<long> GetResponseAsync(string path, bool pathOverride, Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            ValidateApiVersion(method, methodApiVersion);

            return await SubsonicRequest.RequestAsync(path, pathOverride, method, methodApiVersion, parameters, cancelToken);
        }
    }
}
