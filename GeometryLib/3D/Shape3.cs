using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TK.GeometryLib
{
    [XmlInclude(typeof(Cube))]
    public class Shape3 : ICloneable
    {
        public virtual bool Contains(Vector3 inVec3)
        {
            return false;
        }

        public virtual Vector3 Center
        {
            get { return Vector3.Null; }
            set { }
        }

        #region ICloneable Members

        public virtual object Clone()
        {
            return new Shape3();
        }

        #endregion
    }
}
