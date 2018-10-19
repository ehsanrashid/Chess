using System;

namespace Chess
{
    [Serializable]
    public class Magic
    {

        public Bitboard Mask;

#   if !BM2
        public Bitboard Number;
        public byte Shift;
#   endif

        public Bitboard[] Attacks;


        public ushort Index (Bitboard occ)
        {
#       if !BM2
            return (ushort)(((occ & Mask) * Number) >> Shift);
#       else
            return 0;
#       endif
        }

        public Bitboard Attacks_bb (Bitboard occ)
        {
            return Attacks[Index (occ)];
        }


    }
}
