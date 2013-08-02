using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common
{
    [Serializable]
    public class Playlists
    {
        [XmlElement("playlist")]
        public List<Playlist> Playlist;
    }
}
