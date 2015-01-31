using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TK.GeometryLib.SynopticView
{
    public partial class SynopticViewer : Form
    {
        public SynopticViewer()
        {
            InitializeComponent();
            synopticUCtrl1.Handler = new SynopticTestHandler();
            synopticUCtrl1.AddPage("Z:\\ToonKit\\Synoptic\\Head.xml");
        }
    }
}
