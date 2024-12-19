using System;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace WinFormsApp.OpenCV.OpenCvSharp
{
    public partial class ImageProcessingForm : Form
    {
        private string picturePath = $@"E:\home\github\WinformOpenTK\WinForm-OpenTK\WinformOpenTKApp\WinFormsApp\WinFormsApp\Picture\jlt.jpg";
        public ImageProcessingForm()
        {
            InitializeComponent();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            // 加载图像
            Mat image = new Mat(picturePath, ImreadModes.Color);
            // 灰度化处理
            Mat grayImage = new Mat();
            Cv2.CvtColor(image, grayImage, ColorConversionCodes.BGR2GRAY);
            // 将处理后的图像转换为Bitmap并显示
            Bitmap bitmap = BitmapConverter.ToBitmap(grayImage);
            pictureBox1.Image = bitmap;
        }
    }

}
