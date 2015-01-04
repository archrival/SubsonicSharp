using System.Drawing;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Windows
{
    public class ImageFormat : IImageFormat<Image>
    {
        private Image Image { get; set; }

        public ImageFormat(Image image)
        {
            Image = image;
        }

        public Image GetImage()
        {
            return Image;
        }

        public void SetImage(Image image)
        {
            Image = image;
        }
    }
}
