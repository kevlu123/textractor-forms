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

            OnClientSizeChanged(this, EventArgs.Empty);
            console_Textbox.BackColor = Color.CornflowerBlue;
        }

        private void OnClientSizeChanged(object sender, EventArgs e) {
            console_Textbox.Location = Point.Empty;
            console_Textbox.Size = this.ClientSize;
        }
    }
}
