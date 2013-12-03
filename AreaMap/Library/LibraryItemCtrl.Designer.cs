namespace TK.GeometryLib.AreaMapFramework.Library
{
    partial class LibraryItemCtrl
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
            this.removeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 26);
            // 
            // removeMenuItem
            // 
            this.removeMenuItem.Name = "removeMenuItem";
            this.removeMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeMenuItem.Text = "Remove";
            this.removeMenuItem.Click += new System.EventHandler(this.removeMenuItem_Click);
            // 
            // LibraryItemCtrl
            // 
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Size = new System.Drawing.Size(70, 70);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LibraryItemCtrl_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LibraryItemCtrl_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.LibraryItemCtrl_MouseUp);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem removeMenuItem;
    }
}
