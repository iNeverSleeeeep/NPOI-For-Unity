using NPOI.HSSF.Record;
using NPOI.SS;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// High level representation of a row of a spReadsheet.
	/// Only rows that have cells should be Added to a Sheet.
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Glen Stampoultzis (glens at apache.org)
	/// </summary>
	[Serializable]
	public class HSSFRow : IComparable, IRow
	{
		/// <summary>
		/// used for collections
		/// </summary>
		public const int INITIAL_CAPACITY = 5;

		private int rowNum;

		private SortedDictionary<int, ICell> cells = new SortedDictionary<int, ICell>();

		/// reference to low level representation
		[NonSerialized]
		private RowRecord row;

		/// reference to containing low level Workbook
		private HSSFWorkbook book;

		/// reference to containing Sheet
		private HSSFSheet sheet;

		/// <summary>
		/// true, when the row is invisible. This is the case when the height is zero.
		/// </summary>
		public bool IsHidden
		{
			get
			{
				return ZeroHeight;
			}
			set
			{
				ZeroHeight = value;
			}
		}

		/// <summary>
		/// Get row number this row represents
		/// </summary>
		/// <value>the row number (0 based)</value>
		public int RowNum
		{
			get
			{
				return rowNum;
			}
			set
			{
				int lastRowIndex = SpreadsheetVersion.EXCEL97.LastRowIndex;
				if (value < 0 || value > lastRowIndex)
				{
					throw new ArgumentException("Invalid row number (" + value + ") outside allowable range (0.." + lastRowIndex + ")");
				}
				rowNum = value;
				if (row != null)
				{
					row.RowNumber = value;
				}
			}
		}

		/// <summary>
		/// Returns the rows outline level. Increased as you
		/// put it into more Groups (outlines), reduced as
		/// you take it out of them.
		/// </summary>
		/// <value>The outline level.</value>
		public int OutlineLevel => row.OutlineLevel;

		/// Returns the HSSFSheet this row belongs to
		///
		/// @return the HSSFSheet that owns this row
		public ISheet Sheet => sheet;

		/// <summary>
		/// Get the number of the first cell contained in this row.
		/// </summary>
		/// <value>the first logical cell in the row, or -1 if the row does not contain any cells.</value>
		public short FirstCellNum
		{
			get
			{
				if (row.IsEmpty)
				{
					return -1;
				}
				return (short)row.FirstCol;
			}
		}

		/// <summary>
		/// Gets the index of the last cell contained in this row PLUS ONE
		/// . The result also happens to be the 1-based column number of the last cell.  This value can be used as a
		/// standard upper bound when iterating over cells:
		/// </summary>
		/// <value>
		/// short representing the last logical cell in the row PLUS ONE, or -1 if the
		/// row does not contain any cells.
		///             </value>
		/// <example>
		/// short minColIx = row.GetFirstCellNum();
		/// short maxColIx = row.GetLastCellNum();
		/// for(short colIx=minColIx; colIx&lt;maxColIx; colIx++) {
		/// Cell cell = row.GetCell(colIx);
		/// if(cell == null) {
		/// continue;
		/// }
		/// //... do something with cell
		/// }
		/// </example>
		public short LastCellNum
		{
			get
			{
				if (row.IsEmpty)
				{
					return -1;
				}
				return (short)row.LastCol;
			}
		}

		/// <summary>
		/// Gets the number of defined cells (NOT number of cells in the actual row!).
		/// That is to say if only columns 0,4,5 have values then there would be 3.
		/// </summary>
		/// <value>the number of defined cells in the row.</value>
		public int PhysicalNumberOfCells => cells.Count;

		/// <summary>
		/// Gets or sets  whether or not to Display this row with 0 height
		/// </summary>
		/// <value>height is zero or not.</value>
		public bool ZeroHeight
		{
			get
			{
				return row.ZeroHeight;
			}
			set
			{
				row.ZeroHeight = value;
			}
		}

		/// <summary>
		/// Get or sets the row's height or ff (-1) for undefined/default-height in twips (1/20th of a point)
		/// </summary>
		/// <value>rowheight or 0xff for Undefined (use sheet default)</value>
		public short Height
		{
			get
			{
				short height = row.Height;
				if ((height & 0x8000) != 0)
				{
					return sheet.Sheet.DefaultRowHeight;
				}
				return (short)(height & 0x7FFF);
			}
			set
			{
				if (value == -1)
				{
					row.Height = -32513;
					row.BadFontHeight = false;
				}
				else
				{
					row.BadFontHeight = true;
					row.Height = value;
				}
			}
		}

		/// <summary>
		/// is this row formatted? Most aren't, but some rows
		/// do have whole-row styles. For those that do, you
		/// can get the formatting from {@link #getRowStyle()}
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is formatted; otherwise, <c>false</c>.
		/// </value>
		public bool IsFormatted => row.Formatted;

		/// <summary>
		/// Returns the whole-row cell styles. Most rows won't
		/// have one of these, so will return null. Call IsFormmated to check first
		/// </summary>
		/// <value>The row style.</value>
		public ICellStyle RowStyle
		{
			get
			{
				if (!IsFormatted)
				{
					return null;
				}
				short xFIndex = row.XFIndex;
				ExtendedFormatRecord exFormatAt = book.Workbook.GetExFormatAt(xFIndex);
				return new HSSFCellStyle(xFIndex, exFormatAt, book);
			}
			set
			{
				row.Formatted = true;
				row.XFIndex = value.Index;
			}
		}

		/// <summary>
		/// Get the row's height or ff (-1) for Undefined/default-height in points (20*Height)
		/// </summary>
		/// <value>row height or 0xff for Undefined (use sheet default).</value>
		public float HeightInPoints
		{
			get
			{
				return (float)Height / 20f;
			}
			set
			{
				if (value == -1f)
				{
					row.Height = -32513;
				}
				else
				{
					row.BadFontHeight = true;
					row.Height = (short)(value * 20f);
				}
			}
		}

		/// <summary>
		/// Get the lowlevel RowRecord represented by this object - should only be called
		/// by other parts of the high level API
		/// </summary>
		/// <value>RowRecord this row represents</value>
		public RowRecord RowRecord => row;

		/// <summary>
		/// Get cells in the row (existing cells only, no blanks)
		/// </summary>
		public List<ICell> Cells => new List<ICell>(cells.Values);

		[Obsolete]
		public HSSFRow()
		{
		}

		/// <summary>
		/// Creates new HSSFRow from scratch. Only HSSFSheet should do this.
		/// </summary>
		/// <param name="book">low-level Workbook object containing the sheet that Contains this row</param>
		/// <param name="sheet">low-level Sheet object that Contains this Row</param>
		/// <param name="rowNum">the row number of this row (0 based)</param>
		///             <see cref="M:NPOI.HSSF.UserModel.HSSFSheet.CreateRow(System.Int32)" />
		public HSSFRow(HSSFWorkbook book, HSSFSheet sheet, int rowNum)
			: this(book, sheet, new RowRecord(rowNum))
		{
		}

		/// <summary>
		/// Creates an HSSFRow from a low level RowRecord object.  Only HSSFSheet should do
		/// this.  HSSFSheet uses this when an existing file is Read in.
		/// </summary>
		/// <param name="book">low-level Workbook object containing the sheet that Contains this row</param>
		/// <param name="sheet"> low-level Sheet object that Contains this Row</param>
		/// <param name="record">the low level api object this row should represent</param>
		///             <see cref="M:NPOI.HSSF.UserModel.HSSFSheet.CreateRow(System.Int32)" />
		public HSSFRow(HSSFWorkbook book, HSSFSheet sheet, RowRecord record)
		{
			this.book = book;
			this.sheet = sheet;
			row = record;
			RowNum = record.RowNumber;
			record.SetEmpty();
		}

		/// <summary>
		/// Use this to create new cells within the row and return it.
		/// The cell that is returned is a CELL_TYPE_BLANK (<see cref="T:NPOI.SS.UserModel.ICell" />/<see cref="F:NPOI.SS.UserModel.CellType.Blank" />). 
		/// The type can be changed either through calling <c>SetCellValue</c> or <c>SetCellType</c>.
		/// </summary>
		/// <param name="column">the column number this cell represents</param>
		/// <returns>a high level representation of the created cell.</returns>
		public ICell CreateCell(int column)
		{
			return CreateCell(column, CellType.Blank);
		}

		/// <summary>
		/// Use this to create new cells within the row and return it.
		/// The cell that is returned is a CELL_TYPE_BLANK. The type can be changed
		/// either through calling setCellValue or setCellType.
		/// </summary>
		/// <param name="columnIndex">the column number this cell represents</param>
		/// <param name="type">a high level representation of the created cell.</param>
		/// <returns></returns>
		public ICell CreateCell(int columnIndex, CellType type)
		{
			ICell cell = new HSSFCell(book, sheet, RowNum, (short)columnIndex, type);
			AddCell(cell);
			sheet.Sheet.AddValueRecord(RowNum, ((HSSFCell)cell).CellValueRecord);
			return cell;
		}

		public IRow CopyRowTo(int targetIndex)
		{
			return sheet.CopyRow(RowNum, targetIndex);
		}

		public ICell CopyCell(int sourceIndex, int targetIndex)
		{
			return CellUtil.CopyCell(this, sourceIndex, targetIndex);
		}

		/// <summary>
		/// Remove the Cell from this row.
		/// </summary>
		/// <param name="cell">The cell to Remove.</param>
		public void RemoveCell(ICell cell)
		{
			if (cell == null)
			{
				throw new ArgumentException("cell must not be null");
			}
			RemoveCell((HSSFCell)cell, alsoRemoveRecords: true);
		}

		/// <summary>
		/// Removes the cell.
		/// </summary>
		/// <param name="cell">The cell.</param>
		/// <param name="alsoRemoveRecords">if set to <c>true</c> [also remove records].</param>
		private void RemoveCell(ICell cell, bool alsoRemoveRecords)
		{
			int columnIndex = cell.ColumnIndex;
			if (columnIndex < 0)
			{
				throw new Exception("Negative cell indexes not allowed");
			}
			if (!cells.ContainsKey(columnIndex) || cell != cells[columnIndex])
			{
				throw new Exception("Specified cell is not from this row");
			}
			if (cell.IsPartOfArrayFormulaGroup)
			{
				((HSSFCell)cell).NotifyArrayFormulaChanging();
			}
			cells.Remove(columnIndex);
			if (alsoRemoveRecords)
			{
				CellValueRecordInterface cellValueRecord = ((HSSFCell)cell).CellValueRecord;
				sheet.Sheet.RemoveValueRecord(RowNum, cellValueRecord);
			}
			if (cell.ColumnIndex + 1 == row.LastCol)
			{
				row.LastCol = CalculateNewLastCellPlusOne(row.LastCol);
			}
			if (cell.ColumnIndex == row.FirstCol)
			{
				row.FirstCol = CalculateNewFirstCell(row.FirstCol);
			}
		}

		/// used internally to refresh the "last cell plus one" when the last cell is removed.
		/// @return 0 when row contains no cells
		private int CalculateNewLastCellPlusOne(int lastcell)
		{
			int num = lastcell - 1;
			for (ICell cell = RetrieveCell(num); cell == null; cell = RetrieveCell(--num))
			{
				if (num < 0)
				{
					return 0;
				}
			}
			return num + 1;
		}

		/// used internally to refresh the "first cell" when the first cell is removed.
		/// @return 0 when row contains no cells (also when first cell is occupied)
		private int CalculateNewFirstCell(int firstcell)
		{
			int num = firstcell + 1;
			ICell cell = RetrieveCell(num);
			if (cells.Count == 0)
			{
				return 0;
			}
			while (cell == null)
			{
				if (num <= cells.Count)
				{
					return 0;
				}
				cell = RetrieveCell(++num);
			}
			return num;
		}

		/// <summary>
		/// Create a high level Cell object from an existing low level record.  Should
		/// only be called from HSSFSheet or HSSFRow itself.
		/// </summary>
		/// <param name="cell">The low level cell to Create the high level representation from</param>
		/// <returns> the low level record passed in</returns>
		public ICell CreateCellFromRecord(CellValueRecordInterface cell)
		{
			ICell cell2 = new HSSFCell(book, sheet, cell);
			AddCell(cell2);
			int column = cell.Column;
			if (row.IsEmpty)
			{
				row.FirstCol = column;
				row.LastCol = column + 1;
			}
			else if (column < row.FirstCol)
			{
				row.FirstCol = column;
			}
			else if (column > row.LastCol)
			{
				row.LastCol = column + 1;
			}
			return cell2;
		}

		/// <summary>
		/// Removes all the cells from the row, and their
		/// records too.
		/// </summary>
		public void RemoveAllCells()
		{
			ICell[] array = new ICell[cells.Values.Count];
			cells.Values.CopyTo(array, 0);
			ICell[] array2 = array;
			foreach (ICell cell in array2)
			{
				RemoveCell(cell, alsoRemoveRecords: true);
			}
		}

		/// <summary>
		/// Moves the supplied cell to a new column, which
		/// must not already have a cell there!
		/// </summary>
		/// <param name="cell">The cell to move</param>
		/// <param name="newColumn">The new column number (0 based)</param>
		public void MoveCell(ICell cell, int newColumn)
		{
			if (cells.ContainsKey(newColumn))
			{
				throw new ArgumentException("Asked to move cell to column " + newColumn + " but there's already a cell there");
			}
			bool flag = false;
			foreach (ICell value in cells.Values)
			{
				if (value.Equals(cell))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				throw new ArgumentException("Asked to move a cell, but it didn't belong to our row");
			}
			RemoveCell(cell, alsoRemoveRecords: false);
			((HSSFCell)cell).UpdateCellNum(newColumn);
			AddCell(cell);
		}

		/// <summary>
		/// used internally to Add a cell.
		/// </summary>
		/// <param name="cell">The cell.</param>
		private void AddCell(ICell cell)
		{
			int columnIndex = cell.ColumnIndex;
			if (cells.ContainsKey(columnIndex))
			{
				cells.Remove(columnIndex);
			}
			cells.Add(columnIndex, cell);
			if (row.IsEmpty || columnIndex < row.FirstCol)
			{
				row.FirstCol = columnIndex;
			}
			if (row.IsEmpty || columnIndex >= row.LastCol)
			{
				row.LastCol = (short)(columnIndex + 1);
			}
		}

		/// <summary>
		/// Get the hssfcell representing a given column (logical cell)
		/// 0-based. If you ask for a cell that is not defined, then
		/// you Get a null.
		/// This is the basic call, with no policies applied
		/// </summary>
		/// <param name="cellnum">0 based column number</param>
		/// <returns>Cell representing that column or null if Undefined.</returns>
		private ICell RetrieveCell(int cellnum)
		{
			if (!cells.ContainsKey(cellnum))
			{
				return null;
			}
			return cells[cellnum];
		}

		/// <summary>
		/// Get the hssfcell representing a given column (logical cell)
		/// 0-based.  If you ask for a cell that is not defined then
		/// you get a null, unless you have set a different
		/// MissingCellPolicy on the base workbook.
		///
		/// Short method signature provided to retain binary
		/// compatibility.
		/// </summary>
		/// <param name="cellnum">0 based column number</param>
		/// <returns>Cell representing that column or null if undefined.</returns>
		[Obsolete]
		public ICell GetCell(short cellnum)
		{
			int cellnum2 = cellnum & 0xFFFF;
			return GetCell(cellnum2);
		}

		/// <summary>
		/// Get the hssfcell representing a given column (logical cell)
		/// 0-based.  If you ask for a cell that is not defined then
		/// you get a null, unless you have set a different
		/// MissingCellPolicy on the base workbook.
		/// </summary>
		/// <param name="cellnum">0 based column number</param>
		/// <returns>Cell representing that column or null if undefined.</returns>
		public ICell GetCell(int cellnum)
		{
			return GetCell(cellnum, book.MissingCellPolicy);
		}

		/// <summary>
		/// Get the hssfcell representing a given column (logical cell)
		/// 0-based.  If you ask for a cell that is not defined, then
		/// your supplied policy says what to do
		/// </summary>
		/// <param name="cellnum">0 based column number</param>
		/// <param name="policy">Policy on blank / missing cells</param>
		/// <returns>that column or null if Undefined + policy allows.</returns>
		public ICell GetCell(int cellnum, MissingCellPolicy policy)
		{
			ICell cell = RetrieveCell(cellnum);
			if (policy == MissingCellPolicy.RETURN_NULL_AND_BLANK)
			{
				return cell;
			}
			if (policy == MissingCellPolicy.RETURN_BLANK_AS_NULL)
			{
				if (cell == null)
				{
					return cell;
				}
				if (cell.CellType == CellType.Blank)
				{
					return null;
				}
				return cell;
			}
			if (policy == MissingCellPolicy.CREATE_NULL_AS_BLANK)
			{
				if (cell == null)
				{
					return CreateCell(cellnum, CellType.Blank);
				}
				return cell;
			}
			throw new ArgumentException("Illegal policy " + policy + " (" + policy.id + ")");
		}

		/// <summary>
		/// used internally to refresh the "first cell" when the first cell is Removed.
		/// </summary>
		/// <param name="firstcell">The first cell index.</param>
		/// <returns></returns>
		[Obsolete]
		private short FindFirstCell(int firstcell)
		{
			int num = firstcell + 1;
			ICell cell = GetCell(num);
			while (cell == null && num <= LastCellNum)
			{
				cell = GetCell(++num);
			}
			if (num > LastCellNum)
			{
				return -1;
			}
			return (short)num;
		}

		/// <summary>
		/// Gets the cell enumerator of the physically defined cells.
		/// </summary>
		/// <remarks>
		/// Note that the 4th element might well not be cell 4, as the iterator
		/// will not return Un-defined (null) cells.
		/// Call CellNum on the returned cells to know which cell they are.
		/// </remarks>
		public IEnumerator GetEnumerator()
		{
			return cells.Values.GetEnumerator();
		}

		/// <summary>
		/// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
		/// </summary>
		/// <param name="obj">An object to compare with this instance.</param>
		/// <returns>
		/// A 32-bit signed integer that indicates the relative order of the objects being compared. The return value has these meanings:
		/// Value
		/// Meaning
		/// Less than zero
		/// This instance is less than <paramref name="obj" />.
		/// Zero
		/// This instance is equal to <paramref name="obj" />.
		/// Greater than zero
		/// This instance is greater than <paramref name="obj" />.
		/// </returns>
		/// <exception cref="T:System.ArgumentException">
		/// 	<paramref name="obj" /> is not the same type as this instance.
		/// </exception>
		public int CompareTo(object obj)
		{
			HSSFRow hSSFRow = (HSSFRow)obj;
			if (RowNum == hSSFRow.RowNum)
			{
				return 0;
			}
			if (RowNum < hSSFRow.RowNum)
			{
				return -1;
			}
			if (RowNum > hSSFRow.RowNum)
			{
				return 1;
			}
			return -1;
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
		/// </summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
		/// <returns>
		/// true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.
		/// </returns>
		/// <exception cref="T:System.NullReferenceException">
		/// The <paramref name="obj" /> parameter is null.
		/// </exception>
		public override bool Equals(object obj)
		{
			if (!(obj is HSSFRow))
			{
				return false;
			}
			HSSFRow hSSFRow = (HSSFRow)obj;
			if (RowNum == hSSFRow.RowNum)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Returns a hash code. In this case it is the number of the row.
		/// </summary>
		public override int GetHashCode()
		{
			return RowNum;
		}
	}
}
