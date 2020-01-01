using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.XSSF.Util;
using System.Collections.Generic;

namespace NPOI.XSSF.UserModel.Helpers
{
	/// Helper class for dealing with the Column Settings on
	///  a CT_Worksheet (the data part of a sheet).
	/// Note - within POI, we use 0 based column indexes, but
	///  the column defInitions in the XML are 1 based!
	public class ColumnHelper
	{
		private CT_Worksheet worksheet;

		private CT_Cols newCols;

		public ColumnHelper(CT_Worksheet worksheet)
		{
			this.worksheet = worksheet;
			CleanColumns();
		}

		public void CleanColumns()
		{
			newCols = new CT_Cols();
			List<CT_Cols> colsArray = worksheet.GetColsArray();
			if (colsArray != null)
			{
				int num = 0;
				for (num = 0; num < colsArray.Count; num++)
				{
					CT_Cols cT_Cols = colsArray[num];
					List<CT_Col> colArray = cT_Cols.GetColArray();
					for (int i = 0; i < colArray.Count; i++)
					{
						CT_Col col = colArray[i];
						newCols = AddCleanColIntoCols(newCols, col);
					}
				}
				for (int num2 = num - 1; num2 >= 0; num2--)
				{
					worksheet.RemoveCols(num2);
				}
			}
			worksheet.AddNewCols();
			worksheet.SetColsArray(0, newCols);
		}

		public static void SortColumns(CT_Cols newCols)
		{
			List<CT_Col> colArray = newCols.GetColArray();
			colArray.Sort(new CTColComparator());
			newCols.SetColArray(colArray);
		}

		public CT_Col CloneCol(CT_Cols cols, CT_Col col)
		{
			CT_Col cT_Col = cols.AddNewCol();
			cT_Col.min = col.min;
			cT_Col.max = col.max;
			SetColumnAttributes(col, cT_Col);
			return cT_Col;
		}

		/// Returns the Column at the given 0 based index
		public CT_Col GetColumn(long index, bool splitColumns)
		{
			return GetColumn1Based(index + 1, splitColumns);
		}

		/// Returns the Column at the given 1 based index.
		/// POI default is 0 based, but the file stores
		///  as 1 based.
		public CT_Col GetColumn1Based(long index1, bool splitColumns)
		{
			CT_Cols colsArray = worksheet.GetColsArray(0);
			for (int i = 0; i < colsArray.sizeOfColArray(); i++)
			{
				CT_Col colArray = colsArray.GetColArray(i);
				if (colArray.min <= index1 && colArray.max >= index1)
				{
					if (splitColumns)
					{
						if (colArray.min < index1)
						{
							insertCol(colsArray, colArray.min, index1 - 1, new CT_Col[1]
							{
								colArray
							});
						}
						if (colArray.max > index1)
						{
							insertCol(colsArray, index1 + 1, colArray.max, new CT_Col[1]
							{
								colArray
							});
						}
						colArray.min = (uint)index1;
						colArray.max = (uint)index1;
					}
					return colArray;
				}
			}
			return null;
		}

		public CT_Cols AddCleanColIntoCols(CT_Cols cols, CT_Col col)
		{
			bool flag = false;
			for (int i = 0; i < cols.sizeOfColArray(); i++)
			{
				CT_Col colArray = cols.GetColArray(i);
				long[] range = new long[2]
				{
					colArray.min,
					colArray.max
				};
				long[] range2 = new long[2]
				{
					col.min,
					col.max
				};
				long[] overlappingRange = NumericRanges.GetOverlappingRange(range, range2);
				int overlappingType = NumericRanges.GetOverlappingType(range, range2);
				switch (overlappingType)
				{
				case 0:
					colArray.max = (uint)(overlappingRange[0] - 1);
					insertCol(cols, overlappingRange[0], overlappingRange[1], new CT_Col[2]
					{
						colArray,
						col
					});
					i++;
					insertCol(cols, overlappingRange[1] + 1, col.max, new CT_Col[1]
					{
						col
					});
					i++;
					break;
				case 1:
					colArray.min = (uint)(overlappingRange[1] + 1);
					insertCol(cols, overlappingRange[0], overlappingRange[1], new CT_Col[2]
					{
						colArray,
						col
					});
					i++;
					insertCol(cols, col.min, overlappingRange[0] - 1, new CT_Col[1]
					{
						col
					});
					i++;
					break;
				case 3:
					SetColumnAttributes(col, colArray);
					if (col.min != colArray.min)
					{
						insertCol(cols, col.min, colArray.min - 1, new CT_Col[1]
						{
							col
						});
						i++;
					}
					if (col.max != colArray.max)
					{
						insertCol(cols, colArray.max + 1, col.max, new CT_Col[1]
						{
							col
						});
						i++;
					}
					break;
				case 2:
					if (col.min != colArray.min)
					{
						insertCol(cols, colArray.min, col.min - 1, new CT_Col[1]
						{
							colArray
						});
						i++;
					}
					if (col.max != colArray.max)
					{
						insertCol(cols, col.max + 1, colArray.max, new CT_Col[1]
						{
							colArray
						});
						i++;
					}
					colArray.min = (uint)overlappingRange[0];
					colArray.max = (uint)overlappingRange[1];
					SetColumnAttributes(col, colArray);
					break;
				}
				if (overlappingType != -1)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				CloneCol(cols, col);
			}
			SortColumns(cols);
			return cols;
		}

		private CT_Col insertCol(CT_Cols cols, long min, long max, CT_Col[] colsWithAttributes)
		{
			if (!columnExists(cols, min, max))
			{
				CT_Col cT_Col = cols.InsertNewCol(0);
				cT_Col.min = (uint)min;
				cT_Col.max = (uint)max;
				foreach (CT_Col fromCol in colsWithAttributes)
				{
					SetColumnAttributes(fromCol, cT_Col);
				}
				return cT_Col;
			}
			return null;
		}

		/// Does the column at the given 0 based index exist
		///  in the supplied list of column defInitions?
		public bool columnExists(CT_Cols cols, long index)
		{
			return columnExists1Based(cols, index + 1);
		}

		private bool columnExists1Based(CT_Cols cols, long index1)
		{
			for (int i = 0; i < cols.sizeOfColArray(); i++)
			{
				if (cols.GetColArray(i).min == index1)
				{
					return true;
				}
			}
			return false;
		}

		public void SetColumnAttributes(CT_Col fromCol, CT_Col toCol)
		{
			if (fromCol.IsSetBestFit())
			{
				toCol.bestFit = fromCol.bestFit;
			}
			if (fromCol.IsSetCustomWidth())
			{
				toCol.customWidth = fromCol.customWidth;
			}
			if (fromCol.IsSetHidden())
			{
				toCol.hidden = fromCol.hidden;
			}
			if (fromCol.IsSetStyle())
			{
				toCol.style = fromCol.style;
				toCol.styleSpecified = true;
			}
			if (fromCol.IsSetWidth())
			{
				toCol.width = fromCol.width;
			}
			if (fromCol.IsSetCollapsed())
			{
				toCol.collapsed = fromCol.collapsed;
			}
			if (fromCol.IsSetPhonetic())
			{
				toCol.phonetic = fromCol.phonetic;
			}
			if (fromCol.IsSetOutlineLevel())
			{
				toCol.outlineLevel = fromCol.outlineLevel;
			}
			if (fromCol.IsSetCollapsed())
			{
				toCol.collapsed = fromCol.collapsed;
			}
		}

		public void SetColBestFit(long index, bool bestFit)
		{
			CT_Col orCreateColumn1Based = GetOrCreateColumn1Based(index + 1, false);
			orCreateColumn1Based.bestFit = bestFit;
		}

		public void SetCustomWidth(long index, bool width)
		{
			CT_Col orCreateColumn1Based = GetOrCreateColumn1Based(index + 1, true);
			orCreateColumn1Based.customWidth = width;
		}

		public void SetColWidth(long index, double width)
		{
			CT_Col orCreateColumn1Based = GetOrCreateColumn1Based(index + 1, true);
			orCreateColumn1Based.width = width;
		}

		public void SetColHidden(long index, bool hidden)
		{
			CT_Col orCreateColumn1Based = GetOrCreateColumn1Based(index + 1, true);
			orCreateColumn1Based.hidden = hidden;
		}

		/// Return the CT_Col at the given (0 based) column index,
		///  creating it if required.
		internal CT_Col GetOrCreateColumn1Based(long index1, bool splitColumns)
		{
			CT_Col cT_Col = GetColumn1Based(index1, splitColumns);
			if (cT_Col == null)
			{
				cT_Col = worksheet.GetColsArray(0).AddNewCol();
				cT_Col.min = (uint)index1;
				cT_Col.max = (uint)index1;
			}
			return cT_Col;
		}

		public void SetColDefaultStyle(long index, ICellStyle style)
		{
			SetColDefaultStyle(index, style.Index);
		}

		public void SetColDefaultStyle(long index, int styleId)
		{
			CT_Col orCreateColumn1Based = GetOrCreateColumn1Based(index + 1, true);
			orCreateColumn1Based.style = (uint)styleId;
			orCreateColumn1Based.styleSpecified = true;
		}

		public int GetColDefaultStyle(long index)
		{
			if (GetColumn(index, false) != null)
			{
				return (int)GetColumn(index, false).style;
			}
			return -1;
		}

		private bool columnExists(CT_Cols cols, long min, long max)
		{
			for (int i = 0; i < cols.sizeOfColArray(); i++)
			{
				if (cols.GetColArray(i).min == min && cols.GetColArray(i).max == max)
				{
					return true;
				}
			}
			return false;
		}

		public int GetIndexOfColumn(CT_Cols cols, CT_Col col)
		{
			for (int i = 0; i < cols.sizeOfColArray(); i++)
			{
				if (cols.GetColArray(i).min == col.min && cols.GetColArray(i).max == col.max)
				{
					return i;
				}
			}
			return -1;
		}
	}
}
