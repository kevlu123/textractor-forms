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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.attach_Button = new System.Windows.Forms.Button();
            this.textThread_Dropdown = new System.Windows.Forms.ComboBox();
            this.console_Textbox = new System.Windows.Forms.TextBox();
            this.detach_Button = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.process_Label = new System.Windows.Forms.Label();
            this.zen_Button = new System.Windows.Forms.Button();
            this.translate_Button = new System.Windows.Forms.Button();
            this.manualTranslateInput_Textbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.clipboard_Checkbox = new System.Windows.Forms.CheckBox();
            this.autoTranslate_Checkbox = new System.Windows.Forms.CheckBox();
            this.manualTranslateOutput_Textbox = new System.Windows.Forms.TextBox();
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
            // translate_Button
            // 
            this.translate_Button.Enabled = false;
            this.translate_Button.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.translate_Button.Location = new System.Drawing.Point(6, 187);
            this.translate_Button.Name = "translate_Button";
            this.translate_Button.Size = new System.Drawing.Size(131, 25);
            this.translate_Button.TabIndex = 14;
            this.translate_Button.Text = "Manual Translate";
            this.translate_Button.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.translate_Button.UseVisualStyleBackColor = true;
            this.translate_Button.Click += new System.EventHandler(this.ManualTranslateClicked);
            // 
            // manualTranslateInput_Textbox
            // 
            this.manualTranslateInput_Textbox.BackColor = System.Drawing.SystemColors.Window;
            this.manualTranslateInput_Textbox.Enabled = false;
            this.manualTranslateInput_Textbox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manualTranslateInput_Textbox.Location = new System.Drawing.Point(6, 218);
            this.manualTranslateInput_Textbox.Multiline = true;
            this.manualTranslateInput_Textbox.Name = "manualTranslateInput_Textbox";
            this.manualTranslateInput_Textbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.manualTranslateInput_Textbox.Size = new System.Drawing.Size(131, 71);
            this.manualTranslateInput_Textbox.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(10, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 2);
            this.label1.TabIndex = 16;
            // 
            // clipboard_Checkbox
            // 
            this.clipboard_Checkbox.AutoSize = true;
            this.clipboard_Checkbox.Location = new System.Drawing.Point(12, 131);
            this.clipboard_Checkbox.Name = "clipboard_Checkbox";
            this.clipboard_Checkbox.Size = new System.Drawing.Size(108, 17);
            this.clipboard_Checkbox.TabIndex = 17;
            this.clipboard_Checkbox.Text = "Copy to clipboard";
            this.clipboard_Checkbox.UseVisualStyleBackColor = true;
            // 
            // autoTranslate_Checkbox
            // 
            this.autoTranslate_Checkbox.AutoSize = true;
            this.autoTranslate_Checkbox.Checked = true;
            this.autoTranslate_Checkbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoTranslate_Checkbox.Location = new System.Drawing.Point(13, 154);
            this.autoTranslate_Checkbox.Name = "autoTranslate_Checkbox";
            this.autoTranslate_Checkbox.Size = new System.Drawing.Size(91, 17);
            this.autoTranslate_Checkbox.TabIndex = 18;
            this.autoTranslate_Checkbox.Text = "Auto translate";
            this.autoTranslate_Checkbox.UseVisualStyleBackColor = true;
            // 
            // manualTranslateOutput_Textbox
            // 
            this.manualTranslateOutput_Textbox.BackColor = System.Drawing.SystemColors.Window;
            this.manualTranslateOutput_Textbox.Enabled = false;
            this.manualTranslateOutput_Textbox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manualTranslateOutput_Textbox.Location = new System.Drawing.Point(6, 295);
            this.manualTranslateOutput_Textbox.Multiline = true;
            this.manualTranslateOutput_Textbox.Name = "manualTranslateOutput_Textbox";
            this.manualTranslateOutput_Textbox.ReadOnly = true;
            this.manualTranslateOutput_Textbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.manualTranslateOutput_Textbox.Size = new System.Drawing.Size(131, 78);
            this.manualTranslateOutput_Textbox.TabIndex = 19;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 379);
            this.Controls.Add(this.manualTranslateOutput_Textbox);
            this.Controls.Add(this.autoTranslate_Checkbox);
            this.Controls.Add(this.clipboard_Checkbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.manualTranslateInput_Textbox);
            this.Controls.Add(this.translate_Button);
            this.Controls.Add(this.zen_Button);
            this.Controls.Add(this.process_Label);
            this.Controls.Add(this.detach_Button);
            this.Controls.Add(this.console_Textbox);
            this.Controls.Add(this.textThread_Dropdown);
            this.Controls.Add(this.attach_Button);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(595, 418);
            this.Name = "MainForm";
            this.Text = "TextractorForms v1.1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
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
        private System.Windows.Forms.Button translate_Button;
        public System.Windows.Forms.TextBox manualTranslateInput_Textbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox clipboard_Checkbox;
        private System.Windows.Forms.CheckBox autoTranslate_Checkbox;
        public System.Windows.Forms.TextBox manualTranslateOutput_Textbox;
    }
}

