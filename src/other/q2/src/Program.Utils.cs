using System.Collections.Generic;

namespace Utils.Primes {
    internal partial class Program {
        private static ulong Pow(ulong x, ulong k, ulong modulo) {
            if (k > 0) {
                if ((k & 1) == 0) {
                    return Pow((x * x) % modulo, k >> 1, modulo);
                }
                return (x * Pow(x, k - 1, modulo)) % modulo;
            }
            return 1;
        }

        private static ulong Gcd(ulong x, ulong y) {
            while (x != 0 && y != 0) {
                if (x > y)
                    x %= y;
                else
                    y %= x;
            }
            return x + y;
        }

        private static ulong Lcm(ulong x, ulong y) {
            return x / Gcd(x, y) * y;
        }

        private static ulong[] GetFactors(ulong x) {
            var result = new List<ulong>();
            for (ulong factor = 2; x / factor >= factor; ++factor) {
                if (x % factor == 0) {
                    while (x % factor == 0) {
                        result.Add(factor);
                        x /= factor;
                    }
                }
            }
            if (x > 1) {
                result.Add(x);
            }
            return result.ToArray();
        }
    }
}
