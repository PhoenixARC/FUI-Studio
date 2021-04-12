﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO.Compression;
using System.Net;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Resources;

namespace FUI_Studio.Forms
{
    public partial class Form1 : Form
    {
        public Form1(string[] args)
        {
            InitializeComponent();
            if (args.Length > 0 && File.Exists(args[0]) && args[0].EndsWith(".fui"))
                OpenFUI(args[0]);
        }

        #region variables

        public List<string> imgList = new List<string>();
        public string TempDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Fui Studio\\";

        #endregion

        #region Opening FUIs

        public void OpenFUI(string fui)
        {
            try
            {
                foreach (string dir in Directory.GetDirectories(TempDir))
                {
                    Directory.Delete(dir, true);
                }
            }
            catch { }
            try
            {
                string datax = Classes.HexTools.ByteArrayToHexString(fui);
                string basefile = datax.Split(new[] { "FF D8 FF E0", "89 50 4E 47" }, StringSplitOptions.None)[0];
                Directory.CreateDirectory(TempDir + Path.GetFileName(fui) + "\\images\\");


                MemoryStream fsx = new MemoryStream(Classes.HexTools.StringToByteArrayFastest(basefile.Replace(" ", "")));
                var fileStreamx = new FileStream(TempDir + Path.GetFileName(fui) + "\\" + Path.GetFileNameWithoutExtension(fui) + ".bin", FileMode.Create, FileAccess.Write);
                fsx.CopyTo(fileStreamx);
                fileStreamx.Dispose();
                ImageList imageList = new ImageList();
                imageList.ColorDepth = ColorDepth.Depth32Bit;
                imageList.ImageSize = new Size(20, 20);
                imageList.Images.Add(Properties.Resources.Selected);
                imageList.Images.Add(Properties.Resources.FolderIcon);
                imageList.Images.Add(Properties.Resources.ImgIcon);
                imageList.Images.Add(Properties.Resources.metaa);
                treeView1.ImageList = imageList;
                int imageNo = 0;
                treeView1.Nodes.Clear();
                TreeNode tnx = new TreeNode();
                tnx.Text = Path.GetFileName(fui);
                tnx.Tag = fui;
                tnx.ImageIndex = 1;


                TreeNode tn = new TreeNode();
                tn.Text = "images";
                tn.Tag = TempDir + Path.GetFileName(fui) + "\\images\\";
                tn.ImageIndex = 1;

                tnx.Nodes.Add(tn);

                TreeNode tnb = new TreeNode();
                tnb.Text = "references";
                tnb.ImageIndex = 1;

                tnx.Nodes.Add(tnb);

                imgList.Clear();
                Classes.ImageProcessor.extractImage(fui, imgList);


                string[] data = Classes.HexTools.ByteArrayToHexString(fui).Replace("-"," ").Split(new[] { "00 " }, StringSplitOptions.None);

                foreach (string image in imgList)
                {
                    if (image.StartsWith("89 50 4E 47"))
                    {

                        MemoryStream fs = new MemoryStream(Classes.HexTools.StringToByteArrayFastest(image.Replace(" ", "")));
                        var fileStream = new FileStream(tn.Tag.ToString() + "Tile" + imageNo + ".png", FileMode.Create, FileAccess.Write);
                        fs.CopyTo(fileStream);
                        fileStream.Dispose();
                        TreeNode tn1 = new TreeNode();
                        tn1.Text = Path.GetFileName(tn.Tag.ToString() + "Tile" + imageNo + ".png");
                        tn1.Tag = tn.Tag.ToString() + "Tile" + imageNo + ".png";
                        tn1.ImageIndex = 2;
                        tn.Nodes.Add(tn1);
                    }
                    if (image.StartsWith("FF D8 FF E0"))
                    {

                        MemoryStream fs = new MemoryStream(Classes.HexTools.StringToByteArrayFastest(image.Replace(" ", "")));
                        var fileStream = new FileStream(tn.Tag.ToString() + "Tile" + imageNo + ".jpg", FileMode.Create, FileAccess.Write);
                        fs.CopyTo(fileStream);
                        fileStream.Dispose();
                        TreeNode tn1 = new TreeNode();
                        tn1.Text = Path.GetFileName(tn.Tag.ToString() + "Tile" + imageNo + ".jpg");
                        tn1.Tag = tn.Tag.ToString() + "Tile" + imageNo + ".jpg";
                        tn1.ImageIndex = 2;
                        tn.Nodes.Add(tn1);
                    }
                    imageNo++;
                }
                
                foreach(string reference in data)
                {
                    if (reference.Contains("2E 73 77 66"))
                    {
                        string newdata = System.Text.Encoding.Default.GetString(Classes.HexTools.StringToByteArrayFastest((reference.Replace("FF FF ", "").Replace(" ", ""))));

                        TreeNode tn1 = new TreeNode();
                        tn1.Text = newdata.Replace(".swf", ".fui");
                        tn1.ImageIndex = 3;
                        tnb.Nodes.Add(tn1);

                    }
                }

                treeView1.Nodes.Add(tnx);
                foreach (TreeNode reference in tnb.Nodes)
                {
                    if (reference.Text != Path.GetFileName(fui) && File.Exists(Path.GetDirectoryName(fui) + "\\" + reference.Text))
                    {
                        DialogResult dr = MessageBox.Show("Dependent on:" + reference.Text + "\nLoad File?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (dr == DialogResult.Yes)
                        {
                            OpenRefFui(Path.GetDirectoryName(fui) + "\\" + reference.Text);
                        }

                    }
                }
            }
            catch
            {

            }
        }

        public void OpenRefFui(string fui)
        {
            try
            {
                string datax = Classes.HexTools.ByteArrayToHexString(fui);
                string basefile = datax.Split(new[] { "FF D8 FF E0", "89 50 4E 47" }, StringSplitOptions.None)[0];
                Directory.CreateDirectory(TempDir + Path.GetFileName(fui) + "\\images\\");


                MemoryStream fsx = new MemoryStream(Classes.HexTools.StringToByteArrayFastest(basefile.Replace(" ", "")));
                var fileStreamx = new FileStream(TempDir + Path.GetFileName(fui) + "\\" + Path.GetFileNameWithoutExtension(fui) + ".bin", FileMode.Create, FileAccess.Write);
                fsx.CopyTo(fileStreamx);
                fileStreamx.Dispose();
                ImageList imageList = new ImageList();
                imageList.ColorDepth = ColorDepth.Depth32Bit;
                imageList.ImageSize = new Size(20, 20);
                imageList.Images.Add(Properties.Resources.Selected);
                imageList.Images.Add(Properties.Resources.FolderIcon);
                imageList.Images.Add(Properties.Resources.ImgIcon);
                imageList.Images.Add(Properties.Resources.metaa);
                treeView1.ImageList = imageList;
                int imageNo = 0;
                TreeNode tnx = new TreeNode();
                tnx.Text = Path.GetFileName(fui);
                tnx.Tag = fui;
                tnx.ImageIndex = 1;


                TreeNode tn = new TreeNode();
                tn.Text = "images";
                tn.Tag = TempDir + Path.GetFileName(fui) + "\\images\\";
                tn.ImageIndex = 1;

                tnx.Nodes.Add(tn);

                TreeNode tnb = new TreeNode();
                tnb.Text = "references";
                tnb.ImageIndex = 1;

                tnx.Nodes.Add(tnb);

                imgList.Clear();
                Classes.ImageProcessor.extractImage(fui, imgList);


                string[] data = Classes.HexTools.ByteArrayToHexString(fui).Replace("-", " ").Split(new[] { "00 " }, StringSplitOptions.None);

                foreach (string image in imgList)
                {
                    if (image.StartsWith("89 50 4E 47"))
                    {

                        MemoryStream fs = new MemoryStream(Classes.HexTools.StringToByteArrayFastest(image.Replace(" ", "")));
                        var fileStream = new FileStream(tn.Tag.ToString() + "Tile" + imageNo + ".png", FileMode.Create, FileAccess.Write);
                        fs.CopyTo(fileStream);
                        fileStream.Dispose();
                        TreeNode tn1 = new TreeNode();
                        tn1.Text = Path.GetFileName(tn.Tag.ToString() + "Tile" + imageNo + ".png");
                        tn1.Tag = tn.Tag.ToString() + "Tile" + imageNo + ".png";
                        tn1.ImageIndex = 2;
                        tn.Nodes.Add(tn1);
                    }
                    if (image.StartsWith("FF D8 FF E0"))
                    {

                        MemoryStream fs = new MemoryStream(Classes.HexTools.StringToByteArrayFastest(image.Replace(" ", "")));
                        var fileStream = new FileStream(tn.Tag.ToString() + "Tile" + imageNo + ".jpg", FileMode.Create, FileAccess.Write);
                        fs.CopyTo(fileStream);
                        fileStream.Dispose();
                        TreeNode tn1 = new TreeNode();
                        tn1.Text = Path.GetFileName(tn.Tag.ToString() + "Tile" + imageNo + ".jpg");
                        tn1.Tag = tn.Tag.ToString() + "Tile" + imageNo + ".jpg";
                        tn1.ImageIndex = 2;
                        tn.Nodes.Add(tn1);
                    }
                    imageNo++;
                }

                foreach (string reference in data)
                {
                    if (reference.Contains("2E 73 77 66"))
                    {
                        string newdata = System.Text.Encoding.Default.GetString(Classes.HexTools.StringToByteArrayFastest((reference.Replace("FF FF ", "").Replace(" ", ""))));

                        TreeNode tn1 = new TreeNode();
                        tn1.Text = newdata.Replace(".swf", ".fui");
                        tn1.ImageIndex = 3;
                        tnb.Nodes.Add(tn1);

                    }
                }

                treeView1.Nodes.Add(tnx);
            }
            catch
            {

            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            if (opf.ShowDialog() == DialogResult.OK)
                OpenFUI(opf.FileName);
        }

        #endregion

        #region TreeView Selection
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            checkBox1.Checked = false;
            if (treeView1.SelectedNode.Text.EndsWith(".png") || treeView1.SelectedNode.Text.EndsWith(".jpg"))
            {
                object NodeTag = treeView1.SelectedNode.Tag;

                MemoryStream fs = new MemoryStream(File.ReadAllBytes(NodeTag.ToString()));
                Bitmap bmp = new Bitmap(Bitmap.FromStream(fs));
                pictureBox1.Image = bmp;

                extractToolStripMenuItem.Enabled = true;
                replaceToolStripMenuItem.Enabled = true;
                if(treeView1.SelectedNode.Text.EndsWith(".png"))
                checkBox1.Enabled = true;


            }
            else
            {
                pictureBox1.Image = null;
                extractToolStripMenuItem.Enabled = false;
                replaceToolStripMenuItem.Enabled = false;
                checkBox1.Enabled = false;
            }
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.InterpolationMode = InterpolationMode.NearestNeighbor;
            Directory.CreateDirectory(TempDir);
            Classes.Networking.checkUpdate();
            if (Classes.Networking.NeedsUpdate)
                UPDATEToolStripMenuItem.Visible = true;
        }

        private void openWorkingDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(TempDir);
        }

        private void extractToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
                    File.Copy(selectedImage, sfd.FileName);
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
                if (checkBox1.Checked)
                {
                    object NodeTag = treeView1.SelectedNode.Tag;

                    MemoryStream fs = new MemoryStream(File.ReadAllBytes(sfd.FileName));
                    Bitmap bmp = new Bitmap(Bitmap.FromStream(fs));
                    Classes.ImageProcessor.ReverseColorRB(bmp);
                    bmp.Save(NodeTag.ToString(), ImageFormat.Png);
                    return;
                }
                File.Copy(sfd.FileName, treeView1.SelectedNode.Tag.ToString());
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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach(string dir in Directory.GetDirectories(TempDir))
            {
                Directory.Delete(dir, true);
            }
        }


        #region Saving FUIs
        
        
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(TreeNode file in treeView1.Nodes)
            {
                SaveFUI(file.Tag.ToString());
            }
        }

        public void SaveFUI(string fui)
        {
            int imgno = 0;
            string dir = TempDir + Path.GetFileName(fui) + "\\images";
            string outputdata = Classes.HexTools.ByteArrayToHexString(Path.GetDirectoryName(dir) + "\\" + Path.GetFileNameWithoutExtension(fui) + ".bin").Replace("-","");
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
        }

        #endregion

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Forms.about().ShowDialog();
        }

        private void UPDATEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + "\\updater.exe");
            Application.Exit();
        }
    }
}
