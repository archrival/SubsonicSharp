﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Subsonic.Client.Interfaces;

namespace Subsonic.Client.Activities
{
	public class SubsonicActivity<T, TImageType> : ISubsonicActivity<T> where TImageType : class, IDisposable
	{
		private static readonly Lazy<Dictionary<ISubsonicActivityDelegate<T, TImageType>, Tuple<DateTime, T>>> Cache = new Lazy<Dictionary<ISubsonicActivityDelegate<T, TImageType>, Tuple<DateTime, T>>>(() => new Dictionary<ISubsonicActivityDelegate<T, TImageType>, Tuple<DateTime, T>>());
		private TimeSpan Timeout { get; set; }
		protected ISubsonicActivityDelegate<T, TImageType> ActivityDelegate { get; set; }

		protected SubsonicActivity()
		{
			Timeout = new TimeSpan(0, 30, 0);
		}

		public virtual bool IsAvailable()
		{
			return Cache.IsValueCreated;
		}

		public virtual async Task<T> GetResult(CancellationToken? cancelToken = null)
		{
			Tuple<DateTime, T> cache = null;

			if (Cache.Value.ContainsKey(ActivityDelegate))
				cache = Cache.Value[ActivityDelegate];

			DateTime timeStamp = new DateTime();
			T result = default(T);

			if (cache != null)
			{
				timeStamp = cache.Item1;
				result = cache.Item2;
			}

			if ((DateTime.Now - timeStamp) <= Timeout)
				return result;

			result = await ActivityDelegate.GetResult(cancelToken);

			Cache.Value[ActivityDelegate] = new Tuple<DateTime, T>(DateTime.Now, result);

			return result;
		}

		public virtual void SetTimeout(TimeSpan timeout)
		{
			Timeout = timeout;
		}

		public virtual void Invalidate()
		{
			Cache.Value.Clear();
		}
	}
}
