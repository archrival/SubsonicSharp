using System.Xml.Serialization;

namespace Subsonic.Common.Classes
{
    [XmlInclude(typeof(ArtistInfo))]
    [XmlInclude(typeof(ArtistInfo2))]
    public class ArtistInfoBase
    {
        [XmlElement("biography")]
        public string Biography;

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