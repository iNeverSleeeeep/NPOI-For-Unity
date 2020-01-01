using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.XSSF.UserModel.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;

namespace NPOI.XSSF.Model
{
	/// Table of styles shared across all sheets in a workbook.
	///
	/// @author ugo
	public class StylesTable : POIXMLDocumentPart
	{
		private Dictionary<int, string> numberFormats = new Dictionary<int, string>();

		private List<XSSFFont> fonts = new List<XSSFFont>();

		private List<XSSFCellFill> fills = new List<XSSFCellFill>();

		private List<XSSFCellBorder> borders = new List<XSSFCellBorder>();

		private List<CT_Xf> styleXfs = new List<CT_Xf>();

		private List<CT_Xf> xfs = new List<CT_Xf>();

		private List<CT_Dxf> dxfs = new List<CT_Dxf>();

		/// The first style id available for use as a custom style
		public static int FIRST_CUSTOM_STYLE_ID = 165;

		private StyleSheetDocument doc;

		private ThemesTable theme;

		/// get the size of cell styles
		public int NumCellStyles
		{
			get
			{
				return xfs.Count;
			}
		}

		/// For unit testing only
		internal int NumberFormatSize
		{
			get
			{
				return numberFormats.Count;
			}
		}

		/// For unit testing only
		internal int XfsSize
		{
			get
			{
				return xfs.Count;
			}
		}

		/// For unit testing only
		internal int StyleXfsSize
		{
			get
			{
				return styleXfs.Count;
			}
		}

		internal int DXfsSize
		{
			get
			{
				return dxfs.Count;
			}
		}

		/// Create a new, empty StylesTable
		public StylesTable()
		{
			doc = new StyleSheetDocument();
			doc.AddNewStyleSheet();
			Initialize();
		}

		internal StylesTable(PackagePart part, PackageRelationship rel)
			: base(part, rel)
		{
			XmlDocument xmldoc = POIXMLDocumentPart.ConvertStreamToXml(part.GetInputStream());
			ReadFrom(xmldoc);
		}

		public ThemesTable GetTheme()
		{
			return theme;
		}

		public void SetTheme(ThemesTable theme)
		{
			this.theme = theme;
			foreach (XSSFFont font in fonts)
			{
				font.SetThemesTable(theme);
			}
			foreach (XSSFCellBorder border in borders)
			{
				border.SetThemesTable(theme);
			}
		}

		/// Read this shared styles table from an XML file.
		///
		/// @param is The input stream Containing the XML document.
		/// @throws IOException if an error occurs while Reading.
		protected void ReadFrom(XmlDocument xmldoc)
		{
			try
			{
				doc = StyleSheetDocument.Parse(xmldoc, POIXMLDocumentPart.NamespaceManager);
				CT_Stylesheet styleSheet = doc.GetStyleSheet();
				CT_NumFmts numFmts = styleSheet.numFmts;
				if (numFmts != null)
				{
					foreach (CT_NumFmt item2 in numFmts.numFmt)
					{
						numberFormats.Add((int)item2.numFmtId, item2.formatCode);
					}
				}
				CT_Fonts cT_Fonts = styleSheet.fonts;
				if (cT_Fonts != null)
				{
					int num = 0;
					foreach (CT_Font item3 in cT_Fonts.font)
					{
						XSSFFont item = new XSSFFont(item3, num);
						fonts.Add(item);
						num++;
					}
				}
				CT_Fills cT_Fills = styleSheet.fills;
				if (cT_Fills != null)
				{
					foreach (CT_Fill item4 in cT_Fills.fill)
					{
						fills.Add(new XSSFCellFill(item4));
					}
				}
				CT_Borders cT_Borders = styleSheet.borders;
				if (cT_Borders != null)
				{
					foreach (CT_Border item5 in cT_Borders.border)
					{
						borders.Add(new XSSFCellBorder(item5));
					}
				}
				CT_CellXfs cellXfs = styleSheet.cellXfs;
				if (cellXfs != null)
				{
					xfs.AddRange(cellXfs.xf);
				}
				CT_CellStyleXfs cellStyleXfs = styleSheet.cellStyleXfs;
				if (cellStyleXfs != null)
				{
					styleXfs.AddRange(cellStyleXfs.xf);
				}
				CT_Dxfs cT_Dxfs = styleSheet.dxfs;
				if (cT_Dxfs != null)
				{
					dxfs.AddRange(cT_Dxfs.dxf);
				}
			}
			catch (XmlException ex)
			{
				throw new IOException(ex.Message);
			}
		}

		public string GetNumberFormatAt(int idx)
		{
			if (numberFormats.ContainsKey(idx))
			{
				return numberFormats[idx];
			}
			return null;
		}

		public int PutNumberFormat(string fmt)
		{
			if (numberFormats.ContainsValue(fmt))
			{
				foreach (int key in numberFormats.Keys)
				{
					if (numberFormats[key].Equals(fmt))
					{
						return key;
					}
				}
				throw new InvalidOperationException("Found the format, but couldn't figure out where - should never happen!");
			}
			int i;
			for (i = FIRST_CUSTOM_STYLE_ID; numberFormats.ContainsKey(i); i++)
			{
			}
			numberFormats[i] = fmt;
			return i;
		}

		public XSSFFont GetFontAt(int idx)
		{
			return fonts[idx];
		}

		/// Records the given font in the font table.
		/// Will re-use an existing font index if this
		///  font matches another, EXCEPT if forced
		///  registration is requested.
		/// This allows people to create several fonts
		///  then customise them later.
		/// Note - End Users probably want to call
		///  {@link XSSFFont#registerTo(StylesTable)}
		public int PutFont(XSSFFont font, bool forceRegistration)
		{
			int num = -1;
			if (!forceRegistration)
			{
				num = fonts.IndexOf(font);
			}
			if (num != -1)
			{
				return num;
			}
			num = fonts.Count;
			fonts.Add(font);
			return num;
		}

		public int PutFont(XSSFFont font)
		{
			return PutFont(font, false);
		}

		public XSSFCellStyle GetStyleAt(int idx)
		{
			int cellStyleXfId = 0;
			if (xfs[idx].xfId != 0)
			{
				cellStyleXfId = (int)xfs[idx].xfId;
			}
			return new XSSFCellStyle(idx, cellStyleXfId, this, theme);
		}

		public int PutStyle(XSSFCellStyle style)
		{
			CT_Xf coreXf = style.GetCoreXf();
			if (!xfs.Contains(coreXf))
			{
				xfs.Add(coreXf);
			}
			return xfs.IndexOf(coreXf);
		}

		public XSSFCellBorder GetBorderAt(int idx)
		{
			return borders[idx];
		}

		public int PutBorder(XSSFCellBorder border)
		{
			int num = borders.IndexOf(border);
			if (num != -1)
			{
				return num;
			}
			borders.Add(border);
			border.SetThemesTable(theme);
			return borders.Count - 1;
		}

		public XSSFCellFill GetFillAt(int idx)
		{
			return fills[idx];
		}

		public List<XSSFCellBorder> GetBorders()
		{
			return borders;
		}

		public ReadOnlyCollection<XSSFCellFill> GetFills()
		{
			return fills.AsReadOnly();
		}

		public List<XSSFFont> GetFonts()
		{
			return fonts;
		}

		public Dictionary<int, string> GetNumberFormats()
		{
			return numberFormats;
		}

		public int PutFill(XSSFCellFill fill)
		{
			int num = fills.IndexOf(fill);
			if (num != -1)
			{
				return num;
			}
			fills.Add(fill);
			return fills.Count - 1;
		}

		internal CT_Xf GetCellXfAt(int idx)
		{
			return xfs[idx];
		}

		internal int PutCellXf(CT_Xf cellXf)
		{
			xfs.Add(cellXf);
			return xfs.Count;
		}

		internal void ReplaceCellXfAt(int idx, CT_Xf cellXf)
		{
			xfs[idx] = cellXf;
		}

		internal CT_Xf GetCellStyleXfAt(int idx)
		{
			return styleXfs[idx];
		}

		internal int PutCellStyleXf(CT_Xf cellStyleXf)
		{
			styleXfs.Add(cellStyleXf);
			return styleXfs.Count;
		}

		internal void ReplaceCellStyleXfAt(int idx, CT_Xf cellStyleXf)
		{
			styleXfs[idx] = cellStyleXf;
		}

		/// For unit testing only!
		internal CT_Stylesheet GetCTStylesheet()
		{
			return doc.GetStyleSheet();
		}

		/// Write this table out as XML.
		///
		/// @param out The stream to write to.
		/// @throws IOException if an error occurs while writing.
		public void WriteTo(Stream out1)
		{
			CT_Stylesheet styleSheet = doc.GetStyleSheet();
			CT_NumFmts cT_NumFmts = new CT_NumFmts();
			cT_NumFmts.count = (uint)numberFormats.Count;
			if (cT_NumFmts.count != 0)
			{
				cT_NumFmts.countSpecified = true;
			}
			foreach (KeyValuePair<int, string> numberFormat in numberFormats)
			{
				CT_NumFmt cT_NumFmt = cT_NumFmts.AddNewNumFmt();
				cT_NumFmt.numFmtId = (uint)numberFormat.Key;
				cT_NumFmt.formatCode = numberFormat.Value;
			}
			if (cT_NumFmts.count != 0)
			{
				styleSheet.numFmts = cT_NumFmts;
			}
			CT_Fonts cT_Fonts = new CT_Fonts();
			cT_Fonts.count = (uint)fonts.Count;
			if (cT_Fonts.count != 0)
			{
				cT_Fonts.countSpecified = true;
			}
			List<CT_Font> list = new List<CT_Font>(fonts.Count);
			foreach (XSSFFont font in fonts)
			{
				list.Add(font.GetCTFont());
			}
			cT_Fonts.SetFontArray(list);
			styleSheet.fonts = cT_Fonts;
			List<CT_Fill> list2 = new List<CT_Fill>(fills.Count);
			foreach (XSSFCellFill fill in fills)
			{
				list2.Add(fill.GetCTFill());
			}
			CT_Fills cT_Fills = new CT_Fills();
			cT_Fills.SetFillArray(list2);
			cT_Fills.count = (uint)fills.Count;
			if (cT_Fills.count != 0)
			{
				cT_Fills.countSpecified = true;
			}
			styleSheet.fills = cT_Fills;
			List<CT_Border> list3 = new List<CT_Border>(borders.Count);
			foreach (XSSFCellBorder border in borders)
			{
				list3.Add(border.GetCTBorder());
			}
			CT_Borders cT_Borders = new CT_Borders();
			cT_Borders.SetBorderArray(list3);
			cT_Borders.count = (uint)list3.Count;
			styleSheet.borders = cT_Borders;
			if (xfs.Count > 0)
			{
				CT_CellXfs cT_CellXfs = new CT_CellXfs();
				cT_CellXfs.count = (uint)xfs.Count;
				if (cT_CellXfs.count != 0)
				{
					cT_CellXfs.countSpecified = true;
				}
				cT_CellXfs.xf = xfs;
				styleSheet.cellXfs = cT_CellXfs;
			}
			if (styleXfs.Count > 0)
			{
				CT_CellStyleXfs cT_CellStyleXfs = new CT_CellStyleXfs();
				cT_CellStyleXfs.count = (uint)styleXfs.Count;
				if (cT_CellStyleXfs.count != 0)
				{
					cT_CellStyleXfs.countSpecified = true;
				}
				cT_CellStyleXfs.xf = styleXfs;
				styleSheet.cellStyleXfs = cT_CellStyleXfs;
			}
			if (dxfs.Count > 0)
			{
				CT_Dxfs cT_Dxfs = new CT_Dxfs();
				cT_Dxfs.count = (uint)dxfs.Count;
				if (cT_Dxfs.count != 0)
				{
					cT_Dxfs.countSpecified = true;
				}
				cT_Dxfs.dxf = dxfs;
				styleSheet.dxfs = cT_Dxfs;
			}
			doc.Save(out1);
		}

		protected override void Commit()
		{
			PackagePart packagePart = GetPackagePart();
			Stream outputStream = packagePart.GetOutputStream();
			WriteTo(outputStream);
			outputStream.Close();
		}

		private void Initialize()
		{
			XSSFFont item = CreateDefaultFont();
			fonts.Add(item);
			CT_Fill[] array = CreateDefaultFills();
			fills.Add(new XSSFCellFill(array[0]));
			fills.Add(new XSSFCellFill(array[1]));
			CT_Border border = CreateDefaultBorder();
			borders.Add(new XSSFCellBorder(border));
			CT_Xf item2 = CreateDefaultXf();
			styleXfs.Add(item2);
			CT_Xf cT_Xf = CreateDefaultXf();
			cT_Xf.xfId = 0u;
			xfs.Add(cT_Xf);
		}

		private static CT_Xf CreateDefaultXf()
		{
			CT_Xf cT_Xf = new CT_Xf();
			cT_Xf.numFmtId = 0u;
			cT_Xf.fontId = 0u;
			cT_Xf.fillId = 0u;
			cT_Xf.borderId = 0u;
			return cT_Xf;
		}

		private static CT_Border CreateDefaultBorder()
		{
			CT_Border cT_Border = new CT_Border();
			cT_Border.AddNewLeft();
			cT_Border.AddNewRight();
			cT_Border.AddNewTop();
			cT_Border.AddNewBottom();
			cT_Border.AddNewDiagonal();
			return cT_Border;
		}

		private static CT_Fill[] CreateDefaultFills()
		{
			CT_Fill[] array = new CT_Fill[2]
			{
				new CT_Fill(),
				new CT_Fill()
			};
			array[0].AddNewPatternFill().patternType = ST_PatternType.none;
			array[1].AddNewPatternFill().patternType = ST_PatternType.darkGray;
			return array;
		}

		private static XSSFFont CreateDefaultFont()
		{
			CT_Font font = new CT_Font();
			XSSFFont xSSFFont = new XSSFFont(font, 0);
			xSSFFont.FontHeightInPoints = 11;
			xSSFFont.Color = XSSFFont.DEFAULT_FONT_COLOR;
			xSSFFont.FontName = "Calibri";
			xSSFFont.SetFamily(FontFamily.SWISS);
			xSSFFont.SetScheme(FontScheme.MINOR);
			return xSSFFont;
		}

		public CT_Dxf GetDxfAt(int idx)
		{
			return dxfs[idx];
		}

		public int PutDxf(CT_Dxf dxf)
		{
			dxfs.Add(dxf);
			return dxfs.Count;
		}

		public XSSFCellStyle CreateCellStyle()
		{
			CT_Xf cT_Xf = new CT_Xf();
			cT_Xf.numFmtId = 0u;
			cT_Xf.fontId = 0u;
			cT_Xf.fillId = 0u;
			cT_Xf.borderId = 0u;
			cT_Xf.xfId = 0u;
			int count = styleXfs.Count;
			int num = PutCellXf(cT_Xf);
			return new XSSFCellStyle(num - 1, count - 1, this, theme);
		}

		/// Finds a font that matches the one with the supplied attributes
		public XSSFFont FindFont(short boldWeight, short color, short fontHeight, string name, bool italic, bool strikeout, FontSuperScript typeOffset, FontUnderlineType underline)
		{
			foreach (XSSFFont font in fonts)
			{
				if (font.Boldweight == boldWeight && font.Color == color && font.FontHeight == (double)fontHeight && font.FontName.Equals(name) && font.IsItalic == italic && font.IsStrikeout == strikeout && font.TypeOffset == typeOffset && font.Underline == underline)
				{
					return font;
				}
			}
			return null;
		}
	}
}
