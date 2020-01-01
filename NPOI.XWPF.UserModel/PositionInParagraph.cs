namespace NPOI.XWPF.UserModel
{
	/// * postion of a character in a paragrapho
	///                 * 1st RunPositon
	///                 * 2nd TextPosition
	///                 * 3rd CharacterPosition 
	///                 * 
	///                 *
	public class PositionInParagraph
	{
		private int posRun;

		private int posText;

		private int posChar;

		public int Run
		{
			get
			{
				return posRun;
			}
			set
			{
				posRun = value;
			}
		}

		public int Text
		{
			get
			{
				return posText;
			}
			set
			{
				posText = value;
			}
		}

		public int Char
		{
			get
			{
				return posChar;
			}
			set
			{
				posChar = value;
			}
		}

		public PositionInParagraph()
		{
		}

		public PositionInParagraph(int posRun, int posText, int posChar)
		{
			this.posRun = posRun;
			this.posChar = posChar;
			this.posText = posText;
		}
	}
}
