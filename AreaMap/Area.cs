using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK.GeometryLib;
using System.Drawing;
using System.ComponentModel;
using System.Xml.Serialization;
using System.IO;
using TK.BaseLib;

namespace TK.GeometryLib.AreaMapFramework
{
    /// <summary>
    /// Tells if a color is named of defined by its Alpha, Red, Green and Blue Values
    /// </summary>
    public enum ColorFormat
    {
        NamedColor,
        ARGBColor
    }

    public enum AreaBehavior
    {
        SelectOnly, MoveOnly, SelectAndMove, Button
    }

    public enum AreaMovableModes
    {
        None, XOnly, YOnly, Both
    }

    public class Area
    {
        public Area()
        {

        }

        public Area(Shape2 inShape, AreaBehavior inBehavior)
        {
            _shape = inShape;
            _type = (inShape is Circle) ? ShapeType.Circle : ((inShape is Polygon2) ? ShapeType.Polygon : ShapeType.Rectangle);

            AreaColor = Color.Yellow;

            _behavior = inBehavior;
        }

        public Area(Shape2 inShape,Area inParent, int inIndex)
        {
            _shape = inShape;
            _type = (inShape is Circle) ? ShapeType.Circle : ShapeType.Polygon;

            AreaColor = Color.Yellow;
            _disabledBrush = new SolidBrush(Color.FromArgb(20, 127, 127, 127));

            _parent = inParent;
            _blackPen = new Pen(Color.Azure, 1f);
            _hoverBorderPen = new Pen(Color.Azure, 2f);

            _index = inIndex;
            _behavior = AreaBehavior.MoveOnly;
        }

        AreaMap _map = null;
        string _name = "NewArea";
        string _metaData = "";
        string _description = "";
        string _text = "";
        Font _textFont = new Font("Tahoma", 24f, FontStyle.Regular, GraphicsUnit.Pixel);
        Vector2 _textPosition = Vector2.Null;
        float _textSize = 24f;
        Shape2 _shape;
        ShapeType _type = ShapeType.Undefined;
        AreaBehavior _behavior = AreaBehavior.SelectOnly;
        bool _isSelected = false;
        bool _isHovered = false;
        bool _isEditing = false;
        bool _isActive = true;
        bool _isVisible = true;
        bool _fontChanged = false;
        int _disabledOpacity = 60;
        int _enabledOpacity = 180;
        Color _areaColor = Color.Gray;
        Color _borderColor = Color.Black;
        Color _textColor = Color.Black;
        Brush _brush = Brushes.Gray;
        Brush _textBrush = Brushes.Black;
        Brush _selectedBrush = Brushes.Gray;
        Brush _disabledBrush = new SolidBrush(Color.FromArgb(60, 127, 127, 127));
        Brush _redBrush = new SolidBrush(Color.FromArgb(35, 255, 0, 0));
        Pen _borderPen = new Pen(Color.Black, 1f);
        Pen _blackPen = new Pen(Color.Black, 1f);
        Pen _smoothPen = new Pen(Color.FromArgb(127, Color.Black), 1f);
        Pen _whitePen = new Pen(Color.FromArgb(127, Color.White), 1f);
        Pen _hoverBorderPen = new Pen(Color.Black, 2f);
        Area _parent = null;
        List<Area> _subComponents = new List<Area>();
        int _index = -1;
        Vector2 _refCenter = Vector2.Null;
        float _minX = 0f;
        float _maxX = 0f;
        float _minY = 0f;
        float _maxY = 0f;
        float _minSuggestedX = 0f;
        float _maxSuggestedX = 0f;
        float _minSuggestedY = 0f;
        float _maxSuggestedY = 0f;
        string _xParam = "";
        string _yParam = "";
        float _xMultiply = 1f;
        float _yMultiply = 1f;
        float _xOffset = 0f;
        float _yOffset = 0f;
        bool _xYSymmetryMode = false;
        string _imgPath = "";
        Image _image = null;
        Vector2 _imgPosition = Vector2.Null;
        Vector2 _imgScaling = new Vector2(1f,1f);
        Vector2 _imgDimension = Vector2.Null;
        bool _imgIsBackground = false;

        #region PROPERTIES

        [XmlIgnore]
        [Browsable(false)]
        public AreaMap Map
        {
            get { return _map; }
            set { _map = value; }
        }

        [Browsable(false)]
        public ShapeType RealType
        {
            get { return _type; }
            set { _type = value; }
        }

        [CategoryAttribute("Basic")]
        public ConvertibleShapeType Type
        {
            get { return Shape2.ShapeToConvertibleShape(_type); }
            set
            {
                if (!IsEditing)
                {
                    ShapeType type = Shape2.ConvertibleShapeToShape(value);
                    if (type != _type)
                    {
                        bool wasEditing = false;
                        if (IsEditing)
                        {
                            wasEditing = true;
                        }

                        Shape = GeoConverter.Convert(Shape, type, false);
                    }

                    _type = type;
                }
            }
        }

        [CategoryAttribute("Basic")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [CategoryAttribute("Basic")]
        public string MetaData
        {
            get { return _metaData; }
            set { _metaData = value; }
        }

        [CategoryAttribute("Basic")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        [CategoryAttribute("Basic")]
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        [CategoryAttribute("Basic")]
        [XmlIgnore]
        public Font TextFont
        {
            get { return _textFont; }
            set { _textFont = value; _fontChanged = true; }
        }

        [CategoryAttribute("Basic")]
        public Vector2 TextPosition
        {
            get { return _textPosition; }
            set { _textPosition = value; }
        }

        [CategoryAttribute("Status")]
        public bool IsActive
        {
            get { return IsEditing || _isActive; }
            set { _isActive = value; }
        }

        [CategoryAttribute("Status")]
        public bool IsVisible
        {
            get { return IsEditing || _isVisible; }
            set { _isVisible = value; }
        }


        [Browsable(false)]
        public Shape2 Shape
        {
            get { return _shape; }
            set { _shape = value; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public bool IsMovableX
        {
            get
            {
                if (!IsMovable){ return false; }

                return (_maxX - _minX) > 0;
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        public bool IsMovableY
        {
            get
            {
                if (!IsMovable) { return false; }

                return (_maxY - _minY) > 0;
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        public AreaMovableModes MovableMode
        {
            get
            {
                if (IsEditing)
                {
                    return AreaMovableModes.Both;
                }

                if (IsMovableX)
                {
                    if (IsMovableY)
                    {
                        return AreaMovableModes.Both;
                    }
                    else
                    {
                        return AreaMovableModes.XOnly;
                    }
                }
                else
                {
                    if (IsMovableY)
                    {
                        return AreaMovableModes.YOnly;
                    }
                }

                return AreaMovableModes.None;
            }
        }

        [XmlIgnore]
        [CategoryAttribute("Status")]
        public float ValueX
        {
            get
            {
                if (_xYSymmetryMode)
                {
                    float factor = Math.Min(Math.Max(RawValueX + 1, 0), 1);
                    return Math.Min(Math.Max(RawValueY * factor, ValueMinX), ValueMaxX);
                }

                return Math.Min(Math.Max(RawValueX, ValueMinX), ValueMaxX);
            }
            set
            {
                if (!_xYSymmetryMode)
                {
                    Center = new Vector2(Math.Min(Math.Max((value - _xOffset) / _xMultiply, _minX), _maxX) + _refCenter.X, Center.Y);
                }
                else
                {
                    if (value < 0.0001 && ValueY < 0.0001)
                    {
                        Center = new Vector2(_xOffset / _xMultiply + _refCenter.X, _yOffset / _yMultiply + _refCenter.Y);
                        return;
                    }

                    if (value > ValueY)
                    {
                        float factor = (ValueY - value) / value;
                        Center = new Vector2((-factor + _xOffset) / _xMultiply + _refCenter.X,
                            Math.Min(Math.Max((-value + _yOffset) / _yMultiply, _minY), _maxY) + _refCenter.Y);
                    }
                    else
                    {
                        float factor = (value - ValueY) / ValueY;
                        Center = new Vector2((factor + _xOffset) / _xMultiply + _refCenter.X,
                            Math.Min(Math.Max((-ValueY + _yOffset) / _yMultiply, _minY), _maxY) + _refCenter.Y);
                    }
                }
            }
        }
        [XmlIgnore]
        [CategoryAttribute("Status")]
        public float ValueY
        {
            get
            {
                if (_xYSymmetryMode)
                {
                    float factor = Math.Min(Math.Max(-RawValueX + 1, 0), 1);
                    return Math.Min(Math.Max(RawValueY * factor, ValueMinY), ValueMaxY);
                }

                return Math.Min(Math.Max(RawValueY, ValueMinY), ValueMaxY);
            }
            set
            {
                if (!_xYSymmetryMode)
                {
                    Center = new Vector2(Center.X, Math.Min(Math.Max((-value + _yOffset) / _yMultiply, _minY), _maxY) + _refCenter.Y);
                }
                else
                {
                    if (value < 0.0001 && ValueX < 0.0001)
                    {
                        Center = new Vector2(_xOffset / _xMultiply + _refCenter.X, _yOffset / _yMultiply + _refCenter.Y);
                        return;
                    }

                    if (value > ValueX)
                    {
                        float factor = (ValueX - value) / value;
                        Center = new Vector2((factor + _xOffset) / _xMultiply + _refCenter.X,
                            Math.Min(Math.Max((-value + _yOffset) / _yMultiply, _minY), _maxY) + _refCenter.Y);
                    }
                    else
                    {
                        float factor = (value - ValueX) / ValueX;
                        Center = new Vector2((-factor + _xOffset) / _xMultiply + _refCenter.X,
                            Math.Min(Math.Max((-ValueX + _yOffset) / _yMultiply, _minY), _maxY) + _refCenter.Y);
                    }
                }
            }
        }

        [CategoryAttribute("Behavior Main")]
        public AreaBehavior Behavior
        {
            get { return _behavior; }
            set { _behavior = value; }
        }
        [CategoryAttribute("Behavior Main")]
        public bool XYSymmetryMode
        {
            get { return _xYSymmetryMode; }
            set { _xYSymmetryMode = value; }
        }

        [CategoryAttribute("Behavior X")]
        public float MinX
        {
            get { return _minX; }
            set { _minX = value; }
        }
        [CategoryAttribute("Behavior X")]
        public float MaxX
        {
            get { return _maxX; }
            set { _maxX = value; }
        }
        [CategoryAttribute("Behavior X")]
        public float XMultiply
        {
            get { return _xMultiply; }
            set { _xMultiply = value != 0f ? value : 1f; }
        }
        [CategoryAttribute("Behavior X")]
        [XmlIgnore]
        public float ValueMinX
        {
            get
            {
                if (_xYSymmetryMode)
                {
                    return ValueMinY;
                }

                return MinX * _xMultiply + _xOffset;
            }
            set
            {
                if (!_xYSymmetryMode)
                {
                    _minX = -value / _xMultiply - _xOffset;
                }
            }
        }
        [CategoryAttribute("Behavior X")]
        [XmlIgnore]
        public float ValueMaxX
        {
            get
            {
                if (_xYSymmetryMode)
                {
                    return ValueMaxY;
                }

                return MaxX * _xMultiply + _xOffset;
            }
            set
            {
                if (!_xYSymmetryMode)
                {
                    _maxX = -value / _xMultiply - _xOffset;
                }
            }
        }
        [CategoryAttribute("Behavior X")]
        public float ValueSuggestedMinX
        {
            get
            {
                return _minSuggestedX;
            }
            set { _minSuggestedX = value; }
        }
        [CategoryAttribute("Behavior X")]
        public float ValueSuggestedMaxX
        {
            get
            {
                return _maxSuggestedX;
            }
            set { _maxSuggestedX = value; }
        }
        [CategoryAttribute("Behavior X")]
        public float ValueMultiplyX
        {
            get { return 1; }
            set
            {
                if (!_xYSymmetryMode)
                {
                    _minX *= value;
                    _maxX *= value;
                    _xMultiply /= value;
                }
            }
        }
        [CategoryAttribute("Behavior X")]
        public string XParam
        {
            get { return _xParam; }
            set { _xParam = value; }
        }
        [CategoryAttribute("Behavior X")]
        public float XOffset
        {
            get { return _xOffset; }
            set { _xOffset = value; }
        }

        [CategoryAttribute("Behavior Y")]
        public float MinY
        {
            get { return _minY; }
            set { _minY = value; }
        }
        [CategoryAttribute("Behavior Y")]
        public float MaxY
        {
            get { return _maxY; }
            set { _maxY = value; }
        }
        [CategoryAttribute("Behavior Y")]
        public float YMultiply
        {
            get { return _yMultiply; }
            set { _yMultiply = value != 0f ? value : 1f; }
        }
        [CategoryAttribute("Behavior Y")]
        [XmlIgnore]
        public float ValueMinY
        {
            get { return -MaxY * _yMultiply + _yOffset; }
            set { _maxY = -value / _yMultiply - _yOffset; }
        }
        [CategoryAttribute("Behavior Y")]
        [XmlIgnore]
        public float ValueMaxY
        {
            get { return -MinY * _yMultiply + _yOffset; }
            
            set{_minY = -value / _yMultiply - _yOffset;}
        }
        [CategoryAttribute("Behavior Y")]
        public float ValueSuggestedMinY
        {
            get
            {
                return _minSuggestedY;
            }
            set { _minSuggestedY = value; }
        }
        [CategoryAttribute("Behavior Y")]
        public float ValueSuggestedMaxY
        {
            get
            {
                return _maxSuggestedY;
            }
            set { _maxSuggestedY = value; }
        }
        [CategoryAttribute("Behavior Y")]
        public float ValueMultiplyY
        {
            get { return 1; }
            set
            {
                _minY *= value;
                _maxY *= value;
                _yMultiply /= value;
            }
        }
        [CategoryAttribute("Behavior Y")]
        public string YParam
        {
            get { return _yParam; }
            set { _yParam = value; }
        }
        [CategoryAttribute("Behavior Y")]
        public float YOffset
        {
            get { return _yOffset; }
            set { _yOffset = value; }
        }
        // --- TECHNICAL PROPERTIES ---

        [XmlIgnore]
        [Browsable(false)]
        public int Index
        {
            get { return _index; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public bool IsMovable
        {
            get { return _behavior == AreaBehavior.SelectAndMove || _behavior == AreaBehavior.MoveOnly; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public List<Area> SubComponents
        {
            get { return _subComponents; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public Area Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        [Browsable(false)]
        public string XmlColor
        {
            get { return SerializationHelper.SerializeColor(_areaColor); }
            set { AreaColor = SerializationHelper.DeserializeColor(value); }
        }

        [Browsable(false)]
        public string XmlBorderColor
        {
            get { return SerializationHelper.SerializeColor(_borderColor); }
            set { BorderColor = SerializationHelper.DeserializeColor(value); }
        }

        [Browsable(false)]
        public string XmlTextColor
        {
            get { return SerializationHelper.SerializeColor(_textColor); }
            set { TextColor = SerializationHelper.DeserializeColor(value); }
        }

        [Browsable(false)]
        public string XmlTextFont
        {
            get { return SerializationHelper.SerializeFont(_textFont); }
            set { _textFont = SerializationHelper.DeserializeFont(value); }
        }

        [XmlIgnore]
        [CategoryAttribute("Shape")]
        public Vector2 Center
        {
            get
            {
                return _shape.Center;
            }
            set
            {
                _shape.Center = value;
            }
        }

        [Browsable(false)]
        public Vector2 RefCenter
        {
            get { return _refCenter; }
            set { _refCenter = value; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public float LocalMinX
        {
            get { return _refCenter.X + _minX; }
            set { _minX = value - _refCenter.X; }
        }
        [XmlIgnore]
        [Browsable(false)]
        public float LocalMaxX
        {
            get { return _refCenter.X + _maxX; }
            set { _maxX = value - _refCenter.X; }
        }
        [XmlIgnore]
        [Browsable(false)]
        public float LocalMinY
        {
            get { return _refCenter.Y + _minY; }
            set { _minY = value - _refCenter.Y; }
        }
        [XmlIgnore]
        [Browsable(false)]
        public float LocalMaxY
        {
            get { return _refCenter.Y + _maxY; }
            set { _maxY = value - _refCenter.Y; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public float RawValueX
        {
            get
            {
                return (Center.X - _refCenter.X) * _xMultiply + _xOffset;
            }
        }
        [XmlIgnore]
        [Browsable(false)]
        public float RawValueY
        {
            get
            {
                return (_refCenter.Y - Center.Y) * _yMultiply + _yOffset;
            }
        }

        //SubComponent Specific
        [XmlIgnore]
        [CategoryAttribute("Shape")]
        public bool IsSubComponent
        {
            get
            {
                return _parent != null;
            }
        }

        //Status

        [XmlIgnore]
        [Browsable(false)]
        public bool IsSelectable
        {
            get { return _behavior == AreaBehavior.SelectAndMove || _behavior == AreaBehavior.SelectOnly || _isEditing || IsSubComponent; }
        }

        [CategoryAttribute("Shape")]
        public string ImgPath
        {
            get { return _imgPath; }
            set
            {
                _imgPath = value;
                SetImage(_imgPath);
            }
        }

        [CategoryAttribute("Shape")]
        public Vector2 ImgPosition
        {
            get { return _imgPosition; }
            set { _imgPosition = value; }
        }

        [CategoryAttribute("Shape")]
        public bool ImgIsBackground
        {
            get { return _imgIsBackground; }
            set { _imgIsBackground = value; }
        }

        [CategoryAttribute("Shape")]
        public Vector2 ImgScaling
        {
            get { return _imgScaling; }
            set { _imgScaling = value; }
        }

        [XmlIgnore]
        [CategoryAttribute("Shape")]
        public float Radius
        {
            get
            {
                if (_type == ShapeType.Circle)
                {
                    return (_shape as Circle).Radius;
                }

                return 0;
            }
            set
            {
                if (_type == ShapeType.Circle)
                {
                    (_shape as Circle).Radius = value;
                }
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        public Vector2 Corner
        {
            get { return _shape.Corner; }
            set
            {
                Vector2 offset = Center - Corner;
                Center = value + offset;
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        public List<Vector2> Points
        {
            get
            {
                if (_type == ShapeType.Polygon)
                {
                    return (_shape as Polygon2).Points;
                }

                return new List<Vector2>();
            }
        }

        [XmlIgnore]
        [Browsable(false)]
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public bool IsHovered
        {
            get { return _isHovered; }
            set { _isHovered = value; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public bool IsEditing
        {
            get { return _isEditing; }
            set { _isEditing = value; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public bool IsEditionRelated
        {
            get { return _isEditing || _parent != null; }
        }

        [XmlIgnore]
        [CategoryAttribute("Shape")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; _borderPen = new Pen(value, 1f); _hoverBorderPen = new Pen(value, 2f); }
        }
        [XmlIgnore]
        [CategoryAttribute("Shape")]
        public Color TextColor
        {
            get { return _textColor; }
            set { _textColor = value; _textBrush = new SolidBrush(value); }
        }
        
        [XmlIgnore]
        [CategoryAttribute("Shape")]
        public Color AreaColor
        {
            get { return _areaColor; }
            set { _areaColor = Color.FromArgb(_disabledOpacity, value.R, value.G, value.B); _brush = new SolidBrush(_areaColor); _selectedBrush = new SolidBrush(Color.FromArgb(_enabledOpacity, value.R, value.G, value.B)); }
        }

        [XmlIgnore]
        [Browsable(false)]
        public Pen CurrentPen
        {
            get { return _isHovered ? _hoverBorderPen : _borderPen; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public Brush CurrentBrush
        {
            get { return _isActive ? (_isSelected ? _selectedBrush : _brush) : _disabledBrush; }
        }

        #endregion

        internal bool Contains(Vector2 inVector2)
        {
            return _shape.Contains(inVector2);
        }

        public void Draw(Graphics inGraphics, Vector2 inOffset, float inScale)
        {
            if (_isVisible)
            {
                //Adjust font
                if (_fontChanged)
                {
                    _fontChanged = false;
                    _textSize = TextFont.Size / inScale;
                }
                else
                {
                    _textFont = new Font(_textFont.FontFamily, _textSize * inScale, _textFont.Style);
                }

                //Image
                if (_image != null && _imgIsBackground)
                {
                    inGraphics.DrawImage(_image, inOffset.X + (RefCenter.X + _imgPosition.X) * inScale, inOffset.Y + (RefCenter.Y + _imgPosition.Y) * inScale, _imgDimension.X * inScale * _imgScaling.X, _imgDimension.Y * inScale * _imgScaling.Y);
                }

                Pen framePen = _smoothPen;
                //Bounds
                if (IsMovable && !IsSubComponent)
                {
                    float minBounds = 10f;

                    if (IsSelected || IsHovered)
                    {
                        //Use a more solid pen
                        framePen = _blackPen;
                    }

                    Vector2 center = Center;
                    float width = Math.Max(minBounds, MaxX - MinX);
                    float height = Math.Max(minBounds, MaxY - MinY);
                    float x = _refCenter.X + MinX - (IsMovableX ? 0f : minBounds / 2f);
                    float y = _refCenter.Y + MinY - (IsMovableY ? 0f : minBounds / 2f);

                    //Pointer
                    inGraphics.DrawRectangle(framePen, inOffset.X + center.X * inScale - 1, inOffset.Y + center.Y * inScale - 1, 2, 2);

                    //Bounds
                    inGraphics.DrawRectangle(framePen, inOffset.X + x * inScale - 1, inOffset.Y + y * inScale - 1, width * inScale + 2, height * inScale + 2);
                    inGraphics.DrawRectangle(_whitePen, inOffset.X + x * inScale, inOffset.Y + y * inScale, width * inScale, height * inScale);

                    //Suggested Bounds
                    if (HasLimits())
                    {
                        float internalMinXOffset = 0f;
                        float internalMaxXOffset = 0f;

                        if (!_xYSymmetryMode)
                        {
                            if (_minSuggestedX != _maxSuggestedX)
                            {
                                if (IsMovableX)
                                {
                                    if (_minSuggestedX > ValueMinX)
                                    {
                                        internalMinXOffset = (_minSuggestedX - ValueMinX) * inScale / _xMultiply;
                                        inGraphics.FillRectangle(_redBrush, inOffset.X + x * inScale + 1, inOffset.Y + y * inScale + 1, internalMinXOffset - 1, height * inScale - 2);
                                    }
                                    if (_maxSuggestedX < ValueMaxX)
                                    {
                                        internalMaxXOffset = (ValueMaxX - _maxSuggestedX) * inScale / _xMultiply;
                                        inGraphics.FillRectangle(_redBrush, inOffset.X + (x + width) * inScale - internalMaxXOffset, inOffset.Y + y * inScale + 1, internalMaxXOffset - 1, height * inScale - 2);
                                    }
                                }
                            }
                        }

                        if (_minSuggestedY != _maxSuggestedY)
                        {
                            if (IsMovableY)
                            {
                                float internalMinYOffset = 0f;
                                float internalMaxYOffset = 0f;

                                if (_minSuggestedY > ValueMinY)
                                {
                                    internalMinYOffset = (_minSuggestedY - ValueMinY) * inScale / _yMultiply;
                                    inGraphics.FillRectangle(_redBrush, inOffset.X + x * inScale + internalMinXOffset, inOffset.Y + (y + height) * inScale - 1 - internalMinYOffset, width * inScale - internalMinXOffset - internalMaxXOffset, internalMinYOffset);
                                }
                                if (_maxSuggestedY < ValueMaxY)
                                {
                                    internalMaxYOffset = (ValueMaxY - _maxSuggestedY) * inScale / _yMultiply;
                                    inGraphics.FillRectangle(_redBrush, inOffset.X + x * inScale + internalMinXOffset, inOffset.Y + y * inScale + 1, width * inScale - internalMinXOffset - internalMaxXOffset, internalMaxYOffset - 1);
                                }
                            }
                        }
                    }
                }

                //Shape
                switch (RealType)
                {
                    case ShapeType.Circle:
                        //Circle Shape
                        inGraphics.FillEllipse(CurrentBrush, inOffset.X + Corner.X * inScale, inOffset.Y + Corner.Y * inScale, Radius * 2 * inScale, Radius * 2 * inScale);
                        
                        if (_borderColor.A > 0)
                        {
                            inGraphics.DrawEllipse(CurrentPen, inOffset.X + Corner.X * inScale, inOffset.Y + Corner.Y * inScale, Radius * 2 * inScale, Radius * 2 * inScale);
                            //Button style
                            if (Behavior == AreaBehavior.Button)
                            {
                                inGraphics.DrawEllipse(CurrentPen, inOffset.X + Corner.X * inScale + 2, inOffset.Y + Corner.Y * inScale + 2, Radius * 2 * inScale - 4, Radius * 2 * inScale - 4);
                            }
                        }

                        break;

                    case ShapeType.Polygon:
                        //Polygon Shape
                        Polygon2 poly = _shape as Polygon2;
                        if (Points.Count > 2)
                        {
                            PointF[] points = GeoConverter.Convert(Points, inOffset, inScale);
                            inGraphics.FillPolygon(CurrentBrush, points);
                            if (_borderColor.A > 0)
                            {
                                inGraphics.DrawPolygon(CurrentPen, points);
                            }
                        }
                        break;

                    case ShapeType.Rectangle:
                        //Rectangle Shape
                            GeometryLib.Rectangle rect = _shape as GeometryLib.Rectangle;
                            Vector2 corner = inOffset + (rect.Corner * inScale);
                            inGraphics.FillRectangle(CurrentBrush, corner.X, corner.Y, rect.Width * inScale, rect.Height * inScale);

                            if (_borderColor.A > 0)
                            {
                                inGraphics.DrawRectangle(CurrentPen, corner.X, corner.Y, rect.Width * inScale, rect.Height * inScale);

                                //Button style
                                if (Behavior == AreaBehavior.Button)
                                {
                                    inGraphics.DrawRectangle(CurrentPen, corner.X + 2, corner.Y + 2, rect.Width * inScale - 4, rect.Height * inScale - 4);
                                }
                            }
                        break;
                }

                //Image
                if (_image != null && !_imgIsBackground)
                {
                    inGraphics.DrawImage(_image, inOffset.X + (Corner.X + _imgPosition.X) * inScale, inOffset.Y + (Corner.Y + _imgPosition.Y) * inScale, _imgDimension.X * inScale * _imgScaling.X, _imgDimension.Y * inScale * _imgScaling.Y);
                }

                //Text
                if (!string.IsNullOrEmpty(Text))
                {
                    inGraphics.DrawString(_text, _textFont, _textBrush, new PointF(inOffset.X + (Corner.X + _textPosition.X) * inScale, inOffset.Y + (Corner.Y + _textPosition.Y) * inScale));
                }
            }
        }

        public List<Area> Edit()
        {
            Reset();

            List<Area> _newAreas = new List<Area>();

            Area center = new Area(new Circle(Center, 4), this, -1);
            center.AreaColor = Color.Azure;
            center.Name = " " + _name + " center";
            _subComponents.Add(center);
            _newAreas.Add(center);

            int counter = 0;
            foreach (Vector2 vec in Points)
            {
                Area point = new Area(new Circle(vec, 4), this, counter);
                point.AreaColor = Color.Azure;
                point.Name = " " + _name + " point" + counter.ToString();
                _subComponents.Add(point);
                _newAreas.Add(point);
                counter++;
            }

            return _newAreas;
        }

        public void Reset()
        {
            if (IsMovable)
            {
                Center = _refCenter;
            }
        }

        private void SetImage(string _imgPath)
        {
            _image = null;

            if (float.IsNaN(ImgPosition.X))
            {
                ImgPosition.X = 0f;
            }
            if (float.IsNaN(ImgPosition.Y))
            {
                ImgPosition.Y = 0f;
            }

            if(_imgPath == "")
            {
                _image = null;
                return;
            }

            if (_map != null)
            {
                _imgPath = _map.GetAbsolutePath(_imgPath);

                if (File.Exists(_imgPath))
                {
                    try
                    {
                        FileStream stream = new FileStream(_imgPath, FileMode.Open, FileAccess.Read);
                        _image = Image.FromStream(stream);
                        stream.Close();
                    }
                    catch (Exception) { }
                }
            }

            if (_image == null)
            {
                _image = TK.GeometryLib.AreaMapFramework.Properties.Resources.no_image;
            }

            _imgDimension.X = _image.Width;
            _imgDimension.Y = _image.Height;
        }

        public void Offset(Vector2 inVec)
        {
           _shape.Offset(inVec);
           if (IsEditing)
           {
               SetRefCenter();
           }
        }

        public void SetRefCenter()
        {
            Vector2 center = _shape.Center;
            _refCenter = new Vector2(center.X, center.Y);
        }

        public override string ToString()
        {
            return _name;
        }
      
        internal void Clamp(float minX, float maxX, float minY, float maxY)
        {
            _shape.Clamp(minX, maxX, minY, maxY);
        }

        public void ClampCenter(float minX, float maxX, float minY, float maxY)
        {
            _shape.ClampCenter(minX, maxX, minY, maxY);
        }

        public Area Clone()
        {
            Area clone = new Area((Shape2)_shape.Clone(), _behavior);

            clone.Name = _name;
            clone.Text = _text;
            clone.TextFont = _textFont;
            clone.TextPosition = (Vector2)_textPosition.Clone();
            clone.TextColor = _textColor; 
            clone.RealType = _type;
            clone.AreaColor = _areaColor;
            clone.Behavior = _behavior;
            clone.Description = _description;
            clone.MetaData = _metaData;
            clone.ImgPath = _imgPath;
            clone.ImgScaling = (Vector2)_imgScaling.Clone();
            clone.ImgPosition = (Vector2)_imgPosition.Clone();
            clone.ImgIsBackground = _imgIsBackground;
            clone.IsActive = _isActive;
            clone.IsVisible = _isVisible;
            clone.MinX = _minX;
            clone.ValueSuggestedMaxX = _maxSuggestedX;
            clone.ValueSuggestedMaxY = _maxSuggestedY;
            clone.MaxX = _maxX;
            clone.MinY = _minY;
            clone.MaxY = _maxY;
            clone.XParam = _xParam;
            clone.YParam = _yParam;
            clone.XMultiply = _xMultiply;
            clone.YMultiply = _yMultiply;
            clone.XYSymmetryMode = _xYSymmetryMode;

            return clone;
        }

        public Vector2 GetSize()
        {
            return _shape.GetSize();
        }

        public void Scale(Vector2 inScale)
        {
            _shape.Scale(inScale);
        }

        internal void Scale(Vector2 inScaling, Vector2 inReference)
        {
            _shape.Scale(inScaling, inReference);
        }

        public void Mirror()
        {
            _shape.Mirror();
        }

        public void Mirror(Vector2 inCenter)
        {
            _shape.Mirror(inCenter);
        }

        internal void Limit()
        {
            float Xval = ValueX;
            float Yval = ValueY;

            if (ValueSuggestedMaxX != ValueSuggestedMinX && (Xval > ValueSuggestedMaxX || Xval < ValueSuggestedMinX))
            {
                ValueX = Math.Min(Math.Max(Xval, ValueSuggestedMinX), ValueSuggestedMaxX);
            }

            if (ValueSuggestedMaxY != ValueSuggestedMinY && (Yval > ValueSuggestedMaxY || Yval < ValueSuggestedMinY))
            {
                ValueY = Math.Min(Math.Max(Yval, ValueSuggestedMinY), ValueSuggestedMaxY);
            }
        }

        internal bool HasLimits()
        {
            return (ValueSuggestedMaxX != ValueSuggestedMinX && (ValueMaxX > ValueSuggestedMaxX || ValueMinX < ValueSuggestedMinX)) ||
                (ValueSuggestedMaxY != ValueSuggestedMinY && (ValueMaxY > ValueSuggestedMaxY || ValueMinY < ValueSuggestedMinY));
        }
    }
}
