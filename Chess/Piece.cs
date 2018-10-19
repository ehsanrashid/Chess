using System;

namespace Chess
{
    /// <summary>
    /// Piece needs 4 bits to be stored
    /// <para>bit 0-2: Type of piece</para>
    /// <para>bit   3: Color of piece</para>
    /// </summary>
    [Serializable]
    public class Piece
    {
        #region Fields

        #region Static

        public const int Limit = 14;

        public static readonly Piece WhitePawn;
        public static readonly Piece WhiteKnight;
        public static readonly Piece WhiteBishop;
        public static readonly Piece WhiteRook;
        public static readonly Piece WhiteQueen;
        public static readonly Piece WhiteKing;

        public static readonly Piece BlackPawn;
        public static readonly Piece BlackKnight;
        public static readonly Piece BlackBishop;
        public static readonly Piece BlackRook;
        public static readonly Piece BlackQueen;
        public static readonly Piece BlackKing;

        public static readonly Piece NoPiece;
        
        #endregion

        private sbyte _Value;

        #endregion

        #region Properties

        public Color Color { get { return _Value >> 3; } }

        public PieceType Type { get { return _Value & 7; } }

        public bool Ok
        {
            get
            {
                return (WhitePawn._Value <= _Value && _Value <= WhiteKing._Value)
                    || (BlackPawn._Value <= _Value && _Value <= BlackKing._Value);
            }
        }

        public string Name
        {
            get
            {
                switch (_Value)
                {
                    case  0: return "WhitePawn";
                    case  1: return "WhiteKnight";
                    case  2: return "WhiteBishop";
                    case  3: return "WhiteRook";
                    case  4: return "WhiteQueen";
                    case  5: return "WhiteKing";

                    case  8: return "BlackPawn";
                    case  9: return "BlackKnight";
                    case 10: return "BlackBishop";
                    case 11: return "BlackRook";
                    case 12: return "BlackQueen";
                    case 13: return "BlackKing";

                    default: return string.Empty;
                }
            }
        }

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

                    case 8: return "p";
                    case 9: return "n";
                    case 10: return "b";
                    case 11: return "r";
                    case 12: return "q";
                    case 13: return "k";

                    default: return " ";
                }
            }
        }

        public string Figurine
        {
            get
            {
                switch (_Value)
                {
                    case 0: return "\u2659";
                    case 1: return "\u2658";
                    case 2: return "\u2657";
                    case 3: return "\u2656";
                    case 4: return "\u2655";
                    case 5: return "\u2654";

                    case 8: return "\u265F";
                    case 9: return "\u265E";
                    case 10: return "\u265D";
                    case 11: return "\u265C";
                    case 12: return "\u265B";
                    case 13: return "\u265A";

                    case 6:
                    default: return string.Empty;
                }
            }
        }

        #endregion

        #region Constructors

        #region Static

        static Piece ()
        {
            WhitePawn = new Piece (0);
            WhiteKnight = new Piece (1);
            WhiteBishop = new Piece (2);
            WhiteRook = new Piece (3);
            WhiteQueen = new Piece (4);
            WhiteKing = new Piece (5);

            BlackPawn = new Piece (8);
            BlackKnight = new Piece (9);
            BlackBishop = new Piece (10);
            BlackRook = new Piece (11);
            BlackQueen = new Piece (12);
            BlackKing = new Piece (13);

            NoPiece = new Piece (6);
        }

        #endregion

        private Piece (sbyte value)
        {
            _Value = value;
        }

        #endregion

        #region Methods

        #region Static

        public static Piece ToPiece (char pc)
        {
            switch (pc)
            {
                case 'P': return WhitePawn;
                case 'N': return WhiteKnight;
                case 'B': return WhiteBishop;
                case 'R': return WhiteRook;
                case 'Q': return WhiteQueen;
                case 'K': return WhiteKing;

                case 'p': return BlackPawn;
                case 'n': return BlackKnight;
                case 'b': return BlackBishop;
                case 'r': return BlackRook;
                case 'q': return BlackQueen;
                case 'k': return BlackKing;

                default: return NoPiece;
            }
        }

        public static Piece MakePiece (Color c, PieceType pt)
        {
            return (c << 3) + pt;
        }

        #endregion

        #region Operator

        public static implicit operator Piece (int value)
        {
            switch (value)
            {
                case 0: return WhitePawn;
                case 1: return WhiteKnight;
                case 2: return WhiteBishop;
                case 3: return WhiteRook;
                case 4: return WhiteQueen;
                case 5: return WhiteKing;

                case 8: return BlackPawn;
                case 9: return BlackKnight;
                case 10: return BlackBishop;
                case 11: return BlackRook;
                case 12: return BlackQueen;
                case 13: return BlackKing;

                default: return NoPiece;
            }
        }

        public static implicit operator int (Piece pc) { return pc._Value; }

        public static Piece operator ~ (Piece pc)
        {
            return pc._Value ^ 8;
        }

        //public static bool operator == (Piece pc1, Piece pc2)
        //{
        //    // If both are null, or both are same instance, return true.
        //    if (ReferenceEquals (pc1, pc2))
        //    {
        //        return true;
        //    }
        //    // If one is null, but not both, return false.
        //    if (ReferenceEquals (null, pc1)
        //     || ReferenceEquals (null, pc2))
        //    {
        //        return false;
        //    }
        //    // Return true if the fields match:
        //    return pc1._Value == pc2._Value;
        //}

        //public static bool operator != (Piece pc1, Piece pc2)
        //{
        //    return !(pc1 == pc2);
        //}

        #endregion

        #region Override

        public override bool Equals (object obj)
        {
            return obj is Piece ? obj as Piece == this : false;
        }

        public override int GetHashCode ()
        {
            return _Value.GetHashCode ();
        }

        public override string ToString ()
        {
            return Ok ? Notation : "(no piece)";
        }

        #endregion

        #endregion
    }
}