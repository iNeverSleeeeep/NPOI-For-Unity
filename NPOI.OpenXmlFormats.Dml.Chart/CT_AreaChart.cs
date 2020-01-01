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
	public class CT_AreaChart
	{
		private CT_Grouping groupingField;

		private CT_Boolean varyColorsField;

		private List<CT_AreaSer> serField;

		private CT_DLbls dLblsField;

		private CT_ChartLines dropLinesField;

		private List<CT_UnsignedInt> axIdField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
		public CT_Grouping grouping
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

		[XmlElement(Order = 1)]
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

		[XmlElement("ser", Order = 2)]
		public List<CT_AreaSer> ser
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

		[XmlElement(Order = 3)]
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

		[XmlElement(Order = 4)]
		public CT_ChartLines dropLines
		{
			get
			{
				return dropLinesField;
			}
			set
			{
				dropLinesField = value;
			}
		}

		[XmlElement("axId", Order = 5)]
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

		[XmlElement(Order = 6)]
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

		public static CT_AreaChart Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_AreaChart cT_AreaChart = new CT_AreaChart();
			cT_AreaChart.ser = new List<CT_AreaSer>();
			cT_AreaChart.axId = new List<CT_UnsignedInt>();
			cT_AreaChart.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "grouping")
				{
					cT_AreaChart.grouping = CT_Grouping.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "varyColors")
				{
					cT_AreaChart.varyColors = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dLbls")
				{
					cT_AreaChart.dLbls = CT_DLbls.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dropLines")
				{
					cT_AreaChart.dropLines = CT_ChartLines.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ser")
				{
					cT_AreaChart.ser.Add(CT_AreaSer.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "axId")
				{
					cT_AreaChart.axId.Add(CT_UnsignedInt.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_AreaChart.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_AreaChart;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (grouping != null)
			{
				grouping.Write(sw, "grouping");
			}
			if (varyColors != null)
			{
				varyColors.Write(sw, "varyColors");
			}
			if (dLbls != null)
			{
				dLbls.Write(sw, "dLbls");
			}
			if (dropLines != null)
			{
				dropLines.Write(sw, "dropLines");
			}
			if (ser != null)
			{
				foreach (CT_AreaSer item in ser)
				{
					item.Write(sw, "ser");
				}
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
