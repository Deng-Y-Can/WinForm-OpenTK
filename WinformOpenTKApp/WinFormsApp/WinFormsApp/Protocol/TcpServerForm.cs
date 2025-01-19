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
using System.Drawing.Text;
using WinFormsApp.CandyTool;

namespace WinFormsApp.Protocol
{
    public partial class TcpServerForm : Form
    {
        public TcpServerForm()
        {
            InitializeComponent();
            Init();
        }
        public void Init()
        {
            string localIP = ComputerProperties.GetLocalIPAddress();
            textBox1.Text = localIP;
            textBox2.Text = "12345";
        }

        private static List<TcpClient> clients = new List<TcpClient>();
        private static readonly string logFilePath = "server_log.txt";

        static async Task HandleClient(TcpClient client)
        {
            clients.Add(client);
            NetworkStream stream = client.GetStream();
            // 设置超时时间，例如 30 秒
            client.ReceiveTimeout = 30000;
            client.SendTimeout = 30000;
            // 设置发送和接收缓冲区大小，例如 8192 字节
            client.SendBufferSize = 8192;
            client.ReceiveBufferSize = 8192;
            // 启用保持活动状态，每 30 秒发送一个保持活动探测包
            client.LingerState = new LingerOption(true, 30);
            client.NoDelay = true; // 禁用 Nagle 算法，提高性能

            byte[] buffer = new byte[1024];
            try
            {
                while (true)
                {
                    // 接收客户端发送的数据
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
                        // 客户端断开连接
                        break;
                    }
                    string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"接收到客户端的数据: {dataReceived}");
                    Log($"接收到客户端的数据: {dataReceived}");

                    // 发送数据给客户端
                    string response = "你好，客户端！";
                    byte[] responseBytes = Encoding.ASCII.GetBytes(response);
                    try
                    {
                        await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                    }
                    catch (IOException ex) when (ex.InnerException is System.Net.Sockets.SocketException se && se.SocketErrorCode == SocketError.TimedOut)
                    {
                        Console.WriteLine("发送数据超时");
                        Log("发送数据超时");
                        continue;
                    }
                    Console.WriteLine($"发送数据给客户端: {response}");
                    Log($"发送数据给客户端: {response}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"客户端异常: {ex.Message}");
                Log($"客户端异常: {ex.Message}");
            }
            finally
            {
                clients.Remove(client);
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

        public async Task MainTask()
        {

            // 检查命令行参数是否有指定端口
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                return;
            }
            IPAddress iPAddress= IPAddress.Parse(textBox1.Text);
            int port = int.Parse(textBox2.Text);
            // 创建一个 TCP 监听对象，并绑定到本地 IP 地址和端口号
            TcpListener server = new TcpListener(IPAddress.Any, port);
            server.Start();
            Console.WriteLine($"服务器已启动，正在监听端口 {port}...");

            while (true)
            {
                try
                {
                    TcpClient client = await server.AcceptTcpClientAsync();
                    Console.WriteLine("客户端已连接");
                    // 为每个客户端启动一个新的任务进行处理
                    Task.Run(() => HandleClient(client));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"服务端异常: {ex.Message}");
                    Log($"服务端异常: {ex.Message}");
                }
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainTask();
        }
    }
}
