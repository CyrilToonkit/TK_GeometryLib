using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TK.GeometryLib.AreaMapFramework
{
    public partial class AreaMapControl : UserControl
    {
        public AreaMapControl()
        {
            InitializeComponent();
            Init();
        }

        protected SynopticDCCHandler _handler = new SynopticDCCHandler();
        public SynopticDCCHandler Handler
        {
            get { return _handler; }
            set { _handler = value; }
        }

        protected void Init()
        {
            _areaMapComponent.BackgroundImageLayout = ImageLayout.Zoom;
            _areaMapComponent.AllowDrop = true;
            _areaMapComponent.ScalingChanged += new AreaMapComponent.ScalingChangedDelegate(_areaMapComponent_ScalingChanged);
            _areaMapComponent.FunctionCalled += new AreaMapComponent.FunctionCalledDelegate(_areaMapComponent_FunctionCalled);
            ResetAreaMaps();

            selectModeDD.Items = new List<object> { "Rectangle", "Raycast" };
            selectModeDD.SelectedIndex = 0;
        }

        bool _optionsShowing = true;
        bool _muteEvents = false;
        bool _isDocked = true;
        protected AreaMapComponent _areaMapComponent = new AreaMapComponent();

        public bool IsDocked
        {
            get { return _isDocked; }
            set { _isDocked = AreaMapComponent.IsDocked = value; }
        }
        public AreaMapComponent AreaMapComponent
        {
            get { return _areaMapComponent; }
        }

        public bool GridOnTop
        {
            get { return _areaMapComponent.GridOnTop; }
            set { _areaMapComponent.GridOnTop = value; }
        }

        public bool ShowGrid
        {
            get { return _areaMapComponent.ShowGrid; }
            set { _areaMapComponent.ShowGrid = value; }
        }

        public bool OptionsShowing
        {
            get { return _optionsShowing; }
            set
            {
                _optionsShowing = value;
                collapsibleGroup1.Visible = value;
            }
        }

        public void Initialize(AreaMapComponent inComponent)
        {
            _areaMapComponent = inComponent;
            ResetAreaMaps();
        }

        void _areaMapComponent_FunctionCalled(object sender, FunctionEventArgs data)
        {
            switch (data.FunctionName)
            {
                case "RefreshSelection":
                    RefreshSelection();
                    break;
            }
        }

        public void ResetAreaMaps()
        {
            tabControl1.TabPages.Clear();

            if (_areaMapComponent.Maps.Count == 1)
            {
                tabControl1.Visible = false;
                areaMapContainer.Controls.Add(_areaMapComponent);
                _areaMapComponent.Dock = DockStyle.Fill;
            }
            else
            {
                foreach (AreaMap map in _areaMapComponent.Maps)
                {
                    TabPage page = new TabPage(map.Name);
                    page.BackColor = tabControl1.BackColor;
                    tabControl1.TabPages.Add(page);
                }

                tabControl1.Visible = true;
                ResetTabs();
            }
        }

        public void ResetTabs()
        {
            tabControl1.SelectedIndex = -1;
            foreach(TabPage page in tabControl1.TabPages)
            {
                if (page.Text == _areaMapComponent.CurrentAreaMap.Name)
                {
                    _muteEvents = true;
                    tabControl1.SelectedTab = page;
                    _muteEvents = false;
                    return;
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != -1)
            {
                if (!_muteEvents)
                {
                    _areaMapComponent.SetIndex(tabControl1.SelectedIndex);
                }

                _areaMapComponent.Parent.Controls.Remove(_areaMapComponent);
                tabControl1.SelectedTab.Controls.Add(_areaMapComponent);
                _areaMapComponent.Dock = DockStyle.Fill;
            }
        }

        internal void RemoveCurrentMap()
        {
            if (_areaMapComponent.Maps.Count > 1)
            {
                _areaMapComponent.RemoveCurrentMap();
                ResetAreaMaps();
            }
        }

        internal void Add(string inPath)
        {
            _areaMapComponent.Add(inPath);
            ResetAreaMaps();
        }

        public bool LoadXml(string inPath)
        {
            if (_areaMapComponent.Load(inPath) == "")
            {
                ResetAreaMaps();
                return true;
            }

            return false;
        }

        public bool AddXml(string inPath)
        {
            if (_areaMapComponent.Load(inPath, _areaMapComponent.Maps.Count == 0 || _areaMapComponent.Maps[0].Areas.Count == 0 ? false : true) == "")
            {
                ResetAreaMaps();
                return true;
            }

            return false;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            IsDocked = !IsDocked;
        }

        private void tkDropDown1_TextChanged(object sender, EventArgs e)
        {
            if ((string)selectModeDD.SelectedItem == "Rectangle")
            {
                _areaMapComponent.SelectionMode = SelectionModes.Rectangle;
            }
            else
            {
                _areaMapComponent.SelectionMode = SelectionModes.RayCast;
            }
        }

        private void completeSlider1_ValueChanged(object sender, EventArgs e)
        {
            if (!_isDocked)
            {
                AreaMapComponent.Scaling = (float)completeSlider1.Value / 100f;
            }
        }

        private void areaMapContainer_SizeChanged(object sender, EventArgs e)
        {
            if (_isDocked)
            {
                completeSlider1.Value = Math.Max(completeSlider1.Minimum,Math.Min((int)(AreaMapComponent.Scaling * 100f),completeSlider1.Maximum));
            }
        }

        void _areaMapComponent_ScalingChanged(object sender, EventArgs data)
        {
            if (!_isDocked)
            {
                _muteEvents = true;
                completeSlider1.Value = Math.Max(completeSlider1.Minimum, Math.Min((int)(AreaMapComponent.Scaling * 100f), completeSlider1.Maximum));
                _muteEvents = false;
            }
        }

        internal void Clear()
        {
            AreaMapComponent.Clear();
            ResetAreaMaps();
        }

        public void SetSelection(List<string> inSelectionList)
        {
            _areaMapComponent.DeselectAll(false);
            List<Area> areas = new List<Area>();
            foreach (string item in inSelectionList)
            {
                Area found = AreaMapComponent.FindArea(item);
                if (found != null)
                {
                    areas.Add(found);
                }
            }

            if (areas.Count > 0)
            {
                _areaMapComponent.AddToSelection(areas, false);
            }

            _areaMapComponent.Invalidate();
        }

        public void SetSelectionFromMetaData(List<string> inSelectionList)
        {
            _areaMapComponent.DeselectAll(false);
            List<Area> areas = new List<Area>();
            foreach (string item in inSelectionList)
            {
                Area found = AreaMapComponent.FindAreaFromMetaData(item);
                if (found != null)
                {
                    areas.Add(found);
                }
            }

            if (areas.Count > 0)
            {
                _areaMapComponent.AddToSelection(areas, false);
            }

            _areaMapComponent.Invalidate();
        }

        public virtual void RefreshSelection()
        {

        }

        public virtual void RefreshValues()
        {

        }

        private void showAllCB_CheckedChanged(object sender, EventArgs e)
        {
            _areaMapComponent.ShowAll = showAllCB.Checked;
        }

        private void allwaysOnTop_CheckedChanged(object sender, EventArgs e)
        {
            ParentForm.TopMost = allwaysOnTop.Checked;
        }
    }
}
