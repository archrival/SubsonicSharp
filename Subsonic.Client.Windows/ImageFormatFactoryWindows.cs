using Subsonic.Common.Interfaces;
using System.Drawing;

namespace Subsonic.Client.Windows
{
    public class ImageFormatFactoryWindows : IImageFormatFactory<Image>
    {
        public IImageFormat<Image> Create()
        {
            return new ImageFormatWindows();
        }
    }
}