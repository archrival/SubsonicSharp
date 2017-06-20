using Subsonic.Common.Enums;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class PodcastChannel
    {
        [XmlAttribute("coverArt")]
        public string CoverArt;

        [XmlAttribute("description")]
        public string Description;

        [XmlElement("episode")]
        public List<PodcastEpisode> Episodes;

        [XmlAttribute("errorMessage")]
        public string ErrorMessage;

        [XmlAttribute("id")]
        public string Id;

        [XmlAttribute("originalImageUrl")]
        public string OriginalImageUrl;

        [XmlAttribute("status")]
        public PodcastStatus Status;

        [XmlAttribute("title")]
        public string Title;

        [XmlAttribute("url")]
        public string Url;
    }
}