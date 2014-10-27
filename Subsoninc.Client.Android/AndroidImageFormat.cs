using Android.Graphics;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Android
{
    public class AndroidImageFormat : IImageFormat<Bitmap>
    {
        private Bitmap Image { get; set; }

        public AndroidImageFormat(Bitmap image)
        {
            Image = image;
        }

        public Bitmap GetImage()
        {
            return Image;
        }

        public void SetImage(Bitmap image)
        {
            Image = image;
        }
    }
}
