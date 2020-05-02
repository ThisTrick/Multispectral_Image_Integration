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
            this.load = new System.Windows.Forms.Button();
            this.clickMe = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // load
            // 
            this.load.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.load.Location = new System.Drawing.Point(0, 495);
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(576, 58);
            this.load.TabIndex = 0;
            this.load.Text = "load";
            this.load.UseVisualStyleBackColor = true;
            this.load.Click += new System.EventHandler(this.load_Click);
            // 
            // clickMe
            // 
            this.clickMe.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.clickMe.Location = new System.Drawing.Point(0, 438);
            this.clickMe.Name = "clickMe";
            this.clickMe.Size = new System.Drawing.Size(576, 57);
            this.clickMe.TabIndex = 1;
            this.clickMe.Text = "clickMe";
            this.clickMe.UseVisualStyleBackColor = true;
            this.clickMe.Click += new System.EventHandler(this.clickMe_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(576, 404);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 553);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.clickMe);
            this.Controls.Add(this.load);
            this.MaximumSize = new System.Drawing.Size(940, 600);
            this.MinimumSize = new System.Drawing.Size(550, 600);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button load;
        private System.Windows.Forms.Button clickMe;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}

