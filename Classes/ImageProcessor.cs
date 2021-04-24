using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace FUI_Studio.Classes
{
    public class ImageProcessor
    {
        public static void extractImage(string file, List<string> OutputList) 
        {
            string data = HexTools.ByteArrayToHexString(file);
            foreach (string image in data.Split(new[] { "FF D8 FF E0", "89 50 4E 47" }, StringSplitOptions.None))
            {
                if (image.StartsWith(" 0D 0A 1A 0A")) //is PNG
                {
                    OutputList.Add("89 50 4E 47" + image);
                }

                if (image.StartsWith(" 00 10 4A")) //is JPG
                {
                    OutputList.Add("FF D8 FF E0" + image);
                }
            }
        }
        public static void ReverseColorRB(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            int stride = data.Stride;
            IntPtr pixelData = data.Scan0;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    IntPtr pixelOffset = pixelData + 4 * x + stride * y;
                    byte[] color = new byte[4];
                    Marshal.Copy(pixelOffset, color, 0, 4);

                    byte red = color[0];
                    byte blue = color[2];

                    color[0] = blue;
                    color[2] = red;

                    Marshal.Copy(color, 0, pixelOffset, 4);
                }
            }

            bitmap.UnlockBits(data);
        }

    }
}
