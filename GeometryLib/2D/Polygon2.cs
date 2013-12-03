using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TK.GeometryLib
{
    public class Polygon2 : Shape2
    {
        public Polygon2()
        {
        }

        public Polygon2(params Vector2[] inPoints)
        {
            foreach (Vector2 vec in inPoints)
            {
                _points.Add(vec);
            }
        }

        public Polygon2(List<Vector2> inPoints)
        {
            foreach (Vector2 vec in inPoints)
            {
                _points.Add(vec);
            }
        }

        protected List<Vector2> _points = new List<Vector2>();

        public bool IsValid
        {
            get { return _points.Count > 2; }
        }

        public List<Vector2> Points
        {
            get { return _points; }
            set { _points = value; }
        }

        [XmlIgnore]
        public override Vector2 Center
        {
            get
            {
                Vector2 center = Vector2.Null;

                foreach (Vector2 point in _points)
                {
                    center += point;
                }

                return center / (float)_points.Count;
            }
            set
            {
                Vector2 offset = value - Center;
                Offset(offset);
            }
        }

        [XmlIgnore]
        public override Vector2 Corner
        {
            get
            {
                Vector2 corner = new Vector2(float.MaxValue, float.MaxValue);

                foreach (Vector2 point in _points)
                {
                    if (point.X < corner.X)
                    {
                        corner.X = point.X;
                    }

                    if (point.Y < corner.Y)
                    {
                        corner.Y = point.Y;
                    }
                }

                return corner;
            }
        }

        public override Vector2 UpperRightCorner
        {
            get
            {
                Vector2 corner = new Vector2(float.MinValue, float.MaxValue);

                foreach (Vector2 point in _points)
                {
                    if (point.X > corner.X)
                    {
                        corner.X = point.X;
                    }

                    if (point.Y < corner.Y)
                    {
                        corner.Y = point.Y;
                    }
                }

                return corner;
            }
        }

        public override Vector2 LowerRightCorner
        {
            get
            {
                Vector2 corner = new Vector2(float.MinValue, float.MinValue);

                foreach (Vector2 point in _points)
                {
                    if (point.X > corner.X)
                    {
                        corner.X = point.X;
                    }

                    if (point.Y > corner.Y)
                    {
                        corner.Y = point.Y;
                    }
                }

                return corner;
            }
        }

        public override Vector2 LowerLeftCorner
        {
            get
            {
                Vector2 corner = new Vector2(float.MaxValue, float.MinValue);

                foreach (Vector2 point in _points)
                {
                    if (point.X < corner.X)
                    {
                        corner.X = point.X;
                    }

                    if (point.Y > corner.Y)
                    {
                        corner.Y = point.Y;
                    }
                }

                return corner;
            }
        }

        public override void Scale(Vector2 inScale)
        {
            Scale(inScale, Center);
        }

        public override void Scale(Vector2 inScaling, Vector2 inReference)
        {
            Vector2 center = Center;
            Vector2 localPos;

            List<Vector2> points = new List<Vector2>();
            foreach (Vector2 point in _points)
            {
                localPos = point - inReference;
                localPos *= inScaling;
                if (localPos.Length < 2)
                {
                    localPos.Length = 2;
                }
                points.Add(inReference + localPos);
            }

            _points = points;
        }

        public override void Rotate(double inDegrees, Vector2 inPivot)
        {
            List<Vector2> points = new List<Vector2>();
            foreach (Vector2 point in _points)
            {
                Vector2 moved = new Vector2();
                moved.X = (float)(inPivot.X + (point.X - inPivot.X) * Math.Cos(inDegrees) - (point.Y - inPivot.Y) * Math.Sin(inDegrees));
                moved.Y = (float)(inPivot.Y + (point.X - inPivot.X) * Math.Sin(inDegrees) + (point.Y - inPivot.Y) * Math.Cos(inDegrees));

                points.Add(moved);
            }

            _points = points;
        }

        public override void Mirror(Vector2 inCenter)
        {
            float localX;

            List<Vector2> points = new List<Vector2>();
            foreach (Vector2 point in _points)
            {
                localX = inCenter.X - point.X;
                points.Add(new Vector2(inCenter.X + localX, point.Y));
            }

            _points = points;
        }

        public override bool Contains(Vector2 inVec2)
        {
            Vector2 p1, p2;
            bool inside = false;

            if (!IsValid)
            {
                return false;
            }

            Vector2 oldPoint = new Vector2(_points[_points.Count - 1].X, _points[_points.Count - 1].Y);

            for (int i = 0; i < _points.Count; i++)
            {
                Vector2 newPoint = new Vector2(_points[i].X, _points[i].Y);

                if (newPoint.X > oldPoint.X)
                {
                    p1 = oldPoint;
                    p2 = newPoint;
                }
                else
                {
                    p1 = newPoint;
                    p2 = oldPoint;
                }

                if ((newPoint.X < inVec2.X) == (inVec2.X <= oldPoint.X)

                    && ((long)inVec2.Y - (long)p1.Y) * (long)(p2.X - p1.X)

                     < ((long)p2.Y - (long)p1.Y) * (long)(inVec2.X - p1.X))
                {
                    inside = !inside;
                }

                oldPoint = newPoint;
            }

            return inside;
        }

        public override void Offset(Vector2 inVec)
        {
            for (int i = 0; i < _points.Count;i++ )
            {
                _points[i] += inVec;
            }
        }

        public override void Clamp(float minX, float maxX, float minY, float maxY)
        {
            Vector2 offset = Vector2.Null;
            int xOffseted = 0;
            int yOffseted = 0;

            foreach (Vector2 vec in _points)
            {
                if (xOffseted < 2 && vec.X < minX)
                {
                    offset.X = Math.Max(offset.X, (minX - vec.X));
                    xOffseted = 1; 
                }
                if ((xOffseted == 0 || xOffseted == 2) && vec.X > maxX)
                {
                    offset.X = Math.Min(offset.X, -(vec.X - maxX));
                    xOffseted = 2;
                }

                if (yOffseted < 2 && vec.Y < minY)
                {
                    offset.Y = Math.Max(offset.Y, (minY - vec.Y));
                    yOffseted = 1; 
                }
                if ((yOffseted == 0 || yOffseted == 2) && vec.Y > maxY)
                {
                    offset.Y = Math.Max(offset.Y, -(vec.Y - maxY));
                    yOffseted = 2; 
                }
            }

            if(!offset.IsNull)
            {
                for (int i = 0; i < _points.Count; i++)
                {
                    _points[i] += offset;
                }
            }
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

            Polygon2 shape = new Polygon2(points);

            return shape;
        }

        #endregion
    }
}
