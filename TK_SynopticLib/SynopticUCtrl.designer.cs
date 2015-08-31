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
            TK.GeometryLib.AreaMapFramework.SynopticDCCHandler synopticDCCHandler2 = new TK.GeometryLib.AreaMapFramework.SynopticDCCHandler();
            this.collapsibleGroup1 = new TK.GraphComponents.CollapsibleGroup();
            this.mainPanelAreaMapControl = new TK.SynopticLib.DCCAreaMapControl();
            this.collapsibleGroup2 = new TK.GraphComponents.CollapsibleGroup();
            this.setsLB = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.newSetName = new System.Windows.Forms.TextBox();
            this.remSetBT = new System.Windows.Forms.Button();
            this.addSetBT = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.synopticAreaMapControl = new TK.SynopticLib.DCCAreaMapControl();
            this.collapsibleGroup1.SuspendLayout();
            this.collapsibleGroup2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.collapsibleGroup1.Size = new System.Drawing.Size(203, 556);
            this.collapsibleGroup1.TabIndex = 0;
            this.collapsibleGroup1.TabStop = false;
            this.collapsibleGroup1.Text = "Main panel";
            // 
            // mainPanelAreaMapControl
            // 
            this.mainPanelAreaMapControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.mainPanelAreaMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanelAreaMapControl.GridOnTop = false;
            synopticDCCHandler1.ModelName = "No model !";
            this.mainPanelAreaMapControl.Handler = synopticDCCHandler1;
            this.mainPanelAreaMapControl.IsDocked = true;
            this.mainPanelAreaMapControl.Location = new System.Drawing.Point(3, 16);
            this.mainPanelAreaMapControl.Name = "mainPanelAreaMapControl";
            this.mainPanelAreaMapControl.OptionsShowing = false;
            this.mainPanelAreaMapControl.ShowGrid = false;
            this.mainPanelAreaMapControl.Size = new System.Drawing.Size(197, 537);
            this.mainPanelAreaMapControl.TabIndex = 1;
            this.mainPanelAreaMapControl.Load += new System.EventHandler(this.mainPanelAreaMapControl_Load);
            // 
            // collapsibleGroup2
            // 
            this.collapsibleGroup2.AllowResize = true;
            this.collapsibleGroup2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.collapsibleGroup2.Collapsed = true;
            this.collapsibleGroup2.CollapseOnClick = true;
            this.collapsibleGroup2.Controls.Add(this.setsLB);
            this.collapsibleGroup2.Controls.Add(this.panel1);
            this.collapsibleGroup2.Dock = System.Windows.Forms.DockStyle.Right;
            this.collapsibleGroup2.DockingChanges = TK.GraphComponents.DockingPossibilities.None;
            this.collapsibleGroup2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.collapsibleGroup2.Location = new System.Drawing.Point(568, 0);
            this.collapsibleGroup2.Name = "collapsibleGroup2";
            this.collapsibleGroup2.OpenedBaseHeight = 150;
            this.collapsibleGroup2.OpenedBaseWidth = 151;
            this.collapsibleGroup2.Size = new System.Drawing.Size(17, 556);
            this.collapsibleGroup2.TabIndex = 2;
            this.collapsibleGroup2.TabStop = false;
            this.collapsibleGroup2.Text = "Selection Sets";
            // 
            // setsLB
            // 
            this.setsLB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.setsLB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.setsLB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.setsLB.FormattingEnabled = true;
            this.setsLB.Location = new System.Drawing.Point(3, 36);
            this.setsLB.Name = "setsLB";
            this.setsLB.Size = new System.Drawing.Size(11, 511);
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
            this.panel1.Size = new System.Drawing.Size(11, 20);
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
            this.remSetBT.Location = new System.Drawing.Point(-22, 0);
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
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // synopticAreaMapControl
            // 
            this.synopticAreaMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.synopticAreaMapControl.GridOnTop = false;
            synopticDCCHandler2.ModelName = "No model !";
            this.synopticAreaMapControl.Handler = synopticDCCHandler2;
            this.synopticAreaMapControl.IsDocked = true;
            this.synopticAreaMapControl.Location = new System.Drawing.Point(203, 0);
            this.synopticAreaMapControl.Name = "synopticAreaMapControl";
            this.synopticAreaMapControl.OptionsShowing = true;
            this.synopticAreaMapControl.ShowGrid = false;
            this.synopticAreaMapControl.Size = new System.Drawing.Size(365, 556);
            this.synopticAreaMapControl.TabIndex = 1;
            // 
            // SynopticUCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.Controls.Add(this.synopticAreaMapControl);
            this.Controls.Add(this.collapsibleGroup2);
            this.Controls.Add(this.collapsibleGroup1);
            this.Name = "SynopticUCtrl";
            this.Size = new System.Drawing.Size(585, 556);
            this.Load += new System.EventHandler(this.SynopticUCtrl_Load);
            this.SizeChanged += new System.EventHandler(this.SynopticUCtrl_SizeChanged);
            this.collapsibleGroup1.ResumeLayout(false);
            this.collapsibleGroup2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TK.GraphComponents.CollapsibleGroup collapsibleGroup1;
        private DCCAreaMapControl mainPanelAreaMapControl;
        private DCCAreaMapControl synopticAreaMapControl;
        private TK.GraphComponents.CollapsibleGroup collapsibleGroup2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox newSetName;
        private System.Windows.Forms.Button remSetBT;
        private System.Windows.Forms.Button addSetBT;
        private System.Windows.Forms.ListBox setsLB;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}
