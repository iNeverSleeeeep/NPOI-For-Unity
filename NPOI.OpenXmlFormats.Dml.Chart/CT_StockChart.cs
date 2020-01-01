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
	public class CT_StockChart
	{
		private List<CT_LineSer> serField;

		private CT_DLbls dLblsField;

		private CT_ChartLines dropLinesField;

		private CT_ChartLines hiLowLinesField;

		private CT_UpDownBars upDownBarsField;

		private List<CT_UnsignedInt> axIdField;

		private List<CT_Extension> extLstField;

		[XmlElement("ser", Order = 0)]
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

		[XmlElement(Order = 1)]
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

		[XmlElement(Order = 2)]
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

		[XmlElement(Order = 3)]
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

		[XmlElement(Order = 4)]
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

		public static CT_StockChart Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_StockChart cT_StockChart = new CT_StockChart();
			cT_StockChart.ser = new List<CT_LineSer>();
			cT_StockChart.axId = new List<CT_UnsignedInt>();
			cT_StockChart.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "dLbls")
				{
					cT_StockChart.dLbls = CT_DLbls.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dropLines")
				{
					cT_StockChart.dropLines = CT_ChartLines.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hiLowLines")
				{
					cT_StockChart.hiLowLines = CT_ChartLines.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "upDownBars")
				{
					cT_StockChart.upDownBars = CT_UpDownBars.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ser")
				{
					cT_StockChart.ser.Add(CT_LineSer.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "axId")
				{
					cT_StockChart.axId.Add(CT_UnsignedInt.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_StockChart.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_StockChart;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
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
			if (ser != null)
			{
				foreach (CT_LineSer item in ser)
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
