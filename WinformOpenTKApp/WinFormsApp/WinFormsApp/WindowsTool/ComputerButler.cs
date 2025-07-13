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

namespace WinFormsApp.WindowsTool
{
    public partial class ComputerButler : Form
    {
        private ComputerManager manager;
        private bool[] dataLoaded; // 记录每个选项卡的数据是否已加载

        public ComputerButler()
        {
            InitializeComponent();
            manager = new ComputerManager();
            InitializeRefreshTimer();
            dataLoaded = new bool[tabControl1.TabCount];

            // 初始化所有DataGridView控件的列
            InitializeGridViewColumns();

            LoadCurrentTabData();
        }

        private void InitializeGridViewColumns()
        {
            // 端口信息表格
            InitializePortGridViewColumns();

            // 磁盘信息表格
            InitializeDiskGridViewColumns();

            // 服务信息表格
            InitializeServiceGridViewColumns();

            // 进程信息表格
            InitializeProcessGridViewColumns();

            // 设备信息表格
            InitializeDeviceGridViewColumns();

            // 注册表信息表格
            InitializeRegistryGridViewColumns();
        }

        private void InitializePortGridViewColumns()
        {
            portGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Protocol", HeaderText = "协议" });
            portGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "LocalAddress", HeaderText = "本地地址" });
            portGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "LocalPort", HeaderText = "本地端口" });
            portGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "RemoteAddress", HeaderText = "远程地址" });
            portGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "RemotePort", HeaderText = "远程端口" });
            portGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "State", HeaderText = "状态" });
            portGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProcessId", HeaderText = "进程ID" });
            portGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "ProcessName", HeaderText = "进程名称" });
        }

        private void InitializeDiskGridViewColumns()
        {
            diskGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Name", HeaderText = "磁盘名称" });
            diskGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "DriveFormat", HeaderText = "文件系统" });
            diskGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalSize", HeaderText = "总大小" });
            diskGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "UsedSpace", HeaderText = "已用空间" });
            diskGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "AvailableFreeSpace", HeaderText = "可用空间" });
            diskGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "UsagePercentage", HeaderText = "使用率" });
        }

        private void InitializeServiceGridViewColumns()
        {
            serviceGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Name", HeaderText = "服务名称" });
            serviceGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "DisplayName", HeaderText = "显示名称" });
            serviceGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "状态" });
            serviceGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "StartType", HeaderText = "启动类型" });
            serviceGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Account", HeaderText = "运行账户" });
        }

        private void InitializeProcessGridViewColumns()
        {
            processGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "进程ID" });
            processGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Name", HeaderText = "进程名称" });
            processGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "FilePath", HeaderText = "文件路径" });
            processGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "MemoryUsage", HeaderText = "内存使用" });
            processGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "CpuUsage", HeaderText = "CPU使用率" });
            processGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "ThreadsCount", HeaderText = "线程数" });
            processGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "StartTime", HeaderText = "启动时间" });
        }

        private void InitializeDeviceGridViewColumns()
        {
            deviceGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Name", HeaderText = "设备名称" });
            deviceGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "状态" });
            deviceGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "DeviceId", HeaderText = "设备ID" });
            deviceGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "PnpDeviceId", HeaderText = "PNP设备ID" });
            deviceGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Description", HeaderText = "描述" });
        }

        private void InitializeRegistryGridViewColumns()
        {
            registryGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Key", HeaderText = "键" });
            registryGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "ValueName", HeaderText = "值名称" });
            registryGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Value", HeaderText = "值" });
            registryGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "ValueType", HeaderText = "值类型" });
        }

        private void InitializeRefreshTimer()
        {
            refreshTimer = new Timer();
            refreshTimer.Interval = 2000;
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();
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
                    LoadProcessInfo();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"终止进程时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
            if (selectedIndex < 0 || selectedIndex >= dataLoaded.Length)
            {
                return;
            }

            if (dataLoaded[selectedIndex])
                return;

            try
            {
                switch (selectedIndex)
                {
                    case 0: LoadSystemInfo(); break;
                    case 1: LoadNetworkInfo(); break;
                    case 2: LoadCpuInfo(); break;
                    case 3: LoadMemoryInfo(); break;
                    case 4: LoadDiskInfo(); break;
                    case 5: LoadServiceInfo(); break;
                    case 6: LoadProcessInfo(); break;
                    case 7: LoadDeviceInfo(); break;
                    case 8: InitializeRegistryTreeStructure(); break;
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

        private void ServiceStartButton_Click(object sender, EventArgs e)
        {
            ControlSelectedService(ServiceControllerStatus.Running);
        }

        private void ServiceStopButton_Click(object sender, EventArgs e)
        {
            ControlSelectedService(ServiceControllerStatus.Stopped);
        }

        private void ServicePauseButton_Click(object sender, EventArgs e)
        {
            ControlSelectedService(ServiceControllerStatus.Paused);
        }

        private void ServiceRefreshButton_Click(object sender, EventArgs e)
        {
            LoadServiceInfo();
        }

        private void ProcessKillButton_Click(object sender, EventArgs e)
        {
            TerminateSelectedProcess();
        }

        private void ProcessRefreshButton_Click(object sender, EventArgs e)
        {
            LoadProcessInfo();
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

        // 修复性能计数器按钮点击事件
        private void buttonRepairPerf_Click(object sender, EventArgs e)
        {
            try
            {
                // 显示操作提示
                toolStripStatusLabel.Text = "正在修复性能计数器...";
                statusStrip.Refresh();

                // 创建并配置进程启动信息
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/C lodctr /r",
                    Verb = "runas",           // 请求管理员权限
                    UseShellExecute = true,
                    CreateNoWindow = true
                };

                // 启动进程执行命令
                Process.Start(startInfo);

                toolStripStatusLabel.Text = "已请求修复性能计数器，请查看系统提示";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"无法修复性能计数器: {ex.Message}",
                    "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "修复性能计数器失败";
            }
        }

        // 设备管理器按钮点击事件
        private void buttonDeviceManager_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("devmgmt.msc");
                toolStripStatusLabel.Text = "已启动设备管理器";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"无法启动设备管理器: {ex.Message}",
                    "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "启动设备管理器失败";
            }
        }

        // 服务管理器按钮点击事件
        private void buttonServices_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("services.msc");
                toolStripStatusLabel.Text = "已启动服务管理器";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"无法启动服务管理器: {ex.Message}",
                    "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "启动服务管理器失败";
            }
        }

        // 任务管理器按钮点击事件
        private void buttonTaskManager_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("taskmgr.exe");
                toolStripStatusLabel.Text = "已启动任务管理器";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"无法启动任务管理器: {ex.Message}",
                    "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                toolStripStatusLabel.Text = "启动任务管理器失败";
            }
        }
    }

    public class RegistryNode
    {
        public Microsoft.Win32.RegistryKey RootKey { get; set; }
        public string Path { get; set; }
    }
}