using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CT_Map
	{
		private CT_DataBinding dataBindingField;

		private uint idField;

		private string nameField;

		private string rootElementField;

		private string schemaIDField;

		private bool showImportExportValidationErrorsField;

		private bool autoFitField;

		private bool appendField;

		private bool preserveSortAFLayoutField;

		private bool preserveFormatField;

		[XmlElement]
		public CT_DataBinding DataBinding
		{
			get
			{
				return dataBindingField;
			}
			set
			{
				dataBindingField = value;
			}
		}

		[XmlIgnore]
		public bool DataBindingSpecified
		{
			get
			{
				return null != dataBindingField;
			}
		}

		[XmlAttribute]
		public uint ID
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

		[XmlAttribute]
		public string Name
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
		public string RootElement
		{
			get
			{
				return rootElementField;
			}
			set
			{
				rootElementField = value;
			}
		}

		[XmlAttribute]
		public string SchemaID
		{
			get
			{
				return schemaIDField;
			}
			set
			{
				schemaIDField = value;
			}
		}

		[XmlAttribute]
		public bool ShowImportExportValidationErrors
		{
			get
			{
				return showImportExportValidationErrorsField;
			}
			set
			{
				showImportExportValidationErrorsField = value;
			}
		}

		[XmlAttribute]
		public bool AutoFit
		{
			get
			{
				return autoFitField;
			}
			set
			{
				autoFitField = value;
			}
		}

		[XmlAttribute]
		public bool Append
		{
			get
			{
				return appendField;
			}
			set
			{
				appendField = value;
			}
		}

		[XmlAttribute]
		public bool PreserveSortAFLayout
		{
			get
			{
				return preserveSortAFLayoutField;
			}
			set
			{
				preserveSortAFLayoutField = value;
			}
		}

		[XmlAttribute]
		public bool PreserveFormat
		{
			get
			{
				return preserveFormatField;
			}
			set
			{
				preserveFormatField = value;
			}
		}
	}
}
