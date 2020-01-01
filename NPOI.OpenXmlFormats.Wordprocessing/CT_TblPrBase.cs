using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlInclude(typeof(CT_TblPr))]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_TblPrBase
	{
		private CT_String tblStyleField;

		private CT_TblPPr tblpPrField;

		private CT_TblOverlap tblOverlapField;

		private CT_OnOff bidiVisualField;

		private CT_DecimalNumber tblStyleRowBandSizeField;

		private CT_DecimalNumber tblStyleColBandSizeField;

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
		public CT_String tblStyle
		{
			get
			{
				return tblStyleField;
			}
			set
			{
				tblStyleField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_TblPPr tblpPr
		{
			get
			{
				return tblpPrField;
			}
			set
			{
				tblpPrField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_TblOverlap tblOverlap
		{
			get
			{
				return tblOverlapField;
			}
			set
			{
				tblOverlapField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_OnOff bidiVisual
		{
			get
			{
				return bidiVisualField;
			}
			set
			{
				bidiVisualField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_DecimalNumber tblStyleRowBandSize
		{
			get
			{
				return tblStyleRowBandSizeField;
			}
			set
			{
				tblStyleRowBandSizeField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_DecimalNumber tblStyleColBandSize
		{
			get
			{
				return tblStyleColBandSizeField;
			}
			set
			{
				tblStyleColBandSizeField = value;
			}
		}

		[XmlElement(Order = 6)]
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

		[XmlElement(Order = 7)]
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

		[XmlElement(Order = 8)]
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

		[XmlElement(Order = 9)]
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

		[XmlElement(Order = 10)]
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

		[XmlElement(Order = 11)]
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

		[XmlElement(Order = 12)]
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

		[XmlElement(Order = 13)]
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

		[XmlElement(Order = 14)]
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

		public static CT_TblPrBase Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TblPrBase cT_TblPrBase = new CT_TblPrBase();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "tblStyle")
				{
					cT_TblPrBase.tblStyle = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblpPr")
				{
					cT_TblPrBase.tblpPr = CT_TblPPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblOverlap")
				{
					cT_TblPrBase.tblOverlap = CT_TblOverlap.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bidiVisual")
				{
					cT_TblPrBase.bidiVisual = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblStyleRowBandSize")
				{
					cT_TblPrBase.tblStyleRowBandSize = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblStyleColBandSize")
				{
					cT_TblPrBase.tblStyleColBandSize = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblW")
				{
					cT_TblPrBase.tblW = CT_TblWidth.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "jc")
				{
					cT_TblPrBase.jc = CT_Jc.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblCellSpacing")
				{
					cT_TblPrBase.tblCellSpacing = CT_TblWidth.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblInd")
				{
					cT_TblPrBase.tblInd = CT_TblWidth.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblBorders")
				{
					cT_TblPrBase.tblBorders = CT_TblBorders.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "shd")
				{
					cT_TblPrBase.shd = CT_Shd.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblLayout")
				{
					cT_TblPrBase.tblLayout = CT_TblLayoutType.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblCellMar")
				{
					cT_TblPrBase.tblCellMar = CT_TblCellMar.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tblLook")
				{
					cT_TblPrBase.tblLook = CT_ShortHexNumber.Parse(childNode, namespaceManager);
				}
			}
			return cT_TblPrBase;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (tblStyle != null)
			{
				tblStyle.Write(sw, "tblStyle");
			}
			if (tblpPr != null)
			{
				tblpPr.Write(sw, "tblpPr");
			}
			if (tblOverlap != null)
			{
				tblOverlap.Write(sw, "tblOverlap");
			}
			if (bidiVisual != null)
			{
				bidiVisual.Write(sw, "bidiVisual");
			}
			if (tblStyleRowBandSize != null)
			{
				tblStyleRowBandSize.Write(sw, "tblStyleRowBandSize");
			}
			if (tblStyleColBandSize != null)
			{
				tblStyleColBandSize.Write(sw, "tblStyleColBandSize");
			}
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

		public bool IsSetTblW()
		{
			return tblW != null;
		}

		public CT_TblWidth AddNewTblW()
		{
			if (tblWField == null)
			{
				tblWField = new CT_TblWidth();
			}
			return tblWField;
		}

		public CT_TblBorders AddNewTblBorders()
		{
			if (tblBordersField == null)
			{
				tblBordersField = new CT_TblBorders();
			}
			return tblBordersField;
		}

		public CT_String AddNewTblStyle()
		{
			tblStyleField = new CT_String();
			return tblStyleField;
		}

		public bool IsSetTblBorders()
		{
			return tblBordersField != null;
		}

		public bool IsSetTblStyleRowBandSize()
		{
			return tblStyleRowBandSizeField != null;
		}

		public CT_DecimalNumber AddNewTblStyleRowBandSize()
		{
			tblStyleRowBandSizeField = new CT_DecimalNumber();
			return tblStyleRowBandSizeField;
		}

		public bool IsSetTblStyleColBandSize()
		{
			return tblStyleColBandSizeField != null;
		}

		public CT_DecimalNumber AddNewTblStyleColBandSize()
		{
			tblStyleColBandSizeField = new CT_DecimalNumber();
			return tblStyleColBandSizeField;
		}

		public bool IsSetTblCellMar()
		{
			return tblCellMarField != null;
		}

		public CT_TblCellMar AddNewTblCellMar()
		{
			tblCellMarField = new CT_TblCellMar();
			return tblCellMarField;
		}
	}
}
