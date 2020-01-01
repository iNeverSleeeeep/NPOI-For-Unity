using NPOI.OpenXmlFormats.Wordprocessing;

namespace NPOI.XWPF.Model
{
	/// Base class for XWPF paragraphs
	///
	/// @author Yury Batrakov (batrakov at gmail.com)
	public class XMLParagraph
	{
		protected CT_P paragraph;

		public XMLParagraph(CT_P paragraph)
		{
			this.paragraph = paragraph;
		}

		public CT_P GetCTP()
		{
			return paragraph;
		}
	}
}
