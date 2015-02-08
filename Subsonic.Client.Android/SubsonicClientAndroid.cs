using Android.Graphics;
using Subsonic.Client.Interfaces;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Android
{
    public class SubsonicClientAndroid : SubsonicClient<Bitmap>
    {
        public SubsonicClientAndroid(ISubsonicServer subsonicServer, IImageFormatFactory<Bitmap> imageFormatFactory) : base(subsonicServer)
        {
            SubsonicResponse = new SubsonicResponseAndroid<Bitmap>(subsonicServer, imageFormatFactory);
        }
    }
}