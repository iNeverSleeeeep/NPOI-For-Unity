using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing")]
	public class CT_ShapeNonVisual
	{
		private CT_NonVisualDrawingProps cNvPrField;

		private CT_NonVisualDrawingShapeProps cNvSpPrField;

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

		public CT_NonVisualDrawingShapeProps cNvSpPr
		{
			get
			{
				return cNvSpPrField;
			}
			set
			{
				cNvSpPrField = value;
			}
		}

		public CT_NonVisualDrawingProps AddNewCNvPr()
		{
			cNvPrField = new CT_NonVisualDrawingProps();
			return cNvPrField;
		}

		public CT_NonVisualDrawingShapeProps AddNewCNvSpPr()
		{
			cNvSpPrField = new CT_NonVisualDrawingShapeProps();
			return cNvSpPrField;
		}

		public static CT_ShapeNonVisual Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ShapeNonVisual cT_ShapeNonVisual = new CT_ShapeNonVisual();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "cNvPr")
				{
					cT_ShapeNonVisual.cNvPr = CT_NonVisualDrawingProps.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cNvSpPr")
				{
					cT_ShapeNonVisual.cNvSpPr = CT_NonVisualDrawingShapeProps.Parse(childNode, namespaceManager);
				}
			}
			return cT_ShapeNonVisual;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<xdr:{0}", nodeName));
			sw.Write(">");
			if (cNvPr != null)
			{
				cNvPr.Write(sw, "cNvPr");
			}
			if (cNvSpPr != null)
			{
				cNvSpPr.Write(sw, "cNvSpPr");
			}
			sw.Write(string.Format("</xdr:{0}>", nodeName));
		}
	}
}
