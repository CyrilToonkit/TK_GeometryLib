using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using TK.GeometryLib;
using System.Xml.Serialization;
using System.IO;
using TK.BaseLib;
using TK.GraphComponents.Dialogs;
using TK_OSCARLib;
using TK.BaseLib.Math3D;

namespace TK.GeometryLib.AreaMapFramework
{
    public enum SelectionModes
    {
        RayCast, Rectangle
    }

    public partial class AreaMapComponent : Panel
    {
        #region EVENTS

        // -- AreaMouseEnter EVENT --
        // The delegate
        public delegate void AreaMouseEnterDelegate(object sender, AreaEventArgs data);
        // The event
        public event AreaMouseEnterDelegate AreaMouseEnter;
        // The method which fires the Event
        protected void OnAreaMouseEnter(object sender, AreaEventArgs data)
        {
            // Check if there are any Subscribers   
            if (AreaMouseEnter != null)
            {
                // Call the Event
                AreaMouseEnter(sender, data);
            }
        }

        // -- AreaMouseLeave EVENT --
        // The delegate
        public delegate void AreaMouseLeaveDelegate(object sender, AreaEventArgs data);
        // The event
        public event AreaMouseLeaveDelegate AreaMouseLeave;
        // The method which fires the Event
        protected void OnAreaMouseLeave(object sender, AreaEventArgs data)
        {
            // Check if there are any Subscribers   
            if (AreaMouseLeave != null)
            {
                // Call the Event
                AreaMouseLeave(sender, data);
            }
        }

        // -- AreaMouseDown EVENT --
        // The delegate
        public delegate void AreaMouseDownDelegate(object sender, AreaEventArgs data);
        // The event
        public event AreaMouseDownDelegate AreaMouseDown;
        // The method which fires the Event
        protected void OnAreaMouseDown(object sender, AreaEventArgs data)
        {
            // Check if there are any Subscribers   
            if (AreaMouseDown != null)
            {
                // Call the Event
                AreaMouseDown(sender, data);
            }
        }

        // -- AreaMouseUp EVENT --
        // The delegate
        public delegate void AreaMouseUpDelegate(object sender, AreaEventArgs data);
        // The event
        public event AreaMouseUpDelegate AreaMouseUp;
        // The method which fires the Event
        protected void OnAreaMouseUp(object sender, AreaEventArgs data)
        {
            // Check if there are any Subscribers   
            if (AreaMouseUp != null)
            {
                // Call the Event
                AreaMouseUp(sender, data);
            }
        }

        // -- AreaMouseMove EVENT --
        // The delegate
        public delegate void AreaMouseMoveDelegate(object sender, AreaEventArgs data);
        // The event
        public event AreaMouseMoveDelegate AreaMouseMove;
        // The method which fires the Event
        protected void OnAreaMouseMove(object sender, AreaEventArgs data)
        {
            // Check if there are any Subscribers   
            if (AreaMouseMove != null)
            {
                // Call the Event
                AreaMouseMove(sender, data);
            }
        }

        // -- AreaMouseClick EVENT --
        // The delegate
        public delegate void AreaMouseClickDelegate(object sender, AreaEventArgs data);
        // The event
        public event AreaMouseClickDelegate AreaMouseClick;
        // The method which fires the Event
        protected void OnAreaMouseClick(object sender, AreaEventArgs data)
        {
            // Check if there are any Subscribers   
            if (AreaMouseClick != null)
            {
                // Call the Event
                AreaMouseClick(sender, data);
            }
        }

        // -- SelectionChanged EVENT --
        // The delegate
        public delegate void SelectionChangedDelegate(object sender, SelectionChangedEventArgs data);
        // The event
        public event SelectionChangedDelegate SelectionChanged;
        // The method which fires the Event
        protected void OnSelectionChanged(object sender, SelectionChangedEventArgs data)
        {
            // Check if there are any Subscribers   
            if (SelectionChanged != null)
            {
                // Call the Event
                SelectionChanged(sender, data);
            }
        }

        // -- ValueChanged EVENT --
        // The delegate
        public delegate void ValueChangedDelegate(object sender, ValueChangedEventArgs data);
        // The event
        public event ValueChangedDelegate ValueChanged;
        // The method which fires the Event
        protected void OnValueChanged(object sender, ValueChangedEventArgs data)
        {
            // Check if there are any Subscribers   
            if (ValueChanged != null)
            {
                // Call the Event
                ValueChanged(sender, data);
            }
        }

        // -- ValueChanged EVENT --
        // The delegate
        public delegate void ScalingChangedDelegate(object sender, EventArgs data);
        // The event
        public event ScalingChangedDelegate ScalingChanged;
        // The method which fires the Event
        protected void OnScalingChanged(object sender, EventArgs data)
        {
            // Check if there are any Subscribers   
            if (ScalingChanged != null)
            {
                // Call the Event
                ScalingChanged(sender, data);
            }
        }

        // -- FunctionCalled EVENT --
        // The delegate
        public delegate void FunctionCalledDelegate(object sender, FunctionEventArgs data);
        // The event
        public event FunctionCalledDelegate FunctionCalled;
        // The method which fires the Event
        protected void OnFunctionCalled(object sender, FunctionEventArgs data)
        {
            // Check if there are any Subscribers   
            if (FunctionCalled != null)
            {
                // Call the Event
                FunctionCalled(sender, data);
            }
        }
        #endregion

        public AreaMapComponent()
        {
            _maps.Add(new AreaMap());
            InitializeComponent();
            DoubleBuffered = true;

            RegisterEvents();
        }

        public AreaMapComponent(IContainer container)
        {
            container.Add(this);
            _maps.Add(new AreaMap());
            InitializeComponent();
            DoubleBuffered = true;

            RegisterEvents();
        }

        bool _isEditing = false;
        bool _isDocked = true;
        bool _isDragging = false;
        bool _isTransforming = false;
        bool _isRotating = false;
        bool _isScaling = false;
        Vector2 _dragPoint = new Vector2(-1, -1);
        Area _dragged = null;
        Vector2 _selectPoint = new Vector2(-1, -1);
        SelectionModes _selectionMode = SelectionModes.Rectangle;
        GeometryLib.Rectangle _draggedSelection = new GeometryLib.Rectangle();
        Polygon2 _tranform = new Polygon2(new Vector2(), new Vector2(), new Vector2(), new Vector2());
        List<Area> _tranforming = new List<Area>();
        Stroke2 _selectStroke = new Stroke2();
        List<AreaMap> _maps = new List<AreaMap>();
        //List<AreaGroup> _groups = new List<AreaGroup>();
        int _currentGroupIndex = 0;
        int _currentIndex = 0;
        Area _hoverArea = null;
        Size _dimensions = new Size(100, 200);
        float _scaling = 1f;
        Vector2 _offset = Vector2.Null;
        List<Area> _selection = new List<Area>();

        List<Area> _unValidatedValues = new List<Area>();
        Pen _boldPen = new Pen(Color.FromArgb(120, 0, 0, 0), 3f);
        Pen _selectPen = new Pen(Color.DodgerBlue, 1f);
        Brush _selectBrush = new SolidBrush(Color.FromArgb(50, Color.DodgerBlue));

        bool _showAll = false;
        bool _showGrid = false;
        bool _gridOnTop = false;
        bool _showCenter = false;

        int CenterX
        {
            get { return CurrentAreaMap != null ? (int)CurrentAreaMap.Center.X : 0; }
            set
            {
                if (CurrentAreaMap != null)
                    CurrentAreaMap.Center = new Vector2((float)value, CurrentAreaMap.Center.Y);
            }
        }

        internal void RenameFromMapping(Dictionary<string, string> mapping)
        {
            foreach(AreaMap map in _maps)
            {
                foreach (Area area in map.Areas)
                {
                    foreach (KeyValuePair<string,string> kvp in mapping)
                    {
                        area.Name = area.Name.Replace(kvp.Key, kvp.Value);
                        area.MetaData = area.MetaData.Replace(kvp.Key, kvp.Value);
                    }
                }
            }
        }

        int CenterY
        {
            get { return CurrentAreaMap != null ? (int)CurrentAreaMap.Center.Y : 0; }
            set
            {
                if (CurrentAreaMap != null)
                    CurrentAreaMap.Center = new Vector2(CurrentAreaMap.Center.X, (float)value);
            }
        }

        DialogResult rslt = DialogResult.Ignore;

        public AreaMap CurrentAreaMap
        {
            get { return (_maps != null && _maps.Count > _currentIndex) ? _maps[_currentIndex] : null; }
        }

        public AreaGroup CurrentGroup
        {
            get { return (Groups != null && Groups.Count > _currentGroupIndex) ? Groups[_currentGroupIndex] : null; }
        }

        public List<AreaMap> Maps
        {
            get { return _maps; }
        }

        public List<AreaGroup> Groups
        {
            get { return CurrentAreaMap.Groups; }
            set { CurrentAreaMap.Groups = value; }
        }

        public SelectionModes SelectionMode
        {
            get { return _selectionMode; }
            set { _selectionMode = value; }
        }

        public string SelfPath
        {
            get
            {
                string path = "";

                if (Maps.Count > 0)
                {
                    FileInfo info = new FileInfo(Maps[0].Path);
                    path =  info.Directory.Parent.FullName;
                }

                return path;
            }
        }

        public string ReplaceVariables(string instring)
        {
            return instring.Replace("SelfPath", SelfPath);
        }

        [Browsable(false)]
        public bool IsEditing
        {
            get { return _isEditing; }
            set
            {
                if (value != _isEditing)
                {
                    if (_isEditing)
                    {
                        StopEdit();
                    }
                    else
                    {
                        Edit();
                    }
                }
            }
        }

        [Browsable(false)]
        public bool IsDocked
        {
            get { return _isDocked; }
            set
            {
                _isDocked = value;
                if (_isDocked)
                {
                    RefreshOffset();
                    Invalidate();
                }
            }
        }

        [Browsable(false)]
        public bool IsTransforming
        {
            get { return _isTransforming; }
            set
            {
                if (_isTransforming != value)
                {
                    if(value)
                    {
                        List<Area> sel = GetEditSelection();
                        if (sel.Count > 0)
                        {
                            foreach (Area area in sel)
                            {
                                _tranforming.Add(area);
                            }

                            _tranform.Points[0] = new Vector2(float.MaxValue, float.MaxValue);
                            _tranform.Points[1] = new Vector2(float.MinValue, float.MaxValue);
                            _tranform.Points[2] = new Vector2(float.MinValue, float.MinValue);
                            _tranform.Points[3] = new Vector2(float.MaxValue, float.MinValue);

                            foreach (Area area in _tranforming)
                            {
                                //Corner
                                if (area.Shape.Corner.X < _tranform.Points[0].X)
                                {
                                    _tranform.Points[0].X = area.Shape.Corner.X;
                                    _tranform.Points[3].X = area.Shape.Corner.X;
                                }
                                if (area.Shape.Corner.Y < _tranform.Points[0].Y)
                                {
                                    _tranform.Points[0].Y = area.Shape.Corner.Y;
                                    _tranform.Points[1].Y = area.Shape.Corner.Y;
                                }

                                //UpperRightCorner
                                if (area.Shape.LowerRightCorner.X > _tranform.Points[2].X)
                                {
                                    _tranform.Points[2].X = area.Shape.LowerRightCorner.X;
                                    _tranform.Points[1].X = area.Shape.LowerRightCorner.X;
                                }
                                if (area.Shape.LowerRightCorner.Y > _tranform.Points[2].Y)
                                {
                                    _tranform.Points[2].Y = area.Shape.LowerRightCorner.Y;
                                    _tranform.Points[3].Y = area.Shape.LowerRightCorner.Y;
                                }
                            }

                            _tranform.Scale(new Vector2(1.1f, 1.1f));
                            _isTransforming = true;
                        }
                    }
                    else
                    {
                        _tranforming.Clear();
                        SetCursorDefault();
                        _isTransforming = false;
                    }

                    Invalidate();
                }
            }
        }

        [Browsable(false)]
        public bool ShowAll
        {
            get { return _showAll; }
            set
            {
                _showAll = value;
                ApplyGroupValues();
            }
        }

        void RegisterEvents()
        {
            AreaMouseEnter += new AreaMouseEnterDelegate(AreaMap_AreaMouseEnter);
            AreaMouseLeave += new AreaMouseLeaveDelegate(AreaMap_AreaMouseLeave);
            AreaMouseDown += new AreaMouseDownDelegate(AreaMap_AreaMouseDown);
            AreaMouseUp += new AreaMouseUpDelegate(AreaMap_AreaMouseUp);
            AreaMouseMove += new AreaMouseMoveDelegate(AreaMap_AreaMouseMove);
            MouseWheel += new MouseEventHandler(AreaMapComponent_MouseWheel);    
        }

        void resetMenuItem_Click(object sender, System.EventArgs e)
        {
            List<Area> selAreas = GetEditSelection();
            Area area = (Area)(sender as ToolStripMenuItem).Owner.Tag;

            if (!selAreas.Contains(area))
            {
                selAreas.Add(area);
            }

            foreach (Area selarea in selAreas)
            {
                Reset(selarea);
            }
        }

        void deletePointMenuItem_Click(object sender, System.EventArgs e)
        {
            Area area = (Area)(sender as ToolStripMenuItem).Owner.Tag;
            StopEdit(area.Parent);
            (area.Parent.Shape as Polygon2).Points.RemoveAt(area.Index);
            Edit(area.Parent);

            FunctionCalled(this, new FunctionEventArgs("Refresh", new object[0]));
            Invalidate();
        }

        void limitValuesMenuItem_Click(object sender, System.EventArgs e)
        {
            List<Area> selAreas = GetEditSelection();
            Area newarea = (Area)(sender as ToolStripMenuItem).Owner.Tag;

            if (!selAreas.Contains(newarea))
            {
                selAreas.Add(newarea);
            }

            foreach (Area area in selAreas)
            {
                float oldX = area.ValueX;
                float oldY = area.ValueY;

                area.Limit();

                if (area.IsMovable && !area.IsEditing)
                {
                    OnValueChanged(this, new ValueChangedEventArgs(oldX, oldY, area, true));
                }
            }

            Invalidate();
        }

        void symLRMenuItem_Click(object sender, System.EventArgs e)
        {
            List<Area> selAreas = GetEditSelection();
            Area newarea = (Area)(sender as ToolStripMenuItem).Owner.Tag;

            if (!selAreas.Contains(newarea))
            {
                selAreas.Add(newarea);
            }

            foreach (Area area in selAreas)
            {
                if (area.XYSymmetryMode)
                {
                    float oldY = area.ValueY;
                    area.ValueY = area.ValueX;

                    if (area.IsMovable && !area.IsEditing)
                    {
                        OnValueChanged(this, new ValueChangedEventArgs(area.ValueX, oldY, area, true));
                    }
                }
                else
                {
                    if (area.Name.Contains("Left"))
                    {
                        Area oppositeArea = CurrentAreaMap.GetArea(area.Name.Replace("Left", "Right"));

                        float oldX = oppositeArea.ValueX;
                        float oldY = oppositeArea.ValueY;

                        oppositeArea.ValueX = area.ValueX;
                        oppositeArea.ValueY = area.ValueY;

                        if (oppositeArea.IsMovable && !oppositeArea.IsEditing)
                        {
                            OnValueChanged(this, new ValueChangedEventArgs(oldX, oldY, oppositeArea, true));
                        }
                    }
                    else
                    {
                        float oldX = area.ValueX;
                        float oldY = area.ValueY;

                        Area oppositeArea = CurrentAreaMap.GetArea(area.Name.Replace("Right", "Left"));

                        area.ValueX = oppositeArea.ValueX;
                        area.ValueY = oppositeArea.ValueY;

                        if (area.IsMovable && !area.IsEditing)
                        {
                            OnValueChanged(this, new ValueChangedEventArgs(oldX, oldY, area, true));
                        }
                    }
                }
            }

            Invalidate();
        }

        void symRLMenuItem_Click(object sender, System.EventArgs e)
        {
           List<Area> selAreas = GetEditSelection();
            Area newarea = (Area)(sender as ToolStripMenuItem).Owner.Tag;

            if (!selAreas.Contains(newarea))
            {
                selAreas.Add(newarea);
            }

            foreach (Area area in selAreas)
            {
                if (area.XYSymmetryMode)
                {
                    float oldX = area.ValueX;
                    area.ValueX = area.ValueY;

                    if (area.IsMovable && !area.IsEditing)
                    {
                        OnValueChanged(this, new ValueChangedEventArgs(oldX, area.ValueY, area, true));
                    }
                }
                else
                {
                    if (area.Name.Contains("Left"))
                    {
                        float oldX = area.ValueX;
                        float oldY = area.ValueY;

                        Area oppositeArea = CurrentAreaMap.GetArea(area.Name.Replace("Left", "Right"));
                        area.ValueX = oppositeArea.ValueX;
                        area.ValueY = oppositeArea.ValueY;

                        if (area.IsMovable && !area.IsEditing)
                        {
                            OnValueChanged(this, new ValueChangedEventArgs(oldX, oldY, area, true));
                        }
                    }
                    else
                    {
                        Area oppositeArea = CurrentAreaMap.GetArea(area.Name.Replace("Right", "Left"));

                        float oldX = oppositeArea.ValueX;
                        float oldY = oppositeArea.ValueY;

                        oppositeArea.ValueX = area.ValueX;
                        oppositeArea.ValueY = area.ValueY;

                        if (oppositeArea.IsMovable && !oppositeArea.IsEditing)
                        {
                            OnValueChanged(this, new ValueChangedEventArgs(oldX, oldY, oppositeArea, true));
                        }
                    }
                }
            }

            Invalidate();
        }

        void addPointMenuItem_Click(object sender, System.EventArgs e)
        {
            Area area = (Area)(sender as ToolStripMenuItem).Owner.Tag;
            StopEdit(area);
            //Search two points closer to mouse
            Polygon2 shape = area.Shape as Polygon2;
            float closest = float.MaxValue;
            int closest1Index = -1;
            int closest2Index = -1;

            Vector2 mousePos = GeoConverter.Convert(PointToClient((sender as ToolStripMenuItem).Owner.Location), _offset, _scaling);

            int index = 0;
            foreach (Vector2 pos in shape.Points)
            {
                float dist = (pos - mousePos).Length;

                if (dist < closest)
                {
                    closest = dist;
                    closest1Index = index;
                }

                index++;
            }

            index = 0;
            closest = float.MaxValue;
            foreach (Vector2 pos in shape.Points)
            {
                if (index != closest1Index)
                {
                    float dist = (pos - mousePos).Length;

                    if (dist < closest && (Math.Abs(closest1Index - index) == 1 || Math.Abs(closest1Index - index) == shape.Points.Count-1))
                    {
                        closest = dist;
                        closest2Index = index;
                    }
                }
                index++;
            }

            //Insert a point in between the two
            Vector2 meanPoint = (shape.Points[closest1Index] + shape.Points[closest2Index]) / 2;

            int InsertAt = Math.Max(closest1Index, closest2Index);
            if (Math.Abs(closest1Index - index) == shape.Points.Count - 1)
            {
                shape.Points.Add(meanPoint);
            }
            else
            {
                shape.Points.Insert(InsertAt, meanPoint);
            }
            Edit(area);

            FunctionCalled(this, new FunctionEventArgs("Refresh", new object[0]));
            Invalidate();
        }

        public void Reset(Area area)
        {
            float oldX = 0f;
            float oldY = 0f;

            if (area.IsMovable && !area.IsEditing)
            {
                oldX = area.ValueX;
                oldY = area.ValueY;
            }

            area.Reset();

            if (area.IsMovable && !area.IsEditing )
            {
                OnValueChanged(this, new ValueChangedEventArgs(oldX, oldY, area, true));
            }

            Invalidate();
        }

        void AreaMap_AreaMouseEnter(object sender, AreaEventArgs data)
        {
            data.Area.IsHovered = true;
            SetCursorHover(data.Area);
            Invalidate();
        }

        #region CURSORS

        private void SetCursorHover(Area area)
        {
            switch (area.MovableMode)
            {
                    //Moving modes
                case AreaMovableModes.Both:
                    Cursor = Cursors.SizeAll;
                    break;
                case AreaMovableModes.XOnly:
                    Cursor = Cursors.SizeWE;
                    break;
                case AreaMovableModes.YOnly:
                    Cursor = Cursors.SizeNS;
                    break;

                    //Select modes
                default:
                    if (area.IsSelectable || area.Behavior == AreaBehavior.Button)
                    {
                        //Center scales
                        if (area.IsSubComponent && area.Index == -1)
                        {
                            Cursor = Cursors.SizeWE;
                        }
                        else
                        {
                            Cursor = Cursors.Hand;
                        }
                    }
                    else
                    {
                        SetCursorDefault();
                    }
                    break;
            }
        }

        private void SetCursorDefault()
        {
            Cursor = Cursors.Default;
        }

        #endregion

        void AreaMap_AreaMouseLeave(object sender, AreaEventArgs data)
        {
            data.Area.IsHovered = false;
            if (data.Area.IsSelected && !data.Area.IsSelectable) { data.Area.IsSelected = false; }
            SetCursorDefault();
            Invalidate();
        }

        void AreaMap_AreaMouseDown(object sender, AreaEventArgs data)
        {
            _dragPoint = new Vector2(data.X, data.Y);
            _isDragging = false;
            _dragged = data.Area;

            if (!data.Area.IsSelected && !data.Area.IsSelectable)
            {
                SelectwithModifiers(data.Area);
            }
        }

        void AreaMap_AreaMouseUp(object sender, AreaEventArgs data)
        {
            if (!_isDragging)
            {
                if (data.Button == MouseButtons.Right)
                {
                    contextMenuStrip1.Items[2].Visible = false;//SymL->R
                    contextMenuStrip1.Items[3].Visible = false;//SymR->L
                    contextMenuStrip1.Items[4].Visible = false;//Limit

                    if (IsEditing)
                    {
                        //Is a point from a polygon
                        if(data.Area.IsSubComponent 
                        && data.Area.Parent.Type == ConvertibleShapeType.Polygon
                        && data.Area.Index >= 0 )
                        {
                            if ((data.Area.Parent.Shape as Polygon2).Points.Count > 3)
                            {
                                contextMenuStrip2.Tag = data.Area;
                                contextMenuStrip2.Show(PointToScreen(data.Location));
                            }
                        }
                        else if (data.Area.Type == ConvertibleShapeType.Polygon)
                        {
                            contextMenuStrip1.Items[1].Visible = true;
                            contextMenuStrip1.Items[0].Visible = false;
                            contextMenuStrip1.Tag = data.Area;
                            contextMenuStrip1.Show(PointToScreen(data.Location));
                        }
                    }
                    else if(data.Area.IsMovable)
                    {
                        contextMenuStrip1.Items[1].Visible = false;
                        contextMenuStrip1.Items[0].Visible = true; 
                        contextMenuStrip1.Tag = data.Area;

                        if (data.Area.HasLimits())
                        {
                            contextMenuStrip1.Items[4].Visible = true; 
                        }

                        if (data.Area.XYSymmetryMode)
                        {
                            contextMenuStrip1.Items[2].Visible = true;
                            contextMenuStrip1.Items[3].Visible = true;
                        }
                        else
                        {
                            Area oppositeArea = CurrentAreaMap.GetArea(data.Area.Name.Contains("Left") ? data.Area.Name.Replace("Left", "Right") : data.Area.Name.Replace("Right", "Left"));
                            if (oppositeArea != null && oppositeArea.Name != data.Area.Name)
                            {
                                contextMenuStrip1.Items[2].Visible = true;
                                contextMenuStrip1.Items[3].Visible = true;
                            }
                        }

                        contextMenuStrip1.Show(PointToScreen(data.Location));
                    }
                }
                else
                {
                    if (data.Area.IsSelected && !data.Area.IsSelectable)
                    {
                        if (data.Area.Behavior == AreaBehavior.Button && !IsEditing)
                        {
                            data.Area = data.Area.Clone();
                            //Check if we have some commands
                            if (data.Area.MetaData.Contains(Area.COMMANDTAG))
                            {
                                string metaData = data.Area.MetaData;
                                string[] args;

                                AreaMapControl control = GetAreaMapControl();

                                while (metaData.Contains(Area.COMMANDTAG))
                                {
                                    int tagIndex = metaData.IndexOf(Area.COMMANDTAG);
                                    int argsIndex = metaData.IndexOf('(', tagIndex);

                                    string cmdName = metaData.Substring(tagIndex + 1, argsIndex - tagIndex - 1);
                                    args = GetArguments(metaData, Area.COMMANDTAG + cmdName);

                                    if (control != null)
                                    {
                                        control.Handler.ExecuteCommand(cmdName, args);
                                    }

                                    metaData = Remove(metaData, Area.COMMANDTAG);
                                }

                                data.Area.MetaData = metaData;
                            }

                            //Check if we have some internal function
                            if (data.Area.MetaData.Contains(Area.FUNCTIONTAG))
                            {
                                string metaData = data.Area.MetaData;
                                string arg = "";
                                string paramList = "";

                                while (metaData.Contains(Area.FUNCTIONTAG))
                                {
                                    if (metaData.Contains(Area.FUNCTIONTAG + "ShowArea("))
                                    {
                                        arg = GetArgument(metaData, Area.FUNCTIONTAG + "ShowArea");
                                        ShowArea(arg);
                                        metaData = Remove(metaData, Area.FUNCTIONTAG + "ShowArea");
                                    }
                                    else if (metaData.Contains(Area.FUNCTIONTAG + "HideArea("))
                                    {
                                        arg = GetArgument(metaData, Area.FUNCTIONTAG + "HideArea");
                                        HideArea(arg);
                                        metaData = Remove(metaData, Area.FUNCTIONTAG + "HideArea");
                                    }
                                    else if (metaData.Contains(Area.FUNCTIONTAG + "ToggleArea("))
                                    {
                                        arg = GetArgument(metaData, Area.FUNCTIONTAG + "ToggleArea");
                                        ToggleGroup(arg);
                                        metaData = Remove(metaData, Area.FUNCTIONTAG + "ToggleArea");
                                    }
                                    else if (metaData.Contains(Area.FUNCTIONTAG + "ShowGroup("))
                                    {
                                        arg = GetArgument(metaData, Area.FUNCTIONTAG + "ShowGroup");
                                        ShowGroup(arg);
                                        metaData = Remove(metaData, Area.FUNCTIONTAG + "ShowGroup");
                                    }
                                    else if (metaData.Contains(Area.FUNCTIONTAG + "HideGroup("))
                                    {
                                        arg = GetArgument(metaData, Area.FUNCTIONTAG + "HideGroup");
                                        HideGroup(arg);
                                        metaData = Remove(metaData, Area.FUNCTIONTAG + "HideGroup");
                                    }
                                    else if (metaData.Contains(Area.FUNCTIONTAG + "ToggleGroup("))
                                    {
                                        arg = GetArgument(metaData, Area.FUNCTIONTAG + "ToggleGroup");
                                        ToggleGroup(arg);
                                        metaData = Remove(metaData, Area.FUNCTIONTAG + "ToggleGroup");
                                    }
                                    else if (metaData.Contains(Area.FUNCTIONTAG + "FreezeGroup("))
                                    {
                                        arg = GetArgument(metaData, Area.FUNCTIONTAG + "FreezeGroup");
                                        FreezeGroup(arg);
                                        metaData = Remove(metaData, Area.FUNCTIONTAG + "FreezeGroup");
                                    }
                                    else if (metaData.Contains(Area.FUNCTIONTAG + "UnfreezeGroup("))
                                    {
                                        arg = GetArgument(metaData, Area.FUNCTIONTAG + "UnfreezeGroup");
                                        UnfreezeGroup(arg);
                                        metaData = Remove(metaData, Area.FUNCTIONTAG + "UnfreezeGroup");
                                    }
                                    else if (metaData.Contains(Area.FUNCTIONTAG + "SelectArea("))
                                    {
                                        arg = GetArgument(metaData, Area.FUNCTIONTAG + "SelectArea");
                                        SelectArea(arg);
                                        metaData = Remove(metaData, Area.FUNCTIONTAG + "SelectArea");
                                    }
                                    else if (metaData.Contains(Area.FUNCTIONTAG + "SelectGroup("))
                                    {
                                        arg = GetArgument(metaData, Area.FUNCTIONTAG + "SelectGroup");
                                        SelectGroup(arg);
                                        metaData = Remove(metaData, Area.FUNCTIONTAG + "SelectGroup");
                                    }
                                    else if (metaData.Contains(Area.FUNCTIONTAG + "SelectAll("))
                                    {
                                        SelectAll();
                                        metaData = Remove(metaData, Area.FUNCTIONTAG + "SelectAll");
                                    }
                                    else if (metaData.Contains(Area.FUNCTIONTAG + "DeselectAll("))
                                    {
                                        DeselectAll();
                                        metaData = Remove(metaData, Area.FUNCTIONTAG + "DeselectAll");
                                    }
                                    else if (metaData.Contains(Area.FUNCTIONTAG + "RefreshSelection("))
                                    {
                                        OnFunctionCalled(this, new FunctionEventArgs("RefreshSelection", new object[0]));
                                        metaData = Remove(metaData, Area.FUNCTIONTAG + "RefreshSelection");
                                    }
                                    else if (metaData.Contains(Area.FUNCTIONTAG + "ResetAll("))
                                    {
                                        ResetAll();
                                        metaData = Remove(metaData, Area.FUNCTIONTAG + "ResetAll");
                                    }
                                    else if (metaData.Contains(Area.FUNCTIONTAG + "ResetArea("))
                                    {
                                        arg = GetArgument(metaData, Area.FUNCTIONTAG + "ResetArea");
                                        ResetArea(arg);
                                        metaData = Remove(metaData, Area.FUNCTIONTAG + "ResetArea");
                                    }
                                    else if (metaData.Contains(Area.FUNCTIONTAG + "ResetCurrent("))
                                    {
                                        ResetCurrent();
                                        metaData = Remove(metaData, Area.FUNCTIONTAG + "ResetCurrent");
                                    }
                                    else if (metaData.Contains(Area.FUNCTIONTAG + "GetSelectedParams()"))
                                    {
                                        paramList = GetSelectedParams();
                                        metaData = metaData.Replace(Area.FUNCTIONTAG + "GetSelectedParams()", "\"" + paramList + "\"");
                                    }
                                    else if (metaData.Contains(Area.FUNCTIONTAG + "GetCurrentParams()"))
                                    {
                                        paramList = GetCurrentParams();
                                        metaData = metaData.Replace(Area.FUNCTIONTAG + "GetCurrentParams()", "\"" + paramList + "\"");
                                    }
                                    else if (metaData.Contains(Area.FUNCTIONTAG + "GetAllParams()"))
                                    {
                                        paramList = GetAllParams();
                                        metaData = metaData.Replace(Area.FUNCTIONTAG + "GetAllParams()", "\"" + paramList + "\"");
                                    }
                                    else
                                    {
                                        metaData = metaData.Remove(metaData.IndexOf(Area.FUNCTIONTAG), 1);
                                    }
                                }

                                data.Area.MetaData = metaData;
                            }

                            OnAreaMouseClick(this, data);
                        }
                        data.Area.IsSelected = false;
                        Invalidate();
                    }
                    else
                    {
                        SelectwithModifiers(data.Area);
                    }
                }
            }
        }

        public virtual string GetAllParams()
        {
            List<string> paramsList = new List<string>();

            foreach (AreaMap areaMap in _maps)
            {
                foreach (Area area in areaMap.Areas)
                {
                    if (!string.IsNullOrEmpty(area.XParam) && !paramsList.Contains("$." + area.XParam))
                    {
                        paramsList.Add("$." + area.XParam);
                    }

                    if (!string.IsNullOrEmpty(area.YParam) && !paramsList.Contains("$." + area.YParam))
                    {
                        paramsList.Add("$." + area.YParam);
                    }
                }
            }

            return TypesHelper.Join(paramsList);
        }

        public virtual string GetCurrentParams()
        {
            List<string> paramsList = new List<string>();

            foreach (Area area in CurrentAreaMap.Areas)
            {
                if (!string.IsNullOrEmpty(area.XParam) && !paramsList.Contains("$." + area.XParam))
                {
                    paramsList.Add("$." + area.XParam);
                }

                if (!string.IsNullOrEmpty(area.YParam) && !paramsList.Contains("$." + area.YParam))
                {
                    paramsList.Add("$." + area.YParam);
                }
            }

            return TypesHelper.Join(paramsList);
        }

        public virtual string GetSelectedParams()
        {
            List<string> paramsList = new List<string>();

            foreach (AreaMap areaMap in _maps)
            {
                foreach (Area area in areaMap.Areas)
                {
                    if (area.IsSelected)
                    {
                        if (!string.IsNullOrEmpty(area.XParam) && !paramsList.Contains("$." + area.XParam))
                        {
                            paramsList.Add("$." + area.XParam);
                        }

                        if (!string.IsNullOrEmpty(area.YParam) && !paramsList.Contains("$." + area.YParam))
                        {
                            paramsList.Add("$." + area.YParam);
                        }
                    }
                }
            }

            return TypesHelper.Join(paramsList);
        }

        public AreaMapControl GetAreaMapControl()
        {
            AreaMapControl pagesContainer = null;

            try
            {
                UserControl synopticUCtrl = Parent.Parent.Parent.Parent.Parent as UserControl;
                if (synopticUCtrl != null)
                {
                    pagesContainer = synopticUCtrl.Controls[0] as AreaMapControl;
                }
            }
            catch (Exception e) { }

            return pagesContainer;
        }

        public List<AreaMapComponent> GetSynopticPages()
        {
            List<AreaMapComponent> pages = new List<AreaMapComponent>();

            AreaMapControl pagesContainer = GetAreaMapControl();

            if (pagesContainer != null)
            {
                pages.Add(pagesContainer.AreaMapComponent);
            }

            return pages;
        }

        #region Internal functions

        public void SelectGroup(string arg)
        {
            AreaGroup areaG = FindGroup(arg);
            if (areaG != null)
            {
                SelectGroupWithModifiers(areaG);
            }
            else
            {
                List<AreaMapComponent> pages = GetSynopticPages();

                foreach (AreaMapComponent page in pages)
                {
                    areaG = page.FindGroup(arg);

                    if (areaG != null)
                    {
                        page.SelectGroupWithModifiers(areaG);
                    }
                }
            }
        }

        public void SelectArea(string arg)
        {
            Area area = FindArea(arg);
            if (area != null)
            {
                SelectWithModifiers(new List<Area> { area });
            }
        }

        public void ToggleArea(string arg)
        {
            Area area = FindArea(arg);
            if (area != null)
            {
                area.IsVisible = !area.IsVisible;
                Invalidate();
            }
        }

        public void HideArea(string arg)
        {
            Area area = FindArea(arg);
            if (area != null)
            {
                area.IsVisible = false;
                Invalidate();
            }
        }

        public void ShowArea(string arg)
        {
            Area area = FindArea(arg);
            if (area != null)
            {
                area.IsVisible = true;
                Invalidate();
            }
        }

        public void ToggleGroup(string arg)
        {
            AreaGroup areaG = FindGroup(arg);
            if (areaG != null)
            {
                areaG.Visible = !areaG.Visible;
                ApplyGroupValues();
                Invalidate();
            }
        }

        public void HideGroup(string arg)
        {
            AreaGroup areaG = null;
            areaG = FindGroup(arg);
            if (areaG != null)
            {
                areaG.Visible = false;
                ApplyGroupValues();
                Invalidate();
            }

            List<AreaMapComponent> pages = GetSynopticPages();

            foreach (AreaMapComponent page in pages)
            {
                foreach (AreaMap map in page.Maps)
                {
                    areaG = map.FindGroup(arg);

                    if (areaG != null)
                    {
                        areaG.Visible = false;
                    }
                }

                page.ApplyGroupValues();
                page.Invalidate();
            }
        }

        public void ShowGroup(string arg)
        {
            AreaGroup areaG = null;
            areaG = FindGroup(arg);

            if (areaG != null)
            {
                areaG.Visible = true;
                ApplyGroupValues();
                Invalidate();
            }

            List<AreaMapComponent> pages = GetSynopticPages();

            foreach (AreaMapComponent page in pages)
            {
                foreach (AreaMap map in page.Maps)
                {
                    areaG = map.FindGroup(arg);

                    if (areaG != null)
                    {
                        areaG.Visible = true;
                    }
                }

                page.ApplyGroupValues();
                page.Invalidate();
            }
        }

        public void UnfreezeGroup(string arg)
        {
            AreaGroup areaG = FindGroup(arg);
            if (areaG != null)
            {
                areaG.Active = true;
                ApplyGroupValues();
                Invalidate();
            }
            else
            {
                List<AreaMapComponent> pages = GetSynopticPages();

                foreach (AreaMapComponent page in pages)
                {
                    areaG = page.FindGroup(arg);

                    if (areaG != null)
                    {
                        page.UnfreezeGroup(areaG.Name);
                    }
                }
            }
        }

        public void FreezeGroup(string arg)
        {
            AreaGroup areaG = FindGroup(arg);
            if (areaG != null)
            {
                areaG.Active = false;
                ApplyGroupValues();
                Invalidate();
            }
            else
            {
                List<AreaMapComponent> pages = GetSynopticPages();

                foreach (AreaMapComponent page in pages)
                {
                    areaG = page.FindGroup(arg);

                    if (areaG != null)
                    {
                        page.FreezeGroup(areaG.Name);
                    }
                }
            }
        }

        private void ResetArea(string arg)
        {
            Area area = FindArea(arg);
            if (area != null)
            {
                Reset(area);
            }
        }

        #endregion

        void AreaMap_AreaMouseMove(object sender, AreaEventArgs data)
        {
        }

        public Area FindArea(string inName)
        {
            List<Area> areas = GetAllAreas();

            foreach (Area area in areas)
            {
                if (area.Name == inName)
                {
                    return area;
                }
            }

            return null;
        }

        public List<Area> FindAreaFromMetaData(string item)
        {
            List<Area> foundAreas = new List<Area>();
            List<Area> areas = GetAllAreas();

            foreach (Area area in areas)
            {
                if (area.MetaData == item)
                {
                    foundAreas.Add(area);
                }
            }

            return foundAreas;
        }

        private string Remove(string metaData, string funcName)
        {
            int index = metaData.IndexOf(funcName);
            int endIndex = metaData.IndexOf(")", index);

            return metaData.Remove(index, endIndex + 1 - index);
        }

        private string GetArgument(string metaData, string funcName)
        {
            int index = metaData.IndexOf(funcName);
            int endIndex = metaData.IndexOf(")", index);

            return metaData.Substring(index + funcName.Length + 1, endIndex - (index + funcName.Length) - 1).Trim('"');
        }

        private string[] GetArguments(string metaData, string funcName)
        {
            string arg = GetArgument(metaData, funcName);
            string[] args = arg.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < args.Length; i++)
            {
                args[i] = ReplaceVariables(args[i].Trim());
            }

            return args;
        }

        private void SelectwithModifiers(Area area)
        {
            switch (ModifierKeys)
            {
                case Keys.Shift:
                    AddToSelection(area);
                    break;
                case Keys.Control:
                    ToggleSelection(area);
                    break;
                case Keys.Alt:
                    RemoveFromSelection(area);
                    break;
                default:
                    Select(area);
                    break;
            }
        }

        private void SelectWithModifiers(List<Area> selected)
        {
            switch (ModifierKeys)
            {
                case Keys.Shift:
                    AddToSelection(selected);
                    break;
                case Keys.Control:
                    ToggleSelection(selected);
                    break;
                case Keys.Alt:
                    RemoveFromSelection(selected);
                    break;
                default:
                    Select(selected);
                    break;
            }
        }

        #region SHAPES CREATION

        public Area AddCircle(Vector2 inCenter, float inRadius)
        {
            Circle circle = new Circle(inCenter, inRadius);
            Area area = new Area(circle, AreaBehavior.SelectOnly);
            AddArea(area);
            Invalidate();
            return area;
        }

        public Area AddRectangle(Vector2 inCenter, float inWidth, float inHeight)
        {
            GeometryLib.Rectangle rectangle = new GeometryLib.Rectangle(inCenter, inWidth, inHeight);
            Area area = new Area(rectangle, AreaBehavior.SelectOnly);
            AddArea(area);
            Invalidate();
            return area;
        }

        public Area AddPolygon(params Vector2[] inPoints)
        {
            Polygon2 poly = new Polygon2(inPoints);
            Area area = new Area(poly, AreaBehavior.SelectOnly);
            AddArea(area);
            Invalidate();
            return area;
        }

        public Area AddShape(Shape2 shape)
        {
            Area area = new Area(shape, AreaBehavior.SelectOnly);
            AddArea(area);
            Invalidate();
            return area;
        }

        public void AddArea(Area area)
        {
            area.Map = CurrentAreaMap;
            area.ImgPath = area.ImgPath;
            CurrentAreaMap.Areas.Add(area);
            if (IsEditing)
            {
                Edit(area);
            }
        }

        #endregion

        #region MOUSE EVENTS

        private void AreaMap_MouseMove(object sender, MouseEventArgs e)
        {
            //Is Transforming
            if (_isTransforming)
            {
                if (_isRotating || _isScaling)
                {
                    if (_isRotating)
                    {
                        Vector2 oldPos = (_dragPoint - _offset) * (1 / _scaling);
                        Vector2 newPos = (new Vector2(e.X, e.Y) - _offset) * (1 / _scaling);
                        Vector2 oldRot = (oldPos - Center).Normalize();
                        Vector2 newRot = (newPos - Center).Normalize();

                        double rotation = Math.Atan2(newRot.Y, newRot.X) - Math.Atan2(oldRot.Y, oldRot.X);

                        _tranform.Rotate(rotation, _tranform.Center);
                        foreach (Area area in _tranforming)
                        {
                            area.Shape.Rotate(rotation, _tranform.Center);
                        }
                    }
                    else
                    {
                        Vector2 offset = (new Vector2(e.X, e.Y) - _dragPoint) * (1 / _scaling);
                        //Modulate move
                        if (ModifierKeys == Keys.Control)
                        {
                            offset *= .25f;
                        }

                        _tranform.Scale(new Vector2(1f + offset.X * .05f, 1f - offset.Y * .05f));
                        foreach (Area area in _tranforming)
                        {
                            area.Scale(new Vector2(1f + offset.X * .05f, 1f - offset.Y * .05f), _tranform.Center);
                        }
                    }

                    Invalidate();

                    _dragPoint.X = e.X;
                    _dragPoint.Y = e.Y;

                    return;
                }
                else
                {
                    if (_tranform.Contains((GeoConverter.Convert(e.Location) - _offset) * 1f/_scaling))
                    {
                        Cursor = Cursors.SizeAll;
                    }
                    else
                    {
                        Cursor = Cursors.NoMove2D;
                    }
                }
                return;
            }

            //Is Dragging selection
            if ((_selectPoint.X != -1 || _selectStroke.Points.Count > 0) && e.Button == MouseButtons.Left)
            {
                if (_selectionMode == SelectionModes.Rectangle)
                {
                    _draggedSelection.Center = new Vector2(Math.Min(_selectPoint.X, e.X), Math.Min(_selectPoint.Y, e.Y));
                    _draggedSelection.Width = Math.Abs(e.X - _selectPoint.X);
                    _draggedSelection.Height = Math.Abs(e.Y - _selectPoint.Y);
                    _draggedSelection.Offset(new Vector2(_draggedSelection.Width / 2f, _draggedSelection.Height / 2f));
                    
                    Invalidate();
                }
                else
                {
                    if (_selectStroke.AddPoint(GeoConverter.Convert(e.Location), 2f))
                    {
                        Invalidate();
                    }
                }

                
            }
            else
            {
                //Moving in a shape ?
                Area currentArea = null;
                foreach (Area area in CurrentAreaMap.Areas)
                {
                    if (area.IsVisible && area.IsActive && area.Contains(GeoConverter.Convert(e.Location, _offset, _scaling)))
                    {
                        currentArea = area;
                        break;
                    }
                }

                if (_hoverArea != null) // we were hovering a shape
                {
                    if (currentArea == null) //we left the shape => LEAVE
                    {
                        OnAreaMouseLeave(this, new AreaEventArgs(_hoverArea, e));
                    }
                    else
                    {
                        if (currentArea != _hoverArea)//we changed the shape => LEAVE & ENTER
                        {
                            OnAreaMouseLeave(this, new AreaEventArgs(_hoverArea, e));
                            OnAreaMouseEnter(this, new AreaEventArgs(currentArea, e));
                        }
                    }

                    if (_hoverArea.IsMovable && !_hoverArea.IsEditing) { OnAreaMouseMove(this, new AreaEventArgs(_hoverArea, e)); };
                }
                else // we were moving in vacuum
                {
                    if (currentArea != null) //we enter a shape => ENTER
                    {
                        OnAreaMouseEnter(this, new AreaEventArgs(currentArea, e));
                    }
                }

                _hoverArea = currentArea;

                if (_selectPoint.X != -1)
                {
                    _dragPoint = _selectPoint;
                }

                //Editing && Shape moving
                if (_dragPoint.X != -1)
                {
                    Vector2 oldPos = new Vector2(_dragPoint.X, _dragPoint.Y);
                    Vector2 offset = (new Vector2(e.X, e.Y) - oldPos) * (1 / _scaling);
                    //Modulate move
                    if (ModifierKeys == Keys.Control)
                    {
                        offset *= .25f;
                    }

                    bool modified = false;

                    if (!_isDocked && e.Button == MouseButtons.Middle)
                    {
                        //Pan
                        _offset += offset*_scaling;
                        modified = true;
                    }
                    else if (_dragged != null && e.Button == MouseButtons.Left)
                    {
                        List<Area> moved = new List<Area>();
                        if (!_dragged.IsSelected)
                        {
                            SelectwithModifiers(_dragged);
                        }

                        foreach (Area area in CurrentAreaMap.Areas)
                        {
                            if (area.IsSelected && !moved.Contains(area))
                            {
                                if (area.IsEditing || (area.IsMovable && !area.IsSubComponent))
                                {
                                    float oldX = 0f;
                                    float oldY = 0f;

                                    if (!area.IsSubComponent && area.IsMovable && !area.IsEditing)
                                    {
                                        oldX = area.ValueX;
                                        oldY = area.ValueY;
                                    }

                                    area.Offset(offset);
                                    if (area.IsEditing)
                                    {
                                        area.SubComponents[0].Center = area.Center;
                                        foreach (Area subArea in area.SubComponents)
                                        {
                                            if (subArea.Index >= 0)
                                            {
                                                subArea.Center = area.Points[subArea.Index];
                                            }
                                        }
                                    }
                                    else if (!area.IsSubComponent)
                                    {
                                        area.ClampCenter(area.LocalMinX, area.LocalMaxX, area.LocalMinY, area.LocalMaxY);
                                    }

                                    if (!area.IsSubComponent && area.IsMovable && !area.IsEditing)
                                    {
                                        if (!_unValidatedValues.Contains(area))
                                        {
                                            _unValidatedValues.Add(area);
                                        }

                                        OnValueChanged(this, new ValueChangedEventArgs(oldX, oldY, area, false));
                                    }

                                    moved.Add(area);
                                    modified = true;
                                    _isDragging = true;
                                }
                                else
                                {
                                    if (area.IsSubComponent && !moved.Contains(area.Parent))
                                    {
                                        if (area.Index >= 0)
                                        {
                                            area.Offset(offset);
                                            area.Parent.Points[area.Index] = area.Center;
                                            area.Parent.SubComponents[0].Center = area.Parent.Center;
                                            modified = true;
                                        }
                                        else
                                        {
                                            //Center => Scaling
                                            if (offset.X != 0f || offset.Y != 0f)
                                            {
                                                //Homothetic
                                                if (ModifierKeys == Keys.Shift)
                                                {
                                                    offset.X = offset.Y = offset.X + offset.Y;
                                                }

                                                area.Parent.Scale(new Vector2(1f + offset.X * .05f, 1f - offset.Y * .05f));

                                                foreach (Area subArea in area.Parent.SubComponents)
                                                {
                                                    if (subArea.Index >= 0)
                                                    {
                                                        subArea.Center = area.Parent.Points[subArea.Index];
                                                    }
                                                }
                                                modified = true;
                                            }
                                        }
                                    }

                                    if (area.IsSelectable || area.IsMovable)
                                    {
                                        _isDragging = true;
                                    }
                                }
                            }
                        }
                    }

                    _dragPoint.X = e.X;
                    _dragPoint.Y = e.Y;

                    if (modified)
                    {
                        Invalidate();
                    }
                }
            }
        }

        private void AreaMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (_isTransforming)
            {
                if (_tranform.Contains((GeoConverter.Convert(e.Location) - _offset) * 1f / _scaling))
                {
                    _isScaling = true;
                }
                else
                {
                    _isRotating = true;
                }

                _dragPoint = new Vector2(e.X, e.Y);

                return;
            }

            if (_hoverArea != null) // we were hovering a shape
            {
                OnAreaMouseDown(this, new AreaEventArgs(_hoverArea, e));
            }
            else //ready for selection
            {
                if (_selectionMode == SelectionModes.Rectangle)
                {
                    _selectPoint = new Vector2(e.X, e.Y);
                }
                else
                {
                    _selectStroke.AddPoint(GeoConverter.Convert(e.Location), 0f);
                }
            }
        }

        private void AreaMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isTransforming)
            {
                _isRotating = false;
                _isScaling = false;
                _dragPoint = new Vector2(-1, -1);
                return;
            }

            List<Area> selected = new List<Area>();

            if (_draggedSelection.Width > 0 || _draggedSelection.Height > 0)
            {
                GeometryLib.Rectangle scaledSelection = (GeometryLib.Rectangle)_draggedSelection.Clone();
                scaledSelection.Scale(new Vector2(1f / _scaling, 1f / _scaling)); 
                scaledSelection.Center += (Offset * -1);
                scaledSelection.Center *= 1f / _scaling;

                foreach (Area area in CurrentAreaMap.Areas)
                {
                    if ((IsEditing || (area.IsSelectable && area.IsVisible)) && scaledSelection.Touch(area.Shape))
                    {
                        selected.Add(area);
                    }
                }
            }
            else if (_selectStroke.Points.Count > 2)
            {
                GeometryLib.Stroke2 scaledStroke = (GeometryLib.Stroke2)_selectStroke.Clone();
                scaledStroke.Center += (Offset * -1);
                scaledStroke.Scale(new Vector2(1f / _scaling, 1f / _scaling));
                scaledStroke.Center *= 1f / _scaling;
                int step = 1;
                if(scaledStroke.Points.Count > 20)
                {
                    step = scaledStroke.Points.Count / 20;
                }

                for (var i = 0; i < scaledStroke.Points.Count; i += step)
                {
                    Area point = new Area(new Circle(), AreaBehavior.SelectOnly);
                    point.Center = scaledStroke.Points[i];
                    AddArea(point);
                }

                foreach (Area area in CurrentAreaMap.Areas)
                {
                    if ((IsEditing || (area.IsSelectable && area.IsVisible)) && scaledStroke.Touch(area.Shape))
                    {
                        selected.Add(area);
                    }
                }
            }

            if (!_isDragging)
            {
                if (selected.Count > 0)
                {
                    SelectWithModifiers(selected);
                }

                if (_hoverArea != null) // we were hovering a shape
                {
                    OnAreaMouseUp(this, new AreaEventArgs(_hoverArea, e));
                }
                else if (selected.Count == 0 && ModifierKeys == Keys.None)
                {
                    DeselectAll();
                }
            }

            _isDragging = false;
            _dragPoint = new Vector2(-1, -1);
            _dragged = null;
            _selectPoint = new Vector2(-1, -1);
            _selectStroke = new Stroke2();
            _draggedSelection = new GeometryLib.Rectangle();

            foreach (Area area in _unValidatedValues)
            {
                OnValueChanged(this, new ValueChangedEventArgs(area.ValueX, area.ValueY, area, true));
            }
            _unValidatedValues.Clear();


            Invalidate();
        }

        void AreaMapComponent_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!_isDocked)
            {
                float zooming = (float)Math.Min(10, Math.Max(.1, _scaling * (1 + e.Delta * .002f)));
                ScaleFromPoint(zooming, GeoConverter.Convert(e.Location, Offset, _scaling));
                OnScalingChanged(this, new EventArgs());
            }
        }

        #endregion

        private List<Area> GetAllAreas()
        {
            List<Area> allAreas = new List<Area>();

            foreach (AreaMap map in _maps)
            {
                allAreas.AddRange(map.Areas);
            }

            return allAreas;
        }

        #region SELECTION

        public void ToggleSelection(Area inArea)
        {
            if (inArea.IsSelected)
            {
                RemoveFromSelection(inArea);
            }
            else
            {
                AddToSelection(inArea);
            }
        }

        public void AddToSelection(Area inArea)
        {
            if (!inArea.IsSelected)
            {
                SelectionChangedEventArgs args = new SelectionChangedEventArgs(_selection, SelectionChangedModes.SelectionAdded, new List<Area> { inArea });
                if (inArea.IsSelectable && !inArea.IsEditionRelated) { OnSelectionChanged(this, args); }

                if (!args.Cancel)
                {
                    inArea.IsSelected = true;
                    if (inArea.IsSelectable) { _selection.Add(inArea); }
                    Invalidate();
                }
            }
        }

        public void RemoveFromSelection(Area inArea)
        {
            if (inArea.IsSelected)
            {
                SelectionChangedEventArgs args = new SelectionChangedEventArgs(_selection, SelectionChangedModes.SelectionRemoved, new List<Area> { inArea });
                if (inArea.IsSelectable && !inArea.IsEditionRelated) { OnSelectionChanged(this, args); }

                if (!args.Cancel)
                {
                    inArea.IsSelected = false;
                    if (inArea.IsSelectable) { _selection.Remove(inArea); }
                    Invalidate();
                }
            }
        }

        public void Select(Area inArea)
        {
            if (!inArea.IsSelected || Selection.Count > 1)
            {
                SelectionChangedEventArgs args = new SelectionChangedEventArgs(_selection, SelectionChangedModes.Unknown, new List<Area> { inArea });
                if (inArea.IsSelectable && !inArea.IsEditionRelated) { OnSelectionChanged(this, args); }

                if (!args.Cancel)
                {
                    if (inArea.IsSelectable)
                    {
                        foreach (AreaMap map in _maps)
                        {
                            foreach (Area area in map.Areas)
                            {
                                area.IsSelected = false;
                            }
                        }
                        _selection.Clear();
                    }

                    inArea.IsSelected = true;
                    if (inArea.IsSelectable) { _selection.Add(inArea); }
                    Invalidate();
                }
            }
        }

        public void Select(List<Area> selected)
        {
            //Equals selection ?
            bool different = false;
            if (_selection.Count == selected.Count)
            {
                foreach (Area area in selected)
                {
                    if (!_selection.Contains(area))
                    {
                        different = true;
                        break;
                    }
                }
            }
            else
            {
                different = true;
            }
            if (!different)
            {
                Invalidate();
                return;
            }

            List<Area> filtered = new List<Area>();
            List<Area> selectable = new List<Area>();
            foreach (Area area in selected)
            {
                filtered.Add(area);

                if (area.IsSelectable && !area.IsEditionRelated)
                {
                    selectable.Add(area);
                }
            }

            SelectionChangedEventArgs args = new SelectionChangedEventArgs(_selection, SelectionChangedModes.Unknown, selectable);
            if (selectable.Count > 0)
            {
                OnSelectionChanged(this, args);
            }

            if (!args.Cancel)
            {
                foreach (AreaMap map in _maps)
                {
                    foreach (Area area in map.Areas)
                    {
                        area.IsSelected = false;
                    }
                }
                _selection.Clear();

                foreach (Area area in filtered)
                {
                    area.IsSelected = true;
                    _selection.Add(area);
                }

                Invalidate();
            }
        }

        public void RemoveFromSelection(List<Area> selected)
        {
            List<Area> filtered = new List<Area>();
            List<Area> selectable = new List<Area>();
            foreach (Area area in selected)
            {
                if (area.IsSelected)
                {
                    filtered.Add(area);

                    if (area.IsSelectable && !area.IsEditionRelated)
                    {
                        selectable.Add(area);
                    }
                }
            }

            SelectionChangedEventArgs args = new SelectionChangedEventArgs(_selection, SelectionChangedModes.SelectionRemoved, selectable);
            OnSelectionChanged(this, args);

            if (!args.Cancel)
            {
                foreach(Area area in filtered)
                {
                    area.IsSelected = false;
                    _selection.Remove(area);
                }
                Invalidate();
            }
        }

        public void ToggleSelection(List<Area> selected)
        {
            List<Area> removed = new List<Area>();
            List<Area> added = new List<Area>();

            foreach(Area area in selected)
            {
                if(area.IsSelected)
                {
                    removed.Add(area);
                }
                else
                {
                    added.Add(area);
                }
            }

            RemoveFromSelection(removed);
            AddToSelection(added);
        }

        public void AddToSelection(List<Area> selected)
        {
            AddToSelection(selected, true);
        }

        public void AddToSelection(List<Area> selected, bool inRaiseEvents)
        {
            List<Area> filtered = new List<Area>();
            List<Area> selectable = new List<Area>();
            foreach (Area area in selected)
            {
                if (!area.IsSelected)
                {
                    filtered.Add(area);

                    if(area.IsSelectable && !area.IsEditionRelated)
                    {
                        selectable.Add(area);
                    }
                }
            }


            SelectionChangedEventArgs args = new SelectionChangedEventArgs(_selection, SelectionChangedModes.SelectionAdded, selectable);
            if (selectable.Count > 0 && inRaiseEvents)
            {
                OnSelectionChanged(this, args);
            }

            if (!args.Cancel)
            {
                foreach (Area area in filtered)
                {
                    area.IsSelected = true;
                    _selection.Add(area);
                }
                Invalidate();
            }
        }

        public void DeselectAll()
        {
            DeselectAll(true);
        }

        public void DeselectAll(bool inRaiseEvents)
        {
            SelectionChangedEventArgs args = new SelectionChangedEventArgs(_selection, SelectionChangedModes.DeselectAll, new List<Area>());
            if (!IsEditing && inRaiseEvents) { OnSelectionChanged(this, args); }
            if (!args.Cancel)
            {
                foreach (AreaMap map in _maps)
                {
                    foreach (Area area in map.Areas)
                    {
                        area.IsSelected = false;
                    }
                }
                _selection.Clear();

                Invalidate();
            }
        }

        private void SelectAll()
        {
            List<Area> allAreas = new List<Area>();
            foreach (AreaMap map in _maps)
            {
                foreach (Area area in map.Areas)
                {
                    allAreas.Add(area);
                }
            }

            SelectionChangedEventArgs args = new SelectionChangedEventArgs(_selection, SelectionChangedModes.Unknown, allAreas);
            if (!IsEditing) { OnSelectionChanged(this, args); }
            if (!args.Cancel)
            {
                foreach (Area area in allAreas)
                {
                    area.IsSelected = true;
                }
                _selection = allAreas;

                Invalidate();
            }
        }

        #endregion

        public List<Area> Selection
        {
            get { return _selection; }
        }

        public Vector2 Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

        public void ScaleFromPoint(float inScale, Vector2 inPoint)
        {
            Vector2 offset = inPoint * (inScale / _scaling) - inPoint;
            _offset -= offset * _scaling;
            _scaling = inScale;
            Invalidate();
        }

        public Size Dimensions
        {
            get { return _dimensions; }
        }

        public float Ratio
        {
            get {return ((float)_dimensions.Width / (float)_dimensions.Height); }
        }

        public float Scaling
        {
            get { return _scaling; }
            set { _scaling = value; Invalidate(); }
        }

        public Vector2 Center
        {
            get { return new Vector2((float)CenterX, (float)CenterY); }
            set
            {
                CenterX = (int)value.X;
                CenterY = (int)value.Y;
            }
        }

        public bool GridOnTop
        {
            get { return _gridOnTop; }
            set { _gridOnTop = value; Invalidate(); }
        }

        public bool ShowGrid
        {
            get { return _showGrid; }
            set { _showGrid = value; Invalidate(); }
        }

        public bool ShowCenter
        {
            get { return _showCenter; }
            set { _showCenter = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!_isDocked && BackgroundImage != null)
            {
                e.Graphics.Clear(BackColor);
                e.Graphics.DrawImage(BackgroundImage, Offset.X,Offset.Y, _dimensions.Width * _scaling, _dimensions.Height * _scaling );
            }

            //Grid under
            if (_showGrid && !_gridOnTop)
            {
                DrawGrid(e.Graphics);
            }

            //Transform
            if (_isTransforming)
            {
                e.Graphics.FillPolygon(_selectBrush,  new PointF[] {GeoConverter.Convert(_offset + (_tranform.Points[0] * _scaling)),
                GeoConverter.Convert(_offset + (_tranform.Points[1] * _scaling)),
                GeoConverter.Convert(_offset + (_tranform.Points[2] * _scaling)),
                GeoConverter.Convert(_offset + (_tranform.Points[3] * _scaling))});
            }

            //Frame
            e.Graphics.DrawRectangle(Pens.Black, _offset.X, _offset.Y, _dimensions.Width * _scaling, _dimensions.Height * _scaling);

            //AreaMaps
            for (int i = CurrentAreaMap.Areas.Count; i > 0; i--)
            {
                CurrentAreaMap.Areas[i - 1].Draw(e.Graphics, _offset, _scaling);
            }

            //Selection
            if (_draggedSelection.Width > 0 || _draggedSelection.Height > 0)
            {
                Vector2 corner = _draggedSelection.Corner;
                e.Graphics.FillRectangle(_selectBrush, corner.X, corner.Y, _draggedSelection.Width, _draggedSelection.Height);
                e.Graphics.DrawRectangle(Pens.Black, corner.X, corner.Y, _draggedSelection.Width, _draggedSelection.Height);
            }
            if (_selectStroke.Points.Count > 1)
            {
                for (int i = 1; i < _selectStroke.Points.Count; i++)
                {
                    e.Graphics.DrawLine(_boldPen, GeoConverter.Convert(_selectStroke.Points[i - 1]), GeoConverter.Convert(_selectStroke.Points[i]));
                    e.Graphics.DrawLine(_selectPen, GeoConverter.Convert(_selectStroke.Points[i - 1]), GeoConverter.Convert(_selectStroke.Points[i]));
                }
            }

            //Grid on top
            if (_showGrid && _gridOnTop)
            {
                DrawGrid(e.Graphics);
            }

            //Center
            if (_showCenter)
            {
                e.Graphics.DrawLine(_boldPen, new PointF(_offset.X + CenterX * _scaling, _offset.Y), new PointF(_offset.X + CenterX * _scaling, _dimensions.Height * _scaling + _offset.Y));
                e.Graphics.DrawLine(_boldPen, new PointF(_offset.X, _offset.Y + CenterY * _scaling), new PointF(_dimensions.Width * _scaling + _offset.X, _offset.Y + CenterY * _scaling));
            }
        }

        private void DrawGrid(Graphics graphics)
        {
            float xSpacing = 50f * _scaling;
            float ySpacing = 50f * _scaling;

            for (float i = xSpacing; i < _dimensions.Width * _scaling; i += xSpacing)
            {
                graphics.DrawLine(Pens.Gray, new Point((int)(i + _offset.X), (int)_offset.Y), new Point((int)(i + _offset.X), (int)(_dimensions.Height * _scaling + _offset.Y)));
            }

            for (float i = ySpacing; i < _dimensions.Height * _scaling; i += ySpacing)
            {
                graphics.DrawLine(Pens.Gray, new Point((int)_offset.X, (int)(i + _offset.Y)), new Point((int)(_dimensions.Width * _scaling + _offset.X), (int)(i + _offset.Y)));
            }
        }

        private void AreaMap_BackgroundImageChanged(object sender, EventArgs e)
        {
            if (BackgroundImage != null)
            {
                _dimensions = BackgroundImage.Size;
                if (_isDocked)
                {
                    RefreshOffset();
                }
            }
        }

        private void AreaMap_SizeChanged(object sender, EventArgs e)
        {
            if (_isDocked)
            {
                RefreshOffset();
            }
        }

        private void RefreshOffset()
        {
            float realRatio = (float)Width / (float)Height;

            if (realRatio == Ratio)//IsoRatio (unlikely)
            {
                _offset = Vector2.Null;
                _scaling =  (float)Width / (float)_dimensions.Width;
            }
            else
            {
                if (realRatio > Ratio) // Larger than necessary
                {
                    _scaling =  (float)Height / (float)_dimensions.Height;

                    _offset.X = (Width - (_dimensions.Width * _scaling)) / 2f;
                    _offset.Y = 0f;
                }
                else // Taller than necessary
                {
                    _scaling = (float)Width / (float)_dimensions.Width;

                    _offset.Y = (Height - (_dimensions.Height * _scaling)) / 2f;
                    _offset.X = 0f;

                }
            }
        }

        public void SetPicture(string inPath)
        {
            if (!string.IsNullOrEmpty(inPath))
            {
                string absPath = CurrentAreaMap.GetAbsolutePath(inPath);
                if (File.Exists(absPath))
                {
                    CurrentAreaMap.ImagePath = absPath;
                    FileStream stream = new FileStream(absPath, FileMode.Open, FileAccess.Read);
                    BackgroundImage = Image.FromStream(stream);
                    stream.Close();
                }
            }
        }

        public void Clear()
        {
            _maps.Clear();
            _maps.Add(new AreaMap());
            BackgroundImage = null;
        }

        #region SAVING LOADING

        internal string Save(string inPath)
        {
            return Save(CurrentAreaMap, inPath, false);
        }

        public string Save(AreaMap inAreaMap, string inPath, bool inAll)
        {
            string status = "";
            StreamWriter writer = null;

            if (inAreaMap.Path != "")
            {
                inAreaMap.SetPathsRelative();

                string oldFolder = PathHelper.GetFolderPath(inAreaMap.Path);
                string newFolder = PathHelper.GetFolderPath(inPath);
                //Saving in another folder
                if (oldFolder != newFolder)
                {
                    rslt = Migrate(inAreaMap, newFolder, rslt);
                }
            }

            DumpGroups();

            inAreaMap.Path = inPath;
            XmlSerializer ser = new XmlSerializer(typeof(AreaMap));

            try
            {
                writer = new StreamWriter(inPath, false);
                ser.Serialize(writer, inAreaMap);
                /*
                if (inAll)
                {
                    if (CurrentAreaMap.AssociatedPaths.Count > 0)
                    {
                        //Associated
                        foreach (string path in CurrentAreaMap.AssociatedPaths)
                        {
                            if (path != CurrentAreaMap.Path)
                            {
                                AreaMap map = FindFromPath(path);
                                if(map != null)
                                {
                                    Save(map, path, false);
                                }
                            }
                        }
                    }
                }
                */
                //Groups
                /*
                if (Groups.Count > 0)
                {
                    SaveGroups(inPath.Replace(Path.GetFileName(inPath), "Groups.xml"));
                }
                 */
            }
            catch (Exception e) { status = e.Message; }

            if (writer != null)
            {
                writer.Close();
            }

            return status;
        }

        internal string ExportAsPicker(AreaMap inAreaMap, string inPath, bool inAll, double alphaBoost)
        {
            string status = "";
            StreamWriter writer = null;

            string skeleton = Properties.Resources.skeleton;
            StringBuilder pickerBuilder = new StringBuilder();

            List<string> strItems = new List<string>();
            string itemsPostChunk = string.Empty;
            string controlsSkeleton = "'controls': [$METADATA]";
            string scriptSkeleton = "'action_mode': True, 'action_script': u'$METADATA'";

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            Vector2 offset = Vector2.Null;

            string absPath = inAreaMap.GetAbsolutePath(inAreaMap.ImagePath);
            if (File.Exists(absPath))
            {
                FileStream stream = new FileStream(absPath, FileMode.Open, FileAccess.Read);
                Image backgroundImage = Image.FromStream(stream);
                stream.Close();

                offset = new Vector2(backgroundImage.Width / 2.0f, backgroundImage.Height / 2.0f);

                itemsPostChunk = ", 'background': u'" + absPath.Replace("\\", "/") + "'";
            }
            else
            {
                //Re-center
                float minX = float.MaxValue;
                float maxX = float.MinValue;
                float minY = float.MaxValue;
                float maxY = float.MinValue;

                foreach (Area area in inAreaMap.Areas)
                {
                    Vector2 upperLeft = area.Corner;
                    Vector2 upperRight = area.Shape.UpperRightCorner;
                    Vector2 lowerLeft = area.Shape.LowerLeftCorner;
                    Vector2 lowerRight = area.Shape.LowerRightCorner;

                    float shapeminX = Math.Min(upperLeft.X, lowerLeft.X);
                    float shapemaxX = Math.Max(upperRight.X, lowerRight.X);
                    float shapeminY = Math.Min(upperLeft.Y, upperRight.Y);
                    float shapemaxY = Math.Max(lowerLeft.Y, lowerRight.Y);

                    if (shapeminX < minX)
                    {
                        minX = shapeminX;
                    }
                    if (shapemaxX > maxX)
                    {
                        maxX = shapemaxX;
                    }

                    if (shapeminY < minY)
                    {
                        minY = shapeminY;
                    }
                    if (shapemaxY > maxY)
                    {
                        maxY = shapemaxY;
                    }
                }

                offset = new Vector2((minX + maxX) / 2.0f, (minY + maxY) / 2.0f);
            }
            try
            {
                writer = new StreamWriter(inPath, false);
                Vector2 center = Vector2.Null;
                List<Area> areas = new List<Area>(inAreaMap.Areas);
                areas.Reverse();

                foreach (Area area in areas)
                {
                    if (!area.IsVisible && !area.IsActive)
                        continue;

                    List<string> handles = new List<string>();
                    switch(area.Type)
                    {
                        case ConvertibleShapeType.Circle:
                            handles.Add(string.Format(customCulture, "[{0},{1}]", 0f, 0f));
                            handles.Add(string.Format(customCulture, "[{0},{1}]", area.Radius, 0f));
                            break;
                        case ConvertibleShapeType.Rectangle:
                            Rectangle rectangleShape = area.Shape as Rectangle;
                            center = area.Center;
                            handles.Add(string.Format(customCulture, "[{0},{1}]", area.Corner.X - center.X, -(area.Corner.Y - center.Y)));
                            handles.Add(string.Format(customCulture, "[{0},{1}]", area.Corner.X + rectangleShape.Width - center.X, -(area.Corner.Y - center.Y)));
                            handles.Add(string.Format(customCulture, "[{0},{1}]", area.Corner.X + rectangleShape.Width - center.X, -(area.Corner.Y + rectangleShape.Height - center.Y)));
                            handles.Add(string.Format(customCulture, "[{0},{1}]", area.Corner.X - center.X, -(area.Corner.Y + rectangleShape.Height - center.Y)));
                            break;
                        default:
                            center = area.Center;
                            foreach (Vector2 point in area.Points)
                            {
                                handles.Add(string.Format(customCulture, "[{0},{1}]", point.X - center.X, -(point.Y - center.Y)));
                            }
                            break;
                    }

                    string metaData = area.MetaData;
                    AreaBehavior behaviour = area.Behavior;

                    if (behaviour == AreaBehavior.Button)
                    {
                        //Check if we have some internal function
                        if (metaData.Contains(Area.FUNCTIONTAG))
                        {
                            string arg = "";
                            string paramList = "";

                            while (metaData.Contains(Area.FUNCTIONTAG + "SelectGroup("))
                            {
                                behaviour = AreaBehavior.SelectOnly;

                                arg = GetArgument(metaData, Area.FUNCTIONTAG + "SelectGroup");
                                //SelectGroup(arg);
                                AreaGroup areaG = FindGroup(arg);
                                if (areaG == null)
                                {
                                    List<AreaMapComponent> pages = GetSynopticPages();
                                    foreach (AreaMapComponent page in pages)
                                    {
                                        areaG = page.FindGroup(arg);
                                        if (areaG != null)
                                        {
                                            break;
                                        }
                                    }
                                }
                                if (areaG != null)
                                {
                                    List<string> areaNames = new List<string>();
                                    foreach (Area areaFromG in areaG.Areas)
                                    {
                                        areaNames.Add(string.Format("u'{0}'", areaFromG.MetaData));
                                    }
                                    metaData = TypesHelper.Join(areaNames);
                                }
                                else
                                {
                                    metaData = Remove(metaData, Area.FUNCTIONTAG + "SelectGroup");
                                }
                                /*
                                if (metaData.Contains(Area.FUNCTIONTAG + "ShowArea("))
                                {
                                    arg = GetArgument(metaData, Area.FUNCTIONTAG + "ShowArea");
                                    //ShowArea(arg);
                                    metaData = Remove(metaData, Area.FUNCTIONTAG + "ShowArea");
                                }
                                else if (metaData.Contains(Area.FUNCTIONTAG + "HideArea("))
                                {
                                    arg = GetArgument(metaData, Area.FUNCTIONTAG + "HideArea");
                                    //HideArea(arg);
                                    metaData = Remove(metaData, Area.FUNCTIONTAG + "HideArea");
                                }
                                else if (metaData.Contains(Area.FUNCTIONTAG + "ToggleArea("))
                                {
                                    arg = GetArgument(metaData, Area.FUNCTIONTAG + "ToggleArea");
                                    //ToggleGroup(arg);
                                    metaData = Remove(metaData, Area.FUNCTIONTAG + "ToggleArea");
                                }
                                else if (metaData.Contains(Area.FUNCTIONTAG + "ShowGroup("))
                                {
                                    arg = GetArgument(metaData, Area.FUNCTIONTAG + "ShowGroup");
                                    //ShowGroup(arg);
                                    metaData = Remove(metaData, Area.FUNCTIONTAG + "ShowGroup");
                                }
                                else if (metaData.Contains(Area.FUNCTIONTAG + "HideGroup("))
                                {
                                    arg = GetArgument(metaData, Area.FUNCTIONTAG + "HideGroup");
                                    //HideGroup(arg);
                                    metaData = Remove(metaData, Area.FUNCTIONTAG + "HideGroup");
                                }
                                else if (metaData.Contains(Area.FUNCTIONTAG + "ToggleGroup("))
                                {
                                    arg = GetArgument(metaData, Area.FUNCTIONTAG + "ToggleGroup");
                                    //ToggleGroup(arg);
                                    metaData = Remove(metaData, Area.FUNCTIONTAG + "ToggleGroup");
                                }
                                else if (metaData.Contains(Area.FUNCTIONTAG + "FreezeGroup("))
                                {
                                    arg = GetArgument(metaData, Area.FUNCTIONTAG + "FreezeGroup");
                                    //FreezeGroup(arg);
                                    metaData = Remove(metaData, Area.FUNCTIONTAG + "FreezeGroup");
                                }
                                else if (metaData.Contains(Area.FUNCTIONTAG + "UnfreezeGroup("))
                                {
                                    arg = GetArgument(metaData, Area.FUNCTIONTAG + "UnfreezeGroup");
                                    //UnfreezeGroup(arg);
                                    metaData = Remove(metaData, Area.FUNCTIONTAG + "UnfreezeGroup");
                                }
                                else if (metaData.Contains(Area.FUNCTIONTAG + "SelectArea("))
                                {
                                    arg = GetArgument(metaData, Area.FUNCTIONTAG + "SelectArea");
                                    //SelectArea(arg);
                                    metaData = Remove(metaData, Area.FUNCTIONTAG + "SelectArea");
                                }
                                else if (metaData.Contains(Area.FUNCTIONTAG + "SelectGroup("))
                                {
                                    behaviour = AreaBehavior.SelectOnly;

                                    arg = GetArgument(metaData, Area.FUNCTIONTAG + "SelectGroup");
                                    //SelectGroup(arg);
                                    AreaGroup areaG = FindGroup(arg);
                                    if (areaG == null)
                                    {
                                        List<AreaMapComponent> pages = GetSynopticPages();
                                        foreach (AreaMapComponent page in pages)
                                        {
                                            areaG = page.FindGroup(arg);
                                            if (areaG != null)
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    if (areaG != null)
                                    {
                                        List<string> areaNames = new List<string>();
                                        foreach (Area areaFromG in areaG.Areas)
                                        {
                                            areaNames.Add(string.Format("u'{0}'", areaFromG.MetaData));
                                        }
                                        metaData = TypesHelper.Join(areaNames);
                                    }
                                    else
                                    {
                                        metaData = Remove(metaData, Area.FUNCTIONTAG + "SelectGroup");
                                    }
                                }
                                else if (metaData.Contains(Area.FUNCTIONTAG + "SelectAll("))
                                {
                                    //SelectAll();
                                    metaData = Remove(metaData, Area.FUNCTIONTAG + "SelectAll");
                                }
                                else if (metaData.Contains(Area.FUNCTIONTAG + "DeselectAll("))
                                {
                                    //DeselectAll();
                                    metaData = Remove(metaData, Area.FUNCTIONTAG + "DeselectAll");
                                }
                                else if (metaData.Contains(Area.FUNCTIONTAG + "RefreshSelection("))
                                {
                                    //OnFunctionCalled(this, new FunctionEventArgs("RefreshSelection", new object[0]));
                                    metaData = Remove(metaData, Area.FUNCTIONTAG + "RefreshSelection");
                                }
                                else if (metaData.Contains(Area.FUNCTIONTAG + "ResetAll("))
                                {
                                    //ResetAll();
                                    metaData = Remove(metaData, Area.FUNCTIONTAG + "ResetAll");
                                }
                                else if (metaData.Contains(Area.FUNCTIONTAG + "ResetArea("))
                                {
                                    arg = GetArgument(metaData, Area.FUNCTIONTAG + "ResetArea");
                                    //ResetArea(arg);
                                    metaData = Remove(metaData, Area.FUNCTIONTAG + "ResetArea");
                                }
                                else if (metaData.Contains(Area.FUNCTIONTAG + "ResetCurrent("))
                                {
                                    //ResetCurrent();
                                    metaData = Remove(metaData, Area.FUNCTIONTAG + "ResetCurrent");
                                }
                                else if (metaData.Contains(Area.FUNCTIONTAG + "GetSelectedParams()"))
                                {
                                    paramList = GetSelectedParams();
                                    metaData = metaData.Replace(Area.FUNCTIONTAG + "GetSelectedParams()", "\"" + paramList + "\"");
                                }
                                else if (metaData.Contains(Area.FUNCTIONTAG + "GetCurrentParams()"))
                                {
                                    paramList = GetCurrentParams();
                                    metaData = metaData.Replace(Area.FUNCTIONTAG + "GetCurrentParams()", "\"" + paramList + "\"");
                                }
                                else if (metaData.Contains(Area.FUNCTIONTAG + "GetAllParams()"))
                                {
                                    paramList = GetAllParams();
                                    metaData = metaData.Replace(Area.FUNCTIONTAG + "GetAllParams()", "\"" + paramList + "\"");
                                }
                                /*
                                else
                                {
                                    metaData = metaData.Remove(metaData.IndexOf(Area.FUNCTIONTAG), 1);
                                }
                                */
                            }
                        }

                        metaData = metaData.Replace("SelfModelName", "__NAMESPACE__");
                    }
                    else
                    {
                        metaData = string.Format("u'{0}'", metaData);
                    }

                    string action = (behaviour == AreaBehavior.Button ? scriptSkeleton : controlsSkeleton).Replace("$METADATA", metaData.Replace("\r\n", "\\n"));

                    strItems.Add(string.Format(customCulture, "{{'color': ({0}, {1}, {2}, {3}), 'position': [{4}, {5}], 'handles': [{6}], {7}, 'text':'{8}', 'text_color':({9},{10},{11},{12}), 'text_size':{13}}}",
                        (int)area.AreaColor.R, (int)area.AreaColor.G, (int)area.AreaColor.B, (int)(Math.Min(255.0, (double)area.AreaColor.A * alphaBoost)), area.Center.X - offset.X, -(area.Center.Y - offset.Y), TypesHelper.Join(handles), action,
                        area.Text, area.TextColor.R, area.TextColor.G, area.TextColor.B, area.TextColor.A, area.TextFont.SizeInPoints / _scaling));
                }

                skeleton = skeleton.Replace("$TABNAME", inAreaMap.Name);
                skeleton = skeleton.Replace("$POSTCHUNK", itemsPostChunk);
                skeleton = skeleton.Replace("$ITEMS", TypesHelper.Join(strItems));

                writer.Write(skeleton);
            }
            catch (Exception e) { status = e.Message; }

            if (writer != null)
            {
                writer.Close();
            }

            return status;
        }

        private DialogResult Migrate(AreaMap inAreaMap, string newFolder, DialogResult rslt)
        {
            string oldFolder = PathHelper.GetFolderPath(inAreaMap.Path);
            List<string> pictures = GetRelativePictures(inAreaMap);

            if (pictures.Count > 0)
            {
                if (rslt == DialogResult.Ignore)
                {
                    rslt = MessageBox.Show("Your about to change the folder of your AreaMap. Pictures paths are stored relative to their AreaMap xml's path.\nDo you want to copy the files to the new folder ? (Choosing no will set all the paths in absolute).", "Changing folder", MessageBoxButtons.YesNoCancel);
                }

                if (rslt == DialogResult.Cancel)
                {
                    return rslt;
                }
                else if (rslt == DialogResult.No)
                {
                    inAreaMap.SetPathsAbsolute();
                }
                else if (rslt == DialogResult.Yes)
                {
                    string destPath = "";
                    foreach (string path in pictures)
                    {
                        if (path.Contains(oldFolder))
                        {
                            destPath = path.Replace(oldFolder, newFolder);
                            File.Copy(path, destPath, true);
                        }
                    }
                }
            }

            return rslt;
        }

        public static void Migrate(Area inArea, string newFolder)
        {
            string oldFolder = PathHelper.GetFolderPath(inArea.Map.Path);
            string absPath = inArea.Map.GetAbsolutePath(inArea.ImgPath);
            string newPath = absPath.Replace(oldFolder, newFolder);

            if (newPath != absPath && absPath != inArea.ImgPath)
            {
                File.Copy(absPath, newPath, true);
            }
        }

        private List<string> GetRelativePictures(AreaMap inAreaMap)
        {
            List<string> pictures = new List<string>();

            string absPath = "";
            if (inAreaMap.ImagePath != "")
            {
                absPath = inAreaMap.GetAbsolutePath(inAreaMap.ImagePath);
                if (absPath != inAreaMap.ImagePath && File.Exists(absPath))
                {
                    pictures.Add(absPath);
                }
            }

            foreach (Area area in inAreaMap.Areas)
            {
                absPath = inAreaMap.GetAbsolutePath(area.ImgPath);
                if (absPath != area.ImgPath && File.Exists(absPath))
                {
                    pictures.Add(absPath);
                }
            }

            return pictures;
        }

        private AreaMap FindFromPath(string path)
        {
            foreach (AreaMap map in Maps)
            {
                if (map.Path == path)
                {
                    return map;
                }
            }

            return null;
        }

        public string Add(string inPath)
        {
             return Load(inPath, true);
        }

        public string Load(string inPath)
        {
            return Load(inPath, false);
        }

        public string Load(string inPath, bool inAdd)
        {
            string status = "";
            StreamReader reader = null;

            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(AreaMap));

                reader = new StreamReader(inPath);
                if (!inAdd)
                {
                    _maps.Clear();
                }
                
                AreaMap newMap = (AreaMap)ser.Deserialize(reader);
                newMap.Path = inPath;

                //if (newMap.Groups.Count == 0)
                //{
                //newMap.LoadGroups(inPath.Replace(Path.GetFileName(inPath), "Groups.xml"));
                //}
                newMap.ResetGroups();
                _maps.Add(newMap);
                newMap.InitializeComponents();

                if (!inAdd)
                {
                    if (newMap.AssociatedPaths.Count > 0)
                    {
                        foreach (string path in newMap.AssociatedPaths)
                        {
                            if (path != newMap.Path)
                            {
                                Add(path);
                            }
                        }
                    }

                    _currentIndex = 0;
                }
                else
                {
                    //RefreshAssociated();
                }

                //Resolve
                Initialize();
            }
            catch (Exception e) { status = e.Message; }

            if (reader != null)
            {
                reader.Close();
            }

            return status;
        }

        private void DumpGroups()
        {
            foreach (AreaGroup group in Groups)
            {
                group.DumpAreas();
            }
        }

        
        private void LoadGroups(string inPath)
        {
            string status = "";
            StreamReader reader = null;

            if (File.Exists(inPath))
            {
                try
                {
                    XmlSerializer ser = new XmlSerializer(typeof(List<AreaGroup>));

                    reader = new StreamReader(inPath);
                    Groups = (List<AreaGroup>)ser.Deserialize(reader);
                    //ResetGroups();
                    
                }
                catch
                {

                }
            }
        }

        private void SaveGroups(string inPath)
        {
            DumpGroups();
            StreamWriter writer = null;
            XmlSerializer ser = new XmlSerializer(typeof(List<AreaGroup>));

            try
            {
                writer = new StreamWriter(inPath, false);
                ser.Serialize(writer, Groups);
            }
            catch
            {

            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        private void RefreshAssociated()
        {
            List<string> associated = new List<string>();
            foreach (AreaMap map in _maps)
            {
                associated.Add(map.Path);
            }

            foreach (AreaMap map in _maps)
            {
                map.AssociatedPaths = associated;
            }
        }

        private void Initialize()
        {
            //Load Background Image
            
            SetPicture(CurrentAreaMap.ImagePath);
        }

        #endregion

        #region SHAPES

        void Edit()
        {
            if (!_isEditing)
            {
                _isEditing = true;

                List<Area> areas = new List<Area>(CurrentAreaMap.Areas);

                foreach (Area area in areas)
                {
                    if (!area.IsEditing && !area.IsSubComponent)
                    {
                        Edit(area);
                    }
                }

                Invalidate();
            }
        }

        public void Edit(Area area)
        {
            area.IsEditing = true;
            List<Area> subAreas = area.Edit();
            CurrentAreaMap.Areas.InsertRange(0, subAreas);
        }

        public void StopEdit()
        {
            List<Area> areas = new List<Area>();
            foreach (Area area in CurrentAreaMap.Areas)
            {
                if (area.IsEditing)
                {
                    areas.AddRange(area.SubComponents);
                    area.SubComponents.Clear();
                    area.IsEditing = false;
                }
            }
            foreach (Area area in areas)
            {
                CurrentAreaMap.Areas.Remove(area);
            }
            _isEditing = false;
            Invalidate();
        }

        private void StopEdit(Area area)
        {
            List<Area> areas = new List<Area>();
            foreach (Area subArea in area.SubComponents)
            {
                CurrentAreaMap.Areas.Remove(subArea);
            }
            area.SubComponents.Clear();
            area.IsEditing = false;
        }

        public void RemoveAreas(List<Area> selectedAreas)
        {
            List<Area> toDeleteAreas = new List<Area>();

            foreach (Area area in selectedAreas)
            {
                if (area.SubComponents.Count > 0)
                {
                    toDeleteAreas.AddRange(area.SubComponents);
                }
                toDeleteAreas.Add(area);
            }

            foreach (Area area in toDeleteAreas)
            {
                CurrentAreaMap.Areas.Remove(area);
            }
        }

        public void RemoveCurrentMap()
        {
            DeselectAll();
            AreaMap map = CurrentAreaMap;

            if (_currentIndex > 0)
            {
                _currentIndex--;
            }
            else
            {
                _currentIndex++;
            }

            SetPicture(CurrentAreaMap.ImagePath);
            _maps.Remove(map);
            RefreshAssociated();
        }

        public void Duplicate(Area area)
        {
            Area clone = area.Clone();
            clone.Offset(new Vector2(10f, 10f));
            AddArea(clone);
            Invalidate();
        }
        
        public void Mirror(Area area)
        {
            area.Mirror();
            Invalidate();
        }

        public void MirrorY(Area area)
        {
            area.MirrorY();
            Invalidate();
        }

        public void Symmetrize(Area area)
        {
            bool isLeft = true;
            string[] sides = new string[] {"Left_", "Right_"};
            Color[] colors = new Color[] {Color.Red, Color.Blue};
            area.Mirror(Center);

            //Find location
            if (area.Name.Contains("Left_") || area.Name.Contains("L_"))
            {
                if (area.Name.Contains("L_"))
                {
                    sides = new string[] { "L_", "R_" };
                }
            }
            else if (area.Name.Contains("Right_") || area.Name.Contains("R_"))
            {
                isLeft = false;
                if (area.Name.Contains("R_"))
                {
                    sides = new string[] { "L_", "R_" };
                }
            }
            else
            {
                return;
            }

            area.Name = area.Name.Replace(sides[isLeft ? 0 : 1], sides[isLeft ? 1 : 0]);
            area.MetaData = area.MetaData.Replace(sides[isLeft ? 0 : 1], sides[isLeft ? 1 : 0]);
            area.AreaColor = colors[isLeft ? 1 : 0];
        }

        public void SymmetrizeY(Area area)
        {
            bool isLeft = true;
            string[] sides = new string[] { "Left_", "Right_" };
            Color[] colors = new Color[] { Color.Red, Color.Blue };
            area.MirrorY(Center);

            //Find location
            if (area.Name.Contains("Left_") || area.Name.Contains("L_"))
            {
                if (area.Name.Contains("L_"))
                {
                    sides = new string[] { "L_", "R_" };
                }
            }
            else if (area.Name.Contains("Right_") || area.Name.Contains("R_"))
            {
                isLeft = false;
                if (area.Name.Contains("R_"))
                {
                    sides = new string[] { "L_", "R_" };
                }
            }
            else
            {
                return;
            }

            area.Name = area.Name.Replace(sides[isLeft ? 0 : 1], sides[isLeft ? 1 : 0]);
            area.MetaData = area.MetaData.Replace(sides[isLeft ? 0 : 1], sides[isLeft ? 1 : 0]);
            area.AreaColor = colors[isLeft ? 1 : 0];
        }

        public void ResetAll()
        {
            foreach (AreaMap map in _maps)
            {
                foreach (Area area in map.Areas)
                {
                    Reset(area);
                }
            }
            
            Invalidate();
        }

        public void ResetCurrent()
        {
            foreach (Area area in CurrentAreaMap.Areas)
            {
                Reset(area);
            }
            Invalidate();
        }

        public void SetIndex(int inIndex)
        {
            _currentIndex = inIndex;
            SetPicture(CurrentAreaMap.ImagePath);
            ApplyGroupValues();
        }

        #endregion

        #region GROUPS

        public AreaGroup FindGroup(string inName)
        {
            string groupsNames = "";

            foreach (AreaGroup group in Groups)
            {
                groupsNames += group.Name + ",";
            }

            foreach (AreaGroup group in Groups)
            {
                if (group.Name == inName)
                {
                    return group;
                }
            }

            return null;
        }

        public void AddGroup(string inName)
        {
            Groups.Add(new AreaGroup(inName));
        }

        public void CreateGroupFromSelection(string inName)
        {
            CurrentAreaMap.CreateGroupFromSelection(inName);
        }

        public void RemoveGroup(int row)
        {
            CurrentAreaMap.RemoveGroup(row);
        }

        public void AddShapesToGroup(int row)
        {
            CurrentAreaMap.AddShapesToGroup(row);
        }

        public void RemoveShapesFromGroup(int row)
        {
            CurrentAreaMap.RemoveShapesFromGroup(row);
        }

        public void ApplyGroupValues()
        {
            foreach (AreaGroup group in Groups)
            {
                group.ApplyValues(_showAll);
            }
            Invalidate();
        }

        public void SelectGroups(List<AreaGroup> selectedGroups)
        {
            List<Area> toSelect = new List<Area>();

            foreach (AreaGroup group in selectedGroups)
            {
                toSelect.AddRange(group.Areas);
            }

            if (toSelect.Count > 0)
            {
                Select(toSelect);
            }
        }

        #endregion

        public List<Area> GetEditSelection()
        {
            List<Area> maps = GetAllAreas();

            List<Area> editSelection = new List<Area>();
            foreach (Area area in maps)
            {
                if (area.IsSelected)
                {
                    editSelection.Add(area);
                }
            }

            return editSelection;
        }

        public void SelectGroupWithModifiers(AreaGroup areaGroup)
        {
            SelectWithModifiers(areaGroup.Areas);
        }

        private void AreaMapComponent_MouseEnter(object sender, EventArgs e)
        {
            //Why ? it causes the window to go in front of others...need a real reason for this
            //Focus();
        }

        private Vector2 Project(CG_Vector3 cG_Vector3, Projections inProjection)
        {
            switch (inProjection)
            {
                case Projections.XZ:
                    return new Vector2((float)cG_Vector3.X, (float)cG_Vector3.Z);

                case Projections.YZ:
                    return new Vector2((float)cG_Vector3.Z, (float)cG_Vector3.Y);
            }

            return new Vector2((float)cG_Vector3.X, (float)cG_Vector3.Y);
        }

        public Area AddOscarControl(string inName, RigElement inRigElement, ControllerInfos inControllerInfos, Projections inProjection)
        {
            Vector2[] basePoints = new Vector2[]{   new Vector2(-0.5f, -0.5f), new Vector2(0.5f, -0.5f),
                                                    new Vector2(0.5f, 0.5f), new Vector2(-0.5f, 0.5f)};

            Vector2.ScaleVectors(ref basePoints, (float)inRigElement.GetSize());

            CG_Vector3 displayOffset = UnTransform(inRigElement.DisplayOffset.Pos, inRigElement.Trans.Rot);
            CG_Vector3 displayScale = UnTransform(inRigElement.DisplayOffset.Scl, inRigElement.Trans.Rot);

            Vector2 display = Project(displayOffset, inProjection);
            Vector2 scale = Project(displayScale, inProjection);
            Vector2 translation = Project(inRigElement.Trans.Pos, inProjection);

            translation += display;
            Vector2.OffsetVectors(ref basePoints, translation);
            Vector2.ScaleVectors(ref basePoints, scale);
            
            Area newArea = AddPolygon(basePoints);
            newArea.Name = newArea.MetaData = inName;

            newArea.AreaColor = ProjectsManager.Get().ProjectPrefs.GetColor(inRigElement);

            return newArea;
        }

        private CG_Vector3 UnTransform(CG_Vector3 cG_Vector3, CG_Vector3 cG_Vector3_2)
        {
            return cG_Vector3;
        }
    }

    public class AreaEventArgs : MouseEventArgs
    {
        Area _area;
        public Area Area
        {
            get { return _area; }
            set { _area = value; }
        }

        public AreaEventArgs(Area inArea, MouseButtons inButton, int inClicks, int inX, int inY, int inDelta)
            : base(inButton, inClicks, inX, inY, inDelta)
        {
            _area = inArea;
        }

        public AreaEventArgs(Area inArea, MouseEventArgs inE)
            : base(inE.Button, inE.Clicks, inE.X, inE.Y, inE.Delta)
        {
            _area = inArea;
        }
    }

    public class FunctionEventArgs : EventArgs
    {
        string _functionName;
        public string FunctionName
        {
            get { return _functionName; }
        }

        object[] _arguments;
        public object[] Arguments
        {
            get { return _arguments; }
        }

        public FunctionEventArgs(string inFunctionName, object[] inArguments)
        {
            _functionName = inFunctionName;
            _arguments = inArguments;
        }
    }

    public enum SelectionChangedModes
    {
        Unknown, SelectionRemoved, SelectionAdded, DeselectAll
    }

    public class SelectionChangedEventArgs : EventArgs
    {
        SelectionChangedModes _selectionChangedMode = SelectionChangedModes.Unknown;

        List<Area> _oldSelection = new List<Area>();
        List<Area> _newSelection = new List<Area>();
        List<Area> _changedSelection = new List<Area>();
        bool _cancel = false;

        public SelectionChangedModes SelectionChangedMode
        {
            get { return _selectionChangedMode; }
        }
        public List<Area> OldSelection
        {
          get { return _oldSelection; }
        }
        public List<Area> NewSelection
        {
            get { return _newSelection; }
        }
        public List<Area> ChangedSelection
        {
            get { return _changedSelection; }
        }

        public bool Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }

        public SelectionChangedEventArgs(List<Area> inOldSelection, SelectionChangedModes inSelectionChangedMode, List<Area> inChangedSelection)
        {
            _oldSelection = inOldSelection;
            _selectionChangedMode = inSelectionChangedMode;
            _changedSelection = inChangedSelection;

            //Manage new Selection List
            switch (_selectionChangedMode)
            {
                case SelectionChangedModes.Unknown:
                    _newSelection.AddRange(_changedSelection);
                    break;
                case SelectionChangedModes.SelectionAdded:
                    foreach (Area area in _oldSelection)
                    {
                        _newSelection.Add(area);
                    }
                    _newSelection.AddRange(_changedSelection);
                    break;

                case SelectionChangedModes.SelectionRemoved:
                    foreach (Area area in _oldSelection)
                    {
                        if (!_changedSelection.Contains(area))
                        {
                            _newSelection.Add(area);
                        }
                    }
                    break;
            }
        }
    }

    public class ValueChangedEventArgs : EventArgs
    {
        float _oldXValue;
        float _oldYValue;
        Area _area;
        bool _cancel;
        bool _validated;

        public float OldXValue
        {
            get { return _oldXValue; }
            set { _oldXValue = value; }
        }
        public float OldYValue
        {
            get { return _oldYValue; }
            set { _oldYValue = value; }
        }
        public Area Area
        {
            get { return _area; }
            set { _area = value; }
        }
        public bool Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }
        public bool Validated
        {
            get { return _validated; }
        }

        public ValueChangedEventArgs(float inOldXValue,float inOldYValue,Area inArea, bool inValidated)
        {
            _oldXValue = inOldXValue;
            _oldYValue = inOldYValue;
            _area = inArea;
            _validated = inValidated;
        }
    }
}
