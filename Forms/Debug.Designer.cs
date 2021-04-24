
namespace FUI_Studio.Forms
{
    partial class Debug
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
            this.LabelsBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.FontBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // LabelsBox
            // 
            this.LabelsBox.AutoSize = true;
            this.LabelsBox.Location = new System.Drawing.Point(13, 13);
            this.LabelsBox.Name = "LabelsBox";
            this.LabelsBox.Size = new System.Drawing.Size(94, 17);
            this.LabelsBox.TabIndex = 0;
            this.LabelsBox.Text = "Display Labels";
            this.LabelsBox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(95, 450);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FontBox
            // 
            this.FontBox.AutoSize = true;
            this.FontBox.Checked = true;
            this.FontBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.FontBox.Location = new System.Drawing.Point(13, 36);
            this.FontBox.Name = "FontBox";
            this.FontBox.Size = new System.Drawing.Size(84, 17);
            this.FontBox.TabIndex = 2;
            this.FontBox.Text = "Display Font";
            this.FontBox.UseVisualStyleBackColor = true;
            // 
            // Debug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 485);
            this.Controls.Add(this.FontBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LabelsBox);
            this.Name = "Debug";
            this.Text = "Debug";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox LabelsBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox FontBox;
    }
}