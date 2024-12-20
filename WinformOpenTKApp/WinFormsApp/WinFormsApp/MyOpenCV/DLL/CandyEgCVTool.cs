using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using OpenTK.Mathematics;
using Emgu.CV.Util;
using Emgu.CV.UI;

namespace WinFormsApp.MyOpenCV.DLL
{
    public class CandyEgCVTool
    {
        //Dll
        //Emgu.CV\Emgu.CV.Bitmap\Emgu.CV.runtime.windows\Emgu.CV.UI
        /// <summary>
        /// 加载图片
        /// </summary>
        /// <param name="path"></param>
        /// <param name="outpath"></param>
        public static void ShowImage(string path, string outpath = "mysave.png")
        {
            // 读取图像
            Mat imageMat = CvInvoke.Imread(path, ImreadModes.Unchanged);

            // 加载图像
            Image<Bgr, byte> img = new Image<Bgr, byte>(path);
            // 创建一个ImageViewer对象来显示图像
            ShowImage(img);

            // 灰度化图像，为后续处理做准备
            Image<Gray, byte> grayImage = img.Convert<Gray, byte>();

            // 采用自适应阈值处理，相较于简单阈值能更好应对光照等变化，这里使用均值自适应阈值方法
            Image<Gray, byte> thresholdImage = grayImage.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.MeanC, ThresholdType.Binary, 11, new Gray(152));

            // 将图像保存为指定文件名的文件
            img.Save(outpath);

        }
        /// <summary>
        /// 播放视频
        /// </summary>
        /// <param name="videoPath"></param>
        public static void PlayVideo(string videoPath)
        {
            // 创建视频捕获对象并尝试打开视频文件
            VideoCapture capture = new VideoCapture(videoPath);
            if (!capture.IsOpened)
            {
                Console.WriteLine("无法打开视频文件");
                return;
            }

            // 创建图像查看器用于显示视频帧
            ImageViewer viewer = new ImageViewer();

            // 用于存储当前读取的视频帧
            Mat frame = new Mat();

            try
            {
                while (true)
                {
                    // 读取一帧视频
                    capture.Read(frame);
                    if (frame.IsEmpty)
                    {
                        break;
                    }

                    // 将Mat类型的帧转换为Emgu CV的Image类型（这里以BGR格式为例）
                    Image<Bgr, byte> imageFrame = frame.ToImage<Bgr, byte>();

                    // 更新图像查看器中的图像，实现单窗口播放
                    viewer.Image = imageFrame;
                    viewer.Show();

                    // 控制视频播放速度，单位是毫秒，这里设置为30毫秒一帧，可根据需求调整
                    Thread.Sleep(30);
                    viewer.Refresh();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"视频播放出现错误: {ex.Message}");
            }
            finally
            {
                // 释放视频捕获对象资源
                capture.Dispose();
                // 关闭图像查看器窗口
                viewer.Close();
                // 释放图像查看器资源
                viewer.Dispose();
            }
        }
        /// <summary>
        /// 保存视频
        /// </summary>
        /// <param name="videoPath"></param>
        /// <param name="outputVideoPath"></param>
        public static void SaveVideo(string videoPath, string outputVideoPath = "savevidio.avi")
        {
            VideoCapture inputCapture = new VideoCapture(videoPath);
            if (!inputCapture.IsOpened)
            {
                Console.WriteLine("无法打开输入视频文件");
                return;
            }
            int fps = (int)inputCapture.Get(CapProp.Fps);
            int width = (int)inputCapture.Get(CapProp.FrameWidth);
            int height = (int)inputCapture.Get(CapProp.FrameHeight);
            VideoWriter outputWriter = new VideoWriter(outputVideoPath, fps, new System.Drawing.Size(width, height), true);
            if (!outputWriter.IsOpened)
            {
                Console.WriteLine("无法创建输出视频文件");
                inputCapture.Dispose();
                return;
            }
            Mat frame = new Mat();
            while (true)
            {
                inputCapture.Read(frame);
                if (frame.IsEmpty)
                {
                    break;
                }
                outputWriter.Write(frame);
            }
            inputCapture.Dispose();
            outputWriter.Dispose();
        }

        public static void ShowImage(Image<Bgr, byte> image)
        {
            ImageViewer viewer = new ImageViewer();
            viewer.Image = image;
            viewer.Show();
        }
        public static void ShowImage(Image<Gray, byte> image)
        {
            ImageViewer viewer = new ImageViewer();
            viewer.Image = image;
            viewer.Show();
        }
        public static void ShowImage(Image<Hsv, byte> image)
        {
            ImageViewer viewer = new ImageViewer();
            viewer.Image = image;
            viewer.Show();
        }
        /// <summary>
        /// 绘制直线
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="mCvScalar"></param>
        /// <param name="lineWidth"></param>
        public static void Line(Point startPoint, Point endPoint, MCvScalar mCvScalar, int lineWidth = 2)
        {
            // 加载图像
            Image<Bgr, byte> image = new Image<Bgr, byte>(400, 400);
            // 定义线条起点和终点坐标
            startPoint = new Point(50, 50);
            endPoint = new Point(300, 300);
            // 定义线条颜色（这里是白色）
            mCvScalar = new MCvScalar(255, 255, 255, 0);
            // 定义线条宽度
            //lineWidth = 2;
            // 在图像上绘制线条
            CvInvoke.Line(image, startPoint, endPoint, mCvScalar, lineWidth);
            ShowImage(image);
        }
        /// <summary>
        /// 绘制矩形
        /// </summary>
        /// <param name="leftTopX"></param>
        /// <param name="leftTopY"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="borderWidth"></param>
        public static void Rectangle(int leftTopX = 50, int leftTopY = 50, int width = 200, int height = 100, int borderWidth = 2)
        {
            // 加载图像
            Image<Bgr, byte> image = new Image<Bgr, byte>(400, 400);
            // 定义矩形左上角顶点坐标
            //leftTopX = 50;
            //leftTopY = 50;
            // 定义矩形的宽度和高度
            //width = 200;
            //height = 100;
            // 定义矩形颜色（这里是黄色）
            //Bgr rectColor = new Bgr(0, 255, 0);
            MCvScalar mCvScalar = new MCvScalar(0, 255, 255, 0);
            // 定义矩形边框宽度
            //orderWidth = 2;
            Rectangle rectangle = new Rectangle(leftTopX, leftTopY, width, height);
            // 在图像上绘制矩形
            CvInvoke.Rectangle(image, rectangle, mCvScalar, borderWidth);
            ShowImage(image);
        }
        /// <summary>
        /// 绘制圆
        /// </summary>
        /// <param name="center"></param>
        /// <param name="mCvScalar"></param>
        /// <param name="radius"></param>
        public static void Circle(Point center, MCvScalar mCvScalar, int radius = 80)
        {
            // 加载图像
            Image<Bgr, byte> image = new Image<Bgr, byte>(400, 400);
            // 定义圆心坐标
            center = new Point(200, 200);
            // 定义圆的半径
            // radius = 80;
            // 定义圆的颜色（这里是红色）
            Bgr circleColor = new Bgr(255, 0, 0);
            mCvScalar = new MCvScalar(0, 255, 255, 0);
            // 定义圆的线条宽度
            int circleWidth = 2;
            // 在图像上绘制圆
            CvInvoke.Circle(image, center, radius, mCvScalar, circleWidth);
            ShowImage(image);
        }
        /// <summary>
        /// 绘制椭圆
        /// </summary>
        /// <param name="ellipseCenter"></param>
        /// <param name="majorAxis"></param>
        /// <param name="minorAxis"></param>
        public static void Ellipse(Point ellipseCenter, int majorAxis = 120, int minorAxis = 80)
        {
            // 加载图像
            Image<Bgr, byte> image = new Image<Bgr, byte>(400, 400);
            // 定义椭圆中心坐标
            ellipseCenter = new Point(200, 200);
            // 定义长轴和短轴长度
            // majorAxis = 120;
            // minorAxis = 80;
            // 定义旋转角度（以度为单位）
            double angle = 45;
            // 定义起始角度和终止角度（以度为单位）
            double startAngle = 0;
            double endAngle = 360;
            // 定义椭圆颜色（这里是黄色）
            Bgr ellipseColor = new Bgr(255, 255, 0);
            MCvScalar mCvScalar = new MCvScalar(0, 255, 255, 0);
            // 定义椭圆线条宽度
            int ellipseWidth = 2;
            // 在图像上绘制椭圆
            CvInvoke.Ellipse(image, ellipseCenter, new Size(majorAxis, minorAxis), angle, startAngle, endAngle, mCvScalar, ellipseWidth);
            ShowImage(image);
        }
        /// <summary>
        /// 绘制多边形
        /// </summary>
        /// <param name="points"></param>
        /// <param name="mCvScalar"></param>
        /// <param name="borderWidth"></param>
        public static void Polygon(Point[] points, MCvScalar mCvScalar, int borderWidth = 2)
        {
            // 加载图像或创建一个空白图像
            Image<Bgr, byte> image = new Image<Bgr, byte>(400, 400);
            // 定义多边形的顶点坐标数组
            points = new Point[]
            {
              new Point(50, 50),
              new Point(150, 50),
              new Point(200, 150),
              new Point(100, 200)
            };
            // 定义多边形颜色（这里是橙色）
            Bgr polygonColor = new Bgr(255, 165, 0);
            mCvScalar = new MCvScalar(0, 255, 255, 0);
            // 定义多边形线条宽度
            borderWidth = 2;
            // 在图像上绘制多边形
            CvInvoke.Polylines(image, points, true, mCvScalar, borderWidth);
            ShowImage(image);
        }
        /// <summary>
        /// 显示文本
        /// </summary>
        /// <param name="mCvScalar"></param>
        /// <param name="text"></param>
        public static void Text(MCvScalar mCvScalar, string text = "Hello, Zoe!")
        {
            // 加载图像或创建一个空白图像
            Image<Bgr, byte> image = new Image<Bgr, byte>(400, 400);
            // 定义文本内容
            //text = "Hello, Zoe!";
            // 定义文本位置（这里是图像的左上角）
            Point textLocation = new Point(50, 50);
            // 定义字体（这里使用Arial字体）
            Font font = new Font("Arial", 12, FontStyle.Regular);
            // 定义文本颜色（这里是白色）
            mCvScalar = new MCvScalar(0, 255, 255, 0);
            // 在图像上添加文本
            CvInvoke.PutText(image, text, textLocation, FontFace.HersheySimplex, 0.5, mCvScalar, 2);
            // 将带有文本的图像保存到文件
            //image.Save("outText.jpg");
            ShowImage(image);
        }
        /// <summary>
        /// 读像素
        /// </summary>
        /// <param name="path"></param>
        public static void ReadColor(string path)
        {
            // 加载彩色图像
            Image<Bgr, byte> image = new Image<Bgr, byte>(path);
            // 访问图像的某个像素（以坐标(100, 100)为例）
            Bgr pixelColor = image[100, 100];
            double blueValue = pixelColor.Blue;
            double greenValue = pixelColor.Green;
            double redValue = pixelColor.Red;
            Console.WriteLine($"Blue: {blueValue}, Green: {greenValue}, Red: {redValue}");
        }
        /// <summary>
        /// 读像素灰度值
        /// </summary>
        /// <param name="path"></param>
        public static void ReadGrey(string path)
        {
            // 加载灰度图像
            Image<Gray, byte> image = new Image<Gray, byte>(path);
            // 访问图像的某个像素（以坐标(100, 100)为例）
            double grayValue = image[100, 100].Intensity;
            Console.WriteLine($"Gray value: {grayValue}");
        }
        /// <summary>
        /// 修改像素
        /// </summary>
        /// <param name="path"></param>
        public static void ModifyColor(string path, Bgr newColor)
        {
            // 加载彩色图像
            Image<Bgr, byte> image = new Image<Bgr, byte>(path);
            // 修改图像的某个像素（以坐标(100, 100)为例）
            newColor = new Bgr(255, 0, 0); // 设置为蓝色
            image[100, 100] = newColor;
        }
        /// <summary>
        /// 修改像素灰度值，暂不可用
        /// </summary>
        /// <param name="path"></param>
        public static void ModifyGrey(string path)
        {
            // 加载灰度图像
            Image<Gray, byte> image = new Image<Gray, byte>(path);
            // 修改图像的某个像素（以坐标(100, 100)为例）
            byte newGrayValue = 128;
            image[100, 100] = new Gray(newGrayValue);
        }
        /// <summary>
        /// 获得图片尺寸
        /// </summary>
        /// <param name="path"></param>
        public static void GetImageSize(string path)
        {
            // 加载图像
            Image<Bgr, byte> image = new Image<Bgr, byte>(path);
            int width = image.Width;
            int height = image.Height;
            Console.WriteLine($"图像宽度: {width}，高度: {height}");
        }
        /// <summary>
        /// 获得图像感兴趣区域ROI
        /// </summary>
        /// <param name="path"></param>
        public static void GetROI(string path)
        {
            // 加载图像
            Image<Bgr, byte> image = new Image<Bgr, byte>(path);
            // 创建一个Rectangle对象来定义ROI
            Rectangle roiRect = new Rectangle(100, 100, 200, 150);
            // 获取ROI
            Image<Bgr, byte> roi = image.GetSubRect(roiRect);
            // 可以对ROI进行操作，例如绘制一个矩形框来标记ROI
            // 定义文本颜色（这里是黄色）
            MCvScalar mCvScalar = new MCvScalar(0, 255, 255, 0);
            CvInvoke.Rectangle(image, roiRect, mCvScalar, 2);
            ShowImage(image);
        }
        /// <summary>
        /// 保存图像感兴趣区域ROI
        /// </summary>
        /// <param name="path"></param>
        public static void SaveROI(string path, string outFile = "roi_image.jpg")
        {
            // 加载图像
            Image<Bgr, byte> image = new Image<Bgr, byte>(path);
            // 创建一个Rectangle对象来定义ROI
            Rectangle roiRect = new Rectangle(100, 100, 200, 150);
            // 获取ROI
            Image<Bgr, byte> roi = image.GetSubRect(roiRect);
            // 对ROI进行操作，例如将ROI保存为新的图像文件
            roi.Save(outFile);
        }

        /// <summary>
        ///  拆分图像通道
        /// </summary>
        /// <param name="path"></param>
        public static void SplitChannels(string path)
        {
            // 加载彩色图像
            Image<Bgr, byte> image = new Image<Bgr, byte>(path);
            // 拆分图像通道
            VectorOfMat channels = new VectorOfMat();
            CvInvoke.Split(image, channels);
            // 获取蓝色通道
            Mat blueChannel = channels[0];
            // 获取绿色通道
            Mat greenChannel = channels[1];
            // 获取红色通道
            Mat redChannel = channels[2];
        }
        /// <summary>
        /// 合并图像通道
        /// </summary>
        /// <param name="path"></param>
        /// <param name="blueChannel"></param>
        /// <param name="greenChannel"></param>
        /// <param name="redChannel"></param>
        public static void MergeChannels(string path, Mat blueChannel, Mat greenChannel, Mat redChannel)
        {
            // 加载彩色图像
            Image<Bgr, byte> image = new Image<Bgr, byte>(path);
            // 合并图像通道
            VectorOfMat mergedChannels = new VectorOfMat();
            mergedChannels.Push(blueChannel);
            mergedChannels.Push(greenChannel);
            mergedChannels.Push(redChannel);
            Image<Bgr, byte> mergedImage = new Image<Bgr, byte>(image.Size);
            CvInvoke.Merge(mergedChannels, mergedImage);
        }
        /// <summary>
        /// 添加边框
        /// </summary>
        /// <param name="path"></param>
        public static void AddBorder(string path)
        {
            // 加载图像
            Image<Bgr, byte> image = new Image<Bgr, byte>(path);
            // 定义边框（填充）的宽度（这里上下左右都填充10像素）
            int topBorder = 10;
            int bottomBorder = 10;
            int leftBorder = 10;
            int rightBorder = 10;
            // 定义文本颜色（这里是黄色）
            MCvScalar mCvScalar = new MCvScalar(0, 255, 255, 0);
            // 使用CopyMakeBorder函数添加边框（填充）
            Mat paddedImageMat = new Mat();
            CvInvoke.CopyMakeBorder(image, paddedImageMat, topBorder, bottomBorder, leftBorder, rightBorder, BorderType.Constant, mCvScalar);
            Image<Bgr, byte> paddedImage = paddedImageMat.ToImage<Bgr, byte>();
            // 可以对添加边框后的图像进行操作，如显示图像
            ShowImage(paddedImage);
        }
        /// <summary>
        /// 图片加法
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        public static void ImageAdd(string path1, string path2)
        {
            // 加载两个图像
            Image<Bgr, byte> image1 = new Image<Bgr, byte>(path1);
            Image<Bgr, byte> image2 = new Image<Bgr, byte>(path2);
            // 确保两个图像尺寸相同
            if (image1.Size == image2.Size)
            {
                Mat resultMat = new Mat();
                CvInvoke.Add(image1, image2, resultMat);
                Image<Bgr, byte> resultImage = resultMat.ToImage<Bgr, byte>();
                // 可以对相加后的图像进行操作，如显示图像
                ShowImage(resultImage);
            }
            else
            {
                Console.WriteLine("两个图像尺寸不同，无法进行加法运算。");
            }
        }
        /// <summary>
        /// 图像融合
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        public static void ImageAddWeighted(string path1, string path2)
        {
            // 加载两个图像
            Image<Bgr, byte> image1 = new Image<Bgr, byte>(path1);
            Image<Bgr, byte> image2 = new Image<Bgr, byte>(path2);
            // 确保两个图像尺寸相同
            if (image1.Size == image2.Size)
            {
                double alpha = 0.2; // 图像1的权重
                double beta = 0.8; // 图像2的权重
                double gamma = 0; // 偏移量
                Mat resultMat = new Mat();
                CvInvoke.AddWeighted(image1, alpha, image2, beta, gamma, resultMat);
                Image<Bgr, byte> resultImage = resultMat.ToImage<Bgr, byte>();
                // 可以对融合后的图像进行操作，如显示图像
                ShowImage(resultImage);
            }
            else
            {
                Console.WriteLine("两个图像尺寸不同，无法进行融合运算。");
            }
        }
        /// <summary>
        /// 图像按位运算
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        public static void BitwiseOperation(string path1, string path2)
        {
            // 加载原始图像和掩码图像
            Image<Bgr, byte> originalImage = new Image<Bgr, byte>(path1);
            Image<Gray, byte> maskImage = new Image<Gray, byte>(path2);
            // 确保两个图像尺寸相同
            if (originalImage.Size == maskImage.Size)
            {
                Mat resultMat = new Mat();
                CvInvoke.BitwiseAnd(originalImage, originalImage, resultMat, maskImage);
                // CvInvoke.BitwiseOr(originalImage, originalImage, resultMat, maskImage);
                // CvInvoke.BitwiseXor(originalImage, originalImage, resultMat, maskImage);
                // CvInvoke.BitwiseNot(originalImage, resultMat);
                Image<Bgr, byte> resultImage = resultMat.ToImage<Bgr, byte>();
                // 可以对结果图像进行操作，如显示图像
                ShowImage(resultImage);
            }
            else
            {
                Console.WriteLine("原始图像和掩码图像尺寸不匹配，无法进行按位与运算。");
            }

        }

        /// <summary>
        /// 更改颜色空间
        /// </summary>
        /// <param name="path"></param>
        public static void ChangeColorSpace(string path)
        {
            // 加载RGB图像
            Image<Bgr, byte> rgbImage = new Image<Bgr, byte>(path);
            // 创建用于存储HSV图像的对象
            Image<Hsv, byte> hsvImage = new Image<Hsv, byte>(rgbImage.Size);
            // 进行颜色空间转换（RGB to HSV）
            CvInvoke.CvtColor(rgbImage, hsvImage, ColorConversion.Bgr2Hsv);
            // 可以对HSV图像进行操作，如显示图像
            ShowImage(hsvImage);           
        }
        /// <summary>
        /// 等比缩放
        /// </summary>
        /// <param name="path"></param>
        /// <param name="scaleFactor"></param>
        public static void Scale(string path, double scaleFactor = 0.5)
        {
            // 加载图像
            Image<Bgr, byte> image = new Image<Bgr, byte>(path);
            // 定义缩放比例（这里是缩小为原来的0.5倍）
            //scaleFactor = 0.5;
            // 计算目标尺寸
            int newWidth = (int)(image.Width * scaleFactor);
            int newHeight = (int)(image.Height * scaleFactor);
            // 创建用于存储缩放后图像的对象
            Image<Bgr, byte> resizedImage = new Image<Bgr, byte>(newWidth, newHeight);
            // 进行图像缩放，使用双线性插值（Inter.Linear）
            CvInvoke.Resize(image, resizedImage, new System.Drawing.Size(newWidth, newHeight), 0, 0, Inter.Linear);
            // 可以对缩放后的图像进行操作，如显示图像
            ShowImage(resizedImage);           
        }
        /// <summary>
        /// 不等比缩放
        /// </summary>
        /// <param name="path"></param>
        /// <param name="targetWidth"></param>
        /// <param name="targetHeight"></param>
        public static void ScaleBySize(string path, int targetWidth = 300, int targetHeight = 200)
        {
            // 加载图像
            Image<Bgr, byte> image = new Image<Bgr, byte>(path);
            // 定义目标尺寸
            //targetWidth = 300;
            //targetHeight = 200;
            // 创建用于存储缩放后图像的对象
            Image<Bgr, byte> resizedImage = new Image<Bgr, byte>(targetWidth, targetHeight);
            // 进行图像缩放，使用双三次插值（Inter.Cubic）
            CvInvoke.Resize(image, resizedImage, new System.Drawing.Size(targetWidth, targetHeight), 0, 0, Inter.Cubic);
            // 可以对缩放后的图像进行操作，如显示图像
            ShowImage(resizedImage);            
        }
        /// <summary>
        /// 平移
        /// </summary>
        /// <param name="path"></param>
        public static void Translation(string path)
        {
            // 加载图像
            Image<Bgr, byte> image = new Image<Bgr, byte>(path);
            // 定义平移量（在x方向平移100像素，在y方向平移50像素）
            float translateX = 10;
            float translateY = 15;
            // 创建平移变换矩阵
            Mat translationMatrix = new Mat(2, 3, DepthType.Cv32F, 1);
            // 逐个设置矩阵元素
            // 逐个设置矩阵元素
            float[] translationArray = { 1, 0, translateX, 0, 1, translateY };
            translationMatrix.SetTo(translationArray);
            // 创建用于存储平移后图像的对象
            Image<Bgr, byte> translatedImage = new Image<Bgr, byte>(image.Size);
            // 进行图像平移
            CvInvoke.WarpAffine(image, translatedImage, translationMatrix, translatedImage.Size);
            // 可以对平移后的图像进行操作，如显示图像
            ShowImage(translatedImage);            
        }
        /// <summary>
        /// 旋转
        /// </summary>
        /// <param name="path"></param>
        /// <param name="angle"></param>
        public static void Rotate(string path, double angle = 30)
        {
            // 加载图像
            Image<Bgr, byte> image = new Image<Bgr, byte>(path);
            // 定义旋转中心（这里是图像中心）
            Point center = new Point(image.Width / 2, image.Height / 2);
            // 定义旋转角度（这里是逆时针旋转30度）
            //angle = 30;
            // 创建旋转变换矩阵
            Mat rotationMatrix = new Mat(2, 3, DepthType.Cv32F, 1);
            CvInvoke.GetRotationMatrix2D(center, angle, 1.0, rotationMatrix);
            // 创建用于存储旋转后图像的对象
            Image<Bgr, byte> rotatedImage = new Image<Bgr, byte>(image.Size);
            // 进行图像旋转
            CvInvoke.WarpAffine(image, rotatedImage, rotationMatrix, image.Size);
            // 可以对旋转后的图像进行操作，如显示图像
            ShowImage(rotatedImage);          
        }
        /// <summary>
        /// 仿射变换
        /// </summary>
        /// <param name="path"></param>
        public static void AffineTransformation(string path)
        {
            // 加载图像
            Image<Bgr, byte> image = new Image<Bgr, byte>(path);
            // 定义一个简单的平移变换矩阵（向右平移50像素，向下平移30像素）
            Mat translationMatrix = new Mat(2, 3, DepthType.Cv32F, 1);
            //平移
            float translationX = 5;
            float translationY = 3;
            float[] translationArray = { 1, 0, translationX,
                                       0, 1, translationY };
            float scaleX = 0.5f;
            float scaleY = 0.5f;
            // 缩放
            float[] translationArray2 = { scaleX, 0, 0,
                                            0, scaleY, 0 };
            //旋转
            // 定义旋转角度（这里是逆时针旋转30度，将角度转换为弧度）
            float angle = (float)(30 * Math.PI / 180);
            float cosValue = (float)Math.Cos(angle);
            float sinValue = (float)Math.Sin(angle);
            float[] translationArray3 = { cosValue, -sinValue, 0,
                                           sinValue, cosValue, 0 };
            translationMatrix.SetTo(translationArray);
            // 创建用于存储变换后图像的对象
            Image<Bgr, byte> transformedImage = new Image<Bgr, byte>(image.Size);
            // 应用仿射变换
            CvInvoke.WarpAffine(image, transformedImage, translationMatrix, image.Size);
            // 可以对变换后的图像进行操作，如显示图像
            ShowImage(transformedImage);           
        }
        /// <summary>
        /// 透射投影
        /// </summary>
        /// <param name="path"></param>
        public static void Perspectivetransformation(string path)
        {
            // 加载图像
            Image<Bgr, byte> image = new Image<Bgr, byte>(path);
            // 原始图像上的四个点（示例）
            PointF[] srcPoints = new PointF[]
            {
            new PointF(0, 0),
            new PointF(image.Width - 1, 0),
            new PointF(image.Width - 1, image.Height - 1),
            new PointF(0, image.Height - 1)
            };
            // 目标图像上对应的四个点（示例，进行简单的透视变换）
            PointF[] dstPoints = new PointF[]
            {
            new PointF(0, 0),
            new PointF(image.Width - 50, 0),
            new PointF(image.Width - 100, image.Height - 1),
            new PointF(50, image.Height - 1)
            };
            // 计算透视变换矩阵
            Mat perspectiveMatrix = CvInvoke.GetPerspectiveTransform(srcPoints, dstPoints);
            // 创建用于存储变换后图像的对象
            Image<Bgr, byte> perspectiveTransformedImage = new Image<Bgr, byte>(image.Size);
            // 应用透视变换
            CvInvoke.WarpPerspective(image, perspectiveTransformedImage, perspectiveMatrix, image.Size);
            // 可以对变换后的图像进行操作，如显示图像
            ShowImage(perspectiveTransformedImage);           
        }
        /// <summary>
        /// 简单阀值
        /// </summary>
        /// <param name="path"></param>
        public static void SetThreshold(string path)
        {
            // 加载灰度图像
            Image<Gray, byte> grayImage = new Image<Gray, byte>(path);
            // 进行二进制阈值处理
            double thresholdValue = 128;
            Mat binaryImageMat = new Mat();
            CvInvoke.Threshold(grayImage, binaryImageMat, thresholdValue, 255, ThresholdType.Binary);
            Image<Gray, byte> binaryImage = binaryImageMat.ToImage<Gray, byte>();
            // 可以对二进制阈值处理后的图像进行操作，如显示图像
            ShowImage(binaryImage);          
        }
        /// <summary>
        /// 自适应阀值
        /// </summary>
        /// <param name="path"></param>
        public static void CustomThreshold(string path)
        {
            // 加载灰度图像
            Image<Gray, byte> grayImage = new Image<Gray, byte>(path);
            // 创建用于存储自定义阈值处理后图像的对象
            Image<Gray, byte> customThresholdImage = new Image<Gray, byte>(grayImage.Size);
            // 自定义阈值处理，例如将100 - 200之间的像素值设置为255，其他设置为0
            for (int y = 0; y < grayImage.Height; y++)
            {
                for (int x = 0; x < grayImage.Width; x++)
                {
                    byte pixelValue = (byte)grayImage[y, x].Intensity;
                    if (pixelValue >= 100 && pixelValue <= 200)
                    {
                        customThresholdImage[y, x] = new Gray(255);
                    }
                    else
                    {
                        customThresholdImage[y, x] = new Gray(0);
                    }
                }
            }
            // 可以对自定义阈值处理后的图像进行操作，如显示图像
            ShowImage(customThresholdImage);           
        }
        /// <summary>
        /// Otsu二分值
        /// </summary>
        /// <param name="path"></param>
        public static void OtsuThreshold(string path)
        {
            // 加载灰度图像
            Image<Gray, byte> grayImage = new Image<Gray, byte>(path);
            Mat binaryImageMat = new Mat();
            CvInvoke.Threshold(grayImage, binaryImageMat, 0, 255, ThresholdType.Binary | ThresholdType.Otsu);
            Image<Gray, byte> binaryImage = binaryImageMat.ToImage<Gray, byte>();
            ShowImage(binaryImage);          
        }


        /// <summary>
        /// 2D卷积 平均滤波器
        /// </summary>
        /// <param name="path"></param>
        public static void Convolution2D(string path)
        {
            // 加载图像
            Image<Bgr, byte> image = new Image<Bgr, byte>(path);

            // 定义卷积核（以简单的3x3平均滤波器为例）   平均滤波器
            float[] averageKernelData =  {
            (float)(1.0 / 9.0), (float)(1.0 / 9.0), (float)(1.0 / 9.0),
            (float)(1.0 / 9.0), (float)(1.0 / 9.0), (float)(1.0 / 9.0),
            (float)(1.0 / 9.0), (float)(1.0 / 9.0), (float)(1.0 / 9.0)
        };


            Mat kernelMat = new Mat(3, 3, DepthType.Cv32F, 1);
            // 正确设置卷积核Mat元素
            kernelMat.SetTo(averageKernelData);

            // 创建用于存储卷积后图像的对象
            Image<Bgr, byte> filteredImage = new Image<Bgr, byte>(image.Size);
            Mat filteredImageMat = filteredImage.Mat;

            // 定义锚点（通常对于对称卷积核，设置为中心点）
            Point anchor = new Point(1, 1);

            // 调用Filter2D函数进行2D卷积操作
            CvInvoke.Filter2D(image, filteredImageMat, kernelMat, anchor);

            // 可以对卷积后的图像进行操作，如显示图像
            ShowImage(filteredImage);         
        }


        /// <summary>
        /// Gaussian Blur 高斯模糊
        /// </summary>
        /// <param name="path"></param>
        public static void GaussianBlur(string path)
        {
            // 加载图像
            Image<Bgr, byte> image = new Image<Bgr, byte>(path);
            // 进行高斯模糊
            Mat gaussianFilteredImageMat = new Mat();
            CvInvoke.GaussianBlur(image, gaussianFilteredImageMat, new System.Drawing.Size(3, 3), 0);
            Image<Bgr, byte> gaussianFilteredImage = gaussianFilteredImageMat.ToImage<Bgr, byte>();
            // 可以对高斯模糊后的图像进行操作，如显示图像
            ShowImage(gaussianFilteredImage);           
        }

        /// <summary>
        /// 中值模糊
        /// </summary>
        /// <param name="path"></param>
        public static void MedianBlur(string path)
        {
            // 加载图像
            Image<Bgr, byte> image = new Image<Bgr, byte>(path);
            // 进行中值模糊
            Mat medianFilteredImageMat = new Mat();
            CvInvoke.MedianBlur(image, medianFilteredImageMat, 3);
            Image<Bgr, byte> medianFilteredImage = medianFilteredImageMat.ToImage<Bgr, byte>();
            // 可以对中值模糊后的图像进行操作，如显示图像
            ShowImage(medianFilteredImage);          
        }
        /// <summary>
        /// 双边滤波
        /// </summary>
        /// <param name="path"></param>
        public static void BilateralFilter(string path)
        {
            // 加载图像
            Image<Bgr, byte> image = new Image<Bgr, byte>(path);
            // 进行双边滤波
            Mat bilateralFilteredImageMat = new Mat();
            CvInvoke.BilateralFilter(image, bilateralFilteredImageMat, 9, 75, 75);
            Image<Bgr, byte> bilateralFilteredImage = bilateralFilteredImageMat.ToImage<Bgr, byte>();
            // 可以对双边滤波后的图像进行操作，如显示图像
            ShowImage(bilateralFilteredImage);           
        }
        /// <summary>
        /// 形态转换之侵蚀
        /// </summary>
        /// <param name="path"></param>
        public static void Erosion(string path)
        {
            // 加载二值图像
            Image<Gray, byte> binaryImage = new Image<Gray, byte>(path);
            // 创建结构元素（以3x3的矩形为例）
            Mat element = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new System.Drawing.Size(3, 3), new Point(-1, -1));
            // 进行侵蚀操作
            Mat erodedImageMat = new Mat();
            Point anchor = new Point(1, 1);//起点
            int iterations = 1;//迭代次数
            MCvScalar borderValue = new MCvScalar(0.255, 255, 0);
            CvInvoke.Erode(binaryImage, erodedImageMat, element , anchor,  iterations, BorderType.Constant, borderValue);
            Image<Gray, byte> erodedImage = erodedImageMat.ToImage<Gray, byte>();
            // 可以对侵蚀后的图像进行操作，如显示图像
            ShowImage(erodedImage);           
        }
        /// <summary>
        /// 扩张
        /// </summary>
        /// <param name="path"></param>
        public static void Dilation(string path)
        {
            // 加载二值图像
            Image<Gray, byte> binaryImage = new Image<Gray, byte>(path);
            // 创建结构元素（以3x3的矩形为例）
            Mat element = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new System.Drawing.Size(3, 3), new Point(-1, -1));
            // 进行扩张操作
            Mat dilatedImageMat = new Mat();
            Point anchor = new Point(1,1 );
            int iterations = 1;
            MCvScalar borderValue = new MCvScalar(0.255, 255, 0);
            CvInvoke.Dilate(binaryImage, dilatedImageMat, element, anchor, iterations, BorderType.Constant, borderValue);
            Image<Gray, byte> dilatedImage = dilatedImageMat.ToImage<Gray, byte>();
            // 可以对扩张后的图像进行操作，如显示图像
            ShowImage(dilatedImage);         
        }
        /// <summary>
        /// 开运算
        /// </summary>
        /// <param name="path"></param>
        public static void Opening(string path)
        {
            // 加载二值图像
            Image<Gray, byte> binaryImage = new Image<Gray, byte>(path);
            // 创建结构元素（以3x3的矩形为例）
            Mat element = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new System.Drawing.Size(3, 3), new Point(-1, -1));
            // 进行开运算
            Mat openedImageMat = new Mat();
            Point anchor = new Point(1, 1);
            int iterations = 1;
            MCvScalar borderValue = new MCvScalar(0.255, 255, 0);
            CvInvoke.MorphologyEx(binaryImage, openedImageMat, MorphOp.Open, element, anchor, iterations, BorderType.Constant, borderValue);
            Image<Gray, byte> openedImage = openedImageMat.ToImage<Gray, byte>();
            // 可以对开运算后的图像进行操作，如显示图像
            ShowImage(openedImage);          
        }
        /// <summary>
        /// 闭运算
        /// </summary>
        /// <param name="path"></param>
        public static void Closing(string path)
        {
            // 加载二值图像
            Image<Gray, byte> binaryImage = new Image<Gray, byte>(path);
            // 创建结构元素（以3x3的矩形为例）
            Mat element = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new System.Drawing.Size(3, 3), new Point(-1, -1));
            // 进行闭运算
            Mat closedImageMat = new Mat();
            Point anchor = new Point(1, 1);
            int iterations = 1;
            MCvScalar borderValue = new MCvScalar(0.255, 255, 0);
            CvInvoke.MorphologyEx(binaryImage, closedImageMat, MorphOp.Close, element, anchor, iterations, BorderType.Constant, borderValue);
            Image<Gray, byte> closedImage = closedImageMat.ToImage<Gray, byte>();
            // 可以对闭运算后的图像进行操作，如显示图像
            ShowImage(closedImage);           
        }
        /// <summary>
        /// 形态学梯度
        /// </summary>
        /// <param name="path"></param>

        public static void MorphologicalGradient(string path)
        {
            // 加载二值图像
            Image<Gray, byte> binaryImage = new Image<Gray, byte>(path);
            // 创建结构元素（以3x3的矩形为例）
            Mat element = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new System.Drawing.Size(3, 3), new Point(-1, -1));
            // 进行膨胀操作
            Mat dilatedImageMat = new Mat();
            Point anchor = new Point(1, 1);
            int iterations = 1;
            MCvScalar borderValue = new MCvScalar(0.255, 255, 0);
            CvInvoke.Dilate(binaryImage, dilatedImageMat, element, anchor, iterations, BorderType.Constant, borderValue);
            Image<Gray, byte> dilatedImage = dilatedImageMat.ToImage<Gray, byte>();
            // 进行腐蚀操作
            Mat erodedImageMat = new Mat();
            CvInvoke.Erode(binaryImage, erodedImageMat, element, anchor, iterations, BorderType.Constant, borderValue);
            Image<Gray, byte> erodedImage = erodedImageMat.ToImage<Gray, byte>();
            // 计算形态学梯度
            Image<Gray, byte> gradientImage = new Image<Gray, byte>(binaryImage.Size);
            for (int y = 0; y < binaryImage.Height; y++)
            {
                for (int x = 0; x < binaryImage.Width; x++)
                {
                    byte dilatedValue = (byte)dilatedImage[y, x].Intensity;
                    byte erodedValue = (byte)erodedImage[y, x].Intensity;
                    byte gradientValue = (byte)(dilatedValue - erodedValue);
                    gradientImage[y, x] = new Gray(gradientValue);
                }
            }
            // 可以对形态学梯度图像进行操作，如显示图像
            ShowImage(gradientImage);           
        }
        /// <summary>
        /// 顶帽
        /// </summary>
        /// <param name="path"></param>
        public static void TopHat(string path)
        {
            // 加载灰度图像（顶帽变换通常用于灰度图像）
            Image<Gray, byte> grayImage = new Image<Gray, byte>(path);
            // 创建结构元素（以3x3的矩形为例）
            Mat element = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new System.Drawing.Size(3, 3), new Point(-1, -1));
            // 进行开运算
            Mat openedImageMat = new Mat();
            Point anchor = new Point(1, 1);
            int iterations = 1;
            MCvScalar borderValue = new MCvScalar(0.255, 255, 0);
            CvInvoke.MorphologyEx(grayImage, openedImageMat, MorphOp.Open, element, anchor, iterations, BorderType.Constant, borderValue);
            Image<Gray, byte> openedImage = openedImageMat.ToImage<Gray, byte>();
            // 计算顶帽变换
            Image<Gray, byte> topHatImage = new Image<Gray, byte>(grayImage.Size);
            for (int y = 0; y < grayImage.Height; y++)
            {
                for (int x = 0; x < grayImage.Width; x++)
                {
                    byte originalValue = (byte)grayImage[y, x].Intensity;
                    byte openedValue = (byte)openedImage[y, x].Intensity;
                    byte topHatValue = (byte)(originalValue - openedValue);
                    topHatImage[y, x] = new Gray(topHatValue);
                }
            }
            // 可以对顶帽变换图像进行操作，如显示图像
            ShowImage(topHatImage);          
        }
        /// <summary>
        /// 黑帽
        /// </summary>
        /// <param name="path"></param>
        public static void BlackHat(string path)
        {
            // 加载灰度图像（黑帽变换通常用于灰度图像）
            Image<Gray, byte> grayImage = new Image<Gray, byte>(path);
            // 创建结构元素（以3x3的矩形为例）
            Mat element = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new System.Drawing.Size(3, 3), new Point(-1, -1));
            // 进行闭运算
            Mat closedImageMat = new Mat();
            Point anchor = new Point(1, 1);
            int iterations = 1;
            MCvScalar borderValue = new MCvScalar(0.255, 255, 0);
            CvInvoke.MorphologyEx(grayImage, closedImageMat, MorphOp.Close, element, anchor, iterations, BorderType.Constant, borderValue);
            Image<Gray, byte> closedImage = closedImageMat.ToImage<Gray, byte>();
            // 计算黑帽变换
            Image<Gray, byte> blackHatImage = new Image<Gray, byte>(grayImage.Size);
            for (int y = 0; y < grayImage.Height; y++)
            {
                for (int x = 0; x < grayImage.Width; x++)
                {
                    byte originalValue = (byte)grayImage[y, x].Intensity;
                    byte closedValue = (byte)closedImage[y, x].Intensity;
                    byte blackHatValue = (byte)(closedValue - originalValue);
                    blackHatImage[y, x] = new Gray(blackHatValue);
                }
            }
            // 可以对黑帽变换图像进行操作，如显示图像
            ShowImage(blackHatImage);           
        }
        /// <summary>
        /// 图像梯度之Sobel算子
        /// </summary>
        /// <param name="path"></param>
        public static void Sobel(string path)
        {
            // 加载灰度图像
            Image<Gray, byte> grayImage = new Image<Gray, byte>(path);
            // 使用Sobel算子进行水平方向边缘检测
            Image<Gray, byte> sobelXImage = new Image<Gray, byte>(grayImage.Size);
            CvInvoke.Sobel(grayImage, sobelXImage, DepthType.Default, 1, 0, 3);
            // 使用Sobel算子进行垂直方向边缘检测
            Image<Gray, byte> sobelYImage = new Image<Gray, byte>(grayImage.Size);
            CvInvoke.Sobel(grayImage, sobelYImage, DepthType.Default, 0, 1, 3);
            // 计算边缘强度
            Image<Gray, byte> sobelMagnitudeImage = new Image<Gray, byte>(grayImage.Size);
            for (int y = 0; y < grayImage.Height; y++)
            {
                for (int x = 0; x < grayImage.Width; x++)
                {
                    double sobelXValue = sobelXImage[y, x].Intensity;
                    double sobelYValue = sobelYImage[y, x].Intensity;
                    double magnitude = Math.Sqrt(sobelXValue * sobelXValue + sobelYValue * sobelYValue);
                    sobelMagnitudeImage[y, x] = new Gray((byte)magnitude);
                }
            }
            // 可以对Sobel边缘检测后的图像进行操作，如显示图像
            ShowImage(sobelMagnitudeImage);           
        }
        /// <summary>
        /// Scharr算子
        /// </summary>
        /// <param name="path"></param>
        public static void Scharr(string path)
        {
            // 加载灰度图像
            Image<Gray, byte> grayImage = new Image<Gray, byte>(path);
            // 使用Scharr算子进行水平方向边缘检测
            Image<Gray, byte> scharrXImage = new Image<Gray, byte>(grayImage.Size);
            CvInvoke.Scharr(grayImage, scharrXImage, DepthType.Default, 1, 0);
            // 使用Scharr算子进行垂直方向边缘检测
            Image<Gray, byte> scharrYImage = new Image<Gray, byte>(grayImage.Size);
            CvInvoke.Scharr(grayImage, scharrYImage, DepthType.Default, 0, 1);
            // 计算边缘强度
            Image<Gray, byte> scharrMagnitudeImage = new Image<Gray, byte>(grayImage.Size);
            for (int y = 0; y < grayImage.Height; y++)
            {
                for (int x = 0; x < grayImage.Width; x++)
                {
                    double scharrXValue = scharrXImage[y, x].Intensity;
                    double scharrYValue = scharrYImage[y, x].Intensity;
                    double magnitude = Math.Sqrt(scharrXValue * scharrXValue + scharrYValue * scharrYValue);
                    scharrMagnitudeImage[y, x] = new Gray((byte)magnitude);
                }
            }
            // 可以对Scharr边缘检测后的图像进行操作，如显示图像
            ShowImage(scharrMagnitudeImage);           
        }
        /// <summary>
        /// Laplacian算子
        /// </summary>
        /// <param name="path"></param>
        public static void Laplacian(string path)
        {
            // 加载灰度图像
            Image<Gray, byte> grayImage = new Image<Gray, byte>(path);
            // 使用Laplacian算子进行边缘检测
            Image<Gray, byte> laplacianImage = new Image<Gray, byte>(grayImage.Size);
            CvInvoke.Laplacian(grayImage, laplacianImage, DepthType.Default);
            // 可以对Laplacian边缘检测后的图像进行操作，如显示图像
            ShowImage(laplacianImage);           
        }
        /// <summary>
        /// Canny边缘检测
        /// </summary>
        /// <param name="path"></param>
        public static void Canny(string path)
        {
            // 加载图像，若为彩色图像则转换为灰度图
            Image<Bgr, byte> bgrImage = new Image<Bgr, byte>(path);
            Image<Gray, byte> grayImage = bgrImage.Convert<Gray, byte>();
            // 定义Canny边缘检测的参数
            double threshold1 = 50;  // 低阈值
            double threshold2 = 150; // 高阈值
            Mat cannyEdgesMat = new Mat();
            CvInvoke.Canny(grayImage, cannyEdgesMat, threshold1, threshold2);
            Image<Gray, byte> cannyEdgesImage = cannyEdgesMat.ToImage<Gray, byte>();
            ShowImage(cannyEdgesImage);            
        }
        /// <summary>
        /// 图像金字塔之高斯金字塔
        /// </summary>
        /// <param name="path"></param>
        public static void GaussianPyramid(string path)
        {
            // 加载图像
            Image<Bgr, byte> originalImage = new Image<Bgr, byte>(path);
            // 创建高斯金字塔
            List<Image<Bgr, byte>> gaussianPyramid = new List<Image<Bgr, byte>>();
            gaussianPyramid.Add(originalImage);
            Image<Bgr, byte> currentLevelImage = originalImage;
            for (int i = 0; i < 3; i++) // 构建3层高斯金字塔（可根据需要调整层数）
            {
                Image<Bgr, byte> downsampledImage = new Image<Bgr, byte>(currentLevelImage.Width / 2, currentLevelImage.Height / 2);
                CvInvoke.PyrDown(currentLevelImage, downsampledImage);
                gaussianPyramid.Add(downsampledImage);
                currentLevelImage = downsampledImage;
            }
            ShowImage(currentLevelImage);          
        }
        /// <summary>
        /// 拉普拉斯金字塔
        /// </summary>
        /// <param name="path"></param>
        public static void LaplacianPyramid(string path)
        {
            // 加载图像
            Image<Bgr, byte> originalImage = new Image<Bgr, byte>("your_image_path.jpg");
            // 创建高斯金字塔
            List<Image<Bgr, byte>> gaussianPyramid = new List<Image<Bgr, byte>>();
            gaussianPyramid.Add(originalImage);
            Image<Bgr, byte> currentLevelImage = originalImage;
            for (int i = 0; i < 3; i++) // 构建3层高斯金字塔（可根据需要调整层数）
            {
                Image<Bgr, byte> downsampledImage = new Image<Bgr, byte>(currentLevelImage.Width / 2, currentLevelImage.Height / 2);
                CvInvoke.PyrDown(currentLevelImage, downsampledImage);
                gaussianPyramid.Add(downsampledImage);
                currentLevelImage = downsampledImage;
            }


            List<Image<Bgr, byte>> laplacianPyramid = new List<Image<Bgr, byte>>();
            for (int i = 0; i < gaussianPyramid.Count - 1; i++)
            {
                Image<Bgr, byte> currentGaussianLevel = gaussianPyramid[i];
                Image<Bgr, byte> nextGaussianLevel = gaussianPyramid[i + 1];
                // 上采样下一层高斯金字塔图像
                Image<Bgr, byte> upsampledNextLevel = new Image<Bgr, byte>(currentGaussianLevel.Width, currentGaussianLevel.Height);
                CvInvoke.PyrUp(nextGaussianLevel, upsampledNextLevel);
                // 计算拉普拉斯金字塔当前层
                Image<Bgr, byte> laplacianLevel = new Image<Bgr, byte>(currentGaussianLevel.Size);
                CvInvoke.Subtract(currentGaussianLevel, upsampledNextLevel, laplacianLevel);
                laplacianPyramid.Add(laplacianLevel);
            }
            // 处理最后一层（高斯金字塔的最后一层作为拉普拉斯金字塔的最后一层）
            laplacianPyramid.Add(gaussianPyramid[gaussianPyramid.Count - 1]);
        }


        /// <summary>
        /// 查找轮廓
        /// </summary>
        /// <param name="path"></param>
        public static VectorOfVectorOfPoint ContourFinding(string path)
        {
            // 加载图像并转换为灰度图
            Image<Bgr, byte> bgrImage = new Image<Bgr, byte>(path);
            Image<Gray, byte> grayImage = bgrImage.Convert<Gray, byte>();
            // 进行阈值处理得到二值图像
            Mat thresholdedImageMat = new Mat();
            CvInvoke.Threshold(grayImage, thresholdedImageMat, 128, 255, ThresholdType.Binary);
            Image<Gray, byte> thresholdedImage = thresholdedImageMat.ToImage<Gray, byte>();
            // 查找轮廓
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            Mat hierarchy = new Mat();
            CvInvoke.FindContours(thresholdedImageMat, contours, hierarchy, RetrType.Tree, ChainApproxMethod.ChainApproxSimple);
            return contours;
        }
        /// <summary>
        /// 绘制轮廓
        /// </summary>
        /// <param name="path"></param>
        public static void ContourDraw(string path)
        {
            // 加载图像并转换为灰度图
            Image<Bgr, byte> bgrImage = new Image<Bgr, byte>(path);
            VectorOfVectorOfPoint contours = ContourFinding(path);

            // 创建一个用于绘制轮廓的彩色图像副本
            Image<Bgr, byte> contourImage = bgrImage.Copy();
            // 绘制轮廓
            MCvScalar borderValue = new MCvScalar(0.255, 255, 0);
            CvInvoke.DrawContours(contourImage, contours, -1, borderValue, 2);
            // 可以对绘制轮廓后的图像进行操作，如显示图像
            ShowImage(contourImage);          
        }
        /// <summary>
        /// 轮廓特征之特征矩
        /// </summary>
        /// <param name="path"></param>
        public static void Moments(string path)
        {
            // 加载图像并转换为灰度图
            Image<Bgr, byte> bgrImage = new Image<Bgr, byte>(path);
            VectorOfVectorOfPoint contours = ContourFinding(path);
            // 计算每个轮廓的特征矩
            for (int i = 0; i < contours.Size; i++)
            {
                Moments moment = CvInvoke.Moments(contours[i]);
                double area = moment.M00;
                double centroidX = moment.M10 / moment.M00;
                double centroidY = moment.M01 / moment.M00;
                // 可以根据需要输出或使用这些特征矩信息
                Console.WriteLine($"轮廓{i}的面积（通过矩计算）：{area}");
                Console.WriteLine($"轮廓{i}的质心X坐标：{centroidX}");
                Console.WriteLine($"轮廓{i}的质心Y坐标：{centroidY}");
            }
        }
        /// <summary>
        /// 轮廓面积
        /// </summary>
        /// <param name="path"></param>
        public static void ContourArea(string path)
        {
            // 加载图像并转换为灰度图
            Image<Bgr, byte> bgrImage = new Image<Bgr, byte>(path);
            VectorOfVectorOfPoint contours = ContourFinding(path);
            for (int i = 0; i < contours.Size; i++)
            {
                double area = CvInvoke.ContourArea(contours[i]);
                Console.WriteLine($"轮廓{i}的面积：{area}");
            }
        }
        /// <summary>
        /// 轮廓周长
        /// </summary>
        /// <param name="path"></param>
        public static void ContourPerimeter(string path)
        {
            // 加载图像并转换为灰度图
            Image<Bgr, byte> bgrImage = new Image<Bgr, byte>(path);
            VectorOfVectorOfPoint contours = ContourFinding(path);
            for (int i = 0; i < contours.Size; i++)
            {
                double perimeter = CvInvoke.ArcLength(contours[i], true);
                Console.WriteLine($"轮廓{i}的周长：{perimeter}");

            }
        }
        /// <summary>
        /// 轮廓近似
        /// </summary>
        /// <param name="path"></param>
        public static void ContourApproximation(string path)
        {
            // 加载图像并转换为灰度图
            Image<Bgr, byte> bgrImage = new Image<Bgr, byte>(path);
            VectorOfVectorOfPoint contours = ContourFinding(path);
            // 轮廓近似
            double epsilon = 0.01 * CvInvoke.ArcLength(contours[0], true);
            VectorOfPoint approxContour = new VectorOfPoint();
            CvInvoke.ApproxPolyDP(contours[0], approxContour, epsilon, true);
            // 创建一个用于绘制的图像副本
            Image<Bgr, byte> contourImage = bgrImage.Copy();
            // 绘制原始轮廓
            MCvScalar borderValue = new MCvScalar(0.255, 255, 0);
            CvInvoke.DrawContours(contourImage, contours, 0, borderValue, 2);
            // 绘制近似后的轮廓
            CvInvoke.DrawContours(contourImage, new VectorOfVectorOfPoint(approxContour), 0, borderValue, 2);
            // 可以对绘制轮廓后的图像进行操作，如显示图像
            ShowImage(contourImage); 
        }
        /// <summary>
        /// 轮廓凸包
        /// </summary>
        /// <param name="path"></param>
        public static void ConvexHull(string path)
        {
            // 加载图像并转换为灰度图
            Image<Bgr, byte> bgrImage = new Image<Bgr, byte>(path);
            VectorOfVectorOfPoint contours = ContourFinding(path);
            // 计算轮廓凸包
            VectorOfPoint convexHull = new VectorOfPoint();
            CvInvoke.ConvexHull(contours[0], convexHull);
            // 创建一个用于绘制的图像副本
            Image<Bgr, byte> contourImage = bgrImage.Copy();
            // 绘制原始轮廓
            MCvScalar borderValue = new MCvScalar(0.255, 255, 0);
            CvInvoke.DrawContours(contourImage, contours, 0, borderValue, 2);
            // 绘制凸包
            CvInvoke.DrawContours(contourImage, new VectorOfVectorOfPoint(convexHull), 0, borderValue, 2);
            // 可以对绘制轮廓后的图像进行操作，如显示图像
            ShowImage(contourImage);
        }
        /// <summary>
        /// 检查凸度
        /// </summary>
        /// <param name="path"></param>
        public static void ConvexityChecking(string path)
        {
            // 加载图像并转换为灰度图
            Image<Bgr, byte> bgrImage = new Image<Bgr, byte>(path);
            VectorOfVectorOfPoint contours = ContourFinding(path);
            // 计算轮廓凸包
            VectorOfPoint convexHull = new VectorOfPoint();
            CvInvoke.ConvexHull(contours[0], convexHull);
            // 检查凸度
            for (int i = 0; i < contours.Size; i++)
            {
                double contourArea = CvInvoke.ContourArea(contours[i]);
                double convexHullArea = CvInvoke.ContourArea(convexHull);
                bool isConvex = Math.Abs(contourArea - convexHullArea) <Math.Pow(10, -6);
                Console.WriteLine($"轮廓{i}是凸的吗？{(isConvex ? "是" : "不是")}");
            }
        }
        /// <summary>
        /// 直角边界矩形
        /// </summary>
        /// <param name="path"></param>
        public static void BoundingRectangleStraight(string path)
        {
            // 加载图像并转换为灰度图
            Image<Bgr, byte> bgrImage = new Image<Bgr, byte>(path);
            VectorOfVectorOfPoint contours = ContourFinding(path);
            MCvScalar borderValue = new MCvScalar(0.255, 255, 0);
            for (int i = 0; i < contours.Size; i++)
            {
                Rectangle boundingRect = CvInvoke.BoundingRectangle(contours[i]);
                // 可以对边界矩形进行操作，如绘制
                // 创建一个用于绘制的图像副本
                Image<Bgr, byte> contourImage = bgrImage.Copy();
                CvInvoke.Rectangle(contourImage, boundingRect, borderValue, 2);
                ShowImage(contourImage);
            }
        }
        /// <summary>
        /// 旋转边界矩形
        /// </summary>
        /// <param name="path"></param>
        public static void BoundingRectangleRotated(string path)
        {
            // 加载图像并转换为灰度图
            Image<Bgr, byte> bgrImage = new Image<Bgr, byte>(path);
            VectorOfVectorOfPoint contours = ContourFinding(path);
            MCvScalar borderValue = new MCvScalar(0.255, 255, 0);
            for (int i = 0; i < contours.Size; i++)
            {
                RotatedRect rotatedRect = CvInvoke.MinAreaRect(contours[i]);
                // 可以对旋转边界矩形进行操作，如绘制
                // 创建一个用于绘制的图像副本
                Image<Bgr, byte> contourImage = bgrImage.Copy();
                PointF[] vertices = new PointF[4];
                vertices=rotatedRect.GetVertices();
                for (int j = 0; j < 4; j++)
                {
                    PointF p1 = vertices[j];
                    PointF p2 = vertices[(j + 1) % 4];
                    CvInvoke.Line(contourImage, new Point((int)p1.X, (int)p1.Y), new Point((int)p2.X, (int)p2.Y), borderValue, 2);
                }
                ShowImage(contourImage);
            }

        }
        /// <summary>
        /// 最小闭合圈
        /// </summary>
        /// <param name="path"></param>
        public static void MinimalEnclosingCircle(string path)
        {
            // 加载图像并转换为灰度图
            Image<Bgr, byte> bgrImage = new Image<Bgr, byte>(path);
            VectorOfVectorOfPoint contours = ContourFinding(path);
            MCvScalar borderValue = new MCvScalar(0.255, 255, 0);
            // 计算最小闭合圆
            for (int i = 0; i < contours.Size; i++)
            {
                CircleF minEnclosingCircle = CvInvoke.MinEnclosingCircle(contours[i]);
                // 可以对最小闭合圆进行操作，如绘制
                // 创建一个用于绘制的图像副本
                Image<Bgr, byte> contourImage = bgrImage.Copy();
                CvInvoke.Circle(contourImage, new Point((int)minEnclosingCircle.Center.X, (int)minEnclosingCircle.Center.Y),
                                 (int)minEnclosingCircle.Radius, borderValue, 2);
                ShowImage(contourImage);
            }
        }
        /// <summary>
        /// 拟合一个椭圆
        /// </summary>
        /// <param name="path"></param>
        public static void FittingEllipse(string path)
        {
            // 加载图像并转换为灰度图
            Image<Bgr, byte> bgrImage = new Image<Bgr, byte>(path);
            VectorOfVectorOfPoint contours = ContourFinding(path);
            MCvScalar borderValue = new MCvScalar(0.255, 255, 0);
            for (int i = 0; i < contours.Size; i++)
            {
                RotatedRect ellipse = CvInvoke.FitEllipse(contours[i]);
                // 可以对拟合的椭圆进行操作，如绘制
                // 创建一个用于绘制的图像副本
                Image<Bgr, byte> contourImage = bgrImage.Copy();
                PointF[] vertices = new PointF[4];
                vertices=ellipse.GetVertices();
                for (int j = 0; j < 4; j++)
                {
                    PointF p1 = vertices[j];
                    PointF p2 = vertices[(j + 1) % 4];
                    CvInvoke.Line(contourImage, new Point((int)p1.X, (int)p1.Y), new Point((int)p2.X, (int)p2.Y), borderValue, 2);
                }
                ShowImage(contourImage);
            }
        }
        /// <summary>
        /// 拟合直线   有异常  TODO
        /// </summary>
        /// <param name="path"></param>
        public static void FittingLine(string path)
        {
            // 加载图像并转换为灰度图
            Image<Bgr, byte> bgrImage = new Image<Bgr, byte>(path);
            VectorOfVectorOfPoint contours = ContourFinding(path);
            MCvScalar borderValue = new MCvScalar(0.255, 255, 0);
            for (int i = 0; i < contours.Size; i++)
            {

                VectorOfVectorOfPoint line = new VectorOfVectorOfPoint();
                CvInvoke.FitLine(contours[i],line, DistType.L2, 0, 0.01, 0.01);
                // 可以根据直线参数绘制直线
                // 创建一个用于绘制的图像副本
                Image<Bgr, byte> contourImage = bgrImage.Copy();
                float x0 = line[2];
                float y0 = line[3];
                float x1 = x0 + 100 * line[0];
                float y1 = y0 + 100 * line[1];
                CvInvoke.Line(contourImage, new Point((int)x0, (int)y0), new Point((int)x1, (int)y1), borderValue, 2);
                ShowImage(contourImage);
            }

        }
       
        public static void TickMeter(string path)
        {


        }
        public static void ObjectTracking(string path)
        {
           
        }
        

        public static List<Vector3> Test(string path)
        {
            //加载图片
            Image<Bgr, byte> image = new Image<Bgr, byte>(path);
            // 灰度化
            Image<Gray, byte> grayImage = image.Convert<Gray, byte>();
            // 阈值处理，这里简单假设阈值为127，将图像二值化
            Image<Gray, byte> thresholdImage = grayImage.ThresholdBinary(new Gray(127), new Gray(255));
            List<Vector3> pointCloud = new List<Vector3>();
            for (int y = 0; y < thresholdImage.Rows; y++)
            {
                for (int x = 0; x < thresholdImage.Cols; x++)
                {
                    if (thresholdImage[y, x].Intensity > 0)
                    {
                        // 简单地假设z = 0，创建一个Vector3点
                        Vector3 point = new Vector3(x, y, 0);
                        pointCloud.Add(point);
                    }
                }
            }
            return pointCloud;

        }
        public static List<Vector3> Test2(string path)
        {

            List<Vector3> pointCloud = new List<Vector3>();
            // 加载图像，这里替换为实际的图像路径
            Image<Bgr, byte> image = new Image<Bgr, byte>(path);


            // 获取图像的宽度和高度（以像素为单位），后续用于坐标转换等计算
            int width = image.Width;
            int height = image.Height;

            // 假设相机内参（这里简单示例，实际需根据真实相机标定获取准确参数）
            double focalLengthX = 500;  // x方向焦距，示例值，需校准
            double focalLengthY = 500;  // y方向焦距，示例值，需校准
            double principalPointX = width / 2;  // 光心x坐标，简单取图像中心
            double principalPointY = height / 2; // 光心y坐标，简单取图像中心

            // 灰度化图像，为后续处理做准备
            Image<Gray, byte> grayImage = image.Convert<Gray, byte>();

            // 采用自适应阈值处理，相较于简单阈值能更好应对光照等变化，这里使用均值自适应阈值方法
            Image<Gray, byte> thresholdImage = grayImage.ThresholdAdaptive(new Gray(255), AdaptiveThresholdType.MeanC, ThresholdType.Binary, 11, new Gray(152));

            // 查找图像中的轮廓，以便更准确地获取物体边界和内部点
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            Mat hierarchy = new Mat();
            CvInvoke.FindContours(thresholdImage, contours, hierarchy, RetrType.External, ChainApproxMethod.ChainApproxSimple);
            // 遍历查找到的轮廓
            for (int i = 0; i < contours.Size; i++)
            {
                // 获取当前轮廓的点集合
                VectorOfPoint contour = contours[i];
                for (int j = 0; j < contour.Size; j++)
                {
                    // 获取轮廓上的点坐标（图像平面坐标，以像素为单位）
                    Point pointInImage = contour[j];

                    // 将图像平面坐标转换为相机坐标系下的坐标（这里简单假设深度值，实际可能通过深度传感器获取等方式确定）
                    double depth = 1.0;  // 示例深度值，可替换为真实深度
                    double x = (pointInImage.X - principalPointX) * depth / focalLengthX;
                    double y = (pointInImage.Y - principalPointY) * depth / focalLengthY;

                    // 创建对应的Vector3类型点，加入点云数据列表
                    Vector3 pointInCloud = new Vector3((float)x, (float)y, (float)depth);
                    pointCloud.Add(pointInCloud);
                }
            }
            return pointCloud;

        }
    }

}

