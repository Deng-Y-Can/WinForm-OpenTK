using OpenCvSharp;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace WinFormsApp.CandyTool
{
    public class ScreenRecorder : IDisposable
    {
        private readonly string outputPath;
        private readonly int frameRate;
        private readonly Rectangle captureRect;
        private readonly CaptureAreaType captureType;
        private readonly GraphicsPath customPath;
        private VideoWriter writer;
        private Thread recordingThread;
        private bool isRecording;
        private bool isDisposed;
        private DateTime startTime;
        private Action<string> statusCallback;
        private int frameCount = 0;
        private const int FrameBufferSize = 10;
        private Queue<Mat> frameBuffer = new Queue<Mat>();
        private object bufferLock = new object();
        private ManualResetEvent stopEvent = new ManualResetEvent(false);
        private Thread encodingThread;
        private bool isOpenCvInitialized = false;

        public bool IsRecording => isRecording;
        public TimeSpan RecordingTime => isRecording ? DateTime.Now - startTime : TimeSpan.Zero;

        public ScreenRecorder(string outputPath, int frameRate, Rectangle captureRect,
                             CaptureAreaType captureType = CaptureAreaType.FullScreen,
                             GraphicsPath customPath = null,
                             Action<string> statusCallback = null)
        {
            this.outputPath = outputPath;
            this.frameRate = frameRate;
            this.captureRect = captureRect;
            this.captureType = captureType;
            this.customPath = customPath;
            this.statusCallback = statusCallback;

            // 初始化OpenCV环境
            InitializeOpenCv();
        }

        private void InitializeOpenCv()
        {
            try
            {
                // 检查OpenCV是否可以正常初始化
                Cv2.GetBuildInformation();
                isOpenCvInitialized = true;
            }
            catch (Exception ex)
            {
                isOpenCvInitialized = false;
                Console.WriteLine($"OpenCV初始化失败: {ex.Message}");
                throw new Exception("OpenCV初始化失败，请确保OpenCV库已正确安装和配置。", ex);
            }
        }

        public void Start()
        {
            if (isRecording) return;

            // 检查OpenCV是否已初始化
            if (!isOpenCvInitialized)
            {
                throw new InvalidOperationException("OpenCV未初始化，无法开始录制。");
            }

            try
            {
                // 确保输出目录存在
                var directory = Path.GetDirectoryName(outputPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // 创建视频写入器 - 使用更兼容的编码器
                writer = CreateVideoWriter();

                if (!writer.IsOpened())
                {
                    throw new Exception("无法创建视频文件，可能是路径无效、权限不足或编码器不支持");
                }

                isRecording = true;
                startTime = DateTime.Now;
                frameCount = 0;
                frameBuffer = new Queue<Mat>(FrameBufferSize);
                stopEvent.Reset();

                statusCallback?.Invoke("正在录制...");

                // 启动帧捕获线程
                recordingThread = new Thread(RecordLoop);
                recordingThread.IsBackground = true;
                recordingThread.Start();

                // 启动编码线程
                encodingThread = new Thread(EncodingLoop);
                encodingThread.IsBackground = true;
                encodingThread.Start();
            }
            catch (Exception ex)
            {
                statusCallback?.Invoke($"录制失败: {ex.Message}");

                // 确保资源释放
                CleanupResources();
                throw;
            }
        }

        private VideoWriter CreateVideoWriter()
        {
            try
            {
                // 尝试使用MJPG编码器
                var writer = new VideoWriter(
                    outputPath,
                    FourCC.MJPG,
                    frameRate,
                    new OpenCvSharp.Size(captureRect.Width, captureRect.Height),
                    true);

                if (writer.IsOpened())
                {
                    Console.WriteLine("使用MJPG编码器成功");
                    return writer;
                }

                writer?.Release();

                // 如果MJPG失败，尝试使用XVID
                writer = new VideoWriter(
                    outputPath,
                    FourCC.XVID,
                    frameRate,
                    new OpenCvSharp.Size(captureRect.Width, captureRect.Height),
                    true);

                if (writer.IsOpened())
                {
                    Console.WriteLine("使用XVID编码器成功");
                    return writer;
                }

                writer?.Release();

                // 如果XVID也失败，尝试使用默认编码器
                writer = new VideoWriter(
                    outputPath,
                    0, // 使用默认编码器
                    frameRate,
                    new OpenCvSharp.Size(captureRect.Width, captureRect.Height),
                    true);

                if (writer.IsOpened())
                {
                    Console.WriteLine("使用默认编码器成功");
                    return writer;
                }

                throw new Exception("所有尝试的编码器均失败");
            }
            catch (TypeInitializationException ex)
            {
                Console.WriteLine($"编码器初始化失败: {ex.Message}");
                Console.WriteLine($"内部异常: {ex.InnerException?.Message}");

                // 尝试使用默认值
                try
                {
                    var writer = new VideoWriter(
                        outputPath,
                        0, // 使用默认编码器
                        frameRate,
                        new OpenCvSharp.Size(captureRect.Width, captureRect.Height),
                        true);

                    if (writer.IsOpened())
                    {
                        Console.WriteLine("使用默认编码器(备选方案)成功");
                        return writer;
                    }
                }
                catch (Exception)
                {
                    // 忽略
                }

                throw new Exception("编码器初始化失败，请确保OpenCV库已正确安装。", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"创建视频写入器失败: {ex.Message}");
                throw new Exception("创建视频写入器失败", ex);
            }
        }

        public void Stop()
        {
            if (!isRecording) return;

            isRecording = false;
            stopEvent.Set();

            // 等待捕获线程结束
            recordingThread?.Join(2000);

            // 等待编码线程处理完所有帧
            encodingThread?.Join(5000);

            // 释放OpenCV资源
            CleanupResources();

            statusCallback?.Invoke("录制已停止");
        }

        private void RecordLoop()
        {
            var frameInterval = 1000 / frameRate;
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            while (isRecording && !stopEvent.WaitOne(0))
            {
                var startTime = stopwatch.ElapsedMilliseconds;

                try
                {
                    CaptureFrame();

                    // 更新录制时间
                    frameCount++;
                    if (frameCount % frameRate == 0)
                    {
                        statusCallback?.Invoke($"正在录制... 已录制: {RecordingTime:mm\\:ss}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"捕获帧失败: {ex.Message}");
                    statusCallback?.Invoke($"录制异常: {ex.Message}");

                    // 发生错误时停止录制
                    isRecording = false;
                    break;
                }

                // 控制帧率
                var elapsed = stopwatch.ElapsedMilliseconds - startTime;
                if (elapsed < frameInterval)
                {
                    Thread.Sleep((int)(frameInterval - elapsed));
                }
            }
        }

        private void CaptureFrame()
        {
            if (!isRecording || stopEvent.WaitOne(0))
                return;

            try
            {
                using (var bitmap = CaptureScreen())
                {
                    // 将Bitmap转换为OpenCV的Mat
                    var mat = BitmapToMat(bitmap);

                    // 将帧添加到缓冲区
                    lock (bufferLock)
                    {
                        // 如果缓冲区已满，移除最旧的帧
                        if (frameBuffer.Count >= FrameBufferSize)
                        {
                            var oldFrame = frameBuffer.Dequeue();
                            oldFrame.Dispose();
                        }

                        frameBuffer.Enqueue(mat);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"捕获并转换帧失败: {ex.Message}");
                throw;
            }
        }

        private void EncodingLoop()
        {
            while (isRecording || frameBuffer.Count > 0)
            {
                try
                {
                    Mat frame = null;

                    // 从缓冲区获取帧
                    lock (bufferLock)
                    {
                        if (frameBuffer.Count > 0)
                        {
                            frame = frameBuffer.Dequeue();
                        }
                    }

                    // 如果有帧需要编码
                    if (frame != null)
                    {
                        try
                        {
                            // 写入帧
                            if (writer != null && writer.IsOpened())
                            {
                                writer.Write(frame);
                            }
                        }
                        finally
                        {
                            // 释放帧资源
                            frame.Dispose();
                        }
                    }
                    else
                    {
                        // 如果没有帧，短暂休眠
                        Thread.Sleep(10);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"编码帧失败: {ex.Message}");
                    statusCallback?.Invoke($"编码异常: {ex.Message}");

                    // 发生错误时停止录制
                    isRecording = false;
                    break;
                }
            }
        }

        private Bitmap CaptureScreen()
        {
            var bitmap = new Bitmap(captureRect.Width, captureRect.Height);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(captureRect.Location, System.Drawing.Point.Empty, captureRect.Size);

                // 如果是自定义区域，需要处理
                if (captureType != CaptureAreaType.FullScreen && customPath != null)
                {
                    using (var tempBitmap = new Bitmap(captureRect.Width, captureRect.Height))
                    {
                        using (var tempG = Graphics.FromImage(tempBitmap))
                        {
                            // 填充白色背景
                            tempG.FillRectangle(Brushes.White, 0, 0, tempBitmap.Width, tempBitmap.Height);

                            // 设置剪切区域为自定义形状
                            tempG.SetClip(customPath);

                            // 绘制原始位图
                            tempG.DrawImage(bitmap, 0, 0);
                        }

                        // 将临时位图复制回原始位图
                        using (var g2 = Graphics.FromImage(bitmap))
                        {
                            g2.Clear(Color.White);
                            g2.DrawImage(tempBitmap, 0, 0);
                        }
                    }
                }
            }

            return bitmap;
        }

        // 将Bitmap转换为OpenCV的Mat
        private Mat BitmapToMat(Bitmap bitmap)
        {
            try
            {
                // 创建与Bitmap尺寸相同的Mat
                var mat = new Mat(bitmap.Height, bitmap.Width, MatType.CV_8UC3);

                // 锁定Bitmap的位数据
                BitmapData bitmapData = bitmap.LockBits(
                    new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format24bppRgb);

                try
                {
                    // 使用Marshal.Copy逐行复制数据
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        IntPtr ptr = mat.Ptr(y);
                        int offset = y * bitmapData.Stride;

                        // 创建临时缓冲区
                        byte[] buffer = new byte[bitmapData.Stride];

                        // 从位图复制到缓冲区
                        Marshal.Copy(bitmapData.Scan0 + offset, buffer, 0, bitmapData.Stride);

                        // 从缓冲区复制到Mat
                        Marshal.Copy(buffer, 0, ptr, bitmapData.Stride);
                    }
                }
                finally
                {
                    // 解锁Bitmap
                    bitmap.UnlockBits(bitmapData);
                }

                // 转换BGR到RGB（OpenCV默认使用BGR顺序）
                Cv2.CvtColor(mat, mat, ColorConversionCodes.BGR2RGB);

                return mat;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"转换图像失败: {ex.Message}");
                throw;
            }
        }

        private void CleanupResources()
        {
            // 清空帧缓冲区
            lock (bufferLock)
            {
                while (frameBuffer.Count > 0)
                {
                    var frame = frameBuffer.Dequeue();
                    frame.Dispose();
                }
            }

            // 释放视频写入器
            writer?.Release();
            writer = null;

            // 释放事件
            stopEvent?.Dispose();
            stopEvent = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    // 释放托管资源
                    Stop();
                }

                isDisposed = true;
            }
        }
    }

    // 屏幕截图类
    public static class ScreenCapture
    {
        // 截取屏幕并保存为图片
        public static void CaptureAndSave(string filePath, Rectangle captureRect,
                                         CaptureAreaType captureType = CaptureAreaType.FullScreen,
                                         GraphicsPath customPath = null)
        {
            using (var bitmap = CaptureScreen(captureRect, captureType, customPath))
            {
                // 确保目录存在
                var directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                bitmap.Save(filePath, ImageFormat.Png);
            }
        }

        // 截取屏幕
        public static Bitmap CaptureScreen(Rectangle captureRect,
                                         CaptureAreaType captureType = CaptureAreaType.FullScreen,
                                         GraphicsPath customPath = null)
        {
            var bitmap = new Bitmap(captureRect.Width, captureRect.Height);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(captureRect.Location, System.Drawing.Point.Empty, captureRect.Size);

                // 如果是自定义区域，需要处理
                if (captureType != CaptureAreaType.FullScreen && customPath != null)
                {
                    using (var tempBitmap = new Bitmap(captureRect.Width, captureRect.Height))
                    {
                        using (var tempG = Graphics.FromImage(tempBitmap))
                        {
                            // 填充白色背景
                            tempG.FillRectangle(Brushes.White, 0, 0, tempBitmap.Width, tempBitmap.Height);

                            // 设置剪切区域为自定义形状
                            tempG.SetClip(customPath);

                            // 绘制原始位图
                            tempG.DrawImage(bitmap, 0, 0);
                        }

                        // 将临时位图复制回原始位图
                        using (var g2 = Graphics.FromImage(bitmap))
                        {
                            g2.Clear(Color.White);
                            g2.DrawImage(tempBitmap, 0, 0);
                        }
                    }
                }
            }

            return bitmap;
        }
    }

    // 捕获区域类型枚举
    public enum CaptureAreaType
    {
        FullScreen,
        Rectangle,
        Triangle,
        Circle
    }
}
