//
// @brief: 定点数
// @version: 1.0.0
// @author helin
// @date: 03/7/2018
// 
// 
//

using System;
using System.IO;

public partial struct Fix64 : IEquatable<Fix64>, IComparable<Fix64> {
    readonly long m_rawValue;

    public static readonly decimal Precision = (decimal)(new Fix64(1L));
    public static readonly Fix64 One = new Fix64(ONE);
    public static readonly Fix64 Zero = new Fix64();
    public static readonly Fix64 PI = new Fix64(Pi);
    public static readonly Fix64 PITimes2 = new Fix64(PiTimes2);
    public static readonly Fix64 PIOver180 = new Fix64((long)72);
    public static readonly Fix64 Rad2Deg = Fix64.Pi * (Fix64)2 / (Fix64)360;
    public static readonly Fix64 Deg2Rad = (Fix64)360 / (Fix64.Pi * (Fix64)2);

    const long Pi = 12868;
    const long PiTimes2 = 25736;

    public const int FRACTIONAL_PLACES = 12;
    const long ONE = 1L << FRACTIONAL_PLACES;

    public static int Sign(Fix64 value) {
        return
            value.m_rawValue < 0 ? -1 :
            value.m_rawValue > 0 ? 1 :
            0;
    }

    public static Fix64 Abs(Fix64 value) {
        return new Fix64(value.m_rawValue > 0 ? value.m_rawValue : -value.m_rawValue);
    }

    public static Fix64 Floor(Fix64 value) {
        return new Fix64((long)((ulong)value.m_rawValue & 0xFFFFFFFFFFFFF000));
    }

    public static Fix64 Ceiling(Fix64 value) {
        var hasFractionalPart = (value.m_rawValue & 0x0000000000000FFF) != 0;
        return hasFractionalPart ? Floor(value) + One : value;
    }

    public static Fix64 operator +(Fix64 x, Fix64 y) {
        return new Fix64(x.m_rawValue + y.m_rawValue);
    }

    public static Fix64 operator +(Fix64 x, int y)
    {
        return x + (Fix64)y;
    }

    public static Fix64 operator +(int x, Fix64 y)
    {
        return (Fix64)x + y;
    }

    public static Fix64 operator +(Fix64 x, float y)
    {
        return x + (Fix64)y;
    }

    public static Fix64 operator +(float x, Fix64 y)
    {
        return (Fix64)x + y;
    }

    public static Fix64 operator +(Fix64 x, double y)
    {
        return x + (Fix64)y;
    }

    public static Fix64 operator +(double x, Fix64 y)
    {
        return (Fix64)x + y;
    }

    public static Fix64 operator -(Fix64 x, Fix64 y) {
        return new Fix64(x.m_rawValue - y.m_rawValue);
    }

    public static Fix64 operator -(Fix64 x, int y)
    {
        return x - (Fix64)y;
    }

    public static Fix64 operator -(int x, Fix64 y)
    {
        return (Fix64)x - y;
    }

    public static Fix64 operator -(Fix64 x, float y)
    {
        return x - (Fix64)y;
    }

    public static Fix64 operator -(float x, Fix64 y)
    {
        return (Fix64)x + y;
    }

    public static Fix64 operator -(Fix64 x, double y)
    {
        return x - (Fix64)y;
    }

    public static Fix64 operator -(double x, Fix64 y)
    {
        return (Fix64)x - y;
    }


    public static Fix64 operator *(Fix64 x, Fix64 y) {
        return new Fix64((x.m_rawValue * y.m_rawValue) >> FRACTIONAL_PLACES);
    }

    public static Fix64 operator *(Fix64 x, int y)
    {
        return x * (Fix64)y;
    }

    public static Fix64 operator *(int x, Fix64 y)
    {
        return (Fix64)x * y;
    }

    public static Fix64 operator *(Fix64 x, float y)
    {
        return x * (Fix64)y;
    }

    public static Fix64 operator *(float x, Fix64 y)
    {
        return (Fix64)x * y;
    }

    public static Fix64 operator *(Fix64 x, double y)
    {
        return x * (Fix64)y;
    }

    public static Fix64 operator *(double x, Fix64 y)
    {
        return (Fix64)x * y;
    }

    public static Fix64 operator /(Fix64 x, Fix64 y) {
        return new Fix64((x.m_rawValue << FRACTIONAL_PLACES) / y.m_rawValue);
    }

    public static Fix64 operator /(Fix64 x, int y)
    {
        return x / (Fix64)y;
    }

    public static Fix64 operator /(int x, Fix64 y)
    {
        return (Fix64)x / y;
    }

    public static Fix64 operator /(Fix64 x, float y)
    {
        return x / (Fix64)y;
    }

    public static Fix64 operator /(float x, Fix64 y)
    {
        return (Fix64)x / y;
    }

    public static Fix64 operator /(double x, Fix64 y)
    {
        return (Fix64)x / y;
    }

    public static Fix64 operator /(Fix64 x, double y)
    {
        return x / (Fix64)y;
    }

    public static Fix64 operator %(Fix64 x, Fix64 y) {
        return new Fix64(x.m_rawValue % y.m_rawValue);
    }

    public static Fix64 operator -(Fix64 x) {
        return new Fix64(-x.m_rawValue);
    }

    public static bool operator ==(Fix64 x, Fix64 y) {
        return x.m_rawValue == y.m_rawValue;
    }

    public static bool operator !=(Fix64 x, Fix64 y) {
        return x.m_rawValue != y.m_rawValue;
    }

    public static bool operator >(Fix64 x, Fix64 y) {
        return x.m_rawValue > y.m_rawValue;
    }

    public static bool operator >(Fix64 x, int y)
    {
        return x > (Fix64)y;
    }
    public static bool operator <(Fix64 x, int y)
    {
        return x < (Fix64)y;
    }
    public static bool operator >(Fix64 x, float y)
    {
        return x > (Fix64)y;
    }
    public static bool operator <(Fix64 x, float y)
    {
        return x < (Fix64)y;
    }

    public static bool operator <(Fix64 x, Fix64 y) {
        return x.m_rawValue < y.m_rawValue;
    }

    public static bool operator >=(Fix64 x, Fix64 y) {
        return x.m_rawValue >= y.m_rawValue;
    }

    public static bool operator <=(Fix64 x, Fix64 y) {
        return x.m_rawValue <= y.m_rawValue;
    }

    public static Fix64 operator >> (Fix64 x, int amount)
    {
        return new Fix64(x.RawValue >> amount);
    }

    public static Fix64 operator << (Fix64 x, int amount)
    {
        return new Fix64(x.RawValue << amount);
    }


   public static explicit operator Fix64(long value) {
        return new Fix64(value * ONE);
    }
    public static explicit operator long(Fix64 value) {
        return value.m_rawValue >> FRACTIONAL_PLACES;
    }
    public static explicit operator Fix64(float value) {
        return new Fix64((long)(value * ONE));
    }
    public static explicit operator float(Fix64 value) {
        return (float)value.m_rawValue / ONE;
    }
    public static explicit operator int(Fix64 value)
    {
        return (int)((float)value);
    }
    public static explicit operator Fix64(double value) {
        return new Fix64((long)(value * ONE));
    }
    public static explicit operator double(Fix64 value) {
        return (double)value.m_rawValue / ONE;
    }
    public static explicit operator Fix64(decimal value) {
        return new Fix64((long)(value * ONE));
    }
    public static explicit operator decimal(Fix64 value) {
        return (decimal)value.m_rawValue / ONE;
    }

    public override bool Equals(object obj)
    {
        return obj is Fix64 && ((Fix64)obj).m_rawValue == m_rawValue;
    }

    public override int GetHashCode() {
        return m_rawValue.GetHashCode();
    }

    public bool Equals(Fix64 other) {
        return m_rawValue == other.m_rawValue;
    }

    public int CompareTo(Fix64 other) {
        return m_rawValue.CompareTo(other.m_rawValue);
    }

    public override string ToString() {
        return ((decimal)this).ToString();
    }
    public string ToStringRound(int round = 2)
    {
        return (float)Math.Round((float)this, round) + "";
    }

    public static Fix64 FromRaw(long rawValue) {
        return new Fix64(rawValue);
    }

    public static Fix64 Pow(Fix64 x, int y)
    {
        if (y == 1) return x;
        Fix64 result = Fix64.Zero;
        Fix64 tmp = Pow(x, y / 2);
        if ((y & 1) != 0) //奇数    
        {
            result = x * tmp * tmp;
        }
        else
        {
            result = tmp * tmp;
        }

        return result;
    }


    public long RawValue { get { return m_rawValue; } }


    Fix64(long rawValue) {
        m_rawValue = rawValue;
    }

    public Fix64(int value) {
        m_rawValue = value * ONE;
    }

    public static Fix64 Sqrt(Fix64 f, int numberIterations)
    {
        if (f.RawValue < 0)
        {
            throw new ArithmeticException("sqrt error");
        }

        if (f.RawValue == 0)
            return Fix64.Zero;

        Fix64 k = f + Fix64.One >> 1;
        for (int i = 0; i < numberIterations; i++)
            k = (k + (f / k)) >> 1;

        if (k.RawValue < 0)
            throw new ArithmeticException("Overflow");
        else
            return k;
    }

    public static Fix64 Sqrt(Fix64 f)
    {
        byte numberOfIterations = 8;
        if (f.RawValue > 0x64000)
            numberOfIterations = 12;
        if (f.RawValue > 0x3e8000)
            numberOfIterations = 16;
        return Sqrt(f, numberOfIterations);
    }


    #region Sin
    public static Fix64 Sin(Fix64 i)
    {
        Fix64 j = (Fix64)0;
        for (; i < Fix64.Zero; i += Fix64.FromRaw(PiTimes2));
        if (i > Fix64.FromRaw(PiTimes2))
            i %= Fix64.FromRaw(PiTimes2);

        Fix64 k = (i * Fix64.FromRaw(100000000)) / Fix64.FromRaw(7145244444);
        if (i != Fix64.Zero && i != Fix64.FromRaw(6434) && i != Fix64.FromRaw(Pi) &&
            i != Fix64.FromRaw(19302) && i != Fix64.FromRaw(PiTimes2))
            j = (i * Fix64.FromRaw(100000000)) / Fix64.FromRaw(7145244444) - k * Fix64.FromRaw(10);
        if (k <= Fix64.FromRaw(90))
            return sin_lookup(k, j);
        if (k <= Fix64.FromRaw(180))
            return sin_lookup(Fix64.FromRaw(180) - k, j);
        if (k <= Fix64.FromRaw(270))
            return -sin_lookup(k - Fix64.FromRaw(180), j);
        else
            return -sin_lookup(Fix64.FromRaw(360) - k, j);
    }

    private static Fix64 sin_lookup(Fix64 i, Fix64 j)
    {
        if (j > 0 && j < Fix64.FromRaw(10) && i < Fix64.FromRaw(90))
            return Fix64.FromRaw(SIN_TABLE[i.RawValue]) +
                ((Fix64.FromRaw(SIN_TABLE[i.RawValue + 1]) - Fix64.FromRaw(SIN_TABLE[i.RawValue])) /
                Fix64.FromRaw(10)) * j;
        else
            return Fix64.FromRaw(SIN_TABLE[i.RawValue]);
    }

    private static int[] SIN_TABLE = {
        0, 71, 142, 214, 285, 357, 428, 499, 570, 641,
        711, 781, 851, 921, 990, 1060, 1128, 1197, 1265, 1333,
        1400, 1468, 1534, 1600, 1665, 1730, 1795, 1859, 1922, 1985,
        2048, 2109, 2170, 2230, 2290, 2349, 2407, 2464, 2521, 2577,
        2632, 2686, 2740, 2793, 2845, 2896, 2946, 2995, 3043, 3091,
        3137, 3183, 3227, 3271, 3313, 3355, 3395, 3434, 3473, 3510,
        3547, 3582, 3616, 3649, 3681, 3712, 3741, 3770, 3797, 3823,
        3849, 3872, 3895, 3917, 3937, 3956, 3974, 3991, 4006, 4020,
        4033, 4045, 4056, 4065, 4073, 4080, 4086, 4090, 4093, 4095,
        4096
    };
    #endregion


    #region Cos, Tan, Asin
    public static Fix64 Cos(Fix64 i)
    {
        return Sin(i + Fix64.FromRaw(6435));
    }

    public static Fix64 Tan(Fix64 i)
    {
        return Sin(i) / Cos(i);
    }

    public static Fix64 Asin(Fix64 F)
    {
        bool isNegative = F < 0;
        F = Abs(F);

        if (F > Fix64.One)
            throw new ArithmeticException("Bad Asin Input:" +  (double)F);

        Fix64 f1 = ((((Fix64.FromRaw(145103 >> FRACTIONAL_PLACES) * F) -
            Fix64.FromRaw(599880 >> FRACTIONAL_PLACES)* F) +
            Fix64.FromRaw(1420468 >> FRACTIONAL_PLACES)* F) -
            Fix64.FromRaw(3592413 >> FRACTIONAL_PLACES)* F) +
            Fix64.FromRaw(26353447 >> FRACTIONAL_PLACES);
        Fix64 f2 = PI / (Fix64)2 - (Sqrt(Fix64.One - F) * f1);

        return isNegative ? -f2 : f2;
    }
    #endregion

    #region ATan, ATan2
    public static Fix64 Atan(Fix64 F)
    {
        return Asin(F / Sqrt(Fix64.One + (F * F)));
    }

    public static Fix64 Atan2(Fix64 F1, Fix64 F2)
    {
        if (F2.RawValue == 0 && F1.RawValue == 0)
            return (Fix64)0;

        Fix64 result = (Fix64)0;
        if (F2 > 0)
            result = Atan(F1 / F2);
        else if (F2 < 0)
        {
            if (F1 >= (Fix64)0)
                result = (PI - Atan(Abs(F1 / F2)));
            else
                result = -(PI - Atan(Abs(F1 / F2)));
        }
        else
            result = (F1 >= (Fix64)0 ? PI : -PI) / (Fix64)2;

        return result;
    }
    #endregion
}

public struct FixVector3
{
    public Fix64 x;
    public Fix64 y;
    public Fix64 z;

    public FixVector3(Fix64 x, Fix64 y, Fix64 z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public FixVector3(FixVector3 v)
    {
        this.x = v.x;
        this.y = v.y;
        this.z = v.z;
    }

    public Fix64 this[int index]
    {
        get
        {
            if (index == 0)
                return x;
            else if (index == 1)
                return y;
            else
                return z;
        }
        set
        {
            if (index == 0)
                x = value;
            else if (index == 1)
                y = value;
            else
                y = value;
        }
    }

    public static FixVector3 Zero
    {
        get { return new FixVector3(Fix64.Zero, Fix64.Zero, Fix64.Zero); }
    }

    public static FixVector3 operator +(FixVector3 a, FixVector3 b)
    {
        Fix64 x = a.x + b.x;
        Fix64 y = a.y + b.y;
        Fix64 z = a.z + b.z;
        return new FixVector3(x, y, z);
    }

    public static FixVector3 operator -(FixVector3 a, FixVector3 b)
    {
        Fix64 x = a.x - b.x;
        Fix64 y = a.y - b.y;
        Fix64 z = a.z - b.z;
        return new FixVector3(x, y, z);
    }

    public static FixVector3 operator *(Fix64 d, FixVector3 a)
    {
        Fix64 x = a.x * d;
        Fix64 y = a.y * d;
        Fix64 z = a.z * d;
        return new FixVector3(x, y, z);
    }

    public static FixVector3 operator *(FixVector3 a, Fix64 d)
    {
        Fix64 x = a.x * d;
        Fix64 y = a.y * d;
        Fix64 z = a.z * d;
        return new FixVector3(x, y, z);
    }

    public static FixVector3 operator /(FixVector3 a, Fix64 d)
    {
        Fix64 x = a.x / d;
        Fix64 y = a.y / d;
        Fix64 z = a.z / d;
        return new FixVector3(x, y, z);
    }

    public static bool operator ==(FixVector3 lhs, FixVector3 rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
    }

    public static bool operator !=(FixVector3 lhs, FixVector3 rhs)
    {
        return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z;
    }

    public static Fix64 SqrMagnitude(FixVector3 a)
    {
        return a.x * a.x + a.y * a.y + a.z * a.z;
    }

    public static Fix64 Distance(FixVector3 a, FixVector3 b)
    {
        return Magnitude(a - b);
    }

    public static Fix64 Magnitude(FixVector3 a)
    {
        return Fix64.Sqrt(FixVector3.SqrMagnitude(a));
    }

    public void Normalize()
    {
        Fix64 n = x * x + y * y + z * z;
        if (n == Fix64.Zero)
            return;

        n = Fix64.Sqrt(n);

        if (n < (Fix64)0.0001)
        {
            return;
        }

        n = 1 / n;
        x *= n;
        y *= n;
        z *= n;
    }

    public FixVector3 GetNormalized()
    {
        FixVector3 v = new FixVector3(this);
        v.Normalize();
        return v;
    }

    public override string ToString()
    {
        return string.Format("x:{0} y:{1} z:{2}", x, y, z);
    }

    public override bool Equals(object obj)
    {
        return obj is FixVector2 && ((FixVector3)obj) == this;
    }

    public override int GetHashCode()
    {
        return this.x.GetHashCode() + this.y.GetHashCode() + this.z.GetHashCode();
    }

    public static FixVector3 Lerp(FixVector3 from, FixVector3 to, Fix64 factor)
    {
        return from * (1 - factor) + to * factor;
    }
	#if _CLIENTLOGIC_
    public UnityEngine.Vector3 ToVector3()
    {
        return new UnityEngine.Vector3((float)x, (float)y, (float)z);
    }
	#endif
}

public struct FixVector2
{
    public Fix64 x;
    public Fix64 y;

    public FixVector2(Fix64 x, Fix64 y)
    {
        this.x = x;
        this.y = y;
    }
    public FixVector2(Fix64 x, int y)
    {
        this.x = x;
        this.y = (Fix64)y;
    }

    public FixVector2(int x, int y)
    {
        this.x = (Fix64)x;
        this.y = (Fix64)y;
    }
    public FixVector2(FixVector2 v)
    {
        this.x = v.x;
        this.y = v.y;
    }
    public static FixVector2 operator -(FixVector2 a, int b)
    {
        Fix64 x = a.x - b;
        Fix64 y = a.y - b;
        return new FixVector2(x, y);
    }

    public Fix64 this[int index]
    {
        get { return index == 0 ? x : y; }
        set
        {
            if (index == 0)
            {
                x = value;
            }
            else
            {
                y = value;
            }
        }
    }

    public static FixVector2 Zero
    {
        get { return new FixVector2(Fix64.Zero, Fix64.Zero); }
    }

    public static FixVector2 operator +(FixVector2 a, FixVector2 b)
    {
        Fix64 x = a.x + b.x;
        Fix64 y = a.y + b.y;
        return new FixVector2(x, y);
    }

    public static FixVector2 operator -(FixVector2 a, FixVector2 b)
    {
        Fix64 x = a.x - b.x;
        Fix64 y = a.y - b.y;
        return new FixVector2(x, y);
    }

    public static FixVector2 operator *(Fix64 d, FixVector2 a)
    {
        Fix64 x = a.x * d;
        Fix64 y = a.y * d;
        return new FixVector2(x, y);
    }

    public static FixVector2 operator *(FixVector2 a, Fix64 d)
    {
        Fix64 x = a.x * d;
        Fix64 y = a.y * d;
        return new FixVector2(x, y);
    }

    public static FixVector2 operator /(FixVector2 a, Fix64 d)
    {
        Fix64 x = a.x / d;
        Fix64 y = a.y / d;
        return new FixVector2(x, y);
    }

    public static bool operator ==(FixVector2 lhs, FixVector2 rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y;
    }

    public static bool operator !=(FixVector2 lhs, FixVector2 rhs)
    {
        return lhs.x != rhs.x || lhs.y != rhs.y;
    }

    public override bool Equals(object obj)
    {
        return obj is FixVector2 && ((FixVector2)obj) == this;
    }

    public override int GetHashCode()
    {
        return this.x.GetHashCode() + this.y.GetHashCode();
    }


    public static Fix64 SqrMagnitude(FixVector2 a)
    {
        return a.x * a.x + a.y * a.y;
    }

    public static Fix64 Distance(FixVector2 a, FixVector2 b)
    {
        return Magnitude(a - b);
    }

    public static Fix64 Magnitude(FixVector2 a)
    {
        return Fix64.Sqrt(FixVector2.SqrMagnitude(a));
    }

    public void Normalize()
    {
        Fix64 n = x * x + y * y;
        if (n == Fix64.Zero)
            return;

        n = Fix64.Sqrt(n);

        if (n < (Fix64)0.0001)
        {
            return;
        }

        n = 1 / n;
        x *= n;
        y *= n;
    }

    public FixVector2 GetNormalized()
    {
        FixVector2 v = new FixVector2(this);
        v.Normalize();
        return v;
    }

    public override string ToString()
    {
        return string.Format("x:{0} y:{1}", x, y);
    }

	#if _CLIENTLOGIC_
    public UnityEngine.Vector2 ToVector2()
    {
        return new UnityEngine.Vector2((float)x, (float)y);
    }
	#endif
}

public struct NormalVector2
{
    public float x;
    public float y;

    public NormalVector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }


    public NormalVector2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public NormalVector2(NormalVector2 v)
    {
        this.x = v.x;
        this.y = v.y;
    }
    public static NormalVector2 operator -(NormalVector2 a, int b)
    {
        float x = a.x - b;
        float y = a.y - b;
        return new NormalVector2(x, y);
    }

    public float this[int index]
    {
        get { return index == 0 ? x : y; }
        set
        {
            if (index == 0)
            {
                x = value;
            }
            else
            {
                y = value;
            }
        }
    }

    public static NormalVector2 Zero
    {
        get { return new NormalVector2(0, 0); }
    }

    public static NormalVector2 operator +(NormalVector2 a, NormalVector2 b)
    {
        float x = a.x + b.x;
        float y = a.y + b.y;
        return new NormalVector2(x, y);
    }

    public static NormalVector2 operator -(NormalVector2 a, NormalVector2 b)
    {
        float x = a.x - b.x;
        float y = a.y - b.y;
        return new NormalVector2(x, y);
    }

    public static NormalVector2 operator *(float d, NormalVector2 a)
    {
        float x = a.x * d;
        float y = a.y * d;
        return new NormalVector2(x, y);
    }

    public static NormalVector2 operator *(NormalVector2 a, float d)
    {
        float x = a.x * d;
        float y = a.y * d;
        return new NormalVector2(x, y);
    }

    public static NormalVector2 operator /(NormalVector2 a, float d)
    {
        float x = a.x / d;
        float y = a.y / d;
        return new NormalVector2(x, y);
    }

    public static bool operator ==(NormalVector2 lhs, NormalVector2 rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y;
    }

    public static bool operator !=(NormalVector2 lhs, NormalVector2 rhs)
    {
        return lhs.x != rhs.x || lhs.y != rhs.y;
    }

    public override bool Equals(object obj)
    {
        return obj is NormalVector2 && ((NormalVector2)obj) == this;
    }

    public override int GetHashCode()
    {
        return this.x.GetHashCode() + this.y.GetHashCode();
    }


    public static float SqrMagnitude(NormalVector2 a)
    {
        return a.x * a.x + a.y * a.y;
    }

    public static float Distance(NormalVector2 a, NormalVector2 b)
    {
        return Magnitude(a - b);
    }

    public static float Magnitude(NormalVector2 a)
    {
        return NormalVector2.SqrMagnitude(a);
    }

    public void Normalize()
    {
        float n = x * x + y * y;
        if (n == 0)
            return;

        //n = float.Sqrt(n);

        if (n < (float)0.0001)
        {
            return;
        }

        n = 1 / n;
        x *= n;
        y *= n;
    }

    public NormalVector2 GetNormalized()
    {
        NormalVector2 v = new NormalVector2(this);
        v.Normalize();
        return v;
    }

    public override string ToString()
    {
        return string.Format("x:{0} y:{1}", x, y);
    }

	#if _CLIENTLOGIC_
    public UnityEngine.Vector2 ToVector2()
    {
        return new UnityEngine.Vector2((float)x, (float)y);
    }
	#endif
}

public struct IntVector2
{
    public int x;
    public int y;



    public IntVector2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public IntVector2(IntVector2 v)
    {
        this.x = v.x;
        this.y = v.y;
    }
    public static IntVector2 operator -(IntVector2 a, int b)
    {
        int x = a.x - b;
        int y = a.y - b;
        return new IntVector2(x, y);
    }

    public int this[int index]
    {
        get { return index == 0 ? x : y; }
        set
        {
            if (index == 0)
            {
                x = value;
            }
            else
            {
                y = value;
            }
        }
    }

    public static IntVector2 Zero
    {
        get { return new IntVector2(0, 0); }
    }

    public static IntVector2 operator +(IntVector2 a, IntVector2 b)
    {
        int x = a.x + b.x;
        int y = a.y + b.y;
        return new IntVector2(x, y);
    }

    public static IntVector2 operator -(IntVector2 a, IntVector2 b)
    {
        int x = a.x - b.x;
        int y = a.y - b.y;
        return new IntVector2(x, y);
    }

    public static IntVector2 operator *(int d, IntVector2 a)
    {
        int x = a.x * d;
        int y = a.y * d;
        return new IntVector2(x, y);
    }

    public static IntVector2 operator *(IntVector2 a, int d)
    {
        int x = a.x * d;
        int y = a.y * d;
        return new IntVector2(x, y);
    }

    public static IntVector2 operator /(IntVector2 a, int d)
    {
        int x = a.x / d;
        int y = a.y / d;
        return new IntVector2(x, y);
    }

    public static bool operator ==(IntVector2 lhs, IntVector2 rhs)
    {
        return lhs.x == rhs.x && lhs.y == rhs.y;
    }

    public static bool operator !=(IntVector2 lhs, IntVector2 rhs)
    {
        return lhs.x != rhs.x || lhs.y != rhs.y;
    }

    public override bool Equals(object obj)
    {
        return obj is IntVector2 && ((IntVector2)obj) == this;
    }

    public override int GetHashCode()
    {
        return this.x.GetHashCode() + this.y.GetHashCode();
    }


    public static int SqrMagnitude(IntVector2 a)
    {
        return a.x * a.x + a.y * a.y;
    }

    public static int Distance(IntVector2 a, IntVector2 b)
    {
        return Magnitude(a - b);
    }

    public static int Magnitude(IntVector2 a)
    {
        return IntVector2.SqrMagnitude(a);
    }

    public void Normalize()
    {
        int n = x * x + y * y;
        if (n == 0)
            return;

        //n = int.Sqrt(n);

        if (n < (int)0.0001)
        {
            return;
        }

        n = 1 / n;
        x *= n;
        y *= n;
    }

    public IntVector2 GetNormalized()
    {
        IntVector2 v = new IntVector2(this);
        v.Normalize();
        return v;
    }

    public override string ToString()
    {
        return string.Format("x:{0} y:{1}", x, y);
    }

	#if _CLIENTLOGIC_
    public UnityEngine.Vector2 ToVector2()
    {
        return new UnityEngine.Vector2((int)x, (int)y);
    }
	#endif
}