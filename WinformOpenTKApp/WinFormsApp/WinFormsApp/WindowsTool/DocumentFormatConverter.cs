using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Presentation;
using Spire.Presentation.Drawing;

namespace WinFormsApp.WindowsTool
{
    public partial class DocumentFormatConverter : Form
    {
        // 后台转换组件
        private readonly BackgroundWorker _backgroundWorker;
        // 转换统计
        private int _totalFiles;
        private int _completedFiles;
        private int _failedFiles;
        // 取消标志
        private bool _isCanceling;

        // 支持的输入文件扩展名（Word和PowerPoint）
        private readonly string[] _supportedExtensions = { ".doc", ".docx", ".ppt", ".pptx" };

        public DocumentFormatConverter()
        {
            InitializeComponent();
            _backgroundWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _backgroundWorker.DoWork += BackgroundWorker_DoWork;
            _backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
            _backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }

        #region 界面事件处理
        private void btnSourceFolder_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog { Description = "选择包含文档的源文件夹" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtSourceFolder.Text = dialog.SelectedPath;
                    // 自动建议输出文件夹
                    if (string.IsNullOrWhiteSpace(txtOutputFolder.Text))
                    {
                        txtOutputFolder.Text = Path.Combine(dialog.SelectedPath, "转换结果");
                    }
                }
            }
        }

        private void btnOutputFolder_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog { Description = "选择转换后文件的保存文件夹" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtOutputFolder.Text = dialog.SelectedPath;
                }
            }
        }

        private void btnStartConversion_Click(object sender, EventArgs e)
        {
            // 验证输入
            if (!ValidateInput()) return;

            // 确认转换（大文件提示）
            if (_totalFiles > 10)
            {
                var result = MessageBox.Show(
                    $"共找到 {_totalFiles} 个文件，转换可能需要较长时间，是否继续？",
                    "转换提示",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);
                if (result == DialogResult.No) return;
            }

            // 准备转换
            PrepareConversion();

            // 开始后台转换
            _backgroundWorker.RunWorkerAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_backgroundWorker.IsBusy && !_isCanceling)
            {
                _isCanceling = true;
                _backgroundWorker.CancelAsync();
                btnCancel.Text = "取消中...";
                btnCancel.Enabled = false;
                LogMessage("正在取消转换，请稍候...");
            }
        }
        #endregion

        #region 验证和准备工作
        private bool ValidateInput()
        {
            // 验证源文件夹
            if (string.IsNullOrWhiteSpace(txtSourceFolder.Text) || !Directory.Exists(txtSourceFolder.Text))
            {
                MessageBox.Show("请选择有效的源文件夹", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // 验证输出文件夹（自动创建）
            if (string.IsNullOrWhiteSpace(txtOutputFolder.Text))
            {
                txtOutputFolder.Text = Path.Combine(txtSourceFolder.Text, "转换结果");
            }
            try
            {
                if (!Directory.Exists(txtOutputFolder.Text))
                {
                    Directory.CreateDirectory(txtOutputFolder.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"输出文件夹创建失败：{ex.Message}", "路径错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // 验证转换格式选择
            if (!chkConvertToPdf.Checked && !chkConvertToTxt.Checked)
            {
                MessageBox.Show("请至少选择一种输出格式（PDF或TXT）", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // 预检查文件数量
            var searchOption = chkIncludeSubfolders.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            var fileCount = 0;
            foreach (var ext in _supportedExtensions)
            {
                fileCount += Directory.GetFiles(txtSourceFolder.Text, $"*{ext}", searchOption).Length;
            }
            _totalFiles = fileCount;

            if (_totalFiles == 0)
            {
                MessageBox.Show("源文件夹中未找到支持的文档（.doc, .docx, .ppt, .pptx）", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private void PrepareConversion()
        {
            // 初始化状态
            txtLog.Clear();
            progressBar.Value = 0;
            _completedFiles = 0;
            _failedFiles = 0;
            _isCanceling = false;

            // 禁用控件
            btnStartConversion.Enabled = false;
            btnSourceFolder.Enabled = false;
            btnOutputFolder.Enabled = false;
            chkConvertToPdf.Enabled = false;
            chkConvertToTxt.Enabled = false;
            chkIncludeSubfolders.Enabled = false;
            btnCancel.Enabled = true;
            btnCancel.Text = "取消";

            LogMessage($"转换准备完成，共找到 {_totalFiles} 个文件");
            LogMessage("开始转换...");
        }
        #endregion

        #region 后台转换逻辑
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var searchOption = chkIncludeSubfolders.Checked ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                var files = new List<string>();

                // 收集所有支持的文件
                foreach (var ext in _supportedExtensions)
                {
                    files.AddRange(Directory.GetFiles(txtSourceFolder.Text, $"*{ext}", searchOption));
                }

                // 逐个处理文件
                foreach (var file in files)
                {
                    if (_isCanceling)
                    {
                        e.Cancel = true;
                        return;
                    }

                    try
                    {
                        ProcessFile(file);
                        _completedFiles++;
                    }
                    catch (Exception ex)
                    {
                        _failedFiles++;
                        LogMessage($"处理失败：{Path.GetFileName(file)} - {ex.Message}");
                    }

                    // 报告进度
                    var progress = (int)((double)_completedFiles / files.Count * 100);
                    _backgroundWorker.ReportProgress(progress, $"已处理: {_completedFiles}/{files.Count}（失败: {_failedFiles}）");
                }
            }
            catch (Exception ex)
            {
                LogMessage($"转换过程发生错误：{ex.Message}");
                if (ex.InnerException != null)
                {
                    LogMessage($"内部错误：{ex.InnerException.Message}");
                }
            }
        }

        private void ProcessFile(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            var fileExt = Path.GetExtension(filePath).ToLower();
            LogMessage($"开始处理：{fileName}");

            // 构建输出路径（保持目录结构）
            var relativePath = Path.GetRelativePath(txtSourceFolder.Text, filePath);
            var outputDir = Path.Combine(txtOutputFolder.Text, Path.GetDirectoryName(relativePath)!);
            Directory.CreateDirectory(outputDir); // 确保目录存在

            // 根据文件类型转换
            if (fileExt is ".doc" or ".docx")
            {
                if (chkConvertToPdf.Checked)
                {
                    var pdfPath = Path.Combine(outputDir, $"{Path.GetFileNameWithoutExtension(fileName)}.pdf");
                    ConvertWordToPdf(filePath, pdfPath);
                }

                if (chkConvertToTxt.Checked)
                {
                    var txtPath = Path.Combine(outputDir, $"{Path.GetFileNameWithoutExtension(fileName)}.txt");
                    ConvertWordToTxt(filePath, txtPath);
                }
            }
            else if (fileExt is ".ppt" or ".pptx")
            {
                if (chkConvertToPdf.Checked)
                {
                    var pdfPath = Path.Combine(outputDir, $"{Path.GetFileNameWithoutExtension(fileName)}.pdf");
                    ConvertPowerPointToPdf(filePath, pdfPath);
                }

                if (chkConvertToTxt.Checked)
                {
                    var txtPath = Path.Combine(outputDir, $"{Path.GetFileNameWithoutExtension(fileName)}.txt");
                    ConvertPowerPointToTxt(filePath, txtPath);
                }
            }

            LogMessage($"处理完成：{fileName}");
        }

        // Word转PDF
        private void ConvertWordToPdf(string inputPath, string outputPath)
        {
            using (var document = new Document())
            {
                document.LoadFromFile(inputPath);
                document.SaveToFile(outputPath, Spire.Doc.FileFormat.PDF);
            }
            LogMessage($"→ 已转换为PDF：{Path.GetFileName(outputPath)}");
        }

        // Word转TXT
        private void ConvertWordToTxt(string inputPath, string outputPath)
        {
            using (var document = new Document())
            {
                document.LoadFromFile(inputPath);
                document.SaveToFile(outputPath, Spire.Doc.FileFormat.Txt);
            }
            LogMessage($"→ 已转换为TXT：{Path.GetFileName(outputPath)}");
        }

        // PowerPoint转PDF（修复格式枚举错误）
        private void ConvertPowerPointToPdf(string inputPath, string outputPath)
        {
            using (var presentation = new Presentation())
            {
                presentation.LoadFromFile(inputPath);
                presentation.SaveToFile(outputPath, Spire.Presentation.FileFormat.PDF); // 正确使用Presentation的PDF格式
            }
            LogMessage($"→ 已转换为PDF：{Path.GetFileName(outputPath)}");
        }

        // PowerPoint提取文本到TXT
        private void ConvertPowerPointToTxt(string inputPath, string outputPath)
        {
            // 明确指定Presentation为Spire.Presentation.Presentation
            using (Presentation presentation = new Presentation())
            {
                presentation.LoadFromFile(inputPath);
                StringBuilder textBuilder = new StringBuilder();
                textBuilder.AppendLine($"演示文稿：{Path.GetFileNameWithoutExtension(inputPath)}");
                textBuilder.AppendLine("====================================");

                // 1. 明确slide为ISlide类型（Spire.Presentation中的接口）
                foreach (ISlide slide in presentation.Slides)
                {
                    // 现在可正常访问SlideNumber（ISlide接口的属性）
                    textBuilder.AppendLine($"【幻灯片 {slide.SlideNumber}】");
                    textBuilder.AppendLine("------------------------------------");

                    // 2. 明确shape为IShape类型（Spire.Presentation中的接口）
                    foreach (IShape shape in slide.Shapes)
                    {
                        // 文本框：明确为IAutoShape（继承自IShape）
                        if (shape is IAutoShape autoShape)
                        {
                            // 3. 明确paragraph为TextParagraph类型（Spire.Presentation中的文本段落）
                            foreach (TextParagraph paragraph in autoShape.TextFrame.Paragraphs)
                            {
                                if (!string.IsNullOrWhiteSpace(paragraph.Text))
                                {
                                    textBuilder.AppendLine(paragraph.Text.Trim());
                                }
                            }
                        }
                        // 表格：明确为Spire.Presentation.ITable
                        else if (shape is Spire.Presentation.ITable table)
                        {
                            for (int i = 0; i < table.TableRows.Count; i++)
                            {
                                for (int j = 0; j < table.ColumnsList.Count; j++)
                                {
                                    // 表格单元格文本
                                    string cellText = table[i, j].TextFrame.Text.Trim();
                                    if (!string.IsNullOrWhiteSpace(cellText))
                                    {
                                        textBuilder.AppendLine($"表格[{i + 1},{j + 1}]：{cellText}");
                                    }
                                }
                            }
                        }
                    }
                    textBuilder.AppendLine();
                }

                // 保存文本
                File.WriteAllText(outputPath, textBuilder.ToString(), Encoding.UTF8);
                LogMessage($"→ 已提取文本：{Path.GetFileName(outputPath)}");
            }
        }
        #endregion

        #region 进度更新与完成处理
        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            lblProgress.Text = e.UserState.ToString();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // 恢复控件状态
            btnStartConversion.Enabled = true;
            btnSourceFolder.Enabled = true;
            btnOutputFolder.Enabled = true;
            chkConvertToPdf.Enabled = true;
            chkConvertToTxt.Enabled = true;
            chkIncludeSubfolders.Enabled = true;
            btnCancel.Enabled = false;
            btnCancel.Text = "取消";

            // 显示结果提示
            if (e.Cancelled)
            {
                lblProgress.Text = "已取消";
                LogMessage("转换已取消");
                MessageBox.Show("转换已取消", "操作完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                lblProgress.Text = "转换完成";
                var resultMsg = $"转换完成！共处理 {_completedFiles + _failedFiles} 个文件，成功 {_completedFiles} 个，失败 {_failedFiles} 个。";
                LogMessage(resultMsg);
                MessageBox.Show(resultMsg, "转换完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region 辅助方法
        // 线程安全的日志输出
        private void LogMessage(string message)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(() =>
                {
                    txtLog.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}\r\n");
                    txtLog.ScrollToCaret();
                });
            }
            else
            {
                txtLog.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}\r\n");
                txtLog.ScrollToCaret();
            }
        }
        #endregion
    }
}