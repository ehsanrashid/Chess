using System;
using System.Collections.Generic;

namespace Chess
{
    public class Position
    {
        #region Properties

        public Piece[] _Piece { get; private set; }

        public Bitboard[] ColorBB { get; private set; }
        public Bitboard[] TypesBB { get; private set; }

        public List<Square>[,] Squares { get; private set; }

        #endregion

        #region Constructors

        public Position ()
        {

            _Piece = new Piece[Square.Max]
            {
                Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece,
                Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece,
                Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece,
                Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece,
                Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece,
                Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece,
                Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece,
                Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece, Piece.NoPiece
            };


            ColorBB = new Bitboard[Color.Max] { 0, 0 };
            TypesBB = new Bitboard[PieceType.Max + 1] { 0, 0, 0, 0, 0, 0, 0 };

            Squares = new List<Square>[Color.Max, PieceType.Max]
            {
                { new List<Square>(), new List<Square>(), new List<Square>(), new List<Square>(), new List<Square>(), new List<Square>() },
                { new List<Square>(), new List<Square>(), new List<Square>(), new List<Square>(), new List<Square>(), new List<Square>() }
            };



        }

        #endregion


    }
}
