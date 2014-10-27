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

        Task<License> GetLicenseAsync(CancellationToken? cancelToken = null);

        Task<MusicFolders> GetMusicFoldersAsync(CancellationToken? cancelToken = null);

        Task<NowPlaying> GetNowPlayingAsync(CancellationToken? cancelToken = null);

        Task<Starred> GetStarredAsync(CancellationToken? cancelToken = null);

        Task<Starred2> GetStarred2Async(CancellationToken? cancelToken = null);

        Task<Indexes> GetIndexesAsync(string musicFolderId = null, long? ifModifiedSince = null, CancellationToken? cancelToken = null);

        Task<Directory> GetMusicDirectoryAsync(string id, CancellationToken? cancelToken = null);

        Task<ArtistWithAlbumsID3> GetArtistAsync(string id, CancellationToken? cancelToken = null);

        Task<ArtistsID3> GetArtistsAsync(CancellationToken? cancelToken = null);

        Task<AlbumID3> GetAlbumAsync(string id, CancellationToken? cancelToken = null);

        Task<Child> GetSongAsync(string id, CancellationToken? cancelToken = null);

        Task<Videos> GetVideosAsync(CancellationToken? cancelToken = null);

        Task<SearchResult> SearchAsync(string artist = null, string album = null, string title = null, string any = null, int? count = null, int? offset = null, long? newerThan = null);

        Task<SearchResult2> Search2Async(string query, int? artistCount = null, int? artistOffset = null, int? albumCount = null, int? albumOffset = null, int? songCount = null, int? songOffset = null, CancellationToken? cancelToken = null);

        Task<SearchResult3> Search3Async(string query, int? artistCount = null, int? artistOffset = null, int? albumCount = null, int? albumOffset = null, int? songCount = null, int? songOffset = null, CancellationToken? cancelToken = null);

        Task<Playlists> GetPlaylistsAsync(string username = null, CancellationToken? cancelToken = null);

        Task<PlaylistWithSongs> GetPlaylistAsync(string id, CancellationToken? cancelToken = null);

        Task<bool> CreatePlaylistAsync(string playlistId = null, string name = null, IEnumerable<string> songId = null);

        Task<bool> UpdatePlaylistAsync(string playlistId, string name = null, string comment = null, IEnumerable<string> songIdToAdd = null, IEnumerable<string> songIndexToRemove = null);

        Task<bool> DeletePlaylistAsync(string id);

        Task<IImageFormat<T>> GetCoverArtAsync(string id, int? size = null, CancellationToken? cancelToken = null);

        Task<bool> ScrobbleAsync(string id, bool? submission = null, long? time = null);

        Task<Shares> GetSharesAsync(CancellationToken? cancelToken = null);

        Task<bool> ChangePasswordAsync(string username, string password);

        Task<User> GetUserAsync(string username, CancellationToken? cancelToken = null);

        Task<IImageFormat<T>> GetAvatarAsync(string username, CancellationToken? cancelToken = null);

        Task<bool> StarAsync(IEnumerable<string> id = null, IEnumerable<string> albumId = null, IEnumerable<string> artistId = null);

        Task<bool> UnStarAsync(IEnumerable<string> id = null, IEnumerable<string> albumId = null, IEnumerable<string> artistId = null);

        Task<bool> SetRatingAsync(string id, int rating);

        Task<bool> CreateUserAsync(string username, string password, bool? ldapAuthenticated = null, bool? adminRole = null, bool? settingsRole = null, bool? streamRole = null, bool? jukeboxRole = null, bool? downloadRole = null, bool? uploadRole = null, bool? playlistRole = null, bool? coverArtRole = null, bool? commentRole = null, bool? podcastRole = null);

        Task<bool> DeleteUserAsync(string username);

        Task<ChatMessages> GetChatMessagesAsync(double? since = null, CancellationToken? cancelToken = null);

        Task<bool> AddChatMessageAsync(string message);

        Task<AlbumList> GetAlbumListAsync(AlbumListType type, int? size = null, int? offset = null, CancellationToken? cancelToken = null);

        Task<AlbumList2> GetAlbumList2Async(AlbumListType type, int? size = null, int? offset = null, CancellationToken? cancelToken = null);

        Task<RandomSongs> GetRandomSongsAsync(int? size = null, string genre = null, int? fromYear = null, int? toYear = null, string musicFolderId = null, CancellationToken? cancelToken = null);

        Task<Lyrics> GetLyricsAsync(string artist = null, string title = null, CancellationToken? cancelToken = null);

        Task<JukeboxPlaylist> JukeboxControlAsync(CancellationToken? cancelToken = null);

        Task<bool> JukeboxControlAsync(JukeboxControlAction action, int? index = null, float? gain = null, IEnumerable<string> id = null);

        Task<Podcasts> GetPodcastsAsync(CancellationToken? cancelToken = null);

        Task<Genres> GetGenresAsync(CancellationToken? cancelToken = null);

        Task<Songs> GetSongsByGenreAsync(string genre, int? count = null, int? offset = null, CancellationToken? cancelToken = null);

        Task<long> StreamAsync(string id, string path, int? maxBitRate = null, StreamFormat? format = null, int? timeOffset = null, string size = null, bool? estimateContentLength = null, CancellationToken? cancelToken = null, bool noResponse = false);

        Task<long> DownloadAsync(string id, string path, bool pathOverride = false, CancellationToken? cancelToken = null);
   }
}
