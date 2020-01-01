using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_ExternalSheetDataSet
	{
		private CT_ExternalSheetData[] sheetDataField;

		[XmlElement("sheetData")]
		public CT_ExternalSheetData[] sheetData
		{
			get
			{
				return sheetDataField;
			}
			set
			{
				sheetDataField = value;
			}
		}
	}
}
