using NPOI.Util;
using System;

namespace NPOI.SS.Util
{
	public class NumberComparer
	{
		/// This class attempts to reproduce Excel's behaviour for comparing numbers.  Results are
		/// mostly the same as those from {@link Double#compare(double, double)} but with some
		/// rounding.  For numbers that are very close, this code converts to a format having 15
		/// decimal digits of precision and a decimal exponent, before completing the comparison.
		/// <p />
		/// In Excel formula evaluation, expressions like "(0.06-0.01)=0.05" evaluate to "TRUE" even
		/// though the equivalent java expression is <c>false</c>.  In examples like this,
		/// Excel achieves the effect by having additional logic for comparison operations.
		/// <p />
		/// <p />
		/// Note - Excel also gives special treatment to expressions like "0.06-0.01-0.05" which
		/// evaluates to "0" (in java, rounding anomalies give a result of 6.9E-18).  The special
		/// behaviour here is for different reasons to the example above:  If the last operator in a
		/// cell formula is '+' or '-' and the result is less than 2<sup>50</sup> times smaller than
		/// first operand, the result is rounded to zero.
		/// Needless to say, the two rules are not consistent and it is relatively easy to find
		/// examples that satisfy<br />
		/// "A=B" is "TRUE" but "A-B" is not "0"<br />
		/// and<br />
		/// "A=B" is "FALSE" but "A-B" is "0"<br />
		/// <br />
		/// This rule (for rounding the result of a final addition or subtraction), has not been
		/// implemented in POI (as of Jul-2009).
		///
		/// @return <code>negative, 0, or positive</code> according to the standard Excel comparison
		/// of values <c>a</c> and <c>b</c>.
		public static int Compare(double a, double b)
		{
			long num = BitConverter.DoubleToInt64Bits(a);
			long num2 = BitConverter.DoubleToInt64Bits(b);
			int biasedExponent = IEEEDouble.GetBiasedExponent(num);
			int biasedExponent2 = IEEEDouble.GetBiasedExponent(num2);
			if (biasedExponent == 2047)
			{
				throw new ArgumentException("Special double values are not allowed: " + ToHex(a));
			}
			if (biasedExponent2 == 2047)
			{
				throw new ArgumentException("Special double values are not allowed: " + ToHex(a));
			}
			bool flag = num < 0;
			bool flag2 = num2 < 0;
			if (flag != flag2)
			{
				if (!flag)
				{
					return 1;
				}
				return -1;
			}
			int num3 = biasedExponent - biasedExponent2;
			int num4 = Math.Abs(num3);
			if (num4 > 1)
			{
				if (!flag)
				{
					return num3;
				}
				return -num3;
			}
			if (num4 != 1 && num == num2)
			{
				return 0;
			}
			if (biasedExponent == 0)
			{
				if (biasedExponent2 == 0)
				{
					return CompareSubnormalNumbers(num & 0xFFFFFFFFFFFFF, num2 & 0xFFFFFFFFFFFFF, flag);
				}
				return -CompareAcrossSubnormalThreshold(num2, num, flag);
			}
			if (biasedExponent2 == 0)
			{
				return CompareAcrossSubnormalThreshold(num, num2, flag);
			}
			ExpandedDouble expandedDouble = ExpandedDouble.FromRawBitsAndExponent(num, biasedExponent - 1023);
			ExpandedDouble expandedDouble2 = ExpandedDouble.FromRawBitsAndExponent(num2, biasedExponent2 - 1023);
			NormalisedDecimal normalisedDecimal = expandedDouble.NormaliseBaseTen().RoundUnits();
			NormalisedDecimal other = expandedDouble2.NormaliseBaseTen().RoundUnits();
			num3 = normalisedDecimal.CompareNormalised(other);
			if (flag)
			{
				return -num3;
			}
			return num3;
		}

		/// If both numbers are subnormal, Excel seems to use standard comparison rules
		private static int CompareSubnormalNumbers(long fracA, long fracB, bool isNegative)
		{
			int num = (fracA > fracB) ? 1 : ((fracA < fracB) ? (-1) : 0);
			if (!isNegative)
			{
				return num;
			}
			return -num;
		}

		/// Usually any normal number is greater (in magnitude) than any subnormal number.
		/// However there are some anomalous cases around the threshold where Excel produces screwy results
		/// @param isNegative both values are either negative or positive. This parameter affects the sign of the comparison result
		/// @return usually <code>isNegative ? -1 : +1</code>
		private static int CompareAcrossSubnormalThreshold(long normalRawBitsA, long subnormalRawBitsB, bool isNegative)
		{
			long num = subnormalRawBitsB & 0xFFFFFFFFFFFFF;
			if (num == 0)
			{
				if (!isNegative)
				{
					return 1;
				}
				return -1;
			}
			long num2 = normalRawBitsA & 0xFFFFFFFFFFFFF;
			if (num2 <= 7 && num >= 4503599627370490L)
			{
				if (num2 == 7 && num == 4503599627370490L)
				{
					return 0;
				}
				if (!isNegative)
				{
					return -1;
				}
				return 1;
			}
			if (!isNegative)
			{
				return 1;
			}
			return -1;
		}

		/// for formatting double values in error messages
		private static string ToHex(double a)
		{
			return "0x" + StringUtil.ToHexString(BitConverter.DoubleToInt64Bits(a)).ToUpper();
		}
	}
}
