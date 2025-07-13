using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp.MyOpenCV.EmguCV;
using WinFormsApp.OpenCV.OpenCvSharp;
using WinFormsApp.WindowsTool;

namespace WinFormsApp
{
    public partial class ContentsForm : Form
    {
        public ContentsForm()
        {
            InitializeComponent();
            TreeNode();
        }

        public void TreeNode()
        {
            // 创建根节点
            TreeNode rootNode = new TreeNode("目录");
            treeView1.Nodes.Add(rootNode);
            // 创建子节点
            TreeNode subNode1 = new TreeNode("OpenCV");
            TreeNode subNode11 = new TreeNode("EmguCV");
            TreeNode subNode111 = new TreeNode("EmguCV测试");
            TreeNode subNode12 = new TreeNode("OpenCvSharp");
            TreeNode subNode121 = new TreeNode("图片加载1");
            TreeNode subNode122 = new TreeNode("图片加载2");
            TreeNode subNode123 = new TreeNode("摄像头");

            TreeNode subNode2 = new TreeNode("OpenTK");
            TreeNode subNode21 = new TreeNode("函数生成器");
            TreeNode subNode22 = new TreeNode("OpenTK测试");

            TreeNode subNode3 = new TreeNode("小工具");
            TreeNode subNode31 = new TreeNode("书签");
            TreeNode subNode32 = new TreeNode("电脑小管家");
            TreeNode subNode33 = new TreeNode("文件分类");
            TreeNode subNode34 = new TreeNode("摸鱼小工具");
            TreeNode subNode35 = new TreeNode("截屏小工具");
            TreeNode subNode36 = new TreeNode("图片处理器");
            TreeNode subNode37 = new TreeNode("json生成器");
            TreeNode subNode38 = new TreeNode("txt转编码");
            TreeNode subNode39 = new TreeNode("文件格式转换");


            rootNode.Nodes.Add(subNode1);
            subNode1.Nodes.Add(subNode11);
            subNode11.Nodes.Add(subNode111);
            subNode1.Nodes.Add(subNode12);
            subNode12.Nodes.Add(subNode121);
            subNode12.Nodes.Add(subNode122);
            subNode12.Nodes.Add(subNode123);

            rootNode.Nodes.Add(subNode2);
            subNode2.Nodes.Add(subNode21);
            subNode2.Nodes.Add(subNode22);

            rootNode.Nodes.Add(subNode3);
            subNode3.Nodes.Add(subNode31);
            subNode3.Nodes.Add(subNode32);
            subNode3.Nodes.Add(subNode33);
            subNode3.Nodes.Add(subNode34);
            subNode3.Nodes.Add(subNode35);
            subNode3.Nodes.Add(subNode36);
            subNode3.Nodes.Add(subNode37);
            subNode3.Nodes.Add(subNode38);
            subNode3.Nodes.Add(subNode39);

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.Text)
            {
                case "EmguCV测试":
                    OpenCVTestForm form1 = new OpenCVTestForm();
                    form1.Show();
                    break;

                case "图片加载1":
                    ImageProcessingForm form2 = new ImageProcessingForm();
                    form2.Show();
                    break;

                case "图片加载2":
                    OpenCvSharpFourForm form3 = new OpenCvSharpFourForm();
                    form3.Show();
                    break;

                case "摄像头":
                    VideoForm form4 = new VideoForm();
                    form4.Show();
                    break;

                case "函数生成器":
                    FuctionForm form5 = new FuctionForm();
                    form5.Show();
                    break;

                case "OpenTK测试":
                    MainForm form6 = new MainForm();
                    form6.Show();
                    break;

                case "书签":
                    BookLabel form7 = new BookLabel();
                    form7.Show();
                    break;

                case "电脑小管家":
                    ComputerButler form8 = new ComputerButler();
                    form8.Show();
                    break;

                case "文件分类":
                    FileSort form9 = new FileSort();
                    form9.Show();
                    break;

                case "摸鱼小工具":
                    Fish form10 = new Fish();
                    form10.Show();
                    break;

                case "截屏小工具":
                    KeepCatch form11 = new KeepCatch();
                    form11.Show();
                    break;

                case "图片处理器":
                    CandyImage form12 = new CandyImage();
                    form12.Show();
                    break;

                case "json生成器":
                    JsonGeneratorForm form13 = new JsonGeneratorForm();
                    form13.Show();
                    break;

                case "txt转编码":
                    TxtEncoding form14 = new TxtEncoding();
                    form14.Show();
                    break;

                case "文件格式转换":
                    DocumentFormatConverter form15 = new DocumentFormatConverter();
                    form15.Show();
                    break;
            }

            //if (e.Node.Text == "EmguCV测试")
            //{
            //    OpenCVTestForm form2 = new OpenCVTestForm();
            //    form2.Show();
            //}
            //else if (e.Node.Text == "图片加载1")
            //{
            //    ImageProcessingForm form3 = new ImageProcessingForm();
            //    form3.Show();
            //}
            //else if (e.Node.Text == "图片加载2")
            //{
            //    OpenCvSharpFourForm form3 = new OpenCvSharpFourForm();
            //    form3.Show();
            //}
            //else if (e.Node.Text == "摄像头")
            //{
            //    VideoForm form3 = new VideoForm();
            //    form3.Show();
            //}
            //else if (e.Node.Text == "函数生成器")
            //{
            //    FuctionForm form3 = new FuctionForm();
            //    form3.Show();
            //}
            //else if (e.Node.Text == "OpenTK测试")
            //{
            //    MainForm form3 = new MainForm();
            //    form3.Show();
            //}

            //else if (e.Node.Text == "书签")
            //{
            //    BookLabel form3 = new BookLabel();
            //    form3.Show();
            //}
            //else if (e.Node.Text == "电脑小管家")
            //{
            //    ComputerButler form3 = new ComputerButler();
            //    form3.Show();
            //}
            //else if (e.Node.Text == "文件分类")
            //{
            //    FileSort form3 = new FileSort();
            //    form3.Show();
            //}
            //else if (e.Node.Text == "摸鱼小工具")
            //{
            //    Fish form3 = new Fish();
            //    form3.Show();
            //}
            //else if (e.Node.Text == "截屏小工具")
            //{
            //    KeepCatch form3 = new KeepCatch();
            //    form3.Show();
            //}
            //else if (e.Node.Text == "图片处理器")
            //{
            //    CandyImage form3 = new CandyImage();
            //    form3.Show();
            //}
            //else if (e.Node.Text == "json生成器")
            //{
            //    JsonGeneratorForm form3 = new JsonGeneratorForm();
            //    form3.Show();
            //}
            //else if (e.Node.Text == "txt转编码")
            //{
            //    TxtEncoding form3 = new TxtEncoding();
            //    form3.Show();
            //}
            //else if (e.Node.Text == "文件格式转换")
            //{
            //    DocumentFormatConverter form3 = new DocumentFormatConverter();
            //    form3.Show();
            //}
        }
    }
}
