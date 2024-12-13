using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using LearnOpenTK.Common;

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
        private Color4 _backgroundColor;  //������ɫ

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
                    MessageBox.Show($"�����������: {ex.Message}");
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
            // ����һ����ʱ��C#����Ƭ�Σ�������ĺ������ʽ�е� 'x' �滻Ϊʵ�ʵ���ֵ
            string code = $"return {functionExpression.Replace("x", x.ToString())};";
            // ʹ��CSharpScript.EvaluateAsync�����㺯��ֵ
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
        private void ClearColor()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
            GL.ClearColor(0.5f, 0.2f, 0.5f, 1.0f);//������ɫ
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

        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            translation = new Vector2(e.X, e.Y);
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

        private Vector2 _lastPos;
        const float cameraSpeed = 1.5f;
        const float sensitivity = 0.2f;
        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_firstMove)
                {
                    _lastPos = new Vector2(e.X, e.Y);
                    _firstMove = false;
                }
                else
                {

                    var deltaX = e.X - _lastPos.X;
                    var deltaY = e.Y - _lastPos.Y;
                    _lastPos = new Vector2(e.X, e.Y);


                    _camera.Yaw -= deltaX * sensitivity;
                    _camera.Pitch += deltaY * sensitivity;
                }
                Render();

               
            }
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

       
    }

}
