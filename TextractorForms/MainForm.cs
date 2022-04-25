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
        public static MainForm instance;
        private readonly Dictionary<IntPtr, TextThreadData> textThreadData = new Dictionary<IntPtr, TextThreadData>();
        private readonly List<IntPtr> textThreads = new List<IntPtr>();
        private int attachedPid = 0;
        private IntPtr server = IntPtr.Zero;

        public MainForm() {
            instance = this;
            InitializeComponent();

            server = Interop.Socket_ServerOpenWrapper(42942, OnServerRecv);

            Interop.Ext_StartWrapper();
            ThreadAdded(IntPtr.Zero, "Console");
            textThread_Dropdown.SelectedIndex = 0;

            timer.Interval = 200;
            timer.Start();

            OnResize(this, EventArgs.Empty);
        }

        private void OnResize(object sender, EventArgs e) {
            int rightColWidth = ClientSize.Width - attach_Button.Width - 20;
            textThread_Dropdown.Width = rightColWidth;
            console_Textbox.Width = rightColWidth;
            console_Textbox.Height = ClientSize.Height - textThread_Dropdown.Height - 20;
        }

        private void AttachPressed(object sender, EventArgs e) {
            var form = new AttachForm();
            form.ShowDialog();
        }

        private void DetachPressed(object sender, EventArgs e) {
            Interop.Ext_DetachProcess(attachedPid);
        }
        private void ZenModeClicked(object sender, EventArgs e) {
            this.Hide();
            var form = new ZenForm();
            console_Textbox.TextChanged += form.OnTextChanged;
            form.ShowDialog();
            console_Textbox.TextChanged -= form.OnTextChanged;
            this.Show();
        }

        private void OnTimerTicked(object sender, EventArgs e) {
            Interop.SynchronizeTexthook(
                ProcessConnected,
                ProcessDisconnected,
                ThreadAdded,
                ThreadRemoved,
                SentenceReceived
                );

            Interop.Socket_ServerUpdate(server);
        }

        private static string Hex(IntPtr p) {
            return p.ToInt32().ToString("X");
        }

        private void ProcessConnected(int pid) {
            Log($"ProcessConnected {pid}");
            process_Label.Text = Process.GetProcessById(pid).ProcessName;
            attachedPid = pid;

            attach_Button.Enabled = false;
            detach_Button.Enabled = true;
        }

        private void ProcessDisconnected(int pid) {
            Log($"ProcessDisconnected {pid}");

            process_Label.Text = "No process";
            for (int i = 1; i < textThreads.Count; i++)
                textThreadData.Remove(textThreads[i]);
            textThreads.RemoveRange(1, textThreads.Count - 1);
            attachedPid = 0;
            textThread_Dropdown.SelectedIndex = 0;

            attach_Button.Enabled = true;
            detach_Button.Enabled = false;
        }

        private void ThreadAdded(IntPtr addr, string name) {
            if (addr != IntPtr.Zero && name == "Console")
                return;
            if (name == "Clipboard")
                return;

            if (name != "Console")
                textThread_Dropdown.Items.Add($"{Hex(addr)} {name}");
            else
                textThread_Dropdown.Items.Add(name);
            textThreads.Add(addr);
            textThreadData.Add(addr, new TextThreadData() { Translatable = name != "Console" });
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

            try {
                if (textThreads[textThread_Dropdown.SelectedIndex] == addr) {
                    console_Textbox.AppendText("\r\n" + textThreadData[addr].Last().DisplayText());
                }
            } catch (ArgumentOutOfRangeException) { }
        }

        public void Log(object obj) {
            //Debug.WriteLine(obj.ToString());
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

        private void OnFormClosed(object sender, FormClosedEventArgs e) {
            Interop.Socket_ServerClose(server);
        }

        private void OnServerRecv(string data) {
            Log("Tcp: " + data);
        }
    }
}
