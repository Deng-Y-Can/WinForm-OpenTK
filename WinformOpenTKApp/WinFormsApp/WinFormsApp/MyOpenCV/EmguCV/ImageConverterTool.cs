using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp.CandyTool;
using WinFormsApp.MyOpenCV.DLL;

namespace WinFormsApp.MyOpenCV.EmguCV
{
    public partial class ImageConverterTool : Form
    {
        public ImageConverterTool()
        {
            InitializeComponent();
            // 将PictureType中的每个值添加到comboBox1
            foreach (PictureType type in Enum.GetValues(typeof(PictureType)))
            {
                comboBox1.Items.Add(type);
            }
        }

        public List<string> pictureLIst = new List<string>();
        public string picture1Path;

        private void button1_Click(object sender, EventArgs e)
        {
            string path = CandyFile.ChooseImageFile();
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            picture1Path = path;
            ShowImage(picture1Path);
        }

        public void ShowImage(string picture1_Path)
        {
            Image image = Image.FromFile(picture1_Path);
            // 创建PictureBox控件来承载图片
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = image;
            // 设置PictureBox的尺寸模式，使其适应Panel大小或者按合适的方式显示
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            // 将PictureBox添加到Panel中
            panel1.Controls.Add(pictureBox);
            // 设置PictureBox在Panel中的位置和大小，通常可以铺满整个Panel
            pictureBox.Dock = DockStyle.Fill;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                PictureType pictureType = (PictureType)comboBox1.SelectedIndex;
                CandyImageConverter.ConvertImage(picture1Path, $@"out/{Path.GetFileNameWithoutExtension(picture1Path)}.{pictureType.ToString().ToLower()}", int.Parse(textBox1.Text), int.Parse(textBox2.Text));
                MessageBox.Show("已成功转换文件，保存在out文件夹内！");
            }
            catch (Exception ex) 
            
            {
                MessageBox.Show("暂不支持此类文件导出!");
            }
            
        }
    }
}
