using System;
using System.IO;

namespace Subsonic.Common.Interfaces
{
    /// <summary>
    /// Defines methods to store and retrieve platform specific image data.
    /// </summary>
    /// <typeparam name="T">Specifies the platform specific image format to be utilized.</typeparam>
    public interface IImageFormat<T> : IDisposable
    {
        T GetImage();
        void SetImage(T image);
        void SetImageFromStream(Stream stream);
    }
}