using System;
using System.Xml.Serialization;

namespace Subsonic.Common
{
    [XmlInclude(typeof(AlbumWithSongsID3))]
    [Serializable]
    public class AlbumID3
    {
        [XmlAttribute("artist")]
        public string Artist;

        [XmlAttribute("artistId")]
        public string ArtistId;

        [XmlAttribute("coverArt")]
        public string CoverArt;

        [XmlAttribute("created")]
        public DateTime Created;

        [XmlAttribute("duration")]
        public int Duration;

        [XmlAttribute("id")]
        public string Id;

        [XmlAttribute("name")]
        public string Name;

        [XmlAttribute("songCount")]
        public int SongCount;

        [XmlAttribute("starred")]
        public DateTime Starred;
    }
}
