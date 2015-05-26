using System;
using System.Threading;
using System.Threading.Tasks;
using Subsonic.Client.Interfaces;

namespace Subsonic.Client
{
	public class SubsonicActivityDelegate<T, TImageType> : ISubsonicActivityDelegate<T, TImageType>
	{
		public Func<CancellationToken?, Task<T>> Method { get; set; }

		public virtual async Task<T> GetResult(CancellationToken? cancelToken = null)
		{
			return await Method(cancelToken);
		}
	}
}