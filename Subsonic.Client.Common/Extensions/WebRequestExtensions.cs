using System;
using System.Threading.Tasks;
using System.Net;

namespace Subsonic.Client
{
    internal static class WebRequestExtensions
    {
        internal static Task<WebResponse> GetResponseAsync(this WebRequest request, TimeSpan timeout)
        {
            return Task.Factory.StartNew<WebResponse>(() =>
                {
                    var t = Task.Factory.FromAsync<WebResponse>(
                        request.BeginGetResponse,
                        request.EndGetResponse,
                        null);

                    if (!t.Wait(timeout)) throw new TimeoutException();

                    return t.Result;
                });
        }

        internal static Task<WebResponse> GetResponseAsync(this WebRequest request)
        {
            return Task.Factory.StartNew<WebResponse>(() =>
                {
                    var t = Task.Factory.FromAsync<WebResponse>(
                        request.BeginGetResponse,
                        request.EndGetResponse,
                        null);

                    return t.Result;
                });
        }
    }
}

