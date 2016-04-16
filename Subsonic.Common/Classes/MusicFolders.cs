using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class MusicFolders
    {
        [XmlElement("musicFolder")]
        public List<MusicFolder> Items;
    }
}