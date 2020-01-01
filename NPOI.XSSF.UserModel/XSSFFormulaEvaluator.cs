using NPOI.HSSF.UserModel;
using NPOI.SS.Formula;
using NPOI.SS.Formula.Eval;
using NPOI.SS.Formula.Udf;
using NPOI.SS.UserModel;
using System;

namespace NPOI.XSSF.UserModel
{
	/// Evaluates formula cells.<p />
	///
	/// For performance reasons, this class keeps a cache of all previously calculated intermediate
	/// cell values.  Be sure to call {@link #ClearAllCachedResultValues()} if any workbook cells are Changed between
	/// calls to Evaluate~ methods on this class.
	///
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	/// @author Josh Micich
	public class XSSFFormulaEvaluator : IFormulaEvaluator
	{
		private WorkbookEvaluator _bookEvaluator;

		private XSSFWorkbook _book;

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

		public XSSFFormulaEvaluator(IWorkbook workbook)
			: this(workbook as XSSFWorkbook, null, null)
		{
		}

		public XSSFFormulaEvaluator(XSSFWorkbook workbook)
			: this(workbook, null, null)
		{
		}

		/// @param stabilityClassifier used to optimise caching performance. Pass <code>null</code>
		/// for the (conservative) assumption that any cell may have its defInition Changed After
		/// Evaluation begins.
		/// @deprecated (Sep 2009) (reduce overloading) use {@link #Create(XSSFWorkbook, NPOI.ss.formula.IStabilityClassifier, NPOI.ss.formula.udf.UDFFinder)}
		public XSSFFormulaEvaluator(XSSFWorkbook workbook, IStabilityClassifier stabilityClassifier)
		{
			_bookEvaluator = new WorkbookEvaluator(XSSFEvaluationWorkbook.Create(workbook), stabilityClassifier, null);
			_book = workbook;
		}

		private XSSFFormulaEvaluator(XSSFWorkbook workbook, IStabilityClassifier stabilityClassifier, UDFFinder udfFinder)
		{
			_bookEvaluator = new WorkbookEvaluator(XSSFEvaluationWorkbook.Create(workbook), stabilityClassifier, udfFinder);
			_book = workbook;
		}

		/// @param stabilityClassifier used to optimise caching performance. Pass <code>null</code>
		/// for the (conservative) assumption that any cell may have its defInition Changed After
		/// Evaluation begins.
		/// @param udfFinder pass <code>null</code> for default (AnalysisToolPak only)
		public static XSSFFormulaEvaluator Create(XSSFWorkbook workbook, IStabilityClassifier stabilityClassifier, UDFFinder udfFinder)
		{
			return new XSSFFormulaEvaluator(workbook, stabilityClassifier, udfFinder);
		}

		/// Should be called whenever there are major Changes (e.g. moving sheets) to input cells
		/// in the Evaluated workbook.
		/// Failure to call this method After changing cell values will cause incorrect behaviour
		/// of the Evaluate~ methods of this class
		public void ClearAllCachedResultValues()
		{
			_bookEvaluator.ClearAllCachedResultValues();
		}

		public void NotifySetFormula(ICell cell)
		{
			_bookEvaluator.NotifyUpdateCell(new XSSFEvaluationCell((XSSFCell)cell));
		}

		public void NotifyDeleteCell(ICell cell)
		{
			_bookEvaluator.NotifyDeleteCell(new XSSFEvaluationCell((XSSFCell)cell));
		}

		public void NotifyUpdateCell(ICell cell)
		{
			_bookEvaluator.NotifyUpdateCell(new XSSFEvaluationCell((XSSFCell)cell));
		}

		/// If cell Contains a formula, the formula is Evaluated and returned,
		/// else the CellValue simply copies the appropriate cell value from
		/// the cell and also its cell type. This method should be preferred over
		/// EvaluateInCell() when the call should not modify the contents of the
		/// original cell.
		/// @param cell
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

		/// If cell Contains formula, it Evaluates the formula,
		///  and saves the result of the formula. The cell
		///  remains as a formula cell.
		/// Else if cell does not contain formula, this method leaves
		///  the cell unChanged.
		/// Note that the type of the formula result is returned,
		///  so you know what kind of value is also stored with
		///  the formula.
		/// <pre>
		/// int EvaluatedCellType = Evaluator.EvaluateFormulaCell(cell);
		/// </pre>
		/// Be aware that your cell will hold both the formula,
		///  and the result. If you want the cell Replaced with
		///  the result of the formula, use {@link #Evaluate(NPOI.ss.usermodel.Cell)} }
		/// @param cell The cell to Evaluate
		/// @return The type of the formula result (the cell's type remains as HSSFCell.CELL_TYPE_FORMULA however)
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

		/// If cell Contains formula, it Evaluates the formula, and
		///  Puts the formula result back into the cell, in place
		///  of the old formula.
		/// Else if cell does not contain formula, this method leaves
		///  the cell unChanged.
		/// Note that the same instance of HSSFCell is returned to
		/// allow chained calls like:
		/// <pre>
		/// int EvaluatedCellType = Evaluator.EvaluateInCell(cell).CellType;
		/// </pre>
		/// Be aware that your cell value will be Changed to hold the
		///  result of the formula. If you simply want the formula
		///  value computed for you, use {@link #EvaluateFormulaCell(NPOI.ss.usermodel.Cell)} }
		/// @param cell
		public ICell EvaluateInCell(ICell cell)
		{
			if (cell == null)
			{
				return null;
			}
			XSSFCell result = (XSSFCell)cell;
			if (cell.CellType == CellType.Formula)
			{
				CellValue cv = EvaluateFormulaCellValue(cell);
				SetCellType(cell, cv);
				SetCellValue(cell, cv);
			}
			return result;
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
				cell.SetCellValue(new XSSFRichTextString(cv.StringValue));
				break;
			default:
				throw new InvalidOperationException("Unexpected cell value type (" + cellType + ")");
			}
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
		public static void EvaluateAllFormulaCells(IWorkbook wb)
		{
			HSSFFormulaEvaluator.EvaluateAllFormulaCells(wb);
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
		public void EvaluateAll()
		{
			HSSFFormulaEvaluator.EvaluateAllFormulaCells(_book);
		}

		/// Returns a CellValue wrapper around the supplied ValueEval instance.
		private CellValue EvaluateFormulaCellValue(ICell cell)
		{
			if (!(cell is XSSFCell))
			{
				throw new ArgumentException("Unexpected type of cell: " + cell.GetType() + ". Only XSSFCells can be Evaluated.");
			}
			ValueEval valueEval = _bookEvaluator.Evaluate(new XSSFEvaluationCell((XSSFCell)cell));
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
			throw new Exception("Unexpected eval class (" + valueEval.GetType().Name + ")");
		}
	}
}
