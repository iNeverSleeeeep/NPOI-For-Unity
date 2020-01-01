using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using System.Text;

namespace NPOI.XWPF.Model
{
	/// Decorator class for XWPFParagraph allowing to add comments 
	/// found in paragraph to its text
	///
	/// @author Yury Batrakov (batrakov at gmail.com)
	public class XWPFCommentsDecorator : XWPFParagraphDecorator
	{
		private StringBuilder commentText;

		public override string Text
		{
			get
			{
				return base.Text + commentText;
			}
		}

		public XWPFCommentsDecorator(XWPFParagraphDecorator nextDecorator)
			: this(nextDecorator.paragraph, nextDecorator)
		{
		}

		public XWPFCommentsDecorator(XWPFParagraph paragraph, XWPFParagraphDecorator nextDecorator)
			: base(paragraph, nextDecorator)
		{
			commentText = new StringBuilder();
			foreach (CT_MarkupRange commentRangeStart in paragraph.GetCTP().GetCommentRangeStartList())
			{
				XWPFComment commentByID;
				if ((commentByID = paragraph.Document.GetCommentByID(commentRangeStart.id)) != null)
				{
					commentText.Append("\tComment by " + commentByID.Author + ": " + commentByID.Text);
				}
			}
		}

		public string GetCommentText()
		{
			return commentText.ToString();
		}
	}
}
