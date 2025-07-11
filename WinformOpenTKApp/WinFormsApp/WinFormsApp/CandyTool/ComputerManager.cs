using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.ServiceProcess;
using System.Text;
using Microsoft.Win32;

namespace WinFormsApp.CandyTool
{
    // 系统信息模型
    public class SystemInfo
    {
        public string OSVersion { get; set; }
        public string OSArchitecture { get; set; }
        public string ComputerName { get; set; }
        public string UserName { get; set; }
        public DateTime SystemUpTime { get; set; }
    }

    // 网络信息模型
    public class NetworkInfo
    {
        public string IPAddress { get; set; }
        public string MacAddress { get; set; }
        public List<PortInfo> OpenPorts { get; set; } = new List<PortInfo>();
    }

    // 端口信息模型
    public class PortInfo
    {
        public string Protocol { get; set; }
        public string LocalAddress { get; set; }
        public int LocalPort { get; set; }
        public string RemoteAddress { get; set; }
        public int RemotePort { get; set; }
        public string State { get; set; }
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
    }

    // CPU信息模型
    public class CpuInfo
    {
        public string Name { get; set; }
        public int Cores { get; set; }
        public int LogicalProcessors { get; set; }
    }

    // 内存信息模型
    public class RamInfo
    {
        public long TotalMemory { get; set; }
        public long UsedMemory { get; set; }
        public long AvailableMemory { get; set; }
        public float UsagePercentage { get; set; }
    }

    // 磁盘信息模型
    public class DiskInfo
    {
        public string Name { get; set; }
        public string DriveFormat { get; set; }
        public long TotalSize { get; set; }
        public long UsedSpace { get; set; }
        public long AvailableFreeSpace { get; set; }
        public float UsagePercentage { get; set; }
    }

    // 设备信息模型
    public class DeviceInfo
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string DeviceId { get; set; }
        public string PnpDeviceId { get; set; }
        public string Description { get; set; }
    }

    // 服务信息模型
    public class ServiceInfo
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Status { get; set; }
        public string StartType { get; set; }
        public string Account { get; set; }
    }

    // 进程信息模型
    public class ProcessInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public long MemoryUsage { get; set; }
        public float CpuUsage { get; set; }
        public int ThreadsCount { get; set; }
        public DateTime? StartTime { get; set; }
    }

    // 注册表项模型
    public class RegistryItem
    {
        public string Key { get; set; }
        public string ValueName { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
    }

    // 硬件信息模型
    public class HardwareInfo
    {
        public CpuInfo Cpu { get; set; }
        public RamInfo Ram { get; set; }
        public List<DiskInfo> Disks { get; set; } = new List<DiskInfo>();
        public List<DeviceInfo> Devices { get; set; } = new List<DeviceInfo>();
    }

    // 性能监控服务
    public class PerformanceMonitor : IDisposable
    {
        private PerformanceCounter _cpuCounter;
        private PerformanceCounter _ramCounter;
        private ManagementObjectSearcher _cpuSearcher;
        private ManagementObjectSearcher _ramSearcher;
        private bool _useAlternativeMethod = false;
        private bool _isDisposed = false;

        public bool IsInitialized { get; private set; }

        public PerformanceMonitor()
        {
            try
            {
                InitializePerformanceCounters();
            }
            catch (Exception ex)
            {
                Logger.LogError($"初始化性能计数器失败: {ex.Message}");
                Logger.LogInfo("尝试使用替代方法初始化性能监控...");

                try
                {
                    InitializeAlternativePerformanceMonitoring();
                }
                catch (Exception altEx)
                {
                    Logger.LogError($"初始化替代性能监控方法失败: {altEx.Message}");
                    IsInitialized = false;
                }
            }
        }

        private void InitializePerformanceCounters()
        {
            // 检查处理器性能计数器
            if (PerformanceCounterCategory.Exists("Processor") &&
                PerformanceCounterCategory.CounterExists("% Processor Time", "Processor"))
            {
                _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                _cpuCounter.NextValue(); // 初始化计数器
            }
            else
            {
                throw new InvalidOperationException("处理器性能计数器不可用");
            }

            // 检查内存性能计数器
            if (PerformanceCounterCategory.Exists("Memory") &&
                PerformanceCounterCategory.CounterExists("Available MBytes", "Memory"))
            {
                _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            }
            else
            {
                throw new InvalidOperationException("内存性能计数器不可用");
            }

            IsInitialized = true;
            Logger.LogInfo("性能计数器初始化成功");
        }

        private void InitializeAlternativePerformanceMonitoring()
        {
            // 使用WMI作为替代方法
            _cpuSearcher = new ManagementObjectSearcher("SELECT PercentProcessorTime FROM Win32_Processor");
            _ramSearcher = new ManagementObjectSearcher("SELECT FreePhysicalMemory, TotalVisibleMemorySize FROM Win32_OperatingSystem");

            _useAlternativeMethod = true;
            IsInitialized = true;
            Logger.LogInfo("已使用WMI替代方法初始化性能监控");
        }

        public float GetCpuUsage()
        {
            if (!IsInitialized) return 0;

            try
            {
                if (_useAlternativeMethod)
                {
                    float totalUsage = 0;
                    int counter = 0;

                    foreach (ManagementObject obj in _cpuSearcher.Get())
                    {
                        totalUsage += Convert.ToSingle(obj["PercentProcessorTime"]);
                        counter++;
                    }

                    return counter > 0 ? totalUsage / counter : 0;
                }
                else
                {
                    return _cpuCounter.NextValue();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"获取CPU使用率失败: {ex.Message}");
                return 0;
            }
        }

        public long GetAvailableMemory()
        {
            if (!IsInitialized) return 0;

            try
            {
                if (_useAlternativeMethod)
                {
                    foreach (ManagementObject obj in _ramSearcher.Get())
                    {
                        return Convert.ToInt64(obj["FreePhysicalMemory"]) * 1024; // 转换为字节
                    }
                    return 0;
                }
                else
                {
                    return (long)_ramCounter.NextValue() * 1024 * 1024; // 转换为字节
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"获取可用内存失败: {ex.Message}");
                return 0;
            }
        }

        public long GetTotalMemory()
        {
            if (!IsInitialized || !_useAlternativeMethod) return 0;

            try
            {
                foreach (ManagementObject obj in _ramSearcher.Get())
                {
                    return Convert.ToInt64(obj["TotalVisibleMemorySize"]) * 1024; // 转换为字节
                }
                return 0;
            }
            catch (Exception ex)
            {
                Logger.LogError($"获取总内存失败: {ex.Message}");
                return 0;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _cpuCounter?.Dispose();
                    _ramCounter?.Dispose();
                    _cpuSearcher?.Dispose();
                    _ramSearcher?.Dispose();
                }

                _isDisposed = true;
            }
        }

        public static void RepairPerformanceCounters()
        {
            try
            {
                using (Process process = new Process())
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        FileName = "cmd.exe",
                        Arguments = "/C lodctr /r",
                        Verb = "runas" // 请求管理员权限
                    };

                    process.StartInfo = startInfo;
                    process.Start();
                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        MessageBox.Show("性能计数器已成功修复。请重启计算机使更改生效。", "成功",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"修复性能计数器失败。退出代码: {process.ExitCode}", "错误",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"执行修复命令时发生错误: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    // 注册表管理器
    public class RegistryManager
    {
        public List<RegistryItem> GetRegistryItems(Microsoft.Win32.RegistryKey rootKey, string subKey)
        {
            var items = new List<RegistryItem>();

            try
            {
                using (var key = rootKey.OpenSubKey(subKey))
                {
                    if (key != null)
                    {
                        // 添加键值
                        foreach (var valueName in key.GetValueNames())
                        {
                            var value = key.GetValue(valueName);
                            var valueType = key.GetValueKind(valueName).ToString();

                            items.Add(new RegistryItem
                            {
                                Key = key.Name,
                                ValueName = valueName,
                                Value = value?.ToString() ?? "(默认)",
                                ValueType = valueType
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"获取注册表项失败: {ex.Message}");
                items.Add(new RegistryItem
                {
                    Key = subKey,
                    ValueName = "错误",
                    Value = ex.Message,
                    ValueType = "Error"
                });
            }

            return items;
        }
    }

    // 主管理器
    public class ComputerManager
    {
        private PerformanceMonitor _performanceMonitor;
        private RegistryManager _registryManager;

        public ComputerManager()
        {
            _performanceMonitor = new PerformanceMonitor();
            _registryManager = new RegistryManager();
        }

        public SystemInfo GetSystemInfo()
        {
            try
            {
                var info = new SystemInfo();

                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        info.OSVersion = obj["Caption"]?.ToString();
                        info.OSArchitecture = obj["OSArchitecture"]?.ToString();
                        info.ComputerName = Environment.MachineName;
                        info.UserName = Environment.UserName;

                        // 获取系统启动时间
                        if (obj["LastBootUpTime"] != null)
                        {
                            info.SystemUpTime = ManagementDateTimeConverter.ToDateTime(obj["LastBootUpTime"].ToString());
                        }
                    }
                }

                return info;
            }
            catch (Exception ex)
            {
                Logger.LogError($"获取系统信息失败: {ex.Message}");
                return null;
            }
        }

        public NetworkInfo GetNetworkInfo()
        {
            try
            {
                var info = new NetworkInfo();

                // 获取IP和MAC地址
                foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        var ipProps = nic.GetIPProperties();
                        foreach (var ip in ipProps.UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                info.IPAddress = ip.Address.ToString();
                                info.MacAddress = nic.GetPhysicalAddress().ToString();
                                break;
                            }
                        }

                        if (!string.IsNullOrEmpty(info.IPAddress))
                            break;
                    }
                }

                // 获取开放端口
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_IP4TCPConnection"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        try
                        {
                            var localAddress = obj["LocalAddress"]?.ToString();
                            var localPort = Convert.ToInt32(obj["LocalPort"]);
                            var remoteAddress = obj["RemoteAddress"]?.ToString();
                            var remotePort = Convert.ToInt32(obj["RemotePort"]);
                            var state = GetTcpState(Convert.ToInt32(obj["State"]));

                            // 获取进程ID
                            int processId = 0;
                            if (obj["OwningProcess"] != null)
                            {
                                processId = Convert.ToInt32(obj["OwningProcess"]);
                            }

                            // 获取进程名称
                            string processName = "未知";
                            try
                            {
                                if (processId > 0)
                                {
                                    using (var process = Process.GetProcessById(processId))
                                    {
                                        processName = process.ProcessName;
                                    }
                                }
                            }
                            catch { /* 忽略进程访问错误 */ }

                            info.OpenPorts.Add(new PortInfo
                            {
                                Protocol = "TCP",
                                LocalAddress = localAddress,
                                LocalPort = localPort,
                                RemoteAddress = remoteAddress,
                                RemotePort = remotePort,
                                State = state,
                                ProcessId = processId,
                                ProcessName = processName
                            });
                        }
                        catch { /* 忽略个别连接错误 */ }
                    }
                }

                return info;
            }
            catch (Exception ex)
            {
                Logger.LogError($"获取网络信息失败: {ex.Message}");
                return null;
            }
        }

        private string GetTcpState(int stateCode)
        {
            // 根据Windows TCP状态码映射到文本描述
            switch (stateCode)
            {
                case 1: return "CLOSED";
                case 2: return "LISTEN";
                case 3: return "SYN-SENT";
                case 4: return "SYN-RECEIVED";
                case 5: return "ESTABLISHED";
                case 6: return "FIN-WAIT-1";
                case 7: return "FIN-WAIT-2";
                case 8: return "CLOSE-WAIT";
                case 9: return "CLOSING";
                case 10: return "LAST-ACK";
                case 11: return "TIME-WAIT";
                default: return "UNKNOWN";
            }
        }

        public HardwareInfo GetHardwareInfo()
        {
            try
            {
                var info = new HardwareInfo();

                // 获取CPU信息
                info.Cpu = new CpuInfo();
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        info.Cpu.Name = obj["Name"]?.ToString();
                        info.Cpu.Cores = Convert.ToInt32(obj["NumberOfCores"]);
                        info.Cpu.LogicalProcessors = Convert.ToInt32(obj["NumberOfLogicalProcessors"]);
                        break; // 只获取第一个处理器
                    }
                }

                // 获取内存信息
                info.Ram = new RamInfo();
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        info.Ram.TotalMemory = Convert.ToInt64(obj["TotalVisibleMemorySize"]) * 1024; // 转换为字节
                        info.Ram.AvailableMemory = Convert.ToInt64(obj["FreePhysicalMemory"]) * 1024; // 转换为字节
                        info.Ram.UsedMemory = info.Ram.TotalMemory - info.Ram.AvailableMemory;
                        info.Ram.UsagePercentage = (float)info.Ram.UsedMemory / info.Ram.TotalMemory * 100;
                        break;
                    }
                }

                // 获取磁盘信息
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk WHERE DriveType=3"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        var disk = new DiskInfo
                        {
                            Name = obj["Name"]?.ToString(),
                            DriveFormat = obj["FileSystem"]?.ToString(),
                            TotalSize = Convert.ToInt64(obj["Size"]),
                            AvailableFreeSpace = Convert.ToInt64(obj["FreeSpace"])
                        };

                        disk.UsedSpace = disk.TotalSize - disk.AvailableFreeSpace;
                        disk.UsagePercentage = (float)disk.UsedSpace / disk.TotalSize * 100;

                        info.Disks.Add(disk);
                    }
                }

                // 获取设备信息
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        try
                        {
                            info.Devices.Add(new DeviceInfo
                            {
                                Name = obj["Name"]?.ToString(),
                                Status = obj["Status"]?.ToString(),
                                DeviceId = obj["DeviceID"]?.ToString(),
                                PnpDeviceId = obj["PNPDeviceID"]?.ToString(),
                                Description = obj["Description"]?.ToString()
                            });
                        }
                        catch { /* 忽略个别设备错误 */ }
                    }
                }

                return info;
            }
            catch (Exception ex)
            {
                Logger.LogError($"获取硬件信息失败: {ex.Message}");
                return null;
            }
        }

        // ComputerManager 类中需要添加的方法
        public List<DeviceInfo> GetDevices()
        {
            List<DeviceInfo> devices = new List<DeviceInfo>();

            try
            {
                // 使用 WMI 查询获取所有设备
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity"))
                {
                    foreach (ManagementObject device in searcher.Get())
                    {
                        try
                        {
                            string status = "未知";
                            if (device["Status"] != null)
                                status = device["Status"].ToString();

                            string deviceId = device["DeviceID"]?.ToString() ?? "";
                            string pnpDeviceId = device["PNPDeviceID"]?.ToString() ?? "";
                            string name = device["Name"]?.ToString() ?? "";
                            string description = device["Description"]?.ToString() ?? "";

                            devices.Add(new DeviceInfo
                            {
                                Name = name,
                                Status = status,
                                DeviceId = deviceId,
                                PnpDeviceId = pnpDeviceId,
                                Description = description
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"获取设备信息失败: {ex.Message}");
                            // 忽略个别设备获取失败的情况，继续处理其他设备
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"获取设备列表失败: {ex.Message}");
            }

            return devices;
        }


        public List<ServiceInfo> GetServices()
        {
            try
            {
                var services = new List<ServiceInfo>();

                foreach (var service in ServiceController.GetServices())
                {
                    try
                    {
                        services.Add(new ServiceInfo
                        {
                            Name = service.ServiceName,
                            DisplayName = service.DisplayName,
                            Status = service.Status.ToString(),
                            StartType = GetServiceStartType(service),
                            Account = GetServiceAccount(service)
                        });
                    }
                    catch { /* 忽略无法访问的服务 */ }
                }

                return services;
            }
            catch (Exception ex)
            {
                Logger.LogError($"获取服务信息失败: {ex.Message}");
                return new List<ServiceInfo>();
            }
        }

        private string GetServiceStartType(ServiceController service)
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher(
                    $"SELECT StartMode FROM Win32_Service WHERE Name='{service.ServiceName}'"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        return obj["StartMode"]?.ToString();
                    }
                }

                return "未知";
            }
            catch
            {
                return "未知";
            }
        }

        private string GetServiceAccount(ServiceController service)
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher(
                    $"SELECT StartName FROM Win32_Service WHERE Name='{service.ServiceName}'"))
                {
                    foreach (var obj in searcher.Get())
                    {
                        return obj["StartName"]?.ToString();
                    }
                }

                return "未知";
            }
            catch
            {
                return "未知";
            }
        }

        public bool ControlService(string serviceName, ServiceControllerStatus desiredStatus)
        {
            try
            {
                using (var service = new ServiceController(serviceName))
                {
                    switch (desiredStatus)
                    {
                        case ServiceControllerStatus.Running:
                            if (service.Status != ServiceControllerStatus.Running &&
                                service.Status != ServiceControllerStatus.StartPending)
                            {
                                service.Start();
                                service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(30));
                            }
                            break;

                        case ServiceControllerStatus.Stopped:
                            if (service.Status != ServiceControllerStatus.Stopped &&
                                service.Status != ServiceControllerStatus.StopPending)
                            {
                                service.Stop();
                                service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(30));
                            }
                            break;

                        case ServiceControllerStatus.Paused:
                            if (service.Status == ServiceControllerStatus.Running)
                            {
                                service.Pause();
                                service.WaitForStatus(ServiceControllerStatus.Paused, TimeSpan.FromSeconds(30));
                            }
                            break;
                    }

                    return service.Status == desiredStatus;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"控制服务失败: {ex.Message}");
                return false;
            }
        }

        public List<ProcessInfo> GetProcesses()
        {
            try
            {
                var processes = new List<ProcessInfo>();

                foreach (var process in Process.GetProcesses())
                {
                    try
                    {
                        processes.Add(new ProcessInfo
                        {
                            Id = process.Id,
                            Name = process.ProcessName,
                            FilePath = GetProcessFilePath(process),
                            MemoryUsage = process.WorkingSet64,
                            CpuUsage = 0, // 将在后续计算
                            ThreadsCount = process.Threads.Count,
                            StartTime = process.StartTime
                        });
                    }
                    catch { /* 忽略无法访问的进程 */ }
                }

                // 计算CPU使用率（需要两次采样）
                var cpuTimestamps = new Dictionary<int, long>();
                foreach (var process in Process.GetProcesses())
                {
                    try
                    {
                        cpuTimestamps[process.Id] = process.TotalProcessorTime.Ticks;
                    }
                    catch { /* 忽略无法访问的进程 */ }
                }

                Thread.Sleep(500); // 等待一段时间进行第二次采样

                foreach (var process in Process.GetProcesses())
                {
                    try
                    {
                        if (cpuTimestamps.TryGetValue(process.Id, out long previousTicks))
                        {
                            long currentTicks = process.TotalProcessorTime.Ticks;
                            long elapsedTicks = currentTicks - previousTicks;

                            // 计算CPU使用率百分比
                            float cpuUsage = (float)elapsedTicks / (500 * TimeSpan.TicksPerMillisecond) * 100;

                            // 找到对应的进程信息并更新CPU使用率
                            var processInfo = processes.Find(p => p.Id == process.Id);
                            if (processInfo != null)
                            {
                                processInfo.CpuUsage = Math.Min(cpuUsage, 100); // 确保不超过100%
                            }
                        }
                    }
                    catch { /* 忽略无法访问的进程 */ }
                }

                return processes;
            }
            catch (Exception ex)
            {
                Logger.LogError($"获取进程信息失败: {ex.Message}");
                return new List<ProcessInfo>();
            }
        }

        private string GetProcessFilePath(Process process)
        {
            try
            {
                return process.MainModule.FileName;
            }
            catch
            {
                try
                {
                    using (var searcher = new ManagementObjectSearcher(
                        $"SELECT ExecutablePath FROM Win32_Process WHERE ProcessId={process.Id}"))
                    {
                        foreach (var obj in searcher.Get())
                        {
                            return obj["ExecutablePath"]?.ToString();
                        }
                    }

                    return "无法访问";
                }
                catch
                {
                    return "未知";
                }
            }
        }

        public bool TerminateProcess(int processId)
        {
            try
            {
                using (var process = Process.GetProcessById(processId))
                {
                    process.Kill();
                    process.WaitForExit(5000); // 等待最多5秒
                    return process.HasExited;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"终止进程失败: {ex.Message}");
                return false;
            }
        }

        public List<RegistryItem> GetRegistryItems(Microsoft.Win32.RegistryKey rootKey, string subKey)
        {
            return _registryManager.GetRegistryItems(rootKey, subKey);
        }

        public float GetCpuUsage()
        {
            return _performanceMonitor.GetCpuUsage();
        }

        public long GetAvailableMemory()
        {
            return _performanceMonitor.GetAvailableMemory();
        }

        public long GetTotalMemory()
        {
            return _performanceMonitor.GetTotalMemory();
        }
    }


    // 注册表节点信息类
    public class RegistryNode
    {
        public Microsoft.Win32.RegistryKey RootKey { get; set; }
        public string Path { get; set; }
    }



    // 日志记录器
    public static class Logger
    {
        public static void LogInfo(string message)
        {
            Console.WriteLine($"[INFO] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            // 这里可以添加文件日志记录等功能
        }

        public static void LogWarning(string message)
        {
            Console.WriteLine($"[WARNING] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            // 这里可以添加文件日志记录等功能
        }

        public static void LogError(string message)
        {
            Console.WriteLine($"[ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            // 这里可以添加文件日志记录等功能
        }
    }
}
