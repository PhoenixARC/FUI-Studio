using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FUI_Studio.Forms
{
    public partial class FontViewerForm : Form
    {
        public FontViewerForm(string htmlUri, string nom)
        {
            InitializeComponent();
            uri = htmlUri;
            name = nom;
        }

        string uri = "";
        string name = "";
        PrivateFontCollection pfc = new PrivateFontCollection();

        private void FontViewerForm_Load(object sender, EventArgs e)
        {
            pfc.AddFontFile(Environment.CurrentDirectory + "\\Mojangles.ttf");
            webBrowser1.DocumentText = uri.Replace("Mojangles7", "Mojangles").Replace("Mojangles11", "Mojangles") + "\n<body style=\"background - color:grey; \">";
            this.Text = "Font Viewer - " + name;
        }
    }
}
