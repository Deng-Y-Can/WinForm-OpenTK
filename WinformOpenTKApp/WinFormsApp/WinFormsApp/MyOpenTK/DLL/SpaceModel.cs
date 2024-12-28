using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp.MyOpenTK.DLL
{
    public class SpaceModel
    {
        public float[] _vector;
        public Color4 _color;
        public PrimitiveType _primitiveType;
        public SpaceModel(float[] vectorList, Color4 color, PrimitiveType primitiveType)
        {
            _vector = vectorList;
            _color = color;
            this._primitiveType = primitiveType;
        }
        public SpaceModel()
        {
        }
    }
}
