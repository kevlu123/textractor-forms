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
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.process_Label = new System.Windows.Forms.Label();
            this.zen_Button = new System.Windows.Forms.Button();
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
            this.console_Textbox.BackColor = System.Drawing.SystemColors.Window;
            this.console_Textbox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.console_Textbox.Location = new System.Drawing.Point(143, 38);
            this.console_Textbox.Multiline = true;
            this.console_Textbox.Name = "console_Textbox";
            this.console_Textbox.ReadOnly = true;
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
            this.detach_Button.Click += new System.EventHandler(this.DetachPressed);
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.OnTimerTicked);
            // 
            // process_Label
            // 
            this.process_Label.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.process_Label.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.process_Label.Location = new System.Drawing.Point(6, 7);
            this.process_Label.Name = "process_Label";
            this.process_Label.Size = new System.Drawing.Size(131, 25);
            this.process_Label.TabIndex = 12;
            this.process_Label.Text = "No process";
            this.process_Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // zen_Button
            // 
            this.zen_Button.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zen_Button.Location = new System.Drawing.Point(6, 100);
            this.zen_Button.Name = "zen_Button";
            this.zen_Button.Size = new System.Drawing.Size(131, 25);
            this.zen_Button.TabIndex = 13;
            this.zen_Button.Text = "Zen Mode";
            this.zen_Button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.zen_Button.UseVisualStyleBackColor = true;
            this.zen_Button.Click += new System.EventHandler(this.ZenModeClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 379);
            this.Controls.Add(this.zen_Button);
            this.Controls.Add(this.process_Label);
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
        private System.Windows.Forms.Button detach_Button;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ComboBox textThread_Dropdown;
        private System.Windows.Forms.Label process_Label;
        private System.Windows.Forms.Button zen_Button;
        public System.Windows.Forms.TextBox console_Textbox;
    }
}

