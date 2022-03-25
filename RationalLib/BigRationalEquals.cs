
using System;
using System.Diagnostics.CodeAnalysis;

namespace RationalLib
{
    // implementacja tożsamości instancji
    // ponieważ ułamek zapamiętywany jest w najprostszej, nieskraqcalnej postaci
    // oraz jego postać jest znormalizowan (znak ułamka w liczniku)
    // porównywanie mozna zrealizować jako _pole-po-polu_
    public readonly partial struct BigRational : IEquatable<BigRational>
    {
        public bool Equals(BigRational other)
        {
            // if this is NaN or other is NaN then false
            if( (this.Numerator == 0 && this.Denominator == 0)
                 || (other.Numerator == 0 && other.Denominator == 0) 
                )
                return false;

            return (Numerator == other.Numerator && Denominator == other.Denominator);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if ((obj is not BigRational)) return false; // C#10 `is not`

            return Equals((BigRational)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Numerator, Denominator);
        }

        public static bool operator ==(BigRational u1, BigRational u2)
            => u1.Equals(u2);
        public static bool operator !=(BigRational u1, BigRational u2)
            => !(u1==u2);

    }
}
