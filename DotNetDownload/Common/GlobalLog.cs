using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownTest
{
    class GlobalLog
    {
        #region 私有方法

        public static void SaveLog(string msg)
        {
            var rootpath = Environment.CurrentDirectory + "\\log\\";
            var logfile = CommonFunc.GetFileName(rootpath, "log_{0}.txt");
            CommonFunc.WriteFile(logfile, msg);
        }

        #endregion
    }
}
