namespace NPOI.SS.Formula
{
	public class TotallyImmutable : IStabilityClassifier
	{
		public override bool IsCellFinal(int sheetIndex, int rowIndex, int columnIndex)
		{
			return true;
		}
	}
}
