﻿using System;
using Subsonic.Common.Interfaces;
using System.IO;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media.Imaging;

namespace Subsonic.Client.UniversalWindows
{
    public class ImageFormat : IImageFormat<SoftwareBitmapSource>
    {
        public SoftwareBitmapSource Image { get; set; }

        public async Task SetImageFromStreamAsync(Stream stream)
        {
            var decoder = await BitmapDecoder.CreateAsync(stream.AsRandomAccessStream()).AsTask();
            var bitmap = await decoder.GetSoftwareBitmapAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
            SoftwareBitmapSource image = new SoftwareBitmapSource();
            await image.SetBitmapAsync(bitmap);
            Image = image;
        }

        public void Dispose()
        {
            Image?.Dispose();
        }
    }
}