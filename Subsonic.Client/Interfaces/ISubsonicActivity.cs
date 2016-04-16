using System;
using System.Threading;
using System.Threading.Tasks;

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