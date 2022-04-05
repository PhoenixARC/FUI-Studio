using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUI_Studio.Classes.fui
{
    public class Reference : IFuiObject
    {
        public int SymbolIndex;
        public string Name;
        public int Index;
        public int GetByteSize()
        {
            return 0x48;
        }

        public void Parse(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length != GetByteSize()) throw new ArgumentException("data");
            SymbolIndex = BitConverter.ToInt32(data, 0);
            Name = Encoding.UTF8.GetString(data, 4, 0x40);
            Index = BitConverter.ToInt32(data, 0x44);
        }

        public byte[] ToArray()
        {
            var arr = new byte[GetByteSize()];

            return arr;
        }

        public override string ToString()
        {
            return $"Symbol Index: {SymbolIndex}\n" +
                $"Object Index: {Index}\n" +
                $"Name: {Name}";
        }
    }
}
