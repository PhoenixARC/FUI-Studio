using System;
using System.Text;

namespace FUI_Studio.Classes.fui
{
    public class FontName : IFuiObject
    {
        public int Id;
        public string Name;
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

            Id = BitConverter.ToInt32(data, 0);
            Name = Encoding.ASCII.GetString(data, 0, 0x100);
        }

        public byte[] ToArray()
        {
            var arr = new byte[GetByteSize()];

            BitConverter.GetBytes(Id).CopyTo(arr, 0);
            Encoding.ASCII.GetBytes(Name, 0, 0x100, arr, 4);

            return arr;
        }

        public override string ToString()
        {
            return $"Id: {Id}\n" +
                $"Name: {Name}";
        }
    }
}
