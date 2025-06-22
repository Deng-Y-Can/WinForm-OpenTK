using Emgu.CV.Structure;
using Emgu.CV;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Emgu.CV.CvEnum;
using System.Diagnostics;
using OpenCvSharp;
using Size = System.Drawing.Size;
using Mat = Emgu.CV.Mat;
using Point = System.Drawing.Point;

namespace WinFormsApp.MyOpenCV.EmguCV
{
    public partial class CandyImage : Form
    {
        private Bitmap originalImage;
        private Bitmap modifiedImage;
        private Bitmap selectionMask;
        private Rectangle selectionRect = Rectangle.Empty;
        private System.Drawing.Point startPoint;
        private bool isSelecting = false;
        private bool isColorPickerActive = false;
        private Color targetColor = Color.Empty;
        private Color replaceColor = Color.Empty;
        private SelectionMode selectionMode = SelectionMode.Rectangle;
        private Brush selectionFillBrush = new SolidBrush(Color.FromArgb(70, 255, 0, 0));
        private Pen selectionPen = new Pen(Color.Red, 2) { DashStyle = DashStyle.Dash };
        private ToolStrip toolStrip;
        private ToolStripStatusLabel statusLbl;
        private ToolStripComboBox effectComboBox;
        private PictureBox picBox;
        private Panel imagePanel;

        public enum SelectionMode
        {
            Rectangle,
            Ellipse
        }

        /// <summary>
        /// 图像处理效果枚举
        /// </summary>
        public enum ImageEffect
        {
            /// <summary>
            /// 无效果
            /// </summary>
            None,

            /// <summary>
            /// 马赛克效果
            /// </summary>
            Mosaic,

            /// <summary>
            /// 普通模糊
            /// </summary>
            Blur,

            /// <summary>
            /// 锐化效果
            /// </summary>
            Sharpen,

            /// <summary>
            /// 浮雕效果
            /// </summary>
            Emboss,

            /// <summary>
            /// 高斯模糊
            /// </summary>
            GaussianBlur,

            /// <summary>
            /// 灰度化
            /// </summary>
            Grayscale,

            /// <summary>
            /// 反色效果
            /// </summary>
            Invert,

            /// <summary>
            /// 亮度调整
            /// </summary>
            Brightness,

            /// <summary>
            /// 对比度调整
            /// </summary>
            Contrast,

            /// <summary>
            /// 卡通效果
            /// </summary>
            Cartoon,

            /// <summary>
            /// 复古风格
            /// </summary>
            Vintage,

            /// <summary>
            /// 边缘检测
            /// </summary>
            EdgeDetection,

            /// <summary>
            /// 直方图均衡化（增强对比度）
            /// </summary>
            HistogramEqualization,

            /// <summary>
            /// 膨胀（形态学操作）
            /// </summary>
            Dilate,

            /// <summary>
            /// 腐蚀（形态学操作）
            /// </summary>
            Erode,

            /// <summary>
            /// 开运算（先腐蚀后膨胀）
            /// </summary>
            Open,

            /// <summary>
            /// 闭运算（先膨胀后腐蚀）
            /// </summary>
            Close,

            ///// <summary>
            ///// 顺时针旋转90度
            ///// </summary>
            //Rotate90,

            ///// <summary>
            ///// 水平翻转
            ///// </summary>
            //FlipHorizontal,

            ///// <summary>
            ///// 垂直翻转
            ///// </summary>
            //FlipVertical,

            /// <summary>
            /// 增强锐化
            /// </summary>
            SharpenEnhance,

            /// <summary>
            /// 色彩平衡调整
            /// </summary>
            ColorBalance,

            /// <summary>
            /// 模糊增强（边缘保留模糊）
            /// </summary>
            BlurEnhance,

            /// <summary>
            /// 色相调整
            /// </summary>
            HueAdjust,

            /// <summary>
            /// 饱和度调整
            /// </summary>
            SaturationAdjust,

            /// <summary>
            /// 带模糊的边缘检测
            /// </summary>
            BlurEdgeDetection,

            /// <summary>
            /// 柔化效果
            /// </summary>
            //Soften,

            /// <summary>
            /// 素描效果
            /// </summary>
            Sketch,

            /// <summary>
            /// 梦幻效果
            /// </summary>
           // Dreamy,

            /// <summary>
            /// 水彩风格
            /// </summary>
            Watercolor,

            /// <summary>
            /// 霓虹效果
            /// </summary>
            Neon,

            /// <summary>
            /// 饱和度对比度增强
            /// </summary>
            SaturationContrast,

            /// <summary>
            /// 智能锐化
            /// </summary>
            SmartSharpen,

            /// <summary>
            /// 水彩画风格
            /// </summary>
            WatercolorStyle,

            /// <summary>
            /// 印刷风格
            /// </summary>
            PrintStyle,

            /// <summary>
            /// 插画风格
            /// </summary>
            Illustration,

            /// <summary>
            /// 印象派风格
            /// </summary>
            Impressionist,

            /// <summary>
            /// 光照效果
            /// </summary>
            LightEffect,

            /// <summary>
            /// 渐变映射
            /// </summary>
           // GradientMap,

            /// <summary>
            /// 鱼眼效果
            /// </summary>
            //Fisheye,

            /// <summary>
            /// 胶片颗粒效果
            /// </summary>
            FilmGrain,

            /// <summary>
            /// 暖色调
            /// </summary>
            WarmTone,

            /// <summary>
            /// 波普艺术风格
            /// </summary>
            PopArt,

            /// <summary>
            /// 水墨画风格
            /// </summary>
            InkPainting,

            /// <summary>
            /// 3D效果
            /// </summary>
            //_3DEffect,

            /// <summary>
            /// 扭曲变形效果
            /// </summary>
            //Distortion,

            /// <summary>
            /// 雪景效果
            /// </summary>
            Snow,

            ///// <summary>
            ///// 水下效果
            ///// </summary>
            //Underwater,

            ///// <summary>
            ///// 宝丽来风格
            ///// </summary>
            //Polaroid,

            ///// <summary>
            ///// 胶片条效果
            ///// </summary>
            //FilmStrip,

            ///// <summary>
            ///// 运动模糊
            ///// </summary>
            //MotionBlur,

            ///// <summary>
            ///// 油画效果
            ///// </summary>
            //OilPainting,

            /// <summary>
            /// 复古褐色调
            /// </summary>
            Sepia,

            /// <summary>
            /// 深褐色调
            /// </summary>
            SepiaTone,

            /// <summary>
            /// 降噪处理
            /// </summary>
            Denoise,

            /// <summary>
            /// 老照片效果
            /// </summary>
            OldPhoto,

            /// <summary>
            /// 赛博朋克风格
            /// </summary>
            Cyberpunk
        }

        public CandyImage()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "Candy 图片编辑器 V1.2";
            this.Size = new Size(900, 650);
            this.StartPosition = FormStartPosition.CenterScreen;

            // 创建工具栏
            toolStrip = new ToolStrip();
            toolStrip.Dock = DockStyle.Top;
            this.Controls.Add(toolStrip);

            // 添加文件操作按钮
            AddFileButtons();

            // 添加颜色操作按钮
            AddColorButtons();

            // 添加选区操作按钮
            AddSelectionButtons();

            // 添加效果操作
            AddEffectControls();

            // 添加状态栏
            StatusStrip statusStrip = new StatusStrip();
            statusLbl = new ToolStripStatusLabel("就绪");
            statusStrip.Items.Add(statusLbl);
            this.Controls.Add(statusStrip);

            // 创建图片容器
            imagePanel = new Panel();
            imagePanel.Dock = DockStyle.Fill;
            imagePanel.AutoScroll = true;
            imagePanel.HorizontalScroll.Visible = true;
            imagePanel.VerticalScroll.Visible = true;
            this.Controls.Add(imagePanel);

            // 创建图片框
            picBox = new PictureBox();
            picBox.SizeMode = PictureBoxSizeMode.AutoSize;
            picBox.BackColor = Color.White;
            picBox.MouseDown += ImageBox_MouseDown;
            picBox.MouseMove += ImageBox_MouseMove;
            picBox.MouseUp += ImageBox_MouseUp;
            picBox.Paint += ImageBox_Paint;
            imagePanel.Controls.Add(picBox);
        }

        private void AddFileButtons()
        {
            // 添加导入按钮
            ToolStripButton importButton = new ToolStripButton("导入图片");
            importButton.Image = SystemIcons.Hand.ToBitmap();
            importButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            importButton.Click += ImportButton_Click;
            toolStrip.Items.Add(importButton);

            // 添加导出按钮
            ToolStripButton exportButton = new ToolStripButton("导出图片");
            exportButton.Image = SystemIcons.Hand.ToBitmap();
            exportButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            exportButton.Click += ExportButton_Click;
            toolStrip.Items.Add(exportButton);

            // 添加截取按钮
            ToolStripButton cropButton = new ToolStripButton("截取图片");
            cropButton.Image = SystemIcons.Hand.ToBitmap();
            cropButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            cropButton.Click += CropButton_Click;
            toolStrip.Items.Add(cropButton);

            // 添加分隔符
            toolStrip.Items.Add(new ToolStripSeparator());
        }

        private void AddColorButtons()
        {
            // 添加颜色替换按钮
            ToolStripButton colorReplaceButton = new ToolStripButton("颜色替换");
            colorReplaceButton.Click += ColorReplaceButton_Click;
            toolStrip.Items.Add(colorReplaceButton);

            // 添加取色器按钮
            ToolStripButton colorPickerButton = new ToolStripButton("取色器");
            colorPickerButton.Click += ColorPickerButton_Click;
            toolStrip.Items.Add(colorPickerButton);

            // 添加范围颜色替换按钮
            ToolStripButton rangeColorReplaceButton = new ToolStripButton("范围颜色替换");
            rangeColorReplaceButton.Click += RangeColorReplaceButton_Click;
            toolStrip.Items.Add(rangeColorReplaceButton);

            // 添加分隔符
            toolStrip.Items.Add(new ToolStripSeparator());
        }

        private void AddSelectionButtons()
        {
            // 添加选择模式下拉框
            ToolStripComboBox modeComboBox = new ToolStripComboBox();
            modeComboBox.Items.Add("矩形选择");
            modeComboBox.Items.Add("椭圆选择");
            modeComboBox.SelectedIndex = 0;
            modeComboBox.SelectedIndexChanged += (sender, e) =>
            {
                selectionMode = (SelectionMode)modeComboBox.SelectedIndex;
                UpdateStatus($"选择模式: {modeComboBox.SelectedItem}");
            };
            toolStrip.Items.Add(new ToolStripLabel("选择模式:"));
            toolStrip.Items.Add(modeComboBox);

            // 添加框选区域按钮
            ToolStripButton selectAreaButton = new ToolStripButton("框选区域");
            selectAreaButton.Click += SelectAreaButton_Click;
            toolStrip.Items.Add(selectAreaButton);

            // 添加替换图片按钮
            ToolStripButton replaceImageButton = new ToolStripButton("替换图片");
            replaceImageButton.Click += ReplaceImageButton_Click;
            toolStrip.Items.Add(replaceImageButton);

            // 添加添加文字按钮
            ToolStripButton addTextButton = new ToolStripButton("添加文字");
            addTextButton.Click += AddTextButton_Click;
            toolStrip.Items.Add(addTextButton);

            // 添加分隔符
            toolStrip.Items.Add(new ToolStripSeparator());
        }

        private void AddEffectControls()
        {
            // 添加效果选择下拉框
            effectComboBox = new ToolStripComboBox();
            effectComboBox.Items.AddRange(new object[] {
                "无效果",
                "马赛克",
                "模糊",
                "锐化",
                "浮雕",
                "高斯模糊",
                "灰度化",
                "反色",
                "亮度调整",
                "对比度调整",
                "卡通",
                "复古",
                "边缘检测",
                "直方图均衡化",
                "膨胀",
                "腐蚀",
                "开运算",
                "闭运算",

                //"90°旋转",
                //"水平翻转",
                //"垂直翻转",
                "锐化增强",
                "色彩平衡",
                "模糊增强",

                "色调调整",
                "饱和度调整",
                "模糊边缘检测",
                //"柔化",
                "素描",
               // "梦幻",

                "水彩",
                "霓虹",
                "提高色彩饱和度和对比度",
                "智能锐化",
                "水彩画",
                "版画",
                "插画",
                "印象派",

                "光影",
                "复古胶片颗粒",
                "暖色调",
                "波普艺术",
                "中国水墨画",
                "雪景",

                "复古褐",
                "复古棕褐",
                "减少杂色",
                "老照片",

            });
            effectComboBox.SelectedIndex = 0;
            toolStrip.Items.Add(new ToolStripLabel("特效:"));
            toolStrip.Items.Add(effectComboBox);

            // 添加应用特效按钮
            ToolStripButton applyEffectButton = new ToolStripButton("应用特效");
            applyEffectButton.Click += ApplyEffectButton_Click;
            toolStrip.Items.Add(applyEffectButton);
        }

        private void UpdateStatus(string message)
        {
            statusLbl.Text = message;
            Console.WriteLine(message);
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "图像文件|*.jpg;*.jpeg;*.png;*.bmp;*.gif|所有文件|*.*";
                openFileDialog.Title = "选择图片";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        originalImage = new Bitmap(openFileDialog.FileName);
                        modifiedImage = new Bitmap(originalImage);
                        picBox.Image = modifiedImage;
                        selectionRect = Rectangle.Empty;
                        UpdateStatus($"已导入图片: {Path.GetFileName(openFileDialog.FileName)}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"导入图片时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        UpdateStatus("导入图片失败");
                    }
                }
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            if (modifiedImage == null)
            {
                MessageBox.Show("请先导入并编辑图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PNG文件|*.png|JPEG文件|*.jpg|所有文件|*.*";
                saveFileDialog.Title = "保存图片";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ImageFormat format = ImageFormat.Png;
                        string extension = Path.GetExtension(saveFileDialog.FileName).ToLower();

                        if (extension == ".jpg" || extension == ".jpeg")
                            format = ImageFormat.Jpeg;
                        else if (extension == ".bmp")
                            format = ImageFormat.Bmp;
                        else if (extension == ".gif")
                            format = ImageFormat.Gif;

                        modifiedImage.Save(saveFileDialog.FileName, format);
                        UpdateStatus($"已保存图片到: {saveFileDialog.FileName}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"保存图片时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        UpdateStatus("保存图片失败");
                    }
                }
            }
        }

        private void ColorReplaceButton_Click(object sender, EventArgs e)
        {
            if (modifiedImage == null)
            {
                MessageBox.Show("请先导入图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (targetColor == Color.Empty)
            {
                MessageBox.Show("请先使用取色器选择要替换的颜色", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = replaceColor;

                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    replaceColor = colorDialog.Color;

                    if (selectionRect.IsEmpty)
                    {
                        // 对整个图片应用颜色替换
                        ReplaceColor(modifiedImage, targetColor, replaceColor);
                    }
                    else
                    {
                        // 仅对选区应用颜色替换
                        CreateSelectionMask();
                        ReplaceColorInSelection(modifiedImage, targetColor, replaceColor);
                    }

                    UpdateStatus($"已将 {targetColor.Name} 替换为 {replaceColor.Name}");
                    picBox.Invalidate();
                }
            }
        }

        private void ReplaceColor(Bitmap bitmap, Color target, Color replacement)
        {
            BitmapData data = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

            IntPtr ptr = data.Scan0;
            int bytes = Math.Abs(data.Stride) * data.Height;
            byte[] rgbValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            int targetArgb = target.ToArgb();
            int replacementArgb = replacement.ToArgb();

            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                int pixelArgb = (rgbValues[i + 3] << 24) | (rgbValues[i + 2] << 16) | (rgbValues[i + 1] << 8) | rgbValues[i];
                if (pixelArgb == targetArgb)
                {
                    rgbValues[i] = replacement.B;
                    rgbValues[i + 1] = replacement.G;
                    rgbValues[i + 2] = replacement.R;
                    rgbValues[i + 3] = replacement.A;
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bitmap.UnlockBits(data);
        }

        private void ReplaceColorInSelection(Bitmap bitmap, Color target, Color replacement)
        {
            BitmapData data = bitmap.LockBits(
                selectionRect,
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

            IntPtr ptr = data.Scan0;
            int bytes = Math.Abs(data.Stride) * selectionRect.Height;
            byte[] rgbValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            int targetArgb = target.ToArgb();
            int replacementArgb = replacement.ToArgb();

            for (int y = 0; y < selectionRect.Height; y++)
            {
                for (int x = 0; x < selectionRect.Width; x++)
                {
                    int index = y * data.Stride + x * 4;

                    // 检查遮罩
                    if (IsPointInSelection(x, y))
                    {
                        int pixelArgb = (rgbValues[index + 3] << 24) | (rgbValues[index + 2] << 16) | (rgbValues[index + 1] << 8) | rgbValues[index];
                        if (pixelArgb == targetArgb)
                        {
                            rgbValues[index] = replacement.B;
                            rgbValues[index + 1] = replacement.G;
                            rgbValues[index + 2] = replacement.R;
                            rgbValues[index + 3] = replacement.A;
                        }
                    }
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bitmap.UnlockBits(data);
        }

        private void ColorPickerButton_Click(object sender, EventArgs e)
        {
            if (modifiedImage == null)
            {
                MessageBox.Show("请先导入图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            isColorPickerActive = true;
            isSelecting = false;
            UpdateStatus("请点击图片选择要替换的颜色");
        }

        private void SelectAreaButton_Click(object sender, EventArgs e)
        {
            if (modifiedImage == null)
            {
                MessageBox.Show("请先导入图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            isSelecting = true;
            isColorPickerActive = false;
            selectionRect = Rectangle.Empty;
            UpdateStatus("请在图片上框选区域");
        }

        private void ReplaceImageButton_Click(object sender, EventArgs e)
        {
            if (modifiedImage == null)
            {
                MessageBox.Show("请先导入图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (selectionRect.IsEmpty)
            {
                MessageBox.Show("请先框选要替换的区域", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "图像文件|*.jpg;*.jpeg;*.png;*.bmp;*.gif|所有文件|*.*";
                openFileDialog.Title = "选择替换图片";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (Bitmap replacementImage = new Bitmap(openFileDialog.FileName))
                        {
                            CreateSelectionMask();

                            // 根据选区大小调整替换图片
                            Bitmap resizedImage = new Bitmap(replacementImage, selectionRect.Size);

                            // 应用替换
                            ApplyImageInSelection(modifiedImage, resizedImage);

                            picBox.Invalidate();
                            UpdateStatus("已替换选区图片");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"替换图片时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        UpdateStatus("替换图片失败");
                    }
                }
            }
        }

        private void ApplyImageInSelection(Bitmap destination, Bitmap source)
        {
            for (int y = 0; y < selectionRect.Height; y++)
            {
                for (int x = 0; x < selectionRect.Width; x++)
                {
                    // 检查遮罩
                    if (IsPointInSelection(x, y))
                    {
                        destination.SetPixel(
                            selectionRect.X + x,
                            selectionRect.Y + y,
                            source.GetPixel(x, y));
                    }
                }
            }
        }

        private void AddTextButton_Click(object sender, EventArgs e)
        {
            if (modifiedImage == null)
            {
                MessageBox.Show("请先导入图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (selectionRect.IsEmpty)
            {
                MessageBox.Show("请先框选要添加文字的区域", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (TextInputForm textInputForm = new TextInputForm())
            {
                if (textInputForm.ShowDialog() == DialogResult.OK)
                {
                    CreateSelectionMask();

                    using (Graphics g = Graphics.FromImage(modifiedImage))
                    {
                        StringFormat format = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };

                        RectangleF textRect = new RectangleF(
                            selectionRect.X,
                            selectionRect.Y,
                            selectionRect.Width,
                            selectionRect.Height);

                        g.DrawString(
                            textInputForm.InputText,
                            textInputForm.TextFont,
                            new SolidBrush(textInputForm.TextColor),
                            textRect,
                            format);
                    }

                    picBox.Invalidate();
                    UpdateStatus("已添加文字");
                }
            }
        }

        private void ApplyEffectButton_Click(object sender, EventArgs e)
        {
            if (modifiedImage == null)
            {
                MessageBox.Show("请先导入图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (selectionRect.IsEmpty)
            {
                MessageBox.Show("请先框选要应用特效的区域", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ImageEffect effect = (ImageEffect)effectComboBox.SelectedIndex;

            try
            {
                CreateSelectionMask();

                switch (effect)
                {
                    case ImageEffect.Mosaic:
                        ApplyMosaic(modifiedImage, selectionRect, 8);
                        UpdateStatus($"已应用马赛克效果 (强度: 8)");
                        break;

                    case ImageEffect.Blur:
                        ApplyBlur(modifiedImage, selectionRect);
                        UpdateStatus("已应用模糊效果");
                        break;
                    case ImageEffect.Sharpen:
                        ApplySharpen(modifiedImage, selectionRect);
                        UpdateStatus("已应用锐化效果");
                        break;

                    case ImageEffect.Emboss:
                        ApplyEmboss(modifiedImage, selectionRect);
                        UpdateStatus("已应用浮雕效果");
                        break;

                    case ImageEffect.GaussianBlur:
                        ApplyGaussianBlur(modifiedImage, selectionRect);
                        UpdateStatus("已应用高斯模糊效果");
                        break;

                    case ImageEffect.Grayscale:
                        ApplyGrayscale(modifiedImage, selectionRect);
                        UpdateStatus("已应用灰度化效果");
                        break;

                    case ImageEffect.Invert:
                        ApplyInvert(modifiedImage, selectionRect);
                        UpdateStatus("已应用反色效果");
                        break;

                    case ImageEffect.Brightness:
                        ApplyBrightness(modifiedImage, selectionRect, 50);
                        UpdateStatus("已应用亮度调整效果");
                        break;

                    case ImageEffect.Contrast:
                        ApplyContrast(modifiedImage, selectionRect, 1.5f);
                        UpdateStatus("已应用对比度调整效果");
                        break;

                    case ImageEffect.Cartoon:
                        ApplyCartoon(modifiedImage, selectionRect);
                        UpdateStatus("已应用卡通效果");
                        break;              

                    case ImageEffect.Vintage:
                        ApplyVintage(modifiedImage, selectionRect);
                        UpdateStatus("已应用复古效果");
                        break;

                    case ImageEffect.EdgeDetection:
                        ApplyEdgeDetection(modifiedImage, selectionRect);
                        UpdateStatus("已应用边缘检测效果");
                        break;

                    case ImageEffect.HistogramEqualization:
                        ApplyHistogramEqualization(modifiedImage, selectionRect);
                        UpdateStatus("已应用直方图均衡化效果");
                        break;

                    case ImageEffect.Dilate:
                        ApplyDilate(modifiedImage, selectionRect);
                        UpdateStatus("已应用膨胀效果");
                        break;

                    case ImageEffect.Erode:
                        ApplyErode(modifiedImage, selectionRect);
                        UpdateStatus("已应用腐蚀效果");
                        break;

                    case ImageEffect.Open:
                        ApplyOpen(modifiedImage, selectionRect);
                        UpdateStatus("已应用开运算效果");
                        break;

                    case ImageEffect.Close:
                        ApplyClose(modifiedImage, selectionRect);
                        UpdateStatus("已应用闭运算效果");
                        break;

                    //case ImageEffect.Rotate90:
                    //    ApplyRotate90(modifiedImage);
                    //    UpdateStatus("已应用90度旋转效果");
                    //    break;

                    //case ImageEffect.FlipHorizontal:
                    //    ApplyFlipHorizontal(modifiedImage);
                    //    UpdateStatus("已应用水平翻转效果");
                    //    break;

                    //case ImageEffect.FlipVertical:
                    //    ApplyFlipVertical(modifiedImage);
                    //    UpdateStatus("已应用垂直翻转效果");
                    //    break;

                    case ImageEffect.SharpenEnhance:
                        ApplySharpenEnhance(modifiedImage, selectionRect);
                        UpdateStatus("已应用锐化增强效果");
                        break;

                    case ImageEffect.ColorBalance:
                        ApplyColorBalance(modifiedImage, selectionRect);
                        UpdateStatus("已应用色彩平衡调整效果");
                        break;

                    case ImageEffect.BlurEnhance:
                        ApplyBlurEnhance(modifiedImage, selectionRect);
                        UpdateStatus("已应用模糊增强效果");
                        break;

                    case ImageEffect.HueAdjust:
                        ApplyHueAdjust(modifiedImage, selectionRect);
                        UpdateStatus("已应用色调调整效果");
                        break;

                    case ImageEffect.SaturationAdjust:
                        ApplySaturationAdjust(modifiedImage, selectionRect);
                        UpdateStatus("已应用饱和度调整效果");
                        break;

                    case ImageEffect.BlurEdgeDetection:
                        ApplyBlurEdgeDetection(modifiedImage, selectionRect);
                        UpdateStatus("已应用模糊边缘检测效果");
                        break;

                    //case ImageEffect.Soften:
                    //    ApplySoften(modifiedImage, selectionRect);
                    //    UpdateStatus("已应用柔化效果");
                    //    break;

                    case ImageEffect.Sketch:
                        ApplySketch(modifiedImage, selectionRect);
                        UpdateStatus("已应用素描效果");
                        break;

                    //case ImageEffect.Dreamy:
                    //    ApplyDreamy(modifiedImage, selectionRect);
                    //    UpdateStatus("已应用梦幻效果");
                    //    break;

                    case ImageEffect.Watercolor:
                        ApplyWatercolor(modifiedImage, selectionRect);
                        UpdateStatus("已应用水彩效果");
                        break;

                    case ImageEffect.Neon:
                        ApplyNeon(modifiedImage, selectionRect);
                        UpdateStatus("已应用霓虹效果");
                        break;

                    case ImageEffect.SaturationContrast:
                        ApplySaturationContrast(modifiedImage, selectionRect);
                        UpdateStatus("已应用提高色彩饱和度和对比度效果");
                        break;

                    case ImageEffect.SmartSharpen:
                        ApplySmartSharpen(modifiedImage, selectionRect);
                        UpdateStatus("已应用智能锐化效果");
                        break;

                    case ImageEffect.WatercolorStyle:
                        ApplyWatercolorStyle(modifiedImage, selectionRect);
                        UpdateStatus("已应用水彩画风格效果");
                        break;

                    case ImageEffect.PrintStyle:
                        ApplyPrintStyle(modifiedImage, selectionRect);
                        UpdateStatus("已应用版画风格效果");
                        break;

                    case ImageEffect.Illustration:
                        ApplyIllustration(modifiedImage, selectionRect);
                        UpdateStatus("已应用插画风格效果");
                        break;

                    case ImageEffect.Impressionist:
                        ApplyImpressionist(modifiedImage, selectionRect);
                        UpdateStatus("已应用印象派风格效果");
                        break;

                    case ImageEffect.LightEffect:
                        ApplyLightEffect(modifiedImage, selectionRect);
                        UpdateStatus("已应用光影特效");
                        break;

                    //case ImageEffect.GradientMap:
                    //    ApplyGradientMap(modifiedImage, selectionRect);
                    //    UpdateStatus("已应用渐变映射效果");
                    //    break;

                    //case ImageEffect.Fisheye:
                    //    ApplyFisheye(modifiedImage);
                    //    UpdateStatus("已应用鱼眼畸变效果");
                    //    break;

                    case ImageEffect.FilmGrain:
                        ApplyFilmGrain(modifiedImage, selectionRect);
                        UpdateStatus("已应用复古胶片颗粒效果");
                        break;

                    case ImageEffect.WarmTone:
                        ApplyWarmTone(modifiedImage, selectionRect);
                        UpdateStatus("已应用暖色调风格效果");
                        break;

                    case ImageEffect.PopArt:
                        ApplyPopArt(modifiedImage, selectionRect);
                        UpdateStatus("已应用波普艺术风格效果");
                        break;

                    case ImageEffect.InkPainting:
                        ApplyInkPainting(modifiedImage, selectionRect);
                        UpdateStatus("已应用中国水墨画风格效果");
                        break;

                    //case ImageEffect._3DEffect:
                    //    Apply3DEffect(modifiedImage);
                    //    UpdateStatus("已应用立体效果");
                    //    break;

                    //case ImageEffect.Distortion:
                    //    ApplyDistortion(modifiedImage);
                    //    UpdateStatus("已应用扭曲变形效果");
                    //    break;

                    case ImageEffect.Snow:
                        ApplySnow(modifiedImage, selectionRect);
                        UpdateStatus("已应用雪景效果");
                        break;

                    //case ImageEffect.Underwater:
                    //    ApplyUnderwater(modifiedImage, selectionRect);
                    //    UpdateStatus("已应用水下效果");
                    //    break;

                    //case ImageEffect.Polaroid:
                    //    ApplyPolaroid(modifiedImage);
                    //    UpdateStatus("已应用宝丽来风格效果");
                    //    break;

                    //case ImageEffect.FilmStrip:
                    //    ApplyFilmStrip(modifiedImage);
                    //    UpdateStatus("已应用电影胶片风格效果");
                    //    break;

                    //case ImageEffect.MotionBlur:
                    //    ApplyMotionBlur(modifiedImage, selectionRect);
                    //    UpdateStatus("已应用动感模糊效果");
                    //    break;

                    //case ImageEffect.OilPainting:
                    //    ApplyOilPainting(modifiedImage, selectionRect);
                    //    UpdateStatus("已应用油画效果");
                    //    break;

                    case ImageEffect.Sepia:
                        ApplySepia(modifiedImage, selectionRect);
                        UpdateStatus("已应用复古效果");
                        break;

                    case ImageEffect.SepiaTone:
                        ApplySepiaTone(modifiedImage, selectionRect);
                        UpdateStatus("已应用复古棕褐色调效果");
                        break;

                    case ImageEffect.Denoise:
                        ApplyDenoise(modifiedImage, selectionRect);
                        UpdateStatus("已应用减少杂色效果");
                        break;

                    case ImageEffect.OldPhoto:
                        ApplyOldPhoto(modifiedImage, selectionRect);
                        UpdateStatus("已应用老照片风格效果");
                        break;

                    //case ImageEffect.Cyberpunk:
                    //    ApplyCyberpunk(modifiedImage, selectionRect);
                    //    UpdateStatus("已应用赛博朋克风格效果");
                    //    break;

                    case ImageEffect.None:
                        UpdateStatus("未选择任何特效");
                        break;
                }

                picBox.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"应用特效时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus("应用特效失败");
            }
        }

        private void CreateSelectionMask()
        {
            if (selectionRect.IsEmpty)
                return;

            selectionMask = new Bitmap(selectionRect.Width, selectionRect.Height);

            using (Graphics g = Graphics.FromImage(selectionMask))
            {
                // 填充白色(完全不透明)
                g.FillRectangle(Brushes.White, 0, 0, selectionRect.Width, selectionRect.Height);

                if (selectionMode == SelectionMode.Ellipse)
                {
                    // 创建椭圆选区，将选区外的区域设为透明
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        path.AddEllipse(0, 0, selectionRect.Width, selectionRect.Height);

                        using (Region region = new Region(path))
                        {
                            // 排除椭圆区域，剩余区域设为透明
                            g.ExcludeClip(region);
                            g.Clear(Color.Transparent);
                        }
                    }
                }
            }
        }

        private bool IsPointInSelection(int x, int y)
        {
            if (selectionMask == null || x < 0 || x >= selectionMask.Width || y < 0 || y >= selectionMask.Height)
                return false;

            // 检查遮罩的像素是否为非透明
            return selectionMask.GetPixel(x, y).A > 0;
        }

        private void ApplyMosaic(Bitmap bitmap, Rectangle rect, int blockSize)
        {
            using (Bitmap tempBitmap = new Bitmap(bitmap))
            {
                for (int y = 0; y < rect.Height; y += blockSize)
                {
                    for (int x = 0; x < rect.Width; x += blockSize)
                    {
                        // 检查当前点是否在选区区域内
                        if (!IsPointInSelection(x, y))
                            continue;

                        int blockWidth = Math.Min(blockSize, rect.Width - x);
                        int blockHeight = Math.Min(blockSize, rect.Height - y);

                        Color avgColor = GetAverageColor(tempBitmap, rect.X + x, rect.Y + y, blockWidth, blockHeight);

                        for (int dy = 0; dy < blockHeight; dy++)
                        {
                            for (int dx = 0; dx < blockWidth; dx++)
                            {
                                if (IsPointInSelection(x + dx, y + dy))
                                {
                                    bitmap.SetPixel(rect.X + x + dx, rect.Y + y + dy, avgColor);
                                }
                            }
                        }
                    }
                }
            }
        }

        private Color GetAverageColor(Bitmap bitmap, int x, int y, int width, int height)
        {
            long sumR = 0, sumG = 0, sumB = 0;
            int count = 0;

            for (int dy = 0; dy < height; dy++)
            {
                for (int dx = 0; dx < width; dx++)
                {
                    int px = x + dx;
                    int py = y + dy;

                    if (px >= 0 && px < bitmap.Width && py >= 0 && py < bitmap.Height)
                    {
                        Color pixel = bitmap.GetPixel(px, py);
                        sumR += pixel.R;
                        sumG += pixel.G;
                        sumB += pixel.B;
                        count++;
                    }
                }
            }

            if (count == 0) return Color.Black;

            return Color.FromArgb(
                (int)(sumR / count),
                (int)(sumG / count),
                (int)(sumB / count)
            );
        }

        private void ApplyBlur(Bitmap bitmap, Rectangle rect)
        {
            try
            {
                // 创建与选区相同大小的临时图像
                Bitmap selectionBmp = new Bitmap(rect.Width, rect.Height);

                // 复制选区内的图像
                using (Graphics g = Graphics.FromImage(selectionBmp))
                {
                    g.DrawImage(bitmap, 0, 0, rect, GraphicsUnit.Pixel);
                }

                // 方法1: 使用BitmapData和内存操作（兼容性更好）
                using (Image<Bgr, byte> cvImage = new Image<Bgr, byte>(selectionBmp.Width, selectionBmp.Height))
                {
                    BitmapData bmpData = selectionBmp.LockBits(
                        new Rectangle(0, 0, selectionBmp.Width, selectionBmp.Height),
                        ImageLockMode.ReadOnly,
                        PixelFormat.Format24bppRgb);

                    IntPtr ptr = bmpData.Scan0;
                    int bytes = Math.Abs(bmpData.Stride) * bmpData.Height;
                    byte[] rgbValues = new byte[bytes];
                    System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

                    // 将数据复制到EmguCV图像
                    for (int y = 0; y < selectionBmp.Height; y++)
                    {
                        for (int x = 0; x < selectionBmp.Width; x++)
                        {
                            int index = y * bmpData.Stride + x * 3;
                            cvImage[y, x] = new Bgr(rgbValues[index + 2], rgbValues[index + 1], rgbValues[index]);
                        }
                    }

                    selectionBmp.UnlockBits(bmpData);

                    // 创建输出图像
                    using (Image<Bgr, byte> blurredImage = new Image<Bgr, byte>(cvImage.Size))
                    {
                        // 确保核大小为奇数
                        int kernelSize = 5;
                        if (kernelSize % 2 == 0) kernelSize++;

                        // 应用高斯模糊
                        CvInvoke.GaussianBlur(
                            cvImage,
                            blurredImage,
                            new Size(kernelSize, kernelSize),
                            0,
                            0,
                            BorderType.Default);

                        // 将模糊效果应用回原图
                        for (int y = 0; y < rect.Height; y++)
                        {
                            for (int x = 0; x < rect.Width; x++)
                            {
                                if (IsPointInSelection(x, y))
                                {
                                    // 从Image<Bgr, byte>中获取像素值
                                    Bgr color = blurredImage[y, x];

                                    // 应用到原图
                                    bitmap.SetPixel(
                                        rect.X + x,
                                        rect.Y + y,
                                        Color.FromArgb((int)color.Red, (int)color.Green, (int)color.Blue));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"应用模糊效果时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus("应用模糊效果失败");
            }
        }

        private void ImageBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (isSelecting && e.Button == MouseButtons.Left)
            {
                startPoint = e.Location;
                selectionRect = new Rectangle(e.Location, Size.Empty);
                picBox.Invalidate();
            }
            else if (isColorPickerActive && e.Button == MouseButtons.Left)
            {
                if (e.Location.X < modifiedImage?.Width && e.Location.Y < modifiedImage?.Height)
                {
                    targetColor = modifiedImage.GetPixel(e.Location.X, e.Location.Y);
                    isColorPickerActive = false;

                    // 显示颜色信息
                    MessageBox.Show(
                        $"已选择颜色: {targetColor.Name}\nRGB: {targetColor.R}, {targetColor.G}, {targetColor.B}",
                        "颜色选择",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    UpdateStatus($"已选择颜色: {targetColor.Name} ({targetColor.R}, {targetColor.G}, {targetColor.B})");
                }
            }
        }

        private void ImageBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isSelecting && e.Button == MouseButtons.Left)
            {
                int x = Math.Min(startPoint.X, e.Location.X);
                int y = Math.Min(startPoint.Y, e.Location.Y);
                int width = Math.Abs(startPoint.X - e.Location.X);
                int height = Math.Abs(startPoint.Y - e.Location.Y);

                selectionRect = new Rectangle(x, y, width, height);
                picBox.Invalidate();
            }
        }

        private void ImageBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (isSelecting && e.Button == MouseButtons.Left)
            {
                isSelecting = false;

                if (selectionRect.Width > 0 && selectionRect.Height > 0)
                {
                    UpdateStatus($"已选择区域: {selectionRect.Width}x{selectionRect.Height} 像素");
                }
                else
                {
                    selectionRect = Rectangle.Empty;
                    UpdateStatus("选择区域太小，操作取消");
                }

                picBox.Invalidate();
            }
        }

        private void ImageBox_Paint(object sender, PaintEventArgs e)
        {
            if (!selectionRect.IsEmpty)
            {
                if (selectionMode == SelectionMode.Rectangle)
                {
                    e.Graphics.FillRectangle(selectionFillBrush, selectionRect);
                    e.Graphics.DrawRectangle(selectionPen, selectionRect);
                }
                else
                {
                    e.Graphics.FillEllipse(selectionFillBrush, selectionRect);
                    e.Graphics.DrawEllipse(selectionPen, selectionRect);
                }
            }
        }

        // 范围颜色替换相关方法
        private void RangeColorReplaceButton_Click(object sender, EventArgs e)
        {
            if (modifiedImage == null)
            {
                MessageBox.Show("请先导入图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (ColorRangeDialog colorRangeDialog = new ColorRangeDialog())
            {
                if (colorRangeDialog.ShowDialog() == DialogResult.OK)
                {
                    if (selectionRect.IsEmpty)
                    {
                        // 对整个图片应用范围颜色替换
                        ReplaceColorRange(modifiedImage,
                            colorRangeDialog.MinR, colorRangeDialog.MaxR,
                            colorRangeDialog.MinG, colorRangeDialog.MaxG,
                            colorRangeDialog.MinB, colorRangeDialog.MaxB,
                            colorRangeDialog.TargetColor);
                    }
                    else
                    {
                        // 仅对选区应用范围颜色替换
                        CreateSelectionMask();
                        ReplaceColorRangeInSelection(modifiedImage,
                            colorRangeDialog.MinR, colorRangeDialog.MaxR,
                            colorRangeDialog.MinG, colorRangeDialog.MaxG,
                            colorRangeDialog.MinB, colorRangeDialog.MaxB,
                            colorRangeDialog.TargetColor);
                    }

                    UpdateStatus("已完成范围颜色替换");
                    picBox.Invalidate();
                }
            }
        }

        private void ReplaceColorRange(Bitmap bitmap, int minR, int maxR, int minG, int maxG, int minB, int maxB, Color targetColor)
        {
            BitmapData data = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

            IntPtr ptr = data.Scan0;
            int bytes = Math.Abs(data.Stride) * data.Height;
            byte[] rgbValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                byte b = rgbValues[i];
                byte g = rgbValues[i + 1];
                byte r = rgbValues[i + 2];

                // 检查是否在原始颜色范围内
                if (r >= minR && r <= maxR &&
                    g >= minG && g <= maxG &&
                    b >= minB && b <= maxB)
                {
                    // 替换为目标颜色
                    rgbValues[i] = targetColor.B;
                    rgbValues[i + 1] = targetColor.G;
                    rgbValues[i + 2] = targetColor.R;
                    rgbValues[i + 3] = targetColor.A;
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bitmap.UnlockBits(data);
        }

        private void ReplaceColorRangeInSelection(Bitmap bitmap, int minR, int maxR, int minG, int maxG, int minB, int maxB, Color targetColor)
        {
            BitmapData data = bitmap.LockBits(
                selectionRect,
                ImageLockMode.ReadWrite,
                PixelFormat.Format32bppArgb);

            IntPtr ptr = data.Scan0;
            int bytes = Math.Abs(data.Stride) * selectionRect.Height;
            byte[] rgbValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            for (int y = 0; y < selectionRect.Height; y++)
            {
                for (int x = 0; x < selectionRect.Width; x++)
                {
                    int index = y * data.Stride + x * 4;

                    // 检查遮罩
                    if (IsPointInSelection(x, y))
                    {
                        byte b = rgbValues[index];
                        byte g = rgbValues[index + 1];
                        byte r = rgbValues[index + 2];

                        // 检查是否在原始颜色范围内
                        if (r >= minR && r <= maxR &&
                            g >= minG && g <= maxG &&
                            b >= minB && b <= maxB)
                        {
                            // 替换为目标颜色
                            rgbValues[index] = targetColor.B;
                            rgbValues[index + 1] = targetColor.G;
                            rgbValues[index + 2] = targetColor.R;
                            rgbValues[index + 3] = targetColor.A;
                        }
                    }
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
            bitmap.UnlockBits(data);
        }

        // 新增的图片截取功能
        private void CropButton_Click(object sender, EventArgs e)
        {
            if (modifiedImage == null)
            {
                MessageBox.Show("请先导入图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (selectionRect.IsEmpty)
            {
                MessageBox.Show("请先框选要截取的区域", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // 创建与选区相同大小的新图像
                Bitmap croppedImage = new Bitmap(selectionRect.Width, selectionRect.Height);

                // 从原图复制选区内容
                using (Graphics g = Graphics.FromImage(croppedImage))
                {
                    g.DrawImage(modifiedImage,
                        new Rectangle(0, 0, selectionRect.Width, selectionRect.Height),
                        selectionRect,
                        GraphicsUnit.Pixel);
                }

                // 更新当前图像为截取后的图像
                originalImage = croppedImage;
                modifiedImage = croppedImage;
                picBox.Image = modifiedImage;

                // 重置选区
                selectionRect = Rectangle.Empty;

                UpdateStatus("已截取图片");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"截取图片时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus("截取图片失败");
            }
        }


        // 锐化效果处理
        private void ApplySharpen(Bitmap bitmap, Rectangle rect)
        {
            using (Bitmap tempBitmap = new Bitmap(bitmap))
            {
                // 定义锐化卷积核
                float[,] kernel = {
            {0, -1, 0},
            {-1, 5, -1},
            {0, -1, 0}
        };

                ApplyConvolution(bitmap, tempBitmap, rect, kernel);
            }
        }

        

        // 浮雕效果处理
        private void ApplyEmboss(Bitmap bitmap, Rectangle rect)
        {
            using (Bitmap tempBitmap = new Bitmap(bitmap))
            {
                // 定义浮雕卷积核
                float[,] kernel = {
            {-2, -1, 0},
            {-1, 1, 1},
            {0, 1, 2}
        };

                ApplyConvolution(bitmap, tempBitmap, rect, kernel);
            }
        }

        // 高斯模糊效果处理
        private void ApplyGaussianBlur(Bitmap bitmap, Rectangle rect)
        {
            using (Bitmap tempBitmap = new Bitmap(bitmap))
            using (Graphics g = Graphics.FromImage(tempBitmap))
            {
                // 转换为EmguCV格式处理
                using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
                {
                    // 应用高斯模糊
                    using (Image<Bgr, byte> blurredImage = new Image<Bgr, byte>(cvImage.Size))
                    {
                        CvInvoke.GaussianBlur(cvImage, blurredImage, new System.Drawing.Size(5, 5), 0);

                        // 将模糊结果应用到原图选区内
                        for (int y = 0; y < rect.Height; y++)
                        {
                            for (int x = 0; x < rect.Width; x++)
                            {
                                if (IsPointInSelection(x, y))
                                {
                                    Bgr color = blurredImage[y, x];
                                    bitmap.SetPixel(rect.X + x, rect.Y + y, Color.FromArgb((int)color.Red, (int)color.Green, (int)color.Blue));
                                }
                            }
                        }
                    }
                }
            }
        }

        // 灰度化效果处理
        private void ApplyGrayscale(Bitmap bitmap, Rectangle rect)
        {
            using (Bitmap tempBitmap = new Bitmap(bitmap))
            {
                for (int y = 0; y < rect.Height; y++)
                {
                    for (int x = 0; x < rect.Width; x++)
                    {
                        if (IsPointInSelection(x, y))
                        {
                            Color pixel = tempBitmap.GetPixel(rect.X + x, rect.Y + y);
                            int gray = (int)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);
                            bitmap.SetPixel(rect.X + x, rect.Y + y, Color.FromArgb(gray, gray, gray));
                        }
                    }
                }
            }
        }

        // 反色效果处理
        private void ApplyInvert(Bitmap bitmap, Rectangle rect)
        {
            using (Bitmap tempBitmap = new Bitmap(bitmap))
            {
                for (int y = 0; y < rect.Height; y++)
                {
                    for (int x = 0; x < rect.Width; x++)
                    {
                        if (IsPointInSelection(x, y))
                        {
                            Color pixel = tempBitmap.GetPixel(rect.X + x, rect.Y + y);
                            bitmap.SetPixel(rect.X + x, rect.Y + y, Color.FromArgb(255 - pixel.R, 255 - pixel.G, 255 - pixel.B));
                        }
                    }
                }
            }
        }

        // 亮度调整效果处理
        private void ApplyBrightness(Bitmap bitmap, Rectangle rect, int value)
        {
            using (Bitmap tempBitmap = new Bitmap(bitmap))
            {
                for (int y = 0; y < rect.Height; y++)
                {
                    for (int x = 0; x < rect.Width; x++)
                    {
                        if (IsPointInSelection(x, y))
                        {
                            Color pixel = tempBitmap.GetPixel(rect.X + x, rect.Y + y);
                            int r = Math.Min(255, Math.Max(0, pixel.R + value));
                            int g = Math.Min(255, Math.Max(0, pixel.G + value));
                            int b = Math.Min(255, Math.Max(0, pixel.B + value));
                            bitmap.SetPixel(rect.X + x, rect.Y + y, Color.FromArgb(r, g, b));
                        }
                    }
                }
            }
        }

        // 对比度调整效果处理
        private void ApplyContrast(Bitmap bitmap, Rectangle rect, float value)
        {
            using (Bitmap tempBitmap = new Bitmap(bitmap))
            {
                for (int y = 0; y < rect.Height; y++)
                {
                    for (int x = 0; x < rect.Width; x++)
                    {
                        if (IsPointInSelection(x, y))
                        {
                            Color pixel = tempBitmap.GetPixel(rect.X + x, rect.Y + y);
                            int r = Math.Min(255, Math.Max(0, (int)((pixel.R / 255.0 - 0.5) * value + 0.5) * 255));
                            int g = Math.Min(255, Math.Max(0, (int)((pixel.G / 255.0 - 0.5) * value + 0.5) * 255));
                            int b = Math.Min(255, Math.Max(0, (int)((pixel.B / 255.0 - 0.5) * value + 0.5) * 255));
                            bitmap.SetPixel(rect.X + x, rect.Y + y, Color.FromArgb(r, g, b));
                        }
                    }
                }
            }
        }

       

        

        // 卡通效果处理
        private void ApplyCartoon(Bitmap bitmap, Rectangle rect)
        {
            using (Bitmap tempBitmap = new Bitmap(bitmap))
            using (Graphics g = Graphics.FromImage(tempBitmap))
            {
                // 转换为EmguCV格式处理
                using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
                using (Image<Gray, byte> grayImage = cvImage.Convert<Gray, byte>())
                using (Image<Gray, byte> blurredGrayImage = new Image<Gray, byte>(grayImage.Size))
                using (Image<Gray, byte> edgesImage = new Image<Gray, byte>(grayImage.Size))
                {
                    // 中值模糊
                    CvInvoke.MedianBlur(grayImage, blurredGrayImage, 7);

                    // 自适应阈值边缘检测
                    CvInvoke.AdaptiveThreshold(
                        blurredGrayImage, edgesImage, 255,
                        AdaptiveThresholdType.MeanC, ThresholdType.Binary, 9, 2);

                    // 双边滤波
                    using (Image<Bgr, byte> filteredImage = new Image<Bgr, byte>(cvImage.Size))
                    {
                        CvInvoke.BilateralFilter(cvImage, filteredImage, 9, 75, 75);

                        // 与边缘图像进行位运算
                        using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
                        {
                            CvInvoke.BitwiseAnd(filteredImage, filteredImage, resultImage, edgesImage);

                            // 将结果应用到原图选区内
                            for (int y = 0; y < rect.Height; y++)
                            {
                                for (int x = 0; x < rect.Width; x++)
                                {
                                    if (IsPointInSelection(x, y))
                                    {
                                        Bgr color = resultImage[y, x];
                                        bitmap.SetPixel(rect.X + x, rect.Y + y, Color.FromArgb((int)color.Red, (int)color.Green, (int)color.Blue));
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

       

        // 复古效果处理
        private void ApplyVintage(Bitmap bitmap, Rectangle rect)
        {
            using (Bitmap tempBitmap = new Bitmap(bitmap))
            {
                // 定义复古效果卷积核
                float[,] kernel = {
            {0.272f, 0.534f, 0.131f},
            {0.349f, 0.686f, 0.168f},
            {0.393f, 0.769f, 0.189f}
        };

                ApplyConvolution(bitmap, tempBitmap, rect, kernel);
            }
        }

       

        // 卷积操作辅助方法
        private void ApplyConvolution(Bitmap destBitmap, Bitmap srcBitmap, Rectangle rect, float[,] kernel)
        {
            int kernelSize = kernel.GetLength(0);
            int halfSize = kernelSize / 2;

            for (int y = 0; y < rect.Height; y++)
            {
                for (int x = 0; x < rect.Width; x++)
                {
                    if (IsPointInSelection(x, y))
                    {
                        double r = 0, g = 0, b = 0;
                        double kernelSum = 0;

                        for (int ky = 0; ky < kernelSize; ky++)
                        {
                            for (int kx = 0; kx < kernelSize; kx++)
                            {
                                int srcX = rect.X + x + kx - halfSize;
                                int srcY = rect.Y + y + ky - halfSize;

                                // 确保不越界
                                if (srcX >= 0 && srcX < srcBitmap.Width && srcY >= 0 && srcY < srcBitmap.Height)
                                {
                                    Color pixel = srcBitmap.GetPixel(srcX, srcY);
                                    float weight = kernel[ky, kx];

                                    r += pixel.R * weight;
                                    g += pixel.G * weight;
                                    b += pixel.B * weight;
                                    kernelSum += Math.Abs(weight);
                                }
                            }
                        }

                        // 归一化
                        if (kernelSum > 0)
                        {
                            r /= kernelSum;
                            g /= kernelSum;
                            b /= kernelSum;
                        }

                        // 确保值在0-255之间
                        int finalR = Math.Min(255, Math.Max(0, (int)r));
                        int finalG = Math.Min(255, Math.Max(0, (int)g));
                        int finalB = Math.Min(255, Math.Max(0, (int)b));

                        destBitmap.SetPixel(rect.X + x, rect.Y + y, Color.FromArgb(finalR, finalG, finalB));
                    }
                }
            }
        }

        // Bitmap转Image<Bgr, byte>辅助方法
        private Image<Bgr, byte> BitmapToImageBgr(Bitmap bitmap)
        {
            Image<Bgr, byte> result = new Image<Bgr, byte>(bitmap.Width, bitmap.Height);
            BitmapData data = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);

            try
            {
                IntPtr ptr = data.Scan0;
                int bytes = Math.Abs(data.Stride) * data.Height;
                byte[] rgbValues = new byte[bytes];
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        int index = y * data.Stride + x * 3;
                        result[y, x] = new Bgr(rgbValues[index + 2], rgbValues[index + 1], rgbValues[index]);
                    }
                }
            }
            finally
            {
                bitmap.UnlockBits(data);
            }

            return result;
        }

        // 直方图均衡化
        private void ApplyHistogramEqualization(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Ycc, byte> yccImage = cvImage.Convert<Ycc, byte>())
            {
                // 分离 YCC 通道
                Image<Gray, byte>[] channels = yccImage.Split();

                // 仅对亮度通道进行直方图均衡化
                CvInvoke.EqualizeHist(channels[0], channels[0]);

                // 创建合并后的图像
                using (Image<Ycc, byte> resultYcc = new Image<Ycc, byte>(cvImage.Size))
                {
                    // 合并处理后的通道
                    for (int y = 0; y < cvImage.Height; y++)
                    {
                        for (int x = 0; x < cvImage.Width; x++)
                        {
                            Ycc pixel = new Ycc(
                                channels[0][y, x].Intensity,  // Y (亮度)
                                channels[1][y, x].Intensity,  // Cb (蓝色差)
                                channels[2][y, x].Intensity   // Cr (红色差)
                            );
                            resultYcc[y, x] = pixel;
                        }
                    }

                    // 转回 Bgr 格式
                    using (Image<Bgr, byte> resultImage = resultYcc.Convert<Bgr, byte>())
                    {
                        ApplyEffectToSelection(bitmap, rect, resultImage);
                    }
                }

                // 释放分离的通道资源
                foreach (var channel in channels)
                    channel.Dispose();
            }
        }

        // 膨胀操作
        private void ApplyDilate(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> dilatedImage = new Image<Bgr, byte>(cvImage.Size))
            {
                // 创建结构元素
                using (Mat structElement = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1)))
                {
                    // 应用膨胀操作
                    CvInvoke.Dilate(cvImage, dilatedImage, structElement, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
                }

                ApplyEffectToSelection(bitmap, rect, dilatedImage);
            }
        }

        // 腐蚀操作
        private void ApplyErode(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> erodedImage = new Image<Bgr, byte>(cvImage.Size))
            {
                // 创建结构元素
                using (Mat structElement = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1)))
                {
                    // 应用腐蚀操作
                    CvInvoke.Erode(cvImage, erodedImage, structElement, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
                }

                ApplyEffectToSelection(bitmap, rect, erodedImage);
            }
        }

        // 开运算 (先腐蚀后膨胀)
        private void ApplyOpen(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> openedImage = new Image<Bgr, byte>(cvImage.Size))
            {
                // 创建结构元素
                using (Mat structElement = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1)))
                {
                    // 应用开运算
                    CvInvoke.MorphologyEx(cvImage, openedImage, MorphOp.Open, structElement, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
                }

                ApplyEffectToSelection(bitmap, rect, openedImage);
            }
        }

        // 闭运算 (先膨胀后腐蚀)
        private void ApplyClose(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> closedImage = new Image<Bgr, byte>(cvImage.Size))
            {
                // 创建结构元素
                using (Mat structElement = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(3, 3), new Point(-1, -1)))
                {
                    // 应用闭运算
                    CvInvoke.MorphologyEx(cvImage, closedImage, MorphOp.Close, structElement, new Point(-1, -1), 1, BorderType.Default, new MCvScalar());
                }

                ApplyEffectToSelection(bitmap, rect, closedImage);
            }
        }

        // 边缘检测 
        private void ApplyEdgeDetection(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Gray, byte> grayImage = cvImage.Convert<Gray, byte>())
            using (Image<Gray, byte> edgeImage = new Image<Gray, byte>(grayImage.Size))
            {
                // 使用 Canny 边缘检测
                CvInvoke.Canny(grayImage, edgeImage, 50, 150);

                using (Image<Bgr, byte> edgeBgr = edgeImage.Convert<Bgr, byte>())
                {
                    ApplyEffectToSelection(bitmap, rect, edgeBgr);
                }
            }
        }

        // 应用效果到选区内的辅助方法
        private void ApplyEffectToSelection(Bitmap destBitmap, Rectangle rect, Image<Bgr, byte> sourceImage)
        {
            for (int y = 0; y < rect.Height; y++)
            {
                for (int x = 0; x < rect.Width; x++)
                {
                    if (IsPointInSelection(x, y) &&
                        y + rect.Y < destBitmap.Height &&
                        x + rect.X < destBitmap.Width)
                    {
                        Bgr color = sourceImage[y + rect.Y, x + rect.X];
                        destBitmap.SetPixel(rect.X + x, rect.Y + y,
                            Color.FromArgb((int)color.Red, (int)color.Green, (int)color.Blue));
                    }
                }
            }
        }

        /// <summary>
        /// 顺时针旋转图像90度
        /// </summary>
        private void ApplyRotate90(Bitmap bitmap)
        {
            if (bitmap == null || bitmap.Width <= 0 || bitmap.Height <= 0)
                return;

            try
            {
                using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
                using (Image<Bgr, byte> rotatedImage = cvImage.Rotate(90, new Bgr(0, 0, 0)))
                {
                    // 创建临时内存流存储处理后的图像
                    using (MemoryStream ms = new MemoryStream())
                    {
                        rotatedImage.ToBitmap().Save(ms, ImageFormat.Bmp);
                        ms.Position = 0;

                        // 从流加载新的Bitmap，确保数据完整性
                        Bitmap newBitmap = new Bitmap(ms);

                        // 释放原图像并替换
                        bitmap.Dispose();
                        bitmap = newBitmap;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"旋转图像时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // 可以添加日志记录
            }
        }

        /// <summary>
        /// 水平翻转图像（左右翻转）
        /// </summary>
        private void ApplyFlipHorizontal(Bitmap bitmap)
        {
            if (bitmap == null || bitmap.Width <= 0 || bitmap.Height <= 0)
                return;

            try
            {
                using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
                {
                    CvInvoke.Flip(cvImage, cvImage, FlipType.Horizontal);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        cvImage.ToBitmap().Save(ms, ImageFormat.Bmp);
                        ms.Position = 0;

                        Bitmap newBitmap = new Bitmap(ms);
                        bitmap.Dispose();
                        bitmap = newBitmap;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"水平翻转图像时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 垂直翻转图像（上下翻转）
        /// </summary>
        private void ApplyFlipVertical(Bitmap bitmap)
        {
            if (bitmap == null || bitmap.Width <= 0 || bitmap.Height <= 0)
                return;

            try
            {
                using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
                {
                    CvInvoke.Flip(cvImage, cvImage, FlipType.Vertical);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        cvImage.ToBitmap().Save(ms, ImageFormat.Bmp);
                        ms.Position = 0;

                        Bitmap newBitmap = new Bitmap(ms);
                        bitmap.Dispose();
                        bitmap = newBitmap;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"垂直翻转图像时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 锐化增强
        private void ApplySharpenEnhance(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> sharpenedImage = new Image<Bgr, byte>(cvImage.Size))
            {
                // 锐化卷积核
                float[,] kernel = {
            { -1, -1, -1 },
            { -1,  9, -1 },
            { -1, -1, -1 }
        };

                // 应用卷积
                ApplyConvolution(cvImage, sharpenedImage, kernel);

                ApplyEffectToSelection(bitmap, rect, sharpenedImage);
            }
        }

        // 色彩平衡调整
        private void ApplyColorBalance(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> balancedImage = new Image<Bgr, byte>(cvImage.Size))
            {
                // 色彩平衡调整系数 (可根据需要调整)
                double redGain = 1.1;   // 红色增益
                double greenGain = 1.0; // 绿色增益
                double blueGain = 0.9;  // 蓝色增益

                // 调整每个像素的颜色平衡
                for (int y = 0; y < cvImage.Rows; y++)
                {
                    for (int x = 0; x < cvImage.Cols; x++)
                    {
                        Bgr pixel = cvImage[y, x];

                        // 调整RGB值并确保在有效范围内
                        byte red = (byte)Math.Min(255, Math.Max(0, pixel.Red * redGain));
                        byte green = (byte)Math.Min(255, Math.Max(0, pixel.Green * greenGain));
                        byte blue = (byte)Math.Min(255, Math.Max(0, pixel.Blue * blueGain));

                        balancedImage[y, x] = new Bgr(blue, green, red);
                    }
                }

                ApplyEffectToSelection(bitmap, rect, balancedImage);
            }
        }

        // 模糊增强 (边缘保留模糊)
        private void ApplyBlurEnhance(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> blurredImage = new Image<Bgr, byte>(cvImage.Size))
            {
                // 应用双边滤波 (保留边缘的同时模糊图像)
                CvInvoke.BilateralFilter(cvImage, blurredImage, 9, 75, 75);

                // 创建"细节"图像 (原图 - 模糊图)
                using (Image<Bgr, byte> detailImage = cvImage.Sub(blurredImage))
                {
                    // 将细节添加回原图以增强边缘
                    using (Image<Bgr, byte> enhancedImage = cvImage.Add(detailImage.Mul(0.5)))
                    {
                        ApplyEffectToSelection(bitmap, rect, enhancedImage);
                    }
                }
            }
        }

        // 辅助方法：执行卷积操作
        private void ApplyConvolution(Image<Bgr, byte> source, Image<Bgr, byte> dest, float[,] kernel)
        {
            int kernelSize = kernel.GetLength(0);
            int halfSize = kernelSize / 2;

            for (int y = 0; y < source.Rows; y++)
            {
                for (int x = 0; x < source.Cols; x++)
                {
                    double r = 0, g = 0, b = 0;
                    double kernelSum = 0;

                    for (int ky = 0; ky < kernelSize; ky++)
                    {
                        for (int kx = 0; kx < kernelSize; kx++)
                        {
                            int srcX = x + kx - halfSize;
                            int srcY = y + ky - halfSize;

                            if (srcX >= 0 && srcX < source.Cols && srcY >= 0 && srcY < source.Rows)
                            {
                                Bgr pixel = source[srcY, srcX];
                                float weight = kernel[ky, kx];

                                r += pixel.Red * weight;
                                g += pixel.Green * weight;
                                b += pixel.Blue * weight;
                                kernelSum += Math.Abs(weight);
                            }
                        }
                    }

                    if (kernelSum > 0)
                    {
                        r /= kernelSum;
                        g /= kernelSum;
                        b /= kernelSum;
                    }

                    dest[y, x] = new Bgr(
                        Math.Min(255, Math.Max(0, (int)r)),
                        Math.Min(255, Math.Max(0, (int)g)),
                        Math.Min(255, Math.Max(0, (int)b))
                    );
                }
            }
        }


        // 色调调整
        private void ApplyHueAdjust(Bitmap bitmap, Rectangle rect, float hueShift = 30f)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Hsv, byte> hsvImage = cvImage.Convert<Hsv, byte>())
            {
                for (int y = rect.Top; y < rect.Bottom && y < hsvImage.Rows; y++)
                {
                    for (int x = rect.Left; x < rect.Right && x < hsvImage.Cols; x++)
                    {
                        Hsv pixel = hsvImage[y, x];
                        MCvScalar scalar = pixel.MCvScalar;

                        // 调整色调并确保在0-180范围内
                        scalar.V0 = (int)((scalar.V0 + hueShift) % 180);
                        pixel.MCvScalar = scalar;

                        hsvImage[y, x] = pixel;
                    }
                }

                using (Image<Bgr, byte> resultImage = hsvImage.Convert<Bgr, byte>())
                {
                    ApplyEffectToSelection(bitmap, rect, resultImage);
                }
            }
        }

        // 饱和度调整
        private void ApplySaturationAdjust(Bitmap bitmap, Rectangle rect, float saturationFactor = 1.5f)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Hsv, byte> hsvImage = cvImage.Convert<Hsv, byte>())
            {
                for (int y = rect.Top; y < rect.Bottom && y < hsvImage.Rows; y++)
                {
                    for (int x = rect.Left; x < rect.Right && x < hsvImage.Cols; x++)
                    {
                        Hsv pixel = hsvImage[y, x];
                        MCvScalar scalar = pixel.MCvScalar;

                        // 调整饱和度并确保在0-255范围内
                        double newSat = scalar.V1 * saturationFactor;
                        scalar.V1 = Math.Min(255, Math.Max(0, newSat));
                        pixel.MCvScalar = scalar;

                        hsvImage[y, x] = pixel;
                    }
                }

                using (Image<Bgr, byte> resultImage = hsvImage.Convert<Bgr, byte>())
                {
                    ApplyEffectToSelection(bitmap, rect, resultImage);
                }
            }
        }

        // 模糊边缘检测
        private void ApplyBlurEdgeDetection(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Gray, byte> grayImage = cvImage.Convert<Gray, byte>())
            using (Image<Gray, byte> blurredImage = new Image<Gray, byte>(grayImage.Size))
            using (Image<Gray, byte> edgeImage = new Image<Gray, byte>(grayImage.Size))
            {
                CvInvoke.GaussianBlur(grayImage, blurredImage, new Size(5, 5), 0);
                CvInvoke.Canny(blurredImage, edgeImage, 20, 60);

                using (Image<Bgr, byte> edgeBgr = edgeImage.Convert<Bgr, byte>())
                using (Image<Bgr, byte> resultImage = cvImage.Clone())
                {
                    for (int y = rect.Top; y < rect.Bottom && y < resultImage.Rows; y++)
                    {
                        for (int x = rect.Left; x < rect.Right && x < resultImage.Cols; x++)
                        {
                            if (edgeImage[y, x].Intensity > 128)
                            {
                                Bgr original = cvImage[y, x];
                                resultImage[y, x] = new Bgr(
                                    (byte)Math.Min(255, original.Blue * 1.2),
                                    (byte)Math.Min(255, original.Green * 1.2),
                                    (byte)Math.Min(255, original.Red * 1.2)
                                );
                            }
                            else
                            {
                                byte gray = (byte)grayImage[y, x].Intensity;
                                resultImage[y, x] = new Bgr(gray, gray, gray);
                            }
                        }
                    }

                    ApplyEffectToSelection(bitmap, rect, resultImage);
                }
            }
        }

        // 柔化效果
        private void ApplySoften(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> blurredImage = new Image<Bgr, byte>(cvImage.Size))
            using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
            {
                CvInvoke.GaussianBlur(cvImage, blurredImage, new Size(9, 9), 0);

                // 使用 Mat 进行加权混合
                using (Mat alpha = new Mat(resultImage.Size, DepthType.Cv64F, 3))
                using (Mat beta = new Mat(resultImage.Size, DepthType.Cv64F, 3))
                {
                    alpha.SetTo(new MCvScalar(0.7));
                    beta.SetTo(new MCvScalar(0.3));

                    CvInvoke.Multiply(cvImage, alpha, cvImage);
                    CvInvoke.Multiply(blurredImage, beta, blurredImage);
                    CvInvoke.Add(cvImage, blurredImage, resultImage);
                }

                ApplyEffectToSelection(bitmap, rect, resultImage);
            }
        }

        // 素描效果
        private void ApplySketch(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Gray, byte> grayImage = cvImage.Convert<Gray, byte>())
            using (Image<Gray, byte> invertedImage = new Image<Gray, byte>(grayImage.Size))
            using (Image<Gray, byte> blurredImage = new Image<Gray, byte>(grayImage.Size))
            using (Image<Gray, byte> sketchImage = new Image<Gray, byte>(grayImage.Size))
            {
                CvInvoke.BitwiseNot(grayImage, invertedImage);
                CvInvoke.GaussianBlur(invertedImage, blurredImage, new Size(21, 21), 0);

                for (int y = rect.Top; y < rect.Bottom && y < sketchImage.Rows; y++)
                {
                    for (int x = rect.Left; x < rect.Right && x < sketchImage.Cols; x++)
                    {
                        byte gray = (byte)grayImage[y, x].Intensity;
                        byte blurred = (byte)blurredImage[y, x].Intensity;

                        if (blurred == 255) blurred = 254;

                        int value = gray * 255 / (255 - blurred);
                        sketchImage[y, x] = new Gray((byte)Math.Min(255, value));
                    }
                }

                using (Image<Bgr, byte> resultImage = sketchImage.Convert<Bgr, byte>())
                {
                    ApplyEffectToSelection(bitmap, rect, resultImage);
                }
            }
        }

        // 梦幻效果
        private void ApplyDreamy(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> blurredImage = new Image<Bgr, byte>(cvImage.Size))
            using (Image<Bgr, byte> glowImage = new Image<Bgr, byte>(cvImage.Size))
            using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
            {
                CvInvoke.GaussianBlur(cvImage, blurredImage, new Size(15, 15), 0);

                // 创建辉光效果
                using (Mat scale = new Mat(glowImage.Size, DepthType.Cv64F, 3))
                {
                    scale.SetTo(new MCvScalar(2, 2, 2));

                    CvInvoke.Subtract(blurredImage, cvImage, glowImage);
                    CvInvoke.Multiply(glowImage, scale, glowImage);
                    CvInvoke.Add(glowImage, cvImage, glowImage);
                }

                // 混合原图和辉光效果
                using (Mat alpha = new Mat(resultImage.Size, DepthType.Cv64F, 3))
                using (Mat beta = new Mat(resultImage.Size, DepthType.Cv64F, 3))
                {
                    alpha.SetTo(new MCvScalar(0.6));
                    beta.SetTo(new MCvScalar(0.4));

                    CvInvoke.Multiply(cvImage, alpha, cvImage);
                    CvInvoke.Multiply(glowImage, beta, glowImage);
                    CvInvoke.Add(cvImage, glowImage, resultImage);
                }

                // 增强色彩饱和度
                using (Image<Hsv, byte> hsvImage = resultImage.Convert<Hsv, byte>())
                {
                    for (int y = rect.Top; y < rect.Bottom && y < hsvImage.Rows; y++)
                    {
                        for (int x = rect.Left; x < rect.Right && x < hsvImage.Cols; x++)
                        {
                            Hsv pixel = hsvImage[y, x];
                            MCvScalar scalar = pixel.MCvScalar;

                            // 调整饱和度并确保在0-255范围内
                            double newSat = scalar.V1 * 1.2;
                            scalar.V1 = Math.Min(255, Math.Max(0, newSat));
                            pixel.MCvScalar = scalar;

                            hsvImage[y, x] = pixel;
                        }
                    }

                    using (Image<Bgr, byte> finalImage = hsvImage.Convert<Bgr, byte>())
                    {
                        ApplyEffectToSelection(bitmap, rect, finalImage);
                    }
                }
            }
        }

        // 水彩效果
        private void ApplyWatercolor(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
            {
                // 应用水彩特效：中值滤波 + 增强边缘
                CvInvoke.MedianBlur(cvImage, resultImage, 9);

                // 边缘检测并增强
                using (Image<Gray, byte> grayImage = resultImage.Convert<Gray, byte>())
                using (Image<Gray, byte> edgeImage = new Image<Gray, byte>(grayImage.Size))
                {
                    CvInvoke.Canny(grayImage, edgeImage, 80, 160);

                    // 边缘区域变暗，增强水彩效果
                    for (int y = rect.Top; y < rect.Bottom && y < resultImage.Rows; y++)
                    {
                        for (int x = rect.Left; x < rect.Right && x < resultImage.Cols; x++)
                        {
                            if (edgeImage[y, x].Intensity > 100)
                            {
                                Bgr pixel = resultImage[y, x];
                                resultImage[y, x] = new Bgr(
                                    Math.Max(0, pixel.Blue - 30),
                                    Math.Max(0, pixel.Green - 30),
                                    Math.Max(0, pixel.Red - 30)
                                );
                            }
                        }
                    }
                }

                // 增强色彩饱和度
                using (Image<Hsv, byte> hsvImage = resultImage.Convert<Hsv, byte>())
                {
                    for (int y = rect.Top; y < rect.Bottom && y < hsvImage.Rows; y++)
                    {
                        for (int x = rect.Left; x < rect.Right && x < hsvImage.Cols; x++)
                        {
                            Hsv pixel = hsvImage[y, x];
                            MCvScalar scalar = pixel.MCvScalar;
                            scalar.V1 = Math.Min(255, scalar.V1 * 1.3); // 增加饱和度
                            pixel.MCvScalar = scalar;
                            hsvImage[y, x] = pixel;
                        }
                    }

                    using (Image<Bgr, byte> finalImage = hsvImage.Convert<Bgr, byte>())
                    {
                        ApplyEffectToSelection(bitmap, rect, finalImage);
                    }
                }
            }
        }

        // 霓虹效果
        private void ApplyNeon(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Hsv, byte> hsvImage = cvImage.Convert<Hsv, byte>())
            using (Image<Bgr, byte> glowImage = new Image<Bgr, byte>(cvImage.Size))
            using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
            {
                // 增强色彩和饱和度
                for (int y = rect.Top; y < rect.Bottom && y < hsvImage.Rows; y++)
                {
                    for (int x = rect.Left; x < rect.Right && x < hsvImage.Cols; x++)
                    {
                        Hsv pixel = hsvImage[y, x];
                        MCvScalar scalar = pixel.MCvScalar;

                        scalar.V1 = Math.Min(255, scalar.V1 * 1.5); // 增加饱和度
                        scalar.V2 = Math.Min(255, scalar.V2 * 1.2); // 增加亮度

                        pixel.MCvScalar = scalar;
                        hsvImage[y, x] = pixel;
                    }
                }

                using (Image<Bgr, byte> colorEnhanced = hsvImage.Convert<Bgr, byte>())
                {
                    // 应用边缘检测，突出霓虹线条
                    using (Image<Gray, byte> grayImage = colorEnhanced.Convert<Gray, byte>())
                    using (Image<Gray, byte> edgeImage = new Image<Gray, byte>(grayImage.Size))
                    {
                        CvInvoke.Canny(grayImage, edgeImage, 50, 150);

                        // 创建霓虹边缘效果
                        for (int y = rect.Top; y < rect.Bottom && y < glowImage.Rows; y++)
                        {
                            for (int x = rect.Left; x < rect.Right && x < glowImage.Cols; x++)
                            {
                                if (edgeImage[y, x].Intensity > 100)
                                {
                                    // 获取边缘处的原始颜色
                                    Bgr original = colorEnhanced[y, x];

                                    // 增强颜色，创建霓虹效果
                                    glowImage[y, x] = new Bgr(
                                        Math.Min(255, original.Blue * 1.8),
                                        Math.Min(255, original.Green * 1.8),
                                        Math.Min(255, original.Red * 1.8)
                                    );
                                }
                                else
                                {
                                    // 非边缘区域保持原始颜色但降低亮度
                                    Bgr original = colorEnhanced[y, x];
                                    glowImage[y, x] = new Bgr(
                                        Math.Max(0, original.Blue / 2),
                                        Math.Max(0, original.Green / 2),
                                        Math.Max(0, original.Red / 2)
                                    );
                                }
                            }
                        }
                    }

                    // 应用高斯模糊，创建光晕效果
                    using (Image<Bgr, byte> blurredGlow = new Image<Bgr, byte>(glowImage.Size))
                    {
                        CvInvoke.GaussianBlur(glowImage, blurredGlow, new Size(7, 7), 0);

                        // 合并增强的颜色和霓虹光晕
                        CvInvoke.AddWeighted(colorEnhanced, 0.7, blurredGlow, 0.3, 0, resultImage);
                        ApplyEffectToSelection(bitmap, rect, resultImage);
                    }
                }
            }
        }

       

        // 智能锐化
        private void ApplySmartSharpen(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> blurredImage = new Image<Bgr, byte>(cvImage.Size))
            using (Image<Bgr, byte> sharpenedImage = new Image<Bgr, byte>(cvImage.Size))
            {
                // 应用高斯模糊
                CvInvoke.GaussianBlur(cvImage, blurredImage, new Size(5, 5), 0);

                // 智能锐化: 原图 + (原图 - 模糊图) * 锐化强度
                float sharpenIntensity = 1.5f;

                for (int y = rect.Top; y < rect.Bottom && y < cvImage.Rows; y++)
                {
                    for (int x = rect.Left; x < rect.Right && x < cvImage.Cols; x++)
                    {
                        Bgr original = cvImage[y, x];
                        Bgr blurred = blurredImage[y, x];

                        // 计算锐化后的像素值
                        byte blue = (byte)Math.Min(255, Math.Max(0, original.Blue + (original.Blue - blurred.Blue) * sharpenIntensity));
                        byte green = (byte)Math.Min(255, Math.Max(0, original.Green + (original.Green - blurred.Green) * sharpenIntensity));
                        byte red = (byte)Math.Min(255, Math.Max(0, original.Red + (original.Red - blurred.Red) * sharpenIntensity));

                        sharpenedImage[y, x] = new Bgr(blue, green, red);
                    }
                }

                ApplyEffectToSelection(bitmap, rect, sharpenedImage);
            }
        }
        // 饱和度和对比度增强
        private void ApplySaturationContrast(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Hsv, byte> hsvImage = cvImage.Convert<Hsv, byte>())
            {
                // 增强饱和度
                for (int y = rect.Top; y < rect.Bottom && y < hsvImage.Rows; y++)
                {
                    for (int x = rect.Left; x < rect.Right && x < hsvImage.Cols; x++)
                    {
                        Hsv pixel = hsvImage[y, x];
                        // 通过 MCvScalar 访问饱和度（V1 分量）
                        MCvScalar scalar = pixel.MCvScalar;
                        scalar.V1 = Math.Min(255, scalar.V1 * 1.4); // 增加饱和度
                        pixel.MCvScalar = scalar;
                        hsvImage[y, x] = pixel;
                    }
                }

                using (Image<Bgr, byte> colorEnhanced = hsvImage.Convert<Bgr, byte>())
                {
                    // 增强对比度
                    using (Image<Bgr, byte> contrastImage = new Image<Bgr, byte>(colorEnhanced.Size))
                    {
                        float contrastFactor = 1.3f;
                        int contrastCenter = 128;

                        for (int y = rect.Top; y < rect.Bottom && y < colorEnhanced.Rows; y++)
                        {
                            for (int x = rect.Left; x < rect.Right && x < colorEnhanced.Cols; x++)
                            {
                                Bgr pixel = colorEnhanced[y, x];

                                // 对比度调整公式
                                int newBlue = (int)((pixel.Blue - contrastCenter) * contrastFactor + contrastCenter);
                                int newGreen = (int)((pixel.Green - contrastCenter) * contrastFactor + contrastCenter);
                                int newRed = (int)((pixel.Red - contrastCenter) * contrastFactor + contrastCenter);

                                // 确保值在有效范围内
                                pixel.Blue = (byte)Math.Max(0, Math.Min(255, newBlue));
                                pixel.Green = (byte)Math.Max(0, Math.Min(255, newGreen));
                                pixel.Red = (byte)Math.Max(0, Math.Min(255, newRed));

                                contrastImage[y, x] = pixel;
                            }
                        }

                        ApplyEffectToSelection(bitmap, rect, contrastImage);
                    }
                }
            }
        }

        // 水彩画风格
        private void ApplyWatercolorStyle(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
            {
                // 应用中值滤波模拟水彩画的块状效果
                CvInvoke.MedianBlur(cvImage, resultImage, 7);

                // 轻微模糊，模拟水彩画的柔和感
                CvInvoke.GaussianBlur(resultImage, resultImage, new Size(3, 3), 0);

                // 增强色彩饱和度
                using (Image<Hsv, byte> hsvImage = resultImage.Convert<Hsv, byte>())
                {
                    for (int y = rect.Top; y < rect.Bottom && y < hsvImage.Rows; y++)
                    {
                        for (int x = rect.Left; x < rect.Right && x < hsvImage.Cols; x++)
                        {
                            Hsv pixel = hsvImage[y, x];
                            MCvScalar scalar = pixel.MCvScalar;

                            // 调整饱和度（V1 分量）和亮度（V2 分量）
                            scalar.V1 = Math.Min(255, scalar.V1 * 1.2);  // 增加饱和度
                            scalar.V2 = Math.Min(255, scalar.V2 * 1.05); // 轻微增加亮度

                            pixel.MCvScalar = scalar;
                            hsvImage[y, x] = pixel;
                        }
                    }

                    using (Image<Bgr, byte> finalImage = hsvImage.Convert<Bgr, byte>())
                    {
                        ApplyEffectToSelection(bitmap, rect, finalImage);
                    }
                }
            }
        }

        // 版画风格
        private void ApplyPrintStyle(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Gray, byte> grayImage = cvImage.Convert<Gray, byte>())
            using (Image<Gray, byte> edgeImage = new Image<Gray, byte>(grayImage.Size))
            using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
            {
                // 检测边缘
                CvInvoke.Canny(grayImage, edgeImage, 50, 150);

                // 创建版画效果：深色边缘 + 简化色彩
                for (int y = rect.Top; y < rect.Bottom && y < resultImage.Rows; y++)
                {
                    for (int x = rect.Left; x < rect.Right && x < resultImage.Cols; x++)
                    {
                        if (edgeImage[y, x].Intensity > 100)
                        {
                            // 边缘区域为黑色
                            resultImage[y, x] = new Bgr(0, 0, 0);
                        }
                        else
                        {
                            // 非边缘区域，简化色彩
                            Bgr original = cvImage[y, x];

                            // 色彩量化，减少颜色数量
                            byte blue = (byte)((original.Blue / 64) * 64);
                            byte green = (byte)((original.Green / 64) * 64);
                            byte red = (byte)((original.Red / 64) * 64);

                            resultImage[y, x] = new Bgr(blue, green, red);
                        }
                    }
                }

                ApplyEffectToSelection(bitmap, rect, resultImage);
            }
        }

        // 插画风格
        private void ApplyIllustration(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
            using (Image<Gray, byte> grayImage = cvImage.Convert<Gray, byte>())
            using (Image<Gray, byte> edgeImage = new Image<Gray, byte>(grayImage.Size))
            {
                // 检测边缘
                CvInvoke.Canny(grayImage, edgeImage, 50, 150);

                // 色彩量化，减少颜色数量，创建插画效果
                using (Image<Bgr, byte> quantizedImage = new Image<Bgr, byte>(cvImage.Size))
                {
                    for (int y = rect.Top; y < rect.Bottom && y < cvImage.Rows; y++)
                    {
                        for (int x = rect.Left; x < rect.Right && x < cvImage.Cols; x++)
                        {
                            Bgr pixel = cvImage[y, x];

                            // 色彩量化，将每个通道减少为4个级别
                            pixel.Blue = (byte)((pixel.Blue / 64) * 64 + 32);
                            pixel.Green = (byte)((pixel.Green / 64) * 64 + 32);
                            pixel.Red = (byte)((pixel.Red / 64) * 64 + 32);

                            quantizedImage[y, x] = pixel;
                        }
                    }

                    // 将边缘叠加到量化后的图像上
                    for (int y = rect.Top; y < rect.Bottom && y < resultImage.Rows; y++)
                    {
                        for (int x = rect.Left; x < rect.Right && x < resultImage.Cols; x++)
                        {
                            if (edgeImage[y, x].Intensity > 100)
                            {
                                // 边缘区域为黑色
                                resultImage[y, x] = new Bgr(0, 0, 0);
                            }
                            else
                            {
                                // 非边缘区域使用量化后的颜色
                                resultImage[y, x] = quantizedImage[y, x];
                            }
                        }
                    }

                    ApplyEffectToSelection(bitmap, rect, resultImage);
                }
            }
        }

        // 印象派风格
        private void ApplyImpressionist(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
            {
                // 印象派效果：模拟点彩画法，使用中值滤波和轻微模糊
                CvInvoke.MedianBlur(cvImage, resultImage, 5);

                // 应用轻微的高斯模糊，模拟印象派的笔触
                CvInvoke.GaussianBlur(resultImage, resultImage, new Size(3, 3), 0);

                // 增强色彩饱和度，使颜色更加鲜艳
                using (Image<Hsv, byte> hsvImage = resultImage.Convert<Hsv, byte>())
                {
                    for (int y = rect.Top; y < rect.Bottom && y < hsvImage.Rows; y++)
                    {
                        for (int x = rect.Left; x < rect.Right && x < hsvImage.Cols; x++)
                        {
                            Hsv pixel = hsvImage[y, x];
                            MCvScalar scalar = pixel.MCvScalar;
                            scalar.V1 = Math.Min(255, scalar.V1 * 1.3); // 增加饱和度
                            pixel.MCvScalar = scalar;
                            hsvImage[y, x] = pixel;
                        }
                    }

                    using (Image<Bgr, byte> finalImage = hsvImage.Convert<Bgr, byte>())
                    {
                        ApplyEffectToSelection(bitmap, rect, finalImage);
                    }
                }
            }
        }

        // 光影特效
        private void ApplyLightEffect(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            {
                // 创建光源效果 (从右上角到左下角的渐变光)
                using (Image<Bgr, byte> lightEffect = new Image<Bgr, byte>(cvImage.Size))
                {
                    for (int y = 0; y < cvImage.Rows; y++)
                    {
                        for (int x = 0; x < cvImage.Cols; x++)
                        {
                            // 计算光源强度 (从右上角到左下角递减)
                            float intensity = 1.0f - (float)Math.Sqrt(Math.Pow(x - cvImage.Cols, 2) + Math.Pow(y, 2)) /
                                              (float)Math.Sqrt(Math.Pow(cvImage.Cols, 2) + Math.Pow(cvImage.Rows, 2));

                            // 光源颜色为暖黄色
                            byte lightValue = (byte)(intensity * 50);
                            lightEffect[y, x] = new Bgr(lightValue, lightValue + 20, lightValue + 30);
                        }
                    }

                    // 混合光源效果和原图
                    using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
                    {
                        CvInvoke.AddWeighted(cvImage, 1, lightEffect, 0.7, 0, resultImage);
                        ApplyEffectToSelection(bitmap, rect, resultImage);
                    }
                }
            }
        }

       

        // 复古胶片颗粒
        private void ApplyFilmGrain(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            {
                // 创建随机噪声
                using (Image<Bgr, byte> noiseImage = new Image<Bgr, byte>(cvImage.Size))
                {
                    CvInvoke.Randn(noiseImage, new MCvScalar(0, 0, 0), new MCvScalar(15, 15, 15));

                    // 混合噪声和原图
                    using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
                    {
                        CvInvoke.AddWeighted(cvImage, 1, noiseImage, 0.3, 0, resultImage);
                        ApplyEffectToSelection(bitmap, rect, resultImage);
                    }
                }
            }
        }

        // 暖色调风格
        private void ApplyWarmTone(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Hsv, byte> hsvImage = cvImage.Convert<Hsv, byte>())
            {
                // 调整色调、饱和度和亮度
                for (int y = rect.Top; y < rect.Bottom && y < hsvImage.Rows; y++)
                {
                    for (int x = rect.Left; x < rect.Right && x < hsvImage.Cols; x++)
                    {
                        Hsv pixel = hsvImage[y, x];
                        MCvScalar scalar = pixel.MCvScalar;

                        // 向暖色偏移 (增加黄色和红色成分)
                        scalar.V0 = (scalar.V0 + 10) % 180;      // 调整色调
                        scalar.V1 = Math.Min(255, scalar.V1 * 1.2); // 增加饱和度
                        scalar.V2 = Math.Min(255, scalar.V2 * 1.1); // 轻微增加亮度

                        pixel.MCvScalar = scalar;
                        hsvImage[y, x] = pixel;
                    }
                }

                using (Image<Bgr, byte> resultImage = hsvImage.Convert<Bgr, byte>())
                {
                    ApplyEffectToSelection(bitmap, rect, resultImage);
                }
            }
        }

        // 波普艺术风格
        private void ApplyPopArt(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Gray, byte> grayImage = cvImage.Convert<Gray, byte>())
            {
                // 二值化处理
                using (Image<Gray, byte> binaryImage = new Image<Gray, byte>(grayImage.Size))
                {
                    CvInvoke.Threshold(grayImage, binaryImage, 128, 255, ThresholdType.Binary);

                    // 创建波普艺术效果 (黑白二值化 + 颜色简化)
                    using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
                    {
                        for (int y = rect.Top; y < rect.Bottom && y < cvImage.Rows; y++)
                        {
                            for (int x = rect.Left; x < rect.Right && x < cvImage.Cols; x++)
                            {
                                if (binaryImage[y, x].Intensity > 128)
                                {
                                    // 亮区使用鲜艳颜色
                                    resultImage[y, x] = new Bgr(0, 255, 255); // 青色
                                }
                                else
                                {
                                    // 暗区使用对比色
                                    resultImage[y, x] = new Bgr(255, 0, 0); // 蓝色
                                }
                            }
                        }

                        ApplyEffectToSelection(bitmap, rect, resultImage);
                    }
                }
            }
        }

        // 中国水墨画风格
        private void ApplyInkPainting(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            using (Image<Gray, byte> grayImage = cvImage.Convert<Gray, byte>())
            {
                // 高斯模糊减少细节
                using (Image<Gray, byte> blurredImage = new Image<Gray, byte>(grayImage.Size))
                {
                    CvInvoke.GaussianBlur(grayImage, blurredImage, new Size(5, 5), 0);

                    // 边缘检测
                    using (Image<Gray, byte> edgeImage = new Image<Gray, byte>(grayImage.Size))
                    {
                        CvInvoke.Canny(blurredImage, edgeImage, 50, 150);

                        // 创建水墨画效果 (黑白色调)
                        using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
                        {
                            for (int y = rect.Top; y < rect.Bottom && y < resultImage.Rows; y++)
                            {
                                for (int x = rect.Left; x < rect.Right && x < resultImage.Cols; x++)
                                {
                                    if (edgeImage[y, x].Intensity > 100)
                                    {
                                        // 边缘区域为黑色 (模拟墨水)
                                        resultImage[y, x] = new Bgr(0, 0, 0);
                                    }
                                    else
                                    {
                                        // 非边缘区域为灰色 (模拟宣纸)
                                        byte gray = (byte)(blurredImage[y, x].Intensity * 0.3);
                                        resultImage[y, x] = new Bgr(gray, gray, gray);
                                    }
                                }
                            }

                            ApplyEffectToSelection(bitmap, rect, resultImage);
                        }
                    }
                }
            }
        }

        // 雪景效果
        private void ApplySnow(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            {
                // 创建雪花噪声
                using (Image<Bgr, byte> snowNoise = new Image<Bgr, byte>(cvImage.Size))
                {
                    CvInvoke.Randn(snowNoise, new MCvScalar(0, 0, 0), new MCvScalar(20, 20, 20));

                    // 二值化噪声，只保留亮像素作为雪花
                    using (Image<Gray, byte> grayNoise = snowNoise.Convert<Gray, byte>())
                    using (Image<Gray, byte> binaryNoise = new Image<Gray, byte>(grayNoise.Size))
                    {
                        CvInvoke.Threshold(grayNoise, binaryNoise, 200, 255, ThresholdType.Binary);

                        // 混合雪花和原图
                        using (Image<Bgr, byte> resultImage = new Image<Bgr, byte>(cvImage.Size))
                        {
                            for (int y = rect.Top; y < rect.Bottom && y < cvImage.Rows; y++)
                            {
                                for (int x = rect.Left; x < rect.Right && x < cvImage.Cols; x++)
                                {
                                    if (binaryNoise[y, x].Intensity > 128)
                                    {
                                        // 雪花区域增加亮度
                                        Bgr pixel = cvImage[y, x];
                                        resultImage[y, x] = new Bgr(
                                            Math.Min(255, pixel.Blue + 30),
                                            Math.Min(255, pixel.Green + 30),
                                            Math.Min(255, pixel.Red + 30)
                                        );
                                    }
                                    else
                                    {
                                        // 非雪花区域保持原样
                                        resultImage[y, x] = cvImage[y, x];
                                    }
                                }
                            }

                            ApplyEffectToSelection(bitmap, rect, resultImage);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 复古褐
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="rect"></param>
        private void ApplySepia(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            {
                using (Image<Bgr, byte> sepiaImage = new Image<Bgr, byte>(cvImage.Size))
                {
                    // 复古效果矩阵
                    double[,] sepiaMatrix = {
                {0.393, 0.769, 0.189}, // 红色通道
                {0.349, 0.686, 0.168}, // 绿色通道
                {0.272, 0.534, 0.131}  // 蓝色通道
            };

                    // 应用复古矩阵
                    for (int y = rect.Top; y < rect.Bottom && y < cvImage.Rows; y++)
                    {
                        for (int x = rect.Left; x < rect.Right && x < cvImage.Cols; x++)
                        {
                            Bgr pixel = cvImage[y, x];
                            double r = pixel.Red * sepiaMatrix[0, 0] + pixel.Green * sepiaMatrix[0, 1] + pixel.Blue * sepiaMatrix[0, 2];
                            double g = pixel.Red * sepiaMatrix[1, 0] + pixel.Green * sepiaMatrix[1, 1] + pixel.Blue * sepiaMatrix[1, 2];
                            double b = pixel.Red * sepiaMatrix[2, 0] + pixel.Green * sepiaMatrix[2, 1] + pixel.Blue * sepiaMatrix[2, 2];

                            // 确保值在0-255范围内
                            sepiaImage[y, x] = new Bgr(
                                (byte)Math.Min(255, Math.Max(0, b)),
                                (byte)Math.Min(255, Math.Max(0, g)),
                                (byte)Math.Min(255, Math.Max(0, r))
                            );
                        }
                    }

                    ApplyEffectToSelection(bitmap, rect, sepiaImage);
                }
            }
        }

        /// <summary>
        /// 应用复古棕褐色调效果
        /// </summary>
        private void ApplySepiaTone(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            {
                using (Image<Bgr, byte> sepiaImage = new Image<Bgr, byte>(cvImage.Size))
                {
                    // 复古棕褐色调转换矩阵（比普通Sepia更深的棕色调）
                    double[,] sepiaMatrix = {
                {0.393, 0.769, 0.189}, // 红色通道
                {0.349, 0.686, 0.168}, // 绿色通道
                {0.272, 0.534, 0.131}  // 蓝色通道
            };

                    // 应用棕褐色调转换
                    for (int y = rect.Top; y < rect.Bottom && y < cvImage.Rows; y++)
                    {
                        for (int x = rect.Left; x < rect.Right && x < cvImage.Cols; x++)
                        {
                            Bgr pixel = cvImage[y, x];
                            double r = pixel.Red * sepiaMatrix[0, 0] + pixel.Green * sepiaMatrix[0, 1] + pixel.Blue * sepiaMatrix[0, 2];
                            double g = pixel.Red * sepiaMatrix[1, 0] + pixel.Green * sepiaMatrix[1, 1] + pixel.Blue * sepiaMatrix[1, 2];
                            double b = pixel.Red * sepiaMatrix[2, 0] + pixel.Green * sepiaMatrix[2, 1] + pixel.Blue * sepiaMatrix[2, 2];

                            // 确保值在0-255范围内并增强对比度
                            sepiaImage[y, x] = new Bgr(
                                (byte)Math.Min(255, Math.Max(0, b * 1.1)),
                                (byte)Math.Min(255, Math.Max(0, g * 1.05)),
                                (byte)Math.Min(255, Math.Max(0, r * 1.0))
                            );
                        }
                    }

                    ApplyEffectToSelection(bitmap, rect, sepiaImage);
                }
            }
        }

        /// <summary>
        /// 应用减少杂色效果
        /// </summary>
        private void ApplyDenoise(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            {
                using (Image<Bgr, byte> denoisedImage = new Image<Bgr, byte>(cvImage.Size))
                {
                    // 方法1：中值滤波（有效去除椒盐噪声）
                    CvInvoke.MedianBlur(cvImage, denoisedImage, 5);

                    // 方法2：双边滤波（保留边缘的同时去噪）
                    // CvInvoke.BilateralFilter(cvImage, denoisedImage, 9, 75, 75);

                    ApplyEffectToSelection(bitmap, rect, denoisedImage);
                }
            }
        }

        /// <summary>
        /// 应用老照片风格效果
        /// </summary>
        private void ApplyOldPhoto(Bitmap bitmap, Rectangle rect)
        {
            using (Image<Bgr, byte> cvImage = BitmapToImageBgr(bitmap))
            {
                // 1. 应用复古棕色调
                using (Image<Bgr, byte> sepiaImage = new Image<Bgr, byte>(cvImage.Size))
                {
                    double[,] sepiaMatrix = {
                {0.393, 0.769, 0.189},
                {0.349, 0.686, 0.168},
                {0.272, 0.534, 0.131}
            };

                    for (int y = rect.Top; y < rect.Bottom && y < cvImage.Rows; y++)
                    {
                        for (int x = rect.Left; x < rect.Right && x < cvImage.Cols; x++)
                        {
                            Bgr pixel = cvImage[y, x];
                            double r = pixel.Red * sepiaMatrix[0, 0] + pixel.Green * sepiaMatrix[0, 1] + pixel.Blue * sepiaMatrix[0, 2];
                            double g = pixel.Red * sepiaMatrix[1, 0] + pixel.Green * sepiaMatrix[1, 1] + pixel.Blue * sepiaMatrix[1, 2];
                            double b = pixel.Red * sepiaMatrix[2, 0] + pixel.Green * sepiaMatrix[2, 1] + pixel.Blue * sepiaMatrix[2, 2];

                            sepiaImage[y, x] = new Bgr(
                                (byte)Math.Min(255, Math.Max(0, b)),
                                (byte)Math.Min(255, Math.Max(0, g)),
                                (byte)Math.Min(255, Math.Max(0, r))
                            );
                        }
                    }

                    // 2. 添加暗角效果
                    using (Image<Bgr, byte> vignetteImage = new Image<Bgr, byte>(sepiaImage.Size))
                    {
                        for (int y = 0; y < sepiaImage.Rows; y++)
                        {
                            for (int x = 0; x < sepiaImage.Cols; x++)
                            {
                                double dx = x - sepiaImage.Width / 2.0;
                                double dy = y - sepiaImage.Height / 2.0;
                                double r = Math.Sqrt(dx * dx + dy * dy) /
                                          Math.Sqrt((sepiaImage.Width / 2.0) * (sepiaImage.Height / 2.0));

                                float factor = (float)(0.7 + 0.3 * (1 - r));
                                Bgr pixel = sepiaImage[y, x];
                                vignetteImage[y, x] = new Bgr(
                                    (byte)(pixel.Blue * factor),
                                    (byte)(pixel.Green * factor),
                                    (byte)(pixel.Red * factor)
                                );
                            }
                        }

                        // 3. 添加轻微噪点模拟老照片质感
                        using (Image<Bgr, byte> noiseImage = new Image<Bgr, byte>(vignetteImage.Size))
                        {
                            CvInvoke.Randn(noiseImage, new MCvScalar(0, 0, 0), new MCvScalar(5, 5, 5));
                            CvInvoke.AddWeighted(vignetteImage, 0.95, noiseImage, 0.05, 0, vignetteImage);

                            ApplyEffectToSelection(bitmap, rect, vignetteImage);
                        }
                    }
                }
            }
        }

        



    }
}