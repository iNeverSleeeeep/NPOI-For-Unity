using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_TblPrEx : CT_TblPrExBase
	{
		private CT_TblPrExChange tblPrExChangeField;

		[XmlElement(Order = 0)]
		public CT_TblPrExChange tblPrExChange
		{
			get
			{
				return tblPrExChangeField;
			}
			set
			{
				tblPrExChangeField = value;
			}
		}

		public new static CT_TblPrEx Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TblPrEx cT_TblPrEx = new CT_TblPrEx();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "tblPrExChange")
				{
					cT_TblPrEx.tblPrExChange = CT_TblPrExChange.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblW")
				{
					cT_TblPrEx.tblW = CT_TblWidth.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "jc")
				{
					cT_TblPrEx.jc = CT_Jc.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblCellSpacing")
				{
					cT_TblPrEx.tblCellSpacing = CT_TblWidth.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblInd")
				{
					cT_TblPrEx.tblInd = CT_TblWidth.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblBorders")
				{
					cT_TblPrEx.tblBorders = CT_TblBorders.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "shd")
				{
					cT_TblPrEx.shd = CT_Shd.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblLayout")
				{
					cT_TblPrEx.tblLayout = CT_TblLayoutType.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblCellMar")
				{
					cT_TblPrEx.tblCellMar = CT_TblCellMar.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblLook")
				{
					cT_TblPrEx.tblLook = CT_ShortHexNumber.Parse(childNode, namespaceManager);
				}
			}
			return cT_TblPrEx;
		}

		internal new void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (tblPrExChange != null)
			{
				tblPrExChange.Write(sw, "tblPrExChange");
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
	}
}
