using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUI_Studio.Classes.fui
{
    public class TimelineEventName : IFuiObject
    {
        public string EventName;
        public int GetByteSize()
        {
            return 0x40;
        }

        public void Parse(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length != GetByteSize()) throw new ArgumentException();
            Encoding.ASCII.GetString(data, 0, 0x40);
        }

        public byte[] ToArray()
        {
            var arr = new byte[GetByteSize()];
            Encoding.ASCII.GetBytes(EventName, 0, 0x40, arr, 0);
            return arr;
        }

        public override string ToString()
        {
            return string.IsNullOrEmpty(EventName) ? "Empty" : EventName;
        }
    }
}
