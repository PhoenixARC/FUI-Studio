using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FourJ
{
    public class FourJUserInterface
    {

        public class FUI
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
            public List<Bitmap> bitmaps = new List<Bitmap>();
            public List<byte[]> Images = new List<byte[]>();
            public List<string> ImageNames = new List<string>();

            public int LoadProgress = 0;
            public int TotalProgress = 0;
            public int CurrentProgress = 0;
            public string status = "";
            public string FilePath = "";
        }

        #region FuiComponents

        //fuiHeader	0x98
        public class Header
        {
            public string Identifier;
            public int Unknow;
            public int ContentSize;
            public string SwfFileName;
            public int fuiTimelineCount;
            public int fuiTimelineEventNameCount;
            public int fuiTimelineActionCount;
            public int fuiShapeCount;
            public int fuiShapeComponentCount;
            public int fuiVertCount;
            public int fuiTimelineFrameCount;
            public int fuiTimelineEventCount;
            public int fuiReferenceCount;
            public int fuiEdittextCount;
            public int fuiSymbolCount;
            public int fuiBitmapCount;
            public int imagesSize;
            public int fuiFontNameCount;
            public int fuiImportAssetCount;
            public byte[] FrameSize;
        }

        //fuiTimeline	0x1c
        public class Timeline
        {
            public int ObjectType;
            public Int16 FrameIndex;
            public Int16 FrameCount;
            public Int16 ActionIndex;
            public Int16 ActionCount;
            public Rect Rectangle;

            public void total(byte[] Dat)
            {
                FrameIndex = BitConverter.ToInt16(Dat.Skip(0).Take(2).ToArray(), 0);
                FrameCount = BitConverter.ToInt16(Dat.Skip(2).Take(2).ToArray(), 0);
                ActionIndex = BitConverter.ToInt16(Dat.Skip(4).Take(2).ToArray(), 0);
                ActionCount = BitConverter.ToInt16(Dat.Skip(6).Take(2).ToArray(), 0);

            }
        }

        //fuiTimelineAction	0x84
        public class TimelineAction
        {
            public Int16 ActionType;
            public Int16 Unknown;
            public string UnknownName1;
            public string UnknownName2;
            public byte[] totalbytes;
            public void total(byte[] Dat)
            {
                ActionType = BitConverter.ToInt16(Dat.Skip(0).Take(2).ToArray(), 0);
                Unknown = BitConverter.ToInt16(Dat.Skip(2).Take(2).ToArray(), 0);

            }
        }

        //fuiShape	0x1c
        public class Shape
        {
            public int UnknownValue1;
            public int UnknownValue2;
            public int ObjectType;
            public Rect Rectangle;
        }

        //fuiShapeComponent	0x2c
        public class ShapeComponent
        {
            public FillStyle FillInfo;
            public int UnknownValue1;
            public int UnknownValue2;
        }

        //fuiVert	0x8
        public class Vert
        {
            public byte[] x;
            public byte[] y;
        }

        //fuiTimelineFrame	0x48
        public class TimelineFrame
        {
            public string FrameName;
            public int EventIndex;
            public int EventCount;
        }

        //fuiTimelineEvent	0x48
        public class TimelineEvent
        {
            public byte[] EventType;
            public byte[] ObjectType;
            public byte[] Unknown0;
            public byte[] Index;
            public byte[] Unknown1;
            public byte[] NameIndex;
            public Matrix matrix;
            public ColorTransform ColorTransform;
            public RGBA Color;
            public byte[] totalbytes;
            public void total(byte[] Dat)
            {
                EventType = Dat.Skip(0).Take(2).ToArray();
                ObjectType = Dat.Skip(2).Take(2).ToArray();
                Unknown0 = Dat.Skip(4).Take(2).ToArray();
                Index = Dat.Skip(6).Take(2).ToArray();
                Unknown1 = Dat.Skip(8).Take(2).ToArray();
                NameIndex = Dat.Skip(10).Take(2).ToArray();
                totalbytes = Dat;

            }
        }

        //fuiTimelineEventName	0x40
        public class TimelineEventName
        {
            public string EventName;
        }

        //fuiReference	0x48
        public class Reference
        {
            public int SymbolIndex;
            public string ReferenceName;
            public byte[] Index;
        }

        //fuiEdittext	0x138
        public class Edittext
        {
            public int Unknown1;
            public Rect rectangle;
            public int Unknown2;
            public byte[] Unknown3;
            public RGBA Color;
            public byte[] Unknown4;
            public string htmlTextFormat;
        }

        //fuiFontName	0x104
        public class FontName
        {
            public int Unknown1;
            public string Fontname;
            public int Unknown2;
            public string Unknown3;
            public byte[] Unknown4;
            public string Unknown5;
            public byte[] Unknown6;
            public string Unknown7;
        }

        //fuiSymbol	0x48
        public class Symbol
        {
            public string SymbolName;
            public int ObjectType;
            public int Unknown;
        }

        //fuiImportAsset	0x40
        public class ImportAsset
        {
            public string AssetName;
        }

        //fuiBitmap	0x20
        public class Bitmap
        {
            public byte[] Unknown1;
            public int ObjectType;
            public int ScaleWidth;
            public int ScaleHeight;
            public byte[] Size1;
            public byte[] Size2;
            public byte[] Unknown2;
            public int Unknown3;
            public byte[] BigDat;
            public byte[] Image;
        }

        //fuiRect
        public class Rect
        {
            public byte[] MinX;
            public byte[] MaxX;
            public byte[] MinY;
            public byte[] MaxY;
            public byte[] totalbytes;
            public void total(byte[] Dat)
            {
                MinX = Dat.Skip(0).Take(4).ToArray();
                MaxX = Dat.Skip(4).Take(4).ToArray();
                MinY = Dat.Skip(8).Take(4).ToArray();
                MaxY = Dat.Skip(12).Take(4).ToArray();
                totalbytes = Dat;
                
            }
        }

        //fuiRGBA
        public class RGBA
        {
            public byte R;
            public byte G;
            public byte B;
            public byte Alpha;
            public byte[] totalbytes;
            public void total(byte[] Dat)
            {
                R = Dat[0];
                G = Dat[1];
                B = Dat[2];
                Alpha = Dat[3];
                totalbytes = Dat;
                
            }
        }

        //fuiMatrix
        public class Matrix
        {
            public byte[] ScaleX;
            public byte[] ScaleY;
            public byte[] RotSkew0;
            public byte[] RotSkew1;
            public byte[] TranslateX;
            public byte[] TranslateY;
            public byte[] totalbytes;
            public void total(byte[] Dat)
            {
                ScaleX = Dat.Skip(0).Take(4).ToArray();
                ScaleY = Dat.Skip(4).Take(4).ToArray();
                RotSkew0 = Dat.Skip(8).Take(4).ToArray();
                RotSkew1 = Dat.Skip(12).Take(4).ToArray();
                TranslateX = Dat.Skip(16).Take(4).ToArray();
                TranslateY = Dat.Skip(20).Take(4).ToArray();
                totalbytes = Dat;
                
            }
        }

        //fuiColorTransform
        public class ColorTransform
        {
            public byte[] RedMultTerm;
            public byte[] GreenMultTerm;
            public byte[] BlueMultTerm;
            public byte[] AlphaMultTerm;
            public byte[] RedAddTerm;
            public byte[] GreenAddTerm;
            public byte[] BlueAddTerm;
            public byte[] AlphaAddTerm;
            public byte[] totalbytes;
            public void total(byte[] Dat)
            {
                RedMultTerm = Dat.Skip(0).Take(4).ToArray();
                GreenMultTerm = Dat.Skip(4).Take(4).ToArray();
                BlueMultTerm = Dat.Skip(8).Take(4).ToArray();
                AlphaMultTerm = Dat.Skip(12).Take(4).ToArray();
                RedAddTerm = Dat.Skip(16).Take(4).ToArray();
                GreenAddTerm = Dat.Skip(20).Take(4).ToArray();
                BlueAddTerm = Dat.Skip(24).Take(4).ToArray();
                AlphaAddTerm = Dat.Skip(28).Take(4).ToArray();
                totalbytes = Dat;
                
            }
        }

        //fuiFillStyle
        public class FillStyle
        {
            public byte[] Unknown0;
            public RGBA Color;
            public byte[] Unknown1;
            public Matrix Matrix;
            public byte[] totalbytes;
            public void total(byte[] Dat)
            {
                Color = new RGBA();
                Matrix = new Matrix();

                Unknown0 = Dat.Skip(0).Take(4).ToArray();
                Color.total(Dat.Skip(4).Take(4).ToArray());
                Unknown1 = Dat.Skip(8).Take(4).ToArray();
                Matrix.total(Dat.Skip(12).Take((int)0x18).ToArray());
                totalbytes = Dat;
            }
        }


        #endregion

        public class Functions
        {
            bool debug = false;
            public FUI fui2;

            int offset = (int)0x98;

            public void OpenFUI(string Path, FUI fjui, bool ReturnStatus)
            {

                fui2 = fjui;

                byte[] Data = File.ReadAllBytes(Path);
                fjui.status = "Reading Headers";
                ReadHeaders(Data.Skip(0).Take((int)0x98).ToArray());

                fjui.TotalProgress = fjui.header.fuiBitmapCount + fjui.header.fuiEdittextCount + fjui.header.fuiFontNameCount + fjui.header.fuiImportAssetCount + fjui.header.fuiReferenceCount + fjui.header.fuiShapeComponentCount + fjui.header.fuiShapeCount + fjui.header.fuiSymbolCount + fjui.header.fuiTimelineActionCount + fjui.header.fuiTimelineCount + fjui.header.fuiTimelineEventCount + fjui.header.fuiTimelineEventNameCount + fjui.header.fuiTimelineFrameCount + fjui.header.fuiVertCount + (int)0x98;

                if (ReturnStatus)
                {
                    fjui.status = "Reading Timelines";
                    fjui.CurrentProgress += (int)0x98;
                    fjui.LoadProgress = 6;
                }
                CheckTimelines(Data);
                if (ReturnStatus)
                {
                    fjui.status = "Reading Timeline Actions";
                    fjui.CurrentProgress += fjui.header.fuiTimelineCount;
                    fjui.LoadProgress += 6;
                }
                CheckTimelineActions(Data);
                if (ReturnStatus)
                {
                    fjui.status = "Reading Shapes";
                    fjui.CurrentProgress += fjui.header.fuiTimelineActionCount;
                    fjui.LoadProgress += 6;
                }
                CheckShapes(Data);
                if (ReturnStatus)
                {
                    fjui.status = "Reading Components";
                    fjui.CurrentProgress += fjui.header.fuiShapeCount;
                    fjui.LoadProgress += 6;
                }
                CheckShapeComponents(Data);
                if (ReturnStatus)
                {
                    fjui.status = "Reading Verts";
                    fjui.CurrentProgress += fjui.header.fuiShapeComponentCount;
                    fjui.LoadProgress += 6;
                }
                CheckVerts(Data);
                if (ReturnStatus)
                {
                    fjui.status = "Reading Timeline Frames";
                    fjui.CurrentProgress += fjui.header.fuiVertCount;
                    fjui.LoadProgress += 6;
                }
                CheckTimelineFrames(Data);
                if (ReturnStatus)
                {
                    fjui.status = "Reading Timeline Events";
                    fjui.CurrentProgress += fjui.header.fuiTimelineFrameCount;
                    fjui.LoadProgress += 6;
                }
                CheckTimelineEvents(Data);
                if (ReturnStatus)
                {
                    fjui.status = "Reading Timeline Event Names";
                    fjui.CurrentProgress += fjui.header.fuiTimelineEventCount;
                    fjui.LoadProgress += 6;
                }
                CheckTimelineEventNames(Data);
                if (ReturnStatus)
                {
                    fjui.status = "Reading References";
                    fjui.CurrentProgress += fjui.header.fuiTimelineEventNameCount;
                    fjui.LoadProgress += 6;
                }
                CheckReferences(Data);
                if (ReturnStatus)
                {
                    fjui.status = "Reading Edit Texts";
                    fjui.CurrentProgress += fjui.header.fuiReferenceCount;
                    fjui.LoadProgress += 6;
                }
                CheckEdittexts(Data);
                if (ReturnStatus)
                {
                    fjui.status = "Reading Font Names";
                    fjui.CurrentProgress += fjui.header.fuiEdittextCount;
                    fjui.LoadProgress += 6;
                }
                CheckFontNames(Data);
                if (ReturnStatus)
                {
                    fjui.status = "Reading Symbols";
                    fjui.CurrentProgress += fjui.header.fuiFontNameCount;
                    fjui.LoadProgress += 6;
                }
                CheckSymbols(Data);
                if (ReturnStatus)
                {
                    fjui.status = "Reading Import Assets";
                    fjui.CurrentProgress += fjui.header.fuiSymbolCount;
                    fjui.LoadProgress += 6;
                }
                CheckImportAssets(Data);
                if (ReturnStatus)
                {
                    fjui.status = "Reading Bitmaps";
                    fjui.CurrentProgress += fjui.header.fuiImportAssetCount;
                    fjui.LoadProgress += 6;
                }
                CheckBitmaps(Data, fjui.header.fuiBitmapCount);
                if (ReturnStatus)
                {
                    fjui.status = "Finished";
                    fjui.CurrentProgress += fjui.header.fuiBitmapCount;
                    fjui.LoadProgress += 6;
                }
                if (ReturnStatus)
                {
                    fjui.LoadProgress += 10;
                }
            }

            void ReadHeaders(byte[] Header)
            {

                byte[] Identifier = Header.Skip(0).Take(4).ToArray();
                byte[] Unknow = Header.Skip(4).Take(4).ToArray();
                byte[] ContentSize = Header.Skip(8).Take(4).ToArray();
                byte[] SwfFileName = Header.Skip((int)0xc).Take((int)0x40).ToArray();
                byte[] fuiTimelineCount = Header.Skip((int)0x4c).Take(4).ToArray();
                byte[] fuiTimelineEventNameCount = Header.Skip((int)0x50).Take(4).ToArray();
                byte[] fuiTimelineActionCount = Header.Skip((int)0x54).Take(4).ToArray();
                byte[] fuiShapeCount = Header.Skip((int)0x58).Take(4).ToArray();
                byte[] fuiShapeComponentCount = Header.Skip((int)0x5c).Take(4).ToArray();
                byte[] fuiVertCount = Header.Skip((int)0x60).Take(4).ToArray();
                byte[] fuiTimelineFrameCount = Header.Skip((int)0x64).Take(4).ToArray();
                byte[] fuiTimelineEventCount = Header.Skip((int)0x68).Take(4).ToArray();
                byte[] fuiReferenceCount = Header.Skip((int)0x6c).Take(4).ToArray();
                byte[] fuiEdittextCount = Header.Skip((int)0x70).Take(4).ToArray();
                byte[] fuiSymbolCount = Header.Skip((int)0x74).Take(4).ToArray();
                byte[] fuiBitmapCount = Header.Skip((int)0x78).Take(4).ToArray();
                byte[] imagesSize = Header.Skip((int)0x7c).Take(4).ToArray();
                byte[] fuiFontNameCount = Header.Skip((int)0x80).Take(4).ToArray();
                byte[] fuiImportAssetCount = Header.Skip((int)0x84).Take(4).ToArray();
                byte[] FrameSize = Header.Skip((int)0x88).Take((int)0x10).ToArray();


                fui2.header.Identifier = Encoding.Default.GetString(Identifier);
                fui2.header.Unknow = BitConverter.ToInt32(Unknow, 0);
                fui2.header.ContentSize = BitConverter.ToInt32(ContentSize, 0);
                fui2.header.SwfFileName = Encoding.Default.GetString(SwfFileName);
                fui2.header.fuiTimelineCount = BitConverter.ToInt32(fuiTimelineCount, 0);
                fui2.header.fuiTimelineEventNameCount = BitConverter.ToInt32(fuiTimelineEventNameCount, 0);
                fui2.header.fuiTimelineActionCount = BitConverter.ToInt32(fuiTimelineActionCount, 0);
                fui2.header.fuiShapeCount = BitConverter.ToInt32(fuiShapeCount, 0);
                fui2.header.fuiShapeComponentCount = BitConverter.ToInt32(fuiShapeComponentCount, 0);
                fui2.header.fuiVertCount = BitConverter.ToInt32(fuiVertCount, 0);
                fui2.header.fuiTimelineFrameCount = BitConverter.ToInt32(fuiTimelineFrameCount, 0);
                fui2.header.fuiTimelineEventCount = BitConverter.ToInt32(fuiTimelineEventCount, 0);
                fui2.header.fuiReferenceCount = BitConverter.ToInt32(fuiReferenceCount, 0);
                fui2.header.fuiEdittextCount = BitConverter.ToInt32(fuiEdittextCount, 0);
                fui2.header.fuiSymbolCount = BitConverter.ToInt32(fuiSymbolCount, 0);
                fui2.header.fuiBitmapCount = BitConverter.ToInt32(fuiBitmapCount, 0);
                fui2.header.imagesSize = BitConverter.ToInt32(imagesSize, 0);
                fui2.header.fuiFontNameCount = BitConverter.ToInt32(fuiFontNameCount, 0);
                fui2.header.fuiImportAssetCount = BitConverter.ToInt32(fuiImportAssetCount, 0);
                fui2.header.FrameSize = (FrameSize);

                if (debug)
                {
                    Console.WriteLine(" *** HEADERS *** ");
                    Console.WriteLine("Identifier - " + Encoding.Default.GetString(Identifier));
                    Console.WriteLine("Unknow - " + BitConverter.ToInt32(Unknow, 0));
                    Console.WriteLine("ContentSize - " + BitConverter.ToInt32(ContentSize, 0));
                    Console.WriteLine("SwfFileName - " + Encoding.Default.GetString(SwfFileName));
                    Console.WriteLine("fuiTimelineCount - " + BitConverter.ToInt32(fuiTimelineCount, 0));
                    Console.WriteLine("fuiTimelineEventNameCount - " + BitConverter.ToInt32(fuiTimelineEventNameCount, 0));
                    Console.WriteLine("fuiTimelineActionCount - " + BitConverter.ToInt32(fuiTimelineActionCount, 0));
                    Console.WriteLine("fuiShapeCount - " + BitConverter.ToInt32(fuiShapeCount, 0));
                    Console.WriteLine("fuiShapeComponentCount - " + BitConverter.ToInt32(fuiShapeComponentCount, 0));
                    Console.WriteLine("fuiVertCount - " + BitConverter.ToInt32(fuiVertCount, 0));
                    Console.WriteLine("fuiTimelineFrameCount - " + BitConverter.ToInt32(fuiTimelineFrameCount, 0));
                    Console.WriteLine("fuiTimelineEventCount - " + BitConverter.ToInt32(fuiTimelineEventCount, 0));
                    Console.WriteLine("fuiReferenceCount - " + BitConverter.ToInt32(fuiReferenceCount, 0));
                    Console.WriteLine("fuiEdittextCount - " + BitConverter.ToInt32(fuiEdittextCount, 0));
                    Console.WriteLine("fuiSymbolCount - " + BitConverter.ToInt32(fuiSymbolCount, 0));
                    Console.WriteLine("fuiBitmapCount - " + BitConverter.ToInt32(fuiBitmapCount, 0));
                    Console.WriteLine("imagesSize - " + BitConverter.ToInt32(imagesSize, 0));
                    Console.WriteLine("fuiFontNameCount - " + BitConverter.ToInt32(fuiFontNameCount, 0));
                    Console.WriteLine("fuiImportAssetCount - " + BitConverter.ToInt32(fuiImportAssetCount, 0));
                    Console.WriteLine("FrameSize - " + BitConverter.ToString(FrameSize) + "");
                    Console.WriteLine(" ");
                    Console.WriteLine(" ");
                    Console.WriteLine(" ");

                    //rebuild
                    List<byte> Output = new List<byte>();
                    Output.AddRange(Encoding.UTF8.GetBytes(fui2.header.Identifier));
                    Output.AddRange(BitConverter.GetBytes(fui2.header.Unknow));
                    Output.AddRange(BitConverter.GetBytes(fui2.header.ContentSize));
                    Output.AddRange(Encoding.UTF8.GetBytes(fui2.header.SwfFileName));
                    Output.AddRange(BitConverter.GetBytes(fui2.header.fuiTimelineCount));
                    Output.AddRange(BitConverter.GetBytes(fui2.header.fuiTimelineEventNameCount));
                    Output.AddRange(BitConverter.GetBytes(fui2.header.fuiTimelineActionCount));
                    Output.AddRange(BitConverter.GetBytes(fui2.header.fuiShapeCount));
                    Output.AddRange(BitConverter.GetBytes(fui2.header.fuiShapeComponentCount));
                    Output.AddRange(BitConverter.GetBytes(fui2.header.fuiVertCount));
                    Output.AddRange(BitConverter.GetBytes(fui2.header.fuiTimelineFrameCount));
                    Output.AddRange(BitConverter.GetBytes(fui2.header.fuiTimelineEventCount));
                    Output.AddRange(BitConverter.GetBytes(fui2.header.fuiReferenceCount));
                    Output.AddRange(BitConverter.GetBytes(fui2.header.fuiEdittextCount));
                    Output.AddRange(BitConverter.GetBytes(fui2.header.fuiSymbolCount));
                    Output.AddRange(BitConverter.GetBytes(fui2.header.fuiBitmapCount));
                    Output.AddRange(BitConverter.GetBytes(fui2.header.imagesSize));
                    Output.AddRange(BitConverter.GetBytes(fui2.header.fuiFontNameCount));
                    Output.AddRange(BitConverter.GetBytes(fui2.header.fuiImportAssetCount));
                    Output.AddRange((fui2.header.FrameSize));

                    if (BitConverter.ToString(Output.ToArray()) != BitConverter.ToString(Header))
                        throw new Exception();
                }

            }

            void CheckTimelines(byte[] Data)
            {
                int i = 1;
                while (i <= fui2.header.fuiTimelineCount)
                {
                    FourJUserInterface.Timeline tl = new FourJUserInterface.Timeline();
                    tl.ObjectType = BitConverter.ToInt32(Data.Skip(offset).Take(4).ToArray(), 0);
                    tl.total(Data.Skip(offset + 4).Take(8).ToArray());
                    tl.Rectangle = new Rect();
                    tl.Rectangle.total(Data.Skip(offset + (int)0xc).Take((int)0x10).ToArray());
                    fui2.timelines.Add(tl);
                    offset += (int)0x1C;
                    i++;

                    if (debug)
                    {
                        Console.WriteLine("Timeline -- ObjectType=" + tl.ObjectType);
                        Console.WriteLine("Timeline -- FrameIndex=" + tl.FrameIndex);
                        Console.WriteLine("Timeline -- FrameCount=" + tl.FrameCount);
                        Console.WriteLine("Timeline -- ActionIndex=" + tl.ActionIndex);
                        Console.WriteLine("Timeline -- ActionCount=" + tl.ActionCount);
                        Console.WriteLine("Timeline -- Rectangle=" + BitConverter.ToString(tl.Rectangle.totalbytes));
                    }
                }

            }

            void CheckTimelineActions(byte[] Data)
            {
                int i = 1;
                while (i <= fui2.header.fuiTimelineActionCount)
                {
                    FourJUserInterface.TimelineAction tl = new FourJUserInterface.TimelineAction();
                    tl.total(Data.Skip(offset).Take(4).ToArray().Select(b => (byte)b).ToArray());
                    tl.UnknownName1 = Encoding.UTF8.GetString(Data.Skip(offset + (int)0x4).Take((int)0x40).ToArray());
                    tl.UnknownName2 = Encoding.UTF8.GetString(Data.Skip(offset + (int)0x44).Take((int)0x40).ToArray());
                    fui2.timelineActions.Add(tl);
                    offset += (int)0x84;
                    i++;

                    if (debug)
                    {
                        Console.WriteLine("TimelineAction -- ActionType=" + tl.ActionType);
                        Console.WriteLine("TimelineAction -- Unknown=" + tl.Unknown);
                        Console.WriteLine("TimelineAction -- UnknownName1=" + tl.UnknownName1);
                        Console.WriteLine("TimelineAction -- UnknownName2=" + tl.UnknownName2);
                    }
                }
            }

            void CheckShapes(byte[] Data)
            {
                int i = 1;
                while (i <= fui2.header.fuiShapeCount)
                {
                    FourJUserInterface.Shape tl = new FourJUserInterface.Shape();
                    tl.UnknownValue1 = BitConverter.ToInt32(Data.Skip(offset).Take(4).ToArray(), 0);
                    tl.UnknownValue2 = BitConverter.ToInt32(Data.Skip(offset + (int)0x4).Take(4).ToArray(), 0);
                    tl.ObjectType = BitConverter.ToInt32(Data.Skip(offset + (int)0x8).Take(4).ToArray(), 0);
                    tl.Rectangle = new Rect();
                    tl.Rectangle.total(Data.Skip(offset + (int)0xc).Take((int)0x10).ToArray());
                    fui2.shapes.Add(tl);
                    offset += (int)0x1C;
                    i++;

                    if (debug)
                    {
                        Console.WriteLine("Shape -- UnknownValue1=" + tl.UnknownValue1);
                        Console.WriteLine("Shape -- UnknownValue2=" + tl.UnknownValue2);
                        Console.WriteLine("Shape -- ObjectType=" + tl.ObjectType);
                        Console.WriteLine("Shape -- Rectangle=" + BitConverter.ToString(tl.Rectangle.totalbytes));
                    }
                }
            }

            void CheckShapeComponents(byte[] Data)
            {
                int i = 1;
                while (i <= fui2.header.fuiShapeComponentCount)
                {
                    FourJUserInterface.ShapeComponent tl = new FourJUserInterface.ShapeComponent();
                    tl.FillInfo = new FillStyle();
                    Console.WriteLine("Comp1");
                    tl.FillInfo.total(Data.Skip(offset).Take((int)0x24).ToArray());
                    tl.UnknownValue1 = BitConverter.ToInt32(Data.Skip(offset + (int)0x24).Take(4).ToArray(), 0);
                    tl.UnknownValue2 = BitConverter.ToInt32(Data.Skip(offset + (int)0x28).Take(4).ToArray(), 0);
                    fui2.shapeComponents.Add(tl);
                    offset += (int)0x2C;
                    i++;
                    if (debug)
                    {
                        Console.WriteLine("fuiShapeComponentCount -- FillInfo=" + tl.FillInfo);
                        Console.WriteLine("fuiShapeComponentCount -- UnknownValue1=" + tl.UnknownValue1);
                        Console.WriteLine("fuiShapeComponentCount -- UnknownValue2=" + tl.UnknownValue2);
                    }
                }
            }

            void CheckVerts(byte[] Data)
            {
                int i = 1;
                while (i <= fui2.header.fuiVertCount)
                {
                    FourJUserInterface.Vert tl = new FourJUserInterface.Vert();
                    tl.x = (Data.Skip(offset).Take(4).ToArray());
                    tl.y = (Data.Skip(offset + (int)0x4).Take(4).ToArray());
                    fui2.verts.Add(tl);
                    offset += (int)0x8;
                    i++;
                    if (debug)
                    {
                        Console.WriteLine("fuiVert -- x=" + tl.x);
                        Console.WriteLine("fuiVert -- y=" + tl.y);
                    }
                }
            }

            void CheckTimelineFrames(byte[] Data)
            {
                int i = 1;
                while (i <= fui2.header.fuiTimelineFrameCount)
                {
                    FourJUserInterface.TimelineFrame tl = new FourJUserInterface.TimelineFrame();
                    tl.FrameName = Encoding.UTF8.GetString(Data.Skip(offset).Take((int)0x40).ToArray());
                    tl.EventIndex = BitConverter.ToInt32(Data.Skip(offset + (int)0x40).Take(4).ToArray(), 0);
                    tl.EventCount = BitConverter.ToInt32(Data.Skip(offset + (int)0x44).Take(4).ToArray(), 0);
                    fui2.timelineFrames.Add(tl);
                    offset += (int)0x48;
                    i++;
                    if (debug)
                    {
                        Console.WriteLine("fuiTimelineFrame -- FrameName=" + tl.FrameName);
                        Console.WriteLine("fuiTimelineFrame -- Unknown1=" + tl.EventIndex);
                        Console.WriteLine("fuiTimelineFrame -- Unknown2=" + tl.EventCount);
                    }
                }
            }

            void CheckTimelineEvents(byte[] Data)
            {
                int i = 1;
                while (i <= fui2.header.fuiTimelineEventCount)
                {
                    FourJUserInterface.TimelineEvent tl = new FourJUserInterface.TimelineEvent();
                    tl.total(Data.Skip(offset).Take((int)0xc).ToArray().Select(b => (byte)b).ToArray());
                    tl.matrix = new Matrix();
                    tl.ColorTransform = new ColorTransform();
                    tl.Color = new RGBA();
                    tl.matrix.total(Data.Skip(offset + (int)0xc).Take((int)0x18).ToArray());
                    tl.ColorTransform.total(Data.Skip(offset + (int)0x24).Take((int)0x20).ToArray());
                    tl.Color.total(Data.Skip(offset + (int)0x44).Take(4).ToArray());
                    fui2.timelineEvents.Add(tl);
                    offset += (int)0x48;
                    i++;
                    if (debug)
                    {
                        Console.WriteLine("TimelineEvent -- EventType=" + BitConverter.ToString(tl.EventType));
                        Console.WriteLine("TimelineEvent -- ObjectType=" + BitConverter.ToString(tl.ObjectType));
                        Console.WriteLine("TimelineEvent -- Unknown0=" + BitConverter.ToString(tl.Unknown0));
                        Console.WriteLine("TimelineEvent -- Index=" + BitConverter.ToString(tl.Index));
                        Console.WriteLine("TimelineEvent -- Unknown1=" + BitConverter.ToString(tl.Unknown1));
                        Console.WriteLine("TimelineEvent -- NameIndex=" + BitConverter.ToString(tl.NameIndex));
                        Console.WriteLine("TimelineEvent -- matrix=" + BitConverter.ToString(tl.matrix.totalbytes));
                        Console.WriteLine("TimelineEvent -- ColorTransform=" + BitConverter.ToString(tl.ColorTransform.totalbytes));
                        Console.WriteLine("TimelineEvent -- Color=" + BitConverter.ToString(tl.Color.totalbytes));
                    }
                }
            }

            void CheckTimelineEventNames(byte[] Data)
            {

                int i = 1;
                while (i <= fui2.header.fuiTimelineEventNameCount)
                {
                    FourJUserInterface.TimelineEventName tl = new FourJUserInterface.TimelineEventName();
                    tl.EventName = Encoding.UTF8.GetString(Data.Skip(offset).Take((int)0x40).ToArray());
                    fui2.timelineEventNames.Add(tl);
                    offset += (int)0x40;
                    i++;
                    if (debug)
                    {
                        Console.WriteLine("TimelineEventName -- EventName=" + tl.EventName);
                    }
                }
            }

            void CheckReferences(byte[] Data)
            {
                int i = 1;
                while (i <= fui2.header.fuiReferenceCount)
                {
                    FourJUserInterface.Reference tl = new FourJUserInterface.Reference();
                    tl.SymbolIndex = BitConverter.ToInt32(Data.Skip(offset).Take(4).ToArray(), 0);
                    tl.ReferenceName = Encoding.UTF8.GetString(Data.Skip(offset + (int)0x4).Take((int)0x40).ToArray());
                    tl.Index = (Data.Skip(offset + (int)0x40).Take(4).ToArray());
                    fui2.references.Add(tl);
                    offset += (int)0x48;
                    i++;
                    if (debug)
                    {
                        Console.WriteLine("Reference -- Unknown1=" + tl.SymbolIndex);
                        Console.WriteLine("Reference -- ReferenceName=" + tl.ReferenceName);
                        Console.WriteLine("Reference -- Unknown2=" + BitConverter.ToString(tl.Index, 0));
                    }
                }
            }

            void CheckEdittexts(byte[] Data)
            {

                int i = 1;
                while (i <= fui2.header.fuiEdittextCount)
                {
                    FourJUserInterface.Edittext tl = new FourJUserInterface.Edittext();
                    tl.Unknown1 = BitConverter.ToInt32(Data.Skip(offset).Take(4).ToArray(), 0);
                    tl.rectangle = new Rect();
                    tl.rectangle.total(Data.Skip(offset + (int)0x4).Take((int)0x10).ToArray());
                    tl.Unknown2 = BitConverter.ToInt32(Data.Skip(offset + (int)0x14).Take(4).ToArray(), 0);
                    tl.Unknown3 = Data.Skip(offset + (int)0x18).Take(4).ToArray();
                    tl.Color = new RGBA();
                    tl.Color.total(Data.Skip(offset + (int)0x1c).Take(4).ToArray());
                    tl.Unknown4 = Data.Skip(offset + (int)0x20).Take((int)0x18).ToArray();
                    tl.htmlTextFormat = Encoding.UTF8.GetString(Data.Skip(offset + (int)0x38).Take((int)0x100).ToArray());
                    fui2.edittexts.Add(tl);
                    offset += (int)0x138;
                    i++;
                    if (debug)
                    {
                        Console.WriteLine("Edittext -- Unknown1=" + tl.Unknown1);
                        Console.WriteLine("Edittext -- rectangle=" + BitConverter.ToString(tl.rectangle.totalbytes));
                        Console.WriteLine("Edittext -- Unknown2=" + tl.Unknown2);
                        Console.WriteLine("Edittext -- Unknown3=" + tl.Unknown3);
                        Console.WriteLine("Edittext -- Color=" + BitConverter.ToString(tl.Color.totalbytes));
                        Console.Write("Edittext -- Unknown4={");
                        foreach (int item in tl.Unknown4)
                        {
                            Console.Write(item + ", ");
                        }
                        Console.Write("}");
                        Console.WriteLine("");
                        Console.WriteLine("Edittext -- htmlTextFormat=" + tl.htmlTextFormat);
                    }
                }
            }

            void CheckFontNames(byte[] Data)
            {
                int i = 1;
                while (i <= fui2.header.fuiFontNameCount)
                {
                    FourJUserInterface.FontName tl = new FourJUserInterface.FontName();
                    tl.Unknown1 = BitConverter.ToInt32(Data.Skip(offset).Take(4).ToArray(), 0);
                    tl.Fontname = Encoding.UTF8.GetString(Data.Skip(offset + (int)0x4).Take((int)0x40).ToArray());
                    tl.Unknown2 = BitConverter.ToInt32(Data.Skip(offset + (int)0x44).Take(4).ToArray(), 0);
                    tl.Unknown3 = Encoding.UTF8.GetString(Data.Skip(offset + (int)0x48).Take((int)0x40).ToArray());
                    tl.Unknown4 = Data.Skip(offset + (int)0x88).Take(8).ToArray();
                    tl.Unknown5 = Encoding.UTF8.GetString(Data.Skip(offset + (int)0x90).Take((int)0x40).ToArray());
                    tl.Unknown6 = Data.Skip(offset + (int)0xd0).Take(8).ToArray();
                    tl.Unknown7 = Encoding.UTF8.GetString(Data.Skip(offset + (int)0xd8).Take((int)0x2c).ToArray());
                    fui2.fontNames.Add(tl);
                    offset += (int)0x104;
                    i++;
                    if (debug)
                    {
                        Console.WriteLine("FontName -- Unknown1=" + tl.Unknown1);
                        Console.WriteLine("FontName -- Fontname=" + tl.Fontname);
                        Console.WriteLine("FontName -- Unknown2=" + tl.Unknown2);
                        Console.WriteLine("FontName -- Unknown3=" + tl.Unknown3);
                        Console.Write("FontName -- Unknown4={");
                        foreach (int item in tl.Unknown4)
                        {
                            Console.Write(item + ", ");
                        }
                        Console.Write("}");
                        Console.WriteLine("");
                        Console.WriteLine("FontName -- Unknown5=" + tl.Unknown5);
                        Console.Write("FontName -- Unknown6={");
                        foreach (int item in tl.Unknown6)
                        {
                            Console.Write(item + ", ");
                        }
                        Console.Write("}");
                        Console.WriteLine("");
                        Console.WriteLine("FontName -- Unknown7=" + tl.Unknown7);
                    }
                }
            }

            void CheckSymbols(byte[] Data)
            {

                int i = 1;
                while (i <= fui2.header.fuiSymbolCount)
                {
                    FourJUserInterface.Symbol tl = new FourJUserInterface.Symbol();
                    tl.SymbolName = Encoding.UTF8.GetString(Data.Skip(offset).Take((int)0x40).ToArray());
                    tl.ObjectType = BitConverter.ToInt32(Data.Skip(offset + (int)0x40).Take(4).ToArray(), 0);
                    tl.Unknown = BitConverter.ToInt32(Data.Skip(offset + (int)0x44).Take(4).ToArray(), 0);
                    fui2.symbols.Add(tl);
                    offset += (int)0x48;
                    i++;
                    if (debug)
                    {
                        Console.WriteLine("Symbol -- SymbolName=" + tl.SymbolName);
                        Console.WriteLine("Symbol -- ObjectType=" + tl.ObjectType);
                        Console.WriteLine("Symbol -- Unknown=" + tl.Unknown);
                        Console.WriteLine("Offset: " + offset);
                    }

                    if (tl.ObjectType == 3 || tl.ObjectType == 4 || tl.ObjectType == 1)
                        fui2.ImageNames.Add(tl.SymbolName);
                }
            }

            void CheckImportAssets(byte[] Data)
            {
                int i = 1;
                while (i <= fui2.header.fuiImportAssetCount)
                {
                    FourJUserInterface.ImportAsset tl = new FourJUserInterface.ImportAsset();
                    tl.AssetName = Encoding.UTF8.GetString(Data.Skip(offset).Take((int)0x40).ToArray());
                    fui2.importAssets.Add(tl);
                    offset += (int)0x40;
                    i++;
                    if (debug)
                    {
                        Console.WriteLine("ImportAsset -- AssetName=" + tl.AssetName);
                    }
                }

            }

            void CheckBitmaps(byte[] Data, int BitmapCount)
            {
                int i = 1;
                Console.WriteLine("Offset: " + offset);
                int StoredOffset = offset + ((int)0x20* fui2.header.fuiBitmapCount);
                while (i <= fui2.header.fuiBitmapCount)
                {
                    try
                    {
                        FourJUserInterface.Bitmap tl = new FourJUserInterface.Bitmap();
                        List<byte> Dat1 = Data.Skip(offset).Take(4).ToList();
                        List<byte> Dat2 = Data.Skip(offset + (int)0x4).Take(4).ToList();
                        List<byte> Dat3 = Data.Skip(offset + (int)0x8).Take(4).ToList();
                        List<byte> Dat4 = Data.Skip(offset + (int)0xc).Take(4).ToList();
                        List<byte> Dat5 = Data.Skip(offset + (int)0x10).Take(4).ToList();
                        List<byte> Dat6 = Data.Skip(offset + (int)0x14).Take(4).ToList();
                        List<byte> Dat7 = Data.Skip(offset + (int)0x18).Take(4).ToList();
                        List<byte> Dat8 = Data.Skip(offset + (int)0x1c).Take(4).ToList();
                        List<byte> DatX = Data.Skip(offset).Take((int)0x20).ToList();


                        tl.Unknown1 = (Dat1.ToArray());
                        tl.ObjectType = BitConverter.ToInt32(Dat2.ToArray(), 0);
                        tl.ScaleWidth = BitConverter.ToInt32(Dat3.ToArray(), 0);
                        tl.ScaleHeight = BitConverter.ToInt32(Dat4.ToArray(), 0);
                        tl.Size1 = (Dat5.ToArray());
                        tl.Size2 = (Dat6.ToArray());
                        tl.Unknown2 = (Dat7.ToArray());
                        tl.Unknown3 = BitConverter.ToInt32(Dat8.ToArray(), 0);
                        tl.BigDat = DatX.ToArray();

                        if((BitConverter.ToInt32(Dat5.ToArray(), 0) >= 500000000) || (BitConverter.ToInt32(Dat6.ToArray(), 0) >= 500000000))
                        {
                            byte[] NewDat1 = new byte[] { Dat5.ToArray()[3], Dat5.ToArray()[2], Dat5.ToArray()[1], Dat5.ToArray()[0], };
                            byte[] NewDat2 = new byte[] { Dat6.ToArray()[3], Dat6.ToArray()[2], Dat6.ToArray()[1], Dat6.ToArray()[0], };

                            int imgOff = BitConverter.ToInt32(NewDat1, 0);
                            int imgSize = BitConverter.ToInt32(NewDat2, 0);

                            byte[] Dat = Data.Skip(StoredOffset + imgOff).Take(imgSize).ToArray();

                            tl.Image = Dat;
                        }
                        else
                        {
                            int imgOff = BitConverter.ToInt32(Dat5.ToArray(), 0);
                            int imgSize = BitConverter.ToInt32(Dat6.ToArray(), 0);

                            byte[] Dat = Data.Skip(StoredOffset + imgOff).Take(imgSize).ToArray();

                            tl.Image = Dat;
                        }

                        fui2.bitmaps.Add(tl);
                        offset += (int)0x20;

                        //Get Images
                        //fui2.Images.Add(Data.Skip(offset).Take(tl.Size2).ToArray());
                        //offset += tl.Size2;

                        i++; if (debug)
                        {
                            Console.WriteLine("Bitmap -- Unknown1=" + tl.Unknown1);
                            Console.WriteLine("Bitmap -- ObjectType=" + tl.ObjectType);
                            Console.WriteLine("Bitmap -- ScaleWidth=" + tl.ScaleWidth);
                            Console.WriteLine("Bitmap -- ScaleHeight=" + tl.ScaleHeight);
                            Console.WriteLine("Bitmap -- Size1=" + BitConverter.ToString(tl.Size1, 0));
                            Console.WriteLine("Bitmap -- Size2=" + BitConverter.ToString(tl.Size2, 0));
                            Console.WriteLine("Bitmap -- Unknown2=" + tl.Unknown2);
                            Console.WriteLine("Bitmap -- Offset=" + offset);
                        }
                    }
                    catch
                    {
                        MessageBox.Show(94932 + (int)0x20 + "\n" + offset + "");
                        break;
                    }
                }
                foreach (FourJUserInterface.Bitmap bmp in fui2.bitmaps)
                {
                    byte[] DataX = Data.Skip(offset).Take(BitConverter.ToInt32(bmp.Size2, 0)).ToArray();
                    MemoryStream ms = new MemoryStream(DataX, 0, DataX.Length);
                    System.Drawing.Image img = (System.Drawing.Bitmap.FromStream(ms));
                    fui2.Images.Add(DataX);

                    offset += BitConverter.ToInt32(bmp.Size2, 0);
                }
            }

            public bool SaveFUI(string path, FUI fjui)
            {
                try
                {
                    Header header = fjui.header;

                    List<byte> Output = new List<byte>();
                    List<byte> TimelineCount = new List<byte>();
                    List<byte> TimelineActionCount = new List<byte>();
                    List<byte> ShapeCount = new List<byte>();
                    List<byte> ShapeComponentCount = new List<byte>();
                    List<byte> VertCount = new List<byte>();
                    List<byte> TimelineFrameCount = new List<byte>();
                    List<byte> TimelineEventCount = new List<byte>();
                    List<byte> TimelineEventNameCount = new List<byte>();
                    List<byte> ReferenceCount = new List<byte>();
                    List<byte> EdittextCount = new List<byte>();
                    List<byte> FontNameCount = new List<byte>();
                    List<byte> SymbolCount = new List<byte>();
                    List<byte> ImportAssetCount = new List<byte>();
                    List<byte> BitmapCount = new List<byte>();

                    #region writing Header

                    Output.AddRange(Encoding.ASCII.GetBytes(fjui.header.Identifier));
                    Output.AddRange(BitConverter.GetBytes(fjui.header.Unknow));
                    Output.AddRange(BitConverter.GetBytes(fjui.header.ContentSize));
                    Output.AddRange(Encoding.ASCII.GetBytes(fjui.header.SwfFileName));
                    Output.AddRange(BitConverter.GetBytes(fjui.header.fuiTimelineCount));
                    Output.AddRange(BitConverter.GetBytes(fjui.header.fuiTimelineEventNameCount));
                    Output.AddRange(BitConverter.GetBytes(fjui.header.fuiTimelineActionCount));
                    Output.AddRange(BitConverter.GetBytes(fjui.header.fuiShapeCount));
                    Output.AddRange(BitConverter.GetBytes(fjui.header.fuiShapeComponentCount));
                    Output.AddRange(BitConverter.GetBytes(fjui.header.fuiVertCount));
                    Output.AddRange(BitConverter.GetBytes(fjui.header.fuiTimelineFrameCount));
                    Output.AddRange(BitConverter.GetBytes(fjui.header.fuiTimelineEventCount));
                    Output.AddRange(BitConverter.GetBytes(fjui.header.fuiReferenceCount));
                    Output.AddRange(BitConverter.GetBytes(fjui.header.fuiEdittextCount));
                    Output.AddRange(BitConverter.GetBytes(fjui.header.fuiSymbolCount));
                    Output.AddRange(BitConverter.GetBytes(fjui.header.fuiBitmapCount));
                    Output.AddRange(BitConverter.GetBytes(fjui.header.imagesSize));
                    Output.AddRange(BitConverter.GetBytes(fjui.header.fuiFontNameCount));
                    Output.AddRange(BitConverter.GetBytes(fjui.header.fuiImportAssetCount));
                    Output.AddRange((fjui.header.FrameSize));
                    #endregion
                    if (header.fuiTimelineCount > 0)
                    {
                        int i = 0;
                        while (i < header.fuiTimelineCount)
                        {
                            Output.AddRange(BitConverter.GetBytes(fjui.timelines[i].ObjectType));
                            Output.AddRange(BitConverter.GetBytes(fjui.timelines[i].FrameIndex));
                            Output.AddRange(BitConverter.GetBytes(fjui.timelines[i].FrameCount));
                            Output.AddRange(BitConverter.GetBytes(fjui.timelines[i].ActionIndex));
                            Output.AddRange(BitConverter.GetBytes(fjui.timelines[i].ActionCount));
                            Output.AddRange(fjui.timelines[i].Rectangle.totalbytes);
                            TimelineCount.AddRange(BitConverter.GetBytes(fjui.timelines[i].ObjectType));
                            TimelineCount.AddRange(BitConverter.GetBytes(fjui.timelines[i].FrameIndex));
                            TimelineCount.AddRange(BitConverter.GetBytes(fjui.timelines[i].FrameCount));
                            TimelineCount.AddRange(BitConverter.GetBytes(fjui.timelines[i].ActionIndex));
                            TimelineCount.AddRange(BitConverter.GetBytes(fjui.timelines[i].ActionCount));
                            TimelineCount.AddRange(fjui.timelines[i].Rectangle.totalbytes);
                            i++;
                        }
                    }
                    if (header.fuiTimelineActionCount > 0)
                    {
                        int i = 0;
                        while (i < header.fuiTimelineActionCount)
                        {
                            Output.AddRange(fjui.timelineActions[i].totalbytes);
                            Output.AddRange(Encoding.ASCII.GetBytes(fjui.timelineActions[i].UnknownName1));
                            Output.AddRange(Encoding.ASCII.GetBytes(fjui.timelineActions[i].UnknownName2));
                            TimelineActionCount.AddRange(fjui.timelineActions[i].totalbytes);
                            TimelineActionCount.AddRange(Encoding.ASCII.GetBytes(fjui.timelineActions[i].UnknownName1));
                            TimelineActionCount.AddRange(Encoding.ASCII.GetBytes(fjui.timelineActions[i].UnknownName2));
                            i++;
                        }
                    }
                    if (header.fuiShapeCount > 0)
                    {
                        int i = 0;
                        while (i < header.fuiShapeCount)
                        {
                            Output.AddRange(BitConverter.GetBytes(fjui.shapes[i].UnknownValue1));
                            Output.AddRange(BitConverter.GetBytes(fjui.shapes[i].UnknownValue2));
                            Output.AddRange(BitConverter.GetBytes(fjui.shapes[i].ObjectType));
                            Output.AddRange((fjui.shapes[i].Rectangle.totalbytes));
                            ShapeCount.AddRange(BitConverter.GetBytes(fjui.shapes[i].UnknownValue1));
                            ShapeCount.AddRange(BitConverter.GetBytes(fjui.shapes[i].UnknownValue2));
                            ShapeCount.AddRange(BitConverter.GetBytes(fjui.shapes[i].ObjectType));
                            ShapeCount.AddRange((fjui.shapes[i].Rectangle.totalbytes));
                            i++;
                        }
                    }
                    if (header.fuiShapeComponentCount > 0)
                    {
                        int i = 0;
                        while (i < header.fuiShapeComponentCount)
                        {
                            Output.AddRange((fjui.shapeComponents[i].FillInfo.totalbytes));
                            Output.AddRange(BitConverter.GetBytes(fjui.shapeComponents[i].UnknownValue1));
                            Output.AddRange(BitConverter.GetBytes(fjui.shapeComponents[i].UnknownValue2));
                            ShapeComponentCount.AddRange((fjui.shapeComponents[i].FillInfo.totalbytes));
                            ShapeComponentCount.AddRange(BitConverter.GetBytes(fjui.shapeComponents[i].UnknownValue1));
                            ShapeComponentCount.AddRange(BitConverter.GetBytes(fjui.shapeComponents[i].UnknownValue2));
                            i++;
                        }
                    }
                    if (header.fuiVertCount > 0)
                    {
                        int i = 0;
                        while (i < header.fuiVertCount)
                        {
                            Output.AddRange((fjui.verts[i].x));
                            Output.AddRange((fjui.verts[i].y));
                            VertCount.AddRange((fjui.verts[i].x));
                            VertCount.AddRange((fjui.verts[i].y));
                            i++;
                        }
                    }
                    if (header.fuiTimelineFrameCount > 0)
                    {
                        int i = 0;
                        while (i < header.fuiTimelineFrameCount)
                        {
                            Output.AddRange(Encoding.ASCII.GetBytes(fjui.timelineFrames[i].FrameName));
                            Output.AddRange(BitConverter.GetBytes(fjui.timelineFrames[i].EventIndex));
                            Output.AddRange(BitConverter.GetBytes(fjui.timelineFrames[i].EventCount));
                            TimelineFrameCount.AddRange(Encoding.ASCII.GetBytes(fjui.timelineFrames[i].FrameName));
                            TimelineFrameCount.AddRange(BitConverter.GetBytes(fjui.timelineFrames[i].EventIndex));
                            TimelineFrameCount.AddRange(BitConverter.GetBytes(fjui.timelineFrames[i].EventCount));
                            i++;
                        }
                    }
                    if (header.fuiTimelineEventCount > 0)
                    {
                        int i = 0;
                        while (i < header.fuiTimelineEventCount)
                        {
                            Output.AddRange(fjui.timelineEvents[i].totalbytes);
                            Output.AddRange(fjui.timelineEvents[i].matrix.totalbytes);
                            Output.AddRange(fjui.timelineEvents[i].ColorTransform.totalbytes);
                            Output.AddRange(fjui.timelineEvents[i].Color.totalbytes);
                            TimelineEventCount.AddRange(fjui.timelineEvents[i].totalbytes);
                            TimelineEventCount.AddRange(fjui.timelineEvents[i].matrix.totalbytes);
                            TimelineEventCount.AddRange(fjui.timelineEvents[i].ColorTransform.totalbytes);
                            TimelineEventCount.AddRange(fjui.timelineEvents[i].Color.totalbytes);
                            i++;
                        }
                    }
                    if (header.fuiTimelineEventNameCount > 0)
                    {
                        int i = 0;
                        while (i < header.fuiTimelineEventNameCount)
                        {
                            Output.AddRange(Encoding.ASCII.GetBytes(fjui.timelineEventNames[i].EventName));
                            TimelineEventNameCount.AddRange(Encoding.ASCII.GetBytes(fjui.timelineEventNames[i].EventName));
                            i++;
                        }
                    }
                    if (header.fuiReferenceCount > 0)
                    {
                        int i = 0;
                        while (i < header.fuiReferenceCount)
                        {
                            Output.AddRange(BitConverter.GetBytes(fjui.references[i].SymbolIndex));
                            Output.AddRange(Encoding.ASCII.GetBytes(fjui.references[i].ReferenceName));
                            Output.AddRange((fjui.references[i].Index));
                            ReferenceCount.AddRange(BitConverter.GetBytes(fjui.references[i].SymbolIndex));
                            ReferenceCount.AddRange(Encoding.ASCII.GetBytes(fjui.references[i].ReferenceName));
                            ReferenceCount.AddRange((fjui.references[i].Index));
                            i++;
                        }
                    }
                    if (header.fuiEdittextCount > 0)
                    {
                        int i = 0;
                        while (i < header.fuiEdittextCount)
                        {
                            Output.AddRange(BitConverter.GetBytes(fjui.edittexts[i].Unknown1));
                            Output.AddRange((fjui.edittexts[i].rectangle.totalbytes));
                            Output.AddRange(BitConverter.GetBytes(fjui.edittexts[i].Unknown2));
                            Output.AddRange((fjui.edittexts[i].Unknown3));
                            Output.AddRange((fjui.edittexts[i].Color.totalbytes));
                            Output.AddRange((fjui.edittexts[i].Unknown4));
                            Output.AddRange(Encoding.ASCII.GetBytes(fjui.edittexts[i].htmlTextFormat));
                            EdittextCount.AddRange(BitConverter.GetBytes(fjui.edittexts[i].Unknown1));
                            EdittextCount.AddRange((fjui.edittexts[i].rectangle.totalbytes));
                            EdittextCount.AddRange(BitConverter.GetBytes(fjui.edittexts[i].Unknown2));
                            EdittextCount.AddRange((fjui.edittexts[i].Unknown3));
                            EdittextCount.AddRange((fjui.edittexts[i].Color.totalbytes));
                            EdittextCount.AddRange((fjui.edittexts[i].Unknown4));
                            EdittextCount.AddRange(Encoding.ASCII.GetBytes(fjui.edittexts[i].htmlTextFormat));
                            i++;
                        }
                    }
                    if (header.fuiFontNameCount > 0)
                    {
                        int i = 0;
                        while (i < header.fuiFontNameCount)
                        {
                            Output.AddRange(BitConverter.GetBytes(fjui.fontNames[i].Unknown1));
                            Output.AddRange(Encoding.ASCII.GetBytes(fjui.fontNames[i].Fontname));
                            Output.AddRange(BitConverter.GetBytes(fjui.fontNames[i].Unknown2));
                            Output.AddRange(Encoding.ASCII.GetBytes(fjui.fontNames[i].Unknown3));
                            Output.AddRange((fjui.fontNames[i].Unknown4));
                            Output.AddRange(Encoding.ASCII.GetBytes(fjui.fontNames[i].Unknown5));
                            Output.AddRange((fjui.fontNames[i].Unknown6));
                            Output.AddRange(Encoding.ASCII.GetBytes(fjui.fontNames[i].Unknown7));
                            FontNameCount.AddRange(BitConverter.GetBytes(fjui.fontNames[i].Unknown1));
                            FontNameCount.AddRange(Encoding.ASCII.GetBytes(fjui.fontNames[i].Fontname));
                            FontNameCount.AddRange(BitConverter.GetBytes(fjui.fontNames[i].Unknown2));
                            FontNameCount.AddRange(Encoding.ASCII.GetBytes(fjui.fontNames[i].Unknown3));
                            FontNameCount.AddRange((fjui.fontNames[i].Unknown4));
                            FontNameCount.AddRange(Encoding.ASCII.GetBytes(fjui.fontNames[i].Unknown5));
                            FontNameCount.AddRange((fjui.fontNames[i].Unknown6));
                            FontNameCount.AddRange(Encoding.ASCII.GetBytes(fjui.fontNames[i].Unknown7));
                            i++;
                        }
                    }
                    if (header.fuiSymbolCount > 0)
                    {
                        int i = 0;
                        while (i < header.fuiSymbolCount)
                        {
                            Output.AddRange(Encoding.ASCII.GetBytes(fjui.symbols[i].SymbolName));
                            Output.AddRange(BitConverter.GetBytes(fjui.symbols[i].ObjectType));
                            Output.AddRange(BitConverter.GetBytes(fjui.symbols[i].Unknown));
                            SymbolCount.AddRange(Encoding.ASCII.GetBytes(fjui.symbols[i].SymbolName));
                            SymbolCount.AddRange(BitConverter.GetBytes(fjui.symbols[i].ObjectType));
                            SymbolCount.AddRange(BitConverter.GetBytes(fjui.symbols[i].Unknown));
                            i++;
                        }
                    }
                    if (header.fuiImportAssetCount > 0)
                    {
                        int i = 0;
                        while (i < header.fuiImportAssetCount)
                        {
                            Output.AddRange(Encoding.ASCII.GetBytes(fjui.importAssets[i].AssetName));
                            ImportAssetCount.AddRange(Encoding.ASCII.GetBytes(fjui.importAssets[i].AssetName));
                            i++;
                        }
                    }
                    if (header.fuiBitmapCount > 0)
                    {
                        int i = 0;
                        while (i < header.fuiBitmapCount)
                        {
                            try
                            {
                                Output.AddRange((fjui.bitmaps[i].Unknown1));
                                Output.AddRange(BitConverter.GetBytes(fjui.bitmaps[i].ObjectType));
                                Output.AddRange(BitConverter.GetBytes(fjui.bitmaps[i].ScaleWidth));
                                Output.AddRange(BitConverter.GetBytes(fjui.bitmaps[i].ScaleHeight));
                                Output.AddRange((fjui.bitmaps[i].Size1));
                                Output.AddRange((fjui.bitmaps[i].Size2));
                                Output.AddRange((fjui.bitmaps[i].Unknown2));
                                Output.AddRange(BitConverter.GetBytes(fjui.bitmaps[i].Unknown3));
                                BitmapCount.AddRange((fjui.bitmaps[i].Unknown1));
                                BitmapCount.AddRange(BitConverter.GetBytes(fjui.bitmaps[i].ObjectType));
                                BitmapCount.AddRange(BitConverter.GetBytes(fjui.bitmaps[i].ScaleWidth));
                                BitmapCount.AddRange(BitConverter.GetBytes(fjui.bitmaps[i].ScaleHeight));
                                BitmapCount.AddRange((fjui.bitmaps[i].Size1));
                                BitmapCount.AddRange((fjui.bitmaps[i].Size2));
                                BitmapCount.AddRange((fjui.bitmaps[i].Unknown2));
                                BitmapCount.AddRange(BitConverter.GetBytes(fjui.bitmaps[i].Unknown3));
                                /*
                                    Output.AddRange((fjui.bitmaps[i].BigDat));
                                    BitmapCount.AddRange((fjui.bitmaps[i].BigDat));*/
                            }
                            catch { }
                            i++;
                        }
                    }
                    int x = 0;

                    foreach (FourJUserInterface.Bitmap bmp in fjui.bitmaps)
                    {
                        Output.AddRange(bmp.Image);
                    }



                    /*
                    foreach (byte[] dat in fjui.Images)
                    {
                        Output.AddRange(dat);
                        byte[] PNG = { 0x50, 0x4E, 0x47 };
                        byte[] JPG = { 0xFF, 0xD8, 0xFF, 0xE1 };
                        fjui.ImageNames.Reverse();
                        string nom = fjui.ImageNames[x].Replace("_", "").Replace(" ", "").Replace(System.Text.Encoding.ASCII.GetString(new byte[] { 00 }), "");
                        Console.WriteLine(nom);
                        string Start = Encoding.ASCII.GetString(dat.Skip(0).Take(4).ToArray());
                        string LocalDir = path.Replace(".fui", "") + "\\images";
                        Directory.CreateDirectory(LocalDir);
                        if (Start.StartsWith("?PNG"))
                            File.WriteAllBytes(LocalDir + "\\" + nom + ".png", dat);
                        if (dat.Skip(0).Take(4) == JPG)
                            File.WriteAllBytes(LocalDir + "\\" + nom + ".jpg", dat);
                        x++;
                    }
                    */
                    File.WriteAllBytes(path, Output.ToArray());
                    if (debug)
                    {
                        Directory.CreateDirectory(path.Replace(".fui", ""));
                        File.WriteAllBytes(path.Replace(".fui", "") + "\\1" + Path.GetFileName(path.Replace(".fui", ".Timeline")), TimelineCount.ToArray());
                        File.WriteAllBytes(path.Replace(".fui", "") + "\\2" + Path.GetFileName(path.Replace(".fui", ".TimelineAction")), TimelineActionCount.ToArray());
                        File.WriteAllBytes(path.Replace(".fui", "") + "\\3" + Path.GetFileName(path.Replace(".fui", ".Shape")), ShapeCount.ToArray());
                        File.WriteAllBytes(path.Replace(".fui", "") + "\\4" + Path.GetFileName(path.Replace(".fui", ".ShapeComponent")), ShapeComponentCount.ToArray());
                        File.WriteAllBytes(path.Replace(".fui", "") + "\\5" + Path.GetFileName(path.Replace(".fui", ".Vert")), VertCount.ToArray());
                        File.WriteAllBytes(path.Replace(".fui", "") + "\\6" + Path.GetFileName(path.Replace(".fui", ".TimelineFrame")), TimelineFrameCount.ToArray());
                        File.WriteAllBytes(path.Replace(".fui", "") + "\\7" + Path.GetFileName(path.Replace(".fui", ".TimelineEvent")), TimelineEventCount.ToArray());
                        File.WriteAllBytes(path.Replace(".fui", "") + "\\8" + Path.GetFileName(path.Replace(".fui", ".TimelineEventName")), TimelineEventNameCount.ToArray());
                        File.WriteAllBytes(path.Replace(".fui", "") + "\\9" + Path.GetFileName(path.Replace(".fui", ".Reference")), ReferenceCount.ToArray());
                        File.WriteAllBytes(path.Replace(".fui", "") + "\\10" + Path.GetFileName(path.Replace(".fui", ".Edittext")), EdittextCount.ToArray());
                        File.WriteAllBytes(path.Replace(".fui", "") + "\\11" + Path.GetFileName(path.Replace(".fui", ".FontName")), FontNameCount.ToArray());
                        File.WriteAllBytes(path.Replace(".fui", "") + "\\12" + Path.GetFileName(path.Replace(".fui", ".Symbol")), SymbolCount.ToArray());
                        File.WriteAllBytes(path.Replace(".fui", "") + "\\13" + Path.GetFileName(path.Replace(".fui", ".ImportAsset")), ImportAssetCount.ToArray());
                        File.WriteAllBytes(path.Replace(".fui", "") + "\\14" + Path.GetFileName(path.Replace(".fui", ".Bitmap")), BitmapCount.ToArray());
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

    }
}
