using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Globalization;

namespace TK.GeometryLib
{
    //[Serializable()]
    [TypeConverterAttribute(typeof(TransVector2)),
DescriptionAttribute("Expand to see Vector's X Y.")]
    public class Vector2 : ICloneable
    {
        public Vector2()
        {
            this.mx = 0;
            this.my = 0;
        }

		public Vector2(float inx, float iny)
        {
            this.mx = inx;
            this.my = iny;
        }

		public Vector2(float[] inArray)
        {
            this.mx = inArray[0];
            this.my = inArray[1];
        }

		protected float mx;
		public float X
        {
            get { return mx; }
            set { mx = value; }
        }

        protected float my;
		public float Y
        {
            get { return my; }
            set { my = value; }
        }

        protected float _minimumLength = 0;
        public float MinimumLength
        {
            get { return _minimumLength; }
            set { _minimumLength = value; }
        }

        public bool IsNull
        {
            get { return (X == 0 && Y == 0); }
        }

        public bool IsIdentity
        {
            get { return (X == 1 && Y == 1); }
        }

        public float Length
        {
            get { return (float)Math.Sqrt(mx * mx + my * my); }
            set
            {
                float length = Length;
                mx = Math.Max(_minimumLength, (value / length)) * mx;
                my = Math.Max(_minimumLength, (value / length)) * my;
            }
        }

        public Vector2 Normalize()
        {
            Length = 1;
            return this;
        }

        public static Vector2 Null
        {
            get { return new Vector2(); }
        }

        // Returns the dot product of the current vector and
        // the given one
		public float DotProduct(Vector2 vec2)
        {
            return (X * vec2.X) + (Y * vec2.Y);
        }

        // Returns a vector representing the cross product
        // of the current vector and the given one
        public float CrossProduct(Vector2 vec2)
        {
            return (X * vec2.Y) - (Y * vec2.X);
        }

        // Returns a new vector with the contents
        // multiplied together.
        public static Vector2 operator *(Vector2 vec1, Vector2 vec2)
        {
            if (vec1 == null)
                throw new ArgumentNullException("vec1");
            if (vec2 == null)
                throw new ArgumentNullException("vec2");
            return new Vector2(vec1.X * vec2.X, vec1.Y * vec2.Y);
        }

        // Returns a new vector with the contents
        // divided together.
        public static Vector2 operator /(Vector2 vec1, Vector2 vec2)
        {
            if (vec1 == null)
                throw new ArgumentNullException("vec1");
            return new Vector2(vec1.X / vec2.X, vec1.Y / vec2.Y);
        }

        // Returns a new vector with the contents
        // added together.
        public static Vector2 operator +(Vector2 vec1, Vector2 vec2)
        {
            if (vec1 == null)
                throw new ArgumentNullException("vec1");
            if (vec2 == null)
                throw new ArgumentNullException("vec2");
            return new Vector2(vec1.X + vec2.X, vec1.Y + vec2.Y);
        }

        // Returns a new vector with the contents
        // substracted together.
        public static Vector2 operator -(Vector2 vec1, Vector2 vec2)
        {
            if (vec1 == null)
                throw new ArgumentNullException("vec1");
            if (vec2 == null)
                throw new ArgumentNullException("vec2");
            return new Vector2(vec1.X - vec2.X, vec1.Y - vec2.Y);
        }

        // Returns a new vector with the contents
        // multiplied together.
        public static Vector2 operator *(Vector2 vec1, float scal2)
        {
            if (vec1 == null)
                throw new ArgumentNullException("vec1");
            return new Vector2(vec1.X * scal2, vec1.Y * scal2);
        }

        // Returns a new vector with the contents
        // divided together.
        public static Vector2 operator /(Vector2 vec1, float scal2)
        {
            if (vec1 == null)
                throw new ArgumentNullException("vec1");
            return new Vector2(vec1.X / scal2, vec1.Y / scal2);
        }

        // Returns a new vector with the contents
        // added together.
        public static Vector2 operator +(Vector2 vec1, float scal2)
        {
            if (vec1 == null)
                throw new ArgumentNullException("vec1");
            return new Vector2(vec1.X + scal2, vec1.Y + scal2);
        }

        // Returns a new vector with the contents
        // substracted together.
        public static Vector2 operator -(Vector2 vec1, float scal2)
        {
            if (vec1 == null)
                throw new ArgumentNullException("vec1");
            return new Vector2(vec1.X - scal2, vec1.Y - scal2);
        }

        public static bool operator >(Vector2 vec1, Vector2 vec2)
        {
            if (vec1 == null)
                throw new ArgumentNullException("vec1");
            if (vec2 == null)
                throw new ArgumentNullException("vec2");
            return vec1.X > vec2.X && vec1.Y > vec2.Y;
        }

        public static bool operator <(Vector2 vec1, Vector2 vec2)
        {
            if (vec1 == null)
                throw new ArgumentNullException("vec1");
            if (vec2 == null)
                throw new ArgumentNullException("vec2");
            return vec1.X < vec2.X && vec1.Y < vec2.Y;
        }

        public static bool operator >=(Vector2 vec1, Vector2 vec2)
        {
            if (vec1 == null)
                throw new ArgumentNullException("vec1");
            if (vec2 == null)
                throw new ArgumentNullException("vec2");
            return vec1.X >= vec2.X && vec1.Y >= vec2.Y;
        }

        public static bool operator <=(Vector2 vec1, Vector2 vec2)
        {
            if (vec1 == null)
                throw new ArgumentNullException("vec1");
            if (vec2 == null)
                throw new ArgumentNullException("vec2");
            return vec1.X <= vec2.X && vec1.Y <= vec2.Y;
        }

        #region ICloneable Members

        public object Clone()
        {
            return new Vector2(mx, my);
        }

        #endregion

        public static void OffsetVectors(ref Vector2[] basePoints, Vector2 translation)
        {
            foreach (Vector2 vec in basePoints)
            {
                vec.X += translation.X;
                vec.Y += translation.Y;
            }
        }

        public static void ScaleVectors(ref Vector2[] basePoints, float scale)
        {
            foreach (Vector2 vec in basePoints)
            {
                vec.X *= scale;
                vec.Y *= scale;
            }
        }

        public static void ScaleVectors(ref Vector2[] basePoints, Vector2 scale)
        {
            foreach (Vector2 vec in basePoints)
            {
                vec.X *= scale.X;
                vec.Y *= scale.Y;
            }
        }
    }

    public class TransVector2 : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context,
                                      System.Type destinationType)
        {
            if (destinationType == typeof(Vector2))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context,
                               CultureInfo culture,
                               object value,
                               System.Type destinationType)
        {
            if (destinationType == typeof(System.String) && value is Vector2)
            {

                Vector2 Trans = (Vector2)value;

                return ToCleanString(Trans.X) + "," + ToCleanString(Trans.Y);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context,
                              CultureInfo culture, object value)
        {
            if (value is string)
            {
                try
                {
                    string s = (string)value;

                    string[] split = s.Split(",".ToCharArray());

                    if (split.Length == 2)
                    {
                        Vector2 Trans = new Vector2(DoubleHelper.FloatParse(split[0]), DoubleHelper.FloatParse(split[1]));
                        return Trans;
                    }
                }
                catch
                {
                    throw new ArgumentException(
                        "Can not convert '" + (string)value +
                                           "' to type Vector2");
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        private string ToCleanString(float p)
        {
            return p.ToString("0.##").Replace(",", ".");
        }

    }
}
