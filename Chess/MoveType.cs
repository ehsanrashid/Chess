namespace Chess
{
    /// <summary>
    /// Move Type
    /// </summary>
    public enum MoveType : ushort
    {
        NORMAL    = 0x0000, // 0000
        CASTLE    = 0x4000, // 0100
        ENPASSANT = 0x8000, // 1000
        PROMOTE   = 0xC000, // 11xx
    }
}
