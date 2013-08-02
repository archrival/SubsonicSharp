using System;
using System.Xml.Serialization;

namespace Subsonic.Common.Enums
{
    [Serializable]
    [XmlType(IncludeInSchema = false)]
    public enum ItemChoiceType
    {
        [XmlEnum(Name = "album")]
        Album,

        [XmlEnum(Name = "albumList")]
        AlbumList,

        [XmlEnum(Name = "albumList2")]
        AlbumList2,

        [XmlEnum(Name = "artist")]
        Artist,

        [XmlEnum(Name = "artists")]
        Artists,

        [XmlEnum(Name = "chatMessages")]
        ChatMessages,

        [XmlEnum(Name = "directory")]
        Directory,

        [XmlEnum(Name = "error")]
        Error,

        [XmlEnum(Name = "genres")]
        Genres,

        [XmlEnum(Name = "indexes")]
        Indexes,

        [XmlEnum(Name = "jukeboxPlaylist")]
        JukeboxPlaylist,

        [XmlEnum(Name = "jukeboxStatus")]
        JukeboxStatus,

        [XmlEnum(Name = "license")]
        License,

        [XmlEnum(Name = "lyrics")]
        Lyrics,

        [XmlEnum(Name = "musicFolders")]
        MusicFolders,

        [XmlEnum(Name = "nowPlaying")]
        NowPlaying,

        [XmlEnum(Name = "playlist")]
        Playlist,

        [XmlEnum(Name = "playlists")]
        Playlists,

        [XmlEnum(Name = "podcasts")]
        Podcasts,

        [XmlEnum(Name = "randomSongs")]
        RandomSongs,

        [XmlEnum(Name = "searchResult")]
        SearchResult,

        [XmlEnum(Name = "searchResult2")]
        SearchResult2,

        [XmlEnum(Name = "searchResult3")]
        SearchResult3,

        [XmlEnum(Name = "shares")]
        Shares,

        [XmlEnum(Name = "song")]
        Song,

        [XmlEnum(Name = "songsByGenre")]
        Songs,

        [XmlEnum(Name = "starred")]
        Starred,

        [XmlEnum(Name = "starred2")]
        Starred2,

        [XmlEnum(Name = "user")]
        User,

        [XmlEnum(Name = "users")]
        Users,

        [XmlEnum(Name = "videos")]
        Videos,
    }
}
