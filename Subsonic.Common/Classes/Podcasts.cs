using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [Serializable]
    public class Podcasts
    {
        [XmlElement("channel")]
        public List<PodcastChannel> Channel;
    }
}
