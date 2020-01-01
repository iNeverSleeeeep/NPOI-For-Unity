using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	public class CT_TrendlineLbl
	{
		private CT_Layout layoutField;

		private CT_Tx txField;

		private CT_NumFmt numFmtField;

		private CT_ShapeProperties spPrField;

		private CT_TextBody txPrField;

		private List<CT_Extension> extLstField;

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
		public CT_NumFmt numFmt
		{
			get
			{
				return numFmtField;
			}
			set
			{
				numFmtField = value;
			}
		}

		[XmlElement(Order = 3)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 5)]
		public List<CT_Extension> extLst
		{
			get
			{
				return extLstField;
			}
			set
			{
				extLstField = value;
			}
		}

		public static CT_TrendlineLbl Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TrendlineLbl cT_TrendlineLbl = new CT_TrendlineLbl();
			cT_TrendlineLbl.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "layout")
				{
					cT_TrendlineLbl.layout = CT_Layout.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tx")
				{
					cT_TrendlineLbl.tx = CT_Tx.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numFmt")
				{
					cT_TrendlineLbl.numFmt = CT_NumFmt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_TrendlineLbl.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "txPr")
				{
					cT_TrendlineLbl.txPr = CT_TextBody.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_TrendlineLbl.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_TrendlineLbl;
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
			if (numFmt != null)
			{
				numFmt.Write(sw, "numFmt");
			}
			if (spPr != null)
			{
				spPr.Write(sw, "spPr");
			}
			if (txPr != null)
			{
				txPr.Write(sw, "txPr");
			}
			if (extLst != null)
			{
				foreach (CT_Extension item in extLst)
				{
					item.Write(sw, "extLst");
				}
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
