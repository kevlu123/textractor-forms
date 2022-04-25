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
    public partial class AttachForm : Form {
        public static string LastProcessPressed { get; private set; }

        private readonly List<(int pid, string path)> processList;

        private class ProcessComparer : IComparer<(int pid, string path)> {
            public int Compare((int pid, string path) a, (int pid, string path) b) {
                return a.path.CompareTo(b.path);
            }
        }

        private static string PrettyPath(string path) {
            return path.Substring(path.LastIndexOf('\\') + 1);
        }

        public AttachForm() {
            InitializeComponent();

            processList = Interop.Ext_GetProcessListWrapper();
            processList.Sort(new ProcessComparer());

            foreach (var (_, path) in processList) {
                process_ListView.Items.Add(PrettyPath(path));
            }
        }

        private void ProcessDoubleClicked(object sender, MouseEventArgs e) {
            if (process_ListView.SelectedItems.Count == 0)
                return;
            int index = process_ListView.SelectedItems[0].Index;

            LastProcessPressed = PrettyPath(processList[index].path);
            Interop.Ext_InjectProcess(processList[index].pid);
            this.Close();
        }
    }
}
