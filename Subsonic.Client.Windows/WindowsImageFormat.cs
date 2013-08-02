using Subsonic.Common;
using System.Drawing;

namespace Subsonic.Client.Windows
{
    public class WindowsImageFormat : IImageFormat<Image>
    {
        private Image Image { get; set; }

        public WindowsImageFormat(Image image)
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
