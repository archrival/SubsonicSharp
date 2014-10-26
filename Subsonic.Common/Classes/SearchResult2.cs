using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [Serializable]
    public class SearchResult2
    {
        [XmlElement("album")]
        public List<Child> Album;

        [XmlElement("artist")]
        public List<Artist> Artist;

        [XmlElement("song")]
        public List<Child> Song;
    }
}
