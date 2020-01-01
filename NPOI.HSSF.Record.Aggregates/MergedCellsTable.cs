using NPOI.HSSF.Model;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;

namespace NPOI.HSSF.Record.Aggregates
{
	/// @author Josh Micich
	public class MergedCellsTable : RecordAggregate
	{
		private const int MAX_MERGED_REGIONS = 1027;

		private List<CellRangeAddress> _mergedRegions;

		public override int RecordSize
		{
			get
			{
				int count = _mergedRegions.Count;
				if (count < 1)
				{
					return 0;
				}
				int num = count / 1027;
				int numberOfRanges = count % 1027;
				return num * (4 + CellRangeAddressList.GetEncodedSize(1027)) + 4 + CellRangeAddressList.GetEncodedSize(numberOfRanges);
			}
		}

		public List<CellRangeAddress> MergedRegions => _mergedRegions;

		public int NumberOfMergedRegions => _mergedRegions.Count;

		/// <summary>
		/// Creates an empty aggregate
		/// </summary>
		public MergedCellsTable()
		{
			_mergedRegions = new List<CellRangeAddress>();
		}

		/// Reads zero or more consecutive {@link MergeCellsRecord}s
		/// @param rs
		public void Read(RecordStream rs)
		{
			while (rs.PeekNextClass() == typeof(MergeCellsRecord))
			{
				MergeCellsRecord mergeCellsRecord = (MergeCellsRecord)rs.GetNext();
				int numAreas = mergeCellsRecord.NumAreas;
				for (int i = 0; i < numAreas; i++)
				{
					_mergedRegions.Add(mergeCellsRecord.GetAreaAt(i));
				}
			}
		}

		public override void VisitContainedRecords(RecordVisitor rv)
		{
			int count = _mergedRegions.Count;
			if (count >= 1)
			{
				int num = count / 1027;
				int num2 = count % 1027;
				CellRangeAddress[] regions = _mergedRegions.ToArray();
				for (int i = 0; i < num; i++)
				{
					int startIndex = i * 1027;
					rv.VisitRecord(new MergeCellsRecord(regions, startIndex, 1027));
				}
				if (num2 > 0)
				{
					int startIndex2 = num * 1027;
					rv.VisitRecord(new MergeCellsRecord(regions, startIndex2, num2));
				}
			}
		}

		public void AddRecords(MergeCellsRecord[] mcrs)
		{
			for (int i = 0; i < mcrs.Length; i++)
			{
				AddMergeCellsRecord(mcrs[i]);
			}
		}

		private void AddMergeCellsRecord(MergeCellsRecord mcr)
		{
			int numAreas = mcr.NumAreas;
			for (int i = 0; i < numAreas; i++)
			{
				_mergedRegions.Add(mcr.GetAreaAt(i));
			}
		}

		public CellRangeAddress Get(int index)
		{
			CheckIndex(index);
			return _mergedRegions[index];
		}

		public void Remove(int index)
		{
			CheckIndex(index);
			_mergedRegions.RemoveAt(index);
		}

		private void CheckIndex(int index)
		{
			if (index < 0 || index >= _mergedRegions.Count)
			{
				throw new ArgumentException("Specified CF index " + index + " is outside the allowable range (0.." + (_mergedRegions.Count - 1) + ")");
			}
		}

		public void AddArea(int rowFrom, int colFrom, int rowTo, int colTo)
		{
			_mergedRegions.Add(new CellRangeAddress(rowFrom, rowTo, colFrom, colTo));
		}
	}
}
