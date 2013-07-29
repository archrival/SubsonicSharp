using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common
{
    [Serializable]
    public class IndexID3
    {
        [XmlElement("artist")]
        public List<ArtistID3> Artist;

        [XmlAttribute("name")]
        public string Name;
    }
}
