using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.Functions;
using NPOI.SS.Formula.PTG;
using NPOI.SS.Util;
using NPOI.Util;
using System;
using System.Globalization;

namespace NPOI.SS.Formula
{
	/// Contains all the contextual information required to Evaluate an operation
	/// within a formula
	///
	/// For POI internal use only
	///
	/// @author Josh Micich
	public class OperationEvaluationContext
	{
		public static readonly FreeRefFunction UDF = UserDefinedFunction.instance;

		private IEvaluationWorkbook _workbook;

		private int _sheetIndex;

		private int _rowIndex;

		private int _columnIndex;

		private EvaluationTracker _tracker;

		private WorkbookEvaluator _bookEvaluator;

		public int RowIndex => _rowIndex;

		public int ColumnIndex => _columnIndex;

		public OperationEvaluationContext(WorkbookEvaluator bookEvaluator, IEvaluationWorkbook workbook, int sheetIndex, int srcRowNum, int srcColNum, EvaluationTracker tracker)
		{
			_bookEvaluator = bookEvaluator;
			_workbook = workbook;
			_sheetIndex = sheetIndex;
			_rowIndex = srcRowNum;
			_columnIndex = srcColNum;
			_tracker = tracker;
		}

		public IEvaluationWorkbook GetWorkbook()
		{
			return _workbook;
		}

		private SheetRefEvaluator CreateExternSheetRefEvaluator(IExternSheetReferenceToken ptg)
		{
			return CreateExternSheetRefEvaluator(ptg.ExternSheetIndex);
		}

		private SheetRefEvaluator CreateExternSheetRefEvaluator(int externSheetIndex)
		{
			ExternalSheet externalSheet = _workbook.GetExternalSheet(externSheetIndex);
			int num;
			WorkbookEvaluator workbookEvaluator;
			if (externalSheet == null)
			{
				num = _workbook.ConvertFromExternSheetIndex(externSheetIndex);
				workbookEvaluator = _bookEvaluator;
			}
			else
			{
				string workbookName = externalSheet.GetWorkbookName();
				try
				{
					workbookEvaluator = _bookEvaluator.GetOtherWorkbookEvaluator(workbookName);
				}
				catch (WorkbookNotFoundException ex)
				{
					throw new RuntimeException(ex.Message, ex);
				}
				num = workbookEvaluator.GetSheetIndex(externalSheet.GetSheetName());
				if (num < 0)
				{
					throw new Exception("Invalid sheet name '" + externalSheet.GetSheetName() + "' in bool '" + workbookName + "'.");
				}
			}
			return new SheetRefEvaluator(workbookEvaluator, _tracker, num);
		}

		/// @return <code>null</code> if either workbook or sheet is not found
		private SheetRefEvaluator CreateExternSheetRefEvaluator(string workbookName, string sheetName)
		{
			WorkbookEvaluator workbookEvaluator;
			if (workbookName == null)
			{
				workbookEvaluator = _bookEvaluator;
			}
			else
			{
				if (sheetName == null)
				{
					throw new ArgumentException("sheetName must not be null if workbookName is provided");
				}
				try
				{
					workbookEvaluator = _bookEvaluator.GetOtherWorkbookEvaluator(workbookName);
				}
				catch (WorkbookNotFoundException)
				{
					return null;
				}
			}
			int num = (sheetName == null) ? _sheetIndex : workbookEvaluator.GetSheetIndex(sheetName);
			if (num < 0)
			{
				return null;
			}
			return new SheetRefEvaluator(workbookEvaluator, _tracker, num);
		}

		public SheetRefEvaluator GetRefEvaluatorForCurrentSheet()
		{
			return new SheetRefEvaluator(_bookEvaluator, _tracker, _sheetIndex);
		}

		/// Resolves a cell or area reference dynamically.
		/// @param workbookName the name of the workbook Containing the reference.  If <code>null</code>
		/// the current workbook is assumed.  Note - to Evaluate formulas which use multiple workbooks,
		/// a {@link CollaboratingWorkbooksEnvironment} must be set up.
		/// @param sheetName the name of the sheet Containing the reference.  May be <code>null</code>
		/// (when <c>workbookName</c> is also null) in which case the current workbook and sheet is
		/// assumed.
		/// @param refStrPart1 the single cell reference or first part of the area reference.  Must not
		/// be <code>null</code>.
		/// @param refStrPart2 the second part of the area reference. For single cell references this
		/// parameter must be <code>null</code>
		/// @param isA1Style specifies the format for <c>refStrPart1</c> and <c>refStrPart2</c>.
		/// Pass <c>true</c> for 'A1' style and <c>false</c> for 'R1C1' style.
		/// TODO - currently POI only supports 'A1' reference style
		/// @return a {@link RefEval} or {@link AreaEval}
		public ValueEval GetDynamicReference(string workbookName, string sheetName, string refStrPart1, string refStrPart2, bool isA1Style)
		{
			if (!isA1Style)
			{
				throw new Exception("R1C1 style not supported yet");
			}
			SheetRefEvaluator sheetRefEvaluator = CreateExternSheetRefEvaluator(workbookName, sheetName);
			if (sheetRefEvaluator != null)
			{
				SpreadsheetVersion spreadsheetVersion = ((IFormulaParsingWorkbook)_workbook).GetSpreadsheetVersion();
				NameType nameType = ClassifyCellReference(refStrPart1, spreadsheetVersion);
				switch (nameType)
				{
				case NameType.BadCellOrNamedRange:
					return ErrorEval.REF_INVALID;
				case NameType.NamedRange:
				{
					IEvaluationName name = ((IFormulaParsingWorkbook)_workbook).GetName(refStrPart1, _sheetIndex);
					if (!name.IsRange)
					{
						throw new Exception("Specified name '" + refStrPart1 + "' is not a range as expected.");
					}
					return _bookEvaluator.EvaluateNameFormula(name.NameDefinition, this);
				}
				default:
					if (refStrPart2 != null)
					{
						NameType nameType2 = ClassifyCellReference(refStrPart1, spreadsheetVersion);
						switch (nameType2)
						{
						case NameType.BadCellOrNamedRange:
							return ErrorEval.REF_INVALID;
						case NameType.NamedRange:
							throw new Exception("Cannot Evaluate '" + refStrPart1 + "'. Indirect Evaluation of defined names not supported yet");
						default:
						{
							if (nameType2 != nameType)
							{
								return ErrorEval.REF_INVALID;
							}
							int firstRowIndex;
							int lastRowIndex;
							int firstColumnIndex;
							int lastColumnIndex;
							switch (nameType)
							{
							case NameType.Column:
								firstRowIndex = 0;
								lastRowIndex = spreadsheetVersion.LastRowIndex;
								firstColumnIndex = ParseColRef(refStrPart1);
								lastColumnIndex = ParseColRef(refStrPart2);
								break;
							case NameType.Row:
								firstColumnIndex = 0;
								lastColumnIndex = spreadsheetVersion.LastColumnIndex;
								firstRowIndex = ParseRowRef(refStrPart1);
								lastRowIndex = ParseRowRef(refStrPart2);
								break;
							case NameType.Cell:
							{
								CellReference cellReference = new CellReference(refStrPart1);
								firstRowIndex = cellReference.Row;
								firstColumnIndex = cellReference.Col;
								cellReference = new CellReference(refStrPart2);
								lastRowIndex = cellReference.Row;
								lastColumnIndex = cellReference.Col;
								break;
							}
							default:
								throw new InvalidOperationException("Unexpected reference classification of '" + refStrPart1 + "'.");
							}
							return new LazyAreaEval(firstRowIndex, firstColumnIndex, lastRowIndex, lastColumnIndex, sheetRefEvaluator);
						}
						}
					}
					switch (nameType)
					{
					case NameType.Column:
					case NameType.Row:
						return ErrorEval.REF_INVALID;
					case NameType.Cell:
					{
						CellReference cellReference2 = new CellReference(refStrPart1);
						return new LazyRefEval(cellReference2.Row, cellReference2.Col, sheetRefEvaluator);
					}
					default:
						throw new InvalidOperationException("Unexpected reference classification of '" + refStrPart1 + "'.");
					}
				}
			}
			return ErrorEval.REF_INVALID;
		}

		private static int ParseRowRef(string refStrPart)
		{
			return CellReference.ConvertColStringToIndex(refStrPart);
		}

		private static int ParseColRef(string refStrPart)
		{
			return int.Parse(refStrPart, CultureInfo.InvariantCulture) - 1;
		}

		private static NameType ClassifyCellReference(string str, SpreadsheetVersion ssVersion)
		{
			int length = str.Length;
			if (length < 1)
			{
				return NameType.BadCellOrNamedRange;
			}
			return CellReference.ClassifyCellReference(str, ssVersion);
		}

		public FreeRefFunction FindUserDefinedFunction(string functionName)
		{
			return _bookEvaluator.FindUserDefinedFunction(functionName);
		}

		public ValueEval GetRefEval(int rowIndex, int columnIndex)
		{
			SheetRefEvaluator refEvaluatorForCurrentSheet = GetRefEvaluatorForCurrentSheet();
			return new LazyRefEval(rowIndex, columnIndex, refEvaluatorForCurrentSheet);
		}

		public ValueEval GetRef3DEval(int rowIndex, int columnIndex, int extSheetIndex)
		{
			SheetRefEvaluator sre = CreateExternSheetRefEvaluator(extSheetIndex);
			return new LazyRefEval(rowIndex, columnIndex, sre);
		}

		public ValueEval GetAreaEval(int firstRowIndex, int firstColumnIndex, int lastRowIndex, int lastColumnIndex)
		{
			SheetRefEvaluator refEvaluatorForCurrentSheet = GetRefEvaluatorForCurrentSheet();
			return new LazyAreaEval(firstRowIndex, firstColumnIndex, lastRowIndex, lastColumnIndex, refEvaluatorForCurrentSheet);
		}

		public ValueEval GetArea3DEval(int firstRowIndex, int firstColumnIndex, int lastRowIndex, int lastColumnIndex, int extSheetIndex)
		{
			SheetRefEvaluator evaluator = CreateExternSheetRefEvaluator(extSheetIndex);
			return new LazyAreaEval(firstRowIndex, firstColumnIndex, lastRowIndex, lastColumnIndex, evaluator);
		}

		public ValueEval GetNameXEval(NameXPtg nameXPtg)
		{
			ExternalSheet externalSheet = _workbook.GetExternalSheet(nameXPtg.SheetRefIndex);
			if (externalSheet != null)
			{
				string workbookName = externalSheet.GetWorkbookName();
				ExternalName externalName = _workbook.GetExternalName(nameXPtg.SheetRefIndex, nameXPtg.NameIndex);
				try
				{
					WorkbookEvaluator otherWorkbookEvaluator = _bookEvaluator.GetOtherWorkbookEvaluator(workbookName);
					IEvaluationName name = otherWorkbookEvaluator.GetName(externalName.Name, externalName.Ix - 1);
					if (name != null && name.HasFormula)
					{
						if (name.NameDefinition.Length > 1)
						{
							throw new Exception("Complex name formulas not supported yet");
						}
						Ptg ptg = name.NameDefinition[0];
						if (ptg is Ref3DPtg)
						{
							Ref3DPtg ref3DPtg = (Ref3DPtg)ptg;
							int sheetIndexByExternIndex = otherWorkbookEvaluator.GetSheetIndexByExternIndex(ref3DPtg.ExternSheetIndex);
							string sheetName = otherWorkbookEvaluator.GetSheetName(sheetIndexByExternIndex);
							SheetRefEvaluator sre = CreateExternSheetRefEvaluator(workbookName, sheetName);
							return new LazyRefEval(ref3DPtg.Row, ref3DPtg.Column, sre);
						}
						if (ptg is Area3DPtg)
						{
							Area3DPtg area3DPtg = (Area3DPtg)ptg;
							int sheetIndexByExternIndex2 = otherWorkbookEvaluator.GetSheetIndexByExternIndex(area3DPtg.ExternSheetIndex);
							string sheetName2 = otherWorkbookEvaluator.GetSheetName(sheetIndexByExternIndex2);
							SheetRefEvaluator evaluator = CreateExternSheetRefEvaluator(workbookName, sheetName2);
							return new LazyAreaEval(area3DPtg.FirstRow, area3DPtg.FirstColumn, area3DPtg.LastRow, area3DPtg.LastColumn, evaluator);
						}
					}
					return ErrorEval.REF_INVALID;
				}
				catch (WorkbookNotFoundException)
				{
					return ErrorEval.REF_INVALID;
				}
			}
			return new NameXEval(nameXPtg);
		}
	}
}
