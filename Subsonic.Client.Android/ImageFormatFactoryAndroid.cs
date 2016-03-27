using Android.Graphics;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Android
{
    public class ImageFormatFactoryAndroid : IImageFormatFactory<Bitmap>
    {
        public IImageFormat<Bitmap> Create()
        {
            return new ImageFormatAndroid();
        }
    }
}