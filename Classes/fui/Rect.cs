using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUI_Studio.Classes.fui
{
    public class Rect : IFuiObject
    {
        public float minX;
        public float maxX;
        public float minY;
        public float maxY;

        public int GetByteSize()
        {
            return 0x10;
        }

        public void Parse(byte[] data)
        {
            if (data == null) throw new NullReferenceException();
            if (data.Length != GetByteSize()) throw new ArgumentException("Rect data");
            minX = BitConverter.ToSingle(data, 0);
            maxX = BitConverter.ToSingle(data, 4);
            minY = BitConverter.ToSingle(data, 8);
            maxY = BitConverter.ToSingle(data, 12);
        }

        public byte[] ToArray()
        {
            var arr = new byte[GetByteSize()];
            BitConverter.GetBytes(minX).CopyTo(arr, 0);
            BitConverter.GetBytes(maxX).CopyTo(arr, 4);
            BitConverter.GetBytes(minY).CopyTo(arr, 8);
            BitConverter.GetBytes(maxY).CopyTo(arr, 12);
            return arr;
        }

        public SizeF GetSizeF()
        {
            return new SizeF(maxX - minX, maxY - minY);
        }
        public Size GetSize()
        {
            return new Size((int)(maxX - minX), (int)(maxY - minY));
        }

        public override string ToString()
        {
            return $"{maxX - minX} x {maxY - minY}";
        }
    }
}
