using System;
using Subsonic.Client.Interfaces;
using System.Collections.Generic;
using Subsonic.Common.Interfaces;
using System.Threading.Tasks;
using System.Linq;

namespace Subsonic.Client.Activities
{
    public class SubsonicActivity<T, TImageType> : ISubsonicActivity<T>
    {
        protected static readonly Lazy<Dictionary<string, Tuple<DateTime, T>>> Cache = new Lazy<Dictionary<string, Tuple<DateTime, T>>>(() => new Dictionary<string, Tuple<DateTime, T>>());
        protected TimeSpan Timeout { get; set; }
        protected ISubsonicClient<TImageType> SubsonicClient { get ; set;}
        protected Func<object[], Task<T>> Function { get; set; }
        private Version ApiVersion { get; set; }

        public SubsonicActivity(ISubsonicClient<TImageType> subsonicClient)
        {
            Timeout = new TimeSpan(0, 30, 0);
            SubsonicClient = subsonicClient;
            ApiVersion = Subsonic.Common.SubsonicApiVersions.Version1_0_0;
        }

        public virtual bool IsAvailable()
        {
            throw new NotImplementedException();
        }

        public virtual Version GetApiVersion()
        {
            return ApiVersion;
        }

        public virtual async Task<T> GetResult(params object[] param)
        {
            var cache = Cache.Value.Values.FirstOrDefault();

            DateTime timeStamp = new DateTime();
            T result = default(T);

            if (cache != null)
            {
                timeStamp = cache.Item1;
                result = cache.Item2;
            }

            if (DateTime.Now - timeStamp > Timeout)
            {
                result = await Function(param);

                Cache.Value.Clear();
                Cache.Value.Add(DateTime.Now.ToString(), new Tuple<DateTime, T>(DateTime.Now, result));
            }

            return result;
        }

        public virtual async Task<T> GetResult()
        {
            var cache = Cache.Value.Values.FirstOrDefault();

            DateTime timeStamp = new DateTime();
            T result = default(T);

            if (cache != null)
            {
                timeStamp = cache.Item1;
                result = cache.Item2;
            }

            if (DateTime.Now - timeStamp > Timeout)
            {
                result = await Function(null);

                Cache.Value.Clear();
                Cache.Value.Add(DateTime.Now.ToString(), new Tuple<DateTime, T>(DateTime.Now, result));
            }

            return result;
        }

        public virtual void SetApiVersion(Version version)
        {
            ApiVersion = version;
        }

        public virtual void SetTimeout(TimeSpan timeout)
        {
            Timeout = timeout;
        }

        public virtual void SetFunction(Func<object[], Task<T>> function)
        {
            Function = function;
        }

        public virtual void Invalidate()
        {
            Cache.Value.Clear();
        }
    }
}

