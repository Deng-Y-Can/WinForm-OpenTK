namespace WinFormsApp.WindowsTool
{
    partial class FileSort
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为true；否则为false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            btnFunction1 = new Button();
            btnFunction2 = new Button();
            btnFunction3 = new Button();
            lblImportPath = new Label();
            lblExportPath = new Label();
            lblJsonPath = new Label();
            lblFunction2Path = new Label();
            lblFunction3Path = new Label();
            chkFunction2 = new CheckBox();
            folderBrowserDialog1 = new FolderBrowserDialog();
            openFileDialog1 = new OpenFileDialog();
            SuspendLayout();
            // 
            // btnFunction1
            // 
            btnFunction1.Location = new Point(18, 20);
            btnFunction1.Margin = new Padding(4, 5, 4, 5);
            btnFunction1.Name = "btnFunction1";
            btnFunction1.Size = new Size(225, 50);
            btnFunction1.TabIndex = 0;
            btnFunction1.Text = "选择导入/导出路径";
            btnFunction1.UseVisualStyleBackColor = true;
            btnFunction1.Click += btnFunction1_Click;
            // 
            // btnFunction2
            // 
            btnFunction2.Location = new Point(18, 220);
            btnFunction2.Margin = new Padding(4, 5, 4, 5);
            btnFunction2.Name = "btnFunction2";
            btnFunction2.Size = new Size(225, 50);
            btnFunction2.TabIndex = 1;
            btnFunction2.Text = "选择文件夹路径2";
            btnFunction2.UseVisualStyleBackColor = true;
            btnFunction2.Click += btnFunction2_Click;
            // 
            // btnFunction3
            // 
            btnFunction3.Location = new Point(18, 370);
            btnFunction3.Margin = new Padding(4, 5, 4, 5);
            btnFunction3.Name = "btnFunction3";
            btnFunction3.Size = new Size(225, 50);
            btnFunction3.TabIndex = 2;
            btnFunction3.Text = "选择文件夹路径3";
            btnFunction3.UseVisualStyleBackColor = true;
            btnFunction3.Click += btnFunction3_Click;
            // 
            // lblImportPath
            // 
            lblImportPath.AutoSize = true;
            lblImportPath.Location = new Point(27, 90);
            lblImportPath.Margin = new Padding(4, 0, 4, 0);
            lblImportPath.Name = "lblImportPath";
            lblImportPath.Size = new Size(77, 20);
            lblImportPath.TabIndex = 3;
            lblImportPath.Text = "导入路径: ";
            // 
            // lblExportPath
            // 
            lblExportPath.AutoSize = true;
            lblExportPath.Location = new Point(27, 130);
            lblExportPath.Margin = new Padding(4, 0, 4, 0);
            lblExportPath.Name = "lblExportPath";
            lblExportPath.Size = new Size(77, 20);
            lblExportPath.TabIndex = 4;
            lblExportPath.Text = "导出路径: ";
            // 
            // lblJsonPath
            // 
            lblJsonPath.AutoSize = true;
            lblJsonPath.Location = new Point(27, 170);
            lblJsonPath.Margin = new Padding(4, 0, 4, 0);
            lblJsonPath.Name = "lblJsonPath";
            lblJsonPath.Size = new Size(56, 20);
            lblJsonPath.TabIndex = 5;
            lblJsonPath.Text = "JSON: ";
            // 
            // lblFunction2Path
            // 
            lblFunction2Path.AutoSize = true;
            lblFunction2Path.Location = new Point(27, 290);
            lblFunction2Path.Margin = new Padding(4, 0, 4, 0);
            lblFunction2Path.Name = "lblFunction2Path";
            lblFunction2Path.Size = new Size(56, 20);
            lblFunction2Path.TabIndex = 6;
            lblFunction2Path.Text = "路径2: ";
            // 
            // lblFunction3Path
            // 
            lblFunction3Path.AutoSize = true;
            lblFunction3Path.Location = new Point(27, 440);
            lblFunction3Path.Margin = new Padding(4, 0, 4, 0);
            lblFunction3Path.Name = "lblFunction3Path";
            lblFunction3Path.Size = new Size(56, 20);
            lblFunction3Path.TabIndex = 7;
            lblFunction3Path.Text = "路径3: ";
            // 
            // chkFunction2
            // 
            chkFunction2.AutoSize = true;
            chkFunction2.Location = new Point(261, 233);
            chkFunction2.Margin = new Padding(4, 5, 4, 5);
            chkFunction2.Name = "chkFunction2";
            chkFunction2.Size = new Size(136, 24);
            chkFunction2.TabIndex = 8;
            chkFunction2.Text = "是否放在根目录";
            chkFunction2.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            openFileDialog1.Filter = "JSON文件|*.json|所有文件|*.*";
            // 
            // FileSort
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 500);
            Controls.Add(chkFunction2);
            Controls.Add(lblFunction3Path);
            Controls.Add(lblFunction2Path);
            Controls.Add(lblJsonPath);
            Controls.Add(lblExportPath);
            Controls.Add(lblImportPath);
            Controls.Add(btnFunction3);
            Controls.Add(btnFunction2);
            Controls.Add(btnFunction1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "FileSort";
            Text = "文件和文件夹选择器";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFunction1;
        private System.Windows.Forms.Button btnFunction2;
        private System.Windows.Forms.Button btnFunction3;
        private System.Windows.Forms.Label lblImportPath;
        private System.Windows.Forms.Label lblExportPath;
        private System.Windows.Forms.Label lblJsonPath;
        private System.Windows.Forms.Label lblFunction2Path;
        private System.Windows.Forms.Label lblFunction3Path;
        private System.Windows.Forms.CheckBox chkFunction2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}