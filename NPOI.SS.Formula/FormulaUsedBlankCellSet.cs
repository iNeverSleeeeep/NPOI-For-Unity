using NPOI.SS.Util;
using System.Collections;
using System.Text;

namespace NPOI.SS.Formula
{
	/// Optimisation - compacts many blank cell references used by a single formula. 
	///
	/// @author Josh Micich
	public class FormulaUsedBlankCellSet
	{
		private class BlankCellSheetGroup
		{
			private IList _rectangleGroups;

			private int _currentRowIndex;

			private int _firstColumnIndex;

			private int _lastColumnIndex;

			private BlankCellRectangleGroup _currentRectangleGroup;

			public BlankCellSheetGroup()
			{
				_rectangleGroups = new ArrayList();
				_currentRowIndex = -1;
			}

			public void AddCell(int rowIndex, int columnIndex)
			{
				if (_currentRowIndex == -1)
				{
					_currentRowIndex = rowIndex;
					_firstColumnIndex = columnIndex;
					_lastColumnIndex = columnIndex;
				}
				else if (_currentRowIndex == rowIndex && _lastColumnIndex + 1 == columnIndex)
				{
					_lastColumnIndex = columnIndex;
				}
				else
				{
					if (_currentRectangleGroup == null)
					{
						_currentRectangleGroup = new BlankCellRectangleGroup(_currentRowIndex, _firstColumnIndex, _lastColumnIndex);
					}
					else if (!_currentRectangleGroup.AcceptRow(_currentRowIndex, _firstColumnIndex, _lastColumnIndex))
					{
						_rectangleGroups.Add(_currentRectangleGroup);
						_currentRectangleGroup = new BlankCellRectangleGroup(_currentRowIndex, _firstColumnIndex, _lastColumnIndex);
					}
					_currentRowIndex = rowIndex;
					_firstColumnIndex = columnIndex;
					_lastColumnIndex = columnIndex;
				}
			}

			public bool ContainsCell(int rowIndex, int columnIndex)
			{
				for (int num = _rectangleGroups.Count - 1; num >= 0; num--)
				{
					BlankCellRectangleGroup blankCellRectangleGroup = (BlankCellRectangleGroup)_rectangleGroups[num];
					if (blankCellRectangleGroup.ContainsCell(rowIndex, columnIndex))
					{
						return true;
					}
				}
				if (_currentRectangleGroup != null && _currentRectangleGroup.ContainsCell(rowIndex, columnIndex))
				{
					return true;
				}
				if (_currentRowIndex != -1 && _currentRowIndex == rowIndex && _firstColumnIndex <= columnIndex && columnIndex <= _lastColumnIndex)
				{
					return true;
				}
				return false;
			}
		}

		private class BlankCellRectangleGroup
		{
			private int _firstRowIndex;

			private int _firstColumnIndex;

			private int _lastColumnIndex;

			private int _lastRowIndex;

			public BlankCellRectangleGroup(int firstRowIndex, int firstColumnIndex, int lastColumnIndex)
			{
				_firstRowIndex = firstRowIndex;
				_firstColumnIndex = firstColumnIndex;
				_lastColumnIndex = lastColumnIndex;
				_lastRowIndex = firstRowIndex;
			}

			public bool ContainsCell(int rowIndex, int columnIndex)
			{
				if (columnIndex < _firstColumnIndex)
				{
					return false;
				}
				if (columnIndex > _lastColumnIndex)
				{
					return false;
				}
				if (rowIndex < _firstRowIndex)
				{
					return false;
				}
				if (rowIndex > _lastRowIndex)
				{
					return false;
				}
				return true;
			}

			public bool AcceptRow(int rowIndex, int firstColumnIndex, int lastColumnIndex)
			{
				if (firstColumnIndex != _firstColumnIndex)
				{
					return false;
				}
				if (lastColumnIndex != _lastColumnIndex)
				{
					return false;
				}
				if (rowIndex != _lastRowIndex + 1)
				{
					return false;
				}
				_lastRowIndex = rowIndex;
				return true;
			}

			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder(64);
				CellReference cellReference = new CellReference(_firstRowIndex, _firstColumnIndex, pAbsRow: false, pAbsCol: false);
				CellReference cellReference2 = new CellReference(_lastRowIndex, _lastColumnIndex, pAbsRow: false, pAbsCol: false);
				stringBuilder.Append(GetType().Name);
				stringBuilder.Append(" [").Append(cellReference.FormatAsString()).Append(':')
					.Append(cellReference2.FormatAsString())
					.Append("]");
				return stringBuilder.ToString();
			}
		}

		private Hashtable _sheetGroupsByBookSheet;

		public bool IsEmpty => _sheetGroupsByBookSheet.Count == 0;

		public FormulaUsedBlankCellSet()
		{
			_sheetGroupsByBookSheet = new Hashtable();
		}

		public void AddCell(int bookIndex, int sheetIndex, int rowIndex, int columnIndex)
		{
			BlankCellSheetGroup sheetGroup = GetSheetGroup(bookIndex, sheetIndex);
			sheetGroup.AddCell(rowIndex, columnIndex);
		}

		private BlankCellSheetGroup GetSheetGroup(int bookIndex, int sheetIndex)
		{
			BookSheetKey key = new BookSheetKey(bookIndex, sheetIndex);
			BlankCellSheetGroup blankCellSheetGroup = (BlankCellSheetGroup)_sheetGroupsByBookSheet[key];
			if (blankCellSheetGroup == null)
			{
				blankCellSheetGroup = new BlankCellSheetGroup();
				_sheetGroupsByBookSheet[key] = blankCellSheetGroup;
			}
			return blankCellSheetGroup;
		}

		public bool ContainsCell(BookSheetKey key, int rowIndex, int columnIndex)
		{
			return ((BlankCellSheetGroup)_sheetGroupsByBookSheet[key])?.ContainsCell(rowIndex, columnIndex) ?? false;
		}
	}
}
