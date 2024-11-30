using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System.IO;
using System.Text;
using OpenTK.Mathematics;
using OpenTK.GLControl;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using LearnOpenTK.Common;
using System.Security.Cryptography.Xml;

namespace WinFormsApp
{
    public partial class FuctionForm : Form
    {
        private const float X_MIN = -5;
        private const float X_MAX = 5;
        private const int NUM_POINTS = 100;
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
        private readonly float[] _verticesCoordinate =
        {
             X_MIN, 0, 0.0f, 
             X_MAX, 0, 0.0f,
             0, X_MIN, 0.0f,
             0, X_MAX, 0.0f
        };
        private float[] _pointList;

        public FuctionForm()
        {
            InitializeComponent();
            xValues = new float[NUM_POINTS];
            yValues = new float[NUM_POINTS];
            textBox1.Text = "x*x";
            CalculateFunctionValues(textBox1.Text);
        }

        private void glControl1_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string functionExpression = textBox1.Text;
            CalculateFunctionValues(functionExpression);
            _pointList = GetPointList();
            glControl1.Invalidate();
        }
        private void CalculateFunctionValues(string functionExpression)
        {
            float dx = (X_MAX - X_MIN) / (NUM_POINTS - 1);
            for (int i = 0; i < NUM_POINTS; i++)
            {
                xValues[i] = X_MIN + i * dx;
                try
                {
                    yValues[i] = Convert.ToSingle(EvaluateFunction(functionExpression, xValues[i]).ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"函数计算出错: {ex.Message}");
                }
                return;
            }
        }

       
        public float[] GetPointList()
        {          
            float[] pointList= new float[NUM_POINTS*3];
            int index = 0;
            for (int i = 0; i < NUM_POINTS; i++)
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
            glControl1.Width = 600;
            glControl1.Height = 420;
            
            GL.ClearColor(0.5f, 0.2f, 0.5f, 1.0f);//背景颜色
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            _model = Matrix4.Identity;
            _view = Matrix4.Identity;
            _projection= Matrix4.Identity;

            _shader = new Shader(vertCoordinateShader, frageCoordinateShader, 2);
            _shader.Use();
            
        }     

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            GL.ClearColor(0.5f, 0.2f, 0.5f, 1.0f);//背景颜色
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);


            _shader.Use();
            _cVBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _cVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, _verticesCoordinate.Length * sizeof(float), _verticesCoordinate, BufferUsageHint.StaticDraw);

            _cVAO = GL.GenVertexArray();
            GL.BindVertexArray(_cVAO);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            transform = _model * _view * _projection;
            _shader.SetMatrix4("transform", transform);

            _pointList = GetPointList();

            GL.DrawArrays(PrimitiveType.Lines, 0, 4);

            _pVBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _pVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, _pointList.Length * sizeof(float), _pointList, BufferUsageHint.StaticDraw);

            _pVAO = GL.GenVertexArray();
            GL.BindVertexArray(_pVAO);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, true, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.DrawArrays(PrimitiveType.LineStrip, 0, NUM_POINTS);

            glControl1.SwapBuffers();
        }

        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            translation = new Vector2(e.X, e.Y);
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // 计算平移量
                Vector2 currentPosition = new Vector2(e.X, e.Y);
                Vector2 delta = currentPosition - translation;
                translation = currentPosition;

                // 将平移量转换为实际的坐标平移量
                float dx = delta.X * (X_MAX - X_MIN) / glControl1.Width;
                float dy = -delta.Y * (X_MAX - X_MIN) / glControl1.Height;

                

                glControl1.Invalidate();
            }
        }

        private string vertCoordinateShader = $@"
#version 450 core

layout(location = 0) in vec3 aPosition;

uniform mat4 transform;

void main(void)
{{
 
    gl_Position = vec4(aPosition, 1.0) * transform;
}}
";

        private string frageCoordinateShader = $@"
#version 330

out vec4 outputColor;

void main()
{{
    outputColor = vec4(1.0, 1.0, 0, 1.0);
}}"
 ;
    }

}
