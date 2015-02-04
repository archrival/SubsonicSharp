using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using NUnit.Framework;
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
    [TestFixture]
    public class Test
    {
        public static readonly Uri SubsonicServer = new Uri("http://localhost/subsonic/");
        public static readonly Uri MadsonicServer = new Uri("http://localhost/madsonic/");
        public static readonly Uri NonexistentServer = new Uri("http://localhost/ultrasonic/");
        public const string ProxyServer = "localhost";
        public const int ProxyPort = 8888;
        public const string AdminUser = "test_admin";
        public const string DownloadUser = "test_download";
        public const string NoPlayUser = "test_noplay";
        public const string PlayUser = "test_play";
        public const string Password = "password";
        public const string UserToCreate = "test_createduser";
        public const string UserToCreateEmail = "test_createduser@localhost";
        public const string ClientName = "Subsonic.Client.Windows.Tests";

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
        public Random Random;

        [TestFixtureSetUp]
        public void Setup()
        {
            AdminSubsonicServer = new SubsonicServer(SubsonicServer, AdminUser, Password, ClientName);
            DownloadSubsonicServer = new SubsonicServer(SubsonicServer, DownloadUser, Password, ClientName);
            NoPlaySubsonicServer = new SubsonicServer(SubsonicServer, NoPlayUser, Password, ClientName);
            PlaySubsonicServer = new SubsonicServer(SubsonicServer, PlayUser, Password, ClientName);
            NonexistentSubsonicServer = new SubsonicServer(NonexistentServer, AdminUser, Password, ClientName);

            AdminSubsonicClient = new SubsonicClientWindows(AdminSubsonicServer);
            DownloadSubsonicClient = new SubsonicClientWindows(DownloadSubsonicServer);
            NoPlaySubsonicClient = new SubsonicClientWindows(NoPlaySubsonicServer);
            PlaySubsonicClient = new SubsonicClientWindows(PlaySubsonicServer);
            NonexistentSubsonicClient = new SubsonicClientWindows(NonexistentSubsonicServer);
            Random = new Random(DateTime.UtcNow.Millisecond * DateTime.UtcNow.Second * DateTime.UtcNow.Minute);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            AdminSubsonicClient = null;
            DownloadSubsonicClient = null;
            NoPlaySubsonicClient = null;
            PlaySubsonicClient = null;
            NonexistentSubsonicClient = null;
            Random = null;
        }

        [Test]
        public void PingTestAdminOnSubsonic()
        {
            bool result = false;

            Assert.DoesNotThrow(async () =>
                {
                    result = await AdminSubsonicClient.PingAsync();
                });

            Assert.IsTrue(result);
            Assert.IsNotNull(AdminSubsonicServer.GetApiVersion());
            Assert.IsTrue(AdminSubsonicServer.GetApiVersion() >= SubsonicApiVersions.Version1_0_0);
        }

        [Test]
        public void PingTestDownloadOnSubsonic()
        {
            bool result = false;

            Assert.DoesNotThrow(async () =>
                {
                    result = await DownloadSubsonicClient.PingAsync();
                });

            Assert.IsTrue(result);
            Assert.IsNotNull(DownloadSubsonicServer.GetApiVersion());
            Assert.IsTrue(DownloadSubsonicServer.GetApiVersion() >= SubsonicApiVersions.Version1_0_0);
        }

        [Test]
        public void PingTestNoPlayOnSubsonic()
        {
            bool result = false;

            Assert.DoesNotThrow(async () =>
                {
                    result = await NoPlaySubsonicClient.PingAsync();
                });

            Assert.IsTrue(result);
            Assert.IsNotNull(NoPlaySubsonicServer.GetApiVersion());
            Assert.IsTrue(NoPlaySubsonicServer.GetApiVersion() >= SubsonicApiVersions.Version1_0_0);
        }

        [Test]
        public void PingTestPlayOnSubsonic()
        {
            bool result = false;

            Assert.DoesNotThrow(async () =>
                {
                    result = await PlaySubsonicClient.PingAsync();
                });

            Assert.IsTrue(result);
            Assert.IsNotNull(PlaySubsonicServer.GetApiVersion());
            Assert.IsTrue(PlaySubsonicServer.GetApiVersion() >= SubsonicApiVersions.Version1_0_0);
        }

        [Test]
        public void PingTestOnNonexistentServer()
        {
            Assert.IsNull(NonexistentSubsonicServer.GetApiVersion());
            Assert.Throws<SubsonicApiException>(async () => await NonexistentSubsonicClient.PingAsync());
            Assert.IsNull(NonexistentSubsonicServer.GetApiVersion());
        }

        [Test]
        public void LicenseTestAdminOnSubsonic()
        {
            License license = null;

            Assert.DoesNotThrow(async () =>
                {
                    license = await AdminSubsonicClient.GetLicenseAsync();
                });

            Assert.IsTrue(license.Valid);
        }

        [Test]
        public void LicenseTestDownloadOnSubsonic()
        {
            License license = null;

            Assert.DoesNotThrow(async () =>
                {
                    license = await DownloadSubsonicClient.GetLicenseAsync();
                });

            Assert.IsTrue(license.Valid);
        }

        [Test]
        public void LicenseTestNoPlayOnSubsonic()
        {
            License license = null;

            Assert.DoesNotThrow(async () =>
                {
                    license = await NoPlaySubsonicClient.GetLicenseAsync();
                });

            Assert.IsTrue(license.Valid);
        }

        [Test]
        public void LicenseTestPlayOnSubsonic()
        {
            License license = null;

            Assert.DoesNotThrow(async () =>
                {
                    license = await PlaySubsonicClient.GetLicenseAsync();
                });

            Assert.IsTrue(license.Valid);
        }

        [Test]
        public void CreateAdminUserOnSubsonic()
        {
            bool result = false;

            Assert.DoesNotThrow(async () =>
                {
                    result = await AdminSubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail, false, true);
                });

            Assert.IsTrue(result);

            User user = null;

            Assert.DoesNotThrow(async () =>
                {
                    user = await AdminSubsonicClient.GetUserAsync(UserToCreate);
                });

            Assert.That(user.Username.Equals(UserToCreate));
            Assert.That(user.Email.Equals(UserToCreateEmail));
            Assert.IsTrue(user.AdminRole);
            Assert.IsFalse(user.CommentRole);
            Assert.IsFalse(user.CoverArtRole);
            Assert.IsFalse(user.DownloadRole);
            Assert.IsFalse(user.JukeboxRole);
            Assert.IsTrue(user.PlaylistRole);
            Assert.IsFalse(user.PodcastRole);
            Assert.IsFalse(user.ScrobblingEnabled);
            Assert.IsTrue(user.SettingsRole);
            Assert.IsFalse(user.ShareRole);
            Assert.IsTrue(user.StreamRole);
            Assert.IsFalse(user.UploadRole);

            result = false;

            Assert.DoesNotThrow(async () =>
                {
                    result = await AdminSubsonicClient.DeleteUserAsync(UserToCreate);
                });

            Assert.IsTrue(result);
        }

        [Test]
        public void CreateAdminUserAsPlayUserOnSubsonicThrowsUserNotAuthorized()
        {
            SubsonicErrorException ex = Assert.Throws<SubsonicErrorException>(async () => await PlaySubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail, false, true));
            Assert.That(ex.Error.Code.Equals(ErrorCode.UserNotAuthorized));
        }

        [Test]
        public void CreateAdminUserAsNoPlayUserOnSubsonicThrowsUserNotAuthorized()
        {
            SubsonicErrorException ex = Assert.Throws<SubsonicErrorException>(async () => await NoPlaySubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail, false, true));
            Assert.That(ex.Error.Code.Equals(ErrorCode.UserNotAuthorized));
        }

        [Test]
        public void CreateAdminUserAsDownloadUserOnSubsonicThrowsUserNotAuthorized()
        {
            SubsonicErrorException ex = Assert.Throws<SubsonicErrorException>(async () => await DownloadSubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail, false, true));
            Assert.That(ex.Error.Code.Equals(ErrorCode.UserNotAuthorized));
        }

        [Test]
        public void CreateUserAsPlayUserOnSubsonicThrowsUserNotAuthorized()
        {
            SubsonicErrorException ex = Assert.Throws<SubsonicErrorException>(async () => await PlaySubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail));
            Assert.That(ex.Error.Code.Equals(ErrorCode.UserNotAuthorized));
        }

        [Test]
        public void CreateUserAsNoPlayUserOnSubsonicThrowsUserNotAuthorized()
        {
            SubsonicErrorException ex = Assert.Throws<SubsonicErrorException>(async () => await NoPlaySubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail));
            Assert.That(ex.Error.Code.Equals(ErrorCode.UserNotAuthorized));
        }

        [Test]
        public void CreateUserAsDownloadUserOnSubsonicThrowsUserNotAuthorized()
        {
            SubsonicErrorException ex = Assert.Throws<SubsonicErrorException>(async () => await DownloadSubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail));
            Assert.That(ex.Error.Code.Equals(ErrorCode.UserNotAuthorized));
        }

        [Test]
        public void AddChatMessageAsPlayUserOnSubsonic()
        {
            bool result = false;

            string chatMessage = DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture);

            Assert.DoesNotThrow(async () =>
                {
                    result = await PlaySubsonicClient.AddChatMessageAsync(chatMessage);
                });

            Assert.IsTrue(result);

            ChatMessages chatMessages = null;

            Assert.DoesNotThrow(async () =>
                {
                    chatMessages = await NoPlaySubsonicClient.GetChatMessagesAsync();
                });

            Assert.IsTrue(chatMessages.Items.Any(c => c.Message == chatMessage && c.Username == PlayUser));
        }

        [Test]
        public void AddChatMessageAsNoPlayUserOnSubsonic()
        {
            bool result = false;

            string chatMessage = DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture);

            Assert.DoesNotThrow(async () =>
                {
                    result = await NoPlaySubsonicClient.AddChatMessageAsync(chatMessage);
                });

            Assert.IsTrue(result);

            ChatMessages chatMessages = null;

            Assert.DoesNotThrow(async () =>
                {
                    chatMessages = await PlaySubsonicClient.GetChatMessagesAsync();
                });

            Assert.IsTrue(chatMessages.Items.Any(c => c.Message == chatMessage && c.Username == NoPlayUser));
        }

        [Test]
        public void AddChatMessageAsAdminUserOnSubsonic()
        {
            bool result = false;

            string chatMessage = DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture);

            Assert.DoesNotThrow(async () =>
                {
                    result = await AdminSubsonicClient.AddChatMessageAsync(chatMessage);
                });

            Assert.IsTrue(result);

            ChatMessages chatMessages = null;

            Assert.DoesNotThrow(async () =>
                {
                    chatMessages = await NoPlaySubsonicClient.GetChatMessagesAsync();
                });

            Assert.IsTrue(chatMessages.Items.Any(c => c.Message == chatMessage && c.Username == AdminUser));
        }

        [Test]
        public void GetRandomSongsAsAdminUserOnSubsonic()
        {
            int randomNumber = Random.Next(MinRandomSongCount, MaxRandomSongCount);
            RandomSongs randomSongs = null;

            Assert.DoesNotThrow(async () =>
                {
                    randomSongs = await AdminSubsonicClient.GetRandomSongsAsync(randomNumber);
                });

            Assert.AreEqual(randomSongs.Songs.Count, randomNumber);
        }

        [Test]
        public void GetInvalidNumberOfRandomSongsAsAdminUserOnSubsonic()
        {
            RandomSongs randomSongs = null;

            Assert.DoesNotThrow(async () =>
                {
                    randomSongs = await AdminSubsonicClient.GetRandomSongsAsync(InvalidRandomSongCount);
                });

            Assert.Less(randomSongs.Songs.Count, InvalidRandomSongCount);
        }

        [Test]
        public void GetRandomSongsForGenreAsAdminUserOnSubsonic()
        {
            int randomNumber = Random.Next(MinRandomSongCount, MaxRandomSongCount);
            RandomSongs randomSongs = null;
            Genres genres = null;

            Assert.DoesNotThrow(async () =>
                {
                    genres = await AdminSubsonicClient.GetGenresAsync();
                });

            int randomNumberForGenre = Random.Next(0, genres.Items.Count - 1);
            Genre randomGenre = genres.Items.ElementAt(randomNumberForGenre);

            Assert.DoesNotThrow(async () =>
                {
                    randomSongs = await AdminSubsonicClient.GetRandomSongsAsync(randomNumber, randomGenre.Name);
                });

            Assert.IsTrue(randomSongs.Songs.All(s => string.Compare(s.Genre, randomGenre.Name, StringComparison.OrdinalIgnoreCase) == 0));
        }

        [Test]
        public void GetMusicFoldersAsAdminUserOnSubsonic()
        {
            MusicFolders musicFolders = null;

            Assert.DoesNotThrow(async () =>
                {
                    musicFolders = await AdminSubsonicClient.GetMusicFoldersAsync();
                });

            Assert.IsTrue(musicFolders.Items.Any());
        }

        [Test]
        public void GetIndexesForAllMusicFoldersAsAdminUserOnSubsonic()
        {
            Indexes indexes = null;

            Assert.DoesNotThrow(async () =>
                {
                    indexes = await AdminSubsonicClient.GetIndexesAsync();
                });

            Assert.IsTrue(indexes.Items.Any());
        }

        [Test]
        public void GetIndexesForRandomMusicFolderAsAdminUserOnSubsonic()
        {
            MusicFolders musicFolders = null;

            Assert.DoesNotThrow(async () =>
                {
                    musicFolders = await AdminSubsonicClient.GetMusicFoldersAsync();
                });

            Assert.IsTrue(musicFolders.Items.Any());

            int randomMusicFolderNumber = Random.Next(0, musicFolders.Items.Count - 1);
            MusicFolder randomMusicFolder = musicFolders.Items.ElementAt(randomMusicFolderNumber);

            Indexes indexes = null;

            Assert.DoesNotThrow(async () =>
                {
                    indexes = await AdminSubsonicClient.GetIndexesAsync(randomMusicFolder.Id);
                });

            Assert.IsTrue(indexes.Items.Any());
        }

        [Test]
        public void GetIndexesForFutureDateAsAdminUserOnSubsonic()
        {
            Indexes indexes = null;

            long ifModifiedSince = DateTime.UtcNow.AddDays(1).ToUnixTimestampInMilliseconds();

            Assert.DoesNotThrow(async () =>
                {
                    indexes = await AdminSubsonicClient.GetIndexesAsync(ifModifiedSince: ifModifiedSince);
                });

            Assert.IsNull(indexes);
        }

        [Test]
        public void GetMusicDirectoryForRandomMusicFolderAndRandomIndexAsAdminUserOnSubsonic()
        {
            MusicFolders musicFolders = null;

            Assert.DoesNotThrow(async () =>
                {
                    musicFolders = await AdminSubsonicClient.GetMusicFoldersAsync();
                });

            Assert.IsTrue(musicFolders.Items.Any());

            int randomMusicFolderNumber = Random.Next(0, musicFolders.Items.Count - 1);
            MusicFolder randomMusicFolder = musicFolders.Items.ElementAt(randomMusicFolderNumber);

            Indexes indexes = null;

            Assert.DoesNotThrow(async () =>
                {
                    indexes = await AdminSubsonicClient.GetIndexesAsync(randomMusicFolder.Id);
                });

            Assert.IsTrue(indexes.Items.Any());

            int randomIndexNumber = Random.Next(0, indexes.Items.Count - 1);
            Index randomIndex = indexes.Items.ElementAt(randomIndexNumber);

            int randomArtistNumber = Random.Next(0, randomIndex.Artists.Count - 1);
            Artist randomArtist = randomIndex.Artists.ElementAt(randomArtistNumber);

            Directory musicDirectory = null;

            Assert.DoesNotThrow(async () =>
                {
                    musicDirectory = await AdminSubsonicClient.GetMusicDirectoryAsync(randomArtist.Id);
                });

            Assert.IsNotNullOrEmpty(musicDirectory.Id);
            Assert.IsNotNullOrEmpty(musicDirectory.Name);
            Assert.IsTrue(musicDirectory.Children.Any());
        }

        [Test]
        public void GetGenresAsAdminUserOnSubsonic()
        {
            Genres genres = null;

            Assert.DoesNotThrow(async () =>
                {
                    genres = await AdminSubsonicClient.GetGenresAsync();
                });

            Assert.IsTrue(genres.Items.Any());
        }

        [Test]
        public void GetArtistsAsAdminUserOnSubsonic()
        {
            ArtistsID3 artists = null;

            Assert.DoesNotThrow(async () =>
                {
                    artists = await AdminSubsonicClient.GetArtistsAsync();
                });

            Assert.IsTrue(artists.Indexes.Any());
        }

        [Test]
        public void GetRandomArtistAsAdminUserOnSubsonic()
        {
            ArtistsID3 artists = null;

            Assert.DoesNotThrow(async () =>
                {
                    artists = await AdminSubsonicClient.GetArtistsAsync();
                });

            Assert.IsTrue(artists.Indexes.Any());

            int randomArtistIndexNumber = Random.Next(0, artists.Indexes.Count - 1);
            IndexID3 randomArtistIndex = artists.Indexes.ElementAt(randomArtistIndexNumber);

            Assert.IsTrue(randomArtistIndex.Artists.Any());

            int randomArtistNumber = Random.Next(0, randomArtistIndex.Artists.Count - 1);
            ArtistID3 randomArtist = randomArtistIndex.Artists.ElementAt(randomArtistNumber);

            ArtistID3 artist = null;

            Assert.DoesNotThrow(async () =>
                {
                    artist = await AdminSubsonicClient.GetArtistAsync(randomArtist.Id);
                });

            Assert.IsNotNullOrEmpty(artist.Id);
            Assert.IsNotNullOrEmpty(artist.Name);
        }

        [Test]
        public void GetRandomArtistInfo2AsAdminUserOnSubsonic()
        {
            ArtistsID3 artists = null;

            Assert.DoesNotThrow(async () =>
                {
                    artists = await AdminSubsonicClient.GetArtistsAsync();
                });

            Assert.IsTrue(artists.Indexes.Any());

            int randomArtistIndexNumber = Random.Next(0, artists.Indexes.Count - 1);
            IndexID3 randomArtistIndex = artists.Indexes.ElementAt(randomArtistIndexNumber);

            Assert.IsTrue(randomArtistIndex.Artists.Any());

            int randomArtistNumber = Random.Next(0, randomArtistIndex.Artists.Count - 1);
            ArtistID3 randomArtist = randomArtistIndex.Artists.ElementAt(randomArtistNumber);

            ArtistID3 artist = null;

            Assert.DoesNotThrow(async () =>
                {
                    artist = await AdminSubsonicClient.GetArtistAsync(randomArtist.Id);
                });

            Assert.IsNotNullOrEmpty(artist.Id);
            Assert.IsNotNullOrEmpty(artist.Name);

            ArtistInfo2 artistInfo2 = null;

            Assert.DoesNotThrow(async () =>
                {
                    artistInfo2 = await AdminSubsonicClient.GetArtistInfo2Async(randomArtist.Id);
                });

            Assert.IsNotNull(artistInfo2);

            if (!string.IsNullOrWhiteSpace(artistInfo2.LastFmUrl))
            {
                Assert.IsNotNull(artistInfo2.Biography);
                Assert.IsNotNull(artistInfo2.MusicBrainzId);
                Assert.IsNotNull(artistInfo2.SmallImageUrl);
                Assert.IsNotNull(artistInfo2.MediumImageUrl);
                Assert.IsNotNull(artistInfo2.LargeImageUrl);
            }
        }

        [Test]
        public void GetRandomArtistInfoAsAdminUserOnSubsonic()
        {
            MusicFolders musicFolders = null;

            Assert.DoesNotThrow(async () =>
                {
                    musicFolders = await AdminSubsonicClient.GetMusicFoldersAsync();
                });

            Assert.IsTrue(musicFolders.Items.Any());

            int randomMusicFolderNumber = Random.Next(0, musicFolders.Items.Count - 1);
            MusicFolder randomMusicFolder = musicFolders.Items.ElementAt(randomMusicFolderNumber);

            Indexes indexes = null;

            Assert.DoesNotThrow(async () =>
                {
                    indexes = await AdminSubsonicClient.GetIndexesAsync(randomMusicFolder.Id);
                });

            Assert.IsTrue(indexes.Items.Any());

            int randomIndexNumber = Random.Next(0, indexes.Items.Count - 1);
            Index randomIndex = indexes.Items.ElementAt(randomIndexNumber);

            int randomArtistNumber = Random.Next(0, randomIndex.Artists.Count - 1);
            Artist randomArtist = randomIndex.Artists.ElementAt(randomArtistNumber);

            ArtistInfo artistInfo = null;

            Assert.DoesNotThrow(async () =>
                {
                    artistInfo = await AdminSubsonicClient.GetArtistInfoAsync(randomArtist.Id);
                });

            Assert.IsNotNull(artistInfo);

            if (!string.IsNullOrWhiteSpace(artistInfo.LastFmUrl))
            {
                Assert.IsNotNull(artistInfo.Biography);
                Assert.IsNotNull(artistInfo.MusicBrainzId);
                Assert.IsNotNull(artistInfo.SmallImageUrl);
                Assert.IsNotNull(artistInfo.MediumImageUrl);
                Assert.IsNotNull(artistInfo.LargeImageUrl);
            }
        }

        [Test]
        public void GetRandomAlbumListAsAdminUserOnSubsonic()
        {
            AlbumList albumList = null;

            Assert.DoesNotThrow(async () =>
                {
                    albumList = await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.Random);
                });

            Assert.IsNotNull(albumList);
            Assert.IsTrue(albumList.Albums.Any());
        }

        [Test]
        public void GetNewestAlbumListAsAdminUserOnSubsonic()
        {
            AlbumList albumList = null;

            Assert.DoesNotThrow(async () =>
                {
                    albumList = await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.Newest);
                });

            Assert.IsNotNull(albumList);
            Assert.IsTrue(albumList.Albums.Any());
        }

        [Test]
        public void GetHighestAlbumListAsAdminUserOnSubsonic()
        {
            AlbumList albumList = null;

            Assert.DoesNotThrow(async () =>
                {
                    albumList = await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.Highest);
                });

            Assert.IsNotNull(albumList);
            Assert.IsFalse(albumList.Albums.Any());
        }

        [Test]
        public void GetFrequentAlbumListAsAdminUserOnSubsonic()
        {
            AlbumList albumList = null;

            Assert.DoesNotThrow(async () =>
                {
                    albumList = await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.Frequent);
                });

            Assert.IsNotNull(albumList);
            Assert.IsTrue(albumList.Albums.Any());
        }

        [Test]
        public void GetRecentAlbumListAsAdminUserOnSubsonic()
        {
            AlbumList albumList = null;

            Assert.DoesNotThrow(async () =>
                {
                    albumList = await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.Recent);
                });

            Assert.IsNotNull(albumList);
            Assert.IsTrue(albumList.Albums.Any());
        }

        [Test]
        public void GetAlphabeticalByNameAlbumListAsAdminUserOnSubsonic()
        {
            AlbumList albumList = null;

            Assert.DoesNotThrow(async () =>
                {
                    albumList = await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.AlphabeticalByName);
                });

            Assert.IsNotNull(albumList);
            Assert.IsTrue(albumList.Albums.Any());
        }

        [Test]
        public void GetAlphabeticalByArtistAlbumListAsAdminUserOnSubsonic()
        {
            AlbumList albumList = null;

            Assert.DoesNotThrow(async () =>
                {
                    albumList = await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.AlphabeticalByArtist);
                });

            Assert.IsNotNull(albumList);
            Assert.IsTrue(albumList.Albums.Any());
        }

        [Test]
        public void GetStarredAlbumListAsAdminUserOnSubsonic()
        {
            AlbumList albumList = null;

            Assert.DoesNotThrow(async () =>
                {
                    albumList = await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.Starred);
                });

            Assert.IsNotNull(albumList);
            Assert.IsFalse(albumList.Albums.Any());
        }

        [Test]
        public void GetByYearAlbumListAsAdminUserOnSubsonic()
        {
            AlbumList albumList = null;

            int randomFromYear = Random.Next(1950, DateTime.Now.Year);
            int randomToYear = Random.Next(randomFromYear, DateTime.Now.Year);

            Assert.DoesNotThrow(async () =>
                {
                    albumList = await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.ByYear, fromYear: randomFromYear, toYear: randomToYear);
                });

            Assert.IsNotNull(albumList);
            Assert.IsTrue(albumList.Albums.Any());
        }

        [Test]
        public void GetByGenreAlbumListAsAdminUserOnSubsonic()
        {
            AlbumList albumList = null;

            Genres genres = null;

            Assert.DoesNotThrow(async () =>
                {
                    genres = await AdminSubsonicClient.GetGenresAsync();
                });

            int randomNumberForGenre = Random.Next(0, genres.Items.Count - 1);
            Genre randomGenre = genres.Items.ElementAt(randomNumberForGenre);

            Assert.DoesNotThrow(async () =>
                {
                    albumList = await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.ByGenre, genre: randomGenre.Name);
                });

            Assert.IsNotNull(albumList);
            Assert.IsTrue(albumList.Albums.Any());
        }

        [Test]
        public void GetByGenreAlbumListWithInvalidServerVersionAsAdminUserOnSubsonic()
        {
            Genres genres = null;

            Assert.DoesNotThrow(async () =>
                {
                    genres = await AdminSubsonicClient.GetGenresAsync();
                });

            int randomNumberForGenre = Random.Next(0, genres.Items.Count - 1);
            Genre randomGenre = genres.Items.ElementAt(randomNumberForGenre);

            Version previousApiVersion = AdminSubsonicServer.GetApiVersion();
            AdminSubsonicServer.SetApiVersion(SubsonicApiVersions.Version1_10_0);

            Assert.Throws<SubsonicInvalidApiException>(async () => await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.ByGenre, genre: randomGenre.Name));

            AdminSubsonicServer.SetApiVersion(previousApiVersion);
        }

        [Test]
        public void GetByYearAlbumListWithInvalidParametersAsAdminUserOnSubsonic()
        {
            Assert.Throws<SubsonicErrorException>(async () => await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.ByYear));
        }

        [Test]
        public void GenresActivityTest()
        {
            var genresActivity = new GenresActivity<Image>(AdminSubsonicClient);

            Genres genres = null;

            Assert.DoesNotThrow(async () =>
                {
                    genres = await genresActivity.GetResult();
                });

            Assert.IsTrue(genres.Items.Any());

            genres = null;

            Assert.DoesNotThrow(async () =>
                {
                    genres = await genresActivity.GetResult();
                });

            Assert.IsTrue(genres.Items.Any());
        }

        [Test]
        public void MusicFoldersActivityTest()
        {
            var activity = new MusicFoldersActivity<Image>(AdminSubsonicClient);

            MusicFolders musicFolders = null;

            Assert.DoesNotThrow(async () =>
                {
                    musicFolders = await activity.GetResult();
                });

            Assert.IsTrue(musicFolders.Items.Any());

            musicFolders = null;

            Assert.DoesNotThrow(async () =>
                {
                    musicFolders = await activity.GetResult();
                });

            Assert.IsTrue(musicFolders.Items.Any());
        }

        [Test]
        public void ScanMediaFoldersAsAdminUserOnSubsonic()
        {
            bool success = false;

            Assert.DoesNotThrow(async () => 
                {
                    success = await AdminSubsonicClient.ScanMediaFolders();
                });

            Assert.IsTrue(success);
        }

        [Test]
        public void CleanupMediaFoldersAsAdminUserOnSubsonic()
        {
            bool success = false;

            Assert.DoesNotThrow(async () =>
            {
                success = await AdminSubsonicClient.CleanupMediaFolders();
            });

            Assert.IsTrue(success);
        }

        [Test]
        public void GetCoverArtSizeAsAdminUserOnSubsonic()
        {
            AlbumList albumList = null;

            Assert.DoesNotThrow(async () =>
            {
                albumList = await AdminSubsonicClient.GetAlbumListAsync(AlbumListType.Random, 1);
            });

            Assert.IsNotNull(albumList);

            long artSize = 0;

            Assert.DoesNotThrow(async () =>
            {
                artSize = await AdminSubsonicClient.GetCoverArtSizeAsync(albumList.Albums.First().CoverArt);
            });
            
            Assert.GreaterOrEqual(artSize, 0);
        }
    }
}