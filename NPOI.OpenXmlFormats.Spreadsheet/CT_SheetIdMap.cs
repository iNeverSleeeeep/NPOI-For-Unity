using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_SheetIdMap
	{
		private List<CT_SheetId> sheetIdField;

		private uint countField;

		private bool countFieldSpecified;

		public List<CT_SheetId> sheetId
		{
			get
			{
				return sheetIdField;
			}
			set
			{
				sheetIdField = value;
			}
		}

		public uint count
		{
			get
			{
				return countField;
			}
			set
			{
				countField = value;
			}
		}

		[XmlIgnore]
		public bool countSpecified
		{
			get
			{
				return countFieldSpecified;
			}
			set
			{
				countFieldSpecified = value;
			}
		}

		public CT_SheetIdMap()
		{
			sheetIdField = new List<CT_SheetId>();
		}
	}
}
