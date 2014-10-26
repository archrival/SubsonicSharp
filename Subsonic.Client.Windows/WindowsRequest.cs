using Subsonic.Client.Exceptions;
using Subsonic.Common.Classes;
using Subsonic.Common.Enums;
using Subsonic.Common.Interfaces;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Subsonic.Client.Windows
{
    public class WindowsRequest<T> : SubsonicHttpRequest<T>
    {
        public WindowsRequest(SubsonicClient<T> client) : base(client) { }

        /// <summary>
        /// Return an Image for the specified method.
        /// </summary>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>Image</returns>
        public override async Task<IImageFormat<T>> ImageRequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null)
        {
            var requestUri = BuildRequestUri(method, methodApiVersion, parameters);
            var request = BuildRequest(requestUri);
            var content = new MemoryStream();

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            try
            {
                using (var response = await request.GetResponseAsync())
                {
                    if (response != null)
                    {
                        if (!response.ContentType.Contains(HttpContentTypes.TextXml))
                        {
                            if (cancelToken.HasValue)
                                cancelToken.Value.ThrowIfCancellationRequested();

                            using (var stream = response.GetResponseStream())
                                if (stream != null)
                                    await stream.CopyToAsync(content);
                        }
                        else
                        {
                            string restResponse = null;
                            var result = new Response();

                            if (cancelToken.HasValue)
                                cancelToken.Value.ThrowIfCancellationRequested();

                            using (var stream = response.GetResponseStream())
                                if (stream != null)
                                    using (var streamReader = new StreamReader(stream))
                                        restResponse = streamReader.ReadToEnd();

                            if (!string.IsNullOrWhiteSpace(restResponse))
                                result = restResponse.DeserializeFromXml<Response>();

                            if (result.ItemElementName == ItemChoiceType.Error)
                                throw new SubsonicErrorException("Error occurred during request.", result.Item as Error);

                            throw new SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "Unexpected response type: {0}", Enum.GetName(typeof(ItemChoiceType), result.ItemElementName)));
                        }
                    }
                }
            }
            catch (WebException wex)
            {
                var response = wex.Response as HttpWebResponse;
                
                if (response != null && response.StatusCode == HttpStatusCode.NotFound)
                    return null;
            }
            catch (Exception ex)
            {
                throw new SubsonicApiException(ex.Message, ex);
            }

            if (cancelToken.HasValue)
                cancelToken.Value.ThrowIfCancellationRequested();

            return new WindowsImageFormat(Image.FromStream(content)) as IImageFormat<T>;

        }

        /// <summary>
        /// Return an Image for the specified method.
        /// </summary>
        /// <param name="method">Subsonic API method to call.</param>
        /// <param name="methodApiVersion">Subsonic API version of the method.</param>
        /// <param name="parameters">Parameters used by the method.</param>
        /// <returns>Image</returns>
        public override IImageFormat<T> ImageRequest(Methods method, Version methodApiVersion, SubsonicParameters parameters = null)
        {
            var requestUri = BuildRequestUri(method, methodApiVersion, parameters);
            var request = BuildRequest(requestUri);
            var image = default(Image);

            try
            {
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (response != null && (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Redirect))
                    {
                        if (!response.ContentType.Contains(HttpContentTypes.TextXml))
                        {
                            using (var stream = response.GetResponseStream())
                                if (stream != null) image = Image.FromStream(stream);
                        }
                        else
                        {
                            string restResponse = null;
                            var result = new Response();

                            using (var stream = response.GetResponseStream())
                                if (stream != null)
                                    using (var streamReader = new StreamReader(stream))
                                        restResponse = streamReader.ReadToEnd();

                            if (!string.IsNullOrWhiteSpace(restResponse))
                                result = restResponse.DeserializeFromXml<Response>();

                            if (result.ItemElementName == ItemChoiceType.Error)
                                throw new SubsonicErrorException("Error occurred during request.", result.Item as Error);

                            throw new SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "Unexpected response type: {0}", Enum.GetName(typeof(ItemChoiceType), result.ItemElementName)));
                        }
                    }
                    else
                    {
                        if (response != null)
                            throw new SubsonicApiException(string.Format(CultureInfo.CurrentCulture, "Invalid HTTP response status code: {0}", response.StatusCode));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SubsonicApiException(ex.Message, ex);
            }

            return new WindowsImageFormat(image) as IImageFormat<T>;
        }
    }
}
