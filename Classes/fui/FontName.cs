using System;

namespace FUI_Studio.Classes.fui
{
    public class FontName : IFuiObject
    {
        public int Unknown1;
        public string Fontname;
        public int Unknown2;
        public string Unknown3;
        public byte[] Unknown4;
        public string Unknown5;
        public byte[] Unknown6;
        public string Unknown7;

        public int GetByteSize()
        {
            return 0x104;
        }

        public void Parse(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length != GetByteSize()) throw new ArgumentException();


        }

        public byte[] ToArray()
        {
            var arr = new byte[GetByteSize()];

            return arr;
        }

        public override string ToString()
        {
            return "TODO: implement Parse and ToArray..";
        }
    }
}
