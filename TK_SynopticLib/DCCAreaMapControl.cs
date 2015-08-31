using System;
using System.Collections.Generic;
using System.Text;
using TK.GeometryLib.AreaMapFramework;
using System.Windows.Forms;
using System.Diagnostics;
using TK.BaseLib;
using TK.GraphComponents.Dialogs;

namespace TK.SynopticLib
{
    public class DCCAreaMapControl : AreaMapControl
    {
        public DCCAreaMapControl()
            : base()
        {
            AreaMapComponent.AreaMouseClick += new AreaMapComponent.AreaMouseClickDelegate(areaMap1_AreaMouseClick);
            AreaMapComponent.SelectionChanged += new AreaMapComponent.SelectionChangedDelegate(areaMap1_SelectionChanged);
            AreaMapComponent.ValueChanged += new AreaMapComponent.ValueChangedDelegate(areaMap1_ValueChanged);

            tabControl1.BackColor = System.Drawing.Color.FromArgb(80, 80, 80);
        }

        // VALUE CHANGED => Change parameter Value from "XParam" and "YParam" properties

        void areaMap1_ValueChanged(object sender, ValueChangedEventArgs data)
        {
            //Move in X
            if (data.Area.MovableMode == AreaMovableModes.XOnly || data.Area.MovableMode == AreaMovableModes.Both)
            {
                if (_handler.ParamExists(data.Area.XParam))
                {
                    if (data.Validated && _handler.HasAutoKey())
                    {
                        _handler.SaveKey(data.Area.XParam, data.Area.ValueX);
                    }
                    else
                    {
                        //_handler.Error(string.Format("{0} = {1}", data.Area.XParam, data.Area.ValueX));
                        _handler.SetValue(data.Area.XParam, data.Area.ValueX);
                    }
                }
                else
                {
                    _handler.Error("Can't find parameter " + _handler.ModelName + "." + data.Area.XParam + "!");
                }
            }

            //Move in Y
            if (data.Area.MovableMode == AreaMovableModes.YOnly || data.Area.MovableMode == AreaMovableModes.Both)
            {
                if (_handler.ParamExists(data.Area.YParam))
                {
                    if (data.Validated && _handler.HasAutoKey())
                    {
                        _handler.SaveKey(data.Area.YParam, data.Area.ValueY);
                    }
                    else
                    {
                        //_handler.Error(string.Format("{0} = {1}", data.Area.YParam, data.Area.ValueY));
                        _handler.SetValue(data.Area.YParam, data.Area.ValueY);
                    }
                }
                else
                {
                    _handler.Error("Can't find parameter " + _handler.ModelName + "." + data.Area.YParam + "!");
                }
            }
        }

        // SELECTION CHANGED => Switch on data.SelectionChangedMode and Select in DCC from "MetaData" Property

        void areaMap1_SelectionChanged(object sender, SelectionChangedEventArgs data)
        {
            List<string> objs = new List<string>();

            switch (data.SelectionChangedMode)
            {
                // DESELECT ALL
                case SelectionChangedModes.DeselectAll:
                    _handler.DeselectAll();
                    break;

                // SELECTION ADDED
                case SelectionChangedModes.SelectionAdded:
                    //( SelectionList, [HierarchyLevel], [CheckObjectSelectability] )
                    foreach (Area area in data.ChangedSelection)
                    {
                        if (!string.IsNullOrEmpty(area.MetaData))
                        {
                            objs.Add(area.MetaData);
                        }
                    }

                    if (objs.Count > 0)
                    {
                        _handler.AddToSelection(objs);
                    }
                    break;

                // SELECTION REMOVED
                case SelectionChangedModes.SelectionRemoved:
                    //( SelectionList, [HierarchyLevel], [CheckObjectSelectability] )
                    foreach (Area area in data.ChangedSelection)
                    {
                        if (!string.IsNullOrEmpty(area.MetaData))
                        {
                            objs.Add(area.MetaData);
                        }
                    }

                    if (objs.Count > 0)
                    {
                        _handler.RemoveFromSelection(objs);
                    }
                    break;

                // SELECTION UNKNOWN (SelectObj)
                case SelectionChangedModes.Unknown:
                    //( SelectionList, [HierarchyLevel], [CheckObjectSelectability] )
                    foreach (Area area in data.ChangedSelection)
                    {
                        if (!string.IsNullOrEmpty(area.MetaData))
                        {
                            objs.Add(area.MetaData);
                        }
                    }

                    if (objs.Count > 0)
                    {
                        _handler.SetSelection(objs);
                    }
                    break;
            }
        }

        // MOUSE CLICK => Execute code in "MetaData" Property

        void areaMap1_AreaMouseClick(object sender, AreaEventArgs data)
        {
            if (!string.IsNullOrEmpty(data.Area.MetaData))
            {
                int mode = 0;
                if (ModifierKeys == Keys.Shift)
                {
                    mode = 1;
                }
                else if (ModifierKeys == Keys.Control)
                {
                    mode = 2;
                }
                else if (ModifierKeys == Keys.Alt)
                {
                    mode = 3;
                }

                _handler.ExecuteCode(data.Area.MetaData, mode);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // _areaMapComponent
            // 
            this._areaMapComponent.Scaling = 1.99F;
            this._areaMapComponent.Size = new System.Drawing.Size(581, 398);
            // 
            // tabControl1
            // 
            this.tabControl1.Size = new System.Drawing.Size(581, 398);
            // 
            // DCCAreaMapControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.Name = "DCCAreaMapControl";
            this.ResumeLayout(false);

        }

        public override void RefreshSelection()
        {
            SetSelectionFromMetaData(_handler.GetSelection());
        }

        public override void RefreshValues()
        {
            //TKMessageBox.ShowError("RefreshValues called", "Call");
            foreach (Area area in AreaMapComponent.CurrentAreaMap.Areas)
            {
                if (area.IsMovable)
                {
                    if (area.IsMovableX && area.XParam != "")
                    {
                        if (_handler.ParamExists(area.XParam))
                        {
                            float val = _handler.GetValue(area.XParam);
                            //_handler.Error(string.Format("{0} = {1}", area.XParam, val));
                            area.ValueX = val;
                        }
                    }

                    if (area.IsMovableY && area.YParam != "")
                    {
                        if (_handler.ParamExists(area.YParam))
                        {
                            float val = _handler.GetValue(area.YParam);
                            //_handler.Error(string.Format("{0} = {1}",area.YParam, val));
                            area.ValueY = val;
                        }
                    }
                }
            }

            Invalidate();
        }
    }
}
