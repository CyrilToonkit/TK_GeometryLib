namespace TK.GeometryLib.SynopticView
{
    partial class SynopticEditor
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.areaMapControl1 = new TK.GeometryLib.AreaMapFramework.AreaMapEditor();
            this.SuspendLayout();
            // 
            // areaMapControl1
            // 
            this.areaMapControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.areaMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.areaMapControl1.GridOnTop = false;
            this.areaMapControl1.Location = new System.Drawing.Point(0, 0);
            this.areaMapControl1.Name = "areaMapControl1";
            this.areaMapControl1.ShowGrid = false;
            this.areaMapControl1.Size = new System.Drawing.Size(722, 507);
            this.areaMapControl1.SyncSelection = true;
            this.areaMapControl1.TabIndex = 0;
            this.areaMapControl1.Load += new System.EventHandler(this.areaMapControl1_Load);
            // 
            // SynopticEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(722, 507);
            this.Controls.Add(this.areaMapControl1);
            this.Name = "SynopticEditor";
            this.Text = "SynopTiK Editor V0.0";
            this.ResumeLayout(false);

        }

        #endregion

        private AreaMapFramework.AreaMapEditor areaMapControl1;

    }
}

