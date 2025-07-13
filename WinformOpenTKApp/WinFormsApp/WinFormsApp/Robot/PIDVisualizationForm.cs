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
    public partial class PIDVisualizationForm : Form
    {
        // 业务逻辑相关字段（非控件）
        private PIDController _pid;
        private double _setpoint;
        private double _processVariable;
        private double _dt;
        private List<double> _setpointHistory;
        private List<double> _processVariableHistory;
        private List<double> _outputHistory;

        public PIDVisualizationForm()
        {
            // 调用设计器初始化方法（生成控件）
            InitializeComponent();

            // 初始化业务逻辑数据
            _pid = new PIDController(1.0, 0.1, 0.01);
            _setpoint = 50;
            _processVariable = 0;
            _dt = 0.1;
            _setpointHistory = new List<double>();
            _processVariableHistory = new List<double>();
            _outputHistory = new List<double>();

            // 启动定时器（业务逻辑相关）
            _timer.Interval = (int)(_dt * 1000);
            _timer.Start();
        }

        // 事件处理与业务逻辑方法（保持不变）
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
            double output = _pid.Compute(_setpoint, _processVariable, _dt);
            _processVariable += output * _dt;

            _setpointHistory.Add(_setpoint);
            _processVariableHistory.Add(_processVariable);
            _outputHistory.Add(output);

            _graphPanel.Invalidate();
        }

        private void GraphPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            int margin = 50;
            int graphWidth = _graphPanel.ClientSize.Width - 2 * margin;
            int graphHeight = _graphPanel.ClientSize.Height - 2 * margin;

            // 绘制坐标轴
            g.DrawLine(Pens.Black, margin, margin, margin, margin + graphHeight);
            g.DrawLine(Pens.Black, margin, margin + graphHeight, margin + graphWidth, margin + graphHeight);

            // 绘制箭头
            g.DrawLine(Pens.Black, margin, margin, margin - 5, margin + 10);
            g.DrawLine(Pens.Black, margin, margin, margin + 5, margin + 10);
            g.DrawLine(Pens.Black, margin + graphWidth, margin + graphHeight, margin + graphWidth - 10, margin + graphHeight - 5);
            g.DrawLine(Pens.Black, margin + graphWidth, margin + graphHeight, margin + graphWidth - 10, margin + graphHeight + 5);

            // 绘制轴标签
            g.DrawString("Y", Font, Brushes.Black, margin - 30, margin - 20);
            g.DrawString("X", Font, Brushes.Black, margin + graphWidth + 20, margin + graphHeight + 20);

            // 计算最大/最小值
            double maxValue = Math.Max(_setpoint, _processVariable);
            if (_outputHistory.Count > 0)
                maxValue = Math.Max(maxValue, Math.Abs(_outputHistory.Max()));
            double minValue = 0;

            // 绘制X轴刻度
            int numXTicks = 10;
            for (int i = 0; i <= numXTicks; i++)
            {
                int x = margin + i * graphWidth / numXTicks;
                g.DrawLine(Pens.Black, x, margin + graphHeight, x, margin + graphHeight + 5);
                g.DrawString((i * _setpointHistory.Count / numXTicks).ToString(), Font, Brushes.Black, x - 5, margin + graphHeight + 10);
            }

            // 绘制Y轴刻度
            int numYTicks = 10;
            for (int i = 0; i <= numYTicks; i++)
            {
                double value = minValue + i * (maxValue - minValue) / numYTicks;
                int y = margin + graphHeight - (int)(i * graphHeight / numYTicks);
                g.DrawLine(Pens.Black, margin - 5, y, margin, y);
                g.DrawString(value.ToString("F2"), Font, Brushes.Black, margin - 40, y - 5);
            }

            // 绘制曲线
            DrawCurve(g, _setpointHistory, Color.Red, margin, graphWidth, graphHeight, minValue, maxValue);
            DrawCurve(g, _processVariableHistory, Color.Green, margin, graphWidth, graphHeight, minValue, maxValue);
            DrawCurve(g, _outputHistory, Color.Blue, margin, graphWidth, graphHeight, minValue, maxValue);

            // 绘制曲线说明
            string setpointValue = _setpointHistory.Count > 0 ? _setpointHistory.Last().ToString("F2") : "0.00";
            string processVariableValue = _processVariableHistory.Count > 0 ? _processVariableHistory.Last().ToString("F2") : "0.00";
            string outputValue = _outputHistory.Count > 0 ? _outputHistory.Last().ToString("F2") : "0.00";

            SizeF setpointSize = g.MeasureString($"设定值: {setpointValue}", Font);
            SizeF processVariableSize = g.MeasureString($"过程变量: {processVariableValue}", Font);
            SizeF outputSize = g.MeasureString($"输出: {outputValue}", Font);
            float totalWidth = Math.Max(setpointSize.Width, Math.Max(processVariableSize.Width, outputSize.Width));
            float centerX = margin + graphWidth / 2 - totalWidth / 2;
            float startY = margin - 30;

            g.DrawString($"设定值: {setpointValue}", Font, Brushes.Red, centerX, startY);
            g.DrawString($"过程变量: {processVariableValue}", Font, Brushes.Green, centerX, startY + 20);
            g.DrawString($"输出: {outputValue}", Font, Brushes.Blue, centerX, startY + 40);
        }

        private void DrawCurve(Graphics g, List<double> data, Color color, int margin, int graphWidth, int graphHeight, double minValue, double maxValue)
        {
            if (data.Count < 2) return;

            using (Pen pen = new Pen(color, 1.5f))
            {
                for (int i = 0; i < data.Count - 1; i++)
                {
                    int x1 = margin + (int)(i * (double)graphWidth / data.Count);
                    int y1 = margin + graphHeight - (int)((data[i] - minValue) * graphHeight / (maxValue - minValue));
                    int x2 = margin + (int)((i + 1) * (double)graphWidth / data.Count);
                    int y2 = margin + graphHeight - (int)((data[i + 1] - minValue) * graphHeight / (maxValue - minValue));
                    g.DrawLine(pen, x1, y1, x2, y2);
                }
            }
        }

        private void LeftBgColorButton_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    _paramLayoutPanel.BackColor = colorDialog.Color;
                }
            }
        }

        private void RightBgColorButton_Click(object sender, EventArgs e)
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    _graphPanel.BackColor = colorDialog.Color;
                }
            }
        }
    }
}