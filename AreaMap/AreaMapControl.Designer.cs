namespace TK.GeometryLib.AreaMapFramework
{
    partial class AreaMapControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AreaMapControl));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.areaMapContainer = new System.Windows.Forms.Panel();
            this.collapsibleGroup1 = new TK.GraphComponents.CollapsibleGroup();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.selectModeDD = new TK.GraphComponents.TKDropDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.completeSlider1 = new TK.GraphComponents.CompleteSlider();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.allwaysOnTop = new System.Windows.Forms.CheckBox();
            this.showAllCB = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.areaMapContainer.SuspendLayout();
            this.collapsibleGroup1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(581, 391);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseUp);
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(573, 365);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(573, 365);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // areaMapContainer
            // 
            this.areaMapContainer.Controls.Add(this.tabControl1);
            this.areaMapContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.areaMapContainer.Location = new System.Drawing.Point(0, 0);
            this.areaMapContainer.Name = "areaMapContainer";
            this.areaMapContainer.Size = new System.Drawing.Size(581, 391);
            this.areaMapContainer.TabIndex = 1;
            this.areaMapContainer.SizeChanged += new System.EventHandler(this.areaMapContainer_SizeChanged);
            // 
            // collapsibleGroup1
            // 
            this.collapsibleGroup1.AllowResize = false;
            this.collapsibleGroup1.Collapsed = true;
            this.collapsibleGroup1.CollapseOnClick = true;
            this.collapsibleGroup1.Controls.Add(this.tableLayoutPanel1);
            this.collapsibleGroup1.Controls.Add(this.tableLayoutPanel2);
            this.collapsibleGroup1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.collapsibleGroup1.DockingChanges = TK.GraphComponents.DockingPossibilities.None;
            this.collapsibleGroup1.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.collapsibleGroup1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.collapsibleGroup1.Location = new System.Drawing.Point(0, 391);
            this.collapsibleGroup1.Name = "collapsibleGroup1";
            this.collapsibleGroup1.OpenedBaseHeight = 97;
            this.collapsibleGroup1.OpenedBaseWidth = 200;
            this.collapsibleGroup1.Size = new System.Drawing.Size(581, 26);
            this.collapsibleGroup1.TabIndex = 0;
            this.collapsibleGroup1.TabStop = false;
            this.collapsibleGroup1.Text = "Options";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.selectModeDD, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkBox1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.completeSlider1, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 53);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 1F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(575, 0);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(60, 1);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 18, 0, 0);
            this.label1.Size = new System.Drawing.Size(60, 1);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select";
            // 
            // selectModeDD
            // 
            this.selectModeDD.AllowCustomValue = false;
            this.selectModeDD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(112)))), ((int)(((byte)(112)))), ((int)(((byte)(112)))));
            this.selectModeDD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectModeDD.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.selectModeDD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.selectModeDD.Items = ((System.Collections.Generic.List<object>)(resources.GetObject("selectModeDD.Items")));
            this.selectModeDD.Location = new System.Drawing.Point(63, 16);
            this.selectModeDD.Margin = new System.Windows.Forms.Padding(3, 16, 3, 3);
            this.selectModeDD.Name = "selectModeDD";
            this.selectModeDD.ReadOnly = true;
            this.selectModeDD.SelectedIndex = -1;
            this.selectModeDD.Size = new System.Drawing.Size(79, 23);
            this.selectModeDD.TabIndex = 3;
            this.selectModeDD.Text = "Rectangle";
            this.selectModeDD.TextChanged += new System.EventHandler(this.tkDropDown1_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox1.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.checkBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.checkBox1.Location = new System.Drawing.Point(148, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.checkBox1.Size = new System.Drawing.Size(84, 1);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "ZoomAuto";
            this.checkBox1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // completeSlider1
            // 
            this.completeSlider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.completeSlider1.ButtonName = "sliderButton";
            this.completeSlider1.ButtonStatus = TK.GraphComponents.SliderButtonStatus.Empty;
            this.completeSlider1.Decimals = 0;
            this.completeSlider1.DefaultValue = 0;
            this.completeSlider1.DisplayDefault = false;
            this.completeSlider1.DisplayEditBox = true;
            this.completeSlider1.DisplayEditBoxBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.completeSlider1.DisplayEditButton = false;
            this.completeSlider1.DisplayFrames = true;
            this.completeSlider1.DisplayLabel = true;
            this.completeSlider1.DisplayTexts = true;
            this.completeSlider1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.completeSlider1.DoubleDefaultValue = 0;
            this.completeSlider1.DoubleMaximum = 500;
            this.completeSlider1.DoubleMinimum = 10;
            this.completeSlider1.DoubleValue = 100;
            this.completeSlider1.EndText = "%";
            this.completeSlider1.Font = new System.Drawing.Font("Segoe Condensed", 9F);
            this.completeSlider1.ForeColor = System.Drawing.SystemColors.Control;
            this.completeSlider1.FramesLabelsDynamicFrequency = false;
            this.completeSlider1.FramesLabelsFrequency = 0;
            this.completeSlider1.HasDefault = false;
            this.completeSlider1.LabelText = "Zoom";
            this.completeSlider1.Location = new System.Drawing.Point(235, 0);
            this.completeSlider1.Margin = new System.Windows.Forms.Padding(0);
            this.completeSlider1.Maximum = 500;
            this.completeSlider1.Minimum = 10;
            this.completeSlider1.Name = "completeSlider1";
            this.completeSlider1.ShowIntervals = true;
            this.completeSlider1.Size = new System.Drawing.Size(340, 1);
            this.completeSlider1.SliderFont = new System.Drawing.Font("Consolas", 9.75F);
            this.completeSlider1.SliderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.completeSlider1.StartText = "%";
            this.completeSlider1.TabIndex = 5;
            this.completeSlider1.TickFrequency = 20;
            this.completeSlider1.Value = 100;
            this.completeSlider1.ValueChanged += new System.EventHandler(this.completeSlider1_ValueChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.allwaysOnTop, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.showAllCB, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(575, 34);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // allwaysOnTop
            // 
            this.allwaysOnTop.AutoSize = true;
            this.allwaysOnTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.allwaysOnTop.Location = new System.Drawing.Point(290, 3);
            this.allwaysOnTop.Name = "allwaysOnTop";
            this.allwaysOnTop.Size = new System.Drawing.Size(282, 28);
            this.allwaysOnTop.TabIndex = 3;
            this.allwaysOnTop.Text = "Always on top";
            this.allwaysOnTop.UseVisualStyleBackColor = true;
            this.allwaysOnTop.CheckedChanged += new System.EventHandler(this.allwaysOnTop_CheckedChanged);
            // 
            // showAllCB
            // 
            this.showAllCB.AutoSize = true;
            this.showAllCB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.showAllCB.Location = new System.Drawing.Point(3, 3);
            this.showAllCB.Name = "showAllCB";
            this.showAllCB.Size = new System.Drawing.Size(281, 28);
            this.showAllCB.TabIndex = 2;
            this.showAllCB.Text = "All visible";
            this.showAllCB.UseVisualStyleBackColor = true;
            this.showAllCB.CheckedChanged += new System.EventHandler(this.showAllCB_CheckedChanged);
            // 
            // AreaMapControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.areaMapContainer);
            this.Controls.Add(this.collapsibleGroup1);
            this.Name = "AreaMapControl";
            this.Size = new System.Drawing.Size(581, 417);
            this.tabControl1.ResumeLayout(false);
            this.areaMapContainer.ResumeLayout(false);
            this.collapsibleGroup1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.TabControl tabControl1;
        private TK.GraphComponents.CollapsibleGroup collapsibleGroup1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private TK.GraphComponents.TKDropDown selectModeDD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private TK.GraphComponents.CompleteSlider completeSlider1;
        private System.Windows.Forms.Panel areaMapContainer;
        private System.Windows.Forms.CheckBox showAllCB;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox allwaysOnTop;
    }
}
