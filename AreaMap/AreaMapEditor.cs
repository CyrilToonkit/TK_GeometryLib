using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using TK.GeometryLib;
using System.IO;
using TK.BaseLib;
using TK_OSCARLib;
using TK.NodalEditor;

namespace TK.GeometryLib.AreaMapFramework
{
    public partial class AreaMapEditor : UserControl
    {
        string applicationName = "SynopTiK Editor V1.7";

        Form addOscarForm = null;
        AddOscarControlsUCtrl editor = null;

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
            RefreshMetaData();
            RefreshListBox();
            RefreshMetaData();
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

        public string ApplicationName
        {
            get { return applicationName; }
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

        public bool ShowCenter
        {
            get { return AreaMapComponent.ShowCenter; }
            set { AreaMapComponent.ShowCenter = value; }
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

        private void exportAsAnimPickerFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                if (AreaMapComponent.IsEditing)
                {
                    AreaMapComponent.StopEdit();
                }
                string error = AreaMapComponent.ExportAsPicker(AreaMapComponent.CurrentAreaMap, saveFileDialog2.FileName, false, 1.5);

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

                centerXNUD.Value = (decimal)AreaMapComponent.Center.X;
                centerXNUD.Value = (decimal)AreaMapComponent.Center.X;
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
            RefreshMetaData();
        }

        public void RenameFromMapping(string inPath)
        {
            Dictionary<string, string> mapping = ReadMapping(inPath);
            areaMapControl1.RenameFromMapping(mapping);

            RefreshListBox();
            RefreshGroups();
            RefreshMetaData();
        }

        private Dictionary<string, string> ReadMapping(string inPath, string inSeparator)
        {
            Dictionary<string, string> mapping = new Dictionary<string, string>();

            char[] sep = inSeparator.ToCharArray();

            using (var reader = new StreamReader(inPath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(sep);

                    if (!mapping.ContainsKey(values[0]))
                        mapping.Add(values[0], values[1]);
                }
            }

            return mapping;
        }

        private Dictionary<string, string> ReadMapping(string inPath)
        {
            return ReadMapping(inPath, ",");
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

        private void loadGroupsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Xml file (*.xml)|*.xml";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                AreaMapComponent.CurrentAreaMap.LoadGroups(openFileDialog1.FileName);
                RefreshGroups();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPropGrid();
            if (listBox1.SelectedItems.Count > 0)
            {
                metaDataTB.Enabled = true;

                _muteEvents = true;
                metaDataTB.Text = (listBox1.SelectedItems[0] as Area).MetaData;
                _muteEvents = false;
            }
            else
            {
                metaDataTB.Enabled = false;
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
            RefreshListBox(true);
        }

        public void RefreshListBox(bool inUseIndices)
        {
            List<object> coll = new List<object>();

            if (inUseIndices)
            {
                foreach (object obj in listBox1.SelectedIndices)
                {
                    coll.Add(obj);
                }
            }
            else
            {
                foreach (object obj in listBox1.SelectedItems)
                {
                    coll.Add(obj);
                }
            }

            listBox1.DataSource = null;
            listBox1.Items.Clear();
            if (AreaMapComponent.CurrentAreaMap != null)
            {
                listBox1.DataSource = AreaMapComponent.CurrentAreaMap.Areas;
            }

            listBox1.ClearSelected();

            if (inUseIndices)
            {
                foreach (int index in coll)
                {
                    if (index < listBox1.Items.Count)
                        listBox1.SetSelected(index, true);
                }
            }
            else
            {
                foreach (object obj in coll)
                {
                    int index = 0;
                    foreach (object obj2 in listBox1.Items)
                    {
                        if ((obj2 as Area).Name == (obj as Area).Name)
                        {
                            if(index < listBox1.Items.Count)
                                listBox1.SetSelected(index, true);
                            break;
                        }
                        index++;
                    }
                }
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

        private void RefreshMetaData()
        {
            _muteEvents = true;

            tabNameTB.Text = AreaMapComponent.CurrentAreaMap.Name;
            ParentForm.Text = ApplicationName + " - " + AreaMapComponent.CurrentAreaMap.Path;

            centerXNUD.Value = (decimal)AreaMapComponent.Center.X;
            centerYNUD.Value = (decimal)AreaMapComponent.Center.Y;

            _muteEvents = false;
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
            RefreshListBox(false);
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

                        //Search if Area already exists
                        int index = 0;
                        Area existingArea = null;
                        foreach(Area oldArea in AreaMapComponent.CurrentAreaMap.Areas)
                        {
                            if(oldArea.Name == area.Name && area != oldArea)
                            {
                                existingArea = oldArea;
                                break;
                            }
                            index++;
                        }

                        if (existingArea != null)
                        {
                            AreaMapComponent.RemoveAreas(new List<Area> { existingArea });

                            if(index < AreaMapComponent.CurrentAreaMap.Areas.Count - 1)
                            {
                                AreaMapComponent.CurrentAreaMap.Areas.Remove(area);
                                AreaMapComponent.CurrentAreaMap.Areas.Insert(index, area);
                            }
                        }
                    }
                }

                RefreshListBox();
            }
        }

        private void shapeMirrorYBT_Click(object sender, EventArgs e)
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
                        AreaMapComponent.MirrorY(area);
                    }
                }
            }
        }

        private void shapeSymYBT_Click(object sender, EventArgs e)
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
                        AreaMapComponent.SymmetrizeY(area);

                        //Search if Area already exists
                        int index = 0;
                        Area existingArea = null;
                        foreach (Area oldArea in AreaMapComponent.CurrentAreaMap.Areas)
                        {
                            if (oldArea.Name == area.Name && area != oldArea)
                            {
                                existingArea = oldArea;
                                break;
                            }
                            index++;
                        }

                        if (existingArea != null)
                        {
                            AreaMapComponent.RemoveAreas(new List<Area> { existingArea });

                            if (index < AreaMapComponent.CurrentAreaMap.Areas.Count - 1)
                            {
                                AreaMapComponent.CurrentAreaMap.Areas.Remove(area);
                                AreaMapComponent.CurrentAreaMap.Areas.Insert(index, area);
                            }
                        }
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

        private void addFromOscarAssetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Comp file (*.comp)|*.comp";
            openFileDialog1.Title = "Open an Oscar Compound";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ManagerCompanion companion = new ManagerCompanion();
                RigDeserializer deserializer = new RigDeserializer();

                PathHelper.InitPathSubstitutions(AreaMapFramework.Properties.Settings.Default.OscarPath);
                ProjectsManager pm = ProjectsManager.Get(deserializer, new RigCreator(), companion);

                RigCompound node = pm.GetRig(openFileDialog1.FileName) as RigCompound;
                RigsProject nodeProj = null;
                
                foreach (RigsProject proj in pm.Projects)
                {
                    if (node.Path.StartsWith(proj.ResolvedProjectPath))
                    {
                        nodeProj = proj;
                        break;
                    }
                }

                if (nodeProj == null)
                {
                    MessageBox.Show(string.Format("Can't find Project for Node {0} !", node.FullName));
                    return;
                }

                AssetManager am = new AssetManager(nodeProj.AssetFullPath);
                am.GetVersions();
                VersionGeneration version = am.Versions[0];

                GenerationData genData = new GenerationData(version, node);

                genData.GetRenamings();

                /*
                List<string> controllers = new List<string>();

                foreach (RigElement elem in genData.Controllers)
                {
                    if (genData.CtrlsInfos.ContainsKey(elem.FullName) && genData.CtrlsInfos[elem.FullName].Expose)
                    {
                        ControllerInfos ctrl = genData.CtrlsInfos[elem.FullName];
                        controllers.Add(string.Format("{0} : X {1}, Y {2}, Size : {3}, Color {4}", ctrl.RealName, elem.Trans.Pos.X, elem.Trans.Pos.Y, elem.Size, pm.ProjectPrefs.GetColor(elem)));
                    }
                }

                MessageBox.Show(string.Format("Node {0} controllers :\n{1}", node, TypesHelper.Join(controllers, "\n")));
                */

                if (addOscarForm == null)
                {
                    editor = new AddOscarControlsUCtrl();
                    addOscarForm = new Form();
                    addOscarForm.Controls.Add(editor);
                    editor.Dock = DockStyle.Fill;
                    editor.AddBT.Click += new EventHandler(AddBT_Click);
                    addOscarForm.FormClosed += AddOscarForm_FormClosed;
                }

                editor.Init(AreaMapComponent, genData);

                addOscarForm.Show();
            }
        }

        private void AddOscarForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            addOscarForm = null;
        }

        void AddBT_Click(object sender, EventArgs e)
        {
            List<string> sel = editor.GetSelection();

            List<Area> areas = new List<Area>();

            float minX = float.MaxValue;
            float maxX = float.MinValue;

            float minY = float.MaxValue;
            float maxY = float.MinValue;

            foreach (string selItem in sel)
            {
                RigElement elem = editor.GenData.RealNamesControls[selItem];
                Area oscarControlArea = AreaMapComponent.AddOscarControl(selItem, elem, editor.GenData.CtrlsInfos[elem.FullName], editor.Projection);

                Vector2 corner = oscarControlArea.Corner;
                Vector2 otherCorner = oscarControlArea.Shape.LowerRightCorner;

                if (maxX < otherCorner.X)
                {
                    maxX = otherCorner.X;
                }
                if (minX > corner.X)
                {
                    minX = corner.X;
                }

                if (maxY < otherCorner.Y)
                {
                    maxY = otherCorner.Y;
                }
                if (minY > corner.Y)
                {
                    minY = corner.Y;
                }

                areas.Add(oscarControlArea);
            }
            
            float Yextension = maxY - minY;
            float Xextension = maxX - minX;

            float scale = Math.Min(AreaMapComponent.Width / Xextension, AreaMapComponent.Height / Yextension);

            foreach (Area area in areas)
            {
                area.Center *= new Vector2(1.0f, -1.0f);
                area.Center += new Vector2(-minX, maxY);
                area.Scale(new Vector2(scale, scale), new Vector2(0f, 0f));
            }

            AreaMapComponent.Invalidate();
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

        private void showCenterToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            ShowCenter = showCenterToolStripMenuItem.Checked;
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

        private void bringToFrontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Area> areas = AreaMapComponent.GetEditSelection();

            foreach (Area area in areas)
            {
                AreaMapComponent.CurrentAreaMap.Areas.Remove(area);
                AreaMapComponent.CurrentAreaMap.Areas.Insert(0, area);
            }

            RefreshListBox();
        }

        private void sendToBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Area> areas = AreaMapComponent.GetEditSelection();

            foreach (Area area in areas)
            {
                AreaMapComponent.CurrentAreaMap.Areas.Remove(area);
                AreaMapComponent.CurrentAreaMap.Areas.Add(area);
            }

            RefreshListBox();
        }

        private void centerXNUD_ValueChanged(object sender, EventArgs e)
        {
            if (!_muteEvents)
            {
                AreaMapComponent.Center = new Vector2((float)centerXNUD.Value, AreaMapComponent.Center.Y);
                AreaMapComponent.Invalidate();
            }
        }

        private void centerYNUD_ValueChanged(object sender, EventArgs e)
        {
            if (!_muteEvents)
            {
                AreaMapComponent.Center = new Vector2(AreaMapComponent.Center.X, (float)centerYNUD.Value);
                AreaMapComponent.Invalidate();
            }
        }

        private void renameShapesFromCsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Csv file (*.csv,*.txt)|*.csv;*.txt";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                RenameFromMapping(openFileDialog1.FileName);
            }
        }
    }
}
