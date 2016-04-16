using Android.Graphics;
using Subsonic.Common.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Subsonic.Client.Android
{
    public class ImageFormat : IImageFormat<Bitmap>
    {
        public Bitmap Image { get; set; }
        private Stream Stream { get; set; }

        public async Task SetImageFromStreamAsync(Stream stream)
        {
            Stream = stream;
            Image = await BitmapFactory.DecodeStreamAsync(stream);
        }

        public void Dispose()
        {
            Stream?.Close();
            Stream?.Dispose();
            Image?.Dispose();
        }
    }
}