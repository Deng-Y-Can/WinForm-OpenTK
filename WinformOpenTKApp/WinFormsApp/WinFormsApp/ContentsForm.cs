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
using WinFormsApp.OpenCV.OpenCvSharp;

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
           
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text == "EmguCV测试")
            {
                OpenCVTestForm form2 = new OpenCVTestForm();
                form2.Show();
            }
            else if (e.Node.Text == "图片加载1")
            {
                ImageProcessingForm form3 = new ImageProcessingForm();
                form3.Show();
            }
            else if (e.Node.Text == "图片加载2")
            {
                OpenCvSharpFourForm form3 = new OpenCvSharpFourForm();
                form3.Show();
            }
            else if (e.Node.Text == "摄像头")
            {
                VideoForm form3 = new VideoForm();
                form3.Show();
            }
            else if (e.Node.Text == "函数生成器")
            {
                FuctionForm form3 = new FuctionForm();
                form3.Show();
            }
            else if (e.Node.Text == "OpenTK测试")
            {
                MainForm form3 = new MainForm();
                form3.Show();
            }
        }
    }
}
