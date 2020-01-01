using NPOI.OpenXmlFormats.Spreadsheet;

namespace NPOI.XSSF.UserModel.Helpers
{
	/// This class is a wrapper around the CT_XmlColumnPr (Open Office XML Part 4:
	/// chapter 3.5.1.7)
	///
	///
	/// @author Roberto Manicardi
	public class XSSFXmlColumnPr
	{
		private XSSFTable table;

		private CT_TableColumn ctTableColumn;

		private CT_XmlColumnPr ctXmlColumnPr;

		public XSSFXmlColumnPr(XSSFTable table, CT_TableColumn ctTableColum, CT_XmlColumnPr CT_XmlColumnPr)
		{
			this.table = table;
			ctTableColumn = ctTableColum;
			ctXmlColumnPr = CT_XmlColumnPr;
		}

		public long GetMapId()
		{
			return ctXmlColumnPr.mapId;
		}

		public string GetXPath()
		{
			return ctXmlColumnPr.xpath;
		}

		/// (see Open Office XML Part 4: chapter 3.5.1.3)
		/// @return An integer representing the unique identifier of this column. 
		public long GetId()
		{
			return ctTableColumn.id;
		}

		/// If the XPath is, for example, /Node1/Node2/Node3 and /Node1/Node2 is the common XPath for the table, the local XPath is /Node3
		///
		/// @return the local XPath 
		public string GetLocalXPath()
		{
			string text = "";
			int num = table.GetCommonXpath().Split('/').Length - 1;
			string[] array = ctXmlColumnPr.xpath.Split('/');
			for (int i = num; i < array.Length; i++)
			{
				text = text + "/" + array[i];
			}
			return text;
		}

		public ST_XmlDataType GetXmlDataType()
		{
			return ctXmlColumnPr.xmlDataType;
		}
	}
}
