using System;

namespace Chess
{
    /// <summary>
    /// Piece Type
    /// </summary>
    [Serializable]
    public class PieceType
    {
        #region Fields

        public const int Max = 6;

        public static readonly PieceType PAWN;
        public static readonly PieceType NIHT;
        public static readonly PieceType BSHP;
        public static readonly PieceType ROOK;
        public static readonly PieceType QUEN;
        public static readonly PieceType KING;
        public static readonly PieceType NONE;
        public static readonly PieceType ALL; 

        private sbyte _Value;

        #endregion

        #region Properties

        public string Notation
        {
            get
            {
                switch (_Value)
                {
                    case 0: return "P";
                    case 1: return "N";
                    case 2: return "B";
                    case 3: return "R";
                    case 4: return "Q";
                    case 5: return "K";

                    default: return " ";
                }
            }
        }

        public bool Ok
        {
            get { return PAWN <= _Value && _Value <= KING; }
        }

        #endregion

        #region Constructors

        #region Static

        static PieceType ()
        {
            PAWN = new PieceType (0);
            NIHT = new PieceType (1);
            BSHP = new PieceType (2);
            ROOK = new PieceType (3);
            QUEN = new PieceType (4);
            KING = new PieceType (5);
            NONE = new PieceType (6);
            ALL = new PieceType (7);
        }

        #endregion

        private PieceType (sbyte value)
        {
            _Value = value;
        }

        #endregion

        #region Methods

        public static implicit operator PieceType (int value)
        {
            switch (value)
            {
                case 0: return PAWN;
                case 1: return NIHT;
                case 2: return BSHP;
                case 3: return ROOK;
                case 4: return QUEN;
                case 5: return KING;
                case 7: return ALL;
                default: return NONE;
            }
        }
        public static implicit operator int (PieceType pieceType) { return pieceType._Value; }

        //public static bool operator == (PieceType pt1, PieceType pt2)
        //{
        //    // If both are null, or both are same instance, return true.
        //    if (ReferenceEquals (pt1, pt2))
        //    {
        //        return true;
        //    }
        //    // If one is null, but not both, return false.
        //    if (ReferenceEquals (null, pt1)
        //     || ReferenceEquals (null, pt2))
        //    {
        //        return false;
        //    }
        //    // Return true if the fields match:
        //    return pt1._Value == pt2._Value;
        //}
        //public static bool operator != (PieceType pt1, PieceType pt2)
        //{
        //    return !(pt1 == pt2);
        //}

        public static PieceType ToPieceType (char pt)
        {
            switch (pt)
            {
                case 'P': return PAWN;
                case 'N': return NIHT;
                case 'B': return BSHP;
                case 'R': return ROOK;
                case 'Q': return QUEN;
                case 'K': return KING;

                default: return NONE;
            }
        }

        #region Override

        public override bool Equals (object obj)
        {
            return obj is PieceType ? obj as PieceType == this : false;
        }

        public override int GetHashCode ()
        {
            return _Value.GetHashCode ();
        }

        public override string ToString ()
        {
            return Notation;
        }
        
        #endregion

        #endregion
    }
}
