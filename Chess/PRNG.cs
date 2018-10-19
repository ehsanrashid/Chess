using System;
using System.Diagnostics;

namespace Chess
{
    /// <summary>
    /// 
    /// </summary>
    public class PRNG
    {
        #region Fields

        private UInt64 _Seed = 0;

        #endregion

        #region Constructors

        public PRNG (UInt64 seed)
        {
            Debug.Assert (seed != 0);
            _Seed = seed;
        }

        #endregion

        #region Methods

        private UInt64 Rand64 ()
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
