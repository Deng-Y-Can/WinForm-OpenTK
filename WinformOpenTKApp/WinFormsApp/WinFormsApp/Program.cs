using WinFormsApp.MyOpenCV;
using WinFormsApp.MyOpenCV.EmguCV;
using WinFormsApp.MyOpenTK;
using WinFormsApp.OpenCV.OpenCvSharp;
using WinFormsApp.Robot;
using WinFormsApp.WindowsTool;

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
            Application.Run(new BookLabel());
        }
    }
}