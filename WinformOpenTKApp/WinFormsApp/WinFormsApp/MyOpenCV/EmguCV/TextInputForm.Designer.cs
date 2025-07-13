using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp.MyOpenCV.EmguCV
{
    partial class TextInputForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private TextBox textBox;
        private Button colorButton;
        private Button fontButton;
        private Button okButton;
        private Button cancelButton;
        private Panel colorPreviewPanel;
        private Label fontPreviewLabel;

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
            this.components = new System.ComponentModel.Container();
            this.textBox = new System.Windows.Forms.TextBox();
            this.colorButton = new System.Windows.Forms.Button();
            this.fontButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.colorPreviewPanel = new System.Windows.Forms.Panel();
            this.fontPreviewLabel = new System.Windows.Forms.Label();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(12, 25);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox.Size = new System.Drawing.Size(280, 60);
            this.textBox.TabIndex = 0;
            // 
            // colorButton
            // 
            this.colorButton.Location = new System.Drawing.Point(12, 95);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(90, 25);
            this.colorButton.TabIndex = 1;
            this.colorButton.Text = "选择颜色";
            this.colorButton.UseVisualStyleBackColor = true;
            this.colorButton.Click += new System.EventHandler(this.ColorButton_Click);
            // 
            // fontButton
            // 
            this.fontButton.Location = new System.Drawing.Point(144, 95);
            this.fontButton.Name = "fontButton";
            this.fontButton.Size = new System.Drawing.Size(90, 25);
            this.fontButton.TabIndex = 2;
            this.fontButton.Text = "选择字体";
            this.fontButton.UseVisualStyleBackColor = true;
            this.fontButton.Click += new System.EventHandler(this.FontButton_Click);
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(144, 145);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 25);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "确定";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(225, 145);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 25);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // colorPreviewPanel
            // 
            this.colorPreviewPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colorPreviewPanel.Location = new System.Drawing.Point(108, 95);
            this.colorPreviewPanel.Name = "colorPreviewPanel";
            this.colorPreviewPanel.Size = new System.Drawing.Size(30, 25);
            this.colorPreviewPanel.TabIndex = 5;
            // 
            // fontPreviewLabel
            // 
            this.fontPreviewLabel.BackColor = System.Drawing.SystemColors.Control;
            this.fontPreviewLabel.Location = new System.Drawing.Point(240, 95);
            this.fontPreviewLabel.Name = "fontPreviewLabel";
            this.fontPreviewLabel.Size = new System.Drawing.Size(52, 25);
            this.fontPreviewLabel.TabIndex = 6;
            this.fontPreviewLabel.Text = "字体预览";
            this.fontPreviewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TextInputForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 220);
            this.Controls.Add(this.fontPreviewLabel);
            this.Controls.Add(this.colorPreviewPanel);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.fontButton);
            this.Controls.Add(this.colorButton);
            this.Controls.Add(this.textBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TextInputForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加文字";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}