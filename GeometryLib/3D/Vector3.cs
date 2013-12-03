using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Globalization;

namespace TK.GeometryLib
{
    [Serializable()]
    [TypeConverterAttribute(typeof(TransVector3)),
DescriptionAttribute("Expand to see Vector's X Y Z.")]
    public class Vector3 : ICloneable
    {
        public Vector3()
        {
            this.mx = 0;
            this.my = 0;
            this.mz = 0;
        }

		public Vector3(double inx, double iny, double inz)
        {
            this.mx = inx;
            this.my = iny;
            this.mz = inz;
        }

		public Vector3(double[] inArray)
        {
            this.mx = inArray[0];
            this.my = inArray[1];
            this.mz = inArray[2];
        }

		protected double mx;
		public double X
        {
            get { return mx; }
            set { mx = value; }
        }

        protected double my;
		public double Y
        {
            get { return my; }
            set { my = value; }
        }

        protected double mz;
		public double Z
        {
            get { return mz; }
            set { mz = value; }
        }

        public bool IsNull
        {
            get { return (X == 0 && Y == 0 && Z == 0); }
        }

        public static Vector3 Null
        {
            get { return new Vector3(); }
        }

        public bool Identity
        {
            get { return (X == 1 && Y == 1 && Z == 1); }
        }

        public double Length
        {
            get { return Math.Sqrt(mx * mx + my * my + mz * mz); }
        }

        // Returns the dot product of the current vector and
        // the given one
		public double DotProduct(Vector3 vec2)
        {
            return (X * vec2.X) + (Y * vec2.Y) + (Z * vec2.Z);
        }

        // Returns a vector representing the cross product
        // of the current vector and the given one
        public Vector3 CrossProduct(Vector3 vec2)
        {
            return new Vector3(
                (Y * vec2.Z) - (vec2.Y * Z),
                (Z * vec2.X) - (vec2.Z * X),
                (X * vec2.Y) - (vec2.X * Y));
        }

        // Returns a new vector with the contents
        // multiplied together.
        public static Vector3 operator *(Vector3 vec1, Vector3 vec2)
        {
            if (vec1 == null)
                throw new ArgumentNullException("vec1");
            if (vec2 == null)
                throw new ArgumentNullException("vec2");
            return new Vector3(vec1.X * vec2.X, vec1.Y * vec2.Y, vec1.Z * vec2.Z);
        }

        // Returns a new vector with the contents
        // added together.
        public static Vector3 operator +(Vector3 vec1, Vector3 vec2)
        {
            if (vec1 == null)
                throw new ArgumentNullException("vec1");
            if (vec2 == null)
                throw new ArgumentNullException("vec2");
            return new Vector3(vec1.X + vec2.X, vec1.Y + vec2.Y, vec1.Z + vec2.Z);
        }

        // Returns a new vector with the contents
        // substracted together.
        public static Vector3 operator -(Vector3 vec1, Vector3 vec2)
        {
            if (vec1 == null)
                throw new ArgumentNullException("vec1");
            if (vec2 == null)
                throw new ArgumentNullException("vec2");
            return new Vector3(vec1.X - vec2.X, vec1.Y - vec2.Y, vec1.Z - vec2.Z);
        }

        // Returns a new vector with the contents
        // multiplied together.
        public static Vector3 operator *(Vector3 vec1, double scal2)
        {
            if (vec1 == null)
                throw new ArgumentNullException("vec1");
            return new Vector3(vec1.X * scal2, vec1.Y * scal2, vec1.Z * scal2);
        }

        // Returns a new vector with the contents
        // divided together.
        public static Vector3 operator /(Vector3 vec1, double scal2)
        {
            if (vec1 == null)
                throw new ArgumentNullException("vec1");
            return new Vector3(vec1.X / scal2, vec1.Y / scal2, vec1.Z / scal2);
        }

        // Returns a new vector with the contents
        // added together.
        public static Vector3 operator +(Vector3 vec1, double scal2)
        {
            if (vec1 == null)
                throw new ArgumentNullException("vec1");
            return new Vector3(vec1.X + scal2, vec1.Y + scal2, vec1.Z + scal2);
        }

        // Returns a new vector with the contents
        // substracted together.
        public static Vector3 operator -(Vector3 vec1, double scal2)
        {
            if (vec1 == null)
                throw new ArgumentNullException("vec1");
            return new Vector3(vec1.X - scal2, vec1.Y - scal2, vec1.Z - scal2);
        }

        #region ICloneable Members

        public object Clone()
        {
            return new Vector3(mx, my, mz);
        }

        #endregion
    }

    public class TransVector3 : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context,
                                      System.Type destinationType)
        {
            if (destinationType == typeof(Vector3))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context,
                               CultureInfo culture,
                               object value,
                               System.Type destinationType)
        {
            if (destinationType == typeof(System.String) &&
                 value is Vector3)
            {

                Vector3 Trans = (Vector3)value;

                return ToCleanString(Trans.X) + "," + ToCleanString(Trans.Y) + "," + ToCleanString(Trans.Z);
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

                    if (split.Length == 3)
                    {
                        Vector3 Trans = new Vector3(DoubleHelper.DoubleParse(split[0]), DoubleHelper.DoubleParse(split[1]), DoubleHelper.DoubleParse(split[2]));
                        return Trans;
                    }
                }
                catch
                {
                    throw new ArgumentException(
                        "Can not convert '" + (string)value +
                                           "' to type Vector3");
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        private string ToCleanString(double p)
        {
            return p.ToString("0.##").Replace(",", ".");
        }
    }
}
