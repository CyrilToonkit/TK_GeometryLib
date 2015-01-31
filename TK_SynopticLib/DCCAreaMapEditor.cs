using System;
using System.Collections.Generic;
using System.Text;
using TK.GeometryLib.AreaMapFramework;
using System.Windows.Forms;

namespace TK.SynopticLib
{
    public class DCCAreaMapEditor : AreaMapEditor
    {
        public DCCAreaMapEditor()
            : base()
        {
            AreaMapComponent.AreaMouseClick += new AreaMapComponent.AreaMouseClickDelegate(areaMap1_AreaMouseClick);
            AreaMapComponent.SelectionChanged += new AreaMapComponent.SelectionChangedDelegate(areaMap1_SelectionChanged);
            AreaMapComponent.ValueChanged += new AreaMapComponent.ValueChangedDelegate(areaMap1_ValueChanged);
        }

        // VALUE CHANGED => Change parameter Value from "XParam" and "YParam" properties

        void areaMap1_ValueChanged(object sender, ValueChangedEventArgs data)
        {
            /*
            Property playControl = (Property)(Base.GetXSI().ActiveProject2 as XSIProject).Properties["Play Control"];
            Parameter param;

            //Move in X
            if (data.Area.MovableMode == AreaMovableModes.XOnly || data.Area.MovableMode == AreaMovableModes.Both)
            {
                param = Base.GetXSI().Dictionary.GetObject(_modelName + "." + data.Area.XParam, false) as Parameter;
                if (param != null)
                {
                    param.set_Value(playControl.Parameters["Current"].get_Value(0), data.Area.ValueX);
                }
                else
                {
                    Base.Error("Can't find parameter " + _modelName + "." + data.Area.XParam + "!");
                }
            }

            //Move in Y
            if (data.Area.MovableMode == AreaMovableModes.YOnly || data.Area.MovableMode == AreaMovableModes.Both)
            {
                param = Base.GetXSI().Dictionary.GetObject(_modelName + "." + data.Area.YParam, false) as Parameter;
                if (param != null)
                {
                    param.set_Value(playControl.Parameters["Current"].get_Value(0), data.Area.ValueY);
                }
                else
                {
                    Base.Error("Can't find parameter " + _modelName + "." + data.Area.YParam + "!");
                }
            }
             * */
        }

        // SELECTION CHANGED => Switch on data.SelectionChangedMode and Select in XSI from "MetaData" Property

        void areaMap1_SelectionChanged(object sender, SelectionChangedEventArgs data)
        {
            /*
            XSICollection coll;

            switch (data.SelectionChangedMode)
            {
                // DESELECT ALL
                case SelectionChangedModes.DeselectAll:
                    Base.GetXSI().ExecuteCommand("DeselectAll", null);
                    break;

                // SELECTION ADDED
                case SelectionChangedModes.SelectionAdded:
                    //( SelectionList, [HierarchyLevel], [CheckObjectSelectability] )
                    coll = (XSICollection)Base.GetFactory().CreateActiveXObject("XSI.Collection");
                    foreach (Area area in data.ChangedSelection)
                    {
                        ProjectItem item = Base.GetXSI().Dictionary.GetObject(_modelName + "." + area.MetaData, false) as ProjectItem;
                        if (item != null)
                        {
                            coll.Add(item);
                        }
                        else
                        {
                            Base.Error("Can't find item " + _modelName + "." + area.MetaData + "!");
                        }
                    }
                    if (coll.Count > 0)
                    {
                        try
                        {
                            Base.GetXSI().ExecuteCommand("AddToSelection", new object[] { coll, "ASITIS", false });
                        }
                        catch (Exception)
                        {

                        }
                    }
                    break;

                // SELECTION REMOVED
                case SelectionChangedModes.SelectionRemoved:
                    //( SelectionList, [HierarchyLevel], [CheckObjectSelectability] )
                    coll = (XSICollection)Base.GetFactory().CreateActiveXObject("XSI.Collection");
                    foreach (Area area in data.ChangedSelection)
                    {
                        ProjectItem item = Base.GetXSI().Dictionary.GetObject(_modelName + "." + area.MetaData, false) as ProjectItem;
                        if (item != null)
                        {
                            coll.Add(item);
                        }
                        else
                        {
                            Base.Error("Can't find item " + _modelName + "." + area.MetaData + "!");
                        }
                    }
                    if (coll.Count > 0)
                    {
                        try
                        {
                            Base.GetXSI().ExecuteCommand("RemoveFromSelection", new object[] { coll, "ASITIS" });
                        }
                        catch (Exception)
                        {

                        }
                    }
                    break;

                // SELECTION UNKNOWN (SelectObj)
                case SelectionChangedModes.Unknown:
                    //( SelectionList, [HierarchyLevel], [CheckObjectSelectability] )
                    coll = (XSICollection)Base.GetFactory().CreateActiveXObject("XSI.Collection");
                    foreach (Area area in data.ChangedSelection)
                    {
                        ProjectItem item = Base.GetXSI().Dictionary.GetObject(_modelName + "." + area.MetaData, false) as ProjectItem;
                        if (item != null)
                        {
                            coll.Add(item);
                        }
                        else
                        {
                            Base.Error("Can't find item " + _modelName + "." + area.MetaData + "!");
                        }
                    }
                    if (coll.Count > 0)
                    {
                        try
                        {
                            Base.GetXSI().ExecuteCommand("SelectObj", new object[] { coll, "ASITIS", false });
                        }
                        catch (Exception)
                        {

                        }
                    }
                    break;
            }
             * */
        }

        // MOUSE CLICK => Execute JScript code in "MetaData" Property

        void areaMap1_AreaMouseClick(object sender, AreaEventArgs data)
        {
            /*
            object Args = (object)(new object[]{_modelName});
            try
            {
                Base.GetXSI().ExecuteScriptCode("function TK_Execute(SelfModelName){" + data.Area.MetaData + "}", "JScript", "TK_Execute", ref Args);
            }
            catch (Exception)
            {
            }
             * */
        }
    }
}
