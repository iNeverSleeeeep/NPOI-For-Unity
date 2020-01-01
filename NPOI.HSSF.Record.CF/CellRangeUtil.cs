using NPOI.SS.Util;
using NPOI.Util;
using System;
using System.Collections;

namespace NPOI.HSSF.Record.CF
{
	/// @author Dmitriy Kumshayev
	public class CellRangeUtil
	{
		public const int NO_INTERSECTION = 1;

		public const int OVERLAP = 2;

		/// first range is within the second range 
		public const int INSIDE = 3;

		/// first range encloses or is equal to the second 
		public const int ENCLOSES = 4;

		private CellRangeUtil()
		{
		}

		/// Intersect this range with the specified range.
		///
		/// @param crB - the specified range
		/// @return code which reflects how the specified range is related to this range.<br />
		/// Possible return codes are:	
		/// 		NO_INTERSECTION - the specified range is outside of this range;<br /> 
		/// 		OVERLAP - both ranges partially overlap;<br />
		/// 		INSIDE - the specified range is inside of this one<br />
		/// 		ENCLOSES - the specified range encloses (possibly exactly the same as) this range<br />
		public static int Intersect(CellRangeAddress crA, CellRangeAddress crB)
		{
			int firstRow = crB.FirstRow;
			int lastRow = crB.LastRow;
			int firstColumn = crB.FirstColumn;
			int lastColumn = crB.LastColumn;
			if (gt(crA.FirstRow, lastRow) || lt(crA.LastRow, firstRow) || gt(crA.FirstColumn, lastColumn) || lt(crA.LastColumn, firstColumn))
			{
				return 1;
			}
			if (Contains(crA, crB))
			{
				return 3;
			}
			if (Contains(crB, crA))
			{
				return 4;
			}
			return 2;
		}

		/// Do all possible cell merges between cells of the list so that:
		/// 	if a cell range is completely inside of another cell range, it s removed from the list 
		/// 	if two cells have a shared border, merge them into one bigger cell range
		/// @param cellRangeList
		/// @return updated List of cell ranges
		public static CellRangeAddress[] MergeCellRanges(CellRangeAddress[] cellRanges)
		{
			if (cellRanges.Length < 1)
			{
				return cellRanges;
			}
			ArrayList temp = MergeCellRanges(Arrays.AsList(cellRanges));
			return ToArray(temp);
		}

		private static ArrayList MergeCellRanges(ArrayList cellRangeList)
		{
			while (cellRangeList.Count > 1)
			{
				bool flag = false;
				for (int i = 0; i < cellRangeList.Count; i++)
				{
					CellRangeAddress range = (CellRangeAddress)cellRangeList[i];
					for (int j = i + 1; j < cellRangeList.Count; j++)
					{
						CellRangeAddress range2 = (CellRangeAddress)cellRangeList[j];
						CellRangeAddress[] array = MergeRanges(range, range2);
						if (array != null)
						{
							flag = true;
							cellRangeList[i] = array[0];
							cellRangeList.RemoveAt(j--);
							for (int k = 1; k < array.Length; k++)
							{
								j++;
								cellRangeList.Insert(j, array[k]);
							}
						}
					}
				}
				if (!flag)
				{
					break;
				}
			}
			return cellRangeList;
		}

		/// @return the new range(s) to replace the supplied ones.  <c>null</c> if no merge is possible
		private static CellRangeAddress[] MergeRanges(CellRangeAddress range1, CellRangeAddress range2)
		{
			int num = Intersect(range1, range2);
			switch (num)
			{
			case 1:
				if (HasExactSharedBorder(range1, range2))
				{
					return new CellRangeAddress[1]
					{
						CreateEnclosingCellRange(range1, range2)
					};
				}
				return null;
			case 2:
				return ResolveRangeOverlap(range1, range2);
			case 3:
				return new CellRangeAddress[1]
				{
					range1
				};
			case 4:
				return new CellRangeAddress[1]
				{
					range2
				};
			default:
				throw new InvalidOperationException("unexpected intersection result (" + num + ")");
			}
		}

		private static CellRangeAddress[] ResolveRangeOverlap(CellRangeAddress rangeA, CellRangeAddress rangeB)
		{
			if (rangeA.IsFullColumnRange)
			{
				if (rangeA.IsFullRowRange)
				{
					return null;
				}
				return SliceUp(rangeA, rangeB);
			}
			if (rangeA.IsFullRowRange)
			{
				if (rangeB.IsFullColumnRange)
				{
					return null;
				}
				return SliceUp(rangeA, rangeB);
			}
			if (rangeB.IsFullColumnRange)
			{
				return SliceUp(rangeB, rangeA);
			}
			if (rangeB.IsFullRowRange)
			{
				return SliceUp(rangeB, rangeA);
			}
			return SliceUp(rangeA, rangeB);
		}

		/// @param crB never a full row or full column range
		/// @return an array including <b>this</b> <c>CellRange</c> and all parts of <c>range</c> 
		/// outside of this range  
		private static CellRangeAddress[] SliceUp(CellRangeAddress crA, CellRangeAddress crB)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(crB);
			if (!crA.IsFullColumnRange)
			{
				arrayList = CutHorizontally(crA.FirstRow, arrayList);
				arrayList = CutHorizontally(crA.LastRow + 1, arrayList);
			}
			if (!crA.IsFullRowRange)
			{
				arrayList = CutVertically(crA.FirstColumn, arrayList);
				arrayList = CutVertically(crA.LastColumn + 1, arrayList);
			}
			CellRangeAddress[] array = ToArray(arrayList);
			arrayList.Clear();
			arrayList.Add(crA);
			foreach (CellRangeAddress cellRangeAddress in array)
			{
				if (Intersect(crA, cellRangeAddress) != 4)
				{
					arrayList.Add(cellRangeAddress);
				}
			}
			return ToArray(arrayList);
		}

		private static ArrayList CutHorizontally(int cutRow, ArrayList input)
		{
			ArrayList arrayList = new ArrayList();
			CellRangeAddress[] array = ToArray(input);
			foreach (CellRangeAddress cellRangeAddress in array)
			{
				if (cellRangeAddress.FirstRow < cutRow && cutRow < cellRangeAddress.LastRow)
				{
					arrayList.Add(new CellRangeAddress(cellRangeAddress.FirstRow, cutRow, cellRangeAddress.FirstColumn, cellRangeAddress.LastColumn));
					arrayList.Add(new CellRangeAddress(cutRow + 1, cellRangeAddress.LastRow, cellRangeAddress.FirstColumn, cellRangeAddress.LastColumn));
				}
				else
				{
					arrayList.Add(cellRangeAddress);
				}
			}
			return arrayList;
		}

		private static ArrayList CutVertically(int cutColumn, ArrayList input)
		{
			ArrayList arrayList = new ArrayList();
			CellRangeAddress[] array = ToArray(input);
			foreach (CellRangeAddress cellRangeAddress in array)
			{
				if (cellRangeAddress.FirstColumn < cutColumn && cutColumn < cellRangeAddress.LastColumn)
				{
					arrayList.Add(new CellRangeAddress(cellRangeAddress.FirstRow, cellRangeAddress.LastRow, cellRangeAddress.FirstColumn, cutColumn));
					arrayList.Add(new CellRangeAddress(cellRangeAddress.FirstRow, cellRangeAddress.LastRow, cutColumn + 1, cellRangeAddress.LastColumn));
				}
				else
				{
					arrayList.Add(cellRangeAddress);
				}
			}
			return arrayList;
		}

		private static CellRangeAddress[] ToArray(ArrayList temp)
		{
			CellRangeAddress[] array = new CellRangeAddress[temp.Count];
			return (CellRangeAddress[])temp.ToArray(typeof(CellRangeAddress));
		}

		/// Check if the specified range is located inside of this cell range.
		///
		///             @param crB
		///             @return true if this cell range Contains the argument range inside if it's area
		public static bool Contains(CellRangeAddress crA, CellRangeAddress crB)
		{
			int firstRow = crB.FirstRow;
			int lastRow = crB.LastRow;
			int firstColumn = crB.FirstColumn;
			int lastColumn = crB.LastColumn;
			if (le(crA.FirstRow, firstRow) && ge(crA.LastRow, lastRow) && le(crA.FirstColumn, firstColumn))
			{
				return ge(crA.LastColumn, lastColumn);
			}
			return false;
		}

		/// Check if the specified cell range has a shared border with the current range.
		///
		/// @return <c>true</c> if the ranges have a complete shared border (i.e.
		/// the two ranges toher make a simple rectangular region.
		public static bool HasExactSharedBorder(CellRangeAddress crA, CellRangeAddress crB)
		{
			int firstRow = crB.FirstRow;
			int lastRow = crB.LastRow;
			int firstColumn = crB.FirstColumn;
			int lastColumn = crB.LastColumn;
			if ((crA.FirstRow > 0 && crA.FirstRow - 1 == lastRow) || (firstRow > 0 && firstRow - 1 == crA.LastRow))
			{
				if (crA.FirstColumn == firstColumn)
				{
					return crA.LastColumn == lastColumn;
				}
				return false;
			}
			if ((crA.FirstColumn > 0 && crA.FirstColumn - 1 == lastColumn) || (firstColumn > 0 && crA.LastColumn == firstColumn - 1))
			{
				if (crA.FirstRow == firstRow)
				{
					return crA.LastRow == lastRow;
				}
				return false;
			}
			return false;
		}

		/// Create an enclosing CellRange for the two cell ranges.
		///
		/// @return enclosing CellRange
		public static CellRangeAddress CreateEnclosingCellRange(CellRangeAddress crA, CellRangeAddress crB)
		{
			if (crB == null)
			{
				return crA.Copy();
			}
			return new CellRangeAddress(lt(crB.FirstRow, crA.FirstRow) ? crB.FirstRow : crA.FirstRow, gt(crB.LastRow, crA.LastRow) ? crB.LastRow : crA.LastRow, lt(crB.FirstColumn, crA.FirstColumn) ? crB.FirstColumn : crA.FirstColumn, gt(crB.LastColumn, crA.LastColumn) ? crB.LastColumn : crA.LastColumn);
		}

		/// @return true if a &lt; b
		private static bool lt(int a, int b)
		{
			if (a != -1)
			{
				if (b != -1)
				{
					return a < b;
				}
				return true;
			}
			return false;
		}

		/// @return true if a &lt;= b
		private static bool le(int a, int b)
		{
			if (a != b)
			{
				return lt(a, b);
			}
			return true;
		}

		/// @return true if a &gt; b
		private static bool gt(int a, int b)
		{
			return lt(b, a);
		}

		/// @return true if a &gt;= b
		private static bool ge(int a, int b)
		{
			return !lt(a, b);
		}
	}
}
