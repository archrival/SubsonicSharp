using Subsonic.Common.Interfaces;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace Subsonic.Client.Windows
{
    public class ImageFormat : IImageFormat<Image>
    {
        public Image Image { get; set; }

        public async Task SetImageFromStreamAsync(Stream stream)
        {
            await Task.Run(() => Image = Image.FromStream(stream));
        }

        public void Dispose()
        {
            Image?.Dispose();
        }
    }
}
