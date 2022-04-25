namespace TextractorForms {
    partial class ZenForm {
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
            this.console_Textbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // console_Textbox
            // 
            this.console_Textbox.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.console_Textbox.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.console_Textbox.ForeColor = System.Drawing.SystemColors.Window;
            this.console_Textbox.Location = new System.Drawing.Point(7, 7);
            this.console_Textbox.Multiline = true;
            this.console_Textbox.Name = "console_Textbox";
            this.console_Textbox.ReadOnly = true;
            this.console_Textbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.console_Textbox.Size = new System.Drawing.Size(1135, 257);
            this.console_Textbox.TabIndex = 4;
            // 
            // ZenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(1197, 276);
            this.Controls.Add(this.console_Textbox);
            this.Name = "ZenForm";
            this.Opacity = 0.8D;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Zen";
            this.TopMost = true;
            this.ClientSizeChanged += new System.EventHandler(this.OnClientSizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox console_Textbox;
    }
}