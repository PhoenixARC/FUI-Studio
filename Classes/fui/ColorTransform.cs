using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUI_Studio.Classes.fui
{
    public class ColorTransform : IFuiObject
    {
        public float RedMultTerm;
        public float GreenMultTerm;
        public float BlueMultTerm;
        public float AlphaMultTerm;
        public float RedAddTerm;
        public float GreenAddTerm;
        public float BlueAddTerm;
        public float AlphaAddTerm;

        public int GetByteSize()
        {
            return 0x20;
        }

        public void Parse(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length < GetByteSize()) throw new ArgumentException("data");
            RedMultTerm = BitConverter.ToSingle(data, 0);
            GreenMultTerm = BitConverter.ToSingle(data, 4);
            BlueMultTerm = BitConverter.ToSingle(data, 8);
            AlphaMultTerm = BitConverter.ToSingle(data, 12);
            RedAddTerm = BitConverter.ToSingle(data, 16);
            GreenAddTerm = BitConverter.ToSingle(data, 20);
            BlueAddTerm = BitConverter.ToSingle(data, 24);
            AlphaAddTerm = BitConverter.ToSingle(data, 28);
        }

        public byte[] ToArray()
        {
            var arr = new byte[GetByteSize()];
            BitConverter.GetBytes(RedMultTerm).CopyTo(arr, 0);
            BitConverter.GetBytes(GreenMultTerm).CopyTo(arr, 4);
            BitConverter.GetBytes(BlueMultTerm).CopyTo(arr, 8);
            BitConverter.GetBytes(AlphaMultTerm).CopyTo(arr, 12);
            BitConverter.GetBytes(RedAddTerm).CopyTo(arr, 16);
            BitConverter.GetBytes(GreenAddTerm).CopyTo(arr, 20);
            BitConverter.GetBytes(BlueAddTerm).CopyTo(arr, 24);
            BitConverter.GetBytes(AlphaAddTerm).CopyTo(arr, 28);
            return arr;
        }


        public override string ToString()
        {
            return $"Multiple Terms:\n" +
                $"\tR: {RedMultTerm}\n" +
                $"\tG: {GreenMultTerm}\n" +
                $"\tB: {BlueMultTerm}\n" +
                $"\tA: {AlphaMultTerm}\n" +
                $"Addition Terms:\n" +
                $"\tR: {RedAddTerm}\n" +
                $"\tG: {GreenAddTerm}\n" +
                $"\tB: {BlueAddTerm}\n" +
                $"\tA: {AlphaAddTerm}";
        }
    }
}
