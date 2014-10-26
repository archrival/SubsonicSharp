
using System.Xml.Serialization;

namespace Subsonic.Common.Enums
{
    /// <summary>
    /// Methods available in the supported Subsonic API version.
    /// </summary>
    public enum Methods
    {
        [XmlEnum("ping")]
        Ping,

        [XmlEnum("getLicense")]
        GetLicense,

        [XmlEnum("getMusicFolders")]
        GetMusicFolders,

        [XmlEnum("getNowPlaying")]
        GetNowPlaying,

        [XmlEnum("getIndexes")]
        GetIndexes,

        [XmlEnum("getMusicDirectory")]
        GetMusicDirectory,

        [XmlEnum("getArtists")]
        GetArtists,

        [XmlEnum("getArtist")]
        GetArtist,

        [XmlEnum("getAlbum")]
        GetAlbum,

        [XmlEnum("getSong")]
        GetSong,

        [XmlEnum("getVideos")]
        GetVideos,

        [XmlEnum("search")]
        Search,

        [XmlEnum("search2")]
        Search2,

        [XmlEnum("getPlaylists")]
        GetPlaylists,

        [XmlEnum("getPlaylist")]
        GetPlaylist,

        [XmlEnum("createPlaylist")]
        CreatePlaylist,

        [XmlEnum("deletePlaylist")]
        DeletePlaylist,

        [XmlEnum("download")]
        Download,

        [XmlEnum("stream")]
        Stream,

        [XmlEnum("getCoverArt")]
        GetCoverArt,

        [XmlEnum("scrobble")]
        Scrobble,

        [XmlEnum("changePassword")]
        ChangePassword,

        [XmlEnum("getUser")]
        GetUser,

        [XmlEnum("createUser")]
        CreateUser,

        [XmlEnum("deleteUser")]
        DeleteUser,

        [XmlEnum("getChatMessages")]
        GetChatMessages,

        [XmlEnum("addChatMessage")]
        AddChatMessage,

        [XmlEnum("getAlbumList")]
        GetAlbumList,

        [XmlEnum("getAlbumList2")]
        GetAlbumList2,

        [XmlEnum("getRandomSongs")]
        GetRandomSongs,

        [XmlEnum("getLyrics")]
        GetLyrics,

        [XmlEnum("jukeboxControl")]
        JukeboxControl,

        [XmlEnum("getPodcasts")]
        GetPodcasts,

        [XmlEnum("getShareUrl")]
        GetShareUrl,

        [XmlEnum("getStarred")]
        GetStarred,

        [XmlEnum("getStarred2")]
        GetStarred2,

        [XmlEnum("updatePlaylist")]
        UpdatePlaylist,

        [XmlEnum("search3")]
        Search3,

        [XmlEnum("getAvatar")]
        GetAvatar,

        [XmlEnum("star")]
        Star,

        [XmlEnum("unstar")]
        Unstar,

        [XmlEnum("setRating")]
        SetRating,

        [XmlEnum("getShares")]
        GetShares,

        [XmlEnum("getGenres")]
        GetGenres,

        [XmlEnum("getSongsByGenre")]
        GetSongsByGenre,
    }
}
