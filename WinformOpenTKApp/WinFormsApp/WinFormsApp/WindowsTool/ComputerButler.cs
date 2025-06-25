using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp.CandyTool;
using Timer = System.Windows.Forms.Timer;
using System.ServiceProcess;

namespace WinFormsApp.WindowsTool
{
    public partial class ComputerButler : Form
    {
        private ComputerManager manager;
        private Timer refreshTimer;
        private RichTextBox systemInfoBox;
        private TextBox ipTextBox;
        private TextBox macTextBox;
        private DataGridView portGridView;
        private TextBox cpuNameTextBox;
        private TextBox cpuCoresTextBox;
        private TextBox cpuLogicalTextBox;
        private ProgressBar cpuUsageBar;
        private Label cpuUsageLabel;
        private TextBox totalMemoryTextBox;
        private TextBox usedMemoryTextBox;
        private TextBox availableMemoryTextBox;
        private ProgressBar memoryUsageBar;
        private Label memoryUsageLabel;
        private DataGridView diskGridView;
        private DataGridView serviceGridView;
        private DataGridView processGridView;
        private DataGridView deviceGridView;
        private TreeView registryTree;
        private DataGridView registryGridView;
        private TabControl tabControl1;
        private bool[] dataLoaded; // 记录每个选项卡的数据是否已加载

        public ComputerButler()
        {
            InitializeComponent();
            InitializeUI();
            manager = new ComputerManager();
            InitializeRefreshTimer();
        }

        private void InitializeUI()
        {
            this.SuspendLayout();
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "MainForm";
            this.ResumeLayout(false);
            // 设置窗体样式
            this.Text = "系统信息管理器";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // 顶部按钮面板
            Panel topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = SystemColors.Control
            };
            Controls.Add(topPanel);

            // 按钮容器 - 使用FlowLayoutPanel实现自动排列
            FlowLayoutPanel buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = false,
                WrapContents = false,
                Padding = new Padding(5, 5, 5, 5)
            };
            topPanel.Controls.Add(buttonPanel);

            // 刷新按钮
            Button refreshButton = new Button
            {
                Text = "刷新",
                Size = new Size(75, 30),
                Margin = new Padding(2, 0, 2, 0)
            };
            refreshButton.Click += RefreshButton_Click;
            buttonPanel.Controls.Add(refreshButton);

            // 修复性能计数器按钮
            Button repairPerfCountersButton = new Button
            {
                Text = "修复性能计数器",
                Size = new Size(120, 30),
                Margin = new Padding(2, 0, 2, 0)
            };
            repairPerfCountersButton.Click += (sender, e) => PerformanceMonitor.RepairPerformanceCounters();
            buttonPanel.Controls.Add(repairPerfCountersButton);

            // 分隔线
            Label separator = new Label
            {
                Text = "|",
                Size = new Size(10, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(5, 0, 5, 0),

            };
            buttonPanel.Controls.Add(separator);

            // 系统工具按钮
            AddSystemToolButtons(buttonPanel);

            // 选项卡控件
            tabControl1 = new TabControl
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 40),
                Font = new Font("Segoe UI", 9F),
                Size = new Size(500, 20),
                //Margin = new Padding(50, 50, 50,50),
                Padding = new Point(10, 50)

            };
            Controls.Add(tabControl1);

            // 添加选项卡
            tabControl1.TabPages.Add("系统信息");
            tabControl1.TabPages.Add("网络信息");
            tabControl1.TabPages.Add("CPU信息");
            tabControl1.TabPages.Add("内存信息");
            tabControl1.TabPages.Add("磁盘信息");
            tabControl1.TabPages.Add("服务管理");
            tabControl1.TabPages.Add("进程管理");
            tabControl1.TabPages.Add("设备管理");
            tabControl1.TabPages.Add("注册表");

            // 初始化选项卡内容
            InitializeAllTabsControls();

            // 添加状态栏
            StatusStrip statusStrip = new StatusStrip();
            ToolStripStatusLabel statusLabel = new ToolStripStatusLabel("就绪");
            statusStrip.Items.Add(statusLabel);
            Controls.Add(statusStrip);

            // 初始化数据加载状态
            dataLoaded = new bool[tabControl1.TabCount];

            // 添加选项卡切换事件
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;

            // 加载当前选项卡数据
            LoadCurrentTabData();
        }

        private void AddSystemToolButtons(FlowLayoutPanel panel)
        {
            // 摄像头按钮
            Button btnOpenCamera = new Button
            {
                Text = "摄像头",
                Size = new Size(80, 30),
                Margin = new Padding(2, 0, 2, 0)
            };
            btnOpenCamera.Click += BtnOpenCamera_Click;
            panel.Controls.Add(btnOpenCamera);

            // 屏幕录制按钮
            Button btnScreenRecord = new Button
            {
                Text = "录屏",
                Size = new Size(80, 30),
                Margin = new Padding(2, 0, 2, 0)
            };
            btnScreenRecord.Click += BtnScreenRecord_Click;
            panel.Controls.Add(btnScreenRecord);

            // 截图按钮
            Button btnScreenshot = new Button
            {
                Text = "截图",
                Size = new Size(80, 30),
                Margin = new Padding(2, 0, 2, 0)
            };
            btnScreenshot.Click += BtnScreenshot_Click;
            panel.Controls.Add(btnScreenshot);

            // 打开此电脑按钮
            Button btnOpenComputer = new Button
            {
                Text = "此电脑",
                Size = new Size(80, 30),
                Margin = new Padding(2, 0, 2, 0)
            };
            btnOpenComputer.Click += BtnOpenComputer_Click;
            panel.Controls.Add(btnOpenComputer);

            // 设备管理器按钮
            Button btnDeviceManager = new Button
            {
                Text = "设备管理",
                Size = new Size(80, 30),
                Margin = new Padding(2, 0, 2, 0)
            };
            btnDeviceManager.Click += (sender, e) => Process.Start("devmgmt.msc");
            panel.Controls.Add(btnDeviceManager);

            // 服务按钮
            Button btnServices = new Button
            {
                Text = "服务",
                Size = new Size(80, 30),
                Margin = new Padding(2, 0, 2, 0)
            };
            btnServices.Click += (sender, e) => Process.Start("services.msc");
            panel.Controls.Add(btnServices);

            // 任务管理器按钮
            Button btnTaskManager = new Button
            {
                Text = "任务管理器",
                Size = new Size(90, 30),
                Margin = new Padding(2, 0, 2, 0)
            };
            btnTaskManager.Click += (sender, e) => Process.Start("taskmgr.exe");
            panel.Controls.Add(btnTaskManager);
        }

        private void InitializeAllTabsControls()
        {
            InitializeSystemTabControls();
            InitializeNetworkTabControls();
            InitializeCpuTabControls();
            InitializeMemoryTabControls();
            InitializeDiskTabControls();
            InitializeServiceTabControls();
            InitializeProcessTabControls();
            InitializeDeviceTabControls();
            InitializeRegistryTabControls();
        }

        private void InitializeSystemTabControls()
        {
            TabPage systemTab = tabControl1.TabPages[0];
            systemTab.Size = new Size(880, 500);
            systemTab.Controls.Clear();

            systemInfoBox = new RichTextBox
            {
                Location = new Point(10, 10),
                Size = new Size(systemTab.Width - 20, systemTab.Height - 20),
                ReadOnly = true,
                Font = new Font("Consolas", 10F),
                BorderStyle = BorderStyle.FixedSingle
            };
            systemTab.Controls.Add(systemInfoBox);
        }

        private void InitializeNetworkTabControls()
        {
            TabPage networkTab = tabControl1.TabPages[1];
            networkTab.Size = new Size(880, 500);
            networkTab.Controls.Clear();

            // 信息面板
            Panel infoPanel = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(networkTab.Width - 20, 50),
                BorderStyle = BorderStyle.FixedSingle
            };
            networkTab.Controls.Add(infoPanel);

            Label ipLabel = new Label
            {
                Text = "IP地址:",
                Location = new Point(10, 15),
                Size = new Size(60, 20)
            };
            infoPanel.Controls.Add(ipLabel);

            ipTextBox = new TextBox
            {
                Location = new Point(80, 15),
                Size = new Size(150, 20),
                ReadOnly = true,
                BorderStyle = BorderStyle.FixedSingle
            };
            infoPanel.Controls.Add(ipTextBox);

            Label macLabel = new Label
            {
                Text = "MAC地址:",
                Location = new Point(240, 15),
                Size = new Size(60, 20)
            };
            infoPanel.Controls.Add(macLabel);

            macTextBox = new TextBox
            {
                Location = new Point(310, 15),
                Size = new Size(150, 20),
                ReadOnly = true,
                BorderStyle = BorderStyle.FixedSingle
            };
            infoPanel.Controls.Add(macTextBox);

            // 端口列表
            portGridView = new DataGridView
            {
                Location = new Point(10, 70),
                Size = new Size(networkTab.Width - 20, networkTab.Height - 80),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BorderStyle = BorderStyle.FixedSingle,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true
            };
            portGridView.Columns.Add("Protocol", "协议");
            portGridView.Columns.Add("LocalAddress", "本地地址");
            portGridView.Columns.Add("LocalPort", "本地端口");
            portGridView.Columns.Add("RemoteAddress", "远程地址");
            portGridView.Columns.Add("RemotePort", "远程端口");
            portGridView.Columns.Add("State", "状态");
            portGridView.Columns.Add("ProcessId", "进程ID");
            portGridView.Columns.Add("ProcessName", "进程名称");
            networkTab.Controls.Add(portGridView);
        }

        private void InitializeCpuTabControls()
        {
            TabPage cpuTab = tabControl1.TabPages[2];
            cpuTab.Size = new Size(880, 500);
            cpuTab.Controls.Clear();

            // 信息面板
            Panel infoPanel = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(cpuTab.Width - 20, 100),
                BorderStyle = BorderStyle.FixedSingle
            };
            cpuTab.Controls.Add(infoPanel);

            Label cpuNameLabel = new Label
            {
                Text = "CPU名称:",
                Location = new Point(10, 15),
                Size = new Size(60, 20)
            };
            infoPanel.Controls.Add(cpuNameLabel);

            cpuNameTextBox = new TextBox
            {
                Location = new Point(80, 15),
                Size = new Size(300, 20),
                ReadOnly = true,
                BorderStyle = BorderStyle.FixedSingle
            };
            infoPanel.Controls.Add(cpuNameTextBox);

            Label cpuCoresLabel = new Label
            {
                Text = "核心数:",
                Location = new Point(10, 45),
                Size = new Size(60, 20)
            };
            infoPanel.Controls.Add(cpuCoresLabel);

            cpuCoresTextBox = new TextBox
            {
                Location = new Point(80, 45),
                Size = new Size(100, 20),
                ReadOnly = true,
                BorderStyle = BorderStyle.FixedSingle
            };
            infoPanel.Controls.Add(cpuCoresTextBox);

            Label cpuLogicalLabel = new Label
            {
                Text = "逻辑处理器:",
                Location = new Point(200, 45),
                Size = new Size(90, 20)
            };
            infoPanel.Controls.Add(cpuLogicalLabel);

            cpuLogicalTextBox = new TextBox
            {
                Location = new Point(300, 45),
                Size = new Size(100, 20),
                ReadOnly = true,
                BorderStyle = BorderStyle.FixedSingle
            };
            infoPanel.Controls.Add(cpuLogicalTextBox);

            // CPU使用率图表
            Label cpuUsageTitleLabel = new Label
            {
                Text = "CPU使用率:",
                Location = new Point(10, 75),
                Size = new Size(80, 20)
            };
            infoPanel.Controls.Add(cpuUsageTitleLabel);

            cpuUsageBar = new ProgressBar
            {
                Location = new Point(100, 75),
                Size = new Size(cpuTab.Width - 130, 20),
                Maximum = 100,
                Minimum = 0
            };
            infoPanel.Controls.Add(cpuUsageBar);

            cpuUsageLabel = new Label
            {
                Text = "0%",
                Location = new Point(cpuTab.Width - 50, 75),
                Size = new Size(40, 20),
                TextAlign = ContentAlignment.MiddleRight
            };
            infoPanel.Controls.Add(cpuUsageLabel);
        }

        private void InitializeMemoryTabControls()
        {
            TabPage memoryTab = tabControl1.TabPages[3];
            memoryTab.Size = new Size(880, 500);
            memoryTab.Controls.Clear();

            // 信息面板
            Panel infoPanel = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(memoryTab.Width - 20, 130),
                BorderStyle = BorderStyle.FixedSingle
            };
            memoryTab.Controls.Add(infoPanel);

            Label totalMemoryLabel = new Label
            {
                Text = "总内存:",
                Location = new Point(10, 15),
                Size = new Size(60, 20)
            };
            infoPanel.Controls.Add(totalMemoryLabel);

            totalMemoryTextBox = new TextBox
            {
                Location = new Point(80, 15),
                Size = new Size(200, 20),
                ReadOnly = true,
                BorderStyle = BorderStyle.FixedSingle
            };
            infoPanel.Controls.Add(totalMemoryTextBox);

            Label usedMemoryLabel = new Label
            {
                Text = "已用内存:",
                Location = new Point(10, 45),
                Size = new Size(60, 20)
            };
            infoPanel.Controls.Add(usedMemoryLabel);

            usedMemoryTextBox = new TextBox
            {
                Location = new Point(80, 45),
                Size = new Size(200, 20),
                ReadOnly = true,
                BorderStyle = BorderStyle.FixedSingle
            };
            infoPanel.Controls.Add(usedMemoryTextBox);

            Label availableMemoryLabel = new Label
            {
                Text = "可用内存:",
                Location = new Point(10, 75),
                Size = new Size(60, 20)
            };
            infoPanel.Controls.Add(availableMemoryLabel);

            availableMemoryTextBox = new TextBox
            {
                Location = new Point(80, 75),
                Size = new Size(200, 20),
                ReadOnly = true,
                BorderStyle = BorderStyle.FixedSingle
            };
            infoPanel.Controls.Add(availableMemoryTextBox);

            // 内存使用率图表
            Label memoryUsageTitleLabel = new Label
            {
                Text = "内存使用率:",
                Location = new Point(10, 105),
                Size = new Size(80, 20)
            };
            infoPanel.Controls.Add(memoryUsageTitleLabel);

            memoryUsageBar = new ProgressBar
            {
                Location = new Point(100, 105),
                Size = new Size(memoryTab.Width - 130, 20),
                Maximum = 100,
                Minimum = 0
            };
            infoPanel.Controls.Add(memoryUsageBar);

            memoryUsageLabel = new Label
            {
                Text = "0%",
                Location = new Point(memoryTab.Width - 50, 105),
                Size = new Size(40, 20),
                TextAlign = ContentAlignment.MiddleRight
            };
            infoPanel.Controls.Add(memoryUsageLabel);
        }

        private void InitializeDiskTabControls()
        {
            TabPage diskTab = tabControl1.TabPages[4];
            diskTab.Size = new Size(880, 500);
            diskTab.Controls.Clear();

            diskGridView = new DataGridView
            {
                Location = new Point(10, 10),
                Size = new Size(diskTab.Width - 20, diskTab.Height - 20),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BorderStyle = BorderStyle.FixedSingle,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true
            };
            diskGridView.Columns.Add("Name", "驱动器");
            diskGridView.Columns.Add("DriveFormat", "格式");
            diskGridView.Columns.Add("TotalSize", "总大小");
            diskGridView.Columns.Add("UsedSpace", "已用空间");
            diskGridView.Columns.Add("AvailableFreeSpace", "可用空间");
            diskGridView.Columns.Add("UsagePercentage", "使用率");
            diskTab.Controls.Add(diskGridView);
        }

        private void InitializeServiceTabControls()
        {
            TabPage serviceTab = tabControl1.TabPages[5];
            serviceTab.Size = new Size(880, 500);
            serviceTab.Controls.Clear();

            serviceGridView = new DataGridView
            {
                Location = new Point(10, 10),
                Size = new Size(serviceTab.Width - 20, serviceTab.Height - 60),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BorderStyle = BorderStyle.FixedSingle,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true
            };
            serviceGridView.Columns.Add("Name", "服务名称");
            serviceGridView.Columns.Add("DisplayName", "显示名称");
            serviceGridView.Columns.Add("Status", "状态");
            serviceGridView.Columns.Add("StartType", "启动类型");
            serviceGridView.Columns.Add("Account", "账户");
            serviceTab.Controls.Add(serviceGridView);

            // 服务控制按钮面板
            Panel buttonPanel = new Panel
            {
                Location = new Point(10, serviceTab.Height - 45),
                Size = new Size(serviceTab.Width - 20, 35),
                BorderStyle = BorderStyle.FixedSingle
            };
            serviceTab.Controls.Add(buttonPanel);

            Button startButton = new Button
            {
                Text = "启动",
                Location = new Point(5, 5),
                Size = new Size(75, 23)
            };
            startButton.Click += (sender, e) => ControlSelectedService(ServiceControllerStatus.Running);
            buttonPanel.Controls.Add(startButton);

            Button stopButton = new Button
            {
                Text = "停止",
                Location = new Point(85, 5),
                Size = new Size(75, 23)
            };
            stopButton.Click += (sender, e) => ControlSelectedService(ServiceControllerStatus.Stopped);
            buttonPanel.Controls.Add(stopButton);

            Button pauseButton = new Button
            {
                Text = "暂停",
                Location = new Point(165, 5),
                Size = new Size(75, 23)
            };
            pauseButton.Click += (sender, e) => ControlSelectedService(ServiceControllerStatus.Paused);
            buttonPanel.Controls.Add(pauseButton);

            Button refreshButton = new Button
            {
                Text = "刷新",
                Location = new Point(245, 5),
                Size = new Size(75, 23)
            };
            refreshButton.Click += (sender, e) => LoadServiceInfo();
            buttonPanel.Controls.Add(refreshButton);
        }

        private void InitializeProcessTabControls()
        {
            TabPage processTab = tabControl1.TabPages[6];
            processTab.Size = new Size(880, 500);
            processTab.Controls.Clear();

            processGridView = new DataGridView
            {
                Location = new Point(10, 10),
                Size = new Size(processTab.Width - 20, processTab.Height - 60),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BorderStyle = BorderStyle.FixedSingle,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true
            };
            processGridView.Columns.Add("Id", "进程ID");
            processGridView.Columns.Add("Name", "进程名称");
            processGridView.Columns.Add("FilePath", "文件路径");
            processGridView.Columns.Add("MemoryUsage", "内存使用");
            processGridView.Columns.Add("CpuUsage", "CPU使用");
            processGridView.Columns.Add("ThreadsCount", "线程数");
            processGridView.Columns.Add("StartTime", "启动时间");
            processTab.Controls.Add(processGridView);

            // 进程控制按钮面板
            Panel buttonPanel = new Panel
            {
                Location = new Point(10, processTab.Height - 45),
                Size = new Size(processTab.Width - 20, 35),
                BorderStyle = BorderStyle.FixedSingle
            };
            processTab.Controls.Add(buttonPanel);

            Button killButton = new Button
            {
                Text = "终止进程",
                Location = new Point(5, 5),
                Size = new Size(90, 23)
            };
            killButton.Click += (sender, e) => TerminateSelectedProcess();
            buttonPanel.Controls.Add(killButton);

            Button refreshButton = new Button
            {
                Text = "刷新",
                Location = new Point(100, 5),
                Size = new Size(75, 23)
            };
            refreshButton.Click += (sender, e) => LoadProcessInfo();
            buttonPanel.Controls.Add(refreshButton);
        }

        private void InitializeDeviceTabControls()
        {
            TabPage deviceTab = tabControl1.TabPages[7];
            deviceTab.Size = new Size(880, 500);
            deviceTab.Controls.Clear();

            deviceGridView = new DataGridView
            {
                Location = new Point(10, 10),
                Size = new Size(deviceTab.Width - 20, deviceTab.Height - 20),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BorderStyle = BorderStyle.FixedSingle,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true
            };
            deviceGridView.Columns.Add("Name", "设备名称");
            deviceGridView.Columns.Add("Status", "状态");
            deviceGridView.Columns.Add("DeviceId", "设备ID");
            deviceGridView.Columns.Add("PnpDeviceId", "PnP设备ID");
            deviceGridView.Columns.Add("Description", "描述");
            deviceTab.Controls.Add(deviceGridView);
        }

        private void InitializeRegistryTabControls()
        {
            TabPage registryTab = tabControl1.TabPages[8];
            registryTab.Size = new Size(880, 500);
            registryTab.Controls.Clear();

            // 注册表树
            registryTree = new TreeView
            {
                Location = new Point(10, 10),
                Size = new Size(200, registryTab.Height - 20),
                BorderStyle = BorderStyle.FixedSingle
            };
            registryTab.Controls.Add(registryTree);

            // 注册表内容表格
            registryGridView = new DataGridView
            {
                Location = new Point(220, 10),
                Size = new Size(registryTab.Width - 230, registryTab.Height - 20),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BorderStyle = BorderStyle.FixedSingle,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true
            };
            registryGridView.Columns.Add("Key", "键");
            registryGridView.Columns.Add("ValueName", "值名称");
            registryGridView.Columns.Add("Value", "值");
            registryGridView.Columns.Add("ValueType", "值类型");
            registryTab.Controls.Add(registryGridView);

            InitializeRegistryTreeStructure();
        }

        private void InitializeRegistryTreeStructure()
        {
            registryTree.Nodes.Clear();

            TreeNode hklmNode = new TreeNode("HKEY_LOCAL_MACHINE");
            hklmNode.Tag = new RegistryNode { RootKey = Microsoft.Win32.Registry.LocalMachine, Path = "" };
            registryTree.Nodes.Add(hklmNode);

            TreeNode hkcuNode = new TreeNode("HKEY_CURRENT_USER");
            hkcuNode.Tag = new RegistryNode { RootKey = Microsoft.Win32.Registry.CurrentUser, Path = "" };
            registryTree.Nodes.Add(hkcuNode);

            TreeNode hkcrNode = new TreeNode("HKEY_CLASSES_ROOT");
            hkcrNode.Tag = new RegistryNode { RootKey = Microsoft.Win32.Registry.ClassesRoot, Path = "" };
            registryTree.Nodes.Add(hkcrNode);

            TreeNode hkuNode = new TreeNode("HKEY_USERS");
            hkuNode.Tag = new RegistryNode { RootKey = Microsoft.Win32.Registry.Users, Path = "" };
            registryTree.Nodes.Add(hkuNode);

            TreeNode hkccNode = new TreeNode("HKEY_CURRENT_CONFIG");
            hkccNode.Tag = new RegistryNode { RootKey = Microsoft.Win32.Registry.CurrentConfig, Path = "" };
            registryTree.Nodes.Add(hkccNode);

            foreach (TreeNode node in registryTree.Nodes)
            {
                node.Nodes.Add("加载中...");
            }

            if (registryTree.Nodes.Count > 0)
            {
                registryTree.Nodes[0].Expand();
            }

            registryTree.BeforeExpand += RegistryTree_BeforeExpand;
            registryTree.AfterSelect += RegistryTree_AfterSelect;
        }

        private void RegistryTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count > 0 && e.Node.Nodes[0].Text == "加载中...")
            {
                e.Node.Nodes.Clear();

                var nodeInfo = e.Node.Tag as RegistryNode;
                if (nodeInfo != null)
                {
                    using (var key = nodeInfo.RootKey.OpenSubKey(nodeInfo.Path))
                    {
                        if (key != null)
                        {
                            foreach (var subKeyName in key.GetSubKeyNames())
                            {
                                TreeNode subNode = new TreeNode(subKeyName);
                                subNode.Tag = new RegistryNode
                                {
                                    RootKey = nodeInfo.RootKey,
                                    Path = string.IsNullOrEmpty(nodeInfo.Path) ? subKeyName : $"{nodeInfo.Path}\\{subKeyName}"
                                };

                                subNode.Nodes.Add("加载中...");
                                e.Node.Nodes.Add(subNode);
                            }
                        }
                    }
                }
            }
        }

        private void RegistryTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var nodeInfo = e.Node.Tag as RegistryNode;
            if (nodeInfo != null)
            {
                registryGridView.Rows.Clear();

                var registryItems = manager.GetRegistryItems(nodeInfo.RootKey, nodeInfo.Path);
                if (registryItems != null)
                {
                    foreach (var item in registryItems)
                    {
                        registryGridView.Rows.Add(
                            item.Key,
                            item.ValueName,
                            item.Value,
                            item.ValueType
                        );
                    }
                }
            }
        }

        private void LoadSystemInfo()
        {
            try
            {
                var systemInfo = manager.GetSystemInfo();
                if (systemInfo != null)
                {
                    StringBuilder info = new StringBuilder();
                    info.AppendLine($"操作系统: {systemInfo.OSVersion}");
                    info.AppendLine($"系统架构: {systemInfo.OSArchitecture}");
                    info.AppendLine($"计算机名: {systemInfo.ComputerName}");
                    info.AppendLine($"用户名: {systemInfo.UserName}");
                    info.AppendLine($"系统启动时间: {systemInfo.SystemUpTime.ToString("yyyy-MM-dd HH:mm:ss")}");
                    info.AppendLine($"运行时间: {DateTime.Now - systemInfo.SystemUpTime:dd\\.hh\\:mm\\:ss}");

                    systemInfoBox.Text = info.ToString();
                }
                else
                {
                    systemInfoBox.Text = "无法获取系统信息";
                }
            }
            catch (Exception ex)
            {
                systemInfoBox.Text = $"获取系统信息时出错: {ex.Message}";
            }
        }

        private void LoadNetworkInfo()
        {
            try
            {
                var networkInfo = manager.GetNetworkInfo();
                if (networkInfo != null)
                {
                    ipTextBox.Text = networkInfo.IPAddress;
                    macTextBox.Text = networkInfo.MacAddress;

                    portGridView.Rows.Clear();
                    foreach (var port in networkInfo.OpenPorts)
                    {
                        portGridView.Rows.Add(
                            port.Protocol,
                            port.LocalAddress,
                            port.LocalPort,
                            port.RemoteAddress,
                            port.RemotePort,
                            port.State,
                            port.ProcessId,
                            port.ProcessName
                        );
                    }
                }
                else
                {
                    ipTextBox.Text = "无法获取";
                    macTextBox.Text = "无法获取";
                    portGridView.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                ipTextBox.Text = "出错";
                macTextBox.Text = "出错";
                portGridView.Rows.Clear();
                MessageBox.Show($"获取网络信息时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCpuInfo()
        {
            try
            {
                var hardwareInfo = manager.GetHardwareInfo();
                if (hardwareInfo != null && hardwareInfo.Cpu != null)
                {
                    cpuNameTextBox.Text = hardwareInfo.Cpu.Name;
                    cpuCoresTextBox.Text = hardwareInfo.Cpu.Cores.ToString();
                    cpuLogicalTextBox.Text = hardwareInfo.Cpu.LogicalProcessors.ToString();
                }
                else
                {
                    cpuNameTextBox.Text = "无法获取";
                    cpuCoresTextBox.Text = "无法获取";
                    cpuLogicalTextBox.Text = "无法获取";
                }
            }
            catch (Exception ex)
            {
                cpuNameTextBox.Text = "出错";
                cpuCoresTextBox.Text = "出错";
                cpuLogicalTextBox.Text = "出错";
                MessageBox.Show($"获取CPU信息时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMemoryInfo()
        {
            try
            {
                var hardwareInfo = manager.GetHardwareInfo();
                if (hardwareInfo != null && hardwareInfo.Ram != null)
                {
                    totalMemoryTextBox.Text = FormatBytes(hardwareInfo.Ram.TotalMemory);
                    usedMemoryTextBox.Text = FormatBytes(hardwareInfo.Ram.UsedMemory);
                    availableMemoryTextBox.Text = FormatBytes(hardwareInfo.Ram.AvailableMemory);

                    memoryUsageBar.Value = (int)Math.Round(hardwareInfo.Ram.UsagePercentage);
                    memoryUsageLabel.Text = $"内存使用率: {hardwareInfo.Ram.UsagePercentage:F1}%";
                }
                else
                {
                    totalMemoryTextBox.Text = "无法获取";
                    usedMemoryTextBox.Text = "无法获取";
                    availableMemoryTextBox.Text = "无法获取";
                    memoryUsageBar.Value = 0;
                    memoryUsageLabel.Text = "内存使用率: 0%";
                }
            }
            catch (Exception ex)
            {
                totalMemoryTextBox.Text = "出错";
                usedMemoryTextBox.Text = "出错";
                availableMemoryTextBox.Text = "出错";
                memoryUsageBar.Value = 0;
                memoryUsageLabel.Text = "内存使用率: 0%";
                MessageBox.Show($"获取内存信息时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDiskInfo()
        {
            try
            {
                var hardwareInfo = manager.GetHardwareInfo();
                if (hardwareInfo != null && hardwareInfo.Disks != null)
                {
                    diskGridView.Rows.Clear();
                    foreach (var disk in hardwareInfo.Disks)
                    {
                        diskGridView.Rows.Add(
                            disk.Name,
                            disk.DriveFormat,
                            FormatBytes(disk.TotalSize),
                            FormatBytes(disk.UsedSpace),
                            FormatBytes(disk.AvailableFreeSpace),
                            $"{disk.UsagePercentage:F1}%"
                        );
                    }
                }
                else
                {
                    diskGridView.Rows.Clear();
                    diskGridView.Rows.Add("无法获取磁盘信息", "", "", "", "", "");
                }
            }
            catch (Exception ex)
            {
                diskGridView.Rows.Clear();
                diskGridView.Rows.Add($"获取磁盘信息时出错: {ex.Message}", "", "", "", "", "");
                MessageBox.Show($"获取磁盘信息时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadServiceInfo()
        {
            try
            {
                var services = manager.GetServices();
                if (services != null)
                {
                    serviceGridView.Rows.Clear();
                    foreach (var service in services)
                    {
                        serviceGridView.Rows.Add(
                            service.Name,
                            service.DisplayName,
                            service.Status,
                            service.StartType,
                            service.Account
                        );
                    }
                }
                else
                {
                    serviceGridView.Rows.Clear();
                    serviceGridView.Rows.Add("无法获取服务信息", "", "", "", "");
                }
            }
            catch (Exception ex)
            {
                serviceGridView.Rows.Clear();
                serviceGridView.Rows.Add($"获取服务信息时出错: {ex.Message}", "", "", "", "");
                MessageBox.Show($"获取服务信息时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadProcessInfo()
        {
            try
            {
                var processes = manager.GetProcesses();
                if (processes != null)
                {
                    processGridView.Rows.Clear();
                    foreach (var process in processes)
                    {
                        processGridView.Rows.Add(
                            process.Id,
                            process.Name,
                            process.FilePath,
                            FormatBytes(process.MemoryUsage),
                            $"{process.CpuUsage:F1}%",
                            process.ThreadsCount,
                            process.StartTime?.ToString("yyyy-MM-dd HH:mm:ss")
                        );
                    }
                }
                else
                {
                    processGridView.Rows.Clear();
                    processGridView.Rows.Add("无法获取进程信息", "", "", "", "", "", "");
                }
            }
            catch (Exception ex)
            {
                processGridView.Rows.Clear();
                processGridView.Rows.Add($"获取进程信息时出错: {ex.Message}", "", "", "", "", "", "");
                MessageBox.Show($"获取进程信息时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDeviceInfo()
        {
            try
            {
                var devices = manager.GetDevices();
                if (devices != null)
                {
                    deviceGridView.Rows.Clear();
                    foreach (var device in devices)
                    {
                        deviceGridView.Rows.Add(
                            device.Name,
                            device.Status,
                            device.DeviceId,
                            device.PnpDeviceId,
                            device.Description
                        );
                    }
                }
                else
                {
                    deviceGridView.Rows.Clear();
                    deviceGridView.Rows.Add("无法获取设备信息", "", "", "", "");
                }
            }
            catch (Exception ex)
            {
                deviceGridView.Rows.Clear();
                deviceGridView.Rows.Add($"获取设备信息时出错: {ex.Message}", "", "", "", "");
                MessageBox.Show($"获取设备信息时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ControlSelectedService(ServiceControllerStatus desiredStatus)
        {
            if (serviceGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择一个服务", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string serviceName = serviceGridView.SelectedRows[0].Cells["Name"].Value?.ToString();
            if (string.IsNullOrEmpty(serviceName))
            {
                MessageBox.Show("无法获取选中的服务名称", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (ServiceController service = new ServiceController(serviceName))
                {
                    switch (desiredStatus)
                    {
                        case ServiceControllerStatus.Running:
                            if (service.Status != ServiceControllerStatus.Running)
                            {
                                service.Start();
                                MessageBox.Show($"服务 '{serviceName}' 已启动", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show($"服务 '{serviceName}' 已经在运行", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;

                        case ServiceControllerStatus.Stopped:
                            if (service.Status != ServiceControllerStatus.Stopped)
                            {
                                service.Stop();
                                MessageBox.Show($"服务 '{serviceName}' 已停止", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show($"服务 '{serviceName}' 已经停止", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;

                        case ServiceControllerStatus.Paused:
                            if (service.Status == ServiceControllerStatus.Running)
                            {
                                service.Pause();
                                MessageBox.Show($"服务 '{serviceName}' 已暂停", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show($"服务 '{serviceName}' 当前状态不允许暂停", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            break;
                    }

                    // 刷新服务列表
                    LoadServiceInfo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"控制服务时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TerminateSelectedProcess()
        {
            if (processGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择一个进程", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string processName = processGridView.SelectedRows[0].Cells["Name"].Value?.ToString();
            int processId = 0;
            if (!int.TryParse(processGridView.SelectedRows[0].Cells["Id"].Value?.ToString(), out processId))
            {
                MessageBox.Show("无法获取选中的进程ID", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show($"确定要终止进程 '{processName}' (ID: {processId}) 吗？这可能会导致数据丢失或系统不稳定。",
                "确认终止", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    Process process = Process.GetProcessById(processId);
                    process.Kill();
                    MessageBox.Show($"进程 '{processName}' 已终止", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 刷新进程列表
                    LoadProcessInfo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"终止进程时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void InitializeRefreshTimer()
        {
            refreshTimer = new Timer(); // 1秒间隔
            refreshTimer.Interval = 2000;
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2) // CPU信息
            {
                try
                {
                    float cpuUsage = manager.GetCpuUsage();
                    cpuUsageBar.Value = (int)Math.Round(cpuUsage);
                    cpuUsageLabel.Text = $"{cpuUsage:F1}%";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"刷新CPU使用率失败: {ex.Message}");
                }
            }
            else if (tabControl1.SelectedIndex == 3) // 内存信息
            {
                try
                {
                    long availableMemory = manager.GetAvailableMemory();
                    long totalMemory = manager.GetTotalMemory();

                    if (totalMemory > 0)
                    {
                        long usedMemory = totalMemory - availableMemory;
                        float memoryUsagePercentage = (float)usedMemory / totalMemory * 100;

                        memoryUsageBar.Value = (int)Math.Round(memoryUsagePercentage);
                        memoryUsageLabel.Text = $"{memoryUsagePercentage:F1}%";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"刷新内存使用率失败: {ex.Message}");
                }
            }
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCurrentTabData();
        }

        private void LoadCurrentTabData()
        {
            int selectedIndex = tabControl1.SelectedIndex;
            if (selectedIndex < 0 || selectedIndex > dataLoaded.Length)
            {
                return;
            }

            if (dataLoaded[selectedIndex])
                return;

            try
            {
                switch (selectedIndex)
                {
                    case 0: // 系统信息
                        LoadSystemInfo();
                        break;
                    case 1: // 网络信息
                        LoadNetworkInfo();
                        break;
                    case 2: // CPU信息
                        LoadCpuInfo();
                        break;
                    case 3: // 内存信息
                        LoadMemoryInfo();
                        break;
                    case 4: // 磁盘信息
                        LoadDiskInfo();
                        break;
                    case 5: // 服务管理
                        LoadServiceInfo();
                        break;
                    case 6: // 进程管理
                        LoadProcessInfo();
                        break;
                    case 7: // 设备管理
                        LoadDeviceInfo();
                        break;
                    case 8: // 注册表
                            // 注册表数据在树节点选择时加载
                        break;
                }

                dataLoaded[selectedIndex] = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载选项卡数据时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = tabControl1.SelectedIndex;
            dataLoaded[selectedIndex] = false;
            LoadCurrentTabData();
        }

        private void BtnOpenCamera_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("start", "microsoft.windows.camera:");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打开摄像头失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnScreenRecord_Click(object sender, EventArgs e)
        {
            try
            {
                // 使用Win+G快捷键打开游戏栏录制功能
                SendKeys.SendWait("^{ESC}");
                Thread.Sleep(500);
                SendKeys.SendWait("{F9}");
                MessageBox.Show("开始录屏。按Win+Alt+R停止录制。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"开始录屏失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnScreenshot_Click(object sender, EventArgs e)
        {
            try
            {
                // 使用Win+Shift+S快捷键启动截图工具
                SendKeys.SendWait("+^{ESC}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"启动截图工具失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnOpenComputer_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打开计算机失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string FormatBytes(long bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            double size = bytes;

            while (size >= 1024 && order < suffixes.Length - 1)
            {
                order++;
                size /= 1024;
            }

            return $"{size:0.##} {suffixes[order]}";
        }



        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 清理资源
            if (refreshTimer != null)
            {
                refreshTimer.Stop();
                refreshTimer.Dispose();
            }

            base.OnFormClosing(e);
        }
    }

}
