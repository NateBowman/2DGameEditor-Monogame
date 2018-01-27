namespace GameEditor
{
    partial class frm_Import
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
            this.textBox_MGCBContent = new System.Windows.Forms.TextBox();
            this.button_Import = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_MGCBContent
            // 
            this.textBox_MGCBContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_MGCBContent.Location = new System.Drawing.Point(5, 5);
            this.textBox_MGCBContent.Multiline = true;
            this.textBox_MGCBContent.Name = "textBox_MGCBContent";
            this.textBox_MGCBContent.ReadOnly = true;
            this.textBox_MGCBContent.Size = new System.Drawing.Size(313, 475);
            this.textBox_MGCBContent.TabIndex = 0;
            // 
            // button_Import
            // 
            this.button_Import.Dock = System.Windows.Forms.DockStyle.Left;
            this.button_Import.Location = new System.Drawing.Point(0, 0);
            this.button_Import.Margin = new System.Windows.Forms.Padding(5);
            this.button_Import.Name = "button_Import";
            this.button_Import.Padding = new System.Windows.Forms.Padding(5);
            this.button_Import.Size = new System.Drawing.Size(70, 50);
            this.button_Import.TabIndex = 1;
            this.button_Import.Text = "Import && Build";
            this.button_Import.UseVisualStyleBackColor = true;
            this.button_Import.Click += new System.EventHandler(this.button_Import_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Left;
            this.button1.Location = new System.Drawing.Point(70, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 50);
            this.button1.TabIndex = 2;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.button_Import);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(5, 430);
            this.panel2.MinimumSize = new System.Drawing.Size(0, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(313, 50);
            this.panel2.TabIndex = 3;
            // 
            // frm_Import
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 485);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.textBox_MGCBContent);
            this.Name = "frm_Import";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "frm_Import";
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_MGCBContent;
        private System.Windows.Forms.Button button_Import;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
    }
}