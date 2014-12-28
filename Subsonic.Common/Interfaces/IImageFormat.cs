namespace Subsonic.Common.Interfaces
{
    public interface IImageFormat<T>
    {
        T GetImage();
        void SetImage(T image);
    }
}