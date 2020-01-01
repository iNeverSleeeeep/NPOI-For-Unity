using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.Model;
using NPOI.XWPF.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace NPOI.XWPF.Extractor
{
	/// Helper class to extract text from an OOXML Word file
	public class XWPFWordExtractor : POIXMLTextExtractor
	{
		public static XWPFRelation[] SUPPORTED_TYPES = new XWPFRelation[4]
		{
			XWPFRelation.DOCUMENT,
			XWPFRelation.TEMPLATE,
			XWPFRelation.MACRO_DOCUMENT,
			XWPFRelation.MACRO_TEMPLATE_DOCUMENT
		};

		private new XWPFDocument document;

		private bool fetchHyperlinks;

		public override string Text
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				XWPFHeaderFooterPolicy headerFooterPolicy = document.GetHeaderFooterPolicy();
				extractHeaders(stringBuilder, headerFooterPolicy);
				IEnumerator<XWPFParagraph> paragraphsEnumerator = document.GetParagraphsEnumerator();
				while (paragraphsEnumerator.MoveNext())
				{
					XWPFParagraph current = paragraphsEnumerator.Current;
					try
					{
						CT_SectPr cT_SectPr = null;
						if (current.GetCTP().pPr != null)
						{
							cT_SectPr = current.GetCTP().pPr.sectPr;
						}
						XWPFHeaderFooterPolicy hfPolicy = null;
						if (cT_SectPr != null)
						{
							hfPolicy = new XWPFHeaderFooterPolicy(document, cT_SectPr);
							extractHeaders(stringBuilder, hfPolicy);
						}
						foreach (XWPFRun run in current.Runs)
						{
							stringBuilder.Append(run.ToString());
							if (run is XWPFHyperlinkRun && fetchHyperlinks)
							{
								XWPFHyperlink hyperlink = ((XWPFHyperlinkRun)run).GetHyperlink(document);
								if (hyperlink != null)
								{
									stringBuilder.Append(" <" + hyperlink.URL + ">");
								}
							}
						}
						XWPFCommentsDecorator xWPFCommentsDecorator = new XWPFCommentsDecorator(current, null);
						stringBuilder.Append(xWPFCommentsDecorator.GetCommentText()).Append('\n');
						string footnoteText = current.FootnoteText;
						if (footnoteText != null && footnoteText.Length > 0)
						{
							stringBuilder.Append(footnoteText + "\n");
						}
						if (cT_SectPr != null)
						{
							extractFooters(stringBuilder, hfPolicy);
						}
					}
					catch (IOException ex)
					{
						throw new POIXMLException(ex);
					}
					catch (XmlException ex2)
					{
						throw new POIXMLException(ex2);
					}
				}
				IEnumerator<XWPFTable> tablesEnumerator = document.GetTablesEnumerator();
				while (tablesEnumerator.MoveNext())
				{
					stringBuilder.Append(tablesEnumerator.Current.Text).Append('\n');
				}
				extractFooters(stringBuilder, headerFooterPolicy);
				return stringBuilder.ToString();
			}
		}

		public XWPFWordExtractor(OPCPackage Container)
			: this(new XWPFDocument(Container))
		{
		}

		public XWPFWordExtractor(XWPFDocument document)
			: base(document)
		{
			this.document = document;
		}

		/// Should we also fetch the hyperlinks, when fetching 
		///  the text content? Default is to only output the
		///  hyperlink label, and not the contents
		public void SetFetchHyperlinks(bool fetch)
		{
			fetchHyperlinks = fetch;
		}

		private void extractFooters(StringBuilder text, XWPFHeaderFooterPolicy hfPolicy)
		{
			if (hfPolicy.GetFirstPageFooter() != null)
			{
				text.Append(hfPolicy.GetFirstPageFooter().Text);
			}
			if (hfPolicy.GetEvenPageFooter() != null)
			{
				text.Append(hfPolicy.GetEvenPageFooter().Text);
			}
			if (hfPolicy.GetDefaultFooter() != null)
			{
				text.Append(hfPolicy.GetDefaultFooter().Text);
			}
		}

		private void extractHeaders(StringBuilder text, XWPFHeaderFooterPolicy hfPolicy)
		{
			if (hfPolicy.GetFirstPageHeader() != null)
			{
				text.Append(hfPolicy.GetFirstPageHeader().Text);
			}
			if (hfPolicy.GetEvenPageHeader() != null)
			{
				text.Append(hfPolicy.GetEvenPageHeader().Text);
			}
			if (hfPolicy.GetDefaultHeader() != null)
			{
				text.Append(hfPolicy.GetDefaultHeader().Text);
			}
		}
	}
}
