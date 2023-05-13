namespace USBHDDSpy
{
    partial class ProgramInfo
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgramInfo));
            this.LogoImage = new System.Windows.Forms.PictureBox();
            this.ContextLabel = new System.Windows.Forms.Label();
            this.LinkToGithubLabel = new System.Windows.Forms.LinkLabel();
            this.ShowContextTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.LogoImage)).BeginInit();
            this.SuspendLayout();
            // 
            // LogoImage
            // 
            this.LogoImage.ErrorImage = null;
            this.LogoImage.Image = global::USBHDDSpy.Properties.Resources.USBHDDSpyIcon;
            this.LogoImage.Location = new System.Drawing.Point(33, 42);
            this.LogoImage.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.LogoImage.Name = "LogoImage";
            this.LogoImage.Size = new System.Drawing.Size(78, 85);
            this.LogoImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LogoImage.TabIndex = 0;
            this.LogoImage.TabStop = false;
            // 
            // ContextLabel
            // 
            this.ContextLabel.AutoSize = true;
            this.ContextLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.865546F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ContextLabel.Location = new System.Drawing.Point(137, 8);
            this.ContextLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ContextLabel.Name = "ContextLabel";
            this.ContextLabel.Size = new System.Drawing.Size(0, 16);
            this.ContextLabel.TabIndex = 1;
            // 
            // LinkToGithubLabel
            // 
            this.LinkToGithubLabel.AutoSize = true;
            this.LinkToGithubLabel.Font = new System.Drawing.Font("Microsoft YaHei UI", 7.865546F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LinkToGithubLabel.Location = new System.Drawing.Point(137, 137);
            this.LinkToGithubLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LinkToGithubLabel.Name = "LinkToGithubLabel";
            this.LinkToGithubLabel.Size = new System.Drawing.Size(0, 16);
            this.LinkToGithubLabel.TabIndex = 2;
            this.LinkToGithubLabel.Click += new System.EventHandler(this.LinkToGithubLabel_Click);
            // 
            // ShowContextTimer
            // 
            this.ShowContextTimer.Interval = 25;
            this.ShowContextTimer.Tick += new System.EventHandler(this.ShowContextTimer_Tick);
            // 
            // ProgramInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 173);
            this.Controls.Add(this.LinkToGithubLabel);
            this.Controls.Add(this.ContextLabel);
            this.Controls.Add(this.LogoImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProgramInfo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "关于USBHDDSpy";
            this.Load += new System.EventHandler(this.ProgramInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LogoImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox LogoImage;
        private Label ContextLabel;
        private LinkLabel LinkToGithubLabel;
        private System.Windows.Forms.Timer ShowContextTimer;
    }
}