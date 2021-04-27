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
    public partial class ElementModifier : Form
    {
        #region variables

        public int width = 0;
        public int height = 0;
        public int x = 0;
        public bool vis = true;
        public byte[] originalBytes = { 00, 00 };
        public List<byte> outputBytes = new List<byte>(00);
        public byte[] Result { get; set; }
        public bool ok = false;

        #endregion

        public ElementModifier(byte[] elem, string name)
        {
            InitializeComponent();
            width = elem[14];
            height = elem[26];
            x = elem[30];
            originalBytes = elem;
            this.Text = this.Text.Replace("%s", name);
            ElementName.Text = ElementName.Text.Replace("%s", name);
        }

        private void ElementModifier_Load(object sender, EventArgs e)
        {
            outputBytes.AddRange(originalBytes);
            XInt.Value = x;
            widthNum.Value = width;
            heightNum.Value = height;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            outputBytes[14] = (byte)int.Parse(widthNum.Value.ToString());
            outputBytes[26] = (byte)int.Parse(heightNum.Value.ToString());
            outputBytes[30] = (byte)int.Parse(XInt.Value.ToString());
            ok = true;
            Close();
        }

        public static ResultFromFrmMain Execute(byte[] input, string name)
        {
            using (var f = new ElementModifier(input, name))
            {
                var result = new ResultFromFrmMain();
                result.Result = f.ShowDialog();
                if (f.ok)
                    result.Result = DialogResult.OK;

                if (result.Result == DialogResult.OK)
                {
                    f.Result = f.outputBytes.ToArray();
                }
                return result;
            }
        }
    }

    public class ResultFromFrmMain
    {
        public DialogResult Result { get; set; }
        public byte[] Field1 { get; set; }


    }
}
