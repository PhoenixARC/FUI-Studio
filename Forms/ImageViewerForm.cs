using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace FUI_Studio.Forms
{
    public partial class ImageViewerForm : Form
    {
        public ImageViewerForm(byte[] ImgData, string Name)
        {
            InitializeComponent();
            Text = "Image Viewer - " + Name;
        }
        string path = "";
        private void ImageViewerForm_Load(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            MemoryStream fs = new MemoryStream(File.ReadAllBytes(path));
            Bitmap bmp = new Bitmap(Bitmap.FromStream(fs));
            pictureBox1.Image = bmp;
        }
    }
}
