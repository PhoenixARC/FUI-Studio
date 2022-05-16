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

            if (args.Length > 0 && File.Exists(args[0]) && args[0].EndsWith(".fui"))
                OpenFUI(args[0]);
        }

        private void OnLoad(object sender, EventArgs e)
        {
            Settings.Load();
            if (!(settingsIsPortable.Checked = Settings.IsPortable))
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
        }

        public void OpenFUI(string fuiFilepath)
        {
            var fui = FUIFile.Open(fuiFilepath);
            FUIFileTreeView.Nodes.Add(FUIUtil.ConstructFuiTreeNode(fui));
            openFuiFiles.Add(fui);
        }


        private void openWorkingDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Settings.TempDir);
        }


        private int GetRootIndex(TreeNode node)
        {
            if (node == null) throw new ArgumentNullException(node.Text);
            if (node.Parent == null) return node.Index;
            return GetRootIndex(node.Parent);
        }

        private void FUIFileTreeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            richTextBox1.Clear();
            pictureBox1.Hide();
            extractToolStripMenuItem.Enabled = false;
            replaceToolStripMenuItem.Enabled = false;
            var SelectedNode = e.Node;
            if (SelectedNode == null || SelectedNode.Tag == null) return;
            FUIFile fui = openFuiFiles[GetRootIndex(SelectedNode)];
            if (fui == null) return;
            if (SelectedNode.Tag is Image)
            {
                var img = SelectedNode.Tag as Image;
                pictureBox1.Image = img;
                pictureBox1.Show();
            }
            if (!(SelectedNode.Tag is IFuiObject)) return;

            var fuiObj = SelectedNode.Tag as IFuiObject;
            richTextBox1.Text = fuiObj.ToString();
            if (fuiObj is FuiBitmap)
            {
                extractToolStripMenuItem.Enabled = true;
                replaceToolStripMenuItem.Enabled = true;
                var fuiBitmap = fuiObj as FuiBitmap;
                int img_index = fui.bitmaps.IndexOf(fuiBitmap);
                var img = new Bitmap(new MemoryStream(fui.Images[img_index]));
                if ((int)fuiBitmap.format < 6)
                    ImageProcessor.ReverseColorRB(img);
                pictureBox1.Image = img;
                pictureBox1.Show();
                return;
            }
            if (fuiObj is Timeline)
            {
                var fuiTimeline = fuiObj as Timeline;
            }
        }

        private void extractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var SelectedNode = FUIFileTreeView.SelectedNode;
            if (SelectedNode == null) return;
            if (!(SelectedNode.Tag is FuiBitmap))
            {
                MessageBox.Show("The selected node does not seem to be an image", "Invalide node");
                return;
            }

            FUIFile fui = openFuiFiles[GetRootIndex(SelectedNode)];
            if (fui == null) throw new NullReferenceException("fui is null");
            var fuiBitmap = SelectedNode.Tag as FuiBitmap;
            int nodeIndex = fui.bitmaps.IndexOf(fuiBitmap);
            MemoryStream fs = new MemoryStream(fui.Images[nodeIndex]);
            var bitmapInfo = fui.bitmaps[nodeIndex];
            Bitmap img = new Bitmap(fs);
            fs.Dispose();
            string ext = ".jpeg";
            string filter = "jpeg | *.jpeg";
            ImageFormat format = ImageFormat.Jpeg;
            if ((int)bitmapInfo.format < 6)
            {
                ImageProcessor.ReverseColorRB(img);
                ext = ".png";
                filter = "png | *.png";
                format = ImageFormat.Png;
            }
            using (var imageSaveDialog = new SaveFileDialog())
            {
                imageSaveDialog.Title = "Extract image";
                imageSaveDialog.DefaultExt = ext;
                imageSaveDialog.Filter = filter;
                imageSaveDialog.FileName = fui.symbols[bitmapInfo.symbolIndex].Name;
                if (imageSaveDialog.ShowDialog() == DialogResult.OK)
                {
                    img.Save(imageSaveDialog.FileName, format);
                }
            }
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var SelectedNode = FUIFileTreeView.SelectedNode;
            if (SelectedNode == null) return;
            if (!(SelectedNode.Tag is FuiBitmap))
            {
                MessageBox.Show("The selected node does not seem to be an image", "Invalide node");
                return;
            }

            FUIFile fui = openFuiFiles[GetRootIndex(SelectedNode)];
            if (fui == null) return;
            using (var sfd = new OpenFileDialog())
            {
                sfd.Title = "Replace file";
                sfd.Filter = "PNG File (*.png)|*.png|JPEG File (*.jpeg)|*.jpeg|JPG (*jpg)|*.jpg";
                sfd.CheckFileExists = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    var replacement_img = new MemoryStream(File.ReadAllBytes(sfd.FileName));
                    var replacement_bitmap_data = new Bitmap(replacement_img);
                    var fuiBitmapFormat = FuiBitmap.eFuiBitmapType.PNG_WITH_ALPHA_DATA;
                    var imgFormat = ImageFormat.Png;

                    if (sfd.FileName.EndsWith(".png"))
                    {
                        replacement_bitmap_data = ImageProcessor.ReverseColorRB(replacement_bitmap_data);
                    }
                    else if (sfd.FileName.EndsWith(".jpg") || sfd.FileName.EndsWith(".jpeg"))
                    {
                        fuiBitmapFormat = FuiBitmap.eFuiBitmapType.JPEG_NO_ALPHA_DATA;
                        imgFormat = ImageFormat.Jpeg;
                    }
                    else
                        throw new NotSupportedException("Unsupported image file");

                    var img_stream = new MemoryStream();
                    replacement_bitmap_data.Save(img_stream, imgFormat);
                    pictureBox1.Image = replacement_bitmap_data;
                    var fuiBitmap = SelectedNode.Tag as FuiBitmap;
                    int nodeIndex = fui.bitmaps.IndexOf(fuiBitmap);
                    fui.bitmaps[nodeIndex].format = fuiBitmapFormat;
                    fui.bitmaps[nodeIndex].width = replacement_bitmap_data.Width;
                    fui.bitmaps[nodeIndex].height = replacement_bitmap_data.Height;
                    fui.Images[nodeIndex] = img_stream.ToArray();
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fuiOpenFileDialog = new OpenFileDialog())
            {
                fuiOpenFileDialog.Filter = "Fui file | *.fui";
                if (fuiOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    openFuiFiles.Clear();
                    FUIFileTreeView.Nodes.Clear();
                    OpenFUI(fuiOpenFileDialog.FileName);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = FUIFileTreeView.SelectedNode;
            if (node == null)
            {
                MessageBox.Show("Please select the node you want to save", "Save Error");
                return;
            }
            int fuiIndex = GetRootIndex(node);
            node = FUIFileTreeView.Nodes[fuiIndex];
            using (var fuiSaveFileDialog = new SaveFileDialog())
            {
                fuiSaveFileDialog.Title = "Save fui file";
                fuiSaveFileDialog.Filter = "Fui file | *.fui";
                fuiSaveFileDialog.DefaultExt = ".fui";
                if (fuiSaveFileDialog.ShowDialog() != DialogResult.OK) return;
                using (var fs = new FileStream(fuiSaveFileDialog.FileName, FileMode.Create))
                {
                    openFuiFiles[fuiIndex].Build(fs);
                }
            }
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
