using NPOI.XWPF.UserModel;

namespace NPOI.XWPF.Model
{
	/// Base decorator class for XWPFParagraph
	public abstract class XWPFParagraphDecorator
	{
		internal XWPFParagraph paragraph;

		internal XWPFParagraphDecorator nextDecorator;

		public virtual string Text
		{
			get
			{
				if (nextDecorator != null)
				{
					return nextDecorator.Text;
				}
				return paragraph.Text;
			}
		}

		public XWPFParagraphDecorator(XWPFParagraph paragraph)
			: this(paragraph, null)
		{
		}

		public XWPFParagraphDecorator(XWPFParagraph paragraph, XWPFParagraphDecorator nextDecorator)
		{
			this.paragraph = paragraph;
			this.nextDecorator = nextDecorator;
		}
	}
}
