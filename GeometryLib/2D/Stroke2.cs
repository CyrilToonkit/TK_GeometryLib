using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TK.GeometryLib
{
    public class Stroke2 : Polygon2
    {
        public Stroke2()
        {
        }

        public Stroke2(params Vector2[] inPoints)
            :base(inPoints)
        {
        }

        public Stroke2(List<Vector2> inPoints)
            : base(inPoints)
        {
        }

        public override bool Contains(Vector2 inVec2)
        {
            return false;
        }

        public override bool Touch(Shape2 inShape)
        {
            return Touch((Polygon2)GeoConverter.Convert(inShape, ShapeType.Polygon, false));
        }

        public bool Touch(Polygon2 inShape)
        {
            bool inside = true;

            Vector2 primoPoint = null;
            Vector2 lastPoint = null;

            for (int i = 1; i < _points.Count; i++)
            {
                foreach (Vector2 vec in inShape.Points)
                {
                    if (lastPoint != null)
                    {
                        if (Line.LineIntersectsLine(_points[i - 1], _points[i], vec, lastPoint))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        primoPoint = new Vector2(vec.X, vec.Y);
                    }

                    lastPoint = new Vector2(vec.X, vec.Y);
                }

                //Last chance... last segment
                if (Line.LineIntersectsLine(_points[i - 1], _points[i], primoPoint, lastPoint))
                {
                    return true;
                }

                //1 point not inside ? it's not inside...
                if (!inShape.Contains(_points[i - 1]))
                {
                    inside = false;
                }
            }

            return inside;
        }

        public bool AddPoint(Vector2 inPoint, float inMinLength)
        {
            if (_points.Count == 0 || (inPoint - _points[_points.Count - 1]).Length > inMinLength)
            {
                _points.Add(inPoint);
                return true;
            }

            return false;
        }

        #region ICloneable Members

        public override object Clone()
        {
            Vector2[] points = new Vector2[_points.Count];
            int counter = 0;
            foreach (Vector2 vec in _points)
            {
                points[counter] = (Vector2)vec.Clone();
                counter++;
            }

            Stroke2 shape = new Stroke2(points);

            return shape;
        }

        #endregion
    }
}
