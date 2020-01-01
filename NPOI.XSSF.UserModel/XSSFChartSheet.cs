using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.Util;
using System.IO;
using System.Xml;

namespace NPOI.XSSF.UserModel
{
	/// High level representation of Sheet Parts that are of type 'chartsheet'.
	/// <p>
	///  Chart sheet is a special kind of Sheet that Contains only chart and no data.
	/// </p>
	///
	/// @author Yegor Kozlov
	public class XSSFChartSheet : XSSFSheet
	{
		private static byte[] BLANK_WORKSHEET = blankWorksheet();

		protected CT_Chartsheet chartsheet;

		protected XSSFChartSheet(PackagePart part, PackageRelationship rel)
			: base(part, rel)
		{
		}

		internal override void Read(Stream is1)
		{
			base.Read(new MemoryStream(BLANK_WORKSHEET));
			try
			{
				XmlDocument xmldoc = POIXMLDocumentPart.ConvertStreamToXml(is1);
				chartsheet = ChartsheetDocument.Parse(xmldoc, POIXMLDocumentPart.NamespaceManager).GetChartsheet();
			}
			catch (XmlException ex)
			{
				throw new POIXMLException(ex);
			}
		}

		/// Provide access to the CTChartsheet bean holding this sheet's data
		///
		/// @return the CTChartsheet bean holding this sheet's data
		public CT_Chartsheet GetCTChartsheet()
		{
			return chartsheet;
		}

		protected override CT_Drawing GetCTDrawing()
		{
			return chartsheet.drawing;
		}

		protected override CT_LegacyDrawing GetCTLegacyDrawing()
		{
			return chartsheet.legacyDrawing;
		}

		internal override void Write(Stream out1)
		{
			new ChartsheetDocument(chartsheet).Save(out1);
		}

		private static byte[] blankWorksheet()
		{
			MemoryStream memoryStream = new MemoryStream();
			try
			{
				new XSSFSheet().Write(memoryStream);
			}
			catch (IOException e)
			{
				throw new RuntimeException(e);
			}
			return memoryStream.ToArray();
		}
	}
}
