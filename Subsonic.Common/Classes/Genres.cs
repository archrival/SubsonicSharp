using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [Serializable]
    public class Genres
    {
        [XmlElementAttribute("genre")]
        public List<string> Genre;
    }
}
