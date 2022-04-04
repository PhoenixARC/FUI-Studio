using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUI_Studio.Classes.fui
{
    public class TimelineEvent : fui.IFuiObject
    {
        public byte EventType;
        public byte Unknown0;
        public byte ObjectType;
        public byte Unknown1;
        public Int16 Unknown2;
        public Int16 Index;
        public Int16 Unknown3;
        public Int16 NameIndex;
        public Matrix matrix;
        public ColorTransform colorTransform;
        public RGBA Color;

        public int GetByteSize()
        {
            return 0x48;
        }

        public void Parse(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length != GetByteSize()) throw new ArgumentException("data");

            EventType = data[0];
            Unknown0 = data[1];
            ObjectType = data[2];
            Unknown1 = data[3];
            Unknown2 = BitConverter.ToInt16(data, 4);
            Index = BitConverter.ToInt16(data, 6);
            Unknown3 = BitConverter.ToInt16(data, 8);
            NameIndex = BitConverter.ToInt16(data, 10);
            matrix = new Matrix();
            matrix.Parse(data.Skip(12).Take(matrix.GetByteSize()).ToArray());
            colorTransform = new ColorTransform();
            colorTransform.Parse(data.Skip(0x24).Take(colorTransform.GetByteSize()).ToArray());
            Color = new RGBA();
            Color.RGBa = BitConverter.ToUInt16(data, 0x44);
        }

        public byte[] ToArray()
        {
            var arr = new byte[GetByteSize()];

            return arr;
        }

        public override string ToString()
        {
            return $"Event Type: {EventType}\n";
        }
    }
}
