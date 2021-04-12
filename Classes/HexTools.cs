using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FUI_Studio.Classes
{
    class HexTools
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


        public static string newstring(string St, string start, string end)
        {
            //MessageBox.Show(St);
            int pFrom = St.IndexOf(start) + start.Length;
            int pTo = St.LastIndexOf(end);

            string result = St.Substring(pFrom, pTo - pFrom);
            return result;
        }

    }
}
