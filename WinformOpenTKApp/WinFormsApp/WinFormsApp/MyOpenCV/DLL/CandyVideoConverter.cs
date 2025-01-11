using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.IO;

namespace WinFormsApp.MyOpenCV.DLL
{
    public enum VedioType
    {
        MP4,
        AVI,
        MKV,
        FLV,
        M3U8,
        TS,
        MOV,
        ProRes,
        DNxHD,
        DNxHR,
        WMV,
        RM,
        RMVB
    }
    public class CandyVideoConverter
    {
        public static void ConvertVideoFormat(string sourcePath, string targetPath, string targetCodec, int targetFps, Size targetSize,int width,int height)
        {
            VideoCapture videoCapture = new VideoCapture(sourcePath);
            if (!videoCapture.IsOpened)
            {
                Console.WriteLine("无法打开源视频文件");
                return;
            }
            Size size=new Size(width,height);
            VideoWriter videoWriter = new VideoWriter(targetPath, VideoWriter.Fourcc(targetCodec[0], targetCodec[1], targetCodec[2], targetCodec[3]), size,true);
            if (!videoWriter.IsOpened)
            {
                Console.WriteLine("无法创建目标视频文件写入器");
                return;
            }

            Mat frame = new Mat();
            while (videoCapture.Read(frame))
            {
                // 可以在这里对每一帧进行额外的处理，比如调整大小、添加滤镜等计算机视觉相关操作，此处暂不做额外处理
                videoWriter.Write(frame);
            }

            videoCapture.Release();
            videoWriter.Dispose();
        }
    }
}
