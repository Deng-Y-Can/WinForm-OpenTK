using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV.CvEnum;
using System.IO;

namespace WinFormsApp.MyOpenCV.EmguCV
{
    public partial class CandyImage : Form
    {
        private Bitmap originalImage;
        private Bitmap modifiedImage;
        private Bitmap selectionMask;
        private Rectangle selectionRect = Rectangle.Empty;
        private Point startPoint;
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

        public enum ImageEffect
        {
            None,
            Mosaic,
            Blur
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
                "模糊"
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
    }
}