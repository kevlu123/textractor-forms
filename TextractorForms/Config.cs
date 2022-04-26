using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TextractorForms {
    internal class Config {
        private readonly string filename;
        private readonly Dictionary<string, string> properties = new Dictionary<string, string>();

        private const string SEP = "^&*";

        public Config(string filename) {
            try {
                this.filename = Environment.GetEnvironmentVariable("APPDATA") + "\\Kev\\TextractorForms\\" + filename;
            } catch {
                this.filename = filename;
            }

            try {
                foreach (string line in File.ReadAllLines(this.filename)) {
                    if (line.Contains(SEP)) {
                        int index = line.IndexOf(SEP);
                        string key = line.Substring(0, index);
                        properties[key] = line.Substring(index + SEP.Length);
                    }
                }
            } catch { }
        }

        public void Save() {
            string content = string.Join("\r\n", properties.Select(p => $"{p.Key}{SEP}{p.Value}"));
            try {
                Directory.CreateDirectory(Path.GetDirectoryName(filename));
                File.WriteAllText(filename, content);
            } catch { }
        }

        public bool ContainsProperty(string key) {
            return properties.ContainsKey(key);
        }

        public void Set<T>(string key, T value) {
            properties[key] = value.ToString();
        }

        public bool GetBool(string key, bool defaultValue = false) {
            if (properties.TryGetValue(key, out var value) && bool.TryParse(value, out var parsed)) {
                return parsed;
            } else {
                Set(key, defaultValue);
                return defaultValue;
            }
        }

        public string GetString(string key, string defaultValue = null) {
            if (properties.TryGetValue(key, out var value)) {
                return value;
            } else {
                Set(key, defaultValue);
                return defaultValue;
            }
        }

        public int GetInt(string key, int defaultValue = 0) {
            if (properties.TryGetValue(key, out var value) && int.TryParse(value, out var parsed)) {
                return parsed;
            } else {
                Set(key, defaultValue);
                return defaultValue;
            }
        }
    }
}
