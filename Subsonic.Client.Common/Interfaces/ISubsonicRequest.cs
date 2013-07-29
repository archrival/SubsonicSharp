using Subsonic.Common.Classes;
using Subsonic.Common.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Subsonic.Client.Common
{
    public interface ISubsonicRequest
    {
        Task<Response> RequestAsync(Methods method, Version methodApiVersion, SubsonicParameters parameters = null, CancellationToken? cancelToken = null);
    }
}
