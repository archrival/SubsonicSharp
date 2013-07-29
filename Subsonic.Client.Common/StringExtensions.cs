using System;
using System.Globalization;
using System.Text;

namespace Subsonic.Client.Common
{
    public static class StringExtensions
    {
        /// <summary>
        /// Covert ASCII string to Hexadecimal string.
        /// </summary>
        /// <param name="text">String to convert.</param>
        /// <returns>string</returns>
        public static string ToHex(this string text)
        {
            var hexString = new StringBuilder();

            if (!string.IsNullOrEmpty(text))
                foreach (char character in text)
                    hexString.Append(Convert.ToInt32(character).ToString("x", CultureInfo.InvariantCulture));

            return hexString.ToString();
        }
    }
}
