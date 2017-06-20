using System;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Artist
    {
        [XmlAttribute("id")]
        public string Id;

        [XmlAttribute("name")]
        public string Name;

        [XmlIgnore]
        private double? _averageRating;

        [XmlIgnore]
        private DateTime? _starred;

        [XmlIgnore]
        private int? _userRating;

        [XmlAttribute("averageRating")]
        public double AverageRating
        {
            get { return _averageRating.GetValueOrDefault(); }
            set { _averageRating = value; }
        }

        [XmlAttribute("starred")]
        public DateTime Starred
        {
            get { return _starred.GetValueOrDefault(); }
            set { _starred = value; }
        }

        [XmlAttribute("userRating")]
        public int UserRating
        {
            get { return _userRating.GetValueOrDefault(); }
            set { _userRating = value; }
        }

        public bool ShouldSerializeAverageRating()
        {
            return _averageRating.HasValue;
        }

        public bool ShouldSerializeStarred()
        {
            return _starred.HasValue;
        }

        public bool ShouldSerializeUserRating()
        {
            return _userRating.HasValue;
        }
    }
}