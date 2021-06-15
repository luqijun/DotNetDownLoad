namespace DownTest
{
    partial class NewTaskForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnNew = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSelectRootPath = new System.Windows.Forms.Button();
            this.txtHttpUrl = new System.Windows.Forms.TextBox();
            this.txtSaveRootPath = new System.Windows.Forms.TextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(472, 21);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(99, 35);
            this.btnNew.TabIndex = 0;
            this.btnNew.Text = "新建任务";
            this.btnNew.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "HTTP地址：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "保存文件夹：";
            // 
            // btnSelectRootPath
            // 
            this.btnSelectRootPath.Location = new System.Drawing.Point(472, 62);
            this.btnSelectRootPath.Name = "btnSelectRootPath";
            this.btnSelectRootPath.Size = new System.Drawing.Size(20, 30);
            this.btnSelectRootPath.TabIndex = 5;
            this.btnSelectRootPath.UseVisualStyleBackColor = true;
            // 
            // txthttpurl
            // 
            this.txtHttpUrl.Location = new System.Drawing.Point(84, 28);
            this.txtHttpUrl.Name = "txthttpurl";
            this.txtHttpUrl.Size = new System.Drawing.Size(381, 21);
            this.txtHttpUrl.TabIndex = 6;
            // 
            // txtsaverootpath
            // 
            this.txtSaveRootPath.Location = new System.Drawing.Point(84, 66);
            this.txtSaveRootPath.Name = "txtsaverootpath";
            this.txtSaveRootPath.Size = new System.Drawing.Size(381, 21);
            this.txtSaveRootPath.TabIndex = 6;
            // 
            // btnopen
            // 
            this.btnOpen.Location = new System.Drawing.Point(498, 62);
            this.btnOpen.Name = "btnopen";
            this.btnOpen.Size = new System.Drawing.Size(78, 30);
            this.btnOpen.TabIndex = 7;
            this.btnOpen.Text = "打开文件夹";
            this.btnOpen.UseVisualStyleBackColor = true;
            // 
            // NewTaskForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 112);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.txtSaveRootPath);
            this.Controls.Add(this.txtHttpUrl);
            this.Controls.Add(this.btnSelectRootPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnNew);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewTaskForm";
            this.Text = "新建任务";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelectRootPath;
        private System.Windows.Forms.TextBox txtHttpUrl;
        private System.Windows.Forms.TextBox txtSaveRootPath;
        private System.Windows.Forms.Button btnOpen;
    }
}