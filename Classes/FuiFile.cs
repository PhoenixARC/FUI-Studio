using System.IO;
using System.Collections.Generic;
using FUI_Studio.Classes.fui;
using FUI_Studio.Forms;
using System.Linq;
using System;

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

            public FUIFile(Stream stream)
            {
                int headerSize = header.GetByteSize();
                byte[] header_buffer = new byte[headerSize];
                stream.Read(header_buffer, 0, headerSize);
                header.Parse(header_buffer);
                int dataSize = header.ContentSize - header.ImagesSize;
                byte[] data = new byte[dataSize];
                stream.Read(data, 0, dataSize);
                byte[] imgRawData = new byte[header.ImagesSize];
                stream.Read(imgRawData, 0, header.ImagesSize);
                new LoadingFileDialog(this, data).ShowDialog();
                foreach (var bitmap in bitmaps)
                {
                    Images.Add(imgRawData.Skip(bitmap.offset).Take(bitmap.size).ToArray());
                }
            }

            public static FUIFile Open(string FilePath)
            {
                return new FUIFile(File.OpenRead(FilePath));
            }

            public byte[] Build()
            {
                UpdateHeaderCounts();
                AdjustFuiBitmapInfo();
                header.ContentSize = CalculateContentSize() + header.ImagesSize;
                byte[] fuiFileBuffer;
                using (var fuiStream = new MemoryStream())
                {
                    fuiStream.Write(header.ToArray(), 0, header.GetByteSize());
                    ConstructAndWriteObjectBuffer(fuiStream, timelines);
                    ConstructAndWriteObjectBuffer(fuiStream, timelineActions);
                    ConstructAndWriteObjectBuffer(fuiStream, shapes);
                    ConstructAndWriteObjectBuffer(fuiStream, shapeComponents);
                    ConstructAndWriteObjectBuffer(fuiStream, verts);
                    ConstructAndWriteObjectBuffer(fuiStream, timelineFrames);
                    ConstructAndWriteObjectBuffer(fuiStream, timelineEvents);
                    ConstructAndWriteObjectBuffer(fuiStream, timelineEventNames);
                    ConstructAndWriteObjectBuffer(fuiStream, references);
                    ConstructAndWriteObjectBuffer(fuiStream, edittexts);
                    ConstructAndWriteObjectBuffer(fuiStream, fontNames);
                    ConstructAndWriteObjectBuffer(fuiStream, symbols);
                    ConstructAndWriteObjectBuffer(fuiStream, importAssets);
                    ConstructAndWriteObjectBuffer(fuiStream, bitmaps);
                    foreach (byte[] img in Images)
                        fuiStream.Write(img, 0, img.Length);
                    fuiFileBuffer = fuiStream.ToArray();
                }
                return fuiFileBuffer;
            }

            private void UpdateHeaderCounts()
            {
                header.TimelineCount = timelines.Count;
                header.TimelineActionCount = timelineActions.Count;
                header.ShapeCount = shapes.Count;
                header.ShapeComponentCount = shapeComponents.Count;
                header.VertCount = verts.Count;
                header.TimelineFrameCount = timelineFrames.Count;
                header.TimelineEventCount = timelineEvents.Count;
                header.TimelineEventNameCount = timelineEventNames.Count;
                header.ReferenceCount = references.Count;
                header.EdittextCount = edittexts.Count;
                header.FontNameCount = fontNames.Count;
                header.SymbolCount = symbols.Count;
                header.ImportAssetCount = importAssets.Count;
                header.BitmapCount = bitmaps.Count;
            }

            private void AdjustFuiBitmapInfo()
            {
                if (Images.Count != bitmaps.Count) throw new Exception("Counts are different"); // should never happen...
                int offset = 0;
                for (int i = 0; i < bitmaps.Count; i++)
                {
                    var bitmap = bitmaps[i];
                    var img = Images[i];
                    int size = img.Length;
                    bitmap.offset = offset;
                    bitmap.size = size;
                    offset += size;
                }
                header.ImagesSize = offset;
            }

            private int CalculateContentSize()
            {
                return timelines.Count*0x1c + timelineActions.Count*0x84 +
                    shapes.Count*0x1c + shapeComponents.Count*0x2c +
                    verts.Count*0x8 + timelineFrames.Count*0x48 +
                    timelineEvents.Count*0x48 + timelineEventNames.Count*0x40 +
                    references.Count*0x48 + edittexts.Count*0x138 +
                    fontNames.Count*0x104 + symbols.Count*0x48 +
                    importAssets.Count*0x40 + bitmaps.Count*0x20;
            }

            private void ConstructAndWriteObjectBuffer<T>(Stream fs, List<T> objList) where T : IFuiObject
            {
                if (objList == null) throw new ArgumentNullException("obj list is null");
                if (objList.Count == 0) return;
                int objByteSize = objList[0].GetByteSize();
                byte[] buffer = new byte[objList.Count * objByteSize];
                int offset = 0;
                foreach (T obj in objList)
                {
                    obj.ToArray().CopyTo(buffer, offset);
                    offset += objByteSize;
                }
                fs.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
