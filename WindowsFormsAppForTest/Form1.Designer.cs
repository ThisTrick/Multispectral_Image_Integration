namespace WindowsFormsAppForTest
{
    partial class Form1
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
            this.load1 = new System.Windows.Forms.Button();
            this.clickMe = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.load2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // load1
            // 
            this.load1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.load1.Location = new System.Drawing.Point(0, 501);
            this.load1.Name = "load1";
            this.load1.Size = new System.Drawing.Size(576, 52);
            this.load1.TabIndex = 0;
            this.load1.Text = "load1";
            this.load1.UseVisualStyleBackColor = true;
            this.load1.Click += new System.EventHandler(this.load_Click);
            // 
            // clickMe
            // 
            this.clickMe.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.clickMe.Location = new System.Drawing.Point(0, 450);
            this.clickMe.Name = "clickMe";
            this.clickMe.Size = new System.Drawing.Size(576, 51);
            this.clickMe.TabIndex = 3;
            this.clickMe.Text = "clickMe";
            this.clickMe.UseVisualStyleBackColor = true;
            this.clickMe.Click += new System.EventHandler(this.clickMe_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(576, 373);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            // 
            // load2
            // 
            this.load2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.load2.Location = new System.Drawing.Point(0, 401);
            this.load2.Name = "load2";
            this.load2.Size = new System.Drawing.Size(576, 49);
            this.load2.TabIndex = 2;
            this.load2.Text = "load2";
            this.load2.UseVisualStyleBackColor = true;
            this.load2.Click += new System.EventHandler(this.load2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 553);
            this.Controls.Add(this.load2);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.clickMe);
            this.Controls.Add(this.load1);
            this.MaximumSize = new System.Drawing.Size(940, 600);
            this.MinimumSize = new System.Drawing.Size(550, 600);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button load1;
        private System.Windows.Forms.Button clickMe;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button load2;
    }
}

