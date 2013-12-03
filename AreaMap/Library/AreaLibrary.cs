using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TK.GeometryLib.AreaMapFramework.Library
{
    public class AreaLibrary
    {
        public AreaLibrary(AreaMapComponent inComponent)
        {
            _areaMap = inComponent;
        }

        AreaMapComponent _areaMap = new AreaMapComponent();

        public AreaMapComponent AreaMap
        {
            get { return _areaMap; }
        }
    }
}
