using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp.MyOpenCV.DLL;
using SelectionMode = WinFormsApp.MyOpenCV.DLL.SelectionMode;

namespace WinFormsApp.MyOpenCV.EmguCV
{
    public partial class CandyImage2 : Form
    {
        // 控件声明
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
        private float zoomFactor = 1.0f;
        private const float ZoomStep = 0.1f;
        private const float MinZoom = 0.1f;
        private const float MaxZoom = 5.0f;

        private readonly Brush selectionFillBrush = new SolidBrush(Color.FromArgb(70, 255, 0, 0));
        private readonly Pen selectionPen = new Pen(Color.Red, 2) { DashStyle = DashStyle.Dash };

        // 下拉框选项抽离
        private readonly string[] effectOptions = {
            "无效果", "马赛克", "模糊", "锐化", "浮雕", "高斯模糊", "灰度化", "反色",
            "亮度调整", "对比度调整", "卡通", "复古", "边缘检测", "直方图均衡化", "膨胀",
            "腐蚀", "开运算", "闭运算", "锐化增强", "色彩平衡", "模糊增强", "色调调整",
            "饱和度调整", "模糊边缘检测", "素描", "水彩", "霓虹", "提高色彩饱和度和对比度",
            "智能锐化", "水彩画", "版画", "插画", "印象派", "光影", "复古胶片颗粒", "暖色调",
            "波普艺术", "中国水墨画", "雪景", "复古褐", "复古棕褐", "减少杂色", "老照片"
        };

        private readonly string[] selectionModeOptions = { "矩形选择", "椭圆选择" };

        public CandyImage2()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            // 加载下拉框选项
            LoadComboBoxItems();

            // 设置状态栏初始文本
            UpdateStatus("就绪");

            // 启用双缓冲减少闪烁
            picBox.DoubleBuffered(true);
            imagePanel.DoubleBuffered(true);
        }
        // 扩展方法实现双缓冲


        private void LoadComboBoxItems()
        {
            // 加载选择模式选项
            modeComboBox.Items.AddRange(selectionModeOptions);
            modeComboBox.SelectedIndex = 0;

            // 加载特效选项
            effectComboBox.Items.AddRange(effectOptions);
            effectComboBox.SelectedIndex = 0;
        }

        private void UpdateStatus(string message)
        {
            statusLbl.Text = message;
            Console.WriteLine(message);
        }

        #region 菜单和工具栏事件

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
                        zoomFactor = 1.0f;
                        UpdatePictureBoxSize();
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
                        ImageTransformation.ReplaceColor(modifiedImage, targetColor, replaceColor);
                    }
                    else
                    {
                        CreateSelectionMask();
                        ImageTransformation.ReplaceColorInSelection(modifiedImage, targetColor, replaceColor, selectionRect, IsPointInSelection);
                    }

                    UpdateStatus($"已将 {targetColor.Name} 替换为 {replaceColor.Name}");
                    picBox.Invalidate();
                }
            }
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
            picBox.Cursor = Cursors.Cross;
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
            picBox.Cursor = Cursors.Default;
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
                            ImageTransformation.ApplyImageInSelection(modifiedImage, resizedImage, selectionRect, IsPointInSelection);

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
                ImageTransformation.ApplyEffect(effect, modifiedImage, selectionRect, IsPointInSelection);
                UpdateStatus($"已应用 {effectComboBox.SelectedItem} 效果");
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

                // 重置选区和缩放
                selectionRect = Rectangle.Empty;
                zoomFactor = 1.0f;
                UpdatePictureBoxSize();

                UpdateStatus("已截取图片");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"截取图片时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateStatus("截取图片失败");
            }
        }

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
                        ImageTransformation.ReplaceColorRange(modifiedImage,
                            colorRangeDialog.MinR, colorRangeDialog.MaxR,
                            colorRangeDialog.MinG, colorRangeDialog.MaxG,
                            colorRangeDialog.MinB, colorRangeDialog.MaxB,
                            colorRangeDialog.TargetColor);
                    }
                    else
                    {
                        // 仅对选区应用范围颜色替换
                        CreateSelectionMask();
                        ImageTransformation.ReplaceColorRangeInSelection(modifiedImage,
                            colorRangeDialog.MinR, colorRangeDialog.MaxR,
                            colorRangeDialog.MinG, colorRangeDialog.MaxG,
                            colorRangeDialog.MinB, colorRangeDialog.MaxB,
                            colorRangeDialog.TargetColor, selectionRect, IsPointInSelection);
                    }

                    UpdateStatus("已完成范围颜色替换");
                    picBox.Invalidate();
                }
            }
        }

        #endregion

        #region 图片缩放功能

        private void ZoomInButton_Click(object sender, EventArgs e)
        {
            if (modifiedImage != null && zoomFactor < MaxZoom)
            {
                zoomFactor += ZoomStep;
                UpdatePictureBoxSize();
                UpdateStatus($"缩放: {Math.Round(zoomFactor * 100)}%");
            }
        }

        private void ZoomOutButton_Click(object sender, EventArgs e)
        {
            if (modifiedImage != null && zoomFactor > MinZoom)
            {
                zoomFactor -= ZoomStep;
                UpdatePictureBoxSize();
                UpdateStatus($"缩放: {Math.Round(zoomFactor * 100)}%");
            }
        }

        private void ZoomResetButton_Click(object sender, EventArgs e)
        {
            if (modifiedImage != null)
            {
                zoomFactor = 1.0f;
                UpdatePictureBoxSize();
                UpdateStatus("缩放已重置为 100%");
            }
        }

        private void UpdatePictureBoxSize()
        {
            if (modifiedImage != null)
            {
                picBox.Width = (int)(modifiedImage.Width * zoomFactor);
                picBox.Height = (int)(modifiedImage.Height * zoomFactor);
                picBox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void PicBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (modifiedImage == null) return;

            // 滚轮向前滚动放大，向后滚动缩小
            if (e.Delta > 0 && zoomFactor < MaxZoom)
            {
                zoomFactor += ZoomStep;
            }
            else if (e.Delta < 0 && zoomFactor > MinZoom)
            {
                zoomFactor -= ZoomStep;
            }

            UpdatePictureBoxSize();
            UpdateStatus($"缩放: {Math.Round(zoomFactor * 100)}%");
        }

        #endregion

        #region 鼠标事件处理

        private void ImageBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (modifiedImage == null) return;

            // 将鼠标坐标转换为图片实际坐标
            Point imagePoint = ConvertToImageCoordinates(e.Location);

            if (isSelecting && e.Button == MouseButtons.Left)
            {
                startPoint = imagePoint;
                selectionRect = new Rectangle(imagePoint, System.Drawing.Size.Empty);
                picBox.Invalidate();
            }
            else if (isColorPickerActive && e.Button == MouseButtons.Left)
            {
                if (imagePoint.X >= 0 && imagePoint.X < modifiedImage.Width &&
                    imagePoint.Y >= 0 && imagePoint.Y < modifiedImage.Height)
                {
                    targetColor = modifiedImage.GetPixel(imagePoint.X, imagePoint.Y);
                    isColorPickerActive = false;
                    picBox.Cursor = Cursors.Default;

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
            if (modifiedImage == null || !isSelecting || e.Button != MouseButtons.Left) return;

            // 将鼠标坐标转换为图片实际坐标
            Point imagePoint = ConvertToImageCoordinates(e.Location);

            int x = Math.Min(startPoint.X, imagePoint.X);
            int y = Math.Min(startPoint.Y, imagePoint.Y);
            int width = Math.Abs(startPoint.X - imagePoint.X);
            int height = Math.Abs(startPoint.Y - imagePoint.Y);

            // 确保选区在图片范围内
            x = Math.Max(0, x);
            y = Math.Max(0, y);
            width = Math.Min(width, modifiedImage.Width - x);
            height = Math.Min(height, modifiedImage.Height - y);

            selectionRect = new Rectangle(x, y, width, height);
            picBox.Invalidate();
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
            if (!selectionRect.IsEmpty && modifiedImage != null)
            {
                // 计算缩放后的选区矩形
                RectangleF scaledRect = new RectangleF(
                    selectionRect.X * zoomFactor,
                    selectionRect.Y * zoomFactor,
                    selectionRect.Width * zoomFactor,
                    selectionRect.Height * zoomFactor);

                if (selectionMode == SelectionMode.Rectangle)
                {
                    e.Graphics.FillRectangle(selectionFillBrush, scaledRect);
                    e.Graphics.DrawRectangle(selectionPen, scaledRect.X, scaledRect.Y, scaledRect.Width, scaledRect.Height);
                }
                else
                {
                    e.Graphics.FillEllipse(selectionFillBrush, scaledRect);
                    e.Graphics.DrawEllipse(selectionPen, scaledRect.X, scaledRect.Y, scaledRect.Width, scaledRect.Height);
                }
            }
        }

        // 将控件坐标转换为图片实际坐标
        private Point ConvertToImageCoordinates(Point controlPoint)
        {
            if (modifiedImage == null) return Point.Empty;

            return new Point(
                (int)(controlPoint.X / zoomFactor),
                (int)(controlPoint.Y / zoomFactor)
            );
        }

        #endregion

        private void modeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectionMode = (SelectionMode)modeComboBox.SelectedIndex;
            UpdateStatus($"选择模式: {modeComboBox.SelectedItem}");
            picBox.Invalidate(); // 重绘选区
        }

        #region 窗口和控件事件

        private void CandyImage2_Load(object sender, EventArgs e)
        {
            // 窗口加载时的初始化
        }

        private void CandyImage2_Resize(object sender, EventArgs e)
        {
            // 窗口大小改变时重绘
            picBox.Invalidate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 关闭应用程序
            this.Close();
        }

        #endregion
    }

    public static class ControlExtensions
    {
        public static void DoubleBuffered(this Control control, bool enabled)
        {
            // 使用反射设置双缓冲属性
            typeof(Control).InvokeMember(
                "DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null,
                control,
                new object[] { enabled });
        }
    }
}
