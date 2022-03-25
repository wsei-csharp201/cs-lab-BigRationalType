// w `BigRationalCoreUnitTests` użyto `global using`


namespace RationalUnitTests
{
    [TestClass, TestCategory("Equals")]
    public class BigRationalEqualsUnitTests
    {

        #region Testy implementacji przeciążenia `Equals(object)` oraz `Equals(BigRational`)

        [TestMethod, TestCategory("Equals")]
        public void Equals_Argument_Null_zwraca_False()
        {
            Assert.IsFalse((new BigRational()).Equals(null));
        }

        [TestMethod, TestCategory("Equals")]
        public void Equals_Argument_InnyTyp_zwraca_False()
        {
            BigRational u = new();
            var anonymousTypeVariable = new { x = 0, y = 1 };
            Assert.IsFalse(u.Equals(anonymousTypeVariable));
        }

        [TestMethod, TestCategory("Equals")]
        [DataTestMethod]
        [DataRow(1, 2, 1, 2, true)]
        [DataRow(1, 2, 2, 4, true)]
        [DataRow(0, 2, 0, 3, true)]
        [DataRow(2, 1, 4, 2, true)]
        [DataRow(1, 2, 2, 3, false)]
        [DataRow(1, -2, -1, 2, true)]
        public void Equals_OK(long u1Licznik, long u1Mianownik, long u2Licznik, long u2Mianownik, bool wynik)
        {
            BigRational u1 = new(u1Licznik, u1Mianownik);
            BigRational u2 = new(u2Licznik, u2Mianownik);
            Assert.AreEqual(wynik, u1.Equals((object)u2));
        }

        /// <summary>
        /// Test zwrotności: `x.Equals(x)` zwraca `true`
        /// </summary>
        [TestMethod, TestCategory("Equals")]
        public void Equals_Zwrotnosc_TenSamObiekt()
        {
            BigRational u = new();
            Assert.IsTrue(u.Equals((object)u));
        }

        /// <summary>
        /// Test zwrotności: `x.Equals(x)` zwraca `true`
        /// </summary>
        [TestMethod, TestCategory("Equals")]
        public void Equals_Zwrotnosc_TakiSamObiekt()
        {
            BigRational u = new(1, 2);
            BigRational v = new(1, 2);
            Assert.IsTrue(u.Equals(v));
        }

        /// <summary>
        /// Test symetrii: `x.Equals(y)` zwraca tę samą wartość, co `y.Equals(x)`
        /// </summary>
        [TestMethod, TestCategory("Equals")]
        public void Equals_Symetria_PoprawneDane()
        {
            BigRational x = new(1, 2);
            BigRational y = new(x.Numerator, x.Denominator);
            Assert.IsTrue(x.Equals(y));
            Assert.IsTrue(y.Equals(x));
        }

        /// <summary>
        /// Test symetrii: `x.Equals(y)` zwraca tę samą wartość, co `y.Equals(x)`
        /// </summary>
        [TestMethod, TestCategory("Equals")]
        public void Equals_Symetria_ZleDane()
        {
            BigRational x = new(1, 2);
            BigRational y = new(1, 3);
            Assert.IsFalse(x.Equals(y));
            Assert.IsFalse(y.Equals(x));
        }

        /// <summary>
        /// Test przechodniosci: jeżeli `x.Equals(y)` zwraca `true` oraz `y.Equals(z))` zwraca `true`, wtedy `x.Equals(z)` zwraca `true`
        /// </summary>
        [DataTestMethod, TestCategory("Equals")]
        [DataRow(1, 2, 1, 2, 1, 2)]
        [DataRow(1, 2, 2, 4, 3, 6)]
        [DataRow(-1, 2, 2, -4, -3, 6)]
        public void Equals_Przechodniosc_PoprawneDane(int u1l, int u1m, int u2l, int u2m, int u3l, int u3m)
        {
            BigRational x = new(u1l, u1m);
            BigRational y = new(u2l, u2m);
            BigRational z = new(u3l, u3m);

            Assert.IsTrue(x.Equals(y));
            Assert.IsTrue(y.Equals(z));
            Assert.IsTrue(x.Equals(z));
        }

        /// <summary>
        /// Test przechodniości: jeżeli `x.Equals(y)` zwraca `true` oraz `y.Equals(z))` zwraca `true`, wtedy `x.Equals(z)` zwraca `true`
        /// Logika: zdanie: ( (p ⇒ q) ⇔ (¬p ∨ q) ) jest tautologią (zamiana implikacji na alternatywę)
        /// Zdanie (a ∧ b ⇒ c), gdzie a oznacza `x.Equals(y)`, b oznacza `y.Equals(z)`, zaś c oznacza `x.Equals(z)`
        /// zapiszemy w równoważny sposób, bez użycia implikacji: ¬(a ∧ b) ∨ c
        /// czyli (z prawa de Morgana): ¬a ∨ ¬b ∨ c
        /// Pozostaje w teście sprawdzić, czy dla dowolnych danych testowych zdanie to jest zawsze prawdziwe.
        /// Jeśli nie - to powodem będzie wadliwa implementacja `Equals`
        /// </summary>
        [DataTestMethod, TestCategory("Equals")]
        [DataRow(1, 2, 1, 2, 1, 2)]
        [DataRow(1, 2, 2, 4, 3, 6)]
        [DataRow(-1, 2, 2, -4, -3, 6)]
        [DataRow(1, 2, -1, 2, 1, 2)]
        [DataRow(1, 2, 1, 3, 1, 3)]
        [DataRow(1, 2, 1, 2, 1, 3)]
        public void Equals_Przechodniosc_ZPrawLogiki_DowolneDane(int u1l, int u1m, int u2l, int u2m, int u3l, int u3m)
        {
            BigRational x = new(u1l, u1m);
            BigRational y = new(u2l, u2m);
            BigRational z = new(u3l, u3m);

            Assert.IsTrue(!x.Equals(y) || !y.Equals(z) || x.Equals(z));
        }

        #endregion

        #region  Testy przeciążonego operatora `==` dla `BigRational`

        [DataTestMethod, TestCategory("==")]
        [DataRow(1, 2, 1, 2, true)]
        [DataRow(1, 2, 2, 4, true)]
        [DataRow(0, 2, 0, 3, true)]
        [DataRow(2, 1, 4, 2, true)]
        [DataRow(1, 2, 2, 3, false)]
        public void OperatorRownosci_OK(long u1Licznik, long u1Mianownik, long u2Licznik, long u2Mianownik, bool wynik)
        {
            BigRational u1 = new(u1Licznik, u1Mianownik);
            BigRational u2 = new(u2Licznik, u2Mianownik);
            Assert.AreEqual(wynik, u1 == u2);
        }


        [TestMethod, TestCategory("==")]
        public void OperatorRownosci_Symetria_True()
        {
            BigRational u = new(1, 2);
            BigRational v = new(1, 2);
            Assert.IsTrue(u == v && v == u);
        }

        [DataTestMethod, TestCategory("==")]
        [DataRow(1, 2, 1, 2, 1, 2)]
        [DataRow(1, 2, 2, 4, 3, 6)]
        [DataRow(-1, 2, 2, -4, -3, 6)]
        [DataRow(1, 2, -1, 2, 1, 2)]
        [DataRow(1, 2, 1, 3, 1, 3)]
        [DataRow(1, 2, 1, 2, 1, 3)]
        public void OperatorRownosci_Przechodniosc_ZPrawLogiki_DowolneDane(int u1l, int u1m, int u2l, int u2m, int u3l, int u3m)
        {
            BigRational u1 = new(u1l, u1m);
            BigRational u2 = new(u2l, u2m);
            BigRational u3 = new(u3l, u3m);

            Assert.IsTrue(u1 != u2 || u2 != u3 || u1 == u3);
        }

        #endregion


        #region Sytuacje specjalne z `NaN`

        [DataTestMethod]
        [DataRow(0, 0)]  //NaN
        [DataRow(1, 0)]  //+Infinity
        [DataRow(-1, 0)] //-Infinity
        [DataRow(1, 3)]
        public void Equals_NaN_daje_zawsze_false(int numerator, int denominator)
        {
            BigRational u = new (numerator, denominator);
            
            Assert.IsFalse(u.Equals(BigRational.NaN));
            Assert.IsFalse( u == BigRational.NaN );
            Assert.IsFalse( (BigRational.NaN).Equals(u) );
            Assert.IsFalse(BigRational.NaN == u);
        }
        #endregion
    }
}
