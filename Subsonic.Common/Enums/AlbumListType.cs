using System.Xml.Serialization;

namespace Subsonic.Common.Enums
{
    public enum AlbumListType
    {
        [XmlEnum("random")] Random,
        [XmlEnum("newest")] Newest,
        [XmlEnum("highest")] Highest,
        [XmlEnum("frequent")] Frequent,
        [XmlEnum("recent")] Recent,
        [XmlEnum("alphabeticalByName")] AlphabeticalByName,
        [XmlEnum("alphabeticalByArtist")] AlphabeticalByArtist,
        [XmlEnum("starred")] Starred,
        [XmlEnum("byYear")] ByYear,
        [XmlEnum("byGenre")] ByGenre,
    }
}