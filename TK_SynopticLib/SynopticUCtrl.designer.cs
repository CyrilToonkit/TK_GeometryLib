namespace TK.SynopticLib
{
    partial class SynopticUCtrl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            TK.GeometryLib.AreaMapFramework.SynopticDCCHandler synopticDCCHandler1 = new TK.GeometryLib.AreaMapFramework.SynopticDCCHandler();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SynopticUCtrl));
            TK.GeometryLib.AreaMapFramework.SynopticDCCHandler synopticDCCHandler2 = new TK.GeometryLib.AreaMapFramework.SynopticDCCHandler();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.synopticAreaMapControl = new TK.SynopticLib.DCCAreaMapControl();
            this.setsLB = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.newSetName = new System.Windows.Forms.TextBox();
            this.remSetBT = new System.Windows.Forms.Button();
            this.addSetBT = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.collapsibleGroup1 = new TK.GraphComponents.CollapsibleGroup();
            this.mainPanelAreaMapControl = new TK.SynopticLib.DCCAreaMapControl();
            this.setsSNTV = new TK.GraphComponents.stringNodesTreeView(this.components);
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.collapsibleGroup1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // synopticAreaMapControl
            // 
            this.synopticAreaMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.synopticAreaMapControl.GridOnTop = false;
            synopticDCCHandler1.ModelName = "No model !";
            this.synopticAreaMapControl.Handler = synopticDCCHandler1;
            this.synopticAreaMapControl.IsDocked = true;
            this.synopticAreaMapControl.Location = new System.Drawing.Point(213, 0);
            this.synopticAreaMapControl.Name = "synopticAreaMapControl";
            this.synopticAreaMapControl.OptionsShowing = true;
            this.synopticAreaMapControl.ShowGrid = false;
            this.synopticAreaMapControl.Size = new System.Drawing.Size(354, 556);
            this.synopticAreaMapControl.TabIndex = 1;
            // 
            // setsLB
            // 
            this.setsLB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.setsLB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.setsLB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.setsLB.FormattingEnabled = true;
            this.setsLB.Location = new System.Drawing.Point(3, 36);
            this.setsLB.Name = "setsLB";
            this.setsLB.Size = new System.Drawing.Size(14, 303);
            this.setsLB.TabIndex = 2;
            this.setsLB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.setsLB_MouseUp);
            this.setsLB.SelectedValueChanged += new System.EventHandler(this.setsLB_SelectedValueChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.newSetName);
            this.panel1.Controls.Add(this.remSetBT);
            this.panel1.Controls.Add(this.addSetBT);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(14, 20);
            this.panel1.TabIndex = 1;
            // 
            // newSetName
            // 
            this.newSetName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(112)))));
            this.newSetName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newSetName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.newSetName.Location = new System.Drawing.Point(33, 0);
            this.newSetName.Name = "newSetName";
            this.newSetName.Size = new System.Drawing.Size(0, 20);
            this.newSetName.TabIndex = 0;
            this.newSetName.Text = "QuickSet";
            // 
            // remSetBT
            // 
            this.remSetBT.Dock = System.Windows.Forms.DockStyle.Right;
            this.remSetBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.remSetBT.Location = new System.Drawing.Point(-19, 0);
            this.remSetBT.Name = "remSetBT";
            this.remSetBT.Size = new System.Drawing.Size(33, 20);
            this.remSetBT.TabIndex = 2;
            this.remSetBT.Text = "X";
            this.remSetBT.UseVisualStyleBackColor = true;
            this.remSetBT.Click += new System.EventHandler(this.remSetBT_Click);
            // 
            // addSetBT
            // 
            this.addSetBT.Dock = System.Windows.Forms.DockStyle.Left;
            this.addSetBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addSetBT.Location = new System.Drawing.Point(0, 0);
            this.addSetBT.Name = "addSetBT";
            this.addSetBT.Size = new System.Drawing.Size(33, 20);
            this.addSetBT.TabIndex = 1;
            this.addSetBT.Text = "+";
            this.addSetBT.UseVisualStyleBackColor = true;
            this.addSetBT.Click += new System.EventHandler(this.addSetBT_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripTextBox1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(3, 528);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(14, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.ForeColor = System.Drawing.Color.Black;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 19);
            this.toolStripButton1.Text = "+";
            this.toolStripButton1.Visible = false;
            this.toolStripButton1.Click += new System.EventHandler(this.addSetBT_Click);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox1.Text = "QuickSet";
            this.toolStripTextBox1.Visible = false;
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.ForeColor = System.Drawing.Color.Black;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 19);
            this.toolStripButton2.Text = "X";
            this.toolStripButton2.Visible = false;
            this.toolStripButton2.Click += new System.EventHandler(this.remSetBT_Click);
            // 
            // collapsibleGroup1
            // 
            this.collapsibleGroup1.AllowResize = false;
            this.collapsibleGroup1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.collapsibleGroup1.Collapsed = false;
            this.collapsibleGroup1.CollapseOnClick = true;
            this.collapsibleGroup1.Controls.Add(this.mainPanelAreaMapControl);
            this.collapsibleGroup1.Dock = System.Windows.Forms.DockStyle.Left;
            this.collapsibleGroup1.DockingChanges = TK.GraphComponents.DockingPossibilities.None;
            this.collapsibleGroup1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.collapsibleGroup1.Location = new System.Drawing.Point(0, 0);
            this.collapsibleGroup1.Name = "collapsibleGroup1";
            this.collapsibleGroup1.OpenedBaseHeight = 150;
            this.collapsibleGroup1.OpenedBaseWidth = 200;
            this.collapsibleGroup1.Size = new System.Drawing.Size(223, 557);
            this.collapsibleGroup1.TabIndex = 0;
            this.collapsibleGroup1.TabStop = false;
            this.collapsibleGroup1.Text = "Main panel";
            // 
            // mainPanelAreaMapControl
            // 
            this.mainPanelAreaMapControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.mainPanelAreaMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanelAreaMapControl.GridOnTop = false;
            synopticDCCHandler2.ModelName = "No model !";
            this.mainPanelAreaMapControl.Handler = synopticDCCHandler2;
            this.mainPanelAreaMapControl.IsDocked = true;
            this.mainPanelAreaMapControl.Location = new System.Drawing.Point(3, 16);
            this.mainPanelAreaMapControl.Name = "mainPanelAreaMapControl";
            this.mainPanelAreaMapControl.OptionsShowing = false;
            this.mainPanelAreaMapControl.ShowGrid = false;
            this.mainPanelAreaMapControl.Size = new System.Drawing.Size(217, 538);
            this.mainPanelAreaMapControl.TabIndex = 1;
            this.mainPanelAreaMapControl.Load += new System.EventHandler(this.mainPanelAreaMapControl_Load);
            // 
            // setsSNTV
            // 
            this.setsSNTV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.setsSNTV.CreateRoot = false;
            this.setsSNTV.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.setsSNTV.DrawGrid = true;
            this.setsSNTV.EnableDragNodes = false;
            this.setsSNTV.EnableManageNodes = false;
            this.setsSNTV.EnableRenameNode = true;
            this.setsSNTV.EnableReorderNodes = false;
            this.setsSNTV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.setsSNTV.LineColor = System.Drawing.Color.Empty;
            this.setsSNTV.Location = new System.Drawing.Point(3, 347);
            this.setsSNTV.Name = "setsSNTV";
            this.setsSNTV.PathSeparator = "/";
            this.setsSNTV.Root = null;
            this.setsSNTV.Rows = 10000;
            this.setsSNTV.Size = new System.Drawing.Size(14, 181);
            this.setsSNTV.TabIndex = 3;
            this.setsSNTV.Visible = false;
            this.setsSNTV.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.setsSNTV_AfterSelect);
            this.setsSNTV.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.setsSNTV_NodeMouseClick);
            // 
            // SynopticUCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.Controls.Add(this.collapsibleGroup1);
            this.Name = "SynopticUCtrl";
            this.Size = new System.Drawing.Size(585, 556);
            this.Load += new System.EventHandler(this.SynopticUCtrl_Load);
            this.SizeChanged += new System.EventHandler(this.SynopticUCtrl_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.collapsibleGroup1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TK.GraphComponents.CollapsibleGroup collapsibleGroup1;
        private DCCAreaMapControl mainPanelAreaMapControl;
        private DCCAreaMapControl synopticAreaMapControl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox newSetName;
        private System.Windows.Forms.Button remSetBT;
        private System.Windows.Forms.Button addSetBT;
        private System.Windows.Forms.ListBox setsLB;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private TK.GraphComponents.stringNodesTreeView setsSNTV;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
    }
}
