using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_PrintSettings
	{
		private CT_HeaderFooter headerFooterField;

		private CT_PageMargins pageMarginsField;

		private CT_PageSetup pageSetupField;

		private CT_RelId legacyDrawingHFField;

		[XmlElement(Order = 0)]
		public CT_HeaderFooter headerFooter
		{
			get
			{
				return headerFooterField;
			}
			set
			{
				headerFooterField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_PageMargins pageMargins
		{
			get
			{
				return pageMarginsField;
			}
			set
			{
				pageMarginsField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_PageSetup pageSetup
		{
			get
			{
				return pageSetupField;
			}
			set
			{
				pageSetupField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_RelId legacyDrawingHF
		{
			get
			{
				return legacyDrawingHFField;
			}
			set
			{
				legacyDrawingHFField = value;
			}
		}

		public static CT_PrintSettings Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PrintSettings cT_PrintSettings = new CT_PrintSettings();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "headerFooter")
				{
					cT_PrintSettings.headerFooter = CT_HeaderFooter.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pageMargins")
				{
					cT_PrintSettings.pageMargins = CT_PageMargins.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pageSetup")
				{
					cT_PrintSettings.pageSetup = CT_PageSetup.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "legacyDrawingHF")
				{
					cT_PrintSettings.legacyDrawingHF = CT_RelId.Parse(childNode, namespaceManager);
				}
			}
			return cT_PrintSettings;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (headerFooter != null)
			{
				headerFooter.Write(sw, "headerFooter");
			}
			if (pageMargins != null)
			{
				pageMargins.Write(sw, "pageMargins");
			}
			if (pageSetup != null)
			{
				pageSetup.Write(sw, "pageSetup");
			}
			if (legacyDrawingHF != null)
			{
				legacyDrawingHF.Write(sw, "legacyDrawingHF");
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}

		public CT_HeaderFooter AddNewHeaderFooter()
		{
			headerFooterField = new CT_HeaderFooter();
			return headerFooterField;
		}

		public CT_PageSetup AddNewPageSetup()
		{
			pageSetupField = new CT_PageSetup();
			return pageSetupField;
		}

		public CT_PageMargins AddNewPageMargins()
		{
			pageMarginsField = new CT_PageMargins();
			return pageMarginsField;
		}
	}
}
