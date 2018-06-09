using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [XmlInclude(typeof(PlaylistWithSongs))]
    public class Playlist
    {
        [XmlElement("allowedUser")]
        public List<string> AllowedUsers;

        [XmlAttribute("changed")]
        public DateTime Changed;

        [XmlAttribute("comment")]
        public string Comment;

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

        [XmlAttribute("owner")]
        public string Owner;

        [XmlAttribute("songCount")]
        public int SongCount;

        [XmlIgnore]
        private bool? _public;

        [XmlAttribute("public")]
        public bool Public
        {
            get => _public.GetValueOrDefault();
            set => _public = value;
        }

        public bool ShouldSerializePublic()
        {
            return _public.HasValue;
        }
    }
}