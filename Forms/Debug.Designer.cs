
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Debug));
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
            resources.ApplyResources(this.LabelsBox, "LabelsBox");
            this.LabelsBox.Name = "LabelsBox";
            this.LabelsBox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FontBox
            // 
            resources.ApplyResources(this.FontBox, "FontBox");
            this.FontBox.Name = "FontBox";
            this.FontBox.UseVisualStyleBackColor = true;
            // 
            // RunReferences
            // 
            resources.ApplyResources(this.RunReferences, "RunReferences");
            this.RunReferences.Name = "RunReferences";
            this.RunReferences.UseVisualStyleBackColor = true;
            // 
            // LoadImages
            // 
            resources.ApplyResources(this.LoadImages, "LoadImages");
            this.LoadImages.Name = "LoadImages";
            this.LoadImages.UseVisualStyleBackColor = true;
            // 
            // saveElements
            // 
            resources.ApplyResources(this.saveElements, "saveElements");
            this.saveElements.Name = "saveElements";
            this.saveElements.UseVisualStyleBackColor = true;
            // 
            // Debug
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.saveElements);
            this.Controls.Add(this.LoadImages);
            this.Controls.Add(this.RunReferences);
            this.Controls.Add(this.FontBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LabelsBox);
            this.Name = "Debug";
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