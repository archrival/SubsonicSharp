using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using Xunit;
using Subsonic.Client.Exceptions;
using Subsonic.Client.Extensions;
using Subsonic.Common;
using Subsonic.Common.Classes;
using Subsonic.Common.Enums;
using Subsonic.Common.Interfaces;
using Subsonic.Client.Activities;
using Subsonic.Client.Interfaces;

namespace Subsonic.Client.Windows.Tests
{
    public class Tests : IDisposable
    {
        public static readonly Uri SubsonicServer = new Uri("http://192.168.1.3:8080/subsonic/");
        public static readonly Uri MadsonicServer = new Uri("http://192.168.1.3:8080/madsonic/");
        public static readonly Uri NonexistentServer = new Uri("http://localhost/ultrasonic/");
        public const string ProxyServer = "localhost";
        public const int ProxyPort = 8888;
        public const string AdminUser = "admin";
        public const string DownloadUser = "test_download";
        public const string NoPlayUser = "test_noplay";
        public const string PlayUser = "test_play";
        public const string Password = "password";
        public const string UserToCreate = "test_createduser";
        public const string UserToCreateEmail = "test_createduser@localhost";
        public const string ClientName = "Subsonic.Client.Windows.Tests";
        public const string SearchQuery = "Smashing Pumpkins";

        public const int MaxRandomSongCount = 500;
        public const int MinRandomSongCount = 1;
        public const int InvalidRandomSongCount = 501;

        public ISubsonicClient<Image> AdminSubsonicClient;
        public ISubsonicClient<Image> DownloadSubsonicClient;
        public ISubsonicClient<Image> NoPlaySubsonicClient;
        public ISubsonicClient<Image> PlaySubsonicClient;
        public ISubsonicClient<Image> NonexistentSubsonicClient;
        public ISubsonicServer AdminSubsonicServer;
        public ISubsonicServer DownloadSubsonicServer;
        public ISubsonicServer NoPlaySubsonicServer;
        public ISubsonicServer PlaySubsonicServer;
        public ISubsonicServer NonexistentSubsonicServer;
        public IImageFormatFactory<Image> ImageFormatFactory;
        public Random Random;

        public Tests()
        {
            AdminSubsonicServer = new SubsonicServer(SubsonicServer, AdminUser, Password, ClientName);
            DownloadSubsonicServer = new SubsonicServer(SubsonicServer, DownloadUser, Password, ClientName);
            NoPlaySubsonicServer = new SubsonicServer(SubsonicServer, NoPlayUser, Password, ClientName);
            PlaySubsonicServer = new SubsonicServer(SubsonicServer, PlayUser, Password, ClientName);
            NonexistentSubsonicServer = new SubsonicServer(NonexistentServer, AdminUser, Password, ClientName);

            ImageFormatFactory = new ImageFormatFactory();

            AdminSubsonicClient = new SubsonicClient(AdminSubsonicServer, ImageFormatFactory);
            DownloadSubsonicClient = new SubsonicClient(DownloadSubsonicServer, ImageFormatFactory);
            NoPlaySubsonicClient = new SubsonicClient(NoPlaySubsonicServer, ImageFormatFactory);
            PlaySubsonicClient = new SubsonicClient(PlaySubsonicServer, ImageFormatFactory);
            NonexistentSubsonicClient = new SubsonicClient(NonexistentSubsonicServer, ImageFormatFactory);
            Random = new Random(DateTime.UtcNow.Millisecond * DateTime.UtcNow.Second * DateTime.UtcNow.Minute);
        }

        public void Dispose()
        {
            AdminSubsonicClient = null;
            DownloadSubsonicClient = null;
            NoPlaySubsonicClient = null;
            PlaySubsonicClient = null;
            NonexistentSubsonicClient = null;
            Random = null;
        }

        [Fact]
        public async void PingTestAdminOnSubsonic()
        {
            var result = await AdminSubsonicClient.PingAsync();

            Assert.True(result);
            Assert.NotNull(AdminSubsonicServer.ApiVersion);
            Assert.True(AdminSubsonicServer.ApiVersion >= SubsonicApiVersions.Version1_0_0);
        }

        [Fact]
        public async void PingTestDownloadOnSubsonic()
        {
            var result = await DownloadSubsonicClient.PingAsync();

            Assert.True(result);
            Assert.NotNull(DownloadSubsonicServer.ApiVersion);
            Assert.True(DownloadSubsonicServer.ApiVersion >= SubsonicApiVersions.Version1_0_0);
        }

        [Fact]
        public async void PingTestNoPlayOnSubsonic()
        {
            var result = await NoPlaySubsonicClient.PingAsync();

            Assert.True(result);
            Assert.NotNull(NoPlaySubsonicServer.ApiVersion);
            Assert.True(NoPlaySubsonicServer.ApiVersion >= SubsonicApiVersions.Version1_0_0);
        }

        [Fact]
        public async void PingTestPlayOnSubsonic()
        {
            var result = await PlaySubsonicClient.PingAsync();

            Assert.True(result);
            Assert.NotNull(PlaySubsonicServer.ApiVersion);
            Assert.True(PlaySubsonicServer.ApiVersion >= SubsonicApiVersions.Version1_0_0);
        }

        [Fact]
        public async void PingTestOnNonexistentServer()
        {
            Assert.Null(NonexistentSubsonicServer.ApiVersion);
            await Assert.ThrowsAsync<SubsonicApiException>(async () => await NonexistentSubsonicClient.PingAsync());
            Assert.Null(NonexistentSubsonicServer.ApiVersion);
        }

        [Fact]
        public async void LicenseTestAdminOnSubsonic()
        {
            var license = await AdminSubsonicClient.GetLicenseAsync();

            Assert.True(license.Valid);
        }

        [Fact]
        public async void LicenseTestDownloadOnSubsonic()
        {
            var license = await DownloadSubsonicClient.GetLicenseAsync();

            Assert.True(license.Valid);
        }

        [Fact]
        public async void LicenseTestNoPlayOnSubsonic()
        {
            var license = await NoPlaySubsonicClient.GetLicenseAsync();

            Assert.True(license.Valid);
        }

        [Fact]
        public async void LicenseTestPlayOnSubsonic()
        {
            var license = await PlaySubsonicClient.GetLicenseAsync();

            Assert.True(license.Valid);
        }

        [Fact]
        public async void CreateAdminUserOnSubsonic()
        {
            var result = await AdminSubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail, false, true);

            Assert.True(result);

            var user = await AdminSubsonicClient.GetUserAsync(UserToCreate);

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

            result = await AdminSubsonicClient.DeleteUserAsync(UserToCreate);

            Assert.True(result);
        }

        [Fact]
        public async void CreateAdminUserAsPlayUserOnSubsonicThrowsUserNotAuthorized()
        {
            SubsonicErrorException ex = await Assert.ThrowsAsync<SubsonicErrorException>(async () => await PlaySubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail, false, true));
            Assert.True(ex.Error.Code.Equals(ErrorCode.UserNotAuthorized));
        }

        [Fact]
        public async void CreateAdminUserAsNoPlayUserOnSubsonicThrowsUserNotAuthorized()
        {
            SubsonicErrorException ex = await Assert.ThrowsAsync<SubsonicErrorException>(async () => await NoPlaySubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail, false, true));
            Assert.True(ex.Error.Code.Equals(ErrorCode.UserNotAuthorized));
        }

        [Fact]
        public async void CreateAdminUserAsDownloadUserOnSubsonicThrowsUserNotAuthorized()
        {
            SubsonicErrorException ex = await Assert.ThrowsAsync<SubsonicErrorException>(async () => await DownloadSubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail, false, true));
            Assert.True(ex.Error.Code.Equals(ErrorCode.UserNotAuthorized));
        }

        [Fact]
        public async void CreateUserAsPlayUserOnSubsonicThrowsUserNotAuthorized()
        {
            SubsonicErrorException ex = await Assert.ThrowsAsync<SubsonicErrorException>(async () => await PlaySubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail));
            Assert.True(ex.Error.Code.Equals(ErrorCode.UserNotAuthorized));
        }

        [Fact]
        public async void CreateUserAsNoPlayUserOnSubsonicThrowsUserNotAuthorized()
        {
            SubsonicErrorException ex = await Assert.ThrowsAsync<SubsonicErrorException>(async () => await NoPlaySubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail));
            Assert.True(ex.Error.Code.Equals(ErrorCode.UserNotAuthorized));
        }

        [Fact]
        public async void CreateUserAsDownloadUserOnSubsonicThrowsUserNotAuthorized()
        {
            SubsonicErrorException ex = await Assert.ThrowsAsync<SubsonicErrorException>(async () => await DownloadSubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail));
            Assert.True(ex.Error.Code.Equals(ErrorCode.UserNotAuthorized));
        }

        [Fact]
        public async void AddChatMessageAsPlayUserOnSubsonic()
        {
            var chatMessage = DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture);
            var result = await PlaySubsonicClient.AddChatMessageAsync(chatMessage);

            Assert.True(result);

            var chatMessages = await NoPlaySubsonicClient.GetChatMessagesAsync();

            Assert.True(chatMessages.Items.Any(c => c.Message == chatMessage && c.Username == PlayUser));
        }

        [Fact]
        public async void AddChatMessageAsNoPlayUserOnSubsonic()
        {
            var chatMessage = DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture);
            var result = await NoPlaySubsonicClient.AddChatMessageAsync(chatMessage);

            Assert.True(result);

            var chatMessages = await PlaySubsonicClient.GetChatMessagesAsync();

            Assert.True(chatMessages.Items.Any(c => c.Message == chatMessage && c.Username == NoPlayUser));
        }

        [Fact]
        public async void AddChatMessageAsAdminUserOnSubsonic()
        {
            var chatMessage = DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture);
            var result = await AdminSubsonicClient.AddChatMessageAsync(chatMessage);

            Assert.True(result);

            var chatMessages = await NoPlaySubsonicClient.GetChatMessagesAsync();

            Assert.True(chatMessages.Items.Any(c => c.Message == chatMessage && c.Username == AdminUser));
        }

        [Fact]
        public async void GetRandomSongsAsAdminUserOnSubsonic()
        {
            var randomNumber = Random.Next(MinRandomSongCount, MaxRandomSongCount);
            RandomSongs randomSongs = await AdminSubsonicClient.GetRandomSongsAsync(randomNumber);

            Assert.Equal(randomSongs.Songs.Count, randomNumber);
        }

        [Fact]
        public async void GetInvalidNumberOfRandomSongsAsAdminUserOnSubsonic()
        {
            RandomSongs randomSongs = await AdminSubsonicClient.GetRandomSongsAsync(InvalidRandomSongCount);

            Assert.True(randomSongs.Songs.Count < InvalidRandomSongCount);
        }

        [Fact]
        public async void GetRandomSongsForGenreAsAdminUserOnSubsonic()
        {
            var randomNumber = Random.Next(MinRandomSongCount, MaxRandomSongCount);
            var genres = await AdminSubsonicClient.GetGenresAsync();
            var randomNumberForGenre = Random.Next(0, genres.Items.Count - 1);
            var randomGenre = genres.Items.ElementAt(randomNumberForGenre);
            var randomSongs = await AdminSubsonicClient.GetRandomSongsAsync(randomNumber, randomGenre.Name);

            Assert.True(randomSongs.Songs.All(s => string.Compare(s.Genre, randomGenre.Name, StringComparison.OrdinalIgnoreCase) == 0));
        }

        [Fact]
        public async void GetMusicFoldersAsAdminUserOnSubsonic()
        {
            var musicFolders = await AdminSubsonicClient.GetMusicFoldersAsync();

            Assert.True(musicFolders.Items.Any());
        }

        [Fact]
        public async void GetIndexesForAllMusicFoldersAsAdminUserOnSubsonic()
        {
            var indexes = await AdminSubsonicClient.GetIndexesAsync();

            Assert.NotNull(indexes);
            Assert.True(indexes.Items.Any());
        }

        [Fact]
        public async void GetIndexesForRandomMusicFolderAsAdminUserOnSubsonic()
        {
            var musicFolders = await AdminSubsonicClient.GetMusicFoldersAsync();

            Assert.True(musicFolders.Items.Any());

            var randomMusicFolderNumber = Random.Next(0, musicFolders.Items.Count - 1);
            var randomMusicFolder = musicFolders.Items.ElementAt(randomMusicFolderNumber);
            var indexes = await AdminSubsonicClient.GetIndexesAsync(randomMusicFolder.Id);

            Assert.True(indexes.Items.Any());
        }

        [Fact]
        public async void GetIndexesForFutureDateAsAdminUserOnSubsonic()
        {
            long ifModifiedSince = DateTime.UtcNow.AddDays(1).ToUnixTimestampInMilliseconds();
            var indexes = await AdminSubsonicClient.GetIndexesAsync(ifModifiedSince: ifModifiedSince);

            Assert.Null(indexes);
        }

        [Fact]
        public async void GetMusicDirectoryForRandomMusicFolderAndRandomIndexAsAdminUserOnSubsonic()
        {
            var musicFolders = await AdminSubsonicClient.GetMusicFoldersAsync();

            Assert.True(musicFolders.Items.Any());

            var randomMusicFolderNumber = Random.Next(0, musicFolders.Items.Count - 1);
            var randomMusicFolder = musicFolders.Items.ElementAt(randomMusicFolderNumber);
            var indexes = await AdminSubsonicClient.GetIndexesAsync(randomMusicFolder.Id);

            Assert.True(indexes.Items.Any());

            var randomIndexNumber = Random.Next(0, indexes.Items.Count - 1);
            var randomIndex = indexes.Items.ElementAt(randomIndexNumber);
            var randomArtistNumber = Random.Next(0, randomIndex.Artists.Count - 1);
            var randomArtist = randomIndex.Artists.ElementAt(randomArtistNumber);
            var musicDirectory = await AdminSubsonicClient.GetMusicDirectoryAsync(randomArtist.Id);

            Assert.True(!string.IsNullOrWhiteSpace(musicDirectory.Id));
            Assert.True(!string.IsNullOrWhiteSpace(musicDirectory.Name));
            Assert.True(musicDirectory.Children.Any());
        }

        [Fact]
        public async void GetGenresAsAdminUserOnSubsonic()
        {
            var genres = await AdminSubsonicClient.GetGenresAsync();

            Assert.True(genres.Items.Any());
        }

        [Fact]
        public async void GetArtistsAsAdminUserOnSubsonic()
        {
            var artists = await AdminSubsonicClient.GetArtistsAsync();

            Assert.True(artists.Indexes.Any());
        }

        [Fact]
        public async void GetRandomArtistAsAdminUserOnSubsonic()
        {
            var artists = await AdminSubsonicClient.GetArtistsAsync();

            Assert.True(artists.Indexes.Any());

            var randomArtistIndexNumber = Random.Next(0, artists.Indexes.Count - 1);
            var randomArtistIndex = artists.Indexes.ElementAt(randomArtistIndexNumber);

            Assert.True(randomArtistIndex.Artists.Any());

            var randomArtistNumber = Random.Next(0, randomArtistIndex.Artists.Count - 1);
            var randomArtist = randomArtistIndex.Artists.ElementAt(randomArtistNumber);
            var artist = await AdminSubsonicClient.GetArtistAsync(randomArtist.Id);

            Assert.True(!string.IsNullOrWhiteSpace(artist.Id));
            Assert.True(!string.IsNullOrWhiteSpace(artist.Name));
        }

        [Fact]
        public async void GetRandomArtistInfo2AsAdminUserOnSubsonic()
        {
            var artists = await AdminSubsonicClient.GetArtistsAsync();

            Assert.True(artists.Indexes.Any());

            var randomArtistIndexNumber = Random.Next(0, artists.Indexes.Count - 1);
            var randomArtistIndex = artists.Indexes.ElementAt(randomArtistIndexNumber);

            Assert.True(randomArtistIndex.Artists.Any());

            var randomArtistNumber = Random.Next(0, randomArtistIndex.Artists.Count - 1);
            var randomArtist = randomArtistIndex.Artists.ElementAt(randomArtistNumber);
            var artist = await AdminSubsonicClient.GetArtistAsync(randomArtist.Id);

            Assert.True(!string.IsNullOrWhiteSpace(artist.Id));
            Assert.True(!string.IsNullOrWhiteSpace(artist.Name));

            var artistInfo2 = await AdminSubsonicClient.GetArtistInfo2Async(randomArtist.Id);

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
            var musicFolders = await AdminSubsonicClient.GetMusicFoldersAsync();

            Assert.NotNull(musicFolders);
            Assert.True(musicFolders.Items.Any());

            var randomMusicFolderNumber = Random.Next(0, musicFolders.Items.Count - 1);
            var randomMusicFolder = musicFolders.Items.ElementAt(randomMusicFolderNumber);
            var indexes = await AdminSubsonicClient.GetIndexesAsync(randomMusicFolder.Id);

            Assert.NotNull(indexes);
            Assert.True(indexes.Items.Any());

            var randomIndexNumber = Random.Next(0, indexes.Items.Count - 1);
            var randomIndex = indexes.Items.ElementAt(randomIndexNumber);
            var randomArtistNumber = Random.Next(0, randomIndex.Artists.Count - 1);
            var randomArtist = randomIndex.Artists.ElementAt(randomArtistNumber);
            var artistInfo = await AdminSubsonicClient.GetArtistInfoAsync(randomArtist.Id);

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
            var albumList = await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.Random);

            Assert.NotNull(albumList);
            Assert.True(albumList.Albums.Any());
        }

        [Fact]
        public async void GetNewestAlbumListAsAdminUserOnSubsonic()
        {
            var albumList = await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.Newest);

            Assert.NotNull(albumList);
            Assert.True(albumList.Albums.Any());
        }

        [Fact]
        public async void GetHighestAlbumListAsAdminUserOnSubsonic()
        {
            var albumList = await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.Highest);

            Assert.NotNull(albumList);
            Assert.False(albumList.Albums.Any());
        }

        [Fact]
        public async void GetFrequentAlbumListAsAdminUserOnSubsonic()
        {
            var albumList = await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.Frequent);

            Assert.NotNull(albumList);
            Assert.True(albumList.Albums.Any());
        }

        [Fact]
        public async void GetRecentAlbumListAsAdminUserOnSubsonic()
        {
            var albumList = await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.Recent);

            Assert.NotNull(albumList);
            Assert.True(albumList.Albums.Any());
        }

        [Fact]
        public async void GetAlphabeticalByNameAlbumListAsAdminUserOnSubsonic()
        {
            var albumList = await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.AlphabeticalByName);

            Assert.NotNull(albumList);
            Assert.True(albumList.Albums.Any());
        }

        [Fact]
        public async void GetAlphabeticalByArtistAlbumListAsAdminUserOnSubsonic()
        {
            var albumList = await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.AlphabeticalByArtist);

            Assert.NotNull(albumList);
            Assert.True(albumList.Albums.Any());
        }

        [Fact]
        public async void GetStarredAlbumListAsAdminUserOnSubsonic()
        {
            var albumList = await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.Starred);

            Assert.NotNull(albumList);
            Assert.False(albumList.Albums.Any());
        }

        [Fact]
        public async void GetByYearAlbumListAsAdminUserOnSubsonic()
        {
            var randomFromYear = Random.Next(1950, DateTime.Now.Year);
            var randomToYear = Random.Next(randomFromYear, DateTime.Now.Year);
            var albumList = await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.ByYear, fromYear: randomFromYear, toYear: randomToYear);

            Assert.NotNull(albumList);
            Assert.True(albumList.Albums.Any());
        }

        [Fact]
        public async void GetByGenreAlbumListAsAdminUserOnSubsonic()
        { 
            var genres = await AdminSubsonicClient.GetGenresAsync();
            var randomNumberForGenre = Random.Next(0, genres.Items.Count - 1);
            var randomGenre = genres.Items.ElementAt(randomNumberForGenre);
            var albumList = await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.ByGenre, genre: randomGenre.Name);

            Assert.NotNull(albumList);
            Assert.True(albumList.Albums.Any());
        }

        [Fact]
        public async void GetByGenreAlbumListWithInvalidServerVersionAsAdminUserOnSubsonic()
        {
            var genres = await AdminSubsonicClient.GetGenresAsync();
            var randomNumberForGenre = Random.Next(0, genres.Items.Count - 1);
            var randomGenre = genres.Items.ElementAt(randomNumberForGenre);
            var previousApiVersion = AdminSubsonicServer.ApiVersion;
            AdminSubsonicServer.ApiVersion = SubsonicApiVersions.Version1_10_0;
            await Assert.ThrowsAsync<SubsonicInvalidApiException>(async () => await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.ByGenre, genre: randomGenre.Name));

            AdminSubsonicServer.ApiVersion = previousApiVersion;
        }

        [Fact]
        public async void GetByYearAlbumListWithInvalidParametersAsAdminUserOnSubsonic()
        {
            await Assert.ThrowsAsync<SubsonicErrorException>(async () => await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.ByYear));
        }

        [Fact]
        public async void GenresActivityTest()
        {
            var genresActivity = new GenresActivity<Image>(AdminSubsonicClient);
            var genres = await genresActivity.GetResult();

            Assert.True(genres.Items.Any());

            genres = await genresActivity.GetResult();

            Assert.True(genres.Items.Any());
        }

        [Fact]
        public async void MusicFoldersActivityTest()
        {
            var activity = new MusicFoldersActivity<Image>(AdminSubsonicClient);
            var musicFolders = await activity.GetResult();

            Assert.True(musicFolders.Items.Any());

            musicFolders = await activity.GetResult();

            Assert.True(musicFolders.Items.Any());
        }

        [Fact]
        public async void Search2ActivityTest()
        {
            var activity = new Search2Activity<Image>(AdminSubsonicClient, SearchQuery);
            var searchResult2 = await activity.GetResult();

            Assert.True(searchResult2.Artists.Any());

            searchResult2 = await activity.GetResult();

            Assert.True(searchResult2.Artists.Any());
        }

        [Fact]
        public async void ScanMediaFoldersAsAdminUserOnSubsonic()
        {
            var success = await AdminSubsonicClient.ScanMediaFoldersAsync();

            Assert.True(success);
        }

        [Fact]
        public async void CleanupMediaFoldersAsAdminUserOnSubsonic()
        {
            var success = await AdminSubsonicClient.CleanupMediaFoldersAsync();

            Assert.True(success);
        }

        [Fact]
        public async void GetCoverArtSizeAsAdminUserOnSubsonic()
        {
            var albumList = await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.Random, 1);

            Assert.NotNull(albumList);

            var artSize = await AdminSubsonicClient.GetCoverArtSizeAsync(albumList.Albums.First().CoverArt);

            Assert.True(artSize >= 0);
        }

        [Fact]
        public async void SaveRandomSongToPlayQueueAsAdminUserOnSubsonic()
        {
            var randomSongs = await AdminSubsonicClient.GetRandomSongsAsync(1);
            var success = await AdminSubsonicClient.SavePlayQueueAsync(randomSongs.Songs.First().Id, randomSongs.Songs.First().Id, randomSongs.Songs.First().Duration / 2);

            Assert.True(success);
        }

        [Fact]
        public async void GetPlayQueueAsAdminUserOnSubsonic()
        {
            var playQueue = await AdminSubsonicClient.GetPlayQueueAsync();

            Assert.NotNull(playQueue.Username);
        }

        [Fact]
        public async void GetRandomAlbumInfoAsAdminUserOnSubsonic()
        {
            var randomSongs = await AdminSubsonicClient.GetRandomSongsAsync(1);
            var albumInfo = await AdminSubsonicClient.GetAlbumInfoAsync(randomSongs.Songs.First().Id);

            Assert.NotNull(albumInfo);
        }
    }
}