using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Bookmark
    {
        [XmlAttribute("changed")]
        public DateTime Changed;

        [XmlElement("entry")]
        public List<Child> Children;

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