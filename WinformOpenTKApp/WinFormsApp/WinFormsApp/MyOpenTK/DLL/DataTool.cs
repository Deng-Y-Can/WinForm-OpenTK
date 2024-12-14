using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp.MyOpenTK.DLL
{
    public class DataTool
    {
        public static float[] Vector3ListToArray(List<Vector3> vector3s)
        {
            float[] floats = new float[vector3s.Count * 3];
            int index = 0;
            foreach (Vector3 vector3 in vector3s)
            {
                floats[index] = vector3.X;
                index++;
                floats[index] = vector3.Y;
                index++;
                floats[index] = vector3.Z;
                index++;
            }
            return floats;
        }

        public static Vector3 GetMaxVector(List<Vector3> vector3s)
        {
            Vector3 max = new Vector3();
            max.X= vector3s.Max(v=>v.X);
            max.Y = vector3s.Max(v => v.Y);
            max.Z = vector3s.Max(v => v.Z);
            return max;
        }
        public static Vector3 GetMinVector(List<Vector3> vector3s)
        {
            Vector3 min = new Vector3();
            min.X = vector3s.Min(v => v.X);
            min.Y = vector3s.Min(v => v.Y);
            min.Z = vector3s.Min(v => v.Z);
            return min;
        }


    }
}
