using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS;
using NPOI.SS.Formula;
using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.PTG;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.Util;
using NPOI.XSSF.Model;
using System;
using System.Globalization;

namespace NPOI.XSSF.UserModel
{
	/// High level representation of a cell in a row of a spreadsheet.
	/// <p>
	/// Cells can be numeric, formula-based or string-based (text).  The cell type
	/// specifies this.  String cells cannot conatin numbers and numeric cells cannot
	/// contain strings (at least according to our model).  Client apps should do the
	/// conversions themselves.  Formula cells have the formula string, as well as
	/// the formula result, which can be numeric or string.
	/// </p>
	/// <p>
	/// Cells should have their number (0 based) before being Added to a row.  Only
	/// cells that have values should be Added.
	/// </p>
	public class XSSFCell : ICell
	{
		private static string FALSE_AS_STRING = "0";

		private static string TRUE_AS_STRING = "1";

		/// the xml bean Containing information about the cell's location, value,
		/// data type, formatting, and formula
		private CT_Cell _cell;

		/// the XSSFRow this cell belongs to
		private XSSFRow _row;

		/// 0-based column index
		private int _cellNum;

		/// Table of strings shared across this workbook.
		/// If two cells contain the same string, then the cell value is the same index into SharedStringsTable
		private SharedStringsTable _sharedStringSource;

		/// Table of cell styles shared across all cells in a workbook.
		private StylesTable _stylesSource;

		/// Returns the sheet this cell belongs to
		///
		/// @return the sheet this cell belongs to
		public ISheet Sheet
		{
			get
			{
				return _row.Sheet;
			}
		}

		/// Returns the row this cell belongs to
		///
		/// @return the row this cell belongs to
		public IRow Row
		{
			get
			{
				return _row;
			}
		}

		/// Get the value of the cell as a bool.
		/// <p>
		/// For strings, numbers, and errors, we throw an exception. For blank cells we return a false.
		/// </p>
		/// @return the value of the cell as a bool
		/// @throws InvalidOperationException if the cell type returned by {@link #CellType}
		///   is not CellType.Boolean, CellType.Blank or CellType.Formula
		public bool BooleanCellValue
		{
			get
			{
				CellType cellType = CellType;
				switch (cellType)
				{
				case CellType.Blank:
					return false;
				case CellType.Boolean:
					if (_cell.IsSetV())
					{
						return TRUE_AS_STRING.Equals(_cell.v);
					}
					return false;
				case CellType.Formula:
					if (_cell.IsSetV())
					{
						return TRUE_AS_STRING.Equals(_cell.v);
					}
					return false;
				default:
					throw TypeMismatch(CellType.Boolean, cellType, false);
				}
			}
		}

		/// Get the value of the cell as a number.
		/// <p>
		/// For strings we throw an exception. For blank cells we return a 0.
		/// For formulas or error cells we return the precalculated value;
		/// </p>
		/// @return the value of the cell as a number
		/// @throws InvalidOperationException if the cell type returned by {@link #CellType} is CellType.String
		/// @exception NumberFormatException if the cell value isn't a parsable <code>double</code>.
		/// @see DataFormatter for turning this number into a string similar to that which Excel would render this number as.
		public double NumericCellValue
		{
			get
			{
				CellType cellType = CellType;
				switch (cellType)
				{
				case CellType.Blank:
					return 0.0;
				case CellType.Numeric:
				case CellType.Formula:
					if (_cell.IsSetV())
					{
						try
						{
							return double.Parse(_cell.v, CultureInfo.InvariantCulture);
						}
						catch (FormatException)
						{
							throw TypeMismatch(CellType.Numeric, CellType.String, false);
						}
					}
					return 0.0;
				default:
					throw TypeMismatch(CellType.Numeric, cellType, false);
				}
			}
		}

		/// Get the value of the cell as a string
		/// <p>
		/// For numeric cells we throw an exception. For blank cells we return an empty string.
		/// For formulaCells that are not string Formulas, we throw an exception
		/// </p>
		/// @return the value of the cell as a string
		public string StringCellValue
		{
			get
			{
				IRichTextString richStringCellValue = RichStringCellValue;
				if (richStringCellValue != null)
				{
					return richStringCellValue.String;
				}
				return null;
			}
		}

		/// Get the value of the cell as a XSSFRichTextString
		/// <p>
		/// For numeric cells we throw an exception. For blank cells we return an empty string.
		/// For formula cells we return the pre-calculated value if a string, otherwise an exception
		/// </p>
		/// @return the value of the cell as a XSSFRichTextString
		public IRichTextString RichStringCellValue
		{
			get
			{
				CellType cellType = CellType;
				XSSFRichTextString xSSFRichTextString;
				switch (cellType)
				{
				case CellType.Blank:
					xSSFRichTextString = new XSSFRichTextString("");
					break;
				case CellType.String:
					if (_cell.t == ST_CellType.inlineStr)
					{
						xSSFRichTextString = ((!_cell.IsSetIs()) ? ((!_cell.IsSetV()) ? new XSSFRichTextString("") : new XSSFRichTextString(_cell.v)) : new XSSFRichTextString(_cell.@is));
					}
					else if (_cell.t == ST_CellType.str)
					{
						xSSFRichTextString = new XSSFRichTextString(_cell.IsSetV() ? _cell.v : "");
					}
					else if (_cell.IsSetV())
					{
						int idx = int.Parse(_cell.v);
						xSSFRichTextString = new XSSFRichTextString(_sharedStringSource.GetEntryAt(idx));
					}
					else
					{
						xSSFRichTextString = new XSSFRichTextString("");
					}
					break;
				case CellType.Formula:
					CheckFormulaCachedValueType(CellType.String, GetBaseCellType(false));
					xSSFRichTextString = new XSSFRichTextString(_cell.IsSetV() ? _cell.v : "");
					break;
				default:
					throw TypeMismatch(CellType.String, cellType, false);
				}
				xSSFRichTextString.SetStylesTableReference(_stylesSource);
				return xSSFRichTextString;
			}
		}

		/// <summary>
		/// Return a formula for the cell,  for example, <code>SUM(C4:E4)</code>
		/// </summary>
		public string CellFormula
		{
			get
			{
				CellType cellType = CellType;
				if (cellType != CellType.Formula)
				{
					throw TypeMismatch(CellType.Formula, cellType, false);
				}
				CT_CellFormula f = _cell.f;
				if (IsPartOfArrayFormulaGroup && f == null)
				{
					ICell firstCellInArrayFormula = ((XSSFSheet)Sheet).GetFirstCellInArrayFormula(this);
					return firstCellInArrayFormula.CellFormula;
				}
				if (f.t == ST_CellFormulaType.shared)
				{
					return ConvertSharedFormula((int)f.si);
				}
				return f.Value;
			}
			set
			{
				SetCellFormula(value);
			}
		}

		/// <summary>
		/// Returns zero-based column index of this cell
		/// </summary>
		public int ColumnIndex
		{
			get
			{
				return _cellNum;
			}
		}

		/// <summary>
		/// Returns zero-based row index of a row in the sheet that contains this cell
		/// </summary>
		public int RowIndex
		{
			get
			{
				return _row.RowNum;
			}
		}

		/// <summary>
		/// Return the cell's style.
		/// </summary>
		public ICellStyle CellStyle
		{
			get
			{
				XSSFCellStyle result = null;
				if (_stylesSource != null && _stylesSource.NumCellStyles > 0)
				{
					long num = _cell.IsSetS() ? _cell.s : 0;
					result = _stylesSource.GetStyleAt((int)num);
				}
				return result;
			}
			set
			{
				if (value == null)
				{
					if (_cell.IsSetS())
					{
						_cell.unsetS();
					}
				}
				else
				{
					XSSFCellStyle xSSFCellStyle = (XSSFCellStyle)value;
					xSSFCellStyle.VerifyBelongsToStylesSource(_stylesSource);
					long num = _stylesSource.PutStyle(xSSFCellStyle);
					_cell.s = (uint)num;
				}
			}
		}

		/// <summary>
		/// Return the cell type.
		/// </summary>
		public CellType CellType
		{
			get
			{
				if (_cell.f != null || ((XSSFSheet)Sheet).IsCellInArrayFormulaContext(this))
				{
					return CellType.Formula;
				}
				return GetBaseCellType(true);
			}
		}

		/// <summary>
		/// Only valid for formula cells
		/// </summary>
		public CellType CachedFormulaResultType
		{
			get
			{
				if (_cell.f == null)
				{
					throw new InvalidOperationException("Only formula cells have cached results");
				}
				return GetBaseCellType(false);
			}
		}

		/// <summary>
		/// Get the value of the cell as a date.
		/// </summary>
		public DateTime DateCellValue
		{
			get
			{
				CellType cellType = CellType;
				if (cellType == CellType.Blank)
				{
					return DateTime.MinValue;
				}
				double numericCellValue = NumericCellValue;
				bool use1904windowing = ((XSSFWorkbook)Sheet.Workbook).IsDate1904();
				return DateUtil.GetJavaDate(numericCellValue, use1904windowing);
			}
		}

		/// <summary>
		/// Returns the error message, such as #VALUE!
		/// </summary>
		public string ErrorCellString
		{
			get
			{
				CellType baseCellType = GetBaseCellType(true);
				if (baseCellType != CellType.Error)
				{
					throw TypeMismatch(CellType.Error, baseCellType, false);
				}
				return _cell.v;
			}
		}

		/// <summary>
		/// Get the value of the cell as an error code.
		/// For strings, numbers, and bools, we throw an exception.
		/// For blank cells we return a 0.
		/// </summary>
		public byte ErrorCellValue
		{
			get
			{
				string errorCellString = ErrorCellString;
				if (errorCellString == null)
				{
					return 0;
				}
				return FormulaError.ForString(errorCellString).Code;
			}
		}

		/// <summary>
		///  Returns cell comment associated with this cell
		/// </summary>
		public IComment CellComment
		{
			get
			{
				return Sheet.GetCellComment(_row.RowNum, ColumnIndex);
			}
			set
			{
				if (value == null)
				{
					RemoveCellComment();
				}
				else
				{
					value.Row = RowIndex;
					value.Column = ColumnIndex;
				}
			}
		}

		/// <summary>
		/// Returns hyperlink associated with this cell
		/// </summary>
		public IHyperlink Hyperlink
		{
			get
			{
				return ((XSSFSheet)Sheet).GetHyperlink(_row.RowNum, _cellNum);
			}
			set
			{
				XSSFHyperlink xSSFHyperlink = (XSSFHyperlink)value;
				xSSFHyperlink.SetCellReference(new CellReference(_row.RowNum, _cellNum).FormatAsString());
				((XSSFSheet)Sheet).AddHyperlink(xSSFHyperlink);
			}
		}

		public CellRangeAddress ArrayFormulaRange
		{
			get
			{
				XSSFCell firstCellInArrayFormula = ((XSSFSheet)Sheet).GetFirstCellInArrayFormula(this);
				if (firstCellInArrayFormula == null)
				{
					throw new InvalidOperationException("Cell " + GetReference() + " is not part of an array formula.");
				}
				string @ref = firstCellInArrayFormula._cell.f.@ref;
				return CellRangeAddress.ValueOf(@ref);
			}
		}

		public bool IsPartOfArrayFormulaGroup
		{
			get
			{
				return ((XSSFSheet)Sheet).IsCellInArrayFormulaContext(this);
			}
		}

		public bool IsMergedCell
		{
			get
			{
				return Sheet.IsMergedRegion(new CellRangeAddress(RowIndex, RowIndex, ColumnIndex, ColumnIndex));
			}
		}

		/// Construct a XSSFCell.
		///
		/// @param row the parent row.
		/// @param cell the xml bean Containing information about the cell.
		public XSSFCell(XSSFRow row, CT_Cell cell)
		{
			_cell = cell;
			_row = row;
			if (cell.r != null)
			{
				_cellNum = new CellReference(cell.r).Col;
			}
			else
			{
				int lastCellNum = row.LastCellNum;
				if (lastCellNum != -1)
				{
					_cellNum = row.GetCell(lastCellNum - 1).ColumnIndex + 1;
				}
			}
			_sharedStringSource = ((XSSFWorkbook)row.Sheet.Workbook).GetSharedStringSource();
			_stylesSource = ((XSSFWorkbook)row.Sheet.Workbook).GetStylesSource();
		}

		/// @return table of strings shared across this workbook
		protected SharedStringsTable GetSharedStringSource()
		{
			return _sharedStringSource;
		}

		/// @return table of cell styles shared across this workbook
		protected StylesTable GetStylesSource()
		{
			return _stylesSource;
		}

		/// Set a bool value for the cell
		///
		/// @param value the bool value to Set this cell to.  For formulas we'll Set the
		///        precalculated value, for bools we'll Set its value. For other types we
		///        will change the cell to a bool cell and Set its value.
		public void SetCellValue(bool value)
		{
			_cell.t = ST_CellType.b;
			_cell.v = (value ? TRUE_AS_STRING : FALSE_AS_STRING);
		}

		/// Set a numeric value for the cell
		///
		/// @param value  the numeric value to Set this cell to.  For formulas we'll Set the
		///        precalculated value, for numerics we'll Set its value. For other types we
		///        will change the cell to a numeric cell and Set its value.
		public void SetCellValue(double value)
		{
			if (double.IsInfinity(value))
			{
				_cell.t = ST_CellType.e;
				_cell.v = FormulaError.DIV0.String;
			}
			else if (double.IsNaN(value))
			{
				_cell.t = ST_CellType.e;
				_cell.v = FormulaError.NUM.String;
			}
			else
			{
				_cell.t = ST_CellType.n;
				_cell.v = value.ToString(CultureInfo.InvariantCulture);
			}
		}

		private static void CheckFormulaCachedValueType(CellType expectedTypeCode, CellType cachedValueType)
		{
			if (cachedValueType != expectedTypeCode)
			{
				throw TypeMismatch(expectedTypeCode, cachedValueType, true);
			}
		}

		/// Set a string value for the cell.
		///
		/// @param str value to Set the cell to.  For formulas we'll Set the formula
		/// cached string result, for String cells we'll Set its value. For other types we will
		/// change the cell to a string cell and Set its value.
		/// If value is null then we will change the cell to a Blank cell.
		public void SetCellValue(string str)
		{
			SetCellValue((str == null) ? null : new XSSFRichTextString(str));
		}

		/// Set a string value for the cell.
		///
		/// @param str  value to Set the cell to.  For formulas we'll Set the 'pre-Evaluated result string,
		/// for String cells we'll Set its value.  For other types we will
		/// change the cell to a string cell and Set its value.
		/// If value is null then we will change the cell to a Blank cell.
		public void SetCellValue(IRichTextString str)
		{
			if (str == null || string.IsNullOrEmpty(str.String))
			{
				SetCellType(CellType.Blank);
			}
			else
			{
				CellType cellType = CellType;
				CellType cellType2 = cellType;
				if (cellType2 == CellType.Formula)
				{
					_cell.v = str.String;
					_cell.t = ST_CellType.str;
				}
				else if (_cell.t == ST_CellType.inlineStr)
				{
					_cell.v = str.String;
				}
				else
				{
					_cell.t = ST_CellType.s;
					XSSFRichTextString xSSFRichTextString = (XSSFRichTextString)str;
					xSSFRichTextString.SetStylesTableReference(_stylesSource);
					int num = _sharedStringSource.AddEntry(xSSFRichTextString.GetCTRst());
					_cell.v = num.ToString();
				}
			}
		}

		/// <summary>
		/// Creates a non shared formula from the shared formula counterpart
		/// </summary>
		/// <param name="si">Shared Group Index</param>
		/// <returns>non shared formula created for the given shared formula and this cell</returns>
		private string ConvertSharedFormula(int si)
		{
			XSSFSheet xSSFSheet = (XSSFSheet)Sheet;
			CT_CellFormula sharedFormula = xSSFSheet.GetSharedFormula(si);
			if (sharedFormula == null)
			{
				throw new InvalidOperationException("Master cell of a shared formula with sid=" + si + " was not found");
			}
			string value = sharedFormula.Value;
			string @ref = sharedFormula.@ref;
			CellRangeAddress cellRangeAddress = CellRangeAddress.ValueOf(@ref);
			int sheetIndex = xSSFSheet.Workbook.GetSheetIndex(xSSFSheet);
			XSSFEvaluationWorkbook xSSFEvaluationWorkbook = XSSFEvaluationWorkbook.Create(xSSFSheet.Workbook);
			SharedFormula sharedFormula2 = new SharedFormula(SpreadsheetVersion.EXCEL2007);
			Ptg[] ptgs = FormulaParser.Parse(value, xSSFEvaluationWorkbook, FormulaType.Cell, sheetIndex);
			Ptg[] ptgs2 = sharedFormula2.ConvertSharedFormulas(ptgs, RowIndex - cellRangeAddress.FirstRow, ColumnIndex - cellRangeAddress.FirstColumn);
			return FormulaRenderer.ToFormulaString(xSSFEvaluationWorkbook, ptgs2);
		}

		/// Sets formula for this cell.
		/// <p>
		/// Note, this method only Sets the formula string and does not calculate the formula value.
		/// To Set the precalculated value use {@link #setCellValue(double)} or {@link #setCellValue(String)}
		/// </p>
		///
		/// @param formula the formula to Set, e.g. <code>"SUM(C4:E4)"</code>.
		///  If the argument is <code>null</code> then the current formula is Removed.
		/// @throws NPOI.ss.formula.FormulaParseException if the formula has incorrect syntax or is otherwise invalid
		/// @throws InvalidOperationException if the operation is not allowed, for example,
		///  when the cell is a part of a multi-cell array formula
		public void SetCellFormula(string formula)
		{
			if (IsPartOfArrayFormulaGroup)
			{
				NotifyArrayFormulaChanging();
			}
			SetFormula(formula, FormulaType.Cell);
		}

		internal void SetCellArrayFormula(string formula, CellRangeAddress range)
		{
			SetFormula(formula, FormulaType.Array);
			CT_CellFormula f = _cell.f;
			f.t = ST_CellFormulaType.array;
			f.@ref = range.FormatAsString();
		}

		private void SetFormula(string formula, FormulaType formulaType)
		{
			IWorkbook workbook = _row.Sheet.Workbook;
			if (formula == null)
			{
				((XSSFWorkbook)workbook).OnDeleteFormula(this);
				if (_cell.IsSetF())
				{
					_cell.unsetF();
				}
			}
			else
			{
				IFormulaParsingWorkbook workbook2 = XSSFEvaluationWorkbook.Create(workbook);
				FormulaParser.Parse(formula, workbook2, formulaType, workbook.GetSheetIndex(Sheet));
				CT_CellFormula cT_CellFormula = new CT_CellFormula();
				cT_CellFormula.Value = formula;
				_cell.f = cT_CellFormula;
				if (_cell.IsSetV())
				{
					_cell.unsetV();
				}
			}
		}

		/// <summary>
		/// Returns an A1 style reference to the location of this cell
		/// </summary>
		/// <returns>A1 style reference to the location of this cell</returns>
		public string GetReference()
		{
			string r = _cell.r;
			if (r == null)
			{
				return new CellReference(this).FormatAsString();
			}
			return r;
		}

		/// <summary>
		/// Detect cell type based on the "t" attribute of the CT_Cell bean
		/// </summary>
		/// <param name="blankCells"></param>
		/// <returns></returns>
		private CellType GetBaseCellType(bool blankCells)
		{
			switch (_cell.t)
			{
			case ST_CellType.b:
				return CellType.Boolean;
			case ST_CellType.n:
				if (!_cell.IsSetV() && blankCells)
				{
					return CellType.Blank;
				}
				return CellType.Numeric;
			case ST_CellType.e:
				return CellType.Error;
			case ST_CellType.s:
			case ST_CellType.str:
			case ST_CellType.inlineStr:
				return CellType.String;
			default:
				throw new InvalidOperationException("Illegal cell type: " + _cell.t);
			}
		}

		/// <summary>
		///  Set a date value for the cell. Excel treats dates as numeric so you will need to format the cell as a date.
		/// </summary>
		/// <param name="value">the date value to Set this cell to.  For formulas we'll set the precalculated value, 
		/// for numerics we'll Set its value. For other types we will change the cell to a numeric cell and Set its value. </param>
		public void SetCellValue(DateTime value)
		{
			bool use1904windowing = ((XSSFWorkbook)Sheet.Workbook).IsDate1904();
			SetCellValue(DateUtil.GetExcelDate(value, use1904windowing));
		}

		public void SetCellErrorValue(byte errorCode)
		{
			FormulaError cellErrorValue = FormulaError.ForInt(errorCode);
			SetCellErrorValue(cellErrorValue);
		}

		/// <summary>
		/// Set a error value for the cell
		/// </summary>
		/// <param name="error">the error value to Set this cell to. 
		/// For formulas we'll Set the precalculated value , for errors we'll set
		/// its value. For other types we will change the cell to an error cell and Set its value.
		/// </param>
		public void SetCellErrorValue(FormulaError error)
		{
			_cell.t = ST_CellType.e;
			_cell.v = error.String;
		}

		/// <summary>
		/// Sets this cell as the active cell for the worksheet.
		/// </summary>
		public void SetAsActiveCell()
		{
			((XSSFSheet)Sheet).SetActiveCell(GetReference());
		}

		/// <summary>
		/// Blanks this cell. Blank cells have no formula or value but may have styling.
		/// This method erases all the data previously associated with this cell.
		/// </summary>
		private void SetBlank()
		{
			CT_Cell cT_Cell = new CT_Cell();
			cT_Cell.r = _cell.r;
			if (_cell.IsSetS())
			{
				cT_Cell.s = _cell.s;
			}
			_cell.Set(cT_Cell);
		}

		/// <summary>
		/// Sets column index of this cell
		/// </summary>
		/// <param name="num"></param>
		internal void SetCellNum(int num)
		{
			CheckBounds(num);
			_cellNum = num;
			string r = new CellReference(RowIndex, ColumnIndex).FormatAsString();
			_cell.r = r;
		}

		/// <summary>
		/// Set the cells type (numeric, formula or string)
		/// </summary>
		/// <param name="cellType"></param>
		public void SetCellType(CellType cellType)
		{
			CellType cellType2 = CellType;
			if (IsPartOfArrayFormulaGroup)
			{
				NotifyArrayFormulaChanging();
			}
			if (cellType2 == CellType.Formula && cellType != CellType.Formula)
			{
				((XSSFWorkbook)Sheet.Workbook).OnDeleteFormula(this);
			}
			switch (cellType)
			{
			case CellType.Blank:
				SetBlank();
				break;
			case CellType.Boolean:
			{
				string v = ConvertCellValueToBoolean() ? TRUE_AS_STRING : FALSE_AS_STRING;
				_cell.t = ST_CellType.b;
				_cell.v = v;
				break;
			}
			case CellType.Numeric:
				_cell.t = ST_CellType.n;
				break;
			case CellType.Error:
				_cell.t = ST_CellType.e;
				break;
			case CellType.String:
				if (cellType2 != CellType.String)
				{
					string str = ConvertCellValueToString();
					XSSFRichTextString xSSFRichTextString = new XSSFRichTextString(str);
					xSSFRichTextString.SetStylesTableReference(_stylesSource);
					int num = _sharedStringSource.AddEntry(xSSFRichTextString.GetCTRst());
					_cell.v = num.ToString();
				}
				_cell.t = ST_CellType.s;
				break;
			case CellType.Formula:
				if (!_cell.IsSetF())
				{
					CT_CellFormula cT_CellFormula = new CT_CellFormula();
					cT_CellFormula.Value = "0";
					_cell.f = cT_CellFormula;
					if (_cell.IsSetT())
					{
						_cell.unsetT();
					}
				}
				break;
			default:
				throw new ArgumentException("Illegal cell type: " + cellType);
			}
			if (cellType != CellType.Formula && _cell.IsSetF())
			{
				_cell.unsetF();
			}
		}

		/// <summary>
		/// Returns a string representation of the cell
		/// </summary>
		/// <returns>Formula cells return the formula string, rather than the formula result.
		/// Dates are displayed in dd-MMM-yyyy format
		/// Errors are displayed as #ERR&lt;errIdx&gt;
		/// </returns>
		public override string ToString()
		{
			switch (CellType)
			{
			case CellType.Blank:
				return "";
			case CellType.Boolean:
				if (!BooleanCellValue)
				{
					return "FALSE";
				}
				return "TRUE";
			case CellType.Error:
				return ErrorEval.GetText(ErrorCellValue);
			case CellType.Formula:
				return CellFormula;
			case CellType.Numeric:
				if (DateUtil.IsCellDateFormatted(this))
				{
					FormatBase formatBase = new SimpleDateFormat("dd-MMM-yyyy");
					return formatBase.Format(DateCellValue, CultureInfo.CurrentCulture);
				}
				return string.Concat(NumericCellValue);
			case CellType.String:
				return RichStringCellValue.ToString();
			default:
				return "Unknown Cell Type: " + CellType;
			}
		}

		/// Returns the raw, underlying ooxml value for the cell
		/// <p>
		/// If the cell Contains a string, then this value is an index into
		/// the shared string table, pointing to the actual string value. Otherwise,
		/// the value of the cell is expressed directly in this element. Cells Containing formulas express
		/// the last calculated result of the formula in this element.
		/// </p>
		///
		/// @return the raw cell value as Contained in the underlying CT_Cell bean,
		///     <code>null</code> for blank cells.
		public string GetRawValue()
		{
			return _cell.v;
		}

		/// <summary>
		/// Used to help format error messages
		/// </summary>
		/// <param name="cellTypeCode"></param>
		/// <returns></returns>
		private static string GetCellTypeName(CellType cellTypeCode)
		{
			switch (cellTypeCode)
			{
			case CellType.Blank:
				return "blank";
			case CellType.String:
				return "text";
			case CellType.Boolean:
				return "bool";
			case CellType.Error:
				return "error";
			case CellType.Numeric:
				return "numeric";
			case CellType.Formula:
				return "formula";
			default:
				return "#unknown cell type (" + cellTypeCode + ")#";
			}
		}

		/// Used to help format error messages
		private static Exception TypeMismatch(CellType expectedTypeCode, CellType actualTypeCode, bool IsFormulaCell)
		{
			string message = "Cannot get a " + GetCellTypeName(expectedTypeCode) + " value from a " + GetCellTypeName(actualTypeCode) + " " + (IsFormulaCell ? "formula " : "") + "cell";
			return new InvalidOperationException(message);
		}

		/// @throws RuntimeException if the bounds are exceeded.
		private static void CheckBounds(int cellIndex)
		{
			SpreadsheetVersion eXCEL = SpreadsheetVersion.EXCEL2007;
			int lastColumnIndex = SpreadsheetVersion.EXCEL2007.LastColumnIndex;
			if (cellIndex < 0 || cellIndex > lastColumnIndex)
			{
				throw new ArgumentException("Invalid column index (" + cellIndex + ").  Allowable column range for " + eXCEL.ToString() + " is (0.." + lastColumnIndex + ") or ('A'..'" + eXCEL.LastColumnName + "')");
			}
		}

		/// <summary>
		/// Removes the comment for this cell, if there is one.
		/// </summary>
		public void RemoveCellComment()
		{
			IComment cellComment = CellComment;
			if (cellComment != null)
			{
				string reference = GetReference();
				XSSFSheet xSSFSheet = (XSSFSheet)Sheet;
				xSSFSheet.GetCommentsTable(false).RemoveComment(reference);
				xSSFSheet.GetVMLDrawing(false).RemoveCommentShape(RowIndex, ColumnIndex);
			}
		}

		/// Returns the xml bean containing information about the cell's location (reference), value,
		/// data type, formatting, and formula
		///
		/// @return the xml bean containing information about this cell
		internal CT_Cell GetCTCell()
		{
			return _cell;
		}

		/// Chooses a new bool value for the cell when its type is changing.<p />
		///
		/// Usually the caller is calling SetCellType() with the intention of calling
		/// SetCellValue(bool) straight afterwards.  This method only exists to give
		/// the cell a somewhat reasonable value until the SetCellValue() call (if at all).
		/// TODO - perhaps a method like SetCellTypeAndValue(int, Object) should be introduced to avoid this
		private bool ConvertCellValueToBoolean()
		{
			CellType cellType = CellType;
			if (cellType == CellType.Formula)
			{
				cellType = GetBaseCellType(false);
			}
			switch (cellType)
			{
			case CellType.Boolean:
				return TRUE_AS_STRING.Equals(_cell.v);
			case CellType.String:
			{
				int idx = int.Parse(_cell.v);
				XSSFRichTextString xSSFRichTextString = new XSSFRichTextString(_sharedStringSource.GetEntryAt(idx));
				string @string = xSSFRichTextString.String;
				return bool.Parse(@string);
			}
			case CellType.Numeric:
				return double.Parse(_cell.v, CultureInfo.InvariantCulture) != 0.0;
			case CellType.Blank:
			case CellType.Error:
				return false;
			default:
				throw new RuntimeException("Unexpected cell type (" + cellType + ")");
			}
		}

		private string ConvertCellValueToString()
		{
			CellType cellType = CellType;
			switch (cellType)
			{
			case CellType.Blank:
				return "";
			case CellType.Boolean:
				if (!TRUE_AS_STRING.Equals(_cell.v))
				{
					return "FALSE";
				}
				return "TRUE";
			case CellType.String:
			{
				int idx = int.Parse(_cell.v);
				XSSFRichTextString xSSFRichTextString = new XSSFRichTextString(_sharedStringSource.GetEntryAt(idx));
				return xSSFRichTextString.String;
			}
			case CellType.Numeric:
			case CellType.Error:
				return _cell.v;
			default:
				throw new InvalidOperationException("Unexpected cell type (" + cellType + ")");
			case CellType.Formula:
			{
				cellType = GetBaseCellType(false);
				string v = _cell.v;
				switch (cellType)
				{
				case CellType.Boolean:
					if (TRUE_AS_STRING.Equals(v))
					{
						return "TRUE";
					}
					if (FALSE_AS_STRING.Equals(v))
					{
						return "FALSE";
					}
					throw new InvalidOperationException("Unexpected bool cached formula value '" + v + "'.");
				case CellType.Numeric:
				case CellType.String:
				case CellType.Error:
					return v;
				default:
					throw new InvalidOperationException("Unexpected formula result type (" + cellType + ")");
				}
			}
			}
		}

		/// The purpose of this method is to validate the cell state prior to modification
		///
		/// @see #NotifyArrayFormulaChanging()
		internal void NotifyArrayFormulaChanging(string msg)
		{
			if (IsPartOfArrayFormulaGroup)
			{
				CellRangeAddress arrayFormulaRange = ArrayFormulaRange;
				if (arrayFormulaRange.NumberOfCells > 1)
				{
					throw new InvalidOperationException(msg);
				}
				Row.Sheet.RemoveArrayFormula(this);
			}
		}

		/// <summary>
		/// Called when this cell is modified.The purpose of this method is to validate the cell state prior to modification.
		/// </summary>
		/// <exception cref="T:System.InvalidOperationException">if modification is not allowed</exception>
		internal void NotifyArrayFormulaChanging()
		{
			CellReference cellReference = new CellReference(this);
			string msg = "Cell " + cellReference.FormatAsString() + " is part of a multi-cell array formula. You cannot change part of an array.";
			NotifyArrayFormulaChanging(msg);
		}

		public ICell CopyCellTo(int targetIndex)
		{
			return CellUtil.CopyCell(Row, ColumnIndex, targetIndex);
		}
	}
}
