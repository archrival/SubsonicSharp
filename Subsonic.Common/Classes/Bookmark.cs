using System.Xml.Serialization;
using Subsonic.Common.Classes;
using System.Collections.Generic;
using System;

namespace Subsonic.Common
{
    public class Bookmark
    {
        [XmlElement("child")]
        public List<Child> Entry;

        [XmlAttribute("changed")]
        public DateTime Changed;

        [XmlAttribute("comment")]
        public string Comment;

        [XmlAttribute("created")]
        public DateTime Created;

        [XmlAttribute("position")]
        public long Position;

        [XmlAttribute("username")]
        public string Username;
    }
}

