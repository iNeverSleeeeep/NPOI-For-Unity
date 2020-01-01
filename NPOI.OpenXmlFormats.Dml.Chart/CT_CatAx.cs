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
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	public class CT_CatAx
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

		private object itemField;

		private CT_Boolean autoField;

		private CT_LblAlgn lblAlgnField;

		private CT_LblOffset lblOffsetField;

		private CT_Skip tickLblSkipField;

		private CT_Skip tickMarkSkipField;

		private CT_Boolean noMultiLvlLblField;

		private List<CT_Extension> extLstField;

		private CT_Double crossesAtField;

		private CT_Crosses crossesField;

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
		public CT_Boolean auto
		{
			get
			{
				return autoField;
			}
			set
			{
				autoField = value;
			}
		}

		[XmlElement(Order = 16)]
		public CT_LblAlgn lblAlgn
		{
			get
			{
				return lblAlgnField;
			}
			set
			{
				lblAlgnField = value;
			}
		}

		[XmlElement(Order = 17)]
		public CT_LblOffset lblOffset
		{
			get
			{
				return lblOffsetField;
			}
			set
			{
				lblOffsetField = value;
			}
		}

		[XmlElement(Order = 18)]
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

		[XmlElement(Order = 19)]
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

		[XmlElement(Order = 20)]
		public CT_Boolean noMultiLvlLbl
		{
			get
			{
				return noMultiLvlLblField;
			}
			set
			{
				noMultiLvlLblField = value;
			}
		}

		[XmlElement(Order = 21)]
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

		public static CT_CatAx Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CatAx cT_CatAx = new CT_CatAx();
			cT_CatAx.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "axId")
				{
					cT_CatAx.axId = CT_UnsignedInt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "scaling")
				{
					cT_CatAx.scaling = CT_Scaling.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "delete")
				{
					cT_CatAx.delete = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "axPos")
				{
					cT_CatAx.axPos = CT_AxPos.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "majorGridlines")
				{
					cT_CatAx.majorGridlines = CT_ChartLines.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "minorGridlines")
				{
					cT_CatAx.minorGridlines = CT_ChartLines.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "title")
				{
					cT_CatAx.title = CT_Title.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numFmt")
				{
					cT_CatAx.numFmt = CT_NumFmt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "majorTickMark")
				{
					cT_CatAx.majorTickMark = CT_TickMark.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "minorTickMark")
				{
					cT_CatAx.minorTickMark = CT_TickMark.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tickLblPos")
				{
					cT_CatAx.tickLblPos = CT_TickLblPos.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_CatAx.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "txPr")
				{
					cT_CatAx.txPr = CT_TextBody.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "crossAx")
				{
					cT_CatAx.crossAx = CT_UnsignedInt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "crosses")
				{
					cT_CatAx.crosses = CT_Crosses.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "crossesAt")
				{
					cT_CatAx.crossesAt = CT_Double.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "auto")
				{
					cT_CatAx.auto = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lblAlgn")
				{
					cT_CatAx.lblAlgn = CT_LblAlgn.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lblOffset")
				{
					cT_CatAx.lblOffset = CT_LblOffset.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tickLblSkip")
				{
					cT_CatAx.tickLblSkip = CT_Skip.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tickMarkSkip")
				{
					cT_CatAx.tickMarkSkip = CT_Skip.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "noMultiLvlLbl")
				{
					cT_CatAx.noMultiLvlLbl = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_CatAx.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_CatAx;
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
			if (crossesAt != null)
			{
				crossesAt.Write(sw, "crossesAt");
			}
			if (crosses != null)
			{
				crosses.Write(sw, "crosses");
			}
			if (auto != null)
			{
				auto.Write(sw, "auto");
			}
			if (lblAlgn != null)
			{
				lblAlgn.Write(sw, "lblAlgn");
			}
			if (lblOffset != null)
			{
				lblOffset.Write(sw, "lblOffset");
			}
			if (tickLblSkip != null)
			{
				tickLblSkip.Write(sw, "tickLblSkip");
			}
			if (tickMarkSkip != null)
			{
				tickMarkSkip.Write(sw, "tickMarkSkip");
			}
			if (noMultiLvlLbl != null)
			{
				noMultiLvlLbl.Write(sw, "noMultiLvlLbl");
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

		public CT_NumFmt AddNewNumFmt()
		{
			numFmtField = new CT_NumFmt();
			return numFmtField;
		}

		public bool IsSetNumFmt()
		{
			return numFmtField != null;
		}

		public CT_UnsignedInt AddNewAxId()
		{
			axIdField = new CT_UnsignedInt();
			return axIdField;
		}

		public CT_AxPos AddNewAxPos()
		{
			axPosField = new CT_AxPos();
			return axPosField;
		}

		public CT_Scaling AddNewScaling()
		{
			scalingField = new CT_Scaling();
			return scalingField;
		}

		public CT_Crosses AddNewCrosses()
		{
			crossesField = new CT_Crosses();
			return crossesField;
		}

		public CT_UnsignedInt AddNewCrossAx()
		{
			crossAxField = new CT_UnsignedInt();
			return crossAxField;
		}

		public CT_TickLblPos AddNewTickLblPos()
		{
			tickLblPosField = new CT_TickLblPos();
			return tickLblPosField;
		}

		public CT_Boolean AddNewDelete()
		{
			deleteField = new CT_Boolean();
			return deleteField;
		}

		public CT_TickMark AddNewMajorTickMark()
		{
			majorTickMarkField = new CT_TickMark();
			return majorTickMarkField;
		}

		public CT_TickMark AddNewMinorTickMark()
		{
			minorTickMarkField = new CT_TickMark();
			return minorTickMarkField;
		}
	}
}
