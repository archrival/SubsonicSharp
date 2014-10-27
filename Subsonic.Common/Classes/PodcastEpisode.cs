using System;
using System.Xml.Serialization;
using Subsonic.Common.Enums;

namespace Subsonic.Common.Classes
{
    public class PodcastEpisode : Child
    {
        [XmlAttribute("description")]
        public string Description;

        [XmlAttribute("publishDate")]
        public DateTime PublishDate;

        [XmlAttribute("status")]
        public PodcastStatus Status;

        [XmlAttribute("streamId")]
        public string StreamId;
    }
}
