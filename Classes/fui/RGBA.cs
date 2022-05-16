using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUI_Studio.Classes.fui
{
    public class RGBA : IFuiObject
    {
        public uint color;
        public int GetByteSize()
        {
            return 4;
        }
        public void Parse(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length < GetByteSize()) throw new ArgumentException();
            color = BitConverter.ToUInt32(data, 0);
        }

        public byte[] ToArray()
        {
            var arr = new byte[GetByteSize()];
            BitConverter.GetBytes(color).CopyTo(arr, 0);
            return arr;
        }

        public Color GetColor()
        {
            int argb_color = (int)((color >> 8) | (color << 24 & 0xff000000));
            return Color.FromArgb(argb_color);
        }

        public override string ToString()
        {
            return color.ToString("X8");
        }
    }
}
