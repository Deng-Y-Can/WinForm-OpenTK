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

        public ColorRangeDialog()
        {
            InitializeComponent(); // 调用设计器的初始化方法
        }

        // 颜色选择按钮点击事件
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

        // 确定按钮点击事件（验证输入并保存）
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