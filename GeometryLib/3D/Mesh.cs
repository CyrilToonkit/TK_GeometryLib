using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK.GeometryLib
{
    public class Mesh : Shape3
    {
        public Mesh()
        {
        }

        public Mesh(params Vector3[] inPoints)
        {
            foreach (Vector3 vec in inPoints)
            {
                _points.Add(vec);
            }
        }

        public Mesh(List<Vector3> inPoints)
        {
            foreach (Vector3 vec in inPoints)
            {
                _points.Add(vec);
            }
        }

        List<Vector3> _points = new List<Vector3>();

        public List<Vector3> Points
        {
            get { return _points; }
            set { _points = value; }
        }

    }
}
