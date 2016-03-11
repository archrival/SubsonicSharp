using Subsonic.Common.Interfaces;
using System.Drawing;

namespace Subsonic.Client.Windows
{
    public class ImageFormatFactory : IImageFormatFactory<Image>
    {
        public IImageFormat<Image> Create()
        {
            return new ImageFormat();
        }
    }
}