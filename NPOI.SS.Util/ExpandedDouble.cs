using NPOI.Util;
using System;

namespace NPOI.SS.Util
{
	public class ExpandedDouble
	{
		private static readonly BigInteger BI_FRAC_MASK = new BigInteger(4503599627370495L);

		private static readonly BigInteger BI_IMPLIED_FRAC_MSB = new BigInteger(4503599627370496L);

		/// Always 64 bits long (MSB, bit-63 is '1')
		private BigInteger _significand;

		private int _binaryExponent;

		private static BigInteger GetFrac(long rawBits)
		{
			return ((new BigInteger(rawBits) & BI_FRAC_MASK) | BI_IMPLIED_FRAC_MSB) << 11;
		}

		public static ExpandedDouble FromRawBitsAndExponent(long rawBits, int exp)
		{
			return new ExpandedDouble(GetFrac(rawBits), exp);
		}

		public ExpandedDouble(long rawBits)
		{
			int num = (int)(rawBits >> 52);
			if (num == 0)
			{
				BigInteger bigInteger = new BigInteger(rawBits) & BI_FRAC_MASK;
				int num2 = 64 - bigInteger.BitLength();
				_significand = bigInteger << num2;
				_binaryExponent = (num & 0x7FF) - 1023 - num2;
			}
			else
			{
				BigInteger bigInteger2 = _significand = GetFrac(rawBits);
				_binaryExponent = (num & 0x7FF) - 1023;
			}
		}

		public ExpandedDouble(BigInteger frac, int binaryExp)
		{
			if (frac.BitLength() != 64)
			{
				throw new ArgumentException("bad bit length");
			}
			_significand = frac;
			_binaryExponent = binaryExp;
		}

		/// Convert to an equivalent {@link NormalisedDecimal} representation having 15 decimal digits of precision in the
		/// non-fractional bits of the significand.
		public NormalisedDecimal NormaliseBaseTen()
		{
			return NormalisedDecimal.Create(_significand, _binaryExponent);
		}

		/// @return the number of non-fractional bits after the MSB of the significand
		public int GetBinaryExponent()
		{
			return _binaryExponent;
		}

		public BigInteger GetSignificand()
		{
			return _significand;
		}
	}
}
