using NPOI.HSSF.Model;
using System;
using System.Collections;
using System.Globalization;

namespace NPOI.HSSF.Record.Aggregates
{
	/// <summary>
	/// @author Glen Stampoultzis
	/// </summary>
	public class ColumnInfoRecordsAggregate : RecordAggregate
	{
		private class CIRComparator : IComparer
		{
			public static IComparer instance = new CIRComparator();

			private CIRComparator()
			{
			}

			public int Compare(object a, object b)
			{
				return CompareColInfos((ColumnInfoRecord)a, (ColumnInfoRecord)b);
			}

			public static int CompareColInfos(ColumnInfoRecord a, ColumnInfoRecord b)
			{
				return a.FirstColumn - b.FirstColumn;
			}
		}

		private ArrayList records;

		/// It's an aggregate... just made something up 
		public override short Sid => -1012;

		/// <summary>
		/// Gets the num columns.
		/// </summary>
		/// <value>The num columns.</value>
		public int NumColumns => records.Count;

		/// <summary>
		/// Gets the size of the record.
		/// </summary>
		/// <value>The size of the record.</value>
		public override int RecordSize
		{
			get
			{
				int num = 0;
				IEnumerator enumerator = records.GetEnumerator();
				while (enumerator.MoveNext())
				{
					num += ((ColumnInfoRecord)enumerator.Current).RecordSize;
				}
				return num;
			}
		}

		/// <summary>
		/// Gets the max outline level.
		/// </summary>
		/// <value>The max outline level.</value>
		public int MaxOutlineLevel
		{
			get
			{
				int num = 0;
				int count = records.Count;
				for (int i = 0; i < count; i++)
				{
					ColumnInfoRecord colInfo = GetColInfo(i);
					num = Math.Max(colInfo.OutlineLevel, num);
				}
				return num;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HSSF.Record.Aggregates.ColumnInfoRecordsAggregate" /> class.
		/// </summary>
		public ColumnInfoRecordsAggregate()
		{
			records = new ArrayList();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HSSF.Record.Aggregates.ColumnInfoRecordsAggregate" /> class.
		/// </summary>
		/// <param name="rs">The rs.</param>
		public ColumnInfoRecordsAggregate(RecordStream rs)
			: this()
		{
			bool flag = true;
			ColumnInfoRecord columnInfoRecord = null;
			while (rs.PeekNextClass() == typeof(ColumnInfoRecord))
			{
				ColumnInfoRecord columnInfoRecord2 = (ColumnInfoRecord)rs.GetNext();
				records.Add(columnInfoRecord2);
				if (columnInfoRecord != null && CIRComparator.CompareColInfos(columnInfoRecord, columnInfoRecord2) > 0)
				{
					flag = false;
				}
				columnInfoRecord = columnInfoRecord2;
			}
			if (records.Count < 1)
			{
				throw new InvalidOperationException("No column info records found");
			}
			if (!flag)
			{
				records.Sort(CIRComparator.instance);
			}
		}

		public IEnumerator GetEnumerator()
		{
			return records.GetEnumerator();
		}

		/// Performs a deep Clone of the record
		public object Clone()
		{
			ColumnInfoRecordsAggregate columnInfoRecordsAggregate = new ColumnInfoRecordsAggregate();
			for (int i = 0; i < records.Count; i++)
			{
				ColumnInfoRecord columnInfoRecord = (ColumnInfoRecord)records[i];
				columnInfoRecord = (ColumnInfoRecord)columnInfoRecord.Clone();
				columnInfoRecordsAggregate.records.Add(columnInfoRecord);
			}
			return columnInfoRecordsAggregate;
		}

		/// <summary>
		/// Inserts a column into the aggregate (at the end of the list).
		/// </summary>
		/// <param name="col">The column.</param>
		public void InsertColumn(ColumnInfoRecord col)
		{
			records.Add(col);
			records.Sort(CIRComparator.instance);
		}

		/// <summary>
		/// Inserts a column into the aggregate (at the position specified
		/// by index
		/// </summary>
		/// <param name="idx">The index.</param>
		/// <param name="col">The columninfo.</param>
		public void InsertColumn(int idx, ColumnInfoRecord col)
		{
			records.Insert(idx, col);
		}

		/// <summary>
		/// called by the class that is responsible for writing this sucker.
		/// Subclasses should implement this so that their data is passed back in a
		/// byte array.
		/// </summary>
		/// <param name="offset">offset to begin writing at</param>
		/// <param name="data">byte array containing instance data</param>
		/// <returns>number of bytes written</returns>
		public override int Serialize(int offset, byte[] data)
		{
			IEnumerator enumerator = records.GetEnumerator();
			int num = offset;
			while (enumerator.MoveNext())
			{
				num += ((Record)enumerator.Current).Serialize(num, data);
			}
			return num - offset;
		}

		/// <summary>
		/// Visit each of the atomic BIFF records contained in this {@link RecordAggregate} in the order
		/// that they should be written to file.  Implementors may or may not return the actual
		/// Records being used to manage POI's internal implementation.  Callers should not
		/// assume either way, and therefore only attempt to modify those Records after cloning
		/// </summary>
		/// <param name="rv"></param>
		public override void VisitContainedRecords(RecordVisitor rv)
		{
			int count = records.Count;
			if (count < 1)
			{
				return;
			}
			ColumnInfoRecord columnInfoRecord = null;
			int num = 0;
			while (true)
			{
				if (num >= count)
				{
					return;
				}
				ColumnInfoRecord columnInfoRecord2 = (ColumnInfoRecord)records[num];
				rv.VisitRecord(columnInfoRecord2);
				if (columnInfoRecord != null && CIRComparator.CompareColInfos(columnInfoRecord, columnInfoRecord2) > 0)
				{
					break;
				}
				columnInfoRecord = columnInfoRecord2;
				num++;
			}
			throw new InvalidOperationException("Column info records are out of order");
		}

		/// <summary>
		/// Finds the start of column outline group.
		/// </summary>
		/// <param name="idx">The idx.</param>
		/// <returns></returns>
		public int FindStartOfColumnOutlineGroup(int idx)
		{
			ColumnInfoRecord columnInfoRecord = (ColumnInfoRecord)records[idx];
			int outlineLevel = columnInfoRecord.OutlineLevel;
			while (idx != 0)
			{
				ColumnInfoRecord columnInfoRecord2 = (ColumnInfoRecord)records[idx - 1];
				if (columnInfoRecord.FirstColumn - 1 != columnInfoRecord2.LastColumn || columnInfoRecord2.OutlineLevel < outlineLevel)
				{
					break;
				}
				idx--;
				columnInfoRecord = columnInfoRecord2;
			}
			return idx;
		}

		/// <summary>
		/// Finds the end of column outline group.
		/// </summary>
		/// <param name="idx">The idx.</param>
		/// <returns></returns>
		public int FindEndOfColumnOutlineGroup(int idx)
		{
			ColumnInfoRecord columnInfoRecord = (ColumnInfoRecord)records[idx];
			int outlineLevel = columnInfoRecord.OutlineLevel;
			while (idx < records.Count - 1)
			{
				ColumnInfoRecord columnInfoRecord2 = (ColumnInfoRecord)records[idx + 1];
				if (columnInfoRecord.LastColumn + 1 != columnInfoRecord2.FirstColumn || columnInfoRecord2.OutlineLevel < outlineLevel)
				{
					break;
				}
				idx++;
				columnInfoRecord = columnInfoRecord2;
			}
			return idx;
		}

		/// <summary>
		/// Gets the col info.
		/// </summary>
		/// <param name="idx">The idx.</param>
		/// <returns></returns>
		public ColumnInfoRecord GetColInfo(int idx)
		{
			return (ColumnInfoRecord)records[idx];
		}

		/// <summary>
		/// Determines whether [is column group collapsed] [the specified idx].
		/// </summary>
		/// <param name="idx">The idx.</param>
		/// <returns>
		/// 	<c>true</c> if [is column group collapsed] [the specified idx]; otherwise, <c>false</c>.
		/// </returns>
		public bool IsColumnGroupCollapsed(int idx)
		{
			int num = FindEndOfColumnOutlineGroup(idx);
			int num2 = num + 1;
			if (num2 >= records.Count)
			{
				return false;
			}
			ColumnInfoRecord colInfo = GetColInfo(num2);
			if (!GetColInfo(num).IsAdjacentBefore(colInfo))
			{
				return false;
			}
			return colInfo.IsCollapsed;
		}

		/// <summary>
		/// Determines whether [is column group hidden by parent] [the specified idx].
		/// </summary>
		/// <param name="idx">The idx.</param>
		/// <returns>
		/// 	<c>true</c> if [is column group hidden by parent] [the specified idx]; otherwise, <c>false</c>.
		/// </returns>
		public bool IsColumnGroupHiddenByParent(int idx)
		{
			int num = 0;
			bool result = false;
			int num2 = FindEndOfColumnOutlineGroup(idx);
			if (num2 < records.Count)
			{
				ColumnInfoRecord colInfo = GetColInfo(num2 + 1);
				if (GetColInfo(num2).IsAdjacentBefore(colInfo))
				{
					num = colInfo.OutlineLevel;
					result = colInfo.IsHidden;
				}
			}
			int num3 = 0;
			bool result2 = false;
			int num4 = FindStartOfColumnOutlineGroup(idx);
			if (num4 > 0)
			{
				ColumnInfoRecord colInfo2 = GetColInfo(num4 - 1);
				if (colInfo2.IsAdjacentBefore(GetColInfo(num4)))
				{
					num3 = colInfo2.OutlineLevel;
					result2 = colInfo2.IsHidden;
				}
			}
			if (num > num3)
			{
				return result;
			}
			return result2;
		}

		/// <summary>
		/// Collapses the column.
		/// </summary>
		/// <param name="columnNumber">The column number.</param>
		public void CollapseColumn(int columnNumber)
		{
			int num = FindColInfoIdx(columnNumber, 0);
			if (num != -1)
			{
				int num2 = FindStartOfColumnOutlineGroup(num);
				ColumnInfoRecord colInfo = GetColInfo(num2);
				int num3 = SetGroupHidden(num2, colInfo.OutlineLevel, hidden: true);
				SetColumn(num3 + 1, null, null, null, null, true);
			}
		}

		/// <summary>
		/// Expands the column.
		/// </summary>
		/// <param name="columnNumber">The column number.</param>
		public void ExpandColumn(int columnNumber)
		{
			int num = FindColInfoIdx(columnNumber, 0);
			if (num != -1 && IsColumnGroupCollapsed(num))
			{
				int num2 = FindStartOfColumnOutlineGroup(num);
				ColumnInfoRecord colInfo = GetColInfo(num2);
				int num3 = FindEndOfColumnOutlineGroup(num);
				GetColInfo(num3);
				if (!IsColumnGroupHiddenByParent(num))
				{
					for (int i = num2; i <= num3; i++)
					{
						if (colInfo.OutlineLevel == GetColInfo(i).OutlineLevel)
						{
							GetColInfo(i).IsHidden = false;
						}
					}
				}
				SetColumn(colInfo.LastColumn + 1, null, null, null, null, false);
			}
		}

		/// Sets all non null fields into the <c>ci</c> parameter.
		private static void SetColumnInfoFields(ColumnInfoRecord ci, short? xfStyle, int? width, int? level, bool? hidden, bool? collapsed)
		{
			if (((int?)xfStyle).HasValue)
			{
				ci.XFIndex = Convert.ToInt16(xfStyle, CultureInfo.InvariantCulture);
			}
			if (width.HasValue)
			{
				ci.ColumnWidth = Convert.ToInt32(width, CultureInfo.InvariantCulture);
			}
			if (level.HasValue)
			{
				ci.OutlineLevel = (short)level.Value;
			}
			if (hidden.HasValue)
			{
				ci.IsHidden = Convert.ToBoolean(hidden, CultureInfo.InvariantCulture);
			}
			if (collapsed.HasValue)
			{
				ci.IsCollapsed = Convert.ToBoolean(collapsed, CultureInfo.InvariantCulture);
			}
		}

		/// <summary>
		/// Attempts to merge the col info record at the specified index
		/// with either or both of its neighbours
		/// </summary>
		/// <param name="colInfoIx">The col info ix.</param>
		private void AttemptMergeColInfoRecords(int colInfoIx)
		{
			int count = records.Count;
			if (colInfoIx < 0 || colInfoIx >= count)
			{
				throw new ArgumentException("colInfoIx " + colInfoIx + " is out of range (0.." + (count - 1) + ")");
			}
			ColumnInfoRecord colInfo = GetColInfo(colInfoIx);
			int num = colInfoIx + 1;
			if (num < count && MergeColInfoRecords(colInfo, GetColInfo(num)))
			{
				records.RemoveAt(num);
			}
			if (colInfoIx > 0 && MergeColInfoRecords(GetColInfo(colInfoIx - 1), colInfo))
			{
				records.RemoveAt(colInfoIx);
			}
		}

		/// merges two column info records (if they are adjacent and have the same formatting, etc)
		/// @return <c>false</c> if the two column records could not be merged
		private static bool MergeColInfoRecords(ColumnInfoRecord ciA, ColumnInfoRecord ciB)
		{
			if (ciA.IsAdjacentBefore(ciB) && ciA.FormatMatches(ciB))
			{
				ciA.LastColumn = ciB.LastColumn;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Sets all adjacent columns of the same outline level to the specified hidden status.
		/// </summary>
		/// <param name="pIdx">the col info index of the start of the outline group.</param>
		/// <param name="level">The level.</param>
		/// <param name="hidden">The hidden.</param>
		/// <returns>the column index of the last column in the outline group</returns>
		private int SetGroupHidden(int pIdx, int level, bool hidden)
		{
			int i = pIdx;
			ColumnInfoRecord columnInfoRecord = GetColInfo(i);
			for (; i < records.Count; i++)
			{
				columnInfoRecord.IsHidden = hidden;
				if (i + 1 < records.Count)
				{
					ColumnInfoRecord colInfo = GetColInfo(i + 1);
					if (!columnInfoRecord.IsAdjacentBefore(colInfo) || colInfo.OutlineLevel < level)
					{
						break;
					}
					columnInfoRecord = colInfo;
				}
			}
			return columnInfoRecord.LastColumn;
		}

		/// <summary>
		/// Sets the column.
		/// </summary>
		/// <param name="targetColumnIx">The target column ix.</param>
		/// <param name="xfIndex">Index of the xf.</param>
		/// <param name="width">The width.</param>
		/// <param name="level">The level.</param>
		/// <param name="hidden">The hidden.</param>
		/// <param name="collapsed">The collapsed.</param>
		public void SetColumn(int targetColumnIx, short? xfIndex, int? width, int? level, bool? hidden, bool? collapsed)
		{
			ColumnInfoRecord columnInfoRecord = null;
			int num = 0;
			for (num = 0; num < records.Count; num++)
			{
				ColumnInfoRecord columnInfoRecord2 = (ColumnInfoRecord)records[num];
				if (columnInfoRecord2.ContainsColumn(targetColumnIx))
				{
					columnInfoRecord = columnInfoRecord2;
					break;
				}
				if (columnInfoRecord2.FirstColumn > targetColumnIx)
				{
					break;
				}
			}
			if (columnInfoRecord == null)
			{
				ColumnInfoRecord columnInfoRecord3 = new ColumnInfoRecord();
				columnInfoRecord3.FirstColumn = targetColumnIx;
				columnInfoRecord3.LastColumn = targetColumnIx;
				SetColumnInfoFields(columnInfoRecord3, xfIndex, width, level, hidden, collapsed);
				InsertColumn(num, columnInfoRecord3);
				AttemptMergeColInfoRecords(num);
			}
			else
			{
				bool flag = columnInfoRecord.XFIndex != xfIndex;
				bool flag2 = columnInfoRecord.ColumnWidth != width;
				bool flag3 = columnInfoRecord.OutlineLevel != level;
				bool flag4 = columnInfoRecord.IsHidden != hidden;
				bool flag5 = columnInfoRecord.IsCollapsed != collapsed;
				if (flag || flag2 || flag3 || flag4 || flag5)
				{
					if (columnInfoRecord.FirstColumn == targetColumnIx && columnInfoRecord.LastColumn == targetColumnIx)
					{
						SetColumnInfoFields(columnInfoRecord, xfIndex, width, level, hidden, collapsed);
						AttemptMergeColInfoRecords(num);
					}
					else if (columnInfoRecord.FirstColumn == targetColumnIx || columnInfoRecord.LastColumn == targetColumnIx)
					{
						if (columnInfoRecord.FirstColumn == targetColumnIx)
						{
							columnInfoRecord.FirstColumn = targetColumnIx + 1;
						}
						else
						{
							columnInfoRecord.LastColumn = targetColumnIx - 1;
							num++;
						}
						ColumnInfoRecord columnInfoRecord4 = CopyColInfo(columnInfoRecord);
						columnInfoRecord4.FirstColumn = targetColumnIx;
						columnInfoRecord4.LastColumn = targetColumnIx;
						SetColumnInfoFields(columnInfoRecord4, xfIndex, width, level, hidden, collapsed);
						InsertColumn(num, columnInfoRecord4);
						AttemptMergeColInfoRecords(num);
					}
					else
					{
						ColumnInfoRecord columnInfoRecord5 = columnInfoRecord;
						ColumnInfoRecord columnInfoRecord6 = CopyColInfo(columnInfoRecord);
						ColumnInfoRecord columnInfoRecord7 = CopyColInfo(columnInfoRecord);
						int lastColumn = columnInfoRecord.LastColumn;
						columnInfoRecord5.LastColumn = targetColumnIx - 1;
						columnInfoRecord6.FirstColumn = targetColumnIx;
						columnInfoRecord6.LastColumn = targetColumnIx;
						SetColumnInfoFields(columnInfoRecord6, xfIndex, width, level, hidden, collapsed);
						InsertColumn(++num, columnInfoRecord6);
						columnInfoRecord7.FirstColumn = targetColumnIx + 1;
						columnInfoRecord7.LastColumn = lastColumn;
						InsertColumn(++num, columnInfoRecord7);
					}
				}
			}
		}

		private ColumnInfoRecord CopyColInfo(ColumnInfoRecord ci)
		{
			return (ColumnInfoRecord)ci.Clone();
		}

		/// Sets all non null fields into the <c>ci</c> parameter.
		private void SetColumnInfoFields(ColumnInfoRecord ci, short xfStyle, short width, int level, bool hidden, bool collapsed)
		{
			ci.XFIndex = xfStyle;
			ci.ColumnWidth = width;
			ci.OutlineLevel = (short)level;
			ci.IsHidden = hidden;
			ci.IsCollapsed = collapsed;
		}

		/// <summary>
		/// Collapses the col info records.
		/// </summary>
		/// <param name="columnIdx">The column index.</param>
		public void CollapseColInfoRecords(int columnIdx)
		{
			if (columnIdx != 0)
			{
				ColumnInfoRecord columnInfoRecord = (ColumnInfoRecord)records[columnIdx - 1];
				ColumnInfoRecord columnInfoRecord2 = (ColumnInfoRecord)records[columnIdx];
				if (columnInfoRecord.LastColumn == columnInfoRecord2.FirstColumn - 1 && columnInfoRecord.XFIndex == columnInfoRecord2.XFIndex && columnInfoRecord.Options == columnInfoRecord2.Options && columnInfoRecord.ColumnWidth == columnInfoRecord2.ColumnWidth)
				{
					columnInfoRecord.LastColumn = columnInfoRecord2.LastColumn;
					records.Remove(columnIdx);
				}
			}
		}

		/// <summary>
		/// Creates an outline Group for the specified columns.
		/// </summary>
		/// <param name="fromColumnIx">Group from this column (inclusive)</param>
		/// <param name="toColumnIx">Group to this column (inclusive)</param>
		/// <param name="indent">if true the Group will be indented by one level;if false indenting will be Removed by one level.</param>
		public void GroupColumnRange(int fromColumnIx, int toColumnIx, bool indent)
		{
			int fromColInfoIdx = 0;
			for (int i = fromColumnIx; i <= toColumnIx; i++)
			{
				int value = 1;
				int num = FindColInfoIdx(i, fromColInfoIdx);
				if (num != -1)
				{
					value = GetColInfo(num).OutlineLevel;
					value = ((!indent) ? (value - 1) : (value + 1));
					value = Math.Max(0, value);
					value = Math.Min(7, value);
					fromColInfoIdx = Math.Max(0, num - 1);
				}
				SetColumn(i, null, null, value, null, null);
			}
		}

		/// <summary>
		/// Finds the ColumnInfoRecord
		///  which contains the specified columnIndex
		/// </summary>
		/// <param name="columnIndex">index of the column (not the index of the ColumnInfoRecord)</param>
		/// <returns>        /// <c>null</c>
		///  if no column info found for the specified column
		///  </returns>
		public ColumnInfoRecord FindColumnInfo(int columnIndex)
		{
			int count = records.Count;
			for (int i = 0; i < count; i++)
			{
				ColumnInfoRecord colInfo = GetColInfo(i);
				if (colInfo.ContainsColumn(columnIndex))
				{
					return colInfo;
				}
			}
			return null;
		}

		private int FindColInfoIdx(int columnIx, int fromColInfoIdx)
		{
			if (columnIx < 0)
			{
				throw new ArgumentException("column parameter out of range: " + columnIx);
			}
			if (fromColInfoIdx < 0)
			{
				throw new ArgumentException("fromIdx parameter out of range: " + fromColInfoIdx);
			}
			for (int i = fromColInfoIdx; i < records.Count; i++)
			{
				ColumnInfoRecord colInfo = GetColInfo(i);
				if (colInfo.ContainsColumn(columnIx))
				{
					return i;
				}
				if (colInfo.FirstColumn > columnIx)
				{
					break;
				}
			}
			return -1;
		}
	}
}
