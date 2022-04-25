using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextractorForms {
    public partial class ZenForm : Form {
        public ZenForm() {
            InitializeComponent();
            OnTextChanged(this, EventArgs.Empty);
            OnClientSizeChanged(this, EventArgs.Empty);
            this.Focus();
        }

        public void OnTextChanged(object sender, EventArgs e) {
            string text = MainForm.instance.console_Textbox.Text;
            console_Textbox.Text = text.Substring(0, text.Length - 1);
            console_Textbox.AppendText(text.Last().ToString());
        }

        private void OnClientSizeChanged(object sender, EventArgs e) {
            console_Textbox.Location = Point.Empty;
            console_Textbox.Size = this.ClientSize;
        }
    }
}
