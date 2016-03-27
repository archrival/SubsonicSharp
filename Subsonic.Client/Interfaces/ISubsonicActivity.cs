using System;
using System.Threading.Tasks;
using System.Threading;

namespace Subsonic.Client.Interfaces
{
    public interface ISubsonicActivity<T>
    {
        bool IsAvailable();
        Task<T> GetResult(CancellationToken? cancelToken = null);
        void SetTimeout(TimeSpan timeout);
        void Invalidate();
    }
}

