using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Podcasts
    {
        [XmlElement("channel")]
        public List<PodcastChannel> Channels;
    }
}