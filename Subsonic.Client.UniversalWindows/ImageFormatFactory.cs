using Windows.Graphics.Imaging;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.UniversalWindows
{
    public class ImageFormatFactory : IImageFormatFactory<SoftwareBitmap>
    {
        public IImageFormat<SoftwareBitmap> Create()
        {
            return new ImageFormat();
        }
    }
}
