using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Bookmarks
    {
        [XmlElement("bookmark")]
        public List<Bookmark> Items;
    }
}