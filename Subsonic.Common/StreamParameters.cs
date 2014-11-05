using System;

namespace Subsonic.Common
{
    public class StreamParameters
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int BitRate { get; set; }

        public string ToHlsString(ref Version methodApiVersion)
        {
            if (Width > 0 && Height > 0)
                methodApiVersion = SubsonicApiVersions.Version190;

            return methodApiVersion == SubsonicApiVersions.Version190 ? string.Format("{0}@{1}", BitRate, ToString()) : BitRate.ToString();
        }

        public override string ToString()
        {
            return string.Format("{0}x{1}", Width, Height);
        }
    }
}
