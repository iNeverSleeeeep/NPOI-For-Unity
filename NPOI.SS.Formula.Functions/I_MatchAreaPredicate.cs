namespace NPOI.SS.Formula.Functions
{
	public interface I_MatchAreaPredicate : IMatchPredicate
	{
		bool Matches(TwoDEval x, int rowIndex, int columnIndex);
	}
}
