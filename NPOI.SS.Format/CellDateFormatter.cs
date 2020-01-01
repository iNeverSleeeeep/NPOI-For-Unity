using NPOI.SS.Util;
using NPOI.Util;
using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace NPOI.SS.Format
{
	/// Formats a date value.
	///
	/// @author Ken Arnold, Industrious Media LLC
	public class CellDateFormatter : CellFormatter
	{
		private class DatePartHandler : CellFormatPart.IPartHandler
		{
			private CellDateFormatter _formatter;

			private int mStart = -1;

			private int mLen;

			private int hStart = -1;

			private int hLen;

			public DatePartHandler(CellDateFormatter formatter)
			{
				_formatter = formatter;
			}

			public string HandlePart(Match m, string part, CellFormatType type, StringBuilder desc)
			{
				int length = desc.Length;
				switch (part[0])
				{
				case 'S':
				case 's':
					if (mStart >= 0)
					{
						for (int i = 0; i < mLen; i++)
						{
							desc[mStart + i] = 'm';
						}
						mStart = -1;
					}
					return part.ToLower();
				case 'H':
				case 'h':
					mStart = -1;
					hStart = length;
					hLen = part.Length;
					return part.ToLower();
				case 'D':
				case 'd':
					mStart = -1;
					return part.ToLower();
				case 'M':
				case 'm':
					mStart = length;
					mLen = part.Length;
					if (hStart >= 0)
					{
						return part.ToLower();
					}
					return part.ToUpper();
				case 'Y':
				case 'y':
					mStart = -1;
					if (part.Length == 3)
					{
						part = "yyyy";
					}
					return part.ToLower();
				case '0':
				{
					mStart = -1;
					int length2 = part.Length;
					_formatter.sFmt = "%0" + (length2 + 2) + "." + length2 + "f";
					return part.Replace('0', 'f');
				}
				case 'A':
				case 'P':
				case 'a':
				case 'p':
					if (part.Length > 1)
					{
						mStart = -1;
						_formatter.ShowAmPm = true;
						_formatter.ShowM = (char.ToLower(part[1]) == 'm');
						_formatter.amPmUpper = (_formatter.ShowM || char.IsUpper(part[0]));
						if (_formatter.ShowM)
						{
							return "tt";
						}
						return "t";
					}
					return null;
				default:
					return null;
				}
			}

			public void Finish(StringBuilder toAppendTo)
			{
				if (hStart >= 0 && !_formatter.ShowAmPm)
				{
					for (int i = 0; i < hLen; i++)
					{
						toAppendTo[hStart + i] = 'H';
					}
				}
			}
		}

		private bool amPmUpper;

		private bool ShowM;

		private bool ShowAmPm;

		private FormatBase dateFmt;

		private string sFmt;

		private static readonly TimeSpan EXCEL_EPOCH_TIME;

		private static readonly DateTime EXCEL_EPOCH_DATE;

		private static readonly CellFormatter SIMPLE_DATE;

		static CellDateFormatter()
		{
			SIMPLE_DATE = new CellDateFormatter("mm/d/y");
			EXCEL_EPOCH_TIME = (EXCEL_EPOCH_DATE = new DateTime(1904, 1, 1)).TimeOfDay;
		}

		/// Creates a new date formatter with the given specification.
		///
		/// @param format The format.
		public CellDateFormatter(string format)
			: base(format)
		{
			DatePartHandler datePartHandler = new DatePartHandler(this);
			StringBuilder stringBuilder = CellFormatPart.ParseFormat(format, CellFormatType.DATE, datePartHandler);
			datePartHandler.Finish(stringBuilder);
			dateFmt = new SimpleDateFormat(stringBuilder.ToString());
		}

		/// {@inheritDoc} 
		public override void FormatValue(StringBuilder toAppendTo, object value)
		{
			if (value == null)
			{
				value = 0.0;
			}
			if (Number.IsNumber(value))
			{
				double.TryParse(value.ToString(), out double result);
				value = ((result != 0.0) ? ((object)new DateTime((long)((double)EXCEL_EPOCH_TIME.Ticks + result))) : ((object)EXCEL_EPOCH_DATE));
			}
			dateFmt.Format(value, toAppendTo, CultureInfo.CurrentCulture);
		}

		/// {@inheritDoc}
		/// <p />
		/// For a date, this is <tt>"mm/d/y"</tt>.
		public override void SimpleValue(StringBuilder toAppendTo, object value)
		{
			SIMPLE_DATE.FormatValue(toAppendTo, value);
		}
	}
}
