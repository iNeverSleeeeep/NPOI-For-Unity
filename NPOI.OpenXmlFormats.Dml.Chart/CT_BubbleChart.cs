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
	public class CT_BubbleChart
	{
		private CT_Boolean varyColorsField;

		private List<CT_BubbleSer> serField;

		private CT_DLbls dLblsField;

		private CT_Boolean bubble3DField;

		private CT_BubbleScale bubbleScaleField;

		private CT_Boolean showNegBubblesField;

		private CT_SizeRepresents sizeRepresentsField;

		private List<CT_UnsignedInt> axIdField;

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
		public List<CT_BubbleSer> ser
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
		public CT_Boolean bubble3D
		{
			get
			{
				return bubble3DField;
			}
			set
			{
				bubble3DField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_BubbleScale bubbleScale
		{
			get
			{
				return bubbleScaleField;
			}
			set
			{
				bubbleScaleField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_Boolean showNegBubbles
		{
			get
			{
				return showNegBubblesField;
			}
			set
			{
				showNegBubblesField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_SizeRepresents sizeRepresents
		{
			get
			{
				return sizeRepresentsField;
			}
			set
			{
				sizeRepresentsField = value;
			}
		}

		[XmlElement("axId", Order = 7)]
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

		[XmlElement(Order = 8)]
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

		public static CT_BubbleChart Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_BubbleChart cT_BubbleChart = new CT_BubbleChart();
			cT_BubbleChart.ser = new List<CT_BubbleSer>();
			cT_BubbleChart.axId = new List<CT_UnsignedInt>();
			cT_BubbleChart.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "varyColors")
				{
					cT_BubbleChart.varyColors = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dLbls")
				{
					cT_BubbleChart.dLbls = CT_DLbls.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bubble3D")
				{
					cT_BubbleChart.bubble3D = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bubbleScale")
				{
					cT_BubbleChart.bubbleScale = CT_BubbleScale.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "showNegBubbles")
				{
					cT_BubbleChart.showNegBubbles = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sizeRepresents")
				{
					cT_BubbleChart.sizeRepresents = CT_SizeRepresents.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ser")
				{
					cT_BubbleChart.ser.Add(CT_BubbleSer.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "axId")
				{
					cT_BubbleChart.axId.Add(CT_UnsignedInt.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_BubbleChart.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_BubbleChart;
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
			if (bubble3D != null)
			{
				bubble3D.Write(sw, "bubble3D");
			}
			if (bubbleScale != null)
			{
				bubbleScale.Write(sw, "bubbleScale");
			}
			if (showNegBubbles != null)
			{
				showNegBubbles.Write(sw, "showNegBubbles");
			}
			if (sizeRepresents != null)
			{
				sizeRepresents.Write(sw, "sizeRepresents");
			}
			if (ser != null)
			{
				foreach (CT_BubbleSer item in ser)
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
