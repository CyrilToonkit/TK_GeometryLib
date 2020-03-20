using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;

namespace TK.GeometryLib
{
    [Serializable()]
    [TypeConverterAttribute(typeof(TransInspector)),
DescriptionAttribute("Expand to see Transformation.")]
    public class CG_Transform : ISerializable
    {
		public CG_Transform()
		{
            mPos = new Vector3();
            mRot = new Vector3();
            mScl = new Vector3(1,1,1);
		}
		
        private Vector3 mPos;
        public Vector3 Pos
        {
            get { return mPos; }
            set { mPos = value; }
        }

        private Vector3 mRot;
        public Vector3 Rot
        {
            get { return mRot; }
            set { mRot = value; }
        }

        private Vector3 mScl;
        public Vector3 Scl
        {
            get { return mScl; }
            set { mScl = value; }
        }

        [XmlIgnore]
        public bool Identity
        {
            get { return (Pos.IsNull && Rot.IsNull && Scl.Identity); }
        }

        public CG_Transform Copy()
        {
            CG_Transform Trans = new CG_Transform();

            Trans.Scl.X = Scl.X;
            Trans.Scl.Y = Scl.Y;
            Trans.Scl.Z = Scl.Z;

            Trans.Pos.X = Pos.X;
            Trans.Pos.Y = Pos.Y;
            Trans.Pos.Z = Pos.Z;

            Trans.Rot.X = Rot.X;
            Trans.Rot.Y = Rot.Y;
            Trans.Rot.Z = Rot.Z;

            return Trans;
        }

        #region ISerializable Members

        public CG_Transform(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            mPos = (Vector3)info.GetValue("Pos", typeof(Vector3));
            mRot = (Vector3)info.GetValue("Rot", typeof(Vector3));
            mScl = (Vector3)info.GetValue("Scl", typeof(Vector3));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Pos", mPos, typeof(Vector3));
            info.AddValue("Rot", mRot, typeof(Vector3));
            info.AddValue("Scl", mScl, typeof(Vector3));
        }

        #endregion
    }

    public class TransInspector : ExpandableObjectConverter
    {
        public override bool CanConvertTo(ITypeDescriptorContext context,
                                      System.Type destinationType)
        {
            if (destinationType == typeof(CG_Transform))
                return true;

            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertTo(ITypeDescriptorContext context,
                               CultureInfo culture,
                               object value,
                               System.Type destinationType)
        {
            if (destinationType == typeof(System.String) &&
                 value is CG_Transform)
            {

                CG_Transform Trans = (CG_Transform)value;

                return ToCleanString(Trans.Scl.X) + "," + ToCleanString(Trans.Scl.Y) + "," + ToCleanString(Trans.Scl.Z) + ";" +
                       ToCleanString(Trans.Pos.X) + "," + ToCleanString(Trans.Pos.Y) + "," + ToCleanString(Trans.Pos.Z) + ";" +
                       ToCleanString(Trans.Rot.X) + "," + ToCleanString(Trans.Rot.Y) + "," + ToCleanString(Trans.Rot.Z);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        private string ToCleanString(double p)
        {
            return p.ToString("0.##").Replace(",", ".");
        }

        public override object ConvertFrom(ITypeDescriptorContext context,
                              CultureInfo culture, object value)
        {
            if (value is string)
            {
                try
                {
                    string s = (string)value;

                    string[] split = s.Split(";".ToCharArray());

                    if (split.Length == 3)
                    {
                        string[] Scl = split[0].Split(",".ToCharArray());
                        string[] Pos = split[1].Split(",".ToCharArray());
                        string[] Rot = split[2].Split(",".ToCharArray());

                        if (Scl.Length == 3 && Pos.Length == 3 && Rot.Length == 3)
                        {
                            CG_Transform Trans = new CG_Transform();

                            Trans.Scl.X = DoubleHelper.DoubleParse(Scl[0]);
                            Trans.Scl.Y = DoubleHelper.DoubleParse(Scl[1]);
                            Trans.Scl.Z = DoubleHelper.DoubleParse(Scl[2]);

                            Trans.Pos.X = DoubleHelper.DoubleParse(Pos[0]);
                            Trans.Pos.Y = DoubleHelper.DoubleParse(Pos[1]);
                            Trans.Pos.Z = DoubleHelper.DoubleParse(Pos[2]);

                            Trans.Rot.X = DoubleHelper.DoubleParse(Rot[0]);
                            Trans.Rot.Y = DoubleHelper.DoubleParse(Rot[1]);
                            Trans.Rot.Z = DoubleHelper.DoubleParse(Rot[2]);

                            return Trans;
                        }
                    }
                }
                catch
                {
                    throw new ArgumentException(
                        "Can not convert '" + (string)value +
                                           "' to type SpellingOptions");
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

    }
}
