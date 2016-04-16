using Subsonic.Common.Enums;
using System;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class PodcastEpisode : Child
    {
        [XmlAttribute("channelId")]
        public string ChannelId;

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