using System;
using System.Text;

namespace FUI_Studio.Classes.fui
{
    public class ImportAsset : fui.IFuiObject
    {
        public string assetName;
        public int GetByteSize()
        {
            return 0x40;
        }

        public void Parse(byte[] data)
        {
            if (data == null) return;
            if (data.Length != GetByteSize()) return;
            assetName = Encoding.UTF8.GetString(data);
        }

        public byte[] ToArray()
        {
            var arr = new byte[GetByteSize()];
            Encoding.UTF8.GetBytes(assetName, 0, 0x40, arr, 0);
            return arr; 
        }

        public override string ToString()
        {
            return assetName;
        }
    }
}
