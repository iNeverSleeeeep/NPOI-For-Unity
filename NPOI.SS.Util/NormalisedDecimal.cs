using NPOI.Util;
using System;
using System.Globalization;
using System.Text;

namespace NPOI.SS.Util
{
	public class NormalisedDecimal
	{
		/// Number of powers of ten Contained in the significand
		private const int EXPONENT_OFFSET = 14;

		private const int LOG_BASE_10_OF_2_TIMES_2_POW_20 = 315653;

		/// 2<sup>19</sup>
		private const int C_2_POW_19 = 524288;

		/// the value of {@link #_fractionalPart} that represents 0.5
		private const int FRAC_HALF = 8388608;

		/// 10<sup>15</sup>
		private const long MAX_REP_WHOLE_PART = 1000000000000000L;

		private static readonly decimal BD_2_POW_24 = new decimal((BigInteger.One << 24).LongValue());

		/// The decimal exponent increased by one less than the digit count of {@link #_wholePart}
		private int _relativeDecimalExponent;

		/// The whole part of the significand (typically 15 digits).
		///
		/// 47-50 bits long (MSB may be anywhere from bit 46 to 49)
		/// LSB is units bit.
		private long _wholePart;

		/// The fractional part of the significand.
		/// 24 bits (only top 14-17 bits significant): a value between 0x000000 and 0xFFFF80
		private int _fractionalPart;

		public static NormalisedDecimal Create(BigInteger frac, int binaryExponent)
		{
			int num2;
			if (binaryExponent > 49 || binaryExponent < 46)
			{
				int num = 15204352 - binaryExponent * 315653;
				num += 524288;
				num2 = -(num >> 20);
			}
			else
			{
				num2 = 0;
			}
			MutableFPNumber mutableFPNumber = new MutableFPNumber(frac, binaryExponent);
			if (num2 != 0)
			{
				mutableFPNumber.multiplyByPowerOfTen(-num2);
			}
			switch (mutableFPNumber.Get64BitNormalisedExponent())
			{
			case 46:
				if (mutableFPNumber.IsAboveMinRep())
				{
					break;
				}
				goto case 44;
			case 44:
			case 45:
				mutableFPNumber.multiplyByPowerOfTen(1);
				num2--;
				break;
			case 49:
				if (mutableFPNumber.IsBelowMaxRep())
				{
					break;
				}
				goto case 50;
			case 50:
				mutableFPNumber.multiplyByPowerOfTen(-1);
				num2++;
				break;
			default:
				throw new InvalidOperationException("Bad binary exp " + mutableFPNumber.Get64BitNormalisedExponent() + ".");
			case 47:
			case 48:
				break;
			}
			mutableFPNumber.Normalise64bit();
			return mutableFPNumber.CreateNormalisedDecimal(num2);
		}

		/// Rounds at the digit with value 10<sup>decimalExponent</sup>
		public NormalisedDecimal RoundUnits()
		{
			long num = _wholePart;
			if (_fractionalPart >= 8388608)
			{
				num++;
			}
			int relativeDecimalExponent = _relativeDecimalExponent;
			if (num < 1000000000000000L)
			{
				return new NormalisedDecimal(num, 0, relativeDecimalExponent);
			}
			return new NormalisedDecimal(num / 10, 0, relativeDecimalExponent + 1);
		}

		public NormalisedDecimal(long wholePart, int fracPart, int decimalExponent)
		{
			_wholePart = wholePart;
			_fractionalPart = fracPart;
			_relativeDecimalExponent = decimalExponent;
		}

		/// Convert to an equivalent {@link ExpandedDouble} representation (binary frac and exponent).
		/// The resulting transformed object is easily Converted to a 64 bit IEEE double:
		/// <ul>
		/// <li>bits 2-53 of the {@link #GetSignificand()} become the 52 bit 'fraction'.</li>
		/// <li>{@link #GetBinaryExponent()} is biased by 1023 to give the 'exponent'.</li>
		/// </ul>
		/// The sign bit must be obtained from somewhere else.
		/// @return a new {@link NormalisedDecimal} normalised to base 2 representation.
		public ExpandedDouble NormaliseBaseTwo()
		{
			MutableFPNumber mutableFPNumber = new MutableFPNumber(ComposeFrac(), 39);
			mutableFPNumber.multiplyByPowerOfTen(_relativeDecimalExponent);
			mutableFPNumber.Normalise64bit();
			return mutableFPNumber.CreateExpandedDouble();
		}

		/// @return the significand as a fixed point number (with 24 fraction bits and 47-50 whole bits)
		public BigInteger ComposeFrac()
		{
			long wholePart = _wholePart;
			int fractionalPart = _fractionalPart;
			return new BigInteger(new byte[11]
			{
				(byte)(wholePart >> 56),
				(byte)(wholePart >> 48),
				(byte)(wholePart >> 40),
				(byte)(wholePart >> 32),
				(byte)(wholePart >> 24),
				(byte)(wholePart >> 16),
				(byte)(wholePart >> 8),
				(byte)wholePart,
				(byte)(fractionalPart >> 16),
				(byte)(fractionalPart >> 8),
				(byte)fractionalPart
			});
		}

		public string GetSignificantDecimalDigits()
		{
			return _wholePart.ToString(CultureInfo.InvariantCulture);
		}

		/// Rounds the first whole digit position (considers only units digit, not frational part).
		/// Caller should check total digit count of result to see whether the rounding operation caused
		/// a carry out of the most significant digit
		public string GetSignificantDecimalDigitsLastDigitRounded()
		{
			long value = _wholePart + 5;
			StringBuilder stringBuilder = new StringBuilder(24);
			stringBuilder.Append(value);
			stringBuilder[stringBuilder.Length - 1] = '0';
			return stringBuilder.ToString();
		}

		/// @return the number of powers of 10 which have been extracted from the significand and binary exponent.
		public int GetDecimalExponent()
		{
			return _relativeDecimalExponent + 14;
		}

		/// assumes both this and other are normalised
		public int CompareNormalised(NormalisedDecimal other)
		{
			int num = _relativeDecimalExponent - other._relativeDecimalExponent;
			if (num != 0)
			{
				return num;
			}
			if (_wholePart > other._wholePart)
			{
				return 1;
			}
			if (_wholePart < other._wholePart)
			{
				return -1;
			}
			return _fractionalPart - other._fractionalPart;
		}

		public decimal GetFractionalPart()
		{
			return new decimal(_fractionalPart) / BD_2_POW_24;
		}

		private string GetFractionalDigits()
		{
			if (_fractionalPart == 0)
			{
				return "0";
			}
			return GetFractionalPart().ToString(CultureInfo.InvariantCulture).Substring(2);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(GetType().Name);
			stringBuilder.Append(" [");
			string text = _wholePart.ToString(CultureInfo.InvariantCulture);
			stringBuilder.Append(text[0]);
			stringBuilder.Append('.');
			stringBuilder.Append(text.Substring(1));
			stringBuilder.Append(' ');
			stringBuilder.Append(GetFractionalDigits());
			stringBuilder.Append("E");
			stringBuilder.Append(GetDecimalExponent());
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
