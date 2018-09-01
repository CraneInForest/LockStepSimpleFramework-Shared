//
// @brief: 自定义随机数
// @version: 1.0.0
// @author helin
// @date: 03/7/2018
// 
// 
//

using System;
public class SRandom
{
    public static int count = 0;

    ulong randSeed = 1;
    public SRandom(uint seed)
    {
        randSeed = seed;
    }

    public uint Next()
    {
        randSeed = randSeed * 1103515245 + 12345;
        return (uint)(randSeed / 65536);
    }

    // range:[0 ~(max-1)]
    public uint Next(uint max)
    {
        return Next() % max;
    }

    // range:[min~(max-1)]
    public uint Range(uint min, uint max)
    {
        if (min > max)
            throw new ArgumentOutOfRangeException("minValue", string.Format("'{0}' cannot be greater than {1}.", min, max));

        uint num = max - min;
        return Next(num) + min;
    }

    public int Next(int max)
    {
        return (int)(Next() % max);
    }

    public int Range(int min, int max)
    {
        count++;

        if (min > max)
            throw new ArgumentOutOfRangeException("minValue", string.Format("'{0}' cannot be greater than {1}.", min, max));

        int num = max - min;

        return Next(num) + min;
    }

    public Fix64 Range(Fix64 min, Fix64 max)
    {
        if (min > max)
            throw new ArgumentOutOfRangeException("minValue", string.Format("'{0}' cannot be greater than {1}.", min, max));

        uint num = (uint)(max.RawValue - min.RawValue);
        return Fix64.FromRaw(Next(num) + min.RawValue);
    }
}
