
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using Emgu.CV.CvEnum;
using OpenCvSharp;
using CvInvoke = Emgu.CV.CvInvoke;
using Emgu.CV.CvEnum;
using ColorConversion = Emgu.CV.CvEnum.ColorConversion;

namespace WinFormsApp
{

    public partial class OpenCVTestForm : Form
    {
        public OpenCVTestForm()
        {
            InitializeComponent();
        }
        private string imagePath = "E:\\home\\github\\WinformOpenTK\\WinForm-OpenTK\\WinformOpenTKApp\\WinFormsApp\\WinFormsApp\\Picture\\jlt.jpg";

        private void button1_Click(object sender, EventArgs e)
        {
            Image<Bgr, byte> image = new Image<Bgr, byte>(imagePath);

            //CvInvoke.Imshow("Image", image);
            //// 将彩色图像转换为灰度图像
            //CvInvoke.CvtColor(image, image, ColorConversion.Bgr2Rgba);
            //Mat matrix = new Mat();
            //Mat matrixFromImage = image.Mat;
            //CvInvoke.WaitKey(0);


            Image<Hsv, byte> hsvImage = image.Convert<Hsv, byte>();

            CvInvoke.Imshow("HSV Image", hsvImage);
            CvInvoke.WaitKey(0);



        }

        private void button2_Click(object sender, EventArgs e)
        {
            CascadeClassifier cascade = new CascadeClassifier("haarcascade_frontalface_default.xml");
            Image<Bgr, byte> image = new Image<Bgr, byte>("group_photo.jpg");
            Image<Gray, byte> grayImage = image.Convert<Gray, byte>();

            Rectangle[] faces = cascade.DetectMultiScale(grayImage, 1.3, 5);

            foreach (Rectangle face in faces)
            {
                image.Draw(face, new Bgr(Color.Red), 2);
            }

            CvInvoke.Imshow("Face Detection", image);
            CvInvoke.WaitKey(0);

        }
    }
}
