using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Starred2
    {
        [XmlElement("album")] public List<AlbumID3> Album;
        [XmlElement("artist")] public List<ArtistID3> Artist;
        [XmlElement("song")] public List<Child> Song;
    }
}
