using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextractorForms {
    class TextThreadData {
        public bool Translate { get; set; } = true;
        private struct Entry {
            public string extracted;
            public string translated;
        }

        private readonly List<Entry> entries = new List<Entry>();

        public void AddEntry(string extracted) {
            entries.Add(new Entry {extracted = extracted});
        }

        public string DisplayText() {
            if (Translate) {
                return string.Join("\r\n", entries.Select(
                    entry => entry.extracted + "\r\n" + (entry.translated ?? "[...]") + "\r\n"
                    ));
            } else {
                return string.Join("\r\n", entries.Select(
                    entry => entry.extracted + "\r\n"
                    ));
            }
        }
    }
}
