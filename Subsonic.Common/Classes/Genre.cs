using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class Genre
    {
        [XmlAttribute("albumCount")]
        public int AlbumCount;

        [XmlAttribute("songCount")]
        public int SongCount;

        [XmlText]
        public string Name;
    }
}

