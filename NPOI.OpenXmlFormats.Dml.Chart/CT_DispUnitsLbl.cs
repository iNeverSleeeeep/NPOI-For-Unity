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
	public class CT_DispUnitsLbl
	{
		private CT_Layout layoutField;

		private CT_Tx txField;

		private CT_ShapeProperties spPrField;

		private CT_TextBody txPrField;

		[XmlElement(Order = 0)]
		public CT_Layout layout
		{
			get
			{
				return layoutField;
			}
			set
			{
				layoutField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Tx tx
		{
			get
			{
				return txField;
			}
			set
			{
				txField = value;
			}
		}

		[XmlElement(Order = 2)]
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

		[XmlElement(Order = 3)]
		public CT_TextBody txPr
		{
			get
			{
				return txPrField;
			}
			set
			{
				txPrField = value;
			}
		}

		public static CT_DispUnitsLbl Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_DispUnitsLbl cT_DispUnitsLbl = new CT_DispUnitsLbl();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "layout")
				{
					cT_DispUnitsLbl.layout = CT_Layout.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tx")
				{
					cT_DispUnitsLbl.tx = CT_Tx.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_DispUnitsLbl.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "txPr")
				{
					cT_DispUnitsLbl.txPr = CT_TextBody.Parse(childNode, namespaceManager);
				}
			}
			return cT_DispUnitsLbl;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (layout != null)
			{
				layout.Write(sw, "layout");
			}
			if (tx != null)
			{
				tx.Write(sw, "tx");
			}
			if (spPr != null)
			{
				spPr.Write(sw, "spPr");
			}
			if (txPr != null)
			{
				txPr.Write(sw, "txPr");
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
