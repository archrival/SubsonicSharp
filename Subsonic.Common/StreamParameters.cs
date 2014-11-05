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
                methodApiVersion = Versions.Version190;

            return methodApiVersion == Versions.Version190 ? string.Format("{0}@{1}x{2}", BitRate, Width, Height) : BitRate.ToString();
        }

        public override string ToString()
        {
            return string.Format("{0}x{1}", Width, Height);
        }
    }
}
