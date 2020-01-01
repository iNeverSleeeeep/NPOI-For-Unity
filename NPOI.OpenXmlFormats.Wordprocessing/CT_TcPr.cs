using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_TcPr : CT_TcPrInner
	{
		private CT_TcPrChange tcPrChangeField;

		[XmlElement(Order = 0)]
		public CT_TcPrChange tcPrChange
		{
			get
			{
				return tcPrChangeField;
			}
			set
			{
				tcPrChangeField = value;
			}
		}

		public new static CT_TcPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TcPr cT_TcPr = new CT_TcPr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "tcPrChange")
				{
					cT_TcPr.tcPrChange = CT_TcPrChange.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cellIns")
				{
					cT_TcPr.cellIns = CT_TrackChange.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cellDel")
				{
					cT_TcPr.cellDel = CT_TrackChange.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cellMerge")
				{
					cT_TcPr.cellMerge = CT_CellMergeTrackChange.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cnfStyle")
				{
					cT_TcPr.cnfStyle = CT_Cnf.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tcW")
				{
					cT_TcPr.tcW = CT_TblWidth.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "gridSpan")
				{
					cT_TcPr.gridSpan = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hMerge")
				{
					cT_TcPr.hMerge = CT_HMerge.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "vMerge")
				{
					cT_TcPr.vMerge = CT_VMerge.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tcBorders")
				{
					cT_TcPr.tcBorders = CT_TcBorders.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "shd")
				{
					cT_TcPr.shd = CT_Shd.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "noWrap")
				{
					cT_TcPr.noWrap = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tcMar")
				{
					cT_TcPr.tcMar = CT_TcMar.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "textDirection")
				{
					cT_TcPr.textDirection = CT_TextDirection.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tcFitText")
				{
					cT_TcPr.tcFitText = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "vAlign")
				{
					cT_TcPr.vAlign = CT_VerticalJc.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hideMark")
				{
					cT_TcPr.hideMark = CT_OnOff.Parse(childNode, namespaceManager);
				}
			}
			return cT_TcPr;
		}

		public CT_TblWidth AddNewTcW()
		{
			base.tcW = new CT_TblWidth();
			return base.tcW;
		}

		internal new void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (tcPrChange != null)
			{
				tcPrChange.Write(sw, "tcPrChange");
			}
			if (base.cellIns != null)
			{
				base.cellIns.Write(sw, "cellIns");
			}
			if (base.cellDel != null)
			{
				base.cellDel.Write(sw, "cellDel");
			}
			if (base.cellMerge != null)
			{
				base.cellMerge.Write(sw, "cellMerge");
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
