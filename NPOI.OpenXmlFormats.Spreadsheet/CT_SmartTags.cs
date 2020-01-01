using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_SmartTags
	{
		private List<CT_CellSmartTags> cellSmartTagsField;

		public List<CT_CellSmartTags> cellSmartTags
		{
			get
			{
				return cellSmartTagsField;
			}
			set
			{
				cellSmartTagsField = value;
			}
		}

		public CT_SmartTags()
		{
			cellSmartTagsField = new List<CT_CellSmartTags>();
		}
	}
}
