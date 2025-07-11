using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WinFormsApp.CandyTool
{
    public class CandyJson
    {

        // 表示文件或文件夹的类
        public class FileSystemItem
        {
            public string Name { get; set; }
            public string Path { get; set; }
            public string Type { get; set; } // "File" 或 "Directory"
            public List<FileSystemItem> Children { get; set; }
        }

        // 扫描指定目录并保存为JSON
        public static void ScanDirectoryAndSaveAsJson(string directoryPath, string outputFilePath)
        {
            // 验证输入目录是否存在
            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException($"指定的目录不存在: {directoryPath}");
            }

            try
            {
                // 扫描目录
                var rootItem = ScanDirectory(directoryPath);

                // 序列化为JSON
                var jsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true, // 格式化JSON，使其更易读
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping // 允许中文等特殊字符
                };

                string json = JsonSerializer.Serialize(rootItem, jsonOptions);

                // 保存到文件
                File.WriteAllText(outputFilePath, json);

                Console.WriteLine($"目录结构已成功保存到: {outputFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"处理过程中发生错误: {ex.Message}");
                throw;
            }
        }

        // 递归扫描目录
        private static FileSystemItem ScanDirectory(string directoryPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(directoryPath);

            var item = new FileSystemItem
            {
                Name = dirInfo.Name,
                Path = dirInfo.FullName,
                Type = "Directory",
                Children = new List<FileSystemItem>()
            };

            // 扫描子目录
            try
            {
                foreach (var subDir in dirInfo.GetDirectories())
                {
                    item.Children.Add(ScanDirectory(subDir.FullName));
                }
            }
            catch (UnauthorizedAccessException)
            {
                // 处理权限不足的情况
                Console.WriteLine($"警告: 无法访问目录 {dirInfo.FullName}，权限不足");
            }

            // 扫描文件
            try
            {
                foreach (var file in dirInfo.GetFiles())
                {
                    item.Children.Add(new FileSystemItem
                    {
                        Name = file.Name,
                        Path = file.FullName,
                        Type = "File",
                        Children = null // 文件没有子项
                    });
                }
            }
            catch (UnauthorizedAccessException)
            {
                // 处理权限不足的情况
                Console.WriteLine($"警告: 无法访问目录 {dirInfo.FullName} 中的文件，权限不足");
            }

            return item;
        }
    }


    
}
