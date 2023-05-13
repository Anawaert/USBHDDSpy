using static System.Convert;  // 数据类型转换
using System.Text.RegularExpressions;  // 正则表达式
using Microsoft.Win32;  // 界面相关WinAPI
using additionalAttributes;  // 更新检查
using System.Diagnostics;  // 引用Process类

namespace USBHDDSpy
{
    public partial class MainConsole : Form
    {
        public MainConsole()
        {
            InitializeComponent();
        }

        #region 处理程序加载 (The region of functions that deal with the configuration of initialising the program)
        private void MainConsole_Load(object sender, EventArgs e)  // 主窗口加载
        {
            #region 加载时处理读取配置文件和修改UI的部分
                Directory.CreateDirectory(SavePathVal);  // 创建预留的存储目录
            if (File.Exists(DocPath + "UHSLog.info"))
            {
                ReadLFAndApply(DocPath);  // 配置文件存在则读取并应用在UI上
            }
            else
            {
                CreateLogFile(DocPath);
                ReadLFAndApply(DocPath);  // 配置文件不存在就先创建，然后应用在UI上
            }
            #endregion

            #region 加载时处理功能实现的部分
            if (StartCkB.Checked) { StartWithStartup(); } else { NotStartWithStartUp(); }

            if (AutoCopyCkB.Checked) { CopyTimer.Enabled = true; } else { CopyTimer.Enabled = false; }

            StatusLabel.Text = "就绪";
            #endregion

            #region 检查更新
            UpdateCheck updateCheck = new UpdateCheck();
            updateCheck.CheckGitHub(); SetToTheLF(DocPath);  // 检查完更新后再写入下配置文件
            #endregion
        }
        #endregion

        #region 处理“应用”按钮按下时的操作 (The region of dealing with the operations when ""Apply" button was clicked)
        private void ConfirmBtn_Click(object sender, EventArgs e)  // 当按下“应用”按钮后
        {
            SetToTheLF(DocPath);  // 先将UI的更改应用到配置文件中
            ReadLFAndApply(DocPath);  // 再将配置文件读出来应用到UI，其目的是为了确认更改或者前面的读写成功了、

            if (StartCkB.Checked)  // 如果选中开机自启
            {
                StartWithStartup();  // 自启
            }
            else
            {
                NotStartWithStartUp();  // 不自启
            }

            if (!WhereToSaveCkB.Checked)
            {
                SavePathVal = DocPath + @"\USBHDDSpy_Reserve";  // 不选指定文件存储位置则默认在C:\Users\%username%\Documents\USBHDDSpy_Reserve下
            }

            if (!FormatSelectCkB.Checked)
            {
                FormatsVal = "";  // 不选指定扩展名格式则默认为""（空）
            }

            if (AutoCopyCkB.Checked)
            {
                CopyTimer.Start();  // 选中则把CopyTimer使能
            }
            else
            {
                CopyTimer.Stop();  // 不选中则失能
            }

            StatusLabel.Text = "已应用设置";
        }
        #endregion

        #region 处理程序的拖动 (The region of dealing with the effects when moving the application with pointer)
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

        #region 处理配置文件相关的行为 (The region of dealing with the configuration file)

        /// <summary>
        /// 用以创建新的符合初始状态的配置文件
        /// (The function that used for creating a brand new cofiguration file that fits the original condition of the program)
        /// </summary>
        /// <param name="FolderPath">存放配置文件的文件夹的绝对路径 (The absolute path of the configuration file)</param>
        public void CreateLogFile(string FolderPath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(FolderPath + "UHSLog.info", true))  // 在文件已经不存在的前提下创建写入流并创建文件
                {
                    sw.Write("{checkboxinfo}");
                    for (int i = 0; i < 4; i++)
                    {
                        sw.Write(checkboxval[i] + ",");
                    }
                    sw.Write(checkboxval[4] + "{endcheckboxinfo}\n");  // 写入{checkboxinfo}1,1,0,0,0{endcheckboxinfo}

                    sw.Write("{savepathinfo}" + FolderPath + "USBHDDSpy_Reserve" + "{endsavepathinfo}\n" +
                        "{certainformatinfo}{endcertainformatinfo}\n");

                    sw.Write("{updateinfo}1{endupdateinfo}\n");
                    sw.Write("{reliablediskid}{endreliablediskid}");  // 同理
                }
            }
            catch
            {
                MessageBox.Show("在" + DocPath + "下创建USBHDDSpy配置文件失败\n请确保您对" + DocPath + "有足够的读写权限", "USBHDDSpy 消息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        /// <summary>
        /// 用以读取已经存在的配置文件并且应用于UI界面
        /// (The function that used for reading the existed configuration file and applying to UI interface according to the configuration file)
        /// </summary>
        /// <param name="FolderPath">>存放配置文件的文件夹的绝对路径 (The absolute path of the configuration file)</param>
        public void ReadLFAndApply(string FolderPath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(FolderPath + "UHSLog.info"))  // 创建读取流
                {
                    string WholeContent = sr.ReadToEnd();  // 读取配置文件全文，由于整个配置文件的大小非常小，因此使用ReadToEnd()的非异步方法是几乎不影响性能的

                    Regex regexCkB = new Regex(GetCkBvalRegex);  // 声明一个规则为匹配{checkboxinfo}到{endcheckboxinfo}之间全部内容的Regex类对象
                    string CkBvalString = regexCkB.Match(WholeContent).Value;  // 获取匹配值，应该为_,_,_,_,_的形式，其中_为字符串的0或1
                    string[] checkboxvalTemp = CkBvalString.Split(',');  // 以","字符切割，并传入一个字符串数组
                    for (int i = 0; i < 5; i++)  // 实际上这里应该使用foreach，但是既然“我们都知道”分割完后数组长度应该为5，因此就直接拿5来使用了
                    {
                        checkboxval[i] = checkboxvalTemp[i];  // 把全局的静态变量的一并修改
                        switch (i)
                        {
                            case 0:
                                if (checkboxval[i] == "0")
                                {
                                    StartCkB.Checked = false;  // 当checkboxval数组中第一项为"0"时，即代表它应该未被选中，则取消打钩
                                }
                                else
                                {
                                    StartCkB.Checked = true;  // 反之置为选中
                                }
                                break;
                            case 1:
                                if (checkboxval[i] == "0")
                                {
                                    AutoCopyCkB.Checked = false;
                                }
                                else
                                {
                                    AutoCopyCkB.Checked = true;
                                }
                                break;
                            case 2:
                                if (checkboxval[i] == "0")
                                {
                                    FinishedMsgCkB.Checked = false;
                                }
                                else
                                {
                                    FinishedMsgCkB.Checked = true;
                                }
                                break;
                            case 3:
                                if (checkboxval[i] == "0")
                                {
                                    WhereToSaveCkB.Checked = false;
                                }
                                else
                                {
                                    WhereToSaveCkB.Checked = true;
                                }
                                break;
                            case 4:
                                if (checkboxval[i] == "0")
                                {
                                    FormatSelectCkB.Checked = false;
                                }
                                else
                                {
                                    FormatSelectCkB.Checked = true;
                                }
                                break;
                                // 《面向结果编程》
                        }
                    }

                    Regex regexTb = new Regex(GetSavePathRegex);
                    string SavePathString = regexTb.Match(WholeContent).Value;  // 同理从配置文件中扒拉出文件存放路径
                    SavePathTextBox.Text = SavePathString;  // 应用于UI
                    SavePathVal = SavePathString == "" ? SavePathVal : SavePathString;  // 更新静态变量

                    Regex regexCf = new Regex(GetCertainFormatsRegex);
                    string CertainFormatsString = regexCf.Match(WholeContent).Value;
                    CertainFormatsTextBox.Text = CertainFormatsString;
                    FormatsVal = CertainFormatsString;  // 同理于指定格式

                    Regex regexUd = new Regex(GetUpdateOrNotRegex);
                    UpdateOrNot = regexUd.Match(WholeContent).Value;  // 更新选择

                    Regex regexDkId = new Regex(GetDiskIdFromLog);
                    DiskIdVal = regexDkId.Match(WholeContent).Value;  // 读取已注册的硬盘序列号
                }           
            }
            catch
            {
                MessageBox.Show("读取配置文件失败\n请确保您对" + DocPath + "有足够的读写权限", "USBHDDSpy 消息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();

            }
        }

        /// <summary>
        /// 用以将用户界面的修改应用于配置文件当中
        /// (The function that used for saving users' changes and converting the content of configuration file at the same time)
        /// </summary>
        /// <param name="FloderPath">存放配置文件的文件夹的绝对路径 (The absolute path of the configuration file)</param>
        public void SetToTheLF(string FloderPath)
        {
            try
            {
                checkboxval[0] = ToInt32(StartCkB.Checked).ToString(); checkboxval[1] = ToInt32(AutoCopyCkB.Checked).ToString();
                checkboxval[2] = ToInt32(FinishedMsgCkB.Checked).ToString(); checkboxval[3] = ToInt32(WhereToSaveCkB.Checked).ToString(); checkboxval[4] = ToInt32(FormatSelectCkB.Checked).ToString();  // 更新静态变量

                StreamReader sr = new StreamReader(FloderPath + "UHSLog.info");
                string WholeContent = sr.ReadToEnd();

                string laterValTemp = "";
                for (int i = 0; i < 5; i++)
                {
                    laterValTemp += i != 4 ? checkboxval[i] + "," : checkboxval[i];
                }  // 把checkboxval这个静态变量数组手动换为一个字符串存储在laterValTemp中

                WholeContent = Regex.Replace(WholeContent, GetCkBvalRegex, laterValTemp);
                WholeContent = Regex.Replace(WholeContent, GetSavePathRegex, SavePathTextBox.Text);
                WholeContent = Regex.Replace(WholeContent, GetCertainFormatsRegex, CertainFormatsTextBox.Text);
                WholeContent = Regex.Replace(WholeContent, GetUpdateOrNotRegex, UpdateOrNot);
                WholeContent = Regex.Replace(WholeContent, GetDiskIdFromLog, DiskIdVal);
                sr.Close();  // 替换修改原来的配置文件内容

                FileStream fs = new FileStream(FloderPath + "UHSLog.info", FileMode.Create, FileAccess.ReadWrite); fs.Dispose();  // 用最弱智的方法把配置文件清空，不考虑性能了
                StreamWriter sw = new StreamWriter(FloderPath + "UHSLog.info");
                sw.Write(WholeContent);
                sw.Close();  // 把修改后的“全文”写进去
            }
            catch
            {
                MessageBox.Show("写入配置文件失败\n请确保您对" + DocPath + "有足够的读写权限", "USBHDDSpy 消息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();

            }
        }
        #endregion

        #region 处理开机自启 (The region of dealing with starting the program when system is starting up)
        internal const string SoftwareName = "USBHDDSpy";  // 声明常量

        /// <summary>
        /// 用于写入注册表以实现开机自启的函数
        /// The function that used for reading/writing Registry to start program while system's starting up
        /// </summary>
        internal void StartWithStartup()  // 写入注册表以自启
        {
            string? PathTemp = Application.ExecutablePath;  // 可执行文件当前绝对路径
            RegistryKey rk = Registry.CurrentUser;  // 获取当前用户的注册表Key
            try
            {
                RegistryKey rk_2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");  // 添加子Key
                string? old_path = (string?)rk_2.GetValue(SoftwareName);
                if (!PathTemp.Equals(old_path))  // 检测重复
                {
                    rk_2.SetValue(SoftwareName, PathTemp);
                }
                rk.Close(); rk_2.Close();  // 释放引用
            }
            catch { }  // 假装有错误处理
        }

        /// <summary>
        /// 用于写入注册表以取消开机自启的函数
        /// The function that used for reading/writing Registry to cancle to start the program while system's starting up
        /// </summary>
        internal void NotStartWithStartUp()  // 取消自启
        {
            RegistryKey rk = Registry.CurrentUser;
            try
            {
                RegistryKey rk_2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                string? old_path = (string?)rk_2.GetValue(SoftwareName);
                rk_2.DeleteValue(SoftwareName, false);
                rk.Close(); rk_2.Close();  // 同上，逆过程
            }
            catch { }
        }  // 代码参考：https://blog.csdn.net/Raink_LH/article/details/128671015
        #endregion

        #region 处理文件自动复制 (The region of dealing with coping files automatically)

        /// <summary>
        /// 用于处理文件自动复制到指定文件夹的函数，集成于ActionTimer的Tick事件引发函数中
        /// (The function that used for copying files form USB devices towards the specific folder automatically.
        /// It's also the function of ActionTimer triggered by "Tick" event)
        /// </summary>
        /// <param name="sender">ActionTimer对象</param>
        /// <param name="e">事件数据 (Event data)</param>
        public void ActionTimer_Tick(object sender, EventArgs e)  // 当这个函数执行功能时，即将文件复制到目标目录里
        {
            CopyTimer.Enabled = false;  // 先暂停掉计时器避免可能的冲突

            try
            {
                if (IsOneUSBHDD())  // 如果仅有单个U盘插入
                {
                    DriveInfo[] driveInfo = DriveInfo.GetDrives();  // 获取驱动器集合对象
                    foreach (DriveInfo dI in driveInfo)  // 遍历集合查
                    {
                        if (dI.DriveType == DriveType.Removable)  // 当驱动器类型为可移动式驱动器时
                        {
                            if (!DiskRecognitionRelated.IsDiskRegistered(Regex.Match(dI.Name, @".+?(?=:\\)").Value, DiskIdVal))  // 当满足这个驱动器为可移动磁盘,但是它并未进行注册过时
                            {
                                if (dI.IsReady && (dI.TotalSize <= (long)64 * 1024 * 1024 * 1024) &&
                                   (new DriveInfo(Regex.Match(SavePathVal, @".+?(?=:\\)").Value).TotalFreeSpace > dI.TotalSize - dI.TotalFreeSpace))  // 同时这个驱动器满足全盘大小小于16GB，并且满足拷入目录所在驱动器空闲空间多于USB驱动器中所有文件大小总和
                                {
                                    if (FormatsVal != "")  // 如果FormatsVal不是""的话，也就是用户指定了格式
                                    {
                                        foreach (string cF in CertainFormatsToArray(FormatsVal))  // 遍历获取所有指定格式，CertainFormatsToArray(string)方法用于去除空格避免歧义或者错误
                                        {
                                            IEnumerable<string> aimFilesCollection = Directory.EnumerateFiles(dI.Name, @"*." + cF, SearchOption.AllDirectories);  // 获取所有目标文件绝对路径字符串的集合对象，*表示一次或多次匹配，cF为指定格式
                                            foreach (string originPosition in aimFilesCollection)  // 遍历该对象获取单个文件路径
                                            {
                                                StatusLabel.Text = "正在复制" + originPosition + "到" + SavePathVal;
                                                File.Copy(originPosition, SavePathVal + @"\" + GenerateTargetName(originPosition), true);  // 将文件拷贝，若有重名文件则进行覆盖重写
                                            }
                                        }
                                    }
                                    else  // 如果为""，就说明第5个Checkbox的钩没打，即默认全体文件
                                    {
                                        IEnumerable<string> aimFilesCollection_2 = Directory.EnumerateFiles(dI.Name, @"*.*", SearchOption.AllDirectories);  // "*.*"匹配全体文件
                                        foreach (string originPosition_2 in aimFilesCollection_2)
                                        {
                                            StatusLabel.Text = "正在复制" + originPosition_2 + "到" + SavePathVal;
                                            File.Copy(originPosition_2, SavePathVal + @"\" + GenerateTargetName(originPosition_2), true);  // 拷贝文件，如有重复则进行重写
                                        }
                                    }
                                    if (checkboxval[2] == "1")
                                        MessageBox.Show("作业已完成", "USBHDDSpy 消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                            }
                        }
                    }
                }
            }
            catch { }  // 空catch块，暂无发生赋值错误时所需要的操作

            StatusLabel.Text = "就绪";
            CopyTimer.Enabled = true;  // 恢复检测计时
        }  // 略微复杂
        #endregion

        #region 判断插入的USB存储设备是否为单个 (The region of dealing with detecting how many USB devices were pluged in the computer)

        /// <summary>
        /// 用于判断是否插入的U盘数量为1个
        /// (The function that used for detecting that only one USB device was pluged in the computer)
        /// </summary>
        /// <returns>布尔值，真即为1个，假即为0或多个 (The boolean value, true means the single one and false means zero or more than one devices)</returns>
        internal bool IsOneUSBHDD()
        {
            int USBHDD_Amount = 0;  // U盘数量
            foreach (DriveInfo info in DriveInfo.GetDrives())  // 遍历驱动器
            {
                USBHDD_Amount += ToInt32(info.DriveType == DriveType.Removable);  // 有USB驱动器就+1，没有就不加
            }
            return USBHDD_Amount == 1 ? true : false;  // 单个就返回true，0或多个就false
        }  /* 此处将留作未来更新编写多个USB驱动器的识别的功能所用 */
        #endregion

        #region 处理指定扩展名 (The region of dealing with the specific formats inputed by users) 
        /// <summary>
        /// 用以将保存在配置文件中的以逗号分隔的“特定格式”转化为后缀名的正确格式
        /// (The function that can make the specific file formats saved in local configuration file convert to
        /// right string format that program can deal with)
        /// </summary>
        /// <param name="certainFmtString">以逗号(',')分隔的“特定格式”字符串 (The string variable splited by character ( ',') stored in local configuration file)</param>
        /// <returns>string[]，里面包含着所有文件格式名 (string[], including all the file formats that users inputed)</returns>
        private string[] CertainFormatsToArray(string certainFmtString)
        {
            string[] cFS = certainFmtString.Trim().Split(',');  // 先通过Split方法把被逗号分隔的字符串提取到一个数组中
            for (int i = 0; i < cFS.Length; i++)
            {
                cFS[i] = Regex.Match(cFS[i], @"\S+").Value;  // 由于foreach只读，此处使用for循环，通过正则匹配所有非空字符重写每个数组元素
            }
            return cFS;  // 返回这个string类型的数组
        }
        #endregion

        #region 注册为可信任的USB存储设备 (The region of dealing with registering USB storage devices)

        /// <summary>
        /// 用于处理将USB设备注册为受信任的U盘的函数，与该Label的点击事件集成在一起
        /// (The function used for dealing with registering a USB device in order to make it reliable and
        /// the program will not copy anything from it.
        /// This function is also the function triggered by the "Click" event of RecognitionLabel)
        /// </summary>
        /// <param name="sender">发送者 (sender)</param>
        /// <param name="e">事件数据 (EventArgs)</param>
        private void RecognitionLabel_Click(object sender, EventArgs e)  // 当按下注册受信U盘时          /* 注：该方法除了要判断U盘是否注册过，还要把读取到的硬盘标识符写入配置文件中 */
        {
            try
            {
                if (IsOneUSBHDD())  // 当检测到仅有一个U盘时
                {
                    var allDriveInfo = DriveInfo.GetDrives();
                    foreach (DriveInfo disk in allDriveInfo)  // 遍历全体驱动器
                    {
                        if (disk.DriveType == DriveType.Removable)
                        {
                            if (!DiskRecognitionRelated.IsDiskRegistered(Regex.Match(disk.Name, @".+?(?=:\\)").Value, DiskIdVal))  // 当满足这个驱动器为可移动磁盘,但是它并未进行注册过时
                            {
                                string[] DkValArrTemp = DiskIdVal.Split(',');  // 将DiskIdVal以英语字符","分割
                                int NEW_ARR_LENTH = DkValArrTemp[0] == "" ? 1 : DkValArrTemp.Length + 1;  // NEW_ARR_IENTH为DkValArr[]的长度。如果DkValArrTemp[0]为空，即任何U盘都未注册，则令NEW_ARR_LENTH为1，如果不是，即说明至少有一个U盘已经注册，则令其比DkValArrTemp长度多1
                                string[] DkValArr = new string[NEW_ARR_LENTH];  

                                if (DkValArr.Length != DkValArrTemp.Length)  // 如果它俩长度不一,也就是“已注册过”了U盘
                                {
                                    for (int i = 0; i < DkValArrTemp.Length; i++)
                                    {
                                        DkValArr[i] = DkValArrTemp[i];  // 把DkValArrTemp中的各项复制到DkValArr
                                    }
                                    uint ValTemp;
                                    DiskRecognitionRelated.GET_USBHDD_ID(Regex.Match(disk.Name, @".+?(?=:\\)").Value, out ValTemp);
                                    DkValArr[DkValArr.Length - 1] = ValTemp.ToString();  // 最后一项留给新的U盘硬件标识符
                                    DiskIdVal = String.Empty;  // 将DiskIdVal先置空

                                    for (int i = 0; i < DkValArr.Length; i++)
                                    {
                                        DiskIdVal += i <= DkValArr.Length - 2 ? DkValArr[i] + @"," : DkValArr[i];  // “全新”赋值到DiskIdVal中
                                    }

                                    SetToTheLF(DocPath);  // 写入配置文件
                                }
                                else  // 如果一开始即为空的,也就是完全没有U盘注册过
                                {
                                    uint ValTemp;
                                    DiskRecognitionRelated.GET_USBHDD_ID(Regex.Match(disk.Name, @".+?(?=:\\)").Value, out ValTemp);  // ValTemp为U盘的硬盘标识符
                                    DkValArr[0] = ValTemp.ToString();  // 写入这个数组,实际上并无意义
                                    DiskIdVal = ValTemp.ToString();  // 写入静态变量
                                    SetToTheLF(DocPath);  // 修改至配置文件中
                                }
                            }
                            else
                            {
                                MessageBox.Show("\"" + new DriveInfo(disk.Name).VolumeLabel + "\" 已经被注册为受信任的U盘啦", "USBHDDSpy 消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            StatusLabel.Text = "已成功注册 \"" + disk.VolumeLabel + "\" 为受信任的USB存储设备";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("不存在或存在多个U盘，请仅保留单个U盘后再进行注册捏", "USBHDDSpy 消息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("调用Windows API或应用到配置文件出现错误，请检查Windows系统文件的完整性或您对“文档”文件夹的读写权限", "USBHDDSpy 消息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 处理文件保存路径的选择和填充 (The region of dealing with where to save the files when users click the "..." button)
        private void PathSelectBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();  // 实例化一个文件选择查看器
            folderBrowser.Description = "请选择文件拷贝的存储路径：";

            if (folderBrowser.ShowDialog() == DialogResult.OK)  // 当Dialog的路径选择被确定时
            {
                SavePathTextBox.Text = folderBrowser.SelectedPath;  // 填充
            }
        }
        #endregion

        #region 点击查看“关于USBHDDSpy”时 (When users click the "About USBHDDSpy" label...)
        private void AuthorInfo_Click(object sender, EventArgs e)
        {
            var AboutUSBHDDSpy = new ProgramInfo();
            AboutUSBHDDSpy.Show();
        }
        #endregion

        #region 处理托盘小图标的操作 (The region for dealing with some functions within the notice bar)
        private void NoticeBar_DoubleClick(object sender, EventArgs e)  // 双击托盘小图标切换到前台
        {
             this.WindowState = FormWindowState.Normal;           
            this.Visible = true;
            this.Opacity = 1.00F;

            this.ShowInTaskbar = true;
            NoticeBar.Visible = false;
        }

        private void To_Visible_MenuItem_Click(object sender, EventArgs e)  // 切换到前台（ContextMenuStrip已绑定该NotifyInco）
        {
            this.WindowState = FormWindowState.Normal;
            this.Visible = true;
            this.Opacity = 1.00F;
            
            this.ShowInTaskbar = true;
            NoticeBar.Visible = false;
        }

        private void Exit_Programme_MenuItem_Click(object sender, EventArgs e)  // 退出整个应用程序
        {
            NoticeBar.Dispose();
            Application.Exit();
        }

        private void MainConsole_FormClosing(object sender, FormClosingEventArgs e)  // 重写关闭程序时候放置于托盘的处理
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall && e.CloseReason != CloseReason.WindowsShutDown)  // 若退出由非Application.Exit()方法或者Windows系统注销引发
            {
                e.Cancel = true;  // 取消退出
                this.Hide();
                this.Opacity = 0.00F;
                this.WindowState = FormWindowState.Minimized;  // 窗口最小化
                NoticeBar.Visible = true;  // 显示在托盘中
            }
            else
            {
                NoticeBar.Dispose();
                Application.Exit();
            }
        }

        private void DownLoad_AllNew_USBHDDSpyToolStripMenuItem_Click(object sender, EventArgs e)  // 单击“下载最新版的USBHDDSpy”
        {
            using (Process ShellCmd = new Process())
            {
                ShellCmd.StartInfo.FileName = "cmd.exe";
                ShellCmd.StartInfo.RedirectStandardInput = true; ShellCmd.StartInfo.UseShellExecute = false; ShellCmd.StartInfo.CreateNoWindow = true;  // 隐式调用cmd.exe
                ShellCmd.Start();
                ShellCmd.StandardInput.WriteLine("EXPLORER \"https://github.com/Anawaert-Download/USBHDDSpy_Download/archive/refs/heads/main.zip\" & EXIT");  // 导航到下载地址
            }
        }

        private void Visit_GitHub_ToolStripMenuItem_Click(object sender, EventArgs e)  // 单击“访问开源项目”
        {
            using (Process ShellCmd = new Process())
            {
                ShellCmd.StartInfo.FileName = "cmd.exe";
                ShellCmd.StartInfo.RedirectStandardInput = true; ShellCmd.StartInfo.UseShellExecute = false; ShellCmd.StartInfo.CreateNoWindow = true;  // 隐式调用cmd.exe
                ShellCmd.Start();
                ShellCmd.StandardInput.WriteLine("EXPLORER \"https://github.com/Anawaert/USBHDDSpy\" & EXIT");  // 导航到开源项目网址
            }
        }

        private void Cancel_Operation_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NoticeBarMenu.Hide();  // 隐藏
        }

        private void NoticeBarMenu_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.X > NoticeBarMenu.Right) && (e.Y > NoticeBarMenu.Bottom) && (e.X < NoticeBarMenu.Left) && (e.Y < NoticeBarMenu.Top))  // 当鼠标在菜单外部点击时
            {
                NoticeBarMenu.Hide();  // 隐藏菜单
            }
        }
        #endregion

        #region 处理复制目标文件名的名称 (The region that consists of a funtion used for extracting file name from the original path)

        /// <summary>
        /// 用以提取U盘单个文件的具体名称以用于复制到目标文件夹时文件副本的命名
        /// The function that is used for extracting the original file name in order to
        /// make the copies of files in local folder have the same name.
        /// </summary>
        /// <param name="Source">原来的路径名称 The origial complete path of a file</param>
        /// <returns></returns>
        internal string GenerateTargetName(string Source)
        {
            string[] NmTemp = Source.Split("\\");
            return NmTemp[NmTemp.Length - 1];
        }
        #endregion

        #region UI简易动效 (The region for achieving some simple UI effects)
        private void AuthorInfoLabel_MouseHover(object sender, EventArgs e)
        {
            AuthorInfoLabel.BackColor = Color.LightSkyBlue;
            AuthorInfoLabel.ForeColor = Color.White;
        }
        private void AuthorInfoLabel_MouseLeave(object sender, EventArgs e)  // 处理AuthorInfoLabel的鼠标滑过效果
        {
            AuthorInfoLabel.ForeColor = Color.Black;
            AuthorInfoLabel.BackColor = this.BackColor;
        }

        private void RecognitionLabel_MouseHover(object sender, EventArgs e)
        {
            RecognitionLabel.ForeColor = Color.White;
            RecognitionLabel.BackColor = Color.LightSkyBlue;
        }
        private void RecognitionLabel_MouseLeave(object sender, EventArgs e)  // 处理RecognitionLabel的鼠标滑过效果
        {
            RecognitionLabel.ForeColor = Color.Black;
            RecognitionLabel.BackColor = this.BackColor;
        }

        private void ExitLabel_MouseHover(object sender, EventArgs e)
        {
            ExitLabel.ForeColor = Color.White;
            ExitLabel.BackColor = Color.Red;
        }
        private void ExitLabel_MouseLeave(object sender, EventArgs e)  // 处理ExitLabel的鼠标滑过效果
        {
            ExitLabel.ForeColor = Color.Black;
            ExitLabel.BackColor = this.BackColor;
        }

        private void MinimumLabel_MouseHover(object sender, EventArgs e)
        {
            MinimumLabel.ForeColor = Color.White;
            MinimumLabel.BackColor = Color.Red;
        }
        private void MinimumLabel_MouseLeave(object sender, EventArgs e)  // 处理MinimumLabel的鼠标滑过效果
        {
            MinimumLabel.ForeColor = Color.Black;
            MinimumLabel.BackColor = this.BackColor;
        }

        private void MinimumLabel_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ExitLabel_Click(object sender, EventArgs e)  // 处理两个按钮的功能（一个为可视化最小化，一个为隐藏式最小化）
        {
            this.Hide();           
            this.Opacity = 0.00F;
            this.WindowState = FormWindowState.Minimized;

            this.ShowInTaskbar = false;
            this.NoticeBar.Visible = true;
            ExitLabel.BackColor = this.BackColor; ExitLabel.ForeColor = Color.Black;
        }
        #endregion

        #region 常量、静态变量区域 (The region of const and static variables)
        internal static Point MouseOff; public static bool LeftFlag;  // 前者用于程序拖动时记录鼠标指针初始坐标，后者用于指示是否鼠标左键按下
        internal static string DocPath = @"C:\Users\" + Environment.UserName + @"\Documents\";  // LogPath为用户的文档文件夹目录

        internal static string[] checkboxval = { "1", "1", "0", "0", "0" };  // 0为CheckBox未被选中，1为被选中
        internal static string SavePathVal = DocPath + "USBHDDSpy_Reserve";
        internal static string FormatsVal = string.Empty;
        internal static string UpdateOrNot = "1";
        internal static string DiskIdVal = string.Empty;  // 同理,从上到下依次是保存路，特定格式，是否更新，硬盘标识码

        internal const string GetCkBvalRegex = @"(?<=\{checkboxinfo\})(.*?)(?=\{endcheckboxinfo\})";  // 提取文本中{checkboxinfo}到{endcheckboxinfo}中全部内容的正则表达式
        internal const string GetSavePathRegex = @"(?<=\{savepathinfo\})(.*?)(?=\{endsavepathinfo\})";
        internal const string GetCertainFormatsRegex = @"(?<=\{certainformatinfo\})(.*?)(?=\{endcertainformatinfo\})";
        internal const string GetUpdateOrNotRegex = @"(?<=\{updateinfo\})(.*?)(?=\{endupdateinfo\})";
        internal const string GetDiskIdFromLog = @"(?<=\{reliablediskid\})(.*?)(?=\{endreliablediskid\})";  // 同理
                                                                                                             // 正则表达式参考：OpenAI-ChatGPT
        #endregion
    }
}