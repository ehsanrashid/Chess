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
#           if BIT64
            return (ushort)(((occ & Mask) * Number) >> Shift);
#           else
            var lo = ((uint)(occ >> 0x00) & (uint)(Mask >> 0x00)) * (uint)(Number >> 0x00);
            var hi = ((uint)(occ >> 0x20) & (uint)(Mask >> 0x20)) * (uint)(Number >> 0x20);
            return (ushort)((lo ^ hi) >> Shift);
#           endif
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
