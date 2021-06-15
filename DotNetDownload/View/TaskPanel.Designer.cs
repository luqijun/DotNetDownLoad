namespace DownTest
{
    partial class TaskPanel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.barstatus = new System.Windows.Forms.ProgressBar();
            this.btnstop = new System.Windows.Forms.Button();
            this.btnstart = new System.Windows.Forms.Button();
            this.btnrestart = new System.Windows.Forms.Button();
            this.btnopen = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txthttpurl = new System.Windows.Forms.RichTextBox();
            this.lblpercent = new System.Windows.Forms.Label();
            this.btnclose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "任务：";
            // 
            // barstatus
            // 
            this.barstatus.Location = new System.Drawing.Point(70, 6);
            this.barstatus.Name = "barstatus";
            this.barstatus.Size = new System.Drawing.Size(452, 32);
            this.barstatus.TabIndex = 1;
            // 
            // btnstop
            // 
            this.btnstop.Location = new System.Drawing.Point(75, 48);
            this.btnstop.Name = "btnstop";
            this.btnstop.Size = new System.Drawing.Size(60, 29);
            this.btnstop.TabIndex = 2;
            this.btnstop.Text = "暂停";
            this.btnstop.UseVisualStyleBackColor = true;
            // 
            // btnstart
            // 
            this.btnstart.Location = new System.Drawing.Point(14, 48);
            this.btnstart.Name = "btnstart";
            this.btnstart.Size = new System.Drawing.Size(60, 29);
            this.btnstart.TabIndex = 3;
            this.btnstart.Text = "开始";
            this.btnstart.UseVisualStyleBackColor = true;
            // 
            // btnrestart
            // 
            this.btnrestart.Location = new System.Drawing.Point(141, 48);
            this.btnrestart.Name = "btnrestart";
            this.btnrestart.Size = new System.Drawing.Size(77, 29);
            this.btnrestart.TabIndex = 4;
            this.btnrestart.Text = "重新开始";
            this.btnrestart.UseVisualStyleBackColor = true;
            // 
            // btnopen
            // 
            this.btnopen.Location = new System.Drawing.Point(224, 47);
            this.btnopen.Name = "btnopen";
            this.btnopen.Size = new System.Drawing.Size(86, 29);
            this.btnopen.TabIndex = 4;
            this.btnopen.Text = "打开文件";
            this.btnopen.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(335, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Http地址：";
            // 
            // txthttpurl
            // 
            this.txthttpurl.Location = new System.Drawing.Point(422, 44);
            this.txthttpurl.Name = "txthttpurl";
            this.txthttpurl.Size = new System.Drawing.Size(294, 29);
            this.txthttpurl.TabIndex = 6;
            this.txthttpurl.Text = "";
            // 
            // lblpercent
            // 
            this.lblpercent.AutoSize = true;
            this.lblpercent.Location = new System.Drawing.Point(594, 17);
            this.lblpercent.Name = "lblpercent";
            this.lblpercent.Size = new System.Drawing.Size(23, 12);
            this.lblpercent.TabIndex = 7;
            this.lblpercent.Text = "0/0";
            // 
            // btnclose
            // 
            this.btnclose.Location = new System.Drawing.Point(752, 44);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(60, 29);
            this.btnclose.TabIndex = 4;
            this.btnclose.Text = "关闭";
            this.btnclose.UseVisualStyleBackColor = true;
            // 
            // PerTaskPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblpercent);
            this.Controls.Add(this.txthttpurl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnopen);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.btnrestart);
            this.Controls.Add(this.btnstart);
            this.Controls.Add(this.btnstop);
            this.Controls.Add(this.barstatus);
            this.Controls.Add(this.label1);
            this.Name = "PerTaskPanel";
            this.Size = new System.Drawing.Size(831, 76);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar barstatus;
        private System.Windows.Forms.Button btnstop;
        private System.Windows.Forms.Button btnstart;
        private System.Windows.Forms.Button btnrestart;
        private System.Windows.Forms.Button btnopen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txthttpurl;
        private System.Windows.Forms.Label lblpercent;
        private System.Windows.Forms.Button btnclose;
    }
}
