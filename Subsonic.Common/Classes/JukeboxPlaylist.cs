using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common
{
    [Serializable]
    public class JukeboxPlaylist : JukeboxStatus
    {
        [XmlElement("entry")]
        public List<Child> Entry;
    }
}
