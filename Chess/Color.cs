using System;
using System.Diagnostics;

namespace Chess
{
    /// <summary>
    /// Color
    /// </summary>
    [Serializable]
    public class Color
    {
        #region Fields

        #region Static

        public const int Max = 2;

        public static readonly Color WHITE;
        public static readonly Color BLACK;
        public static readonly Color NONE;

        #endregion

        private sbyte _Value;

        #endregion

        #region Properties

        public bool Ok { get { return WHITE._Value <= _Value && _Value <= BLACK._Value; } }

        #endregion

        #region Constructors

        static Color ()
        {
            WHITE = new Color (0);
            BLACK = new Color (1);
            NONE = new Color (2);
        }

        private Color (sbyte value)
        {
            _Value = value;
        }

        #endregion

        #region Methods

        public static implicit operator Color (int value)
        {
            switch (value)
            {
                case 0: return WHITE;
                case 1: return BLACK;
                default: return NONE;
            }
        }
        public static implicit operator int (Color c) { return c._Value; }

        public static Color operator ~ (Color c)
        {
            return c._Value ^ BLACK._Value;
        }

        //public static bool operator == (Color c1, Color c2)
        //{
        //    // If both are null, or both are same instance, return true.
        //    if (ReferenceEquals (c1, c2))
        //    {
        //        return true;
        //    }
        //    // If one is null, but not both, return false.
        //    if (ReferenceEquals (null, c1)
        //     || ReferenceEquals (null, c2))
        //    {
        //        return false;
        //    }
        //    // Return true if the fields match:
        //    return c1._Value == c2._Value;
        //}
        //public static bool operator != (Color c1, Color c2)
        //{
        //    return !(c1 == c2);
        //}

        public static Color ToColor (char c)
        {
            switch (c)
            {
                case 'w': return WHITE;
                case 'b': return BLACK;
                default: Debug.Assert (false); return NONE;
            }
        }

        public char ToChar ()
        {
            switch (_Value)
            {
                case 0: return 'w';
                case 1: return 'b';
                default: Debug.Assert (false); return '-';
            }
        }

        #region Override

        public override bool Equals (object obj)
        {
            return obj is Color ? obj as Color == this : false;
        }

        public override int GetHashCode ()
        {
            return _Value.GetHashCode ();
        }

        public override string ToString ()
        {
            return Ok ? ToChar ().ToString () : "(no color)";
        }
        
        #endregion

        #endregion
    }
}
