using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [Serializable]
    public class Playlists
    {
        [XmlElement("playlist")]
        public List<Playlist> Playlist;
    }
}
