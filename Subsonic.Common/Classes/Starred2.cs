using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Starred2
    {
        [XmlElement("album")] public List<AlbumID3> Albums;
        [XmlElement("artist")] public List<ArtistID3> Artists;
        [XmlElement("song")] public List<Child> Songs;
    }
}
