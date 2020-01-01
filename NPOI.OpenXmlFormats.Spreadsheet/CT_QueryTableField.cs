using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	public class CT_QueryTableField
	{
		private CT_ExtensionList extLstField;

		private uint idField;

		private string nameField;

		private bool dataBoundField;

		private bool rowNumbersField;

		private bool fillFormulasField;

		private bool clippedField;

		private uint tableColumnIdField;

		public CT_ExtensionList extLst
		{
			get
			{
				return extLstField;
			}
			set
			{
				extLstField = value;
			}
		}

		[XmlAttribute]
		public uint id
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

		[DefaultValue(true)]
		[XmlAttribute]
		public bool dataBound
		{
			get
			{
				return dataBoundField;
			}
			set
			{
				dataBoundField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool rowNumbers
		{
			get
			{
				return rowNumbersField;
			}
			set
			{
				rowNumbersField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool fillFormulas
		{
			get
			{
				return fillFormulasField;
			}
			set
			{
				fillFormulasField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool clipped
		{
			get
			{
				return clippedField;
			}
			set
			{
				clippedField = value;
			}
		}

		[DefaultValue(typeof(uint), "0")]
		[XmlAttribute]
		public uint tableColumnId
		{
			get
			{
				return tableColumnIdField;
			}
			set
			{
				tableColumnIdField = value;
			}
		}

		public CT_QueryTableField()
		{
			dataBoundField = true;
			rowNumbersField = false;
			fillFormulasField = false;
			clippedField = false;
			tableColumnIdField = 0u;
		}
	}
}
