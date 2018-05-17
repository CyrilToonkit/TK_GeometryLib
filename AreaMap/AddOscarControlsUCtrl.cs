using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TK_OSCARLib;

namespace TK.GeometryLib.AreaMapFramework
{
    public enum Projections
    {
        XY, YZ, XZ
    }

    public partial class AddOscarControlsUCtrl : UserControl
    {
        GenerationData genData = null;
        public GenerationData GenData
        {
            get { return genData; }
        }

        AreaMapComponent areaMapComponent = null;

        List<string> existingAreas = null;

        string nameFilter = "";
        int lodFilter = -1;
        bool onlyNewFilter = true;

        public Button AddBT
        {
            get
            {
                return addBT;
            }
        }

        public Projections Projection
        {
            get { return (Projections)projDD.SelectedIndex; }
        }

        public bool SizeToFit
        {
            get { return fitCB.Checked; }
        }

        public int SizeX
        {
            get { return decimal.ToInt32(sizeXNUD.Value); }
        }

        public int SizeY
        {
            get { return decimal.ToInt32(sizeYNUD.Value); }
        }

        public AddOscarControlsUCtrl()
        {
            InitializeComponent();
        }

        internal void Init(AreaMapComponent inAreaMapComponent, GenerationData inGenData)
        {
            genData = inGenData;
            areaMapComponent = inAreaMapComponent;

            FillList();
        }

        private bool MatchFilters(RigElement elem)
        {
            if (existingAreas == null)
            {
                existingAreas = new List<string>();
                foreach (Area area in areaMapComponent.CurrentAreaMap.Areas)
                {
                    existingAreas.Add(area.Name);
                }
            }

            return (nameFilter == string.Empty || genData.CtrlsInfos[elem.FullName].RealName.Contains(nameFilter)) &&
                (lodFilter == -1 || lodFilter == (elem.OwnerRig.LOD + elem.LOD)) &&
                (!onlyNewFilter || !existingAreas.Contains(genData.CtrlsInfos[elem.FullName].RealName));
        }

        public List<string> GetSelection()
        {
            List<string> selection = new List<string>();
            foreach (object obj in controlsLB.SelectedItems)
            {
                selection.Add(obj.ToString());
            }
            return selection;
        }

        void SetSelection(List<string> inSelection)
        {
            ListBox.SelectedObjectCollection sel = new ListBox.SelectedObjectCollection(controlsLB);
            sel.Clear();

            foreach (string str in inSelection)
            {
                foreach (object obj in controlsLB.Items)
                {
                    if (obj.ToString() == str)
                    {
                        sel.Add(str);
                        break;
                    }
                }
            }
        }

        private void FillList()
        {
            //Store selection
            List<string> sel = GetSelection();

            controlsLB.Items.Clear();
            foreach (RigElement elem in genData.Controllers)
            {
                if (genData.CtrlsInfos.ContainsKey(elem.FullName) && genData.CtrlsInfos[elem.FullName].Expose && MatchFilters(elem))
                {
                    controlsLB.Items.Add(genData.CtrlsInfos[elem.FullName].RealName);
                }
            }

            //Retrieve selection
            SetSelection(sel);
        }

        private void nameFilterTB_TextChanged(object sender, EventArgs e)
        {
            nameFilter = nameFilterTB.Text;
            FillList();
        }

        private void filterLodNUD_ValueChanged(object sender, EventArgs e)
        {
            lodFilter = decimal.ToInt32(filterLodNUD.Value);
            FillList();
        }

        private void filterNewCB_CheckedChanged(object sender, EventArgs e)
        {
            onlyNewFilter = filterNewCB.Checked;
            FillList();
        }

        private void fitCB_CheckedChanged(object sender, EventArgs e)
        {
            sizeXNUD.Enabled = !fitCB.Checked;
            sizeYNUD.Enabled = !fitCB.Checked;
        }
    }
}
