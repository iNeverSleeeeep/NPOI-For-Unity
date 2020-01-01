using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlInclude(typeof(CT_TcPr))]
	public class CT_TcPrInner : CT_TcPrBase
	{
		private CT_TrackChange cellInsField;

		private CT_TrackChange cellDelField;

		private CT_CellMergeTrackChange cellMergeField;

		[XmlElement(Order = 0)]
		public CT_TrackChange cellIns
		{
			get
			{
				return cellInsField;
			}
			set
			{
				cellInsField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_TrackChange cellDel
		{
			get
			{
				return cellDelField;
			}
			set
			{
				cellDelField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_CellMergeTrackChange cellMerge
		{
			get
			{
				return cellMergeField;
			}
			set
			{
				cellMergeField = value;
			}
		}

		public static CT_TcPrInner Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TcPrInner cT_TcPrInner = new CT_TcPrInner();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "cellIns")
				{
					cT_TcPrInner.cellIns = CT_TrackChange.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cellDel")
				{
					cT_TcPrInner.cellDel = CT_TrackChange.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cellMerge")
				{
					cT_TcPrInner.cellMerge = CT_CellMergeTrackChange.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cnfStyle")
				{
					cT_TcPrInner.cnfStyle = CT_Cnf.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tcW")
				{
					cT_TcPrInner.tcW = CT_TblWidth.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "gridSpan")
				{
					cT_TcPrInner.gridSpan = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hMerge")
				{
					cT_TcPrInner.hMerge = CT_HMerge.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "vMerge")
				{
					cT_TcPrInner.vMerge = CT_VMerge.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tcBorders")
				{
					cT_TcPrInner.tcBorders = CT_TcBorders.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "shd")
				{
					cT_TcPrInner.shd = CT_Shd.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "noWrap")
				{
					cT_TcPrInner.noWrap = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tcMar")
				{
					cT_TcPrInner.tcMar = CT_TcMar.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "textDirection")
				{
					cT_TcPrInner.textDirection = CT_TextDirection.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tcFitText")
				{
					cT_TcPrInner.tcFitText = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "vAlign")
				{
					cT_TcPrInner.vAlign = CT_VerticalJc.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hideMark")
				{
					cT_TcPrInner.hideMark = CT_OnOff.Parse(childNode, namespaceManager);
				}
			}
			return cT_TcPrInner;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (cellIns != null)
			{
				cellIns.Write(sw, "cellIns");
			}
			if (cellDel != null)
			{
				cellDel.Write(sw, "cellDel");
			}
			if (cellMerge != null)
			{
				cellMerge.Write(sw, "cellMerge");
			}
			if (base.cnfStyle != null)
			{
				base.cnfStyle.Write(sw, "cnfStyle");
			}
			if (base.tcW != null)
			{
				base.tcW.Write(sw, "tcW");
			}
			if (base.gridSpan != null)
			{
				base.gridSpan.Write(sw, "gridSpan");
			}
			if (base.hMerge != null)
			{
				base.hMerge.Write(sw, "hMerge");
			}
			if (base.vMerge != null)
			{
				base.vMerge.Write(sw, "vMerge");
			}
			if (base.tcBorders != null)
			{
				base.tcBorders.Write(sw, "tcBorders");
			}
			if (base.shd != null)
			{
				base.shd.Write(sw, "shd");
			}
			if (base.noWrap != null)
			{
				base.noWrap.Write(sw, "noWrap");
			}
			if (base.tcMar != null)
			{
				base.tcMar.Write(sw, "tcMar");
			}
			if (base.textDirection != null)
			{
				base.textDirection.Write(sw, "textDirection");
			}
			if (base.tcFitText != null)
			{
				base.tcFitText.Write(sw, "tcFitText");
			}
			if (base.vAlign != null)
			{
				base.vAlign.Write(sw, "vAlign");
			}
			if (base.hideMark != null)
			{
				base.hideMark.Write(sw, "hideMark");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
