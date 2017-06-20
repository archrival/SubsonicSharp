using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class NowPlaying
    {
        [XmlElement("entry")]
        public List<NowPlayingEntry> Entries;
    }
}