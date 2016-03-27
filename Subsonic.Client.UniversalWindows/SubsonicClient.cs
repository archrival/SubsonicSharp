using Subsonic.Client.Interfaces;
using Subsonic.Common.Interfaces;
using Windows.UI.Xaml.Media.Imaging;

namespace Subsonic.Client.UniversalWindows
{
    public class SubsonicClient : SubsonicClient<SoftwareBitmapSource>
    {
        public SubsonicClient(ISubsonicServer subsonicServer, IImageFormatFactory<SoftwareBitmapSource> imageFormatFactory) : base(subsonicServer)
        {
            SubsonicResponse = new SubsonicResponse<SoftwareBitmapSource>(subsonicServer, imageFormatFactory);
        }
    }
}