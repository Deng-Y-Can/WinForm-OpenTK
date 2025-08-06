namespace WinFormsApp.WindowsTool
{
    partial class CompressionTool
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbtnDecompress = new System.Windows.Forms.RadioButton();
            this.rbtnCompress = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbAlgorithm = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtSourcePath = new System.Windows.Forms.TextBox();
            this.btnBrowseSource = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtDestinationPath = new System.Windows.Forms.TextBox();
            this.btnBrowseDestination = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlDeflateParams = new System.Windows.Forms.Panel();
            this.nudBufferSize = new System.Windows.Forms.NumericUpDown();
            this.lblBufferSize = new System.Windows.Forms.Label();
            this.chkUseZLibHeader = new System.Windows.Forms.CheckBox();
            this.nudCompressionLevel = new System.Windows.Forms.NumericUpDown();
            this.lblCompressionLevel = new System.Windows.Forms.Label();
            this.pnlLzmaParams = new System.Windows.Forms.Panel();
            this.nudDictSize = new System.Windows.Forms.NumericUpDown();
            this.lblDictSize = new System.Windows.Forms.Label();
            this.nudLzmaCompressionLevel = new System.Windows.Forms.NumericUpDown();
            this.lblLzmaCompressionLevel = new System.Windows.Forms.Label();
            this.pnlBzip2Params = new System.Windows.Forms.Panel();
            this.nudBufferSizeBzip2 = new System.Windows.Forms.NumericUpDown();
            this.lblBufferSizeBzip2 = new System.Windows.Forms.Label();
            this.nudBlockSize = new System.Windows.Forms.NumericUpDown();
            this.lblBlockSize = new System.Windows.Forms.Label();
            this.pnlZstdParams = new System.Windows.Forms.Panel();
            this.nudZstdCompressionLevel = new System.Windows.Forms.NumericUpDown();
            this.lblZstdCompressionLevel = new System.Windows.Forms.Label();
            this.pnlDeflateDecompressParams = new System.Windows.Forms.Panel();
            this.nudBufferSizeDecompress = new System.Windows.Forms.NumericUpDown();
            this.lblBufferSizeDecompress = new System.Windows.Forms.Label();
            this.chkUseZLibHeaderDecompress = new System.Windows.Forms.CheckBox();
            this.lblEstimatedSize = new System.Windows.Forms.Label();
            this.txtEstimatedSize = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cmbEncoding = new System.Windows.Forms.ComboBox();
            this.lblEncoding = new System.Windows.Forms.Label();
            this.pnlBzip2DecompressParams = new System.Windows.Forms.Panel();
            this.nudBufferSizeBzip2Decompress = new System.Windows.Forms.NumericUpDown();
            this.lblBufferSizeBzip2Decompress = new System.Windows.Forms.Label();
            this.llblAbout = new System.Windows.Forms.LinkLabel();
            this.pnlParameters = new System.Windows.Forms.Panel();
            this.grpSourceType = new System.Windows.Forms.GroupBox();
            this.rbtnSourceFolder = new System.Windows.Forms.RadioButton();
            this.rbtnSourceFile = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.pnlDeflateParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBufferSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCompressionLevel)).BeginInit();
            this.pnlLzmaParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDictSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLzmaCompressionLevel)).BeginInit();
            this.pnlBzip2Params.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBufferSizeBzip2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBlockSize)).BeginInit();
            this.pnlZstdParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudZstdCompressionLevel)).BeginInit();
            this.pnlDeflateDecompressParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBufferSizeDecompress)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.pnlBzip2DecompressParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBufferSizeBzip2Decompress)).BeginInit();
            this.pnlParameters.SuspendLayout();
            this.grpSourceType.SuspendLayout();
            this.SuspendLayout();

            // 主面板
            this.pnlParameters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlParameters.Controls.Add(this.pnlZstdParams);
            this.pnlParameters.Controls.Add(this.pnlBzip2Params);
            this.pnlParameters.Controls.Add(this.pnlLzmaParams);
            this.pnlParameters.Controls.Add(this.pnlDeflateParams);
            this.pnlParameters.Controls.Add(this.pnlBzip2DecompressParams);
            this.pnlParameters.Controls.Add(this.pnlDeflateDecompressParams);
            this.pnlParameters.Controls.Add(this.txtEstimatedSize);
            this.pnlParameters.Controls.Add(this.lblEstimatedSize);
            this.pnlParameters.Location = new System.Drawing.Point(12, 280);
            this.pnlParameters.Name = "pnlParameters";
            this.pnlParameters.Size = new System.Drawing.Size(560, 140);
            this.pnlParameters.TabIndex = 5;

            // 源类型选择
            this.grpSourceType.Controls.Add(this.rbtnSourceFolder);
            this.grpSourceType.Controls.Add(this.rbtnSourceFile);
            this.grpSourceType.Location = new System.Drawing.Point(12, 83);
            this.grpSourceType.Name = "grpSourceType";
            this.grpSourceType.Size = new System.Drawing.Size(185, 65);
            this.grpSourceType.TabIndex = 11;
            this.grpSourceType.TabStop = false;
            this.grpSourceType.Text = "源类型";

            this.rbtnSourceFolder.AutoSize = true;
            this.rbtnSourceFolder.Location = new System.Drawing.Point(103, 25);
            this.rbtnSourceFolder.Name = "rbtnSourceFolder";
            this.rbtnSourceFolder.Size = new System.Drawing.Size(47, 16);
            this.rbtnSourceFolder.TabIndex = 1;
            this.rbtnSourceFolder.Text = "文件夹";
            this.rbtnSourceFolder.UseVisualStyleBackColor = true;

            this.rbtnSourceFile.AutoSize = true;
            this.rbtnSourceFile.Location = new System.Drawing.Point(20, 25);
            this.rbtnSourceFile.Name = "rbtnSourceFile";
            this.rbtnSourceFile.Size = new System.Drawing.Size(47, 16);
            this.rbtnSourceFile.TabIndex = 0;
            this.rbtnSourceFile.Text = "文件";
            this.rbtnSourceFile.UseVisualStyleBackColor = true;

            // groupBox1 - 操作类型
            this.groupBox1.Controls.Add(this.rbtnDecompress);
            this.groupBox1.Controls.Add(this.rbtnCompress);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(185, 65);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作类型";

            this.rbtnDecompress.AutoSize = true;
            this.rbtnDecompress.Location = new System.Drawing.Point(103, 25);
            this.rbtnDecompress.Name = "rbtnDecompress";
            this.rbtnDecompress.Size = new System.Drawing.Size(59, 16);
            this.rbtnDecompress.TabIndex = 1;
            this.rbtnDecompress.Text = "解压缩";
            this.rbtnDecompress.UseVisualStyleBackColor = true;

            this.rbtnCompress.AutoSize = true;
            this.rbtnCompress.Location = new System.Drawing.Point(20, 25);
            this.rbtnCompress.Name = "rbtnCompress";
            this.rbtnCompress.Size = new System.Drawing.Size(47, 16);
            this.rbtnCompress.TabIndex = 0;
            this.rbtnCompress.Text = "压缩";
            this.rbtnCompress.UseVisualStyleBackColor = true;

            // groupBox2 - 算法选择
            this.groupBox2.Controls.Add(this.cmbAlgorithm);
            this.groupBox2.Location = new System.Drawing.Point(203, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(369, 65);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "压缩算法";

            this.cmbAlgorithm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAlgorithm.FormattingEnabled = true;
            this.cmbAlgorithm.Items.AddRange(new object[] {
            "DEFLATE",
            "LZMA",
            "BZIP2",
            "ZSTD"});
            this.cmbAlgorithm.Location = new System.Drawing.Point(20, 23);
            this.cmbAlgorithm.Name = "cmbAlgorithm";
            this.cmbAlgorithm.Size = new System.Drawing.Size(325, 20);
            this.cmbAlgorithm.TabIndex = 0;

            // groupBox3 - 源文件路径
            this.groupBox3.Controls.Add(this.txtSourcePath);
            this.groupBox3.Controls.Add(this.btnBrowseSource);
            this.groupBox3.Location = new System.Drawing.Point(203, 83);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(369, 65);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "源路径";

            this.txtSourcePath.Location = new System.Drawing.Point(20, 23);
            this.txtSourcePath.Name = "txtSourcePath";
            this.txtSourcePath.ReadOnly = true;
            this.txtSourcePath.Size = new System.Drawing.Size(255, 21);
            this.txtSourcePath.TabIndex = 0;

            this.btnBrowseSource.Location = new System.Drawing.Point(281, 21);
            this.btnBrowseSource.Name = "btnBrowseSource";
            this.btnBrowseSource.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseSource.TabIndex = 1;
            this.btnBrowseSource.Text = "浏览...";
            this.btnBrowseSource.UseVisualStyleBackColor = true;
            this.btnBrowseSource.Click += new System.EventHandler(this.btnBrowseSource_Click);

            // groupBox4 - 目标文件路径
            this.groupBox4.Controls.Add(this.txtDestinationPath);
            this.groupBox4.Controls.Add(this.btnBrowseDestination);
            this.groupBox4.Location = new System.Drawing.Point(12, 154);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(560, 65);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "目标路径";

            this.txtDestinationPath.Location = new System.Drawing.Point(20, 23);
            this.txtDestinationPath.Name = "txtDestinationPath";
            this.txtDestinationPath.ReadOnly = true;
            this.txtDestinationPath.Size = new System.Drawing.Size(445, 21);
            this.txtDestinationPath.TabIndex = 0;

            this.btnBrowseDestination.Location = new System.Drawing.Point(471, 21);
            this.btnBrowseDestination.Name = "btnBrowseDestination";
            this.btnBrowseDestination.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseDestination.TabIndex = 1;
            this.btnBrowseDestination.Text = "浏览...";
            this.btnBrowseDestination.UseVisualStyleBackColor = true;
            this.btnBrowseDestination.Click += new System.EventHandler(this.btnBrowseDestination_Click);

            // 编码设置
            this.groupBox5.Controls.Add(this.cmbEncoding);
            this.groupBox5.Controls.Add(this.lblEncoding);
            this.groupBox5.Location = new System.Drawing.Point(12, 426);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(560, 60);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "文本编码";

            this.cmbEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEncoding.FormattingEnabled = true;
            this.cmbEncoding.Items.AddRange(new object[] {
            "UTF-8",
            "ASCII",
            "Unicode",
            "UTF-16",
            "UTF-32"});
            this.cmbEncoding.Location = new System.Drawing.Point(90, 20);
            this.cmbEncoding.Name = "cmbEncoding";
            this.cmbEncoding.Size = new System.Drawing.Size(150, 20);
            this.cmbEncoding.TabIndex = 1;

            this.lblEncoding.AutoSize = true;
            this.lblEncoding.Location = new System.Drawing.Point(20, 23);
            this.lblEncoding.Name = "lblEncoding";
            this.lblEncoding.Size = new System.Drawing.Size(65, 12);
            this.lblEncoding.TabIndex = 0;
            this.lblEncoding.Text = "编码格式:";

            // 执行按钮
            this.btnExecute.Location = new System.Drawing.Point(417, 492);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(155, 35);
            this.btnExecute.TabIndex = 7;
            this.btnExecute.Text = "压缩";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);

            // 进度条和状态
            this.progressBar.Location = new System.Drawing.Point(12, 492);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(399, 35);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.TabIndex = 8;
            this.progressBar.Visible = false;

            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 530);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 12);
            this.lblStatus.TabIndex = 9;

            // 关于链接
            this.llblAbout.AutoSize = true;
            this.llblAbout.Location = new System.Drawing.Point(510, 530);
            this.llblAbout.Name = "llblAbout";
            this.llblAbout.Size = new System.Drawing.Size(65, 12);
            this.llblAbout.TabIndex = 10;
            this.llblAbout.TabStop = true;
            this.llblAbout.Text = "关于工具...";
            this.llblAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblAbout_LinkClicked);

            // DEFLATE压缩参数
            this.pnlDeflateParams.Controls.Add(this.nudBufferSize);
            this.pnlDeflateParams.Controls.Add(this.lblBufferSize);
            this.pnlDeflateParams.Controls.Add(this.chkUseZLibHeader);
            this.pnlDeflateParams.Controls.Add(this.nudCompressionLevel);
            this.pnlDeflateParams.Controls.Add(this.lblCompressionLevel);
            this.pnlDeflateParams.Location = new System.Drawing.Point(10, 10);
            this.pnlDeflateParams.Name = "pnlDeflateParams";
            this.pnlDeflateParams.Size = new System.Drawing.Size(530, 120);
            this.pnlDeflateParams.TabIndex = 0;

            this.nudBufferSize.Location = new System.Drawing.Point(120, 55);
            this.nudBufferSize.Maximum = new decimal(new int[] {
            1048576,
            0,
            0,
            0});
            this.nudBufferSize.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nudBufferSize.Name = "nudBufferSize";
            this.nudBufferSize.Size = new System.Drawing.Size(120, 21);
            this.nudBufferSize.Increment = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nudBufferSize.Value = new decimal(new int[] {
            8192,
            0,
            0,
            0});

            this.lblBufferSize.AutoSize = true;
            this.lblBufferSize.Location = new System.Drawing.Point(10, 57);
            this.lblBufferSize.Name = "lblBufferSize";
            this.lblBufferSize.Size = new System.Drawing.Size(65, 12);
            this.lblBufferSize.TabIndex = 3;
            this.lblBufferSize.Text = "缓冲区大小:";

            this.chkUseZLibHeader.AutoSize = true;
            this.chkUseZLibHeader.Checked = true;
            this.chkUseZLibHeader.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseZLibHeader.Location = new System.Drawing.Point(270, 25);
            this.chkUseZLibHeader.Name = "chkUseZLibHeader";
            this.chkUseZLibHeader.Size = new System.Drawing.Size(101, 16);
            this.chkUseZLibHeader.TabIndex = 2;
            this.chkUseZLibHeader.Text = "使用ZLIB头部";
            this.chkUseZLibHeader.UseVisualStyleBackColor = true;

            this.nudCompressionLevel.Location = new System.Drawing.Point(120, 23);
            this.nudCompressionLevel.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nudCompressionLevel.Name = "nudCompressionLevel";
            this.nudCompressionLevel.Size = new System.Drawing.Size(120, 21);
            this.nudCompressionLevel.TabIndex = 1;
            this.nudCompressionLevel.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});

            this.lblCompressionLevel.AutoSize = true;
            this.lblCompressionLevel.Location = new System.Drawing.Point(10, 25);
            this.lblCompressionLevel.Name = "lblCompressionLevel";
            this.lblCompressionLevel.Size = new System.Drawing.Size(65, 12);
            this.lblCompressionLevel.TabIndex = 0;
            this.lblCompressionLevel.Text = "压缩级别:";

            // LZMA压缩参数
            this.pnlLzmaParams.Controls.Add(this.nudDictSize);
            this.pnlLzmaParams.Controls.Add(this.lblDictSize);
            this.pnlLzmaParams.Controls.Add(this.nudLzmaCompressionLevel);
            this.pnlLzmaParams.Controls.Add(this.lblLzmaCompressionLevel);
            this.pnlLzmaParams.Location = new System.Drawing.Point(10, 10);
            this.pnlLzmaParams.Name = "pnlLzmaParams";
            this.pnlLzmaParams.Size = new System.Drawing.Size(530, 120);
            this.pnlLzmaParams.TabIndex = 1;

            this.nudDictSize.Location = new System.Drawing.Point(120, 55);
            this.nudDictSize.Maximum = new decimal(new int[] {
            27,
            0,
            0,
            0});
            this.nudDictSize.Minimum = new decimal(new int[] {
            18,
            0,
            0,
            0});
            this.nudDictSize.Name = "nudDictSize";
            this.nudDictSize.Size = new System.Drawing.Size(120, 21);
            this.nudDictSize.TabIndex = 3;
            this.nudDictSize.Value = new decimal(new int[] {
            23,
            0,
            0,
            0});

            this.lblDictSize.AutoSize = true;
            this.lblDictSize.Location = new System.Drawing.Point(10, 57);
            this.lblDictSize.Name = "lblDictSize";
            this.lblDictSize.Size = new System.Drawing.Size(101, 12);
            this.lblDictSize.TabIndex = 2;
            this.lblDictSize.Text = "字典大小(2^N bytes):";

            this.nudLzmaCompressionLevel.Location = new System.Drawing.Point(120, 23);
            this.nudLzmaCompressionLevel.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nudLzmaCompressionLevel.Name = "nudLzmaCompressionLevel";
            this.nudLzmaCompressionLevel.Size = new System.Drawing.Size(120, 21);
            this.nudLzmaCompressionLevel.TabIndex = 1;
            this.nudLzmaCompressionLevel.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});

            this.lblLzmaCompressionLevel.AutoSize = true;
            this.lblLzmaCompressionLevel.Location = new System.Drawing.Point(10, 25);
            this.lblLzmaCompressionLevel.Name = "lblLzmaCompressionLevel";
            this.lblLzmaCompressionLevel.Size = new System.Drawing.Size(65, 12);
            this.lblLzmaCompressionLevel.TabIndex = 0;
            this.lblLzmaCompressionLevel.Text = "压缩级别:";

            // BZIP2压缩参数
            this.pnlBzip2Params.Controls.Add(this.nudBufferSizeBzip2);
            this.pnlBzip2Params.Controls.Add(this.lblBufferSizeBzip2);
            this.pnlBzip2Params.Controls.Add(this.nudBlockSize);
            this.pnlBzip2Params.Controls.Add(this.lblBlockSize);
            this.pnlBzip2Params.Location = new System.Drawing.Point(10, 10);
            this.pnlBzip2Params.Name = "pnlBzip2Params";
            this.pnlBzip2Params.Size = new System.Drawing.Size(530, 120);
            this.pnlBzip2Params.TabIndex = 2;

            this.nudBufferSizeBzip2.Location = new System.Drawing.Point(120, 55);
            this.nudBufferSizeBzip2.Maximum = new decimal(new int[] {
            1048576,
            0,
            0,
            0});
            this.nudBufferSizeBzip2.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nudBufferSizeBzip2.Name = "nudBufferSizeBzip2";
            this.nudBufferSizeBzip2.Size = new System.Drawing.Size(120, 21);
            this.nudBufferSizeBzip2.Increment = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nudBufferSizeBzip2.Value = new decimal(new int[] {
            8192,
            0,
            0,
            0});

            this.lblBufferSizeBzip2.AutoSize = true;
            this.lblBufferSizeBzip2.Location = new System.Drawing.Point(10, 57);
            this.lblBufferSizeBzip2.Name = "lblBufferSizeBzip2";
            this.lblBufferSizeBzip2.Size = new System.Drawing.Size(65, 12);
            this.lblBufferSizeBzip2.TabIndex = 2;
            this.lblBufferSizeBzip2.Text = "缓冲区大小:";

            this.nudBlockSize.Location = new System.Drawing.Point(120, 23);
            this.nudBlockSize.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.nudBlockSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBlockSize.Name = "nudBlockSize";
            this.nudBlockSize.Size = new System.Drawing.Size(120, 21);
            this.nudBlockSize.TabIndex = 1;
            this.nudBlockSize.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});

            this.lblBlockSize.AutoSize = true;
            this.lblBlockSize.Location = new System.Drawing.Point(10, 25);
            this.lblBlockSize.Name = "lblBlockSize";
            this.lblBlockSize.Size = new System.Drawing.Size(65, 12);
            this.lblBlockSize.TabIndex = 0;
            this.lblBlockSize.Text = "块大小(1-9):";

            // ZSTD压缩参数
            this.pnlZstdParams.Controls.Add(this.nudZstdCompressionLevel);
            this.pnlZstdParams.Controls.Add(this.lblZstdCompressionLevel);
            this.pnlZstdParams.Location = new System.Drawing.Point(10, 10);
            this.pnlZstdParams.Name = "pnlZstdParams";
            this.pnlZstdParams.Size = new System.Drawing.Size(530, 120);
            this.pnlZstdParams.TabIndex = 3;

            this.nudZstdCompressionLevel.Location = new System.Drawing.Point(120, 23);
            this.nudZstdCompressionLevel.Maximum = new decimal(new int[] {
            22,
            0,
            0,
            0});
            this.nudZstdCompressionLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudZstdCompressionLevel.Name = "nudZstdCompressionLevel";
            this.nudZstdCompressionLevel.Size = new System.Drawing.Size(120, 21);
            this.nudZstdCompressionLevel.TabIndex = 1;
            this.nudZstdCompressionLevel.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});

            this.lblZstdCompressionLevel.AutoSize = true;
            this.lblZstdCompressionLevel.Location = new System.Drawing.Point(10, 25);
            this.lblZstdCompressionLevel.Name = "lblZstdCompressionLevel";
            this.lblZstdCompressionLevel.Size = new System.Drawing.Size(65, 12);
            this.lblZstdCompressionLevel.TabIndex = 0;
            this.lblZstdCompressionLevel.Text = "压缩级别:";

            // DEFLATE解压参数
            this.pnlDeflateDecompressParams.Controls.Add(this.nudBufferSizeDecompress);
            this.pnlDeflateDecompressParams.Controls.Add(this.lblBufferSizeDecompress);
            this.pnlDeflateDecompressParams.Controls.Add(this.chkUseZLibHeaderDecompress);
            this.pnlDeflateDecompressParams.Location = new System.Drawing.Point(10, 10);
            this.pnlDeflateDecompressParams.Name = "pnlDeflateDecompressParams";
            this.pnlDeflateDecompressParams.Size = new System.Drawing.Size(530, 120);
            this.pnlDeflateDecompressParams.TabIndex = 4;

            this.nudBufferSizeDecompress.Location = new System.Drawing.Point(120, 55);
            this.nudBufferSizeDecompress.Maximum = new decimal(new int[] {
            1048576,
            0,
            0,
            0});
            this.nudBufferSizeDecompress.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nudBufferSizeDecompress.Name = "nudBufferSizeDecompress";
            this.nudBufferSizeDecompress.Size = new System.Drawing.Size(120, 21);
            this.nudBufferSizeDecompress.Increment = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nudBufferSizeDecompress.Value = new decimal(new int[] {
            8192,
            0,
            0,
            0});

            this.lblBufferSizeDecompress.AutoSize = true;
            this.lblBufferSizeDecompress.Location = new System.Drawing.Point(10, 57);
            this.lblBufferSizeDecompress.Name = "lblBufferSizeDecompress";
            this.lblBufferSizeDecompress.Size = new System.Drawing.Size(65, 12);
            this.lblBufferSizeDecompress.TabIndex = 1;
            this.lblBufferSizeDecompress.Text = "缓冲区大小:";

            this.chkUseZLibHeaderDecompress.AutoSize = true;
            this.chkUseZLibHeaderDecompress.Checked = true;
            this.chkUseZLibHeaderDecompress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseZLibHeaderDecompress.Location = new System.Drawing.Point(20, 25);
            this.chkUseZLibHeaderDecompress.Name = "chkUseZLibHeaderDecompress";
            this.chkUseZLibHeaderDecompress.Size = new System.Drawing.Size(101, 16);
            this.chkUseZLibHeaderDecompress.TabIndex = 0;
            this.chkUseZLibHeaderDecompress.Text = "使用ZLIB头部";
            this.chkUseZLibHeaderDecompress.UseVisualStyleBackColor = true;

            // BZIP2解压参数
            this.pnlBzip2DecompressParams.Controls.Add(this.nudBufferSizeBzip2Decompress);
            this.pnlBzip2DecompressParams.Controls.Add(this.lblBufferSizeBzip2Decompress);
            this.pnlBzip2DecompressParams.Location = new System.Drawing.Point(10, 10);
            this.pnlBzip2DecompressParams.Name = "pnlBzip2DecompressParams";
            this.pnlBzip2DecompressParams.Size = new System.Drawing.Size(530, 120);
            this.pnlBzip2DecompressParams.TabIndex = 5;

            this.nudBufferSizeBzip2Decompress.Location = new System.Drawing.Point(120, 23);
            this.nudBufferSizeBzip2Decompress.Maximum = new decimal(new int[] {
            1048576,
            0,
            0,
            0});
            this.nudBufferSizeBzip2Decompress.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nudBufferSizeBzip2Decompress.Name = "nudBufferSizeBzip2Decompress";
            this.nudBufferSizeBzip2Decompress.Size = new System.Drawing.Size(120, 21);
            this.nudBufferSizeBzip2Decompress.Increment = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nudBufferSizeBzip2Decompress.Value = new decimal(new int[] {
            8192,
            0,
            0,
            0});

            this.lblBufferSizeBzip2Decompress.AutoSize = true;
            this.lblBufferSizeBzip2Decompress.Location = new System.Drawing.Point(10, 25);
            this.lblBufferSizeBzip2Decompress.Name = "lblBufferSizeBzip2Decompress";
            this.lblBufferSizeBzip2Decompress.Size = new System.Drawing.Size(65, 12);
            this.lblBufferSizeBzip2Decompress.TabIndex = 0;
            this.lblBufferSizeBzip2Decompress.Text = "缓冲区大小:";

            // LZMA解压预估大小
            this.lblEstimatedSize.AutoSize = true;
            this.lblEstimatedSize.Location = new System.Drawing.Point(10, 25);
            this.lblEstimatedSize.Name = "lblEstimatedSize";
            this.lblEstimatedSize.Size = new System.Drawing.Size(89, 12);
            this.lblEstimatedSize.TabIndex = 6;
            this.lblEstimatedSize.Text = "预估解压大小:";

            this.txtEstimatedSize.Location = new System.Drawing.Point(120, 22);
            this.txtEstimatedSize.Name = "txtEstimatedSize";
            this.txtEstimatedSize.Size = new System.Drawing.Size(120, 21);
            this.txtEstimatedSize.TabIndex = 7;
            this.txtEstimatedSize.Text = "0";

            // 窗体设置
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 551);
            this.Controls.Add(this.grpSourceType);
            this.Controls.Add(this.llblAbout);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.pnlParameters);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CompressionTool";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "高级压缩解压工具";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.pnlDeflateParams.ResumeLayout(false);
            this.pnlDeflateParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBufferSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCompressionLevel)).EndInit();
            this.pnlLzmaParams.ResumeLayout(false);
            this.pnlLzmaParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDictSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLzmaCompressionLevel)).EndInit();
            this.pnlBzip2Params.ResumeLayout(false);
            this.pnlBzip2Params.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBufferSizeBzip2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBlockSize)).EndInit();
            this.pnlZstdParams.ResumeLayout(false);
            this.pnlZstdParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudZstdCompressionLevel)).EndInit();
            this.pnlDeflateDecompressParams.ResumeLayout(false);
            this.pnlDeflateDecompressParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBufferSizeDecompress)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.pnlBzip2DecompressParams.ResumeLayout(false);
            this.pnlBzip2DecompressParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBufferSizeBzip2Decompress)).EndInit();
            this.pnlParameters.ResumeLayout(false);
            this.pnlParameters.PerformLayout();
            this.grpSourceType.ResumeLayout(false);
            this.grpSourceType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnDecompress;
        private System.Windows.Forms.RadioButton rbtnCompress;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbAlgorithm;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtSourcePath;
        private System.Windows.Forms.Button btnBrowseSource;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtDestinationPath;
        private System.Windows.Forms.Button btnBrowseDestination;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel pnlDeflateParams;
        private System.Windows.Forms.NumericUpDown nudBufferSize;
        private System.Windows.Forms.Label lblBufferSize;
        private System.Windows.Forms.CheckBox chkUseZLibHeader;
        private System.Windows.Forms.NumericUpDown nudCompressionLevel;
        private System.Windows.Forms.Label lblCompressionLevel;
        private System.Windows.Forms.Panel pnlLzmaParams;
        private System.Windows.Forms.NumericUpDown nudDictSize;
        private System.Windows.Forms.Label lblDictSize;
        private System.Windows.Forms.NumericUpDown nudLzmaCompressionLevel;
        private System.Windows.Forms.Label lblLzmaCompressionLevel;
        private System.Windows.Forms.Panel pnlBzip2Params;
        private System.Windows.Forms.NumericUpDown nudBufferSizeBzip2;
        private System.Windows.Forms.Label lblBufferSizeBzip2;
        private System.Windows.Forms.NumericUpDown nudBlockSize;
        private System.Windows.Forms.Label lblBlockSize;
        private System.Windows.Forms.Panel pnlZstdParams;
        private System.Windows.Forms.NumericUpDown nudZstdCompressionLevel;
        private System.Windows.Forms.Label lblZstdCompressionLevel;
        private System.Windows.Forms.Panel pnlDeflateDecompressParams;
        private System.Windows.Forms.NumericUpDown nudBufferSizeDecompress;
        private System.Windows.Forms.Label lblBufferSizeDecompress;
        private System.Windows.Forms.CheckBox chkUseZLibHeaderDecompress;
        private System.Windows.Forms.Label lblEstimatedSize;
        private System.Windows.Forms.TextBox txtEstimatedSize;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox cmbEncoding;
        private System.Windows.Forms.Label lblEncoding;
        private System.Windows.Forms.Panel pnlBzip2DecompressParams;
        private System.Windows.Forms.NumericUpDown nudBufferSizeBzip2Decompress;
        private System.Windows.Forms.Label lblBufferSizeBzip2Decompress;
        private System.Windows.Forms.LinkLabel llblAbout;
        private System.Windows.Forms.Panel pnlParameters;
        private System.Windows.Forms.GroupBox grpSourceType;
        private System.Windows.Forms.RadioButton rbtnSourceFolder;
        private System.Windows.Forms.RadioButton rbtnSourceFile;
    }
}