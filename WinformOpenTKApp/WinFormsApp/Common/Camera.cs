using OpenTK.Mathematics;
using System;

namespace LearnOpenTK.Common
{
    
    public class Camera
    {
        private Vector3 _front = -Vector3.UnitZ;
        private Vector3 _up = Vector3.UnitY;
        private Vector3 _right = Vector3.UnitX;

        private float _pitch;
        private float _yaw = -MathHelper.PiOver2;
        private float _fov = MathHelper.PiOver2;

        public Camera(Vector3 position, float aspectRatio)
        {
            Position = position;
            AspectRatio = aspectRatio;
        }
        public Vector3 Position { get; set; }
        public float AspectRatio { private get; set; }
        public Vector3 Front => _front;
        public Vector3 Up => _up;
        public Vector3 Right => _right;
        public float Pitch
        {
            get => MathHelper.RadiansToDegrees(_pitch);
            set
            {

                //var angle = MathHelper.Clamp(value, -89f, 89f);
                var angle = MathHelper.Clamp(value, -180f, 180f);
                _pitch = MathHelper.DegreesToRadians(angle);
                UpdateVectors();
            }
        }
        public float Yaw
        {
            get => MathHelper.RadiansToDegrees(_yaw);
            set
            {
                _yaw = MathHelper.DegreesToRadians(value);
                UpdateVectors();
            }
        }
        public float Fov
        {
            get => MathHelper.RadiansToDegrees(_fov);
            set
            {
                var angle = MathHelper.Clamp(value, 1f, 90f);
                _fov = MathHelper.DegreesToRadians(angle);
            }
        }
        // 使用四元数表示旋转
        private Quaternion rotationQuaternion;
        public Matrix4 GetViewMatrix()
        {
            UpdateVectors();
            return Matrix4.LookAt(Position, Position + _front, _up);
        }


        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(_fov, AspectRatio, 0.01f, 10000f);
        }

        public Vector3 GetNormal()
        {
            return Vector3.Normalize(Position + _front);
        }


        private void UpdateVectors()
        {
            //_front.X = MathF.Cos(_pitch) * MathF.Cos(_yaw);
            //_front.Y = MathF.Sin(_pitch);
            //_front.Z = MathF.Cos(_pitch) * MathF.Sin(_yaw);           
            //_front = Vector3.Normalize(_front);        
            //_right = Vector3.Normalize(Vector3.Cross(_front, Vector3.UnitY));
            //_up = Vector3.Normalize(Vector3.Cross(_right, _front));

            Quaternion yawQuaternion = Quaternion.FromAxisAngle(Vector3.UnitY, MathHelper.DegreesToRadians(_yaw));
            Quaternion pitchQuaternion = Quaternion.FromAxisAngle(Vector3.UnitX, MathHelper.DegreesToRadians(_pitch));

            rotationQuaternion = pitchQuaternion * yawQuaternion;
            Matrix4 rotationMatrix = Matrix4.CreateFromQuaternion(rotationQuaternion);
            Vector3.Transform(-Vector3.UnitZ, rotationQuaternion, out _front);
            Vector3.Transform(Vector3.UnitX, rotationQuaternion, out _right);
            Vector3.Transform(Vector3.UnitY, rotationQuaternion, out _up);


        }

        public static Vector4 EulerToQuaternion(float yaw, float pitch, float roll)
        {
            float cy = MathF.Cos(yaw * 0.5f);
            float sy = MathF.Sin(yaw * 0.5f);
            float cp = MathF.Cos(pitch * 0.5f);
            float sp = MathF.Sin(pitch * 0.5f);
            float cr = MathF.Cos(roll * 0.5f);
            float sr = MathF.Sin(roll * 0.5f);

            float w = cy * cp * cr + sy * sp * sr;
            float x = sy * cp * cr - cy * sp * sr;
            float y = cy * sp * cr + sy * cp * sr;
            float z = cy * cp * sr - sy * sp * cr;
            Vector3 vector = new Vector3(x / w, y / w, z / w);
            return new Vector4(x, y, z, w);
        }
    }
}