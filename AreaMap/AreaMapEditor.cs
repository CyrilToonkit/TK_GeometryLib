﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TK.GeometryLib;
using System.IO;
using TK.BaseLib;

namespace TK.GeometryLib.AreaMapFramework
{
    public partial class AreaMapEditor : UserControl
    {
        public AreaMapEditor()
        {
            InitializeComponent();
            AreaMapComponent.DragDrop += new System.Windows.Forms.DragEventHandler(this.areaMap1_DragDrop);
            AreaMapComponent.DragEnter += new System.Windows.Forms.DragEventHandler(this.areaMap1_DragEnter);

            areaMapControl1.tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);
            AreaMapComponent.SelectionChanged += new AreaMapComponent.SelectionChangedDelegate(areaMap1_SelectionChanged);
            AreaMapComponent.MouseDown += new MouseEventHandler(areaMap1_MouseDown);
            AreaMapComponent.MouseUp += new MouseEventHandler(areaMap1_MouseUp);
            libraryUCtrl1.Initialize(GetLibrary());
        }

        void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabNameTB.Text = AreaMapComponent.CurrentAreaMap.Name;
            ParentForm.Text = "SynopTiK Editor - " + AreaMapComponent.CurrentAreaMap.Path;
            RefreshListBox();
        }

        void areaMap1_MouseUp(object sender, MouseEventArgs e)
        {
            if (AreaMapComponent.IsEditing)
            {
                if (!_muteEvents)
                {
                    _muteEvents = true;
                    RefreshListBoxSelection(AreaMapComponent.Selection);
                    _muteEvents = false;
                }
            }
        }

        void areaMap1_MouseDown(object sender, MouseEventArgs e)
        {
            if (AreaMapComponent.IsEditing)
            {
                if (!_muteEvents)
                {
                    _muteEvents = true;
                    RefreshListBoxSelection(AreaMapComponent.Selection);
                    _muteEvents = false;
                }
            }
        }

        public AreaMapComponent GetLibrary()
        {
            AreaMapComponent comp = new AreaMapComponent();
            comp.Load(LibraryPath);

            return comp;
        }

        public void SetLibrary()
        {
            libraryUCtrl1.AreaMapComp.Save(LibraryPath);
        }

        bool _isDragging = false;
        bool _syncSelection = true;
        bool _muteEvents = false;

        public AreaMapComponent AreaMapComponent
        {
            get { return areaMapControl1.AreaMapComponent; }
        }

        public AreaMapControl AreaMapControl
        {
            get { return areaMapControl1; }
        }

        public bool GridOnTop
        {
            get { return AreaMapComponent.GridOnTop; }
            set { AreaMapComponent.GridOnTop = value; }
        }

        public bool ShowGrid
        {
            get { return AreaMapComponent.ShowGrid; }
            set { AreaMapComponent.ShowGrid = value; }
        }

        public bool SyncSelection
        {
            get { return _syncSelection; }
            set { _syncSelection = value; }
        }

        public string LibraryPath
        {
            get { return Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources"), "Library.xml"); }
        }

        private void areaMap1_SelectionChanged(object sender, SelectionChangedEventArgs data)
        {
            if (!_muteEvents)
            {
                _muteEvents = true;
                RefreshListBoxSelection(data.NewSelection);
                _muteEvents = false;
            }
        }

        private void RefreshListBoxSelection(List<Area> list)
        {
            bool found = false;
            List<bool> founds = new List<bool>();
            foreach (Area listBoxArea in listBox1.Items)
            {
                found = false;
                foreach (Area area in list)
                {
                    if (area == listBoxArea)
                    {
                        found = true;
                        break;
                    }
                }

                founds.Add(found);
            }

            for (int i = 0; i < founds.Count; i++)
            {
                listBox1.SetSelected(i, founds[i]);
            }
        }

        private void saveCurrentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (AreaMapComponent.IsEditing)
                {
                    AreaMapComponent.StopEdit();
                }
                string error = AreaMapComponent.Save(AreaMapComponent.CurrentAreaMap, saveFileDialog1.FileName, false);

                if (!string.IsNullOrEmpty(error))
                {
                    MessageBox.Show(error);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (AreaMapComponent.IsEditing)
                {
                    AreaMapComponent.StopEdit();
                }
                string error = AreaMapComponent.Save(AreaMapComponent.CurrentAreaMap, saveFileDialog1.FileName, true);

                if (!string.IsNullOrEmpty(error))
                {
                    MessageBox.Show(error);
                }
            }
        }

        private void loadPictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Picture files (*.jpg,*.png,*.gif,*.bmp)|*.jpg;*.png;*.gif;*.bmp";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                AreaMapComponent.SetPicture(openFileDialog1.FileName);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            areaMapControl1.Clear();
            RefreshListBox();
            RefreshGroups();
            RefreshPropGrid();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Xml file (*.xml)|*.xml";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {               
                LoadXml(openFileDialog1.FileName);
            }
        }

        public void LoadXml(string inPath)
        {
            areaMapControl1.LoadXml(inPath);

            RefreshListBox();
            RefreshGroups();
        }

        private void loadAsTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Xml file (*.xml)|*.xml";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                areaMapControl1.Add(openFileDialog1.FileName);
                RefreshListBox();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPropGrid();
            if (listBox1.SelectedItems.Count > 0)
            {
                _muteEvents = true;
                metaDataTB.Text = (listBox1.SelectedItems[0] as Area).MetaData;
                _muteEvents = false;
            }

            if (SyncSelection && !_muteEvents)
            {
                _muteEvents = true;

                AreaMapComponent.DeselectAll();
                foreach (Area area in listBox1.SelectedItems)
                {
                    AreaMapComponent.AddToSelection(area);
                }

                _muteEvents = false;
            }
        }

        public void RefreshListBox()
        {
            ListBox.SelectedIndexCollection coll = listBox1.SelectedIndices;

            listBox1.DataSource = null;
            listBox1.Items.Clear();
            listBox1.SelectedIndex = -1;
            if (AreaMapComponent.CurrentAreaMap != null)
            {
                listBox1.DataSource = AreaMapComponent.CurrentAreaMap.Areas;
            }

            foreach(int index in coll)
            {
                listBox1.SetSelected(index, true);
            }
        }

        private void RefreshGroups()
        {
            groupsGridView.DataSource = null;
            groupsGridView.Refresh();
            if (AreaMapComponent.Groups.Count > 0)
            {
                groupsGridView.DataSource = AreaMapComponent.Groups;
            }
        }

        private void RefreshPropGrid()
        {
            propertyGrid1.SelectedObject = null;

            if (listBox1.SelectedItems != null && listBox1.SelectedItems.Count > 0)
            {
                object[] objs = new object[listBox1.SelectedItems.Count];
                int counter = 0;
                foreach (Area area in listBox1.SelectedItems)
                {
                    objs[counter] = area;
                    counter++;
                }

                propertyGrid1.SelectedObjects = objs;
            }
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.Label == "MetaData")
            {
                _muteEvents = true;
                metaDataTB.Text = (string)e.ChangedItem.Value;
                _muteEvents = false;
            }

            RefreshListBox();
            AreaMapComponent.Invalidate();
        }

        private void switchModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AreaMapComponent.IsEditing = !AreaMapComponent.IsEditing;
            switchModeToolStripMenuItem.Text = AreaMapComponent.IsEditing ? "Switch to Control mode" : "Switch to Edit mode";
            RefreshListBox();
        }

        private void resetAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!AreaMapComponent.IsEditing)
            {
                AreaMapComponent.ResetAll();
            }
        }

        private void stopEditingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AreaMapComponent.StopEdit();
            RefreshListBox();
        }

        private void areaMap1_DragDrop(object sender, DragEventArgs e)
        {
            Shape2 shape = null;
            Area area = null;

            if (e.Data.GetDataPresent(typeof(Polygon2)))
            {
                shape = (Polygon2)e.Data.GetData(typeof(Polygon2));
            }
            else
            {
                if (e.Data.GetDataPresent(typeof(Area)))
                {
                    area = (Area)e.Data.GetData(typeof(Area));
                }
                else
                {
                    shape = (Circle)e.Data.GetData(typeof(Circle));
                }
            }

            Point mapDropPoint = AreaMapComponent.PointToClient(new Point(e.X, e.Y));
            mapDropPoint.Offset(GeoConverter.ConvertRound(AreaMapComponent.Offset * -1));

            if (shape != null)
            {
                shape.Offset((new Vector2(mapDropPoint.X, mapDropPoint.Y)) * (1f / AreaMapComponent.Scaling));
                shape.Clamp(0, AreaMapComponent.Dimensions.Width, 0, AreaMapComponent.Dimensions.Height); 
                AreaMapComponent.AddShape(shape);
                RefreshListBox();
            }
            else if (area != null)
            {
                //Has a picture with path relative to library
                if(area.ImgPath != "" && area.ImgPath != libraryUCtrl1.AreaMapComp.CurrentAreaMap.GetAbsolutePath(area.ImgPath))
                {
                    if(AreaMapComponent.CurrentAreaMap.Path != "")
                    {
                        area.Map = libraryUCtrl1.AreaMapComp.CurrentAreaMap;
                        AreaMapComponent.Migrate(area, PathHelper.GetFolderPath(AreaMapComponent.CurrentAreaMap.Path));
                    }
                    else
                    {
                        MessageBox.Show("You must save your AreaMap to be able to use library items with relative pictures !", "File not saved");
                        return;
                    }
                }

                area.Center = (new Vector2(mapDropPoint.X, mapDropPoint.Y)) * (1f / AreaMapComponent.Scaling);
                area.Clamp(0, AreaMapComponent.Dimensions.Width, 0, AreaMapComponent.Dimensions.Height);
                area.SetRefCenter();
                AreaMapComponent.AddArea(area);
                RefreshListBox();
            }
        }

        private void areaMap1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Polygon2)) || e.Data.GetDataPresent(typeof(Circle)) || e.Data.GetDataPresent(typeof(Area)))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void shapeRemoveBT_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                string areaText = "";

                List<Area> selectedAreas = new List<Area>();
                foreach (object areaObj in listBox1.SelectedItems)
                {
                    Area area = areaObj as Area;
                    if (!area.IsSubComponent)
                    {
                        selectedAreas.Add(area);
                        areaText += areaObj.ToString() + ",";
                    }
                }

                if (MessageBox.Show("Are you sure you want to remove " + areaText.Substring(0, areaText.Length - 1) + " ?", "Remove shapes", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    AreaMapComponent.RemoveAreas(selectedAreas);
                    RefreshListBox();
                }
                else
                {
                    MessageBox.Show("Nothing to remove, please select some items!");
                }
            }
        }

        private void shapeDuplicateBT_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                string areaText = "";

                List<Area> selectedAreas = new List<Area>();
                foreach (object areaObj in listBox1.SelectedItems)
                {
                    Area area = areaObj as Area;
                    if (!area.IsSubComponent)
                    {
                        AreaMapComponent.Duplicate(area);
                    }
                }
                RefreshListBox();
            }
        }

        private void shapeMirrorBT_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                string areaText = "";

                List<Area> selectedAreas = new List<Area>();
                foreach (object areaObj in listBox1.SelectedItems)
                {
                    Area area = areaObj as Area;
                    if (!area.IsSubComponent)
                    {
                        AreaMapComponent.Mirror(area);
                    }
                }
            }
        }

        private void shapeSymBT_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count > 0)
            {
                string areaText = "";

                List<Area> selectedAreas = new List<Area>();
                foreach (object areaObj in listBox1.SelectedItems)
                {
                    Area area = areaObj as Area;
                    if (!area.IsSubComponent)
                    {
                        area = area.Clone();
                        AreaMapComponent.AddArea(area);
                        AreaMapComponent.Symmetrize(area);
                    }
                }

                RefreshListBox();
            }
        }

        private void removeTabBT_Click(object sender, EventArgs e)
        {
            areaMapControl1.RemoveCurrentMap();
            RefreshListBox();
        }

        private void addSelectionToLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (object areaObj in listBox1.SelectedItems)
            {
                Area area = areaObj as Area;
                if (!area.IsSubComponent)
                {
                    area.Map.SetPathsRelative();
                    libraryUCtrl1.AddItem(area);
                }
            }
        }

        private void saveLibraryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLibrary();
        }

        private void tabNameTB_TextChanged(object sender, EventArgs e)
        {
            AreaMapComponent.CurrentAreaMap.Name = tabNameTB.Text;
            if (areaMapControl1.tabControl1.SelectedIndex != -1)
            {
                areaMapControl1.tabControl1.SelectedTab.Text = tabNameTB.Text;
            }
        }

        private void addRectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AreaMapComponent.AddRectangle(new Vector2(50, 50), 120f, 80f);
            RefreshListBox();
        }

        private void transformToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AreaMapComponent.IsTransforming = !AreaMapComponent.IsTransforming;
            transformToolStripMenuItem.Text = AreaMapComponent.IsTransforming ? "Stop transform" : "Transform";
        }

        private void groupAddBT_Click(object sender, EventArgs e)
        {
            AreaMapComponent.CreateGroupFromSelection("NewGroup");
            AreaMapComponent.ApplyGroupValues();
            RefreshGroups();
        }

        private void groupRemoveBT_Click(object sender, EventArgs e)
        {
            if (groupsGridView.SelectedCells.Count > 0)
            {
                int row = groupsGridView.SelectedCells[0].RowIndex;

                AreaMapComponent.RemoveGroup(row);
                AreaMapComponent.ApplyGroupValues();
                RefreshGroups();
            }
        }

        private void groupAddObjsBT_Click(object sender, EventArgs e)
        {
            if (groupsGridView.SelectedCells.Count > 0)
            {
                int row = groupsGridView.SelectedCells[0].RowIndex;

                AreaMapComponent.AddShapesToGroup(row);
                AreaMapComponent.ApplyGroupValues();
                RefreshGroups();
            }
        }

        private void groupRemoveShapesBT_Click(object sender, EventArgs e)
        {
            if (groupsGridView.SelectedCells.Count > 0)
            {
                int row = groupsGridView.SelectedCells[0].RowIndex;

                AreaMapComponent.RemoveShapesFromGroup(row);
                AreaMapComponent.ApplyGroupValues();
                RefreshGroups();
            }
        }

        private void groupsGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            AreaMapComponent.ApplyGroupValues();
        }

        private void groupsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            groupsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void groupsGridView_SelectionChanged(object sender, EventArgs e)
        {
            List<AreaGroup> selectedGroups = new List<AreaGroup>();
            foreach (DataGridViewRow rows in groupsGridView.SelectedRows)
            {
                selectedGroups.Add(AreaMapComponent.Groups[rows.Index]);
            }

            if (selectedGroups.Count > 0)
            {
                AreaMapComponent.SelectGroups(selectedGroups);
            }
        }

        private void groupsGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(groupsGridView.SelectedRows.Count == 1 && groupsGridView.SelectedRows[0].Index == e.RowIndex)
            {
                AreaMapComponent.SelectGroups(new List<AreaGroup> { AreaMapComponent.Groups[groupsGridView.SelectedRows[0].Index] });
            }
        }

        private void metaDataTB_TextChanged(object sender, EventArgs e)
        {
            if (!_muteEvents)
            {
                _muteEvents = true;
                List<Area> areas = AreaMapComponent.GetEditSelection();
                foreach (Area area in areas)
                {
                    area.MetaData = metaDataTB.Text;
                }

                RefreshPropGrid();
                _muteEvents = false;
            }
        }

        private void documentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string target = "https://docs.google.com/document/d/13lPb3ZWEfxaL2Lxfr5OdI2TVpaw4-g6mnj9YQrCXRXA/pub";

            try
            {
                System.Diagnostics.Process.Start(target);
            }

            catch
                 (
                  System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);

            }
            catch (System.Exception other)
            {
                MessageBox.Show(other.Message);

            }
        }

        private void showGridToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            ShowGrid = showGridToolStripMenuItem.Checked;
        }

        private void gridOnTopToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            GridOnTop = gridOnTopToolStripMenuItem.Checked;
        }

        private void rerange01To0515ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Area> sel = AreaMapComponent.GetEditSelection();

            foreach (Area area in sel)
            {
                if (area.ValueMinY == 0 && area.ValueMaxY == 1)
                {
                    area.ValueMinY = -0.5f;
                    area.ValueMaxY = 1.5f;

                    area.ValueSuggestedMinY = 0f;
                    area.ValueSuggestedMaxY = 1f;

                    area.ValueMultiplyY = .5f;


                    //Here, 15f is set because the original height of the control is 60px, must be relative...
                    area.ImgPosition = new Vector2(area.ImgPosition.X, area.ImgPosition.Y + 15f);

                    area.RefCenter = new Vector2(area.RefCenter.X, area.RefCenter.Y - 15f);

                    area.Reset();
                }
            }

            AreaMapComponent.Invalidate();
        }
    }
}
