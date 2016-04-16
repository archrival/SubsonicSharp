using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Playlists
    {
        [XmlElement("playlist")]
        public List<Playlist> Items;
    }
}