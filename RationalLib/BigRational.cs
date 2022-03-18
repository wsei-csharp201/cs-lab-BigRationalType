// Core functionalities

global using System.Numerics;

namespace RationalLib
{
    public readonly struct BigRational // readonly struct in C#7.2
    {
        public readonly BigInteger Numerator { get; init; } // C#9
        public readonly BigInteger Denominator { get; init; } // C#9


        #region constants
        public static readonly BigRational Zero = new(0, 1); // 0/1
        public static readonly BigRational One  = new(1, 1); // 1/1
        public static readonly BigRational Half = new(1, 2); // 1/2

        public static readonly BigRational NaN  = new(0, 0); // 0/0, default value of type
        public static readonly BigRational PositiveInfinity = new(1, 0);  // a/0, a > 0
        public static readonly BigRational NegativeInfinity = new(-1, 0); // a/0, a < 0
        #endregion

        #region ctor's
        public BigRational(BigInteger numerator, BigInteger denominator)
        {
            Numerator = numerator;
            Denominator = denominator;

            // sign standarization
            if (Numerator < 0 && Denominator < 0)
                (Numerator, Denominator) = ((-1) * Numerator, (-1) * Denominator);

            if (Numerator > 0 && Denominator < 0)
                (Numerator, Denominator) = ((-1) * Numerator, (-1) * Denominator);

            // special cases
            if (Numerator == 0 && Denominator == 0) // BigRational.NaN
                return;

            if (Numerator > 0 && Denominator == 0) // BigRational.PositiveInfinity
            {                
                (Numerator, Denominator) = (1, 0);
                return;
            }

            if (Numerator < 0 && Denominator == 0) // BigRational.NegativeInfinity
            {
                (Numerator, Denominator) = (-1, 0);                
                return;
            }

            if (Numerator == 0 && Denominator != 0) // BigRational.Zero
            {
                Denominator = 1;
                return;
            }

            if (Denominator == 1) return;
            if (Numerator == 1) return;

            if (Numerator == Denominator) // BigRational.One
            {
                (Numerator, Denominator) = (1, 1);                
                return;
            }

            if (2*Numerator == Denominator) // BigRational.Half
            {
                (Numerator, Denominator) = (1, 2); 
                return;
            }

            BigInteger gcd = BigInteger.GreatestCommonDivisor(Numerator, Denominator);
            (Numerator, Denominator) = (Numerator / gcd, Denominator / gcd);
        }

        public BigRational(BigInteger value)
            : this(value, 1)
        { }

        public BigRational() : this( BigRational.NaN ) { }

        public BigRational(long numerator, long denominator = 1)
            : this((BigInteger)numerator, (BigInteger)denominator)
        { }

        public BigRational(ulong numerator, ulong denominator = 1)
            : this((BigInteger)numerator, (BigInteger)denominator)
        { }

        private BigRational(BigRational instance) => this = instance;     
        #endregion


        #region accessor methods
        public static bool IsNaN(BigRational fraction) => fraction.Equals(NaN); // fraction == NaN

        public static bool IsPositiveInfinity(BigRational fraction) => fraction.Equals(PositiveInfinity);

        public static bool IsNegativeInfinity(BigRational fraction) => fraction.Equals(NegativeInfinity);

        public static bool IsInfinity(BigRational fraction) => IsNegativeInfinity(fraction) || IsPositiveInfinity(fraction);

        public static bool IsFinite(BigRational fraction) => !IsInfinity(fraction) && !IsNaN(fraction);
        #endregion

        public override string ToString() // => $"{Numerator}/{Denominator}";
        {
            if (IsNaN(this)) return "NaN";
            if (IsPositiveInfinity(this)) return "+Infinity";
            if (IsNegativeInfinity(this)) return "-Infinity";

            return $"{Numerator}/{Denominator}";
        }

    }
}