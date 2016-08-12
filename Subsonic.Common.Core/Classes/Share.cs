using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Share
    {
        [XmlAttribute("created")]
        public DateTime Created;

        [XmlAttribute("description")]
        public string Description;

        [XmlElement("entry")]
        public List<Child> Entries;

        [XmlAttribute("expires")]
        public DateTime Expires;

        [XmlAttribute("id")]
        public string Id;

        [XmlAttribute("lastVisited")]
        public DateTime LastVisited;

        [XmlAttribute("url")]
        public string Url;

        [XmlAttribute("username")]
        public string Username;

        [XmlAttribute("visitCount")]
        public int VisitCount;
    }
}