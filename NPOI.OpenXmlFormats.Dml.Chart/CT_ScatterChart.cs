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
	public class CT_ScatterChart
	{
		private CT_ScatterStyle scatterStyleField;

		private CT_Boolean varyColorsField;

		private List<CT_ScatterSer> serField;

		private CT_DLbls dLblsField;

		private List<CT_UnsignedInt> axIdField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
		public CT_ScatterStyle scatterStyle
		{
			get
			{
				return scatterStyleField;
			}
			set
			{
				scatterStyleField = value;
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
		public List<CT_ScatterSer> ser
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

		public static CT_ScatterChart Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ScatterChart cT_ScatterChart = new CT_ScatterChart();
			cT_ScatterChart.ser = new List<CT_ScatterSer>();
			cT_ScatterChart.axId = new List<CT_UnsignedInt>();
			cT_ScatterChart.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "scatterStyle")
				{
					cT_ScatterChart.scatterStyle = CT_ScatterStyle.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "varyColors")
				{
					cT_ScatterChart.varyColors = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dLbls")
				{
					cT_ScatterChart.dLbls = CT_DLbls.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ser")
				{
					cT_ScatterChart.ser.Add(CT_ScatterSer.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "axId")
				{
					cT_ScatterChart.axId.Add(CT_UnsignedInt.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_ScatterChart.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_ScatterChart;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (scatterStyle != null)
			{
				scatterStyle.Write(sw, "scatterStyle");
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
				foreach (CT_ScatterSer item in ser)
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

		public CT_ScatterStyle AddNewScatterStyle()
		{
			scatterStyleField = new CT_ScatterStyle();
			return scatterStyleField;
		}

		public CT_UnsignedInt AddNewAxId()
		{
			if (axIdField == null)
			{
				axIdField = new List<CT_UnsignedInt>();
			}
			CT_UnsignedInt cT_UnsignedInt = new CT_UnsignedInt();
			axIdField.Add(cT_UnsignedInt);
			return cT_UnsignedInt;
		}

		public CT_ScatterSer AddNewSer()
		{
			if (serField == null)
			{
				serField = new List<CT_ScatterSer>();
			}
			CT_ScatterSer cT_ScatterSer = new CT_ScatterSer();
			serField.Add(cT_ScatterSer);
			return cT_ScatterSer;
		}
	}
}
