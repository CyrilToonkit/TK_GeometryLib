using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK.BaseLib;

namespace TK.GeometryLib.AreaMapFramework
{
    public class AreaMap
    {
        string _name = "NewArea";
        string _imagePath = "";
        string _path = "";
        int _index = 0;
        List<Area> _areas = new List<Area>();
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
    }
}
