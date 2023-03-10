using System.IO;  // 用于处理本地文件读写的操作
using System.Text.RegularExpressions;  // 用于正则匹配
using static USBHDDSpy.LogOperations;

namespace USBHDDSpy
{
    public partial class MainConsole : Form
    {
        public static Point MouseOff; public static bool LeftFlag;  // 前者用于程序拖动时记录鼠标指针初始坐标，后者用于指示是否鼠标左键按下
        public string DocPath = @"C:\Users\" + Environment.UserName + @"\Documents\";  // LogPath为用户的文档文件夹目录

        public MainConsole()
        {
            InitializeComponent();
        }

        private void MainConsole_Load(object sender, EventArgs e)
        {
            if (File.Exists(DocPath + "UHSLog.info") == false)
            {
                CreateLogFile(DocPath);
            }
            else
            {
                using (StreamReader sr = new StreamReader(DocPath + "UHSLog.info"))
                {
                    string wholeContent = sr.ReadToEnd();
                 
                }
            }
        }

        #region 处理程序的拖动
        private void MainConsole_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseOff = new Point(-e.X, -e.Y);  // 配合MainConsole_MouseMove方法中Point.Offset方法使用，为负号
                LeftFlag = true;
            }
        }

        private void MainConsole_MouseMove(object sender, MouseEventArgs e)
        {
            if (LeftFlag)
            {
                Point MouseSet = Control.MousePosition;
                MouseSet.Offset(MouseOff.X, MouseOff.Y);
                Location = MouseSet;
            }
        }
        private void MainConsole_MouseUp(object sender, MouseEventArgs e)
        {
            if (LeftFlag)
            {
                LeftFlag = false; 
            }
        }
        #endregion

        #region 处理读取配置文件和应用

        public static void ApplyLog()
        {

        }
        #endregion
    }
}