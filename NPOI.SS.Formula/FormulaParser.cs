using NPOI.SS.Formula.Constant;
using NPOI.SS.Formula.Function;
using NPOI.SS.Formula.PTG;
using NPOI.SS.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace NPOI.SS.Formula
{
	public class FormulaParser
	{
		private class Identifier
		{
			private string _name;

			private bool _isQuoted;

			public string Name => _name;

			public bool IsQuoted => _isQuoted;

			public Identifier(string name, bool IsQuoted)
			{
				_name = name;
				_isQuoted = IsQuoted;
			}

			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder(64);
				stringBuilder.Append(GetType().Name);
				stringBuilder.Append(" [");
				if (_isQuoted)
				{
					stringBuilder.Append("'").Append(_name).Append("'");
				}
				else
				{
					stringBuilder.Append(_name);
				}
				stringBuilder.Append("]");
				return stringBuilder.ToString();
			}
		}

		private class SheetIdentifier
		{
			private string _bookName;

			private Identifier _sheetIdentifier;

			public string BookName => _bookName;

			public Identifier SheetID => _sheetIdentifier;

			public SheetIdentifier(string bookName, Identifier sheetIdentifier)
			{
				_bookName = bookName;
				_sheetIdentifier = sheetIdentifier;
			}

			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder(64);
				stringBuilder.Append(GetType().Name);
				stringBuilder.Append(" [");
				if (_bookName != null)
				{
					stringBuilder.Append(" [").Append(_sheetIdentifier.Name).Append("]");
				}
				if (_sheetIdentifier.IsQuoted)
				{
					stringBuilder.Append("'").Append(_sheetIdentifier.Name).Append("'");
				}
				else
				{
					stringBuilder.Append(_sheetIdentifier.Name);
				}
				stringBuilder.Append("]");
				return stringBuilder.ToString();
			}
		}

		/// A1, $A1, A$1, $A$1, A, 1
		private class SimpleRangePart
		{
			public enum PartType
			{
				Cell,
				Row,
				Column
			}

			private PartType _type;

			private string _rep;

			public bool IsCell => _type == PartType.Cell;

			public bool IsRowOrColumn => _type != PartType.Cell;

			public bool IsColumn => _type == PartType.Column;

			public bool IsRow => _type == PartType.Row;

			public string Rep => _rep;

			public static PartType Get(bool hasLetters, bool hasDigits)
			{
				if (hasLetters)
				{
					if (!hasDigits)
					{
						return PartType.Column;
					}
					return PartType.Cell;
				}
				if (!hasDigits)
				{
					throw new ArgumentException("must have either letters or numbers");
				}
				return PartType.Row;
			}

			public SimpleRangePart(string rep, bool hasLetters, bool hasNumbers)
			{
				_rep = rep;
				_type = Get(hasLetters, hasNumbers);
			}

			public CellReference getCellReference()
			{
				if (_type != 0)
				{
					throw new InvalidOperationException("Not applicable to this type");
				}
				return new CellReference(_rep);
			}

			/// @return <c>true</c> if the two range parts can be combined in an
			/// {@link AreaPtg} ( Note - the explicit range operator (:) may still be valid
			/// when this method returns <c>false</c> )
			public bool IsCompatibleForArea(SimpleRangePart part2)
			{
				return _type == part2._type;
			}

			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder(64);
				stringBuilder.Append(GetType().Name).Append(" [");
				stringBuilder.Append(_rep);
				stringBuilder.Append("]");
				return stringBuilder.ToString();
			}
		}

		private const char TAB = '\t';

		private const char CR = '\r';

		private const char LF = '\n';

		private string formulaString;

		private int formulaLength;

		private int pointer;

		private static SpreadsheetVersion _ssVersion;

		private ParseNode _rootNode;

		/// Lookahead Character.
		/// Gets value '\0' when the input string is exhausted
		private char look;

		private IFormulaParsingWorkbook _book;

		private int _sheetIndex;

		private string CELL_REF_PATTERN = "(\\$?[A-Za-z]+)?(\\$?[0-9]+)?";

		/// Create the formula Parser, with the string that is To be
		///  Parsed against the supplied workbook.
		/// A later call the Parse() method To return ptg list in
		///  rpn order, then call the GetRPNPtg() To retrive the
		///  Parse results.
		/// This class is recommended only for single threaded use.
		///
		/// If you only have a usermodel.HSSFWorkbook, and not a
		///  model.Workbook, then use the convenience method on
		///  usermodel.HSSFFormulaEvaluator
		public FormulaParser(string formula, IFormulaParsingWorkbook book, int sheetIndex)
		{
			formulaString = formula;
			pointer = 0;
			_book = book;
			_ssVersion = ((book == null) ? SpreadsheetVersion.EXCEL97 : book.GetSpreadsheetVersion());
			formulaLength = formulaString.Length;
			_sheetIndex = sheetIndex;
		}

		public static Ptg[] Parse(string formula, IFormulaParsingWorkbook book)
		{
			return Parse(formula, book, FormulaType.Cell);
		}

		/// Parse a formula into a array of tokens
		///
		/// @param formula	 the formula to parse
		/// @param workbook	the parent workbook
		/// @param formulaType the type of the formula, see {@link FormulaType}
		/// @param sheetIndex  the 0-based index of the sheet this formula belongs to.
		/// The sheet index is required to resolve sheet-level names. <code>-1</code> means that
		/// the scope of the name will be ignored and  the parser will match names only by name
		///
		/// @return array of parsed tokens
		/// @throws FormulaParseException if the formula is unparsable
		public static Ptg[] Parse(string formula, IFormulaParsingWorkbook workbook, FormulaType formulaType, int sheetIndex)
		{
			FormulaParser formulaParser = new FormulaParser(formula, workbook, sheetIndex);
			formulaParser.Parse();
			return formulaParser.GetRPNPtg(formulaType);
		}

		public static Ptg[] Parse(string formula, IFormulaParsingWorkbook workbook, FormulaType formulaType)
		{
			return Parse(formula, workbook, formulaType, -1);
		}

		/// Read New Character From Input Stream 
		private void GetChar()
		{
			if (pointer > formulaLength)
			{
				throw new Exception("too far");
			}
			if (pointer < formulaLength)
			{
				look = formulaString[pointer];
			}
			else
			{
				look = '\0';
			}
			pointer++;
		}

		/// Report What Was Expected 
		private Exception expected(string s)
		{
			string msg = (look != '=' || formulaString.Substring(0, pointer - 1).Trim().Length >= 1) ? ("Parse error near char " + (pointer - 1) + " '" + look + "' in specified formula '" + formulaString + "'. Expected " + s) : ("The specified formula '" + formulaString + "' starts with an equals sign which is not allowed.");
			return new FormulaParseException(msg);
		}

		/// Recognize an Alpha Character 
		private static bool IsAlpha(char c)
		{
			if (!char.IsLetter(c) && c != '$')
			{
				return c == '_';
			}
			return true;
		}

		/// Recognize a Decimal Digit 
		private static bool IsDigit(char c)
		{
			return char.IsDigit(c);
		}

		/// Recognize an Alphanumeric 
		private static bool IsAlNum(char c)
		{
			if (!IsAlpha(c))
			{
				return IsDigit(c);
			}
			return true;
		}

		/// Recognize White Space 
		private static bool IsWhite(char c)
		{
			if (c != ' ' && c != '\t' && c != '\r')
			{
				return c == '\n';
			}
			return true;
		}

		/// Skip Over Leading White Space 
		private void SkipWhite()
		{
			while (IsWhite(look))
			{
				GetChar();
			}
		}

		/// Consumes the next input character if it is equal To the one specified otherwise throws an
		/// unchecked exception. This method does <b>not</b> consume whitespace (before or after the
		/// matched character).
		private void Match(char x)
		{
			if (look != x)
			{
				throw expected("'" + x + "'");
			}
			GetChar();
		}

		private string ParseUnquotedIdentifier()
		{
			if (look == '\'')
			{
				throw expected("unquoted identifier");
			}
			StringBuilder stringBuilder = new StringBuilder();
			while (char.IsLetterOrDigit(look) || look == '.')
			{
				stringBuilder.Append(look);
				GetChar();
			}
			if (stringBuilder.Length < 1)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		/// Get a Number 
		private string GetNum()
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (IsDigit(look))
			{
				stringBuilder.Append(look);
				GetChar();
			}
			if (stringBuilder.Length != 0)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		private ParseNode ParseRangeExpression()
		{
			ParseNode parseNode = ParseRangeable();
			bool flag = false;
			while (look == ':')
			{
				int currentParsePosition = pointer;
				GetChar();
				ParseNode parseNode2 = ParseRangeable();
				CheckValidRangeOperand("LHS", currentParsePosition, parseNode);
				CheckValidRangeOperand("RHS", currentParsePosition, parseNode2);
				ParseNode[] children = new ParseNode[2]
				{
					parseNode,
					parseNode2
				};
				parseNode = new ParseNode(RangePtg.instance, children);
				flag = true;
			}
			if (flag)
			{
				return AugmentWithMemPtg(parseNode);
			}
			return parseNode;
		}

		private static ParseNode AugmentWithMemPtg(ParseNode root)
		{
			Ptg token = (!NeedsMemFunc(root)) ? ((OperandPtg)new MemAreaPtg(root.EncodedSize)) : ((OperandPtg)new MemFuncPtg(root.EncodedSize));
			return new ParseNode(token, root);
		}

		/// From OOO doc: "Whenever one operand of the reference subexpression is a function,
		///  a defined name, a 3D reference, or an external reference (and no error occurs),
		///  a tMemFunc token is used"
		private static bool NeedsMemFunc(ParseNode root)
		{
			Ptg token = root.GetToken();
			if (token is AbstractFunctionPtg)
			{
				return true;
			}
			if (token is IExternSheetReferenceToken)
			{
				return true;
			}
			if (token is NamePtg || token is NameXPtg)
			{
				return true;
			}
			if (token is OperationPtg || token is ParenthesisPtg)
			{
				ParseNode[] children = root.GetChildren();
				foreach (ParseNode root2 in children)
				{
					if (NeedsMemFunc(root2))
					{
						return true;
					}
				}
				return false;
			}
			if (token is OperandPtg)
			{
				return false;
			}
			if (token is OperationPtg)
			{
				return true;
			}
			return false;
		}

		/// @return <c>true</c> if the specified character may be used in a defined name
		private static bool IsValidDefinedNameChar(char ch)
		{
			if (!char.IsLetterOrDigit(ch))
			{
				switch (ch)
				{
				case '.':
				case '?':
				case '\\':
				case '_':
					return true;
				default:
					return false;
				}
			}
			return true;
		}

		/// @param currentParsePosition used to format a potential error message
		private void CheckValidRangeOperand(string sideName, int currentParsePosition, ParseNode pn)
		{
			if (!IsValidRangeOperand(pn))
			{
				throw new FormulaParseException("The " + sideName + " of the range operator ':' at position " + currentParsePosition + " is not a proper reference.");
			}
		}

		/// @return false if sub-expression represented the specified ParseNode definitely
		/// cannot appear on either side of the range (':') operator
		private bool IsValidRangeOperand(ParseNode a)
		{
			Ptg token = a.GetToken();
			if (token is OperandPtg)
			{
				return true;
			}
			if (token is AbstractFunctionPtg)
			{
				AbstractFunctionPtg abstractFunctionPtg = (AbstractFunctionPtg)token;
				byte defaultOperandClass = abstractFunctionPtg.DefaultOperandClass;
				return 0 == defaultOperandClass;
			}
			if (token is ValueOperatorPtg)
			{
				return false;
			}
			if (token is OperationPtg)
			{
				return true;
			}
			if (token is ParenthesisPtg)
			{
				return IsValidRangeOperand(a.GetChildren()[0]);
			}
			if (token == ErrPtg.REF_INVALID)
			{
				return true;
			}
			return false;
		}

		/// Parses area refs (things which could be the operand of ':') and simple factors
		/// Examples
		/// <pre>
		///   A$1
		///   $A$1 :  $B1
		///   A1 .......	C2
		///   Sheet1 !$A1
		///   a..b!A1
		///   'my sheet'!A1
		///   .my.sheet!A1
		///   my.named..range.
		///   foo.bar(123.456, "abc")
		///   123.456
		///   "abc"
		///   true
		/// </pre>
		private ParseNode ParseRangeable()
		{
			SkipWhite();
			int num = pointer;
			SheetIdentifier sheetIdentifier = ParseSheetName();
			if (sheetIdentifier == null)
			{
				ResetPointer(num);
			}
			else
			{
				SkipWhite();
				num = pointer;
			}
			SimpleRangePart simpleRangePart = ParseSimpleRangePart();
			if (simpleRangePart == null)
			{
				if (sheetIdentifier != null)
				{
					if (look == '#')
					{
						return new ParseNode(ErrPtg.ValueOf(ParseErrorLiteral()));
					}
					throw new FormulaParseException("Cell reference expected after sheet name at index " + pointer + ".");
				}
				return ParseNonRange(num);
			}
			bool flag = IsWhite(look);
			if (flag)
			{
				SkipWhite();
			}
			if (look == ':')
			{
				int ptr = pointer;
				GetChar();
				SkipWhite();
				SimpleRangePart simpleRangePart2 = ParseSimpleRangePart();
				if (simpleRangePart2 != null && !simpleRangePart.IsCompatibleForArea(simpleRangePart2))
				{
					simpleRangePart2 = null;
				}
				if (simpleRangePart2 == null)
				{
					ResetPointer(ptr);
					if (!simpleRangePart.IsCell)
					{
						string str = (sheetIdentifier != null) ? ("'" + sheetIdentifier.SheetID.Name + '!') : "";
						throw new FormulaParseException(str + simpleRangePart.Rep + "' is not a proper reference.");
					}
					return CreateAreaRefParseNode(sheetIdentifier, simpleRangePart, simpleRangePart2);
				}
				return CreateAreaRefParseNode(sheetIdentifier, simpleRangePart, simpleRangePart2);
			}
			if (look == '.')
			{
				GetChar();
				int num2 = 1;
				while (look == '.')
				{
					num2++;
					GetChar();
				}
				bool flag2 = IsWhite(look);
				SkipWhite();
				SimpleRangePart simpleRangePart3 = ParseSimpleRangePart();
				string str2 = formulaString.Substring(num - 1, pointer - num);
				if (simpleRangePart3 == null)
				{
					if (sheetIdentifier != null)
					{
						throw new FormulaParseException("Complete area reference expected after sheet name at index " + pointer + ".");
					}
					return ParseNonRange(num);
				}
				if (flag || flag2)
				{
					if (simpleRangePart.IsRowOrColumn || simpleRangePart3.IsRowOrColumn)
					{
						throw new FormulaParseException("Dotted range (full row or column) expression '" + str2 + "' must not contain whitespace.");
					}
					return CreateAreaRefParseNode(sheetIdentifier, simpleRangePart, simpleRangePart3);
				}
				if (num2 == 1 && simpleRangePart.IsRow && simpleRangePart3.IsRow)
				{
					return ParseNonRange(num);
				}
				if ((simpleRangePart.IsRowOrColumn || simpleRangePart3.IsRowOrColumn) && num2 != 2)
				{
					throw new FormulaParseException("Dotted range (full row or column) expression '" + str2 + "' must have exactly 2 dots.");
				}
				return CreateAreaRefParseNode(sheetIdentifier, simpleRangePart, simpleRangePart3);
			}
			if (simpleRangePart.IsCell && IsValidCellReference(simpleRangePart.Rep))
			{
				return CreateAreaRefParseNode(sheetIdentifier, simpleRangePart, null);
			}
			if (sheetIdentifier != null)
			{
				throw new FormulaParseException("Second part of cell reference expected after sheet name at index " + pointer + ".");
			}
			return ParseNonRange(num);
		}

		/// Parses simple factors that are not primitive ranges or range components
		/// i.e. '!', ':'(and equiv '...') do not appear
		/// Examples
		/// <pre>
		///   my.named...range.
		///   foo.bar(123.456, "abc")
		///   123.456
		///   "abc"
		///   true
		/// </pre>
		private ParseNode ParseNonRange(int savePointer)
		{
			ResetPointer(savePointer);
			if (char.IsDigit(look))
			{
				return new ParseNode(ParseNumber());
			}
			if (look == '"')
			{
				return new ParseNode(new StringPtg(ParseStringLiteral()));
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (!char.IsLetter(look) && look != '_')
			{
				throw expected("number, string, or defined name");
			}
			while (IsValidDefinedNameChar(look))
			{
				stringBuilder.Append(look);
				GetChar();
			}
			SkipWhite();
			string text = stringBuilder.ToString();
			if (look == '(')
			{
				return Function(text);
			}
			if (text.Equals("TRUE", StringComparison.OrdinalIgnoreCase) || text.Equals("FALSE", StringComparison.OrdinalIgnoreCase))
			{
				return new ParseNode(new BoolPtg(text.ToUpper()));
			}
			if (_book == null)
			{
				throw new InvalidOperationException("Need book to evaluate name '" + text + "'");
			}
			IEvaluationName name = _book.GetName(text, _sheetIndex);
			if (name == null)
			{
				throw new FormulaParseException("Specified named range '" + text + "' does not exist in the current workbook.");
			}
			if (name.IsRange)
			{
				return new ParseNode(name.CreatePtg());
			}
			throw new FormulaParseException("Specified name '" + text + "' is not a range as expected.");
		}

		/// @param sheetIden may be <code>null</code>
		/// @param part1
		/// @param part2 may be <code>null</code>
		private ParseNode CreateAreaRefParseNode(SheetIdentifier sheetIden, SimpleRangePart part1, SimpleRangePart part2)
		{
			int externIdx;
			if (sheetIden == null)
			{
				externIdx = -2147483648;
			}
			else
			{
				string name = sheetIden.SheetID.Name;
				externIdx = ((sheetIden.BookName != null) ? _book.GetExternalSheetIndex(sheetIden.BookName, name) : _book.GetExternalSheetIndex(name));
			}
			Ptg token;
			if (part2 == null)
			{
				CellReference cellReference = part1.getCellReference();
				token = ((sheetIden != null) ? ((RefPtgBase)new Ref3DPtg(cellReference, externIdx)) : ((RefPtgBase)new RefPtg(cellReference)));
			}
			else
			{
				AreaReference areaReference = CreateAreaRef(part1, part2);
				token = ((sheetIden != null) ? ((AreaPtgBase)new Area3DPtg(areaReference, externIdx)) : ((AreaPtgBase)new AreaPtg(areaReference)));
			}
			return new ParseNode(token);
		}

		private static AreaReference CreateAreaRef(SimpleRangePart part1, SimpleRangePart part2)
		{
			if (!part1.IsCompatibleForArea(part2))
			{
				throw new FormulaParseException("has incompatible parts: '" + part1.Rep + "' and '" + part2.Rep + "'.");
			}
			if (part1.IsRow)
			{
				return AreaReference.GetWholeRow(part1.Rep, part2.Rep);
			}
			if (part1.IsColumn)
			{
				return AreaReference.GetWholeColumn(part1.Rep, part2.Rep);
			}
			return new AreaReference(part1.getCellReference(), part2.getCellReference());
		}

		/// Parses out a potential LHS or RHS of a ':' intended to produce a plain AreaRef.  Normally these are
		/// proper cell references but they could also be row or column refs like "$AC" or "10"
		/// @return <code>null</code> (and leaves {@link #_pointer} unchanged if a proper range part does not parse out
		private SimpleRangePart ParseSimpleRangePart()
		{
			int i = pointer - 1;
			bool flag = false;
			bool flag2 = false;
			for (; i < formulaLength; i++)
			{
				char c = formulaString[i];
				if (char.IsDigit(c))
				{
					flag = true;
				}
				else if (char.IsLetter(c))
				{
					flag2 = true;
				}
				else if (c != '$' && c != '_')
				{
					break;
				}
			}
			if (i <= pointer - 1)
			{
				return null;
			}
			string text = formulaString.Substring(pointer - 1, i - pointer + 1);
			Regex regex = new Regex(CELL_REF_PATTERN);
			if (!regex.IsMatch(text))
			{
				return null;
			}
			if (flag2 && flag)
			{
				if (!IsValidCellReference(text))
				{
					return null;
				}
			}
			else if (flag2)
			{
				if (!CellReference.IsColumnWithnRange(text.Replace("$", ""), _ssVersion))
				{
					return null;
				}
			}
			else
			{
				if (!flag)
				{
					return null;
				}
				int num;
				try
				{
					num = int.Parse(text.Replace("$", ""), CultureInfo.InvariantCulture);
				}
				catch (Exception)
				{
					return null;
				}
				if (num < 1 || num > 65536)
				{
					return null;
				}
			}
			ResetPointer(i + 1);
			return new SimpleRangePart(text, flag2, flag);
		}

		/// "A1", "B3" -&gt; "A1:B3"   
		/// "sheet1!A1", "B3" -&gt; "sheet1!A1:B3"
		///
		/// @return <c>null</c> if the range expression cannot / shouldn't be reduced.
		private static Ptg ReduceRangeExpression(Ptg ptgA, Ptg ptgB)
		{
			if (!(ptgB is RefPtg))
			{
				return null;
			}
			RefPtg refPtg = (RefPtg)ptgB;
			if (ptgA is RefPtg)
			{
				RefPtg refPtg2 = (RefPtg)ptgA;
				return new AreaPtg(refPtg2.Row, refPtg.Row, refPtg2.Column, refPtg.Column, refPtg2.IsRowRelative, refPtg.IsRowRelative, refPtg2.IsColRelative, refPtg.IsColRelative);
			}
			if (ptgA is Ref3DPtg)
			{
				Ref3DPtg ref3DPtg = (Ref3DPtg)ptgA;
				return new Area3DPtg(ref3DPtg.Row, refPtg.Row, ref3DPtg.Column, refPtg.Column, ref3DPtg.IsRowRelative, refPtg.IsRowRelative, ref3DPtg.IsColRelative, refPtg.IsColRelative, ref3DPtg.ExternSheetIndex);
			}
			return null;
		}

		/// Note - caller should reset {@link #_pointer} upon <code>null</code> result
		/// @return The sheet name as an identifier <code>null</code> if '!' is not found in the right place
		private SheetIdentifier ParseSheetName()
		{
			string bookName;
			if (look == '[')
			{
				StringBuilder stringBuilder = new StringBuilder();
				GetChar();
				while (look != ']')
				{
					stringBuilder.Append(look);
					GetChar();
				}
				GetChar();
				bookName = stringBuilder.ToString();
			}
			else
			{
				bookName = null;
			}
			if (look == '\'')
			{
				StringBuilder stringBuilder2 = new StringBuilder();
				Match('\'');
				bool flag = look == '\'';
				while (!flag)
				{
					stringBuilder2.Append(look);
					GetChar();
					if (look == '\'')
					{
						Match('\'');
						flag = (look != '\'');
					}
				}
				Identifier sheetIdentifier = new Identifier(stringBuilder2.ToString(), IsQuoted: true);
				SkipWhite();
				if (look == '!')
				{
					GetChar();
					return new SheetIdentifier(bookName, sheetIdentifier);
				}
				return null;
			}
			if (look == '_' || char.IsLetter(look))
			{
				StringBuilder stringBuilder3 = new StringBuilder();
				while (IsUnquotedSheetNameChar(look))
				{
					stringBuilder3.Append(look);
					GetChar();
				}
				SkipWhite();
				if (look == '!')
				{
					GetChar();
					return new SheetIdentifier(bookName, new Identifier(stringBuilder3.ToString(), IsQuoted: false));
				}
				return null;
			}
			return null;
		}

		/// very similar to {@link SheetNameFormatter#isSpecialChar(char)}
		private bool IsUnquotedSheetNameChar(char ch)
		{
			if (char.IsLetterOrDigit(ch))
			{
				return true;
			}
			char c = ch;
			if (c == '.' || c == '_')
			{
				return true;
			}
			return false;
		}

		private void ResetPointer(int ptr)
		{
			pointer = ptr;
			if (pointer <= formulaLength)
			{
				look = formulaString[pointer - 1];
			}
			else
			{
				look = '\0';
			}
		}

		/// @return <c>true</c> if the specified name is a valid cell reference
		private bool IsValidCellReference(string str)
		{
			bool flag = CellReference.ClassifyCellReference(str, _ssVersion) == NameType.Cell;
			if (flag && FunctionMetadataRegistry.GetFunctionByName(str.ToUpper()) != null)
			{
				int ptr = pointer;
				ResetPointer(pointer + str.Length);
				SkipWhite();
				flag = (look != '(');
				ResetPointer(ptr);
			}
			return flag;
		}

		/// Note - Excel Function names are 'case aware but not case sensitive'.  This method may end
		/// up creating a defined name record in the workbook if the specified name is not an internal
		/// Excel Function, and Has not been encountered before.
		///
		/// @param name case preserved Function name (as it was entered/appeared in the formula).
		private ParseNode Function(string name)
		{
			Ptg ptg = null;
			if (!AbstractFunctionPtg.IsBuiltInFunctionName(name))
			{
				if (_book == null)
				{
					throw new InvalidOperationException("Need book to evaluate name '" + name + "'");
				}
				IEvaluationName name2 = _book.GetName(name, _sheetIndex);
				if (name2 == null)
				{
					ptg = _book.GetNameXPtg(name);
					if (ptg == null)
					{
						throw new FormulaParseException("Name '" + name + "' is completely unknown in the current workbook");
					}
				}
				else
				{
					if (!name2.IsFunctionName)
					{
						throw new FormulaParseException("Attempt To use name '" + name + "' as a Function, but defined name in workbook does not refer To a Function");
					}
					ptg = name2.CreatePtg();
				}
			}
			Match('(');
			ParseNode[] args = Arguments();
			Match(')');
			return GetFunction(name, ptg, args);
		}

		/// * Generates the variable Function ptg for the formula.
		/// * 
		/// * For IF Formulas, Additional PTGs are Added To the Tokens
		///             	 * @param name a {@link NamePtg} or {@link NameXPtg} or <code>null</code>
		/// * @return Ptg a null is returned if we're in an IF formula, it needs extreme manipulation and is handled in this Function
		private ParseNode GetFunction(string name, Ptg namePtg, ParseNode[] args)
		{
			FunctionMetadata functionByName = FunctionMetadataRegistry.GetFunctionByName(name.ToUpper());
			int num = args.Length;
			if (functionByName == null)
			{
				if (namePtg == null)
				{
					throw new InvalidOperationException("NamePtg must be supplied for external Functions");
				}
				ParseNode[] array = new ParseNode[num + 1];
				array[0] = new ParseNode(namePtg);
				Array.Copy(args, 0, array, 1, num);
				return new ParseNode(FuncVarPtg.Create(name, (byte)(num + 1)), array);
			}
			if (namePtg != null)
			{
				throw new InvalidOperationException("NamePtg no applicable To internal Functions");
			}
			bool flag = !functionByName.HasFixedArgsLength;
			int index = functionByName.Index;
			if (index == 4 && args.Length == 1)
			{
				return new ParseNode(AttrPtg.GetSumSingle(), args);
			}
			ValidateNumArgs(args.Length, functionByName);
			AbstractFunctionPtg token = (!flag) ? ((AbstractFunctionPtg)FuncPtg.Create(index)) : ((AbstractFunctionPtg)FuncVarPtg.Create(name, (byte)num));
			return new ParseNode(token, args);
		}

		private void ValidateNumArgs(int numArgs, FunctionMetadata fm)
		{
			if (numArgs < fm.MinParams)
			{
				string text = "Too few arguments to function '" + fm.Name + "'. ";
				if (fm.HasFixedArgsLength)
				{
					text = text + "Expected " + fm.MinParams;
				}
				else
				{
					object obj = text;
					text = obj + "At least " + fm.MinParams + " were expected";
				}
				object obj2 = text;
				text = obj2 + " but got " + numArgs + ".";
				throw new FormulaParseException(text);
			}
			int num = (!fm.HasUnlimitedVarags) ? fm.MaxParams : ((_book == null) ? fm.MaxParams : _book.GetSpreadsheetVersion().MaxFunctionArgs);
			if (numArgs > num)
			{
				string text2 = "Too many arguments to function '" + fm.Name + "'. ";
				if (fm.HasFixedArgsLength)
				{
					text2 = text2 + "Expected " + fm.MaxParams;
				}
				else
				{
					object obj3 = text2;
					text2 = obj3 + "At most " + fm.MaxParams + " were expected";
				}
				object obj4 = text2;
				text2 = obj4 + " but got " + numArgs + ".";
				throw new FormulaParseException(text2);
			}
		}

		private static bool IsArgumentDelimiter(char ch)
		{
			if (ch != ',')
			{
				return ch == ')';
			}
			return true;
		}

		/// Get arguments To a Function 
		private ParseNode[] Arguments()
		{
			ArrayList arrayList = new ArrayList(2);
			SkipWhite();
			if (look == ')')
			{
				return ParseNode.EMPTY_ARRAY;
			}
			bool flag = true;
			int num = 0;
			while (true)
			{
				SkipWhite();
				if (IsArgumentDelimiter(look))
				{
					if (flag)
					{
						arrayList.Add(new ParseNode(MissingArgPtg.instance));
						num++;
					}
					if (look == ')')
					{
						break;
					}
					Match(',');
					flag = true;
				}
				else
				{
					arrayList.Add(ComparisonExpression());
					num++;
					flag = false;
					SkipWhite();
					if (!IsArgumentDelimiter(look))
					{
						throw expected("',' or ')'");
					}
				}
			}
			return (ParseNode[])arrayList.ToArray(typeof(ParseNode));
		}

		/// Parse and Translate a Math Factor  
		private ParseNode PowerFactor()
		{
			ParseNode parseNode = PercentFactor();
			while (true)
			{
				SkipWhite();
				if (look != '^')
				{
					break;
				}
				Match('^');
				ParseNode child = PercentFactor();
				parseNode = new ParseNode(PowerPtg.instance, parseNode, child);
			}
			return parseNode;
		}

		private ParseNode PercentFactor()
		{
			ParseNode parseNode = ParseSimpleFactor();
			while (true)
			{
				SkipWhite();
				if (look != '%')
				{
					break;
				}
				Match('%');
				parseNode = new ParseNode(PercentPtg.instance, parseNode);
			}
			return parseNode;
		}

		/// factors (without ^ or % )
		private ParseNode ParseSimpleFactor()
		{
			SkipWhite();
			switch (look)
			{
			case '#':
				return new ParseNode(ErrPtg.ValueOf(ParseErrorLiteral()));
			case '-':
				Match('-');
				return ParseUnary(isPlus: false);
			case '+':
				Match('+');
				return ParseUnary(isPlus: true);
			case '(':
			{
				Match('(');
				ParseNode child = ComparisonExpression();
				Match(')');
				return new ParseNode(ParenthesisPtg.instance, child);
			}
			case '"':
				return new ParseNode(new StringPtg(ParseStringLiteral()));
			case '{':
			{
				Match('{');
				ParseNode result = ParseArray();
				Match('}');
				return result;
			}
			default:
				if (IsAlpha(look) || char.IsDigit(look) || look == '\'' || look == '[')
				{
					return ParseRangeExpression();
				}
				if (look == '.')
				{
					return new ParseNode(ParseNumber());
				}
				throw expected("cell ref or constant literal");
			}
		}

		private ParseNode ParseUnary(bool isPlus)
		{
			bool flag = IsDigit(look) || look == '.';
			ParseNode parseNode = PowerFactor();
			if (flag)
			{
				Ptg token = parseNode.GetToken();
				if (token is NumberPtg)
				{
					if (isPlus)
					{
						return parseNode;
					}
					token = new NumberPtg(0.0 - ((NumberPtg)token).Value);
					return new ParseNode(token);
				}
				if (token is IntPtg)
				{
					if (isPlus)
					{
						return parseNode;
					}
					int value = ((IntPtg)token).Value;
					token = new NumberPtg((double)(-value));
					return new ParseNode(token);
				}
			}
			return new ParseNode(isPlus ? UnaryPlusPtg.instance : UnaryMinusPtg.instance, parseNode);
		}

		private ParseNode ParseArray()
		{
			List<object[]> list = new List<object[]>();
			while (true)
			{
				object[] item = ParseArrayRow();
				list.Add(item);
				if (look == '}')
				{
					break;
				}
				if (look != ';')
				{
					throw expected("'}' or ';'");
				}
				Match(';');
			}
			int count = list.Count;
			object[][] array = new object[count][];
			array = list.ToArray();
			int nColumns = array[0].Length;
			CheckRowLengths(array, nColumns);
			return new ParseNode(new ArrayPtg(array));
		}

		private void CheckRowLengths(object[][] values2d, int nColumns)
		{
			int num = 0;
			int num2;
			while (true)
			{
				if (num >= values2d.Length)
				{
					return;
				}
				num2 = values2d[num].Length;
				if (num2 != nColumns)
				{
					break;
				}
				num++;
			}
			throw new FormulaParseException("Array row " + num + " Has length " + num2 + " but row 0 Has length " + nColumns);
		}

		private object[] ParseArrayRow()
		{
			ArrayList arrayList = new ArrayList();
			while (true)
			{
				arrayList.Add(ParseArrayItem());
				SkipWhite();
				switch (look)
				{
				case ',':
					break;
				default:
					throw expected("'}' or ','");
				case ';':
				case '}':
				{
					object[] array = new object[arrayList.Count];
					return arrayList.ToArray();
				}
				}
				Match(',');
			}
		}

		private object ParseArrayItem()
		{
			SkipWhite();
			switch (look)
			{
			case '"':
				return ParseStringLiteral();
			case '#':
				return ErrorConstant.ValueOf(ParseErrorLiteral());
			case 'F':
			case 'T':
			case 'f':
			case 't':
				return ParseBooleanLiteral();
			case '-':
				Match('-');
				SkipWhite();
				return ConvertArrayNumber(ParseNumber(), isPositive: false);
			default:
				return ConvertArrayNumber(ParseNumber(), isPositive: true);
			}
		}

		private bool ParseBooleanLiteral()
		{
			string value = ParseUnquotedIdentifier();
			if ("TRUE".Equals(value, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
			if ("FALSE".Equals(value, StringComparison.OrdinalIgnoreCase))
			{
				return false;
			}
			throw expected("'TRUE' or 'FALSE'");
		}

		private static double ConvertArrayNumber(Ptg ptg, bool isPositive)
		{
			double num;
			if (ptg is IntPtg)
			{
				num = (double)((IntPtg)ptg).Value;
			}
			else
			{
				if (!(ptg is NumberPtg))
				{
					throw new Exception("Unexpected ptg (" + ptg.GetType().Name + ")");
				}
				num = ((NumberPtg)ptg).Value;
			}
			if (!isPositive)
			{
				num = 0.0 - num;
			}
			return num;
		}

		private Ptg ParseNumber()
		{
			string text = null;
			string exponent = null;
			string num = GetNum();
			if (look == '.')
			{
				GetChar();
				text = GetNum();
			}
			if (look == 'E')
			{
				GetChar();
				string str = "";
				if (look == '+')
				{
					GetChar();
				}
				else if (look == '-')
				{
					GetChar();
					str = "-";
				}
				string num2 = GetNum();
				if (num2 == null)
				{
					throw expected("int");
				}
				exponent = str + num2;
			}
			if (num == null && text == null)
			{
				throw expected("int");
			}
			return GetNumberPtgFromString(num, text, exponent);
		}

		private int ParseErrorLiteral()
		{
			Match('#');
			string text = ParseUnquotedIdentifier().ToUpper();
			switch (text[0])
			{
			case 'V':
				if (text.Equals("VALUE"))
				{
					Match('!');
					return 15;
				}
				throw expected("#VALUE!");
			case 'R':
				if (text.Equals("REF"))
				{
					Match('!');
					return 23;
				}
				throw expected("#REF!");
			case 'D':
				if (text.Equals("DIV"))
				{
					Match('/');
					Match('0');
					Match('!');
					return 7;
				}
				throw expected("#DIV/0!");
			case 'N':
				if (text.Equals("NAME"))
				{
					Match('?');
					return 29;
				}
				if (text.Equals("NUM"))
				{
					Match('!');
					return 36;
				}
				if (text.Equals("NULL"))
				{
					Match('!');
					return 0;
				}
				if (text.Equals("N"))
				{
					Match('/');
					if (look != 'A' && look != 'a')
					{
						throw expected("#N/A");
					}
					Match(look);
					return 42;
				}
				throw expected("#NAME?, #NUM!, #NULL! or #N/A");
			default:
				throw expected("#VALUE!, #REF!, #DIV/0!, #NAME?, #NUM!, #NULL! or #N/A");
			}
		}

		/// Get a PTG for an integer from its string representation.
		/// return Int or Number Ptg based on size of input
		private static Ptg GetNumberPtgFromString(string number1, string number2, string exponent)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (number2 == null)
			{
				stringBuilder.Append(number1);
				if (exponent != null)
				{
					stringBuilder.Append('E');
					stringBuilder.Append(exponent);
				}
				string text = stringBuilder.ToString();
				int num;
				try
				{
					num = int.Parse(text, CultureInfo.InvariantCulture);
				}
				catch (FormatException)
				{
					return new NumberPtg(text);
				}
				catch (OverflowException)
				{
					return new NumberPtg(text);
				}
				if (IntPtg.IsInRange(num))
				{
					return new IntPtg(num);
				}
				return new NumberPtg(text);
			}
			if (number1 != null)
			{
				stringBuilder.Append(number1);
			}
			stringBuilder.Append('.');
			stringBuilder.Append(number2);
			if (exponent != null)
			{
				stringBuilder.Append('E');
				stringBuilder.Append(exponent);
			}
			return new NumberPtg(stringBuilder.ToString());
		}

		private string ParseStringLiteral()
		{
			Match('"');
			StringBuilder stringBuilder = new StringBuilder();
			while (true)
			{
				if (look == '"')
				{
					GetChar();
					if (look != '"')
					{
						break;
					}
				}
				stringBuilder.Append(look);
				GetChar();
			}
			return stringBuilder.ToString();
		}

		/// Parse and Translate a Math Term 
		private ParseNode Term()
		{
			ParseNode parseNode = PowerFactor();
			while (true)
			{
				SkipWhite();
				Ptg instance;
				switch (look)
				{
				case '*':
					Match('*');
					instance = MultiplyPtg.instance;
					break;
				case '/':
					Match('/');
					instance = DividePtg.instance;
					break;
				default:
					return parseNode;
				}
				ParseNode child = PowerFactor();
				parseNode = new ParseNode(instance, parseNode, child);
			}
		}

		private ParseNode ComparisonExpression()
		{
			ParseNode parseNode = ConcatExpression();
			while (true)
			{
				SkipWhite();
				switch (look)
				{
				case '<':
				case '=':
				case '>':
					break;
				default:
					return parseNode;
				}
				Ptg comparisonToken = GetComparisonToken();
				ParseNode child = ConcatExpression();
				parseNode = new ParseNode(comparisonToken, parseNode, child);
			}
		}

		private Ptg GetComparisonToken()
		{
			if (look == '=')
			{
				Match(look);
				return EqualPtg.instance;
			}
			bool flag = look == '>';
			Match(look);
			if (!flag)
			{
				switch (look)
				{
				case '=':
					Match('=');
					return LessEqualPtg.instance;
				case '>':
					Match('>');
					return NotEqualPtg.instance;
				default:
					return LessThanPtg.instance;
				}
			}
			if (look == '=')
			{
				Match('=');
				return GreaterEqualPtg.instance;
			}
			return GreaterThanPtg.instance;
		}

		private ParseNode ConcatExpression()
		{
			ParseNode parseNode = AdditiveExpression();
			while (true)
			{
				SkipWhite();
				if (look != '&')
				{
					break;
				}
				Match('&');
				ParseNode child = AdditiveExpression();
				parseNode = new ParseNode(ConcatPtg.instance, parseNode, child);
			}
			return parseNode;
		}

		/// Parse and Translate an Expression 
		private ParseNode AdditiveExpression()
		{
			ParseNode parseNode = Term();
			while (true)
			{
				SkipWhite();
				Ptg instance;
				switch (look)
				{
				case '+':
					Match('+');
					instance = AddPtg.instance;
					break;
				case '-':
					Match('-');
					instance = SubtractPtg.instance;
					break;
				default:
					return parseNode;
				}
				ParseNode child = Term();
				parseNode = new ParseNode(instance, parseNode, child);
			}
		}

		/// API call To execute the parsing of the formula
		private void Parse()
		{
			pointer = 0;
			GetChar();
			_rootNode = UnionExpression();
			if (pointer <= formulaLength)
			{
				string msg = "Unused input [" + formulaString.Substring(pointer - 1) + "] after attempting To Parse the formula [" + formulaString + "]";
				throw new FormulaParseException(msg);
			}
		}

		private ParseNode UnionExpression()
		{
			ParseNode parseNode = ComparisonExpression();
			bool flag = false;
			while (true)
			{
				SkipWhite();
				char c = look;
				if (c != ',')
				{
					break;
				}
				GetChar();
				flag = true;
				ParseNode child = ComparisonExpression();
				parseNode = new ParseNode(UnionPtg.instance, parseNode, child);
			}
			if (flag)
			{
				return AugmentWithMemPtg(parseNode);
			}
			return parseNode;
		}

		private Ptg[] GetRPNPtg(FormulaType formulaType)
		{
			OperandClassTransformer operandClassTransformer = new OperandClassTransformer(formulaType);
			operandClassTransformer.TransformFormula(_rootNode);
			return ParseNode.ToTokenArray(_rootNode);
		}
	}
}
