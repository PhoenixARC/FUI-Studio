using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FUI_Studio.Forms
{
    public partial class Form1 : Form
    {
        public Form1(string[] args, bool labels, bool font, bool references, bool Images, bool cansaveElements)
        {
            InitializeComponent();
            DisplayLabels = labels;
            DisplayFont = font;
            LoadReferences = references;
            Loadimages = Images;
            saveElements = cansaveElements;

            if (args.Length > 0 && File.Exists(args[0]) && args[0].EndsWith(".fui"))
                OpenFUI(args[0], false);
        }

        #region variables

        public List<string> imgList = new List<string>();

        bool DisplayLabels = false;
        bool DisplayFont = false;
        bool LoadReferences = false;
        bool Loadimages = false;
        bool saveElements = false;

        List<FourJ.FourJUserInterface.FUI> Fuis = new List<FourJ.FourJUserInterface.FUI>();
        FourJ.FourJUserInterface.Functions Funct = new FourJ.FourJUserInterface.Functions();

        List<int[]> startEnds = new List<int[]>();

        #endregion

        #region Opening FUIs

        public void OpenFUI(string fui, bool IsReference)
        {
            Classes.LoadFUI.OpenFUI(fui, IsReference, treeView1, DisplayLabels, DisplayFont, startEnds, imgList, LoadReferences, Loadimages);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FourJ.FourJUserInterface.FUI FUI = new FourJ.FourJUserInterface.FUI();
            OpenFileDialog opf = new OpenFileDialog();
            if (opf.ShowDialog() == DialogResult.OK)
            {
                //OpenFUI(opf.FileName, false);
                Fuis.Clear();
                LoadingFileDialog lfd = new LoadingFileDialog(FUI, opf.FileName);
                lfd.ShowDialog();
            }
            Classes.LoadFUI.OpenFUINew(FUI, false, treeView1, 0);
            Fuis.Add(FUI);
        }

        #endregion

        #region TreeView Selection
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            checkBox1.Checked = false;
            richTextBox1.Text = null;
            if (treeView1.SelectedNode.Text.EndsWith(".swf"))
            {

                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Tag.ToString());
                FourJ.FourJUserInterface.Header tl = Fuis[FuiNum].header;
                string Output = "";

                Output += ("Identifier:" + (tl.Identifier));
                Output += ("\nUnknow:" + (tl.Unknow));
                Output += ("\nContentSize:" + (tl.ContentSize));
                Output += ("\nSwfFileName:" + (tl.SwfFileName.Replace(Encoding.ASCII.GetString(new byte[] { 0x00 }), "")));
                Output += ("\nfuiTimelineCount:" + (tl.fuiTimelineCount));
                Output += ("\nfuiTimelineEventNameCount:" + (tl.fuiTimelineEventNameCount));
                Output += ("\nfuiTimelineActionCount:" + (tl.fuiTimelineActionCount));
                Output += ("\nfuiShapeCount:" + (tl.fuiShapeCount));
                Output += ("\nfuiShapeComponentCount:" + (tl.fuiShapeComponentCount));
                Output += ("\nfuiVertCount:" + (tl.fuiVertCount));
                Output += ("\nfuiTimelineFrameCount:" + (tl.fuiTimelineFrameCount));
                Output += ("\nfuiTimelineEventCount:" + (tl.fuiTimelineEventCount));
                Output += ("\nfuiReferenceCount:" + (tl.fuiReferenceCount));
                Output += ("\nfuiEdittextCount:" + (tl.fuiEdittextCount));
                Output += ("\nfuiSymbolCount:" + (tl.fuiSymbolCount));
                Output += ("\nfuiBitmapCount:" + (tl.fuiBitmapCount));
                Output += ("\nimagesSize:" + (tl.imagesSize));
                Output += ("\nfuiFontNameCount:" + (tl.fuiFontNameCount));
                Output += ("\nfuiImportAssetCount:" + (tl.fuiImportAssetCount));
                Output += ("\nFrameSize:" + BitConverter.ToString(tl.FrameSize) + "");
                richTextBox1.Text = Output;
            }
            else if (treeView1.SelectedNode.Text == "images" || treeView1.SelectedNode.Text == "Font")
            {
                extractToolStripMenuItem.Enabled = true;
            }
            else if (treeView1.SelectedNode.Parent.Text == "Timelines")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.Timeline tl = Fuis[FuiNum].timelines[IndexInt];
                richTextBox1.Text = "ObjectType:" + tl.ObjectType + "\nFrameIndex:" + tl.FrameIndex + "\nFrameCount:" + tl.FrameCount + "\nActionIndex:" + tl.ActionIndex + "\nActionCount:" + tl.ActionCount + "\nRectangle:" + BitConverter.ToString(tl.Rectangle.totalbytes);

                
            }
            else if (treeView1.SelectedNode.Parent.Text == "TimelineActions")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.TimelineAction tl = Fuis[FuiNum].timelineActions[IndexInt];
                richTextBox1.Text = "ActionType:" + (tl.ActionType) + "\nUnknown:" + (tl.Unknown) + "\nUnkStr0:" + tl.UnknownName1.Replace(Encoding.ASCII.GetString(new byte[] { 0x00 }), "") + "\nUnkStr1:" + tl.UnknownName2;
            }
            else if (treeView1.SelectedNode.Parent.Text == "Shapes")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.Shape tl = Fuis[FuiNum].shapes[IndexInt];
                richTextBox1.Text = "UnkVal0:" + tl.UnknownValue1 + "\nUnkVal1:" + tl.UnknownValue2 + "\nObjectType:" + tl.ObjectType + "\nRectangle:" + BitConverter.ToString(tl.Rectangle.totalbytes);
            }
            else if (treeView1.SelectedNode.Parent.Text == "ShapeComponents")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.ShapeComponent tl = Fuis[FuiNum].shapeComponents[IndexInt];
                richTextBox1.Text = "FillInfo:" + BitConverter.ToString(tl.FillInfo.totalbytes) + "\nUnkVal0:" + tl.UnknownValue1 + "\nUnkVal1:" + tl.UnknownValue2;
            }
            else if (treeView1.SelectedNode.Parent.Text == "Verts")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.Vert tl = Fuis[FuiNum].verts[IndexInt];
                if (BitConverter.ToInt32(tl.x, 0) > 1000000 || BitConverter.ToInt32(tl.y, 0) > 1000000)
                {
                    int x = BitConverter.ToInt32(tl.x.Skip(0).Take(4).Reverse().ToArray(), 0);
                    int y = BitConverter.ToInt32(tl.y.Skip(0).Take(4).Reverse().ToArray(), 0);
                    richTextBox1.Text = "X:" + x + "\nY:" + y;
                }
                else
                {
                    richTextBox1.Text = "X:" + BitConverter.ToInt32(tl.x, 0) + "\nY:" + BitConverter.ToInt32(tl.y, 0);
                }
            }
            else if (treeView1.SelectedNode.Parent.Text == "TimelineFrames")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.TimelineFrame tl = Fuis[FuiNum].timelineFrames[IndexInt];
                richTextBox1.Text = "FrameName:" + tl.FrameName.Replace(Encoding.ASCII.GetString(new byte[] { 0x00 }), "") + "\nEventIndex:" + tl.EventIndex + "\nEventCount:" + tl.EventCount;
            }
            else if (treeView1.SelectedNode.Parent.Text == "TimelineEvents")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Parent.Parent.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.TimelineEvent tl = Fuis[FuiNum].timelineEvents[IndexInt];
                richTextBox1.Text = "EventType:" + BitConverter.ToInt16(tl.EventType, 0) + "\nObjectType:" + BitConverter.ToInt16(tl.ObjectType, 0) + "\nUnknown0:" + BitConverter.ToInt16(tl.Unknown0, 0) + "\nIndex:" + BitConverter.ToInt16(tl.Index, 0) + "\nUnknown1:" + BitConverter.ToInt16(tl.Unknown1, 0) + "\nNameIndex:" + BitConverter.ToInt16(tl.NameIndex, 0) + "\nMatrix:" + BitConverter.ToString(tl.matrix.totalbytes) + "\nColorTransform:" + BitConverter.ToString(tl.ColorTransform.totalbytes) + "\nColor:" + BitConverter.ToString(tl.Color.totalbytes);
            }
            else if (treeView1.SelectedNode.Parent.Text == "TimelineEventNames")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.TimelineEventName tl = Fuis[FuiNum].timelineEventNames[IndexInt];
                richTextBox1.Text = "EventName:" + tl.EventName;
            }
            else if (treeView1.SelectedNode.Parent.Text == "References")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.Reference tl = Fuis[FuiNum].references[IndexInt];
                richTextBox1.Text = "SymbolIndex:" + tl.SymbolIndex + "\nReferenceName:" + tl.ReferenceName.Replace(Encoding.ASCII.GetString(new byte[] { 0x00 }), "") + "\nFuiIndex:" + BitConverter.ToString(tl.Index);
            }
            else if (treeView1.SelectedNode.Parent.Text == "EditTexts")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.Edittext tl = Fuis[FuiNum].edittexts[IndexInt];
                richTextBox1.Text = "UnkVal0:" + tl.Unknown1 + "\nRectangle:" + BitConverter.ToString(tl.rectangle.totalbytes) + "\nUnkVal1:" + tl.Unknown2 + "\nUnkVal2:" + BitConverter.ToString(tl.Unknown3) + "\nColor:" + BitConverter.ToString(tl.Color.totalbytes) + "\nUnkVal3:" + BitConverter.ToString(tl.Unknown4) + "\nHtmlTextFormat:" + tl.htmlTextFormat;
            }
            else if (treeView1.SelectedNode.Parent.Text == "FontNames")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.FontName tl = Fuis[FuiNum].fontNames[IndexInt];
                richTextBox1.Text = "UnkVal0:" + tl.Unknown1 + "\nFontName:" + tl.Fontname + "\nUnkVal1:" + tl.Unknown2 + "\nUnkVal2:" + tl.Unknown3 + "\nUnkVal3:" + BitConverter.ToString(tl.Unknown4) + "\nUnkVal4:" + tl.Unknown5 + "\nUnkVal5:" + BitConverter.ToString(tl.Unknown6) + "\nUnkVal6:" + tl.Unknown7;
            }
            else if (treeView1.SelectedNode.Parent.Text == "Symbols")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.Symbol tl = Fuis[FuiNum].symbols[IndexInt];
                richTextBox1.Text = "SymbolName:" + tl.SymbolName.Replace(Encoding.ASCII.GetString(new byte[] { 0x00 }), "") + "\nObjectType:" + tl.ObjectType + "\nSymbolIndex:" + tl.Unknown;
            }
            else if (treeView1.SelectedNode.Parent.Text == "ImportAssets")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.ImportAsset tl = Fuis[FuiNum].importAssets[IndexInt];
                richTextBox1.Text = "Assetname:" + tl.AssetName;
            }
            else if (treeView1.SelectedNode.Parent.Text == "Bitmaps")
            {
                object NodeTag = treeView1.SelectedNode.Tag;
                int BitmNum = int.Parse(NodeTag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                MemoryStream fs = new MemoryStream(Fuis[FuiNum].bitmaps[BitmNum].Image);
                Bitmap bmp = new Bitmap(Bitmap.FromStream(fs));
                pictureBox1.Image = bmp;

                extractToolStripMenuItem.Enabled = true;
                replaceToolStripMenuItem.Enabled = true;
                checkBox1.Enabled = true;

                richTextBox1.Text = "Unk0:" + BitConverter.ToString(Fuis[FuiNum].bitmaps[BitmNum].Unknown1) + "\nObjectType:" + Fuis[FuiNum].bitmaps[BitmNum].ObjectType + "\nScaleWidth:" + Fuis[FuiNum].bitmaps[BitmNum].ScaleWidth + "\nScaleHeight:" + Fuis[FuiNum].bitmaps[BitmNum].ScaleHeight + "\nImageOffset:" + BitConverter.ToString(Fuis[FuiNum].bitmaps[BitmNum].Size1) + "\nImageSize:" + BitConverter.ToString(Fuis[FuiNum].bitmaps[BitmNum].Size2) + "\nUnk1:" + BitConverter.ToString(Fuis[FuiNum].bitmaps[BitmNum].Unknown2) + "\nUnk2:" + Fuis[FuiNum].bitmaps[BitmNum].Unknown3;


            }
            else
            {
                pictureBox1.Image = null;
                extractToolStripMenuItem.Enabled = false;
                replaceToolStripMenuItem.Enabled = false;
                checkBox1.Enabled = false;
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (treeView1.SelectedNode.Parent.Text == "ImportAssets")
                {
                    int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                    int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                    FourJ.FourJUserInterface.ImportAsset tl = Fuis[FuiNum].importAssets[IndexInt];
                    richTextBox1.Text = "Assetname:" + tl.AssetName.Replace(Encoding.ASCII.GetString(new byte[] { 0x00 }), "");
                    Console.WriteLine(Path.GetDirectoryName(Fuis[FuiNum].FilePath) + "/" + tl.AssetName.Replace(Encoding.ASCII.GetString(new byte[] { 0x00 }), "").Replace(".swf", ".fui"));
                    if (File.Exists(Path.GetDirectoryName(Fuis[FuiNum].FilePath) + "/" + tl.AssetName.Replace(Encoding.ASCII.GetString(new byte[] { 0x00 }), "").Replace(".swf", ".fui")))
                        if (MessageBox.Show("Do you want to open " + tl.AssetName.Replace(Encoding.ASCII.GetString(new byte[] { 0x00 }), "") + "?", "Open File?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            FourJ.FourJUserInterface.FUI FUI = new FourJ.FourJUserInterface.FUI();
                            LoadingFileDialog lfd = new LoadingFileDialog(FUI, Path.GetDirectoryName(Fuis[FuiNum].FilePath) + "\\" + tl.AssetName.Replace(Encoding.ASCII.GetString(new byte[] { 0x00 }), "").Replace(".swf", ".fui"));
                            lfd.ShowDialog();
                            Classes.LoadFUI.OpenFUINew(FUI, true, treeView1, treeView1.Nodes.Count);
                            Fuis.Add(FUI);
                        }
                }

            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }
        #endregion

        #region Loading Program

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {

                if (!File.Exists(Environment.CurrentDirectory + "\\settings.ini"))
                    File.WriteAllText(Environment.CurrentDirectory + "\\settings.ini", "**Settings** \nyou can change a variable here!\n**true / false does not accept capitals, 'True' and 'TRUE' do not work, ony 'true'\nIsPortable=" + Program.IsPortable.ToString().Replace("T", "t").Replace("F", "f"));
                Directory.CreateDirectory(Environment.CurrentDirectory + "\\Data");
                File.WriteAllBytes(Environment.CurrentDirectory + "\\Data\\FuiIMGData_Enc.db", Properties.Resources.FuiIMGData_Enc);
            }
            catch { }
            try // Checks if portable flag is checked in settings
            {

                string Data = File.ReadAllText(Environment.CurrentDirectory + "\\settings.ini");
                string[] Lines = Data.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                foreach (string Line in Lines)
                {
                    try
                    {
                        string Param = Line.Split('=')[0];
                        string Value = Line.Split('=')[1];
                        Console.WriteLine(Param + "=" + Value);
                        switch (Param)
                        {
                            case ("IsPortable"):
                                Program.IsPortable = (Value == "true");
                                break;
                        }
                    }
                    catch { }

                }
            }
            catch { }
            try // Determine Location based on portable status
            {
                if (!Program.IsPortable)
                    Program.TempDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Fui Studio\\";
                else
                    Program.TempDir = Environment.CurrentDirectory + "\\Fui Studio\\";
            }
            catch
            {

            }
            pictureBox1.InterpolationMode = InterpolationMode.NearestNeighbor;
            Directory.CreateDirectory(Program.TempDir);
            Classes.Networking.checkUpdate();
            Classes.Networking.TryDlDatabse();
            if (Classes.Networking.NeedsUpdate)
                UPDATEToolStripMenuItem.Visible = true;
            File.WriteAllBytes(Environment.CurrentDirectory + "\\Mojangles.ttf", Properties.Resources.Mojangles);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach(string dir in Directory.GetDirectories(Program.TempDir))
            {
                Directory.Delete(dir, true);
            }
            Application.Exit();
        }

        #endregion

        #region ToolStrip Options

        private void openWorkingDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Program.TempDir);
        }

        private void CheckDBMenuItem_Click(object sender, EventArgs e)
        {
            Classes.Networking.TryDlDatabse();
        }

        private void extractToolStripMenuItem_Click(object sender, EventArgs e)
        {
                switch (treeView1.SelectedNode.Text)
                {
                    case ("images"):
                        FolderBrowserDialog fbd = new FolderBrowserDialog();
                        fbd.RootFolder = Environment.SpecialFolder.UserProfile;
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        Directory.CreateDirectory(fbd.SelectedPath + "\\images\\");

                        int i = 0;
                        foreach (string file in Directory.GetFiles(Program.TempDir + Path.GetFileName(treeView1.SelectedNode.Parent.Text) + "\\images\\"))
                        {
                            try
                            {
                                File.Copy(Program.TempDir + Path.GetFileName(treeView1.SelectedNode.Parent.Text) + "\\images\\Tile" + i + ".png", fbd.SelectedPath + "\\images\\" + treeView1.SelectedNode.Nodes[i].Text, true);
                            }
                            catch
                            {
                                File.Copy(Program.TempDir + Path.GetFileName(treeView1.SelectedNode.Parent.Text) + "\\images\\Tile" + i + ".jpg", fbd.SelectedPath + "\\images\\" + treeView1.SelectedNode.Nodes[i].Text, true);
                            }
                                i++;
                        }

                    }
                        return;
                        break;
                    case ("Font"):
                        FolderBrowserDialog fbd2 = new FolderBrowserDialog();
                        fbd2.RootFolder = Environment.SpecialFolder.UserProfile;
                        if (fbd2.ShowDialog() == DialogResult.OK)
                        {
                            Console.WriteLine(fbd2.SelectedPath);
                            Console.WriteLine(fbd2.SelectedPath + "\\Font\\");
                            Directory.CreateDirectory(fbd2.SelectedPath + "\\Font\\");
                            foreach (TreeNode file in treeView1.SelectedNode.Nodes)
                            {
                                Console.WriteLine(fbd2.SelectedPath + "\\Font\\" + file.Text);
                                Console.WriteLine(file.Tag.ToString());
                                File.AppendAllText(fbd2.SelectedPath + "\\Font\\" + file.Text.Replace(".fnt",".html"), file.Tag.ToString() + "\n");
                            }
                        }
                        return;
                        break;
                }
            try
            {
                string selectedImage = treeView1.SelectedNode.Tag.ToString();
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Save File";
                if (treeView1.SelectedNode.Text.EndsWith(".png"))
                {
                    sfd.Filter = "PNG Image|*.png";
                }
                else if (treeView1.SelectedNode.Text.EndsWith(".jpg"))
                {
                    sfd.Filter = "JPEG Image |*.jpg";
                }
                else
                {
                    MessageBox.Show("Unsupported Extraction");
                }
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (checkBox1.Checked)
                    {
                        object NodeTag = treeView1.SelectedNode.Tag;

                        MemoryStream fs = new MemoryStream(File.ReadAllBytes(NodeTag.ToString()));
                        Bitmap bmp = new Bitmap(Bitmap.FromStream(fs));
                        Classes.ImageProcessor.ReverseColorRB(bmp);
                        bmp.Save(sfd.FileName, ImageFormat.Png);
                        return;
                    }
                    File.Copy(selectedImage, sfd.FileName, true);
                }
            }
            catch { }

        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog sfd = new OpenFileDialog();
            sfd.Title = "replace File";
            if (treeView1.SelectedNode.Text.EndsWith(".png"))
            {
                sfd.Filter = "PNG Image|*.png";
            }
            else if (treeView1.SelectedNode.Text.EndsWith(".jpg"))
            {
                sfd.Filter = "JPEG Image|*.jpg";
            }
            else
            {
                MessageBox.Show("Unsupported replacement");
            }
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (treeView1.SelectedNode.Text.EndsWith(".png"))
                {
                    DialogResult dr = MessageBox.Show("Do you want to correct color on this image?",
                      "PNG Replacement", MessageBoxButtons.YesNo);
                    switch (dr)
                    {
                        case DialogResult.Yes:
                            object NodeTag = treeView1.SelectedNode.Tag;

                            MemoryStream fs = new MemoryStream(File.ReadAllBytes(sfd.FileName));
                            Bitmap bmp = new Bitmap(Bitmap.FromStream(fs));
                            Classes.ImageProcessor.ReverseColorRB(bmp);
                            ImageConverter converter = new ImageConverter();
                            byte[] ouutput = (byte[])converter.ConvertTo(bmp, typeof(byte[]));
                            //bmp.Save(NodeTag.ToString(), ImageFormat.Png);
                            File.WriteAllBytes(treeView1.SelectedNode.Tag.ToString(), ouutput);
                            return;
                            break;
                        case DialogResult.No:
                            break;
                    }
                }
                File.Copy(sfd.FileName, treeView1.SelectedNode.Tag.ToString(), true);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    object NodeTag = treeView1.SelectedNode.Tag;

                    MemoryStream fs = new MemoryStream(File.ReadAllBytes(NodeTag.ToString()));
                    Bitmap bmp = new Bitmap(Bitmap.FromStream(fs));
                    Classes.ImageProcessor.ReverseColorRB(bmp);
                    pictureBox1.Image = bmp;
                }
                else
                {
                    object NodeTag = treeView1.SelectedNode.Tag;

                    MemoryStream fs = new MemoryStream(File.ReadAllBytes(NodeTag.ToString()));
                    Bitmap bmp = new Bitmap(Bitmap.FromStream(fs));
                    pictureBox1.Image = bmp;
                }
            }
            catch { }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Forms.about().ShowDialog();
        }

        private void UPDATEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + "\\updater.exe");
            Application.Exit();
        }

        #endregion

        #region Saving FUIs
        
        
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = 0;
            /*try
            {
                foreach (TreeNode file in treeView1.Nodes)
                {
                    if(SaveFUI(file.Tag.ToString(), index) == 1)
                    {

                    }
                    else
                    {
                        return;
                    }
                    index++;
                }
                MessageBox.Show("Saved!");
            }
            catch
            {
                MessageBox.Show("Error!!\n(T_T)");
            }*/
            try
            {
                foreach (FourJ.FourJUserInterface.FUI file in Fuis)
                {
                    if (!Funct.SaveFUI(file.FilePath, file))
                    {
                        return;
                    }
                }
                MessageBox.Show("Saved!");
            }
            catch
            {
                MessageBox.Show("Error!!\n(T_T)");
            }
        }

        public int SaveFUI(string fui, int index)
        {
            int imgno = 0;
            string dir = Program.TempDir + Path.GetFileName(fui) + "\\images";
            int originalsize = File.ReadAllBytes(Path.GetDirectoryName(dir) + "\\" + Path.GetFileNameWithoutExtension(fui) + ".bin").Length;
            Console.WriteLine("Start: " + startEnds[index][0]);
            Console.WriteLine("end: " + startEnds[index][1]);
            byte[] filebytes = File.ReadAllBytes(Path.GetDirectoryName(dir) + "\\" + Path.GetFileNameWithoutExtension(fui) + ".bin");
            string beginningdata = Classes.HexTools.trueByteArrayToHexString(filebytes.Skip(0).Take(startEnds[index][0]).ToArray()).Replace("-", "");
            string endingdata = Classes.HexTools.trueByteArrayToHexString(filebytes.Skip(startEnds[index][1]).Take(filebytes.Length-1).ToArray()).Replace("-", "");
            string midData = "";
            int length = treeView1.Nodes[index].Nodes.Count;
            foreach (TreeNode tn in treeView1.Nodes[index].Nodes[length-1].Nodes)
            {
                midData += tn.Tag.ToString().Replace(" ","");
            }
            string outputdata = Classes.HexTools.ByteArrayToHexString(Path.GetDirectoryName(dir) + "\\" + Path.GetFileNameWithoutExtension(fui) + ".bin").Replace("-","");
            if (saveElements)
            {
                outputdata = beginningdata + midData + endingdata;
            }
            byte[] outputBin = Classes.HexTools.StringToByteArrayFastest(outputdata.Replace(" ", "").Replace("-", ""));
            if (outputBin.Length > originalsize)
            {
                MessageBox.Show("SIZE ERROR!!");
                return 0;
            }
            while (imgno < Directory.GetFiles(Program.TempDir + Path.GetFileName(fui) + "\\images\\").Length)
            {
                try
                {
                    if(File.Exists(dir + "\\Tile" + imgno + ".png"))
                        outputdata += Classes.HexTools.ByteArrayToHexString(dir + "\\Tile" + imgno + ".png").Replace("-", "");
                    else
                        outputdata += Classes.HexTools.ByteArrayToHexString(dir + "\\Tile" + imgno + ".jpg").Replace("-", "");
                }
                catch
                {
                }
                imgno++;
            }
            byte[] output = Classes.HexTools.StringToByteArrayFastest(outputdata.Replace("-", "").Replace(" ", ""));

            MemoryStream fs = new MemoryStream(output);
            //var fileStream = new FileStream(Path.GetDirectoryName(dir) + "\\out.fui", FileMode.Create, FileAccess.Write);
            File.Delete(fui);
            var fileStream = new FileStream(fui, FileMode.Create, FileAccess.Write);
            fs.CopyTo(fileStream);
            fileStream.Dispose();
            return 1;
        }

        #endregion

        private void testingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[] Dat = new byte[20];
            Encoding.ASCII.GetBytes("Hello World!").CopyTo(Dat, 0);
            Console.WriteLine(Encoding.ASCII.GetString(Dat));

        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Parent.Text == "Timelines")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.Timeline tl = Fuis[FuiNum].timelines[IndexInt];
                Fuis[FuiNum].timelines.Remove(tl);
                Fuis[FuiNum].header.fuiTimelineCount--;


            }
            else if (treeView1.SelectedNode.Parent.Text == "TimelineActions")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.TimelineAction tl = Fuis[FuiNum].timelineActions[IndexInt];
                Fuis[FuiNum].timelineActions.Remove(tl);
                Fuis[FuiNum].header.fuiTimelineActionCount--;
            }
            else if (treeView1.SelectedNode.Parent.Text == "Shapes")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.Shape tl = Fuis[FuiNum].shapes[IndexInt];
                Fuis[FuiNum].shapes.Remove(tl);
                Fuis[FuiNum].header.fuiShapeCount--;
            }
            else if (treeView1.SelectedNode.Parent.Text == "ShapeComponents")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.ShapeComponent tl = Fuis[FuiNum].shapeComponents[IndexInt];
                Fuis[FuiNum].shapeComponents.Remove(tl);
                Fuis[FuiNum].header.fuiShapeComponentCount--;
            }
            else if (treeView1.SelectedNode.Parent.Text == "Verts")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.Vert tl = Fuis[FuiNum].verts[IndexInt];
                Fuis[FuiNum].verts.Remove(tl);
                Fuis[FuiNum].header.fuiVertCount--;
            }
            else if (treeView1.SelectedNode.Parent.Text == "TimelineFrames")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.TimelineFrame tl = Fuis[FuiNum].timelineFrames[IndexInt];
                Fuis[FuiNum].timelineFrames.Remove(tl);
                Fuis[FuiNum].header.fuiTimelineFrameCount--;
            }
            else if (treeView1.SelectedNode.Parent.Text == "TimelineEvents")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.TimelineEvent tl = Fuis[FuiNum].timelineEvents[IndexInt];
                Fuis[FuiNum].timelineEvents.Remove(tl);
                Fuis[FuiNum].header.fuiTimelineEventCount--;
            }
            else if (treeView1.SelectedNode.Parent.Text == "TimelineEventNames")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.TimelineEventName tl = Fuis[FuiNum].timelineEventNames[IndexInt];
                Fuis[FuiNum].timelineEventNames.Remove(tl);
                Fuis[FuiNum].header.fuiTimelineEventNameCount--;
            }
            else if (treeView1.SelectedNode.Parent.Text == "References")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.Reference tl = Fuis[FuiNum].references[IndexInt];
                Fuis[FuiNum].references.Remove(tl);
                Fuis[FuiNum].header.fuiReferenceCount--;
            }
            else if (treeView1.SelectedNode.Parent.Text == "EditTexts")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.Edittext tl = Fuis[FuiNum].edittexts[IndexInt];
                Fuis[FuiNum].edittexts.Remove(tl);
                Fuis[FuiNum].header.fuiEdittextCount--;
            }
            else if (treeView1.SelectedNode.Parent.Text == "FontNames")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.FontName tl = Fuis[FuiNum].fontNames[IndexInt];
                Fuis[FuiNum].fontNames.Remove(tl);
                Fuis[FuiNum].header.fuiFontNameCount--;
            }
            else if (treeView1.SelectedNode.Parent.Text == "Symbols")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.Symbol tl = Fuis[FuiNum].symbols[IndexInt];
                Fuis[FuiNum].symbols.Remove(tl);
                Fuis[FuiNum].header.fuiSymbolCount--;
            }
            else if (treeView1.SelectedNode.Parent.Text == "ImportAssets")
            {
                int IndexInt = int.Parse(treeView1.SelectedNode.Tag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.ImportAsset tl = Fuis[FuiNum].importAssets[IndexInt];
                Fuis[FuiNum].importAssets.Remove(tl);
                Fuis[FuiNum].header.fuiImportAssetCount--;
            }
            else if (treeView1.SelectedNode.Parent.Text == "Bitmaps")
            {
                object NodeTag = treeView1.SelectedNode.Tag;
                int BitmNum = int.Parse(NodeTag.ToString());
                int FuiNum = int.Parse(treeView1.SelectedNode.Parent.Parent.Tag.ToString());
                FourJ.FourJUserInterface.Bitmap tl = Fuis[FuiNum].bitmaps[BitmNum];
                Fuis[FuiNum].bitmaps.Remove(tl);
                Fuis[FuiNum].header.fuiBitmapCount--;

            }

            treeView1.SelectedNode.Remove();
        }
    }
}
