
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Size = System.Drawing.Size;
using Timer = System.Windows.Forms.Timer;

namespace WinFormsApp.WindowsTool
{
    public partial class Fish : Form
    {
        private string[] lines;
        private int currentPage = 0;
        private int totalPages = 0;
        private string settingsFile = "settings.ini";
        private string iconFile = ""; // 图标文件路径
        private DisplayMode displayMode = DisplayMode.BalloonTip; // 默认显示模式：通知气泡
        private string currentTaskbarText = "";
        private int taskbarTextOffset = 0;
        private Timer taskbarScrollTimer;
        private bool isTaskbarAtBottom = true; // 任务栏默认在底部
        private TaskbarPosition taskbarPosition = TaskbarPosition.Left; // 任务栏文本显示位置
        private bool isBalloonVisible = false; // 跟踪气泡是否可见

        // 任务栏文本样式设置
        private Color taskbarTextColor = Color.Green;
        private Color taskbarBackColor = Color.Black;
        private Font taskbarFont = new Font("Arial", 9);

        // 锁定功能相关
        private LockState lockState = LockState.Unlocked; // 默认未锁定
        private Point mouseDownLocation; // 鼠标按下位置
        private bool isDragging = false; // 拖动状态

        // 显示模式枚举
        private enum DisplayMode
        {
            BalloonTip,    // 通知气泡模式
            TaskbarText    // 任务栏文本模式
        }

        // 任务栏文本显示位置枚举
        private enum TaskbarPosition
        {
            Left,
            Right
        }

        // 锁定状态枚举
        private enum LockState
        {
            Unlocked,  // 未锁定，可拖动
            Locked     // 锁定，不可拖动
        }

        // Windows API 声明
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("shell32.dll")]
        private static extern IntPtr SHAppBarMessage(int dwMessage, ref APPBARDATA pData);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct APPBARDATA
        {
            public int cbSize;
            public IntPtr hWnd;
            public int uCallbackMessage;
            public int uEdge;
            public RECT rc;
            public IntPtr lParam;
        }

        private const int ABM_GETTASKBARPOS = 5;

        public Fish()
        {
            InitializeComponent();
            btnStop.Enabled = false;

            // 尝试加载默认图标
            LoadIcon();

            // 初始化任务栏文本滚动定时器
            taskbarScrollTimer = new Timer();
            taskbarScrollTimer.Interval = 100; // 每100毫秒滚动一次
            taskbarScrollTimer.Tick += TaskbarScrollTimer_Tick;

            // 初始化任务栏文本窗体
            taskbarTextForm.Paint += TaskbarTextForm_Paint;
            taskbarTextForm.MouseClick += TaskbarTextForm_MouseClick;
            taskbarTextForm.MouseDown += TaskbarTextForm_MouseDown;
            taskbarTextForm.MouseMove += TaskbarTextForm_MouseMove;
            taskbarTextForm.MouseUp += TaskbarTextForm_MouseUp;

            // 设置任务栏文本窗体为透明
            taskbarTextForm.FormBorderStyle = FormBorderStyle.None;
            taskbarTextForm.BackColor = taskbarBackColor;
            taskbarTextForm.TransparencyKey = taskbarBackColor;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 确保托盘图标可见
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(2000, "TXT 通知工具", "程序已启动，请配置设置并点击开始", ToolTipIcon.Info);

            // 加载保存的设置
            LoadSettings();

            // 更新UI状态
            UpdateDisplayModeUI();
            UpdatePositionUI();
            UpdateTaskbarStyleUI();
            UpdateLockStateUI();

            // 隐藏主窗口，只显示托盘图标
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;

            // 确定任务栏位置
            DetermineTaskbarPosition();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            ShowSettingsForm();
        }

        private void tsmiShowSettings_Click(object sender, EventArgs e)
        {
            ShowSettingsForm();
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            timer.Stop();
            taskbarScrollTimer.Stop();
            taskbarTextForm.Hide();
            HideAllBalloonTips(); // 关闭所有气泡
            notifyIcon.Visible = false;
            Application.Exit();
        }

        private void ShowSettingsForm()
        {
            this.ShowInTaskbar = true;
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog.FileName;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // 验证输入
            if (string.IsNullOrEmpty(txtFilePath.Text) || !File.Exists(txtFilePath.Text))
            {
                MessageBox.Show("请选择一个有效的TXT文件", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 保存设置
            SaveSettings();

            // 读取文件内容
            try
            {
                lines = File.ReadAllLines(txtFilePath.Text, Encoding.UTF8);
                if (lines.Length == 0)
                {
                    MessageBox.Show("文件内容为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 计算总页数
                int linesPerPage = (int)nudLinesPerPage.Value;
                totalPages = (int)Math.Ceiling((double)lines.Length / linesPerPage);
                currentPage = 0;

                // 显示第一页
                ShowCurrentPage();

                // 启动定时器
                timer.Interval = (int)nudPageInterval.Value;
                timer.Start();

                // 如果是任务栏文本模式，启动滚动定时器
                if (displayMode == DisplayMode.TaskbarText)
                {
                    taskbarScrollTimer.Start();
                }

                // 更新UI状态
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                lblStatus.Text = "状态: 正在运行";

                // 最小化窗口
                this.WindowState = FormWindowState.Minimized;
                this.ShowInTaskbar = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"读取文件时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer.Stop();
            taskbarScrollTimer.Stop();
            taskbarTextForm.Hide();
            HideAllBalloonTips(); // 关闭所有气泡
            isBalloonVisible = false; // 重置气泡状态
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            lblStatus.Text = "状态: 已停止";
            notifyIcon.Text = "TXT 通知工具";
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // 显示下一页前关闭之前的气泡
            HideAllBalloonTips();

            // 显示下一页
            currentPage = (currentPage + 1) % totalPages;
            ShowCurrentPage();
        }

        private void ShowCurrentPage()
        {
            int linesPerPage = (int)nudLinesPerPage.Value;
            int charsPerLine = (int)nudCharsPerLine.Value;
            int startLine = currentPage * linesPerPage;
            int endLine = Math.Min(startLine + linesPerPage, lines.Length);

            StringBuilder message = new StringBuilder();
            for (int i = startLine; i < endLine; i++)
            {
                // 截断长行以适应显示
                string line = lines[i];
                if (line.Length > charsPerLine)
                {
                    line = line.Substring(0, charsPerLine) + "...";
                }
                message.AppendLine(line);
            }

            // 添加页码信息
            message.AppendLine();
            message.AppendLine($"第 {currentPage + 1}/{totalPages} 页");

            // 根据显示模式显示内容
            if (displayMode == DisplayMode.BalloonTip)
            {
                // 通知气泡模式
                string shortText = message.ToString().Replace(Environment.NewLine, " ");
                if (shortText.Length > 63) // 托盘图标文本限制为64个字符
                {
                    shortText = shortText.Substring(0, 60) + "...";
                }
                notifyIcon.Text = shortText;

                // 显示气泡通知
                notifyIcon.ShowBalloonTip(
                    Math.Max(2000, (int)nudPageInterval.Value / 2), // 至少显示2秒
                    "TXT 内容",
                    message.ToString(),
                    ToolTipIcon.None);

                isBalloonVisible = true; // 标记气泡为可见

                // 隐藏任务栏文本窗体
                taskbarTextForm.Hide();
            }
            else
            {
                // 任务栏文本模式
                currentTaskbarText = message.ToString().Replace(Environment.NewLine, " ");
                taskbarTextOffset = 0;

                // 调整任务栏文本窗体位置和大小
                UpdateTaskbarTextForm();

                // 显示任务栏文本窗体
                taskbarTextForm.Show();

                // 更新托盘图标文本
                string shortText = currentTaskbarText;
                if (shortText.Length > 63)
                {
                    shortText = shortText.Substring(0, 60) + "...";
                }
                notifyIcon.Text = shortText;

                isBalloonVisible = false; // 任务栏模式下气泡不可见
            }
        }

        private void SaveSettings()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(settingsFile))
                {
                    writer.WriteLine(txtFilePath.Text);
                    writer.WriteLine(nudLinesPerPage.Value);
                    writer.WriteLine(nudCharsPerLine.Value);
                    writer.WriteLine(nudPageInterval.Value);
                    writer.WriteLine(iconFile); // 保存图标文件路径
                    writer.WriteLine((int)displayMode); // 保存显示模式
                    writer.WriteLine((int)taskbarPosition); // 保存任务栏文本位置

                    // 保存任务栏文本样式设置
                    writer.WriteLine(taskbarTextColor.ToArgb());
                    writer.WriteLine(taskbarBackColor.ToArgb());
                    writer.WriteLine(taskbarFont.Name);
                    writer.WriteLine(taskbarFont.Size);

                    // 保存锁定状态
                    writer.WriteLine((int)lockState);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存设置时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSettings()
        {
            if (File.Exists(settingsFile))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(settingsFile))
                    {
                        txtFilePath.Text = reader.ReadLine() ?? "";
                        if (int.TryParse(reader.ReadLine(), out int linesPerPage))
                            nudLinesPerPage.Value = linesPerPage;
                        if (int.TryParse(reader.ReadLine(), out int charsPerLine))
                            nudCharsPerLine.Value = charsPerLine;
                        if (int.TryParse(reader.ReadLine(), out int pageInterval))
                            nudPageInterval.Value = pageInterval;
                        string savedIcon = reader.ReadLine() ?? "";
                        if (!string.IsNullOrEmpty(savedIcon))
                        {
                            iconFile = savedIcon;
                            LoadIcon();
                        }

                        // 加载显示模式
                        if (int.TryParse(reader.ReadLine(), out int mode))
                        {
                            displayMode = (DisplayMode)mode;
                            UpdateDisplayModeUI();
                        }

                        // 加载任务栏文本位置
                        if (int.TryParse(reader.ReadLine(), out int position))
                        {
                            taskbarPosition = (TaskbarPosition)position;
                            UpdatePositionUI();
                        }

                        // 加载任务栏文本样式
                        if (int.TryParse(reader.ReadLine(), out int textColorArgb))
                        {
                            taskbarTextColor = Color.FromArgb(textColorArgb);
                        }
                        if (int.TryParse(reader.ReadLine(), out int backColorArgb))
                        {
                            taskbarBackColor = Color.FromArgb(backColorArgb);
                        }
                        string fontName = reader.ReadLine() ?? "Arial";
                        float fontSize = 9;
                        if (float.TryParse(reader.ReadLine(), out float size))
                        {
                            fontSize = size;
                        }
                        taskbarFont = new Font(fontName, fontSize);

                        // 加载锁定状态
                        if (int.TryParse(reader.ReadLine(), out int lockStateInt))
                        {
                            lockState = (LockState)lockStateInt;
                            UpdateLockStateUI();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"加载设置时出错: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // 更新图标显示
            UpdateIconLabel();
            // 应用任务栏样式
            ApplyTaskbarStyle();
        }

        private void LoadIcon()
        {
            try
            {
                // 如果图标文件存在，加载并转换为Icon
                if (!string.IsNullOrEmpty(iconFile) && File.Exists(iconFile))
                {
                    using (Image image = Image.FromFile(iconFile))
                    {
                        // 调整图片大小为32x32（托盘图标最佳尺寸）
                        using (Bitmap resizedBitmap = new Bitmap(image, new Size(32, 32)))
                        {
                            // 从Bitmap创建Icon
                            IntPtr hIcon = resizedBitmap.GetHicon();
                            notifyIcon.Icon = Icon.FromHandle(hIcon);

                            // 释放图标句柄
                            NativeMethods.DestroyIcon(hIcon);
                        }
                    }
                }
                else
                {
                    // 如果图标文件不存在，使用默认图标
                    notifyIcon.Icon = SystemIcons.Application;
                    iconFile = "";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"加载图标时出错: {ex.Message}");
                // 使用默认图标
                notifyIcon.Icon = SystemIcons.Application;
                iconFile = "";
            }

            // 更新图标显示
            UpdateIconLabel();
        }

        private void UpdateIconLabel()
        {
            if (!string.IsNullOrEmpty(iconFile) && File.Exists(iconFile))
            {
                lblCurrentIcon.Text = "当前图标: " + Path.GetFileName(iconFile);
            }
            else
            {
                lblCurrentIcon.Text = "当前图标: 默认";
            }
        }

        private void UpdateDisplayModeUI()
        {
            rbBalloonTip.Checked = (displayMode == DisplayMode.BalloonTip);
            rbTaskbarText.Checked = (displayMode == DisplayMode.TaskbarText);
        }

        // 更新位置UI
        private void UpdatePositionUI()
        {
            rbLeft.Checked = (taskbarPosition == TaskbarPosition.Left);
            rbRight.Checked = (taskbarPosition == TaskbarPosition.Right);
        }

        // 更新任务栏样式UI
        private void UpdateTaskbarStyleUI()
        {
            txtTaskbarFont.Text = taskbarFont.Name;
            nudTaskbarFontSize.Value = (decimal)taskbarFont.Size;
            lblTaskbarTextColor.BackColor = taskbarTextColor;
            lblTaskbarBackColor.BackColor = taskbarBackColor;
        }

        // 更新锁定状态UI
        private void UpdateLockStateUI()
        {
            chkLockTaskbarText.Checked = (lockState == LockState.Locked);
            UpdateTaskbarTextFormLockStateVisual();
        }

        // 更新任务栏文本窗体的锁定状态视觉效果
        private void UpdateTaskbarTextFormLockStateVisual()
        {
            if (taskbarTextForm != null && taskbarTextForm.Visible)
            {
                if (lockState == LockState.Locked)
                {
                    taskbarTextForm.Cursor = Cursors.NoMove2D;
                }
                else
                {
                    taskbarTextForm.Cursor = Cursors.SizeAll;
                }
            }
        }

        private void btnSetIcon_Click(object sender, EventArgs e)
        {
            using (System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog
            {
                Filter = "图片文件|*.png;*.jpg;*.jpeg|所有文件|*.*",
                Title = "选择托盘图标"
            })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    iconFile = dialog.FileName;
                    LoadIcon();
                    SaveSettings(); // 保存新的图标路径
                    notifyIcon.ShowBalloonTip(1000, "图标已更新", "托盘图标已更新为您选择的图片", ToolTipIcon.Info);
                }
            }
        }

        private void rbDisplayMode_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBalloonTip.Checked)
            {
                displayMode = DisplayMode.BalloonTip;
                taskbarScrollTimer.Stop();
                taskbarTextForm.Hide();
            }
            else if (rbTaskbarText.Checked)
            {
                displayMode = DisplayMode.TaskbarText;
                if (timer.Enabled) // 如果正在运行，更新任务栏文本
                {
                    UpdateTaskbarTextForm();
                    taskbarTextForm.Show();
                    taskbarScrollTimer.Start();
                }
            }

            // 保存设置
            SaveSettings();
        }

        // 位置选择变更处理
        private void rbPosition_CheckedChanged(object sender, EventArgs e)
        {
            if (rbLeft.Checked)
            {
                taskbarPosition = TaskbarPosition.Left;
            }
            else if (rbRight.Checked)
            {
                taskbarPosition = TaskbarPosition.Right;
            }

            // 保存设置
            SaveSettings();

            // 如果正在运行，更新任务栏文本位置
            if (displayMode == DisplayMode.TaskbarText && timer.Enabled)
            {
                UpdateTaskbarTextForm();
            }
        }

        // 任务栏文本颜色选择
        private void lblTaskbarTextColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = taskbarTextColor;
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    taskbarTextColor = colorDialog.Color;
                    UpdateTaskbarStyleUI();
                    SaveSettings();

                    // 如果正在运行，更新任务栏文本样式
                    if (displayMode == DisplayMode.TaskbarText && timer.Enabled)
                    {
                        taskbarTextForm.Invalidate();
                    }
                }
            }
        }

        // 任务栏背景颜色选择
        private void lblTaskbarBackColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                colorDialog.Color = taskbarBackColor;
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    taskbarBackColor = colorDialog.Color;
                    taskbarTextForm.BackColor = taskbarBackColor;
                    taskbarTextForm.TransparencyKey = taskbarBackColor;
                    UpdateTaskbarStyleUI();
                    SaveSettings();

                    // 如果正在运行，更新任务栏文本样式
                    if (displayMode == DisplayMode.TaskbarText && timer.Enabled)
                    {
                        taskbarTextForm.Invalidate();
                    }
                }
            }
        }

        // 任务栏字体大小变更
        private void nudTaskbarFontSize_ValueChanged(object sender, EventArgs e)
        {
            taskbarFont = new Font(taskbarFont.Name, (float)nudTaskbarFontSize.Value);
            UpdateTaskbarStyleUI();
            SaveSettings();

            // 如果正在运行，更新任务栏文本样式
            if (displayMode == DisplayMode.TaskbarText && timer.Enabled)
            {
                UpdateTaskbarTextForm();
                taskbarTextForm.Invalidate();
            }
        }

        // 任务栏字体名称变更
        private void txtTaskbarFont_Validated(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTaskbarFont.Text))
            {
                taskbarFont = new Font(txtTaskbarFont.Text, taskbarFont.Size);
                UpdateTaskbarStyleUI();
                SaveSettings();

                // 如果正在运行，更新任务栏文本样式
                if (displayMode == DisplayMode.TaskbarText && timer.Enabled)
                {
                    UpdateTaskbarTextForm();
                    taskbarTextForm.Invalidate();
                }
            }
        }

        // 锁定状态变更处理
        private void chkLockTaskbarText_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLockTaskbarText.Checked)
            {
                lockState = LockState.Locked;
            }
            else
            {
                lockState = LockState.Unlocked;
            }

            SaveSettings();
            UpdateTaskbarTextFormLockStateVisual();
        }

        private void DetermineTaskbarPosition()
        {
            APPBARDATA appBarData = new APPBARDATA();
            appBarData.cbSize = Marshal.SizeOf(appBarData);
            IntPtr result = SHAppBarMessage(ABM_GETTASKBARPOS, ref appBarData);

            if (result != IntPtr.Zero)
            {
                // 判断任务栏位置
                switch (appBarData.uEdge)
                {
                    case 0: // 左
                        isTaskbarAtBottom = false;
                        break;
                    case 1: // 右
                        isTaskbarAtBottom = false;
                        break;
                    case 2: // 上
                        isTaskbarAtBottom = false;
                        break;
                    case 3: // 下
                        isTaskbarAtBottom = true;
                        break;
                }
            }
        }

        private void UpdateTaskbarTextForm()
        {
            // 获取任务栏位置和大小
            APPBARDATA appBarData = new APPBARDATA();
            appBarData.cbSize = Marshal.SizeOf(appBarData);
            SHAppBarMessage(ABM_GETTASKBARPOS, ref appBarData);

            // 计算文本宽度
            using (Graphics g = taskbarTextForm.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(currentTaskbarText, taskbarFont);

                // 设置窗体大小
                taskbarTextForm.Width = (int)textSize.Width + 10;
                taskbarTextForm.Height = appBarData.rc.Bottom - appBarData.rc.Top - 2;

                // 根据选择的位置设置窗体位置
                if (isTaskbarAtBottom)
                {
                    // 任务栏在底部
                    if (taskbarPosition == TaskbarPosition.Left)
                    {
                        taskbarTextForm.Location = new Point(appBarData.rc.Left + 5, appBarData.rc.Top + 1);
                    }
                    else // 右侧
                    {
                        taskbarTextForm.Location = new Point(appBarData.rc.Right - taskbarTextForm.Width - 5, appBarData.rc.Top + 1);
                    }
                }
                else if (appBarData.uEdge == 1) // 任务栏在右侧
                {
                    // 垂直显示文本（旋转90度）
                    // 这里需要特殊处理文本绘制，使用旋转的Graphics
                    taskbarTextForm.Width = appBarData.rc.Right - appBarData.rc.Left - 2;
                    taskbarTextForm.Height = (int)textSize.Width + 10;

                    if (taskbarPosition == TaskbarPosition.Left)
                    {
                        taskbarTextForm.Location = new Point(appBarData.rc.Left + 1, appBarData.rc.Top + 5);
                    }
                    else // 右侧
                    {
                        taskbarTextForm.Location = new Point(appBarData.rc.Left + 1, appBarData.rc.Bottom - taskbarTextForm.Height - 5);
                    }
                }
                else if (appBarData.uEdge == 0) // 任务栏在左侧
                {
                    // 垂直显示文本（旋转90度）
                    taskbarTextForm.Width = appBarData.rc.Right - appBarData.rc.Left - 2;
                    taskbarTextForm.Height = (int)textSize.Width + 10;

                    if (taskbarPosition == TaskbarPosition.Left)
                    {
                        taskbarTextForm.Location = new Point(appBarData.rc.Right - taskbarTextForm.Width - 1, appBarData.rc.Top + 5);
                    }
                    else // 右侧
                    {
                        taskbarTextForm.Location = new Point(appBarData.rc.Right - taskbarTextForm.Width - 1, appBarData.rc.Bottom - taskbarTextForm.Height - 5);
                    }
                }
                else // 任务栏在上侧
                {
                    if (taskbarPosition == TaskbarPosition.Left)
                    {
                        taskbarTextForm.Location = new Point(appBarData.rc.Left + 5, appBarData.rc.Top + 1);
                    }
                    else // 右侧
                    {
                        taskbarTextForm.Location = new Point(appBarData.rc.Right - taskbarTextForm.Width - 5, appBarData.rc.Top + 1);
                    }
                }
            }

            // 强制重绘
            taskbarTextForm.Invalidate();
        }

        private void TaskbarTextForm_Paint(object sender, PaintEventArgs e)
        {
            // 设置文本颜色
            Brush textBrush = new SolidBrush(taskbarTextColor);

            // 绘制文本
            APPBARDATA appBarData = new APPBARDATA();
            appBarData.cbSize = Marshal.SizeOf(appBarData);
            SHAppBarMessage(ABM_GETTASKBARPOS, ref appBarData);

            if (isTaskbarAtBottom || appBarData.uEdge == 2) // 任务栏在底部或顶部
            {
                e.Graphics.DrawString(currentTaskbarText, taskbarFont, textBrush, 5, 2);
            }
            else // 任务栏在左侧或右侧，需要旋转文本
            {
                // 创建旋转的Graphics
                e.Graphics.TranslateTransform(taskbarTextForm.Width / 2, taskbarTextForm.Height / 2);
                e.Graphics.RotateTransform(90);
                e.Graphics.TranslateTransform(-taskbarTextForm.Width / 2, -taskbarTextForm.Height / 2);

                // 计算居中位置
                SizeF textSize = e.Graphics.MeasureString(currentTaskbarText, taskbarFont);
                float x = (taskbarTextForm.Width - textSize.Height) / 2;
                float y = (taskbarTextForm.Height - textSize.Width) / 2;

                // 绘制旋转后的文本
                e.Graphics.DrawString(currentTaskbarText, taskbarFont, textBrush, x, y);
            }

            // 绘制锁定状态指示
            if (lockState == LockState.Locked)
            {
                e.Graphics.DrawString("🔒", new Font("Wingdings", 8), Brushes.White, 5, 5);
            }

            // 释放资源
            textBrush.Dispose();
        }

        private void TaskbarScrollTimer_Tick(object sender, EventArgs e)
        {
            // 滚动文本
            if (!string.IsNullOrEmpty(currentTaskbarText))
            {
                taskbarTextOffset += 2; // 每次滚动2个像素

                // 如果文本已完全滚动过去，重置偏移量
                using (Graphics g = taskbarTextForm.CreateGraphics())
                {
                    SizeF textSize = g.MeasureString(currentTaskbarText, taskbarFont);
                    if (taskbarTextOffset > textSize.Width)
                    {
                        taskbarTextOffset = 0;
                    }
                }

                // 重绘窗体
                taskbarTextForm.Invalidate();
            }
        }

        private void TaskbarTextForm_MouseClick(object sender, MouseEventArgs e)
        {
            // 点击任务栏文本时显示设置窗口
            if (e.Clicks == 2) // 双击
            {
                // 切换锁定状态
                lockState = (lockState == LockState.Locked) ? LockState.Unlocked : LockState.Locked;
                chkLockTaskbarText.Checked = (lockState == LockState.Locked);
                SaveSettings();
                UpdateTaskbarTextFormLockStateVisual();
            }
            else // 单击
            {
                ShowSettingsForm();
            }
        }

        // 鼠标按下事件 - 开始拖动
        private void TaskbarTextForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (lockState == LockState.Unlocked && e.Button == MouseButtons.Left)
            {
                isDragging = true;
                mouseDownLocation = e.Location;
                taskbarTextForm.Cursor = Cursors.No; // 拖动时显示禁止图标（视觉反馈）
            }
        }

        // 鼠标移动事件 - 执行拖动
        private void TaskbarTextForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // 计算新位置
                Point newLocation = taskbarTextForm.Location;
                newLocation.X += e.X - mouseDownLocation.X;
                newLocation.Y += e.Y - mouseDownLocation.Y;

                // 设置新位置
                taskbarTextForm.Location = newLocation;
            }
        }

        // 鼠标释放事件 - 结束拖动
        private void TaskbarTextForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;
                taskbarTextForm.Cursor = Cursors.SizeAll; // 恢复默认光标
            }
        }

        // 兼容旧版本的 HideBalloonTip 方法
        private void HideBalloonTipCompat(NotifyIcon icon)
        {
            // 通过反射调用私有方法
            System.Reflection.MethodInfo method = typeof(NotifyIcon).GetMethod("HideBalloonTip",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (method != null)
            {
                method.Invoke(icon, null);
            }
        }

        // 关闭所有气泡通知
        private void HideAllBalloonTips()
        {
            if (isBalloonVisible)
            {
                HideBalloonTipCompat(notifyIcon);
                isBalloonVisible = false;
            }
        }

        // 应用任务栏文本样式
        private void ApplyTaskbarStyle()
        {
            taskbarTextForm.BackColor = taskbarBackColor;
            taskbarTextForm.TransparencyKey = taskbarBackColor;
        }

        // 用于释放图标句柄的辅助类
        private static class NativeMethods
        {
            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)] // 修正MarshalType为UnmanagedType.Bool
            public static extern bool DestroyIcon(IntPtr hIcon);
        }
    }
}
