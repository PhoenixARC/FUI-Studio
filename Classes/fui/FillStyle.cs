using System;
using System.Linq;

namespace FUI_Studio.Classes.fui
{
    public class FillStyle : fui.IFuiObject
    {
        public int Type;
        public RGBA Color;
        public int BitmapIndex;
        public Matrix matrix;

        public int GetByteSize()
        {
            return 0x24;
        }

        public void Parse(byte[] data)
        {
            if (data == null) throw new ArgumentNullException();
            if (data.Length != GetByteSize()) throw new ArgumentException();

            Type = BitConverter.ToInt32(data, 0);
            Color = new RGBA();
            Color.RGBa = BitConverter.ToUInt32(data, 4);
            BitmapIndex = BitConverter.ToInt32(data, 8);
            matrix = new Matrix();
            matrix.Parse(data.Skip(12).Take(matrix.GetByteSize()).ToArray());
        }

        public byte[] ToArray()
        {
            var arr = new byte[GetByteSize()];
            BitConverter.GetBytes(Type).CopyTo(arr, 0);
            BitConverter.GetBytes(Color.RGBa).CopyTo(arr, 4);
            BitConverter.GetBytes(BitmapIndex).CopyTo(arr, 8);
            matrix.ToArray().CopyTo(arr, 12);
            return arr;
        }

        public override string ToString()
        {
            return $"Type: {Type}\n" +
                $"Color: #{Color.RGBa.ToString("X8")}\n" +
                $"Bitmap Index: {BitmapIndex}\n" +
                $"Matrix: \n{matrix}";
        }
    }
}
