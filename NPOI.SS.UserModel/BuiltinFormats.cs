using System;
using System.Collections.Generic;
using System.Globalization;

namespace NPOI.SS.UserModel
{
	/// Utility to identify built-in formats.  The following is a list of the formats as
	/// returned by this class.<p />
	///             <p />
	///       0, "General"<br />
	///       1, "0"<br />
	///       2, "0.00"<br />
	///       3, "#,##0"<br />
	///       4, "#,##0.00"<br />
	///       5, "$#,##0_);($#,##0)"<br />
	///       6, "$#,##0_);[Red]($#,##0)"<br />
	///       7, "$#,##0.00);($#,##0.00)"<br />
	///       8, "$#,##0.00_);[Red]($#,##0.00)"<br />
	///       9, "0%"<br />
	///       0xa, "0.00%"<br />
	///       0xb, "0.00E+00"<br />
	///       0xc, "# ?/?"<br />
	///       0xd, "# ??/??"<br />
	///       0xe, "m/d/yy"<br />
	///       0xf, "d-mmm-yy"<br />
	///       0x10, "d-mmm"<br />
	///       0x11, "mmm-yy"<br />
	///       0x12, "h:mm AM/PM"<br />
	///       0x13, "h:mm:ss AM/PM"<br />
	///       0x14, "h:mm"<br />
	///       0x15, "h:mm:ss"<br />
	///       0x16, "m/d/yy h:mm"<br />
	///             <p />
	///       // 0x17 - 0x24 reserved for international and undocumented
	///       0x25, "#,##0_);(#,##0)"<br />
	///       0x26, "#,##0_);[Red](#,##0)"<br />
	///       0x27, "#,##0.00_);(#,##0.00)"<br />
	///       0x28, "#,##0.00_);[Red](#,##0.00)"<br />
	///       0x29, "_(*#,##0_);_(*(#,##0);_(* \"-\"_);_(@_)"<br />
	///       0x2a, "_($*#,##0_);_($*(#,##0);_($* \"-\"_);_(@_)"<br />
	///       0x2b, "_(*#,##0.00_);_(*(#,##0.00);_(*\"-\"??_);_(@_)"<br />
	///       0x2c, "_($*#,##0.00_);_($*(#,##0.00);_($*\"-\"??_);_(@_)"<br />
	///       0x2d, "mm:ss"<br />
	///       0x2e, "[h]:mm:ss"<br />
	///       0x2f, "mm:ss.0"<br />
	///       0x30, "##0.0E+0"<br />
	///       0x31, "@" - This is text format.<br />
	///       0x31  "text" - Alias for "@"<br />
	/// <p />
	///
	/// @author Yegor Kozlov
	///
	/// Modified 6/17/09 by Stanislav Shor - positive formats don't need starting '('
	public class BuiltinFormats
	{
		/// The first user-defined format starts at 164.
		public const int FIRST_USER_DEFINED_FORMAT_INDEX = 164;

		private static string[] _formats;

		static BuiltinFormats()
		{
			List<string> list = new List<string>();
			PutFormat(list, 0, "General");
			PutFormat(list, 1, "0");
			PutFormat(list, 2, "0.00");
			PutFormat(list, 3, "#,##0");
			PutFormat(list, 4, "#,##0.00");
			PutFormat(list, 5, "\"$\"#,##0_);(\"$\"#,##0)");
			PutFormat(list, 6, "\"$\"#,##0_);[Red](\"$\"#,##0)");
			PutFormat(list, 7, "\"$\"#,##0.00_);(\"$\"#,##0.00)");
			PutFormat(list, 8, "\"$\"#,##0.00_);[Red](\"$\"#,##0.00)");
			PutFormat(list, 9, "0%");
			PutFormat(list, 10, "0.00%");
			PutFormat(list, 11, "0.00E+00");
			PutFormat(list, 12, "# ?/?");
			PutFormat(list, 13, "# ??/??");
			PutFormat(list, 14, "m/d/yy");
			PutFormat(list, 15, "d-mmm-yy");
			PutFormat(list, 16, "d-mmm");
			PutFormat(list, 17, "mmm-yy");
			PutFormat(list, 18, "h:mm AM/PM");
			PutFormat(list, 19, "h:mm:ss AM/PM");
			PutFormat(list, 20, "h:mm");
			PutFormat(list, 21, "h:mm:ss");
			PutFormat(list, 22, "m/d/yy h:mm");
			for (int i = 23; i <= 36; i++)
			{
				PutFormat(list, i, "reserved-0x" + i.ToString("X", CultureInfo.CurrentCulture));
			}
			PutFormat(list, 37, "#,##0_);(#,##0)");
			PutFormat(list, 38, "#,##0_);[Red](#,##0)");
			PutFormat(list, 39, "#,##0.00_);(#,##0.00)");
			PutFormat(list, 40, "#,##0.00_);[Red](#,##0.00)");
			PutFormat(list, 41, "_(\"$\"* #,##0_);_(\"$\"* (#,##0);_(\"$\"* \"-\"_);_(@_)");
			PutFormat(list, 42, "_(* #,##0_);_(* (#,##0);_(* \"-\"_);_(@_)");
			PutFormat(list, 43, "_(\"$\"* #,##0.00_);_(\"$\"* (#,##0.00);_(\"$\"* \"-\"??_);_(@_)");
			PutFormat(list, 44, "_(* #,##0.00_);_(* (#,##0.00);_(* \"-\"??_);_(@_)");
			PutFormat(list, 45, "mm:ss");
			PutFormat(list, 46, "[h]:mm:ss");
			PutFormat(list, 47, "mm:ss.0");
			PutFormat(list, 48, "##0.0E+0");
			PutFormat(list, 49, "@");
			string[] array = _formats = list.ToArray();
		}

		private static void PutFormat(List<string> m, int index, string value)
		{
			if (m.Count != index)
			{
				throw new InvalidOperationException("index " + index + " is wrong");
			}
			m.Add(value);
		}

		/// @deprecated (May 2009) use {@link #getAll()}
		[Obsolete]
		public static Dictionary<int, string> GetBuiltinFormats()
		{
			Dictionary<int, string> dictionary = new Dictionary<int, string>();
			for (int i = 0; i < _formats.Length; i++)
			{
				dictionary.Add(i, _formats[i]);
			}
			return dictionary;
		}

		/// @return array of built-in data formats
		public static string[] GetAll()
		{
			return (string[])_formats.Clone();
		}

		/// Get the format string that matches the given format index
		///
		/// @param index of a built in format
		/// @return string represented at index of format or <code>null</code> if there is not a built-in format at that index
		public static string GetBuiltinFormat(int index)
		{
			if (index < 0 || index >= _formats.Length)
			{
				return null;
			}
			return _formats[index];
		}

		/// Get the format index that matches the given format string.
		///
		/// <p>
		/// Automatically converts "text" to excel's format string to represent text.
		/// </p>
		/// @param pFmt string matching a built-in format
		/// @return index of format or -1 if undefined.
		public static int GetBuiltinFormat(string pFmt)
		{
			string text = (string.Compare(pFmt, "TEXT", StringComparison.OrdinalIgnoreCase) != 0) ? pFmt : "@";
			for (int i = 0; i < _formats.Length; i++)
			{
				if (text.Equals(_formats[i]))
				{
					return i;
				}
			}
			return -1;
		}
	}
}
