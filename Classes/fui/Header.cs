using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUI_Studio.Classes.fui
{
    public class Header : IFuiObject
    {
        public byte version = 1;
        public string Identifier = "IUF";
        public int Unknow = 0;
        public int ContentSize = 0;
        public string ImportName = "";
        public int TimelineCount = 0;
        public int TimelineEventNameCount = 0;
        public int TimelineActionCount = 0;
        public int ShapeCount = 0;
        public int ShapeComponentCount = 0;
        public int VertCount = 0;
        public int TimelineFrameCount = 0;
        public int TimelineEventCount = 0;
        public int ReferenceCount = 0;
        public int EdittextCount = 0;
        public int SymbolCount = 0;
        public int BitmapCount = 0;
        public int ImagesSize = 0;
        public int FontNameCount = 0;
        public int ImportAssetCount = 0;
        public Rect StageSize = new Rect();

        public int GetByteSize()
        {
            return 0x98;
        }

        public void Parse(byte[] data)
        {
            if (data == null) throw new ArgumentNullException();
            if (data.Length != GetByteSize()) throw new ArgumentException();
            version = data[0];
            Identifier = Encoding.ASCII.GetString(data, 1, 3);
            ContentSize = BitConverter.ToInt32(data, 8);
            ImportName = Encoding.ASCII.GetString(data, 12, 0x40);

            TimelineCount           = BitConverter.ToInt32(data, 0x4c);
            TimelineEventNameCount  = BitConverter.ToInt32(data, 0x50);
            TimelineActionCount     = BitConverter.ToInt32(data, 0x54);
            ShapeCount              = BitConverter.ToInt32(data, 0x58);
            ShapeComponentCount     = BitConverter.ToInt32(data, 0x5c);
            VertCount               = BitConverter.ToInt32(data, 0x60);
            TimelineFrameCount      = BitConverter.ToInt32(data, 0x64);
            TimelineEventCount      = BitConverter.ToInt32(data, 0x68);
            ReferenceCount          = BitConverter.ToInt32(data, 0x6c);
            EdittextCount           = BitConverter.ToInt32(data, 0x70);
            SymbolCount             = BitConverter.ToInt32(data, 0x74);
            BitmapCount             = BitConverter.ToInt32(data, 0x78);
            ImagesSize              = BitConverter.ToInt32(data, 0x7c);
            FontNameCount           = BitConverter.ToInt32(data, 0x80);
            ImportAssetCount    = BitConverter.ToInt32(data, 0x84);
            StageSize.Parse(data.Skip(0x88).ToArray()); 
        }

        public byte[] ToArray()
        {
            var arr = new byte[GetByteSize()];
            arr[0] = version;
            Encoding.ASCII.GetBytes(Identifier, 0, 3, arr, 1);
            BitConverter.GetBytes(ContentSize).CopyTo(arr, 8);
            Encoding.ASCII.GetBytes(ImportName, 0, 0x40, arr, 12);
            BitConverter.GetBytes(TimelineCount).CopyTo(arr, 0x4c);
            BitConverter.GetBytes(TimelineEventNameCount).CopyTo(arr, 0x50);
            BitConverter.GetBytes(TimelineActionCount).CopyTo(arr, 0x54);
            BitConverter.GetBytes(ShapeCount).CopyTo(arr, 0x58);
            BitConverter.GetBytes(ShapeComponentCount).CopyTo(arr, 0x5c);
            BitConverter.GetBytes(VertCount).CopyTo(arr, 0x60);
            BitConverter.GetBytes(TimelineFrameCount).CopyTo(arr, 0x64);
            BitConverter.GetBytes(TimelineEventCount).CopyTo(arr, 0x68);
            BitConverter.GetBytes(ReferenceCount).CopyTo(arr, 0x6c);
            BitConverter.GetBytes(EdittextCount).CopyTo(arr, 0x70);
            BitConverter.GetBytes(SymbolCount).CopyTo(arr, 0x74);
            BitConverter.GetBytes(BitmapCount).CopyTo(arr, 0x78);
            BitConverter.GetBytes(ImagesSize).CopyTo(arr, 0x7c);
            BitConverter.GetBytes(FontNameCount).CopyTo(arr, 0x80);
            BitConverter.GetBytes(ImportAssetCount).CopyTo(arr, 0x84);
            StageSize.ToArray().CopyTo(arr, 0x88);
            return arr;
        }

        public override string ToString()
        {
            return $"Version: {version}\n" +
                $"Signature: {Identifier}\n" +
                $"Content Size: {ContentSize}\n" +
                $"Import Name: {ImportName}\n" +
                $"Timeline Count: {TimelineCount}\n" +
                $"Timeline Event Name Count: {TimelineEventNameCount}\n" +
                $"Timeline Action Count: {TimelineActionCount}\n" +
                $"Shape Count: {ShapeCount}\n" +
                $"Shape Component Count: {ShapeComponentCount}\n" +
                $"Vert Count: {VertCount}\n" +
                $"Timeline Frame Count: {TimelineFrameCount}\n" +
                $"Timeline Event Count: {TimelineEventCount}\n" +
                $"Reference Count: {ReferenceCount}\n" +
                $"Edittext Count: {EdittextCount}\n" +
                $"Symbol Count: {SymbolCount}\n" +
                $"Bitmap Count: {BitmapCount}\n" +
                $"Images Size: {ImagesSize}\n" +
                $"Font name Count: {FontNameCount}\n" +
                $"Import asset Count: {ImportAssetCount}\n" +
                $"Stage size: {StageSize}";
        }

        public int GetObjectCountSum()
        {
            return TimelineCount + TimelineEventNameCount + TimelineActionCount + 
                ShapeCount + ShapeComponentCount + VertCount +
                TimelineFrameCount + TimelineEventCount +
                ReferenceCount + EdittextCount +
                SymbolCount + BitmapCount + 
                FontNameCount + ImportAssetCount;
        }
    }
}
