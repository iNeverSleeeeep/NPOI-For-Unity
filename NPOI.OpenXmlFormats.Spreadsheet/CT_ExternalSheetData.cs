using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	public class CT_ExternalSheetData
	{
		private CT_ExternalRow[] rowField;

		private uint sheetIdField;

		private bool refreshErrorField;

		[XmlElement("row")]
		public CT_ExternalRow[] row
		{
			get
			{
				return rowField;
			}
			set
			{
				rowField = value;
			}
		}

		[XmlAttribute]
		public uint sheetId
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

		[DefaultValue(false)]
		[XmlAttribute]
		public bool refreshError
		{
			get
			{
				return refreshErrorField;
			}
			set
			{
				refreshErrorField = value;
			}
		}

		public CT_ExternalSheetData()
		{
			refreshErrorField = false;
		}
	}
}
