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
using WinFormsApp.Robot;
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
            TreeNode subNode1_1 = new TreeNode("EmguCV");
            TreeNode subNode1_1_1 = new TreeNode("EmguCV测试");
            TreeNode subNode1_2 = new TreeNode("OpenCvSharp");
            TreeNode subNode1_2_1 = new TreeNode("图片加载1");
            TreeNode subNode1_2_2 = new TreeNode("图片加载2");
            TreeNode subNode1_2_3 = new TreeNode("摄像头");

            TreeNode subNode2 = new TreeNode("OpenTK");
            TreeNode subNode2_1 = new TreeNode("函数生成器");
            TreeNode subNode2_2 = new TreeNode("OpenTK测试");

            TreeNode subNode3 = new TreeNode("小工具");
            TreeNode subNode3_1 = new TreeNode("书签");
            TreeNode subNode3_2 = new TreeNode("电脑小管家");
            TreeNode subNode3_3 = new TreeNode("文件分类");
            TreeNode subNode3_4 = new TreeNode("摸鱼小工具");
            TreeNode subNode3_5 = new TreeNode("截屏小工具");
            TreeNode subNode3_6 = new TreeNode("图片处理器");
            TreeNode subNode3_7 = new TreeNode("json生成器");
            TreeNode subNode3_8 = new TreeNode("txt转编码");
            TreeNode subNode3_9 = new TreeNode("文件格式转换");
            TreeNode subNode3_10 = new TreeNode("打印");
           

            TreeNode subNode4 = new TreeNode("算法");
            TreeNode subNode4_1 = new TreeNode("PID控制");
            TreeNode subNode4_2 = new TreeNode("压缩小工具");
            TreeNode subNode4_3 = new TreeNode("加密小工具");


            rootNode.Nodes.Add(subNode1);
            subNode1.Nodes.Add(subNode1_1);
            subNode1_1.Nodes.Add(subNode1_1_1);
            subNode1.Nodes.Add(subNode1_2);
            subNode1_2.Nodes.Add(subNode1_2_1);
            subNode1_2.Nodes.Add(subNode1_2_2);
            subNode1_2.Nodes.Add(subNode1_2_3);

            rootNode.Nodes.Add(subNode2);
            subNode2.Nodes.Add(subNode2_1);
            subNode2.Nodes.Add(subNode2_2);

            rootNode.Nodes.Add(subNode3);
            subNode3.Nodes.Add(subNode3_1);
            subNode3.Nodes.Add(subNode3_2);
            subNode3.Nodes.Add(subNode3_3);
            subNode3.Nodes.Add(subNode3_4);
            subNode3.Nodes.Add(subNode3_5);
            subNode3.Nodes.Add(subNode3_6);
            subNode3.Nodes.Add(subNode3_7);
            subNode3.Nodes.Add(subNode3_8);
            subNode3.Nodes.Add(subNode3_9);
            subNode3.Nodes.Add(subNode3_10);
            

            rootNode.Nodes.Add(subNode4);
            subNode4.Nodes.Add(subNode4_1);
            subNode4.Nodes.Add(subNode4_2);
            subNode4.Nodes.Add(subNode4_3);

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
                    CandyImage2 form12 = new CandyImage2();
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
                case "PID控制":
                    PIDVisualizationForm form141 = new PIDVisualizationForm();
                    form141.Show();
                    break;
                case "打印":
                    Print form142 = new Print();
                    form142.Show();
                    break;
                case "加密小工具":
                    EncryptionTool form143 = new EncryptionTool();
                    form143.Show();
                    break;
                case "压缩小工具":
                    CompressionTool form144 = new CompressionTool();
                    form144.Show();
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
