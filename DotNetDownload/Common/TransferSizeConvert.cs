using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownTest
{
    public class TransferSizeConvert
    { 
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public string GetSize(long b)
        {
            var result = "";

            double newsize = 0;

            double moresize = (double)b;

            //TB
            ComputeSize(moresize, 4, out newsize);
            if (newsize > 0)
            {
                result += string.Format("{0:F2}TB", newsize);
                return result;
            }

            //GB
            ComputeSize(moresize, 3, out newsize);
            if (newsize > 0)
            {
                result += string.Format("{0:F2}GB ", newsize);
                return result;
            }

            //MB
            ComputeSize(moresize, 2, out newsize);
            if (newsize > 0)
            {
                result += string.Format("{0:F2}MB ", newsize);
                return result;
            }

            //KB
            ComputeSize(moresize, 1, out newsize);
            if (newsize > 0)
            {
                result += string.Format("{0:F2}KB ", newsize);
                return result;
            }

            //B
            ComputeSize(moresize, 0, out newsize);
            if (newsize > 0)
            {
                result += string.Format("{0:F2}B ", newsize);
                return result;
            }
            return result;
        }


        /// <summary>
        /// 计算10000B，转换成对应单位的值
        /// </summary>
        /// <param name="size">总字节数量（B）</param>
        /// <param name="count">1024的幂数</param>
        /// <param name="newsize">总字节数/单位字节数</param>
        private void ComputeSize(double size, int count, out double newsize)
        {
            newsize = 0;

            var minsize = Math.Pow((double)1024, (double)count);
            if (size > minsize)
            {
                newsize = (double)(size / minsize);
            }
        }
    }
}
