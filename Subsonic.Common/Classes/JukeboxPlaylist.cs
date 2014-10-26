using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [Serializable]
    public class JukeboxPlaylist : JukeboxStatus
    {
        [XmlElement("entry")]
        public List<Child> Entry;
    }
}
