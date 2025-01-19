using System.Management;
using System.Net;
using System.Net.NetworkInformation;

namespace WinFormsApp.CandyTool
{
    public class ComputerProperties
    {
        // 获取本地 IP 地址
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "No IPv4 address found";
        }

        // 获取 MAC 地址
        public static string GetMacAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface nic in nics)
            {
                if (nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    return nic.GetPhysicalAddress().ToString();
                }
            }
            return "No MAC address found";
        }

        // 获取 CPU 核数
        public static int GetCpuCores()
        {
            return Environment.ProcessorCount;
        }

        // 获取计算机名称
        public static string GetComputerName()
        {
            return Environment.MachineName;
        }

        // 获取操作系统名称
        public static string GetOperatingSystemName()
        {
            return Environment.OSVersion.VersionString;
        }

        // 获取总内存大小（以字节为单位）
        public static ulong GetTotalMemory()
        {
            return new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;
        }

        // 获取可用内存大小（以字节为单位）
        public ulong GetAvailableMemory()
        {
            return new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory;
        }

        // 获取硬盘总空间（以字节为单位）
        public static long GetTotalHardDiskSpace()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            long totalSpace = 0;
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    totalSpace += drive.TotalSize;
                }
            }
            return totalSpace;
        }

        // 获取可用硬盘空间（以字节为单位）
        public static long GetAvailableHardDiskSpace()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            long availableSpace = 0;
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    availableSpace += drive.AvailableFreeSpace;
                }
            }
            return availableSpace;
        }

        // 获取主板制造商
        public static string GetMotherboardManufacturer()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Manufacturer FROM Win32_BaseBoard");
            foreach (ManagementObject obj in searcher.Get())
            {
                return obj["Manufacturer"].ToString();
            }
            return "Manufacturer not found";
        }

        // 获取主板型号
        public static string GetMotherboardModel()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Product FROM Win32_BaseBoard");
            foreach (ManagementObject obj in searcher.Get())
            {
                return obj["Product"].ToString();
            }
            return "Model not found";
        }

        // 获取 CPU 型号
        public static string GetCpuModel()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Name FROM Win32_Processor");
            foreach (ManagementObject obj in searcher.Get())
            {
                return obj["Name"].ToString();
            }
            return "CPU Model not found";
        }
    }
}
