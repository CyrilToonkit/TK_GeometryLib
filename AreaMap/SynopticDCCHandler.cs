using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK.GraphComponents.Dialogs;

namespace TK.GeometryLib.AreaMapFramework
{
    public class SynopticDCCHandler
    {
        protected string _modelName = "None";
        public virtual string ModelName
        {
            get { return _modelName; }
            set { _modelName = value; }
        }

        public virtual bool HasAutoKey()
        {
            return false;
        }

        public virtual bool ParamExists(string inParam)
        {
            return false;
        }

        public virtual void DeselectAll()
        {
        }

        public virtual void AddToSelection(List<string> objs)
        {
        }

        public virtual void RemoveFromSelection(List<string> objs)
        {
        }

        public virtual void SetSelection(List<string> objs)
        {
        }

        public virtual List<string> GetSelection()
        {
            return new List<string>();
        }

        public virtual void ExecuteCode(string p, int mode)
        {
        }

        public virtual void ExecuteCommand(string p, params string[] args)
        {
        }

        public virtual void SaveKey(string inParam, float inValue)
        {
        }

        public virtual void SetValue(string inParam, float inValue)
        {
        }

        public virtual float GetValue(string p)
        {
            return 0f;
        }

        public virtual void Error(string p)
        {
            TKMessageBox.ShowError(p, "Generic error");
        }

        public virtual bool AddSelectSet(string newItem)
        {
            return false;
        }

        public virtual bool RemoveSelectSet(string p)
        {
            return false;
        }

        public virtual List<string> GetSelectSet(string selectedText)
        {
            return new List<string>();
        }

        public virtual List<string> GetStoredValues()
        {
            return new List<string>();
        }
    }
}
