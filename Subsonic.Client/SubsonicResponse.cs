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
    public class SubsonicResponse<T> : ISubsonicResponse<T> where T : class, IDisposable
    {
        ISubsonicServer SubsonicServer { get; }
        ISubsonicRequest<T> SubsonicRequest { get; }

        protected SubsonicResponse(ISubsonicServer subsonicServer, ISubsonicRequest<T> subsonicRequest)
        {
            SubsonicServer = subsonicServer;
            SubsonicRequest = subsonicRequest;
        }

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

        public virtual async Task<long> GetResponseAsync(string path, bool pathOverride, Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            ValidateApiVersion(method, methodApiVersion);

            return await SubsonicRequest.RequestAsync(path, pathOverride, method, methodApiVersion, parameters, cancelToken);
        }

        public virtual async Task GetNoResponseAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            ValidateApiVersion(method, methodApiVersion);

            await SubsonicRequest.RequestWithoutResponseAsync(method, methodApiVersion, parameters, cancelToken);
        }

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

        public virtual async Task<long> GetContentLengthAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            ValidateApiVersion(method, methodApiVersion);
            return await SubsonicRequest.ContentLengthRequestAsync(method, methodApiVersion, parameters, cancelToken);
        }

        public virtual async Task<IImageFormat<T>> GetImageResponseAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            ValidateApiVersion(method, methodApiVersion);
            return await SubsonicRequest.ImageRequestAsync(method, methodApiVersion, parameters, cancelToken);
        }

        public virtual async Task<string> GetStringResponseAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            ValidateApiVersion(method, methodApiVersion);
            return await SubsonicRequest.StringRequestAsync(method, methodApiVersion, parameters, cancelToken);
        }

        public virtual async Task<bool> GetSettingChangeResponseAsync(SettingMethods method, CancellationToken? cancelToken = null)
        {
            return await SubsonicRequest.SettingChangeRequestAsync(method, cancelToken);
        }

        void ValidateApiVersion(Methods method, Version methodApiVersion)
        {
            if (SubsonicServer.ApiVersion != null && methodApiVersion > SubsonicServer.ApiVersion)
                throw new SubsonicInvalidApiException(string.Format(CultureInfo.CurrentCulture, "Method {0} requires Subsonic Server API version {1}, but the actual Subsonic Server API version is {2}.", Enum.GetName(typeof(Methods), method), methodApiVersion, SubsonicServer.ApiVersion));
        }
    }
}
