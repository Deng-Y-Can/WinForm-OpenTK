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
using WinFormsApp.MyOpenCV.DLL;

namespace WinFormsApp.MyOpenCV.EmguCV
{
    public partial class CandyCV : Form
    {

        public CandyCV()
        {
            InitializeComponent();
        }
        private string picturePath;
        private string vedioPath;
        private CandyEgCVTool candyEgCVTool;
        private MCvScalar mCvScalar = new MCvScalar(0, 255, 255, 0);


        private void Line_Click(object sender, EventArgs e)
        {
            Point pointStart = new Point(0,0);
            Point pointEnd = new Point(1,1);
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
            Point[]  points = new Point[]
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

        }
        private void ImageAddWeighted_Click(object sender, EventArgs e)
        {

        }
        private void BitwiseOperation_Click(object sender, EventArgs e)
        {

        }
        private void a_Click(object sender, EventArgs e)
        {
            
        }
    }
}
