namespace WinFormsApp.WindowsTool
{
    partial class KeepCatch
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
            this.components = new System.ComponentModel.Container();
            this.statusLabel = new System.Windows.Forms.Label();
            this.regionCombox = new System.Windows.Forms.ComboBox();
            this.previewBox = new System.Windows.Forms.PictureBox();
            this.recordBtn = new System.Windows.Forms.Button();
            this.captureBtn = new System.Windows.Forms.Button();
            this.folderBtn = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusLabel
            // 
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusLabel.Height = 30;
            this.statusLabel.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.statusLabel.Text = "就绪";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusLabel.Name = "statusLabel";
            // 
            // regionCombox
            // 
            this.regionCombox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.regionCombox.Height = 40;
            this.regionCombox.Location = new System.Drawing.Point(360, 10);
            this.regionCombox.Name = "regionCombox";
            this.regionCombox.Width = 120;
            this.regionCombox.Items.AddRange(new object[] {
            "全屏", "矩形区域", "三角形区域", "圆形区域"});
            this.regionCombox.SelectedIndex = 0;
            this.regionCombox.SelectedIndexChanged += new System.EventHandler(this.RegionCombo_SelectedIndexChanged);
            // 
            // previewBox
            // 
            this.previewBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.previewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewBox.Name = "previewBox";
            this.previewBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // recordBtn
            // 
            this.recordBtn.Height = 40;
            this.recordBtn.Location = new System.Drawing.Point(120, 10);
            this.recordBtn.Name = "recordBtn";
            this.recordBtn.Text = "开始录制";
            this.recordBtn.Width = 100;
            this.recordBtn.Click += new System.EventHandler(this.RecordBtn_Click);
            // 
            // captureBtn
            // 
            this.captureBtn.Height = 40;
            this.captureBtn.Location = new System.Drawing.Point(10, 10);
            this.captureBtn.Name = "captureBtn";
            this.captureBtn.Text = "截图";
            this.captureBtn.Width = 100;
            this.captureBtn.Click += new System.EventHandler(this.CaptureBtn_Click);
            // 
            // folderBtn
            // 
            this.folderBtn.Height = 40;
            this.folderBtn.Location = new System.Drawing.Point(230, 10);
            this.folderBtn.Name = "folderBtn";
            this.folderBtn.Text = "选择保存位置";
            this.folderBtn.Width = 120;
            this.folderBtn.Click += new System.EventHandler(this.FolderBtn_Click);
            // 
            // panel
            // 
            this.panel.Controls.Add(this.captureBtn);
            this.panel.Controls.Add(this.recordBtn);
            this.panel.Controls.Add(this.folderBtn);
            this.panel.Controls.Add(this.regionCombox);
            this.panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel.Height = 60;
            this.panel.Name = "panel";
            this.panel.Padding = new System.Windows.Forms.Padding(10);
            // 
            // KeepCatch
            // 
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.Controls.Add(this.previewBox);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.statusLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "KeepCatch";
            this.Text = "KeepCatch 屏幕捕捉工具";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KeepCatchForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private Panel panel; // 新增面板控件成员
    }
}