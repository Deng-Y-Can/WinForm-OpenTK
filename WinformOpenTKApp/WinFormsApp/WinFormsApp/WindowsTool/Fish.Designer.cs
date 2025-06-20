namespace WinFormsApp.WindowsTool
{
    partial class Fish
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiShowSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblLinesPerPage = new System.Windows.Forms.Label();
            this.nudLinesPerPage = new System.Windows.Forms.NumericUpDown();
            this.lblCharsPerLine = new System.Windows.Forms.Label();
            this.nudCharsPerLine = new System.Windows.Forms.NumericUpDown();
            this.lblPageInterval = new System.Windows.Forms.Label();
            this.nudPageInterval = new System.Windows.Forms.NumericUpDown();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnSetIcon = new System.Windows.Forms.Button();
            this.lblCurrentIcon = new System.Windows.Forms.Label();
            this.gbDisplayMode = new System.Windows.Forms.GroupBox();
            this.rbTaskbarText = new System.Windows.Forms.RadioButton();
            this.rbBalloonTip = new System.Windows.Forms.RadioButton();
            this.taskbarTextForm = new System.Windows.Forms.Form();
            this.gbPosition = new System.Windows.Forms.GroupBox();
            this.rbRight = new System.Windows.Forms.RadioButton();
            this.rbLeft = new System.Windows.Forms.RadioButton();
            this.gbTaskbarStyle = new System.Windows.Forms.GroupBox();
            this.chkLockTaskbarText = new System.Windows.Forms.CheckBox();
            this.nudTaskbarFontSize = new System.Windows.Forms.NumericUpDown();
            this.lblTaskbarFontSize = new System.Windows.Forms.Label();
            this.txtTaskbarFont = new System.Windows.Forms.TextBox();
            this.lblTaskbarFont = new System.Windows.Forms.Label();
            this.lblTaskbarBackColor = new System.Windows.Forms.Label();
            this.lblTaskbarTextColor = new System.Windows.Forms.Label();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLinesPerPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCharsPerLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPageInterval)).BeginInit();
            this.gbDisplayMode.SuspendLayout();
            this.gbPosition.SuspendLayout();
            this.gbTaskbarStyle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTaskbarFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Text = "TXT 通知工具";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiShowSettings,
            this.tsmiExit});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(153, 70);
            // 
            // tsmiShowSettings
            // 
            this.tsmiShowSettings.Name = "tsmiShowSettings";
            this.tsmiShowSettings.Size = new System.Drawing.Size(152, 22);
            this.tsmiShowSettings.Text = "设置";
            this.tsmiShowSettings.Click += new System.EventHandler(this.tsmiShowSettings_Click);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(152, 22);
            this.tsmiExit.Text = "退出";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "txt";
            this.openFileDialog.Filter = "文本文件 (*.txt)|*.txt|所有文件 (*.*)|*.*";
            this.openFileDialog.Title = "选择 TXT 文件";
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(12, 12);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(260, 21);
            this.txtFilePath.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(278, 10);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "浏览...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // lblLinesPerPage
            // 
            this.lblLinesPerPage.AutoSize = true;
            this.lblLinesPerPage.Location = new System.Drawing.Point(12, 48);
            this.lblLinesPerPage.Name = "lblLinesPerPage";
            this.lblLinesPerPage.Size = new System.Drawing.Size(65, 12);
            this.lblLinesPerPage.TabIndex = 2;
            this.lblLinesPerPage.Text = "每页行数:";
            // 
            // nudLinesPerPage
            // 
            this.nudLinesPerPage.Location = new System.Drawing.Point(83, 46);
            this.nudLinesPerPage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLinesPerPage.Name = "nudLinesPerPage";
            this.nudLinesPerPage.Size = new System.Drawing.Size(60, 21);
            this.nudLinesPerPage.TabIndex = 3;
            this.nudLinesPerPage.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // lblCharsPerLine
            // 
            this.lblCharsPerLine.AutoSize = true;
            this.lblCharsPerLine.Location = new System.Drawing.Point(159, 48);
            this.lblCharsPerLine.Name = "lblCharsPerLine";
            this.lblCharsPerLine.Size = new System.Drawing.Size(65, 12);
            this.lblCharsPerLine.TabIndex = 4;
            this.lblCharsPerLine.Text = "每行字符:";
            // 
            // nudCharsPerLine
            // 
            this.nudCharsPerLine.Location = new System.Drawing.Point(230, 46);
            this.nudCharsPerLine.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCharsPerLine.Name = "nudCharsPerLine";
            this.nudCharsPerLine.Size = new System.Drawing.Size(60, 21);
            this.nudCharsPerLine.TabIndex = 5;
            this.nudCharsPerLine.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // lblPageInterval
            // 
            this.lblPageInterval.AutoSize = true;
            this.lblPageInterval.Location = new System.Drawing.Point(12, 84);
            this.lblPageInterval.Name = "lblPageInterval";
            this.lblPageInterval.Size = new System.Drawing.Size(77, 12);
            this.lblPageInterval.TabIndex = 6;
            this.lblPageInterval.Text = "切换间隔(ms):";
            // 
            // nudPageInterval
            // 
            this.nudPageInterval.Location = new System.Drawing.Point(95, 82);
            this.nudPageInterval.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.nudPageInterval.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudPageInterval.Name = "nudPageInterval";
            this.nudPageInterval.Size = new System.Drawing.Size(80, 21);
            this.nudPageInterval.TabIndex = 7;
            this.nudPageInterval.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(14, 287);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 8;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(95, 287);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(176, 292);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(65, 12);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "状态: 已停止";
            // 
            // btnSetIcon
            // 
            this.btnSetIcon.Location = new System.Drawing.Point(14, 256);
            this.btnSetIcon.Name = "btnSetIcon";
            this.btnSetIcon.Size = new System.Drawing.Size(110, 23);
            this.btnSetIcon.TabIndex = 11;
            this.btnSetIcon.Text = "设置托盘图标";
            this.btnSetIcon.UseVisualStyleBackColor = true;
            this.btnSetIcon.Click += new System.EventHandler(this.btnSetIcon_Click);
            // 
            // lblCurrentIcon
            // 
            this.lblCurrentIcon.AutoSize = true;
            this.lblCurrentIcon.Location = new System.Drawing.Point(130, 261);
            this.lblCurrentIcon.Name = "lblCurrentIcon";
            this.lblCurrentIcon.Size = new System.Drawing.Size(89, 12);
            this.lblCurrentIcon.TabIndex = 12;
            this.lblCurrentIcon.Text = "当前图标: 默认";
            // 
            // gbDisplayMode
            // 
            this.gbDisplayMode.Controls.Add(this.rbTaskbarText);
            this.gbDisplayMode.Controls.Add(this.rbBalloonTip);
            this.gbDisplayMode.Location = new System.Drawing.Point(14, 113);
            this.gbDisplayMode.Name = "gbDisplayMode";
            this.gbDisplayMode.Size = new System.Drawing.Size(276, 34);
            this.gbDisplayMode.TabIndex = 13;
            this.gbDisplayMode.TabStop = false;
            this.gbDisplayMode.Text = "显示模式";
            // 
            // rbTaskbarText
            // 
            this.rbTaskbarText.AutoSize = true;
            this.rbTaskbarText.Location = new System.Drawing.Point(147, 14);
            this.rbTaskbarText.Name = "rbTaskbarText";
            this.rbTaskbarText.Size = new System.Drawing.Size(113, 16);
            this.rbTaskbarText.TabIndex = 1;
            this.rbTaskbarText.Text = "任务栏文本模式";
            this.rbTaskbarText.UseVisualStyleBackColor = true;
            this.rbTaskbarText.CheckedChanged += new System.EventHandler(this.rbDisplayMode_CheckedChanged);
            // 
            // rbBalloonTip
            // 
            this.rbBalloonTip.AutoSize = true;
            this.rbBalloonTip.Checked = true;
            this.rbBalloonTip.Location = new System.Drawing.Point(6, 14);
            this.rbBalloonTip.Name = "rbBalloonTip";
            this.rbBalloonTip.Size = new System.Drawing.Size(113, 16);
            this.rbBalloonTip.TabIndex = 0;
            this.rbBalloonTip.TabStop = true;
            this.rbBalloonTip.Text = "通知气泡模式";
            this.rbBalloonTip.UseVisualStyleBackColor = true;
            this.rbBalloonTip.CheckedChanged += new System.EventHandler(this.rbDisplayMode_CheckedChanged);
            // 
            // taskbarTextForm
            // 
            this.taskbarTextForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.taskbarTextForm.Name = "taskbarTextForm";
            this.taskbarTextForm.Opacity = 0.9D;
            this.taskbarTextForm.ShowInTaskbar = false;
            this.taskbarTextForm.TopMost = true;
            this.taskbarTextForm.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TaskbarTextForm_MouseDown);
            this.taskbarTextForm.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TaskbarTextForm_MouseMove);
            this.taskbarTextForm.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TaskbarTextForm_MouseUp);
            // 
            // gbPosition
            // 
            this.gbPosition.Controls.Add(this.rbRight);
            this.gbPosition.Controls.Add(this.rbLeft);
            this.gbPosition.Location = new System.Drawing.Point(14, 143);
            this.gbPosition.Name = "gbPosition";
            this.gbPosition.Size = new System.Drawing.Size(276, 33);
            this.gbPosition.TabIndex = 14;
            this.gbPosition.TabStop = false;
            this.gbPosition.Text = "显示位置";
            // 
            // rbRight
            // 
            this.rbRight.AutoSize = true;
            this.rbRight.Location = new System.Drawing.Point(147, 14);
            this.rbRight.Name = "rbRight";
            this.rbRight.Size = new System.Drawing.Size(59, 16);
            this.rbRight.TabIndex = 1;
            this.rbRight.Text = "右侧";
            this.rbRight.UseVisualStyleBackColor = true;
            this.rbRight.CheckedChanged += new System.EventHandler(this.rbPosition_CheckedChanged);
            // 
            // rbLeft
            // 
            this.rbLeft.AutoSize = true;
            this.rbLeft.Checked = true;
            this.rbLeft.Location = new System.Drawing.Point(6, 14);
            this.rbLeft.Name = "rbLeft";
            this.rbLeft.Size = new System.Drawing.Size(59, 16);
            this.rbLeft.TabIndex = 0;
            this.rbLeft.TabStop = true;
            this.rbLeft.Text = "左侧";
            this.rbLeft.UseVisualStyleBackColor = true;
            this.rbLeft.CheckedChanged += new System.EventHandler(this.rbPosition_CheckedChanged);
            // 
            // gbTaskbarStyle
            // 
            this.gbTaskbarStyle.Controls.Add(this.chkLockTaskbarText);
            this.gbTaskbarStyle.Controls.Add(this.nudTaskbarFontSize);
            this.gbTaskbarStyle.Controls.Add(this.lblTaskbarFontSize);
            this.gbTaskbarStyle.Controls.Add(this.txtTaskbarFont);
            this.gbTaskbarStyle.Controls.Add(this.lblTaskbarFont);
            this.gbTaskbarStyle.Controls.Add(this.lblTaskbarBackColor);
            this.gbTaskbarStyle.Controls.Add(this.lblTaskbarTextColor);
            this.gbTaskbarStyle.Location = new System.Drawing.Point(14, 182);
            this.gbTaskbarStyle.Name = "gbTaskbarStyle";
            this.gbTaskbarStyle.Size = new System.Drawing.Size(276, 74);
            this.gbTaskbarStyle.TabIndex = 15;
            this.gbTaskbarStyle.TabStop = false;
            this.gbTaskbarStyle.Text = "任务栏文本样式";
            // 
            // chkLockTaskbarText
            // 
            this.chkLockTaskbarText.AutoSize = true;
            this.chkLockTaskbarText.Location = new System.Drawing.Point(198, 18);
            this.chkLockTaskbarText.Name = "chkLockTaskbarText";
            this.chkLockTaskbarText.Size = new System.Drawing.Size(60, 16);
            this.chkLockTaskbarText.TabIndex = 6;
            this.chkLockTaskbarText.Text = "锁定";
            this.chkLockTaskbarText.UseVisualStyleBackColor = true;
            this.chkLockTaskbarText.CheckedChanged += new System.EventHandler(this.chkLockTaskbarText_CheckedChanged);
            // 
            // nudTaskbarFontSize
            // 
            this.nudTaskbarFontSize.Location = new System.Drawing.Point(198, 46);
            this.nudTaskbarFontSize.Minimum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.nudTaskbarFontSize.Name = "nudTaskbarFontSize";
            this.nudTaskbarFontSize.Size = new System.Drawing.Size(50, 21);
            this.nudTaskbarFontSize.TabIndex = 5;
            this.nudTaskbarFontSize.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nudTaskbarFontSize.ValueChanged += new System.EventHandler(this.nudTaskbarFontSize_ValueChanged);
            // 
            // lblTaskbarFontSize
            // 
            this.lblTaskbarFontSize.AutoSize = true;
            this.lblTaskbarFontSize.Location = new System.Drawing.Point(147, 48);
            this.lblTaskbarFontSize.Name = "lblTaskbarFontSize";
            this.lblTaskbarFontSize.Size = new System.Drawing.Size(47, 12);
            this.lblTaskbarFontSize.TabIndex = 4;
            this.lblTaskbarFontSize.Text = "字体大小:";
            // 
            // txtTaskbarFont
            // 
            this.txtTaskbarFont.Location = new System.Drawing.Point(65, 46);
            this.txtTaskbarFont.Name = "txtTaskbarFont";
            this.txtTaskbarFont.Size = new System.Drawing.Size(76, 21);
            this.txtTaskbarFont.TabIndex = 3;
            this.txtTaskbarFont.Text = "Arial";
            this.txtTaskbarFont.Validated += new System.EventHandler(this.txtTaskbarFont_Validated);
            // 
            // lblTaskbarFont
            // 
            this.lblTaskbarFont.AutoSize = true;
            this.lblTaskbarFont.Location = new System.Drawing.Point(6, 48);
            this.lblTaskbarFont.Name = "lblTaskbarFont";
            this.lblTaskbarFont.Size = new System.Drawing.Size(53, 12);
            this.lblTaskbarFont.TabIndex = 2;
            this.lblTaskbarFont.Text = "字体名称:";
            // 
            // lblTaskbarBackColor
            // 
            this.lblTaskbarBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTaskbarBackColor.Location = new System.Drawing.Point(147, 19);
            this.lblTaskbarBackColor.Name = "lblTaskbarBackColor";
            this.lblTaskbarBackColor.Size = new System.Drawing.Size(20, 20);
            this.lblTaskbarBackColor.TabIndex = 1;
            this.lblTaskbarBackColor.Click += new System.EventHandler(this.lblTaskbarBackColor_Click);
            // 
            // lblTaskbarTextColor
            // 
            this.lblTaskbarTextColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTaskbarTextColor.Location = new System.Drawing.Point(65, 19);
            this.lblTaskbarTextColor.Name = "lblTaskbarTextColor";
            this.lblTaskbarTextColor.Size = new System.Drawing.Size(20, 20);
            this.lblTaskbarTextColor.TabIndex = 0;
            this.lblTaskbarTextColor.Click += new System.EventHandler(this.lblTaskbarTextColor_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 322);
            this.Controls.Add(this.gbTaskbarStyle);
            this.Controls.Add(this.gbPosition);
            this.Controls.Add(this.gbDisplayMode);
            this.Controls.Add(this.lblCurrentIcon);
            this.Controls.Add(this.btnSetIcon);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.nudPageInterval);
            this.Controls.Add(this.lblPageInterval);
            this.Controls.Add(this.nudCharsPerLine);
            this.Controls.Add(this.lblCharsPerLine);
            this.Controls.Add(this.nudLinesPerPage);
            this.Controls.Add(this.lblLinesPerPage);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFilePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TXT 通知工具";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudLinesPerPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCharsPerLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPageInterval)).EndInit();
            this.gbDisplayMode.ResumeLayout(false);
            this.gbDisplayMode.PerformLayout();
            this.gbPosition.ResumeLayout(false);
            this.gbPosition.PerformLayout();
            this.gbTaskbarStyle.ResumeLayout(false);
            this.gbTaskbarStyle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTaskbarFontSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmiShowSettings;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblLinesPerPage;
        private System.Windows.Forms.NumericUpDown nudLinesPerPage;
        private System.Windows.Forms.Label lblCharsPerLine;
        private System.Windows.Forms.NumericUpDown nudCharsPerLine;
        private System.Windows.Forms.Label lblPageInterval;
        private System.Windows.Forms.NumericUpDown nudPageInterval;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnSetIcon;
        private System.Windows.Forms.Label lblCurrentIcon;
        private System.Windows.Forms.GroupBox gbDisplayMode;
        private System.Windows.Forms.RadioButton rbTaskbarText;
        private System.Windows.Forms.RadioButton rbBalloonTip;
        private System.Windows.Forms.Form taskbarTextForm;
        private System.Windows.Forms.GroupBox gbPosition;
        private System.Windows.Forms.RadioButton rbRight;
        private System.Windows.Forms.RadioButton rbLeft;
        private System.Windows.Forms.GroupBox gbTaskbarStyle;
        private System.Windows.Forms.CheckBox chkLockTaskbarText;
        private System.Windows.Forms.NumericUpDown nudTaskbarFontSize;
        private System.Windows.Forms.Label lblTaskbarFontSize;
        private System.Windows.Forms.TextBox txtTaskbarFont;
        private System.Windows.Forms.Label lblTaskbarFont;
        private System.Windows.Forms.Label lblTaskbarBackColor;
        private System.Windows.Forms.Label lblTaskbarTextColor;
    }
}