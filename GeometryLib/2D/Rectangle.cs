using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK.GeometryLib
{
    public class Rectangle : Shape2
    {
        public Rectangle()
        {
        }

        public Rectangle(Vector2 inCenter, float inWidth, float inHeight)
        {
            _center = inCenter;
            _width = inWidth;
            _height = inHeight;
        }

        Vector2 _center = Vector2.Null;
        float _width = 0f;
        float _height = 0f;

        public override float Width
        {
            get { return _width; }
            set { _width = Math.Max(1, value); }
        }

        public override float Height
        {
            get { return _height; }
            set { _height = Math.Max(1, value); }
        }

        public override Vector2 Corner
        {
            get { return _center - new Vector2(_width/2f, _height/2f); }
        }

        public override Vector2 UpperRightCorner
        {
            get { return _center - new Vector2(-(_width / 2f), _height / 2f); }
        }

        public override Vector2 LowerRightCorner
        {
            get { return _center + new Vector2(_width / 2f, _height / 2f); }
        }

        public override Vector2 LowerLeftCorner
        {
            get { return _center - new Vector2(_width / 2f, -(_height / 2f)); }
        }

        public override Vector2 Center
        {
            get { return _center; }
            set { _center = value; }
        }

        public override void Scale(Vector2 inScale)
        {
            Width = Math.Max(2,_width * inScale.X);
            Height = Math.Max(2,_height * inScale.Y);
        }

        public override void Scale(Vector2 inScale, Vector2 inReference)
        {
            Center = (Center - inReference) * inScale + inReference;
            Scale(inScale);
        }

        public override bool Touch(Shape2 inShape)
        {
            if (inShape is Circle)
            {
                return Touch(inShape as Circle);
            }
            else if (inShape is Rectangle)
            {
                return Touch(inShape as Rectangle);
            }
            else if (inShape is Polygon2)
            {
                return Touch(inShape as Polygon2);
            }
            else if (inShape is Stroke2)
            {
                return Touch(inShape as Stroke2);
            }

            return false;
        }

        public bool Touch(Circle inCircle)
        {
            return Touch((Polygon2)GeoConverter.Convert(inCircle, ShapeType.Polygon));
        }

        public bool Touch(Rectangle inRectangle)
        {
            return Touch((Polygon2)GeoConverter.Convert(inRectangle, ShapeType.Polygon));
        }

        public bool Touch(Stroke2 inStroke)
        {
            Polygon2 selfPoly = (Polygon2)GeoConverter.Convert(this, ShapeType.Polygon);

            Vector2 lastPoint = null;

            foreach (Vector2 vec in inStroke.Points)
            {
                if (lastPoint != null)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (Line.LineIntersectsLine(selfPoly.Points[0 + i], selfPoly.Points[(1 + i) % 4], vec, lastPoint))
                        {
                            return true;
                        }
                    }
                }

                lastPoint = new Vector2(vec.X, vec.Y);
            }

            return false;
        }

        public bool Touch(Polygon2 inShape)
        {
            bool inside = true;
            Polygon2 selfPoly = (Polygon2)GeoConverter.Convert(this, ShapeType.Polygon);

            Vector2 primoPoint = null;
            Vector2 lastPoint = null;

            foreach (Vector2 vec in inShape.Points)
            {
                if (lastPoint != null)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (Line.LineIntersectsLine(selfPoly.Points[0 + i], selfPoly.Points[(1 + i) % 4], vec, lastPoint))
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    primoPoint = new Vector2(vec.X, vec.Y);
                }

                //1 point not inside ? it's not inside...
                if (!Contains(vec))
                {
                    inside = false;
                }

                lastPoint = new Vector2(vec.X, vec.Y);
            }

            //Last chance... last segment
            for (int i = 0; i < 4; i++)
            {
                if (Line.LineIntersectsLine(selfPoly.Points[0 + i], selfPoly.Points[(1 + i) % 4], primoPoint, lastPoint))
                {
                    return true;
                }
            }

            return inside;
        }

        public List<Vector2> GetPoints()
        {
            List<Vector2> points = new List<Vector2>(4);
            points.Add(new Vector2(Corner.X, Corner.Y));
            points.Add(new Vector2(Corner.X+Width,Corner.Y));
            points.Add(new Vector2(Corner.X + Width, Corner.Y + Height));
            points.Add(new Vector2(Corner.X, Corner.Y + Height));

            return points;
        }

        public override bool Contains(Vector2 inVec2)
        {
            return inVec2 < new Vector2(_center.X + _width / 2f, _center.Y + _height / 2f)
                && inVec2 > new Vector2(_center.X - _width / 2f, _center.Y - _height / 2f);
        }

        public override void Clamp(float minX, float maxX, float minY, float maxY)
        {
            Center.X = Math.Min(maxX - _width / 2f, Math.Max(minX + _width / 2f, Center.X));
            Center.Y = Math.Min(maxY - _height / 2f, Math.Max(minY + _height / 2f, Center.Y));
        }

        #region ICloneable Members

        public override object Clone()
        {
            Rectangle shape = new Rectangle(_center, _width, _height);

            return shape;
        }

        #endregion
    }
}
