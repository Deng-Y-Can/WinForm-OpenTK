using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp.MyOpenCV.EmguCV
{
    public partial class ColorRangeDialog : Form
    {
        // 原始颜色范围
        public int MinR { get; private set; }
        public int MaxR { get; private set; }
        public int MinG { get; private set; }
        public int MaxG { get; private set; }
        public int MinB { get; private set; }
        public int MaxB { get; private set; }

        // 目标颜色
        public Color TargetColor { get; private set; } = Color.White;

        private TextBox txtMinR, txtMaxR, txtMinG, txtMaxG, txtMinB, txtMaxB;
        private Button okButton, cancelButton;
        private Button colorButton;
        private Panel colorPreviewPanel;

        public ColorRangeDialog()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "RGB范围颜色替换";
            this.Size = new Size(350, 280);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // 原始颜色范围标签
            Label lblOriginal = new Label
            {
                Text = "原始颜色范围 (RGB):",
                Location = new Point(12, 12),
                Size = new Size(120, 20)
            };
            this.Controls.Add(lblOriginal);

            // R范围
            Label lblR = new Label
            {
                Text = "R:",
                Location = new Point(12, 40),
                Size = new Size(20, 20)
            };
            this.Controls.Add(lblR);

            txtMinR = new TextBox
            {
                Text = "0",
                Location = new Point(32, 40),
                Size = new Size(40, 25),
                TextAlign = HorizontalAlignment.Center
            };
            this.Controls.Add(txtMinR);

            Label lblTo = new Label
            {
                Text = "到",
                Location = new Point(78, 40),
                Size = new Size(20, 20)
            };
            this.Controls.Add(lblTo);

            txtMaxR = new TextBox
            {
                Text = "255",
                Location = new Point(100, 40),
                Size = new Size(40, 25),
                TextAlign = HorizontalAlignment.Center
            };
            this.Controls.Add(txtMaxR);

            // G范围
            Label lblG = new Label
            {
                Text = "G:",
                Location = new Point(12, 70),
                Size = new Size(20, 20)
            };
            this.Controls.Add(lblG);

            txtMinG = new TextBox
            {
                Text = "0",
                Location = new Point(32, 70),
                Size = new Size(40, 25),
                TextAlign = HorizontalAlignment.Center
            };
            this.Controls.Add(txtMinG);

            txtMaxG = new TextBox
            {
                Text = "255",
                Location = new Point(100, 70),
                Size = new Size(40, 25),
                TextAlign = HorizontalAlignment.Center
            };
            this.Controls.Add(txtMaxG);

            // B范围
            Label lblB = new Label
            {
                Text = "B:",
                Location = new Point(12, 100),
                Size = new Size(20, 20)
            };
            this.Controls.Add(lblB);

            txtMinB = new TextBox
            {
                Text = "0",
                Location = new Point(32, 100),
                Size = new Size(40, 25),
                TextAlign = HorizontalAlignment.Center
            };
            this.Controls.Add(txtMinB);

            txtMaxB = new TextBox
            {
                Text = "255",
                Location = new Point(100, 100),
                Size = new Size(40, 25),
                TextAlign = HorizontalAlignment.Center
            };
            this.Controls.Add(txtMaxB);

            // 目标颜色标签
            Label lblTarget = new Label
            {
                Text = "目标颜色:",
                Location = new Point(12, 140),
                Size = new Size(80, 20)
            };
            this.Controls.Add(lblTarget);

            // 目标颜色选择按钮
            colorButton = new Button
            {
                Text = "选择颜色",
                Location = new Point(100, 140),
                Size = new Size(90, 25)
            };
            colorButton.Click += ColorButton_Click;
            this.Controls.Add(colorButton);

            // 颜色预览面板
            colorPreviewPanel = new Panel
            {
                Location = new Point(200, 140),
                Size = new Size(30, 25),
                BackColor = TargetColor,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(colorPreviewPanel);

            // 确定按钮
            okButton = new Button
            {
                Text = "确定",
                Location = new Point(150, 210),
                Size = new Size(75, 25)
            };
            okButton.DialogResult = DialogResult.OK;
            okButton.Click += OkButton_Click;
            this.Controls.Add(okButton);

            // 取消按钮
            cancelButton = new Button
            {
                Text = "取消",
                Location = new Point(230, 210),
                Size = new Size(75, 25)
            };
            cancelButton.DialogResult = DialogResult.Cancel;
            this.Controls.Add(cancelButton);
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = TargetColor;

                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    TargetColor = colorDialog.Color;
                    colorPreviewPanel.BackColor = TargetColor;
                }
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            // 验证输入
            if (!int.TryParse(txtMinR.Text, out int minR) || minR < 0 || minR > 255 ||
                !int.TryParse(txtMaxR.Text, out int maxR) || maxR < 0 || maxR > 255 || maxR < minR ||
                !int.TryParse(txtMinG.Text, out int minG) || minG < 0 || minG > 255 ||
                !int.TryParse(txtMaxG.Text, out int maxG) || maxG < 0 || maxG > 255 || maxG < minG ||
                !int.TryParse(txtMinB.Text, out int minB) || minB < 0 || minB > 255 ||
                !int.TryParse(txtMaxB.Text, out int maxB) || maxB < 0 || maxB > 255 || maxB < minB)
            {
                MessageBox.Show("请输入有效的RGB范围(0-255)，且最大值不小于最小值", "输入错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 保存输入值
            MinR = minR;
            MaxR = maxR;
            MinG = minG;
            MaxG = maxG;
            MinB = minB;
            MaxB = maxB;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}