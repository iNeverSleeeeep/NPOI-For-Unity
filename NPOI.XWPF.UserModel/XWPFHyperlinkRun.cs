using NPOI.OpenXmlFormats.Wordprocessing;

namespace NPOI.XWPF.UserModel
{
	/// A run of text with a Hyperlink applied to it.
	/// Any given Hyperlink may be made up of multiple of these.
	public class XWPFHyperlinkRun : XWPFRun
	{
		private CT_Hyperlink1 hyperlink;

		public XWPFHyperlinkRun(CT_Hyperlink1 hyperlink, CT_R Run, XWPFParagraph p)
			: base(Run, p)
		{
			this.hyperlink = hyperlink;
		}

		public CT_Hyperlink1 GetCTHyperlink()
		{
			return hyperlink;
		}

		public string GetAnchor()
		{
			return hyperlink.anchor;
		}

		/// Returns the ID of the hyperlink, if one is Set.
		public string GetHyperlinkId()
		{
			return hyperlink.id;
		}

		public void SetHyperlinkId(string id)
		{
			hyperlink.id = id;
		}

		/// If this Hyperlink is an external reference hyperlink,
		///  return the object for it.
		public XWPFHyperlink GetHyperlink(XWPFDocument document)
		{
			string hyperlinkId = GetHyperlinkId();
			if (hyperlinkId == null)
			{
				return null;
			}
			return document.GetHyperlinkByID(hyperlinkId);
		}
	}
}
