using WinFormsApp.MyOpenTK;
using WinFormsApp.OpenCV.OpenCvSharp;

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
            Application.Run(new FuctionForm());
        }
    }
}