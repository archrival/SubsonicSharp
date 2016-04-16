using Subsonic.Common.Interfaces;
using Windows.UI.Xaml.Media.Imaging;

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