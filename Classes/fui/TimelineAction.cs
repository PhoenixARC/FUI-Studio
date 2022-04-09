using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUI_Studio.Classes.fui
{
    public class TimelineAction : IFuiObject
    {
        public byte ActionType;
        public Int16 FrameIndex;
        public string UnknownName1;
        public string UnknownName2;

        public int GetByteSize()
        {
            return 0x84;
        }

        public void Parse(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length != GetByteSize()) throw new ArgumentException();
            ActionType = data[0];
            FrameIndex = BitConverter.ToInt16(data, 2);
            UnknownName1 = Encoding.ASCII.GetString(data, 4, 0x40);
            UnknownName2 = Encoding.ASCII.GetString(data, 0x44, 0x40);
        }

        public byte[] ToArray()
        {
            var arr = new byte[GetByteSize()];
            arr[0] = ActionType;
            arr[1] = 0;
            BitConverter.GetBytes(FrameIndex).CopyTo(arr, 2);
            Encoding.ASCII.GetBytes(UnknownName1, 0, 0x40, arr, 0x4);
            Encoding.ASCII.GetBytes(UnknownName2, 0, 0x40, arr, 0x44);
            return arr;
        }

        public override string ToString()
        {
            return $"Action Type: {ActionType}\n" +
                $"Frame Index: {FrameIndex}\n" +
                $"Str Arg0: {UnknownName1}\n" +
                $"Str Arg1: {UnknownName2}";
        }
    }
}
