using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CT_CustomGeometry2D
	{
		private CT_GeomGuideList avLstField;

		private CT_GeomGuideList gdLstField;

		private List<object> ahLstField;

		private CT_ConnectionSiteList cxnLstField;

		private CT_GeomRect rectField;

		private CT_Path2DList pathLstField;

		[XmlElement(Order = 0)]
		public CT_GeomGuideList avLst
		{
			get
			{
				return avLstField;
			}
			set
			{
				avLstField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_GeomGuideList gdLst
		{
			get
			{
				return gdLstField;
			}
			set
			{
				gdLstField = value;
			}
		}

		[XmlArray(Order = 2)]
		[XmlArrayItem("ahPolar", typeof(CT_PolarAdjustHandle), IsNullable = false)]
		[XmlArrayItem("ahXY", typeof(CT_XYAdjustHandle), IsNullable = false)]
		public List<object> ahLst
		{
			get
			{
				return ahLstField;
			}
			set
			{
				ahLstField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_ConnectionSiteList cxnLst
		{
			get
			{
				return cxnLstField;
			}
			set
			{
				cxnLstField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_GeomRect rect
		{
			get
			{
				return rectField;
			}
			set
			{
				rectField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_Path2DList pathLst
		{
			get
			{
				return pathLstField;
			}
			set
			{
				pathLstField = value;
			}
		}

		public static CT_CustomGeometry2D Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CustomGeometry2D cT_CustomGeometry2D = new CT_CustomGeometry2D();
			cT_CustomGeometry2D.ahLst = new List<object>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "rect")
				{
					cT_CustomGeometry2D.rect = CT_GeomRect.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "avLst")
				{
					cT_CustomGeometry2D.avLst = CT_GeomGuideList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "gdLst")
				{
					cT_CustomGeometry2D.gdLst = CT_GeomGuideList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cxnLst")
				{
					cT_CustomGeometry2D.cxnLst = CT_ConnectionSiteList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pathLst")
				{
					cT_CustomGeometry2D.pathLst = CT_Path2DList.Parse(childNode, namespaceManager);
				}
			}
			return cT_CustomGeometry2D;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			sw.Write(">");
			if (rect != null)
			{
				rect.Write(sw, "rect");
			}
			if (avLst != null)
			{
				avLst.Write(sw, "avLst");
			}
			if (gdLst != null)
			{
				gdLst.Write(sw, "gdLst");
			}
			if (cxnLst != null)
			{
				cxnLstField.Write(sw, "cxnLst");
			}
			if (pathLst != null)
			{
				pathLstField.Write(sw, "pathLst");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
