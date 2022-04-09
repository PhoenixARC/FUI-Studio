
namespace FUI_Studio.Forms
{
    partial class LoadingFileDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.FUIFileNameLabel = new System.Windows.Forms.Label();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(12, 44);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(406, 23);
            this.progressBar.TabIndex = 0;
            // 
            // FUIFileNameLabel
            // 
            this.FUIFileNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FUIFileNameLabel.AutoSize = true;
            this.FUIFileNameLabel.Location = new System.Drawing.Point(177, 9);
            this.FUIFileNameLabel.Name = "FUIFileNameLabel";
            this.FUIFileNameLabel.Size = new System.Drawing.Size(63, 13);
            this.FUIFileNameLabel.TabIndex = 1;
            this.FUIFileNameLabel.Text = "DummyText";
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.RunWorkerCompleted);
            // 
            // LoadingFileDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 79);
            this.Controls.Add(this.FUIFileNameLabel);
            this.Controls.Add(this.progressBar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadingFileDialog";
            this.Text = "Loading New FUI";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label FUIFileNameLabel;
        public System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}