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
	public class CT_UpDownBars
	{
		private CT_GapAmount gapWidthField;

		private CT_UpDownBar upBarsField;

		private CT_UpDownBar downBarsField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
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

		[XmlElement(Order = 1)]
		public CT_UpDownBar upBars
		{
			get
			{
				return upBarsField;
			}
			set
			{
				upBarsField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_UpDownBar downBars
		{
			get
			{
				return downBarsField;
			}
			set
			{
				downBarsField = value;
			}
		}

		[XmlElement(Order = 3)]
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

		public static CT_UpDownBars Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_UpDownBars cT_UpDownBars = new CT_UpDownBars();
			cT_UpDownBars.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "gapWidth")
				{
					cT_UpDownBars.gapWidth = CT_GapAmount.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "upBars")
				{
					cT_UpDownBars.upBars = CT_UpDownBar.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "downBars")
				{
					cT_UpDownBars.downBars = CT_UpDownBar.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_UpDownBars.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_UpDownBars;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (gapWidth != null)
			{
				gapWidth.Write(sw, "gapWidth");
			}
			if (upBars != null)
			{
				upBars.Write(sw, "upBars");
			}
			if (downBars != null)
			{
				downBars.Write(sw, "downBars");
			}
			if (extLst != null)
			{
				foreach (CT_Extension item in extLst)
				{
					item.Write(sw, "extLst");
				}
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
