using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Chess
{
    [Serializable]
    public class Bitboard
    {

#   if !ABM
        [StructLayout (LayoutKind.Explicit)]
        public class Slice
        {
            [FieldOffset (0)]
            public ulong b;
            [FieldOffset (0)]
            [MarshalAs (UnmanagedType.ByValArray, SizeConst = 4)]
            public ushort[] u16 = new ushort[4];
        };
#   endif


#region Fields

        private ulong _Value;

#endregion


#region Properties

        public string Notation
        {
            get
            {
                using (var sw = new StringWriter ())
                {
                    sw.WriteLine (" /---------------\\");
                    for (var r = Rank.R_8; r >= Rank.R_1; --r)
                    {
                        sw.Write (r.Notation + "|");

                        for (var f = File.F_A; f <= File.F_H; ++f)
                        {
                            sw.Write (Contains (Square.MakeSquare (f, r)) ? '+' : '-');
                            if (f < File.F_H)
                            {
                                sw.Write (" ");
                            }
                        }
                        sw.WriteLine ("|");
                        if (r == Rank.R_1)
                        {
                            break;
                        }
                    }
                    sw.WriteLine (" \\---------------/");
                    sw.Write (" ");
                    for (var f = File.F_A; f <= File.F_H; ++f)
                    {
                        sw.Write (" " + f.Notation);
                    }
                    sw.WriteLine ();

                    return sw.ToString ();
                }
            }
        }

#endregion

#region Constructors

        private Bitboard (ulong value = 0)
        {
            _Value = value;
        }

#endregion


#region Methods

#region Static

        public static implicit operator Bitboard (ulong value) { return new Bitboard (value); }
        public static implicit operator ulong (Bitboard bb) { return bb._Value; }

        //public static Bitboard operator ~ (Bitboard bb) { return ~bb._Value; }
        //public static Bitboard operator & (Bitboard bb1, Bitboard bb2) { return bb1._Value & bb2._Value; }
        //public static Bitboard operator | (Bitboard bb1, Bitboard bb2) { return bb1._Value | bb2._Value; }
        //public static Bitboard operator ^ (Bitboard bb1, Bitboard bb2) { return bb1._Value ^ bb2._Value; }

        public static Bitboard operator | (Bitboard bb, Square s) { return bb | Global.Square_bb (s); }
        public static Bitboard operator ^ (Bitboard bb, Square s) { return bb ^ Global.Square_bb (s); }


        public static bool operator == (Bitboard bb1, Bitboard bb2)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals (bb1, bb2))
            {
                return true;
            }
            // If one is null, but not both, return false.
            if (ReferenceEquals (null, bb1)
             || ReferenceEquals (null, bb2))
            {
                return false;
            }
            // Return true if the fields match:
            return bb1._Value == bb2._Value;
        }
        public static bool operator != (Bitboard bb1, Bitboard bb2)
        {
            return !(bb1 == bb2);
        }

#endregion

        public bool Contains (Square s) { Debug.Assert (s.Ok); return 0 != (this & Global.Square_bb (s)); }

        public Bitboard Shift (Delta del)
        {
            switch (del)
            {
                case Delta.DEL_O: return (this);
                case Delta.DEL_N: return (this) << +(int)del;
                case Delta.DEL_S: return (this) >> -(int)del;
                case Delta.DEL_NN: return (this) << +(int)del;
                case Delta.DEL_SS: return (this) >> -(int)del;
                case Delta.DEL_E: return (this & ~Global.FH_bb) << +(int)del;
                case Delta.DEL_W: return (this & ~Global.FA_bb) >> -(int)del;
                case Delta.DEL_NE: return (this & ~Global.FH_bb) << +(int)del;
                case Delta.DEL_SE: return (this & ~Global.FH_bb) >> -(int)del;
                case Delta.DEL_SW: return (this & ~Global.FA_bb) >> -(int)del;
                case Delta.DEL_NW: return (this & ~Global.FA_bb) << +(int)del;
                case Delta.DEL_EE: return (this & ~(Global.FG_bb | Global.FH_bb)) << +(int)del;
                case Delta.DEL_WW: return (this & ~(Global.FA_bb | Global.FB_bb)) >> -(int)del;

                case Delta.DEL_NNE: return (this & ~(Global.FH_bb)) << +(int)del;
                case Delta.DEL_NNW: return (this & ~(Global.FA_bb)) << +(int)del;
                case Delta.DEL_EEN: return (this & ~(Global.FG_bb | Global.FH_bb)) << +(int)del;
                case Delta.DEL_WWN: return (this & ~(Global.FA_bb | Global.FB_bb)) << +(int)del;
                case Delta.DEL_SSE: return (this & ~(Global.FH_bb)) >> -(int)del;
                case Delta.DEL_SSW: return (this & ~(Global.FA_bb)) >> -(int)del;
                case Delta.DEL_EES: return (this & ~(Global.FG_bb | Global.FH_bb)) >> -(int)del;
                case Delta.DEL_WWS: return (this & ~(Global.FA_bb | Global.FB_bb)) >> -(int)del;
                
                default: return 0;
            }
        }

        public byte PopCount ()
        {
#       if !ABM
            var s = new Slice { b = this };

            return (byte)(Global.PopCount16[s.u16[0]]
                        + Global.PopCount16[s.u16[1]]
                        + Global.PopCount16[s.u16[2]]
                        + Global.PopCount16[s.u16[3]]);
#       else
            var b = this;
            b = b - ((b >> 1) & 0x5555555555555555UL);
            b = (b & 0x3333333333333333UL) + ((b >> 2) & 0x3333333333333333UL);
            return (byte)((((b + (b >> 4)) & 0x0F0F0F0F0F0F0F0FUL) * 0x0101010101010101UL) >> 56);
#       endif
        }

#region Override

        public override bool Equals (object obj)
        {
            return obj is Bitboard ? obj as Bitboard == this : false;
        }

        public override int GetHashCode ()
        {
            return _Value.GetHashCode ();
        }

        public override string ToString ()
        {
            return _Value.ToString ();
        }

#endregion

#endregion

    }
}
