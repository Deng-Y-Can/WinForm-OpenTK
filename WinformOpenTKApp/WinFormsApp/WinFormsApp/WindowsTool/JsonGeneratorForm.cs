using System;
using System.Windows.Forms;
using WinFormsApp.CandyTool;

namespace WinFormsApp.WindowsTool
{
    public partial class JsonGeneratorForm : Form
    {
        private string inputDirectoryPath = "";
        private string outputFilePath = "";

        public JsonGeneratorForm()
        {
            InitializeComponent();
        }

        private void btnSelectInputPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "选择要扫描的文件夹";
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    inputDirectoryPath = folderDialog.SelectedPath;
                    lblInputPath.Text = inputDirectoryPath;
                }
            }
        }

        private void btnSelectOutputPath_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog fileDialog = new SaveFileDialog())
            {
                fileDialog.Filter = "JSON文件|*.json";
                fileDialog.Title = "保存JSON文件";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    outputFilePath = fileDialog.FileName;
                    lblOutputPath.Text = outputFilePath;
                }
            }
        }

        private void btnGenerateJson_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(inputDirectoryPath))
            {
                MessageBox.Show("请选择输入文件夹", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(outputFilePath))
            {
                MessageBox.Show("请选择输出文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                CandyJson.ScanDirectoryAndSaveAsJson(inputDirectoryPath, outputFilePath);
                MessageBox.Show("JSON生成成功！", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"生成JSON时出错：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}