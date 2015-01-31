using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TK.GeometryLib.AreaMapFramework
{
    public class AreaGroup
    {
        public AreaGroup()
        {

        }

        public AreaGroup(string inName)
        {
            _name = inName;
        }

        string _name = "NewGroup";
        bool _visible = true;
        bool _active = true;

        List<Area> _areas = new List<Area>();

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }

        [XmlIgnore]
        public List<Area> Areas
        {
            get { return _areas; }
            set { _areas = value; }
        }

        List<string> _areasNames = new List<string>();
        public List<string> AreasNames
        {
            get
            {
                return _areasNames;
            }
            set { _areasNames = value; }
        }

        public void DumpAreas()
        {
            _areasNames.Clear();
            foreach (Area area in Areas)
            {
                _areasNames.Add(area.Name);
            }
        }

        public void FindAreas(List<Area> inAreas)
        {
            _areas.Clear();
            List<string> areaNames = new List<string>(_areasNames);
            string found;
            foreach (Area area in inAreas)
            {
                found = "";
                foreach (string areaName in areaNames)
                {
                    if (areaName == area.Name)
                    {
                        _areas.Add(area);
                        found = areaName;
                        break;
                    }
                }
                if (found != "")
                {
                    areaNames.Remove(found);
                }
            }
        }

        internal void ApplyValues()
        {
            foreach (Area area in _areas)
            {
                area.IsVisible = _visible;
                area.IsActive = _active;
            }
        }
    }
}
