using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing")]
	public class CT_GroupShape
	{
		private CT_GroupShapeProperties grpSpPrField;

		private CT_GroupShapeNonVisual nvGrpSpPrField;

		private CT_Connector connectorField;

		private CT_Picture pictureField;

		private CT_Shape shapeField;

		public CT_GroupShapeNonVisual nvGrpSpPr
		{
			get
			{
				return nvGrpSpPrField;
			}
			set
			{
				nvGrpSpPrField = value;
			}
		}

		public CT_GroupShapeProperties grpSpPr
		{
			get
			{
				return grpSpPrField;
			}
			set
			{
				grpSpPrField = value;
			}
		}

		public void Set(CT_GroupShape groupShape)
		{
			grpSpPrField = groupShape.grpSpPr;
			nvGrpSpPrField = groupShape.nvGrpSpPr;
		}

		public CT_GroupShapeProperties AddNewGrpSpPr()
		{
			grpSpPrField = new CT_GroupShapeProperties();
			return grpSpPrField;
		}

		public CT_GroupShapeNonVisual AddNewNvGrpSpPr()
		{
			nvGrpSpPrField = new CT_GroupShapeNonVisual();
			return nvGrpSpPrField;
		}

		public CT_Connector AddNewCxnSp()
		{
			connectorField = new CT_Connector();
			return connectorField;
		}

		public CT_Shape AddNewSp()
		{
			shapeField = new CT_Shape();
			return shapeField;
		}

		public CT_Picture AddNewPic()
		{
			pictureField = new CT_Picture();
			return pictureField;
		}

		public static CT_GroupShape Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_GroupShape cT_GroupShape = new CT_GroupShape();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "nvGrpSpPr")
				{
					cT_GroupShape.nvGrpSpPr = CT_GroupShapeNonVisual.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "grpSpPr")
				{
					cT_GroupShape.grpSpPr = CT_GroupShapeProperties.Parse(childNode, namespaceManager);
				}
			}
			return cT_GroupShape;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<xdr:{0}", nodeName));
			sw.Write(">");
			if (nvGrpSpPr != null)
			{
				nvGrpSpPr.Write(sw, "nvGrpSpPr");
			}
			if (grpSpPr != null)
			{
				grpSpPr.Write(sw, "grpSpPr");
			}
			sw.Write(string.Format("</xdr:{0}>", nodeName));
		}
	}
}
