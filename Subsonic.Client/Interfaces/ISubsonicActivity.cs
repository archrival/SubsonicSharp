using System;
using System.Threading.Tasks;

namespace Subsonic.Client.Interfaces
{
    public interface ISubsonicActivity<T>
    {
        bool IsAvailable();
        Version GetApiVersion();
        Task<T> GetResult(params object[] param);
        void SetTimeout(TimeSpan timeout);
        void SetFunction(Func<object[], Task<T>> function);
        void Invalidate();
    }
}

