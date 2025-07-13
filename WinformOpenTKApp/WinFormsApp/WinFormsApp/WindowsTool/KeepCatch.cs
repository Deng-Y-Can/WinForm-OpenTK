using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp.CandyTool;

namespace WinFormsApp.WindowsTool
{
    public partial class KeepCatch : Form
    {
        private ScreenRecorder recorder;
        private string saveFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private Rectangle captureRect = Screen.PrimaryScreen.Bounds;
        private CaptureAreaType captureType = CaptureAreaType.FullScreen;
        private GraphicsPath customPath;
        private bool isSelectingRegion = false;
        private Label statusLabel;
        private ComboBox regionCombox;
        private PictureBox previewBox;
        private Button recordBtn;
        private Button captureBtn;
        private Button folderBtn;

        // 三角形选区相关变量
        private List<Point> trianglePoints = new List<Point>();
        private bool isSelectingTriangle = false;

        public KeepCatch()
        {
            InitializeComponent();
            
        }

        // 初始化UI组件
        private void InitializeUI()
        {
            this.Text = "KeepCatch 屏幕捕捉工具";
            this.Size = new Size(500, 400);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // 创建按钮面板
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                Padding = new Padding(10)
            };
            this.Controls.Add(panel);

            // 截图按钮
            captureBtn = new Button
            {
                Text = "截图",
                Width = 100,
                Height = 40,
                Location = new Point(10, 10)
            };
            captureBtn.Click += CaptureBtn_Click;
            panel.Controls.Add(captureBtn);

            // 录制按钮
            recordBtn = new Button
            {
                Text = "开始录制",
                Width = 100,
                Height = 40,
                Location = new Point(120, 10)
            };
            recordBtn.Click += RecordBtn_Click;
            panel.Controls.Add(recordBtn);

            // 选择保存文件夹按钮
            folderBtn = new Button
            {
                Text = "选择保存位置",
                Width = 120,
                Height = 40,
                Location = new Point(230, 10)
            };
            folderBtn.Click += FolderBtn_Click;
            panel.Controls.Add(folderBtn);

            // 区域选择下拉框
            regionCombox = new ComboBox
            {
                Width = 120,
                Height = 40,
                Location = new Point(360, 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            regionCombox.Items.AddRange(new object[] {
                "全屏", "矩形区域", "三角形区域", "圆形区域"
            });
            regionCombox.SelectedIndex = 0;
            regionCombox.SelectedIndexChanged += RegionCombo_SelectedIndexChanged;
            panel.Controls.Add(regionCombox);

            // 状态标签
            statusLabel = new Label
            {
                Text = "就绪",
                Dock = DockStyle.Bottom,
                Height = 30,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };
            this.Controls.Add(statusLabel);

            // 预览区域
            previewBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            this.Controls.Add(previewBox);

            // 窗体关闭时释放资源
            this.FormClosing += KeepCatchForm_FormClosing;
        }

        // 区域选择变更事件
        private void RegionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combo = sender as ComboBox;
            switch (combo.SelectedIndex)
            {
                case 0:
                    captureType = CaptureAreaType.FullScreen;
                    captureRect = Screen.PrimaryScreen.Bounds;
                    customPath = null;
                    trianglePoints.Clear();
                    isSelectingTriangle = false;
                    UpdatePreview();
                    statusLabel.Text = "已选择全屏";
                    break;
                case 1:
                    captureType = CaptureAreaType.Rectangle;
                    trianglePoints.Clear();
                    isSelectingTriangle = false;
                    StartRegionSelection();
                    break;
                case 2:
                    captureType = CaptureAreaType.Triangle;
                    trianglePoints.Clear();
                    isSelectingTriangle = false;
                    StartTriangleSelection();
                    break;
                case 3:
                    captureType = CaptureAreaType.Circle;
                    trianglePoints.Clear();
                    isSelectingTriangle = false;
                    StartRegionSelection();
                    break;
            }
        }

        // 开始区域选择
        private void StartRegionSelection()
        {
            isSelectingRegion = true;
            Cursor = Cursors.Cross;
            statusLabel.Text = "请在屏幕上绘制区域";

            // 隐藏主窗口，便于选择区域
            this.Hide();
            Thread.Sleep(200); // 等待窗口隐藏

            // 创建全屏透明选区窗体
            using (var selectionForm = new Form())
            {
                selectionForm.FormBorderStyle = FormBorderStyle.None;
                selectionForm.WindowState = FormWindowState.Maximized;
                selectionForm.BackColor = Color.Black;
                selectionForm.Opacity = 0.3; // 半透明背景
                selectionForm.TopMost = true;
                selectionForm.ShowInTaskbar = false;

                // 选区覆盖层
                var overlayPanel = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.Transparent
                };
                selectionForm.Controls.Add(overlayPanel);

                // 鼠标事件处理
                Point? startPoint = null;
                Rectangle currentRect = Rectangle.Empty;

                overlayPanel.MouseDown += (s, e) => {
                    startPoint = e.Location;
                    currentRect = new Rectangle(e.X, e.Y, 0, 0);
                };

                overlayPanel.MouseMove += (s, e) => {
                    if (startPoint.HasValue && e.Button == MouseButtons.Left)
                    {
                        currentRect = new Rectangle(
                            Math.Min(startPoint.Value.X, e.X),
                            Math.Min(startPoint.Value.Y, e.Y),
                            Math.Abs(e.X - startPoint.Value.X),
                            Math.Abs(e.Y - startPoint.Value.Y)
                        );
                        overlayPanel.Invalidate();
                    }
                };

                overlayPanel.MouseUp += (s, e) => {
                    if (startPoint.HasValue)
                    {
                        currentRect = new Rectangle(
                            Math.Min(startPoint.Value.X, e.X),
                            Math.Min(startPoint.Value.Y, e.Y),
                            Math.Abs(e.X - startPoint.Value.X),
                            Math.Abs(e.Y - startPoint.Value.Y)
                        );
                        selectionForm.DialogResult = DialogResult.OK;
                    }
                    selectionForm.Close();
                };

                overlayPanel.Paint += (s, e) => {
                    if (currentRect.Width > 0 && currentRect.Height > 0)
                    {
                        // 绘制半透明选区背景
                        using (var brush = new SolidBrush(Color.FromArgb(100, 0, 191, 255)))
                        {
                            e.Graphics.FillRectangle(brush, currentRect);
                        }

                        // 根据选择的形状绘制提示
                        switch (captureType)
                        {
                            case CaptureAreaType.Rectangle:
                                DrawRectanglePreview(e.Graphics, currentRect);
                                break;
                            case CaptureAreaType.Circle:
                                DrawCirclePreview(e.Graphics, currentRect);
                                break;
                        }
                    }
                };

                // 键盘交互
                selectionForm.KeyDown += (s, e) => {
                    if (e.KeyCode == Keys.Escape)
                    {
                        selectionForm.DialogResult = DialogResult.Cancel;
                        selectionForm.Close();
                    }
                };

                // 显示选区窗体
                var result = selectionForm.ShowDialog();
                this.Show();
                Cursor = Cursors.Default;

                if (result == DialogResult.OK && currentRect.Width > 0 && currentRect.Height > 0)
                {
                    captureRect = currentRect;

                    // 生成对应形状的路径
                    switch (captureType)
                    {
                        case CaptureAreaType.Rectangle:
                            customPath = new GraphicsPath();
                            customPath.AddRectangle(new Rectangle(0, 0, captureRect.Width, captureRect.Height));
                            break;
                        case CaptureAreaType.Circle:
                            customPath = new GraphicsPath();
                            customPath.AddEllipse(new Rectangle(0, 0, captureRect.Width, captureRect.Height));
                            break;
                    }

                    statusLabel.Text = $"已选择区域: {captureRect.Width}x{captureRect.Height}";
                    UpdatePreview();
                }
                else
                {
                    // 恢复默认设置
                    captureType = CaptureAreaType.FullScreen;
                    captureRect = Screen.PrimaryScreen.Bounds;
                    customPath = null;
                    regionCombox.SelectedIndex = 0;
                    statusLabel.Text = "区域选择已取消";
                }
            }
        }

        // 开始三角形选择
        private void StartTriangleSelection()
        {
            isSelectingTriangle = true;
            trianglePoints.Clear();
            Cursor = Cursors.Cross;
            statusLabel.Text = "请在屏幕上点击选择三角形的三个顶点";

            // 隐藏主窗口，便于选择区域
            this.Hide();
            Thread.Sleep(200); // 等待窗口隐藏

            // 创建全屏透明选区窗体
            using (var selectionForm = new Form())
            {
                selectionForm.FormBorderStyle = FormBorderStyle.None;
                selectionForm.WindowState = FormWindowState.Maximized;
                selectionForm.BackColor = Color.Black;
                selectionForm.Opacity = 0.3; // 半透明背景
                selectionForm.TopMost = true;
                selectionForm.ShowInTaskbar = false;

                // 选区覆盖层
                var overlayPanel = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.Transparent
                };
                selectionForm.Controls.Add(overlayPanel);

                // 鼠标点击事件处理
                overlayPanel.MouseClick += (s, e) => {
                    if (e.Button == MouseButtons.Left)
                    {
                        trianglePoints.Add(e.Location);
                        overlayPanel.Invalidate();

                        if (trianglePoints.Count == 3)
                        {
                            // 计算三角形的边界矩形
                            int minX = trianglePoints.Min(p => p.X);
                            int minY = trianglePoints.Min(p => p.Y);
                            int maxX = trianglePoints.Max(p => p.X);
                            int maxY = trianglePoints.Max(p => p.Y);

                            captureRect = new Rectangle(minX, minY, maxX - minX, maxY - minY);

                            // 创建三角形路径（相对于选区坐标系）
                            customPath = new GraphicsPath();
                            customPath.AddPolygon(new Point[] {
                                new Point(trianglePoints[0].X - minX, trianglePoints[0].Y - minY),
                                new Point(trianglePoints[1].X - minX, trianglePoints[1].Y - minY),
                                new Point(trianglePoints[2].X - minX, trianglePoints[2].Y - minY)
                            });

                            selectionForm.DialogResult = DialogResult.OK;
                            selectionForm.Close();
                        }
                        else
                        {
                            statusLabel.Text = $"已选择 {trianglePoints.Count}/3 个顶点";
                        }
                    }
                };

                overlayPanel.Paint += (s, e) => {
                    if (trianglePoints.Count > 0)
                    {
                        // 绘制已选顶点
                        using (var brush = new SolidBrush(Color.Yellow))
                        {
                            foreach (var point in trianglePoints)
                            {
                                e.Graphics.FillEllipse(brush, point.X - 5, point.Y - 5, 10, 10);
                            }
                        }

                        // 绘制已形成的边
                        if (trianglePoints.Count > 1)
                        {
                            using (var pen = new Pen(Color.Yellow, 2))
                            {
                                for (int i = 0; i < trianglePoints.Count - 1; i++)
                                {
                                    e.Graphics.DrawLine(pen, trianglePoints[i], trianglePoints[i + 1]);
                                }

                                // 如果已经有3个点，连接第三个点和第一个点形成闭合三角形
                                if (trianglePoints.Count == 3)
                                {
                                    e.Graphics.DrawLine(pen, trianglePoints[2], trianglePoints[0]);

                                    // 填充半透明三角形
                                    using (var fillBrush = new SolidBrush(Color.FromArgb(100, 0, 191, 255)))
                                    {
                                        e.Graphics.FillPolygon(fillBrush, trianglePoints.ToArray());
                                    }
                                }
                            }
                        }
                    }
                };

                // 键盘交互
                selectionForm.KeyDown += (s, e) => {
                    if (e.KeyCode == Keys.Escape)
                    {
                        selectionForm.DialogResult = DialogResult.Cancel;
                        selectionForm.Close();
                    }
                };

                // 显示选区窗体
                var result = selectionForm.ShowDialog();
                this.Show();
                Cursor = Cursors.Default;

                if (result == DialogResult.OK && trianglePoints.Count == 3)
                {
                    statusLabel.Text = $"已选择三角形区域: {captureRect.Width}x{captureRect.Height}";
                    UpdatePreview();
                }
                else
                {
                    // 恢复默认设置
                    captureType = CaptureAreaType.FullScreen;
                    captureRect = Screen.PrimaryScreen.Bounds;
                    customPath = null;
                    regionCombox.SelectedIndex = 0;
                    statusLabel.Text = "区域选择已取消";
                }
            }
        }

        // 绘制矩形预览
        private void DrawRectanglePreview(Graphics g, Rectangle rect)
        {
            using (var pen = new Pen(Color.Yellow, 2))
            {
                g.DrawRectangle(pen, rect);

                // 绘制选区角落标记
                DrawCornerMarkers(g, rect);
            }
        }

        // 绘制圆形预览
        private void DrawCirclePreview(Graphics g, Rectangle rect)
        {
            using (var pen = new Pen(Color.Yellow, 2))
            {
                g.DrawEllipse(pen, rect);

                // 绘制中心点和边缘标记
                DrawCircleMarkers(g, rect);
            }
        }

        // 绘制角落标记
        private void DrawCornerMarkers(Graphics g, Rectangle rect)
        {
            int size = 10;
            using (var brush = new SolidBrush(Color.Yellow))
            {
                g.FillRectangle(brush, rect.Left - size / 2, rect.Top - size / 2, size, size);
                g.FillRectangle(brush, rect.Right - size / 2, rect.Top - size / 2, size, size);
                g.FillRectangle(brush, rect.Left - size / 2, rect.Bottom - size / 2, size, size);
                g.FillRectangle(brush, rect.Right - size / 2, rect.Bottom - size / 2, size, size);
            }
        }

        // 绘制圆形标记
        private void DrawCircleMarkers(Graphics g, Rectangle rect)
        {
            int size = 10;
            using (var brush = new SolidBrush(Color.Yellow))
            {
                // 中心点
                Point center = new Point(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
                g.FillEllipse(brush, center.X - size / 4, center.Y - size / 4, size / 2, size / 2);

                // 边缘标记点
                Point[] points = new Point[] {
                    new Point(center.X, rect.Top),
                    new Point(center.X, rect.Bottom),
                    new Point(rect.Left, center.Y),
                    new Point(rect.Right, center.Y)
                };

                foreach (var point in points)
                {
                    g.FillEllipse(brush, point.X - size / 3, point.Y - size / 3, size / 1.5f, size / 1.5f);
                }
            }
        }

        // 更新预览
        private void UpdatePreview()
        {
            try
            {
                using (var bitmap = ScreenCapture.CaptureScreen(captureRect, captureType, customPath))
                {
                    previewBox.Image?.Dispose();
                    previewBox.Image = (Image)bitmap.Clone();
                }
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"预览失败: {ex.Message}";
            }
        }

        // 截图按钮点击事件
        private void CaptureBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var fileName = $"pic_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                var filePath = Path.Combine(saveFolder, fileName);

                ScreenCapture.CaptureAndSave(filePath, captureRect, captureType, customPath);

                statusLabel.Text = $"截图已保存至: {filePath}";
                System.Media.SystemSounds.Asterisk.Play();

                // 更新预览
                UpdatePreview();
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"截图失败: {ex.Message}";
                MessageBox.Show($"截图失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 录制按钮点击事件
        private void RecordBtn_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;

            if (recorder == null || !recorder.IsRecording)
            {
                try
                {
                    var fileName = $"video_{DateTime.Now:yyyyMMdd_HHmmss}.avi";
                    var filePath = Path.Combine(saveFolder, fileName);

                    // 检查文件是否可写
                    CheckFileWritability(filePath);

                    // 创建带有状态回调的录制器
                    recorder = new ScreenRecorder(filePath, 15, captureRect, captureType, customPath, UpdateStatus);
                    recorder.Start();

                    btn.Text = "停止录制";
                    System.Media.SystemSounds.Beep.Play();
                }
                catch (Exception ex)
                {
                    statusLabel.Text = $"录制失败: {ex.Message}";
                    MessageBox.Show($"录制失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    recorder.Stop();
                    recorder.Dispose();
                    recorder = null;

                    btn.Text = "开始录制";
                    System.Media.SystemSounds.Asterisk.Play();
                }
                catch (Exception ex)
                {
                    statusLabel.Text = $"停止录制失败: {ex.Message}";
                    MessageBox.Show($"停止录制失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 检查文件是否可写
        private void CheckFileWritability(string filePath)
        {
            try
            {
                using (File.Create(filePath, 1, FileOptions.DeleteOnClose)) { }
            }
            catch (Exception ex)
            {
                throw new IOException($"无法写入文件: {filePath}。错误: {ex.Message}");
            }
        }

        // 更新状态标签
        private void UpdateStatus(string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => {
                    statusLabel.Text = message;
                }));
            }
            else
            {
                statusLabel.Text = message;
            }
        }

        // 选择保存文件夹按钮点击事件
        private void FolderBtn_Click(object sender, EventArgs e)
        {
            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.SelectedPath = saveFolder;
                folderDialog.Description = "选择保存位置";

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    saveFolder = folderDialog.SelectedPath;
                    statusLabel.Text = $"保存位置已设置为: {saveFolder}";
                }
            }
        }

        // 窗体关闭时释放资源
        private void KeepCatchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                recorder?.Stop();
                recorder?.Dispose();
                recorder = null;

                previewBox.Image?.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"关闭窗体时出错: {ex.Message}");
            }
        }
    }

}
