using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp.MyOpenCV.EmguCV
{
    public partial class TextInputForm : Form
    {
        public string InputText { get; private set; }
        public Color TextColor { get; private set; }
        public Font TextFont { get; private set; }

        private ColorDialog colorDialog;
        private FontDialog fontDialog;

        public TextInputForm()
        {
            InitializeComponent();
            TextColor = Color.Black;
            TextFont = new Font("Arial", 12);
            fontPreviewLabel.Font = TextFont;
            fontPreviewLabel.ForeColor = TextColor;
            colorPreviewPanel.BackColor = TextColor;
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