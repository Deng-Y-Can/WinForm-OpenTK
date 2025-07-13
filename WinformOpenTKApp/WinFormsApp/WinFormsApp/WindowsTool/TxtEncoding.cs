using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            InitializeComponent();

            // 设置默认编码
            cboEncoding.SelectedIndex = 0; // UTF-8
        }

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

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void btnConvert_Click(object sender, EventArgs e)
        {
            // 验证输入
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

            // 确认是否继续（如果输出文件夹与输入文件夹相同）
            if (txtInputFolder.Text.Trim().Equals(txtOutputFolder.Text.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                DialogResult result = MessageBox.Show("输出文件夹与输入文件夹相同，转换后的文件将覆盖原文件。是否继续？",
                    "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result != DialogResult.Yes)
                {
                    return;
                }
            }

            // 禁用UI控件
            btnConvert.Enabled = false;
            btnSelectInputFolder.Enabled = false;
            btnSelectOutputFolder.Enabled = false;
            cboEncoding.Enabled = false;
            txtLog.Clear();
            progressBar.Value = 0;
            lblStatus.Text = "正在处理...";

            try
            {
                // 获取所有TXT文件
                string[] txtFiles = Directory.GetFiles(txtInputFolder.Text, "*.txt", SearchOption.AllDirectories);

                if (txtFiles.Length == 0)
                {
                    MessageBox.Show("在指定文件夹中未找到TXT文件", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 获取目标编码
                Encoding targetEncoding = GetSelectedEncoding();

                // 转换文件
                int processedCount = 0;
                foreach (string filePath in txtFiles)
                {
                    await Task.Run(() => ConvertFile(filePath, targetEncoding));

                    processedCount++;
                    int progress = (int)((double)processedCount / txtFiles.Length * 100);
                    UpdateProgress(progress, $"已处理: {processedCount}/{txtFiles.Length}");
                }

                lblStatus.Text = "转换完成!";
                MessageBox.Show($"所有文件已成功转换为{GetEncodingName(targetEncoding)}编码。",
                    "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                lblStatus.Text = "转换过程中发生错误";
                MessageBox.Show($"转换过程中发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogMessage($"错误: {ex.Message}");
                if (ex.InnerException != null)
                {
                    LogMessage($"内部错误: {ex.InnerException.Message}");
                }
            }
            finally
            {
                // 启用UI控件
                btnConvert.Enabled = true;
                btnSelectInputFolder.Enabled = true;
                btnSelectOutputFolder.Enabled = true;
                cboEncoding.Enabled = true;
            }
        }

        private void ConvertFile(string filePath, Encoding targetEncoding)
        {
            try
            {
                // 读取文件内容（自动检测编码）
                string content = ReadFileWithAutoEncoding(filePath);

                // 创建输出路径
                string relativePath = Path.GetRelativePath(txtInputFolder.Text, filePath);
                string outputPath = Path.Combine(txtOutputFolder.Text, relativePath);
                string outputDir = Path.GetDirectoryName(outputPath);

                if (!Directory.Exists(outputDir))
                {
                    Directory.CreateDirectory(outputDir);
                }

                // 写入文件（使用指定编码）
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

        private string ReadFileWithAutoEncoding(string filePath)
        {
            // 读取文件前4个字节以检测BOM
            byte[] bom = new byte[4];
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                if (fs.Length >= 4)
                {
                    fs.Read(bom, 0, 4);
                }
                fs.Seek(0, SeekOrigin.Begin);

                // 检测BOM
                Encoding encoding = GetEncodingFromBOM(bom);

                // 如果没有BOM，使用默认编码
                if (encoding == null)
                {
                    // 这里使用默认的检测方式，可以根据需要改进
                    encoding = Encoding.Default;
                }

                // 使用检测到的编码读取文件
                using (StreamReader reader = new StreamReader(fs, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private Encoding GetEncodingFromBOM(byte[] bom)
        {
            // 检测UTF-8 BOM
            if (bom[0] == 0xEF && bom[1] == 0xBB && bom[2] == 0xBF)
            {
                return Encoding.UTF8;
            }
            // 检测UTF-16 (Little Endian)
            else if (bom[0] == 0xFF && bom[1] == 0xFE)
            {
                return Encoding.Unicode;
            }
            // 检测UTF-16 (Big Endian)
            else if (bom[0] == 0xFE && bom[1] == 0xFF)
            {
                return Encoding.BigEndianUnicode;
            }
            // 检测UTF-32 (Little Endian)
            else if (bom[0] == 0xFF && bom[1] == 0xFE && bom[2] == 0x00 && bom[3] == 0x00)
            {
                return Encoding.UTF32;
            }
            // 检测UTF-32 (Big Endian)
            else if (bom[0] == 0x00 && bom[1] == 0x00 && bom[2] == 0xFE && bom[3] == 0xFF)
            {
                return new UTF32Encoding(true, true);
            }
            else
            {
                // 没有BOM，需要其他方式检测编码
                return null;
            }
        }

        private Encoding GetSelectedEncoding()
        {
            switch (cboEncoding.SelectedIndex)
            {
                case 0: // UTF-8
                    return new UTF8Encoding(false); // 无BOM
                case 1: // UTF-8 (带BOM)
                    return new UTF8Encoding(true);  // 带BOM
                case 2: // UTF-16 (Unicode)
                    return Encoding.Unicode;
                case 3: // UTF-16 BE (Big Endian)
                    return Encoding.BigEndianUnicode;
                case 4: // GB2312
                    return Encoding.GetEncoding("gb2312");
                case 5: // GB18030
                    return Encoding.GetEncoding("gb18030");
                case 6: // ASCII
                    return Encoding.ASCII;
                default:
                    return Encoding.UTF8;
            }
        }

        private string GetEncodingName(Encoding encoding)
        {
            if (encoding == Encoding.UTF8)
            {
                return "UTF-8";
            }
            else if (encoding == new UTF8Encoding(true))
            {
                return "UTF-8 (带BOM)";
            }
            else if (encoding == Encoding.Unicode)
            {
                return "UTF-16 (Unicode)";
            }
            else if (encoding == Encoding.BigEndianUnicode)
            {
                return "UTF-16 BE (Big Endian)";
            }
            else if (encoding == Encoding.GetEncoding("gb2312"))
            {
                return "GB2312";
            }
            else if (encoding == Encoding.GetEncoding("gb18030"))
            {
                return "GB18030";
            }
            else if (encoding == Encoding.ASCII)
            {
                return "ASCII";
            }
            else
            {
                return encoding.EncodingName;
            }
        }

        private void LogMessage(string message)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action(() =>
                {
                    txtLog.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}\r\n");
                    txtLog.ScrollToCaret();
                }));
            }
            else
            {
                txtLog.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}\r\n");
                txtLog.ScrollToCaret();
            }
        }

        private void UpdateProgress(int value, string status)
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(new Action(() =>
                {
                    progressBar.Value = value;
                    lblStatus.Text = status;
                }));
            }
            else
            {
                progressBar.Value = value;
                lblStatus.Text = status;
            }
        }
    }
}
