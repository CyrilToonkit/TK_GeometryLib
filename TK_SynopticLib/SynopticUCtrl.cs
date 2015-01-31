using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TK.GeometryLib.AreaMapFramework;

namespace TK.SynopticLib
{
    public partial class SynopticUCtrl : UserControl
    {
        public SynopticUCtrl()
        {
            InitializeComponent();

            //Initialize MainPanel
            _ratio = 0.20944206008583690987124463519313f;
            synopticAreaMapControl.AreaMapComponent.MouseEnter += new EventHandler(AreaMapComponent_MouseEnter);
        }

        public SynopticUCtrl(SynopticDCCHandler inHandler) : this()
        {
            Handler = inHandler;
        }

        public string ModelName
        {
            get { return synopticAreaMapControl.Handler.ModelName; }
            set { synopticAreaMapControl.Handler.ModelName = mainPanelAreaMapControl.Handler.ModelName = value; }
        }

        bool _muteEvents = false;

        float _ratio = 0;
        public float Ratio
        {
            get { return _ratio; }
            set { _ratio = value; }
        }

        public AreaMapComponent AreaMapComponent
        {
            get { return synopticAreaMapControl.AreaMapComponent; }
        }

        public AreaMapControl AreaMapControl
        {
            get { return synopticAreaMapControl; }
        }

        public SynopticDCCHandler Handler
        {
            get { return synopticAreaMapControl.Handler; }
            set { synopticAreaMapControl.Handler = mainPanelAreaMapControl.Handler = value; }
        }

        private void SynopticUCtrl_SizeChanged(object sender, EventArgs e)
        {
            ResizePanel();
        }

        private void ResizePanel()
        {
            if (!_muteEvents && !collapsibleGroup1.Collapsed && _ratio != 0 && collapsibleGroup1.Dock != DockStyle.None)
            {
                _muteEvents = true;
                if (collapsibleGroup1.Dock == DockStyle.Bottom || collapsibleGroup1.Dock == DockStyle.Top)
                {
                    collapsibleGroup1.Height = (int)((float)(collapsibleGroup1.Width - 12) / _ratio) + 24;
                }
                else if (collapsibleGroup1.Dock == DockStyle.Left || collapsibleGroup1.Dock == DockStyle.Right)
                {
                    collapsibleGroup1.Width = (int)((float)(collapsibleGroup1.Height - 24) * _ratio) + 12;
                }
                _muteEvents = false;
            }
        }

        private void SynopticUCtrl_Load(object sender, EventArgs e)
        {
            string path = System.IO.Path.GetDirectoryName(
System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            mainPanelAreaMapControl.LoadXml(path.Substring(6) + "\\Resources\\CommonPanel.xml");
        }

        public bool AddPage(string splitPath)
        {
            return synopticAreaMapControl.AddXml(splitPath);
        }

        void AreaMapComponent_MouseEnter(object sender, EventArgs e)
        {
            synopticAreaMapControl.RefreshSelection();
            synopticAreaMapControl.RefreshValues();
        }

        private void mainPanelAreaMapControl_Load(object sender, EventArgs e)
        {

        }
    }
}
