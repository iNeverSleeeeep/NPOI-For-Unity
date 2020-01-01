using NPOI.Util;
using System;
using System.Text;

namespace NPOI.SS.Format
{
	/// A formatter for the default "General" cell format.
	///
	/// @author Ken Arnold, Industrious Media LLC
	public class CellGeneralFormatter : CellFormatter
	{
		/// Creates a new general formatter. 
		public CellGeneralFormatter()
			: base("General")
		{
		}

		/// The general style is not quite the same as any other, or any combination
		/// of others.
		///
		/// @param toAppendTo The buffer to append to.
		/// @param value      The value to format.
		public override void FormatValue(StringBuilder toAppendTo, object value)
		{
			if (Number.IsNumber(value))
			{
				double.TryParse(value.ToString(), out double result);
				if (result == 0.0)
				{
					toAppendTo.Append('0');
				}
				else
				{
					double num = Math.Log10(Math.Abs(result));
					bool flag = true;
					string text;
					if (num > 10.0 || num < -9.0)
					{
						text = "E5";
					}
					else if ((double)(long)result != result)
					{
						text = "F9";
					}
					else
					{
						text = "F0";
						flag = false;
					}
					toAppendTo.Append(result.ToString(text));
					if (flag)
					{
						int num2 = (!text.StartsWith("E")) ? (toAppendTo.Length - 1) : (toAppendTo.ToString().LastIndexOf("E") - 1);
						while (toAppendTo[num2] == '0')
						{
							toAppendTo.Remove(num2--, 1);
						}
						if (toAppendTo[num2] == '.')
						{
							toAppendTo.Remove(num2--, 1);
						}
						string text2 = toAppendTo.ToString();
						num2 = toAppendTo.ToString().LastIndexOf("E");
						if (num2 > 0)
						{
							num2++;
							if (text2[num2] == '+' || text2[num2] == '-')
							{
								num2++;
							}
							int i;
							for (i = 0; num2 + i < text2.Length && text2[num2 + i] == '0'; i++)
							{
							}
							toAppendTo.Remove(num2, i);
						}
					}
				}
			}
			else if (value is bool)
			{
				toAppendTo.Append(value.ToString().ToUpper());
			}
			else
			{
				toAppendTo.Append(value.ToString());
			}
		}

		/// Equivalent to {@link #formatValue(StringBuilder,Object)}. {@inheritDoc}. 
		public override void SimpleValue(StringBuilder toAppendTo, object value)
		{
			FormatValue(toAppendTo, value);
		}
	}
}
