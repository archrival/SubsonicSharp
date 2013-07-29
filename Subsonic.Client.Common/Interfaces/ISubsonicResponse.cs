using Subsonic.Common.Enums;
using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace Subsonic.Client.Common
{
    public interface ISubsonicResponse
    {
        Task<bool> GetResponseAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);

        Task<T> GetResponseAsync<T>(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);

        T GetResponse<T>(Methods method, Version methodApiVersion, SubsonicParameters parameters = null);

        bool GetResponse(Methods method, Version methodApiVersion, SubsonicParameters parameters = null);
    }
}
