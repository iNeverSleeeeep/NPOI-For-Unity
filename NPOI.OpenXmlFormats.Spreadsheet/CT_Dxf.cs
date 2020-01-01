using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_Dxf
	{
		private CT_Font fontField;

		private CT_NumFmt numFmtField;

		private CT_Fill fillField;

		private CT_CellAlignment alignmentField;

		private CT_Border borderField;

		private CT_CellProtection protectionField;

		private CT_ExtensionList extLstField;

		[XmlElement]
		public CT_Font font
		{
			get
			{
				return fontField;
			}
			set
			{
				fontField = value;
			}
		}

		[XmlElement]
		public CT_NumFmt numFmt
		{
			get
			{
				return numFmtField;
			}
			set
			{
				numFmtField = value;
			}
		}

		[XmlElement]
		public CT_Fill fill
		{
			get
			{
				return fillField;
			}
			set
			{
				fillField = value;
			}
		}

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
		public CT_Border border
		{
			get
			{
				return borderField;
			}
			set
			{
				borderField = value;
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

		public static CT_Dxf Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Dxf cT_Dxf = new CT_Dxf();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "font")
				{
					cT_Dxf.font = CT_Font.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numFmt")
				{
					cT_Dxf.numFmt = CT_NumFmt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "fill")
				{
					cT_Dxf.fill = CT_Fill.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "alignment")
				{
					cT_Dxf.alignment = CT_CellAlignment.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "border")
				{
					cT_Dxf.border = CT_Border.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "protection")
				{
					cT_Dxf.protection = CT_CellProtection.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_Dxf.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_Dxf;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			sw.Write(">");
			if (font != null)
			{
				font.Write(sw, "font");
			}
			if (numFmt != null)
			{
				numFmt.Write(sw, "numFmt");
			}
			if (fill != null)
			{
				fill.Write(sw, "fill");
			}
			if (alignment != null)
			{
				alignment.Write(sw, "alignment");
			}
			if (border != null)
			{
				border.Write(sw, "border");
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

		public bool IsSetBorder()
		{
			return borderField != null;
		}

		public CT_Font AddNewFont()
		{
			return fontField = new CT_Font();
		}

		public CT_Fill AddNewFill()
		{
			return fillField = new CT_Fill();
		}

		public CT_Border AddNewBorder()
		{
			return borderField = new CT_Border();
		}

		public bool IsSetFont()
		{
			return fontField != null;
		}

		public bool IsSetFill()
		{
			return fillField != null;
		}
	}
}
