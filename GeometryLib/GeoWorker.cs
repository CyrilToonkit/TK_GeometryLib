using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK.GeometryLib
{
    public static class GeoWorker
    {
        public static Parallelepiped GetBoundingBox(List<Vector3> inVectors)
        {
            double xMin = double.MaxValue;
            double xMax = double.MinValue;
            double yMin = double.MaxValue;
            double yMax = double.MinValue;
            double zMin = double.MaxValue;
            double zMax = double.MinValue;

            foreach(Vector3 vec in inVectors)
            {
                if(xMin > vec.X){xMin = vec.X;}
                
                if(xMax < vec.X){xMax = vec.X;}
                
                if(yMin > vec.Y){yMin = vec.Y;}
                
                if(yMax < vec.Y){yMax = vec.Y;}
                
                if(zMin > vec.Z){zMin = vec.Z;}
                
                if(zMax < vec.Z){zMax = vec.Z;}
            }

            return new Parallelepiped(new Vector3((xMin + xMax) / 2.0, (yMin + yMax) / 2.0, (zMin + zMax) / 2.0), xMax - xMin, yMax - yMin, zMax - zMin);
        }
    }
}
