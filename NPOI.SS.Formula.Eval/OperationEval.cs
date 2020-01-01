namespace NPOI.SS.Formula.Eval
{
	/// @author Amol S. Deshmukh &lt; amolweb at ya hoo dot com &gt;
	public interface OperationEval : Eval
	{
		int NumberOfOperands
		{
			get;
		}

		Eval Evaluate(Eval[] evals, int srcCellRow, short srcCellCol);
	}
}
