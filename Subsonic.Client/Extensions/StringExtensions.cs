using System;
using System.Globalization;
using System.Text;

namespace Subsonic.Client.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Covert ASCII string to Hexadecimal string.
        /// </summary>
        /// <param name="text">String to convert.</param>
        /// <returns>string</returns>
        public static string ToHexString(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            StringBuilder hexString = new StringBuilder();

            foreach (char character in text)
                hexString.Append(Convert.ToInt32(character).ToString("x", CultureInfo.InvariantCulture));

            return hexString.ToString();
        }

        public static byte[] GetBytes(this string str)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            byte[] bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static int GetHashCode(this string str, int hash, int hashFactor)
        {
            if (str == null)
                return hash;

            return (hash * hashFactor) + str.GetHashCode();
        }
    }
}
