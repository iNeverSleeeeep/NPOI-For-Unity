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
	public class CT_LineChart
	{
		private CT_Grouping groupingField;

		private CT_Boolean varyColorsField;

		private List<CT_LineSer> serField;

		private CT_DLbls dLblsField;

		private CT_ChartLines dropLinesField;

		private CT_ChartLines hiLowLinesField;

		private CT_UpDownBars upDownBarsField;

		private CT_Boolean markerField;

		private CT_Boolean smoothField;

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
		public List<CT_LineSer> ser
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

		[XmlElement(Order = 5)]
		public CT_ChartLines hiLowLines
		{
			get
			{
				return hiLowLinesField;
			}
			set
			{
				hiLowLinesField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_UpDownBars upDownBars
		{
			get
			{
				return upDownBarsField;
			}
			set
			{
				upDownBarsField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_Boolean marker
		{
			get
			{
				return markerField;
			}
			set
			{
				markerField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_Boolean smooth
		{
			get
			{
				return smoothField;
			}
			set
			{
				smoothField = value;
			}
		}

		[XmlElement("axId", Order = 9)]
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

		[XmlElement(Order = 10)]
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

		public static CT_LineChart Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_LineChart cT_LineChart = new CT_LineChart();
			cT_LineChart.ser = new List<CT_LineSer>();
			cT_LineChart.axId = new List<CT_UnsignedInt>();
			cT_LineChart.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "grouping")
				{
					cT_LineChart.grouping = CT_Grouping.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "varyColors")
				{
					cT_LineChart.varyColors = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dLbls")
				{
					cT_LineChart.dLbls = CT_DLbls.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dropLines")
				{
					cT_LineChart.dropLines = CT_ChartLines.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hiLowLines")
				{
					cT_LineChart.hiLowLines = CT_ChartLines.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "upDownBars")
				{
					cT_LineChart.upDownBars = CT_UpDownBars.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "marker")
				{
					cT_LineChart.marker = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "smooth")
				{
					cT_LineChart.smooth = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ser")
				{
					cT_LineChart.ser.Add(CT_LineSer.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "axId")
				{
					cT_LineChart.axId.Add(CT_UnsignedInt.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_LineChart.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_LineChart;
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
			if (ser != null)
			{
				foreach (CT_LineSer item in ser)
				{
					item.Write(sw, "ser");
				}
			}
			if (dLbls != null)
			{
				dLbls.Write(sw, "dLbls");
			}
			if (dropLines != null)
			{
				dropLines.Write(sw, "dropLines");
			}
			if (hiLowLines != null)
			{
				hiLowLines.Write(sw, "hiLowLines");
			}
			if (upDownBars != null)
			{
				upDownBars.Write(sw, "upDownBars");
			}
			if (marker != null)
			{
				marker.Write(sw, "marker");
			}
			if (smooth != null)
			{
				smooth.Write(sw, "smooth");
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

		public CT_Grouping AddNewGrouping()
		{
			groupingField = new CT_Grouping();
			return groupingField;
		}

		public CT_LineSer AddNewSer()
		{
			CT_LineSer cT_LineSer = new CT_LineSer();
			if (serField == null)
			{
				serField = new List<CT_LineSer>();
			}
			serField.Add(cT_LineSer);
			return cT_LineSer;
		}

		public CT_Boolean AddNewVaryColors()
		{
			varyColorsField = new CT_Boolean();
			return varyColorsField;
		}

		public CT_UnsignedInt AddNewAxId()
		{
			CT_UnsignedInt cT_UnsignedInt = new CT_UnsignedInt();
			if (axIdField == null)
			{
				axIdField = new List<CT_UnsignedInt>();
			}
			axIdField.Add(cT_UnsignedInt);
			return cT_UnsignedInt;
		}
	}
}
