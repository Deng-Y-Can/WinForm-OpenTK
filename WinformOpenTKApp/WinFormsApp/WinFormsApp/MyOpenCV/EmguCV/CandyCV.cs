using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
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
using static WinFormsApp.MyOpenCV.DLL.CandyEgCVTool;

namespace WinFormsApp.MyOpenCV.EmguCV
{
    public partial class CandyCV : Form
    {

        public CandyCV()
        {
            InitializeComponent();
        }
        private string picture1Path=$@"E:\Candy\资源库（图标、css）\图片\ml.jpg";
        private string picture2Path=$@"E:\Candy\资源库（图标、css）\图片\hb.jpg";
        private string vedioPath;
        private CandyEgCVTool candyEgCVTool;
        private MCvScalar mCvScalar = new MCvScalar(0, 255, 255, 0);

        private void LoadPicture1_Click(object sender, EventArgs e)
        {
            string path = CandyFile.ChooseImageFile();
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            picture1Path = path;
        }
        private void LoadPicture2_Click(object sender, EventArgs e)
        {
            string path = CandyFile.ChooseImageFile();
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            picture2Path = path;
        }
        private void LoadVideo1_Click(object sender, EventArgs e)
        {
            string path = CandyFile.ChooseVideoFile();
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            vedioPath = path;
        }

        private void PlayPicture_Click(object sender, EventArgs e)
        {
            string path = CandyFile.ChooseImageFile();
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            CandyEgCVTool.ShowImage(path);
        }
        private void PlayVideo_Click(object sender, EventArgs e)
        {
            string path = CandyFile.ChooseVideoFile();
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            CandyEgCVTool.PlayVideo(path);
        }
        private void Line_Click(object sender, EventArgs e)
        {
            Point pointStart = new Point(0, 0);
            Point pointEnd = new Point(1, 1);
            CandyEgCVTool.Line(pointStart, pointEnd, mCvScalar);
        }
        private void Rectangle_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.Rectangle(mCvScalar);
        }
        private void Circle_Click(object sender, EventArgs e)
        {
            Point center = new Point(0, 0);
            CandyEgCVTool.Circle(center, mCvScalar);
        }
        private void Ellipse_Click(object sender, EventArgs e)
        {
            Point center = new Point(0, 0);
            CandyEgCVTool.Ellipse(center, mCvScalar);
        }
        private void Polygon_Click(object sender, EventArgs e)
        {
            Point[] points = new Point[]
            {
              new Point(50, 50),
              new Point(150, 50),
              new Point(200, 150),
              new Point(100, 200)
            };
            CandyEgCVTool.Polygon(points, mCvScalar);
        }
        private void Text_Click(object sender, EventArgs e)
        {
            string text = $@"Hello 2025!  from candy and zoe!";
            CandyEgCVTool.Text(mCvScalar, text);
        }
        private void ImageAdd_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.ImageAdd(picture1Path, picture2Path);
        }
        private void ImageAddWeighted_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.ImageAddWeighted(picture1Path, picture2Path);
        }
        private void BitwiseOperation_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.BitwiseOperation(picture1Path, picture2Path, CandyBitwiseOperation.And);
        }
        
        private void AddBorder_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.AddBorder(picture1Path, mCvScalar, 5);
        }
        private void ChangeColorSpace_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.ChangeColorSpace(picture1Path);
        }
        private void Scale_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.Scale(picture1Path);
        }
        private void ScaleBySize_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.ScaleBySize(picture1Path);
        }
        private void Translation_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.Translation(picture1Path);
        }
        private void Rotate_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.Rotate(picture1Path);
        }
         private void AffineTransformation_Click(object sender, EventArgs e)
        {
            Mat mat =  new Mat(2, 3, DepthType.Cv32F, 1);
            // 定义旋转角度（这里是逆时针旋转30度，将角度转换为弧度）
            float angle = (float)(30 * Math.PI / 180);
            float cosValue = (float)Math.Cos(angle);
            float sinValue = (float)Math.Sin(angle);
            float[] translationArray = { cosValue, -sinValue, 0,
                                           sinValue, cosValue, 0 };
            mat.SetTo(translationArray);
            CandyEgCVTool.AffineTransformation(picture1Path, mat);
        }
        private void Perspectivetransformation_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.Perspectivetransformation(picture1Path);
        }
        private void SimpleThreshold_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.SimpleThreshold(picture1Path);
        }
        private void CustomThreshold_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.CustomThreshold(picture1Path);
        }
        private void OtsuThreshold_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.OtsuThreshold(picture1Path);
        }
        private void AverageBlur_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.AverageBlur(picture1Path);
        }
        private void GaussianBlur_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.GaussianBlur(picture1Path);
        }
        private void MedianBlur_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.MedianBlur(picture1Path);
        }
        private void BilateralFilter_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.BilateralFilter(picture1Path);
        }
        private void Erosion_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.Erosion(picture1Path);
        }
        private void Dilation_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.Dilation(picture1Path);
        }
        private void Opening_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.Opening(picture1Path);
        }
        private void Closing_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.Closing(picture1Path);
        }
        private void MorphologicalGradient_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.MorphologicalGradient(picture1Path);
        }
        private void TopHat_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.TopHat(picture1Path);
        }
        private void BlackHat_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.BlackHat(picture1Path);
        }
        private void Sobel_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.Sobel(picture1Path);
        }
        private void Scharr_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.Scharr(picture1Path);
        }
        private void Laplacian_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.Laplacian(picture1Path);
        }
        private void Canny_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.Canny(picture1Path);
        }
        private void ContourDraw_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.ContourDraw(picture1Path);
        }
        private void ContourApproximation_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.ContourApproximation(picture1Path);
        }
        private void ConvexHull_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.ConvexHull(picture1Path);
        }
        private void BoundingRectangleStraight_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.BoundingRectangleStraight(picture1Path);
        }
        private void BoundingRectangleRotated_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.BoundingRectangleRotated(picture1Path);
        }
        private void MinimalEnclosingCircle_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.MinimalEnclosingCircle(picture1Path);
        }

        private void FittingEllipse_Click(object sender, EventArgs e)
        {
            CandyEgCVTool.FittingEllipse(picture1Path);
        }
        private void a3_Click(object sender, EventArgs e)
        {

        }
       
    }
}
