using NPOI.SS.Formula.Eval;
using System.Collections;

namespace NPOI.SS.Formula
{
	/// Stores the cached result of a formula evaluation, along with the Set of sensititive input cells
	///
	/// @author Josh Micich
	public class FormulaCellCacheEntry : CellCacheEntry
	{
		public new static FormulaCellCacheEntry[] EMPTY_ARRAY = new FormulaCellCacheEntry[0];

		/// Cells 'used' in the current evaluation of the formula corresponding To this cache entry
		///
		/// If any of the following cells Change, this cache entry needs To be Cleared
		private CellCacheEntry[] _sensitiveInputCells;

		private FormulaUsedBlankCellSet _usedBlankCellGroup;

		public bool IsInputSensitive
		{
			get
			{
				if (_sensitiveInputCells != null && _sensitiveInputCells.Length > 0)
				{
					return true;
				}
				if (_usedBlankCellGroup != null)
				{
					return !_usedBlankCellGroup.IsEmpty;
				}
				return false;
			}
		}

		public void SetSensitiveInputCells(CellCacheEntry[] sensitiveInputCells)
		{
			ChangeConsumingCells((sensitiveInputCells == null) ? CellCacheEntry.EMPTY_ARRAY : sensitiveInputCells);
			_sensitiveInputCells = sensitiveInputCells;
		}

		public void ClearFormulaEntry()
		{
			CellCacheEntry[] sensitiveInputCells = _sensitiveInputCells;
			if (sensitiveInputCells != null)
			{
				for (int num = sensitiveInputCells.Length - 1; num >= 0; num--)
				{
					sensitiveInputCells[num].ClearConsumingCell(this);
				}
			}
			_sensitiveInputCells = null;
			ClearValue();
		}

		private void ChangeConsumingCells(CellCacheEntry[] usedCells)
		{
			CellCacheEntry[] sensitiveInputCells = _sensitiveInputCells;
			int num = usedCells.Length;
			for (int i = 0; i < num; i++)
			{
				usedCells[i].AddConsumingCell(this);
			}
			if (sensitiveInputCells != null)
			{
				int num2 = sensitiveInputCells.Length;
				if (num2 >= 1)
				{
					ArrayList arrayList;
					if (num < 1)
					{
						arrayList = new ArrayList();
					}
					else
					{
						arrayList = new ArrayList(num * 3 / 2);
						for (int j = 0; j < num; j++)
						{
							arrayList.Add(usedCells[j]);
						}
					}
					for (int k = 0; k < num2; k++)
					{
						CellCacheEntry cellCacheEntry = sensitiveInputCells[k];
						if (!arrayList.Contains(cellCacheEntry))
						{
							cellCacheEntry.ClearConsumingCell(this);
						}
					}
				}
			}
		}

		public void UpdateFormulaResult(ValueEval result, CellCacheEntry[] sensitiveInputCells, FormulaUsedBlankCellSet usedBlankAreas)
		{
			UpdateValue(result);
			SetSensitiveInputCells(sensitiveInputCells);
			_usedBlankCellGroup = usedBlankAreas;
		}

		public void NotifyUpdatedBlankCell(BookSheetKey bsk, int rowIndex, int columnIndex, IEvaluationListener evaluationListener)
		{
			if (_usedBlankCellGroup != null && _usedBlankCellGroup.ContainsCell(bsk, rowIndex, columnIndex))
			{
				ClearFormulaEntry();
				RecurseClearCachedFormulaResults(evaluationListener);
			}
		}
	}
}
