namespace USBHDDSpy
{
    partial class MainConsole
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.MinimumLabel = new System.Windows.Forms.Label();
            this.ExitLabel = new System.Windows.Forms.Label();
            this.NoticeBar = new System.Windows.Forms.NotifyIcon(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.FunctionalRegion = new System.Windows.Forms.GroupBox();
            this.AuthorInfo = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.FormatSelectCkB = new System.Windows.Forms.CheckBox();
            this.WhereToSaveCkB = new System.Windows.Forms.CheckBox();
            this.PathSelectBtn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.FinishedMsgCkB = new System.Windows.Forms.CheckBox();
            this.AutoCopyCkB = new System.Windows.Forms.CheckBox();
            this.StartCkB = new System.Windows.Forms.CheckBox();
            this.ConfirmBtn = new System.Windows.Forms.Button();
            this.RecognitionLabel = new System.Windows.Forms.Label();
            this.FunctionalRegion.SuspendLayout();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Microsoft YaHei UI Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TitleLabel.Location = new System.Drawing.Point(4, 4);
            this.TitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(94, 20);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "USBHDDSpy";
            // 
            // MinimumLabel
            // 
            this.MinimumLabel.AutoSize = true;
            this.MinimumLabel.Location = new System.Drawing.Point(627, 4);
            this.MinimumLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.MinimumLabel.Name = "MinimumLabel";
            this.MinimumLabel.Size = new System.Drawing.Size(25, 20);
            this.MinimumLabel.TabIndex = 1;
            this.MinimumLabel.Text = "—";
            // 
            // ExitLabel
            // 
            this.ExitLabel.AutoSize = true;
            this.ExitLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ExitLabel.Location = new System.Drawing.Point(598, 4);
            this.ExitLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ExitLabel.Name = "ExitLabel";
            this.ExitLabel.Size = new System.Drawing.Size(20, 20);
            this.ExitLabel.TabIndex = 2;
            this.ExitLabel.Text = "×";
            // 
            // NoticeBar
            // 
            this.NoticeBar.Text = "notifyIcon1";
            this.NoticeBar.Visible = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 279);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 18, 0);
            this.statusStrip1.Size = new System.Drawing.Size(658, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // FunctionalRegion
            // 
            this.FunctionalRegion.Controls.Add(this.RecognitionLabel);
            this.FunctionalRegion.Controls.Add(this.AuthorInfo);
            this.FunctionalRegion.Controls.Add(this.textBox2);
            this.FunctionalRegion.Controls.Add(this.FormatSelectCkB);
            this.FunctionalRegion.Controls.Add(this.WhereToSaveCkB);
            this.FunctionalRegion.Controls.Add(this.PathSelectBtn);
            this.FunctionalRegion.Controls.Add(this.textBox1);
            this.FunctionalRegion.Controls.Add(this.FinishedMsgCkB);
            this.FunctionalRegion.Controls.Add(this.AutoCopyCkB);
            this.FunctionalRegion.Controls.Add(this.StartCkB);
            this.FunctionalRegion.Controls.Add(this.ConfirmBtn);
            this.FunctionalRegion.Location = new System.Drawing.Point(4, 27);
            this.FunctionalRegion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FunctionalRegion.Name = "FunctionalRegion";
            this.FunctionalRegion.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FunctionalRegion.Size = new System.Drawing.Size(651, 245);
            this.FunctionalRegion.TabIndex = 5;
            this.FunctionalRegion.TabStop = false;
            this.FunctionalRegion.Text = "功能与选项";
            // 
            // AuthorInfo
            // 
            this.AuthorInfo.AutoSize = true;
            this.AuthorInfo.Location = new System.Drawing.Point(318, 155);
            this.AuthorInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.AuthorInfo.Name = "AuthorInfo";
            this.AuthorInfo.Size = new System.Drawing.Size(129, 20);
            this.AuthorInfo.TabIndex = 10;
            this.AuthorInfo.Text = "关于USBHDDSpy";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(318, 120);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(297, 27);
            this.textBox2.TabIndex = 9;
            // 
            // FormatSelectCkB
            // 
            this.FormatSelectCkB.AutoSize = true;
            this.FormatSelectCkB.Location = new System.Drawing.Point(318, 91);
            this.FormatSelectCkB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FormatSelectCkB.Name = "FormatSelectCkB";
            this.FormatSelectCkB.Size = new System.Drawing.Size(298, 24);
            this.FormatSelectCkB.TabIndex = 8;
            this.FormatSelectCkB.Text = "仅复制以下扩展名的文件（以逗号隔开）";
            this.FormatSelectCkB.UseVisualStyleBackColor = true;
            // 
            // WhereToSaveCkB
            // 
            this.WhereToSaveCkB.AutoSize = true;
            this.WhereToSaveCkB.Location = new System.Drawing.Point(64, 154);
            this.WhereToSaveCkB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.WhereToSaveCkB.Name = "WhereToSaveCkB";
            this.WhereToSaveCkB.Size = new System.Drawing.Size(193, 24);
            this.WhereToSaveCkB.TabIndex = 6;
            this.WhereToSaveCkB.Text = "使用以下路径以保存拷贝";
            this.WhereToSaveCkB.UseVisualStyleBackColor = true;
            // 
            // PathSelectBtn
            // 
            this.PathSelectBtn.Location = new System.Drawing.Point(459, 211);
            this.PathSelectBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PathSelectBtn.Name = "PathSelectBtn";
            this.PathSelectBtn.Size = new System.Drawing.Size(39, 27);
            this.PathSelectBtn.TabIndex = 5;
            this.PathSelectBtn.Text = "...";
            this.PathSelectBtn.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 211);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(442, 27);
            this.textBox1.TabIndex = 4;
            // 
            // FinishedMsgCkB
            // 
            this.FinishedMsgCkB.AutoSize = true;
            this.FinishedMsgCkB.Location = new System.Drawing.Point(64, 122);
            this.FinishedMsgCkB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FinishedMsgCkB.Name = "FinishedMsgCkB";
            this.FinishedMsgCkB.Size = new System.Drawing.Size(133, 24);
            this.FinishedMsgCkB.TabIndex = 3;
            this.FinishedMsgCkB.Text = "完成后消息提示";
            this.FinishedMsgCkB.UseVisualStyleBackColor = true;
            // 
            // AutoCopyCkB
            // 
            this.AutoCopyCkB.AutoSize = true;
            this.AutoCopyCkB.Checked = true;
            this.AutoCopyCkB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoCopyCkB.Location = new System.Drawing.Point(64, 91);
            this.AutoCopyCkB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AutoCopyCkB.Name = "AutoCopyCkB";
            this.AutoCopyCkB.Size = new System.Drawing.Size(118, 24);
            this.AutoCopyCkB.TabIndex = 2;
            this.AutoCopyCkB.Text = "自动复制文件";
            this.AutoCopyCkB.UseVisualStyleBackColor = true;
            // 
            // StartCkB
            // 
            this.StartCkB.AutoSize = true;
            this.StartCkB.Checked = true;
            this.StartCkB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.StartCkB.Location = new System.Drawing.Point(64, 59);
            this.StartCkB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StartCkB.Name = "StartCkB";
            this.StartCkB.Size = new System.Drawing.Size(103, 24);
            this.StartCkB.TabIndex = 1;
            this.StartCkB.Text = "开机自启动";
            this.StartCkB.UseVisualStyleBackColor = true;
            // 
            // ConfirmBtn
            // 
            this.ConfirmBtn.Location = new System.Drawing.Point(546, 211);
            this.ConfirmBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ConfirmBtn.Name = "ConfirmBtn";
            this.ConfirmBtn.Size = new System.Drawing.Size(96, 27);
            this.ConfirmBtn.TabIndex = 0;
            this.ConfirmBtn.Text = "应用";
            this.ConfirmBtn.UseVisualStyleBackColor = true;
            // 
            // RecognitionLabel
            // 
            this.RecognitionLabel.AutoSize = true;
            this.RecognitionLabel.Location = new System.Drawing.Point(318, 60);
            this.RecognitionLabel.Name = "RecognitionLabel";
            this.RecognitionLabel.Size = new System.Drawing.Size(256, 20);
            this.RecognitionLabel.TabIndex = 11;
            this.RecognitionLabel.Text = "注册受信的U盘（仅能插入单个U盘）";
            // 
            // MainConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(658, 301);
            this.Controls.Add(this.FunctionalRegion);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ExitLabel);
            this.Controls.Add(this.MinimumLabel);
            this.Controls.Add(this.TitleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MainConsole";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Load += new System.EventHandler(this.MainConsole_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainConsole_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainConsole_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainConsole_MouseUp);
            this.FunctionalRegion.ResumeLayout(false);
            this.FunctionalRegion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label TitleLabel;
        private Label MinimumLabel;
        private Label ExitLabel;
        private NotifyIcon NoticeBar;
        private StatusStrip statusStrip1;
        private GroupBox FunctionalRegion;
        private Label AuthorInfo;
        private TextBox textBox2;
        private CheckBox FormatSelectCkB;
        private CheckBox WhereToSaveCkB;
        private Button PathSelectBtn;
        private TextBox textBox1;
        private CheckBox FinishedMsgCkB;
        private CheckBox AutoCopyCkB;
        private CheckBox StartCkB;
        private Button ConfirmBtn;
        private Label RecognitionLabel;
    }
}