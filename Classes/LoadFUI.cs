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

namespace FUI_Studio.Classes
{
    static class LoadFUI
    {



        public static string TempDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Fui Studio\\";


        public static void OpenFUI(string fui, bool isReference, TreeView treeView1, bool DisplayLabels, bool DisplayFont, List<int[]> startEnds, List<string> imgList, bool LoadReferences, bool Loadimages)
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
                string datax = HexTools.ByteArrayToHexString(fui);
                string basefile = datax.Split(new[] { "FF D8 FF E0", "89 50 4E 47" }, StringSplitOptions.None)[0];
                Directory.CreateDirectory(TempDir + Path.GetFileName(fui) + "\\images\\");


                MemoryStream fsx = new MemoryStream(HexTools.StringToByteArrayFastest(basefile.Replace(" ", "")));
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
                imageList.Images.Add(Properties.Resources.font);
                imageList.Images.Add(Properties.Resources.element);
                treeView1.ImageList = imageList;
                int imageNo = 0;
                int PNGNo = 0;

                if(!isReference)
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

                TreeNode tnd = new TreeNode();
                tnd.Text = "Font";
                tnd.ImageIndex = 1;

                TreeNode tnc = new TreeNode();
                tnc.Text = "Labels";
                tnc.ImageIndex = 1;

                TreeNode tn0 = new TreeNode();
                tn0.Text = "Elements(CANNOT SAVE)";
                tn0.ImageIndex = 1;

                tnx.Nodes.Add(tnb);
                if (DisplayLabels)
                    tnx.Nodes.Add(tnc);
                if (DisplayFont)
                    tnx.Nodes.Add(tnd);
                tnx.Nodes.Add(tn0);

                imgList.Clear();

                if(Loadimages)
                    ImageProcessor.extractImage(fui, imgList);

                List<string> LabelList = new List<string>();
                List<string> FontList = new List<string>();

                string[] data = HexTools.ByteArrayToHexString(fui).Replace("-", " ").Split(new[] { "00 " }, StringSplitOptions.None);


                foreach (string reference in data)
                {
                    try
                    {
                        if (!reference.Contains("2E 73 77 66") && HexTools.isAlphaNumeric(System.Text.Encoding.Default.GetString(HexTools.StringToByteArrayFastest((reference.Replace("FF FF ", "").Replace(" ", ""))))) && System.Text.Encoding.Default.GetString(HexTools.StringToByteArrayFastest((reference.Replace("FF FF ", "").Replace(" ", "")))).Length > 3 && DisplayLabels)
                        {

                            string newdata = System.Text.Encoding.Default.GetString(HexTools.StringToByteArrayFastest((reference.Replace("FF FF ", "").Replace(" ", ""))));
                                LabelList.Add(newdata);
                        }
                        if (reference.Contains("3C 70 20 61") && DisplayFont)
                        {

                            string newdata = System.Text.Encoding.Default.GetString(HexTools.StringToByteArrayFastest((reference.Replace("FF FF ", "").Replace(" ", ""))));
                                FontList.Add(newdata);
                        }
                        if (reference.Contains("2E 73 77 66") && LoadReferences)
                        {
                            string newdata = System.Text.Encoding.Default.GetString(HexTools.StringToByteArrayFastest((reference.Replace("FF FF ", "").Replace(" ", ""))));

                            TreeNode tn1 = new TreeNode();
                            tn1.Text = newdata.Replace(".swf", ".fui") + ".ref";
                            tn1.ImageIndex = 3;
                            tnb.Nodes.Add(tn1);

                        }
                    }
                    catch { }
                }
                LabelList.Reverse();
                foreach (string newdata in LabelList)
                {
                    TreeNode tn1 = new TreeNode();
                    tn1.Text = newdata;
                    tn1.ImageIndex = 3;
                    tnc.Nodes.Add(tn1);
                }
                foreach (string newdata in FontList)
                {
                    try
                    {
                        TreeNode tn1 = new TreeNode();
                        string ndat = newdata.Split(new[] { "kerning=\"1\">" }, StringSplitOptions.None)[1].Split(new[] { "<" }, StringSplitOptions.None)[0] + ".fnt";
                        tn1.Text = ndat;
                        tn1.Tag = newdata;
                        tn1.ImageIndex = 4;
                        tnd.Nodes.Add(tn1);
                    }
                    catch { }
                }

                foreach (string image in imgList)
                {
                    if (image.StartsWith("89 50 4E 47"))
                    {
                        try
                        {
                            MemoryStream fs = new MemoryStream(HexTools.StringToByteArrayFastest(image.Replace(" ", "")));
                            var fileStream = new FileStream(tn.Tag.ToString() + "Tile" + imageNo + ".png", FileMode.Create, FileAccess.Write);
                            fs.CopyTo(fileStream);
                            fileStream.Dispose();
                            TreeNode tn1 = new TreeNode();
                            tn1.Text = LabelList[PNGNo] + ".png";
                            tn1.Tag = tn.Tag.ToString() + "Tile" + imageNo + ".png";
                            tn1.ImageIndex = 2;
                            tn.Nodes.Add(tn1);
                        }
                        catch
                        {
                            MemoryStream fs = new MemoryStream(HexTools.StringToByteArrayFastest(image.Replace(" ", "")));
                            var fileStream = new FileStream(tn.Tag.ToString() + "Tile" + imageNo + ".png", FileMode.Create, FileAccess.Write);
                            fs.CopyTo(fileStream);
                            fileStream.Dispose();
                            TreeNode tn1 = new TreeNode();
                            tn1.Text = "Tile" + imageNo + ".png";
                            tn1.Tag = tn.Tag.ToString() + "Tile" + imageNo + ".png";
                            tn1.ImageIndex = 2;
                            tn.Nodes.Add(tn1);
                        }
                        PNGNo++;
                    }
                    if (image.StartsWith("FF D8 FF E0"))
                    {

                        MemoryStream fs = new MemoryStream(HexTools.StringToByteArrayFastest(image.Replace(" ", "")));
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

                treeView1.Nodes.Add(tnx);
                foreach (TreeNode reference in tnb.Nodes)
                {
                    if (reference.Text.Replace(".ref", "") != Path.GetFileName(fui) && File.Exists(Path.GetDirectoryName(fui) + "\\" + reference.Text.Replace(".ref", "")) && LoadReferences)
                    {
                        DialogResult dr = MessageBox.Show(Path.GetFileName(fui) + " is dependent on:" + reference.Text.Replace(".ref", "") + "\nLoad File?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (dr == DialogResult.Yes)
                        {
                            OpenFUI(Path.GetDirectoryName(fui) + "\\" + reference.Text.Replace(".ref", ""), true, treeView1, DisplayLabels, DisplayFont, startEnds, imgList, LoadReferences, Loadimages);
                        }

                    }
                }


                int maxByte = 72;
                int currByte = 0;
                int currRef = 0;
                List<byte[]> outputs = new List<byte[]>();
                byte[] ReadBytes = File.ReadAllBytes(fui);

                int[] pttrn = HexTools.IndexOfSequence(ReadBytes, new byte[] { 05, 00 }, 0).ToArray();
                foreach (int find in pttrn)
                    if (!ReadBytes.Skip(find).Take(maxByte).ToArray().ToString().Contains(".swf"))
                    {
                        var startIndex = find;
                        var length = maxByte;
                        Console.WriteLine("Found at: input[" + find + "]");
                        byte[] output = File.ReadAllBytes(fui).Skip(startIndex).Take(length).ToArray();
                        //File.WriteAllBytes(Environment.CurrentDirectory + "\\export\\fuiChunk" + currByte + ".bin", output);
                        if (!outputs.Contains(output))
                            outputs.Add(output);
                        currByte++;
                    }
                Console.WriteLine(outputs.Count);
                startEnds.Add(new int[] { pttrn[0], pttrn[currByte - 1] + 72 });
                byte[] totalOut = ReadBytes.Skip(pttrn[pttrn.Length - 1] + 72).Take(ReadBytes.Length - (pttrn[pttrn.Length - 1] + 72)).ToArray();

                string[] datay = HexTools.trueByteArrayToHexString(totalOut).Replace("-", " ").Split(new[] { "00 " }, StringSplitOptions.None);

                foreach (string reference in datay)
                {
                    try
                    {
                        string rf = System.Text.Encoding.Default.GetString(HexTools.StringToByteArrayFastest((reference.Replace("FF ", "").Replace(" ", ""))));
                        if (rf.Length > 4 && HexTools.isAlphaNumeric(rf))
                        {
                            TreeNode element = new TreeNode();
                            element.Text = rf + ".elmnt";
                            //Console.WriteLine("outputs[" + currRef + "]");
                            element.Tag = HexTools.trueByteArrayToHexString(outputs[currRef]);
                            element.ImageIndex = 5;
                            tn0.Nodes.Add(element);
                            //File.WriteAllBytes(Environment.CurrentDirectory + "\\export\\" + rf + ".bin", outputs[currRef]);
                            currRef++;
                        }
                    }
                    catch (Exception err)
                    {
                        Console.WriteLine("[E!] " + err.Message);
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("[E!] " + err.Message);
            }
        }

    }
}
