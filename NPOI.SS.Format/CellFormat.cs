using NPOI.SS.UserModel;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NPOI.SS.Format
{
	/// Format a value according to the standard Excel behavior.  This "standard" is
	/// not explicitly documented by Microsoft, so the behavior is determined by
	/// experimentation; see the tests.
	///
	/// An Excel format has up to four parts, Separated by semicolons.  Each part
	/// specifies what to do with particular kinds of values, depending on the number
	/// of parts given: 
	///
	/// - One part (example: <c>[Green]#.##</c>) 
	/// If the value is a number, display according to this one part (example: green text,
	/// with up to two decimal points). If the value is text, display it as is.
	///
	/// - Two parts (example: <c>[Green]#.##;[Red]#.##</c>) 
	/// If the value is a positive number or zero, display according to the first part (example: green
	/// text, with up to two decimal points); if it is a negative number, display
	/// according to the second part (example: red text, with up to two decimal
	/// points). If the value is text, display it as is. 
	///
	/// - Three parts (example: <c>[Green]#.##;[Black]#.##;[Red]#.##</c>) 
	/// If the value is a positive number, display according to the first part (example: green text, with up to
	/// two decimal points); if it is zero, display according to the second part
	/// (example: black text, with up to two decimal points); if it is a negative
	/// number, display according to the third part (example: red text, with up to
	/// two decimal points). If the value is text, display it as is.
	///
	/// - Four parts (example: <c>[Green]#.##;[Black]#.##;[Red]#.##;[@]</c>)
	/// If the value is a positive number, display according to the first part (example: green text,
	/// with up to two decimal points); if it is zero, display according to the
	/// second part (example: black text, with up to two decimal points); if it is a
	/// negative number, display according to the third part (example: red text, with
	/// up to two decimal points). If the value is text, display according to the
	/// fourth part (example: text in the cell's usual color, with the text value
	/// surround by brackets).
	///
	/// In Addition to these, there is a general format that is used when no format
	/// is specified.  This formatting is presented by the {@link #GENERAL_FORMAT}
	/// object.
	///
	/// @author Ken Arnold, Industrious Media LLC
	public class CellFormat
	{
		/// Format a value as it would be were no format specified.  This is also
		/// used when the format specified is <tt>General</tt>.
		public class GeneralCellFormat : CellFormat
		{
			public GeneralCellFormat()
				: base("General")
			{
			}

			public override CellFormatResult Apply(object value)
			{
				string text = new CellGeneralFormatter().Format(value);
				return new CellFormatResult(applies: true, text, Color.Empty);
			}
		}

		private const string INVALID_VALUE_FOR_FORMAT = "###############################################################################################################################################################################################################################################################";

		private const string QUOTE = "\"";

		private string format;

		private CellFormatPart posNumFmt;

		private CellFormatPart zeroNumFmt;

		private CellFormatPart negNumFmt;

		private CellFormatPart textFmt;

		private int formatPartCount;

		private static readonly Regex ONE_PART = new Regex(CellFormatPart.FORMAT_PAT.ToString() + "(;|$)", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

		private static readonly CellFormatPart DEFAULT_TEXT_FORMAT = new CellFormatPart("@");

		private static readonly CellFormat GENERAL_FORMAT = new GeneralCellFormat();

		/// Maps a format string to its Parsed version for efficiencies sake. 
		private static Dictionary<string, CellFormat> formatCache = new Dictionary<string, CellFormat>();

		/// Returns a {@link CellFormat} that applies the given format.  Two calls
		/// with the same format may or may not return the same object.
		///
		/// @param format The format.
		///
		/// @return A {@link CellFormat} that applies the given format.
		public static CellFormat GetInstance(string format)
		{
			CellFormat cellFormat = null;
			if (formatCache.ContainsKey(format))
			{
				cellFormat = formatCache[format];
			}
			if (cellFormat == null)
			{
				cellFormat = ((!format.Equals("General") && !format.Equals("@")) ? new CellFormat(format) : GENERAL_FORMAT);
				formatCache.Add(format, cellFormat);
			}
			return cellFormat;
		}

		/// Creates a new object.
		///
		/// @param format The format.
		private CellFormat(string format)
		{
			this.format = format;
			MatchCollection matchCollection = ONE_PART.Matches(format);
			List<CellFormatPart> list = new List<CellFormatPart>();
			foreach (Match item in matchCollection)
			{
				try
				{
					string text = item.Groups[0].Value;
					if (text.EndsWith(";"))
					{
						text = text.Substring(0, text.Length - 1);
					}
					list.Add(new CellFormatPart(text));
				}
				catch (Exception)
				{
					list.Add(null);
				}
			}
			formatPartCount = list.Count;
			switch (formatPartCount)
			{
			case 1:
				posNumFmt = list[0];
				negNumFmt = null;
				zeroNumFmt = null;
				textFmt = DEFAULT_TEXT_FORMAT;
				break;
			case 2:
				posNumFmt = list[0];
				negNumFmt = list[1];
				zeroNumFmt = null;
				textFmt = DEFAULT_TEXT_FORMAT;
				break;
			case 3:
				posNumFmt = list[0];
				negNumFmt = list[1];
				zeroNumFmt = list[2];
				textFmt = DEFAULT_TEXT_FORMAT;
				break;
			default:
				posNumFmt = list[0];
				negNumFmt = list[1];
				zeroNumFmt = list[2];
				textFmt = list[3];
				break;
			}
		}

		/// Returns the result of Applying the format to the given value.  If the
		/// value is a number (a type of {@link Number} object), the correct number
		/// format type is chosen; otherwise it is considered a text object.
		///
		/// @param value The value
		///
		/// @return The result, in a {@link CellFormatResult}.
		public virtual CellFormatResult Apply(object value)
		{
			if (Number.IsNumber(value))
			{
				double.TryParse(value.ToString(), out double result);
				if (result < 0.0 && ((formatPartCount == 2 && !posNumFmt.HasCondition && !negNumFmt.HasCondition) || (formatPartCount == 3 && !negNumFmt.HasCondition) || (formatPartCount == 4 && !negNumFmt.HasCondition)))
				{
					return negNumFmt.Apply(0.0 - result);
				}
				return GetApplicableFormatPart(result).Apply(result);
			}
			if (value is DateTime)
			{
				double excelDate = DateUtil.GetExcelDate((DateTime)value);
				if (DateUtil.IsValidExcelDate(excelDate))
				{
					return GetApplicableFormatPart(excelDate).Apply(value);
				}
				throw new ArgumentException("value not a valid Excel date");
			}
			return textFmt.Apply(value);
		}

		/// Returns the result of applying the format to the given date.
		///
		/// @param date         The date.
		/// @param numericValue The numeric value for the date.
		///
		/// @return The result, in a {@link CellFormatResult}.
		private CellFormatResult Apply(DateTime date, double numericValue)
		{
			return GetApplicableFormatPart(numericValue).Apply(date);
		}

		/// Fetches the appropriate value from the cell, and returns the result of
		/// Applying it to the appropriate format.  For formula cells, the computed
		/// value is what is used.
		///
		/// @param c The cell.
		///
		/// @return The result, in a {@link CellFormatResult}.
		public CellFormatResult Apply(ICell c)
		{
			switch (UltimateType(c))
			{
			case CellType.Blank:
				return Apply("");
			case CellType.Boolean:
				return Apply(c.BooleanCellValue);
			case CellType.Numeric:
			{
				double numericCellValue = c.NumericCellValue;
				if (GetApplicableFormatPart(numericCellValue).CellFormatType == CellFormatType.DATE)
				{
					if (DateUtil.IsValidExcelDate(numericCellValue))
					{
						return Apply(c.DateCellValue, numericCellValue);
					}
					return Apply("###############################################################################################################################################################################################################################################################");
				}
				return Apply(numericCellValue);
			}
			case CellType.String:
				return Apply(c.StringCellValue);
			default:
				return Apply("?");
			}
		}

		/// Uses the result of Applying this format to the value, Setting the text
		/// and color of a label before returning the result.
		///
		/// @param label The label to apply to.
		/// @param value The value to Process.
		///
		/// @return The result, in a {@link CellFormatResult}.
		public CellFormatResult Apply(Label label, object value)
		{
			CellFormatResult cellFormatResult = Apply(value);
			label.Text = cellFormatResult.Text;
			if (cellFormatResult.TextColor != Color.Empty)
			{
				label.ForeColor = cellFormatResult.TextColor;
			}
			return cellFormatResult;
		}

		/// Uses the result of applying this format to the given date, setting the text
		/// and color of a label before returning the result.
		///
		/// @param label        The label to apply to.
		/// @param date         The date.
		/// @param numericValue The numeric value for the date.
		///
		/// @return The result, in a {@link CellFormatResult}.
		private CellFormatResult Apply(Label label, DateTime date, double numericValue)
		{
			CellFormatResult cellFormatResult = Apply(date, numericValue);
			label.Text = cellFormatResult.Text;
			if (cellFormatResult.TextColor != Color.Empty)
			{
				label.ForeColor = cellFormatResult.TextColor;
			}
			return cellFormatResult;
		}

		/// Fetches the appropriate value from the cell, and uses the result, Setting
		/// the text and color of a label before returning the result.
		///
		/// @param label The label to apply to.
		/// @param c     The cell.
		///
		/// @return The result, in a {@link CellFormatResult}.
		public CellFormatResult Apply(Label label, ICell c)
		{
			switch (UltimateType(c))
			{
			case CellType.Blank:
				return Apply(label, "");
			case CellType.Boolean:
				return Apply(label, c.BooleanCellValue);
			case CellType.Numeric:
			{
				double numericCellValue = c.NumericCellValue;
				if (GetApplicableFormatPart(numericCellValue).CellFormatType == CellFormatType.DATE)
				{
					if (DateUtil.IsValidExcelDate(numericCellValue))
					{
						return Apply(label, c.DateCellValue, numericCellValue);
					}
					return Apply(label, "###############################################################################################################################################################################################################################################################");
				}
				return Apply(label, numericCellValue);
			}
			case CellType.String:
				return Apply(label, c.StringCellValue);
			default:
				return Apply(label, "?");
			}
		}

		/// Returns the {@link CellFormatPart} that applies to the value.  Result
		/// depends on how many parts the cell format has, the cell value and any
		/// conditions.  The value must be a {@link Number}.
		///
		/// @param value The value.
		/// @return The {@link CellFormatPart} that applies to the value.
		private CellFormatPart GetApplicableFormatPart(object value)
		{
			if (Number.IsNumber(value))
			{
				double.TryParse(value.ToString(), out double result);
				if (formatPartCount == 1)
				{
					if (!posNumFmt.HasCondition || (posNumFmt.HasCondition && posNumFmt.Applies(result)))
					{
						return posNumFmt;
					}
					return new CellFormatPart("General");
				}
				if (formatPartCount == 2)
				{
					if ((!posNumFmt.HasCondition && result >= 0.0) || (posNumFmt.HasCondition && posNumFmt.Applies(result)))
					{
						return posNumFmt;
					}
					if (!negNumFmt.HasCondition || (negNumFmt.HasCondition && negNumFmt.Applies(result)))
					{
						return negNumFmt;
					}
					return new CellFormatPart("\"###############################################################################################################################################################################################################################################################\"");
				}
				if ((!posNumFmt.HasCondition && result > 0.0) || (posNumFmt.HasCondition && posNumFmt.Applies(result)))
				{
					return posNumFmt;
				}
				if ((!negNumFmt.HasCondition && result < 0.0) || (negNumFmt.HasCondition && negNumFmt.Applies(result)))
				{
					return negNumFmt;
				}
				return zeroNumFmt;
			}
			throw new ArgumentException("value must be a Number");
		}

		/// Returns the ultimate cell type, following the results of formulas.  If
		/// the cell is a {@link Cell#CELL_TYPE_FORMULA}, this returns the result of
		/// {@link Cell#getCachedFormulaResultType()}.  Otherwise this returns the
		/// result of {@link Cell#getCellType()}.
		///
		/// @param cell The cell.
		///
		/// @return The ultimate type of this cell.
		public static CellType UltimateType(ICell cell)
		{
			CellType cellType = cell.CellType;
			if (cellType == CellType.Formula)
			{
				return cell.CachedFormulaResultType;
			}
			return cellType;
		}

		/// Returns <tt>true</tt> if the other object is a {@link CellFormat} object
		/// with the same format.
		///
		/// @param obj The other object.
		///
		/// @return <tt>true</tt> if the two objects are Equal.
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			if (obj is CellFormat)
			{
				CellFormat cellFormat = (CellFormat)obj;
				return format.Equals(cellFormat.format);
			}
			return false;
		}

		/// Returns a hash code for the format.
		///
		/// @return A hash code for the format.
		public override int GetHashCode()
		{
			return format.GetHashCode();
		}

		public override string ToString()
		{
			return format;
		}
	}
}
