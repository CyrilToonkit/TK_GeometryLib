using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TK.GeometryLib;
using System.Drawing;

namespace TK.GeometryLib.AreaMapFramework.Library
{
    public partial class LibraryItemCtrl : Panel
    {
        public LibraryItemCtrl(LibraryUCtrl inLibrary, Area inArea)
        {
            InitializeComponent();
            _lib = inLibrary;
            _area = inArea;

        }

        public LibraryItemCtrl()
        {
            InitializeComponent();
        }

        public LibraryItemCtrl(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        LibraryUCtrl _lib = null;
        Area _area = null;

        bool _isDragging = false;
        Point clickPoint = new Point(-1,-1);

        public Area Area
        {
            get { return _area; }
        }

        void LibraryItemCtrl_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!_isDragging && clickPoint.X != -1)
            {
                if ((Math.Abs(e.X - clickPoint.X) + Math.Abs(e.Y - clickPoint.Y)) > 2)
                {
                    DoDragDrop(Area.Clone(), DragDropEffects.Copy);
                    _isDragging = true;
                }
            }
        }

        void LibraryItemCtrl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (_isDragging)
            {
                _isDragging = false;
            }

            clickPoint = new Point(e.X, e.Y);
        }

        void LibraryItemCtrl_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!_isDragging)
            {
                if (e.Button == MouseButtons.Right)
                {
                    contextMenuStrip1.Show(PointToScreen(e.Location));
                }
            }

            _isDragging = false;
            clickPoint = new Point(-1, -1);
        }

        void removeMenuItem_Click(object sender, System.EventArgs e)
        {
            _lib.RemoveItem(this);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);

            if (_area != null)
            {
                Vector2 size = _area.GetSize();
                float scale = Math.Min(Width / size.X, Height / size.Y);
                _area.Center = new Vector2(((float)Width / 2f) * (1f/scale), ((float)Height / 2f) * (1f/scale));
                _area.Draw(e.Graphics, Vector2.Null, scale);

                SizeF fontSize = e.Graphics.MeasureString(_area.Name, AreaMapFramework.Properties.Settings.Default.LittleFont);

                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(50, Color.White)), 1, 1, Width - 2, fontSize.Height + 4);
                e.Graphics.DrawString(_area.Name, AreaMapFramework.Properties.Settings.Default.LittleFont, Brushes.Black, new PointF(((float)Width - fontSize.Width)/2f,3f));
            }
        }
    }
}
