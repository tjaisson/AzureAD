using System;
using System.Text;

namespace GuntCore
{
    internal static class GUNTEncrypt
    {
        private static readonly byte[] key = new byte[] { 0xa5, 0x7c, 0xe4, 0x84, 0x5a, 0xd1, 0x9c };

        public static string Encode(string data)
        {
            if (data == null || data == "") return "";
            byte[] dataB = Encoding.GetEncoding(1252).GetBytes(data);
            performXOR(ref dataB);
            return System.Convert.ToBase64String(dataB);
        }

        public static string Decode(string data)
        {
            if (data == null || data == "") return "";
            byte[] dataB;
            try
            {
                dataB = System.Convert.FromBase64String(data);
            }
            catch (FormatException)
            {
                return "";                
            }
            performXOR(ref dataB);
            return Encoding.GetEncoding(1252).GetString(dataB);
        }

        private static void performXOR(ref byte[] dataB)
        {
            int keypos = 0;
            for (int datapos = 0; datapos < dataB.Length; datapos++)
            {
                dataB[datapos] = (byte)(dataB[datapos] ^ key[keypos++]);
                if (keypos >= key.Length)
                    keypos = 0;
            }
        }

    }
}
