using System.Collections;
using System.Collections.Generic;

namespace NPOI.SS.UserModel
{
	/// <summary>
	/// High level representation of a row of a spreadsheet.
	/// </summary>    
	public interface IRow
	{
		/// <summary>
		/// Get row number this row represents
		/// </summary>        
		/// <returns>the row number (0 based)</returns>
		int RowNum
		{
			get;
			set;
		}

		/// <summary>
		/// Get the number of the first cell Contained in this row.
		/// </summary>
		/// <returns>
		/// short representing the first logical cell in the row,
		/// or -1 if the row does not contain any cells.
		/// </returns>
		short FirstCellNum
		{
			get;
		}

		/// <summary>
		/// Gets the index of the last cell Contained in this row <b>PLUS ONE</b>. The result also
		/// happens to be the 1-based column number of the last cell.  This value can be used as a
		/// standard upper bound when iterating over cells:
		/// <pre>
		/// short minColIx = row.GetFirstCellNum();
		/// short maxColIx = row.GetLastCellNum();
		/// for(short colIx=minColIx; colIx&lt;maxColIx; colIx++) {
		/// Cell cell = row.GetCell(colIx);
		/// if(cell == null) {
		/// continue;
		/// }
		/// //... do something with cell
		/// }
		/// </pre>
		/// </summary>
		/// <returns>
		/// short representing the last logical cell in the row <b>PLUS ONE</b>,
		/// or -1 if the row does not contain any cells.
		/// </returns>
		short LastCellNum
		{
			get;
		}

		/// <summary>
		/// Gets the number of defined cells (NOT number of cells in the actual row!).
		/// That is to say if only columns 0,4,5 have values then there would be 3.
		/// </summary>
		/// <returns>int representing the number of defined cells in the row.</returns>
		int PhysicalNumberOfCells
		{
			get;
		}

		/// <summary>
		/// Get whether or not to display this row with 0 height
		/// </summary>
		/// <returns>zHeight height is zero or not.</returns>
		bool ZeroHeight
		{
			get;
			set;
		}

		/// <summary>
		/// Get the row's height measured in twips (1/20th of a point). 
		/// If the height is not set, the default worksheet value is returned,
		/// <see cref="P:NPOI.SS.UserModel.ISheet.DefaultRowHeightInPoints" />
		/// </summary>
		/// <returns>row height measured in twips (1/20th of a point)</returns>
		short Height
		{
			get;
			set;
		}

		/// <summary>
		/// Returns row height measured in point size. 
		/// If the height is not set, the default worksheet value is returned,
		/// <see cref="P:NPOI.SS.UserModel.ISheet.DefaultRowHeightInPoints" />
		/// </summary>
		/// <returns>row height measured in point size
		/// <see cref="P:NPOI.SS.UserModel.ISheet.DefaultRowHeightInPoints" />
		/// </returns>
		float HeightInPoints
		{
			get;
			set;
		}

		/// <summary>
		/// Is this row formatted? Most aren't, but some rows
		/// do have whole-row styles. For those that do, you
		/// can get the formatting from <see cref="P:NPOI.SS.UserModel.IRow.RowStyle" />
		/// </summary>
		bool IsFormatted
		{
			get;
		}

		/// <summary>
		/// Returns the Sheet this row belongs to
		/// </summary>
		/// <returns>the Sheet that owns this row</returns>
		ISheet Sheet
		{
			get;
		}

		/// <summary>
		/// Returns the whole-row cell styles. Most rows won't
		/// have one of these, so will return null. Call IsFormmated to check first
		/// </summary>
		/// <value>The row style.</value>
		ICellStyle RowStyle
		{
			get;
			set;
		}

		/// <summary>
		/// Get cells in the row
		/// </summary>
		List<ICell> Cells
		{
			get;
		}

		/// <summary>
		/// Use this to create new cells within the row and return it.
		///
		/// The cell that is returned is a <see cref="T:NPOI.SS.UserModel.ICell" />/<see cref="F:NPOI.SS.UserModel.CellType.Blank" />.
		/// The type can be changed either through calling <c>SetCellValue</c> or <c>SetCellType</c>.
		/// </summary>
		/// <param name="column">the column number this cell represents</param>
		/// <returns>Cell a high level representation of the created cell.</returns>
		/// <throws>
		/// ArgumentException if columnIndex &lt; 0 or greater than the maximum number of supported columns
		/// (255 for *.xls, 1048576 for *.xlsx)
		/// </throws>
		ICell CreateCell(int column);

		/// <summary>
		/// Use this to create new cells within the row and return it.
		///
		/// The cell that is returned is a <see cref="T:NPOI.SS.UserModel.ICell" />/<see cref="F:NPOI.SS.UserModel.CellType.Blank" />. The type can be changed
		/// either through calling <code>SetCellValue</code> or <code>SetCellType</code>.
		/// </summary>
		/// <param name="column">the column number this cell represents</param>
		/// <param name="type"></param>
		/// <returns>Cell a high level representation of the created cell.</returns>
		/// <throws>ArgumentException if columnIndex &lt; 0 or greater than the maximum number of supported columns
		/// (255 for *.xls, 1048576 for *.xlsx)
		/// </throws>
		ICell CreateCell(int column, CellType type);

		/// <summary>
		/// Remove the Cell from this row.
		/// </summary>
		/// <param name="cell">the cell to remove</param>
		void RemoveCell(ICell cell);

		/// <summary>
		/// Get the cell representing a given column (logical cell) 0-based.  If you
		/// ask for a cell that is not defined....you get a null.
		/// </summary>
		/// <param name="cellnum">0 based column number</param>
		/// <returns>Cell representing that column or null if undefined.</returns>
		/// <see cref="M:NPOI.SS.UserModel.IRow.GetCell(System.Int32,NPOI.SS.UserModel.MissingCellPolicy)" />
		ICell GetCell(int cellnum);

		/// <summary>
		/// Returns the cell at the given (0 based) index, with the specified {@link NPOI.SS.usermodel.Row.MissingCellPolicy}
		/// </summary>
		/// <returns>the cell at the given (0 based) index</returns>
		/// <throws>ArgumentException if cellnum &lt; 0 or the specified MissingCellPolicy is invalid</throws>
		/// <see cref="F:NPOI.SS.UserModel.MissingCellPolicy.RETURN_NULL_AND_BLANK" />
		/// <see cref="F:NPOI.SS.UserModel.MissingCellPolicy.RETURN_BLANK_AS_NULL" />
		/// <see cref="F:NPOI.SS.UserModel.MissingCellPolicy.CREATE_NULL_AS_BLANK" />
		ICell GetCell(int cellnum, MissingCellPolicy policy);

		/// <summary>
		/// Cell iterator of the physically defined cells.  Note element 4 may
		/// actually be row cell depending on how many are defined!
		/// </summary>
		IEnumerator GetEnumerator();

		/// <summary>
		/// Moves the supplied cell to a new column, which
		/// must not already have a cell there!
		/// </summary>
		/// <param name="cell">The cell to move</param>
		/// <param name="newColumn">The new column number (0 based)</param>
		void MoveCell(ICell cell, int newColumn);

		/// <summary>
		/// Copy the current row to the target row
		/// </summary>
		/// <param name="targetIndex">row index of the target row</param>
		/// <returns>the new copied row object</returns>
		IRow CopyRowTo(int targetIndex);

		/// <summary>
		/// Copy the source cell to the target cell. If the target cell exists, the new copied cell will be inserted before the existing one
		/// </summary>
		/// <param name="sourceIndex">index of the source cell</param>
		/// <param name="targetIndex">index of the target cell</param>
		/// <returns>the new copied cell object</returns>
		ICell CopyCell(int sourceIndex, int targetIndex);
	}
}
