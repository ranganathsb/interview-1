using System.IO;

namespace I.Tribit {
    public class TribitPyramidTransformer {
        private readonly TextWriter writer;

        public TribitPyramidTransformer(TextWriter writer) {
            this.writer = writer;
        }

        public void Transform(TribitPyramid tribitPyramid) {
            while (tribitPyramid.Count > 1) {
                writer.WriteLine(tribitPyramid);
                tribitPyramid = TransformImpl(tribitPyramid);
            }
            writer.WriteLine(tribitPyramid);
        }

        protected TribitPyramid TransformImpl(TribitPyramid tribitPyramid) {
            var indices = tribitPyramid.GetPyramidIndices();
            var bitscnt = new[] { 0, 0 };
            for (var i = 0; i < indices.Length; ++i) {
                var triange = tribitPyramid[indices[i]];
                var tribitPyramidRule = TribitPyramidRule.GetBy(triange);
                if (tribitPyramidRule != null) {
                    tribitPyramid[indices[i]] = tribitPyramidRule.To;
                }
                else {
                    bitscnt[triange[0]]++;
                }
            }
            if (bitscnt[0] + bitscnt[1] == indices.Length) {
                if (bitscnt[0] == 0) return new TribitPyramid(1);
                if (bitscnt[1] == 0) return new TribitPyramid(0);

                var bits = new byte[tribitPyramid.Count >> 2];
                for (var i = 0; i < bits.Length; ++i) {
                    bits[i] = tribitPyramid[indices[i]][0];
                }
                return new TribitPyramid(bits);
            }
            return tribitPyramid;
        }
    }
}
