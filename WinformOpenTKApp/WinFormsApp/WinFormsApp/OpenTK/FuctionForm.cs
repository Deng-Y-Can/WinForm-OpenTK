using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using LearnOpenTK.Common;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class FuctionForm : Form
    {
        private const float minX = -20;
        private const float maxX = 20;
        private const int pointCountX = 100;
        private float[] xValues;
        private float[] yValues;
        private float scaleFactor = 1.0f;
        private Vector2 translation = Vector2.Zero;

        private Matrix4 _model;
        private Matrix4 _view;
        private Matrix4 _projection;
        private Matrix4 transform;

        private int _cVBO;
        private int _cVAO;
        private int _pVBO;
        private int _pVAO;

        private Shader _shader;

        private float[] _pointFunctionList;

        private Axis axis;
        private Camera _camera;
        private Color4 _backgroundColor;  //背景颜色

        public FuctionForm()
        {
            InitializeComponent();
            InitializationParam();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string functionExpression = textBox1.Text;
            CalculateFunctionValues(functionExpression);
            _pointFunctionList = GetPointList();
            Render();
            glControl1.Invalidate();
        }


        private void InitializationParam()
        {
            xValues = new float[pointCountX];
            yValues = new float[pointCountX];
            textBox1.Text = "x * x";
            CalculateFunctionValues(textBox1.Text);
            axis = new Axis(maxX, maxX, maxX, pointCountX);
        }
        private void ClearColor()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
            GL.ClearColor(0.5f, 0.2f, 0.5f, 1.0f);//背景颜色
        }

        private void CalculateFunctionValues(string functionExpression)
        {
            float dx = (maxX - minX) / (pointCountX - 1);
            for (int i = 0; i < pointCountX; i++)
            {
                xValues[i] = minX + i * dx;
                try
                {
                    yValues[i] = Convert.ToSingle(EvaluateFunction(functionExpression, xValues[i]).ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"函数计算出错: {ex.Message}");
                }

            }
        }

        public float[] GetPointList()
        {
            float[] pointList = new float[pointCountX * 3];
            int index = 0;
            for (int i = 0; i < pointCountX; i++)
            {
                pointList[index] = xValues[i];
                index++;
                pointList[index] = yValues[i];
                index++;
                pointList[index] = 0;
                index++;
            }
            return pointList;
        }

        private object EvaluateFunction(string functionExpression, float x)
        {
            // 创建一个临时的C#代码片段，将输入的函数表达式中的 'x' 替换为实际的数值
            string code = $"return {functionExpression.Replace("x", x.ToString())};";
            // 使用CSharpScript.EvaluateAsync来计算函数值
            return CSharpScript.EvaluateAsync(code).Result;
        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            ClearColor();
            _camera = new Camera(Vector3.UnitZ * 20, 1.2f);
            _shader = new Shader(vertCoordinateShader, frageCoordinateShader, 2);
            _shader.Use();
            _model = Matrix4.Identity;

        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            ClearColor();

            _shader.Use();
            SetMVP();
            _pointFunctionList = GetPointList();
            _cVBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _cVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, axis._vertexArray.Length * sizeof(float), axis._vertexArray, BufferUsageHint.StaticDraw);

            _cVAO = GL.GenVertexArray();
            GL.BindVertexArray(_cVAO);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.DrawArrays(PrimitiveType.Lines, 0, 6);

            _pVBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _pVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, _pointFunctionList.Length * sizeof(float), _pointFunctionList, BufferUsageHint.StaticDraw);

            _pVAO = GL.GenVertexArray();
            GL.BindVertexArray(_pVAO);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, true, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.DrawArrays(PrimitiveType.LineStrip, 0, pointCountX);

            glControl1.SwapBuffers();
        }

        public void Render()
        {
            ClearColor();
            _shader.Use();
            SetMVP();

            _pointFunctionList = GetPointList();
            GL.BindVertexArray(_cVAO);
            GL.DrawArrays(PrimitiveType.Lines, 0, 6);

            GL.BindVertexArray(_pVAO);
            GL.DrawArrays(PrimitiveType.LineStrip, 0, pointCountX);
            glControl1.SwapBuffers();
        }

        private void SetMVP()
        {
            _shader.SetMatrix4("model", _model);
            _shader.SetMatrix4("view", _camera.GetViewMatrix());
            _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());
        }
        private void SetUnitMVP()
        {
            _shader.SetMatrix4("model", Matrix4.Identity);
            _shader.SetMatrix4("view", Matrix4.Identity);
            _shader.SetMatrix4("projection", Matrix4.Identity);
        }
        private Matrix4 GetMVP()
        {
            return _model * _camera.GetViewMatrix() * _camera.GetProjectionMatrix();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            _camera.Fov += 0.5f;
            Render();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            _camera.Fov -= 0.5f;
            Render();
        }

        private bool _firstMove = true;
        float unMax = 999999999;
        private Vector2 _lastPos;
        const float _cameraSpeed = 1.5f;
        const float _sensitivity = 0.05f;
        private float _rotatefactor = 0.075f;
        private float _maxMouseMoveDistance = 40f;
        private bool _mouseButtonDown = false;
        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_camera != null)
            {
                float mouseX = (float)e.X;
                float mouseY = (float)e.Y;
                float deltaX = unMax;
                float deltaY = unMax;
                if (_firstMove)
                {
                    _lastPos = new Vector2(mouseX, mouseY);
                    _firstMove = false;
                }
                else
                {
                    deltaX = mouseX - _lastPos.X;
                    deltaY = mouseY - _lastPos.Y;
                    _lastPos = new Vector2(mouseX, mouseY);
                }
                if (Math.Abs(deltaX) < _maxMouseMoveDistance && Math.Abs(deltaY) < _maxMouseMoveDistance && _mouseButtonDown)
                {

                    if (e.Button == MouseButtons.Left)
                    {
                        _camera.Yaw -= deltaX * _rotatefactor;
                        _camera.Pitch += deltaY * _rotatefactor;
                    }
                    else
                    {
                        _camera.Position -= _camera.Right * deltaX * _sensitivity;
                        _camera.Position += _camera.Up * deltaY * _sensitivity;
                    }
                    Render();
                }
            }
        }
        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseButtonDown = true;
        }
        private void glControl1_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseButtonDown = false;
        }

        private string vertCoordinateShader = $@"
        #version 330 core
        layout(location = 0) in vec3 aPosition;
        uniform mat4 model;
        uniform mat4 view;
        uniform mat4 projection;

        void main(void)
        {{
         gl_Position = vec4(aPosition, 1.0) * model * view * projection;
        }}
";

        private string frageCoordinateShader = $@"
#version 330

out vec4 outputColor;

void main()
{{
    outputColor = vec4(1.0, 0.0, 0, 1.0);
}}"
 ;
        #region    视图

        public enum CoordinateAxis
        {
            Z_positive,
            Z_negative,
            X_positive,
            X_negative,
            Y_positive,
            Y_negative,
        }
        public void PlaneSwitching(CoordinateAxis coordinateAxis)
        {
            if (_shader != null)
            {
                _model = ModelChange(coordinateAxis);
                Render();
            }
        }
        public Matrix4 ModelChange(CoordinateAxis coordinateAxis)
        {
            Matrix4 model = _model;
            switch (coordinateAxis)
            {
                case CoordinateAxis.Z_positive:
                    model = model * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(0));
                    break;
                case CoordinateAxis.Z_negative:
                    model = model * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(180));
                    break;

                case CoordinateAxis.Y_positive:
                    model = model * Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(90));
                    break;
                case CoordinateAxis.Y_negative:
                    model = model * Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(-90));
                    break;
                case CoordinateAxis.X_positive:
                    model = model * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(90));
                    break;
                case CoordinateAxis.X_negative:
                    model = model * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(-90));
                    break;

            }
            return model;
        }

        private void toolStripMenuItemX_Positive_Click(object sender, EventArgs e)
        {
            PlaneSwitching(CoordinateAxis.X_positive);
        }
        private void toolStripMenuItemX_Negative_Click(object sender, EventArgs e)
        {
            PlaneSwitching(CoordinateAxis.X_negative);
        }

        private void toolStripMenuItemY_Positive_Click(object sender, EventArgs e)
        {
            PlaneSwitching(CoordinateAxis.Y_positive);
        }
        private void toolStripMenuItemY_Negative_Click(object sender, EventArgs e)
        {
            PlaneSwitching(CoordinateAxis.Y_negative);
        }
        private void toolStripMenuItemZ_Positive_Click(object sender, EventArgs e)
        {
            PlaneSwitching(CoordinateAxis.Z_positive);
        }

        private void toolStripMenuItemZ_Negative_Click(object sender, EventArgs e)
        {
            PlaneSwitching(CoordinateAxis.Z_negative);
        }
        #endregion


        private void FuctionForm_SizeChanged(object sender, EventArgs e)
        {
            glControl1.Width = (int)(this.Width*0.6);
            glControl1.Height = (int)(this.Width * 0.6);
            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);
            Render();
        }
    }

}
