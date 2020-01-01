using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NPOI.SS.Format
{
	/// This class : printing out an elapsed time format.
	///
	/// @author Ken Arnold, Industrious Media LLC
	public class CellElapsedFormatter : CellFormatter
	{
		private class TimeSpec
		{
			internal char type;

			internal int pos;

			internal int len;

			internal double factor;

			internal double modBy;

			public TimeSpec(char type, int pos, int len, double factor)
			{
				this.type = type;
				this.pos = pos;
				this.len = len;
				this.factor = factor;
				modBy = 0.0;
			}

			public long ValueFor(double elapsed)
			{
				double num = (modBy != 0.0) ? (elapsed / factor % modBy) : (elapsed / factor);
				if (type == '0')
				{
					return (long)Math.Round(num);
				}
				return (long)num;
			}
		}

		private class ElapsedPartHandler : CellFormatPart.IPartHandler
		{
			private CellElapsedFormatter _formatter;

			public ElapsedPartHandler(CellElapsedFormatter formatter)
			{
				_formatter = formatter;
			}

			public string HandlePart(Match m, string part, CellFormatType type, StringBuilder desc)
			{
				int length = desc.Length;
				switch (part[0])
				{
				case '[':
					if (part.Length >= 3)
					{
						if (_formatter.topmost != null)
						{
							throw new ArgumentException("Duplicate '[' times in format");
						}
						part = part.ToLower();
						int num = part.Length - 2;
						_formatter.topmost = _formatter.AssignSpec(part[1], length, num);
						return part.Substring(1, num);
					}
					break;
				case '0':
				case 'h':
				case 'm':
				case 's':
					part = part.ToLower();
					_formatter.AssignSpec(part[0], length, part.Length);
					return part;
				case '\n':
					return "%n";
				case '"':
					part = part.Substring(1, part.Length - 2);
					break;
				case '\\':
					part = part.Substring(1);
					break;
				case '*':
					if (part.Length > 1)
					{
						part = CellFormatPart.ExpandChar(part);
					}
					break;
				case '_':
					return null;
				}
				return part;
			}
		}

		private const double HOUR__FACTOR = 0.041666666666666664;

		private const double MIN__FACTOR = 0.00069444444444444436;

		private const double SEC__FACTOR = 1.1574074074074073E-05;

		private List<TimeSpec> specs;

		private TimeSpec topmost;

		private string printfFmt;

		private static readonly Regex PERCENTS = new Regex("%");

		/// Creates a elapsed time formatter.
		///
		/// @param pattern The pattern to Parse.
		public CellElapsedFormatter(string pattern)
			: base(pattern)
		{
			specs = new List<TimeSpec>();
			StringBuilder stringBuilder = CellFormatPart.ParseFormat(pattern, CellFormatType.ELAPSED, new ElapsedPartHandler(this));
			for (int num = specs.Count - 1; num >= 0; num--)
			{
				TimeSpec timeSpec = specs[num];
				stringBuilder.Remove(timeSpec.pos, timeSpec.len);
				stringBuilder.Insert(timeSpec.pos, "D" + timeSpec.len);
				if (timeSpec.type != topmost.type)
				{
					timeSpec.modBy = modFor(timeSpec.type, timeSpec.len);
				}
			}
			printfFmt = stringBuilder.ToString();
		}

		private TimeSpec AssignSpec(char type, int pos, int len)
		{
			TimeSpec timeSpec = new TimeSpec(type, pos, len, factorFor(type, len));
			specs.Add(timeSpec);
			return timeSpec;
		}

		private static double factorFor(char type, int len)
		{
			switch (type)
			{
			case 'h':
				return 0.041666666666666664;
			case 'm':
				return 0.00069444444444444436;
			case 's':
				return 1.1574074074074073E-05;
			case '0':
				return 1.1574074074074073E-05 / Math.Pow(10.0, (double)len);
			default:
				throw new ArgumentException("Uknown elapsed time spec: " + type);
			}
		}

		private static double modFor(char type, int len)
		{
			switch (type)
			{
			case 'h':
				return 24.0;
			case 'm':
				return 60.0;
			case 's':
				return 60.0;
			case '0':
				return Math.Pow(10.0, (double)len);
			default:
				throw new ArgumentException("Uknown elapsed time spec: " + type);
			}
		}

		/// {@inheritDoc} 
		public override void FormatValue(StringBuilder toAppendTo, object value)
		{
			double num = (double)value;
			if (num < 0.0)
			{
				toAppendTo.Append('-');
				num = 0.0 - num;
			}
			long[] array = new long[specs.Count];
			for (int i = 0; i < specs.Count; i++)
			{
				array[i] = specs[i].ValueFor(num);
			}
			string[] array2 = printfFmt.Split(":. []".ToCharArray());
			string empty = string.Empty;
			int num2 = 0;
			int num3 = 0;
			Regex regex = new Regex("D\\d+");
			string[] array3 = array2;
			foreach (string text in array3)
			{
				num2 += text.Length;
				if (num2 < printfFmt.Length)
				{
					empty = printfFmt[num2].ToString();
					num2++;
				}
				else
				{
					empty = string.Empty;
				}
				if (regex.IsMatch(text))
				{
					toAppendTo.Append(array[num3].ToString(text)).Append(empty);
					num3++;
				}
				else
				{
					toAppendTo.Append(text).Append(empty);
				}
			}
		}

		/// {@inheritDoc}
		/// <p />
		/// For a date, this is <tt>"mm/d/y"</tt>.
		public override void SimpleValue(StringBuilder toAppendTo, object value)
		{
			FormatValue(toAppendTo, value);
		}
	}
}
