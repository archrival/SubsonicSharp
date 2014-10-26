using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [Serializable]
    public class MusicFolders
    {
        [XmlElement("musicFolder")]
        public List<MusicFolder> MusicFolder;
    }
}
