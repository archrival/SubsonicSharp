namespace Subsonic.Client.Items
{
    public sealed class NowPlayingItem : TrackItem
    {
        public string User { get; set; }
        public string When { get; set; }
        public int AlbumArtSize { get; set; }
    }
}
