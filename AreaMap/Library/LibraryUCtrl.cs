using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TK.GeometryLib;
using TK.BaseLib;

namespace TK.GeometryLib.AreaMapFramework.Library
{
    public partial class LibraryUCtrl : UserControl
    {
        public LibraryUCtrl()
        {
            InitializeComponent();
        }

        public void Initialize(AreaMapComponent inLibrary)
        {
            _areaMap = inLibrary;
            flowLayoutPanel1.Controls.Clear();
            foreach (Area area in _areaMap.CurrentAreaMap.Areas)
            {
                flowLayoutPanel1.Controls.Add(new LibraryItemCtrl(this, area));
            }
        }

        AreaMapComponent _areaMap = null;
        public AreaMapComponent AreaMapComp
        {
            get { return _areaMap; }
            set { _areaMap = value; }
        }

        internal void AddItem(Area inRefArea)
        {
            Area newArea = inRefArea.Clone();
            _areaMap.AddArea(newArea);
            AreaMapComponent.Migrate(inRefArea, PathHelper.GetFolderPath(_areaMap.CurrentAreaMap.Path));
            newArea.Map = _areaMap.CurrentAreaMap;

            _areaMap.CurrentAreaMap.SetPathsRelative();
            flowLayoutPanel1.Controls.Add(new LibraryItemCtrl(this, newArea));
        }

        internal void RemoveItem(LibraryItemCtrl inLibItem)
        {
            _areaMap.RemoveAreas(new List<Area> {inLibItem.Area });
            flowLayoutPanel1.Controls.Remove(inLibItem);
        }
    }
}
