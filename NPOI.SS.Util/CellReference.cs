using NPOI.SS.Formula;
using NPOI.SS.UserModel;
using NPOI.Util;
using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace NPOI.SS.Util
{
	/// @author  Avik Sengupta
	/// @author  Dennis doubleday (patch to seperateRowColumns())
	public class CellReference
	{
		/// The character ($) that signifies a row or column value is absolute instead of relative 
		private const char ABSOLUTE_REFERENCE_MARKER = '$';

		/// The character (!) that Separates sheet names from cell references 
		private const char SHEET_NAME_DELIMITER = '!';

		/// The character (') used to quote sheet names when they contain special characters 
		private const char SPECIAL_NAME_DELIMITER = '\'';

		/// Matches a run of one or more letters followed by a run of one or more digits.
		/// The run of letters is group 1 and the run of digits is group 2.  
		/// Each group may optionally be prefixed with a single '$'.
		private const string CELL_REF_PATTERN = "^\\$?([A-Za-z]+)\\$?([0-9]+)";

		/// Matches a run of one or more letters.  The run of letters is group 1.  
		/// The text may optionally be prefixed with a single '$'.
		private const string COLUMN_REF_PATTERN = "^\\$?([A-Za-z]+)$";

		/// Matches a run of one or more digits.  The run of digits is group 1.
		/// The text may optionally be prefixed with a single '$'.
		private const string ROW_REF_PATTERN = "^\\$?([0-9]+)$";

		/// Named range names must start with a letter or underscore.  Subsequent characters may include
		/// digits or dot.  (They can even end in dot).
		private const string NAMED_RANGE_NAME_PATTERN = "^[_A-Za-z][_.A-Za-z0-9]*$";

		private int _rowIndex;

		private int _colIndex;

		private string _sheetName;

		private bool _isRowAbs;

		private bool _isColAbs;

		public int Row => _rowIndex;

		public short Col => (short)_colIndex;

		public bool IsRowAbsolute => _isRowAbs;

		public bool IsColAbsolute => _isColAbs;

		/// @return possibly <c>null</c> if this is a 2D reference.  Special characters are not
		/// escaped or delimited
		public string SheetName => _sheetName;

		/// Returns the three parts of the cell reference, the
		///  Sheet name (or null if none supplied), the 1 based
		///  row number, and the A based column letter.
		/// This will not include any markers for absolute
		///  references, so use {@link #formatAsString()}
		///  to properly turn references into strings. 
		public string[] CellRefParts => new string[3]
		{
			_sheetName,
			(_rowIndex + 1).ToString(CultureInfo.InvariantCulture),
			ConvertNumToColString(_colIndex)
		};

		/// Create an cell ref from a string representation.  Sheet names containing special characters should be
		/// delimited and escaped as per normal syntax rules for formulas.
		public CellReference(string cellRef)
		{
			if (cellRef.EndsWith("#REF!", StringComparison.CurrentCulture))
			{
				throw new ArgumentException("Cell reference invalid: " + cellRef);
			}
			string[] array = SeparateRefParts(cellRef);
			_sheetName = array[0];
			string text = array[1];
			_isColAbs = (text.Length > 0 && text[0] == '$');
			if (_isColAbs)
			{
				text = text.Substring(1);
			}
			if (text.Length == 0)
			{
				_colIndex = -1;
			}
			else
			{
				_colIndex = ConvertColStringToIndex(text);
			}
			string text2 = array[2];
			_isRowAbs = (text2.Length > 0 && text2[0] == '$');
			if (_isRowAbs)
			{
				text2 = text2.Substring(1);
			}
			if (text2.Length == 0)
			{
				_rowIndex = -1;
			}
			else
			{
				_rowIndex = int.Parse(text2, CultureInfo.InvariantCulture) - 1;
			}
		}

		public CellReference(ICell cell)
			: this(cell.RowIndex, cell.ColumnIndex, pAbsRow: false, pAbsCol: false)
		{
		}

		public CellReference(int pRow, int pCol)
			: this(pRow, pCol, pAbsRow: false, pAbsCol: false)
		{
		}

		public CellReference(int pRow, short pCol)
			: this(pRow, pCol & 0xFFFF, pAbsRow: false, pAbsCol: false)
		{
		}

		public CellReference(int pRow, int pCol, bool pAbsRow, bool pAbsCol)
			: this(null, pRow, pCol, pAbsRow, pAbsCol)
		{
		}

		public CellReference(string pSheetName, int pRow, int pCol, bool pAbsRow, bool pAbsCol)
		{
			if (pRow < -1)
			{
				throw new ArgumentException("row index may not be negative");
			}
			if (pCol < -1)
			{
				throw new ArgumentException("column index may not be negative");
			}
			_sheetName = pSheetName;
			_rowIndex = pRow;
			_colIndex = pCol;
			_isRowAbs = pAbsRow;
			_isColAbs = pAbsCol;
		}

		/// takes in a column reference portion of a CellRef and converts it from
		/// ALPHA-26 number format to 0-based base 10.
		/// 'A' -&gt; 0
		/// 'Z' -&gt; 25
		/// 'AA' -&gt; 26
		/// 'IV' -&gt; 255
		/// @return zero based column index
		public static int ConvertColStringToIndex(string ref1)
		{
			int num = 0;
			int num2 = 0;
			for (int num3 = ref1.Length - 1; num3 >= 0; num3--)
			{
				char c = ref1[num3];
				if (c == '$')
				{
					if (num3 == 0)
					{
						break;
					}
					throw new ArgumentException("Bad col ref format '" + ref1 + "'");
				}
				int num4 = (int)Math.Pow(26.0, (double)num);
				num2 += (Character.GetNumericValue(c) - 9) * num4;
				num++;
			}
			return num2 - 1;
		}

		public static bool IsPartAbsolute(string part)
		{
			return part[0] == '$';
		}

		public static NameType ClassifyCellReference(string str, SpreadsheetVersion ssVersion)
		{
			int length = str.Length;
			if (length < 1)
			{
				throw new ArgumentException("Empty string not allowed");
			}
			char c = str[0];
			char c2 = c;
			if (c2 != '$' && c2 != '.' && c2 != '_' && !char.IsLetter(c) && !char.IsDigit(c))
			{
				throw new ArgumentException("Invalid first char (" + c + ") of cell reference or named range.  Letter expected");
			}
			if (!char.IsDigit(str[length - 1]))
			{
				return ValidateNamedRangeName(str, ssVersion);
			}
			Regex regex = new Regex("^\\$?([A-Za-z]+)\\$?([0-9]+)");
			if (!regex.IsMatch(str))
			{
				return ValidateNamedRangeName(str, ssVersion);
			}
			MatchCollection matchCollection = regex.Matches(str);
			string value = matchCollection[0].Groups[1].Value;
			string value2 = matchCollection[0].Groups[2].Value;
			if (CellReferenceIsWithinRange(value, value2, ssVersion))
			{
				return NameType.Cell;
			}
			if (str.IndexOf('$') >= 0)
			{
				return NameType.BadCellOrNamedRange;
			}
			return NameType.NamedRange;
		}

		private static NameType ValidateNamedRangeName(string str, SpreadsheetVersion ssVersion)
		{
			Regex regex = new Regex("^\\$?([A-Za-z]+)$");
			if (regex.IsMatch(str))
			{
				Group group = regex.Matches(str)[0].Groups[1];
				if (IsColumnWithnRange(group.Value, ssVersion))
				{
					return NameType.Column;
				}
			}
			Regex regex2 = new Regex("^\\$?([0-9]+)$");
			if (regex2.IsMatch(str))
			{
				Group group2 = regex2.Matches(str)[0].Groups[1];
				if (IsRowWithnRange(group2.Value, ssVersion))
				{
					return NameType.Row;
				}
			}
			if (!Regex.IsMatch(str, "^[_A-Za-z][_.A-Za-z0-9]*$"))
			{
				return NameType.BadCellOrNamedRange;
			}
			return NameType.NamedRange;
		}

		/// Takes in a 0-based base-10 column and returns a ALPHA-26
		///  representation.
		/// eg column #3 -&gt; D
		public static string ConvertNumToColString(int col)
		{
			int num = col + 1;
			string text = "";
			int num2 = num;
			while (num2 > 0)
			{
				int num3 = num2 % 26;
				if (num3 == 0)
				{
					num3 = 26;
				}
				num2 = (num2 - num3) / 26;
				char c = (char)(num3 + 64);
				text = c + text;
			}
			return text;
		}

		/// Separates the row from the columns and returns an array of three Strings.  The first element
		/// is the sheet name. Only the first element may be null.  The second element in is the column 
		/// name still in ALPHA-26 number format.  The third element is the row.
		private static string[] SeparateRefParts(string reference)
		{
			int num = reference.LastIndexOf('!');
			string text = ParseSheetName(reference, num);
			int num2 = num + 1;
			int length = reference.Length;
			int i = num2;
			if (reference[i] == '$')
			{
				i++;
			}
			for (; i < length; i++)
			{
				char c = reference[i];
				if (char.IsDigit(c) || c == '$')
				{
					break;
				}
			}
			return new string[3]
			{
				text,
				reference.Substring(num2, i - num2),
				reference.Substring(i)
			};
		}

		private static string ParseSheetName(string reference, int indexOfSheetNameDelimiter)
		{
			if (indexOfSheetNameDelimiter < 0)
			{
				return null;
			}
			if (reference[0] != '\'')
			{
				return reference.Substring(0, indexOfSheetNameDelimiter);
			}
			int num = indexOfSheetNameDelimiter - 1;
			if (reference[num] != '\'')
			{
				throw new Exception("Mismatched quotes: (" + reference + ")");
			}
			StringBuilder stringBuilder = new StringBuilder(indexOfSheetNameDelimiter);
			for (int i = 1; i < num; i++)
			{
				char c = reference[i];
				if (c != '\'')
				{
					stringBuilder.Append(c);
				}
				else
				{
					if (i >= num || reference[i + 1] != '\'')
					{
						throw new Exception("Bad sheet name quote escaping: (" + reference + ")");
					}
					i++;
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		/// Example return values:
		///   <table border="0" cellpAdding="1" cellspacing="0" summary="Example return values">
		///     <tr><th align="left">Result</th><th align="left">Comment</th></tr>
		///     <tr><td>A1</td><td>Cell reference without sheet</td></tr>
		///     <tr><td>Sheet1!A1</td><td>Standard sheet name</td></tr>
		///     <tr><td>'O''Brien''s Sales'!A1'</td><td>Sheet name with special characters</td></tr>
		///   </table>
		///             @return the text representation of this cell reference as it would appear in a formula.
		public string FormatAsString()
		{
			StringBuilder stringBuilder = new StringBuilder(32);
			if (_sheetName != null)
			{
				SheetNameFormatter.AppendFormat(stringBuilder, _sheetName);
				stringBuilder.Append('!');
			}
			AppendCellReference(stringBuilder);
			return stringBuilder.ToString();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append(GetType().Name).Append(" [");
			stringBuilder.Append(FormatAsString());
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		/// Appends cell reference with '$' markers for absolute values as required.
		/// Sheet name is not included.
		public void AppendCellReference(StringBuilder sb)
		{
			if (_colIndex != -1)
			{
				if (_isColAbs)
				{
					sb.Append('$');
				}
				sb.Append(ConvertNumToColString(_colIndex));
			}
			if (_rowIndex != -1)
			{
				if (_isRowAbs)
				{
					sb.Append('$');
				}
				sb.Append(_rowIndex + 1);
			}
		}

		/// Used to decide whether a name of the form "[A-Z]*[0-9]*" that appears in a formula can be 
		/// interpreted as a cell reference.  Names of that form can be also used for sheets and/or
		/// named ranges, and in those circumstances, the question of whether the potential cell 
		/// reference is valid (in range) becomes important.
		/// <p />
		/// Note - that the maximum sheet size varies across Excel versions:
		/// <p />
		/// <blockquote><table border="0" cellpadding="1" cellspacing="0" summary="Notable cases.">
		///   <tr><th>Version </th><th>File Format </th>
		///   	<th>Last Column </th><th>Last Row</th></tr>
		///   <tr><td>97-2003</td><td>BIFF8</td><td>"IV" (2^8)</td><td>65536 (2^14)</td></tr>
		///   <tr><td>2007</td><td>BIFF12</td><td>"XFD" (2^14)</td><td>1048576 (2^20)</td></tr>
		/// </table></blockquote>
		/// POI currently targets BIFF8 (Excel 97-2003), so the following behaviour can be observed for
		/// this method:
		/// <blockquote><table border="0" cellpadding="1" cellspacing="0" summary="Notable cases.">
		///   <tr><th>Input    </th>
		///       <th>Result </th></tr>
		///   <tr><td>"A", "1"</td><td>true</td></tr>
		///   <tr><td>"a", "111"</td><td>true</td></tr>
		///   <tr><td>"A", "65536"</td><td>true</td></tr>
		///   <tr><td>"A", "65537"</td><td>false</td></tr>
		///   <tr><td>"iv", "1"</td><td>true</td></tr>
		///   <tr><td>"IW", "1"</td><td>false</td></tr>
		///   <tr><td>"AAA", "1"</td><td>false</td></tr>
		///   <tr><td>"a", "111"</td><td>true</td></tr>
		///   <tr><td>"Sheet", "1"</td><td>false</td></tr>
		/// </table></blockquote>
		///
		/// @param colStr a string of only letter characters
		/// @param rowStr a string of only digit characters
		/// @return <c>true</c> if the row and col parameters are within range of a BIFF8 spreadsheet.
		public static bool CellReferenceIsWithinRange(string colStr, string rowStr, SpreadsheetVersion ssVersion)
		{
			if (!IsColumnWithnRange(colStr, ssVersion))
			{
				return false;
			}
			return IsRowWithnRange(rowStr, ssVersion);
		}

		public static bool IsRowWithnRange(string rowStr, SpreadsheetVersion ssVersion)
		{
			int num = int.Parse(rowStr, CultureInfo.InvariantCulture);
			if (num < 0)
			{
				throw new InvalidOperationException("Invalid rowStr '" + rowStr + "'.");
			}
			if (num == 0)
			{
				return false;
			}
			return num <= ssVersion.MaxRows;
		}

		public static bool IsColumnWithnRange(string colStr, SpreadsheetVersion ssVersion)
		{
			string lastColumnName = ssVersion.LastColumnName;
			int length = lastColumnName.Length;
			int length2 = colStr.Length;
			if (length2 > length)
			{
				return false;
			}
			if (length2 == length && string.Compare(colStr.ToUpper(), lastColumnName, StringComparison.Ordinal) > 0)
			{
				return false;
			}
			return true;
		}

		public override bool Equals(object o)
		{
			if (!(o is CellReference))
			{
				return false;
			}
			CellReference cellReference = (CellReference)o;
			if (_rowIndex == cellReference._rowIndex && _colIndex == cellReference._colIndex && _isRowAbs == cellReference._isColAbs)
			{
				return _isColAbs == cellReference._isColAbs;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return _isRowAbs.GetHashCode() ^ _isColAbs.GetHashCode() ^ _rowIndex ^ _colIndex;
		}
	}
}
