using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Subsonic.Common.Classes;
using Subsonic.Common.Enums;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Interfaces
{
    public interface ISubsonicRequest<T>
    {
        Task<Response> RequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);
        Task<long> RequestAsync(string path, bool pathOverride, Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);
        Task<long> RequestAsyncNoResponse(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);
        Task<string> StringRequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);
        Task<IImageFormat<T>> ImageRequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);
        Task<long> ImageSizeRequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);
        Task<bool> SettingChangeRequestAsync(SettingMethods method, CancellationToken? cancelToken = null);
        Uri BuildRequestUri(Methods method, Version methodApiVersion, SubsonicParameters parameters = null);
        Uri BuildRequestUriUser(Methods method, Version methodApiVersion, SubsonicParameters parameters = null);
        Uri BuildSettingsRequestUri(SettingMethods method);
        HttpClientHandler GetClientHandler();
        HttpClient GetClient(HttpMessageHandler handler, bool addAuthentication = true);
    }
}
