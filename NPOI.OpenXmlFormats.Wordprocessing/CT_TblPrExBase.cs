using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlInclude(typeof(CT_TblPrEx))]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_TblPrExBase
	{
		private CT_TblWidth tblWField;

		private CT_Jc jcField;

		private CT_TblWidth tblCellSpacingField;

		private CT_TblWidth tblIndField;

		private CT_TblBorders tblBordersField;

		private CT_Shd shdField;

		private CT_TblLayoutType tblLayoutField;

		private CT_TblCellMar tblCellMarField;

		private CT_ShortHexNumber tblLookField;

		[XmlElement(Order = 0)]
		public CT_TblWidth tblW
		{
			get
			{
				return tblWField;
			}
			set
			{
				tblWField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Jc jc
		{
			get
			{
				return jcField;
			}
			set
			{
				jcField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_TblWidth tblCellSpacing
		{
			get
			{
				return tblCellSpacingField;
			}
			set
			{
				tblCellSpacingField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_TblWidth tblInd
		{
			get
			{
				return tblIndField;
			}
			set
			{
				tblIndField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_TblBorders tblBorders
		{
			get
			{
				return tblBordersField;
			}
			set
			{
				tblBordersField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_Shd shd
		{
			get
			{
				return shdField;
			}
			set
			{
				shdField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_TblLayoutType tblLayout
		{
			get
			{
				return tblLayoutField;
			}
			set
			{
				tblLayoutField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_TblCellMar tblCellMar
		{
			get
			{
				return tblCellMarField;
			}
			set
			{
				tblCellMarField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_ShortHexNumber tblLook
		{
			get
			{
				return tblLookField;
			}
			set
			{
				tblLookField = value;
			}
		}

		public static CT_TblPrExBase Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TblPrExBase cT_TblPrExBase = new CT_TblPrExBase();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "tblW")
				{
					cT_TblPrExBase.tblW = CT_TblWidth.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "jc")
				{
					cT_TblPrExBase.jc = CT_Jc.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblCellSpacing")
				{
					cT_TblPrExBase.tblCellSpacing = CT_TblWidth.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblInd")
				{
					cT_TblPrExBase.tblInd = CT_TblWidth.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblBorders")
				{
					cT_TblPrExBase.tblBorders = CT_TblBorders.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "shd")
				{
					cT_TblPrExBase.shd = CT_Shd.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblLayout")
				{
					cT_TblPrExBase.tblLayout = CT_TblLayoutType.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblCellMar")
				{
					cT_TblPrExBase.tblCellMar = CT_TblCellMar.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblLook")
				{
					cT_TblPrExBase.tblLook = CT_ShortHexNumber.Parse(childNode, namespaceManager);
				}
			}
			return cT_TblPrExBase;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (tblW != null)
			{
				tblW.Write(sw, "tblW");
			}
			if (jc != null)
			{
				jc.Write(sw, "jc");
			}
			if (tblCellSpacing != null)
			{
				tblCellSpacing.Write(sw, "tblCellSpacing");
			}
			if (tblInd != null)
			{
				tblInd.Write(sw, "tblInd");
			}
			if (tblBorders != null)
			{
				tblBorders.Write(sw, "tblBorders");
			}
			if (shd != null)
			{
				shd.Write(sw, "shd");
			}
			if (tblLayout != null)
			{
				tblLayout.Write(sw, "tblLayout");
			}
			if (tblCellMar != null)
			{
				tblCellMar.Write(sw, "tblCellMar");
			}
			if (tblLook != null)
			{
				tblLook.Write(sw, "tblLook");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
