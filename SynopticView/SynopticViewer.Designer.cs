namespace TK.GeometryLib.SynopticView
{
    partial class SynopticViewer
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
            this.areaMapControl1 = new TK.GeometryLib.AreaMapFramework.AreaMapControl();
            this.SuspendLayout();
            // 
            // areaMapControl1
            // 
            this.areaMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.areaMapControl1.IsDocked = true;
            this.areaMapControl1.Location = new System.Drawing.Point(0, 0);
            this.areaMapControl1.Name = "areaMapControl1";
            this.areaMapControl1.Size = new System.Drawing.Size(711, 518);
            this.areaMapControl1.TabIndex = 0;
            // 
            // SynopticViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.ClientSize = new System.Drawing.Size(711, 518);
            this.Controls.Add(this.areaMapControl1);
            this.Name = "SynopticViewer";
            this.Text = "SynopticViewer";
            this.ResumeLayout(false);

        }

        #endregion

        private TK.GeometryLib.AreaMapFramework.AreaMapControl areaMapControl1;
    }
}