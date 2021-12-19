using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using FourJ;

namespace FUI_Studio.Forms
{
    public partial class LoadingFileDialog : Form
    {
        public FourJUserInterface.FUI FUI = new FourJ.FourJUserInterface.FUI();
        public FourJUserInterface.Functions FUIFunct = new FourJUserInterface.Functions();

        public LoadingFileDialog(FourJUserInterface.FUI FUIToLoad, string OpenPath)
        {
            InitializeComponent();
            FUI = FUIToLoad;
            Nom = OpenPath;
        }

        public string Nom = "";
        public int Progress = 0;

        private void LoadingFileDialog_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            FUI.FilePath = Nom;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

            Thread Nt = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                FUIFunct.OpenFUI(Nom, FUI, true);
            });
            Nt.Start();
            Thread some_thread = new Thread
            (delegate ()
            {

                {
                    for (int i = 0; i < 1;)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            progressBar1.Maximum = FUI.TotalProgress;
                            while (progressBar1.Value < FUI.TotalProgress)
                            {
                                label1.Text = ("Loading new FourJUI File: " + (FUI.CurrentProgress) + "/" + FUI.TotalProgress);
                                label2.Text = FUI.status;
                                progressBar1.Value = FUI.CurrentProgress;
                                if (FUI.CurrentProgress == FUI.TotalProgress)
                                {
                                    i++;
                                }
                            }
                            label1.Text = ("Loading new FourJUI File: " + (FUI.CurrentProgress) + "/" + FUI.TotalProgress);
                            label2.Text = FUI.status;
                        });
                    }
                    //this.Close();
                }
            });
            some_thread.Start();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }
    }
}

