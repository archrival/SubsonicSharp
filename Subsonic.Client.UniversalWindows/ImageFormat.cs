using Subsonic.Common.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;

namespace Subsonic.Client.UniversalWindows
{
    public class ImageFormat : IImageFormat<SoftwareBitmapSource>
    {
        public SoftwareBitmapSource Image { get; set; }
        private Stream Stream { get; set; }

        public async Task SetImageFromStreamAsync(Stream stream)
        {
            var decoder = await BitmapDecoder.CreateAsync(stream.AsRandomAccessStream()).AsTask();
            var bitmap = await decoder.GetSoftwareBitmapAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
            SoftwareBitmapSource image = new SoftwareBitmapSource();
            await image.SetBitmapAsync(bitmap);

            Stream = stream;
            Image = image;
        }

        public void Dispose()
        {
            Stream?.Dispose();
            Image?.Dispose();
        }
    }
}