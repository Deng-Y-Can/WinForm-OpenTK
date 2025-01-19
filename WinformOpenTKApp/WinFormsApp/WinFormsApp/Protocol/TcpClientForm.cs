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
    public partial class TcpClientForm : Form
    {
        public TcpClientForm()
        {
            InitializeComponent();
        }
        public void Init()
        {
            string localIP = ComputerProperties.GetLocalIPAddress();
            textBox1.Text = "127.0.0.1";
            textBox2.Text = "12345";
        }

        private static readonly string logFilePath = "client_log.txt";

        public async Task MainTask()
        {
            // 检查命令行参数是否有指定端口
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                return;
            }
            IPAddress serverAddress = IPAddress.Parse(textBox1.Text);
            int port = int.Parse(textBox2.Text);

            // 创建一个 TCP 客户端对象，并连接到服务器的 IP 地址和端口号
            TcpClient client = new TcpClient();
            // 设置超时时间，例如 30 秒
            client.ReceiveTimeout = 30000;
            client.SendTimeout = 30000;
            // 设置发送和接收缓冲区大小，例如 8192 字节
            client.SendBufferSize = 8192;
            client.ReceiveBufferSize = 8192;
            // 启用保持活动状态，每 30 秒发送一个保持活动探测包
            client.LingerState = new LingerOption(true, 30);
            client.NoDelay = true; // 禁用 Nagle 算法，提高性能

            await client.ConnectAsync(serverAddress, port);
            Console.WriteLine($"已连接到服务器 {serverAddress}:{port}");

            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            try
            {
                while (true)
                {
                    // 发送数据给服务器
                    string message = "你好，服务器！";
                    byte[] messageBytes = Encoding.ASCII.GetBytes(message);
                    try
                    {
                        await stream.WriteAsync(messageBytes, 0, messageBytes.Length);
                    }
                    catch (IOException ex) when (ex.InnerException is System.Net.Sockets.SocketException se && se.SocketErrorCode == SocketError.TimedOut)
                    {
                        Console.WriteLine("发送数据超时");
                        Log("发送数据超时");
                        continue;
                    }
                    Console.WriteLine($"发送数据给服务器: {message}");
                    Log($"发送数据给服务器: {message}");

                    // 接收服务器的响应
                    int bytesRead = 0;
                    try
                    {
                        bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    }
                    catch (IOException ex) when (ex.InnerException is System.Net.Sockets.SocketException se && se.SocketErrorCode == SocketError.TimedOut)
                    {
                        Console.WriteLine("接收数据超时");
                        Log("接收数据超时");
                        continue;
                    }
                    if (bytesRead == 0)
                    {
                        // 服务器断开连接
                        break;
                    }
                    string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"接收到服务器的响应: {response}");
                    Log($"接收到服务器的响应: {response}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"客户端异常: {ex.Message}");
                Log($"客户端异常: {ex.Message}");
            }
            finally
            {
                client.Close();
            }
        }

        static void Log(string message)
        {
            using (StreamWriter writer = File.AppendText(logFilePath))
            {
                writer.WriteLine($"{DateTime.Now}: {message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainTask();
        }
    }
}
