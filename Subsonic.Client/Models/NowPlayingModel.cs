using Subsonic.Common.Classes;

namespace Subsonic.Client.Models
{
    public class NowPlayingModel : TrackModel
    {
        public string User { get; set; }
        public string When { get; set; }
    }
}
