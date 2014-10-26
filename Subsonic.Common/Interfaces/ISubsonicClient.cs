using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Subsonic.Common.Classes;
using Subsonic.Common.Enums;

namespace Subsonic.Common.Interfaces
{
    public interface ISubsonicClient<T>
    {
        Task<bool>PingAsync();

        bool Ping();

        Task<License> GetLicenseAsync(CancellationToken? cancelToken = null);

        License GetLicense();

        Task<MusicFolders> GetMusicFoldersAsync(CancellationToken? cancelToken = null);

        MusicFolders GetMusicFolders();

        Task<NowPlaying> GetNowPlayingAsync(CancellationToken? cancelToken = null);

        NowPlaying GetNowPlaying();

        Task<Starred> GetStarredAsync(CancellationToken? cancelToken = null);

        Starred GetStarred();

        Task<Starred2> GetStarred2Async(CancellationToken? cancelToken = null);

        Starred2 GetStarred2();

        Task<Indexes> GetIndexesAsync(string musicFolderId = null, long? ifModifiedSince = null, CancellationToken? cancelToken = null);

        Indexes GetIndexes(string musicFolderId = null, long? ifModifiedSince = null);

        Task<Directory> GetMusicDirectoryAsync(string id, CancellationToken? cancelToken = null);

        Directory GetMusicDirectory(string id);

        Task<ArtistWithAlbumsID3> GetArtistAsync(string id, CancellationToken? cancelToken = null);

        ArtistWithAlbumsID3 GetArtist(string id);

        Task<ArtistsID3> GetArtistsAsync(CancellationToken? cancelToken = null);

        ArtistsID3 GetArtists();

        Task<AlbumID3> GetAlbumAsync(string id, CancellationToken? cancelToken = null);

        AlbumID3 GetAlbum(string id);

        Task<Child> GetSongAsync(string id, CancellationToken? cancelToken = null);

        Child GetSong(string id);

        Task<Videos> GetVideosAsync(CancellationToken? cancelToken = null);

        Videos GetVideos();

        Task<SearchResult> SearchAsync(string artist = null, string album = null, string title = null, string any = null, int? count = null, int? offset = null, long? newerThan = null);

        SearchResult Search(string artist = null, string album = null, string title = null, string any = null, int? count = null, int? offset = null, long? newerThan = null);

        Task<SearchResult2> Search2Async(string query, int? artistCount = null, int? artistOffset = null, int? albumCount = null, int? albumOffset = null, int? songCount = null, int? songOffset = null, CancellationToken? cancelToken = null);

        SearchResult2 Search2(string query, int? artistCount = null, int? artistOffset = null, int? albumCount = null, int? albumOffset = null, int? songCount = null, int? songOffset = null);

        Task<SearchResult3> Search3Async(string query, int? artistCount = null, int? artistOffset = null, int? albumCount = null, int? albumOffset = null, int? songCount = null, int? songOffset = null, CancellationToken? cancelToken = null);

        SearchResult3 Search3(string query, int? artistCount = null, int? artistOffset = null, int? albumCount = null, int? albumOffset = null, int? songCount = null, int? songOffset = null);

        Task<Playlists> GetPlaylistsAsync(string username = null, CancellationToken? cancelToken = null);

        Playlists GetPlaylists(string username = null);

        Task<PlaylistWithSongs> GetPlaylistAsync(string id, CancellationToken? cancelToken = null);

        PlaylistWithSongs GetPlaylist(string id);

        Task<bool> CreatePlaylistAsync(string playlistId = null, string name = null, IEnumerable<string> songId = null);

        bool CreatePlaylist(string playlistId = null, string name = null, IEnumerable<string> songId = null);

        Task<bool> UpdatePlaylistAsync(string playlistId, string name = null, string comment = null, IEnumerable<string> songIdToAdd = null, IEnumerable<string> songIndexToRemove = null);

        bool UpdatePlaylist(string playlistId, string name = null, string comment = null, IEnumerable<string> songIdToAdd = null, IEnumerable<string> songIndexToRemove = null);

        Task<bool> DeletePlaylistAsync(string id);

        bool DeletePlaylist(string id);

        Task<long> DownloadAsync(string id, string path, bool pathOverride = false, CancellationToken? cancelToken = null);

        long Download(string id, string path, bool pathOverride = false);

        Task<long> StreamAsync(string id, string path, int? maxBitRate = null, StreamFormat? format = null, int? timeOffset = null, string size = null, bool? estimateContentLength = null, CancellationToken? cancelToken = null, bool noResponse = false);

        long Stream(string id, string path, int? maxBitRate = null, StreamFormat? format = null, int? timeOffset = null, string size = null, bool? estimateContentLength = null);

        Task<IImageFormat<T>> GetCoverArtAsync(string id, int? size = null, CancellationToken? cancelToken = null);

        IImageFormat<T> GetCoverArt(string id, int? size = null);

        Task<bool> ScrobbleAsync(string id, bool? submission = null, long? time = null);

        bool Scrobble(string id, bool? submission = null, long? time = null);

        Task<Shares> GetSharesAsync(CancellationToken? cancelToken = null);

        Shares GetShares();

        Task<bool> ChangePasswordAsync(string username, string password);

        bool ChangePassword(string username, string password);

        Task<User> GetUserAsync(string username, CancellationToken? cancelToken = null);

        User GetUser(string username);

        Task<IImageFormat<T>> GetAvatarAsync(string username, CancellationToken? cancelToken = null);

        IImageFormat<T> GetAvatar(string username);

        Task<bool> StarAsync(IEnumerable<string> id = null, IEnumerable<string> albumId = null, IEnumerable<string> artistId = null);

        bool Star(IEnumerable<string> id = null, IEnumerable<string> albumId = null, IEnumerable<string> artistId = null);

        Task<bool> UnStarAsync(IEnumerable<string> id = null, IEnumerable<string> albumId = null, IEnumerable<string> artistId = null);

        bool UnStar(IEnumerable<string> id = null, IEnumerable<string> albumId = null, IEnumerable<string> artistId = null);

        Task<bool> SetRatingAsync(string id, int rating);

        bool SetRating(string id, int rating);

        Task<bool> CreateUserAsync(string username, string password, bool? ldapAuthenticated = null, bool? adminRole = null, bool? settingsRole = null, bool? streamRole = null, bool? jukeboxRole = null, bool? downloadRole = null, bool? uploadRole = null, bool? playlistRole = null, bool? coverArtRole = null, bool? commentRole = null, bool? podcastRole = null);

        bool CreateUser(string username, string password, bool? ldapAuthenticated = null, bool? adminRole = null, bool? settingsRole = null, bool? streamRole = null, bool? jukeboxRole = null, bool? downloadRole = null, bool? uploadRole = null, bool? playlistRole = null, bool? coverArtRole = null, bool? commentRole = null, bool? podcastRole = null);

        Task<bool> DeleteUserAsync(string username);

        bool DeleteUser(string username);

        Task<ChatMessages> GetChatMessagesAsync(double? since = null, CancellationToken? cancelToken = null);

        ChatMessages GetChatMessages(long? since = null);

        Task<bool> AddChatMessageAsync(string message);

        bool AddChatMessage(string message);

        Task<AlbumList> GetAlbumListAsync(AlbumListType type, int? size = null, int? offset = null, CancellationToken? cancelToken = null);

        AlbumList GetAlbumList(AlbumListType type, int? size = null, int? offset = null);

        Task<AlbumList2> GetAlbumList2Async(AlbumListType type, int? size = null, int? offset = null, CancellationToken? cancelToken = null);

        AlbumList2 GetAlbumList2(AlbumListType type, int? size = null, int? offset = null);

        Task<RandomSongs> GetRandomSongsAsync(int? size = null, string genre = null, int? fromYear = null, int? toYear = null, string musicFolderId = null, CancellationToken? cancelToken = null);

        RandomSongs GetRandomSongs(int? size = null, string genre = null, int? fromYear = null, int? toYear = null, string musicFolderId = null);

        Task<Lyrics> GetLyricsAsync(string artist = null, string title = null, CancellationToken? cancelToken = null);

        Lyrics GetLyrics(string artist = null, string title = null);

        Task<JukeboxPlaylist> JukeboxControlAsync(CancellationToken? cancelToken = null);

        JukeboxPlaylist JukeboxControl();

        Task<bool> JukeboxControlAsync(JukeboxControlAction action, int? index = null, float? gain = null, IEnumerable<string> id = null);

        bool JukeboxControl(JukeboxControlAction action, int? index = null, float? gain = null, IEnumerable<string> id = null);

        Task<Podcasts> GetPodcastsAsync(CancellationToken? cancelToken = null);

        Podcasts GetPodcasts();

        Task<Genres> GetGenresAsync(CancellationToken? cancelToken = null);

        Genres GetGenres();

        Task<Songs> GetSongsByGenreAsync(string genre, int? count = null, int? offset = null, CancellationToken? cancelToken = null);

        Songs GetSongsByGenre(string genre, int? count = null, int? offset = null);
    }
}
