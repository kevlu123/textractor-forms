using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextractorForms {
    class TextThreadData {
        public bool Translatable { get; set; } = true;
        public class Entry {
            public bool translatable;
            public string extracted;
            public string translated;
            public string DisplayText() {
                if (!translatable) {
                    return extracted;
                } else {
                    return extracted + "\r\n" + (translated ?? "[...]") + "\r\n";
                }
            }
        }

        public readonly Queue<Entry> entries = new Queue<Entry>();

        public void AddEntry(string extracted) {
            entries.Enqueue(new Entry { extracted = extracted, translatable = Translatable });
            while (DisplayText().Length > 10000)
                entries.Dequeue();
        }

        public Entry Last() {
            return entries.Last();
        }

        public string DisplayText() {
            return string.Join("\r\n", entries.Select(entry => entry.DisplayText()));
        }
    }
}
