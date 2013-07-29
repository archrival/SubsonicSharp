using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common
{
    [Serializable]
    public class AlbumList
    {
        [XmlElement("album")]
        public List<Child> Album;
    }
}
