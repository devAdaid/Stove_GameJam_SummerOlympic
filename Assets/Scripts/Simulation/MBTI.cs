public class MBTI
{
    public MBTIType_M M { get; private set; }
    public MBTIType_B B { get; private set; }
    public MBTIType_T T { get; private set; }
    public MBTIType_I I { get; private set; }

    public MBTI()
    {
        M = MBTIType_M.Invalid;
        B = MBTIType_B.Invalid;
        T = MBTIType_T.Invalid;
        I = MBTIType_I.Invalid;
    }

    // MBTI 하나씩 고를 경우를 대비한 메소드들
    public void SetM(MBTIType_M m)
    {
        M = m;
    }

    public void SetB(MBTIType_B b)
    {
        B = b;
    }

    public void SetT(MBTIType_T t)
    {
        T = t;
    }

    public void SetI(MBTIType_I i)
    {
        I = i;
    }

    public bool IsValid()
    {
        return M != MBTIType_M.Invalid
            && B != MBTIType_B.Invalid
            && T != MBTIType_T.Invalid
            && I != MBTIType_I.Invalid;
    }

    public override string ToString()
    {
        return M.GetString() + B.GetString() + T.GetString() + I.GetString();
    }
}