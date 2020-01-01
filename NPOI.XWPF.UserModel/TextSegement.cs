namespace NPOI.XWPF.UserModel
{
	/// saves the begin and end position  of a text in a Paragraph
	public class TextSegement
	{
		private PositionInParagraph beginPos;

		private PositionInParagraph endPos;

		public PositionInParagraph BeginPos
		{
			get
			{
				return beginPos;
			}
			set
			{
				beginPos = value;
			}
		}

		public PositionInParagraph EndPos
		{
			get
			{
				return endPos;
			}
		}

		/// <summary>
		/// The index of the start run
		/// </summary>
		public int BeginRun
		{
			get
			{
				return beginPos.Run;
			}
			set
			{
				beginPos.Run = value;
			}
		}

		/// <summary>
		/// The index of the start CT_Text
		/// </summary>
		public int BeginText
		{
			get
			{
				return beginPos.Text;
			}
			set
			{
				beginPos.Text = value;
			}
		}

		/// <summary>
		/// The index of the start text character
		/// </summary>
		public int BeginChar
		{
			get
			{
				return beginPos.Char;
			}
			set
			{
				beginPos.Char = value;
			}
		}

		public int EndRun
		{
			get
			{
				return endPos.Run;
			}
			set
			{
				endPos.Run = value;
			}
		}

		/// <summary>
		/// the index of the end CT_Text 
		/// </summary>
		public int EndText
		{
			get
			{
				return endPos.Text;
			}
			set
			{
				endPos.Text = value;
			}
		}

		/// <summary>
		/// the index of the end text character
		/// </summary>
		public int EndChar
		{
			get
			{
				return endPos.Char;
			}
			set
			{
				endPos.Char = value;
			}
		}

		public TextSegement()
		{
			beginPos = new PositionInParagraph();
			endPos = new PositionInParagraph();
		}

		public TextSegement(int beginRun, int endRun, int beginText, int endText, int beginChar, int endChar)
		{
			PositionInParagraph positionInParagraph = new PositionInParagraph(beginRun, beginText, beginChar);
			PositionInParagraph positionInParagraph2 = new PositionInParagraph(endRun, endText, endChar);
			beginPos = positionInParagraph;
			endPos = positionInParagraph2;
		}

		public TextSegement(PositionInParagraph beginPos, PositionInParagraph endPos)
		{
			this.beginPos = beginPos;
			this.endPos = endPos;
		}
	}
}
