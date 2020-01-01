using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CT_ExternalBook
	{
		private CT_ExternalSheetName[] sheetNamesField;

		private CT_ExternalDefinedName[] definedNamesField;

		private CT_ExternalSheetData[] sheetDataSetField;

		private string idField;

		[XmlArrayItem("sheetName", IsNullable = false)]
		public CT_ExternalSheetName[] sheetNames
		{
			get
			{
				return sheetNamesField;
			}
			set
			{
				sheetNamesField = value;
			}
		}

		[XmlArrayItem("definedName", IsNullable = false)]
		public CT_ExternalDefinedName[] definedNames
		{
			get
			{
				return definedNamesField;
			}
			set
			{
				definedNamesField = value;
			}
		}

		[XmlArrayItem("sheetData", IsNullable = false)]
		public CT_ExternalSheetData[] sheetDataSet
		{
			get
			{
				return sheetDataSetField;
			}
			set
			{
				sheetDataSetField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships")]
		public string id
		{
			get
			{
				return idField;
			}
			set
			{
				idField = value;
			}
		}
	}
}
