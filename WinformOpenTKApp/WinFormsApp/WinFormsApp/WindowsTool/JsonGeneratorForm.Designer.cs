namespace WinFormsApp.WindowsTool
{
    partial class JsonGeneratorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
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
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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

        #endregion

        private System.Windows.Forms.Button btnSelectInputPath;
        private System.Windows.Forms.Button btnSelectOutputPath;
        private System.Windows.Forms.Button btnGenerateJson;
        private System.Windows.Forms.Label lblInputPath;
        private System.Windows.Forms.Label lblOutputPath;
    }
}