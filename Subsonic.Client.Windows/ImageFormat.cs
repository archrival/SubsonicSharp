using Subsonic.Common.Interfaces;
using System.Drawing;
using System.IO;

namespace Subsonic.Client.Windows
{
    public class ImageFormat : IImageFormat<Image>
    {
        private Stream Stream { get; set; }
        public Image Image { get; set; }

        public void SetImageFromStream(Stream stream)
        {
            Image = Image.FromStream(stream);
            Stream = stream;
        }

        public void Dispose()
        {
            Image?.Dispose();

            if (Stream == null) return;

            Stream.Close();
            Stream.Dispose();
        }
    }
}
