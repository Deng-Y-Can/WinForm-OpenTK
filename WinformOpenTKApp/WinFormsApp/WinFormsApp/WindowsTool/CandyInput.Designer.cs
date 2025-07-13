namespace WinFormsApp.WindowsTool
{
    partial class CandyInput
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
            this.components = new System.ComponentModel.Container();
            this.titleLabel = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.safetyLabel = new System.Windows.Forms.Label();
            this.toggleButton = new System.Windows.Forms.Button();
            this.modeComboBox = new System.Windows.Forms.ComboBox();
            this.pinyinLabel = new System.Windows.Forms.Label();
            this.candidateLabel = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.safetyTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.titleLabel.Location = new System.Drawing.Point(120, 15);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(160, 30);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "自定义输入法控制台";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusLabel
            // 
            this.statusLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.statusLabel.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.statusLabel.Location = new System.Drawing.Point(120, 50);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(160, 20);
            this.statusLabel.TabIndex = 1;
            this.statusLabel.Text = "状态: 禁用 | 英文小写";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // safetyLabel
            // 
            this.safetyLabel.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.safetyLabel.ForeColor = System.Drawing.Color.Green;
            this.safetyLabel.Location = new System.Drawing.Point(120, 70);
            this.safetyLabel.Name = "safetyLabel";
            this.safetyLabel.Size = new System.Drawing.Size(160, 15);
            this.safetyLabel.TabIndex = 2;
            this.safetyLabel.Text = "安全状态: 正常";
            this.safetyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // toggleButton
            // 
            this.toggleButton.BackColor = System.Drawing.Color.FromArgb(66, 133, 244);
            this.toggleButton.FlatAppearance.BorderSize = 0;
            this.toggleButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.toggleButton.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.toggleButton.ForeColor = System.Drawing.Color.White;
            this.toggleButton.Location = new System.Drawing.Point(150, 85);
            this.toggleButton.Name = "toggleButton";
            this.toggleButton.Size = new System.Drawing.Size(100, 35);
            this.toggleButton.TabIndex = 3;
            this.toggleButton.Text = "启用输入法";
            this.toggleButton.UseVisualStyleBackColor = false;
            this.toggleButton.Click += new System.EventHandler(this.ToggleButton_Click);
            // 
            // modeComboBox
            // 
            this.modeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modeComboBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.modeComboBox.Location = new System.Drawing.Point(120, 135);
            this.modeComboBox.Name = "modeComboBox";
            this.modeComboBox.Size = new System.Drawing.Size(160, 23);
            this.modeComboBox.TabIndex = 4;
            this.modeComboBox.SelectedIndexChanged += new System.EventHandler(this.ModeComboBox_SelectedIndexChanged);
            // 
            // pinyinLabel
            // 
            this.pinyinLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pinyinLabel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.pinyinLabel.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.pinyinLabel.Location = new System.Drawing.Point(50, 180);
            this.pinyinLabel.Name = "pinyinLabel";
            this.pinyinLabel.Size = new System.Drawing.Size(300, 25);
            this.pinyinLabel.TabIndex = 5;
            this.pinyinLabel.Text = "拼音: ";
            // 
            // candidateLabel
            // 
            this.candidateLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.candidateLabel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.candidateLabel.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            this.candidateLabel.Location = new System.Drawing.Point(50, 210);
            this.candidateLabel.Name = "candidateLabel";
            this.candidateLabel.Size = new System.Drawing.Size(300, 25);
            this.candidateLabel.TabIndex = 6;
            this.candidateLabel.Text = "候选: ";
            // 
            // errorLabel
            // 
            this.errorLabel.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.errorLabel.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.errorLabel.Location = new System.Drawing.Point(50, 240);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(300, 20);
            this.errorLabel.TabIndex = 7;
            this.errorLabel.Text = "注意: 按Ctrl+Shift+X可强制禁用输入法";
            this.errorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // safetyTimer
            // 
            this.safetyTimer.Interval = 1000;
            this.safetyTimer.Tick += new System.EventHandler(this.SafetyTimer_Tick);
            // 
            // CandyInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.ClientSize = new System.Drawing.Size(400, 310);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.candidateLabel);
            this.Controls.Add(this.pinyinLabel);
            this.Controls.Add(this.modeComboBox);
            this.Controls.Add(this.toggleButton);
            this.Controls.Add(this.safetyLabel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.titleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "CandyInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自定义输入法";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CandyInput_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label safetyLabel;
        private System.Windows.Forms.Button toggleButton;
        private System.Windows.Forms.ComboBox modeComboBox;
        private System.Windows.Forms.Label pinyinLabel;
        private System.Windows.Forms.Label candidateLabel;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Timer safetyTimer;
    }
}