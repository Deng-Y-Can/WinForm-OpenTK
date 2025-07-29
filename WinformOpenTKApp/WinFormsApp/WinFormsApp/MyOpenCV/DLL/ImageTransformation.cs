using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace WinFormsApp.MyOpenCV.DLL
{
    public static class ImageTransformation
    {

        public static void ApplyEffect(ImageEffect effect, Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            switch (effect)
            {
                case ImageEffect.Mosaic:
                    ApplyMosaic(bitmap, rect, 8, isPointInSelection);
                    break;
                case ImageEffect.Blur:
                    ApplyBlur(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.Sharpen:
                    ApplySharpen(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.Emboss:
                    ApplyEmboss(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.GaussianBlur:
                    ApplyGaussianBlur(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.Grayscale:
                    ApplyGrayscale(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.Invert:
                    ApplyInvert(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.Brightness:
                    ApplyBrightness(bitmap, rect, 50, isPointInSelection);
                    break;
                case ImageEffect.Contrast:
                    ApplyContrast(bitmap, rect, 1.5f, isPointInSelection);
                    break;
                case ImageEffect.Cartoon:
                    ApplyCartoon(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.Vintage:
                    ApplyVintage(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.EdgeDetection:
                    ApplyEdgeDetection(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.HistogramEqualization:
                    ApplyHistogramEqualization(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.Dilate:
                    ApplyDilate(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.Erode:
                    ApplyErode(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.Open:
                    ApplyOpen(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.Close:
                    ApplyClose(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.SharpenEnhance:
                    ApplySharpenEnhance(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.ColorBalance:
                    ApplyColorBalance(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.BlurEnhance:
                    ApplyBlurEnhance(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.HueAdjust:
                    ApplyHueAdjust(bitmap, rect, 30f, isPointInSelection);
                    break;
                case ImageEffect.SaturationAdjust:
                    ApplySaturationAdjust(bitmap, rect, 1.5f, isPointInSelection);
                    break;
                case ImageEffect.BlurEdgeDetection:
                    ApplyBlurEdgeDetection(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.Sketch:
                    ApplySketch(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.Watercolor:
                    ApplyWatercolor(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.Neon:
                    ApplyNeon(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.SaturationContrast:
                    ApplySaturationContrast(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.SmartSharpen:
                    ApplySmartSharpen(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.WatercolorStyle:
                    ApplyWatercolorStyle(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.PrintStyle:
                    ApplyPrintStyle(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.Illustration:
                    ApplyIllustration(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.Impressionist:
                    ApplyImpressionist(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.LightEffect:
                    ApplyLightEffect(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.FilmGrain:
                    ApplyFilmGrain(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.WarmTone:
                    ApplyWarmTone(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.PopArt:
                    ApplyPopArt(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.InkPainting:
                    ApplyInkPainting(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.Snow:
                    ApplySnow(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.Sepia:
                    ApplySepia(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.SepiaTone:
                    ApplySepiaTone(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.Denoise:
                    ApplyDenoise(bitmap, rect, isPointInSelection);
                    break;
                case ImageEffect.OldPhoto:
                    ApplyOldPhoto(bitmap, rect, isPointInSelection);
                    break;
            }
        }

        public static void ReplaceColor(Bitmap bitmap, Color target, Color replacement)
        {
            BitmapData data = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

            IntPtr ptr = data.Scan0;
            int bytes = Math.Abs(data.Stride) * data.Height;
            byte[] rgbValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            int targetArgb = target.ToArgb();
            int replacementArgb = replacement.ToArgb();

            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                int pixelArgb = (rgbValues[i + 3] << 24) | (rgbValues[i + 2] << 16) | (rgbValues[i + 1] << 8) | rgbValues[i];
                if (pixelArgb == targetArgb)
                {
                    rgbValues[i] = replacement.B;
                    rgbValues[i + 1] = replacement.G;
                    rgbValues[i + 2] = replacement.R;
                    rgbValues[i + 3] = replacement.A;
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bitmap.UnlockBits(data);
        }

        public static void ReplaceColorInSelection(Bitmap bitmap, Color target, Color replacement, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            BitmapData data = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

            IntPtr ptr = data.Scan0;
            int bytes = Math.Abs(data.Stride) * rect.Height;
            byte[] rgbValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            int targetArgb = target.ToArgb();
            int replacementArgb = replacement.ToArgb();

            for (int y = 0; y < rect.Height; y++)
            {
                for (int x = 0; x < rect.Width; x++)
                {
                    int index = y * data.Stride + x * 4;

                    if (isPointInSelection(x, y))
                    {
                        int pixelArgb = (rgbValues[index + 3] << 24) | (rgbValues[index + 2] << 16) | (rgbValues[index + 1] << 8) | rgbValues[index];
                        if (pixelArgb == targetArgb)
                        {
                            rgbValues[index] = replacement.B;
                            rgbValues[index + 1] = replacement.G;
                            rgbValues[index + 2] = replacement.R;
                            rgbValues[index + 3] = replacement.A;
                        }
                    }
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bitmap.UnlockBits(data);
        }

        public static void ReplaceColorRange(Bitmap bitmap, int minR, int maxR, int minG, int maxG, int minB, int maxB, Color targetColor)
        {
            BitmapData data = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

            IntPtr ptr = data.Scan0;
            int bytes = Math.Abs(data.Stride) * data.Height;
            byte[] rgbValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                byte b = rgbValues[i];
                byte g = rgbValues[i + 1];
                byte r = rgbValues[i + 2];

                if (r >= minR && r <= maxR &&
                    g >= minG && g <= maxG &&
                    b >= minB && b <= maxB)
                {
                    rgbValues[i] = targetColor.B;
                    rgbValues[i + 1] = targetColor.G;
                    rgbValues[i + 2] = targetColor.R;
                    rgbValues[i + 3] = targetColor.A;
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bitmap.UnlockBits(data);
        }

        public static void ReplaceColorRangeInSelection(Bitmap bitmap, int minR, int maxR, int minG, int maxG, int minB, int maxB, Color targetColor, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            BitmapData data = bitmap.LockBits(
                rect,
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

            IntPtr ptr = data.Scan0;
            int bytes = Math.Abs(data.Stride) * rect.Height;
            byte[] rgbValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            for (int y = 0; y < rect.Height; y++)
            {
                for (int x = 0; x < rect.Width; x++)
                {
                    int index = y * data.Stride + x * 4;

                    if (isPointInSelection(x, y))
                    {
                        byte b = rgbValues[index];
                        byte g = rgbValues[index + 1];
                        byte r = rgbValues[index + 2];

                        if (r >= minR && r <= maxR &&
                            g >= minG && g <= maxG &&
                            b >= minB && b <= maxB)
                        {
                            rgbValues[index] = targetColor.B;
                            rgbValues[index + 1] = targetColor.G;
                            rgbValues[index + 2] = targetColor.R;
                            rgbValues[index + 3] = targetColor.A;
                        }
                    }
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bitmap.UnlockBits(data);
        }

        public static void ApplyImageInSelection(Bitmap destination, Bitmap source, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            for (int y = 0; y < rect.Height; y++)
            {
                for (int x = 0; x < rect.Width; x++)
                {
                    if (isPointInSelection(x, y))
                    {
                        destination.SetPixel(
                            rect.X + x,
                            rect.Y + y,
                            source.GetPixel(x, y));
                    }
                }
            }
        }

        public static void ApplyMosaic(Bitmap bitmap, Rectangle rect, int blockSize, Func<int, int, bool> isPointInSelection)
        {
            using (Bitmap tempBitmap = new Bitmap(bitmap))
            {
                for (int y = 0; y < rect.Height; y += blockSize)
                {
                    for (int x = 0; x < rect.Width; x += blockSize)
                    {
                        if (!isPointInSelection(x, y))
                            continue;

                        int blockWidth = Math.Min(blockSize, rect.Width - x);
                        int blockHeight = Math.Min(blockSize, rect.Height - y);

                        Color avgColor = GetAverageColor(tempBitmap, rect.X + x, rect.Y + y, blockWidth, blockHeight);

                        for (int dy = 0; dy < blockHeight; dy++)
                        {
                            for (int dx = 0; dx < blockWidth; dx++)
                            {
                                if (isPointInSelection(x + dx, y + dy))
                                {
                                    bitmap.SetPixel(rect.X + x + dx, rect.Y + y + dy, avgColor);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static Color GetAverageColor(Bitmap bitmap, int x, int y, int width, int height)
        {
            long sumR = 0, sumG = 0, sumB = 0;
            int count = 0;

            for (int dy = 0; dy < height; dy++)
            {
                for (int dx = 0; dx < width; dx++)
                {
                    int px = x + dx;
                    int py = y + dy;

                    if (px >= 0 && px < bitmap.Width && py >= 0 && py < bitmap.Height)
                    {
                        Color pixel = bitmap.GetPixel(px, py);
                        sumR += pixel.R;
                        sumG += pixel.G;
                        sumB += pixel.B;
                        count++;
                    }
                }
            }

            if (count == 0) return Color.Black;

            return Color.FromArgb(
                (int)(sumR / count),
                (int)(sumG / count),
                (int)(sumB / count)
            );
        }

        public static void ApplyBlur(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            try
            {
                Bitmap selectionBmp = new Bitmap(rect.Width, rect.Height);

                using (Graphics g = Graphics.FromImage(selectionBmp))
                {
                    g.DrawImage(bitmap, 0, 0, rect, GraphicsUnit.Pixel);
                }

                using (Image<Bgr, byte> cvImage = BitmapToImageBgr(selectionBmp))
                {
                    using (Image<Bgr, byte> blurredImage = new Image<Bgr, byte>(cvImage.Size))
                    {
                        int kernelSize = 5;
                        if (kernelSize % 2 == 0) kernelSize++;

                        CvInvoke.GaussianBlur(
                            cvImage,
                            blurredImage,
                            new Size(kernelSize, kernelSize),
                            0,
                            0,
                            BorderType.Default);

                        for (int y = 0; y < rect.Height; y++)
                        {
                            for (int x = 0; x < rect.Width; x++)
                            {
                                if (isPointInSelection(x, y))
                                {
                                    Bgr color = blurredImage[y, x];
                                    bitmap.SetPixel(
                                        rect.X + x,
                                        rect.Y + y,
                                        Color.FromArgb((int)color.Red, (int)color.Green, (int)color.Blue));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"应用模糊效果时出错: {ex.Message}");
            }
        }

        public static void ApplySharpen(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Bitmap tempBitmap = new Bitmap(bitmap))
            {
                float[,] kernel = {
                    {0, -1, 0},
                    {-1, 5, -1},
                    {0, -1, 0}
                };

                ApplyConvolution(bitmap, tempBitmap, rect, kernel, isPointInSelection);
            }
        }

        public static void ApplyEmboss(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Bitmap tempBitmap = new Bitmap(bitmap))
            {
                float[,] kernel = {
                    {-2, -1, 0},
                    {-1, 1, 1},
                    {0, 1, 2}
                };

                ApplyConvolution(bitmap, tempBitmap, rect, kernel, isPointInSelection);
            }
        }

        public static void ApplyGaussianBlur(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Bitmap tempBitmap = new Bitmap(bitmap))
            using (Graphics g = Graphics.FromImage(tempBitmap))
            {
                using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
                {
                    using (Image<Bgr, byte> blurredImage = new Image<Bgr, byte>(cvImage.Size))
                    {
                        CvInvoke.GaussianBlur(cvImage, blurredImage, new Size(5, 5), 0);
                        ApplyEffectToSelection(bitmap, rect, blurredImage, isPointInSelection);
                    }
                }
            }
        }

        public static void ApplyGrayscale(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Bitmap tempBitmap = new Bitmap(bitmap))
            {
                for (int y = 0; y < rect.Height; y++)
                {
                    for (int x = 0; x < rect.Width; x++)
                    {
                        if (isPointInSelection(x, y))
                        {
                            Color pixel = tempBitmap.GetPixel(rect.X + x, rect.Y + y);
                            int gray = (int)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);
                            bitmap.SetPixel(rect.X + x, rect.Y + y, Color.FromArgb(gray, gray, gray));
                        }
                    }
                }
            }
        }

        public static void ApplyInvert(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Bitmap tempBitmap = new Bitmap(bitmap))
            {
                for (int y = 0; y < rect.Height; y++)
                {
                    for (int x = 0; x < rect.Width; x++)
                    {
                        if (isPointInSelection(x, y))
                        {
                            Color pixel = tempBitmap.GetPixel(rect.X + x, rect.Y + y);
                            bitmap.SetPixel(rect.X + x, rect.Y + y, Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B));
                        }
                    }
                }
            }
        }

        public static void ApplyBrightness(Bitmap bitmap, Rectangle rect, int value, Func<int, int, bool> isPointInSelection)
        {
            using (Bitmap tempBitmap = new Bitmap(bitmap))
            {
                for (int y = 0; y < rect.Height; y++)
                {
                    for (int x = 0; x < rect.Width; x++)
                    {
                        if (isPointInSelection(x, y))
                        {
                            Color pixel = tempBitmap.GetPixel(rect.X + x, rect.Y + y);
                            int r = Math.Min(255, Math.Max(0, pixel.R + value));
                            int g = Math.Min(255, Math.Max(0, pixel.G + value));
                            int b = Math.Min(255, Math.Max(0, pixel.B + value));
                            bitmap.SetPixel(rect.X + x, rect.Y + y, Color.FromArgb(r, g, b));
                        }
                    }
                }
            }
        }

        public static void ApplyContrast(Bitmap bitmap, Rectangle rect, float value, Func<int, int, bool> isPointInSelection)
        {
            using (Bitmap tempBitmap = new Bitmap(bitmap))
            {
                for (int y = 0; y < rect.Height; y++)
                {
                    for (int x = 0; x < rect.Width; x++)
                    {
                        if (isPointInSelection(x, y))
                        {
                            Color pixel = tempBitmap.GetPixel(rect.X + x, rect.Y + y);
                            int r = Math.Min(255, Math.Max(0, (int)((pixel.R / 255.0 - 0.5) * value + 0.5) * 255));
                            int g = Math.Min(255, Math.Max(0, (int)((pixel.G / 255.0 - 0.5) * value + 0.5) * 255));
                            int b = Math.Min(255, Math.Max(0, (int)((pixel.B / 255.0 - 0.5) * value + 0.5) * 255));
                            bitmap.SetPixel(rect.X + x, rect.Y + y, Color.FromArgb(r, g, b));
                        }
                    }
                }
            }
        }

        public static void ApplyCartoon(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Bitmap tempBitmap = new Bitmap(bitmap))
            using (Graphics g = Graphics.FromImage(tempBitmap))
            {
                using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
                using (Image<Gray, byte> grayImage = cvImage.Convert<Gray, byte>())
                using (Image<Gray, byte> blurredGrayImage = new Image<Gray, byte>(grayImage.Size))
                using (Image<Gray, byte> edgesImage = new Image<Gray, byte>(grayImage.Size))
                {
                    CvInvoke.MedianBlur(grayImage, blurredGrayImage, 7);
                    CvInvoke.AdaptiveThreshold(
                        blurredGrayImage, edgesImage, 255,
                        AdaptiveThresholdType.MeanC, ThresholdType.Binary, 9, 2);

                    using (Image<Bgr, byte> filteredImage = new Image<Bgr, byte>(cvImage.Size))
                    {
                        CvInvoke.BilateralFilter(cvImage, filteredImage, 9, 75, 75);

                        using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
                        {
                            CvInvoke.BitwiseAnd(filteredImage, filteredImage, resultImage, edgesImage);
                            ApplyEffectToSelection(bitmap, rect, resultImage, isPointInSelection);
                        }
                    }
                }
            }
        }

        public static void ApplyVintage(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Bitmap tempBitmap = new Bitmap(bitmap))
            {
                float[,] kernel = {
                    {0.272f, 0.534f, 0.131f},
                    {0.349f, 0.686f, 0.168f},
                    {0.393f, 0.769f, 0.189f}
                };

                ApplyConvolution(bitmap, tempBitmap, rect, kernel, isPointInSelection);
            }
        }

        public static void ApplyConvolution(Bitmap destBitmap, Bitmap srcBitmap, Rectangle rect, float[,] kernel, Func<int, int, bool> isPointInSelection)
        {
            int kernelSize = kernel.GetLength(0);
            int halfSize = kernelSize / 2;

            for (int y = 0; y < rect.Height; y++)
            {
                for (int x = 0; x < rect.Width; x++)
                {
                    if (isPointInSelection(x, y))
                    {
                        double r = 0, g = 0, b = 0;
                        double kernelSum = 0;

                        for (int ky = 0; ky < kernelSize; ky++)
                        {
                            for (int kx = 0; kx < kernelSize; kx++)
                            {
                                int srcX = rect.X + x + kx - halfSize;
                                int srcY = rect.Y + y + ky - halfSize;

                                if (srcX >= 0 && srcX < srcBitmap.Width && srcY >= 0 && srcY < srcBitmap.Height)
                                {
                                    Color pixel = srcBitmap.GetPixel(srcX, srcY);
                                    float weight = kernel[ky, kx];

                                    r += pixel.R * weight;
                                    g += pixel.G * weight;
                                    b += pixel.B * weight;
                                    kernelSum += Math.Abs(weight);
                                }
                            }
                        }

                        if (kernelSum > 0)
                        {
                            r /= kernelSum;
                            g /= kernelSum;
                            b /= kernelSum;
                        }

                        int finalR = Math.Min(255, Math.Max(0, (int)r));
                        int finalG = Math.Min(255, Math.Max(0, (int)g));
                        int finalB = Math.Min(255, Math.Max(0, (int)b));

                        destBitmap.SetPixel(rect.X + x, rect.Y + y, Color.FromArgb(finalR, finalG, finalB));
                    }
                }
            }
        }

        public static Image<Bgr, byte> BitmapToImageBgr(Bitmap bitmap)
        {
            Image<Bgr, byte> result = new Image<Bgr, byte>(bitmap.Width, bitmap.Height);
            BitmapData data = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);

            try
            {
                IntPtr ptr = data.Scan0;
                int bytes = Math.Abs(data.Stride) * data.Height;
                byte[] rgbValues = new byte[bytes];
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        int index = y * data.Stride + x * 3;
                        result[y, x] = new Bgr(rgbValues[index + 2], rgbValues[index + 1], rgbValues[index]);
                    }
                }
            }
            finally
            {
                bitmap.UnlockBits(data);
            }

            return result;
        }

        public static void ApplyHistogramEqualization(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Ycc, byte> yccImage = cvImage.Convert<Ycc, byte>())
            {
                Image<Gray, byte>[] channels = yccImage.Split();

                CvInvoke.EqualizeHist(channels[0], channels[0]);

                using (Image<Ycc, byte> resultYcc = new Image<Ycc, byte>(cvImage.Size))
                {
                    for (int y = 0; y < cvImage.Height; y++)
                    {
                        for (int x = 0; x < cvImage.Width; x++)
                        {
                            Ycc pixel = new Ycc(
                                channels[0][y, x].Intensity,
                                channels[1][y, x].Intensity,
                                channels[2][y, x].Intensity
                            );
                            resultYcc[y, x] = pixel;
                        }
                    }

                    using (Image<Bgr, byte> resultImage = resultYcc.Convert<Bgr, byte>())
                    {
                        ApplyEffectToSelection(bitmap, rect, resultImage, isPointInSelection);
                    }
                }

                foreach (var channel in channels)
                    channel.Dispose();
            }
        }

        public static void ApplyDilate(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> dilatedImage = new Image<Bgr, byte>(cvImage.Size))
            {
                using (Mat structElement = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1)))
                {
                    CvInvoke.Dilate(cvImage, dilatedImage, structElement, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
                }

                ApplyEffectToSelection(bitmap, rect, dilatedImage, isPointInSelection);
            }
        }

        public static void ApplyErode(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> erodedImage = new Image<Bgr, byte>(cvImage.Size))
            {
                using (Mat structElement = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1)))
                {
                    CvInvoke.Erode(cvImage, erodedImage, structElement, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
                }

                ApplyEffectToSelection(bitmap, rect, erodedImage, isPointInSelection);
            }
        }

        public static void ApplyOpen(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> openedImage = new Image<Bgr, byte>(cvImage.Size))
            {
                using (Mat structElement = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1)))
                {
                    CvInvoke.MorphologyEx(cvImage, openedImage, MorphOp.Open, structElement, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
                }

                ApplyEffectToSelection(bitmap, rect, openedImage, isPointInSelection);
            }
        }

        public static void ApplyClose(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> closedImage = new Image<Bgr, byte>(cvImage.Size))
            {
                using (Mat structElement = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1)))
                {
                    CvInvoke.MorphologyEx(cvImage, closedImage, MorphOp.Close, structElement, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
                }

                ApplyEffectToSelection(bitmap, rect, closedImage, isPointInSelection);
            }
        }

        public static void ApplyEdgeDetection(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Gray, byte> grayImage = cvImage.Convert<Gray, byte>())
            using (Image<Gray, byte> edgeImage = new Image<Gray, byte>(grayImage.Size))
            {
                CvInvoke.Canny(grayImage, edgeImage, 50, 150);

                using (Image<Bgr, byte> edgeBgr = edgeImage.Convert<Bgr, byte>())
                {
                    ApplyEffectToSelection(bitmap, rect, edgeBgr, isPointInSelection);
                }
            }
        }

        public static void ApplyEffectToSelection(Bitmap destBitmap, Rectangle rect, Image<Bgr, byte> sourceImage, Func<int, int, bool> isPointInSelection)
        {
            for (int y = 0; y < rect.Height; y++)
            {
                for (int x = 0; x < rect.Width; x++)
                {
                    if (isPointInSelection(x, y) &&
                        y + rect.Y < destBitmap.Height &&
                        x + rect.X < destBitmap.Width)
                    {
                        Bgr color = sourceImage[y + rect.Y, x + rect.X];
                        destBitmap.SetPixel(rect.X + x, rect.Y + y,
                            Color.FromArgb((int)color.Red, (int)color.Green, (int)color.Blue));
                    }
                }
            }
        }

        public static void ApplySharpenEnhance(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> sharpenedImage = new Image<Bgr, byte>(cvImage.Size))
            {
                float[,] kernel = {
                    { -1, -1, -1 },
                    { -1,  9, -1 },
                    { -1, -1, -1 }
                };

                ApplyConvolution(cvImage, sharpenedImage, kernel);
                ApplyEffectToSelection(bitmap, rect, sharpenedImage, isPointInSelection);
            }
        }

        public static void ApplyColorBalance(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> balancedImage = new Image<Bgr, byte>(cvImage.Size))
            {
                double redGain = 1.1;
                double greenGain = 1.0;
                double blueGain = 0.9;

                for (int y = 0; y < cvImage.Rows; y++)
                {
                    for (int x = 0; x < cvImage.Cols; x++)
                    {
                        Bgr pixel = cvImage[y, x];
                        byte red = (byte)Math.Min(255, Math.Max(0, pixel.Red * redGain));
                        byte green = (byte)Math.Min(255, Math.Max(0, pixel.Green * greenGain));
                        byte blue = (byte)Math.Min(255, Math.Max(0, pixel.Blue * blueGain));
                        balancedImage[y, x] = new Bgr(blue, green, red);
                    }
                }

                ApplyEffectToSelection(bitmap, rect, balancedImage, isPointInSelection);
            }
        }

        public static void ApplyBlurEnhance(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> blurredImage = new Image<Bgr, byte>(cvImage.Size))
            {
                CvInvoke.BilateralFilter(cvImage, blurredImage, 9, 75, 75);

                using (Image<Bgr, byte> detailImage = cvImage.Sub(blurredImage))
                {
                    using (Image<Bgr, byte> enhancedImage = cvImage.Add(detailImage.Mul(0.5)))
                    {
                        ApplyEffectToSelection(bitmap, rect, enhancedImage, isPointInSelection);
                    }
                }
            }
        }

        public static void ApplyConvolution(Image<Bgr, byte> source, Image<Bgr, byte> dest, float[,] kernel)
        {
            int kernelSize = kernel.GetLength(0);
            int halfSize = kernelSize / 2;

            for (int y = 0; y < source.Rows; y++)
            {
                for (int x = 0; x < source.Cols; x++)
                {
                    double r = 0, g = 0, b = 0;
                    double kernelSum = 0;

                    for (int ky = 0; ky < kernelSize; ky++)
                    {
                        for (int kx = 0; kx < kernelSize; kx++)
                        {
                            int srcX = x + kx - halfSize;
                            int srcY = y + ky - halfSize;

                            if (srcX >= 0 && srcX < source.Cols && srcY >= 0 && srcY < source.Rows)
                            {
                                Bgr pixel = source[srcY, srcX];
                                float weight = kernel[ky, kx];

                                r += pixel.Red * weight;
                                g += pixel.Green * weight;
                                b += pixel.Blue * weight;
                                kernelSum += Math.Abs(weight);
                            }
                        }
                    }

                    if (kernelSum > 0)
                    {
                        r /= kernelSum;
                        g /= kernelSum;
                        b /= kernelSum;
                    }

                    dest[y, x] = new Bgr(
                        Math.Min(255, Math.Max(0, (int)r)),
                        Math.Min(255, Math.Max(0, (int)g)),
                        Math.Min(255, Math.Max(0, (int)b))
                    );
                }
            }
        }

        public static void ApplyHueAdjust(Bitmap bitmap, Rectangle rect, float hueShift, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Hsv, byte> hsvImage = cvImage.Convert<Hsv, byte>())
            {
                for (int y = rect.Top; y < rect.Bottom && y < hsvImage.Rows; y++)
                {
                    for (int x = rect.Left; x < rect.Right && x < hsvImage.Cols; x++)
                    {
                        Hsv pixel = hsvImage[y, x];
                        MCvScalar scalar = pixel.MCvScalar;
                        scalar.V0 = (int)((scalar.V0 + hueShift) % 180);
                        pixel.MCvScalar = scalar;
                        hsvImage[y, x] = pixel;
                    }
                }

                using (Image<Bgr, byte> resultImage = hsvImage.Convert<Bgr, byte>())
                {
                    ApplyEffectToSelection(bitmap, rect, resultImage, isPointInSelection);
                }
            }
        }

        public static void ApplySaturationAdjust(Bitmap bitmap, Rectangle rect, float saturationFactor, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Hsv, byte> hsvImage = cvImage.Convert<Hsv, byte>())
            {
                for (int y = rect.Top; y < rect.Bottom && y < hsvImage.Rows; y++)
                {
                    for (int x = rect.Left; x < rect.Right && x < hsvImage.Cols; x++)
                    {
                        Hsv pixel = hsvImage[y, x];
                        MCvScalar scalar = pixel.MCvScalar;
                        double newSat = scalar.V1 * saturationFactor;
                        scalar.V1 = Math.Min(255, Math.Max(0, newSat));
                        pixel.MCvScalar = scalar;
                        hsvImage[y, x] = pixel;
                    }
                }

                using (Image<Bgr, byte> resultImage = hsvImage.Convert<Bgr, byte>())
                {
                    ApplyEffectToSelection(bitmap, rect, resultImage, isPointInSelection);
                }
            }
        }

        public static void ApplyBlurEdgeDetection(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Gray, byte> grayImage = cvImage.Convert<Gray, byte>())
            using (Image<Gray, byte> blurredImage = new Image<Gray, byte>(grayImage.Size))
            using (Image<Gray, byte> edgeImage = new Image<Gray, byte>(grayImage.Size))
            {
                CvInvoke.GaussianBlur(grayImage, blurredImage, new Size(5, 5), 0);
                CvInvoke.Canny(blurredImage, edgeImage, 20, 60);

                using (Image<Bgr, byte> resultImage = cvImage.Clone())
                {
                    for (int y = rect.Top; y < rect.Bottom && y < resultImage.Rows; y++)
                    {
                        for (int x = rect.Left; x < rect.Right && x < resultImage.Cols; x++)
                        {
                            if (edgeImage[y, x].Intensity > 128)
                            {
                                Bgr original = cvImage[y, x];
                                resultImage[y, x] = new Bgr(
                                    (byte)Math.Min(255, original.Blue * 1.2),
                                    (byte)Math.Min(255, original.Green * 1.2),
                                    (byte)Math.Min(255, original.Red * 1.2)
                                );
                            }
                            else
                            {
                                byte gray = (byte)grayImage[y, x].Intensity;
                                resultImage[y, x] = new Bgr(gray, gray, gray);
                            }
                        }
                    }

                    ApplyEffectToSelection(bitmap, rect, resultImage, isPointInSelection);
                }
            }
        }

        public static void ApplySketch(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Gray, byte> grayImage = cvImage.Convert<Gray, byte>())
            using (Image<Gray, byte> invertedImage = new Image<Gray, byte>(grayImage.Size))
            using (Image<Gray, byte> blurredImage = new Image<Gray, byte>(grayImage.Size))
            using (Image<Gray, byte> sketchImage = new Image<Gray, byte>(grayImage.Size))
            {
                CvInvoke.BitwiseNot(grayImage, invertedImage);
                CvInvoke.GaussianBlur(invertedImage, blurredImage, new Size(21, 21), 0);

                for (int y = rect.Top; y < rect.Bottom && y < sketchImage.Rows; y++)
                {
                    for (int x = rect.Left; x < rect.Right && x < sketchImage.Cols; x++)
                    {
                        byte gray = (byte)grayImage[y, x].Intensity;
                        byte blurred = (byte)blurredImage[y, x].Intensity;

                        if (blurred == 255) blurred = 254;

                        int value = gray * 255 / (255 - blurred);
                        sketchImage[y, x] = new Gray((byte)Math.Min(255, value));
                    }
                }

                using (Image<Bgr, byte> resultImage = sketchImage.Convert<Bgr, byte>())
                {
                    ApplyEffectToSelection(bitmap, rect, resultImage, isPointInSelection);
                }
            }
        }

        public static void ApplyWatercolor(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
            {
                CvInvoke.MedianBlur(cvImage, resultImage, 9);

                using (Image<Gray, byte> grayImage = resultImage.Convert<Gray, byte>())
                using (Image<Gray, byte> edgeImage = new Image<Gray, byte>(grayImage.Size))
                {
                    CvInvoke.Canny(grayImage, edgeImage, 80, 160);

                    for (int y = rect.Top; y < rect.Bottom && y < resultImage.Rows; y++)
                    {
                        for (int x = rect.Left; x < rect.Right && x < resultImage.Cols; x++)
                        {
                            if (edgeImage[y, x].Intensity > 100)
                            {
                                Bgr pixel = resultImage[y, x];
                                resultImage[y, x] = new Bgr(
                                    Math.Max(0, pixel.Blue - 30),
                                    Math.Max(0, pixel.Green - 30),
                                    Math.Max(0, pixel.Red - 30)
                                );
                            }
                        }
                    }

                    using (Image<Hsv, byte> hsvImage = resultImage.Convert<Hsv, byte>())
                    {
                        for (int y = rect.Top; y < rect.Bottom && y < hsvImage.Rows; y++)
                        {
                            for (int x = rect.Left; x < rect.Right && x < hsvImage.Cols; x++)
                            {
                                Hsv pixel = hsvImage[y, x];
                                MCvScalar scalar = pixel.MCvScalar;
                                scalar.V1 = Math.Min(255, scalar.V1 * 1.3);
                                pixel.MCvScalar = scalar;
                                hsvImage[y, x] = pixel;
                            }
                        }

                        using (Image<Bgr, byte> finalImage = hsvImage.Convert<Bgr, byte>())
                        {
                            ApplyEffectToSelection(bitmap, rect, finalImage, isPointInSelection);
                        }
                    }
                }
            }
        }

        public static void ApplyNeon(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Hsv, byte> hsvImage = cvImage.Convert<Hsv, byte>())
            using (Image<Bgr, byte> glowImage = new Image<Bgr, byte>(cvImage.Size))
            using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
            {
                for (int y = rect.Top; y < rect.Bottom && y < hsvImage.Rows; y++)
                {
                    for (int x = rect.Left; x < rect.Right && x < hsvImage.Cols; x++)
                    {
                        Hsv pixel = hsvImage[y, x];
                        MCvScalar scalar = pixel.MCvScalar;
                        scalar.V1 = Math.Min(255, scalar.V1 * 1.5);
                        scalar.V2 = Math.Min(255, scalar.V2 * 1.2);
                        pixel.MCvScalar = scalar;
                        hsvImage[y, x] = pixel;
                    }
                }

                using (Image<Bgr, byte> colorEnhanced = hsvImage.Convert<Bgr, byte>())
                {
                    using (Image<Gray, byte> grayImage = colorEnhanced.Convert<Gray, byte>())
                    using (Image<Gray, byte> edgeImage = new Image<Gray, byte>(grayImage.Size))
                    {
                        CvInvoke.Canny(grayImage, edgeImage, 50, 150);

                        for (int y = rect.Top; y < rect.Bottom && y < glowImage.Rows; y++)
                        {
                            for (int x = rect.Left; x < rect.Right && x < glowImage.Cols; x++)
                            {
                                if (edgeImage[y, x].Intensity > 100)
                                {
                                    Bgr original = colorEnhanced[y, x];
                                    glowImage[y, x] = new Bgr(
                                        Math.Min(255, original.Blue * 1.8),
                                        Math.Min(255, original.Green * 1.8),
                                        Math.Min(255, original.Red * 1.8)
                                    );
                                }
                                else
                                {
                                    Bgr original = colorEnhanced[y, x];
                                    glowImage[y, x] = new Bgr(
                                        Math.Max(0, original.Blue / 2),
                                        Math.Max(0, original.Green / 2),
                                        Math.Max(0, original.Red / 2)
                                    );
                                }
                            }
                        }

                        using (Image<Bgr, byte> blurredGlow = new Image<Bgr, byte>(glowImage.Size))
                        {
                            CvInvoke.GaussianBlur(glowImage, blurredGlow, new Size(7, 7), 0);
                            CvInvoke.AddWeighted(colorEnhanced, 0.7, blurredGlow, 0.3, 0, resultImage);
                            ApplyEffectToSelection(bitmap, rect, resultImage, isPointInSelection);
                        }
                    }
                }
            }
        }

        public static void ApplySmartSharpen(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> blurredImage = new Image<Bgr, byte>(cvImage.Size))
            using (Image<Bgr, byte> sharpenedImage = new Image<Bgr, byte>(cvImage.Size))
            {
                CvInvoke.GaussianBlur(cvImage, blurredImage, new Size(5, 5), 0);
                float sharpenIntensity = 1.5f;

                for (int y = rect.Top; y < rect.Bottom && y < cvImage.Rows; y++)
                {
                    for (int x = rect.Left; x < rect.Right && x < cvImage.Cols; x++)
                    {
                        Bgr original = cvImage[y, x];
                        Bgr blurred = blurredImage[y, x];

                        byte blue = (byte)Math.Min(255, Math.Max(0, original.Blue + (original.Blue - blurred.Blue) * sharpenIntensity));
                        byte green = (byte)Math.Min(255, Math.Max(0, original.Green + (original.Green - blurred.Green) * sharpenIntensity));
                        byte red = (byte)Math.Min(255, Math.Max(0, original.Red + (original.Red - blurred.Red) * sharpenIntensity));

                        sharpenedImage[y, x] = new Bgr(blue, green, red);
                    }
                }

                ApplyEffectToSelection(bitmap, rect, sharpenedImage, isPointInSelection);
            }
        }

        public static void ApplySaturationContrast(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Hsv, byte> hsvImage = cvImage.Convert<Hsv, byte>())
            {
                for (int y = rect.Top; y < rect.Bottom && y < hsvImage.Rows; y++)
                {
                    for (int x = rect.Left; x < rect.Right && x < hsvImage.Cols; x++)
                    {
                        Hsv pixel = hsvImage[y, x];
                        MCvScalar scalar = pixel.MCvScalar;
                        scalar.V1 = Math.Min(255, scalar.V1 * 1.4);
                        pixel.MCvScalar = scalar;
                        hsvImage[y, x] = pixel;
                    }
                }

                using (Image<Bgr, byte> colorEnhanced = hsvImage.Convert<Bgr, byte>())
                {
                    using (Image<Bgr, byte> contrastImage = new Image<Bgr, byte>(colorEnhanced.Size))
                    {
                        float contrastFactor = 1.3f;
                        int contrastCenter = 128;

                        for (int y = rect.Top; y < rect.Bottom && y < colorEnhanced.Rows; y++)
                        {
                            for (int x = rect.Left; x < rect.Right && x < colorEnhanced.Cols; x++)
                            {
                                Bgr pixel = colorEnhanced[y, x];

                                int newBlue = (int)((pixel.Blue - contrastCenter) * contrastFactor + contrastCenter);
                                int newGreen = (int)((pixel.Green - contrastCenter) * contrastFactor + contrastCenter);
                                int newRed = (int)((pixel.Red - contrastCenter) * contrastFactor + contrastCenter);

                                pixel.Blue = (byte)Math.Max(0, Math.Min(255, newBlue));
                                pixel.Green = (byte)Math.Max(0, Math.Min(255, newGreen));
                                pixel.Red = (byte)Math.Max(0, Math.Min(255, newRed));

                                contrastImage[y, x] = pixel;
                            }
                        }

                        ApplyEffectToSelection(bitmap, rect, contrastImage, isPointInSelection);
                    }
                }
            }
        }

        public static void ApplyWatercolorStyle(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
            {
                CvInvoke.MedianBlur(cvImage, resultImage, 7);
                CvInvoke.GaussianBlur(resultImage, resultImage, new Size(3, 3), 0);

                using (Image<Hsv, byte> hsvImage = resultImage.Convert<Hsv, byte>())
                {
                    for (int y = rect.Top; y < rect.Bottom && y < hsvImage.Rows; y++)
                    {
                        for (int x = rect.Left; x < rect.Right && x < hsvImage.Cols; x++)
                        {
                            Hsv pixel = hsvImage[y, x];
                            MCvScalar scalar = pixel.MCvScalar;
                            scalar.V1 = Math.Min(255, scalar.V1 * 1.2);
                            scalar.V2 = Math.Min(255, scalar.V2 * 1.05);
                            pixel.MCvScalar = scalar;
                            hsvImage[y, x] = pixel;
                        }
                    }

                    using (Image<Bgr, byte> finalImage = hsvImage.Convert<Bgr, byte>())
                    {
                        ApplyEffectToSelection(bitmap, rect, finalImage, isPointInSelection);
                    }
                }
            }
        }

        public static void ApplyPrintStyle(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Gray, byte> grayImage = cvImage.Convert<Gray, byte>())
            using (Image<Gray, byte> edgeImage = new Image<Gray, byte>(grayImage.Size))
            using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
            {
                CvInvoke.Canny(grayImage, edgeImage, 50, 150);

                for (int y = rect.Top; y < rect.Bottom && y < resultImage.Rows; y++)
                {
                    for (int x = rect.Left; x < rect.Right && x < resultImage.Cols; x++)
                    {
                        if (edgeImage[y, x].Intensity > 100)
                        {
                            resultImage[y, x] = new Bgr(0, 0, 0);
                        }
                        else
                        {
                            Bgr original = cvImage[y, x];
                            byte blue = (byte)((original.Blue / 64) * 64);
                            byte green = (byte)((original.Green / 64) * 64);
                            byte red = (byte)((original.Red / 64) * 64);
                            resultImage[y, x] = new Bgr(blue, green, red);
                        }
                    }
                }

                ApplyEffectToSelection(bitmap, rect, resultImage, isPointInSelection);
            }
        }

        public static void ApplyIllustration(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
            using (Image<Gray, byte> grayImage = cvImage.Convert<Gray, byte>())
            using (Image<Gray, byte> edgeImage = new Image<Gray, byte>(grayImage.Size))
            {
                CvInvoke.Canny(grayImage, edgeImage, 50, 150);

                using (Image<Bgr, byte> quantizedImage = new Image<Bgr, byte>(cvImage.Size))
                {
                    for (int y = rect.Top; y < rect.Bottom && y < cvImage.Rows; y++)
                    {
                        for (int x = rect.Left; x < rect.Right && x < cvImage.Cols; x++)
                        {
                            Bgr pixel = cvImage[y, x];
                            pixel.Blue = (byte)((pixel.Blue / 64) * 64 + 32);
                            pixel.Green = (byte)((pixel.Green / 64) * 64 + 32);
                            pixel.Red = (byte)((pixel.Red / 64) * 64 + 32);
                            quantizedImage[y, x] = pixel;
                        }
                    }

                    for (int y = rect.Top; y < rect.Bottom && y < resultImage.Rows; y++)
                    {
                        for (int x = rect.Left; x < rect.Right && x < resultImage.Cols; x++)
                        {
                            if (edgeImage[y, x].Intensity > 100)
                            {
                                resultImage[y, x] = new Bgr(0, 0, 0);
                            }
                            else
                            {
                                resultImage[y, x] = quantizedImage[y, x];
                            }
                        }
                    }

                    ApplyEffectToSelection(bitmap, rect, resultImage, isPointInSelection);
                }
            }
        }

        public static void ApplyImpressionist(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
            {
                CvInvoke.MedianBlur(cvImage, resultImage, 5);
                CvInvoke.GaussianBlur(resultImage, resultImage, new Size(3, 3), 0);

                using (Image<Hsv, byte> hsvImage = resultImage.Convert<Hsv, byte>())
                {
                    for (int y = rect.Top; y < rect.Bottom && y < hsvImage.Rows; y++)
                    {
                        for (int x = rect.Left; x < rect.Right && x < hsvImage.Cols; x++)
                        {
                            Hsv pixel = hsvImage[y, x];
                            MCvScalar scalar = pixel.MCvScalar;
                            scalar.V1 = Math.Min(255, scalar.V1 * 1.3);
                            pixel.MCvScalar = scalar;
                            hsvImage[y, x] = pixel;
                        }
                    }

                    using (Image<Bgr, byte> finalImage = hsvImage.Convert<Bgr, byte>())
                    {
                        ApplyEffectToSelection(bitmap, rect, finalImage, isPointInSelection);
                    }
                }
            }
        }

        public static void ApplyLightEffect(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            {
                using (Image<Bgr, byte> lightEffect = new Image<Bgr, byte>(cvImage.Size))
                {
                    for (int y = 0; y < cvImage.Rows; y++)
                    {
                        for (int x = 0; x < cvImage.Cols; x++)
                        {
                            float intensity = 1.0f - (float)Math.Sqrt(Math.Pow(x - cvImage.Cols, 2) + Math.Pow(y, 2)) /
                                              (float)Math.Sqrt(Math.Pow(cvImage.Cols, 2) + Math.Pow(cvImage.Rows, 2));

                            byte lightValue = (byte)(intensity * 50);
                            lightEffect[y, x] = new Bgr(lightValue, lightValue + 20, lightValue + 30);
                        }
                    }

                    using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
                    {
                        CvInvoke.AddWeighted(cvImage, 1, lightEffect, 0.7, 0, resultImage);
                        ApplyEffectToSelection(bitmap, rect, resultImage, isPointInSelection);
                    }
                }
            }
        }

        public static void ApplyFilmGrain(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            {
                using (Image<Bgr, byte> noiseImage = new Image<Bgr, byte>(cvImage.Size))
                {
                    CvInvoke.Randn(noiseImage, new MCvScalar(0, 0, 0), new MCvScalar(15, 15, 15));

                    using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
                    {
                        CvInvoke.AddWeighted(cvImage, 1, noiseImage, 0.3, 0, resultImage);
                        ApplyEffectToSelection(bitmap, rect, resultImage, isPointInSelection);
                    }
                }
            }
        }

        public static void ApplyWarmTone(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Hsv, byte> hsvImage = cvImage.Convert<Hsv, byte>())
            {
                for (int y = rect.Top; y < rect.Bottom && y < hsvImage.Rows; y++)
                {
                    for (int x = rect.Left; x < rect.Right && x < hsvImage.Cols; x++)
                    {
                        Hsv pixel = hsvImage[y, x];
                        MCvScalar scalar = pixel.MCvScalar;
                        scalar.V0 = (scalar.V0 + 10) % 180;
                        scalar.V1 = Math.Min(255, scalar.V1 * 1.2);
                        scalar.V2 = Math.Min(255, scalar.V2 * 1.1);
                        pixel.MCvScalar = scalar;
                        hsvImage[y, x] = pixel;
                    }
                }

                using (Image<Bgr, byte> resultImage = hsvImage.Convert<Bgr, byte>())
                {
                    ApplyEffectToSelection(bitmap, rect, resultImage, isPointInSelection);
                }
            }
        }

        public static void ApplyPopArt(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Gray, byte> grayImage = cvImage.Convert<Gray, byte>())
            {
                using (Image<Gray, byte> binaryImage = new Image<Gray, byte>(grayImage.Size))
                {
                    CvInvoke.Threshold(grayImage, binaryImage, 128, 255, ThresholdType.Binary);

                    using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
                    {
                        for (int y = rect.Top; y < rect.Bottom && y < cvImage.Rows; y++)
                        {
                            for (int x = rect.Left; x < rect.Right && x < cvImage.Cols; x++)
                            {
                                if (binaryImage[y, x].Intensity > 128)
                                {
                                    resultImage[y, x] = new Bgr(0, 255, 255); // 亮区使用青色
                                }
                                else
                                {
                                    resultImage[y, x] = new Bgr(255, 0, 0); // 暗区使用蓝色
                                }
                            }
                        }

                        ApplyEffectToSelection(bitmap, rect, resultImage, isPointInSelection);
                    }
                }
            }
        }

        public static void ApplyInkPainting(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Gray, byte> grayImage = cvImage.Convert<Gray, byte>())
            {
                using (Image<Gray, byte> blurredImage = new Image<Gray, byte>(grayImage.Size))
                {
                    CvInvoke.GaussianBlur(grayImage, blurredImage, new Size(5, 5), 0);

                    using (Image<Gray, byte> edgeImage = new Image<Gray, byte>(grayImage.Size))
                    {
                        CvInvoke.Canny(blurredImage, edgeImage, 50, 150);

                        using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
                        {
                            for (int y = rect.Top; y < rect.Bottom && y < resultImage.Rows; y++)
                            {
                                for (int x = rect.Left; x < rect.Right && x < resultImage.Cols; x++)
                                {
                                    if (edgeImage[y, x].Intensity > 100)
                                    {
                                        resultImage[y, x] = new Bgr(0, 0, 0); // 边缘区域为黑色（模拟墨水）
                                    }
                                    else
                                    {
                                        byte gray = (byte)(blurredImage[y, x].Intensity * 0.3);
                                        resultImage[y, x] = new Bgr(gray, gray, gray); // 非边缘区域为灰色（模拟宣纸）
                                    }
                                }
                            }

                            ApplyEffectToSelection(bitmap, rect, resultImage, isPointInSelection);
                        }
                    }
                }
            }
        }

        public static void ApplySnow(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            {
                using (Image<Bgr, byte> snowNoise = new Image<Bgr, byte>(cvImage.Size))
                {
                    CvInvoke.Randn(snowNoise, new MCvScalar(0, 0, 0), new MCvScalar(20, 20, 20));

                    using (Image<Gray, byte> grayNoise = snowNoise.Convert<Gray, byte>())
                    using (Image<Gray, byte> binaryNoise = new Image<Gray, byte>(grayNoise.Size))
                    {
                        CvInvoke.Threshold(grayNoise, binaryNoise, 200, 255, ThresholdType.Binary);

                        using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
                        {
                            for (int y = rect.Top; y < rect.Bottom && y < cvImage.Rows; y++)
                            {
                                for (int x = rect.Left; x < rect.Right && x < cvImage.Cols; x++)
                                {
                                    if (binaryNoise[y, x].Intensity > 128)
                                    {
                                        Bgr pixel = cvImage[y, x];
                                        resultImage[y, x] = new Bgr(
                                        Math.Min(255, pixel.Blue + 30),
                                        Math.Min(255, pixel.Green + 30),
                                        Math.Min(255, pixel.Red + 30)
                                        );
                                    }
                                    else
                                    {
                                        resultImage[y, x] = cvImage[y, x];
                                    }
                                }
                            }

                            ApplyEffectToSelection(bitmap, rect, resultImage, isPointInSelection);
                        }
                    }
                }
            }
        }

        public static void ApplySepia(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            {
                using (Image<Bgr, byte> sepiaImage = new Image<Bgr, byte>(cvImage.Size))
                {
                    double[,] sepiaMatrix = {
{0.393, 0.769, 0.189},
{0.349, 0.686, 0.168},
{0.272, 0.534, 0.131}
};

                    for (int y = rect.Top; y < rect.Bottom && y < cvImage.Rows; y++)
                    {
                        for (int x = rect.Left; x < rect.Right && x < cvImage.Cols; x++)
                        {
                            Bgr pixel = cvImage[y, x];
                            double r = pixel.Red * sepiaMatrix[0, 0] + pixel.Green * sepiaMatrix[0, 1] + pixel.Blue * sepiaMatrix[0, 2];
                            double g = pixel.Red * sepiaMatrix[1, 0] + pixel.Green * sepiaMatrix[1, 1] + pixel.Blue * sepiaMatrix[1, 2];
                            double b = pixel.Red * sepiaMatrix[2, 0] + pixel.Green * sepiaMatrix[2, 1] + pixel.Blue * sepiaMatrix[2, 2];

                            sepiaImage[y, x] = new Bgr(
                            (byte)Math.Min(255, Math.Max(0, b)),
                            (byte)Math.Min(255, Math.Max(0, g)),
                            (byte)Math.Min(255, Math.Max(0, r))
                            );
                        }
                    }

                    ApplyEffectToSelection(bitmap, rect, sepiaImage, isPointInSelection);
                }
            }
        }

        public static void ApplySepiaTone(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            {
                using (Image<Bgr, byte> sepiaImage = new Image<Bgr, byte>(cvImage.Size))
                {
                    double[,] sepiaMatrix = {
{0.393, 0.769, 0.189},
{0.349, 0.686, 0.168},
{0.272, 0.534, 0.131}
};

                    for (int y = rect.Top; y < rect.Bottom && y < cvImage.Rows; y++)
                    {
                        for (int x = rect.Left; x < rect.Right && x < cvImage.Cols; x++)
                        {
                            Bgr pixel = cvImage[y, x];
                            double r = pixel.Red * sepiaMatrix[0, 0] + pixel.Green * sepiaMatrix[0, 1] + pixel.Blue * sepiaMatrix[0, 2];
                            double g = pixel.Red * sepiaMatrix[1, 0] + pixel.Green * sepiaMatrix[1, 1] + pixel.Blue * sepiaMatrix[1, 2];
                            double b = pixel.Red * sepiaMatrix[2, 0] + pixel.Green * sepiaMatrix[2, 1] + pixel.Blue * sepiaMatrix[2, 2];

                            sepiaImage[y, x] = new Bgr(
                            (byte)Math.Min(255, Math.Max(0, b * 1.1)),
                            (byte)Math.Min(255, Math.Max(0, g * 1.05)),
                            (byte)Math.Min(255, Math.Max(0, r * 1.0))
                            );
                        }
                    }

                    ApplyEffectToSelection(bitmap, rect, sepiaImage, isPointInSelection);
                }
            }
        }

        public static void ApplyDenoise(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            {
                using (Image<Bgr, byte> denoisedImage = new Image<Bgr, byte>(cvImage.Size))
                {
                    CvInvoke.MedianBlur(cvImage, denoisedImage, 5);
                    ApplyEffectToSelection(bitmap, rect, denoisedImage, isPointInSelection);
                }
            }
        }

        public static void ApplyOldPhoto(Bitmap bitmap, Rectangle rect, Func<int, int, bool> isPointInSelection)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            {
                using (Image<Bgr, byte> sepiaImage = new Image<Bgr, byte>(cvImage.Size))
                {
                    double[,] sepiaMatrix = {
{0.393, 0.769, 0.189},
{0.349, 0.686, 0.168},
{0.272, 0.534, 0.131}
};

                    for (int y = rect.Top; y < rect.Bottom && y < cvImage.Rows; y++)
                    {
                        for (int x = rect.Left; x < rect.Right && x < cvImage.Cols; x++)
                        {
                            Bgr pixel = cvImage[y, x];
                            double r = pixel.Red * sepiaMatrix[0, 0] + pixel.Green * sepiaMatrix[0, 1] + pixel.Blue * sepiaMatrix[0, 2];
                            double g = pixel.Red * sepiaMatrix[1, 0] + pixel.Green * sepiaMatrix[1, 1] + pixel.Blue * sepiaMatrix[1, 2];
                            double b = pixel.Red * sepiaMatrix[2, 0] + pixel.Green * sepiaMatrix[2, 1] + pixel.Blue * sepiaMatrix[2, 2];

                            sepiaImage[y, x] = new Bgr(
                            (byte)Math.Min(255, Math.Max(0, b)),
                            (byte)Math.Min(255, Math.Max(0, g)),
                            (byte)Math.Min(255, Math.Max(0, r))
                            );
                        }
                    }

                    using (Image<Bgr, byte> vignetteImage = new Image<Bgr, byte>(sepiaImage.Size))
                    {
                        for (int y = 0; y < sepiaImage.Rows; y++)
                        {
                            for (int x = 0; x < sepiaImage.Cols; x++)
                            {
                                double dx = x - sepiaImage.Width / 2.0;
                                double dy = y - sepiaImage.Height / 2.0;
                                double r = Math.Sqrt(dx * dx + dy * dy) /
                                Math.Sqrt((sepiaImage.Width / 2.0) * (sepiaImage.Height / 2.0));

                                float factor = (float)(0.7 + 0.3 * (1 - r));
                                Bgr pixel = sepiaImage[y, x];
                                vignetteImage[y, x] = new Bgr(
                                (byte)(pixel.Blue * factor),
                                (byte)(pixel.Green * factor),
                                (byte)(pixel.Red * factor)
                                );
                            }
                        }

                        using (Image<Bgr, byte> noiseImage = new Image<Bgr, byte>(vignetteImage.Size))
                        {
                            CvInvoke.Randn(noiseImage, new MCvScalar(0, 0, 0), new MCvScalar(5, 5, 5));
                            CvInvoke.AddWeighted(vignetteImage, 0.95, noiseImage, 0.05, 0, vignetteImage);

                            ApplyEffectToSelection(bitmap, rect, vignetteImage, isPointInSelection);
                        }
                    }
                }
            }
        }


    }



    public enum SelectionMode
    {
        Rectangle,
        Ellipse
    }
    /// <summary>
    /// 图像处理效果枚举
    /// </summary>
    public enum ImageEffect
    {
        /// <summary>
        /// 无效果
        /// </summary>
        None,

        /// <summary>
        /// 马赛克效果
        /// </summary>
        Mosaic,

        /// <summary>
        /// 普通模糊
        /// </summary>
        Blur,

        /// <summary>
        /// 锐化效果
        /// </summary>
        Sharpen,

        /// <summary>
        /// 浮雕效果
        /// </summary>
        Emboss,

        /// <summary>
        /// 高斯模糊
        /// </summary>
        GaussianBlur,

        /// <summary>
        /// 灰度化
        /// </summary>
        Grayscale,

        /// <summary>
        /// 反色效果
        /// </summary>
        Invert,

        /// <summary>
        /// 亮度调整
        /// </summary>
        Brightness,

        /// <summary>
        /// 对比度调整
        /// </summary>
        Contrast,

        /// <summary>
        /// 卡通效果
        /// </summary>
        Cartoon,

        /// <summary>
        /// 复古风格
        /// </summary>
        Vintage,

        /// <summary>
        /// 边缘检测
        /// </summary>
        EdgeDetection,

        /// <summary>
        /// 直方图均衡化（增强对比度）
        /// </summary>
        HistogramEqualization,

        /// <summary>
        /// 膨胀（形态学操作）
        /// </summary>
        Dilate,

        /// <summary>
        /// 腐蚀（形态学操作）
        /// </summary>
        Erode,

        /// <summary>
        /// 开运算（先腐蚀后膨胀）
        /// </summary>
        Open,

        /// <summary>
        /// 闭运算（先膨胀后腐蚀）
        /// </summary>
        Close,

        ///// <summary>
        ///// 顺时针旋转90度
        ///// </summary>
        //Rotate90,

        ///// <summary>
        ///// 水平翻转
        ///// </summary>
        //FlipHorizontal,

        ///// <summary>
        ///// 垂直翻转
        ///// </summary>
        //FlipVertical,

        /// <summary>
        /// 增强锐化
        /// </summary>
        SharpenEnhance,

        /// <summary>
        /// 色彩平衡调整
        /// </summary>
        ColorBalance,

        /// <summary>
        /// 模糊增强（边缘保留模糊）
        /// </summary>
        BlurEnhance,

        /// <summary>
        /// 色相调整
        /// </summary>
        HueAdjust,

        /// <summary>
        /// 饱和度调整
        /// </summary>
        SaturationAdjust,

        /// <summary>
        /// 带模糊的边缘检测
        /// </summary>
        BlurEdgeDetection,

        /// <summary>
        /// 柔化效果
        /// </summary>
        //Soften,

        /// <summary>
        /// 素描效果
        /// </summary>
        Sketch,

        /// <summary>
        /// 梦幻效果
        /// </summary>
        // Dreamy,

        /// <summary>
        /// 水彩风格
        /// </summary>
        Watercolor,

        /// <summary>
        /// 霓虹效果
        /// </summary>
        Neon,

        /// <summary>
        /// 饱和度对比度增强
        /// </summary>
        SaturationContrast,

        /// <summary>
        /// 智能锐化
        /// </summary>
        SmartSharpen,

        /// <summary>
        /// 水彩画风格
        /// </summary>
        WatercolorStyle,

        /// <summary>
        /// 印刷风格
        /// </summary>
        PrintStyle,

        /// <summary>
        /// 插画风格
        /// </summary>
        Illustration,

        /// <summary>
        /// 印象派风格
        /// </summary>
        Impressionist,

        /// <summary>
        /// 光照效果
        /// </summary>
        LightEffect,

        /// <summary>
        /// 渐变映射
        /// </summary>
        // GradientMap,

        /// <summary>
        /// 鱼眼效果
        /// </summary>
        //Fisheye,

        /// <summary>
        /// 胶片颗粒效果
        /// </summary>
        FilmGrain,

        /// <summary>
        /// 暖色调
        /// </summary>
        WarmTone,

        /// <summary>
        /// 波普艺术风格
        /// </summary>
        PopArt,

        /// <summary>
        /// 水墨画风格
        /// </summary>
        InkPainting,

        /// <summary>
        /// 3D效果
        /// </summary>
        //_3DEffect,

        /// <summary>
        /// 扭曲变形效果
        /// </summary>
        //Distortion,

        /// <summary>
        /// 雪景效果
        /// </summary>
        Snow,

        ///// <summary>
        ///// 水下效果
        ///// </summary>
        //Underwater,

        ///// <summary>
        ///// 宝丽来风格
        ///// </summary>
        //Polaroid,

        ///// <summary>
        ///// 胶片条效果
        ///// </summary>
        //FilmStrip,

        ///// <summary>
        ///// 运动模糊
        ///// </summary>
        //MotionBlur,

        ///// <summary>
        ///// 油画效果
        ///// </summary>
        //OilPainting,

        /// <summary>
        /// 复古褐色调
        /// </summary>
        Sepia,

        /// <summary>
        /// 深褐色调
        /// </summary>
        SepiaTone,

        /// <summary>
        /// 降噪处理
        /// </summary>
        Denoise,

        /// <summary>
        /// 老照片效果
        /// </summary>
        OldPhoto,

        /// <summary>
        /// 赛博朋克风格
        /// </summary>
        Cyberpunk
    }
}
