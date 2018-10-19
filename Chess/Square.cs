using System;
using System.Diagnostics;

namespace Chess
{
    /// <summary>
    /// Square
    /// </summary>
    [Serializable]
    public class Square
    {
        #region Fields

        #region Static

        public const int Max = File.Max * Rank.Max;

        public static readonly Square A1;
        public static readonly Square B1;
        public static readonly Square C1;
        public static readonly Square D1;
        public static readonly Square E1;
        public static readonly Square F1;
        public static readonly Square G1;
        public static readonly Square H1;

        public static readonly Square A2;
        public static readonly Square B2;
        public static readonly Square C2;
        public static readonly Square D2;
        public static readonly Square E2;
        public static readonly Square F2;
        public static readonly Square G2;
        public static readonly Square H2;

        public static readonly Square A3;
        public static readonly Square B3;
        public static readonly Square C3;
        public static readonly Square D3;
        public static readonly Square E3;
        public static readonly Square F3;
        public static readonly Square G3;
        public static readonly Square H3;

        public static readonly Square A4;
        public static readonly Square B4;
        public static readonly Square C4;
        public static readonly Square D4;
        public static readonly Square E4;
        public static readonly Square F4;
        public static readonly Square G4;
        public static readonly Square H4;

        public static readonly Square A5;
        public static readonly Square B5;
        public static readonly Square C5;
        public static readonly Square D5;
        public static readonly Square E5;
        public static readonly Square F5;
        public static readonly Square G5;
        public static readonly Square H5;

        public static readonly Square A6;
        public static readonly Square B6;
        public static readonly Square C6;
        public static readonly Square D6;
        public static readonly Square E6;
        public static readonly Square F6;
        public static readonly Square G6;
        public static readonly Square H6;

        public static readonly Square A7;
        public static readonly Square B7;
        public static readonly Square C7;
        public static readonly Square D7;
        public static readonly Square E7;
        public static readonly Square F7;
        public static readonly Square G7;
        public static readonly Square H7;

        public static readonly Square A8;
        public static readonly Square B8;
        public static readonly Square C8;
        public static readonly Square D8;
        public static readonly Square E8;
        public static readonly Square F8;
        public static readonly Square G8;
        public static readonly Square H8;

        public static readonly Square None;

        #endregion

        private sbyte _Value;

        #endregion

        #region Properties

        public File File { get { return _Value & 7; } }

        public Rank Rank { get { return _Value >> 3; } }

        public bool Ok { get { return A1._Value <= _Value && _Value <= H8._Value; } }

        #endregion

        #region Constructors

        #region Static

        static Square ()
        {
            A1 = new Square (0);
            B1 = new Square (1);
            C1 = new Square (2);
            D1 = new Square (3);
            E1 = new Square (4);
            F1 = new Square (5);
            G1 = new Square (6);
            H1 = new Square (7);

            A2 = new Square (8);
            B2 = new Square (9);
            C2 = new Square (10);
            D2 = new Square (11);
            E2 = new Square (12);
            F2 = new Square (13);
            G2 = new Square (14);
            H2 = new Square (15);

            A3 = new Square (16);
            B3 = new Square (17);
            C3 = new Square (18);
            D3 = new Square (19);
            E3 = new Square (20);
            F3 = new Square (21);
            G3 = new Square (22);
            H3 = new Square (23);

            A4 = new Square (24);
            B4 = new Square (25);
            C4 = new Square (26);
            D4 = new Square (27);
            E4 = new Square (28);
            F4 = new Square (29);
            G4 = new Square (30);
            H4 = new Square (31);

            A5 = new Square (32);
            B5 = new Square (33);
            C5 = new Square (34);
            D5 = new Square (35);
            E5 = new Square (36);
            F5 = new Square (37);
            G5 = new Square (38);
            H5 = new Square (39);

            A6 = new Square (40);
            B6 = new Square (41);
            C6 = new Square (42);
            D6 = new Square (43);
            E6 = new Square (44);
            F6 = new Square (45);
            G6 = new Square (46);
            H6 = new Square (47);

            A7 = new Square (48);
            B7 = new Square (49);
            C7 = new Square (50);
            D7 = new Square (51);
            E7 = new Square (52);
            F7 = new Square (53);
            G7 = new Square (54);
            H7 = new Square (55);

            A8 = new Square (56);
            B8 = new Square (57);
            C8 = new Square (58);
            D8 = new Square (59);
            E8 = new Square (60);
            F8 = new Square (61);
            G8 = new Square (62);
            H8 = new Square (63);

            None = new Square (64);
        }

        #endregion

        private Square (sbyte value)
        {
            _Value = value;
        }

        #endregion

        #region Methods

        public static implicit operator Square (int value)
        {
            switch (value)
            {
                case 0: return A1;
                case 1: return B1;
                case 2: return C1;
                case 3: return D1;
                case 4: return E1;
                case 5: return F1;
                case 6: return G1;
                case 7: return H1;

                case 8: return A2;
                case 9: return B2;
                case 10: return C2;
                case 11: return D2;
                case 12: return E2;
                case 13: return F2;
                case 14: return G2;
                case 15: return H2;

                case 16: return A3;
                case 17: return B3;
                case 18: return C3;
                case 19: return D3;
                case 20: return E3;
                case 21: return F3;
                case 22: return G3;
                case 23: return H3;

                case 24: return A4;
                case 25: return B4;
                case 26: return C4;
                case 27: return D4;
                case 28: return E4;
                case 29: return F4;
                case 30: return G4;
                case 31: return H4;

                case 32: return A5;
                case 33: return B5;
                case 34: return C5;
                case 35: return D5;
                case 36: return E5;
                case 37: return F5;
                case 38: return G5;
                case 39: return H5;

                case 40: return A6;
                case 41: return B6;
                case 42: return C6;
                case 43: return D6;
                case 44: return E6;
                case 45: return F6;
                case 46: return G6;
                case 47: return H6;

                case 48: return A7;
                case 49: return B7;
                case 50: return C7;
                case 51: return D7;
                case 52: return E7;
                case 53: return F7;
                case 54: return G7;
                case 55: return H7;

                case 56: return A8;
                case 57: return B8;
                case 58: return C8;
                case 59: return D8;
                case 60: return E8;
                case 61: return F8;
                case 62: return G8;
                case 63: return H8;

                default: return None;
            }

        }
        public static implicit operator int (Square s) { return s._Value; }

        public static Square RelSquare (Color c, Square s)
        {
            return s ^ (c * A8);
        }

        public static Square operator ~ (Square s)
        {
            return MakeSquare (s.File, ~s.Rank);
        }
        public static Square operator ! (Square s)
        {
            return MakeSquare (~s.File, s.Rank);
        }

        public static Delta operator - (Square s1, Square s2) { return (Delta)(s1._Value - s2._Value); }

        public static Square operator + (Square s, Delta d) { return (int)s + (int)d; }
        public static Square operator - (Square s, Delta d) { return (int)s - (int)d; }

        //public static bool operator == (Square s1, Square s2)
        //{
        //    // If both are null, or both are same instance, return true.
        //    if (ReferenceEquals (s1, s2))
        //    {
        //        return true;
        //    }
        //    // If one is null, but not both, return false.
        //    if (ReferenceEquals (null, s1)
        //     || ReferenceEquals (null, s2))
        //    {
        //        return false;
        //    }
        //    // Return true if the fields match:
        //    return s1._Value == s2._Value;
        //}
        //public static bool operator != (Square s1, Square s2)
        //{
        //    return !(s1 == s2);
        //}

        public static bool OppositeColors (Square s1, Square s2)
        {
            int s = s1 ^ s2;
            return (((s >> 3) ^ s) & 1) != 0;
        }

        public static Square MakeSquare (File f, Rank r)
        {
            return ( r << 3) + f;
        }
        public static Square MakeSquare (Rank r, File f)
        {
            return (~r << 3) + f;
        }

        public static Square ToSquare (char f, char r)
        {
            Debug.Assert (char.IsLetter (f)
                       && char.IsDigit (r));
            if (!char.IsLower (f)) f = char.ToLower (f);
            return MakeSquare (File.ToFile (f), Rank.ToRank (r));
        }

        public static Square ToSquare (string s)
        {
            Debug.Assert (s.Length == 2);
            return ToSquare (s[0], s[1]);
        }

        // TODO::
        public static Delta Push (Color c)
        {
            if (Color.WHITE == c)
            {
                return Delta.DEL_N;
            }
            else
            if (Color.BLACK == c)
            {
                return Delta.DEL_S;
            }
            else
            {
                Debug.Assert (false);
                return Delta.DEL_O;
            }
        }

        #region Override

        public override bool Equals (object obj)
        {
            return obj is Square ? obj as Square == this : false;
        }

        public override int GetHashCode ()
        {
            return _Value.GetHashCode ();
        }

        public override string ToString ()
        {
            return Ok ? File.ToString () + Rank.ToString () : "(no square)";
        }

        #endregion

        #endregion
    }
}
