using Microsoft.CodeAnalysis.CSharp.Scripting;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp.MyOpenTK.DLL;

namespace WinFormsApp.CandyModel
{
    public enum CoordinateAxis
    {      
        X,       
        Y,
        Z
    }
    public class BaseCandyFuctionModel
    {
        public Vector3 _minVector3;
        public Vector3 _maxVector3;
       
        public int _pointCount;
        public CoordinateAxis _Axis;
        public  Dictionary<int,Vector3> _vertexDic=new Dictionary<int, Vector3>();
        public float[] _vertexArray;

        public BaseCandyFuctionModel(float Min, float Max, int pointCount, string functionExpression, CoordinateAxis axis= CoordinateAxis.X)
        {
            _pointCount = pointCount;
            _Axis=axis;
            _vertexDic = CalculateFunctionValues(functionExpression, Min, Max, pointCount, axis);
            _vertexArray = DataTool.Vector3ListToArray(_vertexDic.Values.ToList());
            _minVector3 = DataTool.GetMinVector(_vertexDic.Values.ToList());
            _maxVector3 = DataTool.GetMaxVector(_vertexDic.Values.ToList());
        }

        private Dictionary<int, Vector3> CalculateFunctionValues(string functionExpression, float min, float max, int _pointCount, CoordinateAxis axis)
        {
            Dictionary<int, Vector3> vertexDic=new Dictionary<int, Vector3>();
            float dx = (max - min) / (_pointCount - 1);
            if (axis == CoordinateAxis.X)
            {
                int index = 0;
                Vector3 vector3 = new Vector3();
                for (int i = 0; i < _pointCount; i++)
                {
                    index++;
                    vector3.X = min + i * dx;
                    try
                    {
                        vector3.Y= Convert.ToSingle(EvaluateFunction(functionExpression, vector3.X).ToString());
                        vertexDic.Add(index, vector3);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show($"函数计算出错: {ex.Message}");
                    }

                }
            }
            
            return vertexDic;
        }

        private object EvaluateFunction(string functionExpression, float x)
        {
            try
            {
                string code = $"return {functionExpression.Replace("x", x.ToString())};";
                return CSharpScript.EvaluateAsync(code).Result;
            }
            catch
            {
                return x;
            }
            
           
        }
    }
}
