namespace NPOI.SS.Util.CellWalk
{
	/// @author Roman Kashitsyn
	public interface ICellWalkContext
	{
		/// Returns ordinal number of cell in range.  Numeration starts
		/// from top left cell and ends at bottom right cell. Here is a
		/// brief example (number in cell is it's ordinal number):
		///
		/// <table border="1">
		///   <tbody>
		///     <tr><td>1</td><td>2</td></tr>
		///     <tr><td>3</td><td>4</td></tr>
		///   </tbody>
		/// </table>
		///
		/// @return ordinal number of current cell
		long OrdinalNumber
		{
			get;
		}

		/// Returns number of current row.
		/// @return number of current row
		int RowNumber
		{
			get;
		}

		/// Returns number of current column.
		/// @return number of current column
		int ColumnNumber
		{
			get;
		}
	}
}
