using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUI_Studio.Classes.fui
{
    public class ShapeComponent : IFuiObject
    {
        public FillStyle fillInfo;
        public int vertIndex;
        public int vertCount;

        public int GetByteSize()
        {
            return 0x2c;
        }

        public void Parse(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length != GetByteSize()) throw new ArgumentException();
            fillInfo = new FillStyle();
            fillInfo.Parse(data.Take(fillInfo.GetByteSize()).ToArray());
            vertIndex = BitConverter.ToInt32(data, 0x24);
            vertCount = BitConverter.ToInt32(data, 0x28);
        }

        public byte[] ToArray()
        {
            var arr = new byte[GetByteSize()];
            fillInfo.ToArray().CopyTo(arr, 0);
            BitConverter.GetBytes(vertIndex).CopyTo(arr, 0x24);
            BitConverter.GetBytes(vertCount).CopyTo(arr, 0x28);
            return arr;
        }

        public override string ToString()
        {
            return $"Fill Info: \n{fillInfo}\n" +
                $"Vert Index: {vertIndex}\n" +
                $"Vert Count: {vertCount}";
        }
    }
}
