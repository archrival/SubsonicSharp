using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [Serializable]
    public class SearchResult3
    {
        [XmlElement("album")]
        public List<AlbumID3> Album;

        [XmlElement("artist")]
        public List<ArtistID3> Artist;

        [XmlElement("song")]
        public List<Child> Song;
    }
}
