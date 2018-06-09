using System;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [XmlInclude(typeof(AlbumWithSongsID3))]
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

        [XmlAttribute("genre")]
        public string Genre;

        [XmlAttribute("id")]
        public string Id;

        [XmlAttribute("name")]
        public string Name;

        [XmlAttribute("songCount")]
        public int SongCount;

        [XmlIgnore]
        private long? _playCount;

        [XmlIgnore]
        private DateTime? _starred;

        [XmlIgnore]
        private int? _year;

        [XmlAttribute("playCount")]
        public long PlayCount
        {
            get => _playCount.GetValueOrDefault();
            set => _playCount = value;
        }

        [XmlAttribute("starred")]
        public DateTime Starred
        {
            get => _starred.GetValueOrDefault();
            set => _starred = value;
        }

        [XmlAttribute("year")]
        public int Year
        {
            get => _year.GetValueOrDefault();
            set => _year = value;
        }

        public bool ShouldSerializePlayCount()
        {
            return _playCount.HasValue;
        }

        public bool ShouldSerializeStarred()
        {
            return _starred.HasValue;
        }

        public bool ShouldSerializeYear()
        {
            return _year.HasValue;
        }
    }
}