using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot("footnotes", Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = false)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_Footnotes
	{
		private List<CT_FtnEdn> footnoteField;

		[XmlElement("footnote", Order = 0)]
		public List<CT_FtnEdn> footnote
		{
			get
			{
				return footnoteField;
			}
			set
			{
				footnoteField = value;
			}
		}

		public static CT_Footnotes Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Footnotes cT_Footnotes = new CT_Footnotes();
			cT_Footnotes.footnote = new List<CT_FtnEdn>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "footnote")
				{
					cT_Footnotes.footnote.Add(CT_FtnEdn.Parse(childNode, namespaceManager));
				}
			}
			return cT_Footnotes;
		}

		internal void Write(StreamWriter sw)
		{
			sw.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
			sw.Write("<w:footnotes xmlns:ve=\"http://schemas.openxmlformats.org/markup-compatibility/2006\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" ");
			sw.Write("xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\" xmlns:m=\"http://schemas.openxmlformats.org/officeDocument/2006/math\" ");
			sw.Write("xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:wp=\"http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing\" ");
			sw.Write("xmlns:w10=\"urn:schemas-microsoft-com:office:word\" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\" ");
			sw.Write("xmlns:wne=\"http://schemas.microsoft.com/office/word/2006/wordml\">");
			if (footnote != null)
			{
				foreach (CT_FtnEdn item in footnote)
				{
					item.Write(sw, "footnote");
				}
			}
			sw.Write("</w:footnotes>");
		}

		public CT_FtnEdn AddNewFootnote()
		{
			CT_FtnEdn cT_FtnEdn = new CT_FtnEdn();
			if (footnoteField == null)
			{
				footnoteField = new List<CT_FtnEdn>();
			}
			footnoteField.Add(cT_FtnEdn);
			return cT_FtnEdn;
		}
	}
}
