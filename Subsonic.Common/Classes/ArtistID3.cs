using System;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [XmlInclude(typeof(ArtistWithAlbumsID3))]
    public class ArtistID3
    {
        [XmlAttribute("albumCount")]
        public int AlbumCount;

        [XmlAttribute("coverArt")]
        public string CoverArt;

        [XmlAttribute("id")]
        public string Id;

        [XmlAttribute("name")]
        public string Name;

        [XmlIgnore]
        private DateTime? _starred;

        [XmlAttribute("starred")]
        public DateTime Starred
        {
            get { return _starred.GetValueOrDefault(); }
            set { _starred = value; }
        }

        public bool ShouldSerializeStarred()
        {
            return _starred.HasValue;
        }
    }
}