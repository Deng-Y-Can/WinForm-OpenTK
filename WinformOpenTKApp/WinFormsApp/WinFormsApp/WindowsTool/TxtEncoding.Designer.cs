namespace WinFormsApp.WindowsTool
{
    partial class TxtEncoding
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            // 原有控件定义（仅保留TXT编码转换相关）
            this.btnSelectInputFolder = new System.Windows.Forms.Button();
            this.txtInputFolder = new System.Windows.Forms.TextBox();
            this.lblInputFolder = new System.Windows.Forms.Label();
            this.lblOutputFolder = new System.Windows.Forms.Label();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.btnSelectOutputFolder = new System.Windows.Forms.Button();
            this.lblEncoding = new System.Windows.Forms.Label();
            this.cboEncoding = new System.Windows.Forms.ComboBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();

            this.SuspendLayout();

            // 
            // 原有控件属性设置（TXT编码转换相关）
            // 
            // btnSelectInputFolder（选择源文件夹）
            this.btnSelectInputFolder.Location = new System.Drawing.Point(600, 30);
            this.btnSelectInputFolder.Name = "btnSelectInputFolder";
            this.btnSelectInputFolder.Size = new System.Drawing.Size(80, 25);
            this.btnSelectInputFolder.TabIndex = 0;
            this.btnSelectInputFolder.Text = "浏览...";
            this.btnSelectInputFolder.UseVisualStyleBackColor = true;
            this.btnSelectInputFolder.Click += new System.EventHandler(this.btnSelectInputFolder_Click);

            // txtInputFolder（源文件夹路径输入框）
            this.txtInputFolder.Location = new System.Drawing.Point(150, 32);
            this.txtInputFolder.Name = "txtInputFolder";
            this.txtInputFolder.Size = new System.Drawing.Size(440, 25);
            this.txtInputFolder.TabIndex = 1;

            // lblInputFolder（源文件夹标签）
            this.lblInputFolder.AutoSize = true;
            this.lblInputFolder.Location = new System.Drawing.Point(30, 35);
            this.lblInputFolder.Name = "lblInputFolder";
            this.lblInputFolder.Size = new System.Drawing.Size(89, 20);
            this.lblInputFolder.TabIndex = 2;
            this.lblInputFolder.Text = "源文件夹路径:";

            // lblOutputFolder（输出文件夹标签）
            this.lblOutputFolder.AutoSize = true;
            this.lblOutputFolder.Location = new System.Drawing.Point(30, 80);
            this.lblOutputFolder.Name = "lblOutputFolder";
            this.lblOutputFolder.Size = new System.Drawing.Size(89, 20);
            this.lblOutputFolder.TabIndex = 3;
            this.lblOutputFolder.Text = "输出文件夹:";

            // txtOutputFolder（输出文件夹路径输入框）
            this.txtOutputFolder.Location = new System.Drawing.Point(150, 78);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.Size = new System.Drawing.Size(440, 25);
            this.txtOutputFolder.TabIndex = 4;

            // btnSelectOutputFolder（选择输出文件夹）
            this.btnSelectOutputFolder.Location = new System.Drawing.Point(600, 78);
            this.btnSelectOutputFolder.Name = "btnSelectOutputFolder";
            this.btnSelectOutputFolder.Size = new System.Drawing.Size(80, 25);
            this.btnSelectOutputFolder.TabIndex = 5;
            this.btnSelectOutputFolder.Text = "浏览...";
            this.btnSelectOutputFolder.UseVisualStyleBackColor = true;
            this.btnSelectOutputFolder.Click += new System.EventHandler(this.btnSelectOutputFolder_Click);

            // lblEncoding（编码格式标签）
            this.lblEncoding.AutoSize = true;
            this.lblEncoding.Location = new System.Drawing.Point(30, 125);
            this.lblEncoding.Name = "lblEncoding";
            this.lblEncoding.Size = new System.Drawing.Size(89, 20);
            this.lblEncoding.TabIndex = 6;
            this.lblEncoding.Text = "目标编码格式:";

            // cboEncoding（编码格式下拉框）
            this.cboEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEncoding.FormattingEnabled = true;
            this.cboEncoding.Items.AddRange(new object[] {
            "UTF-8",
            "UTF-8 (带BOM)",
            "UTF-16 (Unicode)",
            "UTF-16 BE (Big Endian)",
            "GB2312",
            "GB18030",
            "ASCII"});
            this.cboEncoding.Location = new System.Drawing.Point(150, 122);
            this.cboEncoding.Name = "cboEncoding";
            this.cboEncoding.Size = new System.Drawing.Size(200, 28);
            this.cboEncoding.TabIndex = 7;

            // btnConvert（开始转换按钮）
            this.btnConvert.Location = new System.Drawing.Point(450, 120);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(90, 30);
            this.btnConvert.TabIndex = 8;
            this.btnConvert.Text = "开始转换";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);

            // btnExit（退出按钮）
            this.btnExit.Location = new System.Drawing.Point(570, 120);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(90, 30);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);

            // txtLog（日志文本框）
            this.txtLog.Location = new System.Drawing.Point(30, 180);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(730, 200);
            this.txtLog.TabIndex = 10;

            // lblStatus（状态标签）
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(30, 400);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(41, 20);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "就绪...";

            // progressBar（进度条）
            this.progressBar.Location = new System.Drawing.Point(30, 425);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(730, 25);
            this.progressBar.TabIndex = 12;

            // 
            // 窗体布局属性（按要求补充）
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 465);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);

            // 
            // 原有控件添加到窗体（确保显示）
            // 
            this.Controls.Add(this.btnSelectInputFolder);
            this.Controls.Add(this.txtInputFolder);
            this.Controls.Add(this.lblInputFolder);
            this.Controls.Add(this.lblOutputFolder);
            this.Controls.Add(this.txtOutputFolder);
            this.Controls.Add(this.btnSelectOutputFolder);
            this.Controls.Add(this.lblEncoding);
            this.Controls.Add(this.cboEncoding);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);

            // 
            // 窗体基本属性
            // 
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TxtEncoding";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TXT文件编码转换工具";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // 原有控件字段声明
        private System.Windows.Forms.Button btnSelectInputFolder;
        private System.Windows.Forms.TextBox txtInputFolder;
        private System.Windows.Forms.Label lblInputFolder;
        private System.Windows.Forms.Label lblOutputFolder;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Button btnSelectOutputFolder;
        private System.Windows.Forms.Label lblEncoding;
        private System.Windows.Forms.ComboBox cboEncoding;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}