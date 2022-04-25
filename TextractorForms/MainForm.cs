using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace TextractorForms {
    public partial class MainForm : Form {
        private readonly Dictionary<IntPtr, TextThreadData> textThreadData = new Dictionary<IntPtr, TextThreadData>();
        private readonly List<IntPtr> textThreads = new List<IntPtr>();

        public MainForm() {
            InitializeComponent();

            Interop.Ext_StartWrapper();
            ThreadAdded(IntPtr.Zero, "Console");
            textThread_Dropdown.SelectedIndex = 0;

            timer.Interval = 50;
            timer.Start();

            OnResize(this, EventArgs.Empty);
        }

        private void OnResize(object sender, EventArgs e) {
            int rightColWidth = ClientSize.Width - processes_Dropdown.Width - 20;
            textThread_Dropdown.Width = rightColWidth;
            console_Textbox.Width = rightColWidth;
            console_Textbox.Height = ClientSize.Height - textThread_Dropdown.Height - 20;
        }

        private void AttachPressed(object sender, EventArgs e) {
            var form = new AttachForm();
            form.ShowDialog();
        }

        private void OnTimerTicked(object sender, EventArgs e) {
            Interop.Synchronize(
                ProcessConnected,
                ProcessDisconnected,
                ThreadAdded,
                ThreadRemoved,
                SentenceReceived
                );
        }

        private static string Hex(IntPtr p) {
            return p.ToInt32().ToString("X");
        }

        private void ProcessConnected(int pid) {
            Log($"ProcessConnected {pid}");
            processes_Dropdown.Enabled = true;
            processes_Dropdown.DataSource = new List<string> { AttachForm.LastProcessPressed };
        }

        private void ProcessDisconnected(int pid) {
            Log($"ProcessDisconnected {pid}");
        }

        private void ThreadAdded(IntPtr addr, string name) {
            if (addr != IntPtr.Zero && name == "Console")
                return;
            if (name == "Clipboard")
                return;

            textThread_Dropdown.Items.Add($"{Hex(addr)} {name}");
            textThreads.Add(addr);
            textThreadData.Add(addr, new TextThreadData() { Translate = name != "Console" });
            Log($"ThreadAdded {Hex(addr)} {name}");
        }

        private void ThreadRemoved(IntPtr addr, string name) {
            Log($"ThreadRemoved {Hex(addr)} {name}");
            textThread_Dropdown.Items.Remove($"{Hex(addr)} {name}");
        }

        private void SentenceReceived(IntPtr addr, string name, string text) {
            if (name == "Console")
                addr = IntPtr.Zero;
            if (name == "Clipboard")
                return;

            if (!textThreadData.ContainsKey(addr))
                textThreadData.Add(addr, new TextThreadData());
            textThreadData[addr].AddEntry(text);
        }

        public void Log(object obj) {
            Debug.WriteLine(obj.ToString());
            SentenceReceived(IntPtr.Zero, "Console", obj.ToString());
        }

        private void HookIndexChanged(object sender, EventArgs e) {
            if (textThread_Dropdown.SelectedIndex < textThreads.Count) {
                var textThread = textThreads[textThread_Dropdown.SelectedIndex];
                console_Textbox.Clear();
                console_Textbox.AppendText(textThreadData[textThread].DisplayText());
            } else {
                console_Textbox.Clear();
            }
        }
    }
}
