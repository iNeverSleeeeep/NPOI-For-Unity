using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CT_QueryTableFields
	{
		private List<CT_QueryTableField> queryTableFieldField;

		private uint countField;

		[XmlElement("queryTableField")]
		public List<CT_QueryTableField> queryTableField
		{
			get
			{
				return queryTableFieldField;
			}
			set
			{
				queryTableFieldField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "0")]
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

		public CT_QueryTableFields()
		{
			countField = 0u;
			queryTableFieldField = new List<CT_QueryTableField>();
		}
	}
}
