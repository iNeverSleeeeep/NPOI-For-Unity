using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using System;

namespace NPOI.SS.Formula
{
	/// Performance optimisation for {@link HSSFFormulaEvaluator}. This class stores previously
	/// calculated values of already visited cells, To avoid unnecessary re-calculation when the 
	/// same cells are referenced multiple times
	///
	///
	/// @author Josh Micich
	public class EvaluationCache
	{
		public class EntryOperation : IEntryOperation
		{
			private BookSheetKey bsk;

			private int rowIndex;

			private int columnIndex;

			private IEvaluationListener evaluationListener;

			public EntryOperation(BookSheetKey bsk, int rowIndex, int columnIndex, IEvaluationListener evaluationListener)
			{
				this.bsk = bsk;
				this.evaluationListener = evaluationListener;
				this.rowIndex = rowIndex;
				this.columnIndex = columnIndex;
			}

			public void ProcessEntry(FormulaCellCacheEntry entry)
			{
				entry.NotifyUpdatedBlankCell(bsk, rowIndex, columnIndex, evaluationListener);
			}
		}

		private PlainCellCache _plainCellCache;

		private FormulaCellCache _formulaCellCache;

		/// only used for testing. <c>null</c> otherwise 
		private IEvaluationListener _evaluationListener;

		public EvaluationCache(IEvaluationListener evaluationListener)
		{
			_evaluationListener = evaluationListener;
			_plainCellCache = new PlainCellCache();
			_formulaCellCache = new FormulaCellCache();
		}

		public void NotifyUpdateCell(int bookIndex, int sheetIndex, IEvaluationCell cell)
		{
			FormulaCellCacheEntry formulaCellCacheEntry = _formulaCellCache.Get(cell);
			int rowIndex = cell.RowIndex;
			int columnIndex = cell.ColumnIndex;
			Loc key = new Loc(bookIndex, sheetIndex, cell.RowIndex, cell.ColumnIndex);
			PlainValueCellCacheEntry plainValueCellCacheEntry = _plainCellCache.Get(key);
			if (cell.CellType == CellType.Formula)
			{
				if (formulaCellCacheEntry == null)
				{
					formulaCellCacheEntry = new FormulaCellCacheEntry();
					if (plainValueCellCacheEntry == null)
					{
						if (_evaluationListener != null)
						{
							_evaluationListener.OnChangeFromBlankValue(sheetIndex, rowIndex, columnIndex, cell, formulaCellCacheEntry);
						}
						UpdateAnyBlankReferencingFormulas(bookIndex, sheetIndex, rowIndex, columnIndex);
					}
					_formulaCellCache.Put(cell, formulaCellCacheEntry);
				}
				else
				{
					formulaCellCacheEntry.RecurseClearCachedFormulaResults(_evaluationListener);
					formulaCellCacheEntry.ClearFormulaEntry();
				}
				if (plainValueCellCacheEntry != null)
				{
					plainValueCellCacheEntry.RecurseClearCachedFormulaResults(_evaluationListener);
					_plainCellCache.Remove(key);
				}
			}
			else
			{
				ValueEval valueFromNonFormulaCell = WorkbookEvaluator.GetValueFromNonFormulaCell(cell);
				if (plainValueCellCacheEntry == null)
				{
					if (valueFromNonFormulaCell != BlankEval.instance)
					{
						plainValueCellCacheEntry = new PlainValueCellCacheEntry(valueFromNonFormulaCell);
						if (formulaCellCacheEntry == null)
						{
							if (_evaluationListener != null)
							{
								_evaluationListener.OnChangeFromBlankValue(sheetIndex, rowIndex, columnIndex, cell, plainValueCellCacheEntry);
							}
							UpdateAnyBlankReferencingFormulas(bookIndex, sheetIndex, rowIndex, columnIndex);
						}
						_plainCellCache.Put(key, plainValueCellCacheEntry);
					}
				}
				else
				{
					if (plainValueCellCacheEntry.UpdateValue(valueFromNonFormulaCell))
					{
						plainValueCellCacheEntry.RecurseClearCachedFormulaResults(_evaluationListener);
					}
					if (valueFromNonFormulaCell == BlankEval.instance)
					{
						_plainCellCache.Remove(key);
					}
				}
				if (formulaCellCacheEntry != null)
				{
					_formulaCellCache.Remove(cell);
					formulaCellCacheEntry.SetSensitiveInputCells(null);
					formulaCellCacheEntry.RecurseClearCachedFormulaResults(_evaluationListener);
				}
			}
		}

		private void UpdateAnyBlankReferencingFormulas(int bookIndex, int sheetIndex, int rowIndex, int columnIndex)
		{
			BookSheetKey bsk = new BookSheetKey(bookIndex, sheetIndex);
			_formulaCellCache.ApplyOperation(new EntryOperation(bsk, rowIndex, columnIndex, _evaluationListener));
		}

		public PlainValueCellCacheEntry GetPlainValueEntry(int bookIndex, int sheetIndex, int rowIndex, int columnIndex, ValueEval value)
		{
			Loc key = new Loc(bookIndex, sheetIndex, rowIndex, columnIndex);
			PlainValueCellCacheEntry plainValueCellCacheEntry = _plainCellCache.Get(key);
			if (plainValueCellCacheEntry == null)
			{
				plainValueCellCacheEntry = new PlainValueCellCacheEntry(value);
				_plainCellCache.Put(key, plainValueCellCacheEntry);
				if (_evaluationListener != null)
				{
					_evaluationListener.OnReadPlainValue(sheetIndex, rowIndex, columnIndex, plainValueCellCacheEntry);
				}
			}
			else
			{
				if (!AreValuesEqual(plainValueCellCacheEntry.GetValue(), value))
				{
					throw new InvalidOperationException("value changed");
				}
				if (_evaluationListener != null)
				{
					_evaluationListener.OnCacheHit(sheetIndex, rowIndex, columnIndex, value);
				}
			}
			return plainValueCellCacheEntry;
		}

		private bool AreValuesEqual(ValueEval a, ValueEval b)
		{
			if (a == null)
			{
				return false;
			}
			Type type = a.GetType();
			if (type != b.GetType())
			{
				return false;
			}
			if (a == BlankEval.instance)
			{
				return b == a;
			}
			if (type == typeof(NumberEval))
			{
				return ((NumberEval)a).NumberValue == ((NumberEval)b).NumberValue;
			}
			if (type == typeof(StringEval))
			{
				return ((StringEval)a).StringValue.Equals(((StringEval)b).StringValue);
			}
			if (type == typeof(BoolEval))
			{
				return ((BoolEval)a).BooleanValue == ((BoolEval)b).BooleanValue;
			}
			if (type == typeof(ErrorEval))
			{
				return ((ErrorEval)a).ErrorCode == ((ErrorEval)b).ErrorCode;
			}
			throw new InvalidOperationException("Unexpected value class (" + type.Name + ")");
		}

		public FormulaCellCacheEntry GetOrCreateFormulaCellEntry(IEvaluationCell cell)
		{
			FormulaCellCacheEntry formulaCellCacheEntry = _formulaCellCache.Get(cell);
			if (formulaCellCacheEntry == null)
			{
				formulaCellCacheEntry = new FormulaCellCacheEntry();
				_formulaCellCache.Put(cell, formulaCellCacheEntry);
			}
			return formulaCellCacheEntry;
		}

		/// Should be called whenever there are Changes To input cells in the evaluated workbook.
		public void Clear()
		{
			if (_evaluationListener != null)
			{
				_evaluationListener.OnClearWholeCache();
			}
			_plainCellCache.Clear();
			_formulaCellCache.Clear();
		}

		public void NotifyDeleteCell(int bookIndex, int sheetIndex, IEvaluationCell cell)
		{
			if (cell.CellType == CellType.Formula)
			{
				FormulaCellCacheEntry formulaCellCacheEntry = _formulaCellCache.Remove(cell);
				if (formulaCellCacheEntry != null)
				{
					formulaCellCacheEntry.SetSensitiveInputCells(null);
					formulaCellCacheEntry.RecurseClearCachedFormulaResults(_evaluationListener);
				}
			}
			else
			{
				Loc key = new Loc(bookIndex, sheetIndex, cell.RowIndex, cell.ColumnIndex);
				_plainCellCache.Get(key)?.RecurseClearCachedFormulaResults(_evaluationListener);
			}
		}
	}
}
