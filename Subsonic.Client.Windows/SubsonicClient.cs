using Subsonic.Client.Interfaces;
using Subsonic.Common.Interfaces;
using System.Drawing;

namespace Subsonic.Client.Windows
{
    public class SubsonicClient : SubsonicClient<Image>
    {
        public SubsonicClient(ISubsonicServer subsonicServer) : base(subsonicServer)
        {
            SubsonicResponse = new SubsonicResponse<Image>(subsonicServer, new ImageFormatFactory());
        }

        public SubsonicClient(ISubsonicServer subsonicServer, IImageFormatFactory<Image> imageFormatFactory) : base(subsonicServer)
        {
            SubsonicResponse = new SubsonicResponse<Image>(subsonicServer, imageFormatFactory);
        }
    }
}