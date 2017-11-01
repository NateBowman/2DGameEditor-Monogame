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
            this.SuspendLayout();
            // 
            // textBox_MGCBContent
            // 
            this.textBox_MGCBContent.Location = new System.Drawing.Point(12, 12);
            this.textBox_MGCBContent.Multiline = true;
            this.textBox_MGCBContent.Name = "textBox_MGCBContent";
            this.textBox_MGCBContent.Size = new System.Drawing.Size(624, 338);
            this.textBox_MGCBContent.TabIndex = 0;
            // 
            // button_Import
            // 
            this.button_Import.Location = new System.Drawing.Point(25, 403);
            this.button_Import.Name = "button_Import";
            this.button_Import.Size = new System.Drawing.Size(798, 70);
            this.button_Import.TabIndex = 1;
            this.button_Import.Text = "Import & Build";
            this.button_Import.UseVisualStyleBackColor = true;
            this.button_Import.Click += new System.EventHandler(this.button_Import_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // frm_Import
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 485);
            this.Controls.Add(this.button_Import);
            this.Controls.Add(this.textBox_MGCBContent);
            this.Name = "frm_Import";
            this.Text = "frm_Import";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_MGCBContent;
        private System.Windows.Forms.Button button_Import;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}