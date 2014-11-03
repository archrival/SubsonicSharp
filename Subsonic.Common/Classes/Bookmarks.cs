using System.Xml.Serialization;
using System.Collections.Generic;

namespace Subsonic.Common.Classes
{
    public class Bookmarks
    {
        [XmlElement("bookmark")] public List<Bookmark> Bookmark;
    }
}

