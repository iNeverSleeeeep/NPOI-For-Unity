using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot("document", Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = false)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_Document : CT_DocumentBase
	{
		private CT_Body bodyField;

		[XmlElement(Order = 0)]
		public CT_Body body
		{
			get
			{
				return bodyField;
			}
			set
			{
				bodyField = value;
			}
		}

		public static CT_Document Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Document cT_Document = new CT_Document();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "body")
				{
					cT_Document.body = CT_Body.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "background")
				{
					cT_Document.background = CT_Background.Parse(childNode, namespaceManager);
				}
			}
			return cT_Document;
		}

		internal void Write(StreamWriter sw)
		{
			sw.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
			sw.Write("<w:document xmlns:ve=\"http://schemas.openxmlformats.org/markup-compatibility/2006\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" ");
			sw.Write("xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\" xmlns:m=\"http://schemas.openxmlformats.org/officeDocument/2006/math\" ");
			sw.Write("xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:wp=\"http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing\" ");
			sw.Write("xmlns:w10=\"urn:schemas-microsoft-com:office:word\" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\" ");
			sw.Write("xmlns:wne=\"http://schemas.microsoft.com/office/word/2006/wordml\" xmlns:a=\"http://schemas.openxmlformats.org/drawingml/2006/main\">");
			if (body != null)
			{
				body.Write(sw, "body");
			}
			if (base.background != null)
			{
				base.background.Write(sw, "background");
			}
			sw.Write("</w:document>");
		}

		public void AddNewBody()
		{
			bodyField = new CT_Body();
		}
	}
}
