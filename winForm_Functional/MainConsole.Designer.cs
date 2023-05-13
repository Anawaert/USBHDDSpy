namespace USBHDDSpy
{
    public partial class MainConsole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainConsole));
            this.TitleLabel = new System.Windows.Forms.Label();
            this.MinimumLabel = new System.Windows.Forms.Label();
            this.ExitLabel = new System.Windows.Forms.Label();
            this.NoticeBar = new System.Windows.Forms.NotifyIcon(this.components);
            this.NoticeBarMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.To_Visible_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下载最新版的USBHDDSpyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.访问开源项目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Exit_Programme_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusShow = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.FunctionalRegion = new System.Windows.Forms.GroupBox();
            this.RecognitionLabel = new System.Windows.Forms.Label();
            this.AuthorInfoLabel = new System.Windows.Forms.Label();
            this.CertainFormatsTextBox = new System.Windows.Forms.TextBox();
            this.FormatSelectCkB = new System.Windows.Forms.CheckBox();
            this.WhereToSaveCkB = new System.Windows.Forms.CheckBox();
            this.PathSelectBtn = new System.Windows.Forms.Button();
            this.SavePathTextBox = new System.Windows.Forms.TextBox();
            this.FinishedMsgCkB = new System.Windows.Forms.CheckBox();
            this.AutoCopyCkB = new System.Windows.Forms.CheckBox();
            this.StartCkB = new System.Windows.Forms.CheckBox();
            this.ConfirmBtn = new System.Windows.Forms.Button();
            this.CopyTimer = new System.Windows.Forms.Timer(this.components);
            this.NoticeBarMenu.SuspendLayout();
            this.StatusShow.SuspendLayout();
            this.FunctionalRegion.SuspendLayout();
            this.SuspendLayout();
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("微软雅黑", 10.28571F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TitleLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.TitleLabel.Location = new System.Drawing.Point(3, 3);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(93, 20);
            this.TitleLabel.TabIndex = 0;
            this.TitleLabel.Text = "USBHDDSpy";
            // 
            // MinimumLabel
            // 
            this.MinimumLabel.AutoSize = true;
            this.MinimumLabel.Location = new System.Drawing.Point(470, 3);
            this.MinimumLabel.Name = "MinimumLabel";
            this.MinimumLabel.Size = new System.Drawing.Size(21, 17);
            this.MinimumLabel.TabIndex = 1;
            this.MinimumLabel.Text = " - ";
            this.MinimumLabel.Click += new System.EventHandler(this.MinimumLabel_Click);
            this.MinimumLabel.MouseLeave += new System.EventHandler(this.MinimumLabel_MouseLeave);
            this.MinimumLabel.MouseHover += new System.EventHandler(this.MinimumLabel_MouseHover);
            // 
            // ExitLabel
            // 
            this.ExitLabel.AutoSize = true;
            this.ExitLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ExitLabel.Location = new System.Drawing.Point(494, 3);
            this.ExitLabel.Name = "ExitLabel";
            this.ExitLabel.Size = new System.Drawing.Size(17, 17);
            this.ExitLabel.TabIndex = 2;
            this.ExitLabel.Text = "×";
            this.ExitLabel.Click += new System.EventHandler(this.ExitLabel_Click);
            this.ExitLabel.MouseLeave += new System.EventHandler(this.ExitLabel_MouseLeave);
            this.ExitLabel.MouseHover += new System.EventHandler(this.ExitLabel_MouseHover);
            // 
            // NoticeBar
            // 
            this.NoticeBar.ContextMenuStrip = this.NoticeBarMenu;
            this.NoticeBar.Icon = ((System.Drawing.Icon)(resources.GetObject("NoticeBar.Icon")));
            this.NoticeBar.Text = "USBHDDSpy";
            this.NoticeBar.Visible = true;
            this.NoticeBar.DoubleClick += new System.EventHandler(this.NoticeBar_DoubleClick);
            // 
            // NoticeBarMenu
            // 
            this.NoticeBarMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.NoticeBarMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.To_Visible_MenuItem,
            this.下载最新版的USBHDDSpyToolStripMenuItem,
            this.访问开源项目ToolStripMenuItem,
            this.取消ToolStripMenuItem,
            this.toolStripSeparator1,
            this.Exit_Programme_MenuItem});
            this.NoticeBarMenu.Name = "NoticeBarMenu";
            this.NoticeBarMenu.Size = new System.Drawing.Size(221, 120);
            this.NoticeBarMenu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.NoticeBarMenu_MouseDown);
            // 
            // To_Visible_MenuItem
            // 
            this.To_Visible_MenuItem.Name = "To_Visible_MenuItem";
            this.To_Visible_MenuItem.Size = new System.Drawing.Size(220, 22);
            this.To_Visible_MenuItem.Text = "切换至前台";
            this.To_Visible_MenuItem.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.To_Visible_MenuItem.Click += new System.EventHandler(this.To_Visible_MenuItem_Click);
            // 
            // 下载最新版的USBHDDSpyToolStripMenuItem
            // 
            this.下载最新版的USBHDDSpyToolStripMenuItem.Name = "下载最新版的USBHDDSpyToolStripMenuItem";
            this.下载最新版的USBHDDSpyToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.下载最新版的USBHDDSpyToolStripMenuItem.Text = "下载最新版的USBHDDSpy";
            this.下载最新版的USBHDDSpyToolStripMenuItem.Click += new System.EventHandler(this.DownLoad_AllNew_USBHDDSpyToolStripMenuItem_Click);
            // 
            // 访问开源项目ToolStripMenuItem
            // 
            this.访问开源项目ToolStripMenuItem.Name = "访问开源项目ToolStripMenuItem";
            this.访问开源项目ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.访问开源项目ToolStripMenuItem.Text = "访问开源项目";
            this.访问开源项目ToolStripMenuItem.Click += new System.EventHandler(this.Visit_GitHub_ToolStripMenuItem_Click);
            // 
            // 取消ToolStripMenuItem
            // 
            this.取消ToolStripMenuItem.Name = "取消ToolStripMenuItem";
            this.取消ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.取消ToolStripMenuItem.Text = "取消操作";
            this.取消ToolStripMenuItem.Click += new System.EventHandler(this.Cancel_Operation_ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(217, 6);
            // 
            // Exit_Programme_MenuItem
            // 
            this.Exit_Programme_MenuItem.Name = "Exit_Programme_MenuItem";
            this.Exit_Programme_MenuItem.Size = new System.Drawing.Size(220, 22);
            this.Exit_Programme_MenuItem.Text = "退出USBHDDSpy";
            this.Exit_Programme_MenuItem.Click += new System.EventHandler(this.Exit_Programme_MenuItem_Click);
            // 
            // StatusShow
            // 
            this.StatusShow.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StatusShow.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.StatusShow.Location = new System.Drawing.Point(0, 234);
            this.StatusShow.Name = "StatusShow";
            this.StatusShow.Size = new System.Drawing.Size(512, 22);
            this.StatusShow.SizingGrip = false;
            this.StatusShow.TabIndex = 4;
            this.StatusShow.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.BackColor = System.Drawing.SystemColors.Control;
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(32, 17);
            this.StatusLabel.Text = "就绪";
            // 
            // FunctionalRegion
            // 
            this.FunctionalRegion.Controls.Add(this.RecognitionLabel);
            this.FunctionalRegion.Controls.Add(this.AuthorInfoLabel);
            this.FunctionalRegion.Controls.Add(this.CertainFormatsTextBox);
            this.FunctionalRegion.Controls.Add(this.FormatSelectCkB);
            this.FunctionalRegion.Controls.Add(this.WhereToSaveCkB);
            this.FunctionalRegion.Controls.Add(this.PathSelectBtn);
            this.FunctionalRegion.Controls.Add(this.SavePathTextBox);
            this.FunctionalRegion.Controls.Add(this.FinishedMsgCkB);
            this.FunctionalRegion.Controls.Add(this.AutoCopyCkB);
            this.FunctionalRegion.Controls.Add(this.StartCkB);
            this.FunctionalRegion.Controls.Add(this.ConfirmBtn);
            this.FunctionalRegion.Location = new System.Drawing.Point(3, 23);
            this.FunctionalRegion.Name = "FunctionalRegion";
            this.FunctionalRegion.Size = new System.Drawing.Size(506, 208);
            this.FunctionalRegion.TabIndex = 5;
            this.FunctionalRegion.TabStop = false;
            this.FunctionalRegion.Text = "功能与选项";
            // 
            // RecognitionLabel
            // 
            this.RecognitionLabel.AutoSize = true;
            this.RecognitionLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RecognitionLabel.Location = new System.Drawing.Point(247, 105);
            this.RecognitionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RecognitionLabel.Name = "RecognitionLabel";
            this.RecognitionLabel.Size = new System.Drawing.Size(206, 17);
            this.RecognitionLabel.TabIndex = 11;
            this.RecognitionLabel.Text = "注册受信的U盘（仅能插入单个U盘）";
            this.RecognitionLabel.Click += new System.EventHandler(this.RecognitionLabel_Click);
            this.RecognitionLabel.MouseLeave += new System.EventHandler(this.RecognitionLabel_MouseLeave);
            this.RecognitionLabel.MouseHover += new System.EventHandler(this.RecognitionLabel_MouseHover);
            // 
            // AuthorInfoLabel
            // 
            this.AuthorInfoLabel.AutoSize = true;
            this.AuthorInfoLabel.BackColor = System.Drawing.Color.White;
            this.AuthorInfoLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AuthorInfoLabel.Location = new System.Drawing.Point(247, 132);
            this.AuthorInfoLabel.Name = "AuthorInfoLabel";
            this.AuthorInfoLabel.Size = new System.Drawing.Size(104, 17);
            this.AuthorInfoLabel.TabIndex = 10;
            this.AuthorInfoLabel.Text = "关于USBHDDSpy";
            this.AuthorInfoLabel.Click += new System.EventHandler(this.AuthorInfo_Click);
            this.AuthorInfoLabel.MouseLeave += new System.EventHandler(this.AuthorInfoLabel_MouseLeave);
            this.AuthorInfoLabel.MouseHover += new System.EventHandler(this.AuthorInfoLabel_MouseHover);
            // 
            // CertainFormatsTextBox
            // 
            this.CertainFormatsTextBox.Location = new System.Drawing.Point(247, 75);
            this.CertainFormatsTextBox.Name = "CertainFormatsTextBox";
            this.CertainFormatsTextBox.Size = new System.Drawing.Size(232, 23);
            this.CertainFormatsTextBox.TabIndex = 9;
            // 
            // FormatSelectCkB
            // 
            this.FormatSelectCkB.AutoSize = true;
            this.FormatSelectCkB.Location = new System.Drawing.Point(247, 50);
            this.FormatSelectCkB.Name = "FormatSelectCkB";
            this.FormatSelectCkB.Size = new System.Drawing.Size(243, 21);
            this.FormatSelectCkB.TabIndex = 8;
            this.FormatSelectCkB.Text = "仅复制以下扩展名的文件（以逗号隔开）";
            this.FormatSelectCkB.UseVisualStyleBackColor = true;
            // 
            // WhereToSaveCkB
            // 
            this.WhereToSaveCkB.AutoSize = true;
            this.WhereToSaveCkB.Location = new System.Drawing.Point(50, 131);
            this.WhereToSaveCkB.Name = "WhereToSaveCkB";
            this.WhereToSaveCkB.Size = new System.Drawing.Size(195, 38);
            this.WhereToSaveCkB.TabIndex = 6;
            this.WhereToSaveCkB.Text = "使用以下路径以保存拷贝\r\n默认为当前用户的文档文件夹下";
            this.WhereToSaveCkB.UseVisualStyleBackColor = true;
            // 
            // PathSelectBtn
            // 
            this.PathSelectBtn.Location = new System.Drawing.Point(357, 179);
            this.PathSelectBtn.Name = "PathSelectBtn";
            this.PathSelectBtn.Size = new System.Drawing.Size(30, 23);
            this.PathSelectBtn.TabIndex = 5;
            this.PathSelectBtn.Text = "...";
            this.PathSelectBtn.UseVisualStyleBackColor = true;
            this.PathSelectBtn.Click += new System.EventHandler(this.PathSelectBtn_Click);
            // 
            // SavePathTextBox
            // 
            this.SavePathTextBox.BackColor = System.Drawing.Color.White;
            this.SavePathTextBox.Location = new System.Drawing.Point(6, 179);
            this.SavePathTextBox.Name = "SavePathTextBox";
            this.SavePathTextBox.ReadOnly = true;
            this.SavePathTextBox.Size = new System.Drawing.Size(345, 23);
            this.SavePathTextBox.TabIndex = 4;
            // 
            // FinishedMsgCkB
            // 
            this.FinishedMsgCkB.AutoSize = true;
            this.FinishedMsgCkB.Location = new System.Drawing.Point(50, 104);
            this.FinishedMsgCkB.Name = "FinishedMsgCkB";
            this.FinishedMsgCkB.Size = new System.Drawing.Size(111, 21);
            this.FinishedMsgCkB.TabIndex = 3;
            this.FinishedMsgCkB.Text = "完成后消息提示";
            this.FinishedMsgCkB.UseVisualStyleBackColor = true;
            // 
            // AutoCopyCkB
            // 
            this.AutoCopyCkB.AutoSize = true;
            this.AutoCopyCkB.Checked = true;
            this.AutoCopyCkB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoCopyCkB.Location = new System.Drawing.Point(50, 77);
            this.AutoCopyCkB.Name = "AutoCopyCkB";
            this.AutoCopyCkB.Size = new System.Drawing.Size(99, 21);
            this.AutoCopyCkB.TabIndex = 2;
            this.AutoCopyCkB.Text = "自动复制文件";
            this.AutoCopyCkB.UseVisualStyleBackColor = true;
            // 
            // StartCkB
            // 
            this.StartCkB.AutoSize = true;
            this.StartCkB.Checked = true;
            this.StartCkB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.StartCkB.Location = new System.Drawing.Point(50, 50);
            this.StartCkB.Name = "StartCkB";
            this.StartCkB.Size = new System.Drawing.Size(87, 21);
            this.StartCkB.TabIndex = 1;
            this.StartCkB.Text = "开机自启动";
            this.StartCkB.UseVisualStyleBackColor = true;
            // 
            // ConfirmBtn
            // 
            this.ConfirmBtn.Location = new System.Drawing.Point(425, 179);
            this.ConfirmBtn.Name = "ConfirmBtn";
            this.ConfirmBtn.Size = new System.Drawing.Size(75, 23);
            this.ConfirmBtn.TabIndex = 0;
            this.ConfirmBtn.Text = "应用";
            this.ConfirmBtn.UseVisualStyleBackColor = true;
            this.ConfirmBtn.Click += new System.EventHandler(this.ConfirmBtn_Click);
            // 
            // CopyTimer
            // 
            this.CopyTimer.Interval = 60000;
            this.CopyTimer.Tick += new System.EventHandler(this.ActionTimer_Tick);
            // 
            // MainConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(512, 256);
            this.Controls.Add(this.FunctionalRegion);
            this.Controls.Add(this.StatusShow);
            this.Controls.Add(this.ExitLabel);
            this.Controls.Add(this.MinimumLabel);
            this.Controls.Add(this.TitleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainConsole";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainConsole_FormClosing);
            this.Load += new System.EventHandler(this.MainConsole_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainConsole_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainConsole_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainConsole_MouseUp);
            this.NoticeBarMenu.ResumeLayout(false);
            this.StatusShow.ResumeLayout(false);
            this.StatusShow.PerformLayout();
            this.FunctionalRegion.ResumeLayout(false);
            this.FunctionalRegion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label TitleLabel;
        private Label MinimumLabel;
        private Label ExitLabel;
        private StatusStrip StatusShow;
        private GroupBox FunctionalRegion;
        private Label AuthorInfoLabel;
        private TextBox CertainFormatsTextBox;
        private CheckBox FormatSelectCkB;
        private CheckBox WhereToSaveCkB;
        private Button PathSelectBtn;
        private TextBox SavePathTextBox;
        private CheckBox FinishedMsgCkB;
        private CheckBox AutoCopyCkB;
        private CheckBox StartCkB;
        private Button ConfirmBtn;
        private Label RecognitionLabel;
        private System.Windows.Forms.Timer CopyTimer;
        internal ToolStripStatusLabel StatusLabel;
        private ContextMenuStrip NoticeBarMenu;
        private ToolStripMenuItem To_Visible_MenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem Exit_Programme_MenuItem;
        internal NotifyIcon NoticeBar;
        private ToolStripMenuItem 取消ToolStripMenuItem;
        private ToolStripMenuItem 下载最新版的USBHDDSpyToolStripMenuItem;
        private ToolStripMenuItem 访问开源项目ToolStripMenuItem;
    }
}