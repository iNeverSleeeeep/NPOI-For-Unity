using NPOI.SS.Formula.Eval;
using System.Text;

namespace NPOI.SS.Formula.Functions
{
	/// Implementation for Excel function INDIRECT<p />
	///
	/// INDIRECT() returns the cell or area reference denoted by the text argument.<p /> 
	///
	/// <b>Syntax</b>:<br />
	/// <b>INDIRECT</b>(<b>ref_text</b>,isA1Style)<p />
	///
	/// <b>ref_text</b> a string representation of the desired reference as it would normally be written
	/// in a cell formula.<br />
	/// <b>isA1Style</b> (default TRUE) specifies whether the ref_text should be interpreted as A1-style
	/// or R1C1-style.
	///
	///
	/// @author Josh Micich
	public class Indirect : FreeRefFunction
	{
		public static FreeRefFunction instance = new Indirect();

		private Indirect()
		{
		}

		public ValueEval Evaluate(ValueEval[] args, OperationEvaluationContext ec)
		{
			if (args.Length < 1)
			{
				return ErrorEval.VALUE_INVALID;
			}
			string text;
			bool isA1style;
			try
			{
				ValueEval singleValue = OperandResolver.GetSingleValue(args[0], ec.RowIndex, ec.ColumnIndex);
				text = OperandResolver.CoerceValueToString(singleValue);
				switch (args.Length)
				{
				case 1:
					isA1style = true;
					break;
				case 2:
					isA1style = EvaluateBooleanArg(args[1], ec);
					break;
				default:
					return ErrorEval.VALUE_INVALID;
				}
			}
			catch (EvaluationException ex)
			{
				return ex.GetErrorEval();
			}
			return EvaluateIndirect(ec, text, isA1style);
		}

		private static bool EvaluateBooleanArg(ValueEval arg, OperationEvaluationContext ec)
		{
			ValueEval singleValue = OperandResolver.GetSingleValue(arg, ec.RowIndex, ec.ColumnIndex);
			if (singleValue == BlankEval.instance || singleValue == MissingArgEval.instance)
			{
				return false;
			}
			return OperandResolver.CoerceValueToBoolean(singleValue, stringsAreBlanks: false).Value;
		}

		private static ValueEval EvaluateIndirect(OperationEvaluationContext ec, string text, bool isA1style)
		{
			int num = text.LastIndexOf('!');
			string workbookName;
			string sheetName;
			string text2;
			if (num < 0)
			{
				workbookName = null;
				sheetName = null;
				text2 = text;
			}
			else
			{
				string[] array = ParseWorkbookAndSheetName(text.Substring(0, num));
				if (array == null)
				{
					return ErrorEval.REF_INVALID;
				}
				workbookName = array[0];
				sheetName = array[1];
				text2 = text.Substring(num + 1);
			}
			int num2 = text2.IndexOf(':');
			string refStrPart;
			string refStrPart2;
			if (num2 < 0)
			{
				refStrPart = text2.Trim();
				refStrPart2 = null;
			}
			else
			{
				refStrPart = text2.Substring(0, num2).Trim();
				refStrPart2 = text2.Substring(num2 + 1).Trim();
			}
			return ec.GetDynamicReference(workbookName, sheetName, refStrPart, refStrPart2, isA1style);
		}

		/// @return array of length 2: {workbookName, sheetName,}.  Second element will always be
		/// present.  First element may be null if sheetName is unqualified.
		/// Returns <code>null</code> if text cannot be parsed.
		private static string[] ParseWorkbookAndSheetName(string text)
		{
			int num = text.Length - 1;
			if (num < 0)
			{
				return null;
			}
			if (CanTrim(text))
			{
				return null;
			}
			char c = text[0];
			if (!char.IsWhiteSpace(c))
			{
				switch (c)
				{
				case '\'':
				{
					if (text[num] != '\'')
					{
						return null;
					}
					c = text[1];
					if (char.IsWhiteSpace(c))
					{
						return null;
					}
					string text4;
					int num4;
					if (c == '[')
					{
						int num3 = text.ToString().LastIndexOf(']');
						if (num3 < 0)
						{
							return null;
						}
						text4 = UnescapeString(text.Substring(2, num3 - 2));
						if (text4 == null || CanTrim(text4))
						{
							return null;
						}
						num4 = num3 + 1;
					}
					else
					{
						text4 = null;
						num4 = 1;
					}
					string text5 = UnescapeString(text.Substring(num4, num - num4));
					if (text5 == null)
					{
						return null;
					}
					return new string[2]
					{
						text4,
						text5
					};
				}
				case '[':
				{
					int num2 = text.ToString().LastIndexOf(']');
					if (num2 < 0)
					{
						return null;
					}
					string text2 = text.Substring(1, num2 - 1);
					if (CanTrim(text2))
					{
						return null;
					}
					string text3 = text.Substring(num2 + 1);
					if (CanTrim(text3))
					{
						return null;
					}
					return new string[2]
					{
						text2.ToString(),
						text3.ToString()
					};
				}
				default:
					return new string[2]
					{
						null,
						text.ToString()
					};
				}
			}
			return null;
		}

		/// @return <code>null</code> if there is a syntax error in any escape sequence
		/// (the typical syntax error is a single quote character not followed by another).
		private static string UnescapeString(string text)
		{
			int length = text.Length;
			StringBuilder stringBuilder = new StringBuilder(length);
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				if (c == '\'')
				{
					i++;
					if (i >= length)
					{
						return null;
					}
					c = text[i];
					if (c != '\'')
					{
						return null;
					}
				}
				stringBuilder.Append(c);
			}
			return stringBuilder.ToString();
		}

		private static bool CanTrim(string text)
		{
			int num = text.Length - 1;
			if (num < 0)
			{
				return false;
			}
			if (char.IsWhiteSpace(text[0]))
			{
				return true;
			}
			if (char.IsWhiteSpace(text[num]))
			{
				return true;
			}
			return false;
		}
	}
}
