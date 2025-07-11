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
    public partial class JsonGeneratorForm : Form
    {
        private string inputDirectoryPath = "";
        private string outputFilePath = "";

        public JsonGeneratorForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            this.btnSelectInputPath = new System.Windows.Forms.Button();
            this.btnSelectOutputPath = new System.Windows.Forms.Button();
            this.btnGenerateJson = new System.Windows.Forms.Button();
            this.lblInputPath = new System.Windows.Forms.Label();
            this.lblOutputPath = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // btnSelectInputPath
            this.btnSelectInputPath.Location = new System.Drawing.Point(12, 12);
            this.btnSelectInputPath.Name = "btnSelectInputPath";
            this.btnSelectInputPath.Size = new System.Drawing.Size(150, 30);
            this.btnSelectInputPath.TabIndex = 0;
            this.btnSelectInputPath.Text = "选择输入文件夹";
            this.btnSelectInputPath.UseVisualStyleBackColor = true;
            this.btnSelectInputPath.Click += new System.EventHandler(this.btnSelectInputPath_Click);

            // btnSelectOutputPath
            this.btnSelectOutputPath.Location = new System.Drawing.Point(12, 57);
            this.btnSelectOutputPath.Name = "btnSelectOutputPath";
            this.btnSelectOutputPath.Size = new System.Drawing.Size(150, 30);
            this.btnSelectOutputPath.TabIndex = 1;
            this.btnSelectOutputPath.Text = "选择输出文件";
            this.btnSelectOutputPath.UseVisualStyleBackColor = true;
            this.btnSelectOutputPath.Click += new System.EventHandler(this.btnSelectOutputPath_Click);

            // btnGenerateJson
            this.btnGenerateJson.Location = new System.Drawing.Point(12, 102);
            this.btnGenerateJson.Name = "btnGenerateJson";
            this.btnGenerateJson.Size = new System.Drawing.Size(150, 30);
            this.btnGenerateJson.TabIndex = 2;
            this.btnGenerateJson.Text = "生成JSON";
            this.btnGenerateJson.UseVisualStyleBackColor = true;
            this.btnGenerateJson.Click += new System.EventHandler(this.btnGenerateJson_Click);

            // lblInputPath
            this.lblInputPath.AutoSize = true;
            this.lblInputPath.Location = new System.Drawing.Point(168, 20);
            this.lblInputPath.Name = "lblInputPath";
            this.lblInputPath.Size = new System.Drawing.Size(65, 15);
            this.lblInputPath.TabIndex = 3;
            this.lblInputPath.Text = "未选择路径";

            // lblOutputPath
            this.lblOutputPath.AutoSize = true;
            this.lblOutputPath.Location = new System.Drawing.Point(168, 65);
            this.lblOutputPath.Name = "lblOutputPath";
            this.lblOutputPath.Size = new System.Drawing.Size(65, 15);
            this.lblOutputPath.TabIndex = 4;
            this.lblOutputPath.Text = "未选择路径";

            // JsonGeneratorForm
            this.ClientSize = new System.Drawing.Size(500, 150);
            this.Controls.Add(this.lblOutputPath);
            this.Controls.Add(this.lblInputPath);
            this.Controls.Add(this.btnGenerateJson);
            this.Controls.Add(this.btnSelectOutputPath);
            this.Controls.Add(this.btnSelectInputPath);
            this.Name = "JsonGeneratorForm";
            this.Text = "JSON生成器";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnSelectInputPath;
        private System.Windows.Forms.Button btnSelectOutputPath;
        private System.Windows.Forms.Button btnGenerateJson;
        private System.Windows.Forms.Label lblInputPath;
        private System.Windows.Forms.Label lblOutputPath;

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
