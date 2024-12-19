using System;
using System.Threading;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
namespace WinFormsApp
{
    public partial class VideoForm : Form
    {
        private bool _isRunning = false;
        private VideoCapture _capture;
        private Thread _cameraThread;
        public VideoForm()
        {
            InitializeComponent();
        }



        private void CameraCallback()
        {
            while (_isRunning)
            {
                Mat frame = new Mat();
                _capture.Read(frame);
                if (frame.Empty())
                {
                    break;
                }
                Bitmap bitmap = BitmapConverter.ToBitmap(frame);
                pictureBox1.Invoke(new Action(() =>
                {
                    pictureBox1.Image = bitmap;
                }));
            }
        }

      

        private void button2_Click_1(object sender, EventArgs e)
        {
            _isRunning = false;
            if (_capture.IsOpened())
            {
                _capture.Release();
            }
            if (_cameraThread.IsAlive)
            {
                _cameraThread.Join();
            }
        }

        private void VideoForm_Load(object sender, EventArgs e)
        {
            // 初始化摄像头捕获对象
            _capture = new VideoCapture(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _isRunning = true;
            _cameraThread = new Thread(CameraCallback);
            _cameraThread.Start();
        }
    }
}
