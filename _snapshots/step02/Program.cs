using RationalLib;
using static System.Console;
using System.Numerics;

WriteLine(default(BigRational));

BigRational? u = new BigRational(1, 2);
BigRational v = new BigRational(1, 2);

WriteLine(u == v);
WriteLine(null == u);
WriteLine(u == null);
