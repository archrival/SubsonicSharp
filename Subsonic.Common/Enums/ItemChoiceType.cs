using System.Xml.Serialization;

namespace Subsonic.Common.Enums
{
    [XmlType(IncludeInSchema = false)]
    public enum ItemChoiceType
    {
        [XmlEnum("album")] Album,
        [XmlEnum("albumList")] AlbumList,
        [XmlEnum("albumList2")] AlbumList2,
        [XmlEnum("artist")] Artist,
        [XmlEnum("artists")] Artists,
        [XmlEnum("bookmarks")] Bookmarks,
        [XmlEnum("chatMessages")] ChatMessages,
        [XmlEnum("directory")] Directory,
        [XmlEnum("error")] Error,
        [XmlEnum("genres")] Genres,
        [XmlEnum("indexes")] Indexes,
        [XmlEnum("internetRadioStations")] InternetRadioStations,
        [XmlEnum("jukeboxPlaylist")] JukeboxPlaylist,
        [XmlEnum("jukeboxStatus")] JukeboxStatus,
        [XmlEnum("license")] License,
        [XmlEnum("lyrics")] Lyrics,
        [XmlEnum("musicFolders")] MusicFolders,
        [XmlEnum("nowPlaying")] NowPlaying,
        [XmlEnum("playlist")] Playlist,
        [XmlEnum("playlists")] Playlists,
        [XmlEnum("podcasts")] Podcasts,
        [XmlEnum("randomSongs")] RandomSongs,
        [XmlEnum("searchResult")] SearchResult,
        [XmlEnum("searchResult2")] SearchResult2,
        [XmlEnum("searchResult3")] SearchResult3,
        [XmlEnum("shares")] Shares,
        [XmlEnum("song")] Song,
        [XmlEnum("songsByGenre")] Songs,
        [XmlEnum("starred")] Starred,
        [XmlEnum("starred2")] Starred2,
        [XmlEnum("user")] User,
        [XmlEnum("users")] Users,
        [XmlEnum("videos")] Videos
    }
}