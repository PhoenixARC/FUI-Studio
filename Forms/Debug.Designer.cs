
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
            this.RunReferences = new System.Windows.Forms.CheckBox();
            this.LoadImages = new System.Windows.Forms.CheckBox();
            this.saveElements = new System.Windows.Forms.CheckBox();
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
            this.FontBox.Location = new System.Drawing.Point(13, 36);
            this.FontBox.Name = "FontBox";
            this.FontBox.Size = new System.Drawing.Size(84, 17);
            this.FontBox.TabIndex = 2;
            this.FontBox.Text = "Display Font";
            this.FontBox.UseVisualStyleBackColor = true;
            // 
            // RunReferences
            // 
            this.RunReferences.AutoSize = true;
            this.RunReferences.Location = new System.Drawing.Point(13, 59);
            this.RunReferences.Name = "RunReferences";
            this.RunReferences.Size = new System.Drawing.Size(103, 17);
            this.RunReferences.TabIndex = 3;
            this.RunReferences.Text = "Load references";
            this.RunReferences.UseVisualStyleBackColor = true;
            // 
            // LoadImages
            // 
            this.LoadImages.AutoSize = true;
            this.LoadImages.Location = new System.Drawing.Point(13, 82);
            this.LoadImages.Name = "LoadImages";
            this.LoadImages.Size = new System.Drawing.Size(86, 17);
            this.LoadImages.TabIndex = 4;
            this.LoadImages.Text = "Load images";
            this.LoadImages.UseVisualStyleBackColor = true;
            // 
            // saveElements
            // 
            this.saveElements.AutoSize = true;
            this.saveElements.Location = new System.Drawing.Point(216, 13);
            this.saveElements.Name = "saveElements";
            this.saveElements.Size = new System.Drawing.Size(96, 17);
            this.saveElements.TabIndex = 5;
            this.saveElements.Text = "Save elements";
            this.saveElements.UseVisualStyleBackColor = true;
            // 
            // Debug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 485);
            this.Controls.Add(this.saveElements);
            this.Controls.Add(this.LoadImages);
            this.Controls.Add(this.RunReferences);
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
        private System.Windows.Forms.CheckBox RunReferences;
        private System.Windows.Forms.CheckBox LoadImages;
        private System.Windows.Forms.CheckBox saveElements;
    }
}