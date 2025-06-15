using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp.MyOpenCV.EmguCV
{
    public partial class TextInputForm : Form
    {
        public string InputText { get; private set; }
        public Color TextColor { get; private set; }
        public Font TextFont { get; private set; }

        private TextBox textBox;
        private Button colorButton;
        private Button fontButton;
        private Button okButton;
        private Button cancelButton;
        private Panel colorPreviewPanel;
        private Label fontPreviewLabel;
        private ColorDialog colorDialog;
        private FontDialog fontDialog;

        public TextInputForm()
        {
            InitializeComponent();
            Initialize();
            TextColor = Color.Black;
            TextFont = new Font("Arial", 12);
        }

        private void Initialize()
        {
            this.Text = "添加文字";
            this.Size = new Size(320, 220);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // 文本输入框
            textBox = new TextBox
            {
                Location = new Point(12, 25),
                Size = new Size(280, 60),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            this.Controls.Add(textBox);

            // 颜色选择按钮
            colorButton = new Button
            {
                Text = "选择颜色",
                Location = new Point(12, 95),
                Size = new Size(90, 25)
            };
            colorButton.Click += ColorButton_Click;
            this.Controls.Add(colorButton);

            // 颜色预览面板
            colorPreviewPanel = new Panel
            {
                Location = new Point(108, 95),
                Size = new Size(30, 25),
                BackColor = TextColor,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(colorPreviewPanel);

            // 字体选择按钮
            fontButton = new Button
            {
                Text = "选择字体",
                Location = new Point(144, 95),
                Size = new Size(90, 25)
            };
            fontButton.Click += FontButton_Click;
            this.Controls.Add(fontButton);

            // 字体预览标签
            fontPreviewLabel = new Label
            {
                Text = "字体预览",
                Location = new Point(240, 95),
                Size = new Size(52, 25),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = TextFont,
                BackColor = SystemColors.Control
            };
            this.Controls.Add(fontPreviewLabel);

            // 确定按钮
            okButton = new Button
            {
                Text = "确定",
                Location = new Point(144, 145),
                Size = new Size(75, 25)
            };
            okButton.DialogResult = DialogResult.OK;
            okButton.Click += OkButton_Click;
            this.Controls.Add(okButton);

            // 取消按钮
            cancelButton = new Button
            {
                Text = "取消",
                Location = new Point(225, 145),
                Size = new Size(75, 25)
            };
            cancelButton.DialogResult = DialogResult.Cancel;
            this.Controls.Add(cancelButton);

            // 颜色对话框
            colorDialog = new ColorDialog();

            // 字体对话框
            fontDialog = new FontDialog();
            fontDialog.Font = TextFont;
            fontDialog.Color = TextColor;
        }

        private void ColorButton_Click(object sender, EventArgs e)
        {
            colorDialog.Color = TextColor;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                TextColor = colorDialog.Color;
                colorPreviewPanel.BackColor = TextColor;
                fontPreviewLabel.ForeColor = TextColor;
            }
        }

        private void FontButton_Click(object sender, EventArgs e)
        {
            fontDialog.Font = TextFont;
            fontDialog.Color = TextColor;

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                TextFont = fontDialog.Font;
                TextColor = fontDialog.Color;
                colorPreviewPanel.BackColor = TextColor;
                fontPreviewLabel.ForeColor = TextColor;
                fontPreviewLabel.Font = TextFont;
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            InputText = textBox.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}