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
	public class CT_ManualLayout
	{
		private CT_LayoutTarget layoutTargetField;

		private CT_LayoutMode xModeField;

		private CT_LayoutMode yModeField;

		private CT_LayoutMode wModeField;

		private CT_LayoutMode hModeField;

		private CT_Double xField;

		private CT_Double yField;

		private CT_Double wField;

		private CT_Double hField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
		public CT_LayoutTarget layoutTarget
		{
			get
			{
				return layoutTargetField;
			}
			set
			{
				layoutTargetField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_LayoutMode xMode
		{
			get
			{
				return xModeField;
			}
			set
			{
				xModeField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_LayoutMode yMode
		{
			get
			{
				return yModeField;
			}
			set
			{
				yModeField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_LayoutMode wMode
		{
			get
			{
				return wModeField;
			}
			set
			{
				wModeField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_LayoutMode hMode
		{
			get
			{
				return hModeField;
			}
			set
			{
				hModeField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_Double x
		{
			get
			{
				return xField;
			}
			set
			{
				xField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_Double y
		{
			get
			{
				return yField;
			}
			set
			{
				yField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_Double w
		{
			get
			{
				return wField;
			}
			set
			{
				wField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_Double h
		{
			get
			{
				return hField;
			}
			set
			{
				hField = value;
			}
		}

		[XmlElement(Order = 9)]
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

		public static CT_ManualLayout Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ManualLayout cT_ManualLayout = new CT_ManualLayout();
			cT_ManualLayout.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "layoutTarget")
				{
					cT_ManualLayout.layoutTarget = CT_LayoutTarget.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "xMode")
				{
					cT_ManualLayout.xMode = CT_LayoutMode.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "yMode")
				{
					cT_ManualLayout.yMode = CT_LayoutMode.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "wMode")
				{
					cT_ManualLayout.wMode = CT_LayoutMode.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hMode")
				{
					cT_ManualLayout.hMode = CT_LayoutMode.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "x")
				{
					cT_ManualLayout.x = CT_Double.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "y")
				{
					cT_ManualLayout.y = CT_Double.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "w")
				{
					cT_ManualLayout.w = CT_Double.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "h")
				{
					cT_ManualLayout.h = CT_Double.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_ManualLayout.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_ManualLayout;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (layoutTarget != null)
			{
				layoutTarget.Write(sw, "layoutTarget");
			}
			if (xMode != null)
			{
				xMode.Write(sw, "xMode");
			}
			if (yMode != null)
			{
				yMode.Write(sw, "yMode");
			}
			if (wMode != null)
			{
				wMode.Write(sw, "wMode");
			}
			if (hMode != null)
			{
				hMode.Write(sw, "hMode");
			}
			if (x != null)
			{
				x.Write(sw, "x");
			}
			if (y != null)
			{
				y.Write(sw, "y");
			}
			if (w != null)
			{
				w.Write(sw, "w");
			}
			if (h != null)
			{
				h.Write(sw, "h");
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

		public bool IsSetLayoutTarget()
		{
			return layoutTargetField != null;
		}

		public CT_LayoutTarget AddNewLayoutTarget()
		{
			layoutTargetField = new CT_LayoutTarget();
			return layoutTargetField;
		}

		public bool IsSetY()
		{
			return yField != null;
		}

		public bool IsSetX()
		{
			return xField != null;
		}

		public bool IsSetW()
		{
			return wField != null;
		}

		public bool IsSetH()
		{
			return hField != null;
		}

		public bool IsSetXMode()
		{
			return xModeField != null;
		}

		public CT_LayoutMode AddNewXMode()
		{
			xModeField = new CT_LayoutMode();
			return xModeField;
		}

		public bool IsSetYMode()
		{
			return yModeField != null;
		}

		public CT_LayoutMode AddNewYMode()
		{
			yModeField = new CT_LayoutMode();
			return yModeField;
		}

		public bool IsSetWMode()
		{
			return wModeField != null;
		}

		public bool IsSetHMode()
		{
			return hModeField != null;
		}

		public CT_LayoutMode AddNewHMode()
		{
			if (hModeField == null)
			{
				hModeField = new CT_LayoutMode();
			}
			return hModeField;
		}

		public CT_LayoutMode AddNewWMode()
		{
			if (wModeField == null)
			{
				wModeField = new CT_LayoutMode();
			}
			return wModeField;
		}

		public CT_Double AddNewW()
		{
			if (wField == null)
			{
				wField = new CT_Double();
			}
			return wField;
		}

		public CT_Double AddNewH()
		{
			if (hField == null)
			{
				hField = new CT_Double();
			}
			return hField;
		}

		public CT_Double AddNewY()
		{
			if (yField == null)
			{
				yField = new CT_Double();
			}
			return yField;
		}

		public CT_Double AddNewX()
		{
			if (xField == null)
			{
				xField = new CT_Double();
			}
			return xField;
		}
	}
}
