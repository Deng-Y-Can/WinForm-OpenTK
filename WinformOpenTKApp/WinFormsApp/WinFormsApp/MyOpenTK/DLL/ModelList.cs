using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp.MyOpenTK.DLL
{
    public class ModelList
    {

        /// <summary>
        /// 根据圆心和半径生成表示三维空间中球面的点集合
        /// </summary>
        /// <param name="center">球的圆心坐标（三维向量）</param>
        /// <param name="radius">球的半径</param>
        /// <param name="latitudeCount">纬度方向上划分的份数（影响纬度角度的步长）</param>
        /// <param name="longitudeCount">经度方向上划分的份数（影响经度角度的步长）</param>
        /// <returns>表示球面的三维点集合</returns>
        public static List<Vector3> GenerateSpherePoints(Vector3 center, float radius, int latitudeCount, int longitudeCount)
        {
            List<Vector3> points = new List<Vector3>();

            if (latitudeCount <= 0 || longitudeCount <= 0)
            {
                return points;
            }

            // 计算纬度方向的角度步长
            float latitudeStep = (float)(Math.PI / latitudeCount);
            // 计算经度方向的角度步长
            float longitudeStep = (float)(2 * Math.PI / longitudeCount);

            for (int latitudeIndex = 0; latitudeIndex <= latitudeCount; latitudeIndex++)
            {
                // 当前纬度角度
                float latitude = latitudeIndex * latitudeStep - (float)(Math.PI / 2);

                for (int longitudeIndex = 0; longitudeIndex < longitudeCount; longitudeIndex++)
                {
                    // 当前经度角度
                    float longitude = longitudeIndex * longitudeStep;

                    // 根据球坐标参数方程计算当前点的坐标
                    float x = center.X + radius * (float)(Math.Cos(latitude) * Math.Cos(longitude));
                    float y = center.Y + radius * (float)(Math.Cos(latitude) * Math.Sin(longitude));
                    float z = center.Z + radius * (float)(Math.Sin(latitude));

                    Vector3 point = new Vector3(x, y, z);
                    points.Add(point);
                }
            }

            return points;
        }

        /// <summary>
        /// 根据圆心、半径以及圆上的两个点和点的数量生成表示三维空间中圆的点集合
        /// </summary>
        /// <param name="center">圆的圆心坐标（三维向量）</param>
        /// <param name="radius">圆的半径</param>
        /// <param name="pointOnCircle1">圆上的第一个点，用于确定圆所在平面</param>
        /// <param name="pointOnCircle2">圆上的第二个点，用于更准确确定圆所在平面</param>
        /// <param name="pointCount">要生成的圆上点的数量</param>
        /// <returns>表示圆的三维点集合</returns>
        public static List<Vector3> GenerateCirclePoints(Vector3 center, float radius, Vector3 pointOnCircle1, Vector3 pointOnCircle2, int pointCount)
        {
            List<Vector3> points = new List<Vector3>();

            if (pointCount <= 0)
            {
                return points;
            }

            // 计算从圆心指向圆上第一个已知点的向量，作为一个参考向量
            Vector3 v1 = Vector3.Normalize(pointOnCircle1 - center);

            // 计算从圆心指向圆上第二个已知点的向量
            Vector3 v2 = Vector3.Normalize(pointOnCircle2 - center);

            // 计算圆所在平面的法向量（通过两个向量的叉乘得到）
            Vector3 planeNormal = Vector3.Normalize(Vector3.Cross(v1, v2));

            // 寻找与平面法向量垂直的一个基向量（通过与坐标轴向量点乘判断，选择最不平行的坐标轴向量进行叉乘）
            Vector3 baseVector;
            if (Math.Abs(Vector3.Dot(planeNormal, new Vector3(1, 0, 0))) < Math.Abs(Vector3.Dot(planeNormal, new Vector3(0, 1, 0))))
            {
                if (Math.Abs(Vector3.Dot(planeNormal, new Vector3(0, 1, 0))) < Math.Abs(Vector3.Dot(planeNormal, new Vector3(0, 0, 1))))
                {
                    baseVector = Vector3.Normalize(Vector3.Cross(planeNormal, new Vector3(0, 0, 1)));
                }
                else
                {
                    baseVector = Vector3.Normalize(Vector3.Cross(planeNormal, new Vector3(0, 1, 0)));
                }
            }
            else
            {
                if (Math.Abs(Vector3.Dot(planeNormal, new Vector3(1, 0, 0))) < Math.Abs(Vector3.Dot(planeNormal, new Vector3(0, 0, 1))))
                {
                    baseVector = Vector3.Normalize(Vector3.Cross(planeNormal, new Vector3(0, 0, 1)));
                }
                else
                {
                    baseVector = Vector3.Normalize(Vector3.Cross(planeNormal, new Vector3(1, 0, 0)));
                }
            }

            // 计算第二个基向量，保证与第一个基向量以及平面法向量构成三维空间的一组基
            Vector3 anotherBaseVector = Vector3.Cross(planeNormal, baseVector);

            // 均匀角度步长，用于在圆周上均匀分布点
            float angleStep = (float)(2 * Math.PI / pointCount);

            for (int i = 0; i < pointCount; i++)
            {
                // 当前角度
                float angle = i * angleStep;

                // 根据圆的参数方程结合平面基向量计算当前点的坐标
                float x = center.X + radius * (float)(baseVector.X * Math.Cos(angle) + anotherBaseVector.X * Math.Sin(angle));
                float y = center.Y + radius * (float)(baseVector.Y * Math.Cos(angle) + anotherBaseVector.Y * Math.Sin(angle));
                float z = center.Z + radius * (float)(baseVector.Z * Math.Cos(angle) + anotherBaseVector.Z * Math.Sin(angle));

                Vector3 point = new Vector3(x, y, z);
                points.Add(point);
            }

            return points;
        }


    }

    /// <summary>
    /// 三点确定一个圆
    /// </summary>
    public class CircleFromThreePoints
    {
        public Vector3 _center;
        public float _radius;


        public CircleFromThreePoints(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            CalculateCircle(p1, p2, p3, out _center, out _radius);
        }
        // 计算两个向量的点积
        static float DotProduct(Vector3 a, Vector3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        // 计算两个向量的叉积
        static Vector3 CrossProduct(Vector3 a, Vector3 b)
        {
            return new Vector3(
                a.Y * b.Z - a.Z * b.Y,
                a.Z * b.X - a.X * b.Z,
                a.X * b.Y - a.Y * b.X
            );
        }

        // 通过三个三维坐标点求圆的圆心和半径的核心方法
        public static void CalculateCircle(Vector3 p1, Vector3 p2, Vector3 p3, out Vector3 center, out float radius)
        {
            // 计算两条边向量
            Vector3 v1 = p2 - p1;
            Vector3 v2 = p3 - p1;

            // 计算平面法向量
            Vector3 normal = CrossProduct(v1, v2);

            // 构建线性方程组的系数矩阵
            Matrix3 A = new Matrix3();
            A.Row0 = new Vector3(2 * (p2.X - p1.X), 2 * (p2.Y - p1.Y), 2 * (p2.Z - p1.Z));
            A.Row1 = new Vector3(2 * (p3.X - p1.X), 2 * (p3.Y - p1.Y), 2 * (p3.Z - p1.Z));
            A.Row2 = new Vector3(normal.X, normal.Y, normal.Z);

            // 构建常数项矩阵（这里以向量形式表示，便于后续计算）
            Vector3 constants = new Vector3();
            constants.X = p2.X * p2.X - p1.X * p1.X + p2.Y * p2.Y - p1.Y * p1.Y + p2.Z * p2.Z - p1.Z * p1.Z;
            constants.Y = p3.X * p3.X - p1.X * p1.X + p3.Y * p3.Y - p1.Y * p1.Y + p3.Z * p3.Z - p1.Z * p1.Z;
            constants.Z = DotProduct(normal, p1);

            // 求解线性方程组 Ax = b，利用OpenTK的矩阵求逆和乘法操作
            Matrix3 inverseA = Matrix3.Invert(A);
            Vector3 solution = inverseA * constants;

            // 获取圆心坐标
            center = new Vector3(solution.X, solution.Y, solution.Z);

            // 计算半径，使用圆心到其中一个点的距离作为半径
            radius = Vector3.Distance(center, p1);
        }
    }
 }
