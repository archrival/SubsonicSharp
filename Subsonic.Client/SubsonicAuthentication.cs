using System.Text;
using System;

namespace Subsonic.Client
{
    public class SubsonicAuthentication : ISubsonicAuthentication
    {
        string Password { get; set; }
        int SaltComplexity { get; set; }

        public SubsonicAuthentication(string password, int saltComplexity = 20)
        {
            Password = password;
            SaltComplexity = saltComplexity;
        }

        public void SetPassword(string password)
        {
            Password = password;
        }

        public void SetSaltComplexity(int saltComplexity)
        {
            SaltComplexity = saltComplexity;
        }

        public SubsonicToken GetToken()
        {
            SubsonicToken subsonicToken = new SubsonicToken {Salt = GetUniqueString(SaltComplexity)};

            string tokenSource = string.Format("{0}{1}", Password, subsonicToken.Salt);
            subsonicToken.Token = MD5.GetMd5Sum(tokenSource);

            return subsonicToken;
        }
            
        static string GetUniqueString(int size)
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

            StringBuilder result = new StringBuilder(size);

            Random random = new Random();

            for (int i = 1; i < size; i++)
                result.Append(chars[random.Next(chars.Length)]);
                                
            return result.ToString();
        }
    }
}

