using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common
{
    [Serializable]
    public class PodcastChannel
    {
        [XmlAttribute("description")]
        public string Description;

        [XmlElement("episode")]
        public List<PodcastEpisode> Episode;

        [XmlAttribute("errorMessage")]
        public string ErrorMessage;

        [XmlAttribute("id")]
        public string Id;

        [XmlAttribute("status")]
        public PodcastStatus Status;

        [XmlAttribute("title")]
        public string Title;

        [XmlAttribute("url")]
        public string Url;
    }
}
