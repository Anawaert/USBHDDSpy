using System.Text.RegularExpressions;

namespace USBHDDSpy
{
    public static class LogOperations
    {
        public static string[] checkboxval = { "1","1","0","0","0" };  // 0为CheckBox未被选中，1为被选中
        public static string GetCkBvalRegex = @"\{checkboxinfo\}([\s\S]*?)\{endcheckboxinfo\}";  // 提取文本中{checkboxinfo}到{endcheckboxinfo}中全部内容的正则表达式

        /// <summary>
        /// 用以创建新的符合初始状态的配置文件
        /// </summary>
        /// <param name="FolderPath">存放配置文件目录的绝对路径</param>
        public static void CreateLogFile(string FolderPath)
        {
            using (StreamWriter sw = new StreamWriter(FolderPath + "UHSLog.info"))
            {
                sw.WriteAsync("{checkboxinfo}");
                for (int i = 0; i < 4; i++)
                {
                    sw.WriteAsync(checkboxval[i] + ",");
                }
                sw.WriteAsync(checkboxval[4] + "{endcheckboxinfo}\n");

                sw.WriteAsync("{savepathinfo}{endsavepathinfo}" +
                    "{certainformatinfo}{endcertainformatinfo}");
            }
        }

        /// <summary>
        /// 用以读取已经存在的配置文件并且应用于UI界面
        /// </summary>
        /// <param name="FolderPath">>存放配置文件目录的绝对路径</param>
        public static void ReadLFAndApply(string FolderPath)
        {
            using (StreamReader sr = new StreamReader(FolderPath + "UHSLog.info"))
            {
                string WholeContent = sr.ReadToEnd();
                Regex regex = new Regex(GetCkBvalRegex);
                string CkBvalString = regex.Match(WholeContent).Value;
                string[] checkboxvalTemp = CkBvalString.Split(',');
                for (int i = 0; i < 5; i++)
                {
                    checkboxval[i] = checkboxvalTemp[i];
                }
                
            }
        }
    }
}
