namespace GameEditor
{
    partial class formEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formEditor));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.btnSelectMode = new System.Windows.Forms.ToolStripButton();
            this.btnMoveMode = new System.Windows.Forms.ToolStripButton();
            this.btnPlaceMode = new System.Windows.Forms.ToolStripButton();
            this.btnScaleMode = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.trackBarZoom = new System.Windows.Forms.TrackBar();
            this.labelZoomFactor = new System.Windows.Forms.Label();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.editorControl_GameView = new WinFormsGraphicsDevice.EditorControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button_RemoveAsset = new System.Windows.Forms.Button();
            this.button_ImportAsset = new System.Windows.Forms.Button();
            this.listBox_Assets = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.treeViewHierarchy = new System.Windows.Forms.TreeView();
            this.menuStrip1.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarZoom)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItemHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1139, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolStripMenuItemHelp
            // 
            this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
            this.toolStripMenuItemHelp.Size = new System.Drawing.Size(44, 20);
            this.toolStripMenuItemHelp.Text = "Help";
            this.toolStripMenuItemHelp.Click += new System.EventHandler(this.toolStripMenuItemHelp_Click);
            // 
            // toolStripMain
            // 
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSelectMode,
            this.btnMoveMode,
            this.btnPlaceMode,
            this.btnScaleMode});
            this.toolStripMain.Location = new System.Drawing.Point(0, 24);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(1139, 25);
            this.toolStripMain.TabIndex = 2;
            this.toolStripMain.Text = "toolStrip1";
            // 
            // btnSelectMode
            // 
            this.btnSelectMode.Checked = true;
            this.btnSelectMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnSelectMode.Image = ((System.Drawing.Image)(resources.GetObject("btnSelectMode.Image")));
            this.btnSelectMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectMode.Name = "btnSelectMode";
            this.btnSelectMode.Size = new System.Drawing.Size(58, 22);
            this.btnSelectMode.Text = "Select";
            this.btnSelectMode.ToolTipText = "Activates Select Mode";
            this.btnSelectMode.Click += new System.EventHandler(this.btnSelectMode_Click);
            // 
            // btnMoveMode
            // 
            this.btnMoveMode.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveMode.Image")));
            this.btnMoveMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveMode.Name = "btnMoveMode";
            this.btnMoveMode.Size = new System.Drawing.Size(57, 22);
            this.btnMoveMode.Text = "Move";
            this.btnMoveMode.ToolTipText = "Activates Move Mode";
            this.btnMoveMode.Click += new System.EventHandler(this.btnMoveMode_Click);
            // 
            // btnPlaceMode
            // 
            this.btnPlaceMode.Image = ((System.Drawing.Image)(resources.GetObject("btnPlaceMode.Image")));
            this.btnPlaceMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPlaceMode.Name = "btnPlaceMode";
            this.btnPlaceMode.Size = new System.Drawing.Size(55, 22);
            this.btnPlaceMode.Text = "Place";
            this.btnPlaceMode.ToolTipText = "Activates Place Mode";
            this.btnPlaceMode.Click += new System.EventHandler(this.btnPlaceMode_Click);
            // 
            // btnScaleMode
            // 
            this.btnScaleMode.Image = ((System.Drawing.Image)(resources.GetObject("btnScaleMode.Image")));
            this.btnScaleMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnScaleMode.Name = "btnScaleMode";
            this.btnScaleMode.Size = new System.Drawing.Size(54, 22);
            this.btnScaleMode.Text = "Scale";
            this.btnScaleMode.Click += new System.EventHandler(this.btnScaleMode_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.Location = new System.Drawing.Point(685, 5);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 655);
            this.vScrollBar1.TabIndex = 4;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(240, 487);
            this.propertyGrid1.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.vScrollBar1);
            this.panel1.Controls.Add(this.hScrollBar1);
            this.panel1.Controls.Add(this.editorControl_GameView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(182, 49);
            this.panel1.MinimumSize = new System.Drawing.Size(20, 20);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(707, 686);
            this.panel1.TabIndex = 6;
            // 
            // panel4
            // 
            this.panel4.AutoSize = true;
            this.panel4.Controls.Add(this.trackBarZoom);
            this.panel4.Controls.Add(this.labelZoomFactor);
            this.panel4.Location = new System.Drawing.Point(8, 8);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(45, 108);
            this.panel4.TabIndex = 10;
            // 
            // trackBarZoom
            // 
            this.trackBarZoom.BackColor = System.Drawing.SystemColors.Control;
            this.trackBarZoom.Dock = System.Windows.Forms.DockStyle.Left;
            this.trackBarZoom.LargeChange = 10;
            this.trackBarZoom.Location = new System.Drawing.Point(0, 0);
            this.trackBarZoom.Maximum = 400;
            this.trackBarZoom.Name = "trackBarZoom";
            this.trackBarZoom.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarZoom.Size = new System.Drawing.Size(45, 95);
            this.trackBarZoom.TabIndex = 9;
            this.trackBarZoom.TabStop = false;
            this.trackBarZoom.TickFrequency = 100;
            this.trackBarZoom.Value = 100;
            this.trackBarZoom.ValueChanged += new System.EventHandler(this.trackBarZoom_ValueChanged);
            // 
            // labelZoomFactor
            // 
            this.labelZoomFactor.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelZoomFactor.Location = new System.Drawing.Point(0, 95);
            this.labelZoomFactor.Name = "labelZoomFactor";
            this.labelZoomFactor.Size = new System.Drawing.Size(45, 13);
            this.labelZoomFactor.TabIndex = 10;
            this.labelZoomFactor.Text = "1";
            this.labelZoomFactor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar1.Location = new System.Drawing.Point(5, 660);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(697, 21);
            this.hScrollBar1.TabIndex = 5;
            // 
            // editorControl_GameView
            // 
            this.editorControl_GameView.Camera = null;
            this.editorControl_GameView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorControl_GameView.Location = new System.Drawing.Point(5, 5);
            this.editorControl_GameView.Name = "editorControl_GameView";
            this.editorControl_GameView.Size = new System.Drawing.Size(697, 676);
            this.editorControl_GameView.TabIndex = 0;
            this.editorControl_GameView.Text = "editorControl1";
            this.editorControl_GameView.DragDrop += new System.Windows.Forms.DragEventHandler(this.formEditor_DragEnter);
            this.editorControl_GameView.DragEnter += new System.Windows.Forms.DragEventHandler(this.formEditor_DragEnter);
            this.editorControl_GameView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.editorControl_GameView_MouseDown);
            this.editorControl_GameView.MouseEnter += new System.EventHandler(this.editorControl_GameView_MouseEnter);
            this.editorControl_GameView.MouseLeave += new System.EventHandler(this.editorControl_GameView_MouseLeave);
            this.editorControl_GameView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.editorControl_GameView_MouseMove);
            this.editorControl_GameView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.editorControl_GameView_MouseUp);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.splitContainer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(889, 49);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(250, 686);
            this.panel2.TabIndex = 7;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(5, 5);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AllowDrop = true;
            this.splitContainer1.Panel1.Controls.Add(this.propertyGrid1);
            this.splitContainer1.Panel1MinSize = 100;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Panel2.Controls.Add(this.listBox_Assets);
            this.splitContainer1.Panel2MinSize = 100;
            this.splitContainer1.Size = new System.Drawing.Size(240, 676);
            this.splitContainer1.SplitterDistance = 487;
            this.splitContainer1.TabIndex = 9;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.button_RemoveAsset, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_ImportAsset, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 147);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(240, 38);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // button_RemoveAsset
            // 
            this.button_RemoveAsset.AutoSize = true;
            this.button_RemoveAsset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_RemoveAsset.Location = new System.Drawing.Point(123, 3);
            this.button_RemoveAsset.Name = "button_RemoveAsset";
            this.button_RemoveAsset.Size = new System.Drawing.Size(114, 32);
            this.button_RemoveAsset.TabIndex = 8;
            this.button_RemoveAsset.Text = "Remove Asset";
            this.button_RemoveAsset.UseVisualStyleBackColor = true;
            this.button_RemoveAsset.Click += new System.EventHandler(this.button_RemoveAsset_Click);
            // 
            // button_ImportAsset
            // 
            this.button_ImportAsset.AutoSize = true;
            this.button_ImportAsset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_ImportAsset.Location = new System.Drawing.Point(3, 3);
            this.button_ImportAsset.Name = "button_ImportAsset";
            this.button_ImportAsset.Size = new System.Drawing.Size(114, 32);
            this.button_ImportAsset.TabIndex = 7;
            this.button_ImportAsset.Text = "Import Asset";
            this.button_ImportAsset.UseVisualStyleBackColor = true;
            this.button_ImportAsset.Click += new System.EventHandler(this.button_ImportAsset_Click);
            // 
            // listBox_Assets
            // 
            this.listBox_Assets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox_Assets.FormattingEnabled = true;
            this.listBox_Assets.Location = new System.Drawing.Point(0, 0);
            this.listBox_Assets.Name = "listBox_Assets";
            this.listBox_Assets.Size = new System.Drawing.Size(240, 185);
            this.listBox_Assets.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.treeViewHierarchy);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 49);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5);
            this.panel3.Size = new System.Drawing.Size(182, 686);
            this.panel3.TabIndex = 8;
            // 
            // treeViewHierarchy
            // 
            this.treeViewHierarchy.AllowDrop = true;
            this.treeViewHierarchy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewHierarchy.FullRowSelect = true;
            this.treeViewHierarchy.HideSelection = false;
            this.treeViewHierarchy.HotTracking = true;
            this.treeViewHierarchy.Location = new System.Drawing.Point(5, 5);
            this.treeViewHierarchy.Name = "treeViewHierarchy";
            this.treeViewHierarchy.Size = new System.Drawing.Size(172, 676);
            this.treeViewHierarchy.TabIndex = 10;
            this.treeViewHierarchy.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeViewHierarchy_ItemDrag);
            this.treeViewHierarchy.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewHierarchy_NodeMouseClick);
            this.treeViewHierarchy.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeViewHierarchy_DragDrop);
            this.treeViewHierarchy.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeViewHierarchy_DragEnter);
            // 
            // formEditor
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1139, 735);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "formEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "2D Game Editor";
            this.Load += new System.EventHandler(this.formEditor_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.formEditor_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.formEditor_DragEnter);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarZoom)).EndInit();
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripButton btnSelectMode;
        private System.Windows.Forms.ToolStripButton btnMoveMode;
        private System.Windows.Forms.ToolStripButton btnPlaceMode;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private WinFormsGraphicsDevice.EditorControl editorControl_GameView;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox listBox_Assets;
        private System.Windows.Forms.ToolStripButton btnScaleMode;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TreeView treeViewHierarchy;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button button_RemoveAsset;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button_ImportAsset;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelp;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label labelZoomFactor;
        private System.Windows.Forms.TrackBar trackBarZoom;
    }
}

