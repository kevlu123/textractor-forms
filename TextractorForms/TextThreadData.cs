using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextractorForms {
    public enum TranslationState {
        NA,
        Waiting,
        Done
    }
    class TextThreadData {
        public bool Translatable { get; set; } = true;
        public struct Entry {
            public TranslationState translationState;
            public string extracted;
            public string translated;
            public string DisplayText() {
                switch (translationState) {
                    case TranslationState.NA:
                        return extracted;
                    case TranslationState.Waiting:
                        return extracted + "\r\n" + (translated ?? "[...]") + "\r\n";
                    default:
                        return extracted + "\r\n";
                }
            }
        }

        private readonly Queue<Entry> entries = new Queue<Entry>();

        public void AddEntry(string extracted) {
            if (entries.Count >= 32)
                entries.Dequeue();
            entries.Enqueue(new Entry {
                extracted = extracted,
                translationState = Translatable ? TranslationState.Done : TranslationState.NA,
                });
        }

        public Entry Last() {
            return entries.Last();
        }

        public string DisplayText() {
            return string.Join("\r\n", entries.Select(entry => entry.DisplayText()));
        }
    }
}
