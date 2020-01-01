using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Udf;
using NPOI.SS.UserModel;
using System;

namespace NPOI.SS.Formula.Eval.Forked
{
	/// An alternative workbook Evaluator that saves memory in situations where a single workbook is
	/// concurrently and independently Evaluated many times.  With standard formula Evaluation, around
	/// 90% of memory consumption is due to loading of the {@link HSSFWorkbook} or {@link NPOI.xssf.usermodel.XSSFWorkbook}.
	/// This class enables a 'master workbook' to be loaded just once and shared between many Evaluation
	/// clients.  Each Evaluation client Creates its own {@link ForkedEvaluator} and can Set cell values
	/// that will be used for local Evaluations (and don't disturb Evaluations on other Evaluators).
	///
	/// @author Josh Micich
	public class ForkedEvaluator
	{
		private WorkbookEvaluator _evaluator;

		private ForkedEvaluationWorkbook _sewb;

		private ForkedEvaluator(IEvaluationWorkbook masterWorkbook, IStabilityClassifier stabilityClassifier, UDFFinder udfFinder)
		{
			_sewb = new ForkedEvaluationWorkbook(masterWorkbook);
			_evaluator = new WorkbookEvaluator(_sewb, stabilityClassifier, udfFinder);
		}

		private static IEvaluationWorkbook CreateEvaluationWorkbook(IWorkbook wb)
		{
			if (wb is HSSFWorkbook)
			{
				return HSSFEvaluationWorkbook.Create((HSSFWorkbook)wb);
			}
			throw new ArgumentException("Unexpected workbook type (" + wb.GetType().Name + ")");
		}

		/// @deprecated (Sep 2009) (reduce overloading) use {@link #Create(Workbook, IStabilityClassifier, UDFFinder)}
		public static ForkedEvaluator Create(IWorkbook wb, IStabilityClassifier stabilityClassifier)
		{
			return Create(wb, stabilityClassifier, null);
		}

		/// @param udfFinder pass <code>null</code> for default (AnalysisToolPak only)
		public static ForkedEvaluator Create(IWorkbook wb, IStabilityClassifier stabilityClassifier, UDFFinder udfFinder)
		{
			return new ForkedEvaluator(CreateEvaluationWorkbook(wb), stabilityClassifier, udfFinder);
		}

		/// Sets the specified cell to the supplied <tt>value</tt>
		/// @param sheetName the name of the sheet Containing the cell
		/// @param rowIndex zero based
		/// @param columnIndex zero based
		public void UpdateCell(string sheetName, int rowIndex, int columnIndex, ValueEval value)
		{
			ForkedEvaluationCell orCreateUpdatableCell = _sewb.GetOrCreateUpdatableCell(sheetName, rowIndex, columnIndex);
			orCreateUpdatableCell.SetValue(value);
			_evaluator.NotifyUpdateCell(orCreateUpdatableCell);
		}

		/// Copies the values of all updated cells (modified by calls to {@link
		/// #updateCell(String, int, int, ValueEval)}) to the supplied <tt>workbook</tt>.<br />
		/// Typically, the supplied <tt>workbook</tt> is a writable copy of the 'master workbook',
		/// but at the very least it must contain sheets with the same names.
		public void CopyUpdatedCells(IWorkbook workbook)
		{
			_sewb.CopyUpdatedCells(workbook);
		}

		/// If cell Contains a formula, the formula is Evaluated and returned,
		/// else the CellValue simply copies the appropriate cell value from
		/// the cell and also its cell type. This method should be preferred over
		/// EvaluateInCell() when the call should not modify the contents of the
		/// original cell.
		///
		/// @param sheetName the name of the sheet Containing the cell
		/// @param rowIndex zero based
		/// @param columnIndex zero based
		/// @return <code>null</code> if the supplied cell is <code>null</code> or blank
		public ValueEval Evaluate(string sheetName, int rowIndex, int columnIndex)
		{
			IEvaluationCell evaluationCell = _sewb.GetEvaluationCell(sheetName, rowIndex, columnIndex);
			switch (evaluationCell.CellType)
			{
			case CellType.Boolean:
				return BoolEval.ValueOf(evaluationCell.BooleanCellValue);
			case CellType.Error:
				return ErrorEval.ValueOf(evaluationCell.ErrorCellValue);
			case CellType.Formula:
				return _evaluator.Evaluate(evaluationCell);
			case CellType.Numeric:
				return new NumberEval(evaluationCell.NumericCellValue);
			case CellType.String:
				return new StringEval(evaluationCell.StringCellValue);
			case CellType.Blank:
				return null;
			default:
				throw new InvalidOperationException("Bad cell type (" + evaluationCell.CellType + ")");
			}
		}

		/// Coordinates several formula Evaluators together so that formulas that involve external
		/// references can be Evaluated.
		/// @param workbookNames the simple file names used to identify the workbooks in formulas
		/// with external links (for example "MyData.xls" as used in a formula "[MyData.xls]Sheet1!A1")
		/// @param Evaluators all Evaluators for the full Set of workbooks required by the formulas.
		public static void SetupEnvironment(string[] workbookNames, ForkedEvaluator[] Evaluators)
		{
			WorkbookEvaluator[] array = new WorkbookEvaluator[Evaluators.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Evaluators[i]._evaluator;
			}
			CollaboratingWorkbooksEnvironment.Setup(workbookNames, array);
		}
	}
}
