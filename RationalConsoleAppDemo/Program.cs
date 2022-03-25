using RationalLib;
using static System.Console;
using System.Numerics;

WriteLine(default(BigRational));

BigRational? u = new BigRational(1, 2);
BigRational v = new BigRational(1, 2);

WriteLine(u == v);
WriteLine(null == u);
WriteLine(u == null);


WriteLine(2.1.CompareTo(null));

// jak to jest z `NaN` w typie `double`
// jakiekolwiek porównanie z NaN daje false
WriteLine(0.0 / 0.0 == double.NaN); //false
WriteLine(1.0 / 1.0 == double.NaN); //false
WriteLine(1.0 / 0.0 == double.NaN); //false
WriteLine(-1.0 / 0.0 == double.NaN); //false

WriteLine( (1.0 / 1.0) < double.NaN); //false
WriteLine( (1.0 / 0.0) > double.NaN); //false
WriteLine( (-1.0 / 0.0) < double.NaN); //false

WriteLine((1.0).CompareTo(double.NaN));  // +1
WriteLine((-1.0).CompareTo(double.NaN)); // +1
WriteLine((double.NaN).CompareTo(1.0));  // -1
WriteLine((double.NaN).CompareTo(double.PositiveInfinity));  // -1
WriteLine((double.NaN).CompareTo(double.NegativeInfinity));  // -1
// wniosek: NaN jest mniejsze od czegokolwiek
WriteLine((double.NaN).CompareTo(double.NaN));  // 0


// jak to jest z `+Infinity` i `Equals` w typie `double`
//WriteLine(1.0 / 1.0 < double.PositiveInfinity); //true
//WriteLine(1.0 / 0.0 > double.PositiveInfinity); //false
//WriteLine(1.0 / 0.0 < double.PositiveInfinity); //false