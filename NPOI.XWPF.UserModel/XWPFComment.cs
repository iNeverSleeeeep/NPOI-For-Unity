using NPOI.OpenXmlFormats.Wordprocessing;
using System.Text;

namespace NPOI.XWPF.UserModel
{
	/// * Sketch of XWPF comment class
	/// * 
	///                 * @author Yury Batrakov (batrakov at gmail.com)
	/// * 
	public class XWPFComment
	{
		protected string id;

		protected string author;

		protected StringBuilder text;

		public string Id
		{
			get
			{
				return id;
			}
		}

		public string Author
		{
			get
			{
				return author;
			}
		}

		public string Text
		{
			get
			{
				return text.ToString();
			}
		}

		public XWPFComment(CT_Comment comment, XWPFDocument document)
		{
			text = new StringBuilder();
			id = comment.id.ToString();
			author = comment.author;
			foreach (CT_P p in comment.GetPList())
			{
				XWPFParagraph xWPFParagraph = new XWPFParagraph(p, document);
				text.Append(xWPFParagraph.Text);
			}
		}
	}
}
