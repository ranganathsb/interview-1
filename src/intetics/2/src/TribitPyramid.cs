using System;
using System.Text;

namespace I.Tribit {
    public class TribitPyramid {
        private readonly byte[] bits;

        public TribitPyramid(byte bit) {
            this.bits = new[] { bit };
        }

        public TribitPyramid(byte[] bits) {
            this.bits = new byte[ToPow4(bits.Length)];
            Array.Copy(bits, this.bits, bits.Length);
        }

        public byte[] this[int[] indices] {
            get { return GetByIndices(indices); }
            set { SetByIndices(indices, value); }
        }

        public int Count {
            get { return bits.Length; }
        }

        public int[][] GetPyramidIndices() {
            var numOfRows = (int)Math.Sqrt(bits.Length);
            var pyramid = new int[numOfRows][];
            for (int bitsPos = 0, row = 0, capacity = 1; row < numOfRows; ++row, capacity += 2) {
                pyramid[row] = new int[capacity];
                for (var col = 0; col < capacity; ++col, ++bitsPos) {
                    pyramid[row][col] = bitsPos;
                }
            }
            var result = new int[numOfRows * numOfRows >> 2][];
            for (int row = 0, i = 0; row + 1 < numOfRows; row += 2) {
                for (int iCol = 0, jCol = 0, take = 1; iCol < pyramid[row].Length; ++i) {
                    var res = new int[4];
                    switch (take) {
                        case 1:
                            res[0] = pyramid[row][iCol];
                            res[1] = pyramid[row + 1][jCol + 0];
                            res[2] = pyramid[row + 1][jCol + 1];
                            res[3] = pyramid[row + 1][jCol + 2];
                            iCol += 1;
                            jCol += 3;
                            break;
                        case 3:
                            res[0] = pyramid[row + 1][jCol];
                            res[1] = pyramid[row][iCol + 0];
                            res[2] = pyramid[row][iCol + 1];
                            res[3] = pyramid[row][iCol + 2];
                            iCol += 3;
                            jCol += 1;
                            break;
                    }
                    result[i] = res;
                    take = 4 - take;
                }
            }
            return result;
        }

        public override string ToString() {
            var result = new StringBuilder();
            for (var i = bits.Length - 1; i >= 0; --i) {
                result.Append(bits[i]);
            }
            return result.ToString();
        }

        private byte[] GetByIndices(int[] indices) {
            var result = new byte[indices.Length];
            for (var i = 0; i < indices.Length; ++i) {
                result[i] = bits[indices[i]];
            }
            return result;
        }

        private void SetByIndices(int[] indices, byte[] value) {
            for (var i = 0; i < indices.Length; ++i) {
                bits[indices[i]] = value[i];
            }
        }

        private static int ToPow4(int length) {
            var result = 1;
            while (result < length) {
                result <<= 2;
            }
            return result;
        }
    }
}
