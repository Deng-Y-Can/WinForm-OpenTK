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
        /// 球
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
        /// 圆
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
        /// 正方形
        /// </summary>
        /// <param name="center"></param>
        /// <param name="edgeMidpointDirection"></param>
        /// <returns></returns>
        public static List<Vector3> Square(Vector3 center, Vector3 edgeMidpointDirection)
        {
            List<Vector3> vertices = new List<Vector3>();

            // 先将指向边中点的向量归一化为单位向量
            Vector3 unitEdgeMidpointDirection = edgeMidpointDirection.Normalized();

            // 通过向量叉乘获取与边中点方向向量垂直的单位向量（代表正方形边的方向向量）
            Vector3 edgeDirection = Vector3.Cross(unitEdgeMidpointDirection, new Vector3(0, 0, 1));
            if (edgeDirection.LengthSquared == 0)
            {
                edgeDirection = Vector3.Cross(unitEdgeMidpointDirection, new Vector3(0, 1, 0));
            }
            edgeDirection = edgeDirection.Normalized();

            // 计算边长的一半（假设从中心点到边中点的距离就是边长的一半）
            float halfSideLength = unitEdgeMidpointDirection.Length;

            // 通过中心点和边方向向量以及边长一半来计算四个顶点坐标
            vertices.Add(center + edgeDirection * halfSideLength + unitEdgeMidpointDirection * halfSideLength);
            vertices.Add(center + edgeDirection * halfSideLength - unitEdgeMidpointDirection * halfSideLength);
            vertices.Add(center - edgeDirection * halfSideLength - unitEdgeMidpointDirection * halfSideLength);
            vertices.Add(center - edgeDirection * halfSideLength + unitEdgeMidpointDirection * halfSideLength);

            return vertices;
        }

        public static List<Vector3> Square(Vector3 center, Vector3 edgeMidpointDirection, int n)
        {
            List<Vector3> vertices = new List<Vector3>();

            // 归一化边中点方向向量
            Vector3 unitEdgeMidpointDirection = edgeMidpointDirection.Normalized();

            // 通过向量叉乘获取与边中点方向向量垂直的单位向量（代表正方形边的方向向量）
            Vector3 edgeDirection = Vector3.Cross(unitEdgeMidpointDirection, new Vector3(0, 0, 1));
            if (edgeDirection.LengthSquared == 0)
            {
                edgeDirection = Vector3.Cross(unitEdgeMidpointDirection, new Vector3(0, 1, 0));
            }
            edgeDirection = edgeDirection.Normalized();

            // 计算边长的一半（假设从中心点到边中点的距离就是边长的一半）
            float halfSideLength = unitEdgeMidpointDirection.Length;

            // 计算相邻两点间的距离（边长均匀分割后每段的长度）
            float stepLength = halfSideLength * 2 / (n - 1);

            // 遍历每条边，根据每条边上的点数计算并添加相应顶点坐标
            for (int i = 0; i < n; i++)
            {
                float offset1 = (float)((i - (n - 1) / 2.0) * stepLength);
                for (int j = 0; j < n; j++)
                {
                    float offset2 = (float)((j - (n - 1) / 2.0) * stepLength);
                    Vector3 vertex = center + edgeDirection * offset1 + unitEdgeMidpointDirection * offset2;
                    vertices.Add(vertex);
                }
            }
                
            return vertices;
        }

        /// <summary>
        /// 矩形
        /// </summary>
        /// <param name="center"></param>
        /// <param name="longEdgeMidpointDirection"></param>
        /// <param name="shortEdgeMidpointDirection"></param>
        /// <returns></returns>
        public static List<Vector3> Rectangle(Vector3 center, Vector3 longEdgeMidpointDirection, Vector3 shortEdgeMidpointDirection)
        {
            List<Vector3> vertices = new List<Vector3>();

            // 归一化长边中点方向向量
            Vector3 unitLongEdgeMidpointDirection = longEdgeMidpointDirection.Normalized();
            // 归一化短边中点方向向量
            Vector3 unitShortEdgeMidpointDirection = shortEdgeMidpointDirection.Normalized();

            // 通过向量叉乘获取与长边中点方向向量垂直且和短边方向相关的单位向量（代表长方形长边的方向向量）
            Vector3 longEdgeDirection = Vector3.Cross(unitLongEdgeMidpointDirection, unitShortEdgeMidpointDirection);
            longEdgeDirection = longEdgeDirection.Normalized();

            // 通过向量叉乘获取与短边中点方向向量垂直且和长边方向相关的单位向量（代表长方形短边的方向向量）
            Vector3 shortEdgeDirection = Vector3.Cross(unitShortEdgeMidpointDirection, longEdgeDirection);
            shortEdgeDirection = shortEdgeDirection.Normalized();

            // 计算长边长度的一半（假设从中心点到长边中点的距离就是长边长度的一半）
            float halfLongSideLength = unitLongEdgeMidpointDirection.Length;
            // 计算短边长度的一半（假设从中心点到短边中点的距离就是短边长度的一半）
            float halfShortSideLength = unitShortEdgeMidpointDirection.Length;

            // 通过中心点和边方向向量以及边长一半来计算四个顶点坐标
            vertices.Add(center + longEdgeDirection * halfLongSideLength + shortEdgeDirection * halfShortSideLength);
            vertices.Add(center + longEdgeDirection * halfLongSideLength - shortEdgeDirection * halfShortSideLength);
            vertices.Add(center - longEdgeDirection * halfLongSideLength - shortEdgeDirection * halfShortSideLength);
            vertices.Add(center - longEdgeDirection * halfLongSideLength + shortEdgeDirection * halfShortSideLength);

            return vertices;
        }

        public static List<Vector3> Rectangle(Vector3 center, Vector3 longEdgeMidpointDirection, Vector3 shortEdgeMidpointDirection, float longEdgePoints, float shortEdgePoints)
        {
            List<Vector3> vertices = new List<Vector3>();

            // 归一化长边中点方向向量
            Vector3 unitLongEdgeMidpointDirection = longEdgeMidpointDirection.Normalized();
            // 归一化短边中点方向向量
            Vector3 unitShortEdgeMidpointDirection = shortEdgeMidpointDirection.Normalized();

            // 通过向量叉乘获取与长边中点方向向量垂直且和短边方向相关的单位向量（代表长方形长边的方向向量）
            Vector3 longEdgeDirection = Vector3.Cross(unitLongEdgeMidpointDirection, unitShortEdgeMidpointDirection);
            longEdgeDirection = longEdgeDirection.Normalized();

            // 通过向量叉乘获取与短边中点方向向量垂直且和长边方向相关的单位向量（代表长方形短边的方向向量）
            Vector3 shortEdgeDirection = Vector3.Cross(unitShortEdgeMidpointDirection, longEdgeDirection);
            shortEdgeDirection = shortEdgeDirection.Normalized();

            // 计算长边长度的一半（假设从中心点到长边中点的距离就是长边长度的一半）
            float halfLongSideLength = (float)unitLongEdgeMidpointDirection.Length;
            // 计算短边长度的一半（假设从中心点到短边中点的距离就是短边长度的一半）
            float halfShortSideLength = (float)unitShortEdgeMidpointDirection.Length;

            // 计算长边相邻两点间的距离（长边均匀分割后每段的长度）
            float longEdgeStepLength = halfLongSideLength * 2 / (longEdgePoints - 1);
            // 计算短边相邻两点间的距离（短边均匀分割后每段的长度）
            float shortEdgeStepLength = halfShortSideLength * 2 / (shortEdgePoints - 1);

            // 遍历长边和短边，根据每条边上的点数计算并添加相应顶点坐标
            for (float i = 0; i < longEdgePoints; i++)
            {
                float longEdgeOffset = (i - (longEdgePoints - 1) / 2f) * longEdgeStepLength;
                for (float j = 0; j < shortEdgePoints; j++)
                {
                    float shortEdgeOffset = (j - (shortEdgePoints - 1) / 2f) * shortEdgeStepLength;
                    Vector3 vertex = center + longEdgeDirection * longEdgeOffset + shortEdgeDirection * shortEdgeOffset;
                    vertices.Add(vertex);
                }
            }

            return vertices;
        }
        /// <summary>
        /// 三角形
        /// </summary>
        /// <param name="vertexA"></param>
        /// <param name="vertexB"></param>
        /// <param name="vertexC"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static List<Vector3> Triangle(Vector3 vertexA, Vector3 vertexB, Vector3 vertexC, float n)
        {
            List<Vector3> vertices = new List<Vector3>();

            // 计算三角形三条边对应的向量
            Vector3 edgeAB = vertexB - vertexA;
            Vector3 edgeBC = vertexC - vertexB;
            Vector3 edgeCA = vertexA - vertexC;

            // 计算每条边的长度
            float lengthAB = (float)edgeAB.Length;
            float lengthBC = (float)edgeBC.Length;
            float lengthCA = (float)edgeCA.Length;

            // 计算每条边上相邻两点间的距离（均匀分割每条边）
            float stepLengthAB = lengthAB / (n - 1);
            float stepLengthBC = lengthBC / (n - 1);
            float stepLengthCA = lengthCA / (n - 1);

            // 遍历边AB添加顶点坐标
            for (float i = 0; i < n; i++)
            {
                Vector3 currentVertex = vertexA + edgeAB * (i / (n - 1));
                vertices.Add(currentVertex);
            }

            // 遍历边BC添加顶点坐标（需要排除已经添加过的顶点B）
            for (float i = 1; i < n; i++)
            {
                Vector3 currentVertex = vertexB + edgeBC * (i / (n - 1));
                vertices.Add(currentVertex);
            }

            // 遍历边CA添加顶点坐标（需要排除已经添加过的顶点C和顶点A）
            for (float i = 1; i < n - 1; i++)
            {
                Vector3 currentVertex = vertexC + edgeCA * (i / (n - 1));
                vertices.Add(currentVertex);
            }

            return vertices;
        }
        /// <summary>
        /// 平行四边形
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="edgeVector1"></param>
        /// <param name="edgeVector2"></param>
        /// <returns></returns>
        public static List<Vector3> Parallelogram(Vector3 vertex, Vector3 edgeVector1, Vector3 edgeVector2)
        {
            List<Vector3> vertices = new List<Vector3>();

            // 直接添加第一个顶点坐标
            vertices.Add(vertex);

            // 通过第一个顶点坐标和第一条边向量计算第二个顶点坐标
            Vector3 vertex2 = vertex + edgeVector1;
            vertices.Add(vertex2);

            // 通过第一个顶点坐标和第二条边向量计算第三个顶点坐标
            Vector3 vertex3 = vertex + edgeVector2;
            vertices.Add(vertex3);

            // 通过第一个顶点坐标、第一条边向量和第二条边向量计算第四个顶点坐标
            Vector3 vertex4 = vertex + edgeVector1 + edgeVector2;
            vertices.Add(vertex4);

            return vertices;
        }

        public static List<Vector3> Parallelogram(Vector3 vertex, Vector3 edgeVector1, Vector3 edgeVector2, float edge1Points, float edge2Points)
        {
            List<Vector3> vertices = new List<Vector3>();

            // 计算第一条边相邻两点间的距离（将边均匀分割后每段的长度）
            float stepLength1 = edgeVector1.Length / (edge1Points - 1);
            // 计算第二条边相邻两点间的距离（将边均匀分割后每段的长度）
            float stepLength2 = edgeVector2.Length / (edge2Points - 1);

            // 遍历第一条边，根据点数计算并添加相应顶点坐标
            for (float i = 0; i < edge1Points; i++)
            {
                Vector3 currentVertex = vertex + edgeVector1 * (i / (edge1Points - 1));
                vertices.Add(currentVertex);
            }

            // 遍历第二条边，根据点数计算并添加相应顶点坐标（需排除已添加过的第一个顶点）
            for (float i = 1; i < edge2Points; i++)
            {
                Vector3 currentVertex = vertex + edgeVector2 * (i / (edge2Points - 1));
                vertices.Add(currentVertex);
            }

            // 遍历平行四边形另外两条边（由已添加顶点组成的边），根据点数计算并添加相应顶点坐标
            for (float i = 1; i < edge1Points; i++)
            {
                for (float j = 1; j < edge2Points; j++)
                {
                    Vector3 currentVertex = vertex + edgeVector1 * (i / (edge1Points - 1)) + edgeVector2 * (j / (edge2Points - 1));
                    vertices.Add(currentVertex);
                }
            }

            return vertices;
        }

       

        /// <summary>
        /// 立方体
        /// </summary>
        /// <param name="center">
        /// 表示立方体在三维空间中的中心坐标，类型为Vector3，以此坐标为基准来确定立方体各个顶点的相对位置。
        /// </param>
        /// <param name="sideLength">
        /// 表示立方体的边长，为float类型，用于确定立方体各个顶点相对于中心坐标的偏移量，从而计算出具体顶点坐标。
        /// </param>
        /// <returns>
        /// 返回一个List<Vector3>类型的列表，其中按顺序存放了依据输入参数确定的立方体的8个顶点坐标。
        /// </returns>
        public static List<Vector3> Cube(Vector3 center, float sideLength)
        {
            List<Vector3> vertices = new List<Vector3>();
            float halfSideLength = sideLength / 2;

            // 前下左顶点
            vertices.Add(center + new Vector3(-halfSideLength, -halfSideLength, -halfSideLength));
            // 前下右顶点
            vertices.Add(center + new Vector3(halfSideLength, -halfSideLength, -halfSideLength));
            // 前上右顶点
            vertices.Add(center + new Vector3(halfSideLength, halfSideLength, -halfSideLength));
            // 前上左顶点
            vertices.Add(center + new Vector3(-halfSideLength, halfSideLength, -halfSideLength));
            // 后下左顶点
            vertices.Add(center + new Vector3(-halfSideLength, -halfSideLength, halfSideLength));
            // 后下右顶点
            vertices.Add(center + new Vector3(halfSideLength, -halfSideLength, halfSideLength));
            // 后上右顶点
            vertices.Add(center + new Vector3(halfSideLength, halfSideLength, halfSideLength));
            // 后上左顶点
            vertices.Add(center + new Vector3(-halfSideLength, halfSideLength, halfSideLength));

            return vertices;
        }

        /// <summary>
        ///立方体
        /// 通过传入立方体的中心坐标、边长以及每条边上的点数来唯一确定一个细分后的立方体（包含每条边细分后的顶点）。
        /// </summary>
        /// <param name="center">
        /// 表示立方体在三维空间中的中心坐标，类型为Vector3，以此坐标为基准来确定立方体各个顶点的相对位置。
        /// </param>
        /// <param name="sideLength">
        /// 表示立方体的边长，为float类型，用于确定立方体各个顶点相对于中心坐标的偏移量，从而计算出具体顶点坐标。
        /// </param>
        /// <param name="pointsOnEdge">
        /// 表示立方体每条边上均匀分布的点数（包含顶点），为float类型，需大于等于2，用于控制每条边细分的程度，以获取更详细的顶点坐标信息。
        /// </param>
        /// <returns>
        /// 返回一个List<Vector3>类型的列表，其中按顺序存放了依据输入参数确定的立方体细分后所有顶点的坐标。
        /// </returns>
        public static List<Vector3> Cube(Vector3 center, float sideLength, float pointsOnEdge)
        {
            List<Vector3> vertices = new List<Vector3>();
            float halfSideLength = sideLength / 2;
            float stepLength = sideLength / (pointsOnEdge - 1);  // 每条边上相邻两点间的距离（步长）

            // 遍历三条轴向（x、y、z）的正负方向来添加所有细分后的顶点坐标
            for (float x = -halfSideLength; x <= halfSideLength; x += stepLength)
            {
                for (float y = -halfSideLength; y <= halfSideLength; y += stepLength)
                {
                    for (float z = -halfSideLength; z <= halfSideLength; z += stepLength)
                    {
                        vertices.Add(center + new Vector3(x, y, z));
                    }
                }
            }

            return vertices;
        }


        /// <summary>
        /// 长方体
        /// </summary>
        /// <param name="center">
        /// 表示长方体在三维空间中的中心坐标，类型为Vector3，以此坐标为基准来确定长方体各个顶点的相对位置。
        /// </param>
        /// <param name="length">
        /// 表示长方体的长度（沿x轴方向的尺寸），为float类型，用于确定长方体在对应维度上顶点相对于中心坐标的偏移量，从而计算出具体顶点坐标。
        /// </param>
        /// <param name="width">
        /// 表示长方体的宽度（沿y轴方向的尺寸），为float类型，同样用于确定长方体在对应维度上顶点相对于中心坐标的偏移量，以计算顶点坐标。
        /// </param>
        /// <param name="height">
        /// 表示长方体的高度（沿z轴方向的尺寸），为float类型，也是用于确定长方体在对应维度上顶点相对于中心坐标的偏移量来得出顶点坐标。
        /// </param>
        /// <returns>
        /// 返回一个List<Vector3>类型的列表，其中按顺序存放了依据输入参数确定的长方体的8个顶点坐标。
        /// </returns>
        public static List<Vector3> Cuboid(Vector3 center, float length, float width, float height)
        {
            List<Vector3> vertices = new List<Vector3>();
            float halfLength = length / 2;
            float halfWidth = width / 2;
            float halfHeight = height / 2;

            // 前下左顶点
            vertices.Add(center + new Vector3(-halfLength, -halfWidth, -halfHeight));
            // 前下右顶点
            vertices.Add(center + new Vector3(halfLength, -halfWidth, -halfHeight));
            // 前上右顶点
            vertices.Add(center + new Vector3(halfLength, halfWidth, -halfHeight));
            // 前上左顶点
            vertices.Add(center + new Vector3(-halfLength, halfWidth, -halfHeight));
            // 后下左顶点
            vertices.Add(center + new Vector3(-halfLength, -halfWidth, halfHeight));
            // 后下右顶点
            vertices.Add(center + new Vector3(halfLength, -halfWidth, halfHeight));
            // 后上右顶点
            vertices.Add(center + new Vector3(halfLength, halfWidth, halfHeight));
            // 后上左顶点
            vertices.Add(center + new Vector3(-halfLength, halfWidth, halfHeight));

            return vertices;
        }

        /// <summary>
        ///长方体
        /// </summary>
        /// <param name="center">
        /// 表示长方体在三维空间中的中心坐标，类型为Vector3，以此坐标为基准来确定长方体各个顶点的相对位置。
        /// </param>
        /// <param name="length">
        /// 表示长方体的长度（沿x轴方向的尺寸），为float类型，用于确定长方体在对应维度上顶点相对于中心坐标的偏移量，从而计算出具体顶点坐标。
        /// </param>
        /// <param name="width">
        /// 表示长方体的宽度（沿y轴方向的尺寸），为float类型，同样用于确定长方体在对应维度上顶点相对于中心坐标的偏移量，以计算顶点坐标。
        /// </param>
        /// <param name="height">
        /// 表示长方体的高度（沿z轴方向的尺寸），为float类型，也是用于确定长方体在对应维度上顶点相对于中心坐标的偏移量来得出顶点坐标。
        /// </param>
        /// <param name="pointsOnLengthEdge">
        /// 表示长方体长度方向（x轴方向）每条边上均匀分布的点数（包含顶点），为float类型，需大于等于2，用于控制该方向边细分的程度，以获取更详细的顶点坐标信息。
        /// </param>
        /// <param name="pointsOnWidthEdge">
        /// 表示长方体宽度方向（y轴方向）每条边上均匀分布的点数（包含顶点），为float类型，需大于等于2，用于控制该方向边细分的程度，以获取更详细的顶点坐标信息。
        /// </param>
        /// <param name="pointsOnHeightEdge">
        /// 表示长方体高度方向（z轴方向）每条边上均匀分布的点数（包含顶点），为float类型，需大于等于2，用于控制该方向边细分的程度，以获取更详细的顶点坐标信息。
        /// </param>
        /// <returns>
        /// 返回一个List<Vector3>类型的列表，其中按顺序存放了依据输入参数确定的长方体细分后所有顶点的坐标。
        /// </returns>
        public static List<Vector3> Cuboid(Vector3 center, float length, float width, float height,
            float pointsOnLengthEdge, float pointsOnWidthEdge, float pointsOnHeightEdge)
        {
            List<Vector3> vertices = new List<Vector3>();
            float halfLength = length / 2;
            float halfWidth = width / 2;
            float halfHeight = height / 2;

            float stepLengthX = length / (pointsOnLengthEdge - 1);  // x轴方向每条边上相邻两点间的距离（步长）
            float stepLengthY = width / (pointsOnWidthEdge - 1);   // y轴方向每条边上相邻两点间的距离（步长）
            float stepLengthZ = height / (pointsOnHeightEdge - 1);  // z轴方向每条边上相邻两点间的距离（步长）

            // 遍历长度、宽度、高度方向上的点数范围来添加所有细分后的顶点坐标
            for (float x = -halfLength; x <= halfLength; x += stepLengthX)
            {
                for (float y = -halfWidth; y <= halfWidth; y += stepLengthY)
                {
                    for (float z = -halfHeight; z <= halfHeight; z += stepLengthZ)
                    {
                        vertices.Add(center + new Vector3(x, y, z));
                    }
                }
            }

            return vertices;
        }

        /// <summary>
        /// 扇形
        /// </summary>
        /// <param name="center">
        /// 表示扇形圆心在三维空间中的坐标，是整个扇形的中心参照点，类型为OpenTK.Mathematics中的Vector3，
        /// 所有顶点坐标的计算都围绕此点展开，决定了扇形在空间中的具体位置。
        /// </param>
        /// <param name="startVector">
        /// 从圆心出发指向扇形起始边端点的向量，属于OpenTK.Mathematics中的Vector3类型，
        /// 此向量既明确了扇形起始边的方向，其自身长度又确定了从圆心到起始边端点的距离，
        /// 为后续顶点坐标计算提供基础方向与长度信息。
        /// </param>
        /// <param name="angleInDegrees">
        /// 扇形张开的角度，以角度制为单位，是float类型，表示扇形在三维空间中展开的程度，
        /// 该参数会在方法内部转换为弧度制用于后续计算，通过对转换后的角度进行合理细分来确定弧上不同位置的顶点，以此构建出完整的扇形顶点集合。
        /// </param>
        /// <returns>
        /// 返回一个List<Vector3>类型的列表，其中按一定顺序依次存放了依据输入参数确定的扇形的所有顶点坐标。
        /// </returns>
        public static List<Vector3> Sector(Vector3 center, Vector3 startVector, float angleInDegrees)
        {
            List<Vector3> vertices = new List<Vector3>();
            float startVectorLength = startVector.Length;
            Vector3 unitStartVector = startVector.Normalized();

            // 将角度制转换为弧度制，方便后续计算，因为数学库中的三角函数等操作通常使用弧度制
            float angleInRadians = (float)(angleInDegrees * Math.PI / 180.0);

            // 设置角度细分的步长，这里取一个相对合适的值用于控制扇形精细程度，可根据实际需求调整
            const float angleStep = 0.1f;

            // 添加圆心坐标作为第一个顶点
            vertices.Add(center);

            // 遍历起始边添加顶点坐标（从圆心到扇形弧起始边的向量方向）
            for (float angle = 0; angle <= startVectorLength; angle += angleStep)
            {
                Vector3 currentVertex = center + unitStartVector * angle;
                vertices.Add(currentVertex);
            }

            // 通过旋转起始边方向向量来获取弧上不同角度对应的方向向量，并添加对应顶点坐标
            for (float currentAngle = angleStep; currentAngle < angleInRadians; currentAngle += angleStep)
            {
                Quaternion rotationQuaternion = Quaternion.FromAxisAngle(unitStartVector, currentAngle);
                Vector3 rotatedDirection = Vector3.Transform(unitStartVector, rotationQuaternion);

                for (float length = 0; length <= startVectorLength; length += angleStep)
                {
                    Vector3 vertex = center + rotatedDirection * length;
                    vertices.Add(vertex);
                }
            }

            return vertices;
        }

        /// <summary>
        ///圆锥（包含母线细分后的顶点）。
        /// </summary>
        /// <param name="bottomCenter">
        /// 表示圆锥底面在三维空间中的圆心坐标，类型为Vector3，以此坐标为基准来确定圆锥底面各顶点以及整个圆锥在空间中的位置。
        /// </param>
        /// <param name="height">
        /// 表示圆锥的高度，为float类型，用于确定圆锥顶点在垂直方向上相对于底面圆心的位置。
        /// </param>
        /// <param name="radius">
        /// 表示圆锥底面的半径，为float类型，用于确定圆锥底面圆周上各顶点相对于底面圆心的偏移量，从而计算出底面各顶点坐标。
        /// </param>
        /// <param name="segments">
        /// 表示圆锥母线的细分点数（包含底面圆周上的顶点和圆锥顶点），为float类型，需大于等于2，用于控制母线细分的程度，以获取更详细的顶点坐标信息。
        /// </param>
        /// <returns>
        /// 返回一个List<Vector3>类型的列表，其中按顺序存放了依据输入参数确定的圆锥的所有顶点坐标。
        /// </returns>
        public static List<Vector3> Cone(Vector3 bottomCenter, float height, float radius, float segments)
        {
            List<Vector3> vertices = new List<Vector3>();
            float step = (float)(2 * Math.PI / (segments - 1));  // 底面圆周上相邻两点对应的角度步长

            // 添加圆锥顶点坐标
            vertices.Add(bottomCenter + new Vector3(0, height, 0));

            // 遍历计算并添加底面圆周上各顶点坐标
            for (float angle = 0; angle < 2 * Math.PI; angle += step)
            {
                float x = (float)(radius * Math.Cos(angle));
                float y = 0;
                float z = (float)(radius * Math.Sin(angle));
                vertices.Add(bottomCenter + new Vector3(x, y, z));
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
