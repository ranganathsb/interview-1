namespace I.Tribit {
    public class TribitPyramidRule {
        public readonly byte[] From;
        public readonly byte[] To;

        private TribitPyramidRule(byte[] from, byte[] to) {
            From = from;
            To = to;
        }

        public static TribitPyramidRule GetBy(byte[] from) {
            var result = 0;
            result = (result << 1) | from[3];
            result = (result << 1) | from[2];
            result = (result << 1) | from[1];
            result = (result << 1) | from[0];
            return rules[result];
        }

        private static readonly TribitPyramidRule[] rules = {
            null, //Parse("0000", "0000"),
            Parse("0001", "1000"),
            Parse("0010", "0001"),
            Parse("0011", "0010"),
            Parse("0100", "0000"),
            Parse("0101", "0010"),
            Parse("0110", "1011"),
            Parse("0111", "1011"),
            Parse("1000", "0100"),
            Parse("1001", "0101"),
            Parse("1010", "0111"),
            Parse("1011", "1111"),
            Parse("1100", "1101"),
            Parse("1101", "1110"),
            Parse("1110", "0111"),
            null, //Parse("1111", "1111")
        };

        private static TribitPyramidRule Parse(string from, string to) {
            return new TribitPyramidRule(from.ToBitArray(), to.ToBitArray());
        }
    }
}
