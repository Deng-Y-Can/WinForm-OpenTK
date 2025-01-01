using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp.CandyTool
{
    public class CandyFile
    {
        public static string ChooseFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // 可以设置文件对话框的一些初始属性，比如筛选文件类型等
            openFileDialog.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            openFileDialog.Title = "选择一个文件";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }

            return null;
        }
        public static string ChooseImageFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // 设置文件筛选器，只允许选择常见的图片格式文件
            openFileDialog.Filter = "图片文件(*.jpg;*.jpeg;*.png;*.gif;*.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog.Title = "选择一个图片文件";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }

            return null;
        }
        public static string ChooseVideoFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // 设置文件筛选器，限定为常见的视频文件格式
            openFileDialog.Filter = "视频文件(*.mp4;*.avi;*.mkv;*.mov;*.wmv)|*.mp4;*.avi;*.mkv;*.mov;*.wmv";
            openFileDialog.Title = "选择一个视频文件";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }

            return null;
        }
    }
}
