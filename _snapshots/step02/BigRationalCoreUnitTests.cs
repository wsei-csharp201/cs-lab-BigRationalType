global using System;
global using System.Numerics;
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using RationalLib;


namespace RationalUnitTests
{
    [TestClass]
    public class BigRationalCoreUnitTests
    {

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(3, 4, 3, 4)]
        [DataRow(1, 2, 1, 2)]
        [DataRow(3, 2, 3, 2)]
        [DataRow(-1, 2, -1, 2)]
        [DataRow(-1, -2, 1, 2)] // !! standaryzacja
        [DataRow(1, -2, -1, 2)]
        [DataRow(2, 4, 1, 2)]   // !! upraszczanie
        [DataRow(-2, 4, -1, 2)] // !! standaryzacja + upraszczanie
        [DataRow(-2, -4, 1, 2)]
        [DataRow(2, -4, -1, 2)]
        [DataRow(-2, -4, 1, 2)]
        [DataRow(0, 1, 0, 1)]   // !! jednoznacznoœæ zera
        [DataRow(0, 2, 0, 1)]
        [DataRow(0, -2, 0, 1)]
        [DataRow(-0, 1, 0, 1)]
        [DataRow(0, -1, 0, 1)]
        [DataRow(-0, -1, 0, 1)]
        public void Constructor_2params_int_DenominatorNonZero(int numerator, int denominator, int expectedNumerator, int expectedDenominator)
        {
            // Arrange

            // Act
            var u = new BigRational(numerator, denominator);

            // Assert
            Assert.AreEqual( (BigInteger)expectedNumerator, u.Numerator);
            Assert.AreEqual( (BigInteger)expectedDenominator, u.Denominator);
        }


        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("123456789012345678901111111111", "1", "123456789012345678901111111111", "1")]
        [DataRow("123456789012345678901234567890", "2906161", "42481056284337199109490", "1")]
        public void Constructor_2params_BigInteger_DenominatorNonZero(string numerator, string denominator, string expectedNumerator, string expectedDenominator)
        {
            // Arrange
            var num = BigInteger.Parse(numerator);
            var denom = BigInteger.Parse(denominator);

            // Act
            var u = new BigRational(num, denom);

            // Assert
            Assert.AreEqual(expectedNumerator, u.Numerator.ToString());
            Assert.AreEqual(expectedDenominator, u.Denominator.ToString());
        }




        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(0, 0, 1)]
        [DataRow(-0, 0, 1)]
        [DataRow(1, 1, 1)]
        [DataRow(-1, -1, 1)]
        [DataRow(5, 5, 1)]
        [DataRow(-5, -5, 1)]
        [DataRow(int.MinValue, int.MinValue, 1)]
        [DataRow(int.MaxValue, int.MaxValue, 1)]
        public void Constructor_1param_int(int wholeNumber, int expectedNumerator, int expectedDenominator)
        {
            var u = new BigRational(wholeNumber);

            Assert.AreEqual( (BigInteger)expectedNumerator, u.Numerator);
            Assert.AreEqual( (BigInteger)expectedDenominator, u.Denominator);
        }

        [TestMethod, TestCategory("Constructors")]
        public void Constructor_0params_Numerator_and_Denominator_sets_to_0()
        {
            var u = new BigRational();

            Assert.AreEqual(0, u.Numerator);
            Assert.AreEqual(1, u.Denominator);
        }

        [TestMethod, TestCategory("Constants")]
        public void Rational_Zero_as_static_value_Numerator_equal_0_and_Denominator_equal_1()
        {
            Assert.AreEqual(0, (BigRational.Zero).Numerator);
            Assert.AreEqual(1, (BigRational.Zero).Denominator);
        }

        [TestMethod, TestCategory("Constants")]
        public void Rational_One_as_static_value_Numerator_equal_1_and_Denominator_equal_1()
        {
            Assert.AreEqual(1, BigRational.One.Numerator);
            Assert.AreEqual(1, BigRational.One.Denominator);
        }

        [TestMethod, TestCategory("Constants")]
        public void Rational_Half_as_static_value_Numerator_equal_1_and_Denominator_equal_2()
        {
            Assert.AreEqual(1, BigRational.Half.Numerator);
            Assert.AreEqual(2, BigRational.Half.Denominator);
        }

        [TestMethod, TestCategory("Constants")]
        public void Rational_NaN_as_static_value_Numerator_equal_0_and_Denominator_equal_0()
        {
            Assert.AreEqual(0, BigRational.NaN.Numerator);
            Assert.AreEqual(0, BigRational.NaN.Denominator);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(int.MaxValue)]
        public void Rational_PositiveInfinity_as_static_value_Numerator_greater_0_and_Denominator_equal_0(int numerator)
        {
            var u = new BigRational(numerator, 0);

            Assert.AreEqual(u.Numerator, BigRational.PositiveInfinity.Numerator);
            Assert.AreEqual(u.Denominator, BigRational.PositiveInfinity.Denominator);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1)]
        [DataRow(-5)]
        [DataRow(int.MinValue)]
        public void Rational_NegativeInfinity_as_static_value_Numerator_less_0_and_Denominator_equal_0(int numerator)
        {
            var u = new BigRational(numerator, 0);

            Assert.AreEqual(u.Numerator, BigRational.NegativeInfinity.Numerator);
            Assert.AreEqual(u.Denominator, BigRational.NegativeInfinity.Denominator);
        }


        [DataTestMethod, TestCategory("Accessors")]
        [DataRow(0, 0, true)]
        [DataRow(0, 1, false)]
        [DataRow(0, -1, false)]
        [DataRow(1, 0, false)]
        [DataRow(-1, 0, false)]
        public void Accessor_IsNaN(int numerator, int denominator, bool response)
        {
            var u = new BigRational(numerator, denominator);
            Assert.AreEqual(response, BigRational.IsNaN(u));
        }

        [DataTestMethod, TestCategory("Accessors")]
        [DataRow(0, 0, false)]
        [DataRow(0, 1, false)]
        [DataRow(0, -1, false)]
        [DataRow(1, 0, true)]
        [DataRow(5, 0, true)]
        [DataRow(int.MaxValue, 0, true)]
        [DataRow(-1, 0, false)]
        public void Accessor_IsPositiveInfinity(int numerator, int denominator, bool response)
        {
            var u = new BigRational(numerator, denominator);
            Assert.AreEqual(response, BigRational.IsPositiveInfinity(u));
        }

        [DataTestMethod, TestCategory("Accessors")]
        [DataRow(0, 0, false)]
        [DataRow(0, 1, false)]
        [DataRow(0, -1, false)]
        [DataRow(1, 0, false)]
        [DataRow(5, 0, false)]
        [DataRow(int.MaxValue, 0, false)]        
        [DataRow(-5, 0, true)]
        [DataRow(-1, 0, true)]
        [DataRow(-1, -0, true)]
        [DataRow(1, -0, false)]
        public void Accessor_IsNegativeInfinity(int numerator, int denominator, bool response)
        {
            var u = new BigRational(numerator, denominator);
            Assert.AreEqual(response, BigRational.IsNegativeInfinity(u));
        }


        [DataTestMethod]
        [DataRow(0, 0)]
        [DataRow(1, 0)]
        [DataRow(-1, 0)]
        [DataRow(0, 1)]
        [DataRow(0, -1)]
        [DataRow(6, 4)]
        [DataRow(-6, 4)]
        [DataRow(6, -4)]
        [DataRow(-6, -4)]
        public void Denominator_Always_Non_Negative(int numerator, int denominator)
        {
            var u = new BigRational(numerator, denominator);
            Assert.IsTrue(u.Denominator >= 0);
        }


        [DataTestMethod]
        [DataRow(1, 2, "1/2")]
        [DataRow(1, -2, "-1/2")]
        [DataRow(-1, 2, "-1/2")]
        [DataRow(-1, -2, "1/2")]
        [DataRow(0, 1, "0/1")]
        [DataRow(0, 2, "0/1")]
        [DataRow(0, -1, "0/1")]
        [DataRow(0, 0, "NaN")]        // jak w double
        [DataRow(1, 0, "+Infinity")]  // jak w double
        [DataRow(-1, 0, "-Infinity")] // jak w double
        public void ToString_Canonical_Representation(int numerator, int denominator, string expectedRepresentation)
        {
            var u = new BigRational(numerator, denominator);
            Assert.AreEqual(expectedRepresentation, u.ToString());
        }


    }
}