using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_SheetFormatPr
	{
		private uint baseColWidthField;

		private double defaultColWidthField;

		private double defaultRowHeightField;

		private bool customHeightField;

		private bool zeroHeightField;

		private bool thickTopField;

		private bool thickBottomField;

		private byte outlineLevelRowField;

		private byte outlineLevelColField;

		[XmlAttribute]
		[DefaultValue(typeof(uint), "8")]
		public uint baseColWidth
		{
			get
			{
				return baseColWidthField;
			}
			set
			{
				baseColWidthField = value;
			}
		}

		[XmlAttribute]
		public double defaultColWidth
		{
			get
			{
				return defaultColWidthField;
			}
			set
			{
				defaultColWidthField = value;
			}
		}

		[XmlAttribute]
		public double defaultRowHeight
		{
			get
			{
				return defaultRowHeightField;
			}
			set
			{
				defaultRowHeightField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool customHeight
		{
			get
			{
				return customHeightField;
			}
			set
			{
				customHeightField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool zeroHeight
		{
			get
			{
				return zeroHeightField;
			}
			set
			{
				zeroHeightField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool thickTop
		{
			get
			{
				return thickTopField;
			}
			set
			{
				thickTopField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool thickBottom
		{
			get
			{
				return thickBottomField;
			}
			set
			{
				thickBottomField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(byte), "0")]
		public byte outlineLevelRow
		{
			get
			{
				return outlineLevelRowField;
			}
			set
			{
				outlineLevelRowField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(byte), "0")]
		public byte outlineLevelCol
		{
			get
			{
				return outlineLevelColField;
			}
			set
			{
				outlineLevelColField = value;
			}
		}

		public static CT_SheetFormatPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SheetFormatPr cT_SheetFormatPr = new CT_SheetFormatPr();
			cT_SheetFormatPr.baseColWidth = XmlHelper.ReadUInt(node.Attributes["baseColWidth"]);
			cT_SheetFormatPr.defaultColWidth = XmlHelper.ReadDouble(node.Attributes["defaultColWidth"]);
			cT_SheetFormatPr.defaultRowHeight = XmlHelper.ReadDouble(node.Attributes["defaultRowHeight"]);
			cT_SheetFormatPr.customHeight = XmlHelper.ReadBool(node.Attributes["customHeight"]);
			cT_SheetFormatPr.zeroHeight = XmlHelper.ReadBool(node.Attributes["zeroHeight"]);
			cT_SheetFormatPr.thickTop = XmlHelper.ReadBool(node.Attributes["thickTop"]);
			cT_SheetFormatPr.outlineLevelRow = XmlHelper.ReadByte(node.Attributes["outlineLevelRow"]);
			cT_SheetFormatPr.outlineLevelCol = XmlHelper.ReadByte(node.Attributes["outlineLevelCol"]);
			cT_SheetFormatPr.thickBottom = XmlHelper.ReadBool(node.Attributes["thickBottom"]);
			return cT_SheetFormatPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "baseColWidth", baseColWidth);
			XmlHelper.WriteAttribute(sw, "defaultColWidth", defaultColWidth);
			XmlHelper.WriteAttribute(sw, "defaultRowHeight", defaultRowHeight);
			XmlHelper.WriteAttribute(sw, "customHeight", customHeight, false);
			XmlHelper.WriteAttribute(sw, "zeroHeight", zeroHeight, false);
			XmlHelper.WriteAttribute(sw, "thickTop", thickTop, false);
			XmlHelper.WriteAttribute(sw, "thickBottom", thickBottom, false);
			XmlHelper.WriteAttribute(sw, "outlineLevelRow", outlineLevelRow);
			XmlHelper.WriteAttribute(sw, "outlineLevelCol", outlineLevelCol);
			sw.Write("/>");
		}
	}
}
