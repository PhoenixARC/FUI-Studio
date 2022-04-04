using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUI_Studio.Classes.fui
{
    public class TimelineFrame : fui.IFuiObject
    {
        public string FrameName;
        public int EventIndex;
        public int EventCount;

        public int GetByteSize()
        {
            return 0x48;
        }

        public void Parse(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length != GetByteSize()) throw new ArgumentException();
            FrameName = Encoding.ASCII.GetString(data, 0, 0x40);
            EventIndex = BitConverter.ToInt32(data, 0x40);
            EventCount = BitConverter.ToInt32(data, 0x44);
        }

        public byte[] ToArray()
        {
            var arr = new byte[GetByteSize()];
            Encoding.ASCII.GetBytes(FrameName, 0, 0x40, arr, 0);
            BitConverter.GetBytes(EventIndex).CopyTo(arr, 0x40);
            BitConverter.GetBytes(EventCount).CopyTo(arr, 0x44);
            return arr;
        }

        public override string ToString()
        {
            return $"Frame Name: {FrameName}\n" +
                $"Event Index: {EventIndex}\n" +
                $"Event Count: {EventCount}";
        }
    }
}
