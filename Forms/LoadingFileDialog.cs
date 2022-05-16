using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using static FourJ.UserInterface;
using FUI_Studio.Classes.fui;
using System.IO;
using System.Linq;

namespace FUI_Studio.Forms
{
    public partial class LoadingFileDialog : Form
    {
        private FUIFile _fuiFile;
        private byte[] _objectData;

        public LoadingFileDialog(FUIFile fuiFile, byte[] objectData)
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

            var MemStream = new MemoryStream(data);

            ParseObjectBuffer
                (MemStream,
                ref _fuiFile.timelines, _fuiFile.header.TimelineCount);

            ParseObjectBuffer
                (MemStream,
                ref _fuiFile.timelineActions, _fuiFile.header.TimelineActionCount);

            ParseObjectBuffer
                (MemStream,
                ref _fuiFile.shapes, _fuiFile.header.ShapeCount);

            ParseObjectBuffer
                (MemStream,
                ref _fuiFile.shapeComponents, _fuiFile.header.ShapeComponentCount);

            ParseObjectBuffer
                (MemStream,
                ref _fuiFile.verts, _fuiFile.header.VertCount);

            ParseObjectBuffer
                (MemStream,
                ref _fuiFile.timelineFrames, _fuiFile.header.TimelineFrameCount);

            ParseObjectBuffer
                (MemStream,
                ref _fuiFile.timelineEvents, _fuiFile.header.TimelineEventCount);

            ParseObjectBuffer
                (MemStream,
                ref _fuiFile.timelineEventNames, _fuiFile.header.TimelineEventNameCount);

            ParseObjectBuffer
                (MemStream,
                ref _fuiFile.references, _fuiFile.header.ReferenceCount);

            ParseObjectBuffer
                (MemStream,
                ref _fuiFile.edittexts, _fuiFile.header.EdittextCount);

            ParseObjectBuffer
                (MemStream,
                ref _fuiFile.fontNames, _fuiFile.header.FontNameCount);

            ParseObjectBuffer
                (MemStream,
                ref _fuiFile.symbols, _fuiFile.header.SymbolCount);

            ParseObjectBuffer
                (MemStream,
                ref _fuiFile.importAssets, _fuiFile.header.ImportAssetCount);

            ParseObjectBuffer
                (MemStream,
                ref _fuiFile.bitmaps, _fuiFile.header.BitmapCount);
        }

        private void ParseObjectBuffer<T>(Stream buffer, ref List<T> ObjectList, int ElementCount) where T : IFuiObject, new()
        {
            for (int i = 0; i < ElementCount; i++)
            {
                T fuiObject = new T();
                byte[] ObjectBuffer = new byte[fuiObject.GetByteSize()];
                buffer.Read(ObjectBuffer, 0, fuiObject.GetByteSize());
                fuiObject.Parse(ObjectBuffer);
                ObjectList.Add(fuiObject);
            }
            Invoke((MethodInvoker)delegate
            {
                progressBar.Value += ElementCount;
                FUIFileNameLabel.Text = $"Loading FUI File Objects: {progressBar.Value}/{progressBar.Maximum}";
            });
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DialogResult = DialogResult.OK;
            Thread.Sleep(500);
            Close();
        }
    }
}

