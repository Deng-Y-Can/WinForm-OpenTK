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
        public static void ConvertVideoFormat(string sourcePath, string targetPath,int width,int height)
        {
            VideoCapture videoCapture = new VideoCapture(sourcePath);
            if (!videoCapture.IsOpened)
            {
                Console.WriteLine("无法打开源视频文件");
                return;
            }
            Size size=new Size(width,height);
            //VideoWriter videoWriter = new VideoWriter(targetPath, VideoWriter.Fourcc(targetCodec[0], targetCodec[1], targetCodec[2], targetCodec[3]), size,true);

            VideoWriter videoWriter = null;
            string targetFormat = Path.GetExtension(targetPath).Trim('.');
            switch (targetFormat.ToUpper())
            {
                case "MP4":
                    videoWriter = new VideoWriter(targetPath, VideoWriter.Fourcc('H', '2', '6', '4'), size, true);
                    break;
                case "AVI":
                    videoWriter = new VideoWriter(targetPath, VideoWriter.Fourcc('X', 'V', 'I', 'D'), size, true);
                    break;
                case "MKV":
                    videoWriter = new VideoWriter(targetPath, VideoWriter.Fourcc('V', 'P', '8', '0'), size, true);
                    break;
                case "FLV":
                    videoWriter = new VideoWriter(targetPath, VideoWriter.Fourcc('F', 'L', 'V', '1'), size, true);
                    break;
                case "M3U8":
                    // M3U8处理相对复杂些，这里简单示意用HLS相关编码，实际可能需要更多处理逻辑
                    videoWriter = new VideoWriter(targetPath, VideoWriter.Fourcc('H', 'L', 'S', '1'), size, true);
                    break;
                case "TS":
                    videoWriter = new VideoWriter(targetPath, VideoWriter.Fourcc('M', 'P', '2', 'T'), size, true);
                    break;
                case "MOV":
                    videoWriter = new VideoWriter(targetPath, VideoWriter.Fourcc('A', 'P', 'C', 'M'), size, true);
                    break;
                case "PRORES":
                    videoWriter = new VideoWriter(targetPath, VideoWriter.Fourcc('A', 'P', 'C', 'M'), size, true);
                    break;
                case "DNXHD":
                    videoWriter = new VideoWriter(targetPath, VideoWriter.Fourcc('D', 'N', 'X', 'H'), size, true);
                    break;
                case "DNXHR":
                    videoWriter = new VideoWriter(targetPath, VideoWriter.Fourcc('D', 'N', 'X', 'H'), size, true);
                    break;
                case "WMV":
                    videoWriter = new VideoWriter(targetPath, VideoWriter.Fourcc('W', 'M', 'V', '2'), size, true);
                    break;
                case "RM":
                    videoWriter = new VideoWriter(targetPath, VideoWriter.Fourcc('R', 'V', '1', '0'), size, true);
                    break;
                case "RMVB":
                    videoWriter = new VideoWriter(targetPath, VideoWriter.Fourcc('R', 'V', '4', '0'), size, true);
                    break;
                default:
                    Console.WriteLine($"不支持的目标视频格式: {targetFormat}");
                    return;
            }

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
