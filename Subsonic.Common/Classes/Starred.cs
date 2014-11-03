using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Starred
    {
        [XmlElement("album")] public List<Child> Album;
        [XmlElement("artist")] public List<Artist> Artist;
        [XmlElement("song")] public List<Child> Song;
    }
}
