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
	public class CT_BarChart
	{
		private CT_BarDir barDirField;

		private CT_BarGrouping groupingField;

		private CT_Boolean varyColorsField;

		private List<CT_BarSer> serField;

		private CT_DLbls dLblsField;

		private CT_GapAmount gapWidthField;

		private CT_Overlap overlapField;

		private List<CT_ChartLines> serLinesField;

		private List<CT_UnsignedInt> axIdField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
		public CT_BarDir barDir
		{
			get
			{
				return barDirField;
			}
			set
			{
				barDirField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_BarGrouping grouping
		{
			get
			{
				return groupingField;
			}
			set
			{
				groupingField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_Boolean varyColors
		{
			get
			{
				return varyColorsField;
			}
			set
			{
				varyColorsField = value;
			}
		}

		[XmlElement("ser", Order = 3)]
		public List<CT_BarSer> ser
		{
			get
			{
				return serField;
			}
			set
			{
				serField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_DLbls dLbls
		{
			get
			{
				return dLblsField;
			}
			set
			{
				dLblsField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_GapAmount gapWidth
		{
			get
			{
				return gapWidthField;
			}
			set
			{
				gapWidthField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_Overlap overlap
		{
			get
			{
				return overlapField;
			}
			set
			{
				overlapField = value;
			}
		}

		[XmlElement("serLines", Order = 7)]
		public List<CT_ChartLines> serLines
		{
			get
			{
				return serLinesField;
			}
			set
			{
				serLinesField = value;
			}
		}

		[XmlElement("axId", Order = 8)]
		public List<CT_UnsignedInt> axId
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

		[XmlArray(Order = 9)]
		[XmlArrayItem("ext", IsNullable = false)]
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

		public static CT_BarChart Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_BarChart cT_BarChart = new CT_BarChart();
			cT_BarChart.ser = new List<CT_BarSer>();
			cT_BarChart.serLines = new List<CT_ChartLines>();
			cT_BarChart.axId = new List<CT_UnsignedInt>();
			cT_BarChart.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "barDir")
				{
					cT_BarChart.barDir = CT_BarDir.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "grouping")
				{
					cT_BarChart.grouping = CT_BarGrouping.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "varyColors")
				{
					cT_BarChart.varyColors = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dLbls")
				{
					cT_BarChart.dLbls = CT_DLbls.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "gapWidth")
				{
					cT_BarChart.gapWidth = CT_GapAmount.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "overlap")
				{
					cT_BarChart.overlap = CT_Overlap.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ser")
				{
					cT_BarChart.ser.Add(CT_BarSer.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "serLines")
				{
					cT_BarChart.serLines.Add(CT_ChartLines.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "axId")
				{
					cT_BarChart.axId.Add(CT_UnsignedInt.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_BarChart.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_BarChart;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (barDir != null)
			{
				barDir.Write(sw, "barDir");
			}
			if (grouping != null)
			{
				grouping.Write(sw, "grouping");
			}
			if (varyColors != null)
			{
				varyColors.Write(sw, "varyColors");
			}
			if (gapWidth != null)
			{
				gapWidth.Write(sw, "gapWidth");
			}
			if (overlap != null)
			{
				overlap.Write(sw, "overlap");
			}
			if (ser != null)
			{
				foreach (CT_BarSer item in ser)
				{
					item.Write(sw, "ser");
				}
			}
			if (serLines != null)
			{
				foreach (CT_ChartLines serLine in serLines)
				{
					serLine.Write(sw, "serLines");
				}
			}
			if (dLbls != null)
			{
				dLbls.Write(sw, "dLbls");
			}
			if (axId != null)
			{
				foreach (CT_UnsignedInt item2 in axId)
				{
					item2.Write(sw, "axId");
				}
			}
			if (extLst != null)
			{
				foreach (CT_Extension item3 in extLst)
				{
					item3.Write(sw, "extLst");
				}
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
