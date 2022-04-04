using System;

namespace FUI_Studio.Classes.fui
{
    public class Edittext : fui.IFuiObject
    {
        public int Unknown1;
        public Rect rectangle;
        public int fontId;
        public byte[] Unknown3;
        public RGBA Color;
        public byte[] Unknown4;
        public string htmlTextFormat;

        public int GetByteSize()
        {
            return 0x138;
        }

        public void Parse(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length < GetByteSize()) throw new ArgumentException("data");

        }

        public byte[] ToArray()
        {
            var arr = new byte[GetByteSize()];

            return arr;
        }

        public override string ToString()
        {
            return "TODO: Implement Parsing...";
        }
    }
}
