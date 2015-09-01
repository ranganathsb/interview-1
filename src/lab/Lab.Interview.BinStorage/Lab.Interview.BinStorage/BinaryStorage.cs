using System;
using System.IO;

namespace Lab.Interview.BinStorage {
    public class BinaryStorage : IBinaryStorage {

        public BinaryStorage(StorageConfiguration configuration) {

        }

        public void Add(string key, Stream data, StreamInfo parameters) {
            throw new NotImplementedException();
        }

        public Stream Get(string key) {
            throw new NotImplementedException();
        }

        public bool Contains(string key) {
            throw new NotImplementedException();
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}
