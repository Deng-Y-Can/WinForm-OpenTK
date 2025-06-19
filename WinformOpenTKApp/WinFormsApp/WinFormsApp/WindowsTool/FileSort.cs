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
    public partial class FileSort : Form
    {
        public FileSort()
        {
            InitializeComponent();
        }

        private void btnFunction1_Click(object sender, EventArgs e)
        {
            // 选择导入文件夹
            folderBrowserDialog1.Description = "选择导入文件夹";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                lblImportPath.Text = "导入路径: " + folderBrowserDialog1.SelectedPath;
            }
            else
            {
                MessageBox.Show("未选择导入文件夹！");
                return;
            }

            // 选择导出文件夹
            folderBrowserDialog1.Description = "选择导出文件夹";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                lblExportPath.Text = "导出路径: " + folderBrowserDialog1.SelectedPath;
            }
            else
            {
                MessageBox.Show("未选择导出文件夹！");
                return;
            }

            // 选择JSON文件
            openFileDialog1.Title = "选择JSON参数文件";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                lblJsonPath.Text = "JSON: " + Path.GetFileName(openFileDialog1.FileName);
            }
            else
            {
                MessageBox.Show("未选择JSON文件！");
                return;
            }

            // 执行方法
            try
            {
                // 调用方法，传入三个参数
                FileSorter.SortFiles(lblImportPath.Text.Substring(6),
                        openFileDialog1.FileName, lblExportPath.Text.Substring(6));

                MessageBox.Show("方法1执行成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("方法1执行失败：" + ex.Message);
            }
        }

        private void btnFunction2_Click(object sender, EventArgs e)
        {
            // 选择文件夹
            folderBrowserDialog1.Description = "选择文件夹路径2";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                lblFunction2Path.Text = "路径2: " + folderBrowserDialog1.SelectedPath;

                // 执行方法
                try
                {
                    // 调用方法，传入路径和勾选状态
                    FileSorter.SortByFileType(folderBrowserDialog1.SelectedPath, chkFunction2.Checked);

                    MessageBox.Show("方法2执行成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("方法2执行失败：" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("未选择文件夹！");
            }
        }

        private void btnFunction3_Click(object sender, EventArgs e)
        {
            // 选择文件夹
            folderBrowserDialog1.Description = "选择文件夹路径3";
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                lblFunction3Path.Text = "路径3: " + folderBrowserDialog1.SelectedPath;

                // 执行方法
                try
                {
                    // 调用方法，传入路径
                    FileSorter.ExportToJson(folderBrowserDialog1.SelectedPath);

                    MessageBox.Show("方法3执行成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("方法3执行失败：" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("未选择文件夹！");
            }
        }

      
    }
}
