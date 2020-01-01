namespace NPOI.SS.Formula.Eval
{
	/// @author Amol S Deshmukh &lt; amolweb at ya hoo dot com &gt;
	///
	/// RefEval is the base interface for Ref2D and Ref3DEval. Basically a RefEval
	/// impl should contain reference to the original ReferencePtg or Ref3DPtg as
	/// well as the "value" resulting from the evaluation of the cell
	/// reference. Thus if the HSSFCell has type CELL_TYPE_NUMERIC, the contained
	/// value object should be of type NumberEval; if cell type is CELL_TYPE_STRING,
	/// contained value object should be of type StringEval
	public interface RefEval : ValueEval
	{
		/// The (possibly Evaluated) ValueEval contained
		/// in this RefEval. eg. if cell A1 Contains "test"
		/// then in a formula referring to cell A1 
		/// the RefEval representing
		/// A1 will return as the InnerValueEval the
		/// object of concrete type StringEval
		ValueEval InnerValueEval
		{
			get;
		}

		/// returns the zero based column index.
		int Column
		{
			get;
		}

		/// returns the zero based row index.
		int Row
		{
			get;
		}

		/// Creates an {@link AreaEval} offset by a relative amount from this RefEval
		AreaEval Offset(int relFirstRowIx, int relLastRowIx, int relFirstColIx, int relLastColIx);
	}
}
