using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlRoot(ElementName = "xf", Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = false)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Xf
	{
		private CT_CellAlignment alignmentField;

		private CT_CellProtection protectionField;

		private CT_ExtensionList extLstField;

		private uint numFmtIdField;

		private uint fontIdField;

		private uint fillIdField;

		private uint borderIdField;

		private uint xfIdField;

		private bool quotePrefixField;

		private bool pivotButtonField;

		private bool applyNumberFormatField;

		private bool applyFontField;

		private bool applyFillField;

		private bool applyBorderField;

		private bool applyAlignmentField;

		private bool applyProtectionField;

		private bool numFmtIdSpecifiedField;

		private bool fontIdSpecifiedField;

		private bool fillIdSpecifiedField;

		private bool borderIdSpecifiedField;

		private bool xfIdSpecifiedField;

		[XmlElement]
		public CT_CellAlignment alignment
		{
			get
			{
				return alignmentField;
			}
			set
			{
				alignmentField = value;
			}
		}

		[XmlElement]
		public CT_CellProtection protection
		{
			get
			{
				return protectionField;
			}
			set
			{
				protectionField = value;
			}
		}

		[XmlElement]
		public CT_ExtensionList extLst
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

		[XmlAttribute]
		public uint numFmtId
		{
			get
			{
				return numFmtIdField;
			}
			set
			{
				numFmtIdField = value;
				numFmtIdSpecifiedField = true;
			}
		}

		[XmlIgnore]
		public bool numFmtIdSpecified
		{
			get
			{
				return numFmtIdSpecifiedField;
			}
			set
			{
				numFmtIdSpecifiedField = value;
			}
		}

		[XmlAttribute]
		public uint fontId
		{
			get
			{
				return fontIdField;
			}
			set
			{
				fontIdField = value;
				fontIdSpecifiedField = true;
			}
		}

		[XmlIgnore]
		public bool fontIdSpecified
		{
			get
			{
				return fontIdSpecifiedField;
			}
			set
			{
				fontIdSpecifiedField = value;
			}
		}

		[XmlAttribute]
		public uint fillId
		{
			get
			{
				return fillIdField;
			}
			set
			{
				fillIdField = value;
				fillIdSpecifiedField = true;
			}
		}

		[XmlIgnore]
		public bool fillIdSpecified
		{
			get
			{
				return fillIdSpecifiedField;
			}
			set
			{
				fillIdSpecifiedField = value;
			}
		}

		[XmlAttribute]
		public uint borderId
		{
			get
			{
				return borderIdField;
			}
			set
			{
				borderIdField = value;
				borderIdSpecifiedField = true;
			}
		}

		[XmlIgnore]
		public bool borderIdSpecified
		{
			get
			{
				return borderIdSpecifiedField;
			}
			set
			{
				borderIdSpecifiedField = value;
			}
		}

		[XmlAttribute]
		public uint xfId
		{
			get
			{
				return xfIdField;
			}
			set
			{
				xfIdField = value;
				xfIdSpecifiedField = true;
			}
		}

		[XmlIgnore]
		public bool xfIdSpecified
		{
			get
			{
				return xfIdSpecifiedField;
			}
			set
			{
				xfIdSpecifiedField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool quotePrefix
		{
			get
			{
				return quotePrefixField;
			}
			set
			{
				quotePrefixField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool pivotButton
		{
			get
			{
				return pivotButtonField;
			}
			set
			{
				pivotButtonField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool applyNumberFormat
		{
			get
			{
				return applyNumberFormatField;
			}
			set
			{
				applyNumberFormatField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool applyFont
		{
			get
			{
				return applyFontField;
			}
			set
			{
				applyFontField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool applyFill
		{
			get
			{
				return applyFillField;
			}
			set
			{
				applyFillField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool applyBorder
		{
			get
			{
				return applyBorderField;
			}
			set
			{
				applyBorderField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool applyAlignment
		{
			get
			{
				return applyAlignmentField;
			}
			set
			{
				applyAlignmentField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool applyProtection
		{
			get
			{
				return applyProtectionField;
			}
			set
			{
				applyProtectionField = value;
			}
		}

		public CT_Xf Copy()
		{
			CT_Xf cT_Xf = new CT_Xf();
			if (alignment != null)
			{
				cT_Xf.alignment = alignment.Copy();
			}
			cT_Xf.protection = protection;
			cT_Xf.extLstField = ((extLstField == null) ? null : extLstField.Copy());
			cT_Xf.applyAlignment = applyAlignment;
			cT_Xf.applyBorder = applyBorder;
			cT_Xf.applyFill = applyFill;
			cT_Xf.applyFont = applyFont;
			cT_Xf.applyNumberFormat = applyNumberFormat;
			cT_Xf.applyProtection = applyProtection;
			cT_Xf.borderId = borderId;
			cT_Xf.fillId = fillId;
			cT_Xf.fontId = fontId;
			cT_Xf.numFmtId = numFmtId;
			cT_Xf.pivotButtonField = pivotButtonField;
			cT_Xf.quotePrefixField = quotePrefixField;
			cT_Xf.xfIdField = xfIdField;
			return cT_Xf;
		}

		public static CT_Xf Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Xf cT_Xf = new CT_Xf();
			cT_Xf.numFmtId = XmlHelper.ReadUInt(node.Attributes["numFmtId"]);
			cT_Xf.fontId = XmlHelper.ReadUInt(node.Attributes["fontId"]);
			cT_Xf.fillId = XmlHelper.ReadUInt(node.Attributes["fillId"]);
			cT_Xf.borderId = XmlHelper.ReadUInt(node.Attributes["borderId"]);
			cT_Xf.xfId = XmlHelper.ReadUInt(node.Attributes["xfId"]);
			cT_Xf.quotePrefix = XmlHelper.ReadBool(node.Attributes["quotePrefix"]);
			cT_Xf.pivotButton = XmlHelper.ReadBool(node.Attributes["pivotButton"]);
			cT_Xf.applyNumberFormat = XmlHelper.ReadBool(node.Attributes["applyNumberFormat"]);
			cT_Xf.applyFont = XmlHelper.ReadBool(node.Attributes["applyFont"]);
			cT_Xf.applyFill = XmlHelper.ReadBool(node.Attributes["applyFill"]);
			cT_Xf.applyBorder = XmlHelper.ReadBool(node.Attributes["applyBorder"]);
			cT_Xf.applyAlignment = XmlHelper.ReadBool(node.Attributes["applyAlignment"]);
			cT_Xf.applyProtection = XmlHelper.ReadBool(node.Attributes["applyProtection"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "alignment")
				{
					cT_Xf.alignment = CT_CellAlignment.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "protection")
				{
					cT_Xf.protection = CT_CellProtection.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_Xf.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_Xf;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "numFmtId", (double)numFmtId, true);
			XmlHelper.WriteAttribute(sw, "fontId", (double)fontId, true);
			XmlHelper.WriteAttribute(sw, "fillId", (double)fillId, true);
			XmlHelper.WriteAttribute(sw, "borderId", (double)borderId, true);
			if (applyFill)
			{
				XmlHelper.WriteAttribute(sw, "xfId", (double)xfId, true);
			}
			XmlHelper.WriteAttribute(sw, "quotePrefix", quotePrefix, false);
			XmlHelper.WriteAttribute(sw, "pivotButton", pivotButton, false);
			XmlHelper.WriteAttribute(sw, "applyNumberFormat", applyNumberFormat);
			if (applyBorder)
			{
				XmlHelper.WriteAttribute(sw, "applyBorder", applyBorder, true);
			}
			XmlHelper.WriteAttribute(sw, "applyFont", applyFont, false);
			XmlHelper.WriteAttribute(sw, "applyFill", applyFill, true);
			XmlHelper.WriteAttribute(sw, "applyAlignment", applyAlignment, true);
			XmlHelper.WriteAttribute(sw, "applyProtection", applyProtection, true);
			if (alignment == null && protection == null && extLst == null)
			{
				sw.Write("/>");
			}
			else
			{
				sw.Write(">");
				if (alignment != null)
				{
					alignment.Write(sw, "alignment");
				}
				if (protection != null)
				{
					protection.Write(sw, "protection");
				}
				if (extLst != null)
				{
					extLst.Write(sw, "extLst");
				}
				sw.Write(string.Format("</{0}>", nodeName));
			}
		}

		public bool IsSetFontId()
		{
			return fontIdSpecifiedField;
		}

		public bool IsSetAlignment()
		{
			return alignmentField != null;
		}

		public void UnsetAlignment()
		{
			alignmentField = null;
		}

		public bool IsSetExtLst()
		{
			return extLst == null;
		}

		public void UnsetExtLst()
		{
			extLst = null;
		}

		public bool IsSetProtection()
		{
			return protection != null;
		}

		public void UnsetProtection()
		{
			protection = null;
		}

		public bool IsSetLocked()
		{
			if (IsSetProtection())
			{
				return protectionField.locked;
			}
			return false;
		}

		public CT_CellProtection AddNewProtection()
		{
			protectionField = new CT_CellProtection();
			return protectionField;
		}
	}
}
