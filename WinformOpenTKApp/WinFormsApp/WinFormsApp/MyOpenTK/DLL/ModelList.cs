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

        /// <summary>
        /// 矩形
        /// </summary>
        /// <param name="center"></param>
        /// <param name="pointOnCircle1"></param>
        /// <returns></returns>
        public static List<Vector3> Rectangle(Vector3 center, Vector3 pointOnCircle1)
        {
            List<Vector3> vertices = new List<Vector3>();
            // 计算对角线向量
            Vector3 diagonalVector = pointOnCircle1 - center;

            // 通过向量叉乘获取与对角线向量垂直的两个相互垂直的单位向量
            // 先选择一个参考向量（这里选择 (0, 0, 1) 作为参考向量，只要不和对角线向量平行就行，若平行需要换参考向量）
            Vector3 referenceVector = new Vector3(0, 0, 1);
            // 检查对角线向量是否与参考向量平行，如果平行则换一个参考向量（比如 (0, 1, 0) ）
            if (Math.Abs(Vector3.Dot(diagonalVector, referenceVector)) / (diagonalVector.Length * referenceVector.Length) == 1)
            {
                referenceVector = new Vector3(0, 1, 0);
            }

            // 使用向量叉乘获取第一个垂直向量
            Vector3 perpendicularVector1 = Vector3.Cross(diagonalVector, referenceVector);
            perpendicularVector1.Normalize();

            // 使用向量叉乘获取第二个垂直向量（与对角线向量和第一个垂直向量都垂直）
            Vector3 perpendicularVector2 = Vector3.Cross(diagonalVector, perpendicularVector1);
            perpendicularVector2.Normalize();

            // 计算矩形另外两个顶点的向量偏移量（通过将垂直单位向量乘以对角线向量长度的一半）
            Vector3 offset1 = perpendicularVector1 * (diagonalVector.Length / 2);
            Vector3 offset2 = perpendicularVector2 * (diagonalVector.Length / 2);

            // 计算四个顶点坐标并添加到列表中
            vertices.Add(center + offset1 + offset2);
            vertices.Add(center + offset1 - offset2);
            vertices.Add(center - offset1 - offset2);
            vertices.Add(center - offset1 + offset2);

            return vertices;
        }

        public static List<Vector3> RectangleList(Vector3 center, Vector3 pointOnCircle1, int m, int n)
        {
            List<Vector3> vertices = new List<Vector3>();

            // 计算对角线向量
            Vector3 diagonalVector = pointOnCircle1 - center;

            // 通过向量叉乘获取与对角线向量垂直的两个相互垂直的单位向量
            // 先选择一个参考向量（这里选择 (0, 0, 1) 作为参考向量，只要不和对角线向量平行就行，若平行需要换参考向量）
            Vector3 referenceVector = new Vector3(0, 0, 1);
            // 检查对角线向量是否与参考向量平行，如果平行则换一个参考向量（比如 (0, 1, 0) ）
            if (Math.Abs(Vector3.Dot(diagonalVector, referenceVector)) / (diagonalVector.Length * referenceVector.Length) == 1)
            {
                referenceVector = new Vector3(0, 1, 0);
            }

            // 使用向量叉乘获取第一个垂直向量
            Vector3 perpendicularVector1 = Vector3.Cross(diagonalVector, referenceVector);
            perpendicularVector1.Normalize();

            // 使用向量叉乘获取第二个垂直向量（与对角线向量和第一个垂直向量都垂直）
            Vector3 perpendicularVector2 = Vector3.Cross(diagonalVector, perpendicularVector1);
            perpendicularVector2.Normalize();

            // 计算矩形另外两个顶点的向量偏移量（通过将垂直单位向量乘以对角线向量长度的一半）
            Vector3 offset1 = perpendicularVector1 * (diagonalVector.Length / 2);
            Vector3 offset2 = perpendicularVector2 * (diagonalVector.Length / 2);

            // 生成四条边上的点
            // 第一条边（从 center + offset1 + offset2 到 center + offset1 - offset2）
            for (int i = 0; i < m; i++)
            {
                float t = (float)i / (m - 1);
                Vector3 interpolatedPoint = Vector3.Lerp(center + offset1 + offset2, center + offset1 - offset2, t);
                vertices.Add(interpolatedPoint);
            }

            // 第二条边（从 center + offset1 - offset2 到 center - offset1 - offset2）
            for (int i = 0; i < n; i++)
            {
                float t = (float)i / (n - 1);
                Vector3 interpolatedPoint = Vector3.Lerp(center + offset1 - offset2, center - offset1 - offset2, t);
                vertices.Add(interpolatedPoint);
            }

            // 第三条边（从 center - offset1 - offset2 到 center - offset1 + offset2）
            for (int i = 0; i < m; i++)
            {
                float t = (float)i / (m - 1);
                Vector3 interpolatedPoint = Vector3.Lerp(center - offset1 - offset2, center - offset1 + offset2, t);
                vertices.Add(interpolatedPoint);
            }

            // 第四条边（从 center - offset1 + offset2 到 center + offset1 + offset2）
            for (int i = 0; i < n; i++)
            {
                float t = (float)i / (n - 1);
                Vector3 interpolatedPoint = Vector3.Lerp(center - offset1 + offset2, center + offset1 + offset2, t);
                vertices.Add(interpolatedPoint);
            }

            return vertices;
        }


        /// <summary>
        /// 正方体
        /// </summary>
        /// <param name="center"></param>
        /// <param name="pointOnEdge"></param>
        /// <returns></returns>
        public static List<Vector3> Cube(Vector3 center, Vector3 pointOnEdge)
        {
            List<Vector3> vertices = new List<Vector3>();
            // 计算从中心点指向棱上一点的向量（代表正方体一条棱的方向向量）
            Vector3 edgeVector = pointOnEdge - center;

            // 获取与 edgeVector 垂直的另外两个相互垂直的单位向量（通过向量叉乘）
            // 先选择一个参考向量（这里选择 (0, 0, 1) 作为参考向量，只要不和 edgeVector 平行就行，若平行需要换参考向量）
            Vector3 referenceVector = new Vector3(0, 0, 1);
            if (Math.Abs(Vector3.Dot(edgeVector, referenceVector)) / (edgeVector.Length * referenceVector.Length) == 1)
            {
                referenceVector = new Vector3(0, 1, 0);
            }

            // 使用向量叉乘获取第一个垂直向量
            Vector3 perpendicularVector1 = Vector3.Cross(edgeVector, referenceVector);
            perpendicularVector1.Normalize();

            // 使用向量叉乘获取第二个垂直向量（与 edgeVector 和第一个垂直向量都垂直）
            Vector3 perpendicularVector2 = Vector3.Cross(edgeVector, perpendicularVector1);
            perpendicularVector2.Normalize();

            // 计算出正方体三条棱方向上的半边长向量（假设输入的 pointOnEdge 到中心的距离就是正方体的棱长）
            Vector3 halfEdgeVector = edgeVector / 2;
            Vector3 halfPerpendicularVector1 = perpendicularVector1 / 2;
            Vector3 halfPerpendicularVector2 = perpendicularVector2 / 2;

            // 通过中心和这三个半边长向量的不同组合来计算八个顶点坐标
            vertices.Add(center + halfEdgeVector + halfPerpendicularVector1 + halfPerpendicularVector2);
            vertices.Add(center + halfEdgeVector + halfPerpendicularVector1 - halfPerpendicularVector2);
            vertices.Add(center + halfEdgeVector - halfPerpendicularVector1 - halfPerpendicularVector2);
            vertices.Add(center + halfEdgeVector - halfPerpendicularVector1 + halfPerpendicularVector2);
            vertices.Add(center - halfEdgeVector + halfPerpendicularVector1 + halfPerpendicularVector2);
            vertices.Add(center - halfEdgeVector + halfPerpendicularVector1 - halfPerpendicularVector2);
            vertices.Add(center - halfEdgeVector - halfPerpendicularVector1 - halfPerpendicularVector2);
            vertices.Add(center - halfEdgeVector - halfPerpendicularVector1 + halfPerpendicularVector2);

            return vertices;
        }

        public static List<Vector3> CubeList(Vector3 center, Vector3 pointOnEdge, int n)
        {
            List<Vector3> vertices = new List<Vector3>();

            // 计算从中心点指向棱上一点的向量（代表正方体一条棱的方向向量）
            Vector3 edgeVector = pointOnEdge - center;

            // 获取与 edgeVector 垂直的另外两个相互垂直的单位向量（通过向量叉乘）
            // 先选择一个参考向量（这里选择 (0, 0, 1) 作为参考向量，只要不和 edgeVector 平行就行，若平行需要换参考向量）
            Vector3 referenceVector = new Vector3(0, 0, 1);
            if (Math.Abs(Vector3.Dot(edgeVector, referenceVector)) / (edgeVector.Length * referenceVector.Length) == 1)
            {
                referenceVector = new Vector3(0, 1, 0);
            }

            // 使用向量叉乘获取第一个垂直向量
            Vector3 perpendicularVector1 = Vector3.Cross(edgeVector, referenceVector);
            perpendicularVector1.Normalize();

            // 使用向量叉乘获取第二个垂直向量（与 edgeVector 和第一个垂直向量都垂直）
            Vector3 perpendicularVector2 = Vector3.Cross(edgeVector, perpendicularVector1);
            perpendicularVector2.Normalize();

            // 计算出正方体三条棱方向上的半边长向量（假设输入的 pointOnEdge 到中心的距离就是正方体的棱长）
            Vector3 halfEdgeVector = edgeVector / 2;
            Vector3 halfPerpendicularVector1 = perpendicularVector1 / 2;
            Vector3 halfPerpendicularVector2 = perpendicularVector2 / 2;

            // 遍历正方体的六个面，生成每个面上的点
            for (int face = 0; face < 6; face++)
            {
                // 根据面的不同，确定面上四条边对应的向量组合
                Vector3[] edgeVectorsForFace = new Vector3[4];
                switch (face)
                {
                    case 0: // 前面（以中心为参考，向外方向为正方向来看各向量组合情况）
                        edgeVectorsForFace[0] = halfEdgeVector + halfPerpendicularVector1 + halfPerpendicularVector2;
                        edgeVectorsForFace[1] = halfEdgeVector + halfPerpendicularVector1 - halfPerpendicularVector2;
                        edgeVectorsForFace[2] = halfEdgeVector - halfPerpendicularVector1 - halfPerpendicularVector2;
                        edgeVectorsForFace[3] = halfEdgeVector - halfPerpendicularVector1 + halfPerpendicularVector2;
                        break;
                    case 1: // 后面
                        edgeVectorsForFace[0] = -halfEdgeVector + halfPerpendicularVector1 + halfPerpendicularVector2;
                        edgeVectorsForFace[1] = -halfEdgeVector + halfPerpendicularVector1 - halfPerpendicularVector2;
                        edgeVectorsForFace[2] = -halfEdgeVector - halfPerpendicularVector1 - halfPerpendicularVector2;
                        edgeVectorsForFace[3] = -halfEdgeVector - halfPerpendicularVector1 + halfPerpendicularVector2;
                        break;
                    case 2: // 上面
                        edgeVectorsForFace[0] = halfEdgeVector + halfPerpendicularVector1 + halfPerpendicularVector2;
                        edgeVectorsForFace[1] = halfEdgeVector + halfPerpendicularVector1 - halfPerpendicularVector2;
                        edgeVectorsForFace[2] = -halfEdgeVector + halfPerpendicularVector1 - halfPerpendicularVector2;
                        edgeVectorsForFace[3] = -halfEdgeVector + halfPerpendicularVector1 + halfPerpendicularVector2;
                        break;
                    case 3: // 下面
                        edgeVectorsForFace[0] = halfEdgeVector - halfPerpendicularVector1 + halfPerpendicularVector2;
                        edgeVectorsForFace[1] = halfEdgeVector - halfPerpendicularVector1 - halfPerpendicularVector2;
                        edgeVectorsForFace[2] = -halfEdgeVector - halfPerpendicularVector1 - halfPerpendicularVector2;
                        edgeVectorsForFace[3] = -halfEdgeVector - halfPerpendicularVector1 + halfPerpendicularVector2;
                        break;
                    case 4: // 左面
                        edgeVectorsForFace[0] = halfEdgeVector + halfPerpendicularVector1 + halfPerpendicularVector2;
                        edgeVectorsForFace[1] = -halfEdgeVector + halfPerpendicularVector1 + halfPerpendicularVector2;
                        edgeVectorsForFace[2] = -halfEdgeVector - halfPerpendicularVector1 + halfPerpendicularVector2;
                        edgeVectorsForFace[3] = halfEdgeVector - halfPerpendicularVector1 + halfPerpendicularVector2;
                        break;
                    case 5: // 右面
                        edgeVectorsForFace[0] = halfEdgeVector + halfPerpendicularVector1 - halfPerpendicularVector2;
                        edgeVectorsForFace[1] = -halfEdgeVector + halfPerpendicularVector1 - halfPerpendicularVector2;
                        edgeVectorsForFace[2] = -halfEdgeVector - halfPerpendicularVector1 - halfPerpendicularVector2;
                        edgeVectorsForFace[3] = halfEdgeVector - halfPerpendicularVector1 - halfPerpendicularVector2;
                        break;
                }

                // 对于当前面的四条边，分别通过插值生成多个点
                for (int edge = 0; edge < 4; edge++)
                {
                    Vector3 start = edgeVectorsForFace[edge];
                    Vector3 end = edgeVectorsForFace[(edge + 1) % 4];
                    for (int i = 0; i < n; i++)
                    {
                        float t = (float)i / (n - 1);
                        Vector3 interpolatedPoint = Vector3.Lerp(start, end, t);
                        vertices.Add(center + interpolatedPoint);
                    }
                }
            }

            return vertices;
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
