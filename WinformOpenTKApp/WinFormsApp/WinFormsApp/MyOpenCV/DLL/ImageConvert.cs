using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace WinFormsApp.MyOpenCV.DLL
{
    public class ImageConvert
    {
        public static void ConvertImage(string inputImagePath, string outputImagePath, string modelPath)
        {
            try
            {
                // 检查输入图像文件是否存在
                if (!File.Exists(inputImagePath))
                {
                    Console.WriteLine($"输入图像文件 {inputImagePath} 不存在。");
                    return;
                }

                // 检查模型文件是否存在
                if (!File.Exists(modelPath))
                {
                    Console.WriteLine($"模型文件 {modelPath} 不存在。");
                    return;
                }

                // 读取输入图像
                using (Mat inputImage = Cv2.ImRead(inputImagePath, ImreadModes.Color))
                {
                    if (inputImage.Empty())
                    {
                        Console.WriteLine($"无法读取输入图像 {inputImagePath}。可能不是有效的图像文件。");
                        return;
                    }

                    // 图像预处理
                    using (Mat preprocessedImage = PreprocessImage(inputImage))
                    {
                        if (preprocessedImage == null)
                        {
                            return;
                        }

                        // 模型推理
                        using (Mat outputImage = RunInference(preprocessedImage, modelPath))
                        {
                            if (outputImage == null)
                            {
                                return;
                            }

                            // 保存结果
                            if (!SaveImage(outputImage, outputImagePath))
                            {
                                Console.WriteLine($"无法保存输出图像到 {outputImagePath}。");
                            }
                            else
                            {
                                Console.WriteLine("照片已成功转换为动漫风格并保存。");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发生未知错误: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }

        public static Mat PreprocessImage(Mat inputImage)
        {
            try
            {
                // 调整图像尺寸
                using (Mat resizedImage = new Mat())
                {
                    Cv2.Resize(inputImage, resizedImage, new OpenCvSharp.Size(256, 256));

                    // 归一化
                    Mat normalizedImage = new Mat();
                    resizedImage.ConvertTo(normalizedImage, MatType.CV_32F, 1.0 / 255.0);

                    return normalizedImage;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"图像预处理过程中发生错误: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public static Mat RunInference(Mat preprocessedImage, string modelPath)
        {
            try
            {
                // 创建 ONNX Runtime 会话
                var sessionOptions = new SessionOptions();
                using (var session = new InferenceSession(modelPath, sessionOptions))
                {
                    // 准备输入张量
                    var inputTensor = new DenseTensor<float>(new[] { 1, 256, 256, 3 });
                    for (int y = 0; y < 256; y++)
                    {
                        for (int x = 0; x < 256; x++)
                        {
                            Vec3f pixel = preprocessedImage.Get<Vec3f>(y, x);
                            inputTensor[0, y, x, 0] = pixel.Item2; // B
                            inputTensor[0, y, x, 1] = pixel.Item1; // G
                            inputTensor[0, y, x, 2] = pixel.Item0; // R
                        }
                    }

                    // 创建输入字典
                    var inputs = new List<NamedOnnxValue>
                    {
                        NamedOnnxValue.CreateFromTensor("generator_input:0", inputTensor)
                    };

                    // 进行推理
                    using (var outputs = session.Run(inputs))
                    {
                        // 获取输出张量，需要根据实际输出名称调整
                        var outputName = session.OutputMetadata.Keys.First();
                        var outputTensor = outputs.First(o => o.Name == outputName).AsTensor<float>();

                        // 后处理
                        Mat outputImage = new Mat(256, 256, MatType.CV_32FC3);
                        for (int y = 0; y < 256; y++)
                        {
                            for (int x = 0; x < 256; x++)
                            {
                                float b = outputTensor[0, 0, y, x];
                                float g = outputTensor[0, 1, y, x];
                                float r = outputTensor[0, 2, y, x];
                                outputImage.Set(y, x, new Vec3f(b, g, r));
                            }
                        }

                        // 反归一化
                        outputImage.ConvertTo(outputImage, MatType.CV_8UC3, 255.0);

                        return outputImage;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"模型推理过程中发生错误: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        private static bool SaveImage(Mat image, string outputPath)
        {
            try
            {
                // 检查输出目录是否存在，若不存在则创建
                string outputDirectory = Path.GetDirectoryName(outputPath);
                if (!Directory.Exists(outputDirectory))
                {
                    Directory.CreateDirectory(outputDirectory);
                }

                Cv2.ImWrite(outputPath, image);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"保存图像时发生错误: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        public static void PrintModelInputOutputNames(string modelPath)
        {
            try
            {
                var sessionOptions = new SessionOptions();
                using (var session = new InferenceSession(modelPath, sessionOptions))
                {
                    // 打印输入名称
                    Console.WriteLine("输入名称:");
                    foreach (var input in session.InputMetadata)
                    {
                        Console.WriteLine(input.Key);
                    }

                    // 打印输出名称
                    Console.WriteLine("输出名称:");
                    foreach (var output in session.OutputMetadata)
                    {
                        Console.WriteLine(output.Key);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"在获取模型输入输出名称时发生错误: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}