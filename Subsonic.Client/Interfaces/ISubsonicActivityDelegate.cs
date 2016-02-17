using System;
using System.Threading;
using System.Threading.Tasks;

namespace Subsonic.Client.Interfaces
{
	public interface ISubsonicActivityDelegate<T, TImageType> where TImageType : class, IDisposable
	{
		Task<T> GetResult(CancellationToken? cancelToken = null);
	}
}

