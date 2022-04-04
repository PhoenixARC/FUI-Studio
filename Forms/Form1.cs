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
using FUI_Studio.Classes;
using FUI_Studio.Classes.fui;
using static FourJ.UserInterface;

namespace FUI_Studio.Forms
{
    public partial class Form1 : Form
    {
        List<FUIFile> openFuiFiles = new List<FUIFile>();

        public enum eTreeViewImgTag
        {
            Selected,
            BaseIcon,
            FolderIcon,
            ImgIcon,
            FontIcon,
            ElementIcon
        }
        public Form1(string[] args)
        {
            InitializeComponent();
            ImageList imageList = new ImageList();
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageSize = new Size(20, 20);
            imageList.Images.Add(Properties.Resources.Selected);
            imageList.Images.Add(Properties.Resources.BaseIcon);
            imageList.Images.Add(Properties.Resources.FolderIcon);
            imageList.Images.Add(Properties.Resources.ImgIcon);
            imageList.Images.Add(Properties.Resources.FontIcon);
            imageList.Images.Add(Properties.Resources.element);
            FUIFileTreeView.ImageList = imageList;
            pictureBox1.InterpolationMode = InterpolationMode.NearestNeighbor;

            if (args.Length > 0 && File.Exists(args[0]) && args[0].EndsWith(".fui"))
                OpenFUI(args[0]);
        }

        private void OnLoad(object sender, EventArgs e)
        {
            Settings.Load();
            if (settingsIsPortable.Checked = Settings.IsPortable)
                Settings.TempDir = Environment.CurrentDirectory + "\\Fui Studio\\";
            else
                Settings.TempDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Fui Studio/";

            if (settingsCheckForUpdates.Checked = Settings.CheckForUpdates)
                Networking.checkUpdate();

            try
            {
                Directory.CreateDirectory(Settings.TempDir);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void BeforeClosing(object sender, FormClosingEventArgs e)
        {
            foreach (string dir in Directory.GetDirectories(Settings.TempDir))
            {
                Directory.Delete(dir, true);
            }
            Application.Exit();
        }

        public void OpenFUI(string fuiFilepath)
        {
            var fui = FUIFile.Open(fuiFilepath);
            var fuiFileIndex = FUIFileTreeView.GetNodeCount(false);
            FUIFileTreeView.Nodes.Add(FUIUtil.ConstructTreeNode(fui, fuiFileIndex));
            openFuiFiles.Add(fui);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var opf = new OpenFileDialog())
            {
                opf.Filter = "Fui file | *.fui";
                if (opf.ShowDialog() == DialogResult.OK)
                {
                    //openFuiFiles.Clear();
                    //FUIFileTreeView.Nodes.Clear();
                    OpenFUI(opf.FileName);
                }
            }
        }

        private void openWorkingDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Settings.TempDir);
        }


        private int getRootIndex(TreeNode node)
        {
            if (node == null) throw new ArgumentNullException(nameof(node));
            if (node.Parent == null) return node.Index;
            return getRootIndex(node.Parent);
        }

        private void FUIFileTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            richTextBox1.Text = null;
            var SelectedNode = e.Node;
            var SelectedParentNode = SelectedNode.Parent;
            if (SelectedParentNode == null) return;
            int index = SelectedNode.Index;
            FUIFile fui = openFuiFiles[getRootIndex(SelectedNode)];
            if (fui == null) return;
            switch(SelectedParentNode.Text)
            {
                case "Timelines":
                    richTextBox1.Text = fui.timelines[index].ToString();
                    break;

                case "TimelineActions":
                    richTextBox1.Text = fui.timelineActions[index].ToString();
                    break;

                case "TimelineFrames":
                    richTextBox1.Text = fui.timelineFrames[index].ToString();
                    break;

                case "TimelineEvents":
                    richTextBox1.Text = fui.timelineEvents[index].ToString();
                    break;

                case "TimelineEventNames":
                    richTextBox1.Text = fui.timelineEventNames[index].ToString();
                    break;

                case "Verts":
                    richTextBox1.Text = fui.verts[index].ToString();
                    break;

                case "Shapes":
                    richTextBox1.Text = fui.shapes[index].ToString();
                    break;

                case "ShapeComponents":
                    richTextBox1.Text = fui.shapeComponents[index].ToString();
                    break;

                case "References":
                    richTextBox1.Text = fui.references[index].ToString();
                    break;

                case "EditTexts":
                    richTextBox1.Text = fui.edittexts[index].ToString();
                    break;

                case "FontNames":
                    richTextBox1.Text = fui.fontNames[index].ToString();
                    break;

                case "Symbols":
                    richTextBox1.Text = fui.symbols[index].ToString();
                    break;

                case "ImportAssets":
                    richTextBox1.Text = fui.importAssets[index].ToString();
                    break;

                case "Bitmaps":
                    MemoryStream fs = new MemoryStream(fui.Images[index]);
                    var bitmapInfo = fui.bitmaps[index];
                    Bitmap img = new Bitmap(Image.FromStream(fs));
                    if ((int)bitmapInfo.format < 6)
                        img = ImageProcessor.ReverseColorRB(img);
                    pictureBox1.Image = img;
                    extractToolStripMenuItem.Enabled = true;
                    replaceToolStripMenuItem.Enabled = true;
                    richTextBox1.Text = bitmapInfo.ToString();
                    break;

                default:
                    pictureBox1.Image = null;
                    extractToolStripMenuItem.Enabled = false;
                    replaceToolStripMenuItem.Enabled = false;
                    richTextBox1.Text = fui.header.ToString();
                    break;
            }
        }

        private void extractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switch (FUIFileTreeView.SelectedNode.Text)
            {
                case ("images"):
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    fbd.RootFolder = Environment.SpecialFolder.UserProfile;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    Directory.CreateDirectory(fbd.SelectedPath + "\\images\\");

                    int i = 0;
                    foreach (string file in Directory.GetFiles(Settings.TempDir + Path.GetFileName(FUIFileTreeView.SelectedNode.Parent.Text) + "\\images\\"))
                    {
                        try
                        {
                            File.Copy(Settings.TempDir + Path.GetFileName(FUIFileTreeView.SelectedNode.Parent.Text) + "\\images\\Tile" + i + ".png", fbd.SelectedPath + "\\images\\" + FUIFileTreeView.SelectedNode.Nodes[i].Text, true);
                        }
                        catch
                        {
                            File.Copy(Settings.TempDir + Path.GetFileName(FUIFileTreeView.SelectedNode.Parent.Text) + "\\images\\Tile" + i + ".jpg", fbd.SelectedPath + "\\images\\" + FUIFileTreeView.SelectedNode.Nodes[i].Text, true);
                        }
                        i++;
                    }

                }
                break;
                case ("Font"):
                    FolderBrowserDialog fbd2 = new FolderBrowserDialog();
                    fbd2.RootFolder = Environment.SpecialFolder.UserProfile;
                    if (fbd2.ShowDialog() == DialogResult.OK)
                    {
                        Console.WriteLine(fbd2.SelectedPath);
                        Console.WriteLine(fbd2.SelectedPath + "\\Font\\");
                        Directory.CreateDirectory(fbd2.SelectedPath + "\\Font\\");
                        foreach (TreeNode file in FUIFileTreeView.SelectedNode.Nodes)
                        {
                            Console.WriteLine(fbd2.SelectedPath + "\\Font\\" + file.Text);
                            Console.WriteLine(file.Tag.ToString());
                            File.AppendAllText(fbd2.SelectedPath + "\\Font\\" + file.Text.Replace(".fnt",".html"), file.Tag.ToString() + "\n");
                        }
                    }
                    break;
            }
            try
            {
                string selectedImage = FUIFileTreeView.SelectedNode.Tag.ToString();
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Save File";
                if (FUIFileTreeView.SelectedNode.Text.EndsWith(".png"))
                {
                    sfd.Filter = "PNG Image|*.png";
                }
                else if (FUIFileTreeView.SelectedNode.Text.EndsWith(".jpg"))
                {
                    sfd.Filter = "JPEG Image |*.jpg";
                }
                else
                {
                    MessageBox.Show("Unsupported Extraction");
                }
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    object NodeTag = FUIFileTreeView.SelectedNode.Tag;

                    MemoryStream fs = new MemoryStream(File.ReadAllBytes(NodeTag.ToString()));
                    System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(Image.FromStream(fs));
                    Classes.ImageProcessor.ReverseColorRB(bmp);
                    bmp.Save(sfd.FileName, ImageFormat.Png);

                    File.Copy(selectedImage, sfd.FileName, true);
                }
            }
            catch { }

        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog sfd = new OpenFileDialog();
            sfd.Title = "replace File";
            if (FUIFileTreeView.SelectedNode.Text.EndsWith(".png"))
            {
                sfd.Filter = "PNG Image|*.png";
            }
            else if (FUIFileTreeView.SelectedNode.Text.EndsWith(".jpg"))
            {
                sfd.Filter = "JPEG Image|*.jpg";
            }
            else
            {
                MessageBox.Show("Unsupported replacement");
            }
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (FUIFileTreeView.SelectedNode.Text.EndsWith(".png"))
                {
                    DialogResult dr = MessageBox.Show("Do you want to correct color on this image?",
                      "PNG Replacement", MessageBoxButtons.YesNo);
                    switch (dr)
                    {
                        case DialogResult.Yes:
                            object NodeTag = FUIFileTreeView.SelectedNode.Tag;

                            MemoryStream fs = new MemoryStream(File.ReadAllBytes(sfd.FileName));
                            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(Image.FromStream(fs));
                            Classes.ImageProcessor.ReverseColorRB(bmp);
                            ImageConverter converter = new ImageConverter();
                            byte[] ouutput = (byte[])converter.ConvertTo(bmp, typeof(byte[]));
                            //bmp.Save(NodeTag.ToString(), ImageFormat.Png);
                            File.WriteAllBytes(FUIFileTreeView.SelectedNode.Tag.ToString(), ouutput);
                            break;
                        case DialogResult.No:
                            break;
                    }
                }
                File.Copy(sfd.FileName, FUIFileTreeView.SelectedNode.Tag.ToString(), true);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Forms.About().ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void settingsOnAutoUpdateClick(object sender, EventArgs e)
        {
            settingsCheckForUpdates.Checked = !settingsCheckForUpdates.Checked;
            Settings.CheckForUpdates = settingsCheckForUpdates.Checked;
        }

        private void settingsSaveClicked(object sender, EventArgs e)
        {
            Settings.Save();
            MessageBox.Show("Settings have been saved", "Settings updated");
        }

        private void settingsOnIsPortableClick(object sender, EventArgs e)
        {
            settingsIsPortable.Checked = !settingsIsPortable.Checked;
            Settings.IsPortable = settingsIsPortable.Checked;
        }
    }
}
