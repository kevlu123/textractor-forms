namespace TextractorForms {
    partial class AttachForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AttachForm));
            this.process_ListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // process_ListView
            // 
            this.process_ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.process_ListView.FullRowSelect = true;
            this.process_ListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.process_ListView.HideSelection = false;
            this.process_ListView.Location = new System.Drawing.Point(12, 12);
            this.process_ListView.MultiSelect = false;
            this.process_ListView.Name = "process_ListView";
            this.process_ListView.Size = new System.Drawing.Size(433, 426);
            this.process_ListView.TabIndex = 1;
            this.process_ListView.UseCompatibleStateImageBehavior = false;
            this.process_ListView.View = System.Windows.Forms.View.Details;
            this.process_ListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ProcessDoubleClicked);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 6000;
            // 
            // AttachForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 447);
            this.Controls.Add(this.process_ListView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AttachForm";
            this.Text = "Select process";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView process_ListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}