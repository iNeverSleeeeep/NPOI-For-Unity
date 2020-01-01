using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[DebuggerStepThrough]
	public class CT_ExternalDefinedName
	{
		private string nameField;

		private string refersToField;

		private uint sheetIdField;

		private bool sheetIdFieldSpecified;

		[XmlAttribute]
		public string name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		[XmlAttribute]
		public string refersTo
		{
			get
			{
				return refersToField;
			}
			set
			{
				refersToField = value;
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

		[XmlIgnore]
		public bool sheetIdSpecified
		{
			get
			{
				return sheetIdFieldSpecified;
			}
			set
			{
				sheetIdFieldSpecified = value;
			}
		}
	}
}
