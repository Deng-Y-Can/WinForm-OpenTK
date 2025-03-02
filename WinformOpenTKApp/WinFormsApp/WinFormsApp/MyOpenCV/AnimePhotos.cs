using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp.MyOpenCV.DLL;

namespace WinFormsApp.MyOpenCV
{
    public partial class AnimePhotos : Form
    {
        public AnimePhotos()
        {
            InitializeComponent();
        }
        string inputImagePath = $@"C:\Users\Acer\Desktop\1.jpg";
        string outputImagePath = $@"C:\Users\Acer\Desktop\2.jpg";
        string modelPath = $@"C:\Users\Acer\Desktop\Shinkai_53.onnx";
        private void button1_Click(object sender, EventArgs e)
        {
            ImageConvert.PrintModelInputOutputNames(modelPath);
            ImageConvert.ConvertImage(inputImagePath, outputImagePath, modelPath);
        }
    }
}
