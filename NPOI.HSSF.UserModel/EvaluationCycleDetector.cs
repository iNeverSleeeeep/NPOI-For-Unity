using System;
using System.Collections;
using System.Text;

namespace NPOI.HSSF.UserModel
{
	/// Instances of this class keep track of multiple dependent cell evaluations due
	/// to recursive calls to <c>HSSFFormulaEvaluator.internalEvaluate()</c>.
	/// The main purpose of this class Is to detect an attempt to evaluate a cell
	/// that Is alReady being evaluated. In other words, it detects circular
	/// references in spReadsheet formulas.
	///
	/// @author Josh Micich
	internal class EvaluationCycleDetector
	{
		/// Stores the parameters that identify the evaluation of one cell.<br />
		private class CellEvaluationFrame
		{
			private HSSFWorkbook _workbook;

			private HSSFSheet _sheet;

			private int _srcRowNum;

			private int _srcColNum;

			public CellEvaluationFrame(HSSFWorkbook workbook, HSSFSheet sheet, int srcRowNum, int srcColNum)
			{
				if (workbook == null)
				{
					throw new ArgumentException("workbook must not be null");
				}
				if (sheet == null)
				{
					throw new ArgumentException("sheet must not be null");
				}
				_workbook = workbook;
				_sheet = sheet;
				_srcRowNum = srcRowNum;
				_srcColNum = srcColNum;
			}

			public override bool Equals(object obj)
			{
				CellEvaluationFrame cellEvaluationFrame = (CellEvaluationFrame)obj;
				if (_workbook != cellEvaluationFrame._workbook)
				{
					return false;
				}
				if (_sheet != cellEvaluationFrame._sheet)
				{
					return false;
				}
				if (_srcRowNum != cellEvaluationFrame._srcRowNum)
				{
					return false;
				}
				if (_srcColNum != cellEvaluationFrame._srcColNum)
				{
					return false;
				}
				return true;
			}

			public override int GetHashCode()
			{
				return _workbook.GetHashCode() ^ _sheet.GetHashCode() ^ _srcRowNum ^ _srcColNum;
			}

			/// @return human Readable string for debug purposes
			public string FormatAsString()
			{
				return "R=" + _srcRowNum + " C=" + _srcColNum + " ShIx=" + _workbook.GetSheetIndex(_sheet);
			}

			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder(64);
				stringBuilder.Append(GetType().Name).Append(" [");
				stringBuilder.Append(FormatAsString());
				stringBuilder.Append("]");
				return stringBuilder.ToString();
			}
		}

		private IList _evaluationFrames;

		public EvaluationCycleDetector()
		{
			_evaluationFrames = new ArrayList();
		}

		/// Notifies this evaluation tracker that evaluation of the specified cell Is
		/// about to start.<br />
		///
		/// In the case of a <c>true</c> return code, the caller should
		/// continue evaluation of the specified cell, and also be sure to call
		/// <c>endEvaluate()</c> when complete.<br />
		///
		/// In the case of a <c>false</c> return code, the caller should
		/// return an evaluation result of
		/// <c>ErrorEval.CIRCULAR_REF_ERROR</c>, and not call <c>endEvaluate()</c>.  
		/// <br />
		/// @return <c>true</c> if the specified cell has not been visited yet in the current 
		/// evaluation. <c>false</c> if the specified cell Is alReady being evaluated.
		public bool StartEvaluate(HSSFWorkbook workbook, HSSFSheet sheet, int srcRowNum, int srcColNum)
		{
			CellEvaluationFrame value = new CellEvaluationFrame(workbook, sheet, srcRowNum, srcColNum);
			if (_evaluationFrames.Contains(value))
			{
				return false;
			}
			_evaluationFrames.Add(value);
			return true;
		}

		/// Notifies this evaluation tracker that the evaluation of the specified
		/// cell Is complete. <p />
		///
		/// Every successful call to <c>startEvaluate</c> must be followed by a
		/// call to <c>endEvaluate</c> (recommended in a finally block) to enable
		/// proper tracking of which cells are being evaluated at any point in time.<p />
		///
		/// Assuming a well behaved client, parameters to this method would not be
		/// required. However, they have been included to assert correct behaviour,
		/// and form more meaningful error messages.
		public void EndEvaluate(HSSFWorkbook workbook, HSSFSheet sheet, int srcRowNum, int srcColNum)
		{
			int count = _evaluationFrames.Count;
			if (count < 1)
			{
				throw new InvalidOperationException("Call to endEvaluate without matching call to startEvaluate");
			}
			count--;
			CellEvaluationFrame cellEvaluationFrame = (CellEvaluationFrame)_evaluationFrames[count];
			CellEvaluationFrame cellEvaluationFrame2 = new CellEvaluationFrame(workbook, sheet, srcRowNum, srcColNum);
			if (!cellEvaluationFrame2.Equals(cellEvaluationFrame))
			{
				throw new Exception("Wrong cell specified. Corresponding startEvaluate() call was for cell {" + cellEvaluationFrame.FormatAsString() + "} this endEvaluate() call Is for cell {" + cellEvaluationFrame2.FormatAsString() + "}");
			}
			_evaluationFrames.Remove(count);
		}
	}
}
