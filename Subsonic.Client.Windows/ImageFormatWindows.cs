using Subsonic.Common.Interfaces;
using System.Drawing;
using System.IO;

namespace Subsonic.Client.Windows
{
    public class ImageFormatWindows : IImageFormat<Image>
    {
        private Image Image { get; set; }
        private Stream Stream { get; set; }

        public Image GetImage()
        {
            return Image;
        }

        public void SetImage(Image image)
        {
            Image = image;
        }

        public void SetImageFromStream(Stream stream)
        {
            Image = Image.FromStream(stream);
            Stream = stream;
        }

        public void Dispose()
        {
            if (Image != null)
                Image.Dispose();

            if (Stream == null) return;

            Stream.Close();
            Stream.Dispose();
        }
    }
}
