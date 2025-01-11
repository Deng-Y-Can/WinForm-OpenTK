using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp.CandyTool;
using WinFormsApp.MyOpenCV.DLL;

namespace WinFormsApp.MyOpenCV.EmguCV
{
    public partial class VideoConverterTool : Form
    {
        public VideoConverterTool()
        {
            InitializeComponent();
            foreach (VedioType type in Enum.GetValues(typeof(VedioType)))
            {
                comboBox1.Items.Add(type);
            }
        }
        public List<string> videoLIst = new List<string>();
        public string videoPath;
        private void button1_Click(object sender, EventArgs e)
        {
            string path = CandyFile.ChooseVideoFile();
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            videoPath = path;
            ShowImage(videoPath);
        }

        public void ShowImage(string video_Path)
        {
            //Image image = Image.FromFile(video_Path);
            //// 创建PictureBox控件来承载图片
            //PictureBox pictureBox = new PictureBox();
            //pictureBox.Image = image;
            //// 设置PictureBox的尺寸模式，使其适应Panel大小或者按合适的方式显示
            //pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            //CandyEgCVTool.PlayVideo(video_Path);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                VedioType videoType = (VedioType)comboBox1.SelectedIndex;
                CandyVideoConverter.ConvertVideoFormat(videoPath, $@"out/{Path.GetFileNameWithoutExtension(videoPath)}.{videoType.ToString().ToLower()}", int.Parse(textBox1.Text), int.Parse(textBox2.Text));
                MessageBox.Show("已成功转换文件，保存在out文件夹内！");
            }
            catch (Exception ex)

            {
                MessageBox.Show("暂不支持此类文件导出!");
            }
        }
    }
}
