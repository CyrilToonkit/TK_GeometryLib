using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK.SynopticLib;
using TK.GraphComponents.Dialogs;
using TK.GeometryLib.AreaMapFramework;
using TK.BaseLib;

namespace TK.GeometryLib.SynopticView
{
    public class SynopticTestHandler : SynopticDCCHandler
    {
        public override string ModelName
        {
            get { return _modelName; }
            set { _modelName = value; }
        }

        public override bool HasAutoKey()
        {
            return false;
        }

        public override bool ParamExists(string inParam)
        {
            return false;
        }

        public override void DeselectAll()
        {
        }

        public override void AddToSelection(List<string> objs)
        {
            TKMessageBox.ShowError("AddToSelection", "AddToSelection called");
        }

        public override void RemoveFromSelection(List<string> objs)
        {
            TKMessageBox.ShowError("RemoveFromSelection", "RemoveFromSelection called");
        }

        public override void SetSelection(List<string> objs)
        {
            //TKMessageBox.ShowError("SetSelection", "SetSelection called");
        }

        public override List<string> GetSelection()
        {
            return new List<string>();
        }

        public override void ExecuteCode(string p, int mode)
        {
            TKMessageBox.ShowError("Ask to execute '" + p + "', mode " + mode.ToString(), "ExecuteCode called");
        }

        public override void ExecuteCommand(string p, params string[] args)
        {
            List<string> lArgs = new List<string>(args);
            TKMessageBox.ShowError("Ask to execute command '" + p + "', args : '" + TypesHelper.Join(lArgs) + "'", "ExecuteCommand called");
        }

        public override void SaveKey(string inParam, float inValue)
        {
        }

        public override void SetValue(string inParam, float inValue)
        {
        }

        public override float GetValue(string p)
        {
            return 0f;
        }

        public override void Error(string p)
        {
            TKMessageBox.ShowError(p, "Generic error");
        }
    }
}
