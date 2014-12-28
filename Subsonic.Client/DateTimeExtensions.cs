using System;

namespace Subsonic.Client
{
    public static class DateTimeExtensions
    {
        public static DateTime DateTimeFromUnixTimestamp(long timestamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return dtDateTime.AddMilliseconds(timestamp);
        }
    }
}
