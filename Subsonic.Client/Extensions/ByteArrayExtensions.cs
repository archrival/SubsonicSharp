using System;
using System.Text;

namespace Subsonic.Client.Extensions
{
    public static class ByteArrayExtensions
    {
        public static string GetString(this byte[] bytes)
        {
            var chars = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public static string ToHexString(this byte[] ba)
        {
            var hex = new StringBuilder(ba.Length * 2);

            foreach (var b in ba)
                hex.AppendFormat("{0:x2}", b);

            return hex.ToString();
        }
    }
}