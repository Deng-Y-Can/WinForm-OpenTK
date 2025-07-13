using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WinFormsApp.WindowsTool
{
    public partial class TxtEncoding : Form
    {
        public TxtEncoding()
        {
            // 关键修复：注册代码页编码提供程序（必须在初始化组件前执行）
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            InitializeComponent();

            // 初始化编码下拉框（包含常见中文编码）
            cboEncoding.Items.AddRange(new object[] {
                "UTF-8 (无BOM)",
                "UTF-8 (带BOM)",
                "UTF-16 (Unicode, Little Endian)",
                "UTF-16 (Big Endian)",
                "GB2312",
                "GB18030",
                "ASCII"
            });
            cboEncoding.SelectedIndex = 0; // 默认UTF-8无BOM
        }

        // 选择输入文件夹
        private void btnSelectInputFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "请选择包含TXT文件的文件夹";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtInputFolder.Text = dialog.SelectedPath;
                }
            }
        }

        // 选择输出文件夹
        private void btnSelectOutputFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "请选择转换后文件的输出文件夹";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtOutputFolder.Text = dialog.SelectedPath;
                }
            }
        }

        // 退出
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        // 转换按钮逻辑
        private async void btnConvert_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInputFolder.Text) || !Directory.Exists(txtInputFolder.Text))
            {
                MessageBox.Show("请选择有效的源文件夹", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtOutputFolder.Text))
            {
                MessageBox.Show("请选择输出文件夹", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cboEncoding.SelectedIndex < 0)
            {
                MessageBox.Show("请选择目标编码格式", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtInputFolder.Text.Trim().Equals(txtOutputFolder.Text.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                DialogResult result = MessageBox.Show("输出文件夹与输入文件夹相同，转换后将覆盖原文件。是否继续？",
                    "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes) return;
            }

            // 禁用UI防止重复操作
            btnConvert.Enabled = false;
            btnSelectInputFolder.Enabled = false;
            btnSelectOutputFolder.Enabled = false;
            cboEncoding.Enabled = false;
            txtLog.Clear();
            progressBar.Value = 0;
            lblStatus.Text = "正在处理...";

            try
            {
                string[] txtFiles = Directory.GetFiles(txtInputFolder.Text, "*.txt", SearchOption.AllDirectories);
                if (txtFiles.Length == 0)
                {
                    MessageBox.Show("未找到TXT文件", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Encoding targetEncoding = GetSelectedEncoding();
                int processedCount = 0;
                foreach (string filePath in txtFiles)
                {
                    await Task.Run(() => ConvertFile(filePath, targetEncoding));
                    processedCount++;
                    UpdateProgress((int)((double)processedCount / txtFiles.Length * 100),
                        $"已处理: {processedCount}/{txtFiles.Length}");
                }

                lblStatus.Text = "转换完成!";
                MessageBox.Show($"所有文件已转换为{GetEncodingName(targetEncoding)}编码。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                lblStatus.Text = "转换错误";
                MessageBox.Show($"错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogMessage($"错误: {ex.Message}");
            }
            finally
            {
                // 恢复UI状态
                btnConvert.Enabled = true;
                btnSelectInputFolder.Enabled = true;
                btnSelectOutputFolder.Enabled = true;
                cboEncoding.Enabled = true;
            }
        }

        // 转换单个文件
        private void ConvertFile(string filePath, Encoding targetEncoding)
        {
            try
            {
                string content = ReadFileWithAutoEncoding(filePath);
                string relativePath = Path.GetRelativePath(txtInputFolder.Text, filePath);
                string outputPath = Path.Combine(txtOutputFolder.Text, relativePath);
                string outputDir = Path.GetDirectoryName(outputPath);
                if (!Directory.Exists(outputDir))
                    Directory.CreateDirectory(outputDir);

                // 用目标编码写入文件
                using (StreamWriter writer = new StreamWriter(outputPath, false, targetEncoding))
                {
                    writer.Write(content);
                }
                LogMessage($"成功转换: {relativePath}");
            }
            catch (Exception ex)
            {
                LogMessage($"转换失败: {Path.GetFileName(filePath)} - {ex.Message}");
            }
        }

        // 自动检测编码并读取文件（优化中文编码处理）
        private string ReadFileWithAutoEncoding(string filePath)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            if (fileBytes.Length == 0)
                return "";

            // 1. 检测BOM头确定编码
            Encoding encoding = GetEncodingFromBOM(fileBytes);
            if (encoding != null)
            {
                int bomLength = GetBomLength(encoding);
                return encoding.GetString(fileBytes, bomLength, fileBytes.Length - bomLength);
            }

            // 2. 无BOM时尝试常见中文编码（优先处理GB18030）
            List<Encoding> candidateEncodings = new List<Encoding>
            {
                Encoding.UTF8,
                Encoding.GetEncoding("GB18030"), // 已通过注册支持
                Encoding.GetEncoding("GB2312"),
                Encoding.Default // 系统默认编码
            };

            foreach (var enc in candidateEncodings)
            {
                try
                {
                    string content = enc.GetString(fileBytes);
                    // 检测是否有乱码标记（�）
                    if (!content.Contains('�'))
                    {
                        return content;
                    }
                }
                catch
                {
                    // 编码不匹配时跳过，尝试下一种
                    continue;
                }
            }

            // 3. 所有编码尝试失败时，用UTF-8强制读取（替换错误字符）
            return Encoding.UTF8.GetString(fileBytes, 0, fileBytes.Length);
        }

        // 从BOM头获取编码
        private Encoding GetEncodingFromBOM(byte[] bom)
        {
            // UTF-8 BOM (EF BB BF)
            if (bom.Length >= 3 && bom[0] == 0xEF && bom[1] == 0xBB && bom[2] == 0xBF)
                return Encoding.UTF8;
            // UTF-16 Little Endian (FF FE)
            if (bom.Length >= 2 && bom[0] == 0xFF && bom[1] == 0xFE)
                return Encoding.Unicode;
            // UTF-16 Big Endian (FE FF)
            if (bom.Length >= 2 && bom[0] == 0xFE && bom[1] == 0xFF)
                return Encoding.BigEndianUnicode;
            // UTF-32 Little Endian (FF FE 00 00)
            if (bom.Length >= 4 && bom[0] == 0xFF && bom[1] == 0xFE && bom[2] == 0x00 && bom[3] == 0x00)
                return Encoding.UTF32;
            // UTF-32 Big Endian (00 00 FE FF)
            if (bom.Length >= 4 && bom[0] == 0x00 && bom[1] == 0x00 && bom[2] == 0xFE && bom[3] == 0xFF)
                return new UTF32Encoding(true, true);

            return null;
        }

        // 获取BOM头长度
        private int GetBomLength(Encoding encoding)
        {
            if (encoding == Encoding.UTF8)
                return 3;
            if (encoding == Encoding.Unicode || encoding == Encoding.BigEndianUnicode)
                return 2;
            if (encoding == Encoding.UTF32 || encoding is UTF32Encoding)
                return 4;
            return 0;
        }

        // 获取选中的目标编码
        private Encoding GetSelectedEncoding()
        {
            switch (cboEncoding.SelectedIndex)
            {
                case 0:
                    return new UTF8Encoding(false); // UTF-8无BOM
                case 1:
                    return new UTF8Encoding(true);  // UTF-8带BOM
                case 2:
                    return Encoding.Unicode;        // UTF-16 Little Endian
                case 3:
                    return Encoding.BigEndianUnicode; // UTF-16 Big Endian
                case 4:
                    return Encoding.GetEncoding("GB2312");
                case 5:
                    // 显式处理GB18030，确保编码可用
                    return Encoding.GetEncoding("GB18030");
                case 6:
                    return Encoding.ASCII;
                default:
                    return Encoding.UTF8;
            }
        }

        // 获取编码显示名称
        private string GetEncodingName(Encoding encoding)
        {
            if (encoding == new UTF8Encoding(false))
                return "UTF-8 (无BOM)";
            if (encoding == new UTF8Encoding(true))
                return "UTF-8 (带BOM)";
            if (encoding == Encoding.Unicode)
                return "UTF-16 (Little Endian)";
            if (encoding == Encoding.BigEndianUnicode)
                return "UTF-16 (Big Endian)";
            if (encoding.BodyName == "gb2312")
                return "GB2312";
            if (encoding.BodyName == "gb18030")
                return "GB18030";
            if (encoding == Encoding.ASCII)
                return "ASCII";
            return encoding.EncodingName;
        }

        // 日志输出（跨线程安全）
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

        // 更新进度（跨线程安全）
        private void UpdateProgress(int value, string status)
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(() =>
                {
                    progressBar.Value = value;
                    lblStatus.Text = status;
                });
            }
            else
            {
                progressBar.Value = value;
                lblStatus.Text = status;
            }
        }
    }
}