using NPOI.SS.Util;
using NPOI.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NPOI.SS.Format
{
	/// This class : printing out a value using a number format.
	///
	/// @author Ken Arnold, Industrious Media LLC
	public class CellNumberFormatter : CellFormatter
	{
		/// <summary>
		/// The CellNumberFormatter.simpleValue() method uses the SIMPLE_NUMBER
		/// CellFormatter defined here. The CellFormat.GENERAL_FORMAT CellFormat
		/// no longer uses the SIMPLE_NUMBER CellFormatter.
		/// Note that the simpleValue()/SIMPLE_NUMBER CellFormatter format
		/// ("#" for integer values, and "#.#" for floating-point values) is
		/// different from the 'General' format for numbers ("#" for integer
		/// values and "#.#########" for floating-point values).
		/// </summary>
		private class SimpleNumberCellFormatter : CellFormatter
		{
			public SimpleNumberCellFormatter(string format)
				: base(format)
			{
			}

			public override void FormatValue(StringBuilder toAppendTo, object value)
			{
				if (value != null)
				{
					if (Number.IsNumber(value))
					{
						double.TryParse(value.ToString(), out double result);
						if (result % 1.0 == 0.0)
						{
							SIMPLE_INT.FormatValue(toAppendTo, value);
						}
						else
						{
							SIMPLE_FLOAT.FormatValue(toAppendTo, value);
						}
					}
					else
					{
						CellTextFormatter.SIMPLE_TEXT.FormatValue(toAppendTo, value);
					}
				}
			}

			public override void SimpleValue(StringBuilder toAppendTo, object value)
			{
				FormatValue(toAppendTo, value);
			}
		}

		/// This class is used to mark where the special characters in the format
		/// are, as opposed to the other characters that are simply printed.
		internal class Special
		{
			internal char ch;

			internal int pos;

			public Special(char ch, int pos)
			{
				this.ch = ch;
				this.pos = pos;
			}

			public override string ToString()
			{
				return "'" + ch + "' @ " + pos;
			}
		}

		/// This class represents a single modification to a result string.  The way
		/// this works is complicated, but so is numeric formatting.  In general, for
		/// most formats, we use a DecimalFormat object that will Put the string out
		/// in a known format, usually with all possible leading and trailing zeros.
		/// We then walk through the result and the orginal format, and note any
		/// modifications that need to be made.  Finally, we go through and apply
		/// them all, dealing with overlapping modifications.
		internal class StringMod : IComparable<StringMod>
		{
			public const int BEFORE = 1;

			public const int AFTER = 2;

			public const int REPLACE = 3;

			internal Special special;

			internal int op;

			internal string toAdd;

			internal Special end;

			internal bool startInclusive;

			internal bool endInclusive;

			public StringMod(Special special, string toAdd, int op)
			{
				this.special = special;
				this.toAdd = toAdd;
				this.op = op;
			}

			public StringMod(Special start, bool startInclusive, Special end, bool endInclusive, char toAdd)
				: this(start, startInclusive, end, endInclusive)
			{
				this.toAdd = string.Concat(toAdd);
			}

			public StringMod(Special start, bool startInclusive, Special end, bool endInclusive)
			{
				special = start;
				this.startInclusive = startInclusive;
				this.end = end;
				this.endInclusive = endInclusive;
				op = 3;
				toAdd = "";
			}

			public int CompareTo(StringMod that)
			{
				int num = special.pos - that.special.pos;
				if (num != 0)
				{
					return num;
				}
				return op - that.op;
			}

			public override bool Equals(object that)
			{
				try
				{
					return CompareTo((StringMod)that) == 0;
				}
				catch (Exception)
				{
					return false;
				}
			}

			public override int GetHashCode()
			{
				return special.GetHashCode() + op;
			}
		}

		private class NumPartHandler : CellFormatPart.IPartHandler
		{
			private char insertSignForExponent;

			private CellNumberFormatter formatter;

			public NumPartHandler(CellNumberFormatter formatter)
			{
				this.formatter = formatter;
			}

			public string HandlePart(Match m, string part, CellFormatType type, StringBuilder desc)
			{
				int num = desc.Length;
				switch (part[0])
				{
				case 'E':
				case 'e':
					if (formatter.exponent == null && formatter.specials.Count > 0)
					{
						formatter.specials.Add(formatter.exponent = new Special('.', num));
						insertSignForExponent = part[1];
						return part.Substring(0, 1);
					}
					break;
				case '#':
				case '0':
				case '?':
					if (insertSignForExponent != 0)
					{
						formatter.specials.Add(new Special(insertSignForExponent, num));
						desc.Append(insertSignForExponent);
						insertSignForExponent = '\0';
						num++;
					}
					for (int i = 0; i < part.Length; i++)
					{
						char ch = part[i];
						formatter.specials.Add(new Special(ch, num + i));
					}
					break;
				case '.':
					if (formatter.decimalPoint == null && formatter.specials.Count > 0)
					{
						formatter.specials.Add(formatter.decimalPoint = new Special('.', num));
					}
					break;
				case '/':
					if (formatter.slash == null && formatter.specials.Count > 0)
					{
						formatter.numerator = formatter.previousNumber();
						if (formatter.numerator == firstDigit(formatter.specials))
						{
							formatter.improperFraction = true;
						}
						formatter.specials.Add(formatter.slash = new Special('.', num));
					}
					break;
				case '%':
					formatter.scale *= 100.0;
					break;
				default:
					return null;
				}
				return part;
			}
		}

		/// Based on org.apache.Commons.math.fraction.Fraction from Apache Commons-Math.
		/// YK: The only reason of having this inner class is to avoid dependency on the Commons-Math jar.
		private class Fraction
		{
			/// The denominator. 
			private int denominator;

			/// The numerator. 
			private int numerator;

			/// Access the denominator.
			/// @return the denominator.
			public int Denominator => denominator;

			/// Access the numerator.
			/// @return the numerator.
			public int Numerator => numerator;

			/// Create a fraction given the double value and either the maximum error
			/// allowed or the maximum number of denominator digits.
			///
			/// @param value the double value to convert to a fraction.
			/// @param epsilon maximum error allowed.  The resulting fraction is within
			///        <code>epsilon</code> of <code>value</code>, in absolute terms.
			/// @param maxDenominator maximum denominator value allowed.
			/// @param maxIterations maximum number of convergents
			/// @throws Exception if the continued fraction failed to
			///         converge.
			private Fraction(double value, double epsilon, int maxDenominator, int maxIterations)
			{
				long num = 2147483647L;
				double num2 = value;
				long num3 = (long)Math.Floor(num2);
				if (num3 > num)
				{
					throw new ArgumentException("Overflow trying to convert " + value + " to fraction (" + num3 + "/" + 1L + ")");
				}
				if (Math.Abs((double)num3 - value) < epsilon)
				{
					numerator = (int)num3;
					denominator = 1;
				}
				else
				{
					long num4 = 1L;
					long num5 = 0L;
					long num6 = num3;
					long num7 = 1L;
					int num8 = 0;
					bool flag = false;
					long num11;
					long num12;
					do
					{
						num8++;
						double num9 = 1.0 / (num2 - (double)num3);
						long num10 = (long)Math.Floor(num9);
						num11 = num10 * num6 + num4;
						num12 = num10 * num7 + num5;
						if (num11 > num || num12 > num)
						{
							throw new Exception("Overflow trying to convert " + value + " to fraction (" + num11 + "/" + num12 + ")");
						}
						double num13 = (double)num11 / (double)num12;
						if (num8 < maxIterations && Math.Abs(num13 - value) > epsilon && num12 < maxDenominator)
						{
							num4 = num6;
							num6 = num11;
							num5 = num7;
							num7 = num12;
							num3 = num10;
							num2 = num9;
						}
						else
						{
							flag = true;
						}
					}
					while (!flag);
					if (num8 >= maxIterations)
					{
						throw new Exception("Unable to convert " + value + " to fraction After " + maxIterations + " iterations");
					}
					if (num12 < maxDenominator)
					{
						numerator = (int)num11;
						denominator = (int)num12;
					}
					else
					{
						numerator = (int)num6;
						denominator = (int)num7;
					}
				}
			}

			/// Create a fraction given the double value and maximum denominator.
			/// <p>
			/// References:
			/// <ul>
			/// <li><a href="http://mathworld.wolfram.com/ContinuedFraction.html">
			/// Continued Fraction</a> equations (11) and (22)-(26)</li>
			/// </ul>
			/// </p>
			/// @param value the double value to convert to a fraction.
			/// @param maxDenominator The maximum allowed value for denominator
			/// @throws Exception if the continued fraction failed to
			///         converge
			public Fraction(double value, int maxDenominator)
				: this(value, 0.0, maxDenominator, 100)
			{
			}
		}

		private string desc;

		private string printfFmt;

		private double scale;

		private Special decimalPoint;

		private Special slash;

		private Special exponent;

		private Special numerator;

		private Special Afterint;

		private Special AfterFractional;

		private bool integerCommas;

		private List<Special> specials;

		private List<Special> integerSpecials;

		private List<Special> fractionalSpecials;

		private List<Special> numeratorSpecials;

		private List<Special> denominatorSpecials;

		private List<Special> exponentSpecials;

		private List<Special> exponentDigitSpecials;

		private int maxDenominator;

		private string numeratorFmt;

		private string denominatorFmt;

		private bool improperFraction;

		private DecimalFormat decimalFmt;

		private static List<Special> EmptySpecialList = new List<Special>();

		private static readonly CellFormatter SIMPLE_NUMBER = new SimpleNumberCellFormatter("General");

		private static readonly CellFormatter SIMPLE_INT = new CellNumberFormatter("#");

		private static readonly CellFormatter SIMPLE_FLOAT = new CellNumberFormatter("#.#");

		/// Creates a new cell number formatter.
		///
		/// @param format The format to Parse.
		public CellNumberFormatter(string format)
			: base(format)
		{
			scale = 1.0;
			specials = new List<Special>();
			NumPartHandler partHandler = new NumPartHandler(this);
			StringBuilder stringBuilder = CellFormatPart.ParseFormat(format, CellFormatType.NUMBER, partHandler);
			stringBuilder.ToString();
			if ((decimalPoint != null || exponent != null) && slash != null)
			{
				slash = null;
				numerator = null;
			}
			interpretCommas(stringBuilder);
			int num;
			if (decimalPoint == null)
			{
				num = 0;
			}
			else
			{
				num = interpretPrecision();
				if (num == 0)
				{
					specials.Remove(decimalPoint);
					decimalPoint = null;
				}
			}
			if (num == 0)
			{
				fractionalSpecials = EmptySpecialList;
			}
			else
			{
				int num2 = specials.IndexOf(decimalPoint) + 1;
				fractionalSpecials = specials.GetRange(num2, fractionalEnd() - num2);
			}
			if (exponent == null)
			{
				exponentSpecials = EmptySpecialList;
			}
			else
			{
				int num3 = specials.IndexOf(exponent);
				exponentSpecials = specialsFor(num3, 2);
				exponentDigitSpecials = specialsFor(num3 + 2);
			}
			if (slash == null)
			{
				numeratorSpecials = EmptySpecialList;
				denominatorSpecials = EmptySpecialList;
			}
			else
			{
				if (numerator == null)
				{
					numeratorSpecials = EmptySpecialList;
				}
				else
				{
					numeratorSpecials = specialsFor(specials.IndexOf(numerator));
				}
				denominatorSpecials = specialsFor(specials.IndexOf(slash) + 1);
				if (denominatorSpecials.Count == 0)
				{
					numeratorSpecials = EmptySpecialList;
				}
				else
				{
					maxDenominator = maxValue(denominatorSpecials);
					numeratorFmt = SingleNumberFormat(numeratorSpecials);
					denominatorFmt = SingleNumberFormat(denominatorSpecials);
				}
			}
			integerSpecials = specials.GetRange(0, integerEnd());
			if (exponent == null)
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				int repeatCount = calculateintPartWidth();
				stringBuilder2.Append('0', repeatCount).Append('.').Append('0', num);
				printfFmt = stringBuilder2.ToString();
			}
			else
			{
				StringBuilder stringBuilder3 = new StringBuilder();
				bool flag = true;
				List<Special> list = integerSpecials;
				if (integerSpecials.Count == 1)
				{
					stringBuilder3.Append("0");
					flag = false;
				}
				else
				{
					foreach (Special item in list)
					{
						if (IsDigitFmt(item))
						{
							stringBuilder3.Append(flag ? '#' : '0');
							flag = false;
						}
					}
				}
				if (fractionalSpecials.Count > 0)
				{
					stringBuilder3.Append('.');
					foreach (Special fractionalSpecial in fractionalSpecials)
					{
						if (IsDigitFmt(fractionalSpecial))
						{
							if (!flag)
							{
								stringBuilder3.Append('0');
							}
							flag = false;
						}
					}
				}
				stringBuilder3.Append('E');
				placeZeros(stringBuilder3, exponentSpecials.GetRange(2, exponentSpecials.Count - 2));
				decimalFmt = new DecimalFormat(stringBuilder3.ToString());
			}
			if (exponent != null)
			{
				scale = 1.0;
			}
			desc = stringBuilder.ToString();
		}

		private static void placeZeros(StringBuilder sb, List<Special> specials)
		{
			foreach (Special special in specials)
			{
				if (IsDigitFmt(special))
				{
					sb.Append('0');
				}
			}
		}

		private static Special firstDigit(List<Special> specials)
		{
			foreach (Special special in specials)
			{
				if (IsDigitFmt(special))
				{
					return special;
				}
			}
			return null;
		}

		private static StringMod insertMod(Special special, string toAdd, int where)
		{
			return new StringMod(special, toAdd, where);
		}

		private static StringMod deleteMod(Special start, bool startInclusive, Special end, bool endInclusive)
		{
			return new StringMod(start, startInclusive, end, endInclusive);
		}

		private static StringMod ReplaceMod(Special start, bool startInclusive, Special end, bool endInclusive, char withChar)
		{
			return new StringMod(start, startInclusive, end, endInclusive, withChar);
		}

		private static string SingleNumberFormat(List<Special> numSpecials)
		{
			return "D" + numSpecials.Count;
		}

		private static int maxValue(List<Special> s)
		{
			return (int)Math.Round(Math.Pow(10.0, (double)s.Count) - 1.0);
		}

		private List<Special> specialsFor(int pos, int takeFirst)
		{
			if (pos >= specials.Count)
			{
				return EmptySpecialList;
			}
			IEnumerator<Special> enumerator = specials.GetRange(pos + takeFirst, specials.Count - pos - takeFirst).GetEnumerator();
			enumerator.MoveNext();
			Special special = enumerator.Current;
			int num = pos + takeFirst;
			while (enumerator.MoveNext())
			{
				Special current = enumerator.Current;
				if (!IsDigitFmt(current) || current.pos - special.pos > 1)
				{
					break;
				}
				num++;
				special = current;
			}
			return specials.GetRange(pos, num + 1 - pos);
		}

		private List<Special> specialsFor(int pos)
		{
			return specialsFor(pos, 0);
		}

		private static bool IsDigitFmt(Special s)
		{
			if (s.ch != '0' && s.ch != '?')
			{
				return s.ch == '#';
			}
			return true;
		}

		private Special previousNumber()
		{
			for (int num = specials.Count - 1; num >= 0; num--)
			{
				Special special = specials[num];
				if (IsDigitFmt(special))
				{
					Special result = special;
					Special special2 = special;
					while (num > 0)
					{
						num--;
						special = specials[num];
						if (special2.pos - special.pos > 1 || !IsDigitFmt(special))
						{
							break;
						}
						result = special;
						special2 = special;
					}
					return result;
				}
			}
			return null;
		}

		private int calculateintPartWidth()
		{
			IEnumerator<Special> enumerator = specials.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				Special current = enumerator.Current;
				if (current == Afterint)
				{
					break;
				}
				if (IsDigitFmt(current))
				{
					num++;
				}
			}
			return num;
		}

		private int interpretPrecision()
		{
			if (decimalPoint == null)
			{
				return -1;
			}
			int num = 0;
			int num2 = specials.IndexOf(decimalPoint);
			IEnumerator<Special> enumerator = specials.GetRange(num2, specials.Count - num2).GetEnumerator();
			enumerator.MoveNext();
			while (enumerator.MoveNext())
			{
				Special current = enumerator.Current;
				if (!IsDigitFmt(current))
				{
					break;
				}
				num++;
			}
			return num;
		}

		private void interpretCommas(StringBuilder sb)
		{
			int count = integerEnd();
			List<Special> range = specials.GetRange(0, count);
			bool flag = true;
			integerCommas = false;
			for (int num = range.Count - 1; num >= 0; num--)
			{
				Special special = range[num];
				if (special.ch != ',')
				{
					flag = false;
				}
				else if (flag)
				{
					scale /= 1000.0;
				}
				else
				{
					integerCommas = true;
				}
			}
			if (decimalPoint != null)
			{
				count = fractionalEnd();
				range = specials.GetRange(0, count);
				for (int num2 = range.Count - 1; num2 >= 0; num2--)
				{
					Special special2 = range[num2];
					if (special2.ch != ',')
					{
						break;
					}
					scale /= 1000.0;
				}
			}
			IEnumerator<Special> enumerator = specials.GetEnumerator();
			int num3 = 0;
			List<Special> list = new List<Special>();
			while (enumerator.MoveNext())
			{
				Special current = enumerator.Current;
				current.pos -= num3;
				if (current.ch == ',')
				{
					num3++;
					list.Add(current);
					sb.Remove(current.pos, 1);
				}
			}
			foreach (Special item in list)
			{
				specials.Remove(item);
			}
		}

		private int integerEnd()
		{
			if (decimalPoint != null)
			{
				Afterint = decimalPoint;
			}
			else if (exponent != null)
			{
				Afterint = exponent;
			}
			else if (numerator != null)
			{
				Afterint = numerator;
			}
			else
			{
				Afterint = null;
			}
			if (Afterint != null)
			{
				return specials.IndexOf(Afterint);
			}
			return specials.Count;
		}

		private int fractionalEnd()
		{
			if (exponent != null)
			{
				AfterFractional = exponent;
			}
			else if (numerator != null)
			{
				Afterint = numerator;
			}
			else
			{
				AfterFractional = null;
			}
			return (AfterFractional == null) ? specials.Count : specials.IndexOf(AfterFractional);
		}

		/// {@inheritDoc} 
		public override void FormatValue(StringBuilder toAppendTo, object valueObject)
		{
			double num = (double)valueObject;
			num *= scale;
			bool flag = num < 0.0;
			if (flag)
			{
				num = 0.0 - num;
			}
			double fractional = 0.0;
			if (slash != null)
			{
				if (improperFraction)
				{
					fractional = num;
					num = 0.0;
				}
				else
				{
					fractional = num % 1.0;
					num = (double)(long)num;
				}
			}
			SortedList<StringMod, object> sortedList = new SortedList<StringMod, object>();
			StringBuilder stringBuilder = new StringBuilder(desc);
			if (exponent != null)
			{
				WriteScientific(num, stringBuilder, sortedList);
			}
			else if (improperFraction)
			{
				WriteFraction(num, null, fractional, stringBuilder, sortedList);
			}
			else
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				stringBuilder2.Append(num.ToString(printfFmt));
				if (numerator == null)
				{
					WriteFractional(stringBuilder2, stringBuilder);
					Writeint(stringBuilder2, stringBuilder, integerSpecials, sortedList, integerCommas);
				}
				else
				{
					WriteFraction(num, stringBuilder2, fractional, stringBuilder, sortedList);
				}
			}
			IEnumerator<Special> enumerator = specials.GetEnumerator();
			IEnumerator enumerator2 = sortedList.Keys.GetEnumerator();
			StringMod stringMod = enumerator2.MoveNext() ? ((StringMod)enumerator2.Current) : null;
			int num2 = 0;
			BitArray bitArray = new BitArray(1024);
			while (enumerator.MoveNext())
			{
				Special current = enumerator.Current;
				int num3 = current.pos + num2;
				if (!bitArray[current.pos] && stringBuilder[num3] == '#')
				{
					stringBuilder.Remove(num3, 1);
					num2--;
					bitArray.Set(current.pos, value: true);
				}
				while (stringMod != null && current == stringMod.special)
				{
					int length = stringBuilder.Length;
					int num4 = current.pos + num2;
					int num5 = 0;
					switch (stringMod.op)
					{
					case 2:
						if (!stringMod.toAdd.Equals(",") || !bitArray.Get(current.pos))
						{
							num5 = 1;
							stringBuilder.Insert(num4 + num5, stringMod.toAdd);
						}
						break;
					case 1:
						stringBuilder.Insert(num4 + num5, stringMod.toAdd);
						break;
					case 3:
					{
						int num6 = current.pos;
						if (!stringMod.startInclusive)
						{
							num6++;
							num4++;
						}
						while (bitArray.Get(num6))
						{
							num6++;
							num4++;
						}
						int num7 = stringMod.end.pos;
						if (stringMod.endInclusive)
						{
							num7++;
						}
						int num8 = num7 + num2;
						if (num4 < num8)
						{
							if (stringMod.toAdd == "")
							{
								stringBuilder.Remove(num4, num8 - num4);
							}
							else
							{
								char value = stringMod.toAdd[0];
								for (int i = num4; i < num8; i++)
								{
									stringBuilder[i] = value;
								}
							}
							for (int j = num6; j < num7; j++)
							{
								bitArray.Set(j, value: true);
							}
						}
						break;
					}
					default:
						throw new InvalidOperationException("Unknown op: " + stringMod.op);
					}
					num2 += stringBuilder.Length - length;
					stringMod = ((!enumerator2.MoveNext()) ? null : ((StringMod)enumerator2.Current));
				}
			}
			if (flag)
			{
				toAppendTo.Append('-');
			}
			toAppendTo.Append(stringBuilder);
		}

		private void WriteScientific(double value, StringBuilder output, SortedList<StringMod, object> mods)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string pattern = decimalFmt.Pattern;
			int i;
			for (i = 0; pattern[i] == '#' || pattern[i] == '0'; i++)
			{
			}
			int num = i;
			if (pattern[0] == '#')
			{
				num--;
			}
			if (num >= 6 && value > 1.0)
			{
				pattern = pattern.Substring(1);
				stringBuilder.Append(value.ToString(pattern));
			}
			else
			{
				stringBuilder.Append(value.ToString("E"));
			}
			Writeint(stringBuilder, output, integerSpecials, mods, integerCommas);
			WriteFractional(stringBuilder, output);
			string text = stringBuilder.ToString();
			int num2 = text.IndexOf("E");
			int num3 = num2 + 1;
			char c = stringBuilder[num3];
			if (c != '-')
			{
				c = '+';
				if (text.IndexOf(c, num2) < 0)
				{
					stringBuilder.Insert(num3, '+');
				}
			}
			IEnumerator<Special> enumerator = exponentSpecials.GetEnumerator();
			enumerator.MoveNext();
			enumerator.MoveNext();
			Special current = enumerator.Current;
			char ch = current.ch;
			if (c == '-' || ch == '+')
			{
				mods.Add(ReplaceMod(current, startInclusive: true, current, endInclusive: true, c), null);
			}
			else
			{
				mods.Add(deleteMod(current, startInclusive: true, current, endInclusive: true), null);
			}
			StringBuilder stringBuilder2 = new StringBuilder(stringBuilder.ToString().Substring(num3 + 1));
			if (stringBuilder2.Length > 2 && stringBuilder2[0] == '0')
			{
				stringBuilder2.Remove(0, 1);
			}
			Writeint(stringBuilder2, output, exponentDigitSpecials, mods, ShowCommas: false);
		}

		private void WriteFraction(double value, StringBuilder result, double fractional, StringBuilder output, SortedList<StringMod, object> mods)
		{
			if (!improperFraction)
			{
				if (fractional == 0.0 && !HasChar('0', numeratorSpecials))
				{
					Writeint(result, output, integerSpecials, mods, ShowCommas: false);
					Special start = integerSpecials[integerSpecials.Count - 1];
					Special end = denominatorSpecials[denominatorSpecials.Count - 1];
					if (HasChar('?', integerSpecials, numeratorSpecials, denominatorSpecials))
					{
						mods.Add(ReplaceMod(start, startInclusive: false, end, endInclusive: true, ' '), null);
					}
					else
					{
						mods.Add(deleteMod(start, startInclusive: false, end, endInclusive: true), null);
					}
					return;
				}
				bool flag = value == 0.0 && fractional == 0.0;
				bool flag2 = fractional != 0.0 || HasChar('0', numeratorSpecials);
				bool flag3 = flag && (HasOnly('#', integerSpecials) || !HasChar('0', numeratorSpecials));
				bool flag4 = !flag && value == 0.0 && flag2 && !HasChar('0', integerSpecials);
				if (flag3 || flag4)
				{
					Special start2 = integerSpecials[integerSpecials.Count - 1];
					if (HasChar('?', integerSpecials, numeratorSpecials))
					{
						mods.Add(ReplaceMod(start2, startInclusive: true, numerator, endInclusive: false, ' '), null);
					}
					else
					{
						mods.Add(deleteMod(start2, startInclusive: true, numerator, endInclusive: false), null);
					}
				}
				else
				{
					Writeint(result, output, integerSpecials, mods, ShowCommas: false);
				}
			}
			try
			{
				int num;
				int num2;
				if (fractional == 0.0 || (improperFraction && fractional % 1.0 == 0.0))
				{
					num = (int)Math.Round(fractional);
					num2 = 1;
				}
				else
				{
					Fraction fraction = new Fraction(fractional, maxDenominator);
					num = fraction.Numerator;
					num2 = fraction.Denominator;
				}
				if (improperFraction)
				{
					num += (int)Math.Round(value * (double)num2);
				}
				WriteSingleint(numeratorFmt, num, output, numeratorSpecials, mods);
				WriteSingleint(denominatorFmt, num2, output, denominatorSpecials, mods);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.StackTrace);
			}
		}

		private static bool HasChar(char ch, params List<Special>[] numSpecials)
		{
			foreach (List<Special> list in numSpecials)
			{
				foreach (Special item in list)
				{
					if (item.ch == ch)
					{
						return true;
					}
				}
			}
			return false;
		}

		private static bool HasOnly(char ch, params List<Special>[] numSpecials)
		{
			foreach (List<Special> list in numSpecials)
			{
				foreach (Special item in list)
				{
					if (item.ch != ch)
					{
						return false;
					}
				}
			}
			return true;
		}

		private void WriteSingleint(string fmt, int num, StringBuilder output, List<Special> numSpecials, SortedList<StringMod, object> mods)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(num.ToString(fmt));
			Writeint(stringBuilder, output, numSpecials, mods, ShowCommas: false);
		}

		private void Writeint(StringBuilder result, StringBuilder output, List<Special> numSpecials, SortedList<StringMod, object> mods, bool ShowCommas)
		{
			int num = result.ToString().IndexOf(".") - 1;
			if (num < 0)
			{
				num = ((exponent == null || numSpecials != integerSpecials) ? (result.Length - 1) : (result.ToString().IndexOf("E") - 1));
			}
			int i;
			for (i = 0; i < num; i++)
			{
				char c = result[i];
				if (c != '0' && c != ',')
				{
					break;
				}
			}
			bool flag = false;
			Special special = null;
			int num2 = 0;
			for (int num3 = numSpecials.Count - 1; num3 >= 0; num3--)
			{
				char c2 = (num < 0) ? '0' : result[num];
				Special special2 = numSpecials[num3];
				flag = (ShowCommas && num2 > 0 && num2 % 3 == 0);
				bool flag2 = false;
				if (c2 != '0' || special2.ch == '0' || special2.ch == '?' || num >= i)
				{
					flag2 = (special2.ch == '?' && num < i);
					output[special2.pos] = (flag2 ? ' ' : c2);
					special = special2;
				}
				if (flag)
				{
					mods.Add(insertMod(special2, flag2 ? " " : ",", 2), null);
					flag = false;
				}
				num2++;
				num--;
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (num >= 0)
			{
				num++;
				stringBuilder = new StringBuilder(result.ToString().Substring(0, num));
				if (ShowCommas)
				{
					while (num > 0)
					{
						if (num2 > 0 && num2 % 3 == 0)
						{
							stringBuilder.Insert(num, ',');
						}
						num2++;
						num--;
					}
				}
				mods.Add(insertMod(special, stringBuilder.ToString(), 1), null);
			}
		}

		private void WriteFractional(StringBuilder result, StringBuilder output)
		{
			if (fractionalSpecials.Count > 0)
			{
				int num = result.ToString().IndexOf(".") + 1;
				int num2 = (exponent == null) ? (result.Length - 1) : (result.ToString().IndexOf("E") - 1);
				while (num2 > num && result[num2] == '0')
				{
					num2--;
				}
				IEnumerator<Special> enumerator = fractionalSpecials.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Special current = enumerator.Current;
					if (num >= result.Length)
					{
						break;
					}
					char c = result[num];
					if (c != '0' || current.ch == '0' || num < num2)
					{
						output[current.pos] = c;
					}
					else if (current.ch == '?')
					{
						output[current.pos] = ' ';
					}
					num++;
				}
			}
		}

		/// {@inheritDoc}
		/// <p />
		/// For a number, this is <tt>"#"</tt> for integer values, and <tt>"#.#"</tt>
		/// for floating-point values.
		public override void SimpleValue(StringBuilder toAppendTo, object value)
		{
			SIMPLE_NUMBER.FormatValue(toAppendTo, value);
		}
	}
}
