using System;
using System.Collections.Generic;
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
	public class CT_SerAx
	{
		private CT_UnsignedInt axIdField;

		private CT_Scaling scalingField;

		private CT_Boolean deleteField;

		private CT_AxPos axPosField;

		private CT_ChartLines majorGridlinesField;

		private CT_ChartLines minorGridlinesField;

		private CT_Title titleField;

		private CT_NumFmt numFmtField;

		private CT_TickMark majorTickMarkField;

		private CT_TickMark minorTickMarkField;

		private CT_TickLblPos tickLblPosField;

		private CT_ShapeProperties spPrField;

		private CT_TextBody txPrField;

		private CT_UnsignedInt crossAxField;

		private CT_Skip tickLblSkipField;

		private CT_Skip tickMarkSkipField;

		private List<CT_Extension> extLstField;

		private CT_Crosses crossesField;

		private CT_Double crossesAtField;

		[XmlElement(Order = 0)]
		public CT_UnsignedInt axId
		{
			get
			{
				return axIdField;
			}
			set
			{
				axIdField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Scaling scaling
		{
			get
			{
				return scalingField;
			}
			set
			{
				scalingField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_Boolean delete
		{
			get
			{
				return deleteField;
			}
			set
			{
				deleteField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_AxPos axPos
		{
			get
			{
				return axPosField;
			}
			set
			{
				axPosField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_ChartLines majorGridlines
		{
			get
			{
				return majorGridlinesField;
			}
			set
			{
				majorGridlinesField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_ChartLines minorGridlines
		{
			get
			{
				return minorGridlinesField;
			}
			set
			{
				minorGridlinesField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_Title title
		{
			get
			{
				return titleField;
			}
			set
			{
				titleField = value;
			}
		}

		[XmlElement(Order = 7)]
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

		[XmlElement(Order = 8)]
		public CT_TickMark majorTickMark
		{
			get
			{
				return majorTickMarkField;
			}
			set
			{
				majorTickMarkField = value;
			}
		}

		[XmlElement(Order = 9)]
		public CT_TickMark minorTickMark
		{
			get
			{
				return minorTickMarkField;
			}
			set
			{
				minorTickMarkField = value;
			}
		}

		[XmlElement(Order = 10)]
		public CT_TickLblPos tickLblPos
		{
			get
			{
				return tickLblPosField;
			}
			set
			{
				tickLblPosField = value;
			}
		}

		[XmlElement(Order = 11)]
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

		[XmlElement(Order = 12)]
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

		[XmlElement(Order = 13)]
		public CT_UnsignedInt crossAx
		{
			get
			{
				return crossAxField;
			}
			set
			{
				crossAxField = value;
			}
		}

		[XmlElement(Order = 14)]
		public CT_Crosses crosses
		{
			get
			{
				return crossesField;
			}
			set
			{
				crossesField = value;
			}
		}

		[XmlElement(Order = 15)]
		public CT_Double crossesAt
		{
			get
			{
				return crossesAtField;
			}
			set
			{
				crossesAtField = value;
			}
		}

		[XmlElement(Order = 16)]
		public CT_Skip tickLblSkip
		{
			get
			{
				return tickLblSkipField;
			}
			set
			{
				tickLblSkipField = value;
			}
		}

		[XmlElement(Order = 17)]
		public CT_Skip tickMarkSkip
		{
			get
			{
				return tickMarkSkipField;
			}
			set
			{
				tickMarkSkipField = value;
			}
		}

		[XmlElement(Order = 18)]
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

		public static CT_SerAx Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SerAx cT_SerAx = new CT_SerAx();
			cT_SerAx.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "axId")
				{
					cT_SerAx.axId = CT_UnsignedInt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "scaling")
				{
					cT_SerAx.scaling = CT_Scaling.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "delete")
				{
					cT_SerAx.delete = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "axPos")
				{
					cT_SerAx.axPos = CT_AxPos.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "majorGridlines")
				{
					cT_SerAx.majorGridlines = CT_ChartLines.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "minorGridlines")
				{
					cT_SerAx.minorGridlines = CT_ChartLines.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "title")
				{
					cT_SerAx.title = CT_Title.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numFmt")
				{
					cT_SerAx.numFmt = CT_NumFmt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "majorTickMark")
				{
					cT_SerAx.majorTickMark = CT_TickMark.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "minorTickMark")
				{
					cT_SerAx.minorTickMark = CT_TickMark.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tickLblPos")
				{
					cT_SerAx.tickLblPos = CT_TickLblPos.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_SerAx.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "txPr")
				{
					cT_SerAx.txPr = CT_TextBody.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "crossAx")
				{
					cT_SerAx.crossAx = CT_UnsignedInt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "crosses")
				{
					cT_SerAx.crosses = CT_Crosses.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "crossesAt")
				{
					cT_SerAx.crossesAt = CT_Double.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tickLblSkip")
				{
					cT_SerAx.tickLblSkip = CT_Skip.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tickMarkSkip")
				{
					cT_SerAx.tickMarkSkip = CT_Skip.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_SerAx.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_SerAx;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (axId != null)
			{
				axId.Write(sw, "axId");
			}
			if (scaling != null)
			{
				scaling.Write(sw, "scaling");
			}
			if (delete != null)
			{
				delete.Write(sw, "delete");
			}
			if (axPos != null)
			{
				axPos.Write(sw, "axPos");
			}
			if (majorGridlines != null)
			{
				majorGridlines.Write(sw, "majorGridlines");
			}
			if (minorGridlines != null)
			{
				minorGridlines.Write(sw, "minorGridlines");
			}
			if (title != null)
			{
				title.Write(sw, "title");
			}
			if (numFmt != null)
			{
				numFmt.Write(sw, "numFmt");
			}
			if (majorTickMark != null)
			{
				majorTickMark.Write(sw, "majorTickMark");
			}
			if (minorTickMark != null)
			{
				minorTickMark.Write(sw, "minorTickMark");
			}
			if (tickLblPos != null)
			{
				tickLblPos.Write(sw, "tickLblPos");
			}
			if (spPr != null)
			{
				spPr.Write(sw, "spPr");
			}
			if (txPr != null)
			{
				txPr.Write(sw, "txPr");
			}
			if (crossAx != null)
			{
				crossAx.Write(sw, "crossAx");
			}
			if (crosses != null)
			{
				crosses.Write(sw, "crosses");
			}
			if (crossesAt != null)
			{
				crossesAt.Write(sw, "crossesAt");
			}
			if (tickLblSkip != null)
			{
				tickLblSkip.Write(sw, "tickLblSkip");
			}
			if (tickMarkSkip != null)
			{
				tickMarkSkip.Write(sw, "tickMarkSkip");
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
