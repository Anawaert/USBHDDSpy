using static System.Convert;  // ��������ת��
using System.Text.RegularExpressions;  // ������ʽ
using Microsoft.Win32;  // �������WinAPI
using additionalAttributes;  // ���¼��
using System.Diagnostics;  // ����Process��

namespace USBHDDSpy
{
    public partial class MainConsole : Form
    {
        public MainConsole()
        {
            InitializeComponent();
        }

        #region ���������� (The region of functions that deal with the configuration of initialising the program)
        private void MainConsole_Load(object sender, EventArgs e)  // �����ڼ���
        {
            #region ����ʱ�����ȡ�����ļ����޸�UI�Ĳ���
                Directory.CreateDirectory(SavePathVal);  // ����Ԥ���Ĵ洢Ŀ¼
            if (File.Exists(DocPath + "UHSLog.info"))
            {
                ReadLFAndApply(DocPath);  // �����ļ��������ȡ��Ӧ����UI��
            }
            else
            {
                CreateLogFile(DocPath);
                ReadLFAndApply(DocPath);  // �����ļ������ھ��ȴ�����Ȼ��Ӧ����UI��
            }
            #endregion

            #region ����ʱ������ʵ�ֵĲ���
            if (StartCkB.Checked) { StartWithStartup(); } else { NotStartWithStartUp(); }

            if (AutoCopyCkB.Checked) { CopyTimer.Enabled = true; } else { CopyTimer.Enabled = false; }

            StatusLabel.Text = "����";
            #endregion

            #region ������
            UpdateCheck updateCheck = new UpdateCheck();
            updateCheck.CheckGitHub(); SetToTheLF(DocPath);  // �������º���д���������ļ�
            #endregion
        }
        #endregion

        #region ����Ӧ�á���ť����ʱ�Ĳ��� (The region of dealing with the operations when ""Apply" button was clicked)
        private void ConfirmBtn_Click(object sender, EventArgs e)  // �����¡�Ӧ�á���ť��
        {
            SetToTheLF(DocPath);  // �Ƚ�UI�ĸ���Ӧ�õ������ļ���
            ReadLFAndApply(DocPath);  // �ٽ������ļ�������Ӧ�õ�UI����Ŀ����Ϊ��ȷ�ϸ��Ļ���ǰ��Ķ�д�ɹ��ˡ�

            if (StartCkB.Checked)  // ���ѡ�п�������
            {
                StartWithStartup();  // ����
            }
            else
            {
                NotStartWithStartUp();  // ������
            }

            if (!WhereToSaveCkB.Checked)
            {
                SavePathVal = DocPath + @"\USBHDDSpy_Reserve";  // ��ѡָ���ļ��洢λ����Ĭ����C:\Users\%username%\Documents\USBHDDSpy_Reserve��
            }

            if (!FormatSelectCkB.Checked)
            {
                FormatsVal = "";  // ��ѡָ����չ����ʽ��Ĭ��Ϊ""���գ�
            }

            if (AutoCopyCkB.Checked)
            {
                CopyTimer.Start();  // ѡ�����CopyTimerʹ��
            }
            else
            {
                CopyTimer.Stop();  // ��ѡ����ʧ��
            }

            StatusLabel.Text = "��Ӧ������";
        }
        #endregion

        #region ���������϶� (The region of dealing with the effects when moving the application with pointer)
        private void MainConsole_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseOff = new Point(-e.X, -e.Y);  // ���MainConsole_MouseMove������Point.Offset����ʹ�ã�Ϊ����
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

        #region ���������ļ���ص���Ϊ (The region of dealing with the configuration file)

        /// <summary>
        /// ���Դ����µķ��ϳ�ʼ״̬�������ļ�
        /// (The function that used for creating a brand new cofiguration file that fits the original condition of the program)
        /// </summary>
        /// <param name="FolderPath">��������ļ����ļ��еľ���·�� (The absolute path of the configuration file)</param>
        public void CreateLogFile(string FolderPath)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(FolderPath + "UHSLog.info", true))  // ���ļ��Ѿ������ڵ�ǰ���´���д�����������ļ�
                {
                    sw.Write("{checkboxinfo}");
                    for (int i = 0; i < 4; i++)
                    {
                        sw.Write(checkboxval[i] + ",");
                    }
                    sw.Write(checkboxval[4] + "{endcheckboxinfo}\n");  // д��{checkboxinfo}1,1,0,0,0{endcheckboxinfo}

                    sw.Write("{savepathinfo}" + FolderPath + "USBHDDSpy_Reserve" + "{endsavepathinfo}\n" +
                        "{certainformatinfo}{endcertainformatinfo}\n");

                    sw.Write("{updateinfo}1{endupdateinfo}\n");
                    sw.Write("{reliablediskid}{endreliablediskid}");  // ͬ��
                }
            }
            catch
            {
                MessageBox.Show("��" + DocPath + "�´���USBHDDSpy�����ļ�ʧ��\n��ȷ������" + DocPath + "���㹻�Ķ�дȨ��", "USBHDDSpy ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        /// <summary>
        /// ���Զ�ȡ�Ѿ����ڵ������ļ�����Ӧ����UI����
        /// (The function that used for reading the existed configuration file and applying to UI interface according to the configuration file)
        /// </summary>
        /// <param name="FolderPath">>��������ļ����ļ��еľ���·�� (The absolute path of the configuration file)</param>
        public void ReadLFAndApply(string FolderPath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(FolderPath + "UHSLog.info"))  // ������ȡ��
                {
                    string WholeContent = sr.ReadToEnd();  // ��ȡ�����ļ�ȫ�ģ��������������ļ��Ĵ�С�ǳ�С�����ʹ��ReadToEnd()�ķ��첽�����Ǽ�����Ӱ�����ܵ�

                    Regex regexCkB = new Regex(GetCkBvalRegex);  // ����һ������Ϊƥ��{checkboxinfo}��{endcheckboxinfo}֮��ȫ�����ݵ�Regex�����
                    string CkBvalString = regexCkB.Match(WholeContent).Value;  // ��ȡƥ��ֵ��Ӧ��Ϊ_,_,_,_,_����ʽ������_Ϊ�ַ�����0��1
                    string[] checkboxvalTemp = CkBvalString.Split(',');  // ��","�ַ��и������һ���ַ�������
                    for (int i = 0; i < 5; i++)  // ʵ��������Ӧ��ʹ��foreach�����Ǽ�Ȼ�����Ƕ�֪�����ָ�������鳤��Ӧ��Ϊ5����˾�ֱ����5��ʹ����
                    {
                        checkboxval[i] = checkboxvalTemp[i];  // ��ȫ�ֵľ�̬������һ���޸�
                        switch (i)
                        {
                            case 0:
                                if (checkboxval[i] == "0")
                                {
                                    StartCkB.Checked = false;  // ��checkboxval�����е�һ��Ϊ"0"ʱ����������Ӧ��δ��ѡ�У���ȡ����
                                }
                                else
                                {
                                    StartCkB.Checked = true;  // ��֮��Ϊѡ��
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
                                // ����������̡�
                        }
                    }

                    Regex regexTb = new Regex(GetSavePathRegex);
                    string SavePathString = regexTb.Match(WholeContent).Value;  // ͬ��������ļ��а������ļ����·��
                    SavePathTextBox.Text = SavePathString;  // Ӧ����UI
                    SavePathVal = SavePathString == "" ? SavePathVal : SavePathString;  // ���¾�̬����

                    Regex regexCf = new Regex(GetCertainFormatsRegex);
                    string CertainFormatsString = regexCf.Match(WholeContent).Value;
                    CertainFormatsTextBox.Text = CertainFormatsString;
                    FormatsVal = CertainFormatsString;  // ͬ����ָ����ʽ

                    Regex regexUd = new Regex(GetUpdateOrNotRegex);
                    UpdateOrNot = regexUd.Match(WholeContent).Value;  // ����ѡ��

                    Regex regexDkId = new Regex(GetDiskIdFromLog);
                    DiskIdVal = regexDkId.Match(WholeContent).Value;  // ��ȡ��ע���Ӳ�����к�
                }           
            }
            catch
            {
                MessageBox.Show("��ȡ�����ļ�ʧ��\n��ȷ������" + DocPath + "���㹻�Ķ�дȨ��", "USBHDDSpy ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();

            }
        }

        /// <summary>
        /// ���Խ��û�������޸�Ӧ���������ļ�����
        /// (The function that used for saving users' changes and converting the content of configuration file at the same time)
        /// </summary>
        /// <param name="FloderPath">��������ļ����ļ��еľ���·�� (The absolute path of the configuration file)</param>
        public void SetToTheLF(string FloderPath)
        {
            try
            {
                checkboxval[0] = ToInt32(StartCkB.Checked).ToString(); checkboxval[1] = ToInt32(AutoCopyCkB.Checked).ToString();
                checkboxval[2] = ToInt32(FinishedMsgCkB.Checked).ToString(); checkboxval[3] = ToInt32(WhereToSaveCkB.Checked).ToString(); checkboxval[4] = ToInt32(FormatSelectCkB.Checked).ToString();  // ���¾�̬����

                StreamReader sr = new StreamReader(FloderPath + "UHSLog.info");
                string WholeContent = sr.ReadToEnd();

                string laterValTemp = "";
                for (int i = 0; i < 5; i++)
                {
                    laterValTemp += i != 4 ? checkboxval[i] + "," : checkboxval[i];
                }  // ��checkboxval�����̬���������ֶ���Ϊһ���ַ����洢��laterValTemp��

                WholeContent = Regex.Replace(WholeContent, GetCkBvalRegex, laterValTemp);
                WholeContent = Regex.Replace(WholeContent, GetSavePathRegex, SavePathTextBox.Text);
                WholeContent = Regex.Replace(WholeContent, GetCertainFormatsRegex, CertainFormatsTextBox.Text);
                WholeContent = Regex.Replace(WholeContent, GetUpdateOrNotRegex, UpdateOrNot);
                WholeContent = Regex.Replace(WholeContent, GetDiskIdFromLog, DiskIdVal);
                sr.Close();  // �滻�޸�ԭ���������ļ�����

                FileStream fs = new FileStream(FloderPath + "UHSLog.info", FileMode.Create, FileAccess.ReadWrite); fs.Dispose();  // �������ǵķ����������ļ���գ�������������
                StreamWriter sw = new StreamWriter(FloderPath + "UHSLog.info");
                sw.Write(WholeContent);
                sw.Close();  // ���޸ĺ�ġ�ȫ�ġ�д��ȥ
            }
            catch
            {
                MessageBox.Show("д�������ļ�ʧ��\n��ȷ������" + DocPath + "���㹻�Ķ�дȨ��", "USBHDDSpy ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();

            }
        }
        #endregion

        #region ���������� (The region of dealing with starting the program when system is starting up)
        internal const string SoftwareName = "USBHDDSpy";  // ��������

        /// <summary>
        /// ����д��ע�����ʵ�ֿ��������ĺ���
        /// The function that used for reading/writing Registry to start program while system's starting up
        /// </summary>
        internal void StartWithStartup()  // д��ע���������
        {
            string? PathTemp = Application.ExecutablePath;  // ��ִ���ļ���ǰ����·��
            RegistryKey rk = Registry.CurrentUser;  // ��ȡ��ǰ�û���ע���Key
            try
            {
                RegistryKey rk_2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");  // �����Key
                string? old_path = (string?)rk_2.GetValue(SoftwareName);
                if (!PathTemp.Equals(old_path))  // ����ظ�
                {
                    rk_2.SetValue(SoftwareName, PathTemp);
                }
                rk.Close(); rk_2.Close();  // �ͷ�����
            }
            catch { }  // ��װ�д�����
        }

        /// <summary>
        /// ����д��ע�����ȡ�����������ĺ���
        /// The function that used for reading/writing Registry to cancle to start the program while system's starting up
        /// </summary>
        internal void NotStartWithStartUp()  // ȡ������
        {
            RegistryKey rk = Registry.CurrentUser;
            try
            {
                RegistryKey rk_2 = rk.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                string? old_path = (string?)rk_2.GetValue(SoftwareName);
                rk_2.DeleteValue(SoftwareName, false);
                rk.Close(); rk_2.Close();  // ͬ�ϣ������
            }
            catch { }
        }  // ����ο���https://blog.csdn.net/Raink_LH/article/details/128671015
        #endregion

        #region �����ļ��Զ����� (The region of dealing with coping files automatically)

        /// <summary>
        /// ���ڴ����ļ��Զ����Ƶ�ָ���ļ��еĺ�����������ActionTimer��Tick�¼�����������
        /// (The function that used for copying files form USB devices towards the specific folder automatically.
        /// It's also the function of ActionTimer triggered by "Tick" event)
        /// </summary>
        /// <param name="sender">ActionTimer����</param>
        /// <param name="e">�¼����� (Event data)</param>
        public void ActionTimer_Tick(object sender, EventArgs e)  // ���������ִ�й���ʱ�������ļ����Ƶ�Ŀ��Ŀ¼��
        {
            CopyTimer.Enabled = false;  // ����ͣ����ʱ��������ܵĳ�ͻ

            try
            {
                if (IsOneUSBHDD())  // ������е���U�̲���
                {
                    DriveInfo[] driveInfo = DriveInfo.GetDrives();  // ��ȡ���������϶���
                    foreach (DriveInfo dI in driveInfo)  // �������ϲ�
                    {
                        if (dI.DriveType == DriveType.Removable)  // ������������Ϊ���ƶ�ʽ������ʱ
                        {
                            if (!DiskRecognitionRelated.IsDiskRegistered(Regex.Match(dI.Name, @".+?(?=:\\)").Value, DiskIdVal))  // ���������������Ϊ���ƶ�����,��������δ����ע���ʱ
                            {
                                if (dI.IsReady && (dI.TotalSize <= (long)64 * 1024 * 1024 * 1024) &&
                                   (new DriveInfo(Regex.Match(SavePathVal, @".+?(?=:\\)").Value).TotalFreeSpace > dI.TotalSize - dI.TotalFreeSpace))  // ͬʱ�������������ȫ�̴�СС��16GB���������㿽��Ŀ¼�������������пռ����USB�������������ļ���С�ܺ�
                                {
                                    if (FormatsVal != "")  // ���FormatsVal����""�Ļ���Ҳ�����û�ָ���˸�ʽ
                                    {
                                        foreach (string cF in CertainFormatsToArray(FormatsVal))  // ������ȡ����ָ����ʽ��CertainFormatsToArray(string)��������ȥ���ո����������ߴ���
                                        {
                                            IEnumerable<string> aimFilesCollection = Directory.EnumerateFiles(dI.Name, @"*." + cF, SearchOption.AllDirectories);  // ��ȡ����Ŀ���ļ�����·���ַ����ļ��϶���*��ʾһ�λ���ƥ�䣬cFΪָ����ʽ
                                            foreach (string originPosition in aimFilesCollection)  // �����ö����ȡ�����ļ�·��
                                            {
                                                StatusLabel.Text = "���ڸ���" + originPosition + "��" + SavePathVal;
                                                File.Copy(originPosition, SavePathVal + @"\" + GenerateTargetName(originPosition), true);  // ���ļ����������������ļ�����и�����д
                                            }
                                        }
                                    }
                                    else  // ���Ϊ""����˵����5��Checkbox�Ĺ�û�򣬼�Ĭ��ȫ���ļ�
                                    {
                                        IEnumerable<string> aimFilesCollection_2 = Directory.EnumerateFiles(dI.Name, @"*.*", SearchOption.AllDirectories);  // "*.*"ƥ��ȫ���ļ�
                                        foreach (string originPosition_2 in aimFilesCollection_2)
                                        {
                                            StatusLabel.Text = "���ڸ���" + originPosition_2 + "��" + SavePathVal;
                                            File.Copy(originPosition_2, SavePathVal + @"\" + GenerateTargetName(originPosition_2), true);  // �����ļ��������ظ��������д
                                        }
                                    }
                                    if (checkboxval[2] == "1")
                                        MessageBox.Show("��ҵ�����", "USBHDDSpy ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                            }
                        }
                    }
                }
            }
            catch { }  // ��catch�飬���޷�����ֵ����ʱ����Ҫ�Ĳ���

            StatusLabel.Text = "����";
            CopyTimer.Enabled = true;  // �ָ�����ʱ
        }  // ��΢����
        #endregion

        #region �жϲ����USB�洢�豸�Ƿ�Ϊ���� (The region of dealing with detecting how many USB devices were pluged in the computer)

        /// <summary>
        /// �����ж��Ƿ�����U������Ϊ1��
        /// (The function that used for detecting that only one USB device was pluged in the computer)
        /// </summary>
        /// <returns>����ֵ���漴Ϊ1�����ټ�Ϊ0���� (The boolean value, true means the single one and false means zero or more than one devices)</returns>
        internal bool IsOneUSBHDD()
        {
            int USBHDD_Amount = 0;  // U������
            foreach (DriveInfo info in DriveInfo.GetDrives())  // ����������
            {
                USBHDD_Amount += ToInt32(info.DriveType == DriveType.Removable);  // ��USB��������+1��û�оͲ���
            }
            return USBHDD_Amount == 1 ? true : false;  // �����ͷ���true��0������false
        }  /* �˴�������δ�����±�д���USB��������ʶ��Ĺ������� */
        #endregion

        #region ����ָ����չ�� (The region of dealing with the specific formats inputed by users) 
        /// <summary>
        /// ���Խ������������ļ��е��Զ��ŷָ��ġ��ض���ʽ��ת��Ϊ��׺������ȷ��ʽ
        /// (The function that can make the specific file formats saved in local configuration file convert to
        /// right string format that program can deal with)
        /// </summary>
        /// <param name="certainFmtString">�Զ���(',')�ָ��ġ��ض���ʽ���ַ��� (The string variable splited by character ( ',') stored in local configuration file)</param>
        /// <returns>string[]����������������ļ���ʽ�� (string[], including all the file formats that users inputed)</returns>
        private string[] CertainFormatsToArray(string certainFmtString)
        {
            string[] cFS = certainFmtString.Trim().Split(',');  // ��ͨ��Split�����ѱ����ŷָ����ַ�����ȡ��һ��������
            for (int i = 0; i < cFS.Length; i++)
            {
                cFS[i] = Regex.Match(cFS[i], @"\S+").Value;  // ����foreachֻ�����˴�ʹ��forѭ����ͨ������ƥ�����зǿ��ַ���дÿ������Ԫ��
            }
            return cFS;  // �������string���͵�����
        }
        #endregion

        #region ע��Ϊ�����ε�USB�洢�豸 (The region of dealing with registering USB storage devices)

        /// <summary>
        /// ���ڴ���USB�豸ע��Ϊ�����ε�U�̵ĺ��������Label�ĵ���¼�������һ��
        /// (The function used for dealing with registering a USB device in order to make it reliable and
        /// the program will not copy anything from it.
        /// This function is also the function triggered by the "Click" event of RecognitionLabel)
        /// </summary>
        /// <param name="sender">������ (sender)</param>
        /// <param name="e">�¼����� (EventArgs)</param>
        private void RecognitionLabel_Click(object sender, EventArgs e)  // ������ע������U��ʱ          /* ע���÷�������Ҫ�ж�U���Ƿ�ע�������Ҫ�Ѷ�ȡ����Ӳ�̱�ʶ��д�������ļ��� */
        {
            try
            {
                if (IsOneUSBHDD())  // ����⵽����һ��U��ʱ
                {
                    var allDriveInfo = DriveInfo.GetDrives();
                    foreach (DriveInfo disk in allDriveInfo)  // ����ȫ��������
                    {
                        if (disk.DriveType == DriveType.Removable)
                        {
                            if (!DiskRecognitionRelated.IsDiskRegistered(Regex.Match(disk.Name, @".+?(?=:\\)").Value, DiskIdVal))  // ���������������Ϊ���ƶ�����,��������δ����ע���ʱ
                            {
                                string[] DkValArrTemp = DiskIdVal.Split(',');  // ��DiskIdVal��Ӣ���ַ�","�ָ�
                                int NEW_ARR_LENTH = DkValArrTemp[0] == "" ? 1 : DkValArrTemp.Length + 1;  // NEW_ARR_IENTHΪDkValArr[]�ĳ��ȡ����DkValArrTemp[0]Ϊ�գ����κ�U�̶�δע�ᣬ����NEW_ARR_LENTHΪ1��������ǣ���˵��������һ��U���Ѿ�ע�ᣬ�������DkValArrTemp���ȶ�1
                                string[] DkValArr = new string[NEW_ARR_LENTH];  

                                if (DkValArr.Length != DkValArrTemp.Length)  // ����������Ȳ�һ,Ҳ���ǡ���ע�������U��
                                {
                                    for (int i = 0; i < DkValArrTemp.Length; i++)
                                    {
                                        DkValArr[i] = DkValArrTemp[i];  // ��DkValArrTemp�еĸ���Ƶ�DkValArr
                                    }
                                    uint ValTemp;
                                    DiskRecognitionRelated.GET_USBHDD_ID(Regex.Match(disk.Name, @".+?(?=:\\)").Value, out ValTemp);
                                    DkValArr[DkValArr.Length - 1] = ValTemp.ToString();  // ���һ�������µ�U��Ӳ����ʶ��
                                    DiskIdVal = String.Empty;  // ��DiskIdVal���ÿ�

                                    for (int i = 0; i < DkValArr.Length; i++)
                                    {
                                        DiskIdVal += i <= DkValArr.Length - 2 ? DkValArr[i] + @"," : DkValArr[i];  // ��ȫ�¡���ֵ��DiskIdVal��
                                    }

                                    SetToTheLF(DocPath);  // д�������ļ�
                                }
                                else  // ���һ��ʼ��Ϊ�յ�,Ҳ������ȫû��U��ע���
                                {
                                    uint ValTemp;
                                    DiskRecognitionRelated.GET_USBHDD_ID(Regex.Match(disk.Name, @".+?(?=:\\)").Value, out ValTemp);  // ValTempΪU�̵�Ӳ�̱�ʶ��
                                    DkValArr[0] = ValTemp.ToString();  // д���������,ʵ���ϲ�������
                                    DiskIdVal = ValTemp.ToString();  // д�뾲̬����
                                    SetToTheLF(DocPath);  // �޸��������ļ���
                                }
                            }
                            else
                            {
                                MessageBox.Show("\"" + new DriveInfo(disk.Name).VolumeLabel + "\" �Ѿ���ע��Ϊ�����ε�U����", "USBHDDSpy ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            StatusLabel.Text = "�ѳɹ�ע�� \"" + disk.VolumeLabel + "\" Ϊ�����ε�USB�洢�豸";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("�����ڻ���ڶ��U�̣������������U�̺��ٽ���ע����", "USBHDDSpy ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("����Windows API��Ӧ�õ������ļ����ִ�������Windowsϵͳ�ļ��������Ի����ԡ��ĵ����ļ��еĶ�дȨ��", "USBHDDSpy ��Ϣ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region �����ļ�����·����ѡ������ (The region of dealing with where to save the files when users click the "..." button)
        private void PathSelectBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();  // ʵ����һ���ļ�ѡ��鿴��
            folderBrowser.Description = "��ѡ���ļ������Ĵ洢·����";

            if (folderBrowser.ShowDialog() == DialogResult.OK)  // ��Dialog��·��ѡ��ȷ��ʱ
            {
                SavePathTextBox.Text = folderBrowser.SelectedPath;  // ���
            }
        }
        #endregion

        #region ����鿴������USBHDDSpy��ʱ (When users click the "About USBHDDSpy" label...)
        private void AuthorInfo_Click(object sender, EventArgs e)
        {
            var AboutUSBHDDSpy = new ProgramInfo();
            AboutUSBHDDSpy.Show();
        }
        #endregion

        #region ��������Сͼ��Ĳ��� (The region for dealing with some functions within the notice bar)
        private void NoticeBar_DoubleClick(object sender, EventArgs e)  // ˫������Сͼ���л���ǰ̨
        {
             this.WindowState = FormWindowState.Normal;           
            this.Visible = true;
            this.Opacity = 1.00F;

            this.ShowInTaskbar = true;
            NoticeBar.Visible = false;
        }

        private void To_Visible_MenuItem_Click(object sender, EventArgs e)  // �л���ǰ̨��ContextMenuStrip�Ѱ󶨸�NotifyInco��
        {
            this.WindowState = FormWindowState.Normal;
            this.Visible = true;
            this.Opacity = 1.00F;
            
            this.ShowInTaskbar = true;
            NoticeBar.Visible = false;
        }

        private void Exit_Programme_MenuItem_Click(object sender, EventArgs e)  // �˳�����Ӧ�ó���
        {
            NoticeBar.Dispose();
            Application.Exit();
        }

        private void MainConsole_FormClosing(object sender, FormClosingEventArgs e)  // ��д�رճ���ʱ����������̵Ĵ���
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall && e.CloseReason != CloseReason.WindowsShutDown)  // ���˳��ɷ�Application.Exit()��������Windowsϵͳע������
            {
                e.Cancel = true;  // ȡ���˳�
                this.Hide();
                this.Opacity = 0.00F;
                this.WindowState = FormWindowState.Minimized;  // ������С��
                NoticeBar.Visible = true;  // ��ʾ��������
            }
            else
            {
                NoticeBar.Dispose();
                Application.Exit();
            }
        }

        private void DownLoad_AllNew_USBHDDSpyToolStripMenuItem_Click(object sender, EventArgs e)  // �������������°��USBHDDSpy��
        {
            using (Process ShellCmd = new Process())
            {
                ShellCmd.StartInfo.FileName = "cmd.exe";
                ShellCmd.StartInfo.RedirectStandardInput = true; ShellCmd.StartInfo.UseShellExecute = false; ShellCmd.StartInfo.CreateNoWindow = true;  // ��ʽ����cmd.exe
                ShellCmd.Start();
                ShellCmd.StandardInput.WriteLine("EXPLORER \"https://github.com/Anawaert-Download/USBHDDSpy_Download/archive/refs/heads/main.zip\" & EXIT");  // ���������ص�ַ
            }
        }

        private void Visit_GitHub_ToolStripMenuItem_Click(object sender, EventArgs e)  // ���������ʿ�Դ��Ŀ��
        {
            using (Process ShellCmd = new Process())
            {
                ShellCmd.StartInfo.FileName = "cmd.exe";
                ShellCmd.StartInfo.RedirectStandardInput = true; ShellCmd.StartInfo.UseShellExecute = false; ShellCmd.StartInfo.CreateNoWindow = true;  // ��ʽ����cmd.exe
                ShellCmd.Start();
                ShellCmd.StandardInput.WriteLine("EXPLORER \"https://github.com/Anawaert/USBHDDSpy\" & EXIT");  // ��������Դ��Ŀ��ַ
            }
        }

        private void Cancel_Operation_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NoticeBarMenu.Hide();  // ����
        }

        private void NoticeBarMenu_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.X > NoticeBarMenu.Right) && (e.Y > NoticeBarMenu.Bottom) && (e.X < NoticeBarMenu.Left) && (e.Y < NoticeBarMenu.Top))  // ������ڲ˵��ⲿ���ʱ
            {
                NoticeBarMenu.Hide();  // ���ز˵�
            }
        }
        #endregion

        #region ������Ŀ���ļ��������� (The region that consists of a funtion used for extracting file name from the original path)

        /// <summary>
        /// ������ȡU�̵����ļ��ľ������������ڸ��Ƶ�Ŀ���ļ���ʱ�ļ�����������
        /// The function that is used for extracting the original file name in order to
        /// make the copies of files in local folder have the same name.
        /// </summary>
        /// <param name="Source">ԭ����·������ The origial complete path of a file</param>
        /// <returns></returns>
        internal string GenerateTargetName(string Source)
        {
            string[] NmTemp = Source.Split("\\");
            return NmTemp[NmTemp.Length - 1];
        }
        #endregion

        #region UI���׶�Ч (The region for achieving some simple UI effects)
        private void AuthorInfoLabel_MouseHover(object sender, EventArgs e)
        {
            AuthorInfoLabel.BackColor = Color.LightSkyBlue;
            AuthorInfoLabel.ForeColor = Color.White;
        }
        private void AuthorInfoLabel_MouseLeave(object sender, EventArgs e)  // ����AuthorInfoLabel����껬��Ч��
        {
            AuthorInfoLabel.ForeColor = Color.Black;
            AuthorInfoLabel.BackColor = this.BackColor;
        }

        private void RecognitionLabel_MouseHover(object sender, EventArgs e)
        {
            RecognitionLabel.ForeColor = Color.White;
            RecognitionLabel.BackColor = Color.LightSkyBlue;
        }
        private void RecognitionLabel_MouseLeave(object sender, EventArgs e)  // ����RecognitionLabel����껬��Ч��
        {
            RecognitionLabel.ForeColor = Color.Black;
            RecognitionLabel.BackColor = this.BackColor;
        }

        private void ExitLabel_MouseHover(object sender, EventArgs e)
        {
            ExitLabel.ForeColor = Color.White;
            ExitLabel.BackColor = Color.Red;
        }
        private void ExitLabel_MouseLeave(object sender, EventArgs e)  // ����ExitLabel����껬��Ч��
        {
            ExitLabel.ForeColor = Color.Black;
            ExitLabel.BackColor = this.BackColor;
        }

        private void MinimumLabel_MouseHover(object sender, EventArgs e)
        {
            MinimumLabel.ForeColor = Color.White;
            MinimumLabel.BackColor = Color.Red;
        }
        private void MinimumLabel_MouseLeave(object sender, EventArgs e)  // ����MinimumLabel����껬��Ч��
        {
            MinimumLabel.ForeColor = Color.Black;
            MinimumLabel.BackColor = this.BackColor;
        }

        private void MinimumLabel_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ExitLabel_Click(object sender, EventArgs e)  // ����������ť�Ĺ��ܣ�һ��Ϊ���ӻ���С����һ��Ϊ����ʽ��С����
        {
            this.Hide();           
            this.Opacity = 0.00F;
            this.WindowState = FormWindowState.Minimized;

            this.ShowInTaskbar = false;
            this.NoticeBar.Visible = true;
            ExitLabel.BackColor = this.BackColor; ExitLabel.ForeColor = Color.Black;
        }
        #endregion

        #region ��������̬�������� (The region of const and static variables)
        internal static Point MouseOff; public static bool LeftFlag;  // ǰ�����ڳ����϶�ʱ��¼���ָ���ʼ���꣬��������ָʾ�Ƿ�����������
        internal static string DocPath = @"C:\Users\" + Environment.UserName + @"\Documents\";  // LogPathΪ�û����ĵ��ļ���Ŀ¼

        internal static string[] checkboxval = { "1", "1", "0", "0", "0" };  // 0ΪCheckBoxδ��ѡ�У�1Ϊ��ѡ��
        internal static string SavePathVal = DocPath + "USBHDDSpy_Reserve";
        internal static string FormatsVal = string.Empty;
        internal static string UpdateOrNot = "1";
        internal static string DiskIdVal = string.Empty;  // ͬ��,���ϵ��������Ǳ���·���ض���ʽ���Ƿ���£�Ӳ�̱�ʶ��

        internal const string GetCkBvalRegex = @"(?<=\{checkboxinfo\})(.*?)(?=\{endcheckboxinfo\})";  // ��ȡ�ı���{checkboxinfo}��{endcheckboxinfo}��ȫ�����ݵ�������ʽ
        internal const string GetSavePathRegex = @"(?<=\{savepathinfo\})(.*?)(?=\{endsavepathinfo\})";
        internal const string GetCertainFormatsRegex = @"(?<=\{certainformatinfo\})(.*?)(?=\{endcertainformatinfo\})";
        internal const string GetUpdateOrNotRegex = @"(?<=\{updateinfo\})(.*?)(?=\{endupdateinfo\})";
        internal const string GetDiskIdFromLog = @"(?<=\{reliablediskid\})(.*?)(?=\{endreliablediskid\})";  // ͬ��
                                                                                                             // ������ʽ�ο���OpenAI-ChatGPT
        #endregion
    }
}