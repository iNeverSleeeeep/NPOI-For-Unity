using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing")]
	public class CT_ConnectorNonVisual
	{
		private CT_NonVisualDrawingProps cNvPrField;

		private CT_NonVisualConnectorProperties cNvCxnSpPrField;

		public CT_NonVisualConnectorProperties cNvCxnSpPr
		{
			get
			{
				return cNvCxnSpPrField;
			}
			set
			{
				cNvCxnSpPrField = value;
			}
		}

		public CT_NonVisualDrawingProps cNvPr
		{
			get
			{
				return cNvPrField;
			}
			set
			{
				cNvPrField = value;
			}
		}

		public CT_NonVisualDrawingProps AddNewCNvPr()
		{
			cNvPr = new CT_NonVisualDrawingProps();
			return cNvPr;
		}

		public CT_NonVisualConnectorProperties AddNewCNvCxnSpPr()
		{
			cNvCxnSpPr = new CT_NonVisualConnectorProperties();
			return cNvCxnSpPr;
		}

		public static CT_ConnectorNonVisual Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ConnectorNonVisual cT_ConnectorNonVisual = new CT_ConnectorNonVisual();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "cNvCxnSpPr")
				{
					cT_ConnectorNonVisual.cNvCxnSpPr = CT_NonVisualConnectorProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cNvPr")
				{
					cT_ConnectorNonVisual.cNvPr = CT_NonVisualDrawingProps.Parse(childNode, namespaceManager);
				}
			}
			return cT_ConnectorNonVisual;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<xdr:{0}", nodeName));
			sw.Write(">");
			if (cNvCxnSpPr != null)
			{
				cNvCxnSpPr.Write(sw, "cNvCxnSpPr");
			}
			if (cNvPr != null)
			{
				cNvPr.Write(sw, "cNvPr");
			}
			sw.Write(string.Format("</xdr:{0}>", nodeName));
		}
	}
}
