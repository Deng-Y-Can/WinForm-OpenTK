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
        string inputImagePath = "path/to/your/input/image.jpg";
        string outputImagePath = "path/to/your/output/image.jpg";
        string modelPath = "path/to/your/animeganv2.onnx";
        private void button1_Click(object sender, EventArgs e)
        {
            ImageConvert.ConvertImage(inputImagePath, outputImagePath, modelPath);
        }
    }
}
