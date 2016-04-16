using Subsonic.Client.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Subsonic.Client.Activities
{
    public class SubsonicActivityDelegate<T, TImageType> : ISubsonicActivityDelegate<T, TImageType> where TImageType : class, IDisposable
    {
        public Func<CancellationToken?, Task<T>> Method { get; set; }

        public virtual async Task<T> GetResult(CancellationToken? cancelToken = null)
        {
            return await Method(cancelToken);
        }
    }
}