using System.IO;
using System.Collections.Generic;
using FUI_Studio.Classes.fui;
using FUI_Studio.Forms;
using System.Linq;

namespace FourJ
{
    public class UserInterface
    {
        public class FUIFile
        {
            public Header header = new Header();
            public List<Timeline> timelines = new List<Timeline>();
            public List<TimelineAction> timelineActions = new List<TimelineAction>();
            public List<Shape> shapes = new List<Shape>();
            public List<ShapeComponent> shapeComponents = new List<ShapeComponent>();
            public List<Vert> verts = new List<Vert>();
            public List<TimelineFrame> timelineFrames = new List<TimelineFrame>();
            public List<TimelineEvent> timelineEvents = new List<TimelineEvent>();
            public List<TimelineEventName> timelineEventNames = new List<TimelineEventName>();
            public List<Reference> references = new List<Reference>();
            public List<Edittext> edittexts = new List<Edittext>();
            public List<FontName> fontNames = new List<FontName>();
            public List<Symbol> symbols = new List<Symbol>();
            public List<ImportAsset> importAssets = new List<ImportAsset>();
            public List<FuiBitmap> bitmaps = new List<FuiBitmap>();
            public List<byte[]> Images = new List<byte[]>();

            public string FilePath = "";

            public static FUIFile Open(string FilePath)
            {
                var FUI = new FUIFile();
                FUI.FilePath = FilePath;
                using (var fsStream = File.OpenRead(FilePath))
                {
                    byte[] header_buffer = new byte[FUI.header.GetByteSize()];
                    fsStream.Read(header_buffer, 0, FUI.header.GetByteSize());
                    int fileOffset = FUI.header.GetByteSize();
                    FUI.header.Parse(header_buffer);
                    int dataSize = FUI.header.ContentSize - FUI.header.ImagesSize;
                    byte[] data = new byte[dataSize];
                    fsStream.Seek(fileOffset, SeekOrigin.Begin);
                    fsStream.Read(data, 0, dataSize);
                    fsStream.Seek(fileOffset + dataSize, SeekOrigin.Begin);
                    byte[] imgRawData = new byte[FUI.header.ImagesSize];
                    fsStream.Read(imgRawData, 0, FUI.header.ImagesSize);
                    new LoadingFileDialog(ref FUI, data).ShowDialog();
                    foreach(var bitmap in FUI.bitmaps)
                    {
                        FUI.Images.Add(imgRawData.Skip(bitmap.offset).Take(bitmap.size).ToArray());
                    }
                }
                return FUI;
            }

            public void Build()
            {
                // TODO
            }
        }
    }
}
