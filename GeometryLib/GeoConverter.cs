using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TK.GeometryLib
{
    public static class GeoConverter
    {
        public static PointF[] Convert(List<Vector2> inPoints, Vector2 inOffset, float inScale)
        {
            PointF[] points = new PointF[inPoints.Count];

            int counter = 0;
            foreach (Vector2 vec in inPoints)
            {
                points[counter] = new PointF(inOffset.X + vec.X * inScale, inOffset.Y + vec.Y * inScale);
                counter++;
            }

            return points;
        }

        public static PointF[] Convert(List<Vector2> inPoints)
        {
            PointF[] points = new PointF[inPoints.Count];

            int counter = 0;
            foreach (Vector2 vec in inPoints)
            {
                points[counter] = new PointF(vec.X, vec.Y);
                counter++;
            }

            return points;
        }

        public static PointF Convert(Vector2 point)
        {
            return new PointF(point.X, point.Y);
        }

        public static Point ConvertRound(Vector2 point)
        {
            return new Point((int)point.X, (int)point.Y);
        }

        public static Vector2 Convert(Point point)
        {
            return new Vector2((float)point.X,(float)point.Y);
        }

        public static Vector2 Convert(Point point, Vector2 inOffset, float inScale)
        {
            return new Vector2(((float)point.X - inOffset.X) * (1f / inScale), ((float)point.Y- inOffset.Y) * (1f / inScale));
        }

        public static Shape2 Convert(Circle inCircle, ShapeType shapeType)
        {
            switch (shapeType)
            {
                case ShapeType.Rectangle:
                    return new Rectangle(inCircle.Center, inCircle.Radius * 2f, inCircle.Radius * 2f);
                case ShapeType.Polygon:
                    return new Polygon2(new Rectangle(inCircle.Center, inCircle.Radius * 2f, inCircle.Radius * 2f).GetPoints());
            }

            return inCircle;
        }

        public static Shape2 Convert(Rectangle inRectangle, ShapeType shapeType)
        {
            switch (shapeType)
            {
                case ShapeType.Circle:
                    return new Circle(inRectangle.Center, (inRectangle.Width + inRectangle.Height) / 4f);
                case ShapeType.Polygon:
                    return new Polygon2(inRectangle.GetPoints());
            }

            return inRectangle;
        }

        public static Shape2 Convert(Polygon2 inPolygon, ShapeType shapeType)
        {
            Vector2 corner = inPolygon.Corner;
            Vector2 size = inPolygon.GetSize();

            switch (shapeType)
            {
                case ShapeType.Circle:
                    return new Circle((corner + size / 2f), (size.X + size.Y) / 4f);
                case ShapeType.Rectangle:
                    return new Rectangle((corner + size / 2f), size.X, size.Y);
            }

            return inPolygon;
        }

        public static Shape2 Convert(Shape2 inShape, ShapeType shapeType, bool inThrowError)
        {
            if (inShape is Circle && shapeType != ShapeType.Circle)
            {
                return Convert(inShape as Circle, shapeType);
            }
            else if (inShape is Rectangle && shapeType != ShapeType.Rectangle)
            {
                return Convert(inShape as Rectangle , shapeType);
            }
            else if (inShape is Polygon2 && shapeType != ShapeType.Polygon)
            {
                return Convert(inShape as Polygon2, shapeType);
            }
            else if (inShape is Stroke2 && shapeType != ShapeType.Stroke)
            {
                return Convert(inShape as Stroke2, shapeType);
            }

            if (inThrowError)
            {
                throw new Exception("Cannot convert shape");
            }

            return inShape;
        }
    }
}
