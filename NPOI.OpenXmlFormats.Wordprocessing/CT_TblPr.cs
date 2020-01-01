using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_TblPr : CT_TblPrBase
	{
		private CT_TblPrChange tblPrChangeField;

		[XmlElement(Order = 0)]
		public CT_TblPrChange tblPrChange
		{
			get
			{
				return tblPrChangeField;
			}
			set
			{
				tblPrChangeField = value;
			}
		}

		public new static CT_TblPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TblPr cT_TblPr = new CT_TblPr();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "tblPrChange")
				{
					cT_TblPr.tblPrChange = CT_TblPrChange.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblStyle")
				{
					cT_TblPr.tblStyle = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblpPr")
				{
					cT_TblPr.tblpPr = CT_TblPPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblOverlap")
				{
					cT_TblPr.tblOverlap = CT_TblOverlap.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bidiVisual")
				{
					cT_TblPr.bidiVisual = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblStyleRowBandSize")
				{
					cT_TblPr.tblStyleRowBandSize = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblStyleColBandSize")
				{
					cT_TblPr.tblStyleColBandSize = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblW")
				{
					cT_TblPr.tblW = CT_TblWidth.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "jc")
				{
					cT_TblPr.jc = CT_Jc.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblCellSpacing")
				{
					cT_TblPr.tblCellSpacing = CT_TblWidth.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblInd")
				{
					cT_TblPr.tblInd = CT_TblWidth.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblBorders")
				{
					cT_TblPr.tblBorders = CT_TblBorders.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "shd")
				{
					cT_TblPr.shd = CT_Shd.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblLayout")
				{
					cT_TblPr.tblLayout = CT_TblLayoutType.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblCellMar")
				{
					cT_TblPr.tblCellMar = CT_TblCellMar.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblLook")
				{
					cT_TblPr.tblLook = CT_ShortHexNumber.Parse(childNode, namespaceManager);
				}
			}
			return cT_TblPr;
		}

		internal new void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (tblPrChange != null)
			{
				tblPrChange.Write(sw, "tblPrChange");
			}
			if (base.tblStyle != null)
			{
				base.tblStyle.Write(sw, "tblStyle");
			}
			if (base.tblpPr != null)
			{
				base.tblpPr.Write(sw, "tblpPr");
			}
			if (base.tblOverlap != null)
			{
				base.tblOverlap.Write(sw, "tblOverlap");
			}
			if (base.bidiVisual != null)
			{
				base.bidiVisual.Write(sw, "bidiVisual");
			}
			if (base.tblStyleRowBandSize != null)
			{
				base.tblStyleRowBandSize.Write(sw, "tblStyleRowBandSize");
			}
			if (base.tblStyleColBandSize != null)
			{
				base.tblStyleColBandSize.Write(sw, "tblStyleColBandSize");
			}
			if (base.tblW != null)
			{
				base.tblW.Write(sw, "tblW");
			}
			if (base.jc != null)
			{
				base.jc.Write(sw, "jc");
			}
			if (base.tblCellSpacing != null)
			{
				base.tblCellSpacing.Write(sw, "tblCellSpacing");
			}
			if (base.tblInd != null)
			{
				base.tblInd.Write(sw, "tblInd");
			}
			if (base.tblBorders != null)
			{
				base.tblBorders.Write(sw, "tblBorders");
			}
			if (base.shd != null)
			{
				base.shd.Write(sw, "shd");
			}
			if (base.tblLayout != null)
			{
				base.tblLayout.Write(sw, "tblLayout");
			}
			if (base.tblCellMar != null)
			{
				base.tblCellMar.Write(sw, "tblCellMar");
			}
			if (base.tblLook != null)
			{
				base.tblLook.Write(sw, "tblLook");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public CT_TblLayoutType AddNewTblLayout()
		{
			base.tblLayout = new CT_TblLayoutType();
			return base.tblLayout;
		}
	}
}
