using Android.Graphics;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Android
{
    public class ImageFormatFactory : IImageFormatFactory<Bitmap>
    {
        public IImageFormat<Bitmap> Create()
        {
            return new ImageFormat();
        }
    }
}