using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp.MyOpenTK.DLL;

namespace WinFormsApp.CandyModel;

public class Axis
{
    public float _minX;
    public float _maxX;
    public float _minY;
    public float _maxY;
    public float _minZ;
    public float _maxZ;
    public float _pointCountX;
    public float _pointCountY;
    public float _pointCountZ;
    public List<Vector3> _vertexList;
    public List<Vector3> _vertexSpacingList;
    public float[] _vertexArray;
    public float[] _vertexSpacingArray;
    public Axis(float minX, float maxX, float minY, float maxY, float minZ, float maxZ, float pointCountX, float pointCountY, float pointCountZ)
    {
        _minX = minX;
        _maxX = maxX;
        _minY = minY;
        _maxY = maxY;
        _minZ = minZ;
        _maxZ = maxZ;
        _pointCountX = pointCountX;
        _pointCountY = pointCountY;
        _pointCountZ = pointCountZ;
        _vertexList = GetVertexList(minX, maxX, minY, maxY, minZ, maxZ);
        _vertexSpacingList = GetVertexSpacingList(minX, maxX, minY, maxY, minZ, maxZ, pointCountX, pointCountY, pointCountZ);
        _vertexArray = DataTool.Vector3ListToArray(_vertexList);
        _vertexSpacingArray = DataTool.Vector3ListToArray(_vertexSpacingList);
    }

    public Axis(float maxX, float maxY, float maxZ, float pointCountX)
    {
        _minX = -maxX;
        _maxX = maxX;
        _minY = -maxY;
        _maxY = maxY;
        _minZ = -maxZ;
        _maxZ = maxZ;
        _pointCountX = pointCountX;
        _pointCountY = pointCountX;
        _pointCountZ = pointCountX;
        _vertexList = GetVertexList(-maxX, maxX, -maxY, maxY, -maxZ, maxZ);
        _vertexSpacingList = GetVertexSpacingList(-maxX, maxX, -maxY, maxY, -maxZ, maxZ, pointCountX, pointCountX, pointCountX);
        _vertexArray = DataTool.Vector3ListToArray(_vertexList);
        _vertexSpacingArray = DataTool.Vector3ListToArray(_vertexSpacingList);
    }

    public List<Vector3> GetVertexList(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
    {
        List<Vector3> vector3s = new List<Vector3>();
        vector3s.Add(new Vector3(minX, 0, 0));
        vector3s.Add(new Vector3(maxX, 0, 0));
        vector3s.Add(new Vector3(0, minY, 0));
        vector3s.Add(new Vector3(0, maxY, 0));
        vector3s.Add(new Vector3(0, 0, minZ));
        vector3s.Add(new Vector3(0, 0, maxZ));
        return vector3s;
    }

    public List<Vector3> GetVertexSpacingList(float minX, float maxX, float minY, float maxY, float minZ, float maxZ, float pointCountX, float pointCountY, float pointCountZ)
    {
        List<Vector3> vector3s = new List<Vector3>();
        float dx = (maxX - minX) / (pointCountX - 1);
        vector3s.Add(new Vector3(-minX, 0, 0));
        for (int i = 0; i < pointCountX; i++)
        {
            vector3s.Add(new Vector3(minX + i * dx, 0, 0));
        }
        vector3s.Add(new Vector3(maxX, 0, 0));

        float dy = (maxY - minY) / (pointCountY - 1);
        vector3s.Add(new Vector3( 0, minY, 0));
        for (int i = 0; i < pointCountY; i++)
        {
            vector3s.Add(new Vector3(0, minY + i * dy, 0));
        }
        vector3s.Add(new Vector3(0, maxY,  0));

        float dz = (maxZ - minZ) / (pointCountZ - 1);
        vector3s.Add(new Vector3( 0, 0, minZ));
        for (int i = 0; i < pointCountZ; i++)
        {
            vector3s.Add(new Vector3(0, 0,minZ + i * dz ));
        }
        vector3s.Add(new Vector3(0, 0, maxZ));

        return vector3s;
    }

    
}
