using Android.Graphics;
using Subsonic.Common;
using Subsonic.Common.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Subsonic.Client.Android
{
    public class SubsonicClientAndroid : SubsonicClient<Bitmap>
    {
        public SubsonicClientAndroid(Uri serverUrl, string userName, string password, string clientName) : base(serverUrl, userName, password, clientName)
        {
            var androidResponse = new AndroidResponse<Bitmap>(this);
            SubsonicResponse = androidResponse;
            SubsonicRequest = androidResponse.AndroidRequest;
        }

        public SubsonicClientAndroid(Uri serverUrl, string userName, string password, string proxyServer, int proxyPort, string proxyUserName, string proxyPassword, string clientName) : base(serverUrl, userName, password, clientName)
        {
            var androidResponse = new AndroidResponse<Bitmap>(this);
            SubsonicResponse = androidResponse;
            SubsonicRequest = androidResponse.AndroidRequest;

            ProxyServerUrl = proxyServer;
            ProxyPort = proxyPort;
            ProxyUserName = proxyUserName;
            ProxyPassword = proxyPassword;
        }

        public override async Task<long> StreamAsync(string id, string path, StreamParameters streamParameters = null, StreamFormat? format = null, int? timeOffset = null, bool? estimateContentLength = null, CancellationToken? cancelToken = null, bool noResponse = false)
        {
            var methodApiVersion = SubsonicApiVersions.Version1_2_0;

            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            if (streamParameters != null)
            {
                if (streamParameters.BitRate > 0)
                    parameters.Add(Constants.MaxBitRate, streamParameters.BitRate);

                if (streamParameters.Width > 0 && streamParameters.Height > 0)
                {
                    parameters.Add(Constants.Size, streamParameters);
                    methodApiVersion = methodApiVersion.Max(SubsonicApiVersions.Version1_6_0);
                }
            }

            if (timeOffset != null)
            {
                parameters.Add(Constants.TimeOffset, timeOffset);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersions.Version1_6_0);
            }

            if (estimateContentLength != null)
            {
                parameters.Add(Constants.EstimateContentLength, estimateContentLength);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersions.Version1_8_0);
            }

            if (format != null)
            {
                var streamFormatName = format.GetXmlEnumAttribute();

                if (streamFormatName != null)
                {
                    parameters.Add(Constants.StreamFormat, streamFormatName);
                    methodApiVersion = format == StreamFormat.Raw ? methodApiVersion.Max(SubsonicApiVersions.Version1_9_0) : methodApiVersion.Max(SubsonicApiVersions.Version1_6_0);
                }
            }

            if (noResponse)
                return await SubsonicResponse.GetResponseAsyncNoResponse(Methods.Stream, methodApiVersion, parameters, cancelToken);

            return await SubsonicResponse.GetResponseAsync(path, true, Methods.Stream, methodApiVersion, parameters, cancelToken);

        }

        public override async Task<long> DownloadAsync(string id, string path, bool pathOverride = false, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync(path, pathOverride, Methods.Download, SubsonicApiVersions.Version1_0_0, parameters, cancelToken);
        }
    }
}
