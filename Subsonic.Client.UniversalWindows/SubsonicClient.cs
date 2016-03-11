using Subsonic.Client.Interfaces;
using Subsonic.Common.Interfaces;
using Windows.Graphics.Imaging;

namespace Subsonic.Client.UniversalWindows
{
    public class SubsonicClient : SubsonicClient<SoftwareBitmap>
    {
        public SubsonicClient(ISubsonicServer subsonicServer, IImageFormatFactory<SoftwareBitmap> imageFormatFactory) : base(subsonicServer)
        {
            SubsonicResponse = new SubsonicResponse<SoftwareBitmap>(subsonicServer, imageFormatFactory);
        }
    }
}