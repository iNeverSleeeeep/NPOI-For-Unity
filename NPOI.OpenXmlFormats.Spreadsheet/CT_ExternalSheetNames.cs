using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_ExternalSheetNames
	{
		private CT_ExternalSheetName[] sheetNameField;

		[XmlElement("sheetName")]
		public CT_ExternalSheetName[] sheetName
		{
			get
			{
				return sheetNameField;
			}
			set
			{
				sheetNameField = value;
			}
		}
	}
}
