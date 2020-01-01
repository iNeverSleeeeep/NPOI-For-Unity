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
	public class CT_OfPieChart
	{
		private CT_OfPieType ofPieTypeField;

		private CT_Boolean varyColorsField;

		private List<CT_PieSer> serField;

		private CT_DLbls dLblsField;

		private CT_GapAmount gapWidthField;

		private CT_SplitType splitTypeField;

		private CT_Double splitPosField;

		private List<CT_UnsignedInt> custSplitField;

		private CT_SecondPieSize secondPieSizeField;

		private List<CT_ChartLines> serLinesField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
		public CT_OfPieType ofPieType
		{
			get
			{
				return ofPieTypeField;
			}
			set
			{
				ofPieTypeField = value;
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
		public List<CT_PieSer> ser
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

		[XmlElement(Order = 5)]
		public CT_SplitType splitType
		{
			get
			{
				return splitTypeField;
			}
			set
			{
				splitTypeField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_Double splitPos
		{
			get
			{
				return splitPosField;
			}
			set
			{
				splitPosField = value;
			}
		}

		[XmlElement(Order = 7)]
		public List<CT_UnsignedInt> custSplit
		{
			get
			{
				return custSplitField;
			}
			set
			{
				custSplitField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_SecondPieSize secondPieSize
		{
			get
			{
				return secondPieSizeField;
			}
			set
			{
				secondPieSizeField = value;
			}
		}

		[XmlElement("serLines", Order = 9)]
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

		[XmlArray(Order = 10)]
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

		public static CT_OfPieChart Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_OfPieChart cT_OfPieChart = new CT_OfPieChart();
			cT_OfPieChart.ser = new List<CT_PieSer>();
			cT_OfPieChart.custSplit = new List<CT_UnsignedInt>();
			cT_OfPieChart.serLines = new List<CT_ChartLines>();
			cT_OfPieChart.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "ofPieType")
				{
					cT_OfPieChart.ofPieType = CT_OfPieType.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "varyColors")
				{
					cT_OfPieChart.varyColors = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dLbls")
				{
					cT_OfPieChart.dLbls = CT_DLbls.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "gapWidth")
				{
					cT_OfPieChart.gapWidth = CT_GapAmount.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "splitType")
				{
					cT_OfPieChart.splitType = CT_SplitType.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "splitPos")
				{
					cT_OfPieChart.splitPos = CT_Double.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "secondPieSize")
				{
					cT_OfPieChart.secondPieSize = CT_SecondPieSize.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ser")
				{
					cT_OfPieChart.ser.Add(CT_PieSer.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "custSplit")
				{
					cT_OfPieChart.custSplit.Add(CT_UnsignedInt.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "serLines")
				{
					cT_OfPieChart.serLines.Add(CT_ChartLines.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_OfPieChart.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_OfPieChart;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (ofPieType != null)
			{
				ofPieType.Write(sw, "ofPieType");
			}
			if (varyColors != null)
			{
				varyColors.Write(sw, "varyColors");
			}
			if (dLbls != null)
			{
				dLbls.Write(sw, "dLbls");
			}
			if (gapWidth != null)
			{
				gapWidth.Write(sw, "gapWidth");
			}
			if (splitType != null)
			{
				splitType.Write(sw, "splitType");
			}
			if (splitPos != null)
			{
				splitPos.Write(sw, "splitPos");
			}
			if (secondPieSize != null)
			{
				secondPieSize.Write(sw, "secondPieSize");
			}
			if (ser != null)
			{
				foreach (CT_PieSer item in ser)
				{
					item.Write(sw, "ser");
				}
			}
			if (custSplit != null)
			{
				foreach (CT_UnsignedInt item2 in custSplit)
				{
					item2.Write(sw, "custSplit");
				}
			}
			if (serLines != null)
			{
				foreach (CT_ChartLines serLine in serLines)
				{
					serLine.Write(sw, "serLines");
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
