using NPOI.SS.Formula.Eval;
using System;
using System.Collections;

namespace NPOI.SS.Formula
{
	/// <summary>
	/// Instances of this class keep track of multiple dependent cell evaluations due
	/// To recursive calls To <see cref="M:NPOI.SS.Formula.WorkbookEvaluator.Evaluate(NPOI.SS.Formula.IEvaluationCell)" />
	/// The main purpose of this class is To detect an attempt To evaluate a cell
	/// that is already being evaluated. In other words, it detects circular
	/// references in spreadsheet formulas.
	/// </summary>
	/// <remarks>
	/// @author Josh Micich 
	/// </remarks>
	public class EvaluationTracker
	{
		private IList _evaluationFrames;

		private IList _currentlyEvaluatingCells;

		private EvaluationCache _cache;

		public EvaluationTracker(EvaluationCache cache)
		{
			_cache = cache;
			_evaluationFrames = new ArrayList();
			_currentlyEvaluatingCells = new ArrayList();
		}

		/// Notifies this evaluation tracker that evaluation of the specified cell Is
		/// about To start.<br />
		///
		/// In the case of a <c>true</c> return code, the caller should
		/// continue evaluation of the specified cell, and also be sure To call
		/// <c>endEvaluate()</c> when complete.<br />
		///
		/// In the case of a <c>null</c> return code, the caller should
		/// return an evaluation result of
		/// <c>ErrorEval.CIRCULAR_REF_ERROR</c>, and not call <c>endEvaluate()</c>.
		/// <br />
		/// @return <c>false</c> if the specified cell is already being evaluated
		public bool StartEvaluate(FormulaCellCacheEntry cce)
		{
			if (cce == null)
			{
				throw new ArgumentException("cellLoc must not be null");
			}
			if (_currentlyEvaluatingCells.Contains(cce))
			{
				return false;
			}
			_currentlyEvaluatingCells.Add(cce);
			_evaluationFrames.Add(new CellEvaluationFrame(cce));
			return true;
		}

		public void UpdateCacheResult(ValueEval result)
		{
			int count = _evaluationFrames.Count;
			if (count < 1)
			{
				throw new InvalidOperationException("Call To endEvaluate without matching call To startEvaluate");
			}
			CellEvaluationFrame cellEvaluationFrame = (CellEvaluationFrame)_evaluationFrames[count - 1];
			cellEvaluationFrame.UpdateFormulaResult(result);
		}

		/// Notifies this evaluation tracker that the evaluation of the specified cell is complete. <p />
		///
		/// Every successful call To <c>startEvaluate</c> must be followed by a call To <c>endEvaluate</c> (recommended in a finally block) To enable
		/// proper tracking of which cells are being evaluated at any point in time.<p />
		///
		/// Assuming a well behaved client, parameters To this method would not be
		/// required. However, they have been included To assert correct behaviour,
		/// and form more meaningful error messages.
		public void EndEvaluate(CellCacheEntry cce)
		{
			int count = _evaluationFrames.Count;
			if (count < 1)
			{
				throw new InvalidOperationException("Call To endEvaluate without matching call To startEvaluate");
			}
			count--;
			CellEvaluationFrame cellEvaluationFrame = (CellEvaluationFrame)_evaluationFrames[count];
			if (cce != cellEvaluationFrame.GetCCE())
			{
				throw new InvalidOperationException("Wrong cell specified. ");
			}
			_evaluationFrames.RemoveAt(count);
			_currentlyEvaluatingCells.Remove(cce);
		}

		public void AcceptFormulaDependency(CellCacheEntry cce)
		{
			int num = _evaluationFrames.Count - 1;
			if (num >= 0)
			{
				CellEvaluationFrame cellEvaluationFrame = (CellEvaluationFrame)_evaluationFrames[num];
				cellEvaluationFrame.AddSensitiveInputCell(cce);
			}
		}

		public void AcceptPlainValueDependency(int bookIndex, int sheetIndex, int rowIndex, int columnIndex, ValueEval value)
		{
			int num = _evaluationFrames.Count - 1;
			if (num >= 0)
			{
				CellEvaluationFrame cellEvaluationFrame = (CellEvaluationFrame)_evaluationFrames[num];
				if (value == BlankEval.instance)
				{
					cellEvaluationFrame.AddUsedBlankCell(bookIndex, sheetIndex, rowIndex, columnIndex);
				}
				else
				{
					PlainValueCellCacheEntry plainValueEntry = _cache.GetPlainValueEntry(bookIndex, sheetIndex, rowIndex, columnIndex, value);
					cellEvaluationFrame.AddSensitiveInputCell(plainValueEntry);
				}
			}
		}
	}
}
