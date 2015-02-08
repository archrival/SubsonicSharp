namespace Subsonic.Common.Interfaces
{
    /// <summary>
    /// Defines methods to create platform specific image data.
    /// </summary>
    /// <typeparam name="T">Specifies the platform specific image format to be utilized.</typeparam>
    public interface IImageFormatFactory<T>
    {
        IImageFormat<T> Create();
    }
}