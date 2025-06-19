using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WinFormsApp.CandyTool
{
    public class FileSorter
    {
        // 文件信息数据结构
        private class FileInfo
        {
            public string FullPath { get; set; }
            public string FileName { get; set; }

            public FileInfo(string fullPath, string fileName)
            {
                FullPath = fullPath;
                FileName = fileName;
            }
        }

        // 文件夹节点数据结构
        private class FolderNode
        {
            public string Name { get; set; }
            public string FullPath { get; set; }
            public List<FolderNode> Children { get; set; } = new List<FolderNode>();

            public FolderNode(string name, string fullPath)
            {
                Name = name;
                FullPath = fullPath;
            }
        }

        #region 按JSON配置分类方法
        /// <summary>
        /// 根据JSON配置文件对文件进行分类（递归创建多级文件夹，匹配所有层级文件夹名称）
        /// </summary>
        public static void SortFiles(string sourcePath, string configPath, string targetRootPath)
        {
            try
            {
                ValidatePaths(sourcePath, configPath);

                // 读取并存储所有源文件信息
                var sourceFiles = ReadAllSourceFiles(sourcePath);
                LogInfo($"找到 {sourceFiles.Count} 个源文件");

                // 解析JSON配置并构建文件夹树
                var rootNode = BuildFolderTree(configPath, targetRootPath);

                // 递归创建所有文件夹
                CreateFolderTree(rootNode);

                // 获取所有文件夹节点（用于匹配）
                var allFolders = GetAllFolderNodes(rootNode);

                // 处理每个文件
                ProcessFiles(sourceFiles, allFolders);

                LogInfo($"分类完成！共处理 {sourceFiles.Count} 个文件");
            }
            catch (Exception ex)
            {
                LogError("SortFiles", ex.Message);
            }
        }

        // 读取所有源文件并存储信息
        private static List<FileInfo> ReadAllSourceFiles(string sourcePath)
        {
            var files = new List<FileInfo>();

            try
            {
                foreach (var file in Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories))
                {
                    files.Add(new FileInfo(
                        fullPath: file,
                        fileName: Path.GetFileName(file)
                    ));
                }
            }
            catch (Exception ex)
            {
                LogError("ReadAllSourceFiles", $"读取源文件时出错: {ex.Message}");
            }

            return files;
        }

        // 构建文件夹树
        private static FolderNode BuildFolderTree(string configPath, string rootPath)
        {
            try
            {
                var config = JsonSerializer.Deserialize<Dictionary<string, object>>(File.ReadAllText(configPath));
                var rootNode = new FolderNode("root", rootPath);

                BuildFolderTreeRecursive(rootNode, config);

                return rootNode;
            }
            catch (Exception ex)
            {
                LogError("BuildFolderTree", $"解析配置文件时出错: {ex.Message}");
                return new FolderNode("root", rootPath);
            }
        }

        // 递归构建文件夹树
        // 递归构建文件夹树
        private static void BuildFolderTreeRecursive(FolderNode parentNode, Dictionary<string, object> config)
        {
            foreach (var item in config)
            {
                string folderName = item.Key;
                string folderPath = Path.Combine(parentNode.FullPath, folderName);

                var childNode = new FolderNode(folderName, folderPath);
                parentNode.Children.Add(childNode);

                // 修正类型转换逻辑
                if (item.Value is JsonElement jsonElement &&
                    jsonElement.ValueKind == JsonValueKind.Object)
                {
                    var subfolderConfig = jsonElement.Deserialize<Dictionary<string, object>>();
                    if (subfolderConfig != null)
                    {
                        BuildFolderTreeRecursive(childNode, subfolderConfig);
                    }
                }
                // 处理可能的其他类型（如数组等，根据实际需求调整）
                else if (item.Value is Dictionary<string, object> subfolderConfig)
                {
                    BuildFolderTreeRecursive(childNode, subfolderConfig);
                }
            }
        }

        // 递归创建文件夹树
        private static void CreateFolderTree(FolderNode node)
        {
            try
            {
                if (!Directory.Exists(node.FullPath))
                {
                    Directory.CreateDirectory(node.FullPath);
                    LogInfo($"创建文件夹: {GetRelativePath(node.FullPath)}");
                }

                foreach (var child in node.Children)
                {
                    CreateFolderTree(child);
                }
            }
            catch (Exception ex)
            {
                LogError("CreateFolderTree", $"创建文件夹 '{node.FullPath}' 时出错: {ex.Message}");
            }
        }

        // 获取所有文件夹节点
        private static List<FolderNode> GetAllFolderNodes(FolderNode rootNode)
        {
            var allNodes = new List<FolderNode>();
            var queue = new Queue<FolderNode>();
            queue.Enqueue(rootNode);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                // 跳过根节点
                if (current.Name != "root")
                {
                    allNodes.Add(current);
                }

                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return allNodes;
        }

        // 处理文件并复制到匹配的文件夹
        private static void ProcessFiles(List<FileInfo> sourceFiles, List<FolderNode> allFolders)
        {
            LogInfo("开始处理文件...");

            int totalFiles = sourceFiles.Count;
            int processedFiles = 0;
            int copiedFiles = 0;

            foreach (var file in sourceFiles)
            {
                processedFiles++;
                bool wasCopied = false;

                // 查找匹配的文件夹
                foreach (var folder in allFolders)
                {
                    if (file.FileName.Contains(folder.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        string targetPath = Path.Combine(folder.FullPath, file.FileName);

                        try
                        {
                            // 复制文件（不覆盖已存在文件）
                            if (!File.Exists(targetPath))
                            {
                                File.Copy(file.FullPath, targetPath);
                                copiedFiles++;
                                LogInfo($"[{processedFiles}/{totalFiles}] 复制: {file.FileName} -> {GetRelativePath(folder.FullPath)}");
                            }
                            else
                            {
                                LogInfo($"[{processedFiles}/{totalFiles}] 跳过: {file.FileName} (已存在)");
                            }

                            wasCopied = true;
                        }
                        catch (Exception ex)
                        {
                            LogError("ProcessFiles", $"复制文件 '{file.FileName}' 到 '{folder.FullPath}' 时出错: {ex.Message}");
                        }
                    }
                }

                if (!wasCopied)
                {
                    LogInfo($"[{processedFiles}/{totalFiles}] 未匹配: {file.FileName}");
                }
            }

            LogInfo($"处理完成: {processedFiles} 个文件，成功复制 {copiedFiles} 次");
        }

        // 获取相对于根目录的路径
        private static string GetRelativePath(string fullPath)
        {
            // 简化实现，实际项目中可能需要更健壮的方法
            return fullPath.Substring(fullPath.LastIndexOf(Path.DirectorySeparatorChar) + 1);
        }
        #endregion



        #region 辅助方法
        // 创建目录（如果不存在）
        private static void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        // 生成唯一文件名
        private static string GetUniqueFileName(string directory, string originalFileName)
        {
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(originalFileName);
            string extension = Path.GetExtension(originalFileName);
            int counter = 1;

            string newFileName;
            do
            {
                newFileName = $"{fileNameWithoutExt} ({counter}){extension}";
                counter++;
            }
            while (File.Exists(Path.Combine(directory, newFileName)));

            return newFileName;
        }

        // 验证路径
        private static void ValidatePaths(string sourcePath, string configPath)
        {
            if (!Directory.Exists(sourcePath))
            {
                throw new DirectoryNotFoundException($"源文件夹 '{sourcePath}' 不存在。");
            }

            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException($"配置文件 '{configPath}' 不存在。");
            }
        }
        #endregion

        #region 日志方法
        // 记录错误信息
        private static void LogError(string methodName, string message)
        {
            Debug.WriteLine($"[错误][{methodName}] {message}");
        }

        // 记录普通信息
        private static void LogInfo(string message)
        {
            Debug.WriteLine($"[信息] {message}");
        }
        #endregion

        #region 按文件类型分类方法
        /// <summary>
        /// 按文件类型对指定目录及其子目录进行分类
        /// </summary>
        /// <param name="rootDirectory">根目录路径</param>
        /// <param name="hierarchical">是否分级存储：true表示在每个目录下按类型创建子文件夹；false表示只在根目录按类型创建文件夹</param>
        public static void SortByFileType(string rootDirectory, bool hierarchical = true)
        {
            if (!Directory.Exists(rootDirectory))
            {
                LogError("SortByFileType", $"目录 '{rootDirectory}' 不存在。");
                return;
            }

            try
            {
                LogInfo($"开始按文件类型分类: {rootDirectory} (分级存储: {hierarchical})");

                if (hierarchical)
                {
                    // 分级存储：在每个目录下创建类型文件夹
                    var directories = new List<string> { rootDirectory };
                    directories.AddRange(Directory.GetDirectories(rootDirectory, "*", SearchOption.AllDirectories));

                    foreach (var directory in directories)
                    {
                        SortDirectoryByFileType(directory);
                    }
                }
                else
                {
                    // 非分级存储：只在根目录创建类型文件夹
                    var allFiles = Directory.GetFiles(rootDirectory, "*", SearchOption.AllDirectories);

                    // 按文件类型分组
                    var fileGroups = allFiles.GroupBy(f => Path.GetExtension(f).TrimStart('.').ToLowerInvariant());

                    foreach (var group in fileGroups)
                    {
                        string fileType = group.Key;
                        if (string.IsNullOrEmpty(fileType))
                            fileType = "无扩展名";

                        string targetDirectory = Path.Combine(rootDirectory, fileType);
                        CreateDirectoryIfNotExists(targetDirectory);

                        foreach (string filePath in group)
                        {
                            string fileName = Path.GetFileName(filePath);
                            string targetPath = Path.Combine(targetDirectory, fileName);

                            try
                            {
                                // 如果目标文件已存在，生成唯一文件名
                                if (File.Exists(targetPath))
                                {
                                    targetPath = Path.Combine(targetDirectory, GetUniqueFileName(targetDirectory, fileName));
                                }

                                File.Copy(filePath, targetPath);
                                LogInfo($"  复制: {fileName} -> {fileType}/");
                            }
                            catch (Exception ex)
                            {
                                LogError("SortByFileType", $"复制文件 '{fileName}' 时出错: {ex.Message}");
                            }
                        }
                    }
                }

                LogInfo("按文件类型分类完成！");
            }
            catch (Exception ex)
            {
                LogError("SortByFileType", ex.Message);
            }
        }

        // 处理单个目录的文件类型分类
        private static void SortDirectoryByFileType(string directoryPath)
        {
            try
            {
                var files = Directory.GetFiles(directoryPath);

                if (files.Length == 0)
                    return;

                LogInfo($"处理目录: {directoryPath} ({files.Length} 个文件)");

                var fileGroups = files.GroupBy(f => Path.GetExtension(f).TrimStart('.').ToLowerInvariant());

                foreach (var group in fileGroups)
                {
                    string fileType = group.Key;
                    if (string.IsNullOrEmpty(fileType))
                        fileType = "无扩展名";

                    string targetDirectory = Path.Combine(directoryPath, fileType);
                    CreateDirectoryIfNotExists(targetDirectory);

                    foreach (string filePath in group)
                    {
                        string fileName = Path.GetFileName(filePath);
                        string targetPath = Path.Combine(targetDirectory, fileName);

                        if (File.Exists(targetPath))
                        {
                            targetPath = Path.Combine(targetDirectory, GetUniqueFileName(targetDirectory, fileName));
                        }

                        File.Move(filePath, targetPath);
                        LogInfo($"  移动: {fileName} -> {fileType}/");
                    }
                }
            }
            catch (Exception ex)
            {
                LogError("SortDirectoryByFileType", $"处理目录 '{directoryPath}' 时出错: {ex.Message}");
            }
        }
        #endregion


        /// <summary>
        /// 将指定文件夹的内容导出为JSON格式
        /// </summary>
        /// <param name="sourcePath">源文件夹路径</param>
        /// <param name="outputPath">输出JSON文件的目录</param>
        /// <returns>导出的JSON文件路径</returns>
        public static string ExportToJson(string sourcePath, string outputPath = null)
        {
            if (!Directory.Exists(sourcePath))
            {
                LogError("ExportToJson", $"源文件夹 '{sourcePath}' 不存在。");
                return null;
            }

            try
            {
                // 确定输出路径
                if (string.IsNullOrEmpty(outputPath))
                {
                    outputPath = Path.GetDirectoryName(sourcePath);
                }
                else if (!Directory.Exists(outputPath))
                {
                    Directory.CreateDirectory(outputPath);
                }

                // 生成JSON文件名（格式：文件夹名_年月日时分秒.json）
                string folderName = Path.GetFileName(sourcePath);
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                string jsonFileName = $"{folderName}_{timestamp}.json";
                string jsonFilePath = Path.Combine(outputPath, jsonFileName);

                // 构建文件夹内容结构
                var directoryContent = BuildDirectoryContent(sourcePath);

                // 序列化为JSON并保存
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonContent = JsonSerializer.Serialize(directoryContent, options);
                File.WriteAllText(jsonFilePath, jsonContent);

                LogInfo($"成功导出文件夹内容到: {jsonFilePath}");
                return jsonFilePath;
            }
            catch (Exception ex)
            {
                LogError("ExportToJson", $"导出过程中出错: {ex.Message}");
                return null;
            }
        }

        // 构建目录内容结构
        private static Dictionary<string, object> BuildDirectoryContent(string directoryPath)
        {
            var content = new Dictionary<string, object>
    {
        { "name", Path.GetFileName(directoryPath) },
        { "path", directoryPath },
        { "type", "directory" },
        { "files", new List<Dictionary<string, string>>() },
        { "directories", new List<Dictionary<string, object>>() }
    };

            try
            {
                // 添加文件
                var files = new List<Dictionary<string, string>>();
                foreach (var file in Directory.GetFiles(directoryPath))
                {
                    // 使用完全限定名引用 System.IO.FileInfo
                    long fileSize = new System.IO.FileInfo(file).Length;

                    files.Add(new Dictionary<string, string>
            {
                { "name", Path.GetFileName(file) },
                { "extension", Path.GetExtension(file).TrimStart('.') },
                { "size", fileSize.ToString() },
                { "lastModified", File.GetLastWriteTime(file).ToString("yyyy-MM-dd HH:mm:ss") }
            });
                }
                content["files"] = files;

                // 递归添加子目录
                var directories = new List<Dictionary<string, object>>();
                foreach (var subDirectory in Directory.GetDirectories(directoryPath))
                {
                    directories.Add(BuildDirectoryContent(subDirectory));
                }
                content["directories"] = directories;
            }
            catch (Exception ex)
            {
                LogError("BuildDirectoryContent", $"处理目录 '{directoryPath}' 时出错: {ex.Message}");
                // 继续处理其他内容
            }

            return content;
        }
    }
}
