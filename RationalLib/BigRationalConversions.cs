
namespace RationalLib
{
    public partial struct BigRational : IConvertible
    {
        //implicit - automatyczna konwersja z long na BigRational (wtedy, kiedy tak wynika z kodu)
        static public implicit operator BigRational(long value) => new BigRational(value);

        //explicit - rzutowanie z BigRational na long
        static public explicit operator long(BigRational u) => (long)(u.Numerator / u.Denominator);

        // rzutowanie z ułamka na `double`
        static public explicit operator double(BigRational u) => (double)u.Numerator / (double)u.Denominator;

        // rzutowanie z `double` na `BigRational`
        //static public explicit operator BigRational(double value) => new BigRational(value);


        //parsowanie
        public static BigRational Parse(string napis) => throw new NotImplementedException();
        public static bool TryParse(string napis, out BigRational result) => throw new NotImplementedException();

        #region implementacja interfejsu IConvertible

        public long ToInt64(IFormatProvider? provider) => (long)this;


        public TypeCode GetTypeCode()
        {
            throw new NotImplementedException();
        }

        public bool ToBoolean(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public byte ToByte(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public char ToChar(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public DateTime ToDateTime(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public double ToDouble(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public short ToInt16(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public sbyte ToSByte(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public float ToSingle(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public object ToType(Type conversionType, IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public ushort ToUInt16(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public uint ToUInt32(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        public ulong ToUInt64(IFormatProvider? provider)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
