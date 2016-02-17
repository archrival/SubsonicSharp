using System.Collections.Generic;
using System.Xml.Serialization;
using System;
using System.ComponentModel.DataAnnotations;

namespace Subsonic.Common.Classes
{
    public class Directory
    {
        [XmlElement("child")] public List<Child> Children;
        [XmlAttribute("id")] public string Id;
        [XmlAttribute("name")] public string Name;
        [XmlAttribute("parent")] public string Parent;
        [XmlAttribute("playCount")] public long PlayCount;
        [XmlAttribute("starred")] public DateTime Starred;
        [Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")] [XmlAttribute("averageRating")] public double AverageRating;
        [Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")] [XmlAttribute("userRating")] public int UserRating;
    }
}
