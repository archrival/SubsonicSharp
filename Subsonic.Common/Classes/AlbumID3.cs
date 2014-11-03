﻿using System;
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

        [XmlAttribute("starred")]
        public DateTime Starred;

        [XmlAttribute("year")]
        public int Year;
    }
}
