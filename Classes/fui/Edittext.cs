using System;
using System.Linq;
using System.Text;

namespace FUI_Studio.Classes.fui
{
    public class Edittext : fui.IFuiObject
    {
        public enum eFuiAlignment : int
        {
            LEFT = 0,
            RIGHT = 1,
            CENTER = 2,
        }

        public int Unknown1; // likely old symbol link index
        public Rect size;
        public int fontId;
        public float fontScale;
        public RGBA Color;
        public eFuiAlignment alignment;

        public float unk_0x24;
        public float unk_0x28;
        public float unk_0x2C;
        public float unk_0x30;

        public bool unk_0x34;
        public bool unk_0x35;

        public string htmlTextFormat;

        public int GetByteSize()
        {
            return 0x138;
        }

        public void Parse(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length < GetByteSize()) throw new ArgumentException("data");
            Unknown1 = BitConverter.ToInt32(data, 0);
            size = new Rect();
            size.Parse(data.Skip(4).Take(16).ToArray());
            fontId = BitConverter.ToInt32(data, 20);
            fontScale = BitConverter.ToSingle(data, 24);
            Color = new RGBA();
            Color.color = BitConverter.ToUInt32(data, 28);
            alignment = (eFuiAlignment)BitConverter.ToInt32(data, 32);
            unk_0x24 = BitConverter.ToSingle(data, 36);
            unk_0x28 = BitConverter.ToSingle(data, 40);
            unk_0x2C = BitConverter.ToSingle(data, 44);
            unk_0x30 = BitConverter.ToSingle(data, 48);
            unk_0x34 = BitConverter.ToBoolean(data, 52);
            unk_0x35 = BitConverter.ToBoolean(data, 53);

            htmlTextFormat = Encoding.ASCII.GetString(data, 56, 0x100);
        }

        public byte[] ToArray()
        {
            var arr = new byte[GetByteSize()];
            BitConverter.GetBytes(Unknown1).CopyTo(arr, 0);
            size.ToArray().CopyTo(arr, 4);
            BitConverter.GetBytes(fontId).CopyTo(arr, 20);
            BitConverter.GetBytes(fontScale).CopyTo(arr, 24);
            Color.ToArray().CopyTo(arr, 28);
            BitConverter.GetBytes((int)alignment).CopyTo(arr, 32);
            BitConverter.GetBytes(unk_0x24).CopyTo(arr, 36);
            BitConverter.GetBytes(unk_0x28).CopyTo(arr, 40);
            BitConverter.GetBytes(unk_0x2C).CopyTo(arr, 44);
            BitConverter.GetBytes(unk_0x30).CopyTo(arr, 48);
            BitConverter.GetBytes(unk_0x34).CopyTo(arr, 52);
            BitConverter.GetBytes(unk_0x35).CopyTo(arr, 53);
            Encoding.ASCII.GetBytes(htmlTextFormat, 0, 0x100, arr, 56);
            return arr;
        }

        public override string ToString()
        {
            return $"Unknown: {Unknown1}\n" +
                $"Size: {size}\n" +
                $"Font id: {fontId}\n" +
                $"Font scale: {fontScale}\n" +
                $"Color: {Color}\n" +
                $"Alignment: {alignment}\n" +
                $"unk_0x24: {unk_0x24}\n" +
                $"unk_0x28: {unk_0x28}\n" +
                $"unk_0x2C: {unk_0x2C}\n" +
                $"unk_0x30: {unk_0x30}\n" +
                $"unk_0x34: {unk_0x34}\n" +
                $"unk_0x35: {unk_0x35}\n" +
                $"Html text Data: {htmlTextFormat}";
        }
    }
}
