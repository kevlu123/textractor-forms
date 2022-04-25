namespace TextractorForms {
    partial class MainForm {
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
            this.components = new System.ComponentModel.Container();
            this.attach_Button = new System.Windows.Forms.Button();
            this.textThread_Dropdown = new System.Windows.Forms.ComboBox();
            this.console_Textbox = new System.Windows.Forms.TextBox();
            this.detach_Button = new System.Windows.Forms.Button();
            this.searchHooks_Button = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.processes_Dropdown = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // attach_Button
            // 
            this.attach_Button.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.attach_Button.Location = new System.Drawing.Point(6, 38);
            this.attach_Button.Name = "attach_Button";
            this.attach_Button.Size = new System.Drawing.Size(131, 25);
            this.attach_Button.TabIndex = 1;
            this.attach_Button.Text = "Attach to game";
            this.attach_Button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.attach_Button.UseVisualStyleBackColor = true;
            this.attach_Button.Click += new System.EventHandler(this.AttachPressed);
            // 
            // textThread_Dropdown
            // 
            this.textThread_Dropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.textThread_Dropdown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textThread_Dropdown.FormattingEnabled = true;
            this.textThread_Dropdown.Location = new System.Drawing.Point(143, 7);
            this.textThread_Dropdown.Name = "textThread_Dropdown";
            this.textThread_Dropdown.Size = new System.Drawing.Size(429, 25);
            this.textThread_Dropdown.TabIndex = 2;
            this.textThread_Dropdown.SelectedIndexChanged += new System.EventHandler(this.HookIndexChanged);
            // 
            // console_Textbox
            // 
            this.console_Textbox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.console_Textbox.Location = new System.Drawing.Point(143, 38);
            this.console_Textbox.Multiline = true;
            this.console_Textbox.Name = "console_Textbox";
            this.console_Textbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.console_Textbox.Size = new System.Drawing.Size(429, 335);
            this.console_Textbox.TabIndex = 3;
            // 
            // detach_Button
            // 
            this.detach_Button.Enabled = false;
            this.detach_Button.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detach_Button.Location = new System.Drawing.Point(6, 69);
            this.detach_Button.Name = "detach_Button";
            this.detach_Button.Size = new System.Drawing.Size(131, 25);
            this.detach_Button.TabIndex = 6;
            this.detach_Button.Text = "Detach from game";
            this.detach_Button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.detach_Button.UseVisualStyleBackColor = true;
            // 
            // searchHooks_Button
            // 
            this.searchHooks_Button.Enabled = false;
            this.searchHooks_Button.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchHooks_Button.Location = new System.Drawing.Point(6, 100);
            this.searchHooks_Button.Name = "searchHooks_Button";
            this.searchHooks_Button.Size = new System.Drawing.Size(131, 25);
            this.searchHooks_Button.TabIndex = 11;
            this.searchHooks_Button.Text = "Search for hooks";
            this.searchHooks_Button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.searchHooks_Button.UseVisualStyleBackColor = true;
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.OnTimerTicked);
            // 
            // processes_Dropdown
            // 
            this.processes_Dropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.processes_Dropdown.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.processes_Dropdown.FormattingEnabled = true;
            this.processes_Dropdown.Location = new System.Drawing.Point(6, 7);
            this.processes_Dropdown.Name = "processes_Dropdown";
            this.processes_Dropdown.Size = new System.Drawing.Size(131, 25);
            this.processes_Dropdown.TabIndex = 12;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 379);
            this.Controls.Add(this.processes_Dropdown);
            this.Controls.Add(this.searchHooks_Button);
            this.Controls.Add(this.detach_Button);
            this.Controls.Add(this.console_Textbox);
            this.Controls.Add(this.textThread_Dropdown);
            this.Controls.Add(this.attach_Button);
            this.MinimumSize = new System.Drawing.Size(595, 418);
            this.Name = "MainForm";
            this.Text = "TextractorForms";
            this.ClientSizeChanged += new System.EventHandler(this.OnResize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button attach_Button;
        private System.Windows.Forms.TextBox console_Textbox;
        private System.Windows.Forms.Button detach_Button;
        private System.Windows.Forms.Button searchHooks_Button;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ComboBox textThread_Dropdown;
        private System.Windows.Forms.ComboBox processes_Dropdown;
    }
}

