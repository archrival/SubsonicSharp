using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subsonic.Common.Enums
{
    /// <summary>
    /// Methods available in the supported Subsonic API version.
    /// </summary>
    public enum Methods
    {
        ping,
        getLicense,
        getMusicFolders,
        getNowPlaying,
        getIndexes,
        getMusicDirectory,
        getArtists,
        getArtist,
        getAlbum,
        getSong,
        getVideos,
        search,
        search2,
        getPlaylists,
        getPlaylist,
        createPlaylist,
        deletePlaylist,
        download,
        stream,
        getCoverArt,
        scrobble,
        changePassword,
        getUser,
        createUser,
        deleteUser,
        getChatMessages,
        addChatMessage,
        getAlbumList,
        getAlbumList2,
        getRandomSongs,
        getLyrics,
        jukeboxControl,
        getPodcasts,
        getShareUrl,
        getStarred,
        getStarred2,
        updatePlaylist,
        search3,
        getAvatar,
        star,
        unstar,
        setRating,
        getShares,
        getGenres,
        getSongsByGenre,
    }
}
