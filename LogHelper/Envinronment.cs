using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace LogHelper {

    public class Envinronment : IDictionary<string, object> {
        public Dictionary<string, object> Context { get; set; }
        public Envinronment() {
            Context = new Dictionary<string, object>();
        }
        public object this[string key] {
            get {
                if (Context.ContainsKey(key))
                    return ((IDictionary<string, object>)Context)[key];
                return null;
            }

            set {
                ((IDictionary<string, object>)Context)[key] = value;
            }
        }

        #region Default Implementation
        public int Count {
            get {
                return ((IDictionary<string, object>)Context).Count;
            }
        }

        public bool IsReadOnly {
            get {
                return ((IDictionary<string, object>)Context).IsReadOnly;
            }
        }

        public ICollection<string> Keys {
            get {
                return ((IDictionary<string, object>)Context).Keys;
            }
        }

        public ICollection<object> Values {
            get {
                return ((IDictionary<string, object>)Context).Values;
            }
        }

        public void Add(KeyValuePair<string, object> item) {
            ((IDictionary<string, object>)Context).Add(item);
        }

        public void Add(string key, object value) {
            ((IDictionary<string, object>)Context).Add(key, value);
        }

        public void Clear() {
            ((IDictionary<string, object>)Context).Clear();
        }

        public bool Contains(KeyValuePair<string, object> item) {
            return ((IDictionary<string, object>)Context).Contains(item);
        }

        public bool ContainsKey(string key) {
            return ((IDictionary<string, object>)Context).ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex) {
            ((IDictionary<string, object>)Context).CopyTo(array, arrayIndex);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() {
            return ((IDictionary<string, object>)Context).GetEnumerator();
        }

        public bool Remove(KeyValuePair<string, object> item) {
            return ((IDictionary<string, object>)Context).Remove(item);
        }

        public bool Remove(string key) {
            return ((IDictionary<string, object>)Context).Remove(key);
        }

        public bool TryGetValue(string key, out object value) {
            return ((IDictionary<string, object>)Context).TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return ((IDictionary<string, object>)Context).GetEnumerator();
        }
        #endregion
    }
    public class LogWriter : TextWriter {
        public override Encoding Encoding { get; }
        private Envinronment _envinronment;

        public LogWriter() {
            Encoding = Encoding.UTF8;
        }

        public LogWriter(IDictionary<string, object> envinronment) : this() {
            _envinronment = envinronment as Envinronment;
        }

        public override void WriteLine(string message) {
            DbContext db = _envinronment["db"] as DbContext;

            var entity = _envinronment["entity"];
            var props = entity.GetType().GetProperties();
            props.Where(p => _envinronment[p.Name] != null).ToList().ForEach(x => x.SetValue(entity, _envinronment[x.Name]));
            props.FirstOrDefault(x => x.Name == "Message")?.SetValue(entity, message);

            db.Set(entity.GetType()).Add(entity);
            var connection = db.Database.Connection;
            connection.Open();
            db.SaveChanges();
            connection.Close();
        }
    }
}
