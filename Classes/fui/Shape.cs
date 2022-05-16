using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUI_Studio.Classes.fui
{
    public class Shape : IFuiObject
    {
        private int UnknownValue1; // prolly used as symbol index earlier
        public int ComponentIndex;
        public int ComponentCount;
        public Rect Rectangle;

        public int GetByteSize()
        {
            return 0x1c;
        }

        public void Parse(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length != GetByteSize()) throw new ArgumentException();
            UnknownValue1 = BitConverter.ToInt32(data, 0);
            ComponentIndex = BitConverter.ToInt32(data, 4);
            ComponentCount = BitConverter.ToInt32(data, 8);
            Rectangle = new Rect();
            Rectangle.Parse(data.Skip(12).ToArray());
        }

        public byte[] ToArray()
        {
            var arr = new byte[GetByteSize()];
            BitConverter.GetBytes(UnknownValue1).CopyTo(arr, 0);
            BitConverter.GetBytes(ComponentIndex).CopyTo(arr, 4);
            BitConverter.GetBytes(ComponentCount).CopyTo(arr, 8);
            Rectangle.ToArray().CopyTo(arr, 12);
            return arr;
        }

        public SizeF GetSizeF()
        {
            return Rectangle.GetSizeF();
        }
        
        public Size GetSize()
        {
            return Rectangle.GetSize();
        }

        public override string ToString()
        {
            return $"Unknow 0x00: {UnknownValue1}\n" +
                $"Component Index: {ComponentIndex}\n" +
                $"Component Count: {ComponentCount}\n" +
                $"Size: {Rectangle}";
        }
    }
}
