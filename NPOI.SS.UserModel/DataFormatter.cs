using NPOI.SS.Util;
using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace NPOI.SS.UserModel
{
	/// HSSFDataFormatter contains methods for Formatting the value stored in an
	/// Cell. This can be useful for reports and GUI presentations when you
	/// need to display data exactly as it appears in Excel. Supported Formats
	/// include currency, SSN, percentages, decimals, dates, phone numbers, zip
	/// codes, etc.
	///
	/// Internally, Formats will be implemented using subclasses of <see cref="T:NPOI.SS.Util.FormatBase" />
	/// such as <see cref="T:NPOI.SS.Util.DecimalFormat" /> and <see cref="T:NPOI.SS.Util.SimpleDateFormat" />. Therefore the
	/// Formats used by this class must obey the same pattern rules as these FormatBase
	/// subclasses. This means that only legal number pattern characters ("0", "#",
	/// ".", "," etc.) may appear in number formats. Other characters can be
	/// inserted <em>before</em> or <em> after</em> the number pattern to form a
	/// prefix or suffix.
	///
	///
	/// For example the Excel pattern <c>"$#,##0.00 "USD"_);($#,##0.00 "USD")"
	/// </c> will be correctly Formatted as "$1,000.00 USD" or "($1,000.00 USD)".
	/// However the pattern <c>"00-00-00"</c> is incorrectly Formatted by
	/// DecimalFormat as "000000--". For Excel Formats that are not compatible with
	/// DecimalFormat, you can provide your own custom {@link FormatBase} implementation
	/// via <c>HSSFDataFormatter.AddFormat(String,FormatBase)</c>. The following
	/// custom Formats are already provided by this class:
	///
	/// <pre>
	/// <ul><li>SSN "000-00-0000"</li>
	///     <li>Phone Number "(###) ###-####"</li>
	///     <li>Zip plus 4 "00000-0000"</li>
	/// </ul>
	/// </pre>
	///
	/// If the Excel FormatBase pattern cannot be Parsed successfully, then a default
	/// FormatBase will be used. The default number FormatBase will mimic the Excel General
	/// FormatBase: "#" for whole numbers and "#.##########" for decimal numbers. You
	/// can override the default FormatBase pattern with <c>
	/// HSSFDataFormatter.setDefaultNumberFormat(FormatBase)</c>. <b>Note:</b> the
	/// default FormatBase will only be used when a FormatBase cannot be Created from the
	/// cell's data FormatBase string.
	///
	/// @author James May (james dot may at fmr dot com)
	public class DataFormatter
	{
		/// Pattern to find a number FormatBase: "0" or  "#" 
		private static string numPattern;

		/// Pattern to find days of week as text "ddd...." 
		private static string daysAsText;

		/// Pattern to find "AM/PM" marker 
		private static string amPmPattern;

		/// A regex to find patterns like [$$-1009] and [$�-452]. 
		/// Note that we don't currently process these into locales 
		private static string localePatternGroup;

		private static string colorPattern;

		/// * Cells formatted with a date or time format and which contain invalid date or time values
		///                  *  show 255 pound signs ("#").
		private static string invalidDateTimeString;

		/// <em>General</em> FormatBase for whole numbers. 
		private static DecimalFormat generalWholeNumFormat;

		/// <em>General</em> FormatBase for decimal numbers. 
		private static DecimalFormat generalDecimalNumFormat;

		/// A default FormatBase to use when a number pattern cannot be Parsed. 
		private FormatBase defaultNumFormat;

		private CultureInfo currentCulture;

		private Hashtable formats;

		private bool emulateCsv;

		private static readonly Regex RegexDoubleBackslashAny;

		private static readonly Regex RegexContinueWs;

		private static readonly Regex RegexAnyInDoubleQuote;

		static DataFormatter()
		{
			numPattern = "[0#]+";
			daysAsText = "([d]{3,})";
			amPmPattern = "((A|P)[M/P]*)";
			localePatternGroup = "(\\[\\$[^-\\]]*-[0-9A-Z]+\\])";
			colorPattern = "(\\[BLACK\\])|(\\[BLUE\\])|(\\[CYAN\\])|(\\[GREEN\\])|(\\[MAGENTA\\])|(\\[RED\\])|(\\[WHITE\\])|(\\[YELLOW\\])|(\\[COLOR\\s*\\d\\])|(\\[COLOR\\s*[0-5]\\d\\])";
			generalWholeNumFormat = new DecimalFormat("0");
			generalDecimalNumFormat = new DecimalFormat("#.##########");
			RegexDoubleBackslashAny = new Regex("\\\\.");
			RegexContinueWs = new Regex("\\s");
			RegexAnyInDoubleQuote = new Regex("\"[^\"]*\"");
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < 255; i++)
			{
				stringBuilder.Append('#');
			}
			invalidDateTimeString = stringBuilder.ToString();
		}

		/// Creates a formatter using the {@link Locale#getDefault() default locale}.
		public DataFormatter()
			: this(emulateCsv: false)
		{
		}

		/// Constructor
		public DataFormatter(CultureInfo culture)
		{
			currentCulture = culture;
			formats = new Hashtable();
			FormatBase instance = ZipPlusFourFormat.instance;
			AddFormat("00000\\-0000", instance);
			AddFormat("00000-0000", instance);
			FormatBase instance2 = PhoneFormat.instance;
			AddFormat("[<=9999999]###\\-####;\\(###\\)\\ ###\\-####", instance2);
			AddFormat("[<=9999999]###-####;(###) ###-####", instance2);
			AddFormat("###\\-####;\\(###\\)\\ ###\\-####", instance2);
			AddFormat("###-####;(###) ###-####", instance2);
			FormatBase instance3 = SSNFormat.instance;
			AddFormat("000\\-00\\-0000", instance3);
			AddFormat("000-00-0000", instance3);
		}

		public DataFormatter(bool emulateCsv)
			: this(CultureInfo.CurrentCulture)
		{
			this.emulateCsv = emulateCsv;
		}

		/// Creates a formatter using the given locale.
		///
		/// @param  emulateCsv whether to emulate CSV output.
		public DataFormatter(CultureInfo locale, bool emulateCsv)
			: this(locale)
		{
			this.emulateCsv = emulateCsv;
		}

		/// Return a FormatBase for the given cell if one exists, otherwise try to
		/// Create one. This method will return <c>null</c> if the any of the
		/// following is true:
		/// <ul>
		/// <li>the cell's style is null</li>
		/// <li>the style's data FormatBase string is null or empty</li>
		/// <li>the FormatBase string cannot be recognized as either a number or date</li>
		/// </ul>
		///
		/// @param cell The cell to retrieve a FormatBase for
		/// @return A FormatBase for the FormatBase String
		private FormatBase GetFormat(ICell cell)
		{
			if (cell.CellStyle == null)
			{
				return null;
			}
			int dataFormat = cell.CellStyle.DataFormat;
			string dataFormatString = cell.CellStyle.GetDataFormatString();
			if (dataFormatString == null || dataFormatString.Trim().Length == 0)
			{
				return null;
			}
			return GetFormat(cell.NumericCellValue, dataFormat, dataFormatString);
		}

		private FormatBase GetFormat(double cellValue, int formatIndex, string formatStrIn)
		{
			string text = formatStrIn;
			int num = text.IndexOf(';');
			int num2 = text.LastIndexOf(';');
			if (num != -1 && num != num2)
			{
				int num3 = text.IndexOf(';', num + 1);
				text = ((num3 == num2) ? ((cellValue != 0.0) ? text.Substring(0, num2) : text.Substring(num2 + 1)) : ((cellValue != 0.0) ? text.Substring(0, num3) : text.Substring(num3 + 1, num2 - (num3 + 1))));
			}
			if (emulateCsv && cellValue == 0.0 && text.Contains("#") && !text.Contains("0"))
			{
				text = text.Replace("#", "");
			}
			FormatBase formatBase = (FormatBase)formats[text];
			if (formatBase != null)
			{
				return formatBase;
			}
			if (text.Equals("General", StringComparison.CurrentCultureIgnoreCase) || "@".Equals(text))
			{
				if (IsWholeNumber(cellValue))
				{
					return generalWholeNumFormat;
				}
				return generalDecimalNumFormat;
			}
			formatBase = CreateFormat(cellValue, formatIndex, text);
			formats[text] = formatBase;
			return formatBase;
		}

		/// Create and return a FormatBase based on the FormatBase string from a  cell's
		/// style. If the pattern cannot be Parsed, return a default pattern.
		///
		/// @param cell The Excel cell
		/// @return A FormatBase representing the excel FormatBase. May return null.
		public FormatBase CreateFormat(ICell cell)
		{
			int dataFormat = cell.CellStyle.DataFormat;
			string dataFormatString = cell.CellStyle.GetDataFormatString();
			return CreateFormat(cell.NumericCellValue, dataFormat, dataFormatString);
		}

		private FormatBase CreateFormat(double cellValue, int formatIndex, string sFormat)
		{
			string text = Regex.Replace(sFormat, colorPattern, "", RegexOptions.IgnoreCase);
			MatchCollection matchCollection = Regex.Matches(text, localePatternGroup);
			foreach (Match item in matchCollection)
			{
				string value = item.Value;
				int num = value.IndexOf('$') + 1;
				int num2 = value.IndexOf('-');
				string text2 = value.Substring(num, num2 - num);
				if (text2.IndexOf('$') > -1)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(text2.Substring(0, text2.IndexOf('$')));
					stringBuilder.Append('\\');
					stringBuilder.Append(text2.Substring(text2.IndexOf('$'), text2.Length));
					text2 = stringBuilder.ToString();
				}
				value = Regex.Replace(value, localePatternGroup, text2);
				text = text.Remove(item.Index, item.Length);
				text = text.Insert(item.Index, value);
			}
			if (text == null || text.Trim().Length == 0)
			{
				return GetDefaultFormat(cellValue);
			}
			if ("General".Equals(text, StringComparison.CurrentCultureIgnoreCase) || "@".Equals(text))
			{
				if (IsWholeNumber(cellValue))
				{
					return generalWholeNumFormat;
				}
				return generalDecimalNumFormat;
			}
			if (DateUtil.IsADateFormat(formatIndex, text) && DateUtil.IsValidExcelDate(cellValue))
			{
				return CreateDateFormat(text, cellValue);
			}
			if (text.IndexOf("#/#") >= 0 || text.IndexOf("?/?") >= 0)
			{
				string input = text.Replace("\\\\ ", " ").Replace("\\\\.", "").Replace("\"[^\"]*\"", " ");
				input = RegexDoubleBackslashAny.Replace(input, " ");
				input = RegexAnyInDoubleQuote.Replace(input, " ");
				input = RegexContinueWs.Replace(input, " ");
				bool flag = true;
				string[] array = input.Split(";".ToCharArray());
				foreach (string format in array)
				{
					int num3 = IndexOfFraction(format);
					if (num3 == -1 || num3 != LastIndexOfFraction(format))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					return new FractionFormat(input);
				}
			}
			if (Regex.IsMatch(text, numPattern))
			{
				return CreateNumberFormat(text, cellValue);
			}
			if (emulateCsv)
			{
				return new ConstantStringFormat(cleanFormatForNumber(text));
			}
			return null;
		}

		private int IndexOfFraction(string format)
		{
			int num = format.IndexOf("#/#");
			int num2 = format.IndexOf("?/?");
			if (num != -1)
			{
				if (num2 != -1)
				{
					return Math.Min(num, num2);
				}
				return num;
			}
			return num2;
		}

		private int LastIndexOfFraction(string format)
		{
			int num = format.LastIndexOf("#/#");
			int num2 = format.LastIndexOf("?/?");
			if (num != -1)
			{
				if (num2 != -1)
				{
					return Math.Max(num, num2);
				}
				return num;
			}
			return num2;
		}

		private FormatBase CreateDateFormat(string pformatStr, double cellValue)
		{
			string text = pformatStr.Replace("\\-", "-");
			text = text.Replace("\\,", ",");
			text = text.Replace("\\.", ".");
			text = text.Replace("\\ ", " ");
			text = text.Replace("\\/", "/");
			text = text.Replace(";@", "");
			bool flag = Regex.IsMatch(text, amPmPattern);
			if (flag)
			{
				text = Regex.Replace(text, amPmPattern, "@");
			}
			text = text.Replace("@", "tt");
			MatchCollection matchCollection = Regex.Matches(text, daysAsText);
			if (matchCollection.Count > 0)
			{
				string replacement = matchCollection[0].Groups[0].Value.ToUpper().Replace("D", "E");
				text = Regex.Replace(text, daysAsText, replacement);
			}
			StringBuilder stringBuilder = new StringBuilder();
			char[] array = text.ToCharArray();
			bool flag2 = true;
			ArrayList arrayList = new ArrayList();
			foreach (char c in array)
			{
				switch (c)
				{
				case 'H':
				case 'h':
					flag2 = false;
					if (flag)
					{
						stringBuilder.Append('h');
					}
					else
					{
						stringBuilder.Append('H');
					}
					break;
				case 'M':
				case 'm':
					if (flag2)
					{
						stringBuilder.Append('M');
						arrayList.Add(stringBuilder.Length - 1);
					}
					else
					{
						stringBuilder.Append('m');
					}
					break;
				case 'S':
				case 's':
					stringBuilder.Append('s');
					for (int j = 0; j < arrayList.Count; j++)
					{
						int index = (int)arrayList[j];
						if (stringBuilder[index] == 'M')
						{
							stringBuilder[index] = 'm';
						}
					}
					flag2 = true;
					arrayList.Clear();
					break;
				default:
					if (char.IsLetter(c))
					{
						flag2 = true;
						arrayList.Clear();
						switch (c)
						{
						case 'Y':
						case 'y':
							stringBuilder.Append('y');
							break;
						case 'D':
						case 'd':
							stringBuilder.Append('d');
							break;
						default:
							stringBuilder.Append(c);
							break;
						}
					}
					else
					{
						stringBuilder.Append(c);
					}
					break;
				}
			}
			text = stringBuilder.ToString();
			try
			{
				return new ExcelStyleDateFormatter(text);
			}
			catch (ArgumentException)
			{
				return GetDefaultFormat(cellValue);
			}
		}

		private string cleanFormatForNumber(string formatStr)
		{
			StringBuilder stringBuilder = new StringBuilder(formatStr);
			if (emulateCsv)
			{
				for (int i = 0; i < stringBuilder.Length; i++)
				{
					char c = stringBuilder[i];
					if ((c == '_' || c == '*' || c == '?') && (i <= 0 || stringBuilder[i - 1] != '\\'))
					{
						if (c == '?')
						{
							stringBuilder[i] = ' ';
						}
						else if (i < stringBuilder.Length - 1)
						{
							if (c == '_')
							{
								stringBuilder[i + 1] = ' ';
							}
							else
							{
								stringBuilder.Remove(i + 1, 1);
							}
							stringBuilder.Remove(i, 1);
							i--;
						}
					}
				}
			}
			else
			{
				for (int j = 0; j < stringBuilder.Length; j++)
				{
					char c2 = stringBuilder[j];
					if ((c2 == '_' || c2 == '*') && (j <= 0 || stringBuilder[j - 1] != '\\'))
					{
						if (j < stringBuilder.Length - 1)
						{
							stringBuilder.Remove(j + 1, 1);
						}
						stringBuilder.Remove(j, 1);
						j--;
					}
				}
			}
			for (int k = 0; k < stringBuilder.Length; k++)
			{
				switch (stringBuilder[k])
				{
				case '"':
				case '\\':
					stringBuilder.Remove(k, 1);
					k--;
					break;
				case '+':
					if (k > 0 && stringBuilder[k - 1] == 'E')
					{
						stringBuilder.Remove(k, 1);
						k--;
					}
					break;
				}
			}
			return stringBuilder.ToString();
		}

		private FormatBase CreateNumberFormat(string formatStr, double cellValue)
		{
			string pattern = cleanFormatForNumber(formatStr);
			try
			{
				return new DecimalFormat(pattern);
			}
			catch (ArgumentException)
			{
				return GetDefaultFormat(cellValue);
			}
		}

		/// Return true if the double value represents a whole number
		/// @param d the double value to check
		/// @return <c>true</c> if d is a whole number
		private static bool IsWholeNumber(double d)
		{
			return d == Math.Floor(d);
		}

		/// Returns a default FormatBase for a cell.
		/// @param cell The cell
		/// @return a default FormatBase
		public FormatBase GetDefaultFormat(ICell cell)
		{
			return GetDefaultFormat(cell.NumericCellValue);
		}

		private FormatBase GetDefaultFormat(double cellValue)
		{
			if (defaultNumFormat != null)
			{
				return defaultNumFormat;
			}
			if (IsWholeNumber(cellValue))
			{
				return generalWholeNumFormat;
			}
			return generalDecimalNumFormat;
		}

		/// Returns the Formatted value of an Excel date as a <c>String</c> based
		/// on the cell's <c>DataFormat</c>. i.e. "Thursday, January 02, 2003"
		/// , "01/02/2003" , "02-Jan" , etc.
		///
		/// @param cell The cell
		/// @return a Formatted date string
		private string GetFormattedDateString(ICell cell)
		{
			FormatBase format = GetFormat(cell);
			DateTime dateCellValue = cell.DateCellValue;
			if (format != null)
			{
				return format.Format(dateCellValue, currentCulture);
			}
			return dateCellValue.ToString();
		}

		/// Returns the Formatted value of an Excel number as a <c>String</c>
		/// based on the cell's <c>DataFormat</c>. Supported Formats include
		/// currency, percents, decimals, phone number, SSN, etc.:
		/// "61.54%", "$100.00", "(800) 555-1234".
		///
		/// @param cell The cell
		/// @return a Formatted number string
		private string GetFormattedNumberString(ICell cell)
		{
			FormatBase format = GetFormat(cell);
			double numericCellValue = cell.NumericCellValue;
			if (format == null)
			{
				return numericCellValue.ToString(currentCulture);
			}
			return format.Format(numericCellValue, currentCulture);
		}

		/// Formats the given raw cell value, based on the supplied
		///  FormatBase index and string, according to excel style rules.
		/// @see #FormatCellValue(Cell)
		public string FormatRawCellContents(double value, int formatIndex, string formatString)
		{
			return FormatRawCellContents(value, formatIndex, formatString, use1904Windowing: false);
		}

		/// Performs Excel-style date formatting, using the
		///  supplied Date and format
		private string PerformDateFormatting(DateTime d, FormatBase dateFormat)
		{
			if (dateFormat != null)
			{
				return dateFormat.Format(d, currentCulture);
			}
			return d.ToString();
		}

		/// Formats the given raw cell value, based on the supplied
		///  format index and string, according to excel style rules.
		/// @see #formatCellValue(Cell)
		public string FormatRawCellContents(double value, int formatIndex, string formatString, bool use1904Windowing)
		{
			if (DateUtil.IsADateFormat(formatIndex, formatString))
			{
				if (DateUtil.IsValidExcelDate(value))
				{
					FormatBase format = GetFormat(value, formatIndex, formatString);
					if (format is ExcelStyleDateFormatter)
					{
						((ExcelStyleDateFormatter)format).SetDateToBeFormatted(value);
					}
					DateTime javaDate = DateUtil.GetJavaDate(value, use1904Windowing);
					return PerformDateFormatting(javaDate, format);
				}
				if (emulateCsv)
				{
					return invalidDateTimeString;
				}
			}
			FormatBase format2 = GetFormat(value, formatIndex, formatString);
			if (format2 == null)
			{
				return value.ToString(currentCulture);
			}
			string text = format2.Format(value, currentCulture);
			if (text.Contains("E") && !text.Contains("E-"))
			{
				text = text.Replace("E", "E+");
			}
			return text;
		}

		/// Returns the Formatted value of a cell as a <c>String</c> regardless
		/// of the cell type. If the Excel FormatBase pattern cannot be Parsed then the
		/// cell value will be Formatted using a default FormatBase.
		///
		/// When passed a null or blank cell, this method will return an empty
		/// String (""). Formulas in formula type cells will not be evaluated.
		///
		///
		/// @param cell The cell
		/// @return the Formatted cell value as a String
		public string FormatCellValue(ICell cell)
		{
			return FormatCellValue(cell, null);
		}

		/// Returns the Formatted value of a cell as a <c>String</c> regardless
		/// of the cell type. If the Excel FormatBase pattern cannot be Parsed then the
		/// cell value will be Formatted using a default FormatBase.
		///
		/// When passed a null or blank cell, this method will return an empty
		/// String (""). Formula cells will be evaluated using the given
		/// {@link HSSFFormulaEvaluator} if the evaluator is non-null. If the
		/// evaluator is null, then the formula String will be returned. The caller
		/// is responsible for setting the currentRow on the evaluator
		///
		///
		/// @param cell The cell (can be null)
		/// @param evaluator The HSSFFormulaEvaluator (can be null)
		/// @return a string value of the cell
		public string FormatCellValue(ICell cell, IFormulaEvaluator evaluator)
		{
			if (cell != null)
			{
				CellType cellType = cell.CellType;
				if (evaluator != null && cellType == CellType.Formula)
				{
					try
					{
						cellType = evaluator.EvaluateFormulaCell(cell);
					}
					catch (Exception innerException)
					{
						throw new Exception("Did you forget to set the current row on the HSSFFormulaEvaluator?", innerException);
					}
				}
				switch (cellType)
				{
				case CellType.Formula:
					return cell.CellFormula;
				case CellType.Numeric:
					if (DateUtil.IsCellDateFormatted(cell))
					{
						return GetFormattedDateString(cell);
					}
					return GetFormattedNumberString(cell);
				case CellType.String:
					return cell.RichStringCellValue.String;
				case CellType.Boolean:
					return cell.BooleanCellValue.ToString().ToUpper();
				case CellType.Blank:
					return "";
				case CellType.Error:
					return FormulaError.ForInt(cell.ErrorCellValue).String;
				default:
					throw new Exception("Unexpected celltype (" + cellType + ")");
				}
			}
			return "";
		}

		/// Sets a default number FormatBase to be used when the Excel FormatBase cannot be
		/// Parsed successfully. <b>Note:</b> This is a fall back for when an error
		/// occurs while parsing an Excel number FormatBase pattern. This will not
		/// affect cells with the <em>General</em> FormatBase.
		///
		///
		/// The value that will be passed to the FormatBase's FormatBase method (specified
		/// by <c>java.text.FormatBase#FormatBase</c>) will be a double value from a
		/// numeric cell. Therefore the code in the FormatBase method should expect a
		/// <c>Number</c> value.
		///
		///
		/// @param FormatBase A FormatBase instance to be used as a default
		/// @see java.text.FormatBase#FormatBase
		public void SetDefaultNumberFormat(FormatBase format)
		{
			IEnumerator enumerator = formats.Keys.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string key = (string)enumerator.Current;
				if (formats[key] == generalDecimalNumFormat || formats[key] == generalWholeNumFormat)
				{
					formats[key] = format;
				}
			}
			defaultNumFormat = format;
		}

		/// Adds a new FormatBase to the available formats.
		///
		/// The value that will be passed to the FormatBase's FormatBase method (specified
		/// by <c>java.text.FormatBase#FormatBase</c>) will be a double value from a
		/// numeric cell. Therefore the code in the FormatBase method should expect a
		/// <c>Number</c> value.
		///
		/// @param excelformatStr The data FormatBase string
		/// @param FormatBase A FormatBase instance
		public void AddFormat(string excelformatStr, FormatBase format)
		{
			formats[excelformatStr] = format;
		}
	}
}
