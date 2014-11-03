using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Subsonic.Common.Classes;
using Subsonic.Common.Enums;

namespace Subsonic.Common.Interfaces
{
    public interface ISubsonicClient<T>
    {
        /// <summary>
        /// Used to test connectivity with the server.
        /// </summary>
        /// <returns>bool</returns>
        Task<bool> PingAsync(CancellationToken? cancelToken = null);

        /// <summary>
        /// Get details about the software license. Please note that access to the REST API requires that the server has a valid license (after a 30-day trial period). To get a license key you can give a donation to the Subsonic project.
        /// </summary>
        /// <returns>License</returns>
        Task<License> GetLicenseAsync(CancellationToken? cancelToken = null);

        /// <summary>
        /// Returns all configured top-level music folders.
        /// </summary>
        /// <returns>MusicFolders</returns>
        Task<MusicFolders> GetMusicFoldersAsync(CancellationToken? cancelToken = null);

        /// <summary>
        /// Returns an indexed structure of all artists.
        /// </summary>
        /// <param name="musicFolderId">If specified, only return artists in the music folder with the given ID.</param>
        /// <param name="ifModifiedSince">If specified, only return a result if the artist collection has changed since the given time.</param>
        /// <param name="cancelToken"></param>
        /// <returns>Indexes</returns>
        Task<Indexes> GetIndexesAsync(string musicFolderId = null, long? ifModifiedSince = null,
            CancellationToken? cancelToken = null);

        /// <summary>
        /// Returns a listing of all files in a music directory. Typically used to get list of albums for an artist, or list of songs for an album.
        /// </summary>
        /// <param name="id">A string which uniquely identifies the music folder. Obtained by calls to GetIndexes or GetMusicDirectory.</param>
        /// <param name="cancelToken"></param>
        /// <returns>Directory</returns>
        Task<Directory> GetMusicDirectoryAsync(string id, CancellationToken? cancelToken = null);

        /// <summary>
        /// Returns all genres.
        /// </summary>
        /// <returns>Genres</returns>
        Task<Genres> GetGenresAsync(CancellationToken? cancelToken = null);

        /// <summary>
        /// Similar to getIndexes, but organizes music according to ID3 tags.
        /// </summary>
        /// <returns>ArtistsID3</returns>
        Task<ArtistsID3> GetArtistsAsync(CancellationToken? cancelToken = null);

        /// <summary>
        /// Returns details for an artist, including a list of albums. This method organizes music according to ID3 tags.
        /// </summary>
        /// <param name="id">The artist ID.</param>
        /// <param name="cancelToken"></param>
        /// <returns>ArtistID3</returns>
        Task<ArtistWithAlbumsID3> GetArtistAsync(string id, CancellationToken? cancelToken = null);

        /// <summary>
        /// Returns details for an album, including a list of songs. This method organizes music according to ID3 tags.
        /// </summary>
        /// <param name="id">The album ID.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>AlbumID3</returns>
        Task<AlbumID3> GetAlbumAsync(string id, CancellationToken? cancelToken = null);

        /// <summary>
        /// Returns details for a song.
        /// </summary>
        /// <param name="id">The song ID.</param>
        /// <param name="cancelToken"></param>
        /// <returns>Song</returns>
        Task<Child> GetSongAsync(string id, CancellationToken? cancelToken = null);

        /// <summary>
        /// Returns all video files.
        /// </summary>
        /// <returns>Videos</returns>
        Task<Videos> GetVideosAsync(CancellationToken? cancelToken = null);

        /// <summary>
        /// Returns a list of random, newest, highest rated etc. albums. Similar to the album lists on the home page of the Subsonic web interface.
        /// </summary>
        /// <param name="type"> The list type. Must be one of the following: random, newest, highest, frequent, recent. Since 1.8.0 you can also use alphabeticalByName or alphabeticalByArtist to page through all albums alphabetically, and starred to retrieve starred albums.</param>
        /// <param name="size">The number of albums to return. Max 500. [Default = 10]</param>
        /// <param name="offset">The list offset. Useful if you for example want to page through the list of newest albums. [Default = 0]</param>
        /// <param name="fromYear">The first year in the range.</param>
        /// <param name="toYear">The last year in the range.</param>
        /// <param name="genre">The name of the genre, e.g., "Rock".</param>
        /// <param name="cancelToken"></param>
        /// <returns>AlbumList</returns>
        Task<AlbumList> GetAlbumListAsync(AlbumListType type, int? size = null, int? offset = null, int? fromYear = null,
            int? toYear = null, string genre = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Similar to getAlbumList, but organizes music according to ID3 tags.
        /// </summary>
        /// <param name="type">The list type. Must be one of the following: random, newest, frequent, recent, starred, alphabeticalByName or alphabeticalByArtist.</param>
        /// <param name="size">The number of albums to return. Max 500. [Default = 10]</param>
        /// <param name="offset">The list offset. Useful if you for example want to page through the list of newest albums. [Default = 0]</param>
        /// <param name="fromYear">The first year in the range.</param>
        /// <param name="toYear">The last year in the range.</param>
        /// <param name="genre">The name of the genre, e.g., "Rock".</param>
        /// <param name="cancelToken"></param>
        /// <returns>AlbumList</returns>
        Task<AlbumList2> GetAlbumList2Async(AlbumListType type, int? size = null, int? offset = null,
            int? fromYear = null, int? toYear = null, string genre = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Returns random songs matching the given criteria.
        /// </summary>
        /// <param name="size">The maximum number of songs to return. Max 500. [Default = 10]</param>
        /// <param name="genre">Only returns songs belonging to this genre.</param>
        /// <param name="fromYear">Only return songs published after or in this year.</param>
        /// <param name="toYear">Only return songs published before or in this year.</param>
        /// <param name="musicFolderId">Only return songs in the music folder with the given ID. See GetMusicFolders.</param>
        /// <param name="cancelToken"></param>
        /// <returns>RandomSongs</returns>
        Task<RandomSongs> GetRandomSongsAsync(int? size = null, string genre = null, int? fromYear = null,
            int? toYear = null, string musicFolderId = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Returns songs in a given genre.
        /// </summary>
        /// <param name="genre">The genre, as returned by getGenres.</param>
        /// <param name="count">The maximum number of songs to return. Max 500.</param>
        /// <param name="offset">The offset. Useful if you want to page through the songs in a genre.</param>
        /// <param name="cancelToken"></param>
        /// <returns>Songs</returns>
        Task<Songs> GetSongsByGenreAsync(string genre, int? count = null, int? offset = null,
            CancellationToken? cancelToken = null);

        /// <summary>
        /// Returns what is currently being played by all users.
        /// </summary>
        /// <returns>NowPlaying</returns>
        Task<NowPlaying> GetNowPlayingAsync(CancellationToken? cancelToken = null);

        /// <summary>
        /// Returns starred songs, albums and artists.
        /// </summary>
        /// <returns>Starred</returns>
        Task<Starred> GetStarredAsync(CancellationToken? cancelToken = null);

        /// <summary>
        /// Similar to getStarred, but organizes music according to ID3 tags.
        /// </summary>
        /// <returns>Starred2</returns>
        Task<Starred2> GetStarred2Async(CancellationToken? cancelToken = null);

        /// <summary>
        /// Returns a listing of files matching the given search criteria. Supports paging through the result. Deprecated since 1.4.0, use Search2 instead.
        /// </summary>
        /// <param name="artist">Artist to search for.</param>
        /// <param name="album">Album to search for.</param>
        /// <param name="title">Song title to search for.</param>
        /// <param name="any">Searches all fields.</param>
        /// <param name="count">Maximum number of results to return. [Default = 20]</param>
        /// <param name="offset">Search result offset. Used for paging. [Default = 0]</param>
        /// <param name="newerThan">Only return matches that are newer this time. Given as milliseconds since Jan 1, 1970.</param>
        /// <param name="cancelToken"></param>
        /// <returns>SearchResult</returns>
        Task<SearchResult> SearchAsync(string artist = null, string album = null, string title = null, string any = null,
            int? count = null, int? offset = null, long? newerThan = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Returns albums, artists and songs matching the given search criteria. Supports paging through the result.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="artistCount">Maximum number of artists to return. [Default = 20]</param>
        /// <param name="artistOffset">Search result offset for artists. Used for paging. [Default = 0]</param>
        /// <param name="albumCount">Maximum number of albums to return. [Default = 20]</param>
        /// <param name="albumOffset">Search result offset for albums. Used for paging. [Default = 0]</param>
        /// <param name="songCount">Maximum number of songs to return. [Default = 20]</param>
        /// <param name="songOffset">Search result offset for songs. Used for paging. [Default = 0]</param>
        /// <param name="cancelToken"></param>
        /// <returns>SearchResult2</returns>
        Task<SearchResult2> Search2Async(string query, int? artistCount = null, int? artistOffset = null,
            int? albumCount = null, int? albumOffset = null, int? songCount = null, int? songOffset = null,
            CancellationToken? cancelToken = null);

        /// <summary>
        /// Similar to search2, but organizes music according to ID3 tags.
        /// </summary>
        /// <param name="query">Search query.</param>
        /// <param name="artistCount">Maximum number of artists to return. [Default = 20]</param>
        /// <param name="artistOffset">Search result offset for artists. Used for paging. [Default = 0]</param>
        /// <param name="albumCount">Maximum number of albums to return. [Default = 20]</param>
        /// <param name="albumOffset">Search result offset for albums. Used for paging. [Default = 0]</param>
        /// <param name="songCount">Maximum number of songs to return. [Default = 20]</param>
        /// <param name="songOffset">Search result offset for songs. Used for paging. [Default = 0]</param>
        /// <param name="cancelToken"></param>
        /// <returns>SearchResult3</returns>
        Task<SearchResult3> Search3Async(string query, int? artistCount = null, int? artistOffset = null,
            int? albumCount = null, int? albumOffset = null, int? songCount = null, int? songOffset = null,
            CancellationToken? cancelToken = null);

        /// <summary>
        /// Returns the ID and name of all saved playlists.
        /// </summary>
        /// <param name="username">(Since 1.8.0) If specified, return playlists for this user rather than for the authenticated user. The authenticated user must have admin role if this parameter is used.</param>
        /// <param name="cancelToken"></param>
        /// <returns>Playlists</returns>
        Task<Playlists> GetPlaylistsAsync(string username = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Returns a listing of files in a saved playlist.
        /// </summary>
        /// <param name="id">ID of the playlist to return, as obtained by GetPlaylists.</param>
        /// <param name="cancelToken"></param>
        /// <returns>PlaylistWithSongs</returns>
        Task<PlaylistWithSongs> GetPlaylistAsync(string id, CancellationToken? cancelToken = null);

        /// <summary>
        /// Creates or updates a saved playlist. Note: The user must be authorized to create playlists (see Settings > Users > User is allowed to create and delete playlists).
        /// </summary>
        /// <param name="playlistId">The playlist ID.</param>
        /// <param name="name">The human-readable name of the playlist.</param>
        /// <param name="songId">ID of a song in the playlist. Use one songId parameter for each song in the playlist.</param>
        /// <param name="cancelToken"></param>
        /// <returns>bool</returns>
        Task<bool> CreatePlaylistAsync(string playlistId = null, string name = null, IEnumerable<string> songId = null,
            CancellationToken? cancelToken = null);

        /// <summary>
        /// Updates a playlist. Only the owner of a playlist is allowed to update it.
        /// </summary>
        /// <param name="playlistId">The playlist ID.</param>
        /// <param name="name">The human-readable name of the playlist.</param>
        /// <param name="comment">The playlist comment.</param>
        /// <param name="songIdToAdd">Add this song with this ID to the playlist. Multiple parameters allowed.</param>
        /// <param name="songIndexToRemove">Remove the song at this position in the playlist. Multiple parameters allowed.</param>
        /// <param name="cancelToken"></param>
        /// <returns>bool</returns>
        Task<bool> UpdatePlaylistAsync(string playlistId, string name = null, string comment = null,
            IEnumerable<string> songIdToAdd = null, IEnumerable<string> songIndexToRemove = null,
            CancellationToken? cancelToken = null);

        /// <summary>
        /// Deletes a saved playlist.
        /// </summary>
        /// <param name="id">ID of the playlist to delete, as obtained by GetPlaylists.</param>
        /// <param name="cancelToken"></param>
        /// <returns>bool</returns>
        Task<bool> DeletePlaylistAsync(string id, CancellationToken? cancelToken = null);

        /// <summary>
        /// Streams a given media file.
        /// </summary>
        /// <param name="id">A string which uniquely identifies the file to stream. Obtained by calls to getMusicDirectory.</param>
        /// <param name="path"></param>
        /// <param name="maxBitRate">(Since 1.2.0) If specified, the server will attempt to limit the bitrate to this value, in kilobits per second. If set to zero, no limit is imposed.</param>
        /// <param name="format">(Since 1.6.0) Specifies the preferred target format (e.g., "mp3" or "flv") in case there are multiple applicable transcodings. (Since 1.9.0) you can use the special value "raw" to disable transcoding.</param>
        /// <param name="timeOffset">Only applicable to video streaming. If specified, start streaming at the given offset (in seconds) into the video. Typically used to implement video skipping.</param>
        /// <param name="size">(Since 1.6.0) Only applicable to video streaming. Requested video size specified as WxH, for instance "640x480".</param>
        /// <param name="estimateContentLength">(Since 1.8.0). If set to "true", the Content-Length HTTP header will be set to an estimated value for transcoded or downsampled media.</param>
        /// <param name="cancelToken"></param>
        /// <param name="noResponse"></param>
        /// <returns>long</returns>
        Task<long> StreamAsync(string id, string path, int? maxBitRate = null, StreamFormat? format = null,
            int? timeOffset = null, string size = null, bool? estimateContentLength = null,
            CancellationToken? cancelToken = null, bool noResponse = false);

        /// <summary>
        /// Downloads a given media file. Similar to stream, but this method returns the original media data without transcoding or downsampling.
        /// </summary>
        /// <param name="id">A string which uniquely identifies the file to download. Obtained by calls to GetMusicDirectory.</param>
        /// <param name="path"></param>
        /// <param name="pathOverride"></param>
        /// <param name="cancelToken"></param>
        /// <returns>long</returns>
        Task<long> DownloadAsync(string id, string path, bool pathOverride = false,
            CancellationToken? cancelToken = null);

        /// <summary>
        /// Creates an HLS (HTTP Live Streaming) playlist used for streaming video or audio. HLS is a streaming protocol implemented by Apple and works by breaking the overall stream into a sequence of small HTTP-based file downloads. It's supported by iOS and newer versions of Android. This method also supports adaptive bitrate streaming, see the bitRate parameter.
        /// </summary>
        /// <param name="id">A string which uniquely identifies the media file to stream.</param>
        /// <param name="bitRate">If specified, the server will attempt to limit the bitrate to this value, in kilobits per second. If this parameter is specified more than once, the server will create a variant playlist, suitable for adaptive bitrate streaming. The playlist will support streaming at all the specified bitrates. The server will automatically choose video dimensions that are suitable for the given bitrates. Since 1.9.0 you may explicitly request a certain width (480) and height (360) like so: bitRate=1000@480x360</param>
        /// <param name="cancelToken"></param>
        /// <returns>string</returns>
        Task<string> HlsAsync(string id, int? bitRate = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Returns a cover art image.
        /// </summary>
        /// <param name="id">A string which uniquely identifies the cover art file to download. Obtained by calls to getMusicDirectory.</param>
        /// <param name="size">If specified, scale image to this size.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>bool</returns>
        Task<IImageFormat<T>> GetCoverArtAsync(string id, int? size = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Searches for and returns lyrics for a given song.
        /// </summary>
        /// <param name="artist">The artist name.</param>
        /// <param name="title">The song title.</param>
        /// <param name="cancelToken"></param>
        /// <returns>Lyrics</returns>
        Task<Lyrics> GetLyricsAsync(string artist = null, string title = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Returns the avatar (personal image) for a user.
        /// </summary>
        /// <param name="username">The user in question.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>Image</returns>
        Task<IImageFormat<T>> GetAvatarAsync(string username, CancellationToken? cancelToken = null);

        /// <summary>
        /// Attaches a star to a song, album or artist.
        /// </summary>
        /// <param name="id">The ID of the file (song) or folder (album/artist) to star. Multiple parameters allowed.</param>
        /// <param name="albumId">The ID of an album to star. Use this rather than id if the client accesses the media collection according to ID3 tags rather than file structure. Multiple parameters allowed.</param>
        /// <param name="artistId">The ID of an artist to star. Use this rather than id if the client accesses the media collection according to ID3 tags rather than file structure. Multiple parameters allowed.</param>
        /// <param name="cancelToken"></param>
        /// <returns>bool</returns>
        Task<bool> StarAsync(IEnumerable<string> id = null, IEnumerable<string> albumId = null,
            IEnumerable<string> artistId = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Removes the star from a song, album or artist.
        /// </summary>
        /// <param name="id">The ID of the file (song) or folder (album/artist) to unstar. Multiple parameters allowed.</param>
        /// <param name="albumId">The ID of an album to unstar. Use this rather than id if the client accesses the media collection according to ID3 tags rather than file structure. Multiple parameters allowed.</param>
        /// <param name="artistId">The ID of an artist to unstar. Use this rather than id if the client accesses the media collection according to ID3 tags rather than file structure. Multiple parameters allowed.</param>
        /// <param name="cancelToken"></param>
        /// <returns>bool</returns>
        Task<bool> UnStarAsync(IEnumerable<string> id = null, IEnumerable<string> albumId = null,
            IEnumerable<string> artistId = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Sets the rating for a music file.
        /// </summary>
        /// <param name="id">A string which uniquely identifies the file (song) or folder (album/artist) to rate.</param>
        /// <param name="rating">The rating between 1 and 5 (inclusive), or 0 to remove the rating.</param>
        /// <param name="cancelToken"></param>
        /// <returns>bool</returns>
        Task<bool> SetRatingAsync(string id, int rating, CancellationToken? cancelToken = null);

        /// <summary>
        /// "Scrobbles" a given music file on last.fm. Requires that the user has configured his/her last.fm credentials on the Subsonic server (Settings > Personal).
        /// </summary>
        /// <param name="id">A string which uniquely identifies the file to scrobble.</param>
        /// <param name="submission">Whether this is a "submission" or a "now playing" notification. [Default = true]</param>
        /// <param name="time">(Since 1.8.0) The time (in milliseconds since 1 Jan 1970) at which the song was listened to.</param>
        /// <param name="cancelToken"></param>
        /// <returns>bool</returns>
        Task<bool> ScrobbleAsync(string id, bool? submission = null, long? time = null,
            CancellationToken? cancelToken = null);

        /// <summary>
        /// Returns information about shared media this user is allowed to manage.
        /// </summary>
        /// <returns>Shares</returns>
        Task<Shares> GetSharesAsync(CancellationToken? cancelToken = null);

        /// <summary>
        /// Creates a public URL that can be used by anyone to stream music or video from the Subsonic server. The URL is short and suitable for posting on Facebook, Twitter etc. Note: The user must be authorized to share.
        /// </summary>
        /// <param name="id">ID of a song, album or video to share. Use one id parameter for each entry to share.</param>
        /// <param name="description">A user-defined description that will be displayed to people visiting the shared media.</param>
        /// <param name="expires">The time at which the share expires. Given as milliseconds since 1970.</param>
        /// <param name="cancelToken"></param>
        /// <returns>Shares</returns>
        Task<Shares> CreateShareAsync(IEnumerable<string> id, string description = null, long? expires = null,
            CancellationToken? cancelToken = null);

        /// <summary>
        /// Updates the description and/or expiration date for an existing share.
        /// </summary>
        /// <param name="id">ID of the share to update.</param>
        /// <param name="description">A user-defined description that will be displayed to people visiting the shared media.</param>
        /// <param name="expires">The time at which the share expires. Given as milliseconds since 1970, or zero to remove the expiration.</param>
        /// <param name="cancelToken"></param>
        /// <returns>Shares</returns>
        Task<bool> UpdateShareAsync(string id, string description = null, long? expires = null,
            CancellationToken? cancelToken = null);

        /// <summary>
        /// Deletes an existing share.
        /// </summary>
        /// <param name="id">ID of the share to delete.</param>
        /// <param name="cancelToken"></param>
        /// <returns>Shares</returns>
        Task<bool> DeleteShareAsync(string id, CancellationToken? cancelToken = null);

        /// <summary>
        /// Returns all podcast channels the server subscribes to and their episodes.
        /// </summary>
        /// <returns>Podcasts</returns>
        Task<Podcasts> GetPodcastsAsync(string id = null, bool? includeEpisodes = null,
            CancellationToken? cancelToken = null);

        /// <summary>
        /// Requests the server to check for new Podcast episodes. Note: The user must be authorized for Podcast administration.
        /// </summary>
        /// <returns>bool</returns>
        Task<bool> RefreshPodcastsAsync(CancellationToken? cancelToken = null);

        /// <summary>
        /// Adds a new Podcast channel. Note: The user must be authorized for Podcast administration.
        /// </summary>
        /// <param name="url">The URL of the Podcast to add.</param>
        /// <param name="cancelToken"></param>
        /// <returns>bool</returns>
        Task<bool> CreatePodcastChannelAsync(string url, CancellationToken? cancelToken = null);

        /// <summary>
        /// Deletes a Podcast channel. Note: The user must be authorized for Podcast administration.
        /// </summary>
        /// <param name="id">The ID of the Podcast channel to delete.</param>
        /// <param name="cancelToken"></param>
        /// <returns>bool</returns>
        Task<bool> DeletePodcastChannelAsync(string id, CancellationToken? cancelToken = null);

        /// <summary>
        /// Deletes a Podcast episode. Note: The user must be authorized for Podcast administration.
        /// </summary>
        /// <param name="id">The ID of the Podcast episode to delete.</param>
        /// <param name="cancelToken"></param>
        /// <returns>bool</returns>
        Task<bool> DeletePodcastEpisodeAsync(string id, CancellationToken? cancelToken = null);

        /// <summary>
        /// Request the server to start downloading a given Podcast episode. Note: The user must be authorized for Podcast administration.
        /// </summary>
        /// <param name="id">The ID of the Podcast episode to download.</param>
        /// <param name="path"></param>
        /// <param name="pathOverride"></param>
        /// <param name="cancelToken"></param>
        /// <returns>long</returns>
        Task<long> DownloadPodcastEpisodeAsync(string id, string path, bool pathOverride = false,
            CancellationToken? cancelToken = null);

        /// <summary>
        /// Controls the jukebox, i.e., playback directly on the server's audio hardware. Note: The user must be authorized to control the jukebox.
        /// </summary>
        /// <param name="action">The operation to perform. Must be one of: start, stop, skip, add, clear, remove, shuffle, setGain</param>
        /// <param name="index">Used by skip and remove. Zero-based index of the song to skip to or remove.</param>
        /// <param name="gain"> Used by setGain to control the playback volume. A float value between 0.0 and 1.0.</param>
        /// <param name="id">Used by add. ID of song to add to the jukebox playlist. Use multiple id parameters to add many songs in the same request.</param>
        /// <param name="cancelToken"></param>
        /// <returns>bool</returns>
        Task<bool> JukeboxControlAsync(JukeboxControlAction action, int? index = null, float? gain = null,
            IEnumerable<string> id = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Retrieves the jukebox playlist. Note: The user must be authorized to control the jukebox.
        /// </summary>
        /// <returns>JukeboxPlaylist</returns>
        Task<JukeboxPlaylist> JukeboxControlAsync(CancellationToken? cancelToken = null);

        /// <summary>
        /// Returns all internet radio stations.
        /// </summary>
        /// <returns>InternetRadioStations</returns>
        Task<InternetRadioStations> GetInternetRadioStationsAsync(CancellationToken? cancelToken = null);

        /// <summary>
        /// Returns the current visible (non-expired) chat messages.
        /// </summary>Only return messages that are newer than this time. Given as milliseconds since Jan 1, 1970.
        /// <param name="since"></param>
        /// <param name="cancelToken"></param>
        /// <returns>ChatMessages</returns>
        Task<ChatMessages> GetChatMessagesAsync(double? since = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Adds a message to the chat log.
        /// </summary>
        /// <param name="message">The chat message.</param>
        /// <param name="cancelToken"></param>
        /// <returns>bool</returns>
        Task<bool> AddChatMessageAsync(string message, CancellationToken? cancelToken = null);

        /// <summary>
        /// Get details about a given user, including which authorization roles it has. Can be used to enable/disable certain features in the client, such as jukebox control.
        /// </summary>
        /// <param name="username">The name of the user to retrieve. You can only retrieve your own user unless you have admin privileges.</param>
        /// <param name="cancelToken"></param>
        /// <returns>User</returns>
        Task<User> GetUserAsync(string username, CancellationToken? cancelToken = null);

        /// <summary>
        /// Get details about all users, including which authorization roles they have. Only users with admin privileges are allowed to call this method.
        /// </summary>
        /// <param name="cancelToken"></param>
        /// <returns>Users</returns>
        Task<Users> GetUsersAsync(CancellationToken? cancelToken = null);

        /// <summary>
        /// Creates a new Subsonic user.
        /// </summary>
        /// <param name="username">The name of the new user.</param>
        /// <param name="password">The password for the new user.</param>
        /// <param name="email">The email address of the new user.</param>
        /// <param name="ldapAuthenticated">Whether the user is authenicated in LDAP. [Default = false]</param>
        /// <param name="adminRole">Whether the user is administrator. [Default = false]</param>
        /// <param name="settingsRole">Whether the user is allowed to change settings and password. [Default = true]</param>
        /// <param name="streamRole">Whether the user is allowed to play files. [Default = true]</param>
        /// <param name="jukeboxRole">Whether the user is allowed to play files in jukebox mode. [Default = false]</param>
        /// <param name="downloadRole">Whether the user is allowed to download files. [Default = false]</param>
        /// <param name="uploadRole">Whether the user is allowed to upload files. [Default = false]</param>
        /// <param name="playlistRole">Whether the user is allowed to create and delete playlists. [Default = false]</param>
        /// <param name="coverArtRole">Whether the user is allowed to change cover art and tags. [Default = false]</param>
        /// <param name="commentRole">Whether the user is allowed to create and edit comments and ratings. [Default = false]</param>
        /// <param name="podcastRole">Whether the user is allowed to administrate Podcasts. [Default = false]</param>
        /// <param name="cancelToken"></param>
        /// <returns>bool</returns>
        Task<bool> CreateUserAsync(string username, string password, string email, bool? ldapAuthenticated = null,
            bool? adminRole = null, bool? settingsRole = null, bool? streamRole = null, bool? jukeboxRole = null,
            bool? downloadRole = null, bool? uploadRole = null, bool? playlistRole = null, bool? coverArtRole = null,
            bool? commentRole = null, bool? podcastRole = null, CancellationToken? cancelToken = null);

        /// <summary>
        /// Modifies an existing Subsonic user.
        /// </summary>
        /// <param name="username">The name of the user.</param>
        /// <param name="password">The password of the user, either in clear text or hex-encoded.</param>
        /// <param name="email">The email address of the user.</param>
        /// <param name="ldapAuthenticated">Whether the user is authenicated in LDAP.</param>
        /// <param name="adminRole">Whether the user is administrator.</param>
        /// <param name="settingsRole">Whether the user is allowed to change personal settings and password.</param>
        /// <param name="streamRole">Whether the user is allowed to play files.</param>
        /// <param name="jukeboxRole">Whether the user is allowed to play files in jukebox mode.</param>
        /// <param name="downloadRole">Whether the user is allowed to download files.</param>
        /// <param name="uploadRole">Whether the user is allowed to upload files.</param>
        /// <param name="coverArtRole">Whether the user is allowed to change cover art and tags.</param>
        /// <param name="commentRole">Whether the user is allowed to create and edit comments and ratings.</param>
        /// <param name="podcastRole">Whether the user is allowed to administrate Podcasts.</param>
        /// <param name="shareRole">Whether the user is allowed to share files with anyone.</param>
        /// <param name="cancelToken"></param>
        /// <returns>bool</returns>
        Task<bool> UpdateUserAsync(string username, string password = null, string email = null,
            bool? ldapAuthenticated = null, bool? adminRole = null, bool? settingsRole = null, bool? streamRole = null,
            bool? jukeboxRole = null, bool? downloadRole = null, bool? uploadRole = null, bool? coverArtRole = null,
            bool? commentRole = null, bool? podcastRole = null, bool? shareRole = null,
            CancellationToken? cancelToken = null);

        /// <summary>
        /// Deletes an existing Subsonic user.
        /// </summary>
        /// <param name="username">The name of the user to delete.</param>
        /// <param name="cancelToken"></param>
        /// <returns></returns>
        Task<bool> DeleteUserAsync(string username, CancellationToken? cancelToken = null);

        /// <summary>
        /// Changes the password of an existing Subsonic user, using the following parameters. You can only change your own password unless you have admin privileges.
        /// </summary>
        /// <param name="username">The name of the user which should change its password.</param>
        /// <param name="password">The new password for the user.</param>
        /// <param name="cancelToken"></param>
        /// <returns>bool</returns>
        Task<bool> ChangePasswordAsync(string username, string password, CancellationToken? cancelToken = null);

        /// <summary>
        /// Returns all bookmarks for this user. A bookmark is a position within a certain media file.
        /// </summary>
        /// <returns>Bookmarks</returns>
        Task<Bookmarks> GetBookmarksAsync(CancellationToken? cancelToken = null);

        /// <summary>
        /// Creates or updates a bookmark (a position within a media file). Bookmarks are personal and not visible to other users.
        /// </summary>
        /// <param name="id">ID of the media file to bookmark. If a bookmark already exists for this file it will be overwritten.</param>
        /// <param name="position">The position (in milliseconds) within the media file.</param>
        /// <param name="comment">A user-defined comment.</param>
        /// <param name="cancelToken"></param>
        /// <returns>bool</returns>
        Task<bool> CreateBookmarkAsync(string id, long position, string comment = null,
            CancellationToken? cancelToken = null);

        /// <summary>
        /// Deletes the bookmark for a given file.
        /// </summary>
        /// <param name="id">ID of the media file for which to delete the bookmark. Other users' bookmarks are not affected.</param>
        /// <param name="cancelToken"></param>
        /// <returns>bool</returns>
        Task<bool> DeleteBookmarkAsync(string id, CancellationToken? cancelToken = null);
    }
}