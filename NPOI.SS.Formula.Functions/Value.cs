using NPOI.SS.Formula.Eval;
using System;
using System.Globalization;
using System.Text;

namespace NPOI.SS.Formula.Functions
{
	public class Value : Fixed1ArgFunction
	{
		/// "1,0000" is valid, "1,00" is not 
		private const int MIN_DISTANCE_BETWEEN_THOUSANDS_SEPARATOR = 4;

		private const double ZERO = 0.0;

		public override ValueEval Evaluate(int srcRowIndex, int srcColumnIndex, ValueEval arg0)
		{
			ValueEval singleValue;
			try
			{
				singleValue = OperandResolver.GetSingleValue(arg0, srcRowIndex, srcColumnIndex);
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			string strText = OperandResolver.CoerceValueToString(singleValue);
			double num = ConvertTextToNumber(strText);
			if (double.IsNaN(num))
			{
				return ErrorEval.VALUE_INVALID;
			}
			return new NumberEval(num);
		}

		/// TODO see if the same functionality is needed in {@link OperandResolver#parseDouble(String)}
		///
		/// @return <code>null</code> if there is any problem converting the text
		private static double ConvertTextToNumber(string strText)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			int length = strText.Length;
			int i;
			for (i = 0; i < length; i++)
			{
				char c = strText[i];
				if (char.IsDigit(c) || c == '.')
				{
					break;
				}
				switch (c)
				{
				case '$':
					if (flag)
					{
						return double.NaN;
					}
					flag = true;
					break;
				case '+':
					if (flag3 || flag2)
					{
						return double.NaN;
					}
					flag2 = true;
					break;
				case '-':
					if (flag3 || flag2)
					{
						return double.NaN;
					}
					flag3 = true;
					break;
				default:
					return double.NaN;
				case ' ':
					break;
				}
			}
			if (i >= length)
			{
				if (flag || flag3 || flag2)
				{
					return double.NaN;
				}
				return 0.0;
			}
			bool flag4 = false;
			int num = -32768;
			StringBuilder stringBuilder = new StringBuilder(length);
			for (; i < length; i++)
			{
				char c2 = strText[i];
				if (char.IsDigit(c2))
				{
					stringBuilder.Append(c2);
				}
				else
				{
					switch (c2)
					{
					case ' ':
					{
						string text = strText.Substring(i);
						if (text.Trim().Length > 0)
						{
							return double.NaN;
						}
						break;
					}
					case '.':
						if (flag4)
						{
							return double.NaN;
						}
						if (i - num < 4)
						{
							return double.NaN;
						}
						flag4 = true;
						stringBuilder.Append('.');
						break;
					case ',':
					{
						if (flag4)
						{
							return double.NaN;
						}
						int num2 = i - num;
						if (num2 < 4)
						{
							return double.NaN;
						}
						num = i;
						break;
					}
					case 'E':
					case 'e':
						if (i - num < 4)
						{
							return double.NaN;
						}
						stringBuilder.Append(strText.Substring(i));
						i = length;
						break;
					default:
						return double.NaN;
					}
				}
			}
			if (!flag4 && i - num < 4)
			{
				return double.NaN;
			}
			double num3;
			try
			{
				num3 = double.Parse(stringBuilder.ToString(), CultureInfo.InvariantCulture);
			}
			catch (FormatException)
			{
				return double.NaN;
			}
			if (!flag3)
			{
				return num3;
			}
			return 0.0 - num3;
		}
	}
}
