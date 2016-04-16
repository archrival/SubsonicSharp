using System.Text;

namespace Subsonic.Client
{
    public class MD5
    {
        private static readonly uint[] T =
        {
            0xd76aa478, 0xe8c7b756, 0x242070db, 0xc1bdceee,
            0xf57c0faf, 0x4787c62a, 0xa8304613, 0xfd469501,
            0x698098d8, 0x8b44f7af, 0xffff5bb1, 0x895cd7be,
            0x6b901122, 0xfd987193, 0xa679438e, 0x49b40821,
            0xf61e2562, 0xc040b340, 0x265e5a51, 0xe9b6c7aa,
            0xd62f105d, 0x2441453, 0xd8a1e681, 0xe7d3fbc8,
            0x21e1cde6, 0xc33707d6, 0xf4d50d87, 0x455a14ed,
            0xa9e3e905, 0xfcefa3f8, 0x676f02d9, 0x8d2a4c8a,
            0xfffa3942, 0x8771f681, 0x6d9d6122, 0xfde5380c,
            0xa4beea44, 0x4bdecfa9, 0xf6bb4b60, 0xbebfbc70,
            0x289b7ec6, 0xeaa127fa, 0xd4ef3085, 0x4881d05,
            0xd9d4d039, 0xe6db99e5, 0x1fa27cf8, 0xc4ac5665,
            0xf4292244, 0x432aff97, 0xab9423a7, 0xfc93a039,
            0x655b59c3, 0x8f0ccc92, 0xffeff47d, 0x85845dd1,
            0x6fa87e4f, 0xfe2ce6e0, 0xa3014314, 0x4e0811a1,
            0xf7537e82, 0xbd3af235, 0x2ad7d2bb, 0xeb86d391
        };

        private readonly uint[] _x = new uint[16];
        private byte[] _byteInput;

        public static string GetMd5Sum(string value)
        {
            byte[] bytes = new byte[value.Length];

            for (int i = 0; i < value.Length; i++)
                bytes[i] = (byte)value[i];

            return GetMd5Sum(bytes);
        }

        public static string GetMd5Sum(byte[] value)
        {
            MD5 md5 = new MD5 { _byteInput = new byte[value.Length] };

            for (int i = 0; i < value.Length; i++)
                md5._byteInput[i] = value[i];

            Digest digest = md5.CalculateMd5Value();

            return digest.ToString();
        }

        private Digest CalculateMd5Value()
        {
            uint n;
            Digest dg = new Digest();

            byte[] bMsg = CreatePaddedBuffer();

            n = (uint)(bMsg.Length * 8) / 32;

            for (uint i = 0; i < n / 16; i++)
            {
                CopyBlock(bMsg, i);
                PerformTransformation(ref dg.A, ref dg.B, ref dg.C, ref dg.D);
            }

            return dg;
        }

        private void TransF(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
        {
            a = b + RotateLeft((a + ((b & c) | (~(b) & d)) + _x[k] + T[i - 1]), s);
        }

        private void TransG(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
        {
            a = b + RotateLeft((a + ((b & d) | (c & ~d)) + _x[k] + T[i - 1]), s);
        }

        private void TransH(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
        {
            a = b + RotateLeft((a + (b ^ c ^ d) + _x[k] + T[i - 1]), s);
        }

        private void TransI(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
        {
            a = b + RotateLeft((a + (c ^ (b | ~d)) + _x[k] + T[i - 1]), s);
        }

        private void PerformTransformation(ref uint a, ref uint b, ref uint c, ref uint d)
        {
            uint aa, bb, cc, dd;

            aa = a;
            bb = b;
            cc = c;
            dd = d;

            TransF(ref a, b, c, d, 0, 7, 1);
            TransF(ref d, a, b, c, 1, 12, 2);
            TransF(ref c, d, a, b, 2, 17, 3);
            TransF(ref b, c, d, a, 3, 22, 4);
            TransF(ref a, b, c, d, 4, 7, 5);
            TransF(ref d, a, b, c, 5, 12, 6);
            TransF(ref c, d, a, b, 6, 17, 7);
            TransF(ref b, c, d, a, 7, 22, 8);
            TransF(ref a, b, c, d, 8, 7, 9);
            TransF(ref d, a, b, c, 9, 12, 10);
            TransF(ref c, d, a, b, 10, 17, 11);
            TransF(ref b, c, d, a, 11, 22, 12);
            TransF(ref a, b, c, d, 12, 7, 13);
            TransF(ref d, a, b, c, 13, 12, 14);
            TransF(ref c, d, a, b, 14, 17, 15);
            TransF(ref b, c, d, a, 15, 22, 16);
            TransG(ref a, b, c, d, 1, 5, 17);
            TransG(ref d, a, b, c, 6, 9, 18);
            TransG(ref c, d, a, b, 11, 14, 19);
            TransG(ref b, c, d, a, 0, 20, 20);
            TransG(ref a, b, c, d, 5, 5, 21);
            TransG(ref d, a, b, c, 10, 9, 22);
            TransG(ref c, d, a, b, 15, 14, 23);
            TransG(ref b, c, d, a, 4, 20, 24);
            TransG(ref a, b, c, d, 9, 5, 25);
            TransG(ref d, a, b, c, 14, 9, 26);
            TransG(ref c, d, a, b, 3, 14, 27);
            TransG(ref b, c, d, a, 8, 20, 28);
            TransG(ref a, b, c, d, 13, 5, 29);
            TransG(ref d, a, b, c, 2, 9, 30);
            TransG(ref c, d, a, b, 7, 14, 31);
            TransG(ref b, c, d, a, 12, 20, 32);
            TransH(ref a, b, c, d, 5, 4, 33);
            TransH(ref d, a, b, c, 8, 11, 34);
            TransH(ref c, d, a, b, 11, 16, 35);
            TransH(ref b, c, d, a, 14, 23, 36);
            TransH(ref a, b, c, d, 1, 4, 37);
            TransH(ref d, a, b, c, 4, 11, 38);
            TransH(ref c, d, a, b, 7, 16, 39);
            TransH(ref b, c, d, a, 10, 23, 40);
            TransH(ref a, b, c, d, 13, 4, 41);
            TransH(ref d, a, b, c, 0, 11, 42);
            TransH(ref c, d, a, b, 3, 16, 43);
            TransH(ref b, c, d, a, 6, 23, 44);
            TransH(ref a, b, c, d, 9, 4, 45);
            TransH(ref d, a, b, c, 12, 11, 46);
            TransH(ref c, d, a, b, 15, 16, 47);
            TransH(ref b, c, d, a, 2, 23, 48);
            TransI(ref a, b, c, d, 0, 6, 49);
            TransI(ref d, a, b, c, 7, 10, 50);
            TransI(ref c, d, a, b, 14, 15, 51);
            TransI(ref b, c, d, a, 5, 21, 52);
            TransI(ref a, b, c, d, 12, 6, 53);
            TransI(ref d, a, b, c, 3, 10, 54);
            TransI(ref c, d, a, b, 10, 15, 55);
            TransI(ref b, c, d, a, 1, 21, 56);
            TransI(ref a, b, c, d, 8, 6, 57);
            TransI(ref d, a, b, c, 15, 10, 58);
            TransI(ref c, d, a, b, 6, 15, 59);
            TransI(ref b, c, d, a, 13, 21, 60);
            TransI(ref a, b, c, d, 4, 6, 61);
            TransI(ref d, a, b, c, 11, 10, 62);
            TransI(ref c, d, a, b, 2, 15, 63);
            TransI(ref b, c, d, a, 9, 21, 64);

            a = a + aa;
            b = b + bb;
            c = c + cc;
            d = d + dd;
        }

        private byte[] CreatePaddedBuffer()
        {
            uint pad;
            ulong sizeMsg;
            uint sizeMsgBuff;
            int temp = (448 - ((_byteInput.Length * 8) % 512));

            pad = (uint)((temp + 512) % 512);

            if (pad == 0)
                pad = 512;

            sizeMsgBuff = (uint)((_byteInput.Length) + (pad / 8) + 8);
            sizeMsg = (ulong)_byteInput.Length * 8;
            byte[] bMsg = new byte[sizeMsgBuff];

            for (int i = 0; i < _byteInput.Length; i++)
                bMsg[i] = _byteInput[i];

            bMsg[_byteInput.Length] |= 0x80;

            for (int i = 8; i > 0; i--)
                bMsg[sizeMsgBuff - i] = (byte)(sizeMsg >> ((8 - i) * 8) & 0x00000000000000ff);

            return bMsg;
        }

        private void CopyBlock(byte[] bMsg, uint block)
        {
            block = block << 6;

            for (uint j = 0; j < 61; j += 4)
            {
                _x[j >> 2] = (((uint)bMsg[block + (j + 3)]) << 24) |
                (((uint)bMsg[block + (j + 2)]) << 16) |
                (((uint)bMsg[block + (j + 1)]) << 8) |
                bMsg[block + (j)];
            }
        }

        private static uint RotateLeft(uint uiNumber, ushort shift)
        {
            return ((uiNumber >> 32 - shift) | (uiNumber << shift));
        }
    }

    internal class Digest
    {
        public uint A;
        public uint B;
        public uint C;
        public uint D;

        public Digest()
        {
            A = 0x67452301;
            B = 0xEFCDAB89;
            C = 0x98BADCFE;
            D = 0X10325476;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(ReverseByte(A).ToString("x8"));
            sb.Append(ReverseByte(B).ToString("x8"));
            sb.Append(ReverseByte(C).ToString("x8"));
            sb.Append(ReverseByte(D).ToString("x8"));

            return sb.ToString();
        }

        private static uint ReverseByte(uint uiNumber)
        {
            return (((uiNumber & 0x000000ff) << 24) |
            (uiNumber >> 24) |
            ((uiNumber & 0x00ff0000) >> 8) |
            ((uiNumber & 0x0000ff00) << 8));
        }
    }
}