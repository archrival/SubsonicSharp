using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Genres
    {
        [XmlElementAttribute("genre")]
        public List<string> Genre;
    }
}
