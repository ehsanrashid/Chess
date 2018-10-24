using System;
using System.Diagnostics;

namespace Chess
{
    /// <summary>
    /// XOR Shift64*(Star) Pseudo-Random Number Generator
    /// Based on the original code design/written and dedicated
    /// to the public domain by Sebastiano Vigna (2014)
    ///
    /// It has the following characteristics:
    ///
    ///  -  Outputs 64-bit numbers
    ///  -  Passes Dieharder and SmallCrush test batteries
    ///  -  Does not require warm-up, no zeroland to escape
    ///  -  Internal state is a single 64-bit integer
    ///  -  Period is 2^64 - 1
    ///  -  Speed: 1.60 ns/call (Core i7 @3.40GHz)
    ///
    /// For further analysis see
    ///   <http://vigna.di.unimi.it/ftp/papers/xorshift.pdf>
    /// </summary>
    public class PRNG
    {
        #region Fields

        private ulong _Seed = 0;

        #endregion

        #region Constructors

        public PRNG (ulong seed)
        {
            Debug.Assert (seed != 0);
            _Seed = seed;
        }

        #endregion

        #region Methods

        private ulong Rand64 ()
        {
            _Seed ^= _Seed >> 12;
            _Seed ^= _Seed << 25;
            _Seed ^= _Seed >> 27;
            return _Seed * 0x2545F4914F6CDD1DUL;
        }

        public T Rand<T> ()
        {
            return (T)Convert.ChangeType (Rand64 (), typeof (T));
        }

        // Special generator used to fast initialize magic numbers.
        // Output values only have 1/8th of their bits set on average.
        public T SparseRand<T> ()
        {
            return (T)Convert.ChangeType (Rand64 () & Rand64 () & Rand64 (), typeof (T));
        }

        #endregion
    }
}
