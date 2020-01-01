using NPOI.SS.Formula;
using NPOI.SS.UserModel;

namespace NPOI.HSSF.UserModel
{
	/// HSSF wrapper for a sheet under evaluation
	///
	/// @author Josh Micich
	public class HSSFEvaluationSheet : IEvaluationSheet
	{
		private HSSFSheet _hs;

		public HSSFSheet HSSFSheet => _hs;

		public HSSFEvaluationSheet(HSSFSheet hs)
		{
			_hs = hs;
		}

		public IEvaluationCell GetCell(int rowIndex, int columnIndex)
		{
			HSSFRow hSSFRow = (HSSFRow)_hs.GetRow(rowIndex);
			if (hSSFRow == null)
			{
				return null;
			}
			ICell cell = (HSSFCell)hSSFRow.GetCell(columnIndex);
			if (cell == null)
			{
				return null;
			}
			return new HSSFEvaluationCell(cell, this);
		}
	}
}
