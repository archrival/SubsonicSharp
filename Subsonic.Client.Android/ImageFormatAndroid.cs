using Android.Graphics;
using Subsonic.Common.Interfaces;
using System.IO;

namespace Subsonic.Client.Android
{
    public class ImageFormatAndroid : IImageFormat<Bitmap>
    {
        private Bitmap Image { get; set; }
        private Stream Stream { get; set; }

        public Bitmap GetImage()
        {
            return Image;
        }

        public void SetImage(Bitmap image)
        {
            Image = image;
        }

        public void SetImageFromStream(Stream stream)
        {
            Image = BitmapFactory.DecodeStream(stream);
            Stream = stream;
        }

        public void Dispose()
        {
            if (Image != null) Image.Dispose();
            if (Stream == null) return;

            Stream.Close();
            Stream.Dispose();
        }
    }
}