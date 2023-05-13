using System.Diagnostics;

namespace USBHDDSpy
{
    public partial class ProgramInfo : Form
    {
        public ProgramInfo()
        {
            InitializeComponent();
        }

        private void ProgramInfo_Load(object sender, EventArgs e)
        {
            ShowContextTimer.Enabled = ContextLabel.Text != ContextString || LinkToGithubLabel.Text != LinkString;
        }

        private void ShowContextTimer_Tick(object sender, EventArgs e)
        {
            if (ContextNumber < ContextString.Length)
            {
                ContextLabel.Text += ContextString[ContextNumber];
                ContextNumber++;
            }

            if (LinkNumber < LinkString.Length)  // 模拟内容逐字出现效果
            {
                LinkToGithubLabel.Text += LinkString[LinkNumber];
                LinkNumber++;
            }
        }

        #region （静态）变量声明区域
        internal static string ContextString = "Anawaert USBHDDSpy - 版本号 1.0.5.1\r\n发布于2023-05\r\n\r\n版权所有：\r\nCopyright (C) 2017-2023 Anawaert Studio\r\n\r\n\r\nAnawaert USBHDDSpy 开源项目详见：\r\n";
        internal static string LinkString = "https://github.com/Anawaert/USBHDDSpy\r\n";
        internal int ContextNumber = 0;
        internal int LinkNumber = 0;
        #endregion

        private void LinkToGithubLabel_Click(object sender, EventArgs e)
        {
            using (Process ShellCmd = new Process())
            {
                ShellCmd.StartInfo.FileName = "cmd.exe";
                ShellCmd.StartInfo.RedirectStandardInput = true; ShellCmd.StartInfo.UseShellExecute = false; ShellCmd.StartInfo.CreateNoWindow = true;  // 隐式调用cmd.exe
                ShellCmd.Start();
                ShellCmd.StandardInput.WriteLine("EXPLORER \"https://github.com/Anawaert/USBHDDSpy\" & EXIT");  // 导航到开源项目网址
            }
        }
    }
}