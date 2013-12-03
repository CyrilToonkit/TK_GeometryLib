using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK.GeometryLib
{
    public class Circle : Shape2
    {
        public Circle()
        {
        }

        public Circle(Vector2 inCenter, float inRadius)
        {
            _center = inCenter;
            _radius = inRadius;
        }

        Vector2 _center = Vector2.Null;
        float _radius = 0f;

        public float Radius
        {
            get { return _radius; }
            set { _radius = Math.Max(1, value); }
        }

        public override Vector2 Corner
        {
            get { return _center - _radius; }
        }

        public override Vector2 UpperRightCorner
        {
            get { return _center + _radius + new Vector2(0, -2 * _radius); }
        }

        public override Vector2 LowerRightCorner
        {
            get { return _center + _radius; }
        }

        public override Vector2 LowerLeftCorner
        {
            get { return _center - _radius + new Vector2(0, 2*_radius); }
        }

        public override Vector2 Center
        {
            get { return _center; }
            set { _center = value; }
        }

        public override void Scale(Vector2 inScale)
        {
            Radius = Math.Max(2, _radius * inScale.X);
        }

        public override void Scale(Vector2 inScaling, Vector2 inReference)
        {
            Center = (Center - inReference) * inScaling + inReference;
            Scale(inScaling);
        }

        public override bool Contains(Vector2 inVec2)
        {
            return (_center - inVec2).Length <= _radius;
        }

        public override void Clamp(float minX, float maxX, float minY, float maxY)
        {
            Center.X = Math.Min(maxX - _radius, Math.Max(minX + _radius, Center.X));
            Center.Y = Math.Min(maxY - _radius, Math.Max(minY + _radius, Center.Y));
        }

        #region ICloneable Members

        public override object Clone()
        {
            Circle shape = new Circle(_center, _radius);

            return shape;
        }

        #endregion
    }
}
