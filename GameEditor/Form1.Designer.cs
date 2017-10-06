namespace GameEditor
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSelectMode = new System.Windows.Forms.ToolStripButton();
            this.btnMoveMode = new System.Windows.Forms.ToolStripButton();
            this.btnPlaceMode = new System.Windows.Forms.ToolStripButton();
            this.editorControl1 = new WinFormsGraphicsDevice.EditorControl();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(869, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSelectMode,
            this.btnMoveMode,
            this.btnPlaceMode});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(869, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
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
            // editorControl1
            // 
            this.editorControl1.Location = new System.Drawing.Point(12, 52);
            this.editorControl1.Name = "editorControl1";
            this.editorControl1.Size = new System.Drawing.Size(260, 237);
            this.editorControl1.TabIndex = 0;
            this.editorControl1.Text = "editorControl1";
            this.editorControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.editorControl1_MouseDown);
            this.editorControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.editorControl1_MouseMove);
            this.editorControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.editorControl1_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 586);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.editorControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private WinFormsGraphicsDevice.EditorControl editorControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSelectMode;
        private System.Windows.Forms.ToolStripButton btnMoveMode;
        private System.Windows.Forms.ToolStripButton btnPlaceMode;
    }
}

