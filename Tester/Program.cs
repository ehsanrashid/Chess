using System;

using Chess;

namespace Tester
{
    public class Program
    {
        public static void Main (string[] args)
        {
            Global.Init ();

            //Console.WriteLine (Global.PieceAttacks (PieceType.QUEN, Square.H8).Notation);
            Bitboard b = 12345656;

            Console.WriteLine (b.Notation);

            Console.WriteLine (Global.SlideAttacks (PieceType.BSHP, Square.D4, b).Notation);

            

        }
    }
}
