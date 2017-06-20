using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class PlayQueue
    {
        [XmlAttribute("changed")]
        public DateTime Changed;

        [XmlAttribute("changedBy")]
        public string ChangedBy;

        [XmlAttribute("current")]
        public string Current;

        [XmlElement("entry")]
        public List<Child> Entries;

        [XmlAttribute("username")]
        public string Username;

        [XmlIgnore]
        private long? _position;

        [XmlAttribute("position")]
        public long Position
        {
            get { return _position.GetValueOrDefault(); }
            set { _position = value; }
        }

        public bool ShouldSerializePosition()
        {
            return _position.HasValue;
        }
    }
}