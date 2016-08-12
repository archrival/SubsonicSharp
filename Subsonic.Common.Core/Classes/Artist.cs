using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Artist
    {
        [XmlAttribute("id")]
        public string Id;

        [XmlAttribute("name")]
        public string Name;

        [XmlAttribute("starred")]
        public DateTime Starred;

        [Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        [XmlAttribute("averageRating")]
        public double AverageRating;

        [Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        [XmlAttribute("userRating")]
        public int UserRating;
    }
}