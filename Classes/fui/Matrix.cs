using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUI_Studio.Classes.fui
{
    public class Matrix : IFuiObject
    {
        public float ScaleX;
        public float ScaleY;
        public float RotSkew0;
        public float RotSkew1;
        public float TranslateX;
        public float TranslateY;

        public int GetByteSize()
        {
            return 0x18;
        }

        public void Parse(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length < GetByteSize()) throw new ArgumentException("data");
            ScaleX = BitConverter.ToSingle(data, 0);
            ScaleY = BitConverter.ToSingle(data, 4);
            RotSkew0 = BitConverter.ToSingle(data, 8);
            RotSkew1 = BitConverter.ToSingle(data, 12);
            TranslateX = BitConverter.ToSingle(data, 16);
            TranslateY = BitConverter.ToSingle(data, 20);
        }

        public byte[] ToArray()
        {
            var arr = new byte[GetByteSize()];
            BitConverter.GetBytes(ScaleX).CopyTo(arr, 0);
            BitConverter.GetBytes(ScaleY).CopyTo(arr, 4);
            BitConverter.GetBytes(RotSkew0).CopyTo(arr, 8);
            BitConverter.GetBytes(RotSkew1).CopyTo(arr, 12);
            BitConverter.GetBytes(TranslateX).CopyTo(arr, 16);
            BitConverter.GetBytes(TranslateY).CopyTo(arr, 20);
            return arr;
        }

        public override string ToString()
        {
            return $"Scale X: {ScaleX}\n" +
                $"Scale Y: {ScaleY}\n" +
                $"Rotation Skew 0: {RotSkew0}\n" +
                $"Rotation Skew 1: {RotSkew1}\n" +
                $"Translate X: {TranslateX}\n" +
                $"Translate Y: {TranslateY}";
        }
    }
}
