using NPOI.SS.Formula.Eval;

namespace NPOI.SS.Formula.Functions
{
	/// Represents a single row or column within an <c>AreaEval</c>.
	public interface ValueVector
	{
		int Size
		{
			get;
		}

		ValueEval GetItem(int index);
	}
}
