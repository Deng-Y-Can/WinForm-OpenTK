using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private readonly float[] _vertices =
        {
            -0.5f, -0.5f, 0.0f, // Bottom-left vertex
             0.5f, -0.5f, 0.0f, // Bottom-right vertex
             0.0f,  0.5f, 0.0f  // Top vertex
        };

        private int _vertexBufferObject;

        private int _vertexArrayObject;

        private Shader _shader;

        private void glControl1_Click(object sender, EventArgs e)
        {
            // ������Ȳ���
            GL.Enable(EnableCap.DepthTest);

            //// ������Ȳ��Ժ���
            //GL.DepthFunc(DepthFunction.Less);

            //// ���ñ������
            //GL.Enable(EnableCap.CullFace);

            // �����ӿڴ�С
            GL.Viewport(0, 0, 400, 300);

            // �����ɫ�������Ȼ���
            // GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f); // ���������ɫΪ��ɫ
            // GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.ClearColor(0.5f, 0.2f, 0.5f, 1.0f);//������ɫ
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);


            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);


            _shader = new Shader(vertShader, frageShader, 2);
            _shader.Use();

            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            glControl1.SwapBuffers();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ������Ȳ���
            GL.Enable(EnableCap.DepthTest);

            //// ������Ȳ��Ժ���
            //GL.DepthFunc(DepthFunction.Less);

            //// ���ñ������
            //GL.Enable(EnableCap.CullFace);

            // �����ӿڴ�С
            GL.Viewport(0, 0, 400, 300);

            // �����ɫ�������Ȼ���
            // GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f); // ���������ɫΪ��ɫ
            // GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.ClearColor(0.2f, 0.7f, 0.5f, 1.0f);//������ɫ
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);


            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            _shader = new Shader(vertShader, frageShader, 2);
            //_shader = new Shader("Shaders/shader.vert1", "Shaders/shader.frage1");
            _shader.Use();

            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            glControl1.SwapBuffers();
        }

        private string vertShader = $@"

#version 330 core

layout(location = 0) in vec3 aPosition;

void main(void)
{{
    gl_Position = vec4(aPosition, 1.0);
}}";

        private string frageShader = $@"
#version 330

out vec4 outputColor;

void main()
{{
    outputColor = vec4(1.0, 1.0, 0, 1.0);
}}";
    }
}
