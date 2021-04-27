using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FUI_Studio.Forms
{
    public partial class Debug : Form
    {
        public Debug(string[] args)
        {
            InitializeComponent();
            argumants = args;
        }

        string[] argumants = { };

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            if (argumants[0] == "true")
            {
                Form1 f1 = new Form1(argumants, LabelsBox.Checked, FontBox.Checked, RunReferences.Checked, LoadImages.Checked, saveElements.Checked);
                f1.Show();
            }
            else
            {
                Form1 f1 = new Form1(argumants, LabelsBox.Checked, FontBox.Checked, RunReferences.Checked, LoadImages.Checked, saveElements.Checked);
                f1.Show();
            }
        }
    }
}
