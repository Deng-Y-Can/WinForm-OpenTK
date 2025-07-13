namespace WinFormsApp.MyOpenCV.EmguCV
{
    partial class ColorRangeDialog
    {
        /// <summary>
        /// 必需的设计器变量
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false</param>
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
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblOriginal = new System.Windows.Forms.Label();
            this.lblR = new System.Windows.Forms.Label();
            this.txtMinR = new System.Windows.Forms.TextBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.txtMaxR = new System.Windows.Forms.TextBox();
            this.lblG = new System.Windows.Forms.Label();
            this.txtMinG = new System.Windows.Forms.TextBox();
            this.txtMaxG = new System.Windows.Forms.TextBox();
            this.lblB = new System.Windows.Forms.Label();
            this.txtMinB = new System.Windows.Forms.TextBox();
            this.txtMaxB = new System.Windows.Forms.TextBox();
            this.lblTarget = new System.Windows.Forms.Label();
            this.colorButton = new System.Windows.Forms.Button();
            this.colorPreviewPanel = new System.Windows.Forms.Panel();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblOriginal
            // 
            this.lblOriginal.Location = new System.Drawing.Point(12, 12);
            this.lblOriginal.Name = "lblOriginal";
            this.lblOriginal.Size = new System.Drawing.Size(120, 20);
            this.lblOriginal.TabIndex = 0;
            this.lblOriginal.Text = "原始颜色范围 (RGB):";
            // 
            // lblR
            // 
            this.lblR.Location = new System.Drawing.Point(12, 40);
            this.lblR.Name = "lblR";
            this.lblR.Size = new System.Drawing.Size(20, 20);
            this.lblR.TabIndex = 1;
            this.lblR.Text = "R:";
            // 
            // txtMinR
            // 
            this.txtMinR.Location = new System.Drawing.Point(32, 40);
            this.txtMinR.Name = "txtMinR";
            this.txtMinR.Size = new System.Drawing.Size(40, 25);
            this.txtMinR.TabIndex = 2;
            this.txtMinR.Text = "0";
            this.txtMinR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblTo
            // 
            this.lblTo.Location = new System.Drawing.Point(78, 40);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(20, 20);
            this.lblTo.TabIndex = 3;
            this.lblTo.Text = "到";
            // 
            // txtMaxR
            // 
            this.txtMaxR.Location = new System.Drawing.Point(100, 40);
            this.txtMaxR.Name = "txtMaxR";
            this.txtMaxR.Size = new System.Drawing.Size(40, 25);
            this.txtMaxR.TabIndex = 4;
            this.txtMaxR.Text = "255";
            this.txtMaxR.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblG
            // 
            this.lblG.Location = new System.Drawing.Point(12, 70);
            this.lblG.Name = "lblG";
            this.lblG.Size = new System.Drawing.Size(20, 20);
            this.lblG.TabIndex = 5;
            this.lblG.Text = "G:";
            // 
            // txtMinG
            // 
            this.txtMinG.Location = new System.Drawing.Point(32, 70);
            this.txtMinG.Name = "txtMinG";
            this.txtMinG.Size = new System.Drawing.Size(40, 25);
            this.txtMinG.TabIndex = 6;
            this.txtMinG.Text = "0";
            this.txtMinG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMaxG
            // 
            this.txtMaxG.Location = new System.Drawing.Point(100, 70);
            this.txtMaxG.Name = "txtMaxG";
            this.txtMaxG.Size = new System.Drawing.Size(40, 25);
            this.txtMaxG.TabIndex = 7;
            this.txtMaxG.Text = "255";
            this.txtMaxG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblB
            // 
            this.lblB.Location = new System.Drawing.Point(12, 100);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(20, 20);
            this.lblB.TabIndex = 8;
            this.lblB.Text = "B:";
            // 
            // txtMinB
            // 
            this.txtMinB.Location = new System.Drawing.Point(32, 100);
            this.txtMinB.Name = "txtMinB";
            this.txtMinB.Size = new System.Drawing.Size(40, 25);
            this.txtMinB.TabIndex = 9;
            this.txtMinB.Text = "0";
            this.txtMinB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtMaxB
            // 
            this.txtMaxB.Location = new System.Drawing.Point(100, 100);
            this.txtMaxB.Name = "txtMaxB";
            this.txtMaxB.Size = new System.Drawing.Size(40, 25);
            this.txtMaxB.TabIndex = 10;
            this.txtMaxB.Text = "255";
            this.txtMaxB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblTarget
            // 
            this.lblTarget.Location = new System.Drawing.Point(12, 140);
            this.lblTarget.Name = "lblTarget";
            this.lblTarget.Size = new System.Drawing.Size(80, 20);
            this.lblTarget.TabIndex = 11;
            this.lblTarget.Text = "目标颜色:";
            // 
            // colorButton
            // 
            this.colorButton.Location = new System.Drawing.Point(100, 140);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(90, 25);
            this.colorButton.TabIndex = 12;
            this.colorButton.Text = "选择颜色";
            this.colorButton.UseVisualStyleBackColor = true;
            this.colorButton.Click += new System.EventHandler(this.ColorButton_Click);
            // 
            // colorPreviewPanel
            // 
            this.colorPreviewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorPreviewPanel.Location = new System.Drawing.Point(200, 140);
            this.colorPreviewPanel.Name = "colorPreviewPanel";
            this.colorPreviewPanel.Size = new System.Drawing.Size(30, 25);
            this.colorPreviewPanel.TabIndex = 13;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(150, 210);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 25);
            this.okButton.TabIndex = 14;
            this.okButton.Text = "确定";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(230, 210);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 25);
            this.cancelButton.TabIndex = 15;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // ColorRangeDialog
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 280);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.colorPreviewPanel);
            this.Controls.Add(this.colorButton);
            this.Controls.Add(this.lblTarget);
            this.Controls.Add(this.txtMaxB);
            this.Controls.Add(this.txtMinB);
            this.Controls.Add(this.lblB);
            this.Controls.Add(this.txtMaxG);
            this.Controls.Add(this.txtMinG);
            this.Controls.Add(this.lblG);
            this.Controls.Add(this.txtMaxR);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.txtMinR);
            this.Controls.Add(this.lblR);
            this.Controls.Add(this.lblOriginal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ColorRangeDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RGB范围颜色替换";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblOriginal;
        private System.Windows.Forms.Label lblR;
        private System.Windows.Forms.TextBox txtMinR;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.TextBox txtMaxR;
        private System.Windows.Forms.Label lblG;
        private System.Windows.Forms.TextBox txtMinG;
        private System.Windows.Forms.TextBox txtMaxG;
        private System.Windows.Forms.Label lblB;
        private System.Windows.Forms.TextBox txtMinB;
        private System.Windows.Forms.TextBox txtMaxB;
        private System.Windows.Forms.Label lblTarget;
        private System.Windows.Forms.Button colorButton;
        private System.Windows.Forms.Panel colorPreviewPanel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}