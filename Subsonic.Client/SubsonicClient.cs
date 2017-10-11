using Subsonic.Client.Constants;
using Subsonic.Client.Enums;
using Subsonic.Client.Exceptions;
using Subsonic.Client.Extensions;
using Subsonic.Client.Interfaces;
using Subsonic.Common;
using Subsonic.Common.Classes;
using Subsonic.Common.Enums;
using Subsonic.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Subsonic.Client
{
    public class SubsonicClient<T> : ISubsonicClient<T> where T : class, IDisposable
    {
        protected ISubsonicResponse<T> SubsonicResponse { private get; set; }
        private ISubsonicServer SubsonicServer { get; }
        private bool EncodePasswords { get; }

        protected SubsonicClient(ISubsonicServer subsonicServer, bool encodePasswords = true)
        {
            SubsonicServer = subsonicServer;
            EncodePasswords = encodePasswords;
        }

        public virtual async Task<bool> PingAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync(Methods.Ping, SubsonicApiVersion.Version1_0_0, null, cancelToken);
        }

        public virtual async Task<License> GetLicenseAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<License>(Methods.GetLicense, SubsonicApiVersion.Version1_0_0, null, cancelToken);
        }

        public virtual async Task<MusicFolders> GetMusicFoldersAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<MusicFolders>(Methods.GetMusicFolders, SubsonicApiVersion.Version1_0_0, null, cancelToken);
        }

        public virtual async Task<NowPlaying> GetNowPlayingAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<NowPlaying>(Methods.GetNowPlaying, SubsonicApiVersion.Version1_0_0, null, cancelToken);
        }

        public virtual async Task<Starred> GetStarredAsync(string musicFolderId = null, CancellationToken? cancelToken = null)
        {
            var methodApiVersion = SubsonicApiVersion.Version1_8_0;

            var parameters = SubsonicParameters.Create();

            if (!string.IsNullOrWhiteSpace(musicFolderId))
            {
                parameters.Add(ParameterConstants.MusicFolderId, musicFolderId, true);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_12_0);
            }

            return await SubsonicResponse.GetResponseAsync<Starred>(Methods.GetStarred, methodApiVersion, parameters, cancelToken);
        }

        public virtual async Task<Starred2> GetStarred2Async(string musicFolderId = null, CancellationToken? cancelToken = null)
        {
            var methodApiVersion = SubsonicApiVersion.Version1_8_0;

            var parameters = SubsonicParameters.Create();

            if (!string.IsNullOrWhiteSpace(musicFolderId))
            {
                parameters.Add(ParameterConstants.MusicFolderId, musicFolderId, true);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_12_0);
            }

            return await SubsonicResponse.GetResponseAsync<Starred2>(Methods.GetStarred2, methodApiVersion, parameters, cancelToken);
        }

        public virtual async Task<Indexes> GetIndexesAsync(string musicFolderId = null, long? ifModifiedSince = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.MusicFolderId, musicFolderId);
            parameters.Add(ParameterConstants.IfModifiedSince, ifModifiedSince);

            return await SubsonicResponse.GetResponseAsync<Indexes>(Methods.GetIndexes, SubsonicApiVersion.Version1_0_0, parameters, cancelToken);
        }

        public virtual async Task<Directory> GetMusicDirectoryAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync<Directory>(Methods.GetMusicDirectory, SubsonicApiVersion.Version1_0_0, parameters, cancelToken);
        }

        public virtual async Task<ArtistWithAlbumsID3> GetArtistAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync<ArtistWithAlbumsID3>(Methods.GetArtist, SubsonicApiVersion.Version1_8_0, parameters, cancelToken);
        }

        public virtual async Task<ArtistsID3> GetArtistsAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<ArtistsID3>(Methods.GetArtists, SubsonicApiVersion.Version1_8_0, null, cancelToken);
        }

        public virtual async Task<AlbumID3> GetAlbumAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync<AlbumID3>(Methods.GetAlbum, SubsonicApiVersion.Version1_8_0, parameters, cancelToken);
        }

        public virtual async Task<Child> GetSongAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync<Child>(Methods.GetSong, SubsonicApiVersion.Version1_8_0, parameters, cancelToken);
        }

        public virtual async Task<Videos> GetVideosAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<Videos>(Methods.GetVideos, SubsonicApiVersion.Version1_8_0, null, cancelToken);
        }

        public virtual async Task<VideoInfo> GetVideoInfoAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync<VideoInfo>(Methods.GetVideoInfo, SubsonicApiVersion.Version1_14_0, parameters, cancelToken);
        }

        public virtual async Task<SearchResult> SearchAsync(string artist = null, string album = null, string title = null, string any = null, int? count = null, int? offset = null, long? newerThan = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Artist, artist);
            parameters.Add(ParameterConstants.Album, album);
            parameters.Add(ParameterConstants.Title, title);
            parameters.Add(ParameterConstants.Any, any);
            parameters.Add(ParameterConstants.Count, count);
            parameters.Add(ParameterConstants.Offset, offset);
            parameters.Add(ParameterConstants.NewerThan, newerThan);

            return await SubsonicResponse.GetResponseAsync<SearchResult>(Methods.Search, SubsonicApiVersion.Version1_0_0, parameters, cancelToken);
        }

        public virtual async Task<SearchResult2> Search2Async(string query, int? artistCount = null, int? artistOffset = null, int? albumCount = null, int? albumOffset = null, int? songCount = null, int? songOffset = null, string musicFolderId = null, CancellationToken? cancelToken = null)
        {
            var methodApiVersion = SubsonicApiVersion.Version1_4_0;

            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Query, query, true);
            parameters.Add(ParameterConstants.ArtistCount, artistCount);
            parameters.Add(ParameterConstants.ArtistOffset, artistOffset);
            parameters.Add(ParameterConstants.AlbumCount, albumCount);
            parameters.Add(ParameterConstants.AlbumOffset, albumOffset);
            parameters.Add(ParameterConstants.SongCount, songCount);
            parameters.Add(ParameterConstants.SongOffset, songOffset);

            if (!string.IsNullOrWhiteSpace(musicFolderId))
            {
                parameters.Add(ParameterConstants.MusicFolderId, musicFolderId, true);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_12_0);
            }

            return await SubsonicResponse.GetResponseAsync<SearchResult2>(Methods.Search2, methodApiVersion, parameters, cancelToken);
        }

        public virtual async Task<SearchResult3> Search3Async(string query, int? artistCount = null, int? artistOffset = null, int? albumCount = null, int? albumOffset = null, int? songCount = null, int? songOffset = null, string musicFolderId = null, CancellationToken? cancelToken = null)
        {
            var methodApiVersion = SubsonicApiVersion.Version1_8_0;

            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Query, query, true);
            parameters.Add(ParameterConstants.ArtistCount, artistCount);
            parameters.Add(ParameterConstants.ArtistOffset, artistOffset);
            parameters.Add(ParameterConstants.AlbumCount, albumCount);
            parameters.Add(ParameterConstants.AlbumOffset, albumOffset);
            parameters.Add(ParameterConstants.SongCount, songCount);
            parameters.Add(ParameterConstants.SongOffset, songOffset);

            if (!string.IsNullOrWhiteSpace(musicFolderId))
            {
                parameters.Add(ParameterConstants.MusicFolderId, musicFolderId, true);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_12_0);
            }

            return await SubsonicResponse.GetResponseAsync<SearchResult3>(Methods.Search3, methodApiVersion, parameters, cancelToken);
        }

        public virtual async Task<Playlists> GetPlaylistsAsync(string username = null, CancellationToken? cancelToken = null)
        {
            var methodApiVersion = SubsonicApiVersion.Version1_0_0;
            var parameters = SubsonicParameters.Create();

            if (!string.IsNullOrWhiteSpace(username))
            {
                parameters.Add(ParameterConstants.Username, username);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_8_0);
            }

            return await SubsonicResponse.GetResponseAsync<Playlists>(Methods.GetPlaylists, methodApiVersion, parameters, cancelToken);
        }

        public virtual async Task<PlaylistWithSongs> GetPlaylistAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync<PlaylistWithSongs>(Methods.GetPlaylist, SubsonicApiVersion.Version1_0_0, parameters, cancelToken);
        }

        public virtual async Task<bool> CreatePlaylistAsync(string playlistId = null, string name = null, IEnumerable<string> songId = null, CancellationToken? cancelToken = null)
        {
            if (!string.IsNullOrWhiteSpace(playlistId) && !string.IsNullOrWhiteSpace(name))
                throw new SubsonicApiException("Only one of playlist ID and name can be specified.");

            var parameters = SubsonicParameters.Create(SubsonicParameterType.List);

            if (!string.IsNullOrWhiteSpace(playlistId))
                parameters.Add(ParameterConstants.PlaylistId, playlistId);
            else if (!string.IsNullOrWhiteSpace(name))
                parameters.Add(ParameterConstants.Name, name);
            else
                throw new SubsonicApiException("One of playlist ID and name must be specified.");

            parameters.Add(ParameterConstants.SongId, songId);

            return await SubsonicResponse.GetResponseAsync(Methods.CreatePlaylist, SubsonicApiVersion.Version1_2_0, parameters, cancelToken);
        }

        public virtual async Task<bool> UpdatePlaylistAsync(string playlistId, string name = null, string comment = null, IEnumerable<string> songIdToAdd = null, IEnumerable<string> songIndexToRemove = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create(SubsonicParameterType.List);
            parameters.Add(ParameterConstants.PlaylistId, playlistId, true);
            parameters.Add(ParameterConstants.Name, name);
            parameters.Add(ParameterConstants.Comment, comment);
            parameters.Add(ParameterConstants.SongIdToAdd, songIdToAdd);
            parameters.Add(ParameterConstants.SongIndexToRemove, songIndexToRemove);

            return await SubsonicResponse.GetResponseAsync(Methods.UpdatePlaylist, SubsonicApiVersion.Version1_8_0, parameters);
        }

        public virtual async Task<bool> DeletePlaylistAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync(Methods.DeletePlaylist, SubsonicApiVersion.Version1_2_0, parameters, cancelToken);
        }

        public virtual async Task<long> GetCoverArtSizeAsync(string id, int? size = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);
            parameters.Add(ParameterConstants.Size, size);

            return await SubsonicResponse.GetContentLengthAsync(Methods.GetCoverArt, SubsonicApiVersion.Version1_0_0, parameters, cancelToken);
        }

        public virtual async Task<string> GetCaptionsAsync(string id, string format = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);
            parameters.Add(ParameterConstants.Format, format);

            return await SubsonicResponse.GetStringResponseAsync(Methods.GetCaptions, SubsonicApiVersion.Version1_14_0, parameters, cancelToken);
        }

        public virtual async Task<IImageFormat<T>> GetCoverArtAsync(string id, int? size = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);
            parameters.Add(ParameterConstants.Size, size);

            return await SubsonicResponse.GetImageResponseAsync(Methods.GetCoverArt, SubsonicApiVersion.Version1_0_0, parameters, cancelToken);
        }

        public virtual async Task<bool> ScrobbleAsync(string id, bool? submission = null, long? time = null, CancellationToken? cancelToken = null)
        {
            var methodApiVersion = SubsonicApiVersion.Version1_5_0;

            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);
            parameters.Add(ParameterConstants.Submission, submission);

            if (time != null)
            {
                parameters.Add(ParameterConstants.Time, time);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_8_0);
            }

            return await SubsonicResponse.GetResponseAsync(Methods.Scrobble, methodApiVersion, parameters, cancelToken);
        }

        public virtual async Task<Shares> GetSharesAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<Shares>(Methods.GetShares, SubsonicApiVersion.Version1_6_0, null, cancelToken);
        }

        public virtual async Task<bool> ChangePasswordAsync(string username, string password, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Username, username, true);

            if (EncodePasswords)
                password = string.Format(CultureInfo.InvariantCulture, "enc:{0}", password.ToHexString());

            parameters.Add("password", password, true);

            return await SubsonicResponse.GetResponseAsync(Methods.ChangePassword, SubsonicApiVersion.Version1_1_0, parameters, cancelToken);
        }

        public virtual async Task<User> GetUserAsync(string username, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Username, username, true);

            return await SubsonicResponse.GetResponseAsync<User>(Methods.GetUser, SubsonicApiVersion.Version1_3_0, parameters, cancelToken);
        }

        public virtual async Task<IImageFormat<T>> GetAvatarAsync(string username, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Username, username, true);

            return await SubsonicResponse.GetImageResponseAsync(Methods.GetAvatar, SubsonicApiVersion.Version1_8_0, parameters, cancelToken);
        }

        public virtual async Task<bool> StarAsync(IEnumerable<string> id = null, IEnumerable<string> albumId = null, IEnumerable<string> artistId = null, CancellationToken? cancelToken = null)
        {
            if (id == null && albumId == null && artistId == null)
                throw new SubsonicApiException("You must provide one of id, albumId or artistId");

            var parameters = SubsonicParameters.Create(SubsonicParameterType.List);
            parameters.Add(ParameterConstants.Id, id);
            parameters.Add(ParameterConstants.AlbumId, albumId);
            parameters.Add(ParameterConstants.ArtistId, artistId);

            return await SubsonicResponse.GetResponseAsync(Methods.Star, SubsonicApiVersion.Version1_8_0, parameters, cancelToken);
        }

        public virtual async Task<bool> UnStarAsync(IEnumerable<string> id = null, IEnumerable<string> albumId = null, IEnumerable<string> artistId = null, CancellationToken? cancelToken = null)
        {
            if (id == null && albumId == null && artistId == null)
                throw new SubsonicApiException("You must provide one of id, albumId or artistId");

            var parameters = SubsonicParameters.Create(SubsonicParameterType.List);
            parameters.Add(ParameterConstants.Id, id);
            parameters.Add(ParameterConstants.AlbumId, albumId);
            parameters.Add(ParameterConstants.ArtistId, artistId);

            return await SubsonicResponse.GetResponseAsync(Methods.Unstar, SubsonicApiVersion.Version1_8_0, parameters, cancelToken);
        }

        public virtual async Task<bool> SetRatingAsync(string id, int rating, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);
            parameters.Add(ParameterConstants.Rating, rating, true);

            return await SubsonicResponse.GetResponseAsync(Methods.SetRating, SubsonicApiVersion.Version1_6_0, parameters, cancelToken);
        }

        public virtual async Task<bool> CreateUserAsync(string username, string password, string email, bool? ldapAuthenticated = null, bool? adminRole = null, bool? settingsRole = null, bool? streamRole = null, bool? jukeboxRole = null, bool? downloadRole = null, bool? uploadRole = null, bool? playlistRole = null, bool? coverArtRole = null, bool? commentRole = null, bool? podcastRole = null, bool? shareRole = null, string musicFolderId = null, bool? videoConversionRole = null, CancellationToken? cancelToken = null)
        {
            var methodApiVersion = SubsonicApiVersion.Version1_3_0;

            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Username, username, true);

            if (EncodePasswords)
                password = string.Format(CultureInfo.InvariantCulture, "enc:{0}", password.ToHexString());

            parameters.Add(ParameterConstants.Password, password, true);
            parameters.Add(ParameterConstants.Email, email, true);
            parameters.Add(ParameterConstants.LdapAuthenticated, ldapAuthenticated);
            parameters.Add(ParameterConstants.AdminRole, adminRole);
            parameters.Add(ParameterConstants.SettingsRole, settingsRole);
            parameters.Add(ParameterConstants.StreamRole, streamRole);
            parameters.Add(ParameterConstants.JukeboxRole, jukeboxRole);
            parameters.Add(ParameterConstants.DownloadRole, downloadRole);
            parameters.Add(ParameterConstants.UploadRole, uploadRole);
            parameters.Add(ParameterConstants.PlaylistRole, playlistRole);
            parameters.Add(ParameterConstants.CoverArtRole, coverArtRole);
            parameters.Add(ParameterConstants.CommentRole, commentRole);
            parameters.Add(ParameterConstants.PodcastRole, podcastRole);

            if (shareRole.HasValue)
            {
                parameters.Add(ParameterConstants.ShareRole, shareRole);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_8_0);
            }

            if (!string.IsNullOrWhiteSpace(musicFolderId))
            {
                parameters.Add(ParameterConstants.MusicFolderId, musicFolderId, true);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_12_0);
            }

            if (videoConversionRole.HasValue)
            {
                parameters.Add(ParameterConstants.VideoConversionRole, videoConversionRole);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_14_0);
            }

            return await SubsonicResponse.GetResponseAsync(Methods.CreateUser, methodApiVersion, parameters, cancelToken);
        }

        public virtual async Task<bool> DeleteUserAsync(string username, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Username, username, true);

            return await SubsonicResponse.GetResponseAsync(Methods.DeleteUser, SubsonicApiVersion.Version1_3_0, parameters, cancelToken);
        }

        public virtual async Task<ChatMessages> GetChatMessagesAsync(double? since = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Since, since);

            return await SubsonicResponse.GetResponseAsync<ChatMessages>(Methods.GetChatMessages, SubsonicApiVersion.Version1_2_0, parameters, cancelToken);
        }

        public virtual async Task<bool> AddChatMessageAsync(string message, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Message, message, true);

            return await SubsonicResponse.GetResponseAsync(Methods.AddChatMessage, SubsonicApiVersion.Version1_2_0, parameters, cancelToken);
        }

        public virtual async Task<AlbumList> GetAlbumListAsync(AlbumListType type, int? size = null, int? offset = null, int? fromYear = null, int? toYear = null, string genre = null, string musicFolderId = null, CancellationToken? cancelToken = null)
        {
            var methodApiVersion = SubsonicApiVersion.Version1_2_0;

            if (type == AlbumListType.AlphabeticalByArtist || type == AlbumListType.AlphabeticalByName || type == AlbumListType.Starred)
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_8_0);

            var albumListTypeName = type.GetXmlEnumAttribute();

            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Type, albumListTypeName, true);
            parameters.Add(ParameterConstants.Size, size);
            parameters.Add(ParameterConstants.Offset, offset);

            if (type == AlbumListType.ByYear)
            {
                parameters.Add(ParameterConstants.FromYear, fromYear, true);
                parameters.Add(ParameterConstants.ToYear, toYear, true);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_10_1);
            }

            if (type == AlbumListType.ByGenre)
            {
                parameters.Add(ParameterConstants.Genre, genre, true);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_10_1);
            }

            if (!string.IsNullOrWhiteSpace(musicFolderId))
            {
                parameters.Add(ParameterConstants.MusicFolderId, musicFolderId);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_11_0);
            }

            return await SubsonicResponse.GetResponseAsync<AlbumList>(Methods.GetAlbumList, methodApiVersion, parameters, cancelToken);
        }

        public virtual async Task<AlbumList2> GetAlbumList2Async(AlbumListType type, int? size = null, int? offset = null, int? fromYear = null, int? toYear = null, string genre = null, string musicFolderId = null, CancellationToken? cancelToken = null)
        {
            var methodApiVersion = SubsonicApiVersion.Version1_8_0;

            var albumListTypeName = type.GetXmlEnumAttribute();

            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Type, albumListTypeName, true);
            parameters.Add(ParameterConstants.Size, size);
            parameters.Add(ParameterConstants.Offset, offset);

            if (type == AlbumListType.ByYear)
            {
                parameters.Add(ParameterConstants.FromYear, fromYear, true);
                parameters.Add(ParameterConstants.ToYear, toYear, true);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_10_1);
            }

            if (type == AlbumListType.ByGenre)
            {
                parameters.Add(ParameterConstants.Genre, genre, true);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_10_1);
            }

            if (!string.IsNullOrWhiteSpace(musicFolderId))
            {
                parameters.Add(ParameterConstants.MusicFolderId, musicFolderId, true);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_12_0);
            }

            return await SubsonicResponse.GetResponseAsync<AlbumList2>(Methods.GetAlbumList2, methodApiVersion, parameters, cancelToken);
        }

        public virtual async Task<RandomSongs> GetRandomSongsAsync(int? size = null, string genre = null, int? fromYear = null, int? toYear = null, string musicFolderId = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Size, size);
            parameters.Add(ParameterConstants.Genre, genre);
            parameters.Add(ParameterConstants.FromYear, fromYear);
            parameters.Add(ParameterConstants.ToYear, toYear);
            parameters.Add(ParameterConstants.MusicFolderId, musicFolderId);

            return await SubsonicResponse.GetResponseAsync<RandomSongs>(Methods.GetRandomSongs, SubsonicApiVersion.Version1_2_0, parameters, cancelToken);
        }

        public virtual async Task<Lyrics> GetLyricsAsync(string artist = null, string title = null, CancellationToken? cancelToken = null)
        {
            if (string.IsNullOrWhiteSpace(artist) && string.IsNullOrWhiteSpace(title))
                throw new SubsonicApiException("You must specify an artist and/or a title");

            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Artist, artist);
            parameters.Add(ParameterConstants.Title, title);

            return await SubsonicResponse.GetResponseAsync<Lyrics>(Methods.GetLyrics, SubsonicApiVersion.Version1_2_0, parameters, cancelToken);
        }

        public virtual async Task<JukeboxPlaylist> JukeboxControlAsync(CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Action, ParameterConstants.Get);

            return await SubsonicResponse.GetResponseAsync<JukeboxPlaylist>(Methods.JukeboxControl, SubsonicApiVersion.Version1_2_0, parameters, cancelToken);
        }

        public virtual async Task<bool> JukeboxControlAsync(JukeboxControlAction action, int? index = null, float? gain = null, IEnumerable<string> id = null, CancellationToken? cancelToken = null)
        {
            var actionName = action.GetXmlEnumAttribute();

            if (string.IsNullOrWhiteSpace(actionName))
                throw new SubsonicApiException("You must provide valid action");

            var parameters = SubsonicParameters.Create(SubsonicParameterType.List);

            parameters.Add(ParameterConstants.Action, actionName);

            if ((action == JukeboxControlAction.Skip || action == JukeboxControlAction.Remove) && index != null)
                parameters.Add(ParameterConstants.Index, index.ToString());

            if (action == JukeboxControlAction.Add)
            {
                if (id == null)
                    throw new SubsonicApiException("You must provide at least 1 ID.");

                parameters.Add(ParameterConstants.Id, id);
            }

            if (action == JukeboxControlAction.SetGain)
            {
                if (gain == null || (gain < 0 || gain > 1))
                    throw new SubsonicApiException("Gain value must be >= 0.0 and <= 1.0");

                parameters.Add(ParameterConstants.SetGain, gain.ToString());
            }

            return await SubsonicResponse.GetResponseAsync(Methods.JukeboxControl, SubsonicApiVersion.Version1_2_0, parameters, cancelToken);
        }

        public virtual async Task<Podcasts> GetPodcastsAsync(string id = null, bool? includeEpisodes = null, CancellationToken? cancelToken = null)
        {
            var methodApiVersion = SubsonicApiVersion.Version1_6_0;

            var parameters = SubsonicParameters.Create();

            if (!string.IsNullOrWhiteSpace(id))
            {
                parameters.Add(ParameterConstants.Id, id);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_9_0);
            }

            if (includeEpisodes != null)
            {
                parameters.Add(ParameterConstants.IncludeEpisodes, includeEpisodes);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_9_0);
            }

            return await SubsonicResponse.GetResponseAsync<Podcasts>(Methods.GetPodcasts, methodApiVersion, parameters.Parameters.Count > 0 ? parameters : null, cancelToken);
        }

        public virtual async Task<Genres> GetGenresAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<Genres>(Methods.GetGenres, SubsonicApiVersion.Version1_9_0, null, cancelToken);
        }

        public virtual async Task<Songs> GetSongsByGenreAsync(string genre, int? count = null, int? offset = null, string musicFolderId = null, CancellationToken? cancelToken = null)
        {
            var methodApiVersion = SubsonicApiVersion.Version1_9_0;

            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Genre, genre, true);
            parameters.Add(ParameterConstants.Count, count);
            parameters.Add(ParameterConstants.Offset, offset);

            if (!string.IsNullOrWhiteSpace(musicFolderId))
            {
                parameters.Add(ParameterConstants.MusicFolderId, musicFolderId, true);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_12_0);
            }

            return await SubsonicResponse.GetResponseAsync<Songs>(Methods.GetSongsByGenre, methodApiVersion, parameters, cancelToken);
        }

        public virtual async Task<long> StreamAsync(string id, string path, StreamParameters? streamParameters = null, string format = null, int? timeOffset = null, bool? estimateContentLength = null, bool? converted = null, CancellationToken? cancelToken = null, bool noResponse = false)
        {
            var methodApiVersion = SubsonicApiVersion.Version1_2_0;

            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);

            if (streamParameters.HasValue)
            {
                if (streamParameters.Value.BitRate > 0)
                    parameters.Add(ParameterConstants.MaxBitRate, streamParameters.Value.BitRate);

                if (streamParameters.Value.Width > 0 && streamParameters.Value.Height > 0)
                {
                    parameters.Add(ParameterConstants.Size, streamParameters);
                    methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_6_0);
                }
            }

            if (timeOffset != null)
            {
                parameters.Add(ParameterConstants.TimeOffset, timeOffset);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_6_0);
            }

            if (estimateContentLength != null)
            {
                parameters.Add(ParameterConstants.EstimateContentLength, estimateContentLength);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_8_0);
            }

            if (converted != null)
            {
                parameters.Add(ParameterConstants.Converted, converted);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_14_0);
            }

            if (format != null)
            {
                parameters.Add(ParameterConstants.StreamFormat, format);
                methodApiVersion = format == "raw" ? methodApiVersion.Max(SubsonicApiVersion.Version1_9_0) : methodApiVersion.Max(SubsonicApiVersion.Version1_6_0);
            }

            if (noResponse)
            {
                await SubsonicResponse.GetNoResponseAsync(Methods.Stream, methodApiVersion, parameters, cancelToken);
                return 0;
            }

            return await SubsonicResponse.GetResponseAsync(path, true, Methods.Stream, methodApiVersion, parameters, cancelToken);
        }

        public virtual async Task<long> DownloadAsync(string id, string path, bool pathOverride = false, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync(path, pathOverride, Methods.Download, SubsonicApiVersion.Version1_0_0, parameters, cancelToken);
        }

        public virtual async Task<string> HlsAsync(string id, IList<StreamParameters> streamParameters = null, CancellationToken? cancelToken = null)
        {
            var methodApiVersion = SubsonicApiVersion.Version1_8_0;

            var parameters = SubsonicParameters.Create(SubsonicParameterType.List);
            parameters.Add(ParameterConstants.Id, id, true);

            if (streamParameters != null)
            {
                if (streamParameters.Count == 1)
                {
                    var streamParameter = streamParameters.First();

                    if (streamParameter.Width > 0 && streamParameter.Height > 0)
                    {
                        methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_9_0);
                        parameters.Add(ParameterConstants.BitRate,
                            $"{streamParameter.BitRate}@{streamParameter.Width}x{streamParameter.Height}");
                    }
                    else
                    {
                        parameters.Add(ParameterConstants.BitRate, streamParameter.BitRate.ToString());
                    }
                }
                else if (streamParameters.Count > 1)
                {
                    // If mulitple streamParameters are provided, use the aggregate of all of the distinct bitrates, this should return a playlist which then lists a separate playlist
                    //  for each bitrate/player combination
                    parameters.Add(ParameterConstants.BitRate, streamParameters.Where(sp => sp.BitRate > 0).Select(sp => sp.BitRate.ToString()).Distinct());
                }
            }

            return await SubsonicResponse.GetStringResponseAsync(Methods.Hls, methodApiVersion, parameters, cancelToken);
        }

        public virtual async Task<bool> RefreshPodcastsAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync(Methods.RefreshPodcasts, SubsonicApiVersion.Version1_9_0, null, cancelToken);
        }

        public virtual async Task<bool> CreatePodcastChannelAsync(string url, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Url, url, true);

            return await SubsonicResponse.GetResponseAsync(Methods.CreatePodcastChannel, SubsonicApiVersion.Version1_9_0, parameters, cancelToken);
        }

        public virtual async Task<bool> DeletePodcastChannelAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync(Methods.DeletePodcastChannel, SubsonicApiVersion.Version1_9_0, parameters, cancelToken);
        }

        public virtual async Task<bool> DeletePodcastEpisodeAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync(Methods.DeletePodcastEpisode, SubsonicApiVersion.Version1_9_0, parameters, cancelToken);
        }

        public virtual async Task<long> DownloadPodcastEpisodeAsync(string id, string path, bool pathOverride = false, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync(path, pathOverride, Methods.DownloadPodcastEpisode, SubsonicApiVersion.Version1_9_0, parameters, cancelToken);
        }

        public virtual async Task<InternetRadioStations> GetInternetRadioStationsAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<InternetRadioStations>(Methods.GetInternetRadioStations, SubsonicApiVersion.Version1_9_0, null, cancelToken);
        }

        public virtual async Task<Bookmarks> GetBookmarksAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<Bookmarks>(Methods.GetBookmarks, SubsonicApiVersion.Version1_9_0, null, cancelToken);
        }

        public virtual async Task<bool> CreateBookmarkAsync(string id, long position, string comment = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);
            parameters.Add(ParameterConstants.Position, position, true);
            parameters.Add(ParameterConstants.Comment, comment);

            return await SubsonicResponse.GetResponseAsync(Methods.CreateBookmark, SubsonicApiVersion.Version1_9_0, parameters, cancelToken);
        }

        public virtual async Task<bool> DeleteBookmarkAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync(Methods.DeleteBookmark, SubsonicApiVersion.Version1_9_0, parameters, cancelToken);
        }

        public virtual async Task<bool> UpdateUserAsync(string username, string password = null, string email = null, bool? ldapAuthenticated = null, bool? adminRole = null, bool? settingsRole = null, bool? streamRole = null, bool? jukeboxRole = null, bool? downloadRole = null, bool? uploadRole = null, bool? coverArtRole = null, bool? commentRole = null, bool? podcastRole = null, bool? shareRole = null, string musicFolderId = null, AudioBitrate? maxBitRate = null, bool? videoConversionRole = null, CancellationToken? cancelToken = null)
        {
            var methodApiVersion = SubsonicApiVersion.Version1_10_1;

            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Username, username, true);
            parameters.Add(ParameterConstants.Password, password);
            parameters.Add(ParameterConstants.Email, email);
            parameters.Add(ParameterConstants.LdapAuthenticated, ldapAuthenticated);
            parameters.Add(ParameterConstants.AdminRole, adminRole);
            parameters.Add(ParameterConstants.SettingsRole, settingsRole);
            parameters.Add(ParameterConstants.StreamRole, streamRole);
            parameters.Add(ParameterConstants.JukeboxRole, jukeboxRole);
            parameters.Add(ParameterConstants.DownloadRole, downloadRole);
            parameters.Add(ParameterConstants.UploadRole, uploadRole);
            parameters.Add(ParameterConstants.CoverArtRole, coverArtRole);
            parameters.Add(ParameterConstants.CommentRole, commentRole);
            parameters.Add(ParameterConstants.PodcastRole, podcastRole);
            parameters.Add(ParameterConstants.ShareRole, shareRole);

            if (!string.IsNullOrWhiteSpace(musicFolderId))
            {
                parameters.Add(ParameterConstants.MusicFolderId, musicFolderId, true);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_12_0);
            }

            if (maxBitRate.HasValue)
            {
                parameters.Add(ParameterConstants.MaxBitRate, maxBitRate.Value, true);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_13_0);
            }

            if (videoConversionRole.HasValue)
            {
                parameters.Add(ParameterConstants.VideoConversionRole, videoConversionRole);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_14_0);
            }

            return await SubsonicResponse.GetResponseAsync(Methods.UpdateUser, methodApiVersion, parameters, cancelToken);
        }

        public virtual async Task<Shares> CreateShareAsync(IEnumerable<string> id, string description = null, long? expires = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create(SubsonicParameterType.List);
            parameters.Add(ParameterConstants.Id, id, true);
            parameters.Add(ParameterConstants.Description, description);
            parameters.Add(ParameterConstants.Expires, expires);

            return await SubsonicResponse.GetResponseAsync<Shares>(Methods.CreateShare, SubsonicApiVersion.Version1_6_0, parameters, cancelToken);
        }

        public virtual async Task<bool> UpdateShareAsync(string id, string description = null, long? expires = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);
            parameters.Add(ParameterConstants.Description, description);
            parameters.Add(ParameterConstants.Expires, expires);

            return await SubsonicResponse.GetResponseAsync(Methods.UpdateShare, SubsonicApiVersion.Version1_6_0, parameters, cancelToken);
        }

        public virtual async Task<bool> DeleteShareAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync(Methods.DeleteShare, SubsonicApiVersion.Version1_6_0, parameters, cancelToken);
        }

        public virtual async Task<Users> GetUsersAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<Users>(Methods.GetUsers, SubsonicApiVersion.Version1_8_0, null, cancelToken);
        }

        public virtual async Task<ArtistInfo> GetArtistInfoAsync(string id, int? count = null, bool? includeNotPresent = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);
            parameters.Add(ParameterConstants.Count, count);
            parameters.Add(ParameterConstants.IncludeNotPresent, includeNotPresent);

            return await SubsonicResponse.GetResponseAsync<ArtistInfo>(Methods.GetArtistInfo, SubsonicApiVersion.Version1_11_0, parameters, cancelToken);
        }

        public virtual async Task<ArtistInfo2> GetArtistInfo2Async(string id, int? count = null, bool? includeNotPresent = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);
            parameters.Add(ParameterConstants.Count, count);
            parameters.Add(ParameterConstants.IncludeNotPresent, includeNotPresent);

            return await SubsonicResponse.GetResponseAsync<ArtistInfo2>(Methods.GetArtistInfo2, SubsonicApiVersion.Version1_11_0, parameters, cancelToken);
        }

        public virtual async Task<AlbumInfo> GetAlbumInfoAsync(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync<AlbumInfo>(Methods.GetAlbumInfo, SubsonicApiVersion.Version1_14_0, parameters, cancelToken);
        }

        public virtual async Task<AlbumInfo> GetAlbumInfo2Async(string id, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);

            return await SubsonicResponse.GetResponseAsync<AlbumInfo>(Methods.GetAlbumInfo2, SubsonicApiVersion.Version1_14_0, parameters, cancelToken);
        }

        public virtual async Task<SimilarSongs> GetSimilarSongsAsync(string id, int? count = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);
            parameters.Add(ParameterConstants.Count, count);

            return await SubsonicResponse.GetResponseAsync<SimilarSongs>(Methods.GetSimilarSongs, SubsonicApiVersion.Version1_11_0, parameters, cancelToken);
        }

        public virtual async Task<TopSongs> GetTopSongsAsync(string artist, int? count = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Artist, artist, true);
            parameters.Add(ParameterConstants.Count, count);

            return await SubsonicResponse.GetResponseAsync<TopSongs>(Methods.GetTopSongs, SubsonicApiVersion.Version1_13_0, parameters, cancelToken);
        }

        public virtual async Task<SimilarSongs2> GetSimilarSongs2Async(string id, int? count = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);
            parameters.Add(ParameterConstants.Count, count);

            return await SubsonicResponse.GetResponseAsync<SimilarSongs2>(Methods.GetSimilarSongs2, SubsonicApiVersion.Version1_11_0, parameters, cancelToken);
        }

        public virtual async Task<bool> ScanMediaFoldersAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetSettingChangeResponseAsync(SettingMethods.ScanNow, cancelToken);
        }

        public virtual async Task<bool> CleanupMediaFoldersAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetSettingChangeResponseAsync(SettingMethods.Expunge, cancelToken);
        }

        public virtual async Task<PlayQueue> GetPlayQueueAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<PlayQueue>(Methods.GetPlayQueue, SubsonicApiVersion.Version1_12_0, null, cancelToken);
        }

        public virtual async Task<bool> SavePlayQueueAsync(string id, string current = null, long? position = null, CancellationToken? cancelToken = null)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);
            parameters.Add(ParameterConstants.Current, current);
            parameters.Add(ParameterConstants.Position, position);

            return await SubsonicResponse.GetResponseAsync(Methods.SavePlayQueue, SubsonicApiVersion.Version1_12_0, parameters, cancelToken);
        }

        public virtual Uri BuildDownloadUrl(string id)
        {
            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);

            return SubsonicServer.BuildRequestUriUser(Methods.Download, SubsonicApiVersion.Version1_0_0, parameters);
        }

        public virtual Uri BuildStreamUrl(string id, StreamParameters? streamParameters = null, string format = null, int? timeOffset = null, bool? estimateContentLength = null)
        {
            var methodApiVersion = SubsonicApiVersion.Version1_2_0;

            var parameters = SubsonicParameters.Create();
            parameters.Add(ParameterConstants.Id, id, true);

            if (streamParameters.HasValue)
            {
                if (streamParameters.Value.BitRate > 0)
                    parameters.Add(ParameterConstants.MaxBitRate, streamParameters.Value.BitRate);

                if (streamParameters.Value.Width > 0 && streamParameters.Value.Height > 0)
                {
                    parameters.Add(ParameterConstants.Size, streamParameters);
                    methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_6_0);
                }
            }

            if (timeOffset != null)
            {
                parameters.Add(ParameterConstants.TimeOffset, timeOffset);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_6_0);
            }

            if (estimateContentLength != null)
            {
                parameters.Add(ParameterConstants.EstimateContentLength, estimateContentLength);
                methodApiVersion = methodApiVersion.Max(SubsonicApiVersion.Version1_8_0);
            }

            if (format != null)
            {
                parameters.Add(ParameterConstants.StreamFormat, format);
                methodApiVersion = format == "raw" ? methodApiVersion.Max(SubsonicApiVersion.Version1_9_0) : methodApiVersion.Max(SubsonicApiVersion.Version1_6_0);
            }

            return SubsonicServer.BuildRequestUriUser(Methods.Stream, methodApiVersion, parameters);
        }

        public async Task<ScanStatus> GetScanStatusAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<ScanStatus>(Methods.GetScanStatus, SubsonicApiVersion.Version1_15_0, null, cancelToken);
        }

        public async Task<ScanStatus> StartScanAsync(CancellationToken? cancelToken = null)
        {
            return await SubsonicResponse.GetResponseAsync<ScanStatus>(Methods.StartScan, SubsonicApiVersion.Version1_15_0, null, cancelToken);
        }
    }
}