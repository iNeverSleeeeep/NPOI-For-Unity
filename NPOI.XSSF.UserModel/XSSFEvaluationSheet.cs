using NPOI.SS.Formula;
using NPOI.SS.UserModel;

namespace NPOI.XSSF.UserModel
{
	/// XSSF wrapper for a sheet under Evaluation
	///
	/// @author Josh Micich
	public class XSSFEvaluationSheet : IEvaluationSheet
	{
		private XSSFSheet _xs;

		public XSSFEvaluationSheet(ISheet sheet)
		{
			_xs = (XSSFSheet)sheet;
		}

		public XSSFSheet GetXSSFSheet()
		{
			return _xs;
		}

		public IEvaluationCell GetCell(int rowIndex, int columnIndex)
		{
			IRow row = _xs.GetRow(rowIndex);
			if (row == null)
			{
				return null;
			}
			ICell cell = row.GetCell(columnIndex);
			if (cell == null)
			{
				return null;
			}
			return new XSSFEvaluationCell(cell, this);
		}
	}
}
