using System.Drawing;
using Subsonic.Client.Interfaces;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Windows
{
    public class SubsonicClientWindows : SubsonicClient<Image>
    {
        public SubsonicClientWindows(ISubsonicServer subsonicServer, IImageFormatFactory<Image> imageFormatFactory) : base(subsonicServer)
        {
            SubsonicResponse = new SubsonicResponseWindows<Image>(subsonicServer, imageFormatFactory);
        }
    }
}