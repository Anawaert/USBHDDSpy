using System.Text.RegularExpressions;  // 正则
using System.Diagnostics;  // 为了Process类
using USBHDDSpy;  // 主窗口静态变量

namespace additionalAttributes
{

    /// <summary>
    /// 用于检测程序更新信息的类
    /// </summary>
    internal class UpdateCheck
    {
        internal UpdateCheck()  // 本类的构造函数，本来完全不需要手写的，写着玩的
        {
            relatedURL = "https://github.com/Anawaert/USBHDDSpy";  // 指定URL
        }

        #region 检测更新的函数主体

        /// <summary>
        /// 这是一个用于检测应用程序是否需要更新的函数。依托GitHub的网页页面及可更改读取信息，使用读取页面HTML字节码的方式
        /// 并与程序内硬编码版本对比来判断是否有版本更新
        /// </summary>
        internal async void CheckGitHub()  // 异步防卡UI线程
        {
            try
            {
                HttpClient NewClient = new HttpClient();  
                HttpResponseMessage httpResponse = await NewClient.GetAsync(relatedURL);  // 连接至GitHub上USBHDDSpy的主页
                httpResponse.EnsureSuccessStatusCode();  // 确保Http正确相响应
                string ResponseBody = await httpResponse.Content.ReadAsStringAsync();  // 将相应返回的主页内容从字节码转为字符串

                Regex GetTitleRegex = new Regex(GetTitle);  // 匹配<title>与</title>标签之间的全部内容
                Regex GetDateFromTitleRegex = new Regex(GetDate);  // 匹配<title>与</title>标签之间以xxxx-xx为格式的日期字符串。此处为什么要使用两次正则呢，因为经实测如果仅使用本行代码的正则规则匹配，可能导致匹配到非希望的结果。
                if (GetDateFromTitleRegex.Match(GetTitleRegex.Match(ResponseBody).Value).Value != "2023-05")
                {
                    DialogResult result = MessageBox.Show("当前更新可用，是否现在进行更新并导航至下载界面？", "USBHDDSpy Update", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);  // 弹出对话框
                    if (result == DialogResult.Yes)  // 单击“是”
                    {
                        try
                        {
                            Process ShellCmd = new Process();
                            ShellCmd.StartInfo.FileName = "cmd.exe";
                            ShellCmd.StartInfo.RedirectStandardInput = true; ShellCmd.StartInfo.UseShellExecute = false; ShellCmd.StartInfo.CreateNoWindow = true;  // 隐式调用cmd.exe
                            ShellCmd.Start();
                            ShellCmd.StandardInput.WriteLine("EXPLORER \"https://github.com/Anawaert-Download/USBHDDSpy_Download/archive/refs/heads/main.zip\" & EXIT");  // 使用cmd语句访问下载链接。由于Anawaert无条件建立一个24小时开放的直链下载服务，突发奇想把Github作为下载源，经实践暂且认为可行。
                            ShellCmd.Dispose();  // 释放内存
                        }
                        catch { MessageBox.Show("连接至GitHub时发生错误，更新已取消", "USBHDDSpy 消息", MessageBoxButtons.OK); }  // 当网络连接不通畅或者无网络连接时
                    }
                    else if (result == DialogResult.Cancel)  // 单击“取消”时
                    {
                        if (MessageBox.Show("是否取消更新？\n\n若单击“是”，则将屏蔽自动更新，您需要手动访问GitHub以获取更新；若单击“否”，则将取消本次更新，但不会屏蔽", "USBHHDDSpy 消息", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            MainConsole.UpdateOrNot = "0";  // 将MainConsole中UpdateOrNot静态变量改为"0"，这样以后就不检查更新
                            
                        }
                    }
                }
            }
            catch { }  // 若Http未响应，则暂时搁置更新，保留至下一次开启时进行检查
        }
        #endregion

        #region 属性、静态变量区域
        internal string relatedURL { get; }
        public const string GetTitle = @"<title>.+?</title>";
        public const string GetDate = @"\b\d{4}-\d{2}\b";
        #endregion
    }
}
