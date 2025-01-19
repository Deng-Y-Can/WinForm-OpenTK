using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp.CandyTool;

namespace WinFormsApp.Protocol
{
    public partial class UdpP2P2Form : Form
    {
        public UdpP2P2Form()
        {
            InitializeComponent();
            Init();
        }
        private static List<UdpClient> udpClients = new List<UdpClient>();
        private static bool running = true;
        private static string logFilePath = "udp_p2p_log.txt";
        private const int DEFAULT_SEND_BUFFER_SIZE = 8192;    // 默认发送缓冲区大小
        private const int DEFAULT_RECEIVE_BUFFER_SIZE = 8192; // 默认接收缓冲区大小
        private const int DEFAULT_TIMEOUT = 5000;         // 默认超时时间（毫秒）
        public void Init()
        {
            string localIP = ComputerProperties.GetLocalIPAddress();
            textBox1.Text = "127.0.0.1";
            textBox2.Text = "12345";
            textBox3.Text = "777";
        }
        public async Task MainTask()
        { // 检查命令行参数是否有指定端口
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text))
            {
                return;
            }
            int localPort = int.Parse(textBox3.Text);
            string remoteIPAddress = textBox1.Text;
            int remotePort = int.Parse(textBox2.Text);

            // 创建并添加 UdpClient 对象到列表
            UdpClient udpClient = new UdpClient(localPort);
            udpClient.Client.SendBufferSize = DEFAULT_SEND_BUFFER_SIZE; // 设置发送缓冲区大小
            udpClient.Client.ReceiveBufferSize = DEFAULT_RECEIVE_BUFFER_SIZE; // 设置接收缓冲区大小
            udpClient.Client.ReceiveTimeout = DEFAULT_TIMEOUT; // 设置接收超时时间
            udpClients.Add(udpClient);

            // 启动接收消息的任务
            Task receiveTask = ReceiveMessage();

            while (running)
            {
                // 从控制台读取输入并发送
                string message = Console.ReadLine();
                if (message == "exit")
                {
                    running = false;
                    break;
                }
                await SendMessage(message, remoteIPAddress, remotePort);
            }

            // 关闭所有 UdpClient 对象
            foreach (var client in udpClients)
            {
                client.Close();
            }
        }

        static async Task SendMessage(string message, string remoteIPAddress, int remotePort)
        {
            try
            {
                byte[] data = Encoding.ASCII.GetBytes(message);
                foreach (var udpClient in udpClients)
                {
                    IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(remoteIPAddress), remotePort);
                    await udpClient.SendAsync(data, data.Length, remoteEndPoint);
                    Console.WriteLine($"Sent: {message}");
                    LogMessage($"Sent: {message} to {remoteEndPoint}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
                LogMessage($"Error sending message: {ex.Message}");
            }
        }

        static async Task ReceiveMessage()
        {
            try
            {
                while (running)
                {
                    foreach (var udpClient in udpClients)
                    {
                        UdpReceiveResult result = await udpClient.ReceiveAsync();
                        byte[] receivedBytes = result.Buffer;
                        string receivedMessage = Encoding.ASCII.GetString(receivedBytes);
                        Console.WriteLine($"Received from {result.RemoteEndPoint}: {receivedMessage}");
                        LogMessage($"Received from {result.RemoteEndPoint}: {receivedMessage}");
                    }
                }
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.TimedOut)
                {
                    // 处理接收超时异常
                    Console.WriteLine("接收超时");
                    LogMessage("接收超时");
                }
                else
                {
                    Console.WriteLine($"Error receiving message: {ex.Message}");
                    LogMessage($"Error receiving message: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error receiving message: {ex.Message}");
                LogMessage($"Error receiving message: {ex.Message}");
            }
        }

        static void LogMessage(string logMessage)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {logMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to log file: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainTask();
        }
    }
}
