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
	public class CT_Legend
	{
		private CT_LegendPos legendPosField;

		private List<CT_LegendEntry> legendEntryField;

		private CT_Layout layoutField;

		private CT_Boolean overlayField;

		private CT_ShapeProperties spPrField;

		private CT_TextBody txPrField;

		private List<CT_Extension> extLstField;

		private bool legendPosSpecifiedField;

		private bool layoutSpecifiedField;

		[XmlIgnore]
		public bool legendPosSpecified
		{
			get
			{
				return legendPosSpecifiedField;
			}
			set
			{
				legendPosSpecifiedField = value;
			}
		}

		[XmlElement(Order = 0)]
		public CT_LegendPos legendPos
		{
			get
			{
				return legendPosField;
			}
			set
			{
				legendPosField = value;
			}
		}

		[XmlElement("legendEntry", Order = 1)]
		public List<CT_LegendEntry> legendEntry
		{
			get
			{
				return legendEntryField;
			}
			set
			{
				legendEntryField = value;
			}
		}

		[XmlIgnore]
		public bool layoutSpecified
		{
			get
			{
				return layoutSpecifiedField;
			}
			set
			{
				layoutSpecifiedField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_Layout layout
		{
			get
			{
				return layoutField;
			}
			set
			{
				layoutField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_Boolean overlay
		{
			get
			{
				return overlayField;
			}
			set
			{
				overlayField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_ShapeProperties spPr
		{
			get
			{
				return spPrField;
			}
			set
			{
				spPrField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_TextBody txPr
		{
			get
			{
				return txPrField;
			}
			set
			{
				txPrField = value;
			}
		}

		[XmlArrayItem("ext", IsNullable = false)]
		[XmlArray(Order = 6)]
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

		public static CT_Legend Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Legend cT_Legend = new CT_Legend();
			cT_Legend.legendEntry = new List<CT_LegendEntry>();
			cT_Legend.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "legendPos")
				{
					cT_Legend.legendPos = CT_LegendPos.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "layout")
				{
					cT_Legend.layout = CT_Layout.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "overlay")
				{
					cT_Legend.overlay = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_Legend.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "txPr")
				{
					cT_Legend.txPr = CT_TextBody.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "legendEntry")
				{
					cT_Legend.legendEntry.Add(CT_LegendEntry.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_Legend.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_Legend;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (legendPos != null)
			{
				legendPos.Write(sw, "legendPos");
			}
			if (layout != null)
			{
				layout.Write(sw, "layout");
			}
			if (overlay != null)
			{
				overlay.Write(sw, "overlay");
			}
			if (spPr != null)
			{
				spPr.Write(sw, "spPr");
			}
			if (txPr != null)
			{
				txPr.Write(sw, "txPr");
			}
			if (legendEntry != null)
			{
				foreach (CT_LegendEntry item in legendEntry)
				{
					item.Write(sw, "legendEntry");
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

		public CT_Layout AddNewLayout()
		{
			layoutField = new CT_Layout();
			return layoutField;
		}

		public bool IsSetLegendPos()
		{
			return legendPosSpecifiedField;
		}

		public CT_LegendPos AddNewLegendPos()
		{
			if (legendPosField == null)
			{
				legendPosField = new CT_LegendPos();
			}
			return legendPosField;
		}

		public bool IsSetLayout()
		{
			return layoutSpecifiedField;
		}
	}
}
