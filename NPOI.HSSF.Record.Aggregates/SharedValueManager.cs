using NPOI.SS.Util;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace NPOI.HSSF.Record.Aggregates
{
	/// <summary>
	/// Manages various auxiliary records while constructing a RowRecordsAggregate
	/// @author Josh Micich
	/// </summary>
	[Serializable]
	public class SharedValueManager
	{
		private class SharedFormulaGroup
		{
			private SharedFormulaRecord _sfr;

			private FormulaRecordAggregate[] _frAggs;

			private int _numberOfFormulas;

			/// Coordinates of the first cell having a formula that uses this shared formula.
			/// This is often <i>but not always</i> the top left cell in the range covered by
			/// {@link #_sfr}
			private CellReference _firstCell;

			internal CellReference FirstCell => _firstCell;

			public SharedFormulaRecord SFR => _sfr;

			public SharedFormulaGroup(SharedFormulaRecord sfr, CellReference firstCell)
			{
				if (!sfr.IsInRange(firstCell.Row, firstCell.Col))
				{
					throw new ArgumentException("First formula cell " + firstCell.FormatAsString() + " is not shared formula range " + sfr.Range.ToString() + ".");
				}
				_sfr = sfr;
				_firstCell = firstCell;
				int num = sfr.LastColumn - sfr.FirstColumn + 1;
				int num2 = sfr.LastRow - sfr.FirstRow + 1;
				_frAggs = new FormulaRecordAggregate[num * num2];
				_numberOfFormulas = 0;
			}

			public void Add(FormulaRecordAggregate agg)
			{
				if (_numberOfFormulas == 0 && (_firstCell.Row != agg.Row || _firstCell.Col != agg.Column))
				{
					throw new InvalidOperationException("shared formula coding error");
				}
				if (_numberOfFormulas >= _frAggs.Length)
				{
					throw new Exception("Too many formula records for shared formula group");
				}
				_frAggs[_numberOfFormulas++] = agg;
			}

			public void UnlinkSharedFormulas()
			{
				for (int i = 0; i < _numberOfFormulas; i++)
				{
					_frAggs[i].UnlinkSharedFormula();
				}
			}

			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder(64);
				stringBuilder.Append(GetType().Name).Append(" [");
				stringBuilder.Append(_sfr.Range.ToString());
				stringBuilder.Append("]");
				return stringBuilder.ToString();
			}

			/// Note - the 'first cell' of a shared formula group is not always the top-left cell
			/// of the enclosing range.
			/// @return <c>true</c> if the specified coordinates correspond to the 'first cell'
			/// of this shared formula group.
			public bool IsFirstCell(int row, int column)
			{
				if (_firstCell.Row == row)
				{
					return _firstCell.Col == column;
				}
				return false;
			}
		}

		private class SharedFormulaGroupComparator : Comparer<SharedFormulaGroup>
		{
			public override int Compare(SharedFormulaGroup a, SharedFormulaGroup b)
			{
				CellRangeAddress8Bit range = a.SFR.Range;
				CellRangeAddress8Bit range2 = b.SFR.Range;
				int num = range.FirstRow - range2.FirstRow;
				if (num != 0)
				{
					return num;
				}
				num = range.FirstColumn - range2.FirstColumn;
				if (num != 0)
				{
					return num;
				}
				return 0;
			}
		}

		public static readonly SharedValueManager EMPTY = new SharedValueManager(new SharedFormulaRecord[0], new CellReference[0], new List<ArrayRecord>(), new List<TableRecord>());

		private List<ArrayRecord> _arrayRecords;

		private List<TableRecord> _tableRecords;

		private Dictionary<SharedFormulaRecord, SharedFormulaGroup> _groupsBySharedFormulaRecord;

		/// cached for optimization purposes 
		[NonSerialized]
		private Dictionary<int, SharedFormulaGroup> _groupsCache;

		[NonSerialized]
		private SharedFormulaGroupComparator SVGComparator = new SharedFormulaGroupComparator();

		private SharedValueManager(SharedFormulaRecord[] sharedFormulaRecords, CellReference[] firstCells, List<ArrayRecord> arrayRecords, List<TableRecord> tableRecords)
		{
			int num = sharedFormulaRecords.Length;
			if (num != firstCells.Length)
			{
				throw new ArgumentException("array sizes don't match: " + num + "!=" + firstCells.Length + ".");
			}
			_arrayRecords = new List<ArrayRecord>();
			_arrayRecords.AddRange(arrayRecords);
			_tableRecords = tableRecords;
			Dictionary<SharedFormulaRecord, SharedFormulaGroup> dictionary = new Dictionary<SharedFormulaRecord, SharedFormulaGroup>(num * 3 / 2);
			for (int i = 0; i < num; i++)
			{
				SharedFormulaRecord sharedFormulaRecord = sharedFormulaRecords[i];
				dictionary[sharedFormulaRecord] = new SharedFormulaGroup(sharedFormulaRecord, firstCells[i]);
			}
			_groupsBySharedFormulaRecord = dictionary;
		}

		public static SharedValueManager CreateEmpty()
		{
			return new SharedValueManager(new SharedFormulaRecord[0], new CellReference[0], new List<ArrayRecord>(), new List<TableRecord>());
		}

		/// @param firstCells
		/// @param recs list of sheet records (possibly Contains records for other parts of the Excel file)
		/// @param startIx index of first row/cell record for current sheet
		/// @param endIx one past index of last row/cell record for current sheet.  It is important
		/// that this code does not inadvertently collect <c>SharedFormulaRecord</c>s from any other
		/// sheet (which could happen if endIx is chosen poorly).  (see bug 44449)
		public static SharedValueManager Create(SharedFormulaRecord[] sharedFormulaRecords, CellReference[] firstCells, List<ArrayRecord> arrayRecords, List<TableRecord> tableRecords)
		{
			if (sharedFormulaRecords.Length + firstCells.Length + arrayRecords.Count + tableRecords.Count < 1)
			{
				return EMPTY;
			}
			return new SharedValueManager(sharedFormulaRecords, firstCells, arrayRecords, tableRecords);
		}

		/// @param firstCell as extracted from the {@link ExpPtg} from the cell's formula.
		/// @return never <code>null</code>
		public SharedFormulaRecord LinkSharedFormulaRecord(CellReference firstCell, FormulaRecordAggregate agg)
		{
			SharedFormulaGroup sharedFormulaGroup = FindFormulaGroupForCell(firstCell);
			if (sharedFormulaGroup == null)
			{
				throw new RuntimeException("Failed to find a matching shared formula record");
			}
			sharedFormulaGroup.Add(agg);
			return sharedFormulaGroup.SFR;
		}

		private SharedFormulaGroup FindFormulaGroupForCell(CellReference cellRef)
		{
			if (_groupsCache == null)
			{
				_groupsCache = new Dictionary<int, SharedFormulaGroup>(_groupsBySharedFormulaRecord.Count);
				foreach (SharedFormulaGroup value in _groupsBySharedFormulaRecord.Values)
				{
					_groupsCache.Add(GetKeyForCache(value.FirstCell), value);
				}
			}
			int keyForCache = GetKeyForCache(cellRef);
			SharedFormulaGroup result = null;
			if (_groupsCache.ContainsKey(keyForCache))
			{
				result = _groupsCache[keyForCache];
			}
			return result;
		}

		private int GetKeyForCache(CellReference cellRef)
		{
			return (cellRef.Col + 1 << 16) | cellRef.Row;
		}

		/// Gets the {@link SharedValueRecordBase} record if it should be encoded immediately after the
		/// formula record Contained in the specified {@link FormulaRecordAggregate} agg.  Note - the
		/// shared value record always appears after the first formula record in the group.  For arrays
		/// and tables the first formula is always the in the top left cell.  However, since shared
		/// formula groups can be sparse and/or overlap, the first formula may not actually be in the
		/// top left cell.
		///
		/// @return the SHRFMLA, TABLE or ARRAY record for the formula cell, if it is the first cell of
		/// a table or array region. <code>null</code> if the formula cell is not shared/array/table,
		/// or if the specified formula is not the the first in the group.
		public SharedValueRecordBase GetRecordForFirstCell(FormulaRecordAggregate agg)
		{
			CellReference expReference = agg.FormulaRecord.Formula.ExpReference;
			if (expReference == null)
			{
				return null;
			}
			int row = expReference.Row;
			int col = expReference.Col;
			if (agg.Row != row || agg.Column != col)
			{
				return null;
			}
			if (_groupsBySharedFormulaRecord.Count != 0)
			{
				SharedFormulaGroup sharedFormulaGroup = FindFormulaGroupForCell(expReference);
				if (sharedFormulaGroup != null)
				{
					return sharedFormulaGroup.SFR;
				}
			}
			for (int i = 0; i < _tableRecords.Count; i++)
			{
				TableRecord tableRecord = _tableRecords[i];
				if (tableRecord.IsFirstCell(row, col))
				{
					return tableRecord;
				}
			}
			foreach (ArrayRecord arrayRecord in _arrayRecords)
			{
				if (arrayRecord.IsFirstCell(row, col))
				{
					return arrayRecord;
				}
			}
			return null;
		}

		/// Converts all {@link FormulaRecord}s handled by <c>sharedFormulaRecord</c>
		/// to plain unshared formulas
		public void Unlink(SharedFormulaRecord sharedFormulaRecord)
		{
			SharedFormulaGroup sharedFormulaGroup = _groupsBySharedFormulaRecord[sharedFormulaRecord];
			_groupsBySharedFormulaRecord.Remove(sharedFormulaRecord);
			_groupsCache = null;
			if (sharedFormulaGroup == null)
			{
				throw new InvalidOperationException("Failed to find formulas for shared formula");
			}
			sharedFormulaGroup.UnlinkSharedFormulas();
		}

		/// Add specified Array Record.
		public void AddArrayRecord(ArrayRecord ar)
		{
			_arrayRecords.Add(ar);
		}

		/// Removes the {@link ArrayRecord} for the cell group containing the specified cell.
		/// The caller should clear (set blank) all cells in the returned range.
		/// @return the range of the array formula which was just removed. Never <code>null</code>.
		public CellRangeAddress8Bit RemoveArrayFormula(int rowIndex, int columnIndex)
		{
			foreach (ArrayRecord arrayRecord in _arrayRecords)
			{
				if (arrayRecord.IsInRange(rowIndex, columnIndex))
				{
					_arrayRecords.Remove(arrayRecord);
					return arrayRecord.Range;
				}
			}
			string str = new CellReference(rowIndex, columnIndex, pAbsRow: false, pAbsCol: false).FormatAsString();
			throw new ArgumentException("Specified cell " + str + " is not part of an array formula.");
		}

		/// @return the shared ArrayRecord identified by (firstRow, firstColumn). never <code>null</code>.
		public ArrayRecord GetArrayRecord(int firstRow, int firstColumn)
		{
			foreach (ArrayRecord arrayRecord in _arrayRecords)
			{
				if (arrayRecord.IsFirstCell(firstRow, firstColumn))
				{
					return arrayRecord;
				}
			}
			return null;
		}
	}
}
