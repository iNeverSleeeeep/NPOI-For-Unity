using NPOI.SS.Formula;
using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.PTG;
using NPOI.SS.Formula.Udf;
using NPOI.SS.UserModel;
using System;
using System.Collections;

namespace NPOI.HSSF.UserModel
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public class HSSFFormulaEvaluator : IFormulaEvaluator
	{
		private WorkbookEvaluator _bookEvaluator;

		private static Type[] VALUE_CONTRUCTOR_CLASS_ARRAY;

		private static Type[] AREA3D_CONSTRUCTOR_CLASS_ARRAY;

		private static Type[] REFERENCE_CONSTRUCTOR_CLASS_ARRAY;

		private static Type[] REF3D_CONSTRUCTOR_CLASS_ARRAY;

		private static Hashtable VALUE_EVALS_MAP;

		protected IRow row;

		protected ISheet sheet;

		protected IWorkbook workbook;

		/// Whether to ignore missing references to external workbooks and
		/// use cached formula results in the main workbook instead.
		/// <p>
		/// In some cases exetrnal workbooks referenced by formulas in the main workbook are not avaiable.
		/// With this method you can control how POI handles such missing references:
		/// <ul>
		///     <li>by default ignoreMissingWorkbooks=false and POI throws {@link org.apache.poi.ss.formula.CollaboratingWorkbooksEnvironment.WorkbookNotFoundException}
		///     if an external reference cannot be resolved</li>
		///     <li>if ignoreMissingWorkbooks=true then POI uses cached formula result
		///     that already exists in the main workbook</li>
		/// </ul>
		///             </p>
		/// @param ignore whether to ignore missing references to external workbooks
		public bool IgnoreMissingWorkbooks
		{
			get
			{
				return _bookEvaluator.IgnoreMissingWorkbooks;
			}
			set
			{
				_bookEvaluator.IgnoreMissingWorkbooks = value;
			}
		}

		/// {@inheritDoc} 
		public bool DebugEvaluationOutputForNextEval
		{
			get
			{
				return _bookEvaluator.DebugEvaluationOutputForNextEval;
			}
			set
			{
				_bookEvaluator.DebugEvaluationOutputForNextEval = value;
			}
		}

		static HSSFFormulaEvaluator()
		{
			VALUE_CONTRUCTOR_CLASS_ARRAY = new Type[1]
			{
				typeof(Ptg)
			};
			AREA3D_CONSTRUCTOR_CLASS_ARRAY = new Type[2]
			{
				typeof(Ptg),
				typeof(ValueEval[])
			};
			REFERENCE_CONSTRUCTOR_CLASS_ARRAY = new Type[2]
			{
				typeof(Ptg),
				typeof(ValueEval)
			};
			REF3D_CONSTRUCTOR_CLASS_ARRAY = new Type[2]
			{
				typeof(Ptg),
				typeof(ValueEval)
			};
			VALUE_EVALS_MAP = new Hashtable();
			VALUE_EVALS_MAP[typeof(BoolPtg)] = typeof(BoolEval);
			VALUE_EVALS_MAP[typeof(IntPtg)] = typeof(NumberEval);
			VALUE_EVALS_MAP[typeof(NumberPtg)] = typeof(NumberEval);
			VALUE_EVALS_MAP[typeof(StringPtg)] = typeof(StringEval);
		}

		[Obsolete]
		public HSSFFormulaEvaluator(ISheet sheet, IWorkbook workbook)
			: this(workbook)
		{
			this.sheet = sheet;
			this.workbook = workbook;
		}

		public HSSFFormulaEvaluator(IWorkbook workbook)
			: this(workbook, null)
		{
			this.workbook = workbook;
		}

		/// @param stabilityClassifier used to optimise caching performance. Pass <code>null</code>
		/// for the (conservative) assumption that any cell may have its definition changed after
		/// evaluation begins.
		public HSSFFormulaEvaluator(IWorkbook workbook, IStabilityClassifier stabilityClassifier)
			: this(workbook, stabilityClassifier, null)
		{
		}

		/// @param udfFinder pass <code>null</code> for default (AnalysisToolPak only)
		public HSSFFormulaEvaluator(IWorkbook workbook, IStabilityClassifier stabilityClassifier, UDFFinder udfFinder)
		{
			_bookEvaluator = new WorkbookEvaluator(HSSFEvaluationWorkbook.Create(workbook), stabilityClassifier, udfFinder);
		}

		/// @param stabilityClassifier used to optimise caching performance. Pass <code>null</code>
		/// for the (conservative) assumption that any cell may have its definition changed after
		/// evaluation begins.
		/// @param udfFinder pass <code>null</code> for default (AnalysisToolPak only)
		public static HSSFFormulaEvaluator Create(IWorkbook workbook, IStabilityClassifier stabilityClassifier, UDFFinder udfFinder)
		{
			return new HSSFFormulaEvaluator(workbook, stabilityClassifier, udfFinder);
		}

		private static void SetCellType(ICell cell, CellValue cv)
		{
			CellType cellType = cv.CellType;
			switch (cellType)
			{
			case CellType.Numeric:
			case CellType.String:
			case CellType.Boolean:
			case CellType.Error:
				cell.SetCellType(cellType);
				break;
			default:
				throw new InvalidOperationException("Unexpected cell value type (" + cellType + ")");
			}
		}

		private static void SetCellValue(ICell cell, CellValue cv)
		{
			CellType cellType = cv.CellType;
			switch (cellType)
			{
			case CellType.Boolean:
				cell.SetCellValue(cv.BooleanValue);
				break;
			case CellType.Error:
				cell.SetCellErrorValue((byte)cv.ErrorValue);
				break;
			case CellType.Numeric:
				cell.SetCellValue(cv.NumberValue);
				break;
			case CellType.String:
				cell.SetCellValue(new HSSFRichTextString(cv.StringValue));
				break;
			default:
				throw new InvalidOperationException("Unexpected cell value type (" + cellType + ")");
			}
		}

		/// Coordinates several formula evaluators together so that formulas that involve external
		/// references can be evaluated.
		/// @param workbookNames the simple file names used to identify the workbooks in formulas
		/// with external links (for example "MyData.xls" as used in a formula "[MyData.xls]Sheet1!A1")
		/// @param evaluators all evaluators for the full set of workbooks required by the formulas. 
		public static void SetupEnvironment(string[] workbookNames, HSSFFormulaEvaluator[] evaluators)
		{
			WorkbookEvaluator[] array = new WorkbookEvaluator[evaluators.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = evaluators[i]._bookEvaluator;
			}
			CollaboratingWorkbooksEnvironment.Setup(workbookNames, array);
		}

		/// If cell Contains a formula, the formula is Evaluated and returned,
		/// else the CellValue simply copies the appropriate cell value from
		/// the cell and also its cell type. This method should be preferred over
		/// EvaluateInCell() when the call should not modify the contents of the
		/// original cell. 
		/// @param cell
		/// If cell contains a formula, the formula is evaluated and returned,
		/// else the CellValue simply copies the appropriate cell value from
		/// the cell and also its cell type. This method should be preferred over
		/// evaluateInCell() when the call should not modify the contents of the
		/// original cell.
		///
		/// @param cell may be <c>null</c> signifying that the cell is not present (or blank)
		/// @return <c>null</c> if the supplied cell is <c>null</c> or blank
		public CellValue Evaluate(ICell cell)
		{
			if (cell != null)
			{
				switch (cell.CellType)
				{
				case CellType.Boolean:
					return CellValue.ValueOf(cell.BooleanCellValue);
				case CellType.Error:
					return CellValue.GetError(cell.ErrorCellValue);
				case CellType.Formula:
					return EvaluateFormulaCellValue(cell);
				case CellType.Numeric:
					return new CellValue(cell.NumericCellValue);
				case CellType.String:
					return new CellValue(cell.RichStringCellValue.String);
				case CellType.Blank:
					return null;
				default:
					throw new InvalidOperationException("Bad cell type (" + cell.CellType + ")");
				}
			}
			return null;
		}

		/// Should be called whenever there are major changes (e.g. moving sheets) to input cells
		/// in the evaluated workbook.  If performance is not critical, a single call to this method
		/// may be used instead of many specific calls to the notify~ methods.
		///
		/// Failure to call this method after changing cell values will cause incorrect behaviour
		/// of the evaluate~ methods of this class
		public void ClearAllCachedResultValues()
		{
			_bookEvaluator.ClearAllCachedResultValues();
		}

		/// Should be called to tell the cell value cache that the specified (value or formula) cell 
		/// has changed.
		/// Failure to call this method after changing cell values will cause incorrect behaviour
		/// of the evaluate~ methods of this class
		public void NotifyUpdateCell(ICell cell)
		{
			_bookEvaluator.NotifyUpdateCell(new HSSFEvaluationCell(cell));
		}

		/// Should be called to tell the cell value cache that the specified cell has just been
		/// deleted. 
		/// Failure to call this method after changing cell values will cause incorrect behaviour
		/// of the evaluate~ methods of this class
		public void NotifyDeleteCell(ICell cell)
		{
			_bookEvaluator.NotifyDeleteCell(new HSSFEvaluationCell(cell));
		}

		/// Should be called to tell the cell value cache that the specified (value or formula) cell
		/// has changed.
		/// Failure to call this method after changing cell values will cause incorrect behaviour
		/// of the evaluate~ methods of this class
		public void NotifySetFormula(ICell cell)
		{
			_bookEvaluator.NotifyUpdateCell(new HSSFEvaluationCell(cell));
		}

		/// If cell Contains formula, it Evaluates the formula,
		///  and saves the result of the formula. The cell
		///  remains as a formula cell.
		/// Else if cell does not contain formula, this method leaves
		///  the cell UnChanged. 
		/// Note that the type of the formula result is returned,
		///  so you know what kind of value is also stored with
		///  the formula. 
		/// <pre>
		/// int EvaluatedCellType = evaluator.EvaluateFormulaCell(cell);
		/// </pre>
		/// Be aware that your cell will hold both the formula,
		///  and the result. If you want the cell Replaced with
		///  the result of the formula, use {@link #EvaluateInCell(HSSFCell)}
		/// @param cell The cell to Evaluate
		/// @return The type of the formula result (the cell's type remains as CellType.Formula however)
		public CellType EvaluateFormulaCell(ICell cell)
		{
			if (cell == null || cell.CellType != CellType.Formula)
			{
				return CellType.Unknown;
			}
			CellValue cellValue = EvaluateFormulaCellValue(cell);
			SetCellValue(cell, cellValue);
			return cellValue.CellType;
		}

		/// Returns a CellValue wrapper around the supplied ValueEval instance.
		/// @param eval
		private CellValue EvaluateFormulaCellValue(ICell cell)
		{
			ValueEval valueEval = _bookEvaluator.Evaluate(new HSSFEvaluationCell((HSSFCell)cell));
			if (valueEval is NumberEval)
			{
				NumberEval numberEval = (NumberEval)valueEval;
				return new CellValue(numberEval.NumberValue);
			}
			if (valueEval is BoolEval)
			{
				BoolEval boolEval = (BoolEval)valueEval;
				return CellValue.ValueOf(boolEval.BooleanValue);
			}
			if (valueEval is StringEval)
			{
				StringEval stringEval = (StringEval)valueEval;
				return new CellValue(stringEval.StringValue);
			}
			if (valueEval is ErrorEval)
			{
				return CellValue.GetError(((ErrorEval)valueEval).ErrorCode);
			}
			throw new InvalidOperationException("Unexpected eval class (" + valueEval.GetType().Name + ")");
		}

		/// If cell Contains formula, it Evaluates the formula, and
		///  puts the formula result back into the cell, in place
		///  of the old formula.
		/// Else if cell does not contain formula, this method leaves
		///  the cell UnChanged. 
		/// Note that the same instance of Cell is returned to 
		/// allow chained calls like:
		/// <pre>
		/// int EvaluatedCellType = evaluator.EvaluateInCell(cell).CellType;
		/// </pre>
		/// Be aware that your cell value will be Changed to hold the
		///  result of the formula. If you simply want the formula
		///  value computed for you, use {@link #EvaluateFormulaCell(HSSFCell)}
		/// @param cell
		public ICell EvaluateInCell(ICell cell)
		{
			if (cell == null)
			{
				return null;
			}
			if (cell.CellType == CellType.Formula)
			{
				CellValue cv = EvaluateFormulaCellValue(cell);
				SetCellValue(cell, cv);
				SetCellType(cell, cv);
			}
			return cell;
		}

		/// Loops over all cells in all sheets of the supplied
		///  workbook.
		/// For cells that contain formulas, their formulas are
		///  Evaluated, and the results are saved. These cells
		///  remain as formula cells.
		/// For cells that do not contain formulas, no Changes
		///  are made.
		/// This is a helpful wrapper around looping over all 
		///  cells, and calling EvaluateFormulaCell on each one.
		public static void EvaluateAllFormulaCells(HSSFWorkbook wb)
		{
			EvaluateAllFormulaCells(wb, new HSSFFormulaEvaluator(wb));
		}

		/// Loops over all cells in all sheets of the supplied
		///  workbook.
		/// For cells that contain formulas, their formulas are
		///  evaluated, and the results are saved. These cells
		///  remain as formula cells.
		/// For cells that do not contain formulas, no changes
		///  are made.
		/// This is a helpful wrapper around looping over all
		///  cells, and calling evaluateFormulaCell on each one.
		public static void EvaluateAllFormulaCells(IWorkbook wb)
		{
			IFormulaEvaluator evaluator = wb.GetCreationHelper().CreateFormulaEvaluator();
			EvaluateAllFormulaCells(wb, evaluator);
		}

		private static void EvaluateAllFormulaCells(IWorkbook wb, IFormulaEvaluator evaluator)
		{
			for (int i = 0; i < wb.NumberOfSheets; i++)
			{
				ISheet sheetAt = wb.GetSheetAt(i);
				IEnumerator rowEnumerator = sheetAt.GetRowEnumerator();
				while (rowEnumerator.MoveNext())
				{
					IRow row = (IRow)rowEnumerator.Current;
					foreach (ICell cell in row.Cells)
					{
						if (cell.CellType == CellType.Formula)
						{
							evaluator.EvaluateFormulaCell(cell);
						}
					}
				}
			}
		}

		public void EvaluateAll()
		{
			EvaluateAllFormulaCells(workbook, this);
		}
	}
}
