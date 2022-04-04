using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUI_Studio.Classes.fui
{
    public class Vert : IFuiObject
    {
        public float x;
        public float y;

        public int GetByteSize()
        {
            return 0x8;
        }

        public void Parse(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length != GetByteSize()) throw new ArgumentException();
            x = BitConverter.ToSingle(data, 0);
            y = BitConverter.ToSingle(data, 4);
        }

        public byte[] ToArray()
        {
            var arr = new byte[GetByteSize()];
            BitConverter.GetBytes(x).CopyTo(arr, 0);
            BitConverter.GetBytes(y).CopyTo(arr, 4);
            return arr;
        }

        public override string ToString()
        {
            return $"X: {x}\nY: {y}";
        }
    }
}
