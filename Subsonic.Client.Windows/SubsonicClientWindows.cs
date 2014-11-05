using Subsonic.Common;
using Subsonic.Common.Enums;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace Subsonic.Client.Windows
{
    public class SubsonicClientWindows : SubsonicClient<Image>
    {
        public SubsonicClientWindows(Uri serverUrl, string userName, string password, string clientName) : base(serverUrl, userName, password, clientName)
        {
            var windowsResponse = new WindowsResponse<Image>(this);
            SubsonicResponse = windowsResponse;
            SubsonicRequest = windowsResponse.WindowsRequest;
        }

        public SubsonicClientWindows(Uri serverUrl, string userName, string password, string proxyServer, int proxyPort, string proxyUserName, string proxyPassword, string clientName) : base(serverUrl, userName, password, clientName)
        {
            var windowsResponse = new WindowsResponse<Image>(this);
            SubsonicResponse = windowsResponse;
            SubsonicRequest = windowsResponse.WindowsRequest;

            ProxyServerUrl = proxyServer;
            ProxyPort = proxyPort;
            ProxyUserName = proxyUserName;
            ProxyPassword = proxyPassword;
        }

        public override async Task<long> StreamAsync(string id, string path, StreamParameters streamParameters = null, StreamFormat? format = null, int? timeOffset = null, bool? estimateContentLength = null, CancellationToken? cancelToken = null, bool noResponse = false)
        {
            var methodApiVersion = Versions.Version120;

            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            if (streamParameters != null)
            {
                if (streamParameters.BitRate > 0)
                    parameters.Add(Constants.MaxBitRate, streamParameters.BitRate);

                if (streamParameters.Width > 0 && streamParameters.Height > 0)
                {
                    parameters.Add(Constants.Size, streamParameters);
                    methodApiVersion = Versions.Version160;
                }
            }

            if (format != null)
            {
                var streamFormatName = format.GetXmlEnumAttribute();

                if (streamFormatName != null)
                {
                    parameters.Add(Constants.StreamFormat, streamFormatName);
                    methodApiVersion = format == StreamFormat.Raw ? Versions.Version190 : Versions.Version160;
                }
            }

            if (timeOffset != null)
            {
                parameters.Add(Constants.TimeOffset, timeOffset);
                methodApiVersion = Versions.Version160;
            }

            if (estimateContentLength != null)
            {
                parameters.Add(Constants.EstimateContentLength, estimateContentLength);
                methodApiVersion = Versions.Version180;
            }

            if (noResponse)
                return await SubsonicResponse.GetResponseAsyncNoResponse(Methods.Stream, methodApiVersion, parameters, cancelToken);

            return await SubsonicResponse.GetResponseAsync(path, true, Methods.Stream, methodApiVersion, parameters, cancelToken);

        }

        public override async Task<long> DownloadAsync(string id, string path, bool pathOverride = false, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync(path, pathOverride, Methods.Download, Versions.Version100, parameters, cancelToken);
        }
    }
}
