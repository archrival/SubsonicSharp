using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class NewestPodcasts
    {
        [XmlElement("episode")]
        public List<PodcastEpisode> Episodes;
    }
}