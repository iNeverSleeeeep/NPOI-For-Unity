using NPOI.SS.Formula.Eval;
using System;

namespace NPOI.SS.Formula
{
	/// Stores the parameters that identify the evaluation of one cell.<br />
	public abstract class CellCacheEntry : ICacheEntry
	{
		public static CellCacheEntry[] EMPTY_ARRAY = new CellCacheEntry[0];

		private FormulaCellCacheEntrySet _consumingCells;

		private ValueEval _value;

		protected CellCacheEntry()
		{
			_consumingCells = new FormulaCellCacheEntrySet();
		}

		protected void ClearValue()
		{
			_value = null;
		}

		public bool UpdateValue(ValueEval value)
		{
			if (value == null)
			{
				throw new ArgumentException("Did not expect To Update To null");
			}
			bool result = !AreValuesEqual(_value, value);
			_value = value;
			return result;
		}

		public ValueEval GetValue()
		{
			return _value;
		}

		private static bool AreValuesEqual(ValueEval a, ValueEval b)
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

		public void AddConsumingCell(FormulaCellCacheEntry cellLoc)
		{
			_consumingCells.Add(cellLoc);
		}

		public FormulaCellCacheEntry[] GetConsumingCells()
		{
			return _consumingCells.ToArray();
		}

		public void ClearConsumingCell(FormulaCellCacheEntry cce)
		{
			if (!_consumingCells.Remove(cce))
			{
				throw new InvalidOperationException("Specified formula cell is not consumed by this cell");
			}
		}

		public void RecurseClearCachedFormulaResults(IEvaluationListener listener)
		{
			if (listener == null)
			{
				RecurseClearCachedFormulaResults();
			}
			else
			{
				listener.OnClearCachedValue(this);
				RecurseClearCachedFormulaResults(listener, 1);
			}
		}

		/// Calls formulaCell.SetFormulaResult(null, null) recursively all the way up the tree of 
		/// dependencies. Calls usedCell.ClearConsumingCell(fc) for each child of a cell that Is
		/// Cleared along the way.
		/// @param formulaCells
		protected void RecurseClearCachedFormulaResults()
		{
			FormulaCellCacheEntry[] consumingCells = GetConsumingCells();
			foreach (FormulaCellCacheEntry formulaCellCacheEntry in consumingCells)
			{
				formulaCellCacheEntry.ClearFormulaEntry();
				formulaCellCacheEntry.RecurseClearCachedFormulaResults();
			}
		}

		/// Identical To {@link #RecurseClearCachedFormulaResults()} except for the listener call-backs
		protected void RecurseClearCachedFormulaResults(IEvaluationListener listener, int depth)
		{
			FormulaCellCacheEntry[] consumingCells = GetConsumingCells();
			listener.SortDependentCachedValues(consumingCells);
			foreach (FormulaCellCacheEntry formulaCellCacheEntry in consumingCells)
			{
				listener.OnClearDependentCachedValue(formulaCellCacheEntry, depth);
				formulaCellCacheEntry.ClearFormulaEntry();
				formulaCellCacheEntry.RecurseClearCachedFormulaResults(listener, depth + 1);
			}
		}
	}
}
