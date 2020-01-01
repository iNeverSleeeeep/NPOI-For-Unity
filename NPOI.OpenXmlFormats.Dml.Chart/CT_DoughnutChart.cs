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
	public class CT_DoughnutChart
	{
		private CT_Boolean varyColorsField;

		private List<CT_PieSer> serField;

		private CT_DLbls dLblsField;

		private CT_FirstSliceAng firstSliceAngField;

		private CT_HoleSize holeSizeField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
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

		[XmlElement("ser", Order = 1)]
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

		[XmlElement(Order = 2)]
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

		[XmlElement(Order = 3)]
		public CT_FirstSliceAng firstSliceAng
		{
			get
			{
				return firstSliceAngField;
			}
			set
			{
				firstSliceAngField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_HoleSize holeSize
		{
			get
			{
				return holeSizeField;
			}
			set
			{
				holeSizeField = value;
			}
		}

		[XmlArrayItem("ext", IsNullable = false)]
		[XmlArray(Order = 5)]
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

		public static CT_DoughnutChart Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_DoughnutChart cT_DoughnutChart = new CT_DoughnutChart();
			cT_DoughnutChart.ser = new List<CT_PieSer>();
			cT_DoughnutChart.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "varyColors")
				{
					cT_DoughnutChart.varyColors = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dLbls")
				{
					cT_DoughnutChart.dLbls = CT_DLbls.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "firstSliceAng")
				{
					cT_DoughnutChart.firstSliceAng = CT_FirstSliceAng.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "holeSize")
				{
					cT_DoughnutChart.holeSize = CT_HoleSize.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ser")
				{
					cT_DoughnutChart.ser.Add(CT_PieSer.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_DoughnutChart.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_DoughnutChart;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (varyColors != null)
			{
				varyColors.Write(sw, "varyColors");
			}
			if (dLbls != null)
			{
				dLbls.Write(sw, "dLbls");
			}
			if (firstSliceAng != null)
			{
				firstSliceAng.Write(sw, "firstSliceAng");
			}
			if (holeSize != null)
			{
				holeSize.Write(sw, "holeSize");
			}
			if (ser != null)
			{
				foreach (CT_PieSer item in ser)
				{
					item.Write(sw, "ser");
				}
			}
			if (extLst != null)
			{
				foreach (CT_Extension item2 in extLst)
				{
					item2.Write(sw, "extLst");
				}
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
