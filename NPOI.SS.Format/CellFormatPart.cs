using NPOI.HSSF.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NPOI.SS.Format
{
	/// Objects of this class represent a single part of a cell format expression.
	/// Each cell can have up to four of these for positive, zero, negative, and text
	/// values.
	/// <p />
	/// Each format part can contain a color, a condition, and will always contain a
	/// format specification.  For example <tt>"[Red][&gt;=10]#"</tt> has a color
	/// (<tt>[Red]</tt>), a condition (<tt>&gt;=10</tt>) and a format specification
	/// (<tt>#</tt>).
	/// <p />
	/// This class also Contains patterns for matching the subparts of format
	/// specification.  These are used internally, but are made public in case other
	/// code has use for them.
	///
	/// @author Ken Arnold, Industrious Media LLC
	public class CellFormatPart
	{
		private class CaseInsensitiveComparator : IEqualityComparer<string>
		{
			public bool Equals(string x, string y)
			{
				return x.Equals(y, StringComparison.InvariantCultureIgnoreCase);
			}

			public int GetHashCode(string obj)
			{
				return obj.GetHashCode();
			}
		}

		public interface IPartHandler
		{
			string HandlePart(Match m, string part, CellFormatType type, StringBuilder desc);
		}

		private Color color;

		private CellFormatCondition condition;

		private CellFormatter format;

		private CellFormatType type;

		private static Dictionary<string, Color> NAMED_COLORS;

		public static IEqualityComparer<string> CASE_INSENSITIVE_ORDER;

		/// Pattern for the color part of a cell format part. 
		public static Regex COLOR_PAT;

		/// Pattern for the condition part of a cell format part. 
		public static Regex CONDITION_PAT;

		/// Pattern for the format specification part of a cell format part. 
		public static Regex SPECIFICATION_PAT;

		/// Pattern for an entire cell single part. 
		public static Regex FORMAT_PAT;

		/// Within {@link #FORMAT_PAT}, the group number for the matched color. 
		public static int COLOR_GROUP;

		/// Within {@link #FORMAT_PAT}, the group number for the operator in the
		/// condition.
		public static int CONDITION_OPERATOR_GROUP;

		/// Within {@link #FORMAT_PAT}, the group number for the value in the
		/// condition.
		public static int CONDITION_VALUE_GROUP;

		/// Within {@link #FORMAT_PAT}, the group number for the format
		/// specification.
		public static int SPECIFICATION_GROUP;

		/// Returns the CellFormatType object implied by the format specification for
		/// the format part.
		///
		/// @return The CellFormatType.
		internal CellFormatType CellFormatType => type;

		/// Returns <tt>true</tt> if this format part has a condition.
		///
		/// @return <tt>true</tt> if this format part has a condition.
		internal bool HasCondition => condition != null;

		static CellFormatPart()
		{
			CASE_INSENSITIVE_ORDER = new CaseInsensitiveComparator();
			NAMED_COLORS = new Dictionary<string, Color>(CASE_INSENSITIVE_ORDER);
			Hashtable indexHash = HSSFColor.GetIndexHash();
			foreach (object value2 in indexHash.Values)
			{
				HSSFColor hSSFColor = (HSSFColor)value2;
				Type type = hSSFColor.GetType();
				string name = type.Name;
				if (name.Equals(name.ToUpper()))
				{
					byte[] rGB = hSSFColor.RGB;
					Color value = Color.FromArgb(rGB[0], rGB[1], rGB[2]);
					if (!NAMED_COLORS.ContainsKey(name))
					{
						NAMED_COLORS.Add(name, value);
					}
					if (name.IndexOf('_') > 0 && !NAMED_COLORS.ContainsKey(name.Replace('_', ' ')))
					{
						NAMED_COLORS.Add(name.Replace('_', ' '), value);
					}
					if (name.IndexOf("_PERCENT") > 0 && !NAMED_COLORS.ContainsKey(name.Replace("_PERCENT", "%").Replace('_', ' ')))
					{
						NAMED_COLORS.Add(name.Replace("_PERCENT", "%").Replace('_', ' '), value);
					}
				}
			}
			string text = "([<>=]=?|!=|<>)    # The operator\n  \\s*([0-9]+(?:\\.[0-9]*)?)\\s*  # The constant to test against\n";
			string text2 = "\\[(black|blue|cyan|green|magenta|red|white|yellow|color [0-9]+)\\]";
			string text3 = "\\\\.                 # Quoted single character\n|\"([^\\\\\"]|\\\\.)*\"         # Quoted string of characters (handles escaped quotes like \\\") \n|_.                             # Space as wide as a given character\n|\\*.                           # Repeating fill character\n|@                              # Text: cell text\n|([0?\\#](?:[0?\\#,]*))         # Number: digit + other digits and commas\n|e[-+]                          # Number: Scientific: Exponent\n|m{1,5}                         # Date: month or minute spec\n|d{1,4}                         # Date: day/date spec\n|y{2,4}                         # Date: year spec\n|h{1,2}                         # Date: hour spec\n|s{1,2}                         # Date: second spec\n|am?/pm?                        # Date: am/pm spec\n|\\[h{1,2}\\]                   # Elapsed time: hour spec\n|\\[m{1,2}\\]                   # Elapsed time: minute spec\n|\\[s{1,2}\\]                   # Elapsed time: second spec\n|[^;]                           # A character\n";
			string pattern = "(?:" + text2 + ")?                  # Text color\n(?:\\[" + text + "\\])?                # Condition\n((?:" + text3 + ")+)                        # Format spec\n";
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace;
			COLOR_PAT = new Regex(text2, options);
			CONDITION_PAT = new Regex(text, options);
			SPECIFICATION_PAT = new Regex(text3, options);
			FORMAT_PAT = new Regex(pattern, options);
			COLOR_GROUP = FindGroup(FORMAT_PAT, "[Blue]@", "Blue");
			CONDITION_OPERATOR_GROUP = FindGroup(FORMAT_PAT, "[>=1]@", ">=");
			CONDITION_VALUE_GROUP = FindGroup(FORMAT_PAT, "[>=1]@", "1");
			SPECIFICATION_GROUP = FindGroup(FORMAT_PAT, "[Blue][>1]\\a ?", "\\a ?");
		}

		/// Create an object to represent a format part.
		///
		/// @param desc The string to Parse.
		public CellFormatPart(string desc)
		{
			Match match = FORMAT_PAT.Match(desc);
			if (!match.Success)
			{
				throw new ArgumentException("Unrecognized format: \"" + desc + "\"");
			}
			color = GetColor(match);
			condition = GetCondition(match);
			type = GetCellFormatType(match);
			format = GetFormatter(match);
		}

		/// Returns <tt>true</tt> if this format part applies to the given value. If
		/// the value is a number and this is part has a condition, returns
		/// <tt>true</tt> only if the number passes the condition.  Otherwise, this
		/// allways return <tt>true</tt>.
		///
		/// @param valueObject The value to Evaluate.
		///
		/// @return <tt>true</tt> if this format part applies to the given value.
		public bool Applies(object valueObject)
		{
			if (condition == null || !valueObject.GetType().IsPrimitive)
			{
				if (valueObject == null)
				{
					throw new NullReferenceException("valueObject");
				}
				return true;
			}
			double value = (double)valueObject;
			return condition.Pass(value);
		}

		/// Returns the number of the first group that is the same as the marker
		/// string.  The search starts with group 1.
		///
		/// @param pat    The pattern to use.
		/// @param str    The string to match against the pattern.
		/// @param marker The marker value to find the group of.
		///
		/// @return The matching group number.
		///
		/// @throws ArgumentException No group matches the marker.
		private static int FindGroup(Regex pat, string str, string marker)
		{
			Match match = pat.Match(str);
			if (!match.Success)
			{
				throw new ArgumentException("Pattern \"" + pat.ToString() + "\" doesn't match \"" + str + "\"");
			}
			for (int i = 1; i <= match.Groups.Count; i++)
			{
				string value = match.Groups[i].Value;
				if (value != null && value.Equals(marker))
				{
					return i;
				}
			}
			throw new ArgumentException("\"" + marker + "\" not found in \"" + pat.ToString() + "\"");
		}

		/// Returns the color specification from the matcher, or <tt>null</tt> if
		/// there is none.
		///
		/// @param m The matcher for the format part.
		///
		/// @return The color specification or <tt>null</tt>.
		private static Color GetColor(Match m)
		{
			string text = m.Groups[COLOR_GROUP].Value.ToUpper();
			if (text == null || text.Length == 0)
			{
				return Color.Empty;
			}
			Color result = Color.Empty;
			if (NAMED_COLORS.ContainsKey(text))
			{
				result = NAMED_COLORS[text];
			}
			return result;
		}

		/// Returns the condition specification from the matcher, or <tt>null</tt> if
		/// there is none.
		///
		/// @param m The matcher for the format part.
		///
		/// @return The condition specification or <tt>null</tt>.
		private CellFormatCondition GetCondition(Match m)
		{
			string value = m.Groups[CONDITION_OPERATOR_GROUP].Value;
			if (value == null || value.Length == 0)
			{
				return null;
			}
			return CellFormatCondition.GetInstance(m.Groups[CONDITION_OPERATOR_GROUP].Value, m.Groups[CONDITION_VALUE_GROUP].Value);
		}

		/// Returns the CellFormatType object implied by the format specification for
		/// the format part.
		///
		/// @param matcher The matcher for the format part.
		///
		/// @return The CellFormatType.
		private CellFormatType GetCellFormatType(Match matcher)
		{
			string value = matcher.Groups[SPECIFICATION_GROUP].Value;
			return formatType(value);
		}

		/// Returns the formatter object implied by the format specification for the
		/// format part.
		///
		/// @param matcher The matcher for the format part.
		///
		/// @return The formatter.
		private CellFormatter GetFormatter(Match matcher)
		{
			string value = matcher.Groups[SPECIFICATION_GROUP].Value;
			return type.Formatter(value);
		}

		/// Returns the type of format.
		///
		/// @param fdesc The format specification
		///
		/// @return The type of format.
		private CellFormatType formatType(string fdesc)
		{
			fdesc = fdesc.Trim();
			if (fdesc.Equals("") || fdesc.Equals("General", StringComparison.InvariantCultureIgnoreCase))
			{
				return CellFormatType.GENERAL;
			}
			MatchCollection matchCollection = SPECIFICATION_PAT.Matches(fdesc);
			bool flag = false;
			bool flag2 = false;
			foreach (Match item in matchCollection)
			{
				string value = item.Groups[0].Value;
				if (value.Length > 0)
				{
					switch (value[0])
					{
					case '@':
						return CellFormatType.TEXT;
					case 'D':
					case 'Y':
					case 'd':
					case 'y':
						return CellFormatType.DATE;
					case 'H':
					case 'M':
					case 'S':
					case 'h':
					case 'm':
					case 's':
						flag = true;
						break;
					case '0':
						flag2 = true;
						break;
					case '[':
						return CellFormatType.ELAPSED;
					case '#':
					case '?':
						return CellFormatType.NUMBER;
					}
				}
			}
			if (flag)
			{
				return CellFormatType.DATE;
			}
			if (flag2)
			{
				return CellFormatType.NUMBER;
			}
			return CellFormatType.TEXT;
		}

		/// Returns a version of the original string that has any special characters
		/// quoted (or escaped) as appropriate for the cell format type.  The format
		/// type object is queried to see what is special.
		///
		/// @param repl The original string.
		/// @param type The format type representation object.
		///
		/// @return A version of the string with any special characters Replaced.
		///
		/// @see CellFormatType#isSpecial(char)
		private static string QuoteSpecial(string repl, CellFormatType type)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in repl)
			{
				if (c == '\'' && type.IsSpecial('\''))
				{
					stringBuilder.Append('\0');
				}
				else
				{
					bool flag = type.IsSpecial(c);
					if (flag)
					{
						stringBuilder.Append("'");
					}
					stringBuilder.Append(c);
					if (flag)
					{
						stringBuilder.Append("'");
					}
				}
			}
			return stringBuilder.ToString();
		}

		/// Apply this format part to the given value.  This returns a {@link
		/// CellFormatResult} object with the results.
		///
		/// @param value The value to apply this format part to.
		///
		/// @return A {@link CellFormatResult} object Containing the results of
		///         Applying the format to the value.
		public CellFormatResult Apply(object value)
		{
			bool flag = Applies(value);
			string text;
			Color empty;
			if (flag)
			{
				text = format.Format(value);
				empty = color;
			}
			else
			{
				text = format.SimpleFormat(value);
				empty = Color.Empty;
			}
			return new CellFormatResult(flag, text, empty);
		}

		/// Apply this format part to the given value, Applying the result to the
		/// given label.
		///
		/// @param label The label
		/// @param value The value to apply this format part to.
		///
		/// @return <tt>true</tt> if the
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

		public static StringBuilder ParseFormat(string fdesc, CellFormatType type, IPartHandler partHandler)
		{
			MatchCollection matchCollection = SPECIFICATION_PAT.Matches(fdesc);
			StringBuilder stringBuilder = new StringBuilder();
			Match match = null;
			foreach (Match item in matchCollection)
			{
				string text = Group(item, 0);
				if (text.Length > 0)
				{
					string text2 = partHandler.HandlePart(item, text, type, stringBuilder);
					if (text2 == null)
					{
						switch (text[0])
						{
						case '"':
							text2 = QuoteSpecial(text.Substring(1, text.Length - 2), type);
							break;
						case '\\':
							text2 = QuoteSpecial(text.Substring(1), type);
							break;
						case '_':
							text2 = " ";
							break;
						case '*':
							text2 = ExpandChar(text);
							break;
						default:
							text2 = text;
							break;
						}
					}
					stringBuilder.Append(text.Replace(item.Captures[0].Value, text2));
					if (item.NextMatch().Index - (item.Index + text.Length) > 0)
					{
						stringBuilder.Append(fdesc.Substring(item.Index + text.Length, item.NextMatch().Index - (item.Index + text.Length)));
					}
					match = item;
				}
			}
			if (match != null)
			{
				stringBuilder.Append(fdesc.Substring(match.Index + match.Groups[0].Value.Length));
			}
			if (type.IsSpecial('\''))
			{
				int startIndex = 0;
				while ((startIndex = stringBuilder.ToString().IndexOf("''", startIndex)) >= 0)
				{
					stringBuilder.Remove(startIndex, 2);
				}
				startIndex = 0;
				while ((startIndex = stringBuilder.ToString().IndexOf("\0", startIndex)) >= 0)
				{
					stringBuilder.Remove(startIndex, 1);
					stringBuilder.Insert(startIndex, "''");
				}
			}
			return stringBuilder;
		}

		public static string QuoteReplacement(string s)
		{
			if (s.IndexOf('\\') == -1 && s.IndexOf('$') == -1)
			{
				return s;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in s)
			{
				if (c == '\\' || c == '$')
				{
					stringBuilder.Append('\\');
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		/// Expands a character. This is only partly done, because we don't have the
		/// correct info.  In Excel, this would be expanded to fill the rest of the
		/// cell, but we don't know, in general, what the "rest of the cell" is1.
		///
		/// @param part The character to be repeated is the second character in this
		///             string.
		///
		/// @return The character repeated three times.
		internal static string ExpandChar(string part)
		{
			char c = part[1];
			return string.Concat(c, c, c);
		}

		/// Returns the string from the group, or <tt>""</tt> if the group is
		/// <tt>null</tt>.
		///
		/// @param m The matcher.
		/// @param g The group number.
		///
		/// @return The group or <tt>""</tt>.
		public static string Group(Match m, int g)
		{
			string value = m.Groups[g].Value;
			if (value != null)
			{
				return value;
			}
			return "";
		}

		public override string ToString()
		{
			return format.ToString();
		}
	}
}
