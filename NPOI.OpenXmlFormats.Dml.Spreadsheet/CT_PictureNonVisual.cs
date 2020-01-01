using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing")]
	public class CT_PictureNonVisual
	{
		private CT_NonVisualDrawingProps cNvPrField = new CT_NonVisualDrawingProps();

		private CT_NonVisualPictureProperties cNvPicPrField = new CT_NonVisualPictureProperties();

		[XmlElement]
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

		[XmlElement]
		public CT_NonVisualPictureProperties cNvPicPr
		{
			get
			{
				return cNvPicPrField;
			}
			set
			{
				cNvPicPrField = value;
			}
		}

		public static CT_PictureNonVisual Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PictureNonVisual cT_PictureNonVisual = new CT_PictureNonVisual();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "cNvPr")
				{
					cT_PictureNonVisual.cNvPr = CT_NonVisualDrawingProps.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cNvPicPr")
				{
					cT_PictureNonVisual.cNvPicPr = CT_NonVisualPictureProperties.Parse(childNode, namespaceManager);
				}
			}
			return cT_PictureNonVisual;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<xdr:{0}", nodeName));
			sw.Write(">");
			if (cNvPr != null)
			{
				cNvPr.Write(sw, "cNvPr");
			}
			if (cNvPicPr != null)
			{
				cNvPicPr.Write(sw, "cNvPicPr");
			}
			sw.Write(string.Format("</xdr:{0}>", nodeName));
		}

		public CT_NonVisualDrawingProps AddNewCNvPr()
		{
			cNvPrField = new CT_NonVisualDrawingProps();
			return cNvPrField;
		}

		public CT_NonVisualPictureProperties AddNewCNvPicPr()
		{
			cNvPicPrField = new CT_NonVisualPictureProperties();
			return cNvPicPrField;
		}
	}
}
