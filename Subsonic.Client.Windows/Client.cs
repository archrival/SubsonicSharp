using Subsonic.Client.Common;
using Subsonic.Client.Common.Enums;
using Subsonic.Common;
using Subsonic.Common.Classes;
using Subsonic.Common.Enums;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Subsonic.Client.Windows
{
    public class Client : ISubsonicClient<Image>
    {
        public SubsonicClient SubsonicClient { get; set; }
        private HttpResponse HttpResponse { get; set; }
        
        public Client(string serverUrl, string userName, string password, string clientName)
        {
            SubsonicClient = new SubsonicClient { Name = clientName, Password = password, ServerUrl = serverUrl, UserName = userName };
            HttpResponse = new HttpResponse(SubsonicClient);
        }

        public Client(string serverUrl, string userName, string password, string proxyServer, int proxyPort, string proxyUserName, string proxyPassword, string clientName)
        {
            SubsonicClient = new SubsonicClient { Name = clientName, Password = password, ServerUrl = serverUrl, UserName = userName, ProxyServerUrl = proxyServer, ProxyPort = proxyPort, ProxyUserName = proxyUserName, ProxyPassword = proxyPassword };
            HttpResponse = new HttpResponse(SubsonicClient);
        }


        /// <summary>
        /// Used to test connectivity with the server.
        /// </summary>
        /// <returns>bool</returns>
        public async Task<bool> PingAsync()
        {
            return await HttpResponse.GetResponseAsync(Methods.ping, Versions.Version100);
        }

        /// <summary>
        /// Used to test connectivity with the server.
        /// </summary>
        /// <returns>bool</returns>
        public bool Ping()
        {
            return HttpResponse.GetResponse(Methods.ping, Versions.Version100);
        }

        /// <summary>
        /// Get details about the software license. Please note that access to the REST API requires that the server has a valid license (after a 30-day trial period). To get a license key you can give a donation to the Subsonic project.
        /// </summary>
        /// <returns>License</returns>
        public async Task<License> GetLicenseAsync(CancellationToken? cancelToken = null)
        {
            return await HttpResponse.GetResponseAsync<License>(Methods.getLicense, Versions.Version100, null, cancelToken);
        }

        /// <summary>
        /// Get details about the software license. Please note that access to the REST API requires that the server has a valid license (after a 30-day trial period). To get a license key you can give a donation to the Subsonic project.
        /// </summary>
        /// <returns>License</returns>
        public License GetLicense()
        {
            return HttpResponse.GetResponse<License>(Methods.getLicense, Versions.Version100);
        }

        /// <summary>
        /// Returns all configured top-level music folders.
        /// </summary>
        /// <returns>MusicFolders</returns>
        public async Task<MusicFolders> GetMusicFoldersAsync(CancellationToken? cancelToken = null)
        {
            return await HttpResponse.GetResponseAsync<MusicFolders>(Methods.getMusicFolders, Versions.Version100, null, cancelToken);
        }

        /// <summary>
        /// Returns all configured top-level music folders.
        /// </summary>
        /// <returns>MusicFolders</returns>
        public MusicFolders GetMusicFolders()
        {
            return HttpResponse.GetResponse<MusicFolders>(Methods.getMusicFolders, Versions.Version100);
        }

        /// <summary>
        /// Returns what is currently being played by all users.
        /// </summary>
        /// <returns>NowPlaying</returns>
        public async Task<NowPlaying> GetNowPlayingAsync(CancellationToken? cancelToken = null)
        {
            return await HttpResponse.GetResponseAsync<NowPlaying>(Methods.getNowPlaying, Versions.Version100, null, cancelToken);
        }

        /// <summary>
        /// Returns what is currently being played by all users.
        /// </summary>
        /// <returns>NowPlaying</returns>
        public NowPlaying GetNowPlaying()
        {
            return HttpResponse.GetResponse<NowPlaying>(Methods.getNowPlaying, Versions.Version100);
        }

        /// <summary>
        /// Returns starred songs, albums and artists.
        /// </summary>
        /// <returns>Starred</returns>
        public async Task<Starred> GetStarredAsync(CancellationToken? cancelToken = null)
        {
            return await HttpResponse.GetResponseAsync<Starred>(Methods.getStarred, Versions.Version180, null, cancelToken);
        }

        /// <summary>
        /// Returns starred songs, albums and artists.
        /// </summary>
        /// <returns>Starred</returns>
        public Starred GetStarred()
        {
            return HttpResponse.GetResponse<Starred>(Methods.getStarred, Versions.Version180);
        }

        /// <summary>
        /// Similar to getStarred, but organizes music according to ID3 tags.
        /// </summary>
        /// <returns>Starred2</returns>
        public async Task<Starred2> GetStarred2Async(CancellationToken? cancelToken = null)
        {
            return await HttpResponse.GetResponseAsync<Starred2>(Methods.getStarred2, Versions.Version180, null, cancelToken);
        }

        /// <summary>
        /// Similar to getStarred, but organizes music according to ID3 tags.
        /// </summary>
        /// <returns>Starred2</returns>
        public Starred2 GetStarred2()
        {
            return HttpResponse.GetResponse<Starred2>(Methods.getStarred2, Versions.Version180);
        }

        /// <summary>
        /// Returns an indexed structure of all artists.
        /// </summary>
        /// <param name="musicFolderId">If specified, only return artists in the music folder with the given ID.</param>
        /// <param name="ifModifiedSince">If specified, only return a result if the artist collection has changed since the given time.</param>
        /// <param name="cancelToken"></param>
        /// <returns>Indexes</returns>
        public async Task<Indexes> GetIndexesAsync(string musicFolderId = null, long? ifModifiedSince = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.MusicFolderId, musicFolderId);
            parameters.Add(Constants.IfModifiedSince, ifModifiedSince);

            return await HttpResponse.GetResponseAsync<Indexes>(Methods.getIndexes, Versions.Version100, parameters, cancelToken);
        }

        /// <summary>
        /// Returns an indexed structure of all artists.
        /// </summary>
        /// <param name="musicFolderId">If specified, only return artists in the music folder with the given ID.</param>
        /// <param name="ifModifiedSince">If specified, only return a result if the artist collection has changed since the given time.</param>
        /// <returns>Indexes</returns>
        public Indexes GetIndexes(string musicFolderId = null, long? ifModifiedSince = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.MusicFolderId, musicFolderId);
            parameters.Add(Constants.IfModifiedSince, ifModifiedSince);

            return HttpResponse.GetResponse<Indexes>(Methods.getIndexes, Versions.Version100, parameters);
        }

        /// <summary>
        /// Returns a listing of all files in a music directory. Typically used to get list of albums for an artist, or list of songs for an album.
        /// </summary>
        /// <param name="id">A string which uniquely identifies the music folder. Obtained by calls to GetIndexes or GetMusicDirectory.</param>
        /// <param name="cancelToken"></param>
        /// <returns>Directory</returns>
        public async Task<Directory> GetMusicDirectoryAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);
            
            return await HttpResponse.GetResponseAsync<Directory>(Methods.getMusicDirectory, Versions.Version100, parameters, cancelToken);
        }

        /// <summary>
        /// Returns a listing of all files in a music directory. Typically used to get list of albums for an artist, or list of songs for an album.
        /// </summary>
        /// <param name="id">A string which uniquely identifies the music folder. Obtained by calls to GetIndexes or GetMusicDirectory.</param>
        /// <returns>Directory</returns>
        public Directory GetMusicDirectory(string id)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return HttpResponse.GetResponse<Directory>(Methods.getMusicDirectory, Versions.Version100, parameters);
        }

        /// <summary>
        /// Returns details for an artist, including a list of albums. This method organizes music according to ID3 tags.
        /// </summary>
        /// <param name="id">The artist ID.</param>
        /// <param name="cancelToken"></param>
        /// <returns>ArtistID3</returns>
        public async Task<ArtistWithAlbumsID3> GetArtistAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return await HttpResponse.GetResponseAsync<ArtistWithAlbumsID3>(Methods.getArtist, Versions.Version180, parameters, cancelToken);
        }

        /// <summary>
        /// Returns details for an artist, including a list of albums. This method organizes music according to ID3 tags.
        /// </summary>
        /// <param name="id">The artist ID.</param>
        /// <returns>ArtistID3</returns>
        public ArtistWithAlbumsID3 GetArtist(string id)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return HttpResponse.GetResponse<ArtistWithAlbumsID3>(Methods.getArtist, Versions.Version180, parameters);
        }

        /// <summary>
        /// Similar to getIndexes, but organizes music according to ID3 tags.
        /// </summary>
        /// <returns>ArtistsID3</returns>
        public async Task<ArtistsID3> GetArtistsAsync(CancellationToken? cancelToken = null)
        {
            return await HttpResponse.GetResponseAsync<ArtistsID3>(Methods.getArtists, Versions.Version180, null, cancelToken);
        }

        /// <summary>
        /// Similar to getIndexes, but organizes music according to ID3 tags.
        /// </summary>
        /// <returns>ArtistsID3</returns>
        public ArtistsID3 GetArtists()
        {
            return HttpResponse.GetResponse<ArtistsID3>(Methods.getArtists, Versions.Version180);
        }

        /// <summary>
        /// Returns details for an album, including a list of songs. This method organizes music according to ID3 tags.
        /// </summary>
        /// <param name="id">The album ID.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>AlbumID3</returns>
        public async Task<AlbumID3> GetAlbumAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return await HttpResponse.GetResponseAsync<AlbumID3>(Methods.getAlbum, Versions.Version180, parameters, cancelToken);
        }

        /// <summary>
        /// Returns details for an album, including a list of songs. This method organizes music according to ID3 tags.
        /// </summary>
        /// <param name="id">The album ID.</param>
        /// <returns>AlbumID3</returns>
        public AlbumID3 GetAlbum(string id)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return HttpResponse.GetResponse<AlbumID3>(Methods.getAlbum, Versions.Version180, parameters);
        }

        /// <summary>
        /// Returns details for a song.
        /// </summary>
        /// <param name="id">The song ID.</param>
        /// <param name="cancelToken"></param>
        /// <returns>Song</returns>
        public async Task<Child> GetSongAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return await HttpResponse.GetResponseAsync<Child>(Methods.getSong, Versions.Version180, parameters, cancelToken);
        }

        /// <summary>
        /// Returns details for a song.
        /// </summary>
        /// <param name="id">The song ID.</param>
        /// <returns>Song</returns>
        public Child GetSong(string id)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return HttpResponse.GetResponse<Child>(Methods.getSong, Versions.Version180, parameters);
        }

        /// <summary>
        /// Returns all video files.
        /// </summary>
        /// <returns>Videos</returns>
        public async Task<Videos> GetVideosAsync(CancellationToken? cancelToken = null)
        {
            return await HttpResponse.GetResponseAsync<Videos>(Methods.getVideos, Versions.Version180, null, cancelToken);
        }

        /// <summary>
        /// Returns all video files.
        /// </summary>
        /// <returns>Videos</returns>
        public Videos GetVideos()
        {
            return HttpResponse.GetResponse<Videos>(Methods.getVideos, Versions.Version180);
        }

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
        /// <returns>SearchResult</returns>
        public async Task<SearchResult> SearchAsync(string artist = null, string album = null, string title = null, string any = null, int? count = null, int? offset = null, long? newerThan = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Artist, artist);
            parameters.Add(Constants.Album, album);
            parameters.Add(Constants.Title, title);
            parameters.Add(Constants.Any, any);
            parameters.Add(Constants.Count, count);
            parameters.Add(Constants.Offset, offset);
            parameters.Add(Constants.NewerThan, newerThan);

            return await HttpResponse.GetResponseAsync<SearchResult>(Methods.search, Versions.Version100, parameters);
        }

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
        /// <returns>SearchResult</returns>
        public SearchResult Search(string artist = null, string album = null, string title = null, string any = null, int? count = null, int? offset = null, long? newerThan = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Artist, artist);
            parameters.Add(Constants.Album, album);
            parameters.Add(Constants.Title, title);
            parameters.Add(Constants.Any, any);
            parameters.Add(Constants.Count, count);
            parameters.Add(Constants.Offset, offset);
            parameters.Add(Constants.NewerThan, newerThan);

            return HttpResponse.GetResponse<SearchResult>(Methods.search, Versions.Version100, parameters);
        }

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
        public async Task<SearchResult2> Search2Async(string query, int? artistCount = null, int? artistOffset = null, int? albumCount = null, int? albumOffset = null, int? songCount = null, int? songOffset = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Query, query, true);
            parameters.Add(Constants.ArtistCount, artistCount);
            parameters.Add(Constants.ArtistOffset, artistOffset);
            parameters.Add(Constants.AlbumCount, albumCount);
            parameters.Add(Constants.AlbumOffset, albumOffset);
            parameters.Add(Constants.SongCount, songCount);
            parameters.Add(Constants.SongOffset, songOffset);

            return await HttpResponse.GetResponseAsync<SearchResult2>(Methods.search2, Versions.Version140, parameters, cancelToken);
        }

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
        /// <returns>SearchResult2</returns>
        public SearchResult2 Search2(string query, int? artistCount = null, int? artistOffset = null, int? albumCount = null, int? albumOffset = null, int? songCount = null, int? songOffset = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Query, query, true);
            parameters.Add(Constants.ArtistCount, artistCount);
            parameters.Add(Constants.ArtistOffset, artistOffset);
            parameters.Add(Constants.AlbumCount, albumCount);
            parameters.Add(Constants.AlbumOffset, albumOffset);
            parameters.Add(Constants.SongCount, songCount);
            parameters.Add(Constants.SongOffset, songOffset);

            return HttpResponse.GetResponse<SearchResult2>(Methods.search2, Versions.Version140, parameters);
        }

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
        public async Task<SearchResult3> Search3Async(string query, int? artistCount = null, int? artistOffset = null, int? albumCount = null, int? albumOffset = null, int? songCount = null, int? songOffset = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Query, query, true);
            parameters.Add(Constants.ArtistCount, artistCount);
            parameters.Add(Constants.ArtistOffset, artistOffset);
            parameters.Add(Constants.AlbumCount, albumCount);
            parameters.Add(Constants.AlbumOffset, albumOffset);
            parameters.Add(Constants.SongCount, songCount);
            parameters.Add(Constants.SongOffset, songOffset);

            return await HttpResponse.GetResponseAsync<SearchResult3>(Methods.search3, Versions.Version180, parameters, cancelToken);
        }

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
        /// <returns>SearchResult3</returns>
        public SearchResult3 Search3(string query, int? artistCount = null, int? artistOffset = null, int? albumCount = null, int? albumOffset = null, int? songCount = null, int? songOffset = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Query, query, true);
            parameters.Add(Constants.ArtistCount, artistCount);
            parameters.Add(Constants.ArtistOffset, artistOffset);
            parameters.Add(Constants.AlbumCount, albumCount);
            parameters.Add(Constants.AlbumOffset, albumOffset);
            parameters.Add(Constants.SongCount, songCount);
            parameters.Add(Constants.SongOffset, songOffset);

            return HttpResponse.GetResponse<SearchResult3>(Methods.search3, Versions.Version180, parameters);
        }

        /// <summary>
        /// Returns the ID and name of all saved playlists.
        /// </summary>
        /// <param name="username">(Since 1.8.0) If specified, return playlists for this user rather than for the authenticated user. The authenticated user must have admin role if this parameter is used.</param>
        /// <param name="cancelToken"></param>
        /// <returns>Playlists</returns>
        public async Task<Playlists> GetPlaylistsAsync(string username = null, CancellationToken? cancelToken = null)
        {
            var methodApiVersion = Versions.Version100;
            var parameters = SubsonicParameters.Create();

            if (!string.IsNullOrWhiteSpace(username))
            {
                parameters.Add(Constants.Username, username);
                methodApiVersion = Versions.Version180;
            }

            return await HttpResponse.GetResponseAsync<Playlists>(Methods.getPlaylists, methodApiVersion, parameters, cancelToken);
        }

        /// <summary>
        /// Returns the ID and name of all saved playlists.
        /// </summary>
        /// <param name="username">(Since 1.8.0) If specified, return playlists for this user rather than for the authenticated user. The authenticated user must have admin role if this parameter is used.</param>
        /// <returns>Playlists</returns>
        public Playlists GetPlaylists(string username = null)
        {
            var methodApiVersion = Versions.Version100;
            var parameters = SubsonicParameters.Create();

            if (!string.IsNullOrWhiteSpace(username))
            {
                parameters.Add(Constants.Username, username);
                methodApiVersion = Versions.Version180;
            }

            return HttpResponse.GetResponse<Playlists>(Methods.getPlaylists, methodApiVersion, parameters);
        }

        /// <summary>
        /// Returns a listing of files in a saved playlist.
        /// </summary>
        /// <param name="id">ID of the playlist to return, as obtained by GetPlaylists.</param>
        /// <param name="cancelToken"></param>
        /// <returns>PlaylistWithSongs</returns>
        public async Task<PlaylistWithSongs> GetPlaylistAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return await HttpResponse.GetResponseAsync<PlaylistWithSongs>(Methods.getPlaylist, Versions.Version100, parameters, cancelToken);
        }

        /// <summary>
        /// Returns a listing of files in a saved playlist.
        /// </summary>
        /// <param name="id">ID of the playlist to return, as obtained by GetPlaylists.</param>
        /// <returns>Playlist</returns>
        public PlaylistWithSongs GetPlaylist(string id)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);
            
            return HttpResponse.GetResponse<PlaylistWithSongs>(Methods.getPlaylist, Versions.Version100, parameters);
        }


        /// <summary>
        /// Creates or updates a saved playlist. Note: The user must be authorized to create playlists (see Settings > Users > User is allowed to create and delete playlists).
        /// </summary>
        /// <param name="playlistId">The playlist ID.</param>
        /// <param name="name">The human-readable name of the playlist.</param>
        /// <param name="songId">ID of a song in the playlist. Use one songId parameter for each song in the playlist.</param>
        /// <returns>bool</returns>
        public async Task<bool> CreatePlaylistAsync(string playlistId = null, string name = null, IEnumerable<string> songId = null)
        {
            if (!string.IsNullOrWhiteSpace(playlistId) && !string.IsNullOrWhiteSpace(name))
                throw new Exceptions.SubsonicApiException("Only one of playlist ID and name can be specified.");

            var parameters = SubsonicParameters.Create(SubsonicParameterTypes.List);

            if (!string.IsNullOrWhiteSpace(playlistId))
                parameters.Add(Constants.PlaylistId, playlistId);
            else if (!string.IsNullOrWhiteSpace(name))
                parameters.Add(Constants.Name, name);
            else
                throw new Exceptions.SubsonicApiException("One of playlist ID and name must be specified.");

            parameters.Add(Constants.SongId, songId);

            return await HttpResponse.GetResponseAsync(Methods.createPlaylist, Versions.Version120, parameters);
        }

        /// <summary>
        /// Creates or updates a saved playlist. Note: The user must be authorized to create playlists (see Settings > Users > User is allowed to create and delete playlists).
        /// </summary>
        /// <param name="playlistId">The playlist ID.</param>
        /// <param name="name">The human-readable name of the playlist.</param>
        /// <param name="songId">ID of a song in the playlist. Use one songId parameter for each song in the playlist.</param>
        /// <returns>bool</returns>
        public bool CreatePlaylist(string playlistId = null, string name = null, IEnumerable<string> songId = null)
        {
            var parameters = SubsonicParameters.Create(SubsonicParameterTypes.List);

            if (!string.IsNullOrWhiteSpace(playlistId))
                parameters.Add(Constants.PlaylistId, playlistId);
            else if (!string.IsNullOrWhiteSpace(name))
                parameters.Add(Constants.Name, name);
            else
                throw new Exceptions.SubsonicApiException("One of playlist ID and name must be specified.");

            parameters.Add(Constants.SongId, songId);

            return HttpResponse.GetResponse(Methods.createPlaylist, Versions.Version120, parameters);
        }

        /// <summary>
        /// Updates a playlist. Only the owner of a playlist is allowed to update it.
        /// </summary>
        /// <param name="playlistId">The playlist ID.</param>
        /// <param name="name">The human-readable name of the playlist.</param>
        /// <param name="comment">The playlist comment.</param>
        /// <param name="songIdToAdd">Add this song with this ID to the playlist. Multiple parameters allowed.</param>
        /// <param name="songIndexToRemove">Remove the song at this position in the playlist. Multiple parameters allowed.</param>
        /// <returns>bool</returns>
        public async Task<bool> UpdatePlaylistAsync(string playlistId, string name = null, string comment = null, IEnumerable<string> songIdToAdd = null, IEnumerable<string> songIndexToRemove = null)
        {
            var parameters = SubsonicParameters.Create(SubsonicParameterTypes.List);
            parameters.Add(Constants.Name, name);
            parameters.Add(Constants.Comment, comment);
            parameters.Add(Constants.SongIdToAdd, songIdToAdd);
            parameters.Add(Constants.SongIndexToRemove, songIndexToRemove);

            return await HttpResponse.GetResponseAsync(Methods.updatePlaylist, Versions.Version180, parameters);
        }

        /// <summary>
        /// Updates a playlist. Only the owner of a playlist is allowed to update it.
        /// </summary>
        /// <param name="playlistId">The playlist ID.</param>
        /// <param name="name">The human-readable name of the playlist.</param>
        /// <param name="comment">The playlist comment.</param>
        /// <param name="songIdToAdd">Add this song with this ID to the playlist. Multiple parameters allowed.</param>
        /// <param name="songIndexToRemove">Remove the song at this position in the playlist. Multiple parameters allowed.</param>
        /// <returns>bool</returns>
        public bool UpdatePlaylist(string playlistId, string name = null, string comment = null, IEnumerable<string> songIdToAdd = null, IEnumerable<string> songIndexToRemove = null)
        {
            var parameters = SubsonicParameters.Create(SubsonicParameterTypes.List);
            parameters.Add(Constants.Name, name);
            parameters.Add(Constants.Comment, comment);
            parameters.Add(Constants.SongIdToAdd, songIdToAdd);
            parameters.Add(Constants.SongIndexToRemove, songIndexToRemove);

            return HttpResponse.GetResponse(Methods.updatePlaylist, Versions.Version180, parameters);
        }

        /// <summary>
        /// Deletes a saved playlist.
        /// </summary>
        /// <param name="id">ID of the playlist to delete, as obtained by GetPlaylists.</param>
        /// <returns>bool</returns>
        public async Task<bool> DeletePlaylistAsync(string id)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return await HttpResponse.GetResponseAsync(Methods.deletePlaylist, Versions.Version120, parameters);
        }

        /// <summary>
        /// Deletes a saved playlist.
        /// </summary>
        /// <param name="id">ID of the playlist to delete, as obtained by GetPlaylists.</param>
        /// <returns>bool</returns>
        public bool DeletePlaylist(string id)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return HttpResponse.GetResponse(Methods.deletePlaylist, Versions.Version120, parameters);
        }

        /// <summary>
        /// Downloads a given media file. Similar to stream, but this method returns the original media data without transcoding or downsampling.
        /// </summary>
        /// <param name="id">A string which uniquely identifies the file to download. Obtained by calls to GetMusicDirectory.</param>
        /// <param name="path"> </param>
        /// <param name="pathOverride"> </param>
        /// <param name="cancelToken"></param>
        /// <returns>long</returns>
        public async Task<long> DownloadAsync(string id, string path, bool pathOverride = false, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return await HttpResponse.GetResponseAsync(path, pathOverride, Methods.download, Versions.Version100, parameters, cancelToken);
        }

        /// <summary>
        /// Downloads a given media file. Similar to stream, but this method returns the original media data without transcoding or downsampling.
        /// </summary>
        /// <param name="id">A string which uniquely identifies the file to download. Obtained by calls to GetMusicDirectory.</param>
        /// <param name="path"> </param>
        /// <param name="pathOverride"> </param>
        /// <returns>long</returns>
        public long Download(string id, string path, bool pathOverride = false)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            return HttpResponse.GetResponse(path, pathOverride, Methods.download, Versions.Version100, parameters);
        }

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
        public async Task<long> StreamAsync(string id, string path, int? maxBitRate = null, StreamFormat? format = null, int? timeOffset = null, string size = null, bool? estimateContentLength = null, CancellationToken? cancelToken = null, bool noResponse = false)
        {
            var methodApiVersion = Versions.Version120;
            
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            if (maxBitRate != null && maxBitRate != 0)
                parameters.Add(Constants.MaxBitRate, maxBitRate);

            if (format != null)
            {
                var streamFormatName = EnumHelper.GetXmlEnumAttribute(format);

                if (streamFormatName != null)
                {
                    parameters.Add(Constants.StreamFormat, streamFormatName);
                    methodApiVersion = format == StreamFormat.Raw ? Versions.Version190 : Versions.Version160;
                }
            }

            if (timeOffset != null)
            {
                parameters.Add(Constants.TimeOffset, timeOffset);
                methodApiVersion = Versions.Version160;
            }

            if (!string.IsNullOrWhiteSpace(size))
            {
                parameters.Add(Constants.Size, size);
                methodApiVersion = Versions.Version160;
            }

            if (estimateContentLength != null)
            {
                parameters.Add(Constants.EstimateContentLength, estimateContentLength);
                methodApiVersion = Versions.Version180;
            }

            if (noResponse)
                return await HttpResponse.GetResponseAsyncNoResponse(Methods.stream, methodApiVersion, parameters, cancelToken);
            
            return await HttpResponse.GetResponseAsync(path, true, Methods.stream, methodApiVersion, parameters, cancelToken);
        }

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
        /// <returns>long</returns>
        public long Stream(string id, string path, int? maxBitRate = null, StreamFormat? format = null, int? timeOffset = null, string size = null, bool? estimateContentLength = null)
        {
            var methodApiVersion = Versions.Version120;

            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);

            if (maxBitRate != null && maxBitRate != 0)
                parameters.Add(Constants.MaxBitRate, maxBitRate);

            if (format != null)
            {
                var streamFormatName = EnumHelper.GetXmlEnumAttribute(format);

                if (streamFormatName != null)
                {
                    parameters.Add(Constants.StreamFormat, streamFormatName);
                    methodApiVersion = format == StreamFormat.Raw ? Versions.Version190 : Versions.Version160;
                }
            }

            if (timeOffset != null)
            {
                parameters.Add(Constants.TimeOffset, timeOffset);
                methodApiVersion = Versions.Version160;
            }

            if (!string.IsNullOrWhiteSpace(size))
            {
                parameters.Add(Constants.Size, size);
                methodApiVersion = Versions.Version160;
            }

            if (estimateContentLength != null)
            {
                parameters.Add(Constants.EstimateContentLength, estimateContentLength);
                methodApiVersion = Versions.Version180;
            }

            return HttpResponse.GetResponse(path, true, Methods.stream, methodApiVersion, parameters);
        }

        ///// <summary>
        ///// Downloads a given media file. Similar to stream, but this method returns the original media data without transcoding or downsampling.
        ///// </summary>
        ///// <param name="id">A string which uniquely identifies the file to download. Obtained by calls to GetMusicDirectory.</param>W
        ///// <returns>long</returns>
        //public string BuildDownloadUrl(string id)
        //{
        //    var parameters = SubsonicParameters.Create();
        //    parameters.Add(Constants.Id, id, true);

        //    return SubsonicRequest.BuildRequestUriUser(Methods.download, Versions._version100, parameters);
        //}

        ///// <summary>
        ///// Streams a given media file.
        ///// </summary>
        ///// <param name="id">A string which uniquely identifies the file to stream. Obtained by calls to getMusicDirectory.</param>
        ///// <param name="maxBitRate">(Since 1.2.0) If specified, the server will attempt to limit the bitrate to this value, in kilobits per second. If set to zero, no limit is imposed.</param>
        ///// <param name="format">(Since 1.6.0) Specifies the preferred target format (e.g., "mp3" or "flv") in case there are multiple applicable transcodings</param>
        ///// <param name="timeOffset">Only applicable to video streaming. If specified, start streaming at the given offset (in seconds) into the video. Typically used to implement video skipping.</param>
        ///// <param name="size">(Since 1.6.0) Only applicable to video streaming. Requested video size specified as WxH, for instance "640x480".</param>
        ///// <param name="estimateContentLength">(Since 1.8.0). If set to "true", the Content-Length HTTP header will be set to an estimated value for transcoded or downsampled media.</param>
        ///// <returns>long</returns>
        //public string BuildStreamUrl(string id, int? maxBitRate = null, StreamFormat? format = null, int? timeOffset = null, string size = null, bool? estimateContentLength = null)
        //{
        //    var methodApiVersion = Versions._version120;

        //    var parameters = SubsonicParameters.Create();
        //    parameters.Add(Constants.Id, id, true);

        //    if (maxBitRate != null && maxBitRate != 0)
        //        parameters.Add(Constants.MaxBitRate, maxBitRate);

        //    if (format != null)
        //    {
        //        var streamFormatName = Enum.GetName(typeof(StreamFormat), format);

        //        if (streamFormatName != null)
        //        {
        //            parameters.Add(Constants.StreamFormat, streamFormatName);
        //            methodApiVersion = Versions._version160;
        //        }
        //    }

        //    if (timeOffset != null)
        //    {
        //        parameters.Add(Constants.TimeOffset, timeOffset);
        //        methodApiVersion = Versions._version160;
        //    }

        //    if (!string.IsNullOrWhiteSpace(size))
        //    {
        //        parameters.Add(Constants.Size, size);
        //        methodApiVersion = Versions._version160;
        //    }

        //    if (estimateContentLength != null)
        //    {
        //        parameters.Add(Constants.EstimateContentLength, estimateContentLength);
        //        methodApiVersion = Versions._version180;
        //    }

        //    return SubsonicRequest.BuildRequestUriUser(Methods.stream, methodApiVersion, parameters);
        //}

        /// <summary>
        /// Returns a cover art image.
        /// </summary>
        /// <param name="id">A string which uniquely identifies the cover art file to download. Obtained by calls to getMusicDirectory.</param>
        /// <param name="size">If specified, scale image to this size.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>bool</returns>
        public async Task<long> GetCoverArtSizeAsync(string id, int? size = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);
            parameters.Add(Constants.Size, size);

            return await HttpResponse.GetImageSizeAsync(Methods.getCoverArt, Versions.Version100, parameters, cancelToken);
        }

        /// <summary>
        /// Returns a cover art image.
        /// </summary>
        /// <param name="id">A string which uniquely identifies the cover art file to download. Obtained by calls to getMusicDirectory.</param>
        /// <param name="size">If specified, scale image to this size.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>bool</returns>
        public async Task<IImageFormat<Image>> GetCoverArtAsync(string id, int? size = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);
            parameters.Add(Constants.Size, size);

            return await HttpResponse.GetImageResponseAsync(Methods.getCoverArt, Versions.Version100, parameters, cancelToken);
        }

        /// <summary>
        /// Returns a cover art image.
        /// </summary>
        /// <param name="id">A string which uniquely identifies the cover art file to download. Obtained by calls to getMusicDirectory.</param>
        /// <param name="size">If specified, scale image to this size.</param>
        /// <returns>bool</returns>
        public IImageFormat<Image> GetCoverArt(string id, int? size = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);
            parameters.Add(Constants.Size, size);

            return HttpResponse.GetImageResponse(Methods.getCoverArt, Versions.Version100, parameters);
        }

        /// <summary>
        /// "Scrobbles" a given music file on last.fm. Requires that the user has configured his/her last.fm credentials on the Subsonic server (Settings > Personal).
        /// </summary>
        /// <param name="id">A string which uniquely identifies the file to scrobble.</param>
        /// <param name="submission">Whether this is a "submission" or a "now playing" notification. [Default = true]</param>
        /// <param name="time">(Since 1.8.0) The time (in milliseconds since 1 Jan 1970) at which the song was listened to.</param>
        /// <returns>bool</returns>
        public async Task<bool> ScrobbleAsync(string id, bool? submission = null, long? time = null)
        {
            var methodApiVersion = Versions.Version150;

            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);
            parameters.Add(Constants.Submission, submission);

            if (time != null)
            {
                parameters.Add(Constants.Time, time);
                methodApiVersion = Versions.Version180;
            }

            return await HttpResponse.GetResponseAsync(Methods.scrobble, methodApiVersion, parameters);
        }

        /// <summary>
        /// "Scrobbles" a given music file on last.fm. Requires that the user has configured his/her last.fm credentials on the Subsonic server (Settings > Personal).
        /// </summary>
        /// <param name="id">A string which uniquely identifies the file to scrobble.</param>
        /// <param name="submission">Whether this is a "submission" or a "now playing" notification. [Default = true]</param>
        /// <param name="time">(Since 1.8.0) The time (in milliseconds since 1 Jan 1970) at which the song was listened to.</param>
        /// <returns>bool</returns>
        public bool Scrobble(string id, bool? submission = null, long? time = null)
        {
            var methodApiVersion = Versions.Version150;

            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);
            parameters.Add(Constants.Submission, submission);

            if (time != null)
            {
                parameters.Add(Constants.Time, time);
                methodApiVersion = Versions.Version180;
            }

            return HttpResponse.GetResponse(Methods.scrobble, methodApiVersion, parameters);
        }

        /// <summary>
        /// Returns information about shared media this user is allowed to manage.
        /// </summary>
        /// <returns>Shares</returns>
        public async Task<Shares> GetSharesAsync(CancellationToken? cancelToken = null)
        {
            return await HttpResponse.GetResponseAsync<Shares>(Methods.getShares, Versions.Version160, null, cancelToken);
        }

        /// <summary>
        /// Returns information about shared media this user is allowed to manage.
        /// </summary>
        /// <returns>Shares</returns>
        public Shares GetShares()
        {
            return HttpResponse.GetResponse<Shares>(Methods.getShares, Versions.Version160);
        }

        /// <summary>
        /// Changes the password of an existing Subsonic user, using the following parameters. You can only change your own password unless you have admin privileges.
        /// </summary>
        /// <param name="username">The name of the user which should change its password.</param>
        /// <param name="password">The new password for the user.</param>
        /// <returns>bool</returns>
        public async Task<bool> ChangePasswordAsync(string username, string password)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Username, username, true);

            if (SubsonicClient.EncodePasswords)
                password = string.Format(CultureInfo.InvariantCulture, "enc:{0}", password.ToHex());

            parameters.Add("password", password, true);

            return await HttpResponse.GetResponseAsync(Methods.changePassword, Versions.Version110, parameters);
        }

        /// <summary>
        /// Changes the password of an existing Subsonic user, using the following parameters. You can only change your own password unless you have admin privileges.
        /// </summary>
        /// <param name="username">The name of the user which should change its password.</param>
        /// <param name="password">The new password for the user.</param>
        /// <returns>bool</returns>
        public bool ChangePassword(string username, string password)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Username, username, true);

            if (SubsonicClient.EncodePasswords)
                password = string.Format(CultureInfo.InvariantCulture, "enc:{0}", password.ToHex());

            parameters.Add("password", password, true);

            return HttpResponse.GetResponse(Methods.changePassword, Versions.Version110, parameters);
        }

        /// <summary>
        /// Get details about a given user, including which authorization roles it has. Can be used to enable/disable certain features in the client, such as jukebox control.
        /// </summary>
        /// <param name="username">The name of the user to retrieve. You can only retrieve your own user unless you have admin privileges.</param>
        /// <param name="cancelToken"></param>
        /// <returns>User</returns>
        public async Task<User> GetUserAsync(string username, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Username, username, true);

            return await HttpResponse.GetResponseAsync<User>(Methods.getUser, Versions.Version130, parameters, cancelToken);
        }

        /// <summary>
        /// Get details about a given user, including which authorization roles it has. Can be used to enable/disable certain features in the client, such as jukebox control.
        /// </summary>
        /// <param name="username">The name of the user to retrieve. You can only retrieve your own user unless you have admin privileges.</param>
        /// <returns>User</returns>
        public User GetUser(string username)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Username, username, true);

            return HttpResponse.GetResponse<User>(Methods.getUser, Versions.Version130, parameters);
        }

        /// <summary>
        /// Returns the avatar (personal image) for a user.
        /// </summary>
        /// <param name="username">The user in question.</param>
        /// <param name="cancelToken"> </param>
        /// <returns>Image</returns>
        public async Task<IImageFormat<Image>> GetAvatarAsync(string username, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Username, username, true);

            return await HttpResponse.GetImageResponseAsync(Methods.getAvatar, Versions.Version180, parameters, cancelToken);
        }

        /// <summary>
        /// Returns the avatar (personal image) for a user.
        /// </summary>
        /// <param name="username">The user in question.</param>
        /// <returns>Image</returns>
        public IImageFormat<Image> GetAvatar(string username)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Username, username, true);

            return HttpResponse.GetImageResponse(Methods.getAvatar, Versions.Version180, parameters);
        }

        /// <summary>
        /// Attaches a star to a song, album or artist.
        /// </summary>
        /// <param name="id">The ID of the file (song) or folder (album/artist) to star. Multiple parameters allowed.</param>
        /// <param name="albumId">The ID of an album to star. Use this rather than id if the client accesses the media collection according to ID3 tags rather than file structure. Multiple parameters allowed.</param>
        /// <param name="artistId">The ID of an artist to star. Use this rather than id if the client accesses the media collection according to ID3 tags rather than file structure. Multiple parameters allowed.</param>
        /// <returns>bool</returns>
        public async Task<bool> StarAsync(IEnumerable<string> id = null, IEnumerable<string> albumId = null, IEnumerable<string> artistId = null)
        {
            if (id == null && albumId == null && artistId == null)
                throw new Exceptions.SubsonicApiException("You must provide one of id, albumId or artistId");

            var parameters = SubsonicParameters.Create(SubsonicParameterTypes.List);
            parameters.Add(Constants.Id, id);
            parameters.Add(Constants.AlbumId, albumId);
            parameters.Add(Constants.ArtistId, artistId);

            return await HttpResponse.GetResponseAsync(Methods.star, Versions.Version180, parameters);
        }

        /// <summary>
        /// Attaches a star to a song, album or artist.
        /// </summary>
        /// <param name="id">The ID of the file (song) or folder (album/artist) to star. Multiple parameters allowed.</param>
        /// <param name="albumId">The ID of an album to star. Use this rather than id if the client accesses the media collection according to ID3 tags rather than file structure. Multiple parameters allowed.</param>
        /// <param name="artistId">The ID of an artist to star. Use this rather than id if the client accesses the media collection according to ID3 tags rather than file structure. Multiple parameters allowed.</param>
        /// <returns>bool</returns>
        public bool Star(IEnumerable<string> id = null, IEnumerable<string> albumId = null, IEnumerable<string> artistId = null)
        {
            if (id == null && albumId == null && artistId == null)
                throw new Exceptions.SubsonicApiException("You must provide one of id, albumId or artistId");

            var parameters = SubsonicParameters.Create(SubsonicParameterTypes.List);
            parameters.Add(Constants.Id, id);
            parameters.Add(Constants.AlbumId, albumId);
            parameters.Add(Constants.ArtistId, artistId);

            return HttpResponse.GetResponse(Methods.star, Versions.Version180, parameters);
        }

        /// <summary>
        /// Removes the star from a song, album or artist.
        /// </summary>
        /// <param name="id">The ID of the file (song) or folder (album/artist) to unstar. Multiple parameters allowed.</param>
        /// <param name="albumId">The ID of an album to unstar. Use this rather than id if the client accesses the media collection according to ID3 tags rather than file structure. Multiple parameters allowed.</param>
        /// <param name="artistId">The ID of an artist to unstar. Use this rather than id if the client accesses the media collection according to ID3 tags rather than file structure. Multiple parameters allowed.</param>
        /// <returns>bool</returns>
        public async Task<bool> UnStarAsync(IEnumerable<string> id = null, IEnumerable<string> albumId = null, IEnumerable<string> artistId = null)
        {
            if (id == null && albumId == null && artistId == null)
                throw new Exceptions.SubsonicApiException("You must provide one of id, albumId or artistId");

            var parameters = SubsonicParameters.Create(SubsonicParameterTypes.List);
            parameters.Add(Constants.Id, id);
            parameters.Add(Constants.AlbumId, albumId);
            parameters.Add(Constants.ArtistId, artistId);

            return await HttpResponse.GetResponseAsync(Methods.unstar, Versions.Version180, parameters);
        }

        /// <summary>
        /// Removes the star from a song, album or artist.
        /// </summary>
        /// <param name="id">The ID of the file (song) or folder (album/artist) to unstar. Multiple parameters allowed.</param>
        /// <param name="albumId">The ID of an album to unstar. Use this rather than id if the client accesses the media collection according to ID3 tags rather than file structure. Multiple parameters allowed.</param>
        /// <param name="artistId">The ID of an artist to unstar. Use this rather than id if the client accesses the media collection according to ID3 tags rather than file structure. Multiple parameters allowed.</param>
        /// <returns>bool</returns>
        public bool UnStar(IEnumerable<string> id = null, IEnumerable<string> albumId = null, IEnumerable<string> artistId = null)
        {
            if (id == null && albumId == null && artistId == null)
                throw new Exceptions.SubsonicApiException("You must provide one of id, albumId or artistId");

            var parameters = SubsonicParameters.Create(SubsonicParameterTypes.List);
            parameters.Add(Constants.Id, id);
            parameters.Add(Constants.AlbumId, albumId);
            parameters.Add(Constants.ArtistId, artistId);

            return HttpResponse.GetResponse(Methods.unstar, Versions.Version180, parameters);
        }

        /// <summary>
        /// Sets the rating for a music file.
        /// </summary>
        /// <param name="id">A string which uniquely identifies the file (song) or folder (album/artist) to rate.</param>
        /// <param name="rating">The rating between 1 and 5 (inclusive), or 0 to remove the rating.</param>
        /// <returns>bool</returns>
        public async Task<bool> SetRatingAsync(string id, int rating)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);
            parameters.Add(Constants.Rating, rating, true);
            
            return await HttpResponse.GetResponseAsync(Methods.setRating, Versions.Version160, parameters);
        }

        /// <summary>
        /// Sets the rating for a music file.
        /// </summary>
        /// <param name="id">A string which uniquely identifies the file (song) or folder (album/artist) to rate.</param>
        /// <param name="rating">The rating between 1 and 5 (inclusive), or 0 to remove the rating.</param>
        /// <returns>bool</returns>
        public bool SetRating(string id, int rating)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Id, id, true);
            parameters.Add(Constants.Rating, rating, true);

            return HttpResponse.GetResponse(Methods.setRating, Versions.Version160, parameters);
        }

        /// <summary>
        /// Creates a new Subsonic user.
        /// </summary>
        /// <param name="username">The name of the new user.</param>
        /// <param name="password">The password for the new user.</param>
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
        /// <returns>bool</returns>
        public async Task<bool> CreateUserAsync(string username, string password, bool? ldapAuthenticated = null, bool? adminRole = null, bool? settingsRole = null, bool? streamRole = null, bool? jukeboxRole = null, bool? downloadRole = null, bool? uploadRole = null, bool? playlistRole = null, bool? coverArtRole = null, bool? commentRole = null, bool? podcastRole = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Username, username, true);

            if (SubsonicClient.EncodePasswords)
                password = string.Format(CultureInfo.InvariantCulture, "enc:{0}", password.ToHex());

            parameters.Add(Constants.Password, password, true);
            parameters.Add(Constants.LdapAuthenticated, ldapAuthenticated);
            parameters.Add(Constants.AdminRole, adminRole);
            parameters.Add(Constants.SettingsRole, settingsRole);
            parameters.Add(Constants.StreamRole, streamRole);
            parameters.Add(Constants.JukeboxRole, jukeboxRole);
            parameters.Add(Constants.DownloadRole, downloadRole);
            parameters.Add(Constants.UploadRole, uploadRole);
            parameters.Add(Constants.PlaylistRole, playlistRole);
            parameters.Add(Constants.CoverArtRole, coverArtRole);
            parameters.Add(Constants.CommentRole, commentRole);
            parameters.Add(Constants.PodcastRole, podcastRole);

            return await HttpResponse.GetResponseAsync(Methods.createUser, Versions.Version130, parameters);
        }

        /// <summary>
        /// Creates a new Subsonic user.
        /// </summary>
        /// <param name="username">The name of the new user.</param>
        /// <param name="password">The password for the new user.</param>
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
        /// <returns>bool</returns>
        public bool CreateUser(string username, string password, bool? ldapAuthenticated = null, bool? adminRole = null, bool? settingsRole = null, bool? streamRole = null, bool? jukeboxRole = null, bool? downloadRole = null, bool? uploadRole = null, bool? playlistRole = null, bool? coverArtRole = null, bool? commentRole = null, bool? podcastRole = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Username, username, true);

            if (SubsonicClient.EncodePasswords)
                password = string.Format(CultureInfo.InvariantCulture, "enc:{0}", password.ToHex());

            parameters.Add(Constants.Password, password, true);
            parameters.Add(Constants.LdapAuthenticated, ldapAuthenticated);
            parameters.Add(Constants.AdminRole, adminRole);
            parameters.Add(Constants.SettingsRole, settingsRole);
            parameters.Add(Constants.StreamRole, streamRole);
            parameters.Add(Constants.JukeboxRole, jukeboxRole);
            parameters.Add(Constants.DownloadRole, downloadRole);
            parameters.Add(Constants.UploadRole, uploadRole);
            parameters.Add(Constants.PlaylistRole, playlistRole);
            parameters.Add(Constants.CoverArtRole, coverArtRole);
            parameters.Add(Constants.CommentRole, commentRole);
            parameters.Add(Constants.PodcastRole, podcastRole);

            return HttpResponse.GetResponse(Methods.createUser, Versions.Version130, parameters);
        }

        /// <summary>
        /// Deletes an existing Subsonic user.
        /// </summary>
        /// <param name="username">The name of the user to delete.</param>
        /// <returns></returns>
        public async Task<bool> DeleteUserAsync(string username)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Username, username, true);
               
            return await HttpResponse.GetResponseAsync(Methods.deleteUser, Versions.Version130, parameters);
        }

        /// <summary>
        /// Deletes an existing Subsonic user.
        /// </summary>
        /// <param name="username">The name of the user to delete.</param>
        /// <returns></returns>
        public bool DeleteUser(string username)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Username, username, true);

            return HttpResponse.GetResponse(Methods.deleteUser, Versions.Version130, parameters);
        }

        /// <summary>
        /// Returns the current visible (non-expired) chat messages.
        /// </summary>Only return messages that are newer than this time. Given as milliseconds since Jan 1, 1970.
        /// <param name="since"></param>
        /// <param name="cancelToken"></param>
        /// <returns>ChatMessages</returns>
        public async Task<ChatMessages> GetChatMessagesAsync(double? since = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Since, since);

            return await HttpResponse.GetResponseAsync<ChatMessages>(Methods.getChatMessages, Versions.Version120, parameters, cancelToken);
        }

        /// <summary>
        /// Returns the current visible (non-expired) chat messages.
        /// </summary>Only return messages that are newer than this time. Given as milliseconds since Jan 1, 1970.
        /// <param name="since"></param>
        /// <returns>ChatMessages</returns>
        public ChatMessages GetChatMessages(long? since = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Since, since);

            return HttpResponse.GetResponse<ChatMessages>(Methods.getChatMessages, Versions.Version120, parameters);
        }

        /// <summary>
        /// Adds a message to the chat log.
        /// </summary>
        /// <param name="message">The chat message.</param>
        /// <returns>bool</returns>
        public async Task<bool> AddChatMessageAsync(string message)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Message, message);

            return await HttpResponse.GetResponseAsync(Methods.addChatMessage, Versions.Version120, parameters);
        }

        /// <summary>
        /// Adds a message to the chat log.
        /// </summary>
        /// <param name="message">The chat message.</param>
        /// <returns>bool</returns>
        public bool AddChatMessage(string message)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Message, message);

            return HttpResponse.GetResponse(Methods.addChatMessage, Versions.Version120, parameters);
        }

        /// <summary>
        /// Returns a list of random, newest, highest rated etc. albums. Similar to the album lists on the home page of the Subsonic web interface.
        /// </summary>
        /// <param name="type">	The list type. Must be one of the following: random, newest, highest, frequent, recent. Since 1.8.0 you can also use alphabeticalByName or alphabeticalByArtist to page through all albums alphabetically, and starred to retrieve starred albums.</param>
        /// <param name="size">The number of albums to return. Max 500. [Default = 10]</param>
        /// <param name="offset">The list offset. Useful if you for example want to page through the list of newest albums. [Default = 0]</param>
        /// <param name="cancelToken"></param>
        /// <returns>AlbumList</returns>
        public async Task<AlbumList> GetAlbumListAsync(AlbumListType type, int? size = null, int? offset = null, CancellationToken? cancelToken = null)
        {
            var methodApiVersion = Versions.Version120;

            if (type == AlbumListType.AlphabeticalByArtist || type == AlbumListType.AlphabeticalByName || type == AlbumListType.Starred)
                methodApiVersion = Versions.Version180;

            var albumListTypeName = EnumHelper.GetXmlEnumAttribute(type);

            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Type, albumListTypeName, true);
            parameters.Add(Constants.Size, size);
            parameters.Add(Constants.Offset, offset);

            return await HttpResponse.GetResponseAsync<AlbumList>(Methods.getAlbumList, methodApiVersion, parameters, cancelToken);
        }

        /// <summary>
        /// Returns a list of random, newest, highest rated etc. albums. Similar to the album lists on the home page of the Subsonic web interface.
        /// </summary>
        /// <param name="type">	The list type. Must be one of the following: random, newest, highest, frequent, recent. Since 1.8.0 you can also use alphabeticalByName or alphabeticalByArtist to page through all albums alphabetically, and starred to retrieve starred albums.</param>
        /// <param name="size">The number of albums to return. Max 500. [Default = 10]</param>
        /// <param name="offset">The list offset. Useful if you for example want to page through the list of newest albums. [Default = 0]</param>
        /// <returns>AlbumList</returns>
        public AlbumList GetAlbumList(AlbumListType type, int? size = null, int? offset = null)
        {
            var methodApiVersion = Versions.Version120;

            if (type == AlbumListType.AlphabeticalByArtist || type == AlbumListType.AlphabeticalByName || type == AlbumListType.Starred)
                methodApiVersion = Versions.Version180;

            var albumListTypeName = EnumHelper.GetXmlEnumAttribute(type);

            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Type, albumListTypeName, true);
            parameters.Add(Constants.Size, size);
            parameters.Add(Constants.Offset, offset);

            return HttpResponse.GetResponse<AlbumList>(Methods.getAlbumList, methodApiVersion, parameters);
        }

        /// <summary>
        /// Similar to getAlbumList, but organizes music according to ID3 tags.
        /// </summary>
        /// <param name="type">The list type. Must be one of the following: random, newest, frequent, recent, starred, alphabeticalByName or alphabeticalByArtist.</param>
        /// <param name="size">The number of albums to return. Max 500. [Default = 10]</param>
        /// <param name="offset">The list offset. Useful if you for example want to page through the list of newest albums. [Default = 0]</param>
        /// <param name="cancelToken"></param>
        /// <returns>AlbumList</returns>
        public async Task<AlbumList2> GetAlbumList2Async(AlbumListType type, int? size = null, int? offset = null, CancellationToken? cancelToken = null)
        {
            var albumListTypeName = EnumHelper.GetXmlEnumAttribute(type);

            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Type, albumListTypeName, true);
            parameters.Add(Constants.Size, size);
            parameters.Add(Constants.Offset, offset);
            
            return await HttpResponse.GetResponseAsync<AlbumList2>(Methods.getAlbumList2, Versions.Version180, parameters, cancelToken);
        }

        /// <summary>
        /// Similar to getAlbumList, but organizes music according to ID3 tags.
        /// </summary>
        /// <param name="type">The list type. Must be one of the following: random, newest, frequent, recent, starred, alphabeticalByName or alphabeticalByArtist.</param>
        /// <param name="size">The number of albums to return. Max 500. [Default = 10]</param>
        /// <param name="offset">The list offset. Useful if you for example want to page through the list of newest albums. [Default = 0]</param>
        /// <returns>AlbumList</returns>
        public AlbumList2 GetAlbumList2(AlbumListType type, int? size = null, int? offset = null)
        {
            var albumListTypeName = EnumHelper.GetXmlEnumAttribute(type);

            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Type, albumListTypeName, true);
            parameters.Add(Constants.Size, size);
            parameters.Add(Constants.Offset, offset);

            return HttpResponse.GetResponse<AlbumList2>(Methods.getAlbumList2, Versions.Version180, parameters);
        }

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
        public async Task<RandomSongs> GetRandomSongsAsync(int? size = null, string genre = null, int? fromYear = null, int? toYear = null, string musicFolderId = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Size, size);
            parameters.Add(Constants.Genre, genre);
            parameters.Add(Constants.FromYear, fromYear);
            parameters.Add(Constants.ToYear, toYear);
            parameters.Add(Constants.MusicFolderId, musicFolderId);

            return await HttpResponse.GetResponseAsync<RandomSongs>(Methods.getRandomSongs, Versions.Version120, parameters, cancelToken);
        }

        /// <summary>
        /// Returns random songs matching the given criteria.
        /// </summary>
        /// <param name="size">The maximum number of songs to return. Max 500. [Default = 10]</param>
        /// <param name="genre">Only returns songs belonging to this genre.</param>
        /// <param name="fromYear">Only return songs published after or in this year.</param>
        /// <param name="toYear">Only return songs published before or in this year.</param>
        /// <param name="musicFolderId">Only return songs in the music folder with the given ID. See GetMusicFolders.</param>
        /// <returns>RandomSongs</returns>
        public RandomSongs GetRandomSongs(int? size = null, string genre = null, int? fromYear = null, int? toYear = null, string musicFolderId = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Size, size);
            parameters.Add(Constants.Genre, genre);
            parameters.Add(Constants.FromYear, fromYear);
            parameters.Add(Constants.ToYear, toYear);
            parameters.Add(Constants.MusicFolderId, musicFolderId);

            return HttpResponse.GetResponse<RandomSongs>(Methods.getRandomSongs, Versions.Version120, parameters);
        }

        /// <summary>
        /// Searches for and returns lyrics for a given song.
        /// </summary>
        /// <param name="artist">The artist name.</param>
        /// <param name="title">The song title.</param>
        /// <param name="cancelToken"></param>
        /// <returns>Lyrics</returns>
        public async Task<Lyrics> GetLyricsAsync(string artist = null, string title = null, CancellationToken? cancelToken = null)
        {
            if (string.IsNullOrWhiteSpace(artist) && string.IsNullOrWhiteSpace(title))
                throw new Exceptions.SubsonicApiException("You must specify an artist and/or a title");

            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Artist, artist);
            parameters.Add(Constants.Title, title);

            return await HttpResponse.GetResponseAsync<Lyrics>(Methods.getLyrics, Versions.Version120, parameters, cancelToken);
        }

        /// <summary>
        /// Searches for and returns lyrics for a given song.
        /// </summary>
        /// <param name="artist">The artist name.</param>
        /// <param name="title">The song title.</param>
        /// <returns>Lyrics</returns>
        public Lyrics GetLyrics(string artist = null, string title = null)
        {
            if (string.IsNullOrWhiteSpace(artist) && string.IsNullOrWhiteSpace(title))
                throw new Exceptions.SubsonicApiException("You must specify an artist and/or a title");

            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Artist, artist);
            parameters.Add(Constants.Title, title);

            return HttpResponse.GetResponse<Lyrics>(Methods.getLyrics, Versions.Version120, parameters);
        }

        /// <summary>
        /// Retrieves the jukebox playlist. Note: The user must be authorized to control the jukebox (see Settings > Users > User is allowed to play files in jukebox mode).
        /// </summary>
        /// <returns>JukeboxPlaylist</returns>
        public async Task<JukeboxPlaylist> JukeboxControlAsync(CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Action, Constants.Get);

            return await HttpResponse.GetResponseAsync<JukeboxPlaylist>(Methods.jukeboxControl, Versions.Version120, parameters, cancelToken);
        }

        /// <summary>
        /// Retrieves the jukebox playlist. Note: The user must be authorized to control the jukebox (see Settings > Users > User is allowed to play files in jukebox mode).
        /// </summary>
        /// <returns>JukeboxPlaylist</returns>
        public JukeboxPlaylist JukeboxControl()
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Action, Constants.Get);

            return HttpResponse.GetResponse<JukeboxPlaylist>(Methods.jukeboxControl, Versions.Version120, parameters);
        }

        /// <summary>
        /// Controls the jukebox, i.e., playback directly on the server's audio hardware. Note: The user must be authorized to control the jukebox (see Settings > Users > User is allowed to play files in jukebox mode).
        /// </summary>
        /// <param name="action">The operation to perform. Must be one of: start, stop, skip, add, clear, remove, shuffle, setGain</param>
        /// <param name="index">Used by skip and remove. Zero-based index of the song to skip to or remove.</param>
        /// <param name="gain">	Used by setGain to control the playback volume. A float value between 0.0 and 1.0.</param>
        /// <param name="id">Used by add. ID of song to add to the jukebox playlist. Use multiple id parameters to add many songs in the same request.</param>
        /// <returns>bool</returns>
        public async Task<bool> JukeboxControlAsync(JukeboxControlAction action, int? index = null, float? gain = null, IEnumerable<string> id = null)
        {
            var actionName = EnumHelper.GetXmlEnumAttribute(action);

            if (string.IsNullOrWhiteSpace(actionName))
                throw new Exceptions.SubsonicApiException("You must provide valid action");

            var parameters = SubsonicParameters.Create(SubsonicParameterTypes.List);

            parameters.Add(Constants.Action, actionName);

            if ((action == JukeboxControlAction.Skip || action == JukeboxControlAction.Remove) && index != null)
                parameters.Add(Constants.Index, index.ToString());

            if (action == JukeboxControlAction.Add)
            {
                if (id == null)
                    throw new Exceptions.SubsonicApiException("You must provide at least 1 ID.");

                parameters.Add(Constants.Id, id);
            }

            if (action == JukeboxControlAction.SetGain)
            {
                if (gain == null || (gain < 0 || gain > 1))
                    throw new Exceptions.SubsonicApiException("Gain value must be >= 0.0 and <= 1.0");

                parameters.Add(Constants.SetGain, gain.ToString());
            }

            return await HttpResponse.GetResponseAsync(Methods.jukeboxControl, Versions.Version120, parameters);
        }

        /// <summary>
        /// Controls the jukebox, i.e., playback directly on the server's audio hardware. Note: The user must be authorized to control the jukebox (see Settings > Users > User is allowed to play files in jukebox mode).
        /// </summary>
        /// <param name="action">The operation to perform. Must be one of: start, stop, skip, add, clear, remove, shuffle, setGain</param>
        /// <param name="index">Used by skip and remove. Zero-based index of the song to skip to or remove.</param>
        /// <param name="gain">	Used by setGain to control the playback volume. A float value between 0.0 and 1.0.</param>
        /// <param name="id">Used by add. ID of song to add to the jukebox playlist. Use multiple id parameters to add many songs in the same request.</param>
        /// <returns>bool</returns>
        public bool JukeboxControl(JukeboxControlAction action, int? index = null, float? gain = null, IEnumerable<string> id = null)
        {
            var actionName = EnumHelper.GetXmlEnumAttribute(action);

            if (string.IsNullOrWhiteSpace(actionName))
                throw new Exceptions.SubsonicApiException("You must provide valid action");

            var parameters = SubsonicParameters.Create(SubsonicParameterTypes.List);

            parameters.Add(Constants.Action, actionName);

            if ((action == JukeboxControlAction.Skip || action == JukeboxControlAction.Remove) && index != null)
                parameters.Add(Constants.Index, index.ToString());

            if (action == JukeboxControlAction.Add)
            {
                if (id == null)
                    throw new Exceptions.SubsonicApiException("You must provide at least 1 ID.");

                parameters.Add(Constants.Id, id);
            }

            if (action == JukeboxControlAction.SetGain)
            {
                if (gain == null || (gain < 0 || gain > 1))
                    throw new Exceptions.SubsonicApiException("Gain value must be >= 0.0 and <= 1.0");

                parameters.Add(Constants.SetGain, gain.ToString());
            }

            return HttpResponse.GetResponse(Methods.jukeboxControl, Versions.Version120, parameters);
        }

        /// <summary>
        /// Returns all podcast channels the server subscribes to and their episodes.
        /// </summary>
        /// <returns>Podcasts</returns>
        public async Task<Podcasts> GetPodcastsAsync(CancellationToken? cancelToken = null)
        {
            return await HttpResponse.GetResponseAsync<Podcasts>(Methods.getPodcasts, Versions.Version160, null, cancelToken);
        }

        /// <summary>
        /// Returns all podcast channels the server subscribes to and their episodes.
        /// </summary>
        /// <returns>Podcasts</returns>
        public Podcasts GetPodcasts()
        {
            return HttpResponse.GetResponse<Podcasts>(Methods.getPodcasts, Versions.Version160);
        }

        /// <summary>
        /// Returns all genres.
        /// </summary>
        /// <returns>Genres</returns>
        public async Task<Genres> GetGenresAsync(CancellationToken? cancelToken = null)
        {
            return await HttpResponse.GetResponseAsync<Genres>(Methods.getGenres, Versions.Version190, null, cancelToken);
        }

        /// <summary>
        /// Returns all genres.
        /// </summary>
        /// <returns>Podcasts</returns>
        public Genres GetGenres()
        {
            return HttpResponse.GetResponse<Genres>(Methods.getGenres, Versions.Version190);
        }

        /// <summary>
        /// Returns songs in a given genre.
        /// </summary>
        /// <param name="genre">The genre, as returned by getGenres.</param>
        /// <param name="count">The maximum number of songs to return. Max 500.</param>
        /// <param name="offset">The offset. Useful if you want to page through the songs in a genre.</param>
        /// <param name="cancelToken"></param>
        /// <returns>Songs</returns>
        public async Task<Songs> GetSongsByGenreAsync(string genre, int? count = null, int? offset = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Genre, genre, true);
            parameters.Add(Constants.Count, count);
            parameters.Add(Constants.Offset, offset);

            return await HttpResponse.GetResponseAsync<Songs>(Methods.getSongsByGenre, Versions.Version190, parameters, cancelToken);
        }

        /// <summary>
        /// Returns songs in a given genre.
        /// </summary>
        /// <param name="genre">The genre, as returned by getGenres.</param>
        /// <param name="count">The maximum number of songs to return. Max 500.</param>
        /// <param name="offset">The offset. Useful if you want to page through the songs in a genre.</param>
        /// <returns>Songs</returns>
        public Songs GetSongsByGenre(string genre, int? count = null, int? offset = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(Constants.Genre, genre, true);
            parameters.Add(Constants.Count, count);
            parameters.Add(Constants.Offset, offset);

            return HttpResponse.GetResponse<Songs>(Methods.getSongsByGenre, Versions.Version190, parameters);
        }
    }
}
