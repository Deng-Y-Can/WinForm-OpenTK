using WinFormsApp.MyOpenCV;
using WinFormsApp.MyOpenCV.EmguCV;
using WinFormsApp.MyOpenTK;
using WinFormsApp.OpenCV.OpenCvSharp;
using WinFormsApp.Robot;

namespace WinFormsApp
{
    internal static class Program
    {
        /// <summary>
        ///  ���������
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new AnimePhotos2());
        }
    }
}