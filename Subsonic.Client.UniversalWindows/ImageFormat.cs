using System;
using Subsonic.Common.Interfaces;
using System.IO;
using Windows.Graphics.Imaging;

namespace Subsonic.Client.UniversalWindows
{
    public class ImageFormat : IImageFormat<SoftwareBitmap>
    {
        private Stream Stream { get; set; }
        public SoftwareBitmap Image { get; set; }

        public async void SetImageFromStream(Stream stream)
        {
            var decoder = await BitmapDecoder.CreateAsync(stream.AsRandomAccessStream()).AsTask();
            Image = await decoder.GetSoftwareBitmapAsync();
            Stream = stream;
        }

        public void Dispose()
        {
            Image?.Dispose();
            Stream?.Dispose();
        }
    }
}
