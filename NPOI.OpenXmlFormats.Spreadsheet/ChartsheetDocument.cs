using System.IO;
using System.Xml;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class ChartsheetDocument
	{
		private CT_Chartsheet sheet;

		public ChartsheetDocument()
		{
		}

		public ChartsheetDocument(CT_Chartsheet sheet)
		{
			this.sheet = sheet;
		}

		public static ChartsheetDocument Parse(XmlDocument xmldoc, XmlNamespaceManager nsmgr)
		{
			CT_Chartsheet cT_Chartsheet = CT_Chartsheet.Parse(xmldoc, nsmgr);
			return new ChartsheetDocument(cT_Chartsheet);
		}

		public CT_Chartsheet GetChartsheet()
		{
			return sheet;
		}

		public void SetChartsheet(CT_Chartsheet sheet)
		{
			this.sheet = sheet;
		}

		public void Save(Stream stream)
		{
			sheet.Write(stream);
		}
	}
}
