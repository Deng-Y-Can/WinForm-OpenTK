using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp.CandyTool;

namespace WinFormsApp.MyOpenCV.EmguCV
{
    public partial class AnimePhotos2 : Form
    {
        public AnimePhotos2()
        {
            InitializeComponent();
        }
        string picture1Path = "";
        private void button1_Click(object sender, EventArgs e)
        {
            string path = CandyFile.ChooseImageFile();
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            picture1Path = path;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // 读取图片
                Mat image = CvInvoke.Imread(picture1Path, ImreadModes.Color);
                if (image.IsEmpty)
                {
                    Console.WriteLine("无法读取图片，请检查路径。");
                    return;
                }

                // 缩放图片（可选，用于处理大尺寸图片）
                double scaleFactor = 0.5;
                Size newSize = new Size((int)(image.Width * scaleFactor), (int)(image.Height * scaleFactor));
                Mat resizedImage = new Mat();
                CvInvoke.Resize(image, resizedImage, newSize);

                // 可调节的参数
                int adaptiveThresholdBlockSize = 9;
                double adaptiveThresholdC = 9;
                int kmeansClusterCount = 8;

                // 进行动漫化处理
                Mat cartoonImage = CartoonizeImage(resizedImage, adaptiveThresholdBlockSize, adaptiveThresholdC, kmeansClusterCount);

                // 显示原始图片和动漫化后的图片
                CvInvoke.Imshow("Original Image", image);
                CvInvoke.Imshow("Cartoonized Image", cartoonImage);

                // 保存动漫化后的图片
                string outputPath = "cartoonized_image.jpg";
                CvInvoke.Imwrite(outputPath, cartoonImage);
                Console.WriteLine($"动漫化图片已保存到 {outputPath}");

                // 等待按键事件
                CvInvoke.WaitKey(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发生错误: {ex.Message}");
            }
        }
        static Mat CartoonizeImage(Mat image, int adaptiveThresholdBlockSize, double adaptiveThresholdC, int kmeansClusterCount)
        {
            // 步骤1: 灰度化和边缘检测
            Mat gray = new Mat();
            CvInvoke.CvtColor(image, gray, ColorConversion.Bgr2Gray);
            CvInvoke.MedianBlur(gray, gray, 5);
            Mat edges = new Mat();
            CvInvoke.AdaptiveThreshold(gray, edges, 255, AdaptiveThresholdType.MeanC, ThresholdType.Binary, adaptiveThresholdBlockSize, adaptiveThresholdC);

            // 步骤2: 颜色量化
            Mat data = image.Reshape(3, image.Rows * image.Cols);
            data.ConvertTo(data, DepthType.Cv32F);
            Mat labels = new Mat();
            Mat centers = new Mat();
            MCvTermCriteria criteria = new MCvTermCriteria(20, 0.001);
            CvInvoke.Kmeans(data, kmeansClusterCount, labels, criteria, 10, KMeansInitType.RandomCenters, centers);
            centers.ConvertTo(centers, DepthType.Cv8U);

            // 确保 result 的数据类型和 centers 一致
            Mat result = new Mat(image.Rows * image.Cols, 3, centers.Depth, centers.NumberOfChannels);
            int[] labelData = new int[labels.Rows * labels.Cols];

            // 使用 Marshal.Copy 复制数据
            Marshal.Copy(labels.DataPointer, labelData, 0, labelData.Length);

            for (int i = 0; i < data.Rows; i++)
            {
                int label = labelData[i];
                centers.Row(label).CopyTo(result.Row(i));
            }

            Mat cartoon = result.Reshape(3, image.Rows);

            // 步骤3: 合并边缘和颜色量化后的图像
            Mat finalCartoon = new Mat();
            CvInvoke.BitwiseAnd(cartoon, cartoon, finalCartoon, edges);

            return finalCartoon;
        }
    }
}
