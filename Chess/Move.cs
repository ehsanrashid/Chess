using System;

namespace Chess
{
    /// <summary>
    /// Move stored in 16-bits
    /// bit 00-05: destiny square of move: (0...63)
    /// bit 06-11:  origin square of move: (0...63)
    /// bit 12-13: promotion piece of move: (Knight...Queen) - 1
    /// bit 14-15: type of move: (0) Normal (1) Castle, (2) En-Passant, (3) Promotion
    /// NOTE: EN-PASSANT bit is set only when a pawn can be captured
    ///
    /// Special cases are MOVE::NONE and MOVE::NULL.
    /// </summary>
    [Serializable]
    public class Move
    {
        #region Fields

        #region Static

        public static readonly Move None;
        public static readonly Move Null;

        #endregion

        private ushort _Value;

        #endregion

        #region Properties

        public Square Org { get { return (_Value >> 6) & Square.H8; } }

        public Square Dst { get { return (_Value >> 0) & Square.H8; } }

        public PieceType Promote { get { return ((_Value >> 12) & 3) + 1; } }

        public MoveType Type { get { return (MoveType)(_Value & (ushort)MoveType.PROMOTE); } }

        public bool Ok { get { return Org != Dst; } }

        #endregion

        #region Constructors

        #region Static

        static Move ()
        {
            None = new Move (0x00);
            Null = new Move (0x41);
        }

        #endregion

        private Move (ushort value)
        {
            _Value = value;
        }

        #endregion

        #region Methods

        #region Static

        #region Operator

        public static implicit operator Move (ushort value)
        {
            switch (value)
            {
                case 0x00: return None;
                case 0x41: return Null;

                default: return new Move (value);
            }
        }
        public static implicit operator ushort (Move move) { return move._Value; }

        //public static bool operator == (Move move1, Move move2)
        //{
        //    // If both are null, or both are same instance, return true.
        //    if (ReferenceEquals (move1, move2))
        //    {
        //        return true;
        //    }
        //    // If one is null, but not both, return false.
        //    if (ReferenceEquals (null, move1)
        //     || ReferenceEquals (null, move2))
        //    {
        //        return false;
        //    }
        //    // Return true if the fields match:
        //    return move1._Value == move2._Value;
        //}
        //public static bool operator != (Move move1, Move move2)
        //{
        //    return !(move1 == move2);
        //}

        #endregion

        public static Move MakeMove (Square org, Square dst)
        {
            return (Move)((ushort)MoveType.NORMAL + (org << 6) + dst);
        }
        public static Move MakeMove (Square org, Square dst, MoveType MT)
        {
            return (Move)((ushort)MT + (org << 6) + dst);
        }
        public static Move MakeMove (Square org, Square dst, PieceType pt)
        {
            return (Move)((ushort)MoveType.PROMOTE + ((((pt - PieceType.NIHT) << 6) + org) << 6) + dst);
        }

        #endregion

        #region Override

        public override bool Equals (object obj)
        {
            return obj is Move ? obj as Move == this : false;
        }

        public override int GetHashCode ()
        {
            return _Value.GetHashCode ();
        }

        public override string ToString ()
        {
            return
                _Value == None._Value ? "(none)" :
                _Value == Null._Value ? "(null)" :
                Ok ? Org.ToString () + Dst.ToString () + (Type == MoveType.PROMOTE ? Promote.Notation : "") : "(no move)";
        }

        #endregion

        #endregion
    }

}
