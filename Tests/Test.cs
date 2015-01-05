using System;
using System.Drawing;
using System.Linq;
using NUnit.Framework;
using Subsonic.Client.Exceptions;
using Subsonic.Client.Extensions;
using Subsonic.Common.Classes;
using Subsonic.Common.Enums;
using Subsonic.Common.Interfaces;

namespace Subsonic.Client.Windows.Tests
{
    [TestFixture()]
    public class Test
    {
        public static readonly Uri SubsonicServer = new Uri("http://localhost:8080/subsonic/");
        public static readonly Uri MadsonicServer = new Uri("http://localhost:8080/madsonic/");
        public static readonly Uri NonexistentServer = new Uri("http://localhost:8080/ultrasonic/");
        public static readonly string AdminUser = "test_admin";
        public static readonly string DownloadUser = "test_download";
        public static readonly string NoPlayUser = "test_noplay";
        public static readonly string PlayUser = "test_play";
        public static readonly string Password = "password";
        public static readonly string UserToCreate = "test_createduser";
        public static readonly string UserToCreateEmail = "test_createduser@localhost";
        public static readonly string ClientName = "Subsonic.Client.Windows.Tests";

        public static readonly int MaxRandomSongCount = 500;
        public static readonly int MinRandomSongCount = 1;
        public static readonly int InvalidRandomSongCount = 501;

        public ISubsonicClient<Image> AdminSubsonicClient;
        public ISubsonicClient<Image> DownloadSubsonicClient;
        public ISubsonicClient<Image> NoPlaySubsonicClient;
        public ISubsonicClient<Image> PlaySubsonicClient;
        public ISubsonicClient<Image> NonexistentSubsonicClient;
        public SubsonicServerWindows AdminSubsonicServer;
        public SubsonicServerWindows DownloadSubsonicServer;
        public SubsonicServerWindows NoPlaySubsonicServer;
        public SubsonicServerWindows PlaySubsonicServer;
        public SubsonicServerWindows NonexistentSubsonicServer;
        public Random Random;

        [TestFixtureSetUp]
        public void Setup()
        {
            AdminSubsonicServer = new SubsonicServerWindows(SubsonicServer, AdminUser, Password, ClientName);
            DownloadSubsonicServer = new SubsonicServerWindows(SubsonicServer, DownloadUser, Password, ClientName);
            NoPlaySubsonicServer = new SubsonicServerWindows(SubsonicServer, NoPlayUser, Password, ClientName);
            PlaySubsonicServer = new SubsonicServerWindows(SubsonicServer, PlayUser, Password, ClientName);
            NonexistentSubsonicServer = new SubsonicServerWindows(NonexistentServer, AdminUser, Password, ClientName);

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
            Assert.IsTrue(AdminSubsonicServer.GetApiVersion() >= Subsonic.Common.SubsonicApiVersions.Version1_0_0);
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
            Assert.IsTrue(DownloadSubsonicServer.GetApiVersion() >= Subsonic.Common.SubsonicApiVersions.Version1_0_0);
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
            Assert.IsTrue(NoPlaySubsonicServer.GetApiVersion() >= Subsonic.Common.SubsonicApiVersions.Version1_0_0);
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
            Assert.IsTrue(PlaySubsonicServer.GetApiVersion() >= Subsonic.Common.SubsonicApiVersions.Version1_0_0);
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

            string chatMessage = DateTime.UtcNow.Ticks.ToString();

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

            string chatMessage = DateTime.UtcNow.Ticks.ToString();

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

            string chatMessage = DateTime.UtcNow.Ticks.ToString();

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
    }
}