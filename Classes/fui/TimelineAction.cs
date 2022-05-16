using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FUI_Studio.Classes.fui
{
    public class TimelineAction : IFuiObject
    {
        public byte ActionType;
        public byte unk_0x01;
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
            unk_0x01 = data[1];
            FrameIndex = BitConverter.ToInt16(data, 2);
            UnknownName1 = Encoding.UTF8.GetString(data, 4, 0x40);
            UnknownName2 = Encoding.UTF8.GetString(data, 0x44, 0x40);
        }

        public byte[] ToArray()
        {
            var arr = new byte[GetByteSize()];
            arr[0] = ActionType;
            arr[1] = unk_0x01;
            BitConverter.GetBytes(FrameIndex).CopyTo(arr, 2);
            Encoding.UTF8.GetBytes(UnknownName1, 0, 0x40, arr, 0x4);
            Encoding.UTF8.GetBytes(UnknownName2, 0, 0x40, arr, 0x44);
            return arr;
        }

        public override string ToString()
        {
            string str = $"Action Type: {ActionType}\n";
            str += $"Unknown 0x01: {unk_0x01}\n";
            str += $"Frame Index: {FrameIndex}\n";
            str += $"Str Arg0: {UnknownName1}\n";
            str += $"Str Arg1: {UnknownName2}\n";
            return str;
        }
    }
}
