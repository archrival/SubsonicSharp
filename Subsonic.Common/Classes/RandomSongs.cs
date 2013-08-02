using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common
{
    [Serializable]
    public class RandomSongs
    {
        [XmlElement("song")]
        public List<Child> Song;
    }
}
