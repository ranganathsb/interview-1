using System;
using System.Diagnostics;

namespace Utils.Primes {
    internal partial class Program {
        private const ulong Max = ulong.MaxValue;

        private static void Main() {
            var stopwatch = Stopwatch.StartNew();
            var maxPeriodLength = 0UL;
            for (var x = Max; x > 2; --x) {
                if (x <= maxPeriodLength) {
                    break;
                }

                if (x % 2 == 0) continue;
                if (x % 5 == 0) continue;

                var periodLength = GetPeriodLength(x);
                if (periodLength > maxPeriodLength) {
                    maxPeriodLength = periodLength;
                }
                Console.WriteLine("x = {0:N0}: |1/x| = {1:N0}", x, maxPeriodLength);
            }
            stopwatch.Stop();
            Console.WriteLine("Max period length = {0:N0}", maxPeriodLength);
            Console.WriteLine("Elapsed time: {0:N0} ms", stopwatch.ElapsedMilliseconds);
            Console.ReadKey();
        }

        private static ulong GetPeriodLength(ulong x) {
            ulong result = 1;
            for (ulong p = 3; p <= x / p; p += 2) {
                if (x % p == 0) {
                    ulong mul = 1;
                    ulong pow = 0;
                    while (x % p == 0) {
                        mul *= p;
                        pow += 1;
                        x /= p;
                    }
                    result = Lcm(result, Get(p, mul, pow));
                }
            }
            if (x > 1) {
                result = Lcm(result, Get(x));
            }
            return result;
        }

        private static ulong Get(ulong p, ulong f, ulong k) {
            if (k == 1) {
                return Get(p);
            }
            return GetLog(f);
        }

        private static ulong Get(ulong p) {
            var periodLength = p - 1;
            var factors = GetFactors(p - 1);
            for (var set = 1; set < 1 << factors.Length; ++set) {
                ulong divisor = 1;
                for (var i = 0; i < factors.Length; ++i) {
                    if ((set & (1 << i)) != 0) {
                        divisor *= factors[i];
                    }
                }
                if (Pow(10, divisor, p) == 1) {
                    periodLength = Math.Min(periodLength, divisor);
                }
            }
            return periodLength;
        }

        private static ulong GetLog(ulong p) {
            ulong pow = 1;
            for (ulong ten = 10; ten != 1; ++pow) {
                ten = (10 * ten) % p;
            }
            return pow;
        }
    }
}
