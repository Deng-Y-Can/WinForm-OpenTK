using LearnOpenTK.Common;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp.CandyModel;
using WinFormsApp.MyOpenTK.DLL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp.MyOpenTK
{
    public partial class CandyHomeForm : Form
    {
        public CandyHomeForm()
        {
            InitializeComponent();
            glControl1.MouseWheel += new System.Windows.Forms.MouseEventHandler(MouseWheel);

        }

        private float cameraHeight = 20;

        private Color4 _backgroundColor = new Color4(0.6f, 0.6f, 1, 1);  //背景颜色
        private Matrix4 _model;
        private int _mainVBO;
        private int _mainVAO;
        private Shader _shader;
        private SpaceModel candyModel = new SpaceModel();

        private void InitializationParam()
        {

            Color4 color = new Color4(0.7f, 0.2f, 0.5f, 1);
            //球
            // Vector3 center = new Vector3(0, 0, 0);
            //candyModel._vector = DataTool.Vector3ListToArray(ModelList.GenerateSpherePoints(center, 5, 100, 100));

            //圆
            Vector3 vector3 = new Vector3(5, 0, 1);
            Vector3 vector31 = new Vector3(7, 2, 2);
            Vector3 vector32 = new Vector3(4, 5, 3);
            CircleFromThreePoints circleFromThreePoints = new CircleFromThreePoints(vector3, vector31, vector32);
            List<Vector3> TestData = ModelList.GenerateCirclePoints(circleFromThreePoints._center, circleFromThreePoints._radius, vector3, vector31, 100);
            candyModel._vector = DataTool.Vector3ListToArray(TestData);

            candyModel._primitiveType = PrimitiveType.Points;
            candyModel._color = color;
            Render();
        }
        private void glControl1_Load(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            ClearColor();
            _model = Matrix4.Identity;
            _camera = new Camera(Vector3.UnitZ * cameraHeight, 1.2f);
            Color4 color = new Color4(0.7f, 0.2f, 0.5f, 1);
            _shader = new Shader(vertMainShader, GetFrageSharde(color), 2);
            _shader.Use();
            InitializationParam();
        }

        private void BindData(SpaceModel spaceModel)
        {
            _shader = new Shader(vertMainShader, GetFrageSharde(spaceModel._color), 2);
            _shader.Use();
            SetMVP(_shader);
            _mainVBO = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _mainVBO);
            GL.BufferData(BufferTarget.ArrayBuffer, spaceModel._vector.Length * sizeof(float), spaceModel._vector, BufferUsageHint.StaticDraw);

            _mainVAO = GL.GenVertexArray();
            GL.BindVertexArray(_mainVAO);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.DrawArrays(spaceModel._primitiveType, 0, spaceModel._vector.Length);
        }

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
        private void ClearColor()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
            GL.ClearColor(_backgroundColor.R, _backgroundColor.G, _backgroundColor.B, 1.0f);//背景颜色
        }
        private bool _firstMove = true;
        private float unMax = 999999999;
        private Vector2 _lastPos;
        private const float _cameraSpeed = 1.5f;
        private const float _sensitivity = 0.05f;
        private float _rotatefactor = 0.1f;
        private float _maxMouseMoveDistance = 40f;
        private float scale = 1;
        private float _scalingPositionFactor = 15f;
        private bool _mouseButtonDown = false;
        private Quaternion rotationQuaternion;
        private Camera _camera;
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
                        Quaternion yawQuaternion = Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(deltaX * _rotatefactor / scale));
                        Quaternion pitchQuaternion = Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(deltaY * _rotatefactor / scale));

                        rotationQuaternion = pitchQuaternion * yawQuaternion;
                        Matrix4 rotationMatrix = Matrix4.CreateFromQuaternion(rotationQuaternion);
                        _model = _model * rotationMatrix;
                    }
                    else
                    {
                        _camera.Position -= _camera.Right * deltaX * _sensitivity / scale;
                        _camera.Position -= _camera.Up * -deltaY * _sensitivity / scale;
                    }
                    Render();
                }
            }
        }
        /// <summary>
        /// 鼠标滚轮控制模型放缩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (_camera != null)
            {
                //滚轮事件

                float frontLength = e.Delta / 100 * _scalingPositionFactor;
                _camera.Position += _camera.Front * frontLength;

                _firstMove = true;
                Render();
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
        public void Render()
        {
            ClearColor();
            _shader.Use();
            BindData(candyModel);
            glControl1.SwapBuffers();
        }
        private void SetMVP(Shader shader)
        {
            shader.SetMatrix4("model", _model);
            shader.SetMatrix4("view", _camera.GetViewMatrix());
            shader.SetMatrix4("projection", _camera.GetProjectionMatrix());
        }
        private void SetUnitMVP(Shader shader)
        {
            shader.SetMatrix4("model", Matrix4.Identity);
            shader.SetMatrix4("view", Matrix4.Identity);
            shader.SetMatrix4("projection", Matrix4.Identity);
        }
        private Matrix4 GetMVP()
        {
            return _model * _camera.GetViewMatrix() * _camera.GetProjectionMatrix();
        }

        private string GetFrageSharde(Color4 color)
        {
            string frageMainShader = "";
            frageMainShader = $@"
            #version 330
            out vec4 frageColor;      
            void main()
            {{
               frageColor =  vec4({color.R}, {color.G},{color.B}, {color.A});
            }}"
            ;
            return frageMainShader;
        }



        private string vertMainShader = $@"
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

        private void CandyHomeForm_SizeChanged(object sender, EventArgs e)
        {
            glControl1.Width = (int)(this.Width * 0.7);
            glControl1.Height = (int)(this.Width * 0.7);
            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);
            Render();
        }
    }
}
