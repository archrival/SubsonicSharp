using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    public class AlbumInfo
    {
        [XmlElement("notes")]
        public string Notes;

        [XmlElement("musicBrainzId")]
        public string MusicBrainzId;

        [XmlElement("lastFmUrl")]
        public string LastFmUrl;

        [XmlElement("smallImageUrl")]
        public string SmallImageUrl;

        [XmlElement("mediumImageUrl")]
        public string MediumImageUrl;

        [XmlElement("largeImageUrl")]
        public string LargeImageUrl;
    }
}