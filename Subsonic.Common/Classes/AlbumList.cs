using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class AlbumList
    {
        [XmlElement("album")]
        public List<Child> Album;
    }
}
