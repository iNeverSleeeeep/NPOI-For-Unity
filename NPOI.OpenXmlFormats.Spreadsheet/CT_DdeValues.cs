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
	public class CT_DdeValues
	{
		private CT_DdeValue[] valueField;

		private uint rowsField;

		private uint colsField;

		[XmlElement("value")]
		public CT_DdeValue[] value
		{
			get
			{
				return valueField;
			}
			set
			{
				valueField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "1")]
		public uint rows
		{
			get
			{
				return rowsField;
			}
			set
			{
				rowsField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "1")]
		public uint cols
		{
			get
			{
				return colsField;
			}
			set
			{
				colsField = value;
			}
		}

		public CT_DdeValues()
		{
			rowsField = 1u;
			colsField = 1u;
		}
	}
}
