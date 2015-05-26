using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Subsonic.Client.Interfaces;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Activities
{
	public class SubsonicActivity<T, TImageType> : ISubsonicActivity<T>
    {
		protected static readonly Lazy<Dictionary<ISubsonicActivityDelegate<T, TImageType>, Tuple<DateTime, T>>> Cache = new Lazy<Dictionary<ISubsonicActivityDelegate<T, TImageType>, Tuple<DateTime, T>>>(() => new Dictionary<ISubsonicActivityDelegate<T, TImageType>, Tuple<DateTime, T>>());
        protected TimeSpan Timeout { get; set; }
        protected ISubsonicClient<TImageType> SubsonicClient { get ; set;}
		protected ISubsonicActivityDelegate<T, TImageType> ActivityDelegate { get; set; }

        public SubsonicActivity(ISubsonicClient<TImageType> subsonicClient)
        {
            Timeout = new TimeSpan(0, 30, 0);
            SubsonicClient = subsonicClient;
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

			if ((DateTime.Now - timeStamp) > Timeout)
            {
				result = await ActivityDelegate.GetResult(cancelToken);

                Cache.Value.Clear();
				Cache.Value.Add(ActivityDelegate, new Tuple<DateTime, T>(DateTime.Now, result));
            }

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
