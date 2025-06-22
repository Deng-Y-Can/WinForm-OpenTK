namespace WinFormsApp
{
    partial class BookLabel
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
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
            btnSelectExcel = new Button();
            btnSelectTemplate = new Button();
            btnSelectOutput = new Button();
            btnProcess = new Button();
            lblExcelPath = new Label();
            lblTemplatePath = new Label();
            lblOutputPath = new Label();
            btnSelectFont = new Button();
            btnSelectColor = new Button();
            lblFontSettings = new Label();
            lblCurrentFont = new Label();
            lblCurrentColor = new Label();
            fontDialog = new FontDialog();
            colorDialog = new ColorDialog();
            numFontSize = new NumericUpDown();
            lblFontSize = new Label();
            ((System.ComponentModel.ISupportInitialize)numFontSize).BeginInit();
            SuspendLayout();
            // 
            // btnSelectExcel
            // 
            btnSelectExcel.Location = new Point(18, 20);
            btnSelectExcel.Margin = new Padding(4, 5, 4, 5);
            btnSelectExcel.Name = "btnSelectExcel";
            btnSelectExcel.Size = new Size(180, 50);
            btnSelectExcel.TabIndex = 0;
            btnSelectExcel.Text = "选择Excel文件";
            btnSelectExcel.UseVisualStyleBackColor = true;
            btnSelectExcel.Click += btnSelectExcel_Click;
            // 
            // btnSelectTemplate
            // 
            btnSelectTemplate.Location = new Point(18, 97);
            btnSelectTemplate.Margin = new Padding(4, 5, 4, 5);
            btnSelectTemplate.Name = "btnSelectTemplate";
            btnSelectTemplate.Size = new Size(180, 50);
            btnSelectTemplate.TabIndex = 1;
            btnSelectTemplate.Text = "选择Word模板";
            btnSelectTemplate.UseVisualStyleBackColor = true;
            btnSelectTemplate.Click += btnSelectTemplate_Click;
            // 
            // btnSelectOutput
            // 
            btnSelectOutput.Location = new Point(18, 173);
            btnSelectOutput.Margin = new Padding(4, 5, 4, 5);
            btnSelectOutput.Name = "btnSelectOutput";
            btnSelectOutput.Size = new Size(180, 50);
            btnSelectOutput.TabIndex = 2;
            btnSelectOutput.Text = "选择输出文件夹";
            btnSelectOutput.UseVisualStyleBackColor = true;
            btnSelectOutput.Click += btnSelectOutput_Click;
            // 
            // btnProcess
            // 
            btnProcess.Location = new Point(18, 250);
            btnProcess.Margin = new Padding(4, 5, 4, 5);
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new Size(180, 50);
            btnProcess.TabIndex = 3;
            btnProcess.Text = "开始处理";
            btnProcess.UseVisualStyleBackColor = true;
            btnProcess.Click += btnProcess_Click;
            // 
            // lblExcelPath
            // 
            lblExcelPath.AutoSize = true;
            lblExcelPath.Location = new Point(222, 37);
            lblExcelPath.Margin = new Padding(4, 0, 4, 0);
            lblExcelPath.Name = "lblExcelPath";
            lblExcelPath.Size = new Size(84, 20);
            lblExcelPath.TabIndex = 4;
            lblExcelPath.Text = "未选择文件";
            // 
            // lblTemplatePath
            // 
            lblTemplatePath.AutoSize = true;
            lblTemplatePath.Location = new Point(222, 113);
            lblTemplatePath.Margin = new Padding(4, 0, 4, 0);
            lblTemplatePath.Name = "lblTemplatePath";
            lblTemplatePath.Size = new Size(84, 20);
            lblTemplatePath.TabIndex = 5;
            lblTemplatePath.Text = "未选择文件";
            // 
            // lblOutputPath
            // 
            lblOutputPath.AutoSize = true;
            lblOutputPath.Location = new Point(222, 190);
            lblOutputPath.Margin = new Padding(4, 0, 4, 0);
            lblOutputPath.Name = "lblOutputPath";
            lblOutputPath.Size = new Size(99, 20);
            lblOutputPath.TabIndex = 6;
            lblOutputPath.Text = "未选择文件夹";
            // 
            // btnSelectFont
            // 
            btnSelectFont.Location = new Point(18, 327);
            btnSelectFont.Margin = new Padding(4, 5, 4, 5);
            btnSelectFont.Name = "btnSelectFont";
            btnSelectFont.Size = new Size(180, 50);
            btnSelectFont.TabIndex = 7;
            btnSelectFont.Text = "选择字体";
            btnSelectFont.UseVisualStyleBackColor = true;
            btnSelectFont.Click += btnSelectFont_Click;
            // 
            // btnSelectColor
            // 
            btnSelectColor.Location = new Point(222, 327);
            btnSelectColor.Margin = new Padding(4, 5, 4, 5);
            btnSelectColor.Name = "btnSelectColor";
            btnSelectColor.Size = new Size(180, 50);
            btnSelectColor.TabIndex = 8;
            btnSelectColor.Text = "选择颜色";
            btnSelectColor.UseVisualStyleBackColor = true;
            btnSelectColor.Click += btnSelectColor_Click;
            // 
            // lblFontSettings
            // 
            lblFontSettings.AutoSize = true;
            lblFontSettings.Location = new Point(18, 300);
            lblFontSettings.Margin = new Padding(4, 0, 4, 0);
            lblFontSettings.Name = "lblFontSettings";
            lblFontSettings.Size = new Size(73, 20);
            lblFontSettings.TabIndex = 11;
            lblFontSettings.Text = "字体设置:";
            // 
            // lblCurrentFont
            // 
            lblCurrentFont.AutoSize = true;
            lblCurrentFont.Location = new Point(222, 300);
            lblCurrentFont.Margin = new Padding(4, 0, 4, 0);
            lblCurrentFont.Name = "lblCurrentFont";
            lblCurrentFont.Size = new Size(149, 20);
            lblCurrentFont.TabIndex = 12;
            lblCurrentFont.Text = "当前字体: 宋体, 10pt";
            // 
            // lblCurrentColor
            // 
            lblCurrentColor.AutoSize = true;
            lblCurrentColor.Location = new Point(426, 300);
            lblCurrentColor.Margin = new Padding(4, 0, 4, 0);
            lblCurrentColor.Name = "lblCurrentColor";
            lblCurrentColor.Size = new Size(107, 20);
            lblCurrentColor.TabIndex = 13;
            lblCurrentColor.Text = "当前颜色: 黑色";
            // 
            // numFontSize
            // 
            numFontSize.Location = new Point(514, 337);
            numFontSize.Margin = new Padding(4, 5, 4, 5);
            numFontSize.Minimum = new decimal(new int[] { 6, 0, 0, 0 });
            numFontSize.Name = "numFontSize";
            numFontSize.Size = new Size(90, 27);
            numFontSize.TabIndex = 10;
            numFontSize.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // lblFontSize
            // 
            lblFontSize.AutoSize = true;
            lblFontSize.Location = new Point(426, 340);
            lblFontSize.Margin = new Padding(4, 0, 4, 0);
            lblFontSize.Name = "lblFontSize";
            lblFontSize.Size = new Size(73, 20);
            lblFontSize.TabIndex = 9;
            lblFontSize.Text = "字体大小:";
            // 
            // BookLabel
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(799, 465);
            Controls.Add(lblOutputPath);
            Controls.Add(lblTemplatePath);
            Controls.Add(lblExcelPath);
            Controls.Add(btnProcess);
            Controls.Add(btnSelectOutput);
            Controls.Add(btnSelectTemplate);
            Controls.Add(btnSelectExcel);
            Controls.Add(lblCurrentColor);
            Controls.Add(lblCurrentFont);
            Controls.Add(lblFontSettings);
            Controls.Add(numFontSize);
            Controls.Add(lblFontSize);
            Controls.Add(btnSelectColor);
            Controls.Add(btnSelectFont);
            Margin = new Padding(4, 5, 4, 5);
            Name = "BookLabel";
            Text = "Excel到Word书签处理工具";
            ((System.ComponentModel.ISupportInitialize)numFontSize).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectExcel;
        private System.Windows.Forms.Button btnSelectTemplate;
        private System.Windows.Forms.Button btnSelectOutput;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Label lblExcelPath;
        private System.Windows.Forms.Label lblTemplatePath;
        private System.Windows.Forms.Label lblOutputPath;
        private System.Windows.Forms.Button btnSelectFont;
        private System.Windows.Forms.Button btnSelectColor;
        private System.Windows.Forms.Label lblFontSettings;
        private System.Windows.Forms.Label lblCurrentFont;
        private System.Windows.Forms.Label lblCurrentColor;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.NumericUpDown numFontSize;
        private System.Windows.Forms.Label lblFontSize;
    }
}