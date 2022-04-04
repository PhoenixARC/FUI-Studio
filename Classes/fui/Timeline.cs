using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUI_Studio.Classes.fui
{
    public class Timeline : fui.IFuiObject
    {
        public int symbolIndex;
        public Int16 frameIndex;
        public Int16 frameCount;
        public Int16 actionIndex;
        public Int16 actionCount;
        public Rect rectangle;

        public int GetByteSize()
        {
            return 0x1c;
        }

        public void Parse(byte[] data)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (data.Length != GetByteSize()) throw new ArgumentException();
            symbolIndex = BitConverter.ToInt32(data, 0);
            frameIndex = BitConverter.ToInt16(data, 4);
            frameCount = BitConverter.ToInt16(data, 6);
            actionIndex = BitConverter.ToInt16(data, 8);
            actionCount = BitConverter.ToInt16(data, 10);
            rectangle = new Rect();
            rectangle.Parse(data.Skip(12).Take(rectangle.GetByteSize()).ToArray());
        }

        public byte[] ToArray()
        {
            var arr = new byte[GetByteSize()];
            BitConverter.GetBytes(symbolIndex).CopyTo(arr, 0);
            BitConverter.GetBytes(frameIndex).CopyTo(arr, 4);
            BitConverter.GetBytes(frameCount).CopyTo(arr, 6);
            BitConverter.GetBytes(actionIndex).CopyTo(arr, 8);
            BitConverter.GetBytes(actionCount).CopyTo(arr, 10);
            rectangle.ToArray().CopyTo(arr, 12);
            return arr;
        }

        public override string ToString()
        {
            return $"Symbol Index: {symbolIndex}\n" +
                $"Frame Index: {frameIndex}\n" +
                $"Frame Count: {frameCount}\n" +
                $"Action Index: {actionIndex}\n" +
                $"Action Count: {actionCount}\n" +
                $"Rectangle: {rectangle}";
        }
    }
}
