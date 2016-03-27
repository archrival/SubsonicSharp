using Subsonic.Client.Interfaces;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Android
{
    public class SubsonicResponseAndroid<T> : SubsonicResponse<T>
    {
        public SubsonicResponseAndroid(ISubsonicServer subsonicServer, IImageFormatFactory<T> imageFormatFactory) : base(subsonicServer, new SubsonicRequestAndroid<T>(subsonicServer, imageFormatFactory)) { }
    }
}