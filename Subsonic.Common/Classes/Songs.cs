using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [Serializable]
    public class Songs
    {
        [XmlElement("song")]
        public List<Child> Song;
    }
}
