using NPOI.SS.Util;
using NPOI.Util;
using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace NPOI.SS.Formula
{
	/// Formats sheet names for use in formula expressions.
	///
	/// @author Josh Micich
	public class SheetNameFormatter
	{
		private const string BIFF8_LAST_COLUMN = "IV";

		private const int BIFF8_LAST_COLUMN_TEXT_LEN = 2;

		private const char DELIMITER = '\'';

		private const string CELL_REF_PATTERN = "^([A-Za-z]+)([0-9]+)$";

		private static readonly string BIFF8_LAST_ROW = 65536.ToString(CultureInfo.InvariantCulture);

		private static readonly int BIFF8_LAST_ROW_TEXT_LEN = BIFF8_LAST_ROW.Length;

		private SheetNameFormatter()
		{
		}

		/// Used to format sheet names as they would appear in cell formula expressions.
		/// @return the sheet name UnChanged if there is no need for delimiting.  Otherwise the sheet
		/// name is enclosed in single quotes (').  Any single quotes which were already present in the 
		/// sheet name will be converted to double single quotes ('').  
		public static string Format(string rawSheetName)
		{
			StringBuilder stringBuilder = new StringBuilder(rawSheetName.Length + 2);
			AppendFormat(stringBuilder, rawSheetName);
			return stringBuilder.ToString();
		}

		/// Convenience method for when a StringBuilder is already available
		///
		/// @param out - sheet name will be Appended here possibly with delimiting quotes 
		public static void AppendFormat(StringBuilder out1, string rawSheetName)
		{
			if (NeedsDelimiting(rawSheetName))
			{
				out1.Append('\'');
				AppendAndEscape(out1, rawSheetName);
				out1.Append('\'');
			}
			else
			{
				out1.Append(rawSheetName);
			}
		}

		public static void AppendFormat(StringBuilder out1, string workbookName, string rawSheetName)
		{
			if (NeedsDelimiting(workbookName) || NeedsDelimiting(rawSheetName))
			{
				out1.Append('\'');
				out1.Append('[');
				AppendAndEscape(out1, workbookName.Replace('[', '(').Replace(']', ')'));
				out1.Append(']');
				AppendAndEscape(out1, rawSheetName);
				out1.Append('\'');
			}
			else
			{
				out1.Append('[');
				out1.Append(workbookName);
				out1.Append(']');
				out1.Append(rawSheetName);
			}
		}

		private static void AppendAndEscape(StringBuilder sb, string rawSheetName)
		{
			int length = rawSheetName.Length;
			for (int i = 0; i < length; i++)
			{
				char c = rawSheetName[i];
				if (c == '\'')
				{
					sb.Append('\'');
				}
				sb.Append(c);
			}
		}

		private static bool NeedsDelimiting(string rawSheetName)
		{
			int length = rawSheetName.Length;
			if (length < 1)
			{
				throw new Exception("Zero Length string is an invalid sheet name");
			}
			if (char.IsDigit(rawSheetName[0]))
			{
				return true;
			}
			for (int i = 0; i < length; i++)
			{
				char ch = rawSheetName[i];
				if (IsSpecialChar(ch))
				{
					return true;
				}
			}
			if (char.IsLetter(rawSheetName[0]) && char.IsDigit(rawSheetName[length - 1]) && NameLooksLikePlainCellReference(rawSheetName))
			{
				return true;
			}
			if (NameLooksLikeBooleanLiteral(rawSheetName))
			{
				return true;
			}
			return false;
		}

		private static bool NameLooksLikeBooleanLiteral(string rawSheetName)
		{
			switch (rawSheetName[0])
			{
			case 'T':
			case 't':
				return "TRUE".Equals(rawSheetName, StringComparison.OrdinalIgnoreCase);
			case 'F':
			case 'f':
				return "FALSE".Equals(rawSheetName, StringComparison.OrdinalIgnoreCase);
			default:
				return false;
			}
		}

		/// @return <c>true</c> if the presence of the specified Char in a sheet name would 
		/// require the sheet name to be delimited in formulas.  This includes every non-alphanumeric 
		/// Char besides Underscore '_'.
		private static bool IsSpecialChar(char ch)
		{
			if (!char.IsLetterOrDigit(ch))
			{
				switch (ch)
				{
				case '.':
				case '_':
					return false;
				case '\t':
				case '\n':
				case '\r':
					throw new Exception("Illegal Char (0x" + StringUtil.ToHexString(ch) + ") found in sheet name");
				default:
					return true;
				}
			}
			return false;
		}

		/// Used to decide whether sheet names like 'AB123' need delimiting due to the fact that they 
		/// look like cell references.
		/// <p />
		/// This code is currently being used for translating formulas represented with <code>Ptg</code>
		/// tokens into human readable text form.  In formula expressions, a sheet name always has a 
		/// trailing '!' so there is little chance for ambiguity.  It doesn't matter too much what this 
		/// method returns but it is worth noting the likely consumers of these formula text strings:
		/// <ol>
		/// <li>POI's own formula parser</li>
		/// <li>Visual reading by human</li>
		/// <li>VBA automation entry into Excel cell contents e.g.  ActiveCell.Formula = "=c64!A1"</li>
		/// <li>Manual entry into Excel cell contents</li>
		/// <li>Some third party formula parser</li>
		/// </ol>
		///
		/// At the time of writing, POI's formula parser tolerates cell-like sheet names in formulas
		/// with or without delimiters.  The same goes for Excel(2007), both manual and automated entry.  
		/// <p />
		/// For better or worse this implementation attempts to replicate Excel's formula renderer.
		/// Excel uses range checking on the apparent 'row' and 'column' components.  Note however that
		/// the maximum sheet size varies across versions.
		/// @see org.apache.poi.hssf.util.CellReference
		public static bool CellReferenceIsWithinRange(string lettersPrefix, string numbersSuffix)
		{
			return CellReference.CellReferenceIsWithinRange(lettersPrefix, numbersSuffix, SpreadsheetVersion.EXCEL97);
		}

		/// Note - this method assumes the specified rawSheetName has only letters and digits.  It 
		/// cannot be used to match absolute or range references (using the dollar or colon char).
		///
		/// Some notable cases:
		///    <blockquote><table border="0" cellpAdding="1" cellspacing="0" summary="Notable cases.">
		///      <tr><th>Input </th><th>Result </th><th>Comments</th></tr>
		///      <tr><td>"A1" </td><td>true</td><td> </td></tr>
		///      <tr><td>"a111" </td><td>true</td><td> </td></tr>
		///      <tr><td>"AA" </td><td>false</td><td> </td></tr>
		///      <tr><td>"aa1" </td><td>true</td><td> </td></tr>
		///      <tr><td>"A1A" </td><td>false</td><td> </td></tr>
		///      <tr><td>"A1A1" </td><td>false</td><td> </td></tr>
		///      <tr><td>"A$1:$C$20" </td><td>false</td><td>Not a plain cell reference</td></tr>
		///      <tr><td>"SALES20080101" </td><td>true</td>
		///      		<td>Still needs delimiting even though well out of range</td></tr>
		///    </table></blockquote>
		///
		/// @return <c>true</c> if there is any possible ambiguity that the specified rawSheetName
		/// could be interpreted as a valid cell name.
		public static bool NameLooksLikePlainCellReference(string rawSheetName)
		{
			Regex regex = new Regex("^([A-Za-z]+)([0-9]+)$");
			if (!regex.IsMatch(rawSheetName))
			{
				return false;
			}
			Match match = regex.Matches(rawSheetName)[0];
			string value = match.Groups[1].Value;
			string value2 = match.Groups[2].Value;
			return CellReferenceIsWithinRange(value, value2);
		}
	}
}
