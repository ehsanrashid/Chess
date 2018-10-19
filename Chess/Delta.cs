namespace Chess
{
    /// <summary>
    /// 
    /// </summary>
    public enum Delta : sbyte
    {
        DEL_O = 0,

        DEL_N = 8,
        DEL_E = 1,
        DEL_S = -DEL_N,
        DEL_W = -DEL_E,

        DEL_NN = DEL_N + DEL_N,
        DEL_EE = DEL_E + DEL_E,
        DEL_SS = DEL_S + DEL_S,
        DEL_WW = DEL_W + DEL_W,

        DEL_NE = DEL_N + DEL_E,
        DEL_SE = DEL_S + DEL_E,
        DEL_SW = DEL_S + DEL_W,
        DEL_NW = DEL_N + DEL_W,

        DEL_NNE = DEL_NN + DEL_E,
        DEL_NNW = DEL_NN + DEL_W,

        DEL_EEN = DEL_EE + DEL_N,
        DEL_EES = DEL_EE + DEL_S,

        DEL_SSE = DEL_SS + DEL_E,
        DEL_SSW = DEL_SS + DEL_W,

        DEL_WWN = DEL_WW + DEL_N,
        DEL_WWS = DEL_WW + DEL_S,
    }
}
