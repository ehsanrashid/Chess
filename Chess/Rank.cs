using System;

namespace Chess
{
    /// <summary>
    /// Rank
    /// </summary>
    [Serializable]
    public class Rank
    {
        #region Fields

        #region Static

        public const int Max = 8;

        public static readonly Rank R_1;
        public static readonly Rank R_2;
        public static readonly Rank R_3;
        public static readonly Rank R_4;
        public static readonly Rank R_5;
        public static readonly Rank R_6;
        public static readonly Rank R_7;
        public static readonly Rank R_8;

        public static readonly Rank None;

        #endregion

        private sbyte _Value;

        #endregion

        #region Properties

        public bool Ok { get { return R_1._Value <= _Value && _Value <= R_8._Value; } }

        public char Notation { get { return (char)('1' + _Value - R_1._Value); } }

        #endregion

        #region Constructors

        #region Static

        static Rank ()
        {
            R_1 = new Rank (0);
            R_2 = new Rank (1);
            R_3 = new Rank (2);
            R_4 = new Rank (3);
            R_5 = new Rank (4);
            R_6 = new Rank (5);
            R_7 = new Rank (6);
            R_8 = new Rank (7);

            None = new Rank (8);
        }

        #endregion

        private Rank (sbyte value)
        {
            _Value = value;
        }

        #endregion

        #region Methods     

        #region Static

        #region Operator

        public static implicit operator Rank (int value)
        {
            switch (value)
            {
                case 0: return R_1;
                case 1: return R_2;
                case 2: return R_3;
                case 3: return R_4;
                case 4: return R_5;
                case 5: return R_6;
                case 6: return R_7;
                case 7: return R_8;
                default: return None;
            }
        }
        public static implicit operator int (Rank r) { return r._Value; }

        public static Rank operator ~ (Rank r)
        {
            return r._Value ^ R_8._Value;
        }

        //public static bool operator == (Rank r1, Rank r2)
        //{
        //    // If both are null, or both are same instance, return true.
        //    if (ReferenceEquals (r1, r2))
        //    {
        //        return true;
        //    }
        //    // If one is null, but not both, return false.
        //    if (ReferenceEquals (null, r1)
        //     || ReferenceEquals (null, r2))
        //    {
        //        return false;
        //    }
        //    // Return true if the fields match:
        //    return r1._Value == r2._Value;
        //}
        //public static bool operator != (Rank r1, Rank r2)
        //{
        //    return !(r1 == r2);
        //}

        #endregion

        public static Rank RelativeRank (Color c, Rank r)
        {
            return r._Value ^ (c * R_8._Value);
        }
        public static Rank RelativeRank (Color c, Square s)
        {
            return RelativeRank (c, s.Rank);
        }

        public static Rank ToRank (char r)
        {
            return r - '1';
        }

        #endregion


        #region Override

        public override bool Equals (object obj)
        {
            return obj is Rank ? obj as Rank == this : false;
        }

        public override int GetHashCode ()
        {
            return _Value.GetHashCode ();
        }

        public override string ToString ()
        {
            return Ok ? Notation.ToString () : "(no rank)";
        }

        #endregion

        #endregion
    }
}
