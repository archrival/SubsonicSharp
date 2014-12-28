using System;
using System.Drawing;
using NUnit.Framework;
using Subsonic.Client.Exceptions;
using Subsonic.Client.Windows;
using Subsonic.Common.Classes;
using Subsonic.Common.Interfaces;
using System.Linq;

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

        public ISubsonicClient<Image> AdminSubsonicClient;
        public ISubsonicClient<Image> DownloadSubsonicClient;
        public ISubsonicClient<Image> NoPlaySubsonicClient;
        public ISubsonicClient<Image> PlaySubsonicClient;
        public ISubsonicClient<Image> NonexistentSubsonicClient;

        [TestFixtureSetUp]
        public void Setup()
        {
            AdminSubsonicClient = new SubsonicClientWindows(SubsonicServer, AdminUser, Password, ClientName);
            DownloadSubsonicClient = new SubsonicClientWindows(SubsonicServer, DownloadUser, Password, ClientName);
            NoPlaySubsonicClient = new SubsonicClientWindows(SubsonicServer, NoPlayUser, Password, ClientName);
            PlaySubsonicClient = new SubsonicClientWindows(SubsonicServer, PlayUser, Password, ClientName);
            NonexistentSubsonicClient = new SubsonicClientWindows(NonexistentServer, AdminUser, Password, ClientName);
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
        }

        [Test]
        public void PingTestOnNonexistentServer()
        {
            Assert.Throws<SubsonicApiException>(async () => await NonexistentSubsonicClient.PingAsync());
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
        public void CreateAdminUserAsPlayUserOnSubsonic()
        {
            Assert.Throws<SubsonicErrorException>(async () => await PlaySubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail, false, true));
        }

        [Test]
        public void CreateAdminUserAsNoPlayUserOnSubsonic()
        {
            Assert.Throws<SubsonicErrorException>(async () => await NoPlaySubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail, false, true));
        }

        [Test]
        public void CreateAdminUserAsDownloadUserOnSubsonic()
        {
            Assert.Throws<SubsonicErrorException>(async () => await DownloadSubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail, false, true));
        }

        [Test]
        public void CreateUserAsPlayUserOnSubsonic()
        {
            Assert.Throws<SubsonicErrorException>(async () => await PlaySubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail));
        }

        [Test]
        public void CreateUserAsNoPlayUserOnSubsonic()
        {
            Assert.Throws<SubsonicErrorException>(async () => await NoPlaySubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail));
        }

        [Test]
        public void CreateUserAsDownloadUserOnSubsonic()
        {
            Assert.Throws<SubsonicErrorException>(async () => await DownloadSubsonicClient.CreateUserAsync(UserToCreate, Password, UserToCreateEmail));
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

            Assert.That(chatMessages.ChatMessage.Any(c => c.Message == chatMessage && c.Username == PlayUser));
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

            Assert.That(chatMessages.ChatMessage.Any(c => c.Message == chatMessage && c.Username == NoPlayUser));
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

            Assert.That(chatMessages.ChatMessage.Any(c => c.Message == chatMessage && c.Username == AdminUser));
        }
    }
}