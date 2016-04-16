using Subsonic.Client.Activities;
using Subsonic.Client.Exceptions;
using Subsonic.Client.Extensions;
using Subsonic.Client.Interfaces;
using Subsonic.Common;
using Subsonic.Common.Classes;
using Subsonic.Common.Enums;
using Subsonic.Common.Interfaces;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using Xunit;

namespace Subsonic.Client.Windows.Tests
{
    public class Tests : IDisposable
    {
        private static readonly Uri SubsonicServer = new Uri("http://localhost/subsonic/");
        private static readonly Uri NonexistentServer = new Uri("http://localhost/ultrasonic/");
        private const string ProxyServer = "localhost";
        private const int ProxyPort = 8888;
        private const string AdminUser = "admin";
        private const string DownloadUser = "test_download";
        private const string NoPlayUser = "test_noplay";
        private const string PlayUser = "test_play";
        private const string Password = "Subsonic!";
        private const string UserToCreate = "test_createduser";
        private const string UserToCreateEmail = "test_createduser@localhost";
        private const string ClientName = "Subsonic.Client.Windows.Tests";
        private const string SearchQuery = "Smashing Pumpkins";

        private const int MaxRandomSongCount = 500;
        private const int MinRandomSongCount = 1;
        private const int InvalidRandomSongCount = 501;

        private ISubsonicClient<Image> _adminSubsonicClient;
        private ISubsonicClient<Image> _downloadSubsonicClient;
        private ISubsonicClient<Image> _noPlaySubsonicClient;
        private ISubsonicClient<Image> _playSubsonicClient;
        private ISubsonicClient<Image> _nonexistentSubsonicClient;
        private readonly ISubsonicServer _adminSubsonicServer;
        private readonly ISubsonicServer _downloadSubsonicServer;
        private readonly ISubsonicServer _noPlaySubsonicServer;
        private readonly ISubsonicServer _playSubsonicServer;
        private readonly ISubsonicServer _nonexistentSubsonicServer;
        private Random _random;

        public Tests()
        {
            _adminSubsonicServer = new SubsonicServer(SubsonicServer, AdminUser, Password, ClientName);
            _downloadSubsonicServer = new SubsonicServer(SubsonicServer, DownloadUser, Password, ClientName);
            _noPlaySubsonicServer = new SubsonicServer(SubsonicServer, NoPlayUser, Password, ClientName);
            _playSubsonicServer = new SubsonicServer(SubsonicServer, PlayUser, Password, ClientName);
            _nonexistentSubsonicServer = new SubsonicServer(NonexistentServer, AdminUser, Password, ClientName);

            var imageFormatFactory = new ImageFormatFactory();

            _adminSubsonicClient = new SubsonicClient(_adminSubsonicServer, imageFormatFactory);
            _downloadSubsonicClient = new SubsonicClient(_downloadSubsonicServer, imageFormatFactory);
            _noPlaySubsonicClient = new SubsonicClient(_noPlaySubsonicServer, imageFormatFactory);
            _playSubsonicClient = new SubsonicClient(_playSubsonicServer, imageFormatFactory);
            _nonexistentSubsonicClient = new SubsonicClient(_nonexistentSubsonicServer, imageFormatFactory);
            _random = new Random(DateTime.UtcNow.Millisecond * DateTime.UtcNow.Second * DateTime.UtcNow.Minute);
        }

        public void Dispose()
        {
            _adminSubsonicClient = null;
            _downloadSubsonicClient = null;
            _noPlaySubsonicClient = null;
            _playSubsonicClient = null;
            _nonexistentSubsonicClient = null;
            _random = null;
        }

        [Fact]
        public async void PingTestAdminOnSubsonic()
        {
            var result = await _adminSubsonicClient.PingAsync();

            Assert.True(result);
            Assert.NotNull(_adminSubsonicServer.ApiVersion);
            Assert.True(_adminSubsonicServer.ApiVersion >= SubsonicApiVersion.Version1_0_0);
        }

        [Fact]
        public async void PingTestDownloadOnSubsonic()
        {
            var result = await _downloadSubsonicClient.PingAsync();

            Assert.True(result);
            Assert.NotNull(_downloadSubsonicServer.ApiVersion);
            Assert.True(_downloadSubsonicServer.ApiVersion >= SubsonicApiVersion.Version1_0_0);
        }

        [Fact]
        public async void PingTestNoPlayOnSubsonic()
        {
            var result = await _noPlaySubsonicClient.PingAsync();

            Assert.True(result);
            Assert.NotNull(_noPlaySubsonicServer.ApiVersion);
            Assert.True(_noPlaySubsonicServer.ApiVersion >= SubsonicApiVersion.Version1_0_0);
        }

        [Fact]
        public async void PingTestPlayOnSubsonic()
        {
            var result = await _playSubsonicClient.PingAsync();

            Assert.True(result);
            Assert.NotNull(_playSubsonicServer.ApiVersion);
            Assert.True(_playSubsonicServer.ApiVersion >= SubsonicApiVersion.Version1_0_0);
        }

        [Fact]
        public async void PingTestOnNonexistentServer()
        {
            Assert.Null(_nonexistentSubsonicServer.ApiVersion);
            await Assert.ThrowsAsync<SubsonicApiException>(async () => await _nonexistentSubsonicClient.PingAsync());
            Assert.Null(_nonexistentSubsonicServer.ApiVersion);
        }

        [Fact]
        public async void LicenseTestAdminOnSubsonic()
        {
            var license = await _adminSubsonicClient.GetLicenseAsync();

            Assert.True(license.Valid);
        }

        [Fact]
        public async void LicenseTestDownloadOnSubsonic()
        {
            var license = await _downloadSubsonicClient.GetLicenseAsync();

            Assert.True(license.Valid);
        }

        [Fact]
        public async void LicenseTestNoPlayOnSubsonic()
        {
            var license = await _noPlaySubsonicClient.GetLicenseAsync();

            Assert.True(license.Valid);
        }

        [Fact]
        public async void LicenseTestPlayOnSubsonic()
        {
            var license = await _playSubsonicClient.GetLicenseAsync();

            Assert.True(license.Valid);
        }

        [Fact]
        public async void CreateAdminUserOnSubsonic()
        {
            var result = await _adminSubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail, false, true);

            Assert.True(result);

            var user = await _adminSubsonicClient.GetUserAsync(UserToCreate);

            Assert.True(user.Username.Equals(UserToCreate));
            Assert.True(user.Email.Equals(UserToCreateEmail));
            Assert.True(user.AdminRole);
            Assert.False(user.CommentRole);
            Assert.False(user.CoverArtRole);
            Assert.False(user.DownloadRole);
            Assert.False(user.JukeboxRole);
            Assert.True(user.PlaylistRole);
            Assert.False(user.PodcastRole);
            Assert.False(user.ScrobblingEnabled);
            Assert.True(user.SettingsRole);
            Assert.False(user.ShareRole);
            Assert.True(user.StreamRole);
            Assert.False(user.UploadRole);

            result = await _adminSubsonicClient.DeleteUserAsync(UserToCreate);

            Assert.True(result);
        }

        [Fact]
        public async void CreateAdminUserAsPlayUserOnSubsonicThrowsUserNotAuthorized()
        {
            SubsonicErrorException ex = await Assert.ThrowsAsync<SubsonicErrorException>(async () => await _playSubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail, false, true));
            Assert.True(ex.Error.Code.Equals(ErrorCode.UserNotAuthorized));
        }

        [Fact]
        public async void CreateAdminUserAsNoPlayUserOnSubsonicThrowsUserNotAuthorized()
        {
            SubsonicErrorException ex = await Assert.ThrowsAsync<SubsonicErrorException>(async () => await _noPlaySubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail, false, true));
            Assert.True(ex.Error.Code.Equals(ErrorCode.UserNotAuthorized));
        }

        [Fact]
        public async void CreateAdminUserAsDownloadUserOnSubsonicThrowsUserNotAuthorized()
        {
            SubsonicErrorException ex = await Assert.ThrowsAsync<SubsonicErrorException>(async () => await _downloadSubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail, false, true));
            Assert.True(ex.Error.Code.Equals(ErrorCode.UserNotAuthorized));
        }

        [Fact]
        public async void CreateUserAsPlayUserOnSubsonicThrowsUserNotAuthorized()
        {
            SubsonicErrorException ex = await Assert.ThrowsAsync<SubsonicErrorException>(async () => await _playSubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail));
            Assert.True(ex.Error.Code.Equals(ErrorCode.UserNotAuthorized));
        }

        [Fact]
        public async void CreateUserAsNoPlayUserOnSubsonicThrowsUserNotAuthorized()
        {
            SubsonicErrorException ex = await Assert.ThrowsAsync<SubsonicErrorException>(async () => await _noPlaySubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail));
            Assert.True(ex.Error.Code.Equals(ErrorCode.UserNotAuthorized));
        }

        [Fact]
        public async void CreateUserAsDownloadUserOnSubsonicThrowsUserNotAuthorized()
        {
            SubsonicErrorException ex = await Assert.ThrowsAsync<SubsonicErrorException>(async () => await _downloadSubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail));
            Assert.True(ex.Error.Code.Equals(ErrorCode.UserNotAuthorized));
        }

        [Fact]
        public async void AddChatMessageAsPlayUserOnSubsonic()
        {
            var chatMessage = DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture);
            var result = await _playSubsonicClient.AddChatMessageAsync(chatMessage);

            Assert.True(result);

            var chatMessages = await _noPlaySubsonicClient.GetChatMessagesAsync();

            Assert.True(chatMessages.Items.Any(c => c.Message == chatMessage && c.Username == PlayUser));
        }

        [Fact]
        public async void AddChatMessageAsNoPlayUserOnSubsonic()
        {
            var chatMessage = DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture);
            var result = await _noPlaySubsonicClient.AddChatMessageAsync(chatMessage);

            Assert.True(result);

            var chatMessages = await _playSubsonicClient.GetChatMessagesAsync();

            Assert.True(chatMessages.Items.Any(c => c.Message == chatMessage && c.Username == NoPlayUser));
        }

        [Fact]
        public async void AddChatMessageAsAdminUserOnSubsonic()
        {
            var chatMessage = DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture);
            var result = await _adminSubsonicClient.AddChatMessageAsync(chatMessage);

            Assert.True(result);

            var chatMessages = await _noPlaySubsonicClient.GetChatMessagesAsync();

            Assert.True(chatMessages.Items.Any(c => c.Message == chatMessage && c.Username == AdminUser));
        }

        [Fact]
        public async void GetRandomSongsAsAdminUserOnSubsonic()
        {
            var randomNumber = _random.Next(MinRandomSongCount, MaxRandomSongCount);
            RandomSongs randomSongs = await _adminSubsonicClient.GetRandomSongsAsync(randomNumber);

            Assert.Equal(randomSongs.Songs.Count, randomNumber);
        }

        [Fact]
        public async void GetInvalidNumberOfRandomSongsAsAdminUserOnSubsonic()
        {
            RandomSongs randomSongs = await _adminSubsonicClient.GetRandomSongsAsync(InvalidRandomSongCount);

            Assert.True(randomSongs.Songs.Count < InvalidRandomSongCount);
        }

        [Fact]
        public async void GetRandomSongsForGenreAsAdminUserOnSubsonic()
        {
            var randomNumber = _random.Next(MinRandomSongCount, MaxRandomSongCount);
            var genres = await _adminSubsonicClient.GetGenresAsync();
            var randomNumberForGenre = _random.Next(0, genres.Items.Count - 1);
            var randomGenre = genres.Items.ElementAt(randomNumberForGenre);
            var randomSongs = await _adminSubsonicClient.GetRandomSongsAsync(randomNumber, randomGenre.Name);

            Assert.True(randomSongs.Songs.All(s => string.Compare(s.Genre, randomGenre.Name, StringComparison.OrdinalIgnoreCase) == 0));
        }

        [Fact]
        public async void GetMusicFoldersAsAdminUserOnSubsonic()
        {
            var musicFolders = await _adminSubsonicClient.GetMusicFoldersAsync();

            Assert.True(musicFolders.Items.Any());
        }

        [Fact]
        public async void GetIndexesForAllMusicFoldersAsAdminUserOnSubsonic()
        {
            var indexes = await _adminSubsonicClient.GetIndexesAsync();

            Assert.NotNull(indexes);
            Assert.True(indexes.Items.Any());
        }

        [Fact]
        public async void GetIndexesForRandomMusicFolderAsAdminUserOnSubsonic()
        {
            var musicFolders = await _adminSubsonicClient.GetMusicFoldersAsync();

            Assert.True(musicFolders.Items.Any());

            var randomMusicFolderNumber = _random.Next(0, musicFolders.Items.Count - 1);
            var randomMusicFolder = musicFolders.Items.ElementAt(randomMusicFolderNumber);
            var indexes = await _adminSubsonicClient.GetIndexesAsync(randomMusicFolder.Id);

            Assert.True(indexes.Items.Any());
        }

        [Fact]
        public async void GetIndexesForFutureDateAsAdminUserOnSubsonic()
        {
            long ifModifiedSince = DateTime.UtcNow.AddDays(1).ToUnixTimestampInMilliseconds();
            var indexes = await _adminSubsonicClient.GetIndexesAsync(ifModifiedSince: ifModifiedSince);

            Assert.Null(indexes);
        }

        [Fact]
        public async void GetMusicDirectoryForRandomMusicFolderAndRandomIndexAsAdminUserOnSubsonic()
        {
            var musicFolders = await _adminSubsonicClient.GetMusicFoldersAsync();

            Assert.True(musicFolders.Items.Any());

            var randomMusicFolderNumber = _random.Next(0, musicFolders.Items.Count - 1);
            var randomMusicFolder = musicFolders.Items.ElementAt(randomMusicFolderNumber);
            var indexes = await _adminSubsonicClient.GetIndexesAsync(randomMusicFolder.Id);

            Assert.True(indexes.Items.Any());

            var randomIndexNumber = _random.Next(0, indexes.Items.Count - 1);
            var randomIndex = indexes.Items.ElementAt(randomIndexNumber);
            var randomArtistNumber = _random.Next(0, randomIndex.Artists.Count - 1);
            var randomArtist = randomIndex.Artists.ElementAt(randomArtistNumber);
            var musicDirectory = await _adminSubsonicClient.GetMusicDirectoryAsync(randomArtist.Id);

            Assert.True(!string.IsNullOrWhiteSpace(musicDirectory.Id));
            Assert.True(!string.IsNullOrWhiteSpace(musicDirectory.Name));
            Assert.True(musicDirectory.Children.Any());
        }

        [Fact]
        public async void GetGenresAsAdminUserOnSubsonic()
        {
            var genres = await _adminSubsonicClient.GetGenresAsync();

            Assert.True(genres.Items.Any());
        }

        [Fact]
        public async void GetArtistsAsAdminUserOnSubsonic()
        {
            var artists = await _adminSubsonicClient.GetArtistsAsync();

            Assert.True(artists.Indexes.Any());
        }

        [Fact]
        public async void GetRandomArtistAsAdminUserOnSubsonic()
        {
            var artists = await _adminSubsonicClient.GetArtistsAsync();

            Assert.True(artists.Indexes.Any());

            var randomArtistIndexNumber = _random.Next(0, artists.Indexes.Count - 1);
            var randomArtistIndex = artists.Indexes.ElementAt(randomArtistIndexNumber);

            Assert.True(randomArtistIndex.Artists.Any());

            var randomArtistNumber = _random.Next(0, randomArtistIndex.Artists.Count - 1);
            var randomArtist = randomArtistIndex.Artists.ElementAt(randomArtistNumber);
            var artist = await _adminSubsonicClient.GetArtistAsync(randomArtist.Id);

            Assert.True(!string.IsNullOrWhiteSpace(artist.Id));
            Assert.True(!string.IsNullOrWhiteSpace(artist.Name));
        }

        [Fact]
        public async void GetRandomArtistInfo2AsAdminUserOnSubsonic()
        {
            var artists = await _adminSubsonicClient.GetArtistsAsync();

            Assert.True(artists.Indexes.Any());

            var randomArtistIndexNumber = _random.Next(0, artists.Indexes.Count - 1);
            var randomArtistIndex = artists.Indexes.ElementAt(randomArtistIndexNumber);

            Assert.True(randomArtistIndex.Artists.Any());

            var randomArtistNumber = _random.Next(0, randomArtistIndex.Artists.Count - 1);
            var randomArtist = randomArtistIndex.Artists.ElementAt(randomArtistNumber);
            var artist = await _adminSubsonicClient.GetArtistAsync(randomArtist.Id);

            Assert.True(!string.IsNullOrWhiteSpace(artist.Id));
            Assert.True(!string.IsNullOrWhiteSpace(artist.Name));

            var artistInfo2 = await _adminSubsonicClient.GetArtistInfo2Async(randomArtist.Id);

            Assert.NotNull(artistInfo2);

            if (!string.IsNullOrWhiteSpace(artistInfo2.LastFmUrl))
                Assert.NotNull(artistInfo2.Biography);

            Assert.NotNull(artistInfo2.MusicBrainzId);
            Assert.NotNull(artistInfo2.SmallImageUrl);
            Assert.NotNull(artistInfo2.MediumImageUrl);
            Assert.NotNull(artistInfo2.LargeImageUrl);
        }

        [Fact]
        public async void GetRandomArtistInfoAsAdminUserOnSubsonic()
        {
            var musicFolders = await _adminSubsonicClient.GetMusicFoldersAsync();

            Assert.NotNull(musicFolders);
            Assert.True(musicFolders.Items.Any());

            var randomMusicFolderNumber = _random.Next(0, musicFolders.Items.Count - 1);
            var randomMusicFolder = musicFolders.Items.ElementAt(randomMusicFolderNumber);
            var indexes = await _adminSubsonicClient.GetIndexesAsync(randomMusicFolder.Id);

            Assert.NotNull(indexes);
            Assert.True(indexes.Items.Any());

            var randomIndexNumber = _random.Next(0, indexes.Items.Count - 1);
            var randomIndex = indexes.Items.ElementAt(randomIndexNumber);
            var randomArtistNumber = _random.Next(0, randomIndex.Artists.Count - 1);
            var randomArtist = randomIndex.Artists.ElementAt(randomArtistNumber);
            var artistInfo = await _adminSubsonicClient.GetArtistInfoAsync(randomArtist.Id);

            Assert.NotNull(artistInfo);

            if (!string.IsNullOrWhiteSpace(artistInfo.LastFmUrl))
                Assert.NotNull(artistInfo.Biography);

            Assert.NotNull(artistInfo.MusicBrainzId);
            Assert.NotNull(artistInfo.SmallImageUrl);
            Assert.NotNull(artistInfo.MediumImageUrl);
            Assert.NotNull(artistInfo.LargeImageUrl);
        }

        [Fact]
        public async void GetRandomAlbumListAsAdminUserOnSubsonic()
        {
            var albumList = await _adminSubsonicClient.GetAlbumListAsync(AlbumListType.Random);

            Assert.NotNull(albumList);
            Assert.True(albumList.Albums.Any());
        }

        [Fact]
        public async void GetNewestAlbumListAsAdminUserOnSubsonic()
        {
            var albumList = await _adminSubsonicClient.GetAlbumListAsync(AlbumListType.Newest);

            Assert.NotNull(albumList);
            Assert.True(albumList.Albums.Any());
        }

        [Fact]
        public async void GetHighestAlbumListAsAdminUserOnSubsonic()
        {
            var albumList = await _adminSubsonicClient.GetAlbumListAsync(AlbumListType.Highest);

            Assert.NotNull(albumList);
            Assert.False(albumList.Albums.Any());
        }

        [Fact]
        public async void GetFrequentAlbumListAsAdminUserOnSubsonic()
        {
            var albumList = await _adminSubsonicClient.GetAlbumListAsync(AlbumListType.Frequent);

            Assert.NotNull(albumList);
            Assert.True(albumList.Albums.Any());
        }

        [Fact]
        public async void GetRecentAlbumListAsAdminUserOnSubsonic()
        {
            var albumList = await _adminSubsonicClient.GetAlbumListAsync(AlbumListType.Recent);

            Assert.NotNull(albumList);
            Assert.True(albumList.Albums.Any());
        }

        [Fact]
        public async void GetAlphabeticalByNameAlbumListAsAdminUserOnSubsonic()
        {
            var albumList = await _adminSubsonicClient.GetAlbumListAsync(AlbumListType.AlphabeticalByName);

            Assert.NotNull(albumList);
            Assert.True(albumList.Albums.Any());
        }

        [Fact]
        public async void GetAlphabeticalByArtistAlbumListAsAdminUserOnSubsonic()
        {
            var albumList = await _adminSubsonicClient.GetAlbumListAsync(AlbumListType.AlphabeticalByArtist);

            Assert.NotNull(albumList);
            Assert.True(albumList.Albums.Any());
        }

        [Fact]
        public async void GetStarredAlbumListAsAdminUserOnSubsonic()
        {
            var albumList = await _adminSubsonicClient.GetAlbumListAsync(AlbumListType.Starred);

            Assert.NotNull(albumList);
            Assert.False(albumList.Albums.Any());
        }

        [Fact]
        public async void GetByYearAlbumListAsAdminUserOnSubsonic()
        {
            var randomFromYear = _random.Next(1950, DateTime.Now.Year);
            var randomToYear = _random.Next(randomFromYear, DateTime.Now.Year);
            var albumList = await _adminSubsonicClient.GetAlbumListAsync(AlbumListType.ByYear, fromYear: randomFromYear, toYear: randomToYear);

            Assert.NotNull(albumList);
            Assert.True(albumList.Albums.Any());
        }

        [Fact]
        public async void GetByGenreAlbumListAsAdminUserOnSubsonic()
        {
            var genres = await _adminSubsonicClient.GetGenresAsync();
            var randomNumberForGenre = _random.Next(0, genres.Items.Count - 1);
            var randomGenre = genres.Items.ElementAt(randomNumberForGenre);
            var albumList = await _adminSubsonicClient.GetAlbumListAsync(AlbumListType.ByGenre, genre: randomGenre.Name);

            Assert.NotNull(albumList);
            Assert.True(albumList.Albums.Any());
        }

        [Fact]
        public async void GetByGenreAlbumListWithInvalidServerVersionAsAdminUserOnSubsonic()
        {
            var genres = await _adminSubsonicClient.GetGenresAsync();
            var randomNumberForGenre = _random.Next(0, genres.Items.Count - 1);
            var randomGenre = genres.Items.ElementAt(randomNumberForGenre);
            var previousApiVersion = _adminSubsonicServer.ApiVersion;
            _adminSubsonicServer.ApiVersion = SubsonicApiVersion.Version1_10_0;
            await Assert.ThrowsAsync<SubsonicInvalidApiException>(async () => await _adminSubsonicClient.GetAlbumListAsync(AlbumListType.ByGenre, genre: randomGenre.Name));

            _adminSubsonicServer.ApiVersion = previousApiVersion;
        }

        [Fact]
        public async void GetByYearAlbumListWithInvalidParametersAsAdminUserOnSubsonic()
        {
            await Assert.ThrowsAsync<SubsonicErrorException>(async () => await _adminSubsonicClient.GetAlbumListAsync(AlbumListType.ByYear));
        }

        [Fact]
        public async void GenresActivityTest()
        {
            var genresActivity = new GenresActivity<Image>(_adminSubsonicClient);
            var genres = await genresActivity.GetResult();

            Assert.True(genres.Items.Any());

            genres = await genresActivity.GetResult();

            Assert.True(genres.Items.Any());
        }

        [Fact]
        public async void MusicFoldersActivityTest()
        {
            var activity = new MusicFoldersActivity<Image>(_adminSubsonicClient);
            var musicFolders = await activity.GetResult();

            Assert.True(musicFolders.Items.Any());

            musicFolders = await activity.GetResult();

            Assert.True(musicFolders.Items.Any());
        }

        [Fact]
        public async void Search2ActivityTest()
        {
            var activity = new Search2Activity<Image>(_adminSubsonicClient, SearchQuery);
            var searchResult2 = await activity.GetResult();

            Assert.True(searchResult2.Artists.Any());

            searchResult2 = await activity.GetResult();

            Assert.True(searchResult2.Artists.Any());
        }

        [Fact]
        public async void ScanMediaFoldersAsAdminUserOnSubsonic()
        {
            var success = await _adminSubsonicClient.ScanMediaFoldersAsync();

            Assert.True(success);
        }

        [Fact]
        public async void CleanupMediaFoldersAsAdminUserOnSubsonic()
        {
            var success = await _adminSubsonicClient.CleanupMediaFoldersAsync();

            Assert.True(success);
        }

        [Fact]
        public async void GetCoverArtSizeAsAdminUserOnSubsonic()
        {
            var albumList = await _adminSubsonicClient.GetAlbumListAsync(AlbumListType.Random, 1);

            Assert.NotNull(albumList);

            var artSize = await _adminSubsonicClient.GetCoverArtSizeAsync(albumList.Albums.First().CoverArt);

            Assert.True(artSize >= 0);
        }

        [Fact]
        public async void SaveRandomSongToPlayQueueAsAdminUserOnSubsonic()
        {
            var randomSongs = await _adminSubsonicClient.GetRandomSongsAsync(1);
            var success = await _adminSubsonicClient.SavePlayQueueAsync(randomSongs.Songs.First().Id, randomSongs.Songs.First().Id, randomSongs.Songs.First().Duration / 2);

            Assert.True(success);
        }

        [Fact]
        public async void GetPlayQueueAsAdminUserOnSubsonic()
        {
            var playQueue = await _adminSubsonicClient.GetPlayQueueAsync();

            Assert.NotNull(playQueue.Username);
        }

        [Fact]
        public async void GetRandomAlbumInfoAsAdminUserOnSubsonic()
        {
            var randomSongs = await _adminSubsonicClient.GetRandomSongsAsync(1);
            var albumInfo = await _adminSubsonicClient.GetAlbumInfoAsync(randomSongs.Songs.First().Id);

            Assert.NotNull(albumInfo);
        }
    }
}