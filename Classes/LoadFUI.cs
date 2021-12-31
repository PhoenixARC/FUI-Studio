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




        public static void OpenFUI(string fui, bool isReference, TreeView treeView1, bool DisplayLabels, bool DisplayFont, List<int[]> startEnds, List<string> imgList, bool LoadReferences, bool Loadimages)
        {

            try
            {
                if (!isReference)
                    foreach (string dir in Directory.GetDirectories(Program.TempDir))
                    {
                        Directory.Delete(dir, true);
                    }
            }
            catch { }
            try
            {
                List<string> ImageNames = new List<string>();
                try
                {
                    string EncryptedData = File.ReadAllText(Environment.CurrentDirectory + "\\Data\\FuiIMGData_Enc.db");
                    string decryptedData = Program.Decrypt(EncryptedData);
                    foreach (string Group in decryptedData.Split(new[] { "--" }, StringSplitOptions.None))
                    {
                        string[] Dat = Group.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);
                        if (Path.GetFileName(fui).StartsWith(Dat[0].Replace("WiiU", "WiiU").Replace("PS3", "WiiU").Replace("Vita", "WiiU").Replace("Xbox", "WiiU") + ".fui"))
                        {
                            Console.WriteLine(Path.GetFileNameWithoutExtension(fui) + "  ----  " + Dat[0]);
                            foreach (string Line in Dat)
                            {
                                if (Line != Dat[0])
                                    ImageNames.Add(Line);
                            }
                        }
                    }
                }
                catch { }
                string datax = HexTools.ByteArrayToHexString(fui);
                string basefile = datax.Split(new[] { "FF D8 FF E0", "89 50 4E 47" }, StringSplitOptions.None)[0];
                Directory.CreateDirectory(Program.TempDir + Path.GetFileName(fui) + "\\images\\");


                MemoryStream fsx = new MemoryStream(HexTools.StringToByteArrayFastest(basefile.Replace(" ", "")));
                var fileStreamx = new FileStream(Program.TempDir + Path.GetFileName(fui) + "\\" + Path.GetFileNameWithoutExtension(fui) + ".bin", FileMode.Create, FileAccess.Write);
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

                if (!isReference)
                    treeView1.Nodes.Clear();

                TreeNode tnx = new TreeNode();
                tnx.Text = Path.GetFileName(fui);
                tnx.Tag = fui;
                tnx.ImageIndex = 1;


                TreeNode tn = new TreeNode();
                tn.Text = "images";
                tn.Tag = Program.TempDir + Path.GetFileName(fui) + "\\images\\";
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

                if (Loadimages)
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
                    try
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
                                tn1.Text = ImageNames[imageNo] + ".png";
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
                            tn1.Text = Path.GetFileName(tn.Tag.ToString() + ImageNames[imageNo] + ".jpg");
                            tn1.Tag = tn.Tag.ToString() + "Tile" + imageNo + ".jpg";
                            tn1.ImageIndex = 2;
                            tn.Nodes.Add(tn1);
                        }
                    }
                    catch
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
                    }
                    imageNo++;
                }

                treeView1.Nodes.Add(tnx);
                foreach (TreeNode reference in tnb.Nodes)
                {
                    if (reference.Text.Replace(".ref", "") != Path.GetFileName(fui) && !Directory.Exists(Program.TempDir + reference.Text.Replace(".ref", "")) && File.Exists(Path.GetDirectoryName(fui) + "\\" + reference.Text.Replace(".ref", "")) && LoadReferences)
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

        public static void OpenFUINew(FourJ.FourJUserInterface.FUI fui, bool isReference, TreeView treeView1, int FuiIndex)
        {

            if (!isReference)
            {
                treeView1.Nodes.Clear();
            }
            TreeNode TN0 = new TreeNode(fui.header.SwfFileName.Replace(System.Text.Encoding.ASCII.GetString(new byte[] { 00 }), ""));


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

            TN0.Tag = FuiIndex;
            TN0.ImageIndex = 1;


            if (fui.timelines.Count != 0)
            {
                TreeNode TN1 = new TreeNode("Timelines");

                TN1.ImageIndex = 1;

                int i = 0;

                foreach (FourJ.FourJUserInterface.Timeline tl in fui.timelines)
                {
                    TreeNode TN2 = new TreeNode("fuiTimeline" + i);

                    TN2.ImageIndex = 3;
                    

                    TN2.Tag = i;
                    i++;


                    if (tl.FrameCount != 0)
                    {
                        TreeNode TN3 = new TreeNode("TimelineFrames");

                        TN3.ImageIndex = 1;

                        int x = tl.FrameIndex;

                        while (x < tl.FrameIndex + tl.FrameCount)
                        {
                            TreeNode TN4 = new TreeNode("fuiTimelineFrame" + x);

                            TN4.ImageIndex = 3;

                            TN4.Tag = x;
                            x++;


                            if (fui.timelineFrames[x - 1].EventCount != 0)
                            {
                                TreeNode TN5 = new TreeNode("TimelineEvents");

                                TN5.ImageIndex = 1;
                                int y = (fui.timelineFrames[x - 1].EventIndex);

                                while (y < (fui.timelineFrames[x - 1].EventIndex + fui.timelineFrames[x - 1].EventCount))
                                {
                                    TreeNode TN6 = new TreeNode("fuiTimelineEvent" + y);

                                    TN6.ImageIndex = 3;

                                    TN6.Tag = y;
                                    y++;


                                    if (BitConverter.ToInt16(fui.timelineEvents[y - 1].NameIndex, 0) >= 0) 
                                    {
                                        TreeNode TN7 = new TreeNode("TimelineEventNames");

                                        TN7.ImageIndex = 1;

                                        Int16 z = BitConverter.ToInt16(fui.timelineEvents[y - 1].NameIndex, 0);

                                        TreeNode TN8 = new TreeNode("fuiTimelineEventName" + z);

                                        TN8.ImageIndex = 3;

                                        TN8.Tag = int.Parse(z + "");

                                        TN7.Nodes.Add(TN8);


                                        TN6.Nodes.Add(TN7);
                                    }


                                    TN5.Nodes.Add(TN6);
                                }


                                TN4.Nodes.Add(TN5);
                            }

                            TN3.Nodes.Add(TN4);
                        }


                        TN2.Nodes.Add(TN3);
                    }
                    if (tl.ActionCount != 0)
                    {
                        TreeNode TN3 = new TreeNode("TimelineActions");

                        TN3.ImageIndex = 1;

                        int x = tl.ActionIndex;

                        while (x < tl.ActionIndex + tl.ActionCount)
                        {
                            TreeNode TN4 = new TreeNode("fuiTimelineAction" + x);

                            TN4.ImageIndex = 3;

                            TN4.Tag = x;
                            x++;

                            TN3.Nodes.Add(TN4);
                        }


                        TN2.Nodes.Add(TN3);
                    }

                    TN1.Nodes.Add(TN2);
                }


                TN0.Nodes.Add(TN1);
            }
            if (fui.shapes.Count != 0)
            {
                TreeNode TN1 = new TreeNode("Shapes");

                TN1.ImageIndex = 1;

                int i = 0;

                foreach (FourJ.FourJUserInterface.Shape tl in fui.shapes)
                {
                    TreeNode TN2 = new TreeNode("fuiShape" + i);

                    TN2.ImageIndex = 3;

                    TN2.Tag = i;
                    i++;

                    TN1.Nodes.Add(TN2);
                }


                TN0.Nodes.Add(TN1);
            }
            if (fui.shapeComponents.Count != 0)
            {
                TreeNode TN1 = new TreeNode("ShapeComponents");

                TN1.ImageIndex = 1;

                int i = 0;

                foreach (FourJ.FourJUserInterface.ShapeComponent tl in fui.shapeComponents)
                {
                    TreeNode TN2 = new TreeNode("fuiShapeComponent" + i);

                    TN2.ImageIndex = 3;

                    TN2.Tag = i;
                    i++;

                    TN1.Nodes.Add(TN2);
                }


                TN0.Nodes.Add(TN1);
            }
            if (fui.verts.Count != 0)
            {
                TreeNode TN1 = new TreeNode("Verts");

                TN1.ImageIndex = 1;

                int i = 0;

                foreach (FourJ.FourJUserInterface.Vert tl in fui.verts)
                {
                    TreeNode TN2 = new TreeNode("fuiVert" + i);

                    TN2.ImageIndex = 3;

                    TN2.Tag = i;
                    i++;

                    TN1.Nodes.Add(TN2);
                }


                TN0.Nodes.Add(TN1);
            }
            if (fui.references.Count != 0)
            {
                TreeNode TN1 = new TreeNode("References");

                TN1.ImageIndex = 1;

                int i = 0;

                foreach (FourJ.FourJUserInterface.Reference tl in fui.references)
                {
                    TreeNode TN2 = new TreeNode("fuiReference" + i);

                    TN2.ImageIndex = 3;

                    TN2.Tag = i;
                    i++;

                    TN1.Nodes.Add(TN2);
                }


                TN0.Nodes.Add(TN1);
            }
            if (fui.edittexts.Count != 0)
            {
                TreeNode TN1 = new TreeNode("EditTexts");

                TN1.ImageIndex = 1;

                int i = 0;

                foreach (FourJ.FourJUserInterface.Edittext tl in fui.edittexts)
                {
                    TreeNode TN2 = new TreeNode("fuiEditText" + i);

                    TN2.ImageIndex = 3;

                    TN2.Tag = i;
                    i++;

                    TN1.Nodes.Add(TN2);
                }


                TN0.Nodes.Add(TN1);
            }
            if (fui.fontNames.Count != 0)
            {
                TreeNode TN1 = new TreeNode("FontNames");

                TN1.ImageIndex = 1;

                int i = 0;

                foreach (FourJ.FourJUserInterface.FontName tl in fui.fontNames)
                {
                    TreeNode TN2 = new TreeNode("fuiFontName" + i);

                    TN2.ImageIndex = 3;

                    TN2.Tag = i;
                    i++;

                    TN1.Nodes.Add(TN2);
                }


                TN0.Nodes.Add(TN1);
            }
            if (fui.symbols.Count != 0)
            {
                TreeNode TN1 = new TreeNode("Symbols");

                TN1.ImageIndex = 1;

                int i = 0;

                foreach (FourJ.FourJUserInterface.Symbol tl in fui.symbols)
                {
                    TreeNode TN2 = new TreeNode("fuiSymbol" + i);

                    TN2.ImageIndex = 3;

                    TN2.Tag = i;
                    i++;

                    TN1.Nodes.Add(TN2);
                }


                TN0.Nodes.Add(TN1);
            }
            if (fui.importAssets.Count != 0)
            {
                TreeNode TN1 = new TreeNode("ImportAssets");

                TN1.ImageIndex = 1;

                int i = 0;

                foreach (FourJ.FourJUserInterface.ImportAsset tl in fui.importAssets)
                {
                    TreeNode TN2 = new TreeNode("fuiImportAsset" + i);

                    TN2.ImageIndex = 3;

                    TN2.Tag = i;
                    i++;

                    TN1.Nodes.Add(TN2);
                }


                TN0.Nodes.Add(TN1);
            }
            if (fui.bitmaps.Count != 0)
            {
                TreeNode TN1 = new TreeNode("Bitmaps");

                TN1.ImageIndex = 1;

                int i = 0;

                foreach (FourJ.FourJUserInterface.Bitmap tl in fui.bitmaps)
                {
                    TreeNode TN2 = new TreeNode("fuiBitmap" + i);

                    TN2.ImageIndex = 2;

                    TN2.Tag = i;
                    i++;

                    TN1.Nodes.Add(TN2);
                }


                TN0.Nodes.Add(TN1);
            }

            treeView1.Nodes.Add(TN0);
        }
    }
}
