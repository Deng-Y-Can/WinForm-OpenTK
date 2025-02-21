using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp.Robot
{
    // PID 控制器类
    public class PIDController
    {
        private double _kp;
        private double _ki;
        private double _kd;
        private double _integral;
        private double _previousError;

        public PIDController(double kp, double ki, double kd)
        {
            _kp = kp;
            _ki = ki;
            _kd = kd;
            _integral = 0;
            _previousError = 0;
        }

        public double Compute(double setpoint, double processVariable, double dt)
        {
            double error = setpoint - processVariable;
            _integral += error * dt;
            double derivative = (error - _previousError) / dt;
            double output = _kp * error + _ki * _integral + _kd * derivative;
            _previousError = error;
            return output;
        }

        public void Reset()
        {
            _integral = 0;
            _previousError = 0;
        }

        public void UpdateParameters(double kp, double ki, double kd)
        {
            _kp = kp;
            _ki = ki;
            _kd = kd;
        }
    }

    // 自定义 Panel 类，启用双缓冲
    public class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            DoubleBuffered = true;
        }
    }
}
