using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common
{
    [Serializable]
    public class ArtistsID3
    {
        [XmlElement("index")]
        public List<IndexID3> Index;
    }
}
