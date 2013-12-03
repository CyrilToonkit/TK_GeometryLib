using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TK.GeometryLib
{
    public enum ShapeType
    {
        Undefined, Circle, Polygon, Rectangle, Stroke
    }

    public enum ConvertibleShapeType
    {
        Circle, Polygon, Rectangle
    }

    [XmlInclude(typeof(Polygon2)), XmlInclude(typeof(Circle)), XmlInclude(typeof(Rectangle)), XmlInclude(typeof(Stroke2))]
    public class Shape2 : ICloneable
    {
        public virtual bool Touch(Shape2 inShape)
        {
            return false;
        }

        public virtual bool Contains(Vector2 inVec2)
        {
            return false;
        }

        public virtual Vector2 Center
        {
            get { return Vector2.Null; }
            set { }
        }

        public virtual Vector2 Corner
        {
            get { return Vector2.Null; }
        }

        public virtual Vector2 UpperRightCorner
        {
            get { return Vector2.Null; }
        }

        public virtual Vector2 LowerRightCorner
        {
            get { return Vector2.Null; }
        }

        public virtual Vector2 LowerLeftCorner
        {
            get { return Vector2.Null; }
        }

        public virtual void Offset(Vector2 inVec)
        {
            Center += inVec;
        }

        public virtual void Scale(Vector2 inScale)
        {
            
        }

        public virtual void Scale(Vector2 inScaling, Vector2 inReference)
        {

        }

        public virtual void Clamp(float minX, float maxX, float minY, float maxY)
        {

        }

        public virtual void Mirror()
        {
            Mirror(Center);
        }

        public virtual void Mirror(Vector2 inCenter)
        {
            Vector2 symCenter = new Vector2(inCenter.X, Center.Y);
            Vector2 local = Center - symCenter;
            local *= -1;
            Center = symCenter + local;
        }

        public virtual void Rotate(double inDegrees, Vector2 inPivot)
        {
            Vector2 moved = new Vector2();
            moved.X = (float)(inPivot.X + (Center.X - inPivot.X) * Math.Cos(inDegrees) - (Center.Y - inPivot.Y) * Math.Sin(inDegrees));
            moved.Y = (float)(inPivot.Y + (Center.X - inPivot.X) * Math.Sin(inDegrees) + (Center.Y - inPivot.Y) *  Math.Cos(inDegrees));
            Center = moved;
        }

        public Vector2 GetSize()
        {
            return (Center - Corner) * 2;
        }

        public void ClampCenter(float minX, float maxX, float minY, float maxY)
        {
            Vector2 clamped = new Vector2(
                Math.Min(maxX, Math.Max(minX, Center.X)),
                Math.Min(maxY, Math.Max(minY, Center.Y)));

            Center = clamped;
        }

        public static ConvertibleShapeType ShapeToConvertibleShape(ShapeType inType)
        {
            switch (inType)
            {
                case ShapeType.Circle :
                    return ConvertibleShapeType.Circle;
                case ShapeType.Polygon :
                    return ConvertibleShapeType.Polygon;
            }

            return ConvertibleShapeType.Rectangle;
        }

        public static ShapeType ConvertibleShapeToShape(ConvertibleShapeType inType)
        {
            switch (inType)
            {
                case ConvertibleShapeType.Circle:
                    return ShapeType.Circle;
                case ConvertibleShapeType.Polygon:
                    return ShapeType.Polygon;
            }

            return ShapeType.Rectangle;
        }

        #region ICloneable Members

        public virtual object Clone()
        {
            Shape2 shape = new Shape2();

            return shape;
        }

        #endregion


    }
}
