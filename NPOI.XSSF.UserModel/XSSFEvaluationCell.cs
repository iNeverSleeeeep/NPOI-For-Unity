using NPOI.SS.Formula;
using NPOI.SS.UserModel;

namespace NPOI.XSSF.UserModel
{
	/// XSSF wrapper for a cell under Evaluation
	///
	/// @author Josh Micich
	public class XSSFEvaluationCell : IEvaluationCell
	{
		private IEvaluationSheet _evalSheet;

		private XSSFCell _cell;

		public object IdentityKey
		{
			get
			{
				return _cell;
			}
		}

		public bool BooleanCellValue
		{
			get
			{
				return _cell.BooleanCellValue;
			}
		}

		public CellType CellType
		{
			get
			{
				return _cell.CellType;
			}
		}

		public int ColumnIndex
		{
			get
			{
				return _cell.ColumnIndex;
			}
		}

		public int ErrorCellValue
		{
			get
			{
				return _cell.ErrorCellValue;
			}
		}

		public double NumericCellValue
		{
			get
			{
				return _cell.NumericCellValue;
			}
		}

		public int RowIndex
		{
			get
			{
				return _cell.RowIndex;
			}
		}

		public IEvaluationSheet Sheet
		{
			get
			{
				return _evalSheet;
			}
		}

		public string StringCellValue
		{
			get
			{
				return _cell.RichStringCellValue.String;
			}
		}

		public CellType CachedFormulaResultType
		{
			get
			{
				return _cell.CachedFormulaResultType;
			}
		}

		public XSSFEvaluationCell(ICell cell, XSSFEvaluationSheet EvaluationSheet)
		{
			_cell = (XSSFCell)cell;
			_evalSheet = EvaluationSheet;
		}

		public XSSFEvaluationCell(ICell cell)
			: this(cell, new XSSFEvaluationSheet(cell.Sheet))
		{
		}

		public XSSFCell GetXSSFCell()
		{
			return _cell;
		}
	}
}
