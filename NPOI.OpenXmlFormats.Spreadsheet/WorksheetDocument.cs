using System.IO;
using System.Xml;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class WorksheetDocument
	{
		private CT_Worksheet sheet;

		public WorksheetDocument()
		{
		}

		public WorksheetDocument(CT_Worksheet sheet)
		{
			this.sheet = sheet;
		}

		public static WorksheetDocument Parse(XmlDocument xmldoc, XmlNamespaceManager namespaceMgr)
		{
			CT_Worksheet cT_Worksheet = CT_Worksheet.Parse(xmldoc.DocumentElement, namespaceMgr);
			return new WorksheetDocument(cT_Worksheet);
		}

		public CT_Worksheet GetWorksheet()
		{
			return sheet;
		}

		public void SetChartsheet(CT_Worksheet sheet)
		{
			this.sheet = sheet;
		}

		public void Save(Stream stream)
		{
			sheet.Write(stream);
		}
	}
}
