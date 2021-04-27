
namespace FUI_Studio.Forms
{
    partial class ElementModifier
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
            this.ElementName = new System.Windows.Forms.Label();
            this.IsVisible = new System.Windows.Forms.CheckBox();
            this.XInt = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.heightNum = new System.Windows.Forms.NumericUpDown();
            this.widthNum = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.YInt = new System.Windows.Forms.Label();
            this.Width = new System.Windows.Forms.Label();
            this.Height = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.XInt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthNum)).BeginInit();
            this.SuspendLayout();
            // 
            // ElementName
            // 
            this.ElementName.AutoSize = true;
            this.ElementName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ElementName.Location = new System.Drawing.Point(13, 13);
            this.ElementName.Name = "ElementName";
            this.ElementName.Size = new System.Drawing.Size(79, 13);
            this.ElementName.TabIndex = 0;
            this.ElementName.Text = "Element:  %s";
            // 
            // IsVisible
            // 
            this.IsVisible.AutoSize = true;
            this.IsVisible.Location = new System.Drawing.Point(305, 0);
            this.IsVisible.Name = "IsVisible";
            this.IsVisible.Size = new System.Drawing.Size(56, 17);
            this.IsVisible.TabIndex = 1;
            this.IsVisible.Text = "Visible";
            this.IsVisible.UseVisualStyleBackColor = true;
            this.IsVisible.Visible = false;
            // 
            // XInt
            // 
            this.XInt.Location = new System.Drawing.Point(47, 65);
            this.XInt.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.XInt.Name = "XInt";
            this.XInt.Size = new System.Drawing.Size(120, 20);
            this.XInt.TabIndex = 2;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Enabled = false;
            this.numericUpDown2.Location = new System.Drawing.Point(205, 65);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown2.TabIndex = 3;
            // 
            // heightNum
            // 
            this.heightNum.Location = new System.Drawing.Point(228, 120);
            this.heightNum.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.heightNum.Name = "heightNum";
            this.heightNum.Size = new System.Drawing.Size(120, 20);
            this.heightNum.TabIndex = 5;
            // 
            // widthNum
            // 
            this.widthNum.Location = new System.Drawing.Point(46, 120);
            this.widthNum.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.widthNum.Name = "widthNum";
            this.widthNum.Size = new System.Drawing.Size(120, 20);
            this.widthNum.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Location";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Size";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "X";
            // 
            // YInt
            // 
            this.YInt.AutoSize = true;
            this.YInt.Location = new System.Drawing.Point(185, 67);
            this.YInt.Name = "YInt";
            this.YInt.Size = new System.Drawing.Size(14, 13);
            this.YInt.TabIndex = 9;
            this.YInt.Text = "Y";
            // 
            // Width
            // 
            this.Width.AutoSize = true;
            this.Width.Location = new System.Drawing.Point(7, 122);
            this.Width.Name = "Width";
            this.Width.Size = new System.Drawing.Size(32, 13);
            this.Width.TabIndex = 10;
            this.Width.Text = "width";
            // 
            // Height
            // 
            this.Height.AutoSize = true;
            this.Height.Location = new System.Drawing.Point(184, 122);
            this.Height.Name = "Height";
            this.Height.Size = new System.Drawing.Size(36, 13);
            this.Height.TabIndex = 11;
            this.Height.Text = "height";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(135, 173);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 12;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.button1_Click);
            // 
            // ElementModifier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 204);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.Height);
            this.Controls.Add(this.Width);
            this.Controls.Add(this.YInt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.heightNum);
            this.Controls.Add(this.widthNum);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.XInt);
            this.Controls.Add(this.IsVisible);
            this.Controls.Add(this.ElementName);
            this.Name = "ElementModifier";
            this.Text = "Element Modifier (%s)";
            this.Load += new System.EventHandler(this.ElementModifier_Load);
            ((System.ComponentModel.ISupportInitialize)(this.XInt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.heightNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.widthNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ElementName;
        private System.Windows.Forms.CheckBox IsVisible;
        private System.Windows.Forms.NumericUpDown XInt;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown heightNum;
        private System.Windows.Forms.NumericUpDown widthNum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label YInt;
        private System.Windows.Forms.Label Width;
        private System.Windows.Forms.Label Height;
        private System.Windows.Forms.Button buttonOK;
    }
}