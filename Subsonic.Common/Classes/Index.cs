﻿using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Index
    {
        [XmlElement("artist")]
        public List<Artist> Artists;

        [XmlAttribute("name")]
        public string Name;
    }
}