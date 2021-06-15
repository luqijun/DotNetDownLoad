using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownTest
{
    public partial class NewTaskForm : Form
    {
        #region 事件
        public delegate void NewTaskHandler(string httpurl, string saverootpath);
        public event NewTaskHandler NewTaskEvent;
        #endregion

        public NewTaskForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            this.Load += NewTaskForm_Load;
            this.btnNew.Click += BtnNew_Click;
            this.btnSelectRootPath.Click += BtnSelectRootPath_Click;
            //this.btnTest.Click += BtnTest_Click;
            this.btnOpen.Click += BtnOpen_Click;
            this.KeyPreview = true;//键盘事件传递
            this.KeyDown += NewTaskForm_KeyDown;
            this.Focus();
            this.txtHttpUrl.Focus();
        }

        #region 事件
        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewTaskForm_Load(object sender, EventArgs e)
        {
            //文件夹地址
            var path = GetRootPath().Trim();
            this.txtSaveRootPath.Text = path;
            //测试地址
            this.txtHttpUrl.Text = "https://down.qq.com/qqweb/PCQQ/PCQQ_EXE/PCQQ2021.exe";
        }

        /// <summary>
        /// 回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewTaskForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnNew_Click(null, null);
                return;
            }
        }

        /// <summary>
        /// 设置测试下载地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTest_Click(object sender, EventArgs e)
        {
            this.txtHttpUrl.Text = "https://down.qq.com/qqweb/PCQQ/PCQQ_EXE/PCQQ2021.exe";
        }

        /// <summary>
        /// 选择保存路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelectRootPath_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择所在文件夹";
            //默认文件夹
            var currentsaverootpath = this.txtSaveRootPath.Text;
            if (Directory.Exists(currentsaverootpath))
            {
                dialog.SelectedPath = currentsaverootpath;
            }
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    return;
                }
                this.txtSaveRootPath.Text = dialog.SelectedPath.Trim();
                //保存
                var path = this.txtSaveRootPath.Text.Trim();
                SetRootPath(path);
            }
        }

        /// <summary>
        /// 新建任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNew_Click(object sender, EventArgs e)
        {
            var httpurl = this.txtHttpUrl.Text.Trim();
            var saverootpath = this.txtSaveRootPath.Text.Trim();

            var down = new MultiHttpDownload(0, httpurl, saverootpath);
            var msg = "";
            var isvalid = down.IsValid(out msg);
            if (isvalid == false)
            {
                MessageBox.Show(msg);
                this.txtHttpUrl.Focus();
                return;
            }
            if (NewTaskEvent != null)
            {
                NewTaskEvent(httpurl, saverootpath);
            }
            this.Close();
        }

        /// <summary>
        /// 打开文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOpen_Click(object sender, EventArgs e)
        {
            var httpurl = this.txtHttpUrl.Text.Trim();
            var saverootpath = this.txtSaveRootPath.Text.Trim();

            var down = new MultiHttpDownload(0, httpurl, saverootpath);

            var savepath = down.SavePath;
            //文件不存在时，打开文件夹
            if (File.Exists(savepath) == false)
            {
                savepath = saverootpath;
            }
            CommonFunc.OpenFileDirectory(savepath);
        }
        #endregion

        #region 私有方法

        /// <summary>
        /// 存储保存文件路径
        /// </summary>
        /// <param name="path"></param>
        private void SetRootPath(string path)
        {
            var savefile = System.Configuration.ConfigurationManager.AppSettings["SavePath"];

            CommonFunc.WriteFile(savefile, path, false);
        }

        /// <summary>
        /// 读取保存文件路径
        /// </summary>
        /// <returns></returns>
        private string GetRootPath()
        {
            var saveFile = System.Configuration.ConfigurationManager.AppSettings["SavePath"];

            var path = saveFile;
            if (string.IsNullOrEmpty(path))
            {
                path = @"C:\Users\Administrator\Desktop";
            }
            return path;
        }
        #endregion

    }
}
