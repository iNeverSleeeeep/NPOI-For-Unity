using NPOI.SS.Formula.Eval;
using System.Collections;
using System.Text;

namespace NPOI.SS.Formula
{
	/// Stores details about the current evaluation of a cell.<br />
	internal class CellEvaluationFrame
	{
		private FormulaCellCacheEntry _cce;

		private ArrayList _sensitiveInputCells;

		private FormulaUsedBlankCellSet _usedBlankCellGroup;

		public CellEvaluationFrame(FormulaCellCacheEntry cce)
		{
			_cce = cce;
			_sensitiveInputCells = new ArrayList();
		}

		public CellCacheEntry GetCCE()
		{
			return _cce;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append(GetType().Name).Append(" [");
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		/// @param inputCell a cell directly used by the formula of this evaluation frame
		public void AddSensitiveInputCell(CellCacheEntry inputCell)
		{
			_sensitiveInputCells.Add(inputCell);
		}

		/// @return never <c>null</c>, (possibly empty) array of all cells directly used while 
		/// evaluating the formula of this frame.
		private CellCacheEntry[] GetSensitiveInputCells()
		{
			int count = _sensitiveInputCells.Count;
			if (count < 1)
			{
				return CellCacheEntry.EMPTY_ARRAY;
			}
			CellCacheEntry[] array = new CellCacheEntry[count];
			return (CellCacheEntry[])_sensitiveInputCells.ToArray(typeof(CellCacheEntry));
		}

		public void AddUsedBlankCell(int bookIndex, int sheetIndex, int rowIndex, int columnIndex)
		{
			if (_usedBlankCellGroup == null)
			{
				_usedBlankCellGroup = new FormulaUsedBlankCellSet();
			}
			_usedBlankCellGroup.AddCell(bookIndex, sheetIndex, rowIndex, columnIndex);
		}

		public void UpdateFormulaResult(ValueEval result)
		{
			_cce.UpdateFormulaResult(result, GetSensitiveInputCells(), _usedBlankCellGroup);
		}
	}
}
