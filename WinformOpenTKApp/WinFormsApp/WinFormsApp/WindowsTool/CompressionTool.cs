using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp.CandyTool;

namespace WinFormsApp.WindowsTool
{
    public partial class CompressionTool : Form
    {
        private string _sourcePath = string.Empty;
        private string _destinationPath = string.Empty;
        private bool _isSourceFolder = false;

        // 算法与文件扩展名的映射
        private Dictionary<string, string> _algorithmExtensions = new Dictionary<string, string>
        {
            {"DEFLATE", "deflate"},
            {"LZMA", "lzma"},
            {"BZIP2", "bz2"},
            {"ZSTD", "zst"}
        };

        // 委托用于跨线程访问控件
        private delegate string GetControlValueDelegate();

        public CompressionTool()
        {
            InitializeComponent();
            InitializeCustomSettings();
        }

        private void InitializeCustomSettings()
        {
            // 设置默认编码
            cmbEncoding.SelectedItem = "UTF-8";

            // 初始化事件处理
            rbtnCompress.CheckedChanged += OperationTypeChanged;
            rbtnDecompress.CheckedChanged += OperationTypeChanged;
            cmbAlgorithm.SelectedIndexChanged += AlgorithmChanged;
            rbtnSourceFile.CheckedChanged += SourceTypeChanged;
            rbtnSourceFolder.CheckedChanged += SourceTypeChanged;

            // 默认选择压缩和DEFLATE算法
            rbtnCompress.Checked = true;
            rbtnSourceFile.Checked = true;
            cmbAlgorithm.SelectedItem = "DEFLATE";

            // 初始状态更新
            OperationTypeChanged(null, EventArgs.Empty);
            AlgorithmChanged(null, EventArgs.Empty);
            SourceTypeChanged(null, EventArgs.Empty);
        }

        private void SourceTypeChanged(object sender, EventArgs e)
        {
            _isSourceFolder = rbtnSourceFolder.Checked;

            // 只有压缩时才允许选择文件夹
            rbtnSourceFolder.Enabled = rbtnCompress.Checked;

            if (!rbtnCompress.Checked)
            {
                rbtnSourceFile.Checked = true;
                _isSourceFolder = false;
            }

            // 重置源路径
            txtSourcePath.Text = string.Empty;
            _sourcePath = string.Empty;
            _destinationPath = string.Empty;
            txtDestinationPath.Text = string.Empty;
        }

        private void OperationTypeChanged(object sender, EventArgs e)
        {
            bool isCompress = rbtnCompress.Checked;

            // 更新按钮文本
            btnExecute.Text = isCompress ? "压缩" : "解压";

            // 只有压缩时才显示源类型选择
            grpSourceType.Visible = isCompress;

            // 更新参数面板可见性
            UpdateParameterVisibility();

            // 重置路径
            SourceTypeChanged(null, EventArgs.Empty);
        }

        private void AlgorithmChanged(object sender, EventArgs e)
        {
            UpdateParameterVisibility();

            // 当算法改变时，更新目标文件扩展名
            if (!string.IsNullOrEmpty(_sourcePath) && !string.IsNullOrEmpty(cmbAlgorithm.Text))
            {
                GenerateDestinationPath();
            }
        }

        private void UpdateParameterVisibility()
        {
            bool isCompress = rbtnCompress.Checked;
            string algorithm = cmbAlgorithm.Text;

            // 重置所有参数面板不可见
            pnlDeflateParams.Visible = false;
            pnlLzmaParams.Visible = false;
            pnlBzip2Params.Visible = false;
            pnlZstdParams.Visible = false;
            pnlDeflateDecompressParams.Visible = false;
            pnlBzip2DecompressParams.Visible = false;
            txtEstimatedSize.Visible = false;
            lblEstimatedSize.Visible = false;

            // 根据算法显示对应的参数面板
            if (isCompress)
            {
                switch (algorithm)
                {
                    case "DEFLATE":
                        pnlDeflateParams.Visible = true;
                        break;
                    case "LZMA":
                        pnlLzmaParams.Visible = true;
                        break;
                    case "BZIP2":
                        pnlBzip2Params.Visible = true;
                        break;
                    case "ZSTD":
                        pnlZstdParams.Visible = true;
                        break;
                }
            }
            else
            {
                // 解压时只需要显示部分参数
                switch (algorithm)
                {
                    case "DEFLATE":
                        pnlDeflateDecompressParams.Visible = true;
                        break;
                    case "BZIP2":
                        pnlBzip2DecompressParams.Visible = true;
                        break;
                    case "LZMA":
                        txtEstimatedSize.Visible = true;
                        lblEstimatedSize.Visible = true;
                        break;
                }
            }
        }

        private void btnBrowseSource_Click(object sender, EventArgs e)
        {
            if (rbtnCompress.Checked && _isSourceFolder)
            {
                using (var dialog = new FolderBrowserDialog())
                {
                    dialog.Description = "选择要压缩的文件夹";
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        _sourcePath = dialog.SelectedPath;
                        txtSourcePath.Text = _sourcePath;

                        // 自动生成目标路径
                        GenerateDestinationPath();
                    }
                }
            }
            else
            {
                using (var dialog = new OpenFileDialog())
                {
                    dialog.Title = rbtnCompress.Checked ? "选择要压缩的文件" : "选择要解压的文件";

                    // 解压时过滤文件类型
                    if (!rbtnCompress.Checked && !string.IsNullOrEmpty(cmbAlgorithm.Text))
                    {
                        string ext = GetAlgorithmExtension(cmbAlgorithm.Text);
                        dialog.Filter = $"{cmbAlgorithm.Text}文件|*.{ext}|所有文件|*.*";
                    }

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        _sourcePath = dialog.FileName;
                        txtSourcePath.Text = _sourcePath;

                        // 自动生成目标路径
                        GenerateDestinationPath();
                    }
                }
            }
        }

        private void btnBrowseDestination_Click(object sender, EventArgs e)
        {
            if (rbtnCompress.Checked && _isSourceFolder)
            {
                using (var dialog = new SaveFileDialog())
                {
                    string algorithm = cmbAlgorithm.Text;
                    string ext = GetAlgorithmExtension(algorithm);

                    dialog.Title = "保存压缩文件";
                    dialog.DefaultExt = ext;
                    dialog.Filter = $"{algorithm}压缩文件|*.{ext}|所有文件|*.*";

                    // 建议文件名
                    if (!string.IsNullOrEmpty(_sourcePath))
                    {
                        string folderName = Path.GetFileName(_sourcePath);
                        dialog.FileName = $"{folderName}.{ext}";
                    }

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        _destinationPath = dialog.FileName;
                        txtDestinationPath.Text = _destinationPath;
                    }
                }
            }
            else
            {
                using (var dialog = new SaveFileDialog())
                {
                    dialog.Title = rbtnCompress.Checked ? "保存压缩文件" : "保存解压文件";

                    // 设置默认扩展名
                    if (rbtnCompress.Checked)
                    {
                        string algorithm = cmbAlgorithm.Text;
                        string ext = GetAlgorithmExtension(algorithm);
                        dialog.DefaultExt = ext;
                        dialog.Filter = $"{algorithm}压缩文件|*.{ext}|所有文件|*.*";
                    }
                    else
                    {
                        // 解压时尝试根据源文件推断扩展名
                        string suggestedExt = "dat";
                        if (!string.IsNullOrEmpty(_sourcePath))
                        {
                            string sourceExt = Path.GetExtension(_sourcePath).TrimStart('.').ToLower();
                            // 移除已知的压缩扩展名
                            foreach (var ext in _algorithmExtensions.Values)
                            {
                                if (sourceExt.EndsWith(ext))
                                {
                                    suggestedExt = sourceExt.Substring(0, sourceExt.Length - ext.Length);
                                    if (string.IsNullOrEmpty(suggestedExt))
                                        suggestedExt = "dat";
                                    break;
                                }
                            }
                        }

                        dialog.DefaultExt = suggestedExt;
                        dialog.Filter = $"{suggestedExt.ToUpper()}文件|*.{suggestedExt}|所有文件|*.*";
                    }

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        _destinationPath = dialog.FileName;
                        txtDestinationPath.Text = _destinationPath;
                    }
                }
            }
        }

        private string GetAlgorithmExtension(string algorithm)
        {
            if (_algorithmExtensions.TryGetValue(algorithm, out string ext))
                return ext;
            return "cmp";
        }

        private void GenerateDestinationPath()
        {
            if (string.IsNullOrEmpty(_sourcePath) || string.IsNullOrEmpty(cmbAlgorithm.Text))
                return;

            string algorithm = cmbAlgorithm.Text;
            string ext = GetAlgorithmExtension(algorithm);
            string directory = Path.GetDirectoryName(_sourcePath);

            if (rbtnCompress.Checked)
            {
                string baseName = _isSourceFolder
                    ? Path.GetFileName(_sourcePath)
                    : Path.GetFileNameWithoutExtension(_sourcePath);

                _destinationPath = Path.Combine(directory, $"{baseName}.{ext}");
            }
            else
            {
                string sourceFileName = Path.GetFileNameWithoutExtension(_sourcePath);
                // 移除可能存在的压缩扩展名
                foreach (var algoExt in _algorithmExtensions.Values)
                {
                    if (sourceFileName.EndsWith($".{algoExt}", StringComparison.OrdinalIgnoreCase))
                    {
                        sourceFileName = sourceFileName.Substring(0, sourceFileName.Length - $".{algoExt}".Length);
                        break;
                    }
                }

                _destinationPath = Path.Combine(directory, $"{sourceFileName}_decompressed");

                // 如果源是文件且有扩展名，保留原扩展名
                if (!_isSourceFolder && !string.IsNullOrEmpty(Path.GetExtension(_sourcePath)))
                {
                    _destinationPath += Path.GetExtension(_sourcePath);
                }
            }

            txtDestinationPath.Text = _destinationPath;
        }

        private async void btnExecute_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_sourcePath) ||
                (!_isSourceFolder && !File.Exists(_sourcePath)) ||
                (_isSourceFolder && !Directory.Exists(_sourcePath)))
            {
                ShowMessage("请选择有效的源文件或文件夹", "错误", MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(_destinationPath))
            {
                ShowMessage("请选择目标文件路径", "错误", MessageBoxIcon.Error);
                return;
            }

            // 检查目标文件是否已存在
            if (File.Exists(_destinationPath))
            {
                var result = ShowConfirmMessage("目标文件已存在，是否覆盖？", "确认覆盖", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes)
                {
                    return;
                }
            }

            // 禁用按钮防止重复操作
            btnExecute.Enabled = false;
            progressBar.Visible = true;
            UpdateStatus("正在处理...");

            try
            {
                // 先在UI线程获取所有需要的参数
                bool isCompress = rbtnCompress.Checked;
                string algorithm = cmbAlgorithm.Text;
                int compressionLevel = (int)nudCompressionLevel.Value;
                bool useZLibHeader = chkUseZLibHeader.Checked;
                int bufferSize = (int)nudBufferSize.Value;
                uint dictSize = (uint)(1 << (int)nudDictSize.Value);
                int blockSize = (int)nudBlockSize.Value;
                int bufferSizeBzip2 = (int)nudBufferSizeBzip2.Value;
                bool useZLibHeaderDecompress = chkUseZLibHeaderDecompress.Checked;
                int bufferSizeDecompress = (int)nudBufferSizeDecompress.Value;
                int bufferSizeBzip2Decompress = (int)nudBufferSizeBzip2Decompress.Value;
                long estimatedSize = 0;
                long.TryParse(txtEstimatedSize.Text, out estimatedSize);

                await Task.Run(() =>
                {
                    if (isCompress)
                    {
                        if (_isSourceFolder)
                        {
                            CompressFolder(algorithm, compressionLevel, useZLibHeader,
                                bufferSize, dictSize, blockSize, bufferSizeBzip2);
                        }
                        else
                        {
                            CompressFile(algorithm, compressionLevel, useZLibHeader,
                                bufferSize, dictSize, blockSize, bufferSizeBzip2);
                        }
                    }
                    else
                    {
                        DecompressFile(algorithm, useZLibHeaderDecompress,
                            bufferSizeDecompress, bufferSizeBzip2Decompress, estimatedSize);
                    }
                });

                UpdateStatus(isCompress ? "压缩完成" : "解压完成");
                ShowMessage($"{(isCompress ? "压缩" : "解压")}操作已成功完成！\n目标文件: {_destinationPath}",
                    "成功", MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                UpdateStatus("操作失败");
                ShowMessage($"操作失败: {ex.Message}", "错误", MessageBoxIcon.Error);
            }
            finally
            {
                // 在UI线程恢复控件状态
                this.Invoke((MethodInvoker)delegate
                {
                    btnExecute.Enabled = true;
                    progressBar.Visible = false;
                });
            }
        }

        // 压缩文件夹方法
        private void CompressFolder(string algorithm, int compressionLevel, bool useZLibHeader,
                                 int bufferSize, uint dictSize, int blockSize, int bufferSizeBzip2)
        {
            // 创建临时内存流存储文件夹内容
            using (var memoryStream = new MemoryStream())
            {
                // 写入文件夹信息头
                WriteFolderHeader(memoryStream, _sourcePath);

                // 写入文件夹中的所有文件
                WriteFolderFiles(memoryStream, _sourcePath);

                // 将内存流数据压缩并写入目标文件
                byte[] folderData = memoryStream.ToArray();
                byte[] compressedData;

                switch (algorithm)
                {
                    case "DEFLATE":
                        compressedData = CandyCompress.CompressByDEFLATE(folderData,
                            compressionLevel, false, bufferSize, useZLibHeader);
                        break;
                    case "LZMA":
                        if (!CandyCompress.Initialize())
                        {
                            throw new Exception("无法初始化7z.dll，请确保该文件存在于程序目录下");
                        }
                        compressedData = CandyCompress.CompressByLZMA(folderData, compressionLevel, dictSize);
                        break;
                    case "BZIP2":
                        compressedData = CandyCompress.CompressByBZIP2(folderData, blockSize, bufferSizeBzip2);
                        break;
                    case "ZSTD":
                        compressedData = CandyCompress.CompressByZSTD(folderData, compressionLevel);
                        break;
                    default:
                        throw new NotSupportedException($"不支持的压缩算法: {algorithm}");
                }

                File.WriteAllBytes(_destinationPath, compressedData);
            }
        }

        // 写入文件夹信息头
        private void WriteFolderHeader(MemoryStream stream, string folderPath)
        {
            string folderName = Path.GetFileName(folderPath) + "\\";
            byte[] nameBytes = Encoding.UTF8.GetBytes(folderName);

            // 写入头标识
            byte[] headerMarker = Encoding.UTF8.GetBytes("FOLDERARC");
            stream.Write(headerMarker, 0, headerMarker.Length);

            // 写入文件夹名长度和名称
            byte[] nameLength = BitConverter.GetBytes(nameBytes.Length);
            stream.Write(nameLength, 0, nameLength.Length);
            stream.Write(nameBytes, 0, nameBytes.Length);
        }

        // 写入文件夹中的所有文件
        private void WriteFolderFiles(MemoryStream stream, string folderPath)
        {
            string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);

            // 写入文件数量
            byte[] fileCount = BitConverter.GetBytes(files.Length);
            stream.Write(fileCount, 0, fileCount.Length);

            foreach (string file in files)
            {
                // 计算相对路径
                string relativePath = Path.GetRelativePath(folderPath, file);

                // 写入相对路径
                byte[] pathBytes = Encoding.UTF8.GetBytes(relativePath);
                byte[] pathLength = BitConverter.GetBytes(pathBytes.Length);
                stream.Write(pathLength, 0, pathLength.Length);
                stream.Write(pathBytes, 0, pathBytes.Length);

                // 写入文件内容
                byte[] fileContent = File.ReadAllBytes(file);
                byte[] contentLength = BitConverter.GetBytes(fileContent.Length);
                stream.Write(contentLength, 0, contentLength.Length);
                stream.Write(fileContent, 0, fileContent.Length);
            }
        }

        // 压缩单个文件方法
        private void CompressFile(string algorithm, int compressionLevel, bool useZLibHeader,
                                 int bufferSize, uint dictSize, int blockSize, int bufferSizeBzip2)
        {
            switch (algorithm)
            {
                case "DEFLATE":
                    CandyCompress.CompressFileByDEFLATE(_sourcePath, _destinationPath,
                        compressionLevel, bufferSize, useZLibHeader);
                    break;
                case "LZMA":
                    if (!CandyCompress.Initialize())
                    {
                        throw new Exception("无法初始化7z.dll，请确保该文件存在于程序目录下");
                    }
                    CandyCompress.CompressFileByLZMA(_sourcePath, _destinationPath, compressionLevel);
                    break;
                case "BZIP2":
                    CandyCompress.CompressFileByBZIP2(_sourcePath, _destinationPath,
                        blockSize, bufferSizeBzip2);
                    break;
                case "ZSTD":
                    CandyCompress.CompressFileByZSTD(_sourcePath, _destinationPath, compressionLevel);
                    break;
                default:
                    throw new NotSupportedException($"不支持的压缩算法: {algorithm}");
            }
        }

        // 解压文件方法
        private void DecompressFile(string algorithm, bool useZLibHeaderDecompress,
                                   int bufferSizeDecompress, int bufferSizeBzip2Decompress, long estimatedSize)
        {
            byte[] compressedData = File.ReadAllBytes(_sourcePath);
            byte[] decompressedData;

            switch (algorithm)
            {
                case "DEFLATE":
                    decompressedData = CandyCompress.DecompressByDEFLATE(
                        compressedData, false, bufferSizeDecompress, useZLibHeaderDecompress);
                    break;
                case "LZMA":
                    if (!CandyCompress.Initialize())
                    {
                        throw new Exception("无法初始化7z.dll，请确保该文件存在于程序目录下");
                    }
                    decompressedData = CandyCompress.DecompressByLZMA(compressedData, estimatedSize);
                    break;
                case "BZIP2":
                    decompressedData = CandyCompress.DecompressByBZIP2(compressedData, bufferSizeBzip2Decompress);
                    break;
                case "ZSTD":
                    decompressedData = CandyCompress.DecompressByZSTD(compressedData);
                    break;
                default:
                    throw new NotSupportedException($"不支持的解压算法: {algorithm}");
            }

            // 检查是否是文件夹压缩包
            if (IsFolderArchive(decompressedData))
            {
                // 创建目标文件夹
                string targetFolder = _destinationPath;
                if (File.Exists(targetFolder))
                {
                    File.Delete(targetFolder);
                }
                Directory.CreateDirectory(targetFolder);

                // 解析并提取文件夹内容
                ExtractFolderArchive(decompressedData, targetFolder);
            }
            else
            {
                // 普通文件解压
                File.WriteAllBytes(_destinationPath, decompressedData);
            }
        }

        // 检查是否是文件夹压缩包
        private bool IsFolderArchive(byte[] data)
        {
            if (data.Length < 8)
                return false;

            byte[] headerMarker = Encoding.UTF8.GetBytes("FOLDERARC");
            for (int i = 0; i < headerMarker.Length; i++)
            {
                if (data[i] != headerMarker[i])
                    return false;
            }
            return true;
        }

        // 提取文件夹压缩包内容
        private void ExtractFolderArchive(byte[] data, string targetFolder)
        {
            using (var memoryStream = new MemoryStream(data))
            {
                // 跳过头标识
                byte[] headerMarker = new byte[8];
                memoryStream.Read(headerMarker, 0, headerMarker.Length);

                // 读取文件夹名
                byte[] nameLengthBytes = new byte[4];
                memoryStream.Read(nameLengthBytes, 0, nameLengthBytes.Length);
                int nameLength = BitConverter.ToInt32(nameLengthBytes, 0);

                byte[] nameBytes = new byte[nameLength];
                memoryStream.Read(nameBytes, 0, nameBytes.Length);
                string folderName = Encoding.UTF8.GetString(nameBytes);

                // 读取文件数量
                byte[] fileCountBytes = new byte[4];
                memoryStream.Read(fileCountBytes, 0, fileCountBytes.Length);
                int fileCount = BitConverter.ToInt32(fileCountBytes, 0);

                // 提取每个文件
                for (int i = 0; i < fileCount; i++)
                {
                    // 读取路径长度和路径
                    byte[] pathLengthBytes = new byte[4];
                    memoryStream.Read(pathLengthBytes, 0, pathLengthBytes.Length);
                    int pathLength = BitConverter.ToInt32(pathLengthBytes, 0);

                    byte[] pathBytes = new byte[pathLength];
                    memoryStream.Read(pathBytes, 0, pathBytes.Length);
                    string relativePath = Encoding.UTF8.GetString(pathBytes);

                    // 读取内容长度和内容
                    byte[] contentLengthBytes = new byte[4];
                    memoryStream.Read(contentLengthBytes, 0, contentLengthBytes.Length);
                    int contentLength = BitConverter.ToInt32(contentLengthBytes, 0);

                    byte[] contentBytes = new byte[contentLength];
                    memoryStream.Read(contentBytes, 0, contentBytes.Length);

                    // 保存文件
                    string fullPath = Path.Combine(targetFolder, relativePath);
                    string directory = Path.GetDirectoryName(fullPath);

                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    File.WriteAllBytes(fullPath, contentBytes);
                }
            }
        }

        // 线程安全的状态更新方法
        private void UpdateStatus(string message)
        {
            if (lblStatus.InvokeRequired)
            {
                lblStatus.Invoke(new Action<string>(UpdateStatus), message);
            }
            else
            {
                lblStatus.Text = message;
            }
        }

        // 线程安全的消息框显示方法
        private void ShowMessage(string message, string caption, MessageBoxIcon icon)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string, string, MessageBoxIcon>(ShowMessage), message, caption, icon);
            }
            else
            {
                MessageBox.Show(message, caption, MessageBoxButtons.OK, icon);
            }
        }

        // 线程安全的确认消息框
        private DialogResult ShowConfirmMessage(string message, string caption, MessageBoxButtons buttons)
        {
            if (this.InvokeRequired)
            {
                return (DialogResult)this.Invoke(new Func<string, string, MessageBoxButtons, DialogResult>(ShowConfirmMessage),
                    message, caption, buttons);
            }
            else
            {
                return MessageBox.Show(message, caption, buttons, MessageBoxIcon.Question);
            }
        }

        private Encoding GetSelectedEncoding()
        {
            string encodingName = cmbEncoding.Text;
            switch (encodingName)
            {
                case "UTF-8":
                    return Encoding.UTF8;
                case "ASCII":
                    return Encoding.ASCII;
                case "Unicode":
                    return Encoding.Unicode;
                case "UTF-16":
                    return Encoding.Unicode;
                case "UTF-32":
                    return Encoding.UTF32;
                default:
                    return Encoding.UTF8;
            }
        }

        private void llblAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("压缩解压工具 v1.1\n支持算法: DEFLATE, LZMA, BZIP2, ZSTD\n支持文件和文件夹压缩",
                "关于", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
