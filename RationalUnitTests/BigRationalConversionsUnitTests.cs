// w `BigRationalCoreUnitTests` użyto `global using`

namespace RationalUnitTests
{

    [TestClass]
    class BigRationalConversionsUnitTests
    {

            [DataTestMethod]
            [DataRow(1, 2, 0)]
            [DataRow(2, 1, 2)]
            [DataRow(3, 2, 1)]
            [DataRow(-3, 2, -1)]
            public void Konwersja_explicit_na_long(int licznik, int mianownik, int wynik)
            {
            BigRational u = new (licznik, mianownik);
                long y = (long)u;
                Assert.AreEqual((long)wynik, y);
            }

            [DataTestMethod]
            [DataRow(1)]
            [DataRow(2)]
            [DataRow(3)]
            [DataRow(-3)]
            public void Konwersja_implicit_z_long(int liczba)
            {
                BigRational u = liczba;
                Assert.AreEqual(liczba, u.Numerator);
                Assert.AreEqual((long)1, u.Denominator);
            }
    }
}
