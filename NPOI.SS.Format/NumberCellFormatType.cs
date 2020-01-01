namespace NPOI.SS.Format
{
	internal class NumberCellFormatType : CellFormatType
	{
		public override CellFormatter Formatter(string pattern)
		{
			return new CellNumberFormatter(pattern);
		}

		public override bool IsSpecial(char ch)
		{
			return false;
		}
	}
}
