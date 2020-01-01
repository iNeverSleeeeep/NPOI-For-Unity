namespace NPOI.SS.Format
{
	internal class GeneralCellFormatType : CellFormatType
	{
		public override CellFormatter Formatter(string pattern)
		{
			return new CellGeneralFormatter();
		}

		public override bool IsSpecial(char ch)
		{
			return false;
		}
	}
}
