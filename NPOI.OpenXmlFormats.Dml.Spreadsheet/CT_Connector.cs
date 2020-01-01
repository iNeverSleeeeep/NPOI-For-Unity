using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing")]
	public class CT_Connector
	{
		private string macroField;

		private bool fPublishedField;

		private CT_ShapeProperties spPrField;

		private CT_ShapeStyle styleField;

		private CT_ConnectorNonVisual nvCxnSpPrField;

		public CT_ConnectorNonVisual nvCxnSpPr
		{
			get
			{
				return nvCxnSpPrField;
			}
			set
			{
				nvCxnSpPrField = value;
			}
		}

		public CT_ShapeProperties spPr
		{
			get
			{
				return spPrField;
			}
			set
			{
				spPrField = value;
			}
		}

		public CT_ShapeStyle style
		{
			get
			{
				return styleField;
			}
			set
			{
				styleField = value;
			}
		}

		[XmlAttribute]
		public string macro
		{
			get
			{
				return macroField;
			}
			set
			{
				macroField = value;
			}
		}

		[XmlAttribute]
		public bool fPublished
		{
			get
			{
				return fPublishedField;
			}
			set
			{
				fPublishedField = value;
			}
		}

		public static CT_Connector Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Connector cT_Connector = new CT_Connector();
			cT_Connector.macro = XmlHelper.ReadString(node.Attributes["macro"]);
			cT_Connector.fPublished = XmlHelper.ReadBool(node.Attributes["fPublished"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "nvCxnSpPr")
				{
					cT_Connector.nvCxnSpPr = CT_ConnectorNonVisual.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_Connector.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "style")
				{
					cT_Connector.style = CT_ShapeStyle.Parse(childNode, namespaceManager);
				}
			}
			return cT_Connector;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<xdr:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "macro", macro, true);
			XmlHelper.WriteAttribute(sw, "fPublished", fPublished, false);
			sw.Write(">");
			if (nvCxnSpPr != null)
			{
				nvCxnSpPr.Write(sw, "nvCxnSpPr");
			}
			if (spPr != null)
			{
				spPr.Write(sw, "spPr");
			}
			if (style != null)
			{
				style.Write(sw, "style");
			}
			sw.Write(string.Format("</xdr:{0}>", nodeName));
		}

		public void Set(CT_Connector obj)
		{
			macroField = obj.macro;
			fPublishedField = obj.fPublished;
			spPrField = obj.spPr;
			styleField = obj.style;
			nvCxnSpPrField = obj.nvCxnSpPr;
		}

		public CT_ConnectorNonVisual AddNewNvCxnSpPr()
		{
			nvCxnSpPr = new CT_ConnectorNonVisual();
			return nvCxnSpPr;
		}

		public CT_ShapeProperties AddNewSpPr()
		{
			spPrField = new CT_ShapeProperties();
			return spPrField;
		}

		public CT_ShapeStyle AddNewStyle()
		{
			styleField = new CT_ShapeStyle();
			return styleField;
		}
	}
}
