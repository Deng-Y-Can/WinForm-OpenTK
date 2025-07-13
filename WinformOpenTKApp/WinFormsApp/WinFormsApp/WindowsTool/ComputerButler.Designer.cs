namespace WinFormsApp.WindowsTool
{
    partial class ComputerButler
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComputerButler));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageSystem = new System.Windows.Forms.TabPage();
            this.systemInfoBox = new System.Windows.Forms.RichTextBox();
            this.tabPageNetwork = new System.Windows.Forms.TabPage();
            this.portGridView = new System.Windows.Forms.DataGridView();
            this.panelNetworkInfo = new System.Windows.Forms.Panel();
            this.macTextBox = new System.Windows.Forms.TextBox();
            this.labelMac = new System.Windows.Forms.Label();
            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.labelIp = new System.Windows.Forms.Label();
            this.tabPageCpu = new System.Windows.Forms.TabPage();
            this.cpuUsageLabel = new System.Windows.Forms.Label();
            this.cpuUsageBar = new System.Windows.Forms.ProgressBar();
            this.labelCpuUsage = new System.Windows.Forms.Label();
            this.cpuLogicalTextBox = new System.Windows.Forms.TextBox();
            this.labelLogicalProcessors = new System.Windows.Forms.Label();
            this.cpuCoresTextBox = new System.Windows.Forms.TextBox();
            this.labelCores = new System.Windows.Forms.Label();
            this.cpuNameTextBox = new System.Windows.Forms.TextBox();
            this.labelCpuName = new System.Windows.Forms.Label();
            this.panelCpuInfo = new System.Windows.Forms.Panel();
            this.tabPageMemory = new System.Windows.Forms.TabPage();
            this.memoryUsageLabel = new System.Windows.Forms.Label();
            this.memoryUsageBar = new System.Windows.Forms.ProgressBar();
            this.labelMemoryUsage = new System.Windows.Forms.Label();
            this.availableMemoryTextBox = new System.Windows.Forms.TextBox();
            this.labelAvailableMemory = new System.Windows.Forms.Label();
            this.usedMemoryTextBox = new System.Windows.Forms.TextBox();
            this.labelUsedMemory = new System.Windows.Forms.Label();
            this.totalMemoryTextBox = new System.Windows.Forms.TextBox();
            this.labelTotalMemory = new System.Windows.Forms.Label();
            this.panelMemoryInfo = new System.Windows.Forms.Panel();
            this.tabPageDisk = new System.Windows.Forms.TabPage();
            this.diskGridView = new System.Windows.Forms.DataGridView();
            this.tabPageService = new System.Windows.Forms.TabPage();
            this.panelServiceButtons = new System.Windows.Forms.Panel();
            this.ServiceRefreshButton = new System.Windows.Forms.Button();
            this.ServicePauseButton = new System.Windows.Forms.Button();
            this.ServiceStopButton = new System.Windows.Forms.Button();
            this.ServiceStartButton = new System.Windows.Forms.Button();
            this.serviceGridView = new System.Windows.Forms.DataGridView();
            this.tabPageProcess = new System.Windows.Forms.TabPage();
            this.panelProcessButtons = new System.Windows.Forms.Panel();
            this.ProcessRefreshButton = new System.Windows.Forms.Button();
            this.ProcessKillButton = new System.Windows.Forms.Button();
            this.processGridView = new System.Windows.Forms.DataGridView();
            this.tabPageDevice = new System.Windows.Forms.TabPage();
            this.deviceGridView = new System.Windows.Forms.DataGridView();
            this.tabPageRegistry = new System.Windows.Forms.TabPage();
            this.registryGridView = new System.Windows.Forms.DataGridView();
            this.registryTree = new System.Windows.Forms.TreeView();
            this.panelTop = new System.Windows.Forms.Panel();
            this.flowLayoutPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonRepairPerf = new System.Windows.Forms.Button();
            this.labelSeparator = new System.Windows.Forms.Label();
            this.buttonCamera = new System.Windows.Forms.Button();
            this.buttonScreenRecord = new System.Windows.Forms.Button();
            this.buttonScreenshot = new System.Windows.Forms.Button();
            this.buttonMyComputer = new System.Windows.Forms.Button();
            this.buttonDeviceManager = new System.Windows.Forms.Button();
            this.buttonServices = new System.Windows.Forms.Button();
            this.buttonTaskManager = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPageSystem.SuspendLayout();
            this.tabPageNetwork.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portGridView)).BeginInit();
            this.panelNetworkInfo.SuspendLayout();
            this.tabPageCpu.SuspendLayout();
            this.panelCpuInfo.SuspendLayout();
            this.tabPageMemory.SuspendLayout();
            this.panelMemoryInfo.SuspendLayout();
            this.tabPageDisk.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.diskGridView)).BeginInit();
            this.tabPageService.SuspendLayout();
            this.panelServiceButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.serviceGridView)).BeginInit();
            this.tabPageProcess.SuspendLayout();
            this.panelProcessButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.processGridView)).BeginInit();
            this.tabPageDevice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deviceGridView)).BeginInit();
            this.tabPageRegistry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.registryGridView)).BeginInit();
            this.panelTop.SuspendLayout();
            this.flowLayoutPanelButtons.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageSystem);
            this.tabControl1.Controls.Add(this.tabPageNetwork);
            this.tabControl1.Controls.Add(this.tabPageCpu);
            this.tabControl1.Controls.Add(this.tabPageMemory);
            this.tabControl1.Controls.Add(this.tabPageDisk);
            this.tabControl1.Controls.Add(this.tabPageService);
            this.tabControl1.Controls.Add(this.tabPageProcess);
            this.tabControl1.Controls.Add(this.tabPageDevice);
            this.tabControl1.Controls.Add(this.tabPageRegistry);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabControl1.Location = new System.Drawing.Point(0, 40);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(900, 520);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1_SelectedIndexChanged);
            // 
            // tabPageSystem
            // 
            this.tabPageSystem.Controls.Add(this.systemInfoBox);
            this.tabPageSystem.Location = new System.Drawing.Point(4, 25);
            this.tabPageSystem.Name = "tabPageSystem";
            this.tabPageSystem.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSystem.Size = new System.Drawing.Size(892, 491);
            this.tabPageSystem.TabIndex = 0;
            this.tabPageSystem.Text = "系统信息";
            this.tabPageSystem.UseVisualStyleBackColor = true;
            // 
            // systemInfoBox
            // 
            this.systemInfoBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.systemInfoBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.systemInfoBox.Font = new System.Drawing.Font("Consolas", 10F);
            this.systemInfoBox.Location = new System.Drawing.Point(3, 3);
            this.systemInfoBox.Name = "systemInfoBox";
            this.systemInfoBox.ReadOnly = true;
            this.systemInfoBox.Size = new System.Drawing.Size(886, 485);
            this.systemInfoBox.TabIndex = 0;
            this.systemInfoBox.Text = "";
            // 
            // tabPageNetwork
            // 
            this.tabPageNetwork.Controls.Add(this.portGridView);
            this.tabPageNetwork.Controls.Add(this.panelNetworkInfo);
            this.tabPageNetwork.Location = new System.Drawing.Point(4, 25);
            this.tabPageNetwork.Name = "tabPageNetwork";
            this.tabPageNetwork.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNetwork.Size = new System.Drawing.Size(892, 491);
            this.tabPageNetwork.TabIndex = 1;
            this.tabPageNetwork.Text = "网络信息";
            this.tabPageNetwork.UseVisualStyleBackColor = true;
            // 
            // portGridView
            // 
            this.portGridView.AllowUserToAddRows = false;
            this.portGridView.AllowUserToDeleteRows = false;
            this.portGridView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.portGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.portGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.portGridView.Location = new System.Drawing.Point(3, 56);
            this.portGridView.Name = "portGridView";
            this.portGridView.ReadOnly = true;
            this.portGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.portGridView.Size = new System.Drawing.Size(886, 432);
            this.portGridView.TabIndex = 1;
            // 
            // panelNetworkInfo
            // 
            this.panelNetworkInfo.Controls.Add(this.macTextBox);
            this.panelNetworkInfo.Controls.Add(this.labelMac);
            this.panelNetworkInfo.Controls.Add(this.ipTextBox);
            this.panelNetworkInfo.Controls.Add(this.labelIp);
            this.panelNetworkInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNetworkInfo.Location = new System.Drawing.Point(3, 3);
            this.panelNetworkInfo.Name = "panelNetworkInfo";
            this.panelNetworkInfo.Size = new System.Drawing.Size(886, 53);
            this.panelNetworkInfo.TabIndex = 0;
            // 
            // macTextBox
            // 
            this.macTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.macTextBox.Location = new System.Drawing.Point(310, 15);
            this.macTextBox.Name = "macTextBox";
            this.macTextBox.ReadOnly = true;
            this.macTextBox.Size = new System.Drawing.Size(150, 23);
            this.macTextBox.TabIndex = 3;
            // 
            // labelMac
            // 
            this.labelMac.Location = new System.Drawing.Point(240, 18);
            this.labelMac.Name = "labelMac";
            this.labelMac.Size = new System.Drawing.Size(60, 20);
            this.labelMac.TabIndex = 2;
            this.labelMac.Text = "MAC地址:";
            // 
            // ipTextBox
            // 
            this.ipTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ipTextBox.Location = new System.Drawing.Point(80, 15);
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.ReadOnly = true;
            this.ipTextBox.Size = new System.Drawing.Size(150, 23);
            this.ipTextBox.TabIndex = 1;
            // 
            // labelIp
            // 
            this.labelIp.Location = new System.Drawing.Point(10, 18);
            this.labelIp.Name = "labelIp";
            this.labelIp.Size = new System.Drawing.Size(60, 20);
            this.labelIp.TabIndex = 0;
            this.labelIp.Text = "IP地址:";
            // 
            // tabPageCpu
            // 
            this.tabPageCpu.Controls.Add(this.panelCpuInfo);
            this.tabPageCpu.Location = new System.Drawing.Point(4, 25);
            this.tabPageCpu.Name = "tabPageCpu";
            this.tabPageCpu.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCpu.Size = new System.Drawing.Size(892, 491);
            this.tabPageCpu.TabIndex = 2;
            this.tabPageCpu.Text = "CPU信息";
            this.tabPageCpu.UseVisualStyleBackColor = true;
            // 
            // cpuUsageLabel
            // 
            this.cpuUsageLabel.Location = new System.Drawing.Point(870, 78);
            this.cpuUsageLabel.Name = "cpuUsageLabel";
            this.cpuUsageLabel.Size = new System.Drawing.Size(40, 20);
            this.cpuUsageLabel.TabIndex = 6;
            this.cpuUsageLabel.Text = "0%";
            this.cpuUsageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cpuUsageBar
            // 
            this.cpuUsageBar.Location = new System.Drawing.Point(100, 75);
            this.cpuUsageBar.Name = "cpuUsageBar";
            this.cpuUsageBar.Size = new System.Drawing.Size(760, 20);
            this.cpuUsageBar.TabIndex = 5;
            // 
            // labelCpuUsage
            // 
            this.labelCpuUsage.Location = new System.Drawing.Point(10, 78);
            this.labelCpuUsage.Name = "labelCpuUsage";
            this.labelCpuUsage.Size = new System.Drawing.Size(80, 20);
            this.labelCpuUsage.TabIndex = 4;
            this.labelCpuUsage.Text = "CPU使用率:";
            // 
            // cpuLogicalTextBox
            // 
            this.cpuLogicalTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cpuLogicalTextBox.Location = new System.Drawing.Point(300, 45);
            this.cpuLogicalTextBox.Name = "cpuLogicalTextBox";
            this.cpuLogicalTextBox.ReadOnly = true;
            this.cpuLogicalTextBox.Size = new System.Drawing.Size(100, 23);
            this.cpuLogicalTextBox.TabIndex = 3;
            // 
            // labelLogicalProcessors
            // 
            this.labelLogicalProcessors.Location = new System.Drawing.Point(200, 48);
            this.labelLogicalProcessors.Name = "labelLogicalProcessors";
            this.labelLogicalProcessors.Size = new System.Drawing.Size(90, 20);
            this.labelLogicalProcessors.TabIndex = 2;
            this.labelLogicalProcessors.Text = "逻辑处理器:";
            // 
            // cpuCoresTextBox
            // 
            this.cpuCoresTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cpuCoresTextBox.Location = new System.Drawing.Point(80, 45);
            this.cpuCoresTextBox.Name = "cpuCoresTextBox";
            this.cpuCoresTextBox.ReadOnly = true;
            this.cpuCoresTextBox.Size = new System.Drawing.Size(100, 23);
            this.cpuCoresTextBox.TabIndex = 1;
            // 
            // labelCores
            // 
            this.labelCores.Location = new System.Drawing.Point(10, 48);
            this.labelCores.Name = "labelCores";
            this.labelCores.Size = new System.Drawing.Size(60, 20);
            this.labelCores.TabIndex = 0;
            this.labelCores.Text = "核心数:";
            // 
            // cpuNameTextBox
            // 
            this.cpuNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cpuNameTextBox.Location = new System.Drawing.Point(80, 15);
            this.cpuNameTextBox.Name = "cpuNameTextBox";
            this.cpuNameTextBox.ReadOnly = true;
            this.cpuNameTextBox.Size = new System.Drawing.Size(300, 23);
            this.cpuNameTextBox.TabIndex = 1;
            // 
            // labelCpuName
            // 
            this.labelCpuName.Location = new System.Drawing.Point(10, 18);
            this.labelCpuName.Name = "labelCpuName";
            this.labelCpuName.Size = new System.Drawing.Size(60, 20);
            this.labelCpuName.TabIndex = 0;
            this.labelCpuName.Text = "CPU名称:";
            // 
            // panelCpuInfo
            // 
            this.panelCpuInfo.Controls.Add(this.cpuUsageLabel);
            this.panelCpuInfo.Controls.Add(this.cpuUsageBar);
            this.panelCpuInfo.Controls.Add(this.labelCpuUsage);
            this.panelCpuInfo.Controls.Add(this.cpuLogicalTextBox);
            this.panelCpuInfo.Controls.Add(this.labelLogicalProcessors);
            this.panelCpuInfo.Controls.Add(this.cpuCoresTextBox);
            this.panelCpuInfo.Controls.Add(this.labelCores);
            this.panelCpuInfo.Controls.Add(this.cpuNameTextBox);
            this.panelCpuInfo.Controls.Add(this.labelCpuName);
            this.panelCpuInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCpuInfo.Location = new System.Drawing.Point(3, 3);
            this.panelCpuInfo.Name = "panelCpuInfo";
            this.panelCpuInfo.Size = new System.Drawing.Size(886, 485);
            this.panelCpuInfo.TabIndex = 0;
            // 
            // tabPageMemory
            // 
            this.tabPageMemory.Controls.Add(this.panelMemoryInfo);
            this.tabPageMemory.Location = new System.Drawing.Point(4, 25);
            this.tabPageMemory.Name = "tabPageMemory";
            this.tabPageMemory.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMemory.Size = new System.Drawing.Size(892, 491);
            this.tabPageMemory.TabIndex = 3;
            this.tabPageMemory.Text = "内存信息";
            this.tabPageMemory.UseVisualStyleBackColor = true;
            // 
            // memoryUsageLabel
            // 
            this.memoryUsageLabel.Location = new System.Drawing.Point(870, 108);
            this.memoryUsageLabel.Name = "memoryUsageLabel";
            this.memoryUsageLabel.Size = new System.Drawing.Size(40, 20);
            this.memoryUsageLabel.TabIndex = 8;
            this.memoryUsageLabel.Text = "0%";
            this.memoryUsageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // memoryUsageBar
            // 
            this.memoryUsageBar.Location = new System.Drawing.Point(100, 105);
            this.memoryUsageBar.Name = "memoryUsageBar";
            this.memoryUsageBar.Size = new System.Drawing.Size(760, 20);
            this.memoryUsageBar.TabIndex = 7;
            // 
            // labelMemoryUsage
            // 
            this.labelMemoryUsage.Location = new System.Drawing.Point(10, 108);
            this.labelMemoryUsage.Name = "labelMemoryUsage";
            this.labelMemoryUsage.Size = new System.Drawing.Size(80, 20);
            this.labelMemoryUsage.TabIndex = 6;
            this.labelMemoryUsage.Text = "内存使用率:";
            // 
            // availableMemoryTextBox
            // 
            this.availableMemoryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.availableMemoryTextBox.Location = new System.Drawing.Point(80, 75);
            this.availableMemoryTextBox.Name = "availableMemoryTextBox";
            this.availableMemoryTextBox.ReadOnly = true;
            this.availableMemoryTextBox.Size = new System.Drawing.Size(200, 23);
            this.availableMemoryTextBox.TabIndex = 5;
            // 
            // labelAvailableMemory
            // 
            this.labelAvailableMemory.Location = new System.Drawing.Point(10, 78);
            this.labelAvailableMemory.Name = "labelAvailableMemory";
            this.labelAvailableMemory.Size = new System.Drawing.Size(60, 20);
            this.labelAvailableMemory.TabIndex = 4;
            this.labelAvailableMemory.Text = "可用内存:";
            // 
            // usedMemoryTextBox
            // 
            this.usedMemoryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.usedMemoryTextBox.Location = new System.Drawing.Point(80, 45);
            this.usedMemoryTextBox.Name = "usedMemoryTextBox";
            this.usedMemoryTextBox.ReadOnly = true;
            this.usedMemoryTextBox.Size = new System.Drawing.Size(200, 23);
            this.usedMemoryTextBox.TabIndex = 3;
            // 
            // labelUsedMemory
            // 
            this.labelUsedMemory.Location = new System.Drawing.Point(10, 48);
            this.labelUsedMemory.Name = "labelUsedMemory";
            this.labelUsedMemory.Size = new System.Drawing.Size(60, 20);
            this.labelUsedMemory.TabIndex = 2;
            this.labelUsedMemory.Text = "已用内存:";
            // 
            // totalMemoryTextBox
            // 
            this.totalMemoryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.totalMemoryTextBox.Location = new System.Drawing.Point(80, 15);
            this.totalMemoryTextBox.Name = "totalMemoryTextBox";
            this.totalMemoryTextBox.ReadOnly = true;
            this.totalMemoryTextBox.Size = new System.Drawing.Size(200, 23);
            this.totalMemoryTextBox.TabIndex = 1;
            // 
            // labelTotalMemory
            // 
            this.labelTotalMemory.Location = new System.Drawing.Point(10, 18);
            this.labelTotalMemory.Name = "labelTotalMemory";
            this.labelTotalMemory.Size = new System.Drawing.Size(60, 20);
            this.labelTotalMemory.TabIndex = 0;
            this.labelTotalMemory.Text = "总内存:";
            // 
            // panelMemoryInfo
            // 
            this.panelMemoryInfo.Controls.Add(this.memoryUsageLabel);
            this.panelMemoryInfo.Controls.Add(this.memoryUsageBar);
            this.panelMemoryInfo.Controls.Add(this.labelMemoryUsage);
            this.panelMemoryInfo.Controls.Add(this.availableMemoryTextBox);
            this.panelMemoryInfo.Controls.Add(this.labelAvailableMemory);
            this.panelMemoryInfo.Controls.Add(this.usedMemoryTextBox);
            this.panelMemoryInfo.Controls.Add(this.labelUsedMemory);
            this.panelMemoryInfo.Controls.Add(this.totalMemoryTextBox);
            this.panelMemoryInfo.Controls.Add(this.labelTotalMemory);
            this.panelMemoryInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMemoryInfo.Location = new System.Drawing.Point(3, 3);
            this.panelMemoryInfo.Name = "panelMemoryInfo";
            this.panelMemoryInfo.Size = new System.Drawing.Size(886, 485);
            this.panelMemoryInfo.TabIndex = 0;
            // 
            // tabPageDisk
            // 
            this.tabPageDisk.Controls.Add(this.diskGridView);
            this.tabPageDisk.Location = new System.Drawing.Point(4, 25);
            this.tabPageDisk.Name = "tabPageDisk";
            this.tabPageDisk.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDisk.Size = new System.Drawing.Size(892, 491);
            this.tabPageDisk.TabIndex = 4;
            this.tabPageDisk.Text = "磁盘信息";
            this.tabPageDisk.UseVisualStyleBackColor = true;
            // 
            // diskGridView
            // 
            this.diskGridView.AllowUserToAddRows = false;
            this.diskGridView.AllowUserToDeleteRows = false;
            this.diskGridView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.diskGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.diskGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diskGridView.Location = new System.Drawing.Point(3, 3);
            this.diskGridView.Name = "diskGridView";
            this.diskGridView.ReadOnly = true;
            this.diskGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.diskGridView.Size = new System.Drawing.Size(886, 485);
            this.diskGridView.TabIndex = 0;
            // 
            // tabPageService
            // 
            this.tabPageService.Controls.Add(this.panelServiceButtons);
            this.tabPageService.Controls.Add(this.serviceGridView);
            this.tabPageService.Location = new System.Drawing.Point(4, 25);
            this.tabPageService.Name = "tabPageService";
            this.tabPageService.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageService.Size = new System.Drawing.Size(892, 491);
            this.tabPageService.TabIndex = 5;
            this.tabPageService.Text = "服务管理";
            this.tabPageService.UseVisualStyleBackColor = true;
            // 
            // panelServiceButtons
            // 
            this.panelServiceButtons.Controls.Add(this.ServiceRefreshButton);
            this.panelServiceButtons.Controls.Add(this.ServicePauseButton);
            this.panelServiceButtons.Controls.Add(this.ServiceStopButton);
            this.panelServiceButtons.Controls.Add(this.ServiceStartButton);
            this.panelServiceButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelServiceButtons.Location = new System.Drawing.Point(3, 453);
            this.panelServiceButtons.Name = "panelServiceButtons";
            this.panelServiceButtons.Size = new System.Drawing.Size(886, 35);
            this.panelServiceButtons.TabIndex = 1;
            // 
            // ServiceRefreshButton
            // 
            this.ServiceRefreshButton.Location = new System.Drawing.Point(245, 5);
            this.ServiceRefreshButton.Name = "ServiceRefreshButton";
            this.ServiceRefreshButton.Size = new System.Drawing.Size(75, 23);
            this.ServiceRefreshButton.TabIndex = 3;
            this.ServiceRefreshButton.Text = "刷新";
            this.ServiceRefreshButton.UseVisualStyleBackColor = true;
            this.ServiceRefreshButton.Click += new System.EventHandler(this.ServiceRefreshButton_Click);
            // 
            // ServicePauseButton
            // 
            this.ServicePauseButton.Location = new System.Drawing.Point(165, 5);
            this.ServicePauseButton.Name = "ServicePauseButton";
            this.ServicePauseButton.Size = new System.Drawing.Size(75, 23);
            this.ServicePauseButton.TabIndex = 2;
            this.ServicePauseButton.Text = "暂停";
            this.ServicePauseButton.UseVisualStyleBackColor = true;
            this.ServicePauseButton.Click += new System.EventHandler(this.ServicePauseButton_Click);
            // 
            // ServiceStopButton
            // 
            this.ServiceStopButton.Location = new System.Drawing.Point(85, 5);
            this.ServiceStopButton.Name = "ServiceStopButton";
            this.ServiceStopButton.Size = new System.Drawing.Size(75, 23);
            this.ServiceStopButton.TabIndex = 1;
            this.ServiceStopButton.Text = "停止";
            this.ServiceStopButton.UseVisualStyleBackColor = true;
            this.ServiceStopButton.Click += new System.EventHandler(this.ServiceStopButton_Click);
            // 
            // ServiceStartButton
            // 
            this.ServiceStartButton.Location = new System.Drawing.Point(5, 5);
            this.ServiceStartButton.Name = "ServiceStartButton";
            this.ServiceStartButton.Size = new System.Drawing.Size(75, 23);
            this.ServiceStartButton.TabIndex = 0;
            this.ServiceStartButton.Text = "启动";
            this.ServiceStartButton.UseVisualStyleBackColor = true;
            this.ServiceStartButton.Click += new System.EventHandler(this.ServiceStartButton_Click);
            // 
            // serviceGridView
            // 
            this.serviceGridView.AllowUserToAddRows = false;
            this.serviceGridView.AllowUserToDeleteRows = false;
            this.serviceGridView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.serviceGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.serviceGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serviceGridView.Location = new System.Drawing.Point(3, 3);
            this.serviceGridView.Name = "serviceGridView";
            this.serviceGridView.ReadOnly = true;
            this.serviceGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.serviceGridView.Size = new System.Drawing.Size(886, 485);
            this.serviceGridView.TabIndex = 0;
            // 
            // tabPageProcess
            // 
            this.tabPageProcess.Controls.Add(this.panelProcessButtons);
            this.tabPageProcess.Controls.Add(this.processGridView);
            this.tabPageProcess.Location = new System.Drawing.Point(4, 25);
            this.tabPageProcess.Name = "tabPageProcess";
            this.tabPageProcess.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageProcess.Size = new System.Drawing.Size(892, 491);
            this.tabPageProcess.TabIndex = 6;
            this.tabPageProcess.Text = "进程管理";
            this.tabPageProcess.UseVisualStyleBackColor = true;
            // 
            // panelProcessButtons
            // 
            this.panelProcessButtons.Controls.Add(this.ProcessRefreshButton);
            this.panelProcessButtons.Controls.Add(this.ProcessKillButton);
            this.panelProcessButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelProcessButtons.Location = new System.Drawing.Point(3, 453);
            this.panelProcessButtons.Name = "panelProcessButtons";
            this.panelProcessButtons.Size = new System.Drawing.Size(886, 35);
            this.panelProcessButtons.TabIndex = 1;
            // 
            // ProcessRefreshButton
            // 
            this.ProcessRefreshButton.Location = new System.Drawing.Point(100, 5);
            this.ProcessRefreshButton.Name = "ProcessRefreshButton";
            this.ProcessRefreshButton.Size = new System.Drawing.Size(75, 23);
            this.ProcessRefreshButton.TabIndex = 1;
            this.ProcessRefreshButton.Text = "刷新";
            this.ProcessRefreshButton.UseVisualStyleBackColor = true;
            this.ProcessRefreshButton.Click += new System.EventHandler(this.ProcessRefreshButton_Click);
            // 
            // ProcessKillButton
            // 
            this.ProcessKillButton.Location = new System.Drawing.Point(5, 5);
            this.ProcessKillButton.Name = "ProcessKillButton";
            this.ProcessKillButton.Size = new System.Drawing.Size(90, 23);
            this.ProcessKillButton.TabIndex = 0;
            this.ProcessKillButton.Text = "终止进程";
            this.ProcessKillButton.UseVisualStyleBackColor = true;
            this.ProcessKillButton.Click += new System.EventHandler(this.ProcessKillButton_Click);
            // 
            // processGridView
            // 
            this.processGridView.AllowUserToAddRows = false;
            this.processGridView.AllowUserToDeleteRows = false;
            this.processGridView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.processGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.processGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.processGridView.Location = new System.Drawing.Point(3, 3);
            this.processGridView.Name = "processGridView";
            this.processGridView.ReadOnly = true;
            this.processGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.processGridView.Size = new System.Drawing.Size(886, 485);
            this.processGridView.TabIndex = 0;
            // 
            // tabPageDevice
            // 
            this.tabPageDevice.Controls.Add(this.deviceGridView);
            this.tabPageDevice.Location = new System.Drawing.Point(4, 25);
            this.tabPageDevice.Name = "tabPageDevice";
            this.tabPageDevice.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDevice.Size = new System.Drawing.Size(892, 491);
            this.tabPageDevice.TabIndex = 7;
            this.tabPageDevice.Text = "设备管理";
            this.tabPageDevice.UseVisualStyleBackColor = true;
            // 
            // deviceGridView
            // 
            this.deviceGridView.AllowUserToAddRows = false;
            this.deviceGridView.AllowUserToDeleteRows = false;
            this.deviceGridView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.deviceGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.deviceGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deviceGridView.Location = new System.Drawing.Point(3, 3);
            this.deviceGridView.Name = "deviceGridView";
            this.deviceGridView.ReadOnly = true;
            this.deviceGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.deviceGridView.Size = new System.Drawing.Size(886, 485);
            this.deviceGridView.TabIndex = 0;
            // 
            // tabPageRegistry
            // 
            this.tabPageRegistry.Controls.Add(this.registryGridView);
            this.tabPageRegistry.Controls.Add(this.registryTree);
            this.tabPageRegistry.Location = new System.Drawing.Point(4, 25);
            this.tabPageRegistry.Name = "tabPageRegistry";
            this.tabPageRegistry.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRegistry.Size = new System.Drawing.Size(892, 491);
            this.tabPageRegistry.TabIndex = 8;
            this.tabPageRegistry.Text = "注册表";
            this.tabPageRegistry.UseVisualStyleBackColor = true;
            // 
            // registryGridView
            // 
            this.registryGridView.AllowUserToAddRows = false;
            this.registryGridView.AllowUserToDeleteRows = false;
            this.registryGridView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.registryGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.registryGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.registryGridView.Location = new System.Drawing.Point(213, 3);
            this.registryGridView.Name = "registryGridView";
            this.registryGridView.ReadOnly = true;
            this.registryGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.registryGridView.Size = new System.Drawing.Size(676, 485);
            this.registryGridView.TabIndex = 1;
            // 
            // registryTree
            // 
            this.registryTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.registryTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.registryTree.Location = new System.Drawing.Point(3, 3);
            this.registryTree.Name = "registryTree";
            this.registryTree.Size = new System.Drawing.Size(210, 485);
            this.registryTree.TabIndex = 0;
            this.registryTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.RegistryTree_AfterSelect);
            this.registryTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.RegistryTree_BeforeExpand);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.flowLayoutPanelButtons);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 40;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(900, 40);
            this.panelTop.TabIndex = 1;
            // 
            // flowLayoutPanelButtons
            // 
            this.flowLayoutPanelButtons.Controls.Add(this.buttonRefresh);
            this.flowLayoutPanelButtons.Controls.Add(this.buttonRepairPerf);
            this.flowLayoutPanelButtons.Controls.Add(this.labelSeparator);
            this.flowLayoutPanelButtons.Controls.Add(this.buttonCamera);
            this.flowLayoutPanelButtons.Controls.Add(this.buttonScreenRecord);
            this.flowLayoutPanelButtons.Controls.Add(this.buttonScreenshot);
            this.flowLayoutPanelButtons.Controls.Add(this.buttonMyComputer);
            this.flowLayoutPanelButtons.Controls.Add(this.buttonDeviceManager);
            this.flowLayoutPanelButtons.Controls.Add(this.buttonServices);
            this.flowLayoutPanelButtons.Controls.Add(this.buttonTaskManager);
            this.flowLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelButtons.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flowLayoutPanelButtons.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
            this.flowLayoutPanelButtons.Size = new System.Drawing.Size(900, 40);
            this.flowLayoutPanelButtons.TabIndex = 0;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(3, 3);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(75, 30);
            this.buttonRefresh.TabIndex = 0;
            this.buttonRefresh.Text = "刷新";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // buttonRepairPerf
            // 
            this.buttonRepairPerf.Location = new System.Drawing.Point(84, 3);
            this.buttonRepairPerf.Name = "buttonRepairPerf";
            this.buttonRepairPerf.Size = new System.Drawing.Size(120, 30);
            this.buttonRepairPerf.TabIndex = 1;
            this.buttonRepairPerf.Text = "修复性能计数器";
            this.buttonRepairPerf.UseVisualStyleBackColor = true;
            this.buttonRepairPerf.Click += new System.EventHandler(this.buttonRepairPerf_Click);
            // 
            // labelSeparator
            // 
            this.labelSeparator.Location = new System.Drawing.Point(209, 0);
            this.labelSeparator.Name = "labelSeparator";
            this.labelSeparator.Size = new System.Drawing.Size(10, 36);
            this.labelSeparator.TabIndex = 2;
            this.labelSeparator.Text = "|";
            this.labelSeparator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonCamera
            // 
            this.buttonCamera.Location = new System.Drawing.Point(225, 3);
            this.buttonCamera.Name = "buttonCamera";
            this.buttonCamera.Size = new System.Drawing.Size(80, 30);
            this.buttonCamera.TabIndex = 3;
            this.buttonCamera.Text = "摄像头";
            this.buttonCamera.UseVisualStyleBackColor = true;
            this.buttonCamera.Click += new System.EventHandler(this.BtnOpenCamera_Click);
            // 
            // buttonScreenRecord
            // 
            this.buttonScreenRecord.Location = new System.Drawing.Point(311, 3);
            this.buttonScreenRecord.Name = "buttonScreenRecord";
            this.buttonScreenRecord.Size = new System.Drawing.Size(80, 30);
            this.buttonScreenRecord.TabIndex = 4;
            this.buttonScreenRecord.Text = "录屏";
            this.buttonScreenRecord.UseVisualStyleBackColor = true;
            this.buttonScreenRecord.Click += new System.EventHandler(this.BtnScreenRecord_Click);
            // 
            // buttonScreenshot
            // 
            this.buttonScreenshot.Location = new System.Drawing.Point(397, 3);
            this.buttonScreenshot.Name = "buttonScreenshot";
            this.buttonScreenshot.Size = new System.Drawing.Size(80, 30);
            this.buttonScreenshot.TabIndex = 5;
            this.buttonScreenshot.Text = "截图";
            this.buttonScreenshot.UseVisualStyleBackColor = true;
            this.buttonScreenshot.Click += new System.EventHandler(this.BtnScreenshot_Click);
            // 
            // buttonMyComputer
            // 
            this.buttonMyComputer.Location = new System.Drawing.Point(483, 3);
            this.buttonMyComputer.Name = "buttonMyComputer";
            this.buttonMyComputer.Size = new System.Drawing.Size(80, 30);
            this.buttonMyComputer.TabIndex = 6;
            this.buttonMyComputer.Text = "此电脑";
            this.buttonMyComputer.UseVisualStyleBackColor = true;
            this.buttonMyComputer.Click += new System.EventHandler(this.BtnOpenComputer_Click);
            // 
            // buttonDeviceManager
            // 
            this.buttonDeviceManager.Location = new System.Drawing.Point(569, 3);
            this.buttonDeviceManager.Name = "buttonDeviceManager";
            this.buttonDeviceManager.Size = new System.Drawing.Size(80, 30);
            this.buttonDeviceManager.TabIndex = 7;
            this.buttonDeviceManager.Text = "设备管理";
            this.buttonDeviceManager.UseVisualStyleBackColor = true;
            this.buttonDeviceManager.Click += new System.EventHandler(this.buttonDeviceManager_Click);
            // 
            // buttonServices
            // 
            this.buttonServices.Location = new System.Drawing.Point(655, 3);
            this.buttonServices.Name = "buttonServices";
            this.buttonServices.Size = new System.Drawing.Size(80, 30);
            this.buttonServices.TabIndex = 8;
            this.buttonServices.Text = "服务";
            this.buttonServices.UseVisualStyleBackColor = true;
            this.buttonServices.Click += new System.EventHandler(this.buttonServices_Click);
            // 
            // buttonTaskManager
            // 
            this.buttonTaskManager.Location = new System.Drawing.Point(741, 3);
            this.buttonTaskManager.Name = "buttonTaskManager";
            this.buttonTaskManager.Size = new System.Drawing.Size(90, 30);
            this.buttonTaskManager.TabIndex = 9;
            this.buttonTaskManager.Text = "任务管理器";
            this.buttonTaskManager.UseVisualStyleBackColor = true;
            this.buttonTaskManager.Click += new System.EventHandler(this.buttonTaskManager_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 560);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(900, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(37, 17);
            this.toolStripStatusLabel.Text = "就绪";
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 2000;
            this.refreshTimer.Tick += new System.EventHandler(this.RefreshTimer_Tick);
            // 
            // ComputerButler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 582);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.statusStrip);
            this.Name = "ComputerButler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "系统信息管理器";
            this.tabControl1.ResumeLayout(false);
            this.tabPageSystem.ResumeLayout(false);
            this.tabPageNetwork.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.portGridView)).EndInit();
            this.panelNetworkInfo.ResumeLayout(false);
            this.panelNetworkInfo.PerformLayout();
            this.tabPageCpu.ResumeLayout(false);
            this.panelCpuInfo.ResumeLayout(false);
            this.panelCpuInfo.PerformLayout();
            this.tabPageMemory.ResumeLayout(false);
            this.panelMemoryInfo.ResumeLayout(false);
            this.panelMemoryInfo.PerformLayout();
            this.tabPageDisk.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.diskGridView)).EndInit();
            this.tabPageService.ResumeLayout(false);

            this.panelServiceButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.serviceGridView)).EndInit();
            this.tabPageProcess.ResumeLayout(false);
            this.panelProcessButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.processGridView)).EndInit();
            this.tabPageDevice.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.deviceGridView)).EndInit();
            this.tabPageRegistry.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.registryGridView)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.flowLayoutPanelButtons.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageSystem;
        private System.Windows.Forms.RichTextBox systemInfoBox;
        private System.Windows.Forms.TabPage tabPageNetwork;
        private System.Windows.Forms.DataGridView portGridView;
        private System.Windows.Forms.Panel panelNetworkInfo;
        private System.Windows.Forms.TextBox macTextBox;
        private System.Windows.Forms.Label labelMac;
        private System.Windows.Forms.TextBox ipTextBox;
        private System.Windows.Forms.Label labelIp;
        private System.Windows.Forms.TabPage tabPageCpu;
        private System.Windows.Forms.Label cpuUsageLabel;
        private System.Windows.Forms.ProgressBar cpuUsageBar;
        private System.Windows.Forms.Label labelCpuUsage;
        private System.Windows.Forms.TextBox cpuLogicalTextBox;
        private System.Windows.Forms.Label labelLogicalProcessors;
        private System.Windows.Forms.TextBox cpuCoresTextBox;
        private System.Windows.Forms.Label labelCores;
        private System.Windows.Forms.TextBox cpuNameTextBox;
        private System.Windows.Forms.Label labelCpuName;
        private System.Windows.Forms.Panel panelCpuInfo;
        private System.Windows.Forms.TabPage tabPageMemory;
        private System.Windows.Forms.Label memoryUsageLabel;
        private System.Windows.Forms.ProgressBar memoryUsageBar;
        private System.Windows.Forms.Label labelMemoryUsage;
        private System.Windows.Forms.TextBox availableMemoryTextBox;
        private System.Windows.Forms.Label labelAvailableMemory;
        private System.Windows.Forms.TextBox usedMemoryTextBox;
        private System.Windows.Forms.Label labelUsedMemory;
        private System.Windows.Forms.TextBox totalMemoryTextBox;
        private System.Windows.Forms.Label labelTotalMemory;
        private System.Windows.Forms.Panel panelMemoryInfo;
        private System.Windows.Forms.TabPage tabPageDisk;
        private System.Windows.Forms.DataGridView diskGridView;
        private System.Windows.Forms.TabPage tabPageService;
        private System.Windows.Forms.Panel panelServiceButtons;
        private System.Windows.Forms.Button ServiceRefreshButton;
        private System.Windows.Forms.Button ServicePauseButton;
        private System.Windows.Forms.Button ServiceStopButton;
        private System.Windows.Forms.Button ServiceStartButton;
        private System.Windows.Forms.DataGridView serviceGridView;
        private System.Windows.Forms.TabPage tabPageProcess;
        private System.Windows.Forms.Panel panelProcessButtons;
        private System.Windows.Forms.Button ProcessRefreshButton;
        private System.Windows.Forms.Button ProcessKillButton;
        private System.Windows.Forms.DataGridView processGridView;
        private System.Windows.Forms.TabPage tabPageDevice;
        private System.Windows.Forms.DataGridView deviceGridView;
        private System.Windows.Forms.TabPage tabPageRegistry;
        private System.Windows.Forms.DataGridView registryGridView;
        private System.Windows.Forms.TreeView registryTree;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelButtons;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonRepairPerf;
        private System.Windows.Forms.Label labelSeparator;
        private System.Windows.Forms.Button buttonCamera;
        private System.Windows.Forms.Button buttonScreenRecord;
        private System.Windows.Forms.Button buttonScreenshot;
        private System.Windows.Forms.Button buttonMyComputer;
        private System.Windows.Forms.Button buttonDeviceManager;
        private System.Windows.Forms.Button buttonServices;
        private System.Windows.Forms.Button buttonTaskManager;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Timer refreshTimer;
    }
}