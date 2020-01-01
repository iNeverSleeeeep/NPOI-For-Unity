namespace NPOI.SS.Format
{
	internal class ElapsedCellFormatType : CellFormatType
	{
		public override bool IsSpecial(char ch)
		{
			return false;
		}

		public override CellFormatter Formatter(string pattern)
		{
			return new CellElapsedFormatter(pattern);
		}
	}
}
