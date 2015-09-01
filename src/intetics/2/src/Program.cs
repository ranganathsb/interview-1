using System;

namespace I.Tribit {
    public class Program {
        internal static void Main(string[] args) {
            try {
                if (args.Length == 1) {
                    Process(args[0]);
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Unhandled exception is {0}",
                    ex.Message + ex.StackTrace);
            }
        }

        private static void Process(string s) {
            var tribitPyramid = s.ToTribitPyramid();
            if (tribitPyramid != null) {
                new TribitPyramidTransformer(Console.Out)
                    .Transform(tribitPyramid);
            }
        }
    }
}
