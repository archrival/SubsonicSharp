using System.Xml.Serialization;

namespace Subsonic.Common.Enums
{
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

        [XmlEnum("getUsers")]
        GetUsers,

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

        [XmlEnum("hls")]
        Hls,

        [XmlEnum("refreshPodcasts")]
        RefreshPodcasts,

        [XmlEnum("createPodcastChannel")]
        CreatePodcastChannel,

        [XmlEnum("deletePodcastChannel")]
        DeletePodcastChannel,

        [XmlEnum("deletePodcastEpisode")]
        DeletePodcastEpisode,

        [XmlEnum("downloadPodcastEpisode")]
        DownloadPodcastEpisode,

        [XmlEnum("getInternetRadioStations")]
        GetInternetRadioStations,

        [XmlEnum("updateUser")]
        UpdateUser,

        [XmlEnum("getBookmarks")]
        GetBookmarks,

        [XmlEnum("createBookmark")]
        CreateBookmark,

        [XmlEnum("deleteBookmark")]
        DeleteBookmark,

        [XmlEnum("createShare")]
        CreateShare,

        [XmlEnum("updateShare")]
        UpdateShare,

        [XmlEnum("deleteShare")]
        DeleteShare,

        [XmlEnum("getArtistInfo")]
        GetArtistInfo,

        [XmlEnum("getArtistInfo2")]
        GetArtistInfo2,

        [XmlEnum("getAlbumInfo")]
        GetAlbumInfo,

        [XmlEnum("getAlbumInfo2")]
        GetAlbumInfo2,

        [XmlEnum("getSimilarSongs")]
        GetSimilarSongs,

        [XmlEnum("getSimilarSongs2")]
        GetSimilarSongs2,

        [XmlEnum("getTopSongs")]
        GetTopSongs,

        [XmlEnum("getPlayQueue")]
        GetPlayQueue,

        [XmlEnum("savePlayQueue")]
        SavePlayQueue,

        [XmlEnum("getVideoInfo")]
        GetVideoInfo,

        [XmlEnum("getCaptions")]
        GetCaptions
    }
}