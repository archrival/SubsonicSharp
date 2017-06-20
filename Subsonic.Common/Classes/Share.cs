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

        [XmlAttribute("id")]
        public string Id;

        [XmlAttribute("url")]
        public string Url;

        [XmlAttribute("username")]
        public string Username;

        [XmlAttribute("visitCount")]
        public int VisitCount;

        [XmlIgnore]
        private DateTime? _expires;

        [XmlIgnore]
        private DateTime? _lastVisited;

        [XmlAttribute("expires")]
        public DateTime Expires
        {
            get { return _expires.GetValueOrDefault(); }
            set { _expires = value; }
        }

        [XmlAttribute("lastVisited")]
        public DateTime LastVisited
        {
            get { return _lastVisited.GetValueOrDefault(); }
            set { _lastVisited = value; }
        }

        public bool ShouldSerializeExpires()
        {
            return _expires.HasValue;
        }

        public bool ShouldSerializeLastVisited()
        {
            return _lastVisited.HasValue;
        }
    }
}