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
            //自己选择文件路径
            //string selectedFilePath = SelectFilePath();
            //if (!string.IsNullOrEmpty(selectedFilePath))
            //{
            //    Console.WriteLine($"你选择的文件路径是: {selectedFilePath}");
            //}
            //else
            //{
            //    Console.WriteLine("未选择任何文件。");
            //}

            // 加载图像
            Mat image = new Mat(picturePath, ImreadModes.Color);
            // 将Mat对象转换为Bitmap对象
            Bitmap bitmap = BitmapConverter.ToBitmap(image);
            // 在PictureBox中显示图像
            pictureBox1.Image = bitmap;
        }

        public static string SelectFilePath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择文件";
            openFileDialog.Filter = "所有文件|*.*";
            // 可以根据需要设置特定的文件类型过滤器，例如：
            // openFileDialog.Filter = "图像文件|*.jpg;*.png;*.bmp|文本文件|*.txt|所有文件|*.*";

            // 设置默认路径为C盘
            openFileDialog.InitialDirectory = "C:\\";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }

            return null;
        }
    }
}
