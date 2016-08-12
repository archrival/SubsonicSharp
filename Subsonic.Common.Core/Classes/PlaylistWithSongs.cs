using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class PlaylistWithSongs : Playlist
    {
        [XmlElement("entry")]
        public List<Child> Entries;
    }
}