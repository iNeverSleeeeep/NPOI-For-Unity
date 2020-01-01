using NPOI.SS.Formula.PTG;
using System;
using System.Text;

namespace NPOI.SS.Formula
{
	/// @author Josh Micich
	public class FormulaShifter
	{
		public enum ShiftMode
		{
			Row,
			Sheet
		}

		/// Extern sheet index of sheet where moving is occurring
		private int _externSheetIndex;

		private int _firstMovedIndex;

		private int _lastMovedIndex;

		private int _amountToMove;

		private int _srcSheetIndex;

		private int _dstSheetIndex;

		private ShiftMode _mode;

		private FormulaShifter(int externSheetIndex, int firstMovedIndex, int lastMovedIndex, int amountToMove)
		{
			if (amountToMove == 0)
			{
				throw new ArgumentException("amountToMove must not be zero");
			}
			if (firstMovedIndex > lastMovedIndex)
			{
				throw new ArgumentException("firstMovedIndex, lastMovedIndex out of order");
			}
			_externSheetIndex = externSheetIndex;
			_firstMovedIndex = firstMovedIndex;
			_lastMovedIndex = lastMovedIndex;
			_amountToMove = amountToMove;
			_mode = ShiftMode.Row;
			_srcSheetIndex = (_dstSheetIndex = -1);
		}

		/// Create an instance for shifting sheets.
		///
		/// For example, this will be called on {@link org.apache.poi.hssf.usermodel.HSSFWorkbook#setSheetOrder(String, int)}  
		private FormulaShifter(int srcSheetIndex, int dstSheetIndex)
		{
			_externSheetIndex = (_firstMovedIndex = (_lastMovedIndex = (_amountToMove = -1)));
			_srcSheetIndex = srcSheetIndex;
			_dstSheetIndex = dstSheetIndex;
			_mode = ShiftMode.Sheet;
		}

		public static FormulaShifter CreateForRowShift(int externSheetIndex, int firstMovedRowIndex, int lastMovedRowIndex, int numberOfRowsToMove)
		{
			return new FormulaShifter(externSheetIndex, firstMovedRowIndex, lastMovedRowIndex, numberOfRowsToMove);
		}

		public static FormulaShifter CreateForSheetShift(int srcSheetIndex, int dstSheetIndex)
		{
			return new FormulaShifter(srcSheetIndex, dstSheetIndex);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(GetType().Name);
			stringBuilder.Append(" [");
			stringBuilder.Append(_firstMovedIndex);
			stringBuilder.Append(_lastMovedIndex);
			stringBuilder.Append(_amountToMove);
			return stringBuilder.ToString();
		}

		/// @param ptgs - if necessary, will get modified by this method
		/// @param currentExternSheetIx - the extern sheet index of the sheet that contains the formula being adjusted
		/// @return <c>true</c> if a change was made to the formula tokens
		public bool AdjustFormula(Ptg[] ptgs, int currentExternSheetIx)
		{
			bool result = false;
			for (int i = 0; i < ptgs.Length; i++)
			{
				Ptg ptg = AdjustPtg(ptgs[i], currentExternSheetIx);
				if (ptg != null)
				{
					result = true;
					ptgs[i] = ptg;
				}
			}
			return result;
		}

		private Ptg AdjustPtg(Ptg ptg, int currentExternSheetIx)
		{
			switch (_mode)
			{
			case ShiftMode.Row:
				return AdjustPtgDueToRowMove(ptg, currentExternSheetIx);
			case ShiftMode.Sheet:
				return AdjustPtgDueToShiftMove(ptg);
			default:
				throw new InvalidOperationException("Unsupported shift mode: " + _mode);
			}
		}

		/// @return <c>true</c> if this Ptg needed to be changed
		private Ptg AdjustPtgDueToRowMove(Ptg ptg, int currentExternSheetIx)
		{
			if (ptg is RefPtg)
			{
				if (currentExternSheetIx != _externSheetIndex)
				{
					return null;
				}
				RefPtg rptg = (RefPtg)ptg;
				return RowMoveRefPtg(rptg);
			}
			if (ptg is Ref3DPtg)
			{
				Ref3DPtg ref3DPtg = (Ref3DPtg)ptg;
				if (_externSheetIndex != ref3DPtg.ExternSheetIndex)
				{
					return null;
				}
				return RowMoveRefPtg(ref3DPtg);
			}
			if (ptg is Area2DPtgBase)
			{
				if (currentExternSheetIx != _externSheetIndex)
				{
					return ptg;
				}
				return RowMoveAreaPtg((Area2DPtgBase)ptg);
			}
			if (ptg is Area3DPtg)
			{
				Area3DPtg area3DPtg = (Area3DPtg)ptg;
				if (_externSheetIndex != area3DPtg.ExternSheetIndex)
				{
					return null;
				}
				return RowMoveAreaPtg(area3DPtg);
			}
			return null;
		}

		private Ptg AdjustPtgDueToShiftMove(Ptg ptg)
		{
			Ptg result = null;
			if (ptg is Ref3DPtg)
			{
				Ref3DPtg ref3DPtg = (Ref3DPtg)ptg;
				if (ref3DPtg.ExternSheetIndex == _srcSheetIndex)
				{
					ref3DPtg.ExternSheetIndex = _dstSheetIndex;
					result = ref3DPtg;
				}
				else if (ref3DPtg.ExternSheetIndex == _dstSheetIndex)
				{
					ref3DPtg.ExternSheetIndex = _srcSheetIndex;
					result = ref3DPtg;
				}
			}
			return result;
		}

		private Ptg RowMoveRefPtg(RefPtgBase rptg)
		{
			int row = rptg.Row;
			if (_firstMovedIndex <= row && row <= _lastMovedIndex)
			{
				rptg.Row = row + _amountToMove;
				return rptg;
			}
			int num = _firstMovedIndex + _amountToMove;
			int num2 = _lastMovedIndex + _amountToMove;
			if (num2 < row || row < num)
			{
				return null;
			}
			if (num <= row && row <= num2)
			{
				return CreateDeletedRef(rptg);
			}
			throw new InvalidOperationException("Situation not covered: (" + _firstMovedIndex + ", " + _lastMovedIndex + ", " + _amountToMove + ", " + row + ", " + row + ")");
		}

		private Ptg RowMoveAreaPtg(AreaPtgBase aptg)
		{
			int firstRow = aptg.FirstRow;
			int lastRow = aptg.LastRow;
			if (_firstMovedIndex <= firstRow && lastRow <= _lastMovedIndex)
			{
				aptg.FirstRow = firstRow + _amountToMove;
				aptg.LastRow = lastRow + _amountToMove;
				return aptg;
			}
			int num = _firstMovedIndex + _amountToMove;
			int num2 = _lastMovedIndex + _amountToMove;
			if (firstRow < _firstMovedIndex && _lastMovedIndex < lastRow)
			{
				if (num < firstRow && firstRow <= num2)
				{
					aptg.FirstRow = num2 + 1;
					return aptg;
				}
				if (num <= lastRow && lastRow < num2)
				{
					aptg.LastRow = num - 1;
					return aptg;
				}
				return null;
			}
			if (_firstMovedIndex <= firstRow && firstRow <= _lastMovedIndex)
			{
				if (_amountToMove < 0)
				{
					aptg.FirstRow = firstRow + _amountToMove;
					return aptg;
				}
				if (num > lastRow)
				{
					return null;
				}
				int firstRow2 = firstRow + _amountToMove;
				if (num2 < lastRow)
				{
					aptg.FirstRow = firstRow2;
					return aptg;
				}
				int num3 = _lastMovedIndex + 1;
				if (num > num3)
				{
					firstRow2 = num3;
				}
				aptg.FirstRow = firstRow2;
				aptg.LastRow = Math.Max(lastRow, num2);
				return aptg;
			}
			if (_firstMovedIndex <= lastRow && lastRow <= _lastMovedIndex)
			{
				if (_amountToMove > 0)
				{
					aptg.LastRow = lastRow + _amountToMove;
					return aptg;
				}
				if (num2 < firstRow)
				{
					return null;
				}
				int lastRow2 = lastRow + _amountToMove;
				if (num > firstRow)
				{
					aptg.LastRow = lastRow2;
					return aptg;
				}
				int num4 = _firstMovedIndex - 1;
				if (num2 < num4)
				{
					lastRow2 = num4;
				}
				aptg.FirstRow = Math.Min(firstRow, num);
				aptg.LastRow = lastRow2;
				return aptg;
			}
			if (num2 < firstRow || lastRow < num)
			{
				return null;
			}
			if (num <= firstRow && lastRow <= num2)
			{
				return CreateDeletedRef(aptg);
			}
			if (firstRow <= num && num2 <= lastRow)
			{
				return null;
			}
			if (num < firstRow && firstRow <= num2)
			{
				aptg.FirstRow = num2 + 1;
				return aptg;
			}
			if (num < lastRow && lastRow <= num2)
			{
				aptg.LastRow = num - 1;
				return aptg;
			}
			throw new InvalidOperationException("Situation not covered: (" + _firstMovedIndex + ", " + _lastMovedIndex + ", " + _amountToMove + ", " + firstRow + ", " + lastRow + ")");
		}

		private static Ptg CreateDeletedRef(Ptg ptg)
		{
			if (ptg is RefPtg)
			{
				return new RefErrorPtg();
			}
			if (ptg is Ref3DPtg)
			{
				Ref3DPtg ref3DPtg = (Ref3DPtg)ptg;
				return new DeletedRef3DPtg(ref3DPtg.ExternSheetIndex);
			}
			if (ptg is AreaPtg)
			{
				return new AreaErrPtg();
			}
			if (ptg is Area3DPtg)
			{
				Area3DPtg area3DPtg = (Area3DPtg)ptg;
				return new DeletedArea3DPtg(area3DPtg.ExternSheetIndex);
			}
			throw new ArgumentException("Unexpected ref ptg class (" + ptg.GetType().Name + ")");
		}
	}
}
