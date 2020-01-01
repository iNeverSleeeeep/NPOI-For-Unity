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
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	public class CT_Bar3DChart
	{
		private CT_BarDir barDirField;

		private CT_BarGrouping groupingField;

		private CT_Boolean varyColorsField;

		private List<CT_BarSer> serField;

		private CT_DLbls dLblsField;

		private CT_GapAmount gapWidthField;

		private CT_GapAmount gapDepthField;

		private CT_Shape shapeField;

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
		public CT_GapAmount gapDepth
		{
			get
			{
				return gapDepthField;
			}
			set
			{
				gapDepthField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_Shape shape
		{
			get
			{
				return shapeField;
			}
			set
			{
				shapeField = value;
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

		public CT_Bar3DChart()
		{
			extLstField = new List<CT_Extension>();
			axIdField = new List<CT_UnsignedInt>();
			shapeField = new CT_Shape();
			gapDepthField = new CT_GapAmount();
			gapWidthField = new CT_GapAmount();
			dLblsField = new CT_DLbls();
			serField = new List<CT_BarSer>();
			varyColorsField = new CT_Boolean();
			groupingField = new CT_BarGrouping();
			barDirField = new CT_BarDir();
		}

		public static CT_Bar3DChart Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Bar3DChart cT_Bar3DChart = new CT_Bar3DChart();
			cT_Bar3DChart.ser = new List<CT_BarSer>();
			cT_Bar3DChart.axId = new List<CT_UnsignedInt>();
			cT_Bar3DChart.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "barDir")
				{
					cT_Bar3DChart.barDir = CT_BarDir.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "grouping")
				{
					cT_Bar3DChart.grouping = CT_BarGrouping.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "varyColors")
				{
					cT_Bar3DChart.varyColors = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dLbls")
				{
					cT_Bar3DChart.dLbls = CT_DLbls.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "gapWidth")
				{
					cT_Bar3DChart.gapWidth = CT_GapAmount.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "gapDepth")
				{
					cT_Bar3DChart.gapDepth = CT_GapAmount.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "shape")
				{
					cT_Bar3DChart.shape = CT_Shape.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ser")
				{
					cT_Bar3DChart.ser.Add(CT_BarSer.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "axId")
				{
					cT_Bar3DChart.axId.Add(CT_UnsignedInt.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_Bar3DChart.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_Bar3DChart;
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
			if (dLbls != null)
			{
				dLbls.Write(sw, "dLbls");
			}
			if (gapWidth != null)
			{
				gapWidth.Write(sw, "gapWidth");
			}
			if (gapDepth != null)
			{
				gapDepth.Write(sw, "gapDepth");
			}
			if (shape != null)
			{
				shape.Write(sw, "shape");
			}
			if (ser != null)
			{
				foreach (CT_BarSer item in ser)
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
