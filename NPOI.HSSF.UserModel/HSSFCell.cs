using NPOI.HSSF.Model;
using NPOI.HSSF.Record;
using NPOI.HSSF.Record.Aggregates;
using NPOI.SS;
using NPOI.SS.Formula;
using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.PTG;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections;
using System.Globalization;
using System.IO;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// High level representation of a cell in a row of a spReadsheet.
	/// Cells can be numeric, formula-based or string-based (text).  The cell type
	/// specifies this.  String cells cannot conatin numbers and numeric cells cannot
	/// contain strings (at least according to our model).  Client apps should do the
	/// conversions themselves.  Formula cells have the formula string, as well as
	/// the formula result, which can be numeric or string.
	/// Cells should have their number (0 based) before being Added to a row.  Only
	/// cells that have values should be Added.
	/// </summary>
	/// <remarks>
	/// @author  Andrew C. Oliver (acoliver at apache dot org)
	/// @author  Dan Sherman (dsherman at Isisph.com)
	/// @author  Brian Sanders (kestrel at burdell dot org) Active Cell support
	/// @author  Yegor Kozlov cell comments support
	/// </remarks>
	[Serializable]
	public class HSSFCell : ICell
	{
		public const short ENCODING_UNCHANGED = -1;

		public const short ENCODING_COMPRESSED_UNICODE = 0;

		public const short ENCODING_UTF_16 = 1;

		private const string FILE_FORMAT_NAME = "BIFF8";

		private CellType cellType;

		private HSSFRichTextString stringValue;

		private HSSFWorkbook book;

		private HSSFSheet sheet;

		private CellValueRecordInterface record;

		private IComment comment;

		public static readonly int LAST_COLUMN_NUMBER = SpreadsheetVersion.EXCEL97.LastColumnIndex;

		private static readonly string LAST_COLUMN_NAME = SpreadsheetVersion.EXCEL97.LastColumnName;

		/// <summary>
		/// the Workbook that this Cell is bound to
		/// </summary>
		public InternalWorkbook BoundWorkbook => book.Workbook;

		public ISheet Sheet => sheet;

		/// <summary>
		/// the HSSFRow this cell belongs to
		/// </summary>
		public IRow Row
		{
			get
			{
				int rowIndex = RowIndex;
				return sheet.GetRow(rowIndex);
			}
		}

		/// <summary>
		/// Get the cells type (numeric, formula or string)
		/// </summary>
		/// <value>The type of the cell.</value>
		public CellType CellType => cellType;

		/// <summary>
		/// Gets or sets the cell formula.
		/// </summary>
		/// <value>The cell formula.</value>
		public string CellFormula
		{
			get
			{
				if (!(record is FormulaRecordAggregate))
				{
					throw TypeMismatch(CellType.Formula, cellType, isFormulaCell: true);
				}
				return HSSFFormulaParser.ToFormulaString(book, ((FormulaRecordAggregate)record).FormulaTokens);
			}
			set
			{
				SetCellFormula(value);
			}
		}

		/// <summary>
		/// Get the value of the cell as a number.  For strings we throw an exception.
		/// For blank cells we return a 0.
		/// </summary>
		/// <value>The numeric cell value.</value>
		public double NumericCellValue
		{
			get
			{
				switch (cellType)
				{
				case CellType.Blank:
					return 0.0;
				case CellType.Numeric:
					return ((NumberRecord)record).Value;
				default:
					throw TypeMismatch(CellType.Numeric, cellType, isFormulaCell: false);
				case CellType.Formula:
				{
					FormulaRecord formulaRecord = ((FormulaRecordAggregate)record).FormulaRecord;
					CheckFormulaCachedValueType(CellType.Numeric, formulaRecord);
					return formulaRecord.Value;
				}
				}
			}
		}

		/// <summary>
		/// Get the value of the cell as a date.  For strings we throw an exception.
		/// For blank cells we return a null.
		/// </summary>
		/// <value>The date cell value.</value>
		public DateTime DateCellValue
		{
			get
			{
				if (cellType == CellType.Blank)
				{
					return DateTime.MaxValue;
				}
				if (cellType == CellType.String)
				{
					throw new InvalidDataException("You cannot get a date value from a String based cell");
				}
				if (cellType == CellType.Boolean)
				{
					throw new InvalidDataException("You cannot get a date value from a bool cell");
				}
				if (cellType == CellType.Error)
				{
					throw new InvalidDataException("You cannot get a date value from an error cell");
				}
				double numericCellValue = NumericCellValue;
				if (book.Workbook.IsUsing1904DateWindowing)
				{
					return DateUtil.GetJavaDate(numericCellValue, use1904windowing: true);
				}
				return DateUtil.GetJavaDate(numericCellValue, use1904windowing: false);
			}
		}

		/// <summary>
		/// Get the value of the cell as a string - for numeric cells we throw an exception.
		/// For blank cells we return an empty string.
		/// For formulaCells that are not string Formulas, we return empty String
		/// </summary>
		/// <value>The string cell value.</value>
		public string StringCellValue
		{
			get
			{
				IRichTextString richStringCellValue = RichStringCellValue;
				return richStringCellValue.String;
			}
		}

		/// <summary>
		/// Get the value of the cell as a string - for numeric cells we throw an exception.
		/// For blank cells we return an empty string.
		/// For formulaCells that are not string Formulas, we return empty String
		/// </summary>
		/// <value>The rich string cell value.</value>
		public IRichTextString RichStringCellValue
		{
			get
			{
				switch (cellType)
				{
				case CellType.Blank:
					return new HSSFRichTextString("");
				case CellType.String:
					return stringValue;
				default:
					throw TypeMismatch(CellType.String, cellType, isFormulaCell: false);
				case CellType.Formula:
				{
					FormulaRecordAggregate formulaRecordAggregate = (FormulaRecordAggregate)record;
					CheckFormulaCachedValueType(CellType.String, formulaRecordAggregate.FormulaRecord);
					string text = formulaRecordAggregate.StringValue;
					return new HSSFRichTextString((text == null) ? "" : text);
				}
				}
			}
		}

		/// <summary>
		/// Get the value of the cell as a bool.  For strings, numbers, and errors, we throw an exception.
		/// For blank cells we return a false.
		/// </summary>
		/// <value><c>true</c> if [boolean cell value]; otherwise, <c>false</c>.</value>
		public bool BooleanCellValue
		{
			get
			{
				switch (cellType)
				{
				case CellType.Blank:
					return false;
				case CellType.Boolean:
					return ((BoolErrRecord)record).BooleanValue;
				default:
					throw TypeMismatch(CellType.Boolean, cellType, isFormulaCell: false);
				case CellType.Formula:
				{
					FormulaRecord formulaRecord = ((FormulaRecordAggregate)record).FormulaRecord;
					CheckFormulaCachedValueType(CellType.Boolean, formulaRecord);
					return formulaRecord.CachedBooleanValue;
				}
				}
			}
		}

		/// <summary>
		/// Get the value of the cell as an error code.  For strings, numbers, and bools, we throw an exception.
		/// For blank cells we return a 0.
		/// </summary>
		/// <value>The error cell value.</value>
		public byte ErrorCellValue
		{
			get
			{
				switch (cellType)
				{
				case CellType.Error:
					return ((BoolErrRecord)record).ErrorValue;
				default:
					throw TypeMismatch(CellType.Error, cellType, isFormulaCell: false);
				case CellType.Formula:
				{
					FormulaRecord formulaRecord = ((FormulaRecordAggregate)record).FormulaRecord;
					CheckFormulaCachedValueType(CellType.Error, formulaRecord);
					return (byte)formulaRecord.CachedErrorValue;
				}
				}
			}
		}

		/// <summary>
		/// Get the style for the cell.  This is a reference to a cell style contained in the workbook
		/// object.
		/// </summary>
		/// <value>The cell style.</value>
		public ICellStyle CellStyle
		{
			get
			{
				short xFIndex = record.XFIndex;
				ExtendedFormatRecord exFormatAt = book.Workbook.GetExFormatAt(xFIndex);
				return new HSSFCellStyle(xFIndex, exFormatAt, book);
			}
			set
			{
				((HSSFCellStyle)value).VerifyBelongsToWorkbook(book);
				short xFIndex = (((HSSFCellStyle)value).UserStyleName == null) ? value.Index : ApplyUserCellStyle((HSSFCellStyle)value);
				record.XFIndex = xFIndex;
			}
		}

		/// <summary>
		/// Should only be used by HSSFSheet and friends.  Returns the low level CellValueRecordInterface record
		/// </summary>
		/// <value>the cell via the low level api.</value>
		public CellValueRecordInterface CellValueRecord => record;

		/// <summary>
		/// Returns comment associated with this cell
		/// </summary>
		/// <value>The cell comment associated with this cell.</value>
		public IComment CellComment
		{
			get
			{
				if (comment == null)
				{
					comment = sheet.FindCellComment(record.Row, record.Column);
				}
				return comment;
			}
			set
			{
				if (value == null)
				{
					RemoveCellComment();
				}
				else
				{
					value.Row = record.Row;
					value.Column = record.Column;
					comment = value;
				}
			}
		}

		/// <summary>
		/// Gets the index of the column.
		/// </summary>
		/// <value>The index of the column.</value>
		public int ColumnIndex => record.Column & 0xFFFF;

		/// <summary>
		/// Gets the (zero based) index of the row containing this cell
		/// </summary>
		/// <value>The index of the row.</value>
		public int RowIndex => record.Row;

		/// <summary>
		/// Returns hyperlink associated with this cell
		/// </summary>
		/// <value>The hyperlink associated with this cell or null if not found</value>
		public IHyperlink Hyperlink
		{
			get
			{
				IEnumerator enumerator = sheet.Sheet.Records.GetEnumerator();
				while (enumerator.MoveNext())
				{
					RecordBase recordBase = (RecordBase)enumerator.Current;
					if (recordBase is HyperlinkRecord)
					{
						HyperlinkRecord hyperlinkRecord = (HyperlinkRecord)recordBase;
						if (hyperlinkRecord.FirstColumn == record.Column && hyperlinkRecord.FirstRow == record.Row)
						{
							return new HSSFHyperlink(hyperlinkRecord);
						}
					}
				}
				return null;
			}
			set
			{
				value.FirstRow = record.Row;
				value.LastRow = record.Row;
				value.FirstColumn = record.Column;
				value.LastColumn = record.Column;
				switch (value.Type)
				{
				case HyperlinkType.Url:
				case HyperlinkType.Email:
					value.Label = "url";
					break;
				case HyperlinkType.File:
					value.Label = "file";
					break;
				case HyperlinkType.Document:
					value.Label = "place";
					break;
				}
				int index = sheet.Sheet.FindFirstRecordLocBySid(10);
				sheet.Sheet.Records.Insert(index, ((HSSFHyperlink)value).record);
			}
		}

		/// <summary>
		/// Only valid for formula cells
		/// </summary>
		/// <value>one of (CellType.Numeric,CellType.String, CellType.Boolean, CellType.Error) depending
		/// on the cached value of the formula</value>
		public CellType CachedFormulaResultType
		{
			get
			{
				if (cellType != CellType.Formula)
				{
					throw new InvalidOperationException("Only formula cells have cached results");
				}
				return ((FormulaRecordAggregate)record).FormulaRecord.CachedResultType;
			}
		}

		public bool IsPartOfArrayFormulaGroup
		{
			get
			{
				if (cellType != CellType.Formula)
				{
					return false;
				}
				return ((FormulaRecordAggregate)record).IsPartOfArrayFormula;
			}
		}

		public CellRangeAddress ArrayFormulaRange
		{
			get
			{
				if (cellType != CellType.Formula)
				{
					string str = new CellReference(this).FormatAsString();
					throw new InvalidOperationException("Cell " + str + " is not part of an array formula.");
				}
				return ((FormulaRecordAggregate)record).GetArrayFormulaRange();
			}
		}

		public bool IsMergedCell
		{
			get
			{
				foreach (CellRangeAddress mergedRegion in sheet.Sheet.MergedRecords.MergedRegions)
				{
					if (mergedRegion.FirstColumn <= ColumnIndex && mergedRegion.LastColumn >= ColumnIndex && mergedRegion.FirstRow <= RowIndex && mergedRegion.LastRow >= RowIndex)
					{
						return true;
					}
				}
				return false;
			}
		}

		/// <summary>
		/// Creates new Cell - Should only be called by HSSFRow.  This Creates a cell
		/// from scratch.
		/// When the cell is initially Created it is Set to CellType.Blank. Cell types
		/// can be Changed/overwritten by calling SetCellValue with the appropriate
		/// type as a parameter although conversions from one type to another may be
		/// prohibited.
		/// </summary>
		/// <param name="book">Workbook record of the workbook containing this cell</param>
		/// <param name="sheet">Sheet record of the sheet containing this cell</param>
		/// <param name="row">the row of this cell</param>
		/// <param name="col">the column for this cell</param>
		public HSSFCell(HSSFWorkbook book, HSSFSheet sheet, int row, short col)
			: this(book, sheet, row, col, CellType.Blank)
		{
		}

		/// <summary>
		/// Creates new Cell - Should only be called by HSSFRow.  This Creates a cell
		/// from scratch.
		/// </summary>
		/// <param name="book">Workbook record of the workbook containing this cell</param>
		/// <param name="sheet">Sheet record of the sheet containing this cell</param>
		/// <param name="row">the row of this cell</param>
		/// <param name="col">the column for this cell</param>
		/// <param name="type">CellType.Numeric, CellType.String, CellType.Formula, CellType.Blank,
		/// CellType.Boolean, CellType.Error</param>
		public HSSFCell(HSSFWorkbook book, HSSFSheet sheet, int row, short col, CellType type)
		{
			CheckBounds(col);
			cellType = CellType.Unknown;
			stringValue = null;
			this.book = book;
			this.sheet = sheet;
			short xFIndexForColAt = sheet.Sheet.GetXFIndexForColAt(col);
			SetCellType(type, setValue: false, row, col, xFIndexForColAt);
		}

		/// <summary>
		/// Creates an Cell from a CellValueRecordInterface.  HSSFSheet uses this when
		/// reading in cells from an existing sheet.
		/// </summary>
		/// <param name="book">Workbook record of the workbook containing this cell</param>
		/// <param name="sheet">Sheet record of the sheet containing this cell</param>
		/// <param name="cval">the Cell Value Record we wish to represent</param>
		public HSSFCell(HSSFWorkbook book, HSSFSheet sheet, CellValueRecordInterface cval)
		{
			record = cval;
			cellType = DetermineType(cval);
			stringValue = null;
			this.book = book;
			this.sheet = sheet;
			switch (cellType)
			{
			case CellType.Blank:
				break;
			case CellType.String:
				stringValue = new HSSFRichTextString(book.Workbook, (LabelSSTRecord)cval);
				break;
			case CellType.Formula:
				stringValue = new HSSFRichTextString(((FormulaRecordAggregate)cval).StringValue);
				break;
			}
		}

		/// private constructor to prevent blank construction
		private HSSFCell()
		{
		}

		/// used internally -- given a cell value record, figure out its type
		private CellType DetermineType(CellValueRecordInterface cval)
		{
			if (!(cval is FormulaRecordAggregate))
			{
				NPOI.HSSF.Record.Record record = (NPOI.HSSF.Record.Record)cval;
				switch (record.Sid)
				{
				case 515:
					return CellType.Numeric;
				case 513:
					return CellType.Blank;
				case 253:
					return CellType.String;
				case -2000:
					return CellType.Formula;
				case 517:
				{
					BoolErrRecord boolErrRecord = (BoolErrRecord)record;
					if (!boolErrRecord.IsBoolean)
					{
						return CellType.Error;
					}
					return CellType.Boolean;
				}
				default:
					throw new Exception("Bad cell value rec (" + cval.GetType().Name + ")");
				}
			}
			return CellType.Formula;
		}

		/// <summary>
		/// Set the cells type (numeric, formula or string)
		/// </summary>
		/// <param name="cellType">Type of the cell.</param>
		public void SetCellType(CellType cellType)
		{
			NotifyFormulaChanging();
			if (IsPartOfArrayFormulaGroup)
			{
				NotifyArrayFormulaChanging();
			}
			int row = record.Row;
			int column = record.Column;
			short xFIndex = record.XFIndex;
			SetCellType(cellType, setValue: true, row, column, xFIndex);
		}

		/// <summary>
		/// Sets the cell type. The SetValue flag indicates whether to bother about
		/// trying to preserve the current value in the new record if one is Created.
		/// The SetCellValue method will call this method with false in SetValue
		/// since it will overWrite the cell value later
		/// </summary>
		/// <param name="cellType">Type of the cell.</param>
		/// <param name="setValue">if set to <c>true</c> [set value].</param>
		/// <param name="row">The row.</param>
		/// <param name="col">The col.</param>
		/// <param name="styleIndex">Index of the style.</param>
		private void SetCellType(CellType cellType, bool setValue, int row, int col, short styleIndex)
		{
			if (cellType > CellType.Error)
			{
				throw new Exception("I have no idea what type that Is!");
			}
			switch (cellType)
			{
			case CellType.Formula:
			{
				FormulaRecordAggregate formulaRecordAggregate = null;
				formulaRecordAggregate = ((cellType == this.cellType) ? ((FormulaRecordAggregate)record) : sheet.Sheet.RowsAggregate.CreateFormula(row, col));
				formulaRecordAggregate.Column = col;
				if (setValue)
				{
					formulaRecordAggregate.FormulaRecord.Value = NumericCellValue;
				}
				formulaRecordAggregate.XFIndex = styleIndex;
				formulaRecordAggregate.Row = row;
				record = formulaRecordAggregate;
				break;
			}
			case CellType.Numeric:
			{
				NumberRecord numberRecord = null;
				numberRecord = ((cellType == this.cellType) ? ((NumberRecord)record) : new NumberRecord());
				numberRecord.Column = col;
				if (setValue)
				{
					numberRecord.Value = NumericCellValue;
				}
				numberRecord.XFIndex = styleIndex;
				numberRecord.Row = row;
				record = numberRecord;
				break;
			}
			case CellType.String:
			{
				LabelSSTRecord labelSSTRecord = null;
				labelSSTRecord = ((cellType == this.cellType) ? ((LabelSSTRecord)record) : new LabelSSTRecord());
				labelSSTRecord.Column = col;
				labelSSTRecord.Row = row;
				labelSSTRecord.XFIndex = styleIndex;
				if (setValue)
				{
					string str = ConvertCellValueToString();
					int str2 = labelSSTRecord.SSTIndex = book.Workbook.AddSSTString(new UnicodeString(str));
					UnicodeString sSTString = book.Workbook.GetSSTString(str2);
					stringValue = new HSSFRichTextString();
					stringValue.UnicodeString = sSTString;
				}
				record = labelSSTRecord;
				break;
			}
			case CellType.Blank:
			{
				BlankRecord blankRecord = null;
				blankRecord = ((cellType == this.cellType) ? ((BlankRecord)record) : new BlankRecord());
				blankRecord.Column = col;
				blankRecord.XFIndex = styleIndex;
				blankRecord.Row = row;
				record = blankRecord;
				break;
			}
			case CellType.Boolean:
			{
				BoolErrRecord boolErrRecord2 = null;
				boolErrRecord2 = ((cellType == this.cellType) ? ((BoolErrRecord)record) : new BoolErrRecord());
				boolErrRecord2.Column = col;
				if (setValue)
				{
					boolErrRecord2.SetValue(ConvertCellValueToBoolean());
				}
				boolErrRecord2.XFIndex = styleIndex;
				boolErrRecord2.Row = row;
				record = boolErrRecord2;
				break;
			}
			case CellType.Error:
			{
				BoolErrRecord boolErrRecord = null;
				boolErrRecord = ((cellType == this.cellType) ? ((BoolErrRecord)record) : new BoolErrRecord());
				boolErrRecord.Column = col;
				if (setValue)
				{
					boolErrRecord.SetValue(15);
				}
				boolErrRecord.XFIndex = styleIndex;
				boolErrRecord.Row = row;
				record = boolErrRecord;
				break;
			}
			}
			if (cellType != this.cellType && this.cellType != CellType.Unknown)
			{
				sheet.Sheet.ReplaceValueRecord(record);
			}
			this.cellType = cellType;
		}

		private string ConvertCellValueToString()
		{
			switch (cellType)
			{
			case CellType.Blank:
				return "";
			case CellType.Boolean:
				if (!((BoolErrRecord)record).BooleanValue)
				{
					return "FALSE";
				}
				return "TRUE";
			case CellType.String:
			{
				int sSTIndex = ((LabelSSTRecord)record).SSTIndex;
				return book.Workbook.GetSSTString(sSTIndex).String;
			}
			case CellType.Numeric:
				return NumberToTextConverter.ToText(((NumberRecord)record).Value);
			case CellType.Error:
				return HSSFErrorConstants.GetText(((BoolErrRecord)record).ErrorValue);
			default:
				throw new InvalidDataException("Unexpected cell type (" + cellType + ")");
			case CellType.Formula:
			{
				FormulaRecordAggregate formulaRecordAggregate = (FormulaRecordAggregate)record;
				FormulaRecord formulaRecord = formulaRecordAggregate.FormulaRecord;
				switch (formulaRecord.CachedResultType)
				{
				case CellType.Boolean:
					if (!formulaRecord.CachedBooleanValue)
					{
						return "FALSE";
					}
					return "TRUE";
				case CellType.String:
					return formulaRecordAggregate.StringValue;
				case CellType.Numeric:
					return NumberToTextConverter.ToText(formulaRecord.Value);
				case CellType.Error:
					return HSSFErrorConstants.GetText(formulaRecord.CachedErrorValue);
				default:
					throw new InvalidDataException("Unexpected formula result type (" + cellType + ")");
				}
			}
			}
		}

		/// <summary>
		/// Set a numeric value for the cell
		/// </summary>
		/// <param name="value">the numeric value to Set this cell to.  For formulas we'll Set the
		/// precalculated value, for numerics we'll Set its value. For other types we
		/// will Change the cell to a numeric cell and Set its value.</param>
		public void SetCellValue(double value)
		{
			if (double.IsInfinity(value))
			{
				SetCellErrorValue(FormulaError.DIV0.Code);
			}
			else if (double.IsNaN(value))
			{
				SetCellErrorValue(FormulaError.NUM.Code);
			}
			else
			{
				int row = record.Row;
				int column = record.Column;
				short xFIndex = record.XFIndex;
				switch (cellType)
				{
				case CellType.Numeric:
					((NumberRecord)record).Value = value;
					break;
				case CellType.Formula:
					((FormulaRecordAggregate)record).SetCachedDoubleResult(value);
					break;
				default:
					SetCellType(CellType.Numeric, setValue: false, row, column, xFIndex);
					((NumberRecord)record).Value = value;
					break;
				}
			}
		}

		/// <summary>
		/// Set a date value for the cell. Excel treats dates as numeric so you will need to format the cell as
		/// a date.
		/// </summary>
		/// <param name="value">the date value to Set this cell to.  For formulas we'll Set the
		/// precalculated value, for numerics we'll Set its value. For other types we
		/// will Change the cell to a numeric cell and Set its value.</param>
		public void SetCellValue(DateTime value)
		{
			SetCellValue(DateUtil.GetExcelDate(value, book.Workbook.IsUsing1904DateWindowing));
		}

		/// <summary>
		/// Set a string value for the cell. Please note that if you are using
		/// full 16 bit Unicode you should call SetEncoding() first.
		/// </summary>
		/// <param name="value">value to Set the cell to.  For formulas we'll Set the formula
		/// string, for String cells we'll Set its value.  For other types we will
		/// Change the cell to a string cell and Set its value.
		/// If value is null then we will Change the cell to a Blank cell.</param>
		public void SetCellValue(string value)
		{
			HSSFRichTextString cellValue = new HSSFRichTextString(value);
			SetCellValue(cellValue);
		}

		/// set a error value for the cell
		///
		/// @param errorCode the error value to set this cell to.  For formulas we'll set the
		///        precalculated value , for errors we'll set
		///        its value. For other types we will change the cell to an error
		///        cell and set its value.
		public void SetCellErrorValue(byte errorCode)
		{
			int row = record.Row;
			int column = record.Column;
			short xFIndex = record.XFIndex;
			switch (cellType)
			{
			case CellType.Error:
				((BoolErrRecord)record).SetValue(errorCode);
				break;
			case CellType.Formula:
				((FormulaRecordAggregate)record).SetCachedErrorResult(errorCode);
				break;
			default:
				SetCellType(CellType.Error, setValue: false, row, column, xFIndex);
				((BoolErrRecord)record).SetValue(errorCode);
				break;
			}
		}

		/// <summary>
		/// Set a string value for the cell. Please note that if you are using
		/// full 16 bit Unicode you should call SetEncoding() first.
		/// </summary>
		/// <param name="value">value to Set the cell to.  For formulas we'll Set the formula
		/// string, for String cells we'll Set its value.  For other types we will
		/// Change the cell to a string cell and Set its value.
		/// If value is null then we will Change the cell to a Blank cell.</param>
		public void SetCellValue(IRichTextString value)
		{
			HSSFRichTextString hSSFRichTextString = (HSSFRichTextString)value;
			int row = record.Row;
			int column = record.Column;
			short xFIndex = record.XFIndex;
			if (hSSFRichTextString == null)
			{
				NotifyFormulaChanging();
				SetCellType(CellType.Blank, setValue: false, row, column, xFIndex);
			}
			else
			{
				if (hSSFRichTextString.Length > SpreadsheetVersion.EXCEL97.MaxTextLength)
				{
					throw new ArgumentException("The maximum length of cell contents (text) is 32,767 characters");
				}
				if (cellType == CellType.Formula)
				{
					FormulaRecordAggregate formulaRecordAggregate = (FormulaRecordAggregate)record;
					formulaRecordAggregate.SetCachedStringResult(value.String);
					stringValue = new HSSFRichTextString(value.String);
				}
				else
				{
					if (cellType != CellType.String)
					{
						SetCellType(CellType.String, setValue: false, row, column, xFIndex);
					}
					int num = 0;
					UnicodeString unicodeString = hSSFRichTextString.UnicodeString;
					num = book.Workbook.AddSSTString(unicodeString);
					((LabelSSTRecord)record).SSTIndex = num;
					stringValue = hSSFRichTextString;
					stringValue.SetWorkbookReferences(book.Workbook, (LabelSSTRecord)record);
					stringValue.UnicodeString = book.Workbook.GetSSTString(num);
				}
			}
		}

		/// Should be called any time that a formula could potentially be deleted.
		/// Does nothing if this cell currently does not hold a formula
		private void NotifyFormulaChanging()
		{
			if (record is FormulaRecordAggregate)
			{
				((FormulaRecordAggregate)record).NotifyFormulaChanging();
			}
		}

		public void SetCellFormula(string formula)
		{
			if (IsPartOfArrayFormulaGroup)
			{
				NotifyArrayFormulaChanging();
			}
			int row = record.Row;
			int column = record.Column;
			short xFIndex = record.XFIndex;
			if (string.IsNullOrEmpty(formula))
			{
				NotifyFormulaChanging();
				SetCellType(CellType.Blank, setValue: false, row, column, xFIndex);
			}
			else
			{
				int sheetIndex = book.GetSheetIndex(sheet);
				Ptg[] parsedExpression = HSSFFormulaParser.Parse(formula, book, FormulaType.Cell, sheetIndex);
				SetCellType(CellType.Formula, setValue: false, row, column, xFIndex);
				FormulaRecordAggregate formulaRecordAggregate = (FormulaRecordAggregate)record;
				FormulaRecord formulaRecord = formulaRecordAggregate.FormulaRecord;
				formulaRecord.Options = 2;
				formulaRecord.Value = 0.0;
				if (formulaRecordAggregate.XFIndex == 0)
				{
					formulaRecordAggregate.XFIndex = 15;
				}
				formulaRecordAggregate.SetParsedExpression(parsedExpression);
			}
		}

		/// <summary>
		/// Used to help format error messages
		/// </summary>
		/// <param name="cellTypeCode">The cell type code.</param>
		/// <returns></returns>
		private string GetCellTypeName(CellType cellTypeCode)
		{
			switch (cellTypeCode)
			{
			case CellType.Blank:
				return "blank";
			case CellType.String:
				return "text";
			case CellType.Boolean:
				return "boolean";
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

		/// <summary>
		/// Types the mismatch.
		/// </summary>
		/// <param name="expectedTypeCode">The expected type code.</param>
		/// <param name="actualTypeCode">The actual type code.</param>
		/// <param name="isFormulaCell">if set to <c>true</c> [is formula cell].</param>
		/// <returns></returns>
		private Exception TypeMismatch(CellType expectedTypeCode, CellType actualTypeCode, bool isFormulaCell)
		{
			string message = "Cannot get a " + GetCellTypeName(expectedTypeCode) + " value from a " + GetCellTypeName(actualTypeCode) + " " + (isFormulaCell ? "formula " : "") + "cell";
			return new InvalidOperationException(message);
		}

		/// <summary>
		/// Checks the type of the formula cached value.
		/// </summary>
		/// <param name="expectedTypeCode">The expected type code.</param>
		/// <param name="fr">The fr.</param>
		private void CheckFormulaCachedValueType(CellType expectedTypeCode, FormulaRecord fr)
		{
			CellType cachedResultType = fr.CachedResultType;
			if (cachedResultType != expectedTypeCode)
			{
				throw TypeMismatch(expectedTypeCode, cachedResultType, isFormulaCell: true);
			}
		}

		/// <summary>
		/// Set a bool value for the cell
		/// </summary>
		/// <param name="value">the bool value to Set this cell to.  For formulas we'll Set the
		/// precalculated value, for bools we'll Set its value. For other types we
		/// will Change the cell to a bool cell and Set its value.</param>
		public void SetCellValue(bool value)
		{
			int row = record.Row;
			int column = record.Column;
			short xFIndex = record.XFIndex;
			switch (cellType)
			{
			case CellType.Boolean:
				((BoolErrRecord)record).SetValue(value);
				break;
			case CellType.Formula:
				((FormulaRecordAggregate)record).SetCachedBooleanResult(value);
				break;
			default:
				SetCellType(CellType.Boolean, setValue: false, row, column, xFIndex);
				((BoolErrRecord)record).SetValue(value);
				break;
			}
		}

		/// <summary>
		/// Chooses a new bool value for the cell when its type is changing.
		/// Usually the caller is calling SetCellType() with the intention of calling
		/// SetCellValue(bool) straight afterwards.  This method only exists to give
		/// the cell a somewhat reasonable value until the SetCellValue() call (if at all).
		/// TODO - perhaps a method like SetCellTypeAndValue(int, Object) should be introduced to avoid this
		/// </summary>
		/// <returns></returns>
		private bool ConvertCellValueToBoolean()
		{
			switch (cellType)
			{
			case CellType.Boolean:
				return ((BoolErrRecord)record).BooleanValue;
			case CellType.String:
			{
				int sSTIndex = ((LabelSSTRecord)record).SSTIndex;
				string @string = book.Workbook.GetSSTString(sSTIndex).String;
				return Convert.ToBoolean(@string, CultureInfo.CurrentCulture);
			}
			case CellType.Numeric:
				return ((NumberRecord)record).Value != 0.0;
			case CellType.Formula:
			{
				FormulaRecord formulaRecord = ((FormulaRecordAggregate)record).FormulaRecord;
				CheckFormulaCachedValueType(CellType.Boolean, formulaRecord);
				return formulaRecord.CachedBooleanValue;
			}
			case CellType.Blank:
			case CellType.Error:
				return false;
			default:
				throw new Exception("Unexpected cell type (" + cellType + ")");
			}
		}

		/// Applying a user-defined style (UDS) is special. Excel does not directly reference user-defined styles, but
		/// instead create a 'proxy' ExtendedFormatRecord referencing the UDS as parent.
		///
		/// The proceudre to apply a UDS is as follows:
		///
		/// 1. search for a ExtendedFormatRecord with parentIndex == style.getIndex()
		///    and xfType ==  ExtendedFormatRecord.XF_CELL.
		/// 2. if not found then create a new ExtendedFormatRecord and copy all attributes from the user-defined style
		///    and set the parentIndex to be style.getIndex()
		/// 3. return the index of the ExtendedFormatRecord, this will be assigned to the parent cell record
		///
		/// @param style  the user style to apply
		///
		/// @return  the index of a ExtendedFormatRecord record that will be referenced by the cell
		private short ApplyUserCellStyle(HSSFCellStyle style)
		{
			if (style.UserStyleName == null)
			{
				throw new ArgumentException("Expected user-defined style");
			}
			InternalWorkbook workbook = book.Workbook;
			short num = -1;
			int numExFormats = workbook.NumExFormats;
			for (short num2 = 0; num2 < numExFormats; num2 = (short)(num2 + 1))
			{
				ExtendedFormatRecord exFormatAt = workbook.GetExFormatAt(num2);
				if (exFormatAt.XFType == 0 && exFormatAt.ParentIndex == style.Index)
				{
					num = num2;
					break;
				}
			}
			if (num == -1)
			{
				ExtendedFormatRecord extendedFormatRecord = workbook.CreateCellXF();
				extendedFormatRecord.CloneStyleFrom(workbook.GetExFormatAt(style.Index));
				extendedFormatRecord.IndentionOptions = 0;
				extendedFormatRecord.XFType = 0;
				extendedFormatRecord.ParentIndex = style.Index;
				return (short)numExFormats;
			}
			return num;
		}

		/// <summary>
		/// Checks the bounds.
		/// </summary>
		/// <param name="cellIndex">The cell num.</param>
		/// <exception cref="T:System.Exception">if the bounds are exceeded.</exception>
		private void CheckBounds(int cellIndex)
		{
			if (cellIndex < 0 || cellIndex > LAST_COLUMN_NUMBER)
			{
				throw new ArgumentException("Invalid column index (" + cellIndex + ").  Allowable column range for BIFF8 is (0.." + LAST_COLUMN_NUMBER + ") or ('A'..'" + LAST_COLUMN_NAME + "')");
			}
		}

		/// <summary>
		/// Sets this cell as the active cell for the worksheet
		/// </summary>
		public void SetAsActiveCell()
		{
			int row = record.Row;
			int column = record.Column;
			sheet.Sheet.SetActiveCell(row, column);
		}

		/// <summary>
		/// Returns a string representation of the cell
		/// This method returns a simple representation,
		/// anthing more complex should be in user code, with
		/// knowledge of the semantics of the sheet being Processed.
		/// Formula cells return the formula string,
		/// rather than the formula result.
		/// Dates are Displayed in dd-MMM-yyyy format
		/// Errors are Displayed as #ERR&lt;errIdx&gt;
		/// </summary>
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
				return ErrorEval.GetText(((BoolErrRecord)record).ErrorValue);
			case CellType.Formula:
				return CellFormula;
			case CellType.Numeric:
			{
				CellStyle.GetDataFormatString();
				DataFormatter dataFormatter = new DataFormatter();
				return dataFormatter.FormatCellValue(this);
			}
			case CellType.String:
				return StringCellValue;
			default:
				return "Unknown Cell Type: " + CellType;
			}
		}

		/// <summary>
		/// Removes the comment for this cell, if
		/// there is one.
		/// </summary>
		/// <remarks>WARNING - some versions of excel will loose
		/// all comments after performing this action!</remarks>
		public void RemoveCellComment()
		{
			HSSFComment hSSFComment = sheet.FindCellComment(record.Row, record.Column);
			comment = null;
			if (hSSFComment != null)
			{
				(sheet.DrawingPatriarch as HSSFPatriarch).RemoveShape(hSSFComment);
			}
		}

		/// Updates the cell record's idea of what
		///  column it belongs in (0 based)
		/// @param num the new cell number
		internal void UpdateCellNum(int num)
		{
			record.Column = num;
		}

		internal void SetCellArrayFormula(CellRangeAddress range)
		{
			int row = record.Row;
			int column = record.Column;
			short xFIndex = record.XFIndex;
			SetCellType(CellType.Formula, setValue: false, row, column, xFIndex);
			Ptg[] parsedExpression = new Ptg[1]
			{
				new ExpPtg(range.FirstRow, range.FirstColumn)
			};
			FormulaRecordAggregate formulaRecordAggregate = (FormulaRecordAggregate)record;
			formulaRecordAggregate.SetParsedExpression(parsedExpression);
		}

		public ICell CopyCellTo(int targetIndex)
		{
			return Row.CopyCell(ColumnIndex, targetIndex);
		}

		/// <summary>
		/// The purpose of this method is to validate the cell state prior to modification
		/// </summary>
		/// <param name="msg"></param>
		internal void NotifyArrayFormulaChanging(string msg)
		{
			CellRangeAddress arrayFormulaRange = ArrayFormulaRange;
			if (arrayFormulaRange.NumberOfCells > 1)
			{
				throw new InvalidOperationException(msg);
			}
			Row.Sheet.RemoveArrayFormula(this);
		}

		/// <summary>
		/// Called when this cell is modified.
		/// The purpose of this method is to validate the cell state prior to modification.
		/// </summary>
		internal void NotifyArrayFormulaChanging()
		{
			CellReference cellReference = new CellReference(this);
			string msg = "Cell " + cellReference.FormatAsString() + " is part of a multi-cell array formula. You cannot change part of an array.";
			NotifyArrayFormulaChanging(msg);
		}
	}
}
