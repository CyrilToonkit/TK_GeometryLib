using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK.BaseLib;
using System.IO;
using System.Xml.Serialization;

namespace TK.GeometryLib.AreaMapFramework
{
    public class AreaMap
    {
        string _name = "NewArea";
        string _imagePath = "";
        string _path = "";
        int _index = 0;
        List<Area> _areas = new List<Area>();
        List<AreaGroup> _groups = new List<AreaGroup>();

        //Deprecated
        List<string> _associatedPaths = new List<string>();

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
        }
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }
        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }
        public List<Area> Areas
        {
            get { return _areas; }
            set { _areas = value; }
        }
        public List<AreaGroup> Groups
        {
            get { return _groups; }
            set { _groups = value; }
        }
        public List<string> AssociatedPaths
        {
            get { return _associatedPaths; }
            set { _associatedPaths = value; }
        }

        internal void InitializeComponents()
        {
            foreach (Area area in _areas)
            {
                area.SetRefCenter();
                area.Map = this;
                area.ImgPath = area.ImgPath;
                area.MetaData = area.MetaData.Replace("\n", "\r\n");
            }
        }

        internal void SetPathsRelative()
        {
            ImagePath = GetRelativePath(ImagePath);

            foreach(Area area in Areas)
            {
                area.ImgPath = GetRelativePath(area.ImgPath);
            }
        }

        internal void SetPathsAbsolute()
        {
            ImagePath = GetAbsolutePath(ImagePath);

            foreach (Area area in Areas)
            {
                area.ImgPath = GetAbsolutePath(area.ImgPath);
            }
        }

        internal string GetRelativePath(string _imgPath)
        {
            string folderName = PathHelper.GetFolderPath(_path);
            if (_imgPath.StartsWith(folderName))
            {
                _imgPath = _imgPath.Replace(folderName, "..");
            }

            return _imgPath;
        }

        internal string GetAbsolutePath(string inRelativePath)
        {
            if (inRelativePath.StartsWith(".."))
            {
                inRelativePath = inRelativePath.Replace("..", PathHelper.GetFolderPath(_path));
            }

            return inRelativePath;
        }

        internal Area GetArea(string p)
        {
            foreach (Area area in _areas)
            {
                if (area.Name == p)
                {
                    return area; 
                }
            }

            return null;
        }

        #region GROUPS

        public AreaGroup FindGroup(string inName)
        {
            foreach (AreaGroup group in _groups)
            {
                if (group.Name == inName)
                {
                    return group;
                }
            }

            return null;
        }

        public void AddGroup(string inName)
        {
            _groups.Add(new AreaGroup(inName));
        }

        public void CreateGroupFromSelection(string inName)
        {
            AreaGroup group = new AreaGroup();
            group.Areas.AddRange(GetEditSelection());
            _groups.Add(group);
        }

        public void RemoveGroup(int row)
        {
            if (row < _groups.Count)
            {
                _groups[row].Visible = true;
                _groups[row].Active = true;

                _groups[row].ApplyValues(false);
                _groups.RemoveAt(row);
            }
        }

        public void AddShapesToGroup(int row)
        {
            if (row < _groups.Count)
            {
                List<Area> EditSelection = GetEditSelection();
                foreach (Area area in EditSelection)
                {
                    if (!_groups[row].Areas.Contains(area))
                    {
                        _groups[row].Areas.Add(area);
                    }
                }
            }
        }

        public List<Area> GetEditSelection()
        {
            List<Area> editSelection = new List<Area>();
            foreach (Area area in Areas)
            {
                if (area.IsSelected)
                {
                    editSelection.Add(area);
                }
            }

            return editSelection;
        }

        public void RemoveShapesFromGroup(int row)
        {
            if (row < _groups.Count)
            {
                List<Area> EditSelection = GetEditSelection();
                foreach (Area area in EditSelection)
                {
                    if (_groups[row].Areas.Contains(area))
                    {
                        _groups[row].Areas.Remove(area);
                    }
                }
            }
        }

        public void ResetGroups()
        {
            foreach (AreaGroup group in _groups)
            {
                group.FindAreas(_areas);
            }
        }

        public void DumpGroups()
        {
            foreach (AreaGroup group in _groups)
            {
                group.DumpAreas();
            }
        }

        public void LoadGroups(string inPath)
        {
            string status = "";
            StreamReader reader = null;

            if (File.Exists(inPath))
            {
                try
                {
                    XmlSerializer ser = new XmlSerializer(typeof(List<AreaGroup>));

                    reader = new StreamReader(inPath);
                    _groups = (List<AreaGroup>)ser.Deserialize(reader);
                    ResetGroups();

                }
                catch
                {

                }
            }
        }

        public void SaveGroups(string inPath)
        {
            DumpGroups();
            StreamWriter writer = null;
            XmlSerializer ser = new XmlSerializer(typeof(List<AreaGroup>));

            try
            {
                writer = new StreamWriter(inPath, false);
                ser.Serialize(writer, _groups);
            }
            catch
            {

            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        #endregion
    }
}
