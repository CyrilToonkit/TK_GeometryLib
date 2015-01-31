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
            TK.GeometryLib.AreaMapFramework.SynopticDCCHandler synopticDCCHandler1 = new TK.GeometryLib.AreaMapFramework.SynopticDCCHandler();
            TK.GeometryLib.AreaMapFramework.SynopticDCCHandler synopticDCCHandler2 = new TK.GeometryLib.AreaMapFramework.SynopticDCCHandler();
            this.collapsibleGroup1 = new TK.GraphComponents.CollapsibleGroup();
            this.mainPanelAreaMapControl = new TK.SynopticLib.DCCAreaMapControl();
            this.synopticAreaMapControl = new TK.SynopticLib.DCCAreaMapControl();
            this.collapsibleGroup1.SuspendLayout();
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
            this.collapsibleGroup1.Size = new System.Drawing.Size(198, 556);
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
            this.mainPanelAreaMapControl.Size = new System.Drawing.Size(192, 537);
            this.mainPanelAreaMapControl.TabIndex = 1;
            this.mainPanelAreaMapControl.Load += new System.EventHandler(this.mainPanelAreaMapControl_Load);
            // 
            // synopticAreaMapControl
            // 
            this.synopticAreaMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.synopticAreaMapControl.GridOnTop = false;
            synopticDCCHandler2.ModelName = "No model !";
            this.synopticAreaMapControl.Handler = synopticDCCHandler2;
            this.synopticAreaMapControl.IsDocked = true;
            this.synopticAreaMapControl.Location = new System.Drawing.Point(198, 0);
            this.synopticAreaMapControl.Name = "synopticAreaMapControl";
            this.synopticAreaMapControl.OptionsShowing = true;
            this.synopticAreaMapControl.ShowGrid = false;
            this.synopticAreaMapControl.Size = new System.Drawing.Size(387, 556);
            this.synopticAreaMapControl.TabIndex = 1;
            // 
            // SynopticUCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.Controls.Add(this.synopticAreaMapControl);
            this.Controls.Add(this.collapsibleGroup1);
            this.Name = "SynopticUCtrl";
            this.Size = new System.Drawing.Size(585, 556);
            this.Load += new System.EventHandler(this.SynopticUCtrl_Load);
            this.SizeChanged += new System.EventHandler(this.SynopticUCtrl_SizeChanged);
            this.collapsibleGroup1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TK.GraphComponents.CollapsibleGroup collapsibleGroup1;
        private DCCAreaMapControl mainPanelAreaMapControl;
        private DCCAreaMapControl synopticAreaMapControl;
    }
}
