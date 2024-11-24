using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
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


            _shader = new Shader("Shaders/shader.vert1", "Shaders/shader.frage1");
            _shader.Use();

            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            glControl1.SwapBuffers();
        }
    }
}
