using System;
using System.Threading;
using System.Threading.Tasks;
using Subsonic.Common.Enums;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Interfaces
{
    public interface ISubsonicResponse<T>
    {
        Task<bool> GetResponseAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);
        Task<TResponse> GetResponseAsync<TResponse>(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);
        Task<string> GetStringResponseAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);
        Task<long> GetResponseAsync(string path, bool pathOverride, Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);
        Task<long> GetResponseAsyncNoResponse(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);
        Task<long> GetImageSizeAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);
        Task<IImageFormat<T>> GetImageResponseAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);
        Task<bool> GetSettingChangeResponseAsync(SettingMethods method, CancellationToken? cancelToken = null);
    }
}
