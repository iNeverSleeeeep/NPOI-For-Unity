using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot("endnotes", Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = false)]
	public class CT_Endnotes
	{
		private List<CT_FtnEdn> endnoteField = new List<CT_FtnEdn>();

		[XmlElement("endnote", Order = 0)]
		public List<CT_FtnEdn> endnote
		{
			get
			{
				return endnoteField;
			}
			set
			{
				endnoteField = value;
			}
		}

		public static CT_Endnotes Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Endnotes cT_Endnotes = new CT_Endnotes();
			cT_Endnotes.endnote = new List<CT_FtnEdn>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "endnote")
				{
					cT_Endnotes.endnote.Add(CT_FtnEdn.Parse(childNode, namespaceManager));
				}
			}
			return cT_Endnotes;
		}

		internal void Write(StreamWriter sw)
		{
			sw.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
			sw.Write("<w:endnotes xmlns:ve=\"http://schemas.openxmlformats.org/markup-compatibility/2006\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" ");
			sw.Write("xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\" xmlns:m=\"http://schemas.openxmlformats.org/officeDocument/2006/math\" ");
			sw.Write("xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:wp=\"http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing\" ");
			sw.Write("xmlns:w10=\"urn:schemas-microsoft-com:office:word\" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\" ");
			sw.Write("xmlns:wne=\"http://schemas.microsoft.com/office/word/2006/wordml\">");
			if (endnote != null)
			{
				foreach (CT_FtnEdn item in endnote)
				{
					item.Write(sw, "endnote");
				}
			}
			sw.Write("</w:endnotes>");
		}
	}
}
