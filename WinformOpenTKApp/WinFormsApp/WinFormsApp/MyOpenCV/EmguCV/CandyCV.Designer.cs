namespace WinFormsApp.MyOpenCV.EmguCV
{
    partial class CandyCV
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripMenuItem();
            toolStripMenuItem6 = new ToolStripMenuItem();
            toolStripMenuItem7 = new ToolStripMenuItem();
            toolStripMenuItem8 = new ToolStripMenuItem();
            toolStripMenuItem9 = new ToolStripMenuItem();
            toolStripMenuItem10 = new ToolStripMenuItem();
            toolStripMenuItem11 = new ToolStripMenuItem();
            toolStripMenuItem16 = new ToolStripMenuItem();
            toolStripMenuItem17 = new ToolStripMenuItem();
            toolStripMenuItem18 = new ToolStripMenuItem();
            toolStripMenuItem12 = new ToolStripMenuItem();
            toolStripMenuItem13 = new ToolStripMenuItem();
            toolStripMenuItem14 = new ToolStripMenuItem();
            toolStripMenuItem15 = new ToolStripMenuItem();
            toolStripMenuItem19 = new ToolStripMenuItem();
            toolStripMenuItem20 = new ToolStripMenuItem();
            toolStripMenuItem21 = new ToolStripMenuItem();
            toolStripMenuItem22 = new ToolStripMenuItem();
            toolStripMenuItem23 = new ToolStripMenuItem();
            toolStripMenuItem24 = new ToolStripMenuItem();
            toolStripMenuItem25 = new ToolStripMenuItem();
            toolStripMenuItem26 = new ToolStripMenuItem();
            toolStripMenuItem27 = new ToolStripMenuItem();
            toolStripMenuItem28 = new ToolStripMenuItem();
            toolStripMenuItem29 = new ToolStripMenuItem();
            toolStripMenuItem30 = new ToolStripMenuItem();
            toolStripMenuItem31 = new ToolStripMenuItem();
            toolStripMenuItem32 = new ToolStripMenuItem();
            toolStripMenuItem33 = new ToolStripMenuItem();
            toolStripMenuItem34 = new ToolStripMenuItem();
            toolStripMenuItem35 = new ToolStripMenuItem();
            toolStripMenuItem36 = new ToolStripMenuItem();
            toolStripMenuItem37 = new ToolStripMenuItem();
            toolStripMenuItem38 = new ToolStripMenuItem();
            toolStripMenuItem39 = new ToolStripMenuItem();
            toolStripMenuItem40 = new ToolStripMenuItem();
            toolStripMenuItem41 = new ToolStripMenuItem();
            toolStripMenuItem42 = new ToolStripMenuItem();
            toolStripMenuItem43 = new ToolStripMenuItem();
            toolStripMenuItem44 = new ToolStripMenuItem();
            toolStripMenuItem45 = new ToolStripMenuItem();
            toolStripMenuItem46 = new ToolStripMenuItem();
            toolStripMenuItem47 = new ToolStripMenuItem();
            toolStripMenuItem48 = new ToolStripMenuItem();
            toolStripMenuItem49 = new ToolStripMenuItem();
            toolStripMenuItem50 = new ToolStripMenuItem();
            toolStripMenuItem51 = new ToolStripMenuItem();
            toolStripMenuItem52 = new ToolStripMenuItem();
            toolStripMenuItem53 = new ToolStripMenuItem();
            toolStripMenuItem54 = new ToolStripMenuItem();
            toolStripMenuItem55 = new ToolStripMenuItem();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem4, toolStripMenuItem11 });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem2, toolStripMenuItem3 });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(83, 24);
            toolStripMenuItem1.Text = "文件处理";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(152, 26);
            toolStripMenuItem2.Text = "打开图片";
            toolStripMenuItem2.Click += PlayPicture_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(152, 26);
            toolStripMenuItem3.Text = "打开视频";
            toolStripMenuItem3.Click += PlayVideo_Click;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem5, toolStripMenuItem6, toolStripMenuItem7, toolStripMenuItem8, toolStripMenuItem9, toolStripMenuItem10 });
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(53, 24);
            toolStripMenuItem4.Text = "绘图";
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new Size(137, 26);
            toolStripMenuItem5.Text = "直线";
            toolStripMenuItem5.Click += Line_Click;
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new Size(137, 26);
            toolStripMenuItem6.Text = "矩形";
            toolStripMenuItem6.Click += Rectangle_Click;
            // 
            // toolStripMenuItem7
            // 
            toolStripMenuItem7.Name = "toolStripMenuItem7";
            toolStripMenuItem7.Size = new Size(137, 26);
            toolStripMenuItem7.Text = "圆";
            toolStripMenuItem7.Click += Circle_Click;
            // 
            // toolStripMenuItem8
            // 
            toolStripMenuItem8.Name = "toolStripMenuItem8";
            toolStripMenuItem8.Size = new Size(137, 26);
            toolStripMenuItem8.Text = "椭圆";
            toolStripMenuItem8.Click += Ellipse_Click;
            // 
            // toolStripMenuItem9
            // 
            toolStripMenuItem9.Name = "toolStripMenuItem9";
            toolStripMenuItem9.Size = new Size(137, 26);
            toolStripMenuItem9.Text = "多边形";
            toolStripMenuItem9.Click += Polygon_Click;
            // 
            // toolStripMenuItem10
            // 
            toolStripMenuItem10.Name = "toolStripMenuItem10";
            toolStripMenuItem10.Size = new Size(137, 26);
            toolStripMenuItem10.Text = "文本";
            toolStripMenuItem10.Click += Text_Click;
            // 
            // toolStripMenuItem11
            // 
            toolStripMenuItem11.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem16, toolStripMenuItem12, toolStripMenuItem19, toolStripMenuItem26, toolStripMenuItem30, toolStripMenuItem35, toolStripMenuItem43, toolStripMenuItem48 });
            toolStripMenuItem11.Name = "toolStripMenuItem11";
            toolStripMenuItem11.Size = new Size(83, 24);
            toolStripMenuItem11.Text = "图像操作";
            // 
            // toolStripMenuItem16
            // 
            toolStripMenuItem16.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem17, toolStripMenuItem18 });
            toolStripMenuItem16.Name = "toolStripMenuItem16";
            toolStripMenuItem16.Size = new Size(224, 26);
            toolStripMenuItem16.Text = "基础操作";
            // 
            // toolStripMenuItem17
            // 
            toolStripMenuItem17.Name = "toolStripMenuItem17";
            toolStripMenuItem17.Size = new Size(182, 26);
            toolStripMenuItem17.Text = "增加边框";
            toolStripMenuItem17.Click += AddBorder_Click;
            // 
            // toolStripMenuItem18
            // 
            toolStripMenuItem18.Name = "toolStripMenuItem18";
            toolStripMenuItem18.Size = new Size(182, 26);
            toolStripMenuItem18.Text = "改变颜色空间";
            toolStripMenuItem18.Click += ChangeColorSpace_Click;
            // 
            // toolStripMenuItem12
            // 
            toolStripMenuItem12.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem13, toolStripMenuItem14, toolStripMenuItem15 });
            toolStripMenuItem12.Name = "toolStripMenuItem12";
            toolStripMenuItem12.Size = new Size(224, 26);
            toolStripMenuItem12.Text = "算法运算";
            // 
            // toolStripMenuItem13
            // 
            toolStripMenuItem13.Name = "toolStripMenuItem13";
            toolStripMenuItem13.Size = new Size(186, 26);
            toolStripMenuItem13.Text = "图像加法";
            toolStripMenuItem13.Click += ImageAdd_Click;
            // 
            // toolStripMenuItem14
            // 
            toolStripMenuItem14.Name = "toolStripMenuItem14";
            toolStripMenuItem14.Size = new Size(186, 26);
            toolStripMenuItem14.Text = "图像融合";
            toolStripMenuItem14.Click += ImageAddWeighted_Click;
            // 
            // toolStripMenuItem15
            // 
            toolStripMenuItem15.Name = "toolStripMenuItem15";
            toolStripMenuItem15.Size = new Size(186, 26);
            toolStripMenuItem15.Text = " 图像按位运算";
            toolStripMenuItem15.Click += BitwiseOperation_Click;
            // 
            // toolStripMenuItem19
            // 
            toolStripMenuItem19.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem20, toolStripMenuItem21, toolStripMenuItem22, toolStripMenuItem23, toolStripMenuItem24, toolStripMenuItem25 });
            toolStripMenuItem19.Name = "toolStripMenuItem19";
            toolStripMenuItem19.Size = new Size(224, 26);
            toolStripMenuItem19.Text = "几何变换";
            // 
            // toolStripMenuItem20
            // 
            toolStripMenuItem20.Name = "toolStripMenuItem20";
            toolStripMenuItem20.Size = new Size(167, 26);
            toolStripMenuItem20.Text = "等比缩放";
            toolStripMenuItem20.Click += Scale_Click;
            // 
            // toolStripMenuItem21
            // 
            toolStripMenuItem21.Name = "toolStripMenuItem21";
            toolStripMenuItem21.Size = new Size(167, 26);
            toolStripMenuItem21.Text = "不等比缩放";
            toolStripMenuItem21.Click += ScaleBySize_Click;
            // 
            // toolStripMenuItem22
            // 
            toolStripMenuItem22.Name = "toolStripMenuItem22";
            toolStripMenuItem22.Size = new Size(167, 26);
            toolStripMenuItem22.Text = "平移";
            toolStripMenuItem22.Click += Translation_Click;
            // 
            // toolStripMenuItem23
            // 
            toolStripMenuItem23.Name = "toolStripMenuItem23";
            toolStripMenuItem23.Size = new Size(167, 26);
            toolStripMenuItem23.Text = "旋转";
            toolStripMenuItem23.Click += Rotate_Click;
            // 
            // toolStripMenuItem24
            // 
            toolStripMenuItem24.Name = "toolStripMenuItem24";
            toolStripMenuItem24.Size = new Size(167, 26);
            toolStripMenuItem24.Text = "仿射变换";
            toolStripMenuItem24.Click += AffineTransformation_Click;
            // 
            // toolStripMenuItem25
            // 
            toolStripMenuItem25.Name = "toolStripMenuItem25";
            toolStripMenuItem25.Size = new Size(167, 26);
            toolStripMenuItem25.Text = "透射投影";
            toolStripMenuItem25.Click += Perspectivetransformation_Click;
            // 
            // toolStripMenuItem26
            // 
            toolStripMenuItem26.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem27, toolStripMenuItem28, toolStripMenuItem29 });
            toolStripMenuItem26.Name = "toolStripMenuItem26";
            toolStripMenuItem26.Size = new Size(224, 26);
            toolStripMenuItem26.Text = "阀值";
            // 
            // toolStripMenuItem27
            // 
            toolStripMenuItem27.Name = "toolStripMenuItem27";
            toolStripMenuItem27.Size = new Size(171, 26);
            toolStripMenuItem27.Text = "简单阀值";
            toolStripMenuItem27.Click += SimpleThreshold_Click;
            // 
            // toolStripMenuItem28
            // 
            toolStripMenuItem28.Name = "toolStripMenuItem28";
            toolStripMenuItem28.Size = new Size(171, 26);
            toolStripMenuItem28.Text = "自适应阀值";
            toolStripMenuItem28.Click += CustomThreshold_Click;
            // 
            // toolStripMenuItem29
            // 
            toolStripMenuItem29.Name = "toolStripMenuItem29";
            toolStripMenuItem29.Size = new Size(171, 26);
            toolStripMenuItem29.Text = "Otsu二分值";
            toolStripMenuItem29.Click += OtsuThreshold_Click;
            // 
            // toolStripMenuItem30
            // 
            toolStripMenuItem30.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem31, toolStripMenuItem32, toolStripMenuItem33, toolStripMenuItem34 });
            toolStripMenuItem30.Name = "toolStripMenuItem30";
            toolStripMenuItem30.Size = new Size(224, 26);
            toolStripMenuItem30.Text = "图像模糊(平滑)";
            // 
            // toolStripMenuItem31
            // 
            toolStripMenuItem31.Name = "toolStripMenuItem31";
            toolStripMenuItem31.Size = new Size(152, 26);
            toolStripMenuItem31.Text = "平均滤波";
            toolStripMenuItem31.Click += AverageBlur_Click;
            // 
            // toolStripMenuItem32
            // 
            toolStripMenuItem32.Name = "toolStripMenuItem32";
            toolStripMenuItem32.Size = new Size(152, 26);
            toolStripMenuItem32.Text = "高斯模糊";
            toolStripMenuItem32.Click += GaussianBlur_Click;
            // 
            // toolStripMenuItem33
            // 
            toolStripMenuItem33.Name = "toolStripMenuItem33";
            toolStripMenuItem33.Size = new Size(152, 26);
            toolStripMenuItem33.Text = "中值滤波";
            toolStripMenuItem33.Click += MedianBlur_Click;
            // 
            // toolStripMenuItem34
            // 
            toolStripMenuItem34.Name = "toolStripMenuItem34";
            toolStripMenuItem34.Size = new Size(152, 26);
            toolStripMenuItem34.Text = "双边滤波";
            toolStripMenuItem34.Click += BilateralFilter_Click;
            // 
            // toolStripMenuItem35
            // 
            toolStripMenuItem35.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem36, toolStripMenuItem37, toolStripMenuItem38, toolStripMenuItem39, toolStripMenuItem40, toolStripMenuItem41, toolStripMenuItem42 });
            toolStripMenuItem35.Name = "toolStripMenuItem35";
            toolStripMenuItem35.Size = new Size(224, 26);
            toolStripMenuItem35.Text = "形态转换";
            // 
            // toolStripMenuItem36
            // 
            toolStripMenuItem36.Name = "toolStripMenuItem36";
            toolStripMenuItem36.Size = new Size(167, 26);
            toolStripMenuItem36.Text = "侵蚀";
            toolStripMenuItem36.Click += Erosion_Click;
            // 
            // toolStripMenuItem37
            // 
            toolStripMenuItem37.Name = "toolStripMenuItem37";
            toolStripMenuItem37.Size = new Size(167, 26);
            toolStripMenuItem37.Text = "扩张";
            toolStripMenuItem37.Click += Dilation_Click;
            // 
            // toolStripMenuItem38
            // 
            toolStripMenuItem38.Name = "toolStripMenuItem38";
            toolStripMenuItem38.Size = new Size(167, 26);
            toolStripMenuItem38.Text = "开运算";
            toolStripMenuItem38.Click += Opening_Click;
            // 
            // toolStripMenuItem39
            // 
            toolStripMenuItem39.Name = "toolStripMenuItem39";
            toolStripMenuItem39.Size = new Size(167, 26);
            toolStripMenuItem39.Text = "闭运算";
            toolStripMenuItem39.Click += Closing_Click;
            // 
            // toolStripMenuItem40
            // 
            toolStripMenuItem40.Name = "toolStripMenuItem40";
            toolStripMenuItem40.Size = new Size(167, 26);
            toolStripMenuItem40.Text = "形态学梯度";
            toolStripMenuItem40.Click += MorphologicalGradient_Click;
            // 
            // toolStripMenuItem41
            // 
            toolStripMenuItem41.Name = "toolStripMenuItem41";
            toolStripMenuItem41.Size = new Size(167, 26);
            toolStripMenuItem41.Text = "顶帽";
            toolStripMenuItem41.Click += TopHat_Click;
            // 
            // toolStripMenuItem42
            // 
            toolStripMenuItem42.Name = "toolStripMenuItem42";
            toolStripMenuItem42.Size = new Size(167, 26);
            toolStripMenuItem42.Text = "黑帽";
            toolStripMenuItem42.Click += BlackHat_Click;
            // 
            // toolStripMenuItem43
            // 
            toolStripMenuItem43.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem44, toolStripMenuItem45, toolStripMenuItem46, toolStripMenuItem47 });
            toolStripMenuItem43.Name = "toolStripMenuItem43";
            toolStripMenuItem43.Size = new Size(224, 26);
            toolStripMenuItem43.Text = "图像梯度";
            // 
            // toolStripMenuItem44
            // 
            toolStripMenuItem44.Name = "toolStripMenuItem44";
            toolStripMenuItem44.Size = new Size(193, 26);
            toolStripMenuItem44.Text = "Sobel算子";
            toolStripMenuItem44.Click += Sobel_Click;
            // 
            // toolStripMenuItem45
            // 
            toolStripMenuItem45.Name = "toolStripMenuItem45";
            toolStripMenuItem45.Size = new Size(193, 26);
            toolStripMenuItem45.Text = "Scharr 算子";
            toolStripMenuItem45.Click += Scharr_Click;
            // 
            // toolStripMenuItem46
            // 
            toolStripMenuItem46.Name = "toolStripMenuItem46";
            toolStripMenuItem46.Size = new Size(193, 26);
            toolStripMenuItem46.Text = "Laplacian 算子";
            toolStripMenuItem46.Click += Laplacian_Click;
            // 
            // toolStripMenuItem47
            // 
            toolStripMenuItem47.Name = "toolStripMenuItem47";
            toolStripMenuItem47.Size = new Size(193, 26);
            toolStripMenuItem47.Text = "Canny算法";
            toolStripMenuItem47.Click += Canny_Click;
            // 
            // toolStripMenuItem48
            // 
            toolStripMenuItem48.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem49, toolStripMenuItem50, toolStripMenuItem51, toolStripMenuItem52, toolStripMenuItem53, toolStripMenuItem54, toolStripMenuItem55 });
            toolStripMenuItem48.Name = "toolStripMenuItem48";
            toolStripMenuItem48.Size = new Size(224, 26);
            toolStripMenuItem48.Text = "轮廓";
            // 
            // toolStripMenuItem49
            // 
            toolStripMenuItem49.Name = "toolStripMenuItem49";
            toolStripMenuItem49.Size = new Size(224, 26);
            toolStripMenuItem49.Text = "绘制轮廓";
            toolStripMenuItem49.Click += ContourDraw_Click;
            // 
            // toolStripMenuItem50
            // 
            toolStripMenuItem50.Name = "toolStripMenuItem50";
            toolStripMenuItem50.Size = new Size(224, 26);
            toolStripMenuItem50.Text = "轮廓近似";
            toolStripMenuItem50.Click += ContourApproximation_Click;
            // 
            // toolStripMenuItem51
            // 
            toolStripMenuItem51.Name = "toolStripMenuItem51";
            toolStripMenuItem51.Size = new Size(224, 26);
            toolStripMenuItem51.Text = "轮廓凸包";
            toolStripMenuItem51.Click += ConvexHull_Click;
            // 
            // toolStripMenuItem52
            // 
            toolStripMenuItem52.Name = "toolStripMenuItem52";
            toolStripMenuItem52.Size = new Size(224, 26);
            toolStripMenuItem52.Text = "直角边界矩形";
            toolStripMenuItem52.Click += BoundingRectangleStraight_Click;
            // 
            // toolStripMenuItem53
            // 
            toolStripMenuItem53.Name = "toolStripMenuItem53";
            toolStripMenuItem53.Size = new Size(224, 26);
            toolStripMenuItem53.Text = "旋转边界矩形";
            toolStripMenuItem53.Click += BoundingRectangleRotated_Click;
            // 
            // toolStripMenuItem54
            // 
            toolStripMenuItem54.Name = "toolStripMenuItem54";
            toolStripMenuItem54.Size = new Size(224, 26);
            toolStripMenuItem54.Text = "最小闭合圈";
            toolStripMenuItem54.Click += MinimalEnclosingCircle_Click;
            // 
            // toolStripMenuItem55
            // 
            toolStripMenuItem55.Name = "toolStripMenuItem55";
            toolStripMenuItem55.Size = new Size(224, 26);
            toolStripMenuItem55.Text = "拟合椭圆";
            toolStripMenuItem55.Click += FittingEllipse_Click;
            // 
            // button1
            // 
            button1.Location = new Point(518, 88);
            button1.Name = "button1";
            button1.Size = new Size(117, 29);
            button1.TabIndex = 1;
            button1.Text = "加载测试图片1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += LoadPicture1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(518, 138);
            button2.Name = "button2";
            button2.Size = new Size(117, 29);
            button2.TabIndex = 2;
            button2.Text = "加载测试图片2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += LoadPicture2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(518, 196);
            button3.Name = "button3";
            button3.Size = new Size(117, 29);
            button3.TabIndex = 3;
            button3.Text = "加载测试视频";
            button3.UseVisualStyleBackColor = true;
            button3.Click += LoadVideo1_Click;
            // 
            // CandyCV
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "CandyCV";
            Text = "CandyCV";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem toolStripMenuItem7;
        private ToolStripMenuItem toolStripMenuItem8;
        private ToolStripMenuItem toolStripMenuItem9;
        private ToolStripMenuItem toolStripMenuItem10;
        private ToolStripMenuItem toolStripMenuItem11;
        private ToolStripMenuItem toolStripMenuItem12;
        private ToolStripMenuItem toolStripMenuItem13;
        private ToolStripMenuItem toolStripMenuItem14;
        private ToolStripMenuItem toolStripMenuItem15;
        private Button button1;
        private Button button2;
        private Button button3;
        private ToolStripMenuItem toolStripMenuItem16;
        private ToolStripMenuItem toolStripMenuItem17;
        private ToolStripMenuItem toolStripMenuItem18;
        private ToolStripMenuItem toolStripMenuItem19;
        private ToolStripMenuItem toolStripMenuItem20;
        private ToolStripMenuItem toolStripMenuItem21;
        private ToolStripMenuItem toolStripMenuItem22;
        private ToolStripMenuItem toolStripMenuItem23;
        private ToolStripMenuItem toolStripMenuItem24;
        private ToolStripMenuItem toolStripMenuItem25;
        private ToolStripMenuItem toolStripMenuItem26;
        private ToolStripMenuItem toolStripMenuItem27;
        private ToolStripMenuItem toolStripMenuItem28;
        private ToolStripMenuItem toolStripMenuItem29;
        private ToolStripMenuItem toolStripMenuItem30;
        private ToolStripMenuItem toolStripMenuItem31;
        private ToolStripMenuItem toolStripMenuItem32;
        private ToolStripMenuItem toolStripMenuItem33;
        private ToolStripMenuItem toolStripMenuItem34;
        private ToolStripMenuItem toolStripMenuItem35;
        private ToolStripMenuItem toolStripMenuItem36;
        private ToolStripMenuItem toolStripMenuItem37;
        private ToolStripMenuItem toolStripMenuItem38;
        private ToolStripMenuItem toolStripMenuItem39;
        private ToolStripMenuItem toolStripMenuItem40;
        private ToolStripMenuItem toolStripMenuItem41;
        private ToolStripMenuItem toolStripMenuItem42;
        private ToolStripMenuItem toolStripMenuItem43;
        private ToolStripMenuItem toolStripMenuItem44;
        private ToolStripMenuItem toolStripMenuItem45;
        private ToolStripMenuItem toolStripMenuItem46;
        private ToolStripMenuItem toolStripMenuItem47;
        private ToolStripMenuItem toolStripMenuItem48;
        private ToolStripMenuItem toolStripMenuItem49;
        private ToolStripMenuItem toolStripMenuItem50;
        private ToolStripMenuItem toolStripMenuItem51;
        private ToolStripMenuItem toolStripMenuItem52;
        private ToolStripMenuItem toolStripMenuItem53;
        private ToolStripMenuItem toolStripMenuItem54;
        private ToolStripMenuItem toolStripMenuItem55;
    }
}