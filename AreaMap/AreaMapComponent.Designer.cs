namespace TK.GeometryLib.AreaMapFramework
{
    partial class AreaMapComponent
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.resetMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addPointMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.symLRMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.symRLMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deletePointToolstripmenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.limitValues = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.AccessibleName = "";
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetMenuItem,
            this.addPointMenuItem,
            this.symLRMenuItem,
            this.symRLMenuItem,
            this.limitValues});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(206, 114);
            // 
            // resetMenuItem
            // 
            this.resetMenuItem.Name = "resetMenuItem";
            this.resetMenuItem.Size = new System.Drawing.Size(205, 22);
            this.resetMenuItem.Text = "Reset";
            this.resetMenuItem.Click += new System.EventHandler(this.resetMenuItem_Click);
            // 
            // addPointMenuItem
            // 
            this.addPointMenuItem.Name = "addPointMenuItem";
            this.addPointMenuItem.Size = new System.Drawing.Size(205, 22);
            this.addPointMenuItem.Text = "Add point";
            this.addPointMenuItem.Click += new System.EventHandler(this.addPointMenuItem_Click);
            // 
            // symLRMenuItem
            // 
            this.symLRMenuItem.Name = "symLRMenuItem";
            this.symLRMenuItem.Size = new System.Drawing.Size(205, 22);
            this.symLRMenuItem.Text = "Sym L->R";
            this.symLRMenuItem.Click += new System.EventHandler(this.symLRMenuItem_Click);
            // 
            // symRLMenuItem
            // 
            this.symRLMenuItem.Name = "symRLMenuItem";
            this.symRLMenuItem.Size = new System.Drawing.Size(205, 22);
            this.symRLMenuItem.Text = "Sym R->L";
            this.symRLMenuItem.Click += new System.EventHandler(this.symRLMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.AccessibleName = "";
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deletePointToolstripmenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(139, 26);
            // 
            // deletePointToolstripmenuItem
            // 
            this.deletePointToolstripmenuItem.Name = "deletePointToolstripmenuItem";
            this.deletePointToolstripmenuItem.Size = new System.Drawing.Size(138, 22);
            this.deletePointToolstripmenuItem.Text = "Delete point";
            this.deletePointToolstripmenuItem.Click += new System.EventHandler(this.deletePointMenuItem_Click);
            // 
            // limitValues
            // 
            this.limitValues.Name = "limitValues";
            this.limitValues.Size = new System.Drawing.Size(205, 22);
            this.limitValues.Text = "Reset in suggested range";
            this.limitValues.Click += new System.EventHandler(this.limitValuesMenuItem_Click);
            // 
            // AreaMapComponent
            // 
            this.BackgroundImageChanged += new System.EventHandler(this.AreaMap_BackgroundImageChanged);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AreaMap_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AreaMap_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AreaMap_MouseUp);
            this.SizeChanged += new System.EventHandler(this.AreaMap_SizeChanged);
            this.MouseEnter += new System.EventHandler(this.AreaMapComponent_MouseEnter);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem resetMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem deletePointToolstripmenuItem;
        public System.Windows.Forms.ToolStripMenuItem addPointMenuItem;
        private System.Windows.Forms.ToolStripMenuItem symLRMenuItem;
        private System.Windows.Forms.ToolStripMenuItem symRLMenuItem;
        private System.Windows.Forms.ToolStripMenuItem limitValues;
    }
}
