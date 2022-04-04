using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUI_Studio.Classes.fui
{
    public class Symbol : fui.IFuiObject
    {
        public string Name;
        public int ObjectType;
        public int Index;
        public int GetByteSize()
        {
            return 0x48;
        }

        public void Parse(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length != GetByteSize()) throw new ArgumentException("data");
            Name = Encoding.ASCII.GetString(data, 0, 0x40);
            ObjectType = BitConverter.ToInt32(data, 0x40);
            Index = BitConverter.ToInt32(data, 0x44);
        }

        public byte[] ToArray()
        {
            var arr = new byte[GetByteSize()];
            Encoding.ASCII.GetBytes(Name, 0, 0x40, arr, 0);
            BitConverter.GetBytes(ObjectType).CopyTo(arr, 0x40);
            BitConverter.GetBytes(Index).CopyTo(arr, 0x44);
            return arr;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
