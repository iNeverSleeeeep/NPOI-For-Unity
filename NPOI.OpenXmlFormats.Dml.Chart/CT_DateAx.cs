using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	public class CT_DateAx
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

		private CT_LblOffset lblOffsetField;

		private CT_TimeUnit baseTimeUnitField;

		private CT_AxisUnit majorUnitField;

		private CT_TimeUnit majorTimeUnitField;

		private CT_AxisUnit minorUnitField;

		private CT_TimeUnit minorTimeUnitField;

		private List<CT_Extension> extLstField;

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

		[XmlElement("crossesAt", typeof(CT_Double), Order = 14)]
		[XmlElement("crosses", typeof(CT_Crosses), Order = 14)]
		public object Item
		{
			get
			{
				return itemField;
			}
			set
			{
				itemField = value;
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

		[XmlElement(Order = 17)]
		public CT_TimeUnit baseTimeUnit
		{
			get
			{
				return baseTimeUnitField;
			}
			set
			{
				baseTimeUnitField = value;
			}
		}

		[XmlElement(Order = 18)]
		public CT_AxisUnit majorUnit
		{
			get
			{
				return majorUnitField;
			}
			set
			{
				majorUnitField = value;
			}
		}

		[XmlElement(Order = 19)]
		public CT_TimeUnit majorTimeUnit
		{
			get
			{
				return majorTimeUnitField;
			}
			set
			{
				majorTimeUnitField = value;
			}
		}

		[XmlElement(Order = 20)]
		public CT_AxisUnit minorUnit
		{
			get
			{
				return minorUnitField;
			}
			set
			{
				minorUnitField = value;
			}
		}

		[XmlElement(Order = 21)]
		public CT_TimeUnit minorTimeUnit
		{
			get
			{
				return minorTimeUnitField;
			}
			set
			{
				minorTimeUnitField = value;
			}
		}

		[XmlElement(Order = 22)]
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

		public static CT_DateAx Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_DateAx cT_DateAx = new CT_DateAx();
			cT_DateAx.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "axId")
				{
					cT_DateAx.axId = CT_UnsignedInt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "scaling")
				{
					cT_DateAx.scaling = CT_Scaling.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "delete")
				{
					cT_DateAx.delete = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "axPos")
				{
					cT_DateAx.axPos = CT_AxPos.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "majorGridlines")
				{
					cT_DateAx.majorGridlines = CT_ChartLines.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "minorGridlines")
				{
					cT_DateAx.minorGridlines = CT_ChartLines.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "title")
				{
					cT_DateAx.title = CT_Title.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numFmt")
				{
					cT_DateAx.numFmt = CT_NumFmt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "majorTickMark")
				{
					cT_DateAx.majorTickMark = CT_TickMark.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "minorTickMark")
				{
					cT_DateAx.minorTickMark = CT_TickMark.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tickLblPos")
				{
					cT_DateAx.tickLblPos = CT_TickLblPos.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_DateAx.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "txPr")
				{
					cT_DateAx.txPr = CT_TextBody.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "crossAx")
				{
					cT_DateAx.crossAx = CT_UnsignedInt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "Item")
				{
					cT_DateAx.Item = new object();
				}
				else if (childNode.LocalName == "auto")
				{
					cT_DateAx.auto = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lblOffset")
				{
					cT_DateAx.lblOffset = CT_LblOffset.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "baseTimeUnit")
				{
					cT_DateAx.baseTimeUnit = CT_TimeUnit.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "majorUnit")
				{
					cT_DateAx.majorUnit = CT_AxisUnit.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "majorTimeUnit")
				{
					cT_DateAx.majorTimeUnit = CT_TimeUnit.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "minorUnit")
				{
					cT_DateAx.minorUnit = CT_AxisUnit.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "minorTimeUnit")
				{
					cT_DateAx.minorTimeUnit = CT_TimeUnit.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_DateAx.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_DateAx;
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
			if (Item != null)
			{
				sw.Write("<Item/>");
			}
			if (auto != null)
			{
				auto.Write(sw, "auto");
			}
			if (lblOffset != null)
			{
				lblOffset.Write(sw, "lblOffset");
			}
			if (baseTimeUnit != null)
			{
				baseTimeUnit.Write(sw, "baseTimeUnit");
			}
			if (majorUnit != null)
			{
				majorUnit.Write(sw, "majorUnit");
			}
			if (majorTimeUnit != null)
			{
				majorTimeUnit.Write(sw, "majorTimeUnit");
			}
			if (minorUnit != null)
			{
				minorUnit.Write(sw, "minorUnit");
			}
			if (minorTimeUnit != null)
			{
				minorTimeUnit.Write(sw, "minorTimeUnit");
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
