using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class AlbumList2
    {
        [XmlElement("album")]
        public List<AlbumID3> Albums;
    }
}