using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace FUI_Studio.Classes
{
    public class ImageProcessor
    {
        public static Bitmap ReverseColorRB(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, width, height),
                ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            int stride = data.Stride;
            IntPtr pixelData = data.Scan0;

            byte[] color = new byte[4];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    IntPtr pixelOffset = pixelData + 4 * x + stride * y;
                    Marshal.Copy(pixelOffset, color, 0, 4);

                    byte tmp = color[0];
                    color[0] = color[2];
                    color[2] = tmp;

                    Marshal.Copy(color, 0, pixelOffset, 4);
                }
            }
            bitmap.UnlockBits(data);
            return bitmap;
        }
    }
}
