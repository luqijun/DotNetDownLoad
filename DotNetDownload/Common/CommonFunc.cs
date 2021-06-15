using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownTest
{
    /// <summary>
    /// 公共方法 
    /// </summary>
    public static class CommonFunc
    {
        #region 文件处理
        /// <summary>
        /// 生成文件名
        /// </summary>
        /// <param name="rootpath">文件夹所在目录： Environment.CurrentDirectory + "\\output\\"</param>
        /// <param name="fileformat">文件名格式："数据字典_{0}.xlsx</param>
        /// <returns>返回文件路径： Environment.CurrentDirectory + "\\output\\"+"数据字典_1.xlsx</returns>
        public static string getFileName(string rootpath, string fileformat)
        {
            if (Directory.Exists(rootpath) == false)
                Directory.CreateDirectory(rootpath);

            var filename = string.Format(fileformat, DateTime.Now.ToyyyyMMddHHmmss());
            string filepath = rootpath + filename;
            while (File.Exists(filepath))
            {
                filename = string.Format(fileformat, DateTime.Now.ToyyyyMMddHHmmss());
                filepath = rootpath + filename;
            }
            return filepath;
        }
        /// <summary>
        /// 打开文件夹
        /// </summary>
        /// <param name="filepath"></param>
        public static void OpenFileDirectory(string filepath)
        {
            //打开文件夹并选中单个文件
            System.Diagnostics.Process.Start("Explorer", "/select," + filepath);
        }

        /// <summary>
        /// 写入到文件
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="msg"></param>
        public static void WriteFile(string filepath, string msg, bool isappend = true)
        {
            using (TextWriter tw = new StreamWriter(filepath, isappend, Encoding.UTF8))//true在文件末尾添加数据
            {
                tw.WriteLine(msg);
                tw.Close();
            }
        }

        /// <summary>
        /// 读取文件数据
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static List<string> ReadFile(string filepath)
        {
            List<string> lstmsg = new List<string>();

            //lstmsg = ReadFileByRead(filepath);
            lstmsg = ReadFileByStream(filepath);

            return lstmsg;
        }
        public static List<string> ReadFileByRead(string filepath)
        {

            var encoding = FileEncoding.GetType(filepath);

            List<string> lstmsg = new List<string>();
            using (StreamReader sr = new StreamReader(filepath, encoding))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line.ToString());
                    lstmsg.Add(line.ToString());
                }
            }

            ReadFileByStream(filepath);

            return lstmsg;
        }
        /// <summary>
        /// 读取文件数据(使用流)
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private static List<string> ReadFileByStream(string filepath)
        {
            var encoding = FileEncoding.GetType(filepath);

            List<string> lstmsg = new List<string>();
            string fullstring = "";
            using (FileStream fsRead = File.OpenRead(filepath))
            {
                byte[] buf = new byte[1024 * 8];
                int total = 0;
                int size = 0;
                do
                {
                    //注意第二个参数是在buffer中的偏移量，不是在文件中的偏移量
                    size = fsRead.Read(buf, 0, buf.Length);


                    //默认
                    //fullstring += encoding.GetString(buf);
                    //改为过滤BOM转换方法
                    fullstring += GetUTF8String(buf);

                    total += size;
                } while (size > 0);

                if (total != fsRead.Length)
                {
                    Console.WriteLine("Error: total != fsRead.Length");
                }

                fsRead.Close();
            }

            lstmsg = fullstring.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            return lstmsg;
        }

        /// <summary>
        /// C#去除字符串的BOM(这里说的是去除”从其他地方读取来的字符串”,而不是在代码里面写的字符串.)
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        private static string GetUTF8String(byte[] buffer)
        {
            if (buffer == null)
                return null;
            if (buffer.Length <= 3)
            {
                return Encoding.UTF8.GetString(buffer);
            }

            byte[] bomBuffer = new byte[] { 0xef, 0xbb, 0xbf };
            if (buffer[0] == bomBuffer[0] && buffer[1] == bomBuffer[1] && buffer[2] == bomBuffer[2])
            {
                return new UTF8Encoding(false).GetString(buffer, 3, buffer.Length - 3);
            }
            return Encoding.UTF8.GetString(buffer);
        }

        #endregion

        #region 获取列数据
        /// <summary>
        /// 获取列数据
        /// </summary>
        /// <param name="row"></param>
        /// <param name="columnname">列名</param>
        /// <returns></returns>
        public static string GetColumnValue(this DataRow row, string columnname)
        {
            var result = "";
            try
            {
                result = row[columnname].ToString();
            }
            catch (Exception)
            {
            }
            return result;
        }
        #endregion

        #region 时间格式
        public static string ToyyyyMMddHHmmss(this DateTime dt)
        {
            return dt.ToString("yyyyMMddHHmmss");
        }
        public static string ToyyyyMMddHHmmssDate(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string ToyyyyMMdd(this DateTime dt)
        {
            return dt.ToString("yyyyMMdd");
        }
        #endregion
    }
}
