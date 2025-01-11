using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using Emgu.CV.UI;
using System;
using System.Drawing;
using System.IO;

namespace WinFormsApp.MyOpenCV.DLL
{
    public enum PictureType
    {
        JPG,
        JPEG,
        WEBP,
        PNG,
        GIF,
        FLIF,
        SVG,
        EPS,
        RAW,
        NEF,
        CR2,
        TIFF,
        BMP,
        ICO
    }

    public class CandyImageConverter
    {

        // 调整图像大小
        private static Image<Bgr, byte> ResizeImage(Image<Bgr, byte> image, int width, int height)
        {
            return image.Resize(width, height, Inter.Cubic);
        }

        // 文件转换
        public static void ConvertImage(string inputPath, string outputPath, int width, int height)
        {
            using (var image = new Image<Bgr, byte>(inputPath))
            {
                using (var resizedImage = ResizeImage(image, width, height))
                {
                    resizedImage.Save(outputPath);
                }
            }
        }
    }
}
