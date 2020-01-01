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
	public class CT_RadarChart
	{
		private CT_RadarStyle radarStyleField;

		private CT_Boolean varyColorsField;

		private List<CT_RadarSer> serField;

		private CT_DLbls dLblsField;

		private List<CT_UnsignedInt> axIdField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
		public CT_RadarStyle radarStyle
		{
			get
			{
				return radarStyleField;
			}
			set
			{
				radarStyleField = value;
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
		public List<CT_RadarSer> ser
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

		[XmlElement("axId", Order = 4)]
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

		public static CT_RadarChart Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_RadarChart cT_RadarChart = new CT_RadarChart();
			cT_RadarChart.ser = new List<CT_RadarSer>();
			cT_RadarChart.axId = new List<CT_UnsignedInt>();
			cT_RadarChart.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "radarStyle")
				{
					cT_RadarChart.radarStyle = CT_RadarStyle.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "varyColors")
				{
					cT_RadarChart.varyColors = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dLbls")
				{
					cT_RadarChart.dLbls = CT_DLbls.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ser")
				{
					cT_RadarChart.ser.Add(CT_RadarSer.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "axId")
				{
					cT_RadarChart.axId.Add(CT_UnsignedInt.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_RadarChart.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_RadarChart;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (radarStyle != null)
			{
				radarStyle.Write(sw, "radarStyle");
			}
			if (varyColors != null)
			{
				varyColors.Write(sw, "varyColors");
			}
			if (dLbls != null)
			{
				dLbls.Write(sw, "dLbls");
			}
			if (ser != null)
			{
				foreach (CT_RadarSer item in ser)
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
