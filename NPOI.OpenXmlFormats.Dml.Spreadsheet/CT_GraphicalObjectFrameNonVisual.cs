using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing")]
	public class CT_GraphicalObjectFrameNonVisual
	{
		private CT_NonVisualDrawingProps cNvPrField;

		private CT_NonVisualGraphicFrameProperties cNvGraphicFramePrField;

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

		public CT_NonVisualGraphicFrameProperties cNvGraphicFramePr
		{
			get
			{
				return cNvGraphicFramePrField;
			}
			set
			{
				cNvGraphicFramePrField = value;
			}
		}

		public static CT_GraphicalObjectFrameNonVisual Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_GraphicalObjectFrameNonVisual cT_GraphicalObjectFrameNonVisual = new CT_GraphicalObjectFrameNonVisual();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "cNvPr")
				{
					cT_GraphicalObjectFrameNonVisual.cNvPr = CT_NonVisualDrawingProps.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cNvGraphicFramePr")
				{
					cT_GraphicalObjectFrameNonVisual.cNvGraphicFramePr = CT_NonVisualGraphicFrameProperties.Parse(childNode, namespaceManager);
				}
			}
			return cT_GraphicalObjectFrameNonVisual;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<xdr:{0}", nodeName));
			sw.Write(">");
			if (cNvPr != null)
			{
				cNvPr.Write(sw, "cNvPr");
			}
			if (cNvGraphicFramePr != null)
			{
				cNvGraphicFramePr.Write(sw, "cNvGraphicFramePr");
			}
			sw.Write(string.Format("</xdr:{0}>", nodeName));
		}

		public CT_NonVisualDrawingProps AddNewCNvPr()
		{
			cNvPrField = new CT_NonVisualDrawingProps();
			return cNvPrField;
		}

		public CT_NonVisualGraphicFrameProperties AddNewCNvGraphicFramePr()
		{
			cNvGraphicFramePrField = new CT_NonVisualGraphicFrameProperties();
			return cNvGraphicFramePrField;
		}
	}
}
