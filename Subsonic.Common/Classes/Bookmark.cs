using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Bookmark
    {
        [XmlElement("child")] public List<Child> Children;
        [XmlAttribute("changed")] public DateTime Changed;
        [XmlAttribute("comment")] public string Comment;
        [XmlAttribute("created")] public DateTime Created;
        [XmlAttribute("position")] public long Position;
        [XmlAttribute("username")] public string Username;
    }
}

