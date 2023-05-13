using System.Runtime.InteropServices;  //  DLLImportAttribute的引用

namespace additionalAttributes
{

    /// <summary>
    /// 用于读取USB可移动磁盘设备的硬件标识符的类
    /// </summary>
    internal class DiskRecognitionRelated
    {
        /// <summary>
        /// 由DLLImportAttribute特性导入的外部DLL函数GetVolumeInformationW，用以读出U盘硬件标识符
        /// </summary>
        /// <param name="rootPathName">设备的根路径名，即驱动器号</param>
        /// <param name="volumeNameBuffer">卷名称缓冲区指针</param>
        /// <param name="volumeNameSize">卷名称缓冲区大小</param>
        /// <param name="volumeSerialNumber">卷序列号，正是我们需要的</param>
        /// <param name="maxiumComponentLength">最大文件名长度</param>
        /// <param name="fileSystemFlags">文件系统标志</param>
        /// <param name="fileSystemNameBuffer">文件系统名称缓冲区指针</param>
        /// <param name="nFileSystemNameSize">文件系统名称缓冲区大小</param>
        /// <returns>返回值为boolean，如果执行成功就返回ture，反之false</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]  // 引用非托管链接库kernal32.dll，以Unicode字符编码引用传值
        static extern bool GetVolumeInformationW(string rootPathName,
                                                       IntPtr volumeNameBuffer,
                                                       int volumeNameSize,
                                                       out uint volumeSerialNumber,
                                                       out uint maxiumComponentLength,
                                                       out uint fileSystemFlags,
                                                       IntPtr fileSystemNameBuffer,
                                                       int nFileSystemNameSize);  // 声明GetVolumeInfomationW函数原型

        /// <summary>
        /// 用以获取指定驱动器的硬件标识符
        /// </summary>
        /// <param name="driveName">驱动器名称</param>
        /// <param name="UINT_DISK_ID">外传的参数，无符号整型的驱动器硬件标识符</param>
        internal static void GET_USBHDD_ID(string driveName, out uint UINT_DISK_ID)  // out参数向外传值
        {
            GetVolumeInformationW(driveName + @":\",
                              IntPtr.Zero,
                              0,
                              out UINT_DISK_ID,
                              out uint maxComponentLength,
                              out uint flags,
                              IntPtr.Zero,
                              0);
        }

        /// <summary>
        /// 检测U盘是否已经被注册
        /// </summary>
        /// <param name="DiskIDString">已注册的驱动器ID字符串，它应该来自USBHDD命名空间下的MainConsole类</param>
        /// <returns>返回值为布尔类型，指示是否登记注册过该驱动器为受信任的驱动器</returns>
        internal static bool IsDiskRegistered(string dvName, string DiskIDString)
        {
            uint DkIdTemp;
            GET_USBHDD_ID(dvName, out DkIdTemp);  // 获取硬盘识别码
            bool IsMatch = false;  // 默认无匹配

            string[] DiskIDArrTemp = DiskIDString.Split(',');
            foreach (string SingleDiskId in DiskIDArrTemp)
            {
                IsMatch = SingleDiskId == DkIdTemp.ToString();  // 如果有匹配项，则将IsMatch赋值为true（IsMatch作用域在这个foreach外）
            }

            return IsMatch;
        }
    }
}
