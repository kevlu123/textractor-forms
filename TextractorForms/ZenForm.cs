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

        public static Point formPosition;
        public static Size formSize;
        public ZenForm(Point? pos, Size? size) {
            InitializeComponent();

            if (pos.HasValue) {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = pos.Value;
            }
            if (size.HasValue) {
                this.Size = size.Value;
            }

            OnClientSizeChanged(this, EventArgs.Empty);
            console_Textbox.BackColor = Color.CornflowerBlue;
        }

        private void OnClientSizeChanged(object sender, EventArgs e) {
            console_Textbox.Location = Point.Empty;
            console_Textbox.Size = this.ClientSize;
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e) {
            formPosition = this.Location;
            formSize = this.Size;
        }
    }
}
