using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[XmlRoot(ElementName = "styleSheet", Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = false)]
	[DesignerCategory("code")]
	public class CT_Stylesheet
	{
		private CT_NumFmts numFmtsField;

		private CT_Fonts fontsField;

		private CT_Fills fillsField;

		private CT_Borders bordersField;

		private CT_CellStyleXfs cellStyleXfsField;

		private CT_CellXfs cellXfsField;

		private CT_CellStyles cellStylesField;

		private CT_Dxfs dxfsField;

		private CT_TableStyles tableStylesField;

		private CT_Colors colorsField;

		private CT_ExtensionList extLstField;

		[XmlElement]
		public CT_NumFmts numFmts
		{
			get
			{
				return numFmtsField;
			}
			set
			{
				numFmtsField = value;
			}
		}

		[XmlElement]
		public CT_Fonts fonts
		{
			get
			{
				return fontsField;
			}
			set
			{
				fontsField = value;
			}
		}

		[XmlElement]
		public CT_Fills fills
		{
			get
			{
				return fillsField;
			}
			set
			{
				fillsField = value;
			}
		}

		[XmlElement]
		public CT_Borders borders
		{
			get
			{
				return bordersField;
			}
			set
			{
				bordersField = value;
			}
		}

		[XmlElement]
		public CT_CellStyleXfs cellStyleXfs
		{
			get
			{
				return cellStyleXfsField;
			}
			set
			{
				cellStyleXfsField = value;
			}
		}

		[XmlElement]
		public CT_CellXfs cellXfs
		{
			get
			{
				return cellXfsField;
			}
			set
			{
				cellXfsField = value;
			}
		}

		[XmlElement]
		public CT_CellStyles cellStyles
		{
			get
			{
				return cellStylesField;
			}
			set
			{
				cellStylesField = value;
			}
		}

		[XmlElement]
		public CT_Dxfs dxfs
		{
			get
			{
				return dxfsField;
			}
			set
			{
				dxfsField = value;
			}
		}

		[XmlElement]
		public CT_TableStyles tableStyles
		{
			get
			{
				return tableStylesField;
			}
			set
			{
				tableStylesField = value;
			}
		}

		[XmlElement]
		public CT_Colors colors
		{
			get
			{
				return colorsField;
			}
			set
			{
				colorsField = value;
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

		public static CT_Stylesheet Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Stylesheet cT_Stylesheet = new CT_Stylesheet();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "numFmts")
				{
					cT_Stylesheet.numFmts = CT_NumFmts.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "fonts")
				{
					cT_Stylesheet.fonts = CT_Fonts.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "fills")
				{
					cT_Stylesheet.fills = CT_Fills.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "borders")
				{
					cT_Stylesheet.borders = CT_Borders.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cellStyleXfs")
				{
					cT_Stylesheet.cellStyleXfs = CT_CellStyleXfs.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cellXfs")
				{
					cT_Stylesheet.cellXfs = CT_CellXfs.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cellStyles")
				{
					cT_Stylesheet.cellStyles = CT_CellStyles.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dxfs")
				{
					cT_Stylesheet.dxfs = CT_Dxfs.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tableStyles")
				{
					cT_Stylesheet.tableStyles = CT_TableStyles.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "colors")
				{
					cT_Stylesheet.colors = CT_Colors.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_Stylesheet.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_Stylesheet;
		}

		internal void Write(StreamWriter sw)
		{
			sw.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
			sw.Write("<styleSheet xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\">");
			if (numFmts != null)
			{
				numFmts.Write(sw, "numFmts");
			}
			if (fonts != null)
			{
				fonts.Write(sw, "fonts");
			}
			if (fills != null)
			{
				fills.Write(sw, "fills");
			}
			if (borders != null)
			{
				borders.Write(sw, "borders");
			}
			if (cellStyleXfs != null)
			{
				cellStyleXfs.Write(sw, "cellStyleXfs");
			}
			if (cellXfs != null)
			{
				cellXfs.Write(sw, "cellXfs");
			}
			if (cellStyles != null)
			{
				cellStyles.Write(sw, "cellStyles");
			}
			if (dxfs != null)
			{
				dxfs.Write(sw, "dxfs");
			}
			if (tableStyles != null)
			{
				tableStyles.Write(sw, "tableStyles");
			}
			if (colors != null)
			{
				colors.Write(sw, "colors");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write("</styleSheet>");
		}

		public CT_Borders AddNewBorders()
		{
			bordersField = new CT_Borders();
			return bordersField;
		}

		public CT_CellStyleXfs AddNewCellStyleXfs()
		{
			cellStyleXfsField = new CT_CellStyleXfs();
			return cellStyleXfsField;
		}

		public CT_CellXfs AddNewCellXfs()
		{
			cellXfsField = new CT_CellXfs();
			return cellXfsField;
		}
	}
}
