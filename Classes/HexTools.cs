using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using System.Linq;


namespace FUI_Studio.Classes
{
    static class HexTools
    {
        public static byte[] StringToByteArrayFastest(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }

        public static int GetHexVal(char hex)
        {
            int val = (int)hex;
            //For uppercase A-F letters:
            //return val - (val < 58 ? 48 : 55);
            //For lowercase a-f letters:
            //return val - (val < 58 ? 48 : 87);
            //Or the two combined, but a bit slower:
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }

        public static string ByteArrayToHexString(string input)
        {
            Console.WriteLine(input);
            return System.BitConverter.ToString(File.ReadAllBytes(input)).Replace('-', ' ');
        }

        public static string trueByteArrayToHexString(byte[] input)
        {
            return System.BitConverter.ToString(input).Replace('-', ' ');
        }


        public static string newstring(string St, string start, string end)
        {
            //MessageBox.Show(St);
            int pFrom = St.IndexOf(start) + start.Length;
            int pTo = St.LastIndexOf(end);

            string result = St.Substring(pFrom, pTo - pFrom);
            return result;
        }

        public static bool isAlphaNumeric(string strToCheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z0-9,_-]*$");
            return rg.IsMatch(strToCheck);
        }

        public static List<int> IndexOfSequence(this byte[] buffer, byte[] pattern, int startIndex)
        {
            List<int> positions = new List<int>();
            int i = Array.IndexOf<byte>(buffer, pattern[0], startIndex);
            while (i >= 0 && i <= buffer.Length - pattern.Length)
            {
                byte[] segment = new byte[pattern.Length];
                Buffer.BlockCopy(buffer, i, segment, 0, pattern.Length);
                if (buffer[i + 3] == 00 && buffer[i + 2] != 00)
                    if (segment.SequenceEqual<byte>(pattern))
                        positions.Add(i);
                i = Array.IndexOf<byte>(buffer, pattern[0], i + 1);
            }
            return positions;
        }

    }
}
