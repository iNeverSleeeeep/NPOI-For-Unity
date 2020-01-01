using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using System.Text;

namespace NPOI.XWPF.Model
{
	/// Decorator class for XWPFParagraph allowing to add hyperlinks 
	///  found in paragraph to its text.
	///
	/// Note - Adds the hyperlink at the end, not in the right place...
	///
	/// @deprecated Use {@link XWPFHyperlinkRun} instead
	public class XWPFHyperlinkDecorator : XWPFParagraphDecorator
	{
		private StringBuilder hyperlinkText;

		public override string Text
		{
			get
			{
				return base.Text + hyperlinkText;
			}
		}

		/// @param nextDecorator The next decorator to use
		/// @param outputHyperlinkUrls Should we output the links too, or just the link text?
		public XWPFHyperlinkDecorator(XWPFParagraphDecorator nextDecorator, bool outputHyperlinkUrls)
			: this(nextDecorator.paragraph, nextDecorator, outputHyperlinkUrls)
		{
		}

		/// @param prgrph The paragraph of text to work on
		/// @param outputHyperlinkUrls Should we output the links too, or just the link text?
		public XWPFHyperlinkDecorator(XWPFParagraph prgrph, XWPFParagraphDecorator nextDecorator, bool outputHyperlinkUrls)
			: base(prgrph, nextDecorator)
		{
			hyperlinkText = new StringBuilder();
			foreach (CT_Hyperlink1 hyperlink in paragraph.GetCTP().GetHyperlinkList())
			{
				foreach (CT_R r in hyperlink.GetRList())
				{
					foreach (CT_Text t in r.GetTList())
					{
						hyperlinkText.Append(t.Value);
					}
				}
				if (outputHyperlinkUrls && paragraph.Document.GetHyperlinkByID(hyperlink.id) != null)
				{
					hyperlinkText.Append(" <" + paragraph.Document.GetHyperlinkByID(hyperlink.id).URL + ">");
				}
			}
		}
	}
}
