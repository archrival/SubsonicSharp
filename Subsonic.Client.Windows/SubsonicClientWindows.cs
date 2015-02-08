using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using Subsonic.Client.Constants;
using Subsonic.Client.Extensions;
using Subsonic.Client.Interfaces;
using Subsonic.Common;
using Subsonic.Common.Enums;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Windows
{
    public class SubsonicClientWindows : SubsonicClient<Image>
    {
        public SubsonicClientWindows(ISubsonicServer subsonicServer, IImageFormatFactory<Image> imageFormatFactory) : base(subsonicServer)
        {
            SubsonicResponse = new SubsonicResponseWindows<Image>(subsonicServer, imageFormatFactory);
        }

        public override async Task<long> StreamAsync(string id, string path, StreamParameters streamParameters = null, StreamFormat? format = null, int? timeOffset = null, bool? estimateContentLength = null, CancellationToken? cancelToken = null, bool noResponse = false)
        {
            var methodApiVersion = SubsonicApiVersions.Version1_2_0;

            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);

            if (streamParameters != null)
            {
                if (streamParameters.BitRate > 0)
                    parameters.Add(ParameterConstants.MaxBitRate, streamParameters.BitRate);

                if (streamParameters.Width > 0 && streamParameters.Height > 0)
                {
                    parameters.Add(ParameterConstants.Size, streamParameters);
                    methodApiVersion = methodApiVersion.Max(SubsonicApiVersions.Version1_6_0);
                }
            }

            if (timeOffset != null)
            {
                parameters.Add(ParameterConstants.TimeOffset, timeOffset);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersions.Version1_6_0);
            }

            if (estimateContentLength != null)
            {
                parameters.Add(ParameterConstants.EstimateContentLength, estimateContentLength);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersions.Version1_8_0);
            }

            if (format != null)
            {
                var streamFormatName = format.GetXmlEnumAttribute();

                if (streamFormatName != null)
                {
                    parameters.Add(ParameterConstants.StreamFormat, streamFormatName);
                    methodApiVersion = format == StreamFormat.Raw ? methodApiVersion.Max(SubsonicApiVersions.Version1_9_0) : methodApiVersion.Max(SubsonicApiVersions.Version1_6_0);
                }
            }

            if (noResponse)
            {
                await SubsonicResponse.GetNoResponseAsync(Methods.Stream, methodApiVersion, parameters, cancelToken);
                return 0;
            }

            return await SubsonicResponse.GetResponseAsync(path, true, Methods.Stream, methodApiVersion, parameters, cancelToken);

        }

        public override async Task<long> DownloadAsync(string id, string path, bool pathOverride = false, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync(path, pathOverride, Methods.Download, SubsonicApiVersions.Version1_0_0, parameters, cancelToken);
        }
    }
}
