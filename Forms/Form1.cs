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
        public string TempDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Fui Studio\\";

        bool DisplayLabels = false;
        bool DisplayFont = false;
        bool LoadReferences = false;
        bool Loadimages = false;
        bool saveElements = false;

        List<int[]> startEnds = new List<int[]>();

        #endregion

        #region Opening FUIs

        public void OpenFUI(string fui, bool IsReference)
        {
            Classes.LoadFUI.OpenFUI(fui, IsReference, treeView1, DisplayLabels, DisplayFont, startEnds, imgList, LoadReferences, Loadimages);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            if (opf.ShowDialog() == DialogResult.OK)
                OpenFUI(opf.FileName, false);
        }

        #endregion

        #region TreeView Selection
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            checkBox1.Checked = false;
            richTextBox1.Text = null;
            if (treeView1.SelectedNode.Text.EndsWith(".png") || treeView1.SelectedNode.Text.EndsWith(".jpg"))
            {
                object NodeTag = treeView1.SelectedNode.Tag;

                MemoryStream fs = new MemoryStream(File.ReadAllBytes(NodeTag.ToString()));
                Bitmap bmp = new Bitmap(Bitmap.FromStream(fs));
                pictureBox1.Image = bmp;

                extractToolStripMenuItem.Enabled = true;
                replaceToolStripMenuItem.Enabled = true;
                if (treeView1.SelectedNode.Text.EndsWith(".png"))
                    checkBox1.Enabled = true;


            }
            else if (treeView1.SelectedNode.Text == "images" || treeView1.SelectedNode.Text == "Font")
            {
                extractToolStripMenuItem.Enabled = true;
            }
            else if (treeView1.SelectedNode.Text.EndsWith(".fnt"))
            {
                richTextBox1.Text = treeView1.SelectedNode.Tag.ToString();
            }
            else if (treeView1.SelectedNode.Text.EndsWith(".elmnt"))
            {
                //richTextBox1.Text = treeView1.SelectedNode.Tag.ToString();
                string[] stats = treeView1.SelectedNode.Tag.ToString().Split(new[] { " " }, StringSplitOptions.None);
                richTextBox1.Text = "width = "+ Classes.HexTools.StringToByteArrayFastest(stats[14])[0]+"\nheight = "+ Classes.HexTools.StringToByteArrayFastest(stats[26])[0]+"\nx = "+ Classes.HexTools.StringToByteArrayFastest(stats[30])[0];
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
                Console.WriteLine(Path.GetExtension(treeView1.SelectedNode.Text));
                    switch (Path.GetExtension(treeView1.SelectedNode.Text))
                    {
                        case (".png"):
                            ImageViewerForm img = new ImageViewerForm(treeView1.SelectedNode.Tag.ToString());
                            img.ShowDialog();
                            break;
                        case (".jpg"):
                            ImageViewerForm img2 = new ImageViewerForm(treeView1.SelectedNode.Tag.ToString());
                            img2.ShowDialog();
                            break;
                        case (".fnt"):
                            FontViewerForm fnt = new FontViewerForm(treeView1.SelectedNode.Tag.ToString(), treeView1.SelectedNode.Text);
                            fnt.ShowDialog();
                            break;
                        case (".elmnt"):
                            var result = ElementModifier.Execute(Classes.HexTools.StringToByteArrayFastest(treeView1.SelectedNode.Tag.ToString().Replace(" ","")), treeView1.SelectedNode.Text.Replace(".elmnt",""));
                            if (result.Result == DialogResult.OK)
                            {
                                Console.WriteLine("xx");
                            treeView1.SelectedNode.Tag = "";
                            }
                        break;
                        case (".ref"):
                            string fui = treeView1.SelectedNode.Parent.Parent.Tag.ToString();
                            string reference = treeView1.SelectedNode.Text.Replace(".ref","");
                            List<string> OpenFUIs = new List<string>();
                        if (reference != Path.GetFileName(fui) && File.Exists(Path.GetDirectoryName(fui) + "\\" + reference))
                        {
                            foreach (TreeNode node in treeView1.Nodes)
                            {
                                OpenFUIs.Add(node.Text);
                            }
                            DialogResult dr = MessageBox.Show(Path.GetFileName(fui) + " is dependent on:" + reference + "\nLoad File?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                            if (dr == DialogResult.Yes)
                            {
                                OpenFUI(Path.GetDirectoryName(fui) + "\\" + reference, true);
                            }

                        }
                            break;
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
            pictureBox1.InterpolationMode = InterpolationMode.NearestNeighbor;
            Directory.CreateDirectory(TempDir);
            Classes.Networking.checkUpdate();
            if (Classes.Networking.NeedsUpdate)
                UPDATEToolStripMenuItem.Visible = true;
            File.WriteAllBytes(Environment.CurrentDirectory + "\\Mojangles.ttf", Properties.Resources.Mojangles);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach(string dir in Directory.GetDirectories(TempDir))
            {
                Directory.Delete(dir, true);
            }
            Application.Exit();
        }

        #endregion

        #region ToolStrip Options

        private void openWorkingDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(TempDir);
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
                            foreach (string file in Directory.GetFiles(TempDir + Path.GetFileName(treeView1.SelectedNode.Parent.Text) + "\\images\\"))
                                File.Copy(file, fbd.SelectedPath + "\\images\\" + Path.GetFileName(file), true);
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
            try
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
            }
        }

        public int SaveFUI(string fui, int index)
        {
            int imgno = 0;
            string dir = TempDir + Path.GetFileName(fui) + "\\images";
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
            while (imgno < Directory.GetFiles(TempDir + Path.GetFileName(fui) + "\\images\\").Length)
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

    }
}
