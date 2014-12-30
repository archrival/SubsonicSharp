using System;

namespace Subsonic.Client
{
    public static class DateTimeExtensions
    {
        public static DateTime FromUnixTimestampInMilliseconds(this long timestamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return dtDateTime.AddMilliseconds(timestamp);
        }

        public static long ToUnixTimestampInMilliseconds(this DateTime target)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, target.Kind);
            return (long)(target - dtDateTime).TotalMilliseconds;
        }
    }
}
