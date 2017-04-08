using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject {
    public class TextOutput : TextWriter {
        private List<Tuple<TextWriter>> _writers;
        public TextOutput() {
            _writers = new List<Tuple<TextWriter>>();
        }


        public override Encoding Encoding {
            get {
                return Encoding.UTF8;
            }
        }

        public new void  Write (string message) {
            for (int i = 0; i < _writers.Count; i++) {
                _writers[i].Item1.WriteLine(message);
            }
        }

        public void RegisterWriter(TextWriter writer) {
            this._writers.Add(new Tuple<TextWriter>(writer));
        }
    }
}
