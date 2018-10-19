using System;

namespace Chess
{
    /// <summary>
    /// File
    /// </summary>
    [Serializable]
    public class File
    {
        #region Fields

        #region Static

        public const int Max = 8;

        public static readonly File F_A;
        public static readonly File F_B;
        public static readonly File F_C;
        public static readonly File F_D;
        public static readonly File F_E;
        public static readonly File F_F;
        public static readonly File F_G;
        public static readonly File F_H;

        public static readonly File None;

        #endregion

        private sbyte _Value;

        #endregion

        #region Properties

        public bool Ok { get { return F_A._Value <= _Value && _Value <= F_H._Value; } }

        public char Notation { get { return (char)('a' + _Value - F_A._Value); } }

        #endregion

        #region Constructors

        #region Static

        static File ()
        {
            F_A = new File (0);
            F_B = new File (1);
            F_C = new File (2);
            F_D = new File (3);
            F_E = new File (4);
            F_F = new File (5);
            F_G = new File (6);
            F_H = new File (7);

            None = new File (8);
        }

        #endregion

        private File (sbyte value)
        {
            _Value = value;
        }

        #endregion

        #region Methods

        #region Static

        public static File ToFile (char f)
        {
            return f - 'a';
        }

        #endregion

        #region Operator

        public static implicit operator File (int value)
        {
            switch (value)
            {
                case 0: return F_A;
                case 1: return F_B;
                case 2: return F_C;
                case 3: return F_D;
                case 4: return F_E;
                case 5: return F_F;
                case 6: return F_G;
                case 7: return F_H;
                default: return None;
            }
        }
        public static implicit operator int (File f) { return f._Value; }

        public static File operator ~ (File f)
        {
            return f._Value ^ F_H._Value;
        }

        //public static bool operator == (File f1, File f2)
        //{
        //    // If both are null, or both are same instance, return true.
        //    if (ReferenceEquals (f1, f2))
        //    {
        //        return true;
        //    }
        //    // If one is null, but not both, return false.
        //    if (ReferenceEquals (null, f1)
        //     || ReferenceEquals (null, f2))
        //    {
        //        return false;
        //    }
        //    // Return true if the fields match:
        //    return f1._Value == f2._Value;
        //}
        //public static bool operator != (File f1, File f2)
        //{
        //    return !(f1 == f2);
        //}

        #endregion

        #region Override

        public override bool Equals (object obj)
        {
            return obj is File ? obj as File == this : false;
        }

        public override int GetHashCode ()
        {
            return _Value.GetHashCode ();
        }

        public override string ToString ()
        {
            return Ok ? Notation.ToString () : "(no file)";
        }

        #endregion       

        #endregion
    }
}
