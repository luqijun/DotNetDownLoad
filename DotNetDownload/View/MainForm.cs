using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownTest
{
    public partial class MainForm : Form
    {
        private List<string> alltask = new List<string>();

        public MainForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += Form1_Load;
            this.FormClosed += Form1_FormClosed;
            this.btnNew.Click += BtnNew_Click;
            this.btncleanlog.Click += Btncleanlog_Click;
            this.btngettmppath.Click += Btngettmppath_Click;
        }

        #region 控件事件

        /// <summary>
        /// 加载完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// 新建任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNew_Click(object sender, EventArgs e)
        {
            var start = new NewTaskForm();
            start.NewTaskEvent += Start_newTask;
            start.ShowDialog();
        }

        /// <summary>
        /// 开始任务
        /// </summary>
        /// <param name="httpurl"></param>
        /// <param name="saverootpath"></param>
        private void Start_newTask(string httpurl, string saverootpath)
        {
            if (alltask.Contains(httpurl))
            {
                WriteLog(string.Format("任务：{0}已存在！", httpurl));
                return;
            }
            alltask.Add(httpurl);

            var usercontrol = new TaskPanel();
            usercontrol.DeleteTaskEvent += Usercontrol_deleteTask;
            usercontrol.LogTaskEvent += Usercontrol_logTask;
            usercontrol.Anchor = AnchorStyles.Top & AnchorStyles.Left & AnchorStyles.Right;
            this.flowLayoutPanel1.Controls.Add(usercontrol);
            usercontrol.NewTask(httpurl, saverootpath);
        }

        /// <summary>
        /// 清理日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btncleanlog_Click(object sender, EventArgs e)
        {
            this.txtresult.Clear();
        }

        /// <summary>
        /// 获取临时文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btngettmppath_Click(object sender, EventArgs e)
        {
            var tmppath = System.IO.Path.GetTempPath();
            WriteLog(tmppath);
        }
        #endregion

        #region 回调事件
        private void Usercontrol_logTask(string msg)
        {
            WriteLog(msg);
        }

        private void Usercontrol_deleteTask(string httpurl, string saverootpath, string msg)
        {
            alltask.Remove(httpurl);
            if (string.IsNullOrEmpty(msg) == false)
            {
                WriteLog(msg);
            }
        }
        #endregion

        #region 私有方法

        private void WriteLog(string msg)
        {
            this.txtresult.AppendText(msg + Environment.NewLine);
            this.txtresult.Select(this.txtresult.TextLength, 0);
            this.txtresult.ScrollToCaret();
            //保存日志
            SaveLog(msg);
        }

        private void SaveLog(string msg)
        {
            var rootpath = Environment.CurrentDirectory + "\\log\\";
            var logfile = CommonFunc.getFileName(rootpath, "log_{0}.txt");
            CommonFunc.WriteFile(logfile, msg);
        }

        #endregion
    }
}
