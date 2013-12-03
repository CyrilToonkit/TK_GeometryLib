using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK.GeometryLib
{
    public class Parallelepiped : Shape3
    {
        public Parallelepiped()
        {

        }

        public Parallelepiped(Vector3 inCenter, double inWidth, double inHeight, double inDepth)
        {
            _center = inCenter;
            _width = inWidth;
            _height = inHeight;
            _depth = inDepth;
        }

        Vector3 _center = new Vector3();
        double _width = 0.0;
        double _height = 0.0;
        double _depth = 0.0;

        public double Width
        {
            get { return _width; }
            set { _width = value; }
        }
        public double Height
        {
            get { return _height; }
            set { _height = value; }
        }
        public double Depth
        {
            get { return _depth; }
            set { _depth = value; }
        }

        public override bool Contains(Vector3 inVec3)
        {
            return inVec3.X > (_center.X - _width / 2.0) && inVec3.X < (_center.X + _width / 2.0)
                && inVec3.Y > (_center.Y - _height / 2.0) && inVec3.Y < (_center.Y + _height / 2.0)
                && inVec3.Z > (_center.Z - _depth / 2.0) && inVec3.Z < (_center.Z + _depth / 2.0);
        }

        public override Vector3 Center
        {
            get { return _center; }
            set { _center = value; }
        }

        public List<Parallelepiped> Subdivide(int inCount)
        {
            List<Parallelepiped> subBoxes = new List<Parallelepiped>();

            double width = _width / (double)inCount;
            double height = _height / (double)inCount;
            double depth = _depth / (double)inCount;

            for (int i = 0; i < inCount; i++)
            {
                for (int j = 0; j < inCount; j++)
                {
                    for (int k = 0; k < inCount; k++)
                    {
                        subBoxes.Add(new Parallelepiped(new Vector3(k * width + (width / 2.0) + _center.X - (_width / 2), j * height + (height / 2.0) + _center.Y - (_height / 2), i * depth + (depth / 2.0) + _center.Z - (_depth / 2)),
                            width,height,depth));
                    }
                }
            }

            return subBoxes;
        }

        #region ICloneable Members

        public override object Clone()
        {
            return new Parallelepiped((Vector3)_center.Clone(), _width, _height, _depth);
        }

        #endregion
    }
}
