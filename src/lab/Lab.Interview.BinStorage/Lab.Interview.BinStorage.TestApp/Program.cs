using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Lab.Interview.BinStorage.TestApp {
    internal class Program {
        internal static void Main(string[] args) {
            if (args.Length < 2
                || !Directory.Exists(args[0])
                || !Directory.Exists(args[1])) {
                Console.WriteLine("Usage: Lab.Interview.BinStorage.TestApp.exe InputFolder StorageFolder");
                return;
            }

            // Create storage and add data
            Console.WriteLine("Creating storage from " + args[0]);
            var stopwatch = Stopwatch.StartNew();
            using (var storage = new BinaryStorage(new StorageConfiguration() { WorkingFolder = args[1] })) {
                Directory.EnumerateFiles(args[0], "*", SearchOption.AllDirectories)
                    .AsParallel().WithDegreeOfParallelism(4).ForAll(s => {
                        AddFile(storage, s);
                    });

            }
            Console.WriteLine("Time to create: " + stopwatch.Elapsed);

            // Open storage and read data
            Console.WriteLine("Verifying data");
            stopwatch = Stopwatch.StartNew();
            using (var storage = new BinaryStorage(new StorageConfiguration() { WorkingFolder = args[1] })) {
                Directory.EnumerateFiles(args[0], "*", SearchOption.AllDirectories)
                    .AsParallel().WithDegreeOfParallelism(4).ForAll(s => {
                        using (var ms1 = new MemoryStream()) {
                            storage.Get(s).CopyTo(ms1);
                            using (var ms2 = new MemoryStream()) {
                                using (var file = new FileStream(s, FileMode.Open)) {
                                    {
                                        file.CopyTo(ms2);
                                        if (ms2.Length != ms1.Length)
                                            throw new Exception();
                                    }
                                }
                            }
                        }
                    });
            }
            Console.WriteLine("Time to verify: " + stopwatch.Elapsed);
        }

        private static void AddFile(IBinaryStorage storage, string fileName) {
            using (var file = new FileStream(fileName, FileMode.Open)) {
                storage.Add(fileName, file, StreamInfo.Empty);
            }
        }

        private static void AddBytes(IBinaryStorage storage, string key, byte[] data) {
            var streamInfo = new StreamInfo();
            using (var md5 = MD5.Create()) {
                streamInfo.Hash = md5.ComputeHash(data);
            }
            streamInfo.Length = data.Length;
            streamInfo.IsCompressed = false;

            using (var ms = new MemoryStream(data)) {
                storage.Add(key, ms, streamInfo);
            }
        }

        private static void Dump(IBinaryStorage storage, string key, string fileName) {
            using (var file = new FileStream(fileName, FileMode.Create)) {
                storage.Get(key).CopyTo(file);
            }
        }
    }
}
