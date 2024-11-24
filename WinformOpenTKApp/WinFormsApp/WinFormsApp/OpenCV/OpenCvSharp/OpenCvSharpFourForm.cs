using System;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
namespace WinFormsApp
{
    public partial class OpenCvSharpFourForm : Form
    {
        public OpenCvSharpFourForm()
        {
            InitializeComponent();
        }

        private string picturePath = $@"E:\home\github\WinformOpenTK\WinForm-OpenTK\WinformOpenTKApp\WinFormsApp\WinFormsApp\Picture\jlt.jpg";
        private void button1_Click(object sender, EventArgs e)
        {
            // 加载图像
            Mat image = new Mat(picturePath, ImreadModes.Color);
            // 将Mat对象转换为Bitmap对象
            Bitmap bitmap = BitmapConverter.ToBitmap(image);
            // 在PictureBox中显示图像
            pictureBox1.Image = bitmap;
        }
    }
}
