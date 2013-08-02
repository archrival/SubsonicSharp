
namespace Subsonic.Common
{
    public interface IImageFormat<T>
    {
        T GetImage();

        void SetImage(T image);
    }
}
