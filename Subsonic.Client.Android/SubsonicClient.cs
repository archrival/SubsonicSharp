using Android.Graphics;
using Subsonic.Client.Interfaces;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Android
{
    public class SubsonicClient : SubsonicClient<Bitmap>
    {
        public SubsonicClient(ISubsonicServer subsonicServer) : base(subsonicServer)
        {
            SubsonicResponse = new SubsonicResponse<Bitmap>(subsonicServer, new ImageFormatFactory());
        }

        public SubsonicClient(ISubsonicServer subsonicServer, IImageFormatFactory<Bitmap> imageFormatFactory) : base(subsonicServer)
        {
            SubsonicResponse = new SubsonicResponse<Bitmap>(subsonicServer, imageFormatFactory);
        }
    }
}