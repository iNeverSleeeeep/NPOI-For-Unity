using NPOI.Util;
using System;

namespace NPOI.SS.Util
{
	public class MutableFPNumber
	{
		private class Rounder
		{
			private static BigInteger[] HALF_BITS;

			static Rounder()
			{
				BigInteger[] array = new BigInteger[33];
				long num = 1L;
				for (int i = 1; i < array.Length; i++)
				{
					array[i] = new BigInteger(num);
					num <<= 1;
				}
				HALF_BITS = array;
			}

			/// @param nBits number of bits to shift right
			public static BigInteger Round(BigInteger bi, int nBits)
			{
				if (nBits < 1)
				{
					return bi;
				}
				return bi + HALF_BITS[nBits];
			}
		}

		/// Holds values for quick multiplication and division by 10
		private class TenPower
		{
			private static readonly BigInteger FIVE = new BigInteger(5L);

			private static TenPower[] _cache = new TenPower[350];

			public BigInteger _multiplicand;

			public BigInteger _divisor;

			public int _divisorShift;

			public int _multiplierShift;

			private TenPower(int index)
			{
				BigInteger bigInteger = FIVE.Pow(index);
				int num = bigInteger.BitLength();
				int shiftVal = 80 + num;
				BigInteger bigInteger2 = (BigInteger.One << shiftVal) / bigInteger;
				int num2 = bigInteger2.BitLength() - 80;
				_divisor = bigInteger2 >> num2;
				num -= num2;
				_divisorShift = -(num + index + 80);
				int num3 = bigInteger.BitLength() - 68;
				if (num3 > 0)
				{
					_multiplierShift = index + num3;
					_multiplicand = bigInteger >> num3;
				}
				else
				{
					_multiplierShift = index;
					_multiplicand = bigInteger;
				}
			}

			public static TenPower GetInstance(int index)
			{
				TenPower tenPower = _cache[index];
				if (tenPower == null)
				{
					tenPower = new TenPower(index);
					_cache[index] = tenPower;
				}
				return tenPower;
			}
		}

		/// Width of a long
		private const int C_64 = 64;

		/// Minimum precision after discarding whole 32-bit words from the significand
		private const int MIN_PRECISION = 72;

		/// The minimum value in 'Base-10 normalised form'.<br />
		/// When {@link #_binaryExponent} == 46 this is the the minimum {@link #_frac} value
		///  (10<sup>14</sup>-0.05) * 2^17
		///  <br />
		///  Values between (10<sup>14</sup>-0.05) and 10<sup>14</sup> will be represented as '1'
		///  followed by 14 zeros.
		///  Values less than (10<sup>14</sup>-0.05) will get Shifted by one more power of 10
		///
		///  This frac value rounds to '1' followed by fourteen zeros with an incremented decimal exponent
		private static readonly BigInteger BI_MIN_BASE = new BigInteger(new int[2]
		{
			-1243209484,
			2147477094
		}, 1);

		/// For 'Base-10 normalised form'<br />
		/// The maximum {@link #_frac} value when {@link #_binaryExponent} == 49
		/// (10^15-0.5) * 2^14
		private static readonly BigInteger BI_MAX_BASE = new BigInteger(new int[2]
		{
			-480270031,
			-1610620928
		}, 1);

		private BigInteger _significand;

		private int _binaryExponent;

		public MutableFPNumber(BigInteger frac, int binaryExponent)
		{
			_significand = frac;
			_binaryExponent = binaryExponent;
		}

		public MutableFPNumber Copy()
		{
			return new MutableFPNumber(_significand, _binaryExponent);
		}

		public void Normalise64bit()
		{
			int num = _significand.BitLength();
			int num2 = num - 64;
			if (num2 != 0)
			{
				if (num2 < 0)
				{
					throw new InvalidOperationException("Not enough precision");
				}
				_binaryExponent += num2;
				if (num2 > 32)
				{
					int num3 = (num2 - 1) & 0xFFFFE0;
					_significand >>= num3;
					num2 -= num3;
					num -= num3;
				}
				if (num2 < 1)
				{
					throw new InvalidOperationException();
				}
				_significand = Rounder.Round(_significand, num2);
				if (_significand.BitLength() > num)
				{
					num2++;
					_binaryExponent++;
				}
				_significand >>= num2;
			}
		}

		public int Get64BitNormalisedExponent()
		{
			return _binaryExponent + _significand.BitLength() - 64;
		}

		public bool IsBelowMaxRep()
		{
			int n = _significand.BitLength() - 64;
			return _significand.CompareTo(BI_MAX_BASE.ShiftLeft(n)) < 0;
		}

		public bool IsAboveMinRep()
		{
			int n = _significand.BitLength() - 64;
			return _significand.CompareTo(BI_MIN_BASE.ShiftLeft(n)) > 0;
		}

		public NormalisedDecimal CreateNormalisedDecimal(int pow10)
		{
			int num = _binaryExponent - 39;
			int fracPart = (_significand.IntValue() << num) & 0xFFFF80;
			long wholePart = (_significand >> 64 - _binaryExponent - 1).LongValue();
			return new NormalisedDecimal(wholePart, fracPart, pow10);
		}

		public void multiplyByPowerOfTen(int pow10)
		{
			TenPower instance = TenPower.GetInstance(Math.Abs(pow10));
			if (pow10 < 0)
			{
				mulShift(instance._divisor, instance._divisorShift);
			}
			else
			{
				mulShift(instance._multiplicand, instance._multiplierShift);
			}
		}

		private void mulShift(BigInteger multiplicand, int multiplierShift)
		{
			_significand *= multiplicand;
			_binaryExponent += multiplierShift;
			int num = (_significand.BitLength() - 72) & -32;
			if (num > 0)
			{
				_significand >>= num;
				_binaryExponent += num;
			}
		}

		public ExpandedDouble CreateExpandedDouble()
		{
			return new ExpandedDouble(_significand, _binaryExponent);
		}
	}
}
