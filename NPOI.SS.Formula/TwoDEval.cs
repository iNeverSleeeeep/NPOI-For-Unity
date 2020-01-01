using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula
{
	/// Common interface of {@link AreaEval} and {@link NPOI.ss.formula.Eval.AreaEvalBase}
	///
	/// @author Josh Micich
	public interface TwoDEval : ValueEval
	{
		int Width
		{
			get;
		}

		int Height
		{
			get;
		}

		/// @return <c>true</c> if the area has just a single row, this also includes
		/// the trivial case when the area has just a single cell.
		bool IsRow
		{
			get;
		}

		/// @return <c>true</c> if the area has just a single column, this also includes
		/// the trivial case when the area has just a single cell.
		bool IsColumn
		{
			get;
		}

		/// @param rowIndex relative row index (zero based)
		/// @param columnIndex relative column index (zero based)
		/// @return element at the specified row and column position
		ValueEval GetValue(int rowIndex, int columnIndex);

		/// @param rowIndex relative row index (zero based)
		/// @return a single row {@link TwoDEval}
		TwoDEval GetRow(int rowIndex);

		/// @param columnIndex relative column index (zero based)
		/// @return a single column {@link TwoDEval}
		TwoDEval GetColumn(int columnIndex);

		/// @return true if the  cell at row and col is a subtotal
		bool IsSubTotal(int rowIndex, int columnIndex);
	}
}
