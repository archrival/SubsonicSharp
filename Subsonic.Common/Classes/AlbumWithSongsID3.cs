using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class AlbumWithSongsID3 : AlbumID3
    {
        [XmlElement("song")]
        public List<Child> Songs;
    }
}