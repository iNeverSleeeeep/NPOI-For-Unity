using NPOI.SS.Formula;
using NPOI.SS.UserModel;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// HSSF wrapper for a cell under evaluation
	/// @author Josh Micich
	/// </summary>
	public class HSSFEvaluationCell : IEvaluationCell
	{
		private IEvaluationSheet _evalSheet;

		private ICell _cell;

		public ICell HSSFCell => _cell;

		public bool BooleanCellValue => _cell.BooleanCellValue;

		public CellType CellType => _cell.CellType;

		public int ColumnIndex => _cell.ColumnIndex;

		public int ErrorCellValue => _cell.ErrorCellValue;

		public double NumericCellValue => _cell.NumericCellValue;

		public int RowIndex => _cell.RowIndex;

		public IEvaluationSheet Sheet => _evalSheet;

		public string StringCellValue => _cell.RichStringCellValue.String;

		public object IdentityKey => _cell;

		public CellType CachedFormulaResultType => _cell.CachedFormulaResultType;

		public HSSFEvaluationCell(ICell cell, IEvaluationSheet evalSheet)
		{
			_cell = cell;
			_evalSheet = evalSheet;
		}

		public HSSFEvaluationCell(ICell cell)
		{
			_cell = cell;
			_evalSheet = new HSSFEvaluationSheet((HSSFSheet)cell.Sheet);
		}

		public override int GetHashCode()
		{
			return _cell.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			ICell cell = ((HSSFEvaluationCell)obj)._cell;
			if (_cell.RowIndex == cell.RowIndex && _cell.ColumnIndex == cell.ColumnIndex && _cell.CellFormula == cell.CellFormula)
			{
				return _cell.Sheet == cell.Sheet;
			}
			return false;
		}
	}
}
