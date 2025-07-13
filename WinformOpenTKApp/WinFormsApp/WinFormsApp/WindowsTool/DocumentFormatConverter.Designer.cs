namespace WinFormsApp.WindowsTool
{
    partial class DocumentFormatConverter
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
            this.lblSourceFolder = new System.Windows.Forms.Label();
            this.txtSourceFolder = new System.Windows.Forms.TextBox();
            this.btnSourceFolder = new System.Windows.Forms.Button();
            this.lblOutputFolder = new System.Windows.Forms.Label();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.btnOutputFolder = new System.Windows.Forms.Button();
            this.lblConversionOptions = new System.Windows.Forms.Label();
            this.chkConvertToPdf = new System.Windows.Forms.CheckBox();
            this.chkConvertToTxt = new System.Windows.Forms.CheckBox();
            this.chkIncludeSubfolders = new System.Windows.Forms.CheckBox();
            this.btnStartConversion = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();

            // 
            // 控件属性设置（与之前相同）
            // 
            this.lblSourceFolder.AutoSize = true;
            this.lblSourceFolder.Location = new System.Drawing.Point(12, 22);
            this.lblSourceFolder.Name = "lblSourceFolder";
            this.lblSourceFolder.Size = new System.Drawing.Size(77, 15);
            this.lblSourceFolder.TabIndex = 0;
            this.lblSourceFolder.Text = "源文件夹:";

            this.txtSourceFolder.Location = new System.Drawing.Point(95, 19);
            this.txtSourceFolder.Name = "txtSourceFolder";
            this.txtSourceFolder.Size = new System.Drawing.Size(448, 25);
            this.txtSourceFolder.TabIndex = 1;

            this.btnSourceFolder.Location = new System.Drawing.Point(549, 17);
            this.btnSourceFolder.Name = "btnSourceFolder";
            this.btnSourceFolder.Size = new System.Drawing.Size(38, 28);
            this.btnSourceFolder.TabIndex = 2;
            this.btnSourceFolder.Text = "...";
            this.btnSourceFolder.UseVisualStyleBackColor = true;
            this.btnSourceFolder.Click += new System.EventHandler(this.btnSourceFolder_Click);

            this.lblOutputFolder.AutoSize = true;
            this.lblOutputFolder.Location = new System.Drawing.Point(12, 58);
            this.lblOutputFolder.Name = "lblOutputFolder";
            this.lblOutputFolder.Size = new System.Drawing.Size(89, 15);
            this.lblOutputFolder.TabIndex = 3;
            this.lblOutputFolder.Text = "输出文件夹:";

            this.txtOutputFolder.Location = new System.Drawing.Point(95, 55);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.Size = new System.Drawing.Size(448, 25);
            this.txtOutputFolder.TabIndex = 4;

            this.btnOutputFolder.Location = new System.Drawing.Point(549, 53);
            this.btnOutputFolder.Name = "btnOutputFolder";
            this.btnOutputFolder.Size = new System.Drawing.Size(38, 28);
            this.btnOutputFolder.TabIndex = 5;
            this.btnOutputFolder.Text = "...";
            this.btnOutputFolder.UseVisualStyleBackColor = true;
            this.btnOutputFolder.Click += new System.EventHandler(this.btnOutputFolder_Click);

            this.lblConversionOptions.AutoSize = true;
            this.lblConversionOptions.Location = new System.Drawing.Point(12, 99);
            this.lblConversionOptions.Name = "lblConversionOptions";
            this.lblConversionOptions.Size = new System.Drawing.Size(95, 15);
            this.lblConversionOptions.TabIndex = 6;
            this.lblConversionOptions.Text = "转换选项:";

            this.chkConvertToPdf.AutoSize = true;
            this.chkConvertToPdf.Checked = true;
            this.chkConvertToPdf.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkConvertToPdf.Location = new System.Drawing.Point(113, 98);
            this.chkConvertToPdf.Name = "chkConvertToPdf";
            this.chkConvertToPdf.Size = new System.Drawing.Size(113, 19);
            this.chkConvertToPdf.TabIndex = 7;
            this.chkConvertToPdf.Text = "转换为PDF";
            this.chkConvertToPdf.UseVisualStyleBackColor = true;

            this.chkConvertToTxt.AutoSize = true;
            this.chkConvertToTxt.Location = new System.Drawing.Point(248, 98);
            this.chkConvertToTxt.Name = "chkConvertToTxt";
            this.chkConvertToTxt.Size = new System.Drawing.Size(113, 19);
            this.chkConvertToTxt.TabIndex = 8;
            this.chkConvertToTxt.Text = "转换为TXT";
            this.chkConvertToTxt.UseVisualStyleBackColor = true;

            this.chkIncludeSubfolders.AutoSize = true;
            this.chkIncludeSubfolders.Checked = true;
            this.chkIncludeSubfolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeSubfolders.Location = new System.Drawing.Point(383, 98);
            this.chkIncludeSubfolders.Name = "chkIncludeSubfolders";
            this.chkIncludeSubfolders.Size = new System.Drawing.Size(127, 19);
            this.chkIncludeSubfolders.TabIndex = 9;
            this.chkIncludeSubfolders.Text = "包含子文件夹";
            this.chkIncludeSubfolders.UseVisualStyleBackColor = true;

            this.btnStartConversion.Location = new System.Drawing.Point(15, 135);
            this.btnStartConversion.Name = "btnStartConversion";
            this.btnStartConversion.Size = new System.Drawing.Size(120, 35);
            this.btnStartConversion.TabIndex = 10;
            this.btnStartConversion.Text = "开始转换";
            this.btnStartConversion.UseVisualStyleBackColor = true;
            this.btnStartConversion.Click += new System.EventHandler(this.btnStartConversion_Click);

            this.btnCancel.Enabled = false;
            this.btnCancel.Location = new System.Drawing.Point(141, 135);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 35);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.progressBar.Location = new System.Drawing.Point(15, 185);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(572, 23);
            this.progressBar.TabIndex = 12;

            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(12, 167);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(65, 15);
            this.lblProgress.TabIndex = 13;
            this.lblProgress.Text = "准备中...";

            this.txtLog.Location = new System.Drawing.Point(15, 214);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(572, 199);
            this.txtLog.TabIndex = 14;

            // 
            // 关键：将所有控件添加到窗体的 Controls 集合中
            // 
            this.Controls.Add(this.lblSourceFolder);
            this.Controls.Add(this.txtSourceFolder);
            this.Controls.Add(this.btnSourceFolder);
            this.Controls.Add(this.lblOutputFolder);
            this.Controls.Add(this.txtOutputFolder);
            this.Controls.Add(this.btnOutputFolder);
            this.Controls.Add(this.lblConversionOptions);
            this.Controls.Add(this.chkConvertToPdf);
            this.Controls.Add(this.chkConvertToTxt);
            this.Controls.Add(this.chkIncludeSubfolders);
            this.Controls.Add(this.btnStartConversion);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.txtLog);

            // 
            // 窗体属性设置
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 425);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DocumentFormatConverter";
            this.Text = "文档格式转换工具";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblSourceFolder;
        private System.Windows.Forms.TextBox txtSourceFolder;
        private System.Windows.Forms.Button btnSourceFolder;
        private System.Windows.Forms.Label lblOutputFolder;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Button btnOutputFolder;
        private System.Windows.Forms.Label lblConversionOptions;
        private System.Windows.Forms.CheckBox chkConvertToPdf;
        private System.Windows.Forms.CheckBox chkConvertToTxt;
        private System.Windows.Forms.CheckBox chkIncludeSubfolders;
        private System.Windows.Forms.Button btnStartConversion;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.TextBox txtLog;
    }
}