namespace I.Tribit {
    public static class TribitPyramidParser {
        public static TribitPyramid ToTribitPyramid(this string s) {
            var bits = s.ToBitArray();
            if (bits != null) {
                return new TribitPyramid(bits);
            }
            return null;
        }

        public static byte[] ToBitArray(this string s) {
            if (!string.IsNullOrEmpty(s)) {
                var bits = new byte[s.Length];
                for (var i = 0; i < s.Length; ++i) {
                    if ('0' <= s[i] && s[i] <= '1') {
                        bits[s.Length - 1 - i] = (byte)(s[i] - '0');
                    }
                    else return null;
                }
                return bits;
            }
            return null;
        }
    }
}
