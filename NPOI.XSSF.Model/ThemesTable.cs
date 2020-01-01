using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats.Dml;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace NPOI.XSSF.Model
{
	/// Class that represents theme of XLSX document. The theme includes specific
	/// colors and fonts.
	///
	/// @author Petr Udalau(Petr.Udalau at exigenservices.com) - theme colors
	public class ThemesTable : POIXMLDocumentPart
	{
		private ThemeDocument theme;

		internal ThemesTable(PackagePart part, PackageRelationship rel)
			: base(part, rel)
		{
			XmlDocument xmldoc = POIXMLDocumentPart.ConvertStreamToXml(part.GetInputStream());
			try
			{
				theme = ThemeDocument.Parse(xmldoc, POIXMLDocumentPart.NamespaceManager);
			}
			catch (XmlException ex)
			{
				throw new IOException(ex.Message);
			}
		}

		internal ThemesTable(ThemeDocument theme)
		{
			this.theme = theme;
		}

		public XSSFColor GetThemeColor(int idx)
		{
			CT_ColorScheme clrScheme = theme.GetTheme().themeElements.clrScheme;
			NPOI.OpenXmlFormats.Dml.CT_Color cT_Color = null;
			int num = 0;
			List<NPOI.OpenXmlFormats.Dml.CT_Color> list = new List<NPOI.OpenXmlFormats.Dml.CT_Color>();
			list.Add(clrScheme.dk1);
			list.Add(clrScheme.lt1);
			list.Add(clrScheme.dk2);
			list.Add(clrScheme.lt2);
			list.Add(clrScheme.accent1);
			list.Add(clrScheme.accent2);
			list.Add(clrScheme.accent3);
			list.Add(clrScheme.accent4);
			list.Add(clrScheme.accent5);
			list.Add(clrScheme.accent6);
			list.Add(clrScheme.hlink);
			list.Add(clrScheme.folHlink);
			foreach (NPOI.OpenXmlFormats.Dml.CT_Color item in list)
			{
				if (num == idx)
				{
					cT_Color = item;
					byte[] rgb = null;
					if (cT_Color.srgbClr != null)
					{
						rgb = cT_Color.srgbClr.val;
					}
					else if (cT_Color.sysClr != null)
					{
						rgb = cT_Color.sysClr.lastClr;
					}
					return new XSSFColor(rgb);
				}
				num++;
			}
			return null;
		}

		/// If the colour is based on a theme, then inherit 
		///  information (currently just colours) from it as
		///  required.
		public void InheritFromThemeAsRequired(XSSFColor color)
		{
			if (color != null && color.GetCTColor().themeSpecified)
			{
				XSSFColor themeColor = GetThemeColor(color.Theme);
				color.GetCTColor().SetRgb(themeColor.GetCTColor().GetRgb());
			}
		}
	}
}
