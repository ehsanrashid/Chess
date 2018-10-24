using System;

using Chess;


// BM2;ABM;BIT64;

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
            Console.WriteLine (b.PopCount());

            Console.WriteLine (Global.Attacks (PieceType.BSHP, Square.D4, b).Notation);

            

        }
    }
}
