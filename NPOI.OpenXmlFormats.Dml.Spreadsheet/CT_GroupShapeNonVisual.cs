using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing")]
	public class CT_GroupShapeNonVisual
	{
		private CT_NonVisualDrawingProps cNvPrField;

		private CT_NonVisualGroupDrawingShapeProps cNvGrpSpPrField;

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

		public CT_NonVisualGroupDrawingShapeProps cNvGrpSpPr
		{
			get
			{
				return cNvGrpSpPrField;
			}
			set
			{
				cNvGrpSpPrField = value;
			}
		}

		public static CT_GroupShapeNonVisual Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_GroupShapeNonVisual cT_GroupShapeNonVisual = new CT_GroupShapeNonVisual();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "cNvPr")
				{
					cT_GroupShapeNonVisual.cNvPr = CT_NonVisualDrawingProps.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cNvGrpSpPr")
				{
					cT_GroupShapeNonVisual.cNvGrpSpPr = CT_NonVisualGroupDrawingShapeProps.Parse(childNode, namespaceManager);
				}
			}
			return cT_GroupShapeNonVisual;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<xdr:{0}", nodeName));
			sw.Write(">");
			if (cNvPr != null)
			{
				cNvPr.Write(sw, "cNvPr");
			}
			if (cNvGrpSpPr != null)
			{
				cNvGrpSpPr.Write(sw, "cNvGrpSpPr");
			}
			sw.Write(string.Format("</xdr:{0}>", nodeName));
		}

		public CT_NonVisualGroupDrawingShapeProps AddNewCNvGrpSpPr()
		{
			cNvGrpSpPrField = new CT_NonVisualGroupDrawingShapeProps();
			return cNvGrpSpPrField;
		}

		public CT_NonVisualDrawingProps AddNewCNvPr()
		{
			cNvPrField = new CT_NonVisualDrawingProps();
			return cNvPrField;
		}
	}
}
