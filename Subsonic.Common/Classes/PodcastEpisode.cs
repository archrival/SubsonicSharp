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

        [XmlAttribute("status")]
        public PodcastStatus Status;

        [XmlAttribute("streamId")]
        public string StreamId;

        [XmlIgnore]
        private DateTime? _publishDate;

        [XmlAttribute("publishDate")]
        public DateTime PublishDate
        {
            get => _publishDate.GetValueOrDefault();
            set => _publishDate = value;
        }

        public bool ShouldSerializePublishDate()
        {
            return _publishDate.HasValue;
        }
    }
}