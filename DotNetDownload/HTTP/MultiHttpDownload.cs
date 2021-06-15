using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Net;

namespace DownTest
{
    /// <summary>
    /// 下载文件类
    /// </summary>
    public class MultiHttpDownload
    {
        #region 变量

        private int _threadNum;    //线程数量
        private long _fileSize;    //文件大小
        private string _fileUrl;   //文件地址
        private string _fileName;   //文件名
        private string _saverootpath = "";
        private string _savePath;   //保存路径
        private short _threadCompleteNum; //线程完成数量
        private bool _isComplete;   //是否完成
        private volatile int _downloadSize; //当前下载大小(实时的)
        private Thread[] _thread;   //线程数组
        private List<string> _tempFiles = new List<string>();
        private object locker = new object();

        #endregion

        #region 事件
        public delegate void ReadHandler(DownStatus status, long size, long currentsize, Exception e);
        public event ReadHandler ReadingEvent;

        #endregion

        #region 属性
        /// <summary>
        /// 下载状态
        /// </summary>
        public DownStatus DownStatus { get; set; }
        /// <summary>
        /// 保存路径
        /// </summary>
        public string SavePath { get { return _savePath; } }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="threahNum"></param>
        /// <param name="fileUrl"></param>
        /// <param name="saverootpath"></param>

        public MultiHttpDownload(int threahNum, string fileUrl, string saverootpath)
        {
            var savePath = saverootpath + "\\" + System.IO.Path.GetFileName(fileUrl);

            this._saverootpath = saverootpath;
            this._threadNum = threahNum;
            this._thread = new Thread[threahNum];
            this._fileUrl = fileUrl;
            this._savePath = savePath;
            this.DownStatus = DownStatus.None;
        }

        #endregion

        #region 检查是否有效
        /// <summary>
        /// 检查是否有效
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool IsValid(out string msg)
        {
            msg = "";
            var issuccess = true;
            try
            {
                if (string.IsNullOrEmpty(this._fileUrl))
                {
                    throw new Exception("请输入url地址！");
                }
                if (string.IsNullOrEmpty(this._savePath))
                {
                    throw new Exception("请输入保存地址！");
                }
                if (File.Exists(this._savePath))
                {
                    throw new Exception(string.Format("保存文件[{0}]已存在！", this._savePath));
                }
                if (Directory.Exists(this._saverootpath) == false)
                {
                    Directory.CreateDirectory(this._saverootpath);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                issuccess = false;
            }
            return issuccess;
        }
        #endregion

        #region 开始任务

        /// <summary>
        /// 开始任务
        /// </summary>
        public void Start()
        {
            try
            {
                var msg = "";
                var isvalid = IsValid(out msg);
                if (isvalid == false)
                {
                    throw new Exception(msg);
                }

                //初始化全局变量
                _threadCompleteNum = 0;
                _tempFiles = new List<string>();


                this.DownStatus = DownStatus.Downing;
                var readthread = new Thread(new ParameterizedThreadStart(ReadingThread));
                readthread.Start();

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_fileUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                _fileSize = response.ContentLength;
                Console.WriteLine("_fileSize =" + _fileSize);
                int singelNum = (int)(_fileSize / _threadNum);  //平均分配
                int remainder = (int)(_fileSize % _threadNum);  //获取剩余的
                request.Abort();
                response.Close();
                for (int i = 0; i < _threadNum; i++)
                {
                    var begin = singelNum * i;
                    var end = singelNum * (i + 1);

                    //最后一个进程，需要将剩余的也下载
                    if ((i + 1) == _threadNum)
                    {
                        end += remainder - 1;
                    }
                    //下载指定位置的数据
                    int[] ran = new int[] { begin, end };
                    _thread[i] = new Thread(new ParameterizedThreadStart(Download));
                    _thread[i].Name = System.IO.Path.GetFileNameWithoutExtension(_fileUrl) + "_{0}".Replace("{0}", Convert.ToString(i + 1));
                    _thread[i].Start(ran);
                }
            }
            catch (Exception e)
            {
                this.DownStatus = DownStatus.Stop;
            }
        }

        /// <summary>
        /// 检查下载进度进程
        /// </summary>
        /// <param name="obj"></param>
        private void ReadingThread(object obj)
        {
            try
            {
                while (true)
                {
                    try
                    {
                        long currentsize = 0;
                        foreach (var file in _tempFiles)
                        {
                            if (File.Exists(file))
                            {
                                FileInfo fileInfo = new FileInfo(file);
                                currentsize += fileInfo.Length;
                            }
                        }
                        //读取事件
                        if (ReadingEvent != null)
                        {
                            ReadingEvent(this.DownStatus, _fileSize, currentsize, null);
                        }
                        //结束进度进程
                        if (this.DownStatus != DownStatus.Downing)
                        {
                            break;
                        }

                        Thread.Sleep(1000);

                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 下载文件进程
        /// </summary>
        /// <param name="obj"></param>
        private void Download(object obj)
        {
            try
            {
                Stream httpFileStream = null, localFileStram = null;
                try
                {
                    int[] ran = obj as int[];

                    var begin = ran[0];
                    var end = ran[1];

                    string tmpFileBlock = GetTmpPath() + Thread.CurrentThread.Name + ".tmp";
                    lock (locker)
                    {
                        //添加临时文件列表
                        if (_tempFiles.Contains(tmpFileBlock) == false)
                        {
                            _tempFiles.Add(tmpFileBlock);
                        }
                    }

                    var seek = 0;
                    var isneeddown = true;

                    Console.WriteLine("tmpFileBlock=" + tmpFileBlock + "begin=" + begin + ",end=" + end);

                    //如果文件已存在，则获取已下载长度，继续下载
                    if (File.Exists(tmpFileBlock))
                    {
                        FileInfo fileInfo = new FileInfo(tmpFileBlock);
                        var existlength = (int)fileInfo.Length;

                        var needsize = end - begin + 1;

                        Console.WriteLine("existlength=" + existlength + ",needsize=" + needsize + ",end=" + end + ",begin=" + begin);

                        if (existlength > needsize)
                        {
                            //文件长度超过需要下载的长度，表示文件不是该任务创建的，需要删除，重新下载                             
                            File.Delete(tmpFileBlock);

                            seek = 0;
                            isneeddown = true;
                        }
                        else if (existlength == needsize)
                        {
                            //文件下载已完成
                            isneeddown = false;
                        }
                        else
                        {
                            //文件尚未下载完成，指定下载位置

                            begin = (existlength - 1);//已下载的长度-1（位置从0开始）
                            seek = existlength;//已下载的长度
                            isneeddown = true;

                        }
                    }
                    Console.WriteLine("isneeddown=" + isneeddown + "begin=" + begin + ",end=" + end + ",seek=" + seek);

                    //判断是否需要下载数据
                    if (isneeddown)
                    {
                        HttpWebRequest httprequest = (HttpWebRequest)WebRequest.Create(_fileUrl);
                        Console.WriteLine("begin =" + begin + ",end=" + end);
                        Console.WriteLine("seek=" + seek);
                        httprequest.AddRange(begin, end);
                        HttpWebResponse httpresponse = (HttpWebResponse)httprequest.GetResponse();
                        httpFileStream = httpresponse.GetResponseStream();

                        //如果不存在，则新建
                        localFileStram = new FileStream(tmpFileBlock, FileMode.OpenOrCreate);
                        //指定写入位置
                        localFileStram.Seek(seek, SeekOrigin.Begin);
                        byte[] by = new byte[5000];
                        int getByteSize = httpFileStream.Read(by, 0, (int)by.Length); //Read方法将返回读入by变量中的总字节数

                        while (getByteSize > 0)
                        {
                            if (this.DownStatus == DownStatus.Stop)
                            {
                                throw new Exception("任务已停止！");
                            }
                            Thread.Sleep(20);
                            lock (locker) _downloadSize += getByteSize;
                            localFileStram.Write(by, 0, getByteSize);
                            getByteSize = httpFileStream.Read(by, 0, (int)by.Length);
                        }
                    }
                    lock (locker) _threadCompleteNum++;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message.ToString());
                }
                finally
                {
                    if (httpFileStream != null) httpFileStream.Dispose();
                    if (localFileStram != null) localFileStram.Dispose();
                }
                if (_threadCompleteNum == _threadNum)
                {
                    Complete();
                    _isComplete = true;
                    this.DownStatus = DownStatus.Complete;
                }
            }
            catch (Exception e)
            {
                this.DownStatus = DownStatus.Stop;
            }
        }

        /// <summary>
        /// 下载完成后合并文件块
        /// </summary>
        private void Complete()
        {
            Stream mergeFile = new FileStream(@_savePath, FileMode.Create);
            BinaryWriter addWriter = new BinaryWriter(mergeFile);
            //按序号排序
            _tempFiles.Sort();
            int i = 0;
            foreach (string file in _tempFiles)
            {
                i++;
                using (FileStream fs = new FileStream(file, FileMode.Open))
                {
                    BinaryReader TempReader = new BinaryReader(fs);

                    //由于一个文件拆分成多个文件时，每个文件最后都会拼接上结尾符"\0"，导致总长度多出(n-1)个字符，需要需要针对前面(n-1)个文件去除最后的"\0"。
                    if (i == _tempFiles.Count)
                    {
                        addWriter.Write(TempReader.ReadBytes((int)fs.Length));
                    }
                    else
                    {
                        addWriter.Write(TempReader.ReadBytes((int)fs.Length - 1));
                    }
                    TempReader.Close();
                }
            }
            addWriter.Close();
            //删除临时文件夹
            foreach (string file in _tempFiles)
            {
                //File.Delete(file);
            }
        }
        #endregion

        #region 删除临时文件

        /// <summary>
        /// 删除临时文件
        /// </summary>
        public void DeleteTmpFile()
        {
            this.DownStatus = DownStatus.Deleted;
            foreach (string file in _tempFiles)
            {
                File.Delete(file);
            }
        }

        #endregion

        #region 暂停任务

        /// <summary>
        /// 暂停
        /// </summary>
        public void Stop()
        {
            this.DownStatus = DownStatus.Stop;
        }

        #endregion

        #region 私有方法 

        /// <summary>
        /// 获取临时文件夹
        /// </summary>
        /// <returns></returns>
        private string GetTmpPath()
        {
            return System.IO.Path.GetTempPath();
        }

        #endregion
    }

    public enum DownStatus
    {
        None = 0,
        Downing = 1,
        Stop = 2,
        Complete = 3,
        Deleted = 4

    }
}