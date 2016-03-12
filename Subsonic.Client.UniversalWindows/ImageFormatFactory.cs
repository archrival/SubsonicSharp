using Windows.UI.Xaml.Media.Imaging;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.UniversalWindows
{
    public class ImageFormatFactory : IImageFormatFactory<SoftwareBitmapSource>
    {
        public IImageFormat<SoftwareBitmapSource> Create()
        {
            return new ImageFormat();
        }
    }
}
