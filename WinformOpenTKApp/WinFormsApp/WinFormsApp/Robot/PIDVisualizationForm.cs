using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp.Robot
{
    // 主窗体类
    public partial class PIDVisualizationForm : Form
    {
        private PIDController _pid;
        private double _setpoint;
        private double _processVariable;
        private double _dt;
        private List<double> _setpointHistory;
        private List<double> _processVariableHistory;
        private List<double> _outputHistory;
        private TextBox _kpTextBox;
        private TextBox _kiTextBox;
        private TextBox _kdTextBox;
        private TextBox _dtTextBox;
        private Button _updateButton;
        private Label _kpLabel;
        private Label _kiLabel;
        private Label _kdLabel;
        private Label _dtLabel;
        private DoubleBufferedPanel _graphPanel;
        private System.Windows.Forms.Timer _timer;
        private TableLayoutPanel _mainLayoutPanel;
        private TableLayoutPanel _paramLayoutPanel;
        private Button _leftBgColorButton;
        private Button _rightBgColorButton;

        public PIDVisualizationForm()
        {
            InitializeComponent();
            // 初始化 PID 控制器
            _pid = new PIDController(1.0, 0.1, 0.01);
            _setpoint = 50;
            _processVariable = 0;
            _dt = 0.1;
            _setpointHistory = new List<double>();
            _processVariableHistory = new List<double>();
            _outputHistory = new List<double>();

            // 设置窗体样式
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

            // 创建主布局面板
            _mainLayoutPanel = new TableLayoutPanel();
            _mainLayoutPanel.Dock = DockStyle.Fill;
            _mainLayoutPanel.ColumnCount = 2;
            _mainLayoutPanel.RowCount = 1;
            _mainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            _mainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            Controls.Add(_mainLayoutPanel);

            // 创建参数布局面板
            _paramLayoutPanel = new TableLayoutPanel();
            _paramLayoutPanel.Dock = DockStyle.Fill;
            _paramLayoutPanel.RowCount = 1;
            _mainLayoutPanel.Controls.Add(_paramLayoutPanel, 0, 0);

            // 创建参数面板
            Panel paramPanel = new Panel();
            paramPanel.BackColor = Color.LightGray;
            paramPanel.BorderStyle = BorderStyle.FixedSingle;
            paramPanel.Padding = new Padding(20);
            paramPanel.Dock = DockStyle.Fill;
            _paramLayoutPanel.Controls.Add(paramPanel);

            // 创建标签
            _kpLabel = new Label();
            _kpLabel.Text = "Kp:";
            _kpLabel.AutoSize = true;

            _kiLabel = new Label();
            _kiLabel.Text = "Ki:";
            _kiLabel.AutoSize = true;

            _kdLabel = new Label();
            _kdLabel.Text = "Kd:";
            _kdLabel.AutoSize = true;

            _dtLabel = new Label();
            _dtLabel.Text = "时间间隔 (s):";
            _dtLabel.AutoSize = true;

            // 创建文本框并设置默认值
            _kpTextBox = new TextBox();
            _kpTextBox.Size = new Size(120, 23);
            _kpTextBox.Text = "1";

            _kiTextBox = new TextBox();
            _kiTextBox.Size = new Size(120, 23);
            _kiTextBox.Text = "0.1";

            _kdTextBox = new TextBox();
            _kdTextBox.Size = new Size(120, 23);
            _kdTextBox.Text = "0.01";

            _dtTextBox = new TextBox();
            _dtTextBox.Size = new Size(120, 23);
            _dtTextBox.Text = _dt.ToString();

            // 创建按钮
            _updateButton = new Button();
            _updateButton.Text = "更新参数";
            _updateButton.Size = new Size(180, 40);
            _updateButton.BackColor = Color.SteelBlue;
            _updateButton.ForeColor = Color.White;
            _updateButton.FlatStyle = FlatStyle.Flat;
            _updateButton.FlatAppearance.BorderSize = 0;
            _updateButton.Click += UpdateButton_Click;

            // 使用 TableLayoutPanel 对标签和文本框进行竖向布局
            TableLayoutPanel verticalLayoutPanel = new TableLayoutPanel();
            verticalLayoutPanel.Dock = DockStyle.Fill;
            verticalLayoutPanel.ColumnCount = 2;
            verticalLayoutPanel.RowCount = 7;
            verticalLayoutPanel.AutoSize = true;
            verticalLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            // 按顺序添加标签和文本框到 TableLayoutPanel
            verticalLayoutPanel.Controls.Add(_kpLabel, 0, 0);
            verticalLayoutPanel.Controls.Add(_kpTextBox, 1, 0);
            verticalLayoutPanel.Controls.Add(_kiLabel, 0, 1);
            verticalLayoutPanel.Controls.Add(_kiTextBox, 1, 1);
            verticalLayoutPanel.Controls.Add(_kdLabel, 0, 2);
            verticalLayoutPanel.Controls.Add(_kdTextBox, 1, 2);
            verticalLayoutPanel.Controls.Add(_dtLabel, 0, 3);
            verticalLayoutPanel.Controls.Add(_dtTextBox, 1, 3);
            verticalLayoutPanel.Controls.Add(_updateButton, 0, 4);
            verticalLayoutPanel.SetColumnSpan(_updateButton, 2);

            // 创建更改背景颜色的按钮
            _leftBgColorButton = new Button();
            _leftBgColorButton.Text = "左侧背景颜色";
            _leftBgColorButton.Size = new Size(180, 40);
            _leftBgColorButton.TextAlign = ContentAlignment.MiddleCenter; // 设置文字居中对齐
            _leftBgColorButton.Click += LeftBgColorButton_Click;
            verticalLayoutPanel.Controls.Add(_leftBgColorButton, 0, 5);
            verticalLayoutPanel.SetColumnSpan(_leftBgColorButton, 2);

            _rightBgColorButton = new Button();
            _rightBgColorButton.Text = "右侧背景颜色";
            _rightBgColorButton.Size = new Size(180, 40);
            _rightBgColorButton.TextAlign = ContentAlignment.MiddleCenter; // 设置文字居中对齐
            _rightBgColorButton.Click += RightBgColorButton_Click;
            verticalLayoutPanel.Controls.Add(_rightBgColorButton, 0, 6);
            verticalLayoutPanel.SetColumnSpan(_rightBgColorButton, 2);

            paramPanel.Controls.Add(verticalLayoutPanel);

            // 创建绘图面板
            _graphPanel = new DoubleBufferedPanel();
            _graphPanel.Dock = DockStyle.Fill;
            _graphPanel.Padding = new Padding(40, 40, 40, 40);
            _graphPanel.Paint += GraphPanel_Paint;
            _mainLayoutPanel.Controls.Add(_graphPanel, 1, 0);

            // 启动定时器
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = (int)(_dt * 1000);
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (double.TryParse(_kpTextBox.Text, out double kp) &&
                double.TryParse(_kiTextBox.Text, out double ki) &&
                double.TryParse(_kdTextBox.Text, out double kd) &&
                double.TryParse(_dtTextBox.Text, out double newDt) && newDt > 0)
            {
                _pid.UpdateParameters(kp, ki, kd);
                _dt = newDt;
                _timer.Interval = (int)(_dt * 1000);
                // 重置积分和误差
                _pid.Reset();
                _setpointHistory.Clear();
                _processVariableHistory.Clear();
                _outputHistory.Clear();
                _processVariable = 0;
                _graphPanel.Invalidate();
            }
            else
            {
                MessageBox.Show("请输入有效的参数！时间间隔需大于 0。");
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // 计算 PID 输出
            double output = _pid.Compute(_setpoint, _processVariable, _dt);
            // 模拟系统响应
            _processVariable += output * _dt;

            // 记录数据
            _setpointHistory.Add(_setpoint);
            _processVariableHistory.Add(_processVariable);
            _outputHistory.Add(output);

            // 重绘图表
            _graphPanel.Invalidate();
        }

        private void GraphPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int margin = 50;
            int graphWidth = _graphPanel.ClientSize.Width - 2 * margin;
            int graphHeight = _graphPanel.ClientSize.Height - 2 * margin;

            // 绘制 y 坐标轴
            g.DrawLine(Pens.Black, margin, margin, margin, margin + graphHeight);
            g.DrawLine(Pens.Black, margin, margin + graphHeight, margin + graphWidth, margin + graphHeight);

            // 绘制 y 轴箭头
            g.DrawLine(Pens.Black, margin, margin, margin - 5, margin + 10);
            g.DrawLine(Pens.Black, margin, margin, margin + 5, margin + 10);

            // 绘制 x 轴箭头
            g.DrawLine(Pens.Black, margin + graphWidth, margin + graphHeight, margin + graphWidth - 10, margin + graphHeight - 5);
            g.DrawLine(Pens.Black, margin + graphWidth, margin + graphHeight, margin + graphWidth - 10, margin + graphHeight + 5);

            // 绘制 y 轴标签，调整位置避免与数字折叠
            g.DrawString("Y", Font, Brushes.Black, margin - 30, margin - 20);
            // 绘制 x 轴标签，调整位置避免与数字折叠
            g.DrawString("X", Font, Brushes.Black, margin + graphWidth + 20, margin + graphHeight + 20);

            // 计算最大和最小值
            double maxValue = Math.Max(_setpoint, _processVariable);
            if (_outputHistory.Count > 0)
            {
                maxValue = Math.Max(maxValue, Math.Abs(_outputHistory.Max()));
            }
            double minValue = 0;

            // 绘制 x 轴刻度
            int numXTicks = 10;
            for (int i = 0; i <= numXTicks; i++)
            {
                int x = margin + i * graphWidth / numXTicks;
                g.DrawLine(Pens.Black, x, margin + graphHeight, x, margin + graphHeight + 5);
                g.DrawString((i * _setpointHistory.Count / numXTicks).ToString(), Font, Brushes.Black, x - 5, margin + graphHeight + 10);
            }

            // 绘制 y 轴刻度
            int numYTicks = 10;
            for (int i = 0; i <= numYTicks; i++)
            {
                double value = minValue + i * (maxValue - minValue) / numYTicks;
                int y = margin + graphHeight - (int)(i * graphHeight / numYTicks);
                g.DrawLine(Pens.Black, margin - 5, y, margin, y);
                g.DrawString(value.ToString("F2"), Font, Brushes.Black, margin - 40, y - 5);
            }

            // 绘制设定值曲线
            DrawCurve(g, _setpointHistory, Color.Red, margin, graphWidth, graphHeight, minValue, maxValue);
            // 绘制过程变量曲线
            DrawCurve(g, _processVariableHistory, Color.Green, margin, graphWidth, graphHeight, minValue, maxValue);
            // 绘制输出曲线
            DrawCurve(g, _outputHistory, Color.Blue, margin, graphWidth, graphHeight, minValue, maxValue);

            // 绘制每条线的含义注释及当前值
            string setpointValue = _setpointHistory.Count > 0 ? _setpointHistory.Last().ToString("F2") : "0.00";
            string processVariableValue = _processVariableHistory.Count > 0 ? _processVariableHistory.Last().ToString("F2") : "0.00";
            string outputValue = _outputHistory.Count > 0 ? _outputHistory.Last().ToString("F2") : "0.00";

            // 计算注释信息的总宽度
            SizeF setpointSize = g.MeasureString($"设定值: {setpointValue}", Font);
            SizeF processVariableSize = g.MeasureString($"过程变量: {processVariableValue}", Font);
            SizeF outputSize = g.MeasureString($"输出: {outputValue}", Font);
            float totalWidth = Math.Max(setpointSize.Width, Math.Max(processVariableSize.Width, outputSize.Width));

            // 计算注释信息的绘制位置
            float centerX = margin + graphWidth / 2 - totalWidth / 2;
            float startY = margin - 30;

            g.DrawString($"设定值: {setpointValue}", Font, Brushes.Red, centerX, startY);
            g.DrawString($"过程变量: {processVariableValue}", Font, Brushes.Green, centerX, startY + 20);
            g.DrawString($"输出: {outputValue}", Font, Brushes.Blue, centerX, startY + 40);
        }

        private void DrawCurve(Graphics g, List<double> data, Color color, int margin, int graphWidth, int graphHeight, double minValue, double maxValue)
        {
            if (data.Count < 2) return;

            Pen pen = new Pen(color, 1.5f);
            for (int i = 0; i < data.Count - 1; i++)
            {
                int x1 = margin + (int)(i * (double)graphWidth / data.Count);
                int y1 = margin + graphHeight - (int)((data[i] - minValue) * graphHeight / (maxValue - minValue));
                int x2 = margin + (int)((i + 1) * (double)graphWidth / data.Count);
                int y2 = margin + graphHeight - (int)((data[i + 1] - minValue) * graphHeight / (maxValue - minValue));
                g.DrawLine(pen, x1, y1, x2, y2);
            }
            pen.Dispose();
        }

        private void LeftBgColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                _paramLayoutPanel.BackColor = colorDialog.Color;
            }
        }

        private void RightBgColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                _graphPanel.BackColor = colorDialog.Color;
            }
        }
    }
}
