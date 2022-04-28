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
using System.IO;
using System.Reflection;

namespace TextractorForms {
    public partial class MainForm : Form {
        public static MainForm instance;
        private readonly Dictionary<IntPtr, TextThreadData> textThreadData = new Dictionary<IntPtr, TextThreadData>();
        private readonly List<IntPtr> textThreads = new List<IntPtr>();
        private int attachedPid;
        private readonly IntPtr server;
        private bool clientConnected;
        private ZenForm zenForm;
        private readonly Config cfg;
        private readonly Config translationCache;
        private readonly string curDir;

        public MainForm() {
            instance = this;
            InitializeComponent();

            curDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            translationCache = new Config("Cache.txt");
            cfg = new Config("TextractorForms.ini");
            clipboard_Checkbox.Checked = cfg.GetBool("copy_to_clipboard", false);
            autoTranslate_Checkbox.Checked = cfg.GetBool("auto_translate", true);

            Interop.Ext_StartWrapper();
            ThreadAdded(IntPtr.Zero, "Console");
            textThread_Dropdown.SelectedIndex = 0;

            server = Interop.Socket_ServerOpenWrapper(42942, OnServerRecv);
            StartTranslatorClient();

            timer.Interval = 200;
            timer.Start();

            var (pos, size) = GetConfigWindowDimensions("main_window", 595, 418);
            if (pos.HasValue) {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = pos.Value;
            }
            if (size.HasValue) {
                this.Size = size.Value;
            }

            LoadExtensions();

            OnResize(this, EventArgs.Empty);
        }

        private void OnResize(object sender, EventArgs e) {
            int rightColWidth = ClientSize.Width - attach_Button.Width - 20;
            textThread_Dropdown.Width = rightColWidth;
            console_Textbox.Width = rightColWidth;
            console_Textbox.Height = ClientSize.Height - textThread_Dropdown.Height - 20;

            int height = (this.ClientSize.Height - manualTranslateInput_Textbox.Location.Y - 16) / 2;
            manualTranslateInput_Textbox.Height = height;
            manualTranslateOutput_Textbox.Height = height;
            manualTranslateOutput_Textbox.Top = manualTranslateInput_Textbox.Bottom + 8;
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
            var (pos, size) = GetConfigWindowDimensions("zen_window", 1200, 300);
            zenForm = new ZenForm(pos, size);
            zenForm.console_Textbox.Clear();
            var textThread = textThreads[textThread_Dropdown.SelectedIndex];
            zenForm.console_Textbox.Text = textThreadData[textThread].Last().DisplayText();
            zenForm.console_Textbox.AppendText(" ");
            zenForm.ShowDialog();
            zenForm = null;
            this.Show();

            SetConfigWindowDimensions("zen_window", ZenForm.formPosition, ZenForm.formSize);
        }

        private (Point? pos, Size? size) GetConfigWindowDimensions(string property, int defaultWidth, int defaultHeight) {
            Point? pos = null;
            Size? size = null;
            if (cfg.ContainsProperty($"{property}_x")) {
                pos = new Point(
                    cfg.GetInt($"{property}_x", 0),
                    cfg.GetInt($"{property}_y", 0)
                    );
                size = new Size(
                    cfg.GetInt($"{property}_w", defaultWidth),
                    cfg.GetInt($"{property}_h", defaultHeight)
                    );
            }
            return (pos, size);
        }

        private void SetConfigWindowDimensions(string property, Point pos, Size size) {
            cfg.Set($"{property}_x", pos.X);
            cfg.Set($"{property}_y", pos.Y);
            cfg.Set($"{property}_w", size.Width);
            cfg.Set($"{property}_h", size.Height);
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
            if (!clientConnected && Interop.Socket_ServerConnected(server) == 1) {
                clientConnected = true;
                translate_Button.Enabled = true;
                manualTranslateInput_Textbox.Enabled = true;
                manualTranslateOutput_Textbox.Enabled = true;
                Log("Translator initialized.");
            } else if (clientConnected && Interop.Socket_ServerConnected(server) == 0) {
                clientConnected = false;
                translate_Button.Enabled = false;
                manualTranslateInput_Textbox.Enabled = false;
                manualTranslateOutput_Textbox.Enabled = false;
                Log("Translator disconnected due to an error.");
            }
        }

        private static string Hex(IntPtr p) {
            return p.ToInt32().ToString("X");
        }

        private void ProcessConnected(int pid) {
            Log($"Process connected. PID={pid}.");
            process_Label.Text = Process.GetProcessById(pid).ProcessName;
            attachedPid = pid;

            attach_Button.Enabled = false;
            detach_Button.Enabled = true;
        }

        private void ProcessDisconnected(int pid) {
            Log($"Process disconnected. PID={pid}.");

            process_Label.Text = "No process";
            for (int i = 1; i < textThreads.Count; i++) {
                textThreadData.Remove(textThreads[i]);
                textThread_Dropdown.Items.RemoveAt(1);
            }
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
                textThread_Dropdown.Items.Add($"{Hex(addr)}:{name}");
            else
                textThread_Dropdown.Items.Add(name);

            textThreads.Add(addr);
            textThreadData.Add(addr, new TextThreadData() { Translatable = name != "Console" });

            if (name != "Console")
                Log($"Thread added {Hex(addr)}:{name}");
        }

        private void ThreadRemoved(IntPtr addr, string name) {
            Log($"Thread removed {Hex(addr)}:{name}");
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

            if (textThread_Dropdown.SelectedIndex != -1 && textThreads[textThread_Dropdown.SelectedIndex] == addr) {
                AppendSentenceToCurrentConsole(GetTextThread().Last().DisplayText());

                string jp = GetTextThread().Last().extracted;
                if (clipboard_Checkbox.Checked)
                    Clipboard.SetText(jp);

                if (textThread_Dropdown.SelectedIndex > 0 && autoTranslate_Checkbox.Checked)
                    Translate(jp);
            }
        }

        public void Log(object obj) {
            SentenceReceived(IntPtr.Zero, "Console", obj.ToString());
        }

        private void HookIndexChanged(object sender, EventArgs e) {
            try {
                Interop.SetCurrentTextThread(textThreads[textThread_Dropdown.SelectedIndex]);
            } catch (ArgumentOutOfRangeException) { }
            UpdateConsole();
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e) {
            translationCache.Save();

            SetConfigWindowDimensions("main_window", this.Location, this.Size);
            cfg.Set("copy_to_clipboard", clipboard_Checkbox.Checked);
            cfg.Set("auto_translate", autoTranslate_Checkbox.Checked);
            cfg.Save();

            if (attachedPid != 0)
                DetachPressed(sender, e);
            Interop.Socket_ServerClose(server);
        }

        private TextThreadData GetTextThread() {
            var textThread = textThreads[textThread_Dropdown.SelectedIndex];
            return textThreadData[textThread];
        }

        private void AppendSentenceToCurrentConsole(string text) {
            console_Textbox.AppendText(text + "\r\n");
            if (GetTextThread().Translatable)
                console_Textbox.AppendText("\r\n");
            if (zenForm != null) {
                zenForm.console_Textbox.Text = text;
            }
        }

        private void UpdateConsole() {
            zenForm?.console_Textbox.Clear();
            if (textThread_Dropdown.SelectedIndex < textThreads.Count) {
                var textThread = textThreads[textThread_Dropdown.SelectedIndex];
                console_Textbox.Clear();
                console_Textbox.AppendText(textThreadData[textThread].DisplayText());
                if (textThreadData[textThread].Translatable)
                    console_Textbox.AppendText("\r\n");
                if (zenForm != null && textThreadData[textThread].entries.Count > 0)
                    zenForm.console_Textbox.Text = textThreadData[textThread].Last().DisplayText();
            } else {
                console_Textbox.Clear();
            }
        }

        private void OnServerRecv(string data) {
            int sepIndex = data.IndexOf("^&*");
            string jp = data.Substring(0, sepIndex);
            string en = data.Substring(sepIndex + 3)
                .Replace("<unk>", "");

            if (!translationCache.ContainsProperty(jp))
                translationCache.Set(jp, en);

            foreach (var entry in GetTextThread().entries) {
                if (entry.extracted == jp) {
                    entry.translated = en;
                }
            }

            if (manualTranslateInput_Textbox.Text == jp) {
                manualTranslateOutput_Textbox.Text = en;
            }

            UpdateConsole();
        }

        private void StartTranslatorClient() {
            Log("Initializing translator. No translations will be performed in the meantime.");
            try {
                Process.Start(curDir + "/Translator/Python38/pythonw.exe", "./Translator/main.py");
            } catch (Exception ex){
                Log("Error starting translator: " + ex.Message);
            }
        }

        private void ManualTranslateClicked(object sender, EventArgs e) {
            Translate(manualTranslateInput_Textbox.Text);
        }

        private void Translate(string text) {
            if (translationCache.ContainsProperty(text)) {
                OnServerRecv(text + "^&*" + translationCache.GetString(text));
            } else {
                Interop.Socket_ServerSendAllWrapper(server, text);
            }
        }

        private void LoadExtensions() {
            string[] dlls;
            try {
                dlls = File.ReadAllLines(curDir + "/ExtensionOrder.txt");
            } catch {
                return;
            }

            foreach (string dll in dlls) {
                Interop.LoadExtension($"{curDir}/Extensions/{dll}");
            }
        }
    }
}
