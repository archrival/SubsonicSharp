using Subsonic.Common.Interfaces;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace Subsonic.Client.Windows
{
    public class ImageFormat : IImageFormat<Image>
    {
        public Image Image { get; set; }
        private Stream Stream { get; set; }

        public void Dispose()
        {
            Stream?.Close();
            Stream?.Dispose();
            Image?.Dispose();
        }

        public async Task SetImageFromStreamAsync(Stream stream)
        {
            await Task.CompletedTask;

            Stream = stream;
            Image = Image.FromStream(stream);
        }
    }
}