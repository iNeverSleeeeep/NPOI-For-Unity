namespace NPOI.SS.Format
{
	internal class DateCellFormatType : CellFormatType
	{
		public override bool IsSpecial(char ch)
		{
			if (ch != '\'')
			{
				if (ch <= '\u007f')
				{
					return char.IsLetter(ch);
				}
				return false;
			}
			return true;
		}

		public override CellFormatter Formatter(string pattern)
		{
			return new CellDateFormatter(pattern);
		}
	}
}
