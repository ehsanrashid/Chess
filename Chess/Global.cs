using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace Chess
{

    public static class Global
    {

        #region Fields

        public static readonly Bitboard Empty;

        public static readonly Bitboard FA_bb;
        public static readonly Bitboard FB_bb;
        public static readonly Bitboard FC_bb;
        public static readonly Bitboard FD_bb;
        public static readonly Bitboard FE_bb;
        public static readonly Bitboard FF_bb;
        public static readonly Bitboard FG_bb;
        public static readonly Bitboard FH_bb;

        public static readonly Bitboard R1_bb;
        public static readonly Bitboard R2_bb;
        public static readonly Bitboard R3_bb;
        public static readonly Bitboard R4_bb;
        public static readonly Bitboard R5_bb;
        public static readonly Bitboard R6_bb;
        public static readonly Bitboard R7_bb;
        public static readonly Bitboard R8_bb;

        public static readonly Bitboard Corner_bb;
        public static readonly Bitboard[] Color_bb;


        private static readonly Bitboard[] _Square_bb;
        private static readonly Bitboard[] _File_bb;
        private static readonly Bitboard[] _Rank_bb;

        private static readonly Bitboard[] _AdjFile_bb;
        private static readonly Bitboard[] _AdjRank_bb;

        private static readonly byte[,] _SquareDist;

        private static readonly Bitboard[,] _FrontRank_bb;

        private static readonly Bitboard[,] _FrontSquare_bb;

        private static readonly Bitboard[,] _PawnAttackSpan;
        private static readonly Bitboard[,] _PawnPassSpan;


        private static readonly Delta[][] PieceDeltas =
        {
            new[] { Delta.DEL_O },
            new[] { Delta.DEL_SSW, Delta.DEL_SSE, Delta.DEL_WWS, Delta.DEL_EES, Delta.DEL_WWN, Delta.DEL_EEN, Delta.DEL_NNW, Delta.DEL_NNE },
            new[] { Delta.DEL_SW, Delta.DEL_SE, Delta.DEL_NW, Delta.DEL_NE },
            new[] { Delta.DEL_S, Delta.DEL_W, Delta.DEL_E, Delta.DEL_N },
            new[] { Delta.DEL_SW, Delta.DEL_S, Delta.DEL_SE, Delta.DEL_W, Delta.DEL_E, Delta.DEL_NW, Delta.DEL_N, Delta.DEL_NE },
            new[] { Delta.DEL_SW, Delta.DEL_S, Delta.DEL_SE, Delta.DEL_W, Delta.DEL_E, Delta.DEL_NW, Delta.DEL_N, Delta.DEL_NE },
        };

        private static readonly Bitboard[,] _PawnAttacks;
        private static readonly Bitboard[,] _PieceAttacks;


#   if !ABM
        public static byte[] PopCount16;
#   endif


        private static Magic[] BMagics;
        private static Magic[] RMagics;

        #endregion


        static Global ()
        {
            Empty = 0x0000000000000000;

            FA_bb = 0x0101010101010101;
            FB_bb = 0x0202020202020202;
            FC_bb = 0x0404040404040404;
            FD_bb = 0x0808080808080808;
            FE_bb = 0x1010101010101010;
            FF_bb = 0x2020202020202020;
            FG_bb = 0x4040404040404040;
            FH_bb = 0x8080808080808080;

            R1_bb = 0x00000000000000FF;
            R2_bb = 0x000000000000FF00;
            R3_bb = 0x0000000000FF0000;
            R4_bb = 0x00000000FF000000;
            R5_bb = 0x000000FF00000000;
            R6_bb = 0x0000FF0000000000;
            R7_bb = 0x00FF000000000000;
            R8_bb = 0xFF00000000000000;

            Corner_bb = (FA_bb | FH_bb) & (R1_bb | R8_bb); // 32 DARK  squares.

            Color_bb = new Bitboard[Color.Max]
            {
                0x55AA55AA55AA55AA, // 32 LIGHT squares.
                0xAA55AA55AA55AA55  // 32 DARK  squares.
            };


            _Square_bb = new Bitboard[Square.Max]
            {
                0x0000000000000001,
                0x0000000000000002,
                0x0000000000000004,
                0x0000000000000008,
                0x0000000000000010,
                0x0000000000000020,
                0x0000000000000040,
                0x0000000000000080,
                0x0000000000000100,
                0x0000000000000200,
                0x0000000000000400,
                0x0000000000000800,
                0x0000000000001000,
                0x0000000000002000,
                0x0000000000004000,
                0x0000000000008000,
                0x0000000000010000,
                0x0000000000020000,
                0x0000000000040000,
                0x0000000000080000,
                0x0000000000100000,
                0x0000000000200000,
                0x0000000000400000,
                0x0000000000800000,
                0x0000000001000000,
                0x0000000002000000,
                0x0000000004000000,
                0x0000000008000000,
                0x0000000010000000,
                0x0000000020000000,
                0x0000000040000000,
                0x0000000080000000,
                0x0000000100000000,
                0x0000000200000000,
                0x0000000400000000,
                0x0000000800000000,
                0x0000001000000000,
                0x0000002000000000,
                0x0000004000000000,
                0x0000008000000000,
                0x0000010000000000,
                0x0000020000000000,
                0x0000040000000000,
                0x0000080000000000,
                0x0000100000000000,
                0x0000200000000000,
                0x0000400000000000,
                0x0000800000000000,
                0x0001000000000000,
                0x0002000000000000,
                0x0004000000000000,
                0x0008000000000000,
                0x0010000000000000,
                0x0020000000000000,
                0x0040000000000000,
                0x0080000000000000,
                0x0100000000000000,
                0x0200000000000000,
                0x0400000000000000,
                0x0800000000000000,
                0x1000000000000000,
                0x2000000000000000,
                0x4000000000000000,
                0x8000000000000000,
            };

            _File_bb = new Bitboard[File.Max]
            {
                FA_bb, FB_bb, FC_bb, FD_bb, FE_bb, FF_bb, FG_bb, FH_bb
            };

            _Rank_bb = new Bitboard[Rank.Max]
            {
                R1_bb, R2_bb, R3_bb, R4_bb, R5_bb, R6_bb, R7_bb, R8_bb
            };

            _AdjFile_bb = new Bitboard[File.Max]
            {
                FB_bb,
                FA_bb|FC_bb,
                FB_bb|FD_bb,
                FC_bb|FE_bb,
                FD_bb|FF_bb,
                FE_bb|FG_bb,
                FF_bb|FH_bb,
                FG_bb
            };

            _AdjRank_bb = new Bitboard[Rank.Max]
            {
                R2_bb,
                R1_bb|R3_bb,
                R2_bb|R4_bb,
                R3_bb|R5_bb,
                R4_bb|R6_bb,
                R5_bb|R7_bb,
                R6_bb|R8_bb,
                R7_bb,
            };

            _SquareDist = new byte[Square.Max, Square.Max];

            for (var s1 = Square.A1; s1 <= Square.H8; ++s1)
            {
                //for (var i = _DistRings_BB.GetLowerBound (0); i <= _DistRings_BB.GetUpperBound (1); ++i)
                //{
                //    _DistRings_BB[s1, i] = new Bitboard ();
                //}
                for (var s2 = Square.A1; s2 <= Square.H8; ++s2)
                {
                    if (s1 != s2)
                    {
                        _SquareDist[s1, s2] = Math.Max (FileDist (s1, s2), RankDist (s1, s2));
                        //_DistRings_BB[s1, _SquareDist[s1, s2] - 1] |= s2;
                    }
                }
            }


            _FrontRank_bb = new Bitboard[Color.Max, Rank.Max]
            {
                {
                R2_bb|R3_bb|R4_bb|R5_bb|R6_bb|R7_bb|R8_bb,
                R3_bb|R4_bb|R5_bb|R6_bb|R7_bb|R8_bb,
                R4_bb|R5_bb|R6_bb|R7_bb|R8_bb,
                R5_bb|R6_bb|R7_bb|R8_bb,
                R6_bb|R7_bb|R8_bb,
                R7_bb|R8_bb,
                R8_bb,
                0,
                },
                {
                0,
                R1_bb,
                R2_bb|R1_bb,
                R3_bb|R2_bb|R1_bb,
                R4_bb|R3_bb|R2_bb|R1_bb,
                R5_bb|R4_bb|R3_bb|R2_bb|R1_bb,
                R6_bb|R5_bb|R4_bb|R3_bb|R2_bb|R1_bb,
                R7_bb|R6_bb|R5_bb|R4_bb|R3_bb|R2_bb|R1_bb
                }
            };

            _FrontSquare_bb = new Bitboard[Color.Max, Square.Max];
            _PawnAttackSpan = new Bitboard[Color.Max, Square.Max];
            _PawnPassSpan = new Bitboard[Color.Max, Square.Max];

            _PawnAttacks = new Bitboard[Color.Max, Square.Max];

            for (var c = Color.WHITE; c <= Color.BLACK; ++c)
            {
                for (var s = Square.A1; s <= Square.H8; ++s)
                {
                    _FrontSquare_bb[c, s] = _FrontRank_bb[c, s.Rank] & _File_bb[s.File];
                    _PawnAttackSpan[c, s] = _FrontRank_bb[c, s.Rank] & _AdjFile_bb[s.File];
                    _PawnPassSpan[c, s] = _FrontSquare_bb[c, s] | _PawnAttackSpan[c, s];
                    _PawnAttacks[c, s] = _PawnAttackSpan[c, s] & _AdjRank_bb[s.Rank];
                }
            }


            _PieceAttacks = new Bitboard[PieceType.Max, Square.Max];
            for (var s = Square.A1; s <= Square.H8; ++s)
            {
                //_PawnAttacks[Color.WHITE, s] = 0;
                //foreach (var del in new[] { Delta.DEL_NW, Delta.DEL_NE })
                //{
                //    var sq = s + del;
                //    if (sq.Ok && 1 == SquareDist (s, sq))
                //    {
                //        _PawnAttacks[Color.WHITE, s] |= sq;
                //    }
                //}
                //_PawnAttacks[Color.BLACK, s] = 0;
                //foreach (var del in new[] { Delta.DEL_SE, Delta.DEL_SW })
                //{
                //    var sq = s + del;
                //    if (sq.Ok && 1 == SquareDist (s, sq))
                //    {
                //        _PawnAttacks[Color.BLACK, s] |= sq;
                //    }
                //}

                _PieceAttacks[PieceType.NIHT, s] = 0;
                foreach (var del in PieceDeltas[PieceType.NIHT])
                {
                    var sq = s + del;
                    if (sq.Ok && 2 == SquareDist (s, sq))
                    {
                        _PieceAttacks[PieceType.NIHT, s] |= sq;
                    }
                }

                _PieceAttacks[PieceType.KING, s] = 0;
                foreach (var del in PieceDeltas[PieceType.KING])
                {
                    var sq = s + del;
                    if (sq.Ok && 1 == SquareDist (s, sq))
                    {
                        _PieceAttacks[PieceType.KING, s] |= sq;
                    }
                }

                _PieceAttacks[PieceType.BSHP, s] = SlideAttacks (PieceType.BSHP, s);
                _PieceAttacks[PieceType.ROOK, s] = SlideAttacks (PieceType.ROOK, s);
                _PieceAttacks[PieceType.QUEN, s] = _PieceAttacks[PieceType.BSHP, s] | _PieceAttacks[PieceType.ROOK, s];
            }

#if !ABM
            PopCount16 = new byte[1 << 16];
            for (UInt32 i = 0; i < (1 << 16); ++i)
            {
                PopCount16[i] = PopCount_16 (i);
            }
#endif




        }

        public static void Init ()
        {
            BMagics = new Magic[Square.Max];
            InitializeTable (PieceType.BSHP, BMagics);

            RMagics = new Magic[Square.Max];
            InitializeTable (PieceType.ROOK, RMagics);
        }

        public static Bitboard Square_bb (Square s) { Debug.Assert (s.Ok); return _Square_bb[s]; }

        public static Bitboard File_bb (File f) { return _File_bb[f]; }
        public static Bitboard File_bb (Square s) { return File_bb (s.File); }

        public static Bitboard Rank_bb (Rank r) { return _Rank_bb[r]; }
        public static Bitboard Rank_bb (Square s) { return Rank_bb (s.Rank); }

        public static byte SquareDist (Square s1, Square s2) { Debug.Assert (s1.Ok); Debug.Assert (s2.Ok); return _SquareDist[s1, s2]; }
        public static byte FileDist (Square s1, Square s2) { Debug.Assert (s1.Ok); Debug.Assert (s2.Ok); return (byte)(s1.File < s2.File ? s2.File - s1.File : s1.File - s2.File); }
        public static byte RankDist (Square s1, Square s2) { Debug.Assert (s1.Ok); Debug.Assert (s2.Ok); return (byte)(s1.Rank < s2.Rank ? s2.Rank - s1.Rank : s1.Rank - s2.Rank); }


        public static Bitboard FrontRank_bb (Color c, Rank r) { Debug.Assert (r.Ok); return _FrontRank_bb[c, r]; }
        public static Bitboard FrontRank_bb (Color c, Square s) { Debug.Assert (s.Ok); return FrontRank_bb (c, s.Rank); }

        public static Bitboard FrontSquare_bb (Color c, Square s) { Debug.Assert (s.Ok); return _FrontSquare_bb[c, s]; }

        public static Bitboard PawnAttackSpan (Color c, Square s) { Debug.Assert (s.Ok); return _PawnAttackSpan[c, s]; }
        public static Bitboard PawnPassSpan (Color c, Square s) { Debug.Assert (s.Ok); return _PawnPassSpan[c, s]; }

        public static Bitboard PawnAttacks (Color c, Square s) { Debug.Assert (s.Ok); return _PawnAttacks[c, s]; }
        public static Bitboard PieceAttacks (PieceType pt, Square s) { Debug.Assert (PieceType.PAWN < pt && pt <= PieceType.KING); Debug.Assert (s.Ok); return _PieceAttacks[pt, s]; }

#if !ABM
        // PopCount_16() counts the non-zero bits using SWAR-Popcount algorithm
        public static byte PopCount_16 (UInt32 u)
        {
            u -= (u >> 1) & 0x5555U;
            u = ((u >> 2) & 0x3333U) + (u & 0x3333U);
            u = ((u >> 4) + u) & 0x0F0FU;
            return (byte)((u * 0x0101U) >> 8);
        }
#endif

        public static Bitboard SlideAttacks (PieceType pt, Square s, Bitboard occ)
        {
            Bitboard attacks = 0;
            foreach (var del in PieceDeltas[pt])
            {
                for (var sq = s + del; sq.Ok && 1 == SquareDist (sq, sq - del); sq += del)
                {
                    attacks |= sq;
                    if (occ.Contains (sq))
                    {
                        break;
                    }
                }
            }
            return attacks;
        }
        public static Bitboard SlideAttacks (PieceType pt, Square s)
        {
            return SlideAttacks (pt, s, 0);
        }

        private static void InitializeTable (PieceType pt, Magic[] magics)
        {

#       if !BM2
            const UInt16 MaxLTSize = 0x1000;
            var occupancy = new Bitboard[MaxLTSize];
            var reference = new Bitboard[MaxLTSize];

            var Seeds = new UInt32[Rank.Max]
#           if BIT64
                { 0x002D8, 0x0284C, 0x0D6E5, 0x08023, 0x02FF9, 0x03AFC, 0x04105, 0x000FF };
#           else
                { 0x02311, 0x0AE10, 0x0D447, 0x09856, 0x01663, 0x173E5, 0x199D0, 0x0427C };
#           endif

#       endif

            // attacksBB[s] is a pointer to the beginning of the attacks table for square 's'
            //var totalSize = 0U;
            for (var s = Square.A1; s <= Square.H8; ++s)
            {

                var magic = magics[s] = new Magic();

                // Given a square 's', the mask is the bitboard of sliding attacks from 's'
                // computed on an empty board. The index must be big enough to contain
                // all the attacks for each possible subset of the mask and so is 2 power
                // the number of 1s of the mask. Hence deduce the size of the shift to
                // apply to the 64 or 32 bits word to get the index.
                magic.Mask = SlideAttacks (pt, s)
                            // Board edges are not considered in the relevant occupancies
                           & ~(((FA_bb | FH_bb) & ~File_bb (s)) | ((R1_bb | R8_bb) & ~Rank_bb (s)));

                var maskPopCount = magic.Mask.PopCount ();
                magic.Attacks = new Bitboard[(int)Math.Pow (2, maskPopCount)];

#           if !BM2
                magic.Shift = (byte)(
#               if BIT64
                    64
#               else
                    32
#               endif
                    - maskPopCount);
#           endif


                // Use Carry-Rippler trick to enumerate all subsets of masksBB[s] and
                // store the corresponding sliding attack bitboard in reference[].
                var size = 0U;
                Bitboard occ = 0;

                do
                {
#               if BM2
                    attacksBB[s][PEXT(occ, masksBB[s])] = sliding_attacks (deltas, s, occ);
#               else
                    occupancy[size] = occ;
                    reference[size] = SlideAttacks (pt, s, occ);
#               endif
                    
                    ++size;
                    occ = (occ - magic.Mask) & magic.Mask;
                } while (occ != 0);


#           if !BM2
                var prng = new PRNG (Seeds[s.Rank]);
                var i = 0U;

                // Find a magic for square 's' picking up an (almost) random number
                // until found the one that passes the verification test.
                do
                {
                    do
                    {
                        magic.Number = prng.SparseRand<ulong> ();
                    } while (((Bitboard)((magic.Mask * magic.Number) >> 0x38)).PopCount() < 6);

                    // A good magic must map every possible occupancy to an index that
                    // looks up the correct sliding attack in the attacksBB[s] database.
                    // Note that build up the database for square 's' as a side
                    // effect of verifying the magic.
                    var used = Enumerable.Repeat (false, (int)size).ToArray ();
                    for (i = 0; i < size; ++i)
                    {
                        var idx = magic.Index (occupancy[i]);
                        Debug.Assert (idx < size);
                        if (used[idx])
                        {
                            if (magic.Attacks[idx] != reference[i])
                            {
                                break;
                            }
                            continue;
                        }
                        used[idx] = true;
                        magic.Attacks[idx] = reference[i];
                    }

                } while (i < size);
#           endif
                //totalSize += size;

            }




        }


        public static Bitboard Attacks (PieceType pt, Square s, Bitboard occ)
        {
            if (PieceType.NIHT == pt)
            {
                return _PieceAttacks[PieceType.NIHT, s];
            }
            else
            if (PieceType.BSHP == pt)
            {
                return BMagics[s].Attacks_bb (occ);
            }
            else
            if (PieceType.ROOK == pt)
            {
                return RMagics[s].Attacks_bb (occ);
            }
            else
            if (PieceType.QUEN == pt)
            {
                return BMagics[s].Attacks_bb (occ)
                     | RMagics[s].Attacks_bb (occ);
            }
            else
            if (PieceType.KING == pt)
            {
                return _PieceAttacks[PieceType.KING, s];
            }
            else
            {
                return 0;
            }
        }


    }



}
