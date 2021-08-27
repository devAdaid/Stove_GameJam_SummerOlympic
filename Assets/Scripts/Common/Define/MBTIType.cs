public enum MBTIType_M
{
    Invalid = 0,
    E,
    I,
}


public enum MBTIType_B
{
    Invalid = 0,
    S,
    N,
}

public enum MBTIType_T
{
    Invalid = 0,
    T,
    F,
}

public enum MBTIType_I
{
    Invalid = 0,
    P,
    J,
}

internal static class MBTIExtensions
{
    public static string GetString(this MBTIType_M m) => m
    switch
    {
        MBTIType_M.E => "E",
        MBTIType_M.I => "I",
        _ => "?"
    };

    public static string GetString(this MBTIType_B b) => b
    switch
    {
        MBTIType_B.N => "N",
        MBTIType_B.S => "S",
        _ => "?"
    };

    public static string GetString(this MBTIType_T t) => t
    switch
    {
        MBTIType_T.F => "F",
        MBTIType_T.T => "T",
        _ => "?"
    };

    public static string GetString(this MBTIType_I i) => i
    switch
    {
        MBTIType_I.J => "J",
        MBTIType_I.P => "P",
        _ => "?"
    };
}