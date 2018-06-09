using Subsonic.Client.Interfaces;
using System;
using System.Text;

namespace Subsonic.Client
{
    public class SubsonicAuthentication : ISubsonicAuthentication
    {
        public SubsonicAuthentication(string password, int saltComplexity = 20)
        {
            Password = password;
            SaltComplexity = saltComplexity;
        }

        private string Password { get; set; }
        private int SaltComplexity { get; set; }

        public SubsonicToken GetToken()
        {
            var subsonicToken = new SubsonicToken { Salt = GetUniqueString(SaltComplexity) };

            var tokenSource = $"{Password}{subsonicToken.Salt}";
            subsonicToken.Token = MD5.GetMd5Sum(tokenSource);

            return subsonicToken;
        }

        public void SetPassword(string password)
        {
            Password = password;
        }

        public void SetSaltComplexity(int saltComplexity)
        {
            SaltComplexity = saltComplexity;
        }

        private static string GetUniqueString(int size)
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

            var result = new StringBuilder(size);

            var random = new Random();

            for (var i = 1; i < size; i++)
                result.Append(chars[random.Next(chars.Length)]);

            return result.ToString();
        }
    }
}