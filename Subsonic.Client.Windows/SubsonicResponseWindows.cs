using Subsonic.Client.Interfaces;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Windows
{
    public class SubsonicResponseWindows<T> : SubsonicResponse<T>
    {
        public SubsonicResponseWindows(ISubsonicServer subsonicServer, IImageFormatFactory<T> imageFormatFactory) : base(subsonicServer, new SubsonicRequestWindows<T>(subsonicServer, imageFormatFactory)) { }
    }
}