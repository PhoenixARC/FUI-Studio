using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using static FourJ.UserInterface;
using FUI_Studio.Classes.fui;

namespace FUI_Studio.Forms
{
    public partial class LoadingFileDialog : Form
    {
        private FUIFile _fuiFile;
        private byte[] _objectData;

        public LoadingFileDialog(ref FUIFile fuiFile, byte[] objectData)
        {
            InitializeComponent();
            _fuiFile = fuiFile;
            _objectData = objectData;
            progressBar.Value = 0;
            progressBar.Maximum = fuiFile.header.GetObjectCountSum()-1;
            FUIFileNameLabel.Text = "Start Loading...";
        }

        private void OnLoad(object sender, EventArgs e)
        {
            backgroundWorker.RunWorkerAsync(_objectData);
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] data = (byte[])e.Argument;
            int offset = 0;

            ParseObjectBuffer
                (CopyData(data, _fuiFile.header.TimelineCount * 0x1c, ref offset),
                ref _fuiFile.timelines, _fuiFile.header.TimelineCount, 0x1c);

            ParseObjectBuffer
                (CopyData(data, _fuiFile.header.TimelineActionCount * 0x84, ref offset),
                ref _fuiFile.timelineActions, _fuiFile.header.TimelineActionCount, 0x84);

            ParseObjectBuffer
                (CopyData(data, _fuiFile.header.ShapeCount * 0x1c, ref offset),
                ref _fuiFile.shapes, _fuiFile.header.ShapeCount, 0x1c);

            ParseObjectBuffer
                (CopyData(data, _fuiFile.header.ShapeComponentCount * 0x2c, ref offset),
                ref _fuiFile.shapeComponents, _fuiFile.header.ShapeComponentCount, 0x2c);

            ParseObjectBuffer
                (CopyData(data, _fuiFile.header.VertCount * 0x8, ref offset),
                ref _fuiFile.verts, _fuiFile.header.VertCount, 0x8);

            ParseObjectBuffer
                (CopyData(data, _fuiFile.header.TimelineFrameCount * 0x48, ref offset),
                ref _fuiFile.timelineFrames, _fuiFile.header.TimelineFrameCount, 0x48);

            ParseObjectBuffer
                (CopyData(data, _fuiFile.header.TimelineEventCount * 0x48, ref offset),
                ref _fuiFile.timelineEvents, _fuiFile.header.TimelineEventCount, 0x48);

            ParseObjectBuffer
                (CopyData(data, _fuiFile.header.TimelineEventNameCount * 0x40, ref offset),
                ref _fuiFile.timelineEventNames, _fuiFile.header.TimelineEventNameCount, 0x40);

            ParseObjectBuffer
                (CopyData(data, _fuiFile.header.ReferenceCount * 0x48, ref offset),
                ref _fuiFile.references, _fuiFile.header.ReferenceCount, 0x48);

            ParseObjectBuffer
                (CopyData(data, _fuiFile.header.EdittextCount * 0x138, ref offset),
                ref _fuiFile.edittexts, _fuiFile.header.EdittextCount, 0x138);

            ParseObjectBuffer
                (CopyData(data, _fuiFile.header.FontNameCount * 0x104, ref offset),
                ref _fuiFile.fontNames, _fuiFile.header.FontNameCount, 0x104);

            ParseObjectBuffer
                (CopyData(data, _fuiFile.header.SymbolCount * 0x48, ref offset),
                ref _fuiFile.symbols, _fuiFile.header.SymbolCount, 0x48);

            ParseObjectBuffer
                (CopyData(data, _fuiFile.header.ImportAssetCount * 0x40, ref offset),
                ref _fuiFile.importAssets, _fuiFile.header.ImportAssetCount, 0x40);

            ParseObjectBuffer
                (CopyData(data, _fuiFile.header.BitmapCount * 0x20, ref offset),
                ref _fuiFile.bitmaps, _fuiFile.header.BitmapCount, 0x20);
        }

        private void ParseObjectBuffer<T>(byte[] ObjectBuffer, ref List<T> ObjectList, int ElementCount, int ElementSize) where T : IFuiObject, new()
        {
            for (int offset = 0; offset < ElementCount * ElementSize;)
            {
                T fuiObject = new T();
                fuiObject.Parse(CopyData(ObjectBuffer, ElementSize, ref offset));
                ObjectList.Add(fuiObject);
            }
            Invoke((MethodInvoker)delegate
            {
                progressBar.Value += ElementCount;
                FUIFileNameLabel.Text = $"Loading FUI File Objects: {progressBar.Value}/{progressBar.Maximum}";
            });
        }

        private byte[] CopyData(byte[] Source, int Size, ref int Offset)
        {
            byte[] Buffer = new byte[Size];
            Array.Copy(Source, Offset, Buffer, 0, Size);
            Offset += Size;
            return Buffer;
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Thread.Sleep(500);
            Close();
        }
    }
}

