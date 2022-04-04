using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUI_Studio.Classes.fui
{
    public class Header : IFuiObject
    {
        public string Identifier = "IUF";
        public byte version = 1;
        public int Unknow = 0;
        public int ContentSize = 0;
        public string SwfFileName = "";
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
        public int imagesSize = 0;
        public int FontNameCount = 0;
        public int ImportAssetCount = 0;
        public Rect FrameSize = new Rect();

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
            SwfFileName = Encoding.ASCII.GetString(data, 12, 0x40);

            TimelineCount = BitConverter.ToInt32(data, 0x4c);
            TimelineEventNameCount = BitConverter.ToInt32(data, 0x50);
            TimelineActionCount = BitConverter.ToInt32(data, 0x54);
            ShapeCount = BitConverter.ToInt32(data, 0x5c);
            ShapeComponentCount = BitConverter.ToInt32(data, 0x58);
            VertCount = BitConverter.ToInt32(data, 0x60);
            TimelineFrameCount = BitConverter.ToInt32(data, 0x64);
            TimelineEventCount = BitConverter.ToInt32(data, 0x68);
            ReferenceCount = BitConverter.ToInt32(data, 0x6c);
            EdittextCount = BitConverter.ToInt32(data, 0x70);
            SymbolCount = BitConverter.ToInt32(data, 0x74);
            BitmapCount = BitConverter.ToInt32(data, 0x78);
            imagesSize = BitConverter.ToInt32(data, 0x7c);
            FontNameCount = BitConverter.ToInt32(data, 0x80);
            ImportAssetCount = BitConverter.ToInt32(data, 0x84);
            FrameSize.Parse(data.Skip(0x88).ToArray()); 
        }

        public byte[] ToArray()
        {
            var arr = new byte[GetByteSize()];

            return arr;
        }

        public override string ToString()
        {
            return $"Version: {version}\n" +
                $"Signature: {Identifier}\n" +
                $"Content Size: {ContentSize}\n" +
                $"Import Name: {SwfFileName}\n" +
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
                $"imagesSize: {imagesSize}\n" +
                $"Font Name Count: {FontNameCount}\n" +
                $"Import Asset Count: {ImportAssetCount}\n" +
                $"Frame Size: {FrameSize}";
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
